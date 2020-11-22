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

        private readonly string connectionString = "Data Source=" + Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\" +
            Application.ProductName + "\\wheel_tension.sqlite3;Version=3;";

        private bool isTrackbarMouseDown = false;
        private bool isTrackbarScrolling = false;

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
            Text = $"{Application.ProductName} {Application.ProductVersion}";
            Update();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN (fixing blinking controls)
                return parms;
            }
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
            varianceTrackBar.Enabled = true;
        }

        private void leftSideSpokeCountComboBox_TextChanged(object sender, EventArgs e)
        {
            var leftSideSpokesCount = Int16.Parse(leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem));

            int indentFromComboBox = formControl.GetOffsetFromControl(leftSideSpokeCountComboBox) + stepBetweenControls;

            formControl.AddNumericUpDownToGroupBox(
                leftSideSpokesGroupBox,
                "leftSideSpokesTm1ReadingNumericUpDown",
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
                "leftSideSpokesTensionTextBox",
                textBoxProperties,
                leftSideSpokeCountComboBox.Size.Width,
                leftSideSpokeCountComboBox.Size.Height,
                leftSideSpokesCount,
                stepBetweenControls,
                tensionLeftSpokesLabel.Location.X,
                indentFromComboBox
            );

            foreach (NumericUpDown item in leftSideSpokesGroupBox.Controls.OfType<NumericUpDown>())
            {
                if (item.Name.IndexOf("leftSideSpokesTm1ReadingNumericUpDown") > -1)
                {
                    item.Enter += new EventHandler(makeNumericUpDownSelection);
                }
            }
        }

        private void makeNumericUpDownSelection(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;

            int length = numericUpDown.Value.ToString().Length;
            numericUpDown.Select(0, length);
        }

        private void rightSideSpokeCountComboBox_TextChanged(object sender, EventArgs e)
        {
            var rightSideSpokesCount = Int16.Parse(rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem));

            int indentFromComboBox = formControl.GetOffsetFromControl(rightSideSpokeCountComboBox) + stepBetweenControls;

            formControl.AddNumericUpDownToGroupBox(
                rightSideSpokesGroupBox,
                "rightSideSpokesTm1ReadingNumericUpDown",
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
                "rightSideSpokesTensionTextBox",
                textBoxProperties,
                rightSideSpokeCountComboBox.Size.Width,
                rightSideSpokeCountComboBox.Size.Height,
                rightSideSpokesCount,
                stepBetweenControls,
                tensionLeftSpokesLabel.Location.X,
                indentFromComboBox
            );

            foreach (NumericUpDown item in rightSideSpokesGroupBox.Controls.OfType<NumericUpDown>())
            {
                if (item.Name.IndexOf("rightSideSpokesTm1ReadingNumericUpDown") > -1)
                {
                    item.Enter += new EventHandler(makeNumericUpDownSelection);
                }
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);
            var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

            if (String.IsNullOrEmpty(leftSideSpokeCountComboBoxSelected) || String.IsNullOrEmpty(rightSideSpokeCountComboBoxSelected))
            {
                MessageBox.Show("Number of spokes not selected!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (leftSideSpokeCountComboBoxSelected != rightSideSpokeCountComboBoxSelected)
                {
                    MessageBox.Show("Your wheel isn't symmetrical!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                List<float> leftSideSpokesTm1 = formControl.GetValuesFromGroupControls(
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTm1ReadingNumericUpDown").Select(x => float.Parse(x.Value)).ToList();

                List<float> rightSideSpokesTm1 = formControl.GetValuesFromGroupControls(
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTm1ReadingNumericUpDown").Select(x => float.Parse(x.Value)).ToList();

                List<float> leftSpokesAngles = parameterCalculations.CalculateSpokeAngles(leftSideSpokesTm1);
                List<float> rightSpokesAngles = parameterCalculations.CalculateSpokeAngles(rightSideSpokesTm1);

                float[] leftSideSpokesTensionKgf = parameterCalculations.CalculateTensionKgf(
                    conversionTableGridView,
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTm1ReadingNumericUpDown");

                float[] rightSideSpokesTensionKgf = parameterCalculations.CalculateTensionKgf(
                    conversionTableGridView,
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTm1ReadingNumericUpDown");

                spokeTensionChart.Series.Clear();

                spokeTensionChart.ChartAreas["ChartArea"].AxisY.Maximum = new List<float> { leftSideSpokesTm1.Max(), rightSideSpokesTm1.Max() }.Max() * 2.0;

                formControl.SetValuesToGroupControlsText(
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTensionTextBox",
                    leftSideSpokesTensionKgf.Select(x => x.ToString()).ToArray());

                formControl.SetValuesToGroupControlsText(
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTensionTextBox",
                    rightSideSpokesTensionKgf.Select(x => x.ToString()).ToArray());

                tensionChart.DrawTension(spokeTensionChart, "Left Side Spokes", leftSpokesAngles, leftSideSpokesTm1);
                tensionChart.DrawTension(spokeTensionChart, "Right Side Spokes", rightSpokesAngles, rightSideSpokesTm1);

                standartDevLeftSpokesTensionTextBox.Text = parameterCalculations.StdDev(leftSideSpokesTensionKgf.ToList()).ToString();
                standartDevRightSpokesTensionTextBox.Text = parameterCalculations.StdDev(rightSideSpokesTensionKgf.ToList()).ToString();

                averageLeftSpokesTensionTextBox.Text = leftSideSpokesTensionKgf.Average().ToString();
                averageRightSpokesTensionTextBox.Text = rightSideSpokesTensionKgf.Average().ToString();

                int variance = varianceTrackBar.Value;

                leftSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageLeftSpokesTensionTextBox.Text), variance, true).ToString();

                leftSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageLeftSpokesTensionTextBox.Text), variance, false).ToString();

                rightSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), variance, true).ToString();

                rightSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), variance, false).ToString();

                var leftSpokesLowerTensionLimit = double.Parse(leftSpokesLowerTensionLimitTextBox.Text);
                var leftSpokesUpperTensionLimit = double.Parse(leftSpokesUpperTensionLimitTextBox.Text);
                var rightSpokesLowerTensionLimit = double.Parse(rightSpokesLowerTensionLimitTextBox.Text);
                var rightSpokesUpperTensionLimit = double.Parse(rightSpokesUpperTensionLimitTextBox.Text);

                parameterCalculations.SetWithinTensionLimit(
                    leftSideSpokesGroupBox,
                    withinTensionLimitLeftSpokesLabel,
                    errorProviderTensionLimitError,
                    "leftSideSpokesTensionTextBox",
                    leftSpokesLowerTensionLimit,
                    leftSpokesUpperTensionLimit);

                parameterCalculations.SetWithinTensionLimit(
                    rightSideSpokesGroupBox,
                    withinTensionLimitRightSpokesLabel,
                    errorProviderTensionLimitError,
                    "rightSideSpokesTensionTextBox",
                    rightSpokesLowerTensionLimit,
                    rightSpokesUpperTensionLimit);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Select file to save",
                Filter = "Config files (*.config)|*.config",
                RestoreDirectory = true
            };

            saveFileDialog.ShowDialog();

            var appSettingPath = saveFileDialog.FileName;

            SaveSettings(appSettingPath);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select file to open",
                Filter = "Config files (*.config)|*.config",
                RestoreDirectory = true
            };

            openFileDialog.ShowDialog();

            var appSettingPath = openFileDialog.FileName;

            LoadSettings(appSettingPath);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void leftSideSpokeCountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);

            if (!String.IsNullOrEmpty(leftSideSpokeCountComboBoxSelected))
            {
                leftSideSpokeCountComboBox_TextChanged(leftSideSpokeCountComboBox, e);
            }

            SendKeys.SendWait("{ENTER}");
        }

        private void rightSideSpokeCountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

            if (!String.IsNullOrEmpty(rightSideSpokeCountComboBoxSelected))
            {
                rightSideSpokeCountComboBox_TextChanged(rightSideSpokeCountComboBox, e);
            }

            SendKeys.SendWait("{ENTER}");
        }

        private void LoadSettings(string appSettingPath)
        {
            if (!String.IsNullOrEmpty(appSettingPath))
            {
                var appSettings = new AppSettings(appSettingPath);

                var settings = appSettings.LoadSettings();

                try
                {
                    var leftSideSpokesTm1ReadingNumericUpDownValues = new List<string>();
                    var rightSideSpokesTm1ReadingNumericUpDownValues = new List<string>();

                    materialComboBox.SelectedItem = settings["materialComboBoxSelectedItem"];
                    shapeComboBox.SelectedItem = settings["shapeComboBoxSelectedItem"];
                    thicknessComboBox.SelectedItem = settings["thicknessComboBoxSelectedItem"];
                    varianceTrackBar.Value = int.Parse(settings["varianceTrackBarValue"]);
                    leftSideSpokeCountComboBox.SelectedItem = settings["leftSideSpokeCountComboBoxSelectedItem"];
                    rightSideSpokeCountComboBox.SelectedItem = settings["rightSideSpokeCountComboBoxSelectedItem"];

                    if (!String.IsNullOrEmpty(settings["leftSideSpokeCountComboBoxSelectedItem"]))
                    {
                        for (int i = 0; i < int.Parse(settings["leftSideSpokeCountComboBoxSelectedItem"]); i++)
                        {
                            var key = $"leftSideSpokesTm1ReadingNumericUpDown{i + 1}";

                            if (!String.IsNullOrEmpty(settings[key]))
                            {
                                leftSideSpokesTm1ReadingNumericUpDownValues.Add(settings[key]);
                            }
                        }

                        formControl.SetValuesToGroupControlsText(
                            leftSideSpokesGroupBox,
                            "leftSideSpokesTm1ReadingNumericUpDown",
                            leftSideSpokesTm1ReadingNumericUpDownValues.ToArray());
                    }

                    if (!String.IsNullOrEmpty(settings["rightSideSpokeCountComboBoxSelectedItem"]))
                    {
                        for (int i = 0; i < int.Parse(settings["rightSideSpokeCountComboBoxSelectedItem"]); i++)
                        {
                            var key = $"rightSideSpokesTm1ReadingNumericUpDown{i + 1}";

                            if (!String.IsNullOrEmpty(settings[key]))
                            {
                                rightSideSpokesTm1ReadingNumericUpDownValues.Add(settings[key]);
                            }
                        }

                        formControl.SetValuesToGroupControlsText(
                            rightSideSpokesGroupBox,
                            "rightSideSpokesTm1ReadingNumericUpDown",
                            rightSideSpokesTm1ReadingNumericUpDownValues.ToArray());
                    }

                    if (!String.IsNullOrEmpty(settings["leftSideSpokeCountComboBoxSelectedItem"]) &&
                        !String.IsNullOrEmpty(settings["rightSideSpokeCountComboBoxSelectedItem"]))
                    {
                        calculateButton.PerformClick();
                    }
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show($"The settings hasn't been loaded!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SaveSettings(string appSettingPath)
        {
            if (!String.IsNullOrEmpty(appSettingPath))
            {
                var appSettings = new AppSettings(appSettingPath);

                var settings = new Dictionary<string, string>();

                var materialComboBoxSelected = materialComboBox.GetItemText(materialComboBox.SelectedItem);
                var shapeComboBoxSelected = shapeComboBox.GetItemText(shapeComboBox.SelectedItem);
                var thicknessComboBoxSelected = thicknessComboBox.GetItemText(thicknessComboBox.SelectedItem);
                var varianceTrackBarValue = varianceTrackBar.Value.ToString();
                var leftSideSpokeCountComboBoxSelected = leftSideSpokeCountComboBox.GetItemText(leftSideSpokeCountComboBox.SelectedItem);
                var rightSideSpokeCountComboBoxSelected = rightSideSpokeCountComboBox.GetItemText(rightSideSpokeCountComboBox.SelectedItem);

                List<string> leftSideSpokesTm1ReadingNumericUpDownValues = formControl.GetValuesFromGroupControls(
                    leftSideSpokesGroupBox,
                    "leftSideSpokesTm1ReadingNumericUpDown").Values.ToList();

                List<string> rightSideSpokesTm1ReadingNumericUpDownValues = formControl.GetValuesFromGroupControls(
                    rightSideSpokesGroupBox,
                    "rightSideSpokesTm1ReadingNumericUpDown").Values.ToList();

                settings.Add("materialComboBoxSelectedItem", materialComboBoxSelected);
                settings.Add("shapeComboBoxSelectedItem", shapeComboBoxSelected);
                settings.Add("thicknessComboBoxSelectedItem", thicknessComboBoxSelected);
                settings.Add("varianceTrackBarValue", varianceTrackBarValue);
                settings.Add("leftSideSpokeCountComboBoxSelectedItem", leftSideSpokeCountComboBoxSelected);
                settings.Add("rightSideSpokeCountComboBoxSelectedItem", rightSideSpokeCountComboBoxSelected);

                for (int i = 0; i < leftSideSpokesTm1ReadingNumericUpDownValues.Count; i++)
                {
                    settings.Add($"leftSideSpokesTm1ReadingNumericUpDown{i + 1}", leftSideSpokesTm1ReadingNumericUpDownValues[i]);
                }

                for (int i = 0; i < rightSideSpokesTm1ReadingNumericUpDownValues.Count; i++)
                {
                    settings.Add($"rightSideSpokesTm1ReadingNumericUpDown{i + 1}", rightSideSpokesTm1ReadingNumericUpDownValues[i]);
                }

                appSettings.SaveSettings(settings);
            }
        }

        private void varianceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var varianceTrackBarValue = varianceTrackBar.Value;

            varianceValueLabel.Text = $"{varianceTrackBarValue}%";
            withinTensionLimitLeftSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
            withinTensionLimitRightSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
            leftSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Upper Tension Limit (kgf)";
            rightSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Lower Tension Limit (kgf)";
            leftSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Upper Tension Limit (kgf)";
            rightSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Lower Tension Limit (kgf)";
        }

        private void varianceTrackBar_Scroll(object sender, EventArgs e)
        {
            isTrackbarScrolling = true;
        }

        private void varianceTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (isTrackbarMouseDown == true && isTrackbarScrolling == true)
            {
                var varianceTrackBarValue = varianceTrackBar.Value;

                withinTensionLimitLeftSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
                withinTensionLimitRightSpokesLabel.Text = $"Within {varianceTrackBarValue}% limit";
                leftSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Upper Tension Limit (kgf)";
                rightSpokesUpperTensionLimitLabel.Text = $"+{varianceTrackBarValue}% Lower Tension Limit (kgf)";
                leftSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Upper Tension Limit (kgf)";
                rightSpokesLowerTensionLimitLabel.Text = $"-{varianceTrackBarValue}% Lower Tension Limit (kgf)";

                leftSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                        double.Parse(averageLeftSpokesTensionTextBox.Text), varianceTrackBarValue, true).ToString();

                leftSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageLeftSpokesTensionTextBox.Text), varianceTrackBarValue, false).ToString();

                rightSpokesLowerTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), varianceTrackBarValue, true).ToString();

                rightSpokesUpperTensionLimitTextBox.Text = parameterCalculations.TensionLimit(
                    double.Parse(averageRightSpokesTensionTextBox.Text), varianceTrackBarValue, false).ToString();

                var leftSpokesLowerTensionLimit = double.Parse(leftSpokesLowerTensionLimitTextBox.Text);
                var leftSpokesUpperTensionLimit = double.Parse(leftSpokesUpperTensionLimitTextBox.Text);
                var rightSpokesLowerTensionLimit = double.Parse(rightSpokesLowerTensionLimitTextBox.Text);
                var rightSpokesUpperTensionLimit = double.Parse(rightSpokesUpperTensionLimitTextBox.Text);

                parameterCalculations.SetWithinTensionLimit(
                    leftSideSpokesGroupBox,
                    withinTensionLimitLeftSpokesLabel,
                    errorProviderTensionLimitError,
                    "leftSideSpokesTensionTextBox",
                    leftSpokesLowerTensionLimit,
                    leftSpokesUpperTensionLimit);

                parameterCalculations.SetWithinTensionLimit(
                    rightSideSpokesGroupBox,
                    withinTensionLimitRightSpokesLabel,
                    errorProviderTensionLimitError,
                    "rightSideSpokesTensionTextBox",
                    rightSpokesLowerTensionLimit,
                    rightSpokesUpperTensionLimit);

            }

            isTrackbarMouseDown = false;
            isTrackbarScrolling = false;
        }

        private void varianceTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            isTrackbarMouseDown = true;
        }
    }
}

