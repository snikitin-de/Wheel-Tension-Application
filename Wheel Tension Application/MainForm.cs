// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    public partial class MainForm : Form
    {
        private readonly int stepBetweenControls = 3;

        private readonly string connectionString = "Data Source=" + Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory())) +
        "\\wheel_tension.sqlite3;Version=3;";

        private readonly List<string> numericUpDownProperties = new List<string>() { "Minimum", "Maximum", "DecimalPlaces", "Increment", "Size" };
        private readonly List<string> textBoxProperties = new List<string>() { "Enabled", "Size" };

        private Dictionary<string, string> parameters = new Dictionary<string, string>() { };

        private Database database = new Database();
        private FormControls formControl = new FormControls();
        private TensionChart tensionChart = new TensionChart();
        private ParameterCalculations parameterCalculations = new ParameterCalculations();


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

            List<string> materialsList = database.ExecuteSelectQuery(connectionString, materialsListCommand, parameters);

            formControl.SetComboBoxValue(materialComboBox, materialsList, true, true);
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

            List<string> shapesList = database.ExecuteSelectQuery(connectionString, shapesListCommand, parameters);

            parameterCalculations.CalculateTensionKgf(conversionTableGridView, leftSideSpokesGroupBox, "leftSideSpokesNumericUpDown", "leftSideSpokesTextBox");
            parameterCalculations.CalculateTensionKgf(conversionTableGridView, rightSideSpokesGroupBox, "rightSideSpokesNumericUpDown", "rightSideSpokesTextBox");

            formControl.SetComboBoxValue(shapeComboBox, shapesList, true, true);
            formControl.SetComboBoxValue(thicknessComboBox, null, false, false);
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

            List<string> thicknessList = database.ExecuteSelectQuery(connectionString, thicknessListCommand, parameters);

            formControl.SetComboBoxValue(thicknessComboBox, thicknessList, true, true);
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

            List<string> tm1Deflection = database.ExecuteSelectQuery(connectionString, tm1ListCommand, parameters);
            List<string> tension = database.ExecuteSelectQuery(connectionString, tensionListCommand, parameters);

            var rowHeaders = new string[] { "TM-1 READING", "SPOKE TENSION (KGF)" };
            var rows = new List<string[]> { tm1Deflection.ToArray(), tension.ToArray() };

            formControl.SetDataGridViewValues(conversionTableGridView, tm1Deflection.Count, rowHeaders, rows);

            leftSideSpokeCountComboBox.Enabled = true;
            rightSideSpokeCountComboBox.Enabled = true;
        }

        private void leftSideSpokeCountComboBox_TextChanged(object sender, EventArgs e)
        {
            var leftSideSpokesCount = Int16.Parse(leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem));

            int indentFromComboBox = formControl.GetOffsetFromControl(leftSideSpokeCountComboBox) + stepBetweenControls;

            formControl.AddNumericUpDownToGroupBox(
                leftSideSpokesGroupBox,
                "leftSideSpokesNumericUpDown",
                numericUpDownProperties,
                leftSideSpokeCountComboBox.Size.Width,
                leftSideSpokeCountComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                leftSideSpokeCountComboBox.Location.X,
                indentFromComboBox
            );

            formControl.AddTextBoxToGroupBox(
                leftSideSpokesGroupBox,
                "leftSideSpokesTextBox",
                textBoxProperties,
                leftSideSpokeCountComboBox.Size.Width,
                leftSideSpokeCountComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                tensionLeftSpokesLabel.Location.X,
                indentFromComboBox
            );
        }

        private void rightSideSpokeCountComboBox_TextChanged(object sender, EventArgs e)
        {
            var rightSideSpokesCount = Int16.Parse(rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem));

            int indentFromComboBox = formControl.GetOffsetFromControl(rightSideSpokeCountComboBox) + stepBetweenControls;

            formControl.AddNumericUpDownToGroupBox(
                rightSideSpokesGroupBox,
                "rightSideSpokesNumericUpDown",
                numericUpDownProperties,
                rightSideSpokeCountComboBox.Size.Width,
                rightSideSpokeCountComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                rightSideSpokeCountComboBox.Location.X,
                indentFromComboBox
            );

            formControl.AddTextBoxToGroupBox(
                rightSideSpokesGroupBox,
                "rightSideSpokesTextBox",
                textBoxProperties,
                rightSideSpokeCountComboBox.Size.Width,
                rightSideSpokeCountComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                tensionLeftSpokesLabel.Location.X,
                indentFromComboBox
            );
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);
            var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

            List<float> leftSideSpokesTm1 = formControl.GetValuesFromGroupControls(leftSideSpokesGroupBox, "leftSideSpokesNumericUpDown").Select(x => float.Parse(x)).ToList();
            List<float> rightSideSpokesTm1 = formControl.GetValuesFromGroupControls(rightSideSpokesGroupBox, "rightSideSpokesNumericUpDown").Select(x => float.Parse(x)).ToList();

            spokeTensionChart.Series.Clear();

            if (leftSideSpokeCountComboBoxSelected == String.Empty || rightSideSpokeCountComboBoxSelected == String.Empty)
            {
                MessageBox.Show("Number of spokes not selected!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                spokeTensionChart.ChartAreas["ChartArea"].AxisY.Maximum = new List<float> { leftSideSpokesTm1.Max(), rightSideSpokesTm1.Max() }.Max() * 2.0;

                if (leftSideSpokeCountComboBoxSelected != rightSideSpokeCountComboBoxSelected)
                {
                    MessageBox.Show("Your wheel isn't symmetrical!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                List<float> leftSpokesAngles = parameterCalculations.CalculateSpokeAngles(leftSideSpokesTm1);
                List<float> rightSpokesAngles = parameterCalculations.CalculateSpokeAngles(rightSideSpokesTm1);

                tensionChart.DrawTension(spokeTensionChart, "Left Side Spokes", leftSpokesAngles, leftSideSpokesTm1);
                tensionChart.DrawTension(spokeTensionChart, "Right Side Spokes", rightSpokesAngles, rightSideSpokesTm1);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }
    }
}

