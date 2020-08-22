using System;
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
        readonly private List<string> numericUpDownProperties = new List<string>() { "Minimum", "Maximum", "DecimalPlaces", "Increment", "Size" };

        readonly private string connectionString = "Data Source=" + Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + 
        "\\wheel_tension.sqlite3;Version=3;";

        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void DrawChart(string SeriesName, List<float> tm1Reading)
        {
            List<float> spokesAngles = CalculateSpokeAngles(tm1Reading);

            spokesAngles.Add(360);
            tm1Reading.Add(tm1Reading[0]);

            chart.ChartAreas["ChartArea"].BackColor = ColorTranslator.FromHtml("#E5ECF6");
            chart.ChartAreas["ChartArea"].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["ChartArea"].AxisY.MajorGrid.LineColor = Color.White;

            chart.Series.Add(SeriesName);
            chart.Series[SeriesName].BorderWidth = 2;
            chart.Series[SeriesName].ChartType = SeriesChartType.Polar;
            chart.Series[SeriesName].MarkerStyle = MarkerStyle.Circle;
            chart.Series[SeriesName].MarkerSize = 5;

            float angle;
            float tm1;

            for (var i = 0; i < spokesAngles.Count; i++)
            {
                int spoke = i + 1;

                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                chart.Series[SeriesName].Points.AddXY(angle, tm1);
                chart.Series[SeriesName].Points[i].ToolTip = $"TM-1 Reading: #VALY \nAngle: #VALX \nSpoke: {spoke}";
                chart.Series[SeriesName].Points[i].Label = spoke.ToString();
            }

            for (var i = 0; i < spokesAngles.Count; i++)
            {
                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                chart.Series[SeriesName].Points.AddXY(0, 0);
                chart.Series[SeriesName].Points.AddXY(angle, tm1);
            }
        }

        private List<float> CalculateSpokeAngles(List<float> tm1Reading)
        {
            var tm1ReadingLength = tm1Reading.Count;

            float angle = 0;

            List<float> angles = new List<float>(tm1ReadingLength);


            float angleStep;
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
            var itemsCount = groupBox.Controls.OfType<Control>().Count();

            int indent = indentY;

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

            for (int i = 0; i < count; i++)
            {
                var indexAdd = i + 1;

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

        private void AddNumericUpDownToGroupBox(ComboBox comboBox, GroupBox groupBox, string name, int stepControl)
        {
            var selected = comboBox.GetItemText(comboBox.SelectedItem);

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
                groupBox,
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
            var values = new List<float>() { };

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
            var text = new List<string>();

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

        private void SetComboBoxValue(ComboBox comboBox, List<string> values, bool isAddRange, bool isEnabled)
        {
            comboBox.Items.Clear();

            if (isAddRange)
            {
                comboBox.Items.AddRange(values.ToArray());
            }
            
            comboBox.Enabled = isEnabled;
        }

        private void SetDataGridViewValues(int columnCount, string[] rowHeaders, List<string[]> rows)
        {
            conversionTableGridView.ColumnCount = columnCount;
            conversionTableGridView.RowHeadersWidth = 165;

            try
            {
                int columnsWidth = (conversionTableGridView.Width - conversionTableGridView.RowHeadersWidth) / conversionTableGridView.ColumnCount;

                for (var i = 0; i < columnCount; i++)
                {
                    conversionTableGridView.Columns[i].Name = "";
                    conversionTableGridView.Columns[i].Width = columnsWidth;
                }

                conversionTableGridView.Rows.Clear();

                if (rows.Count != rowHeaders.Length)
                {
                    MessageBox.Show("Number of row headers must be equal number of rows!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                {
                    for (var i = 0; i < rows.Count; i++)
                    {
                        conversionTableGridView.Rows.Add(rows[i]);
                        conversionTableGridView.Rows[i].HeaderCell.Value = rowHeaders[i];
                    }
                }
            }
            catch (System.DivideByZeroException)
            {
                MessageBox.Show("There are no such parameters in the database!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var materialsListCommand = @"
                SELECT material
                FROM tm1_conversion_table
                GROUP BY material";

            List<string> materialsList = GetWheelParameterFromDB(connectionString, materialsListCommand, parameters);

            SetComboBoxValue(materialComboBox, materialsList, true, true);
        }

        private void leftSideComboBox_TextChanged(object sender, EventArgs e)
        {
            var stepControl = 3;

            AddNumericUpDownToGroupBox(leftSideComboBox, wheelTensionGroupBox, "leftSideSpokesNumericUpDown", stepControl);
        }

        private void rightSideComboBox_TextChanged(object sender, EventArgs e)
        {
            var stepControl = 3;

            AddNumericUpDownToGroupBox(rightSideComboBox, wheelTensionGroupBox, "rightSideSpokesNumericUpDown", stepControl);
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            var leftSideComboBoxSelected = leftSideComboBox.GetItemText(leftSideComboBox.SelectedItem);
            var rightSideComboBoxSelected = rightSideComboBox.GetItemText(rightSideComboBox.SelectedItem);

            List<float> leftSideSpokesTm1 = GetWheelTensions("leftSideSpokesNumericUpDown");
            List<float> rightSideSpokesTm1 = GetWheelTensions("rightSideSpokesNumericUpDown");

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

                DrawChart("Left Side Spokes", leftSideSpokesTm1);
                DrawChart("Right Side Spokes", rightSideSpokesTm1);
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

            List<string> thicknessList = GetWheelParameterFromDB(connectionString, thicknessListCommand, parameters);

            SetComboBoxValue(thicknessComboBox, thicknessList, true, true);
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

            List<string> shapesList = GetWheelParameterFromDB(connectionString, shapesListCommand, parameters);

            SetComboBoxValue(shapeComboBox, shapesList, true, true);
            SetComboBoxValue(thicknessComboBox, null, false, false);
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

            List<string> tm1Deflection = GetWheelParameterFromDB(connectionString, tm1ListCommand, parameters);
            List<string> tension = GetWheelParameterFromDB(connectionString, tensionListCommand, parameters);

            var rowHeaders = new string[] { "TM-1 READING", "SPOKE TENSION (KGF)" };
            var rows = new List<string[]> { tm1Deflection.ToArray(), tension.ToArray() };

            SetDataGridViewValues(tm1Deflection.Count, rowHeaders, rows);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }
    }
}
