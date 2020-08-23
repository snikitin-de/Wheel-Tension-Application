// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    public partial class MainForm : Form
    {
        readonly private List<string> numericUpDownProperties = new List<string>() { "Minimum", "Maximum", "DecimalPlaces", "Increment", "Size" };

        readonly private string connectionString = "Data Source=" + Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + 
        "\\wheel_tension.sqlite3;Version=3;";

        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var materialsListCommand = @"
                SELECT material
                FROM tm1_conversion_table
                GROUP BY material";

            var database = new Database();

            List<string> materialsList = database.ExecuteSelectQuery(connectionString, materialsListCommand, parameters);

            var formControl = new FormControls();

            formControl.SetComboBoxValue(materialComboBox, materialsList, true, true);
        }

        private void leftSideComboBox_TextChanged(object sender, EventArgs e)
        {
            var stepBetweenControls = 3;

            var formControl = new FormControls();

            var leftSideSpokesCount = Int16.Parse(leftSideComboBox.GetItemText(leftSideComboBox.SelectedItem));

            int indentFromComboBox = formControl.GetOffsetFromControl(leftSideComboBox) + stepBetweenControls;

            formControl.AddNumericUpDownToGroupBox(
                wheelTensionGroupBox,
                "leftSideSpokesNumericUpDown",
                numericUpDownProperties,
                leftSideComboBox.Size.Width,
                leftSideComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                leftSideComboBox.Location.X,
                indentFromComboBox
            );
        }

        private void rightSideComboBox_TextChanged(object sender, EventArgs e)
        {
            var stepBetweenControls = 3;

            var formControl = new FormControls();

            var rightSideSpokesCount = Int16.Parse(rightSideComboBox.GetItemText(rightSideComboBox.SelectedItem));

            int indentFromComboBox = formControl.GetOffsetFromControl(rightSideComboBox) + stepBetweenControls;

            formControl.AddNumericUpDownToGroupBox(
                wheelTensionGroupBox,
                "rightSideSpokesNumericUpDown",
                numericUpDownProperties,
                rightSideComboBox.Size.Width,
                rightSideComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                rightSideComboBox.Location.X,
                indentFromComboBox
            );
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            var leftSideComboBoxSelected = leftSideComboBox.GetItemText(leftSideComboBox.SelectedItem);
            var rightSideComboBoxSelected = rightSideComboBox.GetItemText(rightSideComboBox.SelectedItem);

            var formControl = new FormControls();

            List<float> leftSideSpokesTm1 = formControl.GetWheelTensions(wheelTensionGroupBox, "leftSideSpokesNumericUpDown");
            List<float> rightSideSpokesTm1 = formControl.GetWheelTensions(wheelTensionGroupBox, "rightSideSpokesNumericUpDown");

            chart.Series.Clear();

            if (leftSideComboBoxSelected == String.Empty || rightSideComboBoxSelected == String.Empty)
            {
                MessageBox.Show("Number of spokes not selected!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                chart.ChartAreas["ChartArea"].AxisY.Maximum = new List<float> { leftSideSpokesTm1.Max(), rightSideSpokesTm1.Max() }.Max() * 2.0;

                if (leftSideComboBoxSelected != rightSideComboBoxSelected)
                {
                    MessageBox.Show("Your wheel isn't symmetrical!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                var tensionChart = new TensionChart();
                var parameterCalculations = new ParameterCalculations();

                List<float> leftSpokesAngles = parameterCalculations.CalculateSpokeAngles(leftSideSpokesTm1);
                List<float> rightSpokesAngles = parameterCalculations.CalculateSpokeAngles(rightSideSpokesTm1);

                tensionChart.DrawTension(chart, "Left Side Spokes", leftSpokesAngles, leftSideSpokesTm1);
                tensionChart.DrawTension(chart, "Right Side Spokes", rightSpokesAngles, rightSideSpokesTm1);
            }
        }

        private void shapeComboBox_TextChanged(object sender, EventArgs e)
        {
            var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
            var shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);

            var thicknessListCommand = @"
                SELECT thickness
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape
                GROUP BY thickness";

            parameters = new Dictionary<string, string>()
            {
                { "@material", materialComboBoxSelected },
                { "@shape", shapeComboBoxSelected }
            };

            var database = new Database();
            var formControl = new FormControls();

            List<string> thicknessList = database.ExecuteSelectQuery(connectionString, thicknessListCommand, parameters);

            formControl.SetComboBoxValue(thicknessComboBox, thicknessList, true, true);
        }

        private void materialComboBox_TextChanged(object sender, EventArgs e)
        {
            var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);

            var shapesListCommand = @"
                SELECT shape
                FROM tm1_conversion_table
                WHERE material = @material
                GROUP BY shape";

            parameters = new Dictionary<string, string>()
            {
                { "@material", materialComboBoxSelected }
            };

            var database = new Database();
            var formControl = new FormControls();

            List<string> shapesList = database.ExecuteSelectQuery(connectionString, shapesListCommand, parameters);

            formControl.SetComboBoxValue(shapeComboBox, shapesList, true, true);
            formControl.SetComboBoxValue(thicknessComboBox, null, false, false);
        }

        private void thicknessComboBox_TextChanged(object sender, EventArgs e)
        {
            var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
            var shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);
            var thicknessComboBoxSelected = thicknessComboBox.GetItemText(thicknessComboBox.SelectedItem);

            var tm1ListCommand = @"
                SELECT tm1_deflection_reading
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape AND thickness = @thickness
                GROUP BY tm1_deflection_reading";
            var tensionListCommand = @"
                SELECT tension
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape AND thickness = @thickness
                GROUP BY tension";

            parameters = new Dictionary<string, string>()
            {
                { "@material", materialComboBoxSelected },
                { "@shape", shapeComboBoxSelected },
                { "@thickness", thicknessComboBoxSelected }
            };

            var database = new Database();

            List<string> tm1Deflection = database.ExecuteSelectQuery(connectionString, tm1ListCommand, parameters);
            List<string> tension = database.ExecuteSelectQuery(connectionString, tensionListCommand, parameters);

            var rowHeaders = new string[] { "TM-1 READING", "SPOKE TENSION (KGF)" };
            var rows = new List<string[]> { tm1Deflection.ToArray(), tension.ToArray() };
            var formControl = new FormControls();

            formControl.SetDataGridViewValues(conversionTableGridView, tm1Deflection.Count, rowHeaders, rows);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }
    }
}
