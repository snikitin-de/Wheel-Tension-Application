﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Wheel_Tension_Application
{
    public partial class MainForm : Form
    {
        readonly private Dictionary<string, Dictionary<string, List<string>>> wheel_spokes = new Dictionary<string, Dictionary<string, List<string>>>();
        readonly private Dictionary<string, List<string>> shape = new Dictionary<string, List<string>>();

        readonly private List<string> numericUpDownProperties = new List<string>() { "Minimum", "Maximum", "DecimalPlaces", "Increment", "Size" };

        readonly private string connectionString = "Data Source=" + Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + 
        "\\wheel_tension.sqlite3;Version=3;";

        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void DrawChart(string SeriesName, List<float> tm1Reading, Color color)
        {
            List<float> spokesAngles = CalculateSpokeAngles(tm1Reading);

            spokesAngles.Add(360);
            tm1Reading.Add(tm1Reading[0]);

            chart.ChartAreas["ChartArea"].BackColor = System.Drawing.ColorTranslator.FromHtml("#E5ECF6");
            chart.ChartAreas["ChartArea"].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["ChartArea"].AxisY.MajorGrid.LineColor = Color.White;

            chart.Series.Add(SeriesName);
            chart.Series[SeriesName].BorderWidth = 2;
            chart.Series[SeriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            chart.Series[SeriesName].MarkerStyle = MarkerStyle.Circle;
            chart.Series[SeriesName].MarkerSize = 5;

            float angle;
            float tm1;

            for (int i = 0; i < spokesAngles.Count; i++)
            {
                int spoke = i + 1;

                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                chart.Series[SeriesName].Points.AddXY(angle, tm1);
                chart.Series[SeriesName].Points[i].ToolTip = $"TM-1 Reading: #VALY \nAngle: #VALX \nSpoke: {spoke}";

                chart.Series[SeriesName].Points[i].Label = spoke.ToString();
            }

            for (int i = 0; i < spokesAngles.Count; i++)
            {
                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                chart.Series[SeriesName].Points.AddXY(0, 0);
                chart.Series[SeriesName].Points.AddXY(angle, tm1);
            }
        }

        private List<float> CalculateSpokeAngles(List<float> tm1Reading)
        {
            int tm1ReadingLength = tm1Reading.Count;

            float angleStep = 0;
            float angle = 0;

            List<float> angles = new List<float>(tm1ReadingLength);

            if (tm1Reading.Count > 0)
            {
                angleStep = (float)360 / tm1ReadingLength;
            }
            else
            {
                angleStep = (float)360 / 24;
            }

            for (int i = 0; i < tm1ReadingLength; i++)
            {
                angles.Add(angle);

                angle += angleStep;
            }

            return angles;
        }

        private void AddGroupControlsToGroupBox(GroupBox groupBox, Control control, List<string> controlProperties, string name, int indentX, int indentY, int controlHeight, int step, int count)
        {
            int itemsCount = groupBox.Controls.OfType<Control>().Count();

            if (count < itemsCount)
            {
                foreach (Control item in groupBox.Controls.OfType<Control>().Reverse())
                {
                    if (item.Name.IndexOf(name) > -1)
                    {
                        groupBox.Controls.Remove(item);
                    }
                }
            }

            int indent = indentY;

            for (int i = 0; i < count; i++)
            {
                int indexAdd = i + 1;

                Type type = control.GetType();

                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                Control newControl = (Control)ctor.Invoke(null);

                newControl.Name = name + indexAdd;
                newControl.Location = new Point(indentX, indent);

                foreach (string property in controlProperties)
                {
                     PropertyInfo propertyInfo = control.GetType().GetProperty(property);
                     propertyInfo.SetValue(newControl, Convert.ChangeType(propertyInfo.GetValue(control), propertyInfo.PropertyType), null);
                } 

                groupBox.Controls.Add(newControl);

                indent += controlHeight + step;
            }
        }

        private void AddNumericUpDownToGroupBox(ComboBox comboBox, string name, int stepControl)
        {
            string selected = comboBox.GetItemText(comboBox.SelectedItem);
            int indentFromComboBox = GetIndentFromComboBox(comboBox, stepControl);

            NumericUpDown numericUpDown = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 44,
                DecimalPlaces = 1,
                Increment = 0.5M,
                Size = new Size(comboBox.Size.Width, comboBox.Size.Height)
            };

            AddGroupControlsToGroupBox(
                wheelTensionGroupBox,
                numericUpDown,
                numericUpDownProperties,
                name,
                comboBox.Location.X,
                indentFromComboBox,
                comboBox.Size.Height,
                stepControl,
                Int16.Parse(selected)
            );
        }

        private int GetIndentFromComboBox(ComboBox comboBox, int stepControl)
        {
            return comboBox.Location.Y + comboBox.Size.Height + stepControl;
        }

        private List<float> GetWheelTensions(string name)
        {
            List<float> values = new List<float>() { };

            foreach (Control item in wheelTensionGroupBox.Controls.OfType<NumericUpDown>().Reverse())
            {
                if (item.Name.IndexOf(name) > -1)
                {
                    values.Add(float.Parse(item.Text));
                }
            }

            return values;
        }

        private List<string> GetWheelParameterFromDB(string connectionString, string command, Dictionary<string, string> parameters)
        {
            List<string> text = new List<string>();

            try
            {
                using (SQLiteConnection objConnection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand objCommand = objConnection.CreateCommand())
                    {
                        objConnection.Open();
                        objCommand.CommandText = command;

                        if (parameters.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> parameter in parameters)
                            {
                                SQLiteParameter param = new SQLiteParameter(parameter.Key) { Value = parameter.Value };

                                objCommand.Parameters.Add(param);
                            }
                        }

                        using (SQLiteDataReader reader = objCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = reader.GetValue(0).ToString();

                                text.Add(row);
                            }
                        }

                        objConnection.Close();
                    }
                }
            }
            catch (SQLiteException exception)
            {
                MessageBox.Show(exception.Message, "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string materialsListCommand = @"
                SELECT material
                FROM tm1_conversion_table
                GROUP BY material";

            parameters = new Dictionary<string, string>();

            List<string> materialsList = GetWheelParameterFromDB(connectionString, materialsListCommand, parameters);

            materialComboBox.Items.Clear();
            materialComboBox.Items.AddRange(materialsList.ToArray());
        }

        private void leftSideComboBox_TextChanged(object sender, EventArgs e)
        {
            int stepControl = 3;
            AddNumericUpDownToGroupBox(leftSideComboBox, "leftSideSpokesNumericUpDown", stepControl);
        }

        private void rightSideComboBox_TextChanged(object sender, EventArgs e)
        {
            int stepControl = 3;
            AddNumericUpDownToGroupBox(rightSideComboBox, "rightSideSpokesNumericUpDown", stepControl);
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            string leftSideComboBoxSelected = leftSideComboBox.GetItemText(leftSideComboBox.SelectedItem);
            string rightSideComboBoxSelected = rightSideComboBox.GetItemText(rightSideComboBox.SelectedItem);

            List<float> leftSideSpokesTm1 = GetWheelTensions("leftSideSpokesNumericUpDown");
            List<float> rightSideSpokesTm1 = GetWheelTensions("rightSideSpokesNumericUpDown");

            chart.Series.Clear();

            Color leftSideSpokesColor = System.Drawing.ColorTranslator.FromHtml("#FFD500");
            Color rightSideSpokesColor = System.Drawing.ColorTranslator.FromHtml("#BA86FF");

            chart.ChartAreas["ChartArea"].AxisY.Maximum = new List<float> { leftSideSpokesTm1.Max(), rightSideSpokesTm1.Max() }.Max() * 2.0;

            if (leftSideComboBoxSelected == String.Empty || rightSideComboBoxSelected == String.Empty)
            {
                MessageBox.Show("Number of spokes not selected!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
            else if (leftSideComboBoxSelected != rightSideComboBoxSelected)
            {
                MessageBox.Show("Your wheel isn't symmetrical!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DrawChart("Left Side Spokes", leftSideSpokesTm1, leftSideSpokesColor);
                DrawChart("Right Side Spokes", rightSideSpokesTm1, rightSideSpokesColor);
            }
            else
            {
                DrawChart("Left Side Spokes", leftSideSpokesTm1, leftSideSpokesColor);
                DrawChart("Right Side Spokes", rightSideSpokesTm1, rightSideSpokesColor);
            }
        }

        private void shapeComboBox_TextChanged(object sender, EventArgs e)
        {
            string materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
            string shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);

            string thicknessListCommand = @"
                SELECT thickness
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape
                GROUP BY thickness";

            parameters = new Dictionary<string, string>();

            parameters.Add("@material", materialComboBoxSelected);
            parameters.Add("@shape", shapeComboBoxSelected);

            List<string> thicknessList = GetWheelParameterFromDB(connectionString, thicknessListCommand, parameters);

            thicknessComboBox.Items.Clear();
            thicknessComboBox.Items.AddRange(thicknessList.ToArray());
            thicknessComboBox.Enabled = true;
        }

        private void materialComboBox_TextChanged(object sender, EventArgs e)
        {
            string materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);

            string shapesListCommand = @"
                SELECT shape
                FROM tm1_conversion_table
                WHERE material = @material
                GROUP BY shape";

            parameters = new Dictionary<string, string>();

            parameters.Add("@material", materialComboBoxSelected);

            List<string> shapesList = GetWheelParameterFromDB(connectionString, shapesListCommand, parameters);

            shapeComboBox.Items.Clear();
            shapeComboBox.Items.AddRange(shapesList.ToArray());
            shapeComboBox.Enabled = true;

            thicknessComboBox.Items.Clear();
            thicknessComboBox.Enabled = false;
        }

        private void thicknessComboBox_TextChanged(object sender, EventArgs e)
        {
            string materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
            string shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);
            string thicknessComboBoxSelected = thicknessComboBox.GetItemText(thicknessComboBox.SelectedItem);

            string tm1ListCommand = @"
                SELECT tm1_deflection_reading
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape AND thickness = @thickness
                GROUP BY tm1_deflection_reading";

            string tensionListCommand = @"
                SELECT tension
                FROM tm1_conversion_table
                WHERE material = @material AND shape = @shape AND thickness = @thickness
                GROUP BY tension";

            parameters = new Dictionary<string, string>();

            parameters.Add("@material", materialComboBoxSelected);
            parameters.Add("@shape", shapeComboBoxSelected);
            parameters.Add("@thickness", thicknessComboBoxSelected);

            List<string> tm1Deflection = GetWheelParameterFromDB(connectionString, tm1ListCommand, parameters);
            List<string> tension = GetWheelParameterFromDB(connectionString, tensionListCommand, parameters);

            conversionTableGridView.ColumnCount = tm1Deflection.Count;
            conversionTableGridView.RowHeadersWidth = 165;

            try
            {
                int columnsWidth = (conversionTableGridView.Width - conversionTableGridView.RowHeadersWidth) / conversionTableGridView.ColumnCount;

                for (int i = 0; i < tm1Deflection.Count; i++)
                {
                    conversionTableGridView.Columns[i].Name = "";
                    conversionTableGridView.Columns[i].Width = columnsWidth;
                }

                conversionTableGridView.Rows.Clear();

                conversionTableGridView.Rows.Add(tm1Deflection.ToArray());
                conversionTableGridView.Rows.Add(tension.ToArray());

                conversionTableGridView.Rows[0].HeaderCell.Value = "TM-1 READING";
                conversionTableGridView.Rows[1].HeaderCell.Value = "SPOKE TENSION (KGF)";
            }
            catch (System.DivideByZeroException)
            {
                MessageBox.Show("There are no such parameters in the database!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }
    }
}
