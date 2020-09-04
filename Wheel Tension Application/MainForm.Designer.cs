// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace Wheel_Tension_Application
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.spokeTensionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.calculateButton = new System.Windows.Forms.Button();
            this.wheelSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.thicknessComboBox = new System.Windows.Forms.ComboBox();
            this.shapeComboBox = new System.Windows.Forms.ComboBox();
            this.materialComboBox = new System.Windows.Forms.ComboBox();
            this.thicknessLabel = new System.Windows.Forms.Label();
            this.shapeLabel = new System.Windows.Forms.Label();
            this.materialLabel = new System.Windows.Forms.Label();
            this.conversionTableGroupBox = new System.Windows.Forms.GroupBox();
            this.conversionTableGridView = new System.Windows.Forms.DataGridView();
            this.wheelTensionGroupBox = new System.Windows.Forms.GroupBox();
            this.varianceComboBox = new System.Windows.Forms.ComboBox();
            this.varianceLabel = new System.Windows.Forms.Label();
            this.rightSideSpokesGroupBox = new System.Windows.Forms.GroupBox();
            this.withinTensionLimitRightSpokesLabel = new System.Windows.Forms.Label();
            this.tensionRightSpokesLabel = new System.Windows.Forms.Label();
            this.rightSpokesLowerTensionLimitTextBox = new System.Windows.Forms.TextBox();
            this.tm1ReadingRightSpokesLabel = new System.Windows.Forms.Label();
            this.rightSpokesUpperTensionLimitTextBox = new System.Windows.Forms.TextBox();
            this.rightSideSpokeCountComboBox = new System.Windows.Forms.ComboBox();
            this.standartDevRightSpokesTensionTextBox = new System.Windows.Forms.TextBox();
            this.averageRightSpokesTensionTextBox = new System.Windows.Forms.TextBox();
            this.rightSpokesLowerTensionLimitLabel = new System.Windows.Forms.Label();
            this.rightSpokesUpperTensionLimitLabel = new System.Windows.Forms.Label();
            this.standartDevRightSpokesTensionLabel = new System.Windows.Forms.Label();
            this.averageRightSpokesTensionLabel = new System.Windows.Forms.Label();
            this.leftSideSpokesGroupBox = new System.Windows.Forms.GroupBox();
            this.withinTensionLimitLeftSpokesLabel = new System.Windows.Forms.Label();
            this.tensionLeftSpokesLabel = new System.Windows.Forms.Label();
            this.tm1ReadingLeftSpokesLabel = new System.Windows.Forms.Label();
            this.leftSpokesLowerTensionLimitTextBox = new System.Windows.Forms.TextBox();
            this.leftSpokesUpperTensionLimitTextBox = new System.Windows.Forms.TextBox();
            this.standartDevLeftSpokesTensionTextBox = new System.Windows.Forms.TextBox();
            this.leftSideSpokeCountComboBox = new System.Windows.Forms.ComboBox();
            this.averageLeftSpokesTensionTextBox = new System.Windows.Forms.TextBox();
            this.leftSpokesLowerTensionLimitLabel = new System.Windows.Forms.Label();
            this.leftSpokesUpperTensionLimitLabel = new System.Windows.Forms.Label();
            this.standartDevLeftSpokesTensionLabel = new System.Windows.Forms.Label();
            this.averageLeftSpokesTensionLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProviderTensionLimitError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spokeTensionChart)).BeginInit();
            this.wheelSettingsGroupBox.SuspendLayout();
            this.conversionTableGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conversionTableGridView)).BeginInit();
            this.wheelTensionGroupBox.SuspendLayout();
            this.rightSideSpokesGroupBox.SuspendLayout();
            this.leftSideSpokesGroupBox.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTensionLimitError)).BeginInit();
            this.SuspendLayout();
            // 
            // spokeTensionChart
            // 
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.Name = "ChartArea";
            this.spokeTensionChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend";
            this.spokeTensionChart.Legends.Add(legend1);
            this.spokeTensionChart.Location = new System.Drawing.Point(7, 247);
            this.spokeTensionChart.Name = "spokeTensionChart";
            this.spokeTensionChart.Size = new System.Drawing.Size(767, 667);
            this.spokeTensionChart.TabIndex = 0;
            this.spokeTensionChart.Text = "Wheel Tension Balancing";
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(920, 859);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(263, 55);
            this.calculateButton.TabIndex = 1;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // wheelSettingsGroupBox
            // 
            this.wheelSettingsGroupBox.Controls.Add(this.thicknessComboBox);
            this.wheelSettingsGroupBox.Controls.Add(this.shapeComboBox);
            this.wheelSettingsGroupBox.Controls.Add(this.materialComboBox);
            this.wheelSettingsGroupBox.Controls.Add(this.thicknessLabel);
            this.wheelSettingsGroupBox.Controls.Add(this.shapeLabel);
            this.wheelSettingsGroupBox.Controls.Add(this.materialLabel);
            this.wheelSettingsGroupBox.Location = new System.Drawing.Point(7, 27);
            this.wheelSettingsGroupBox.Name = "wheelSettingsGroupBox";
            this.wheelSettingsGroupBox.Size = new System.Drawing.Size(767, 110);
            this.wheelSettingsGroupBox.TabIndex = 2;
            this.wheelSettingsGroupBox.TabStop = false;
            this.wheelSettingsGroupBox.Text = "Wheel Spokes Settings";
            // 
            // thicknessComboBox
            // 
            this.thicknessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thicknessComboBox.Enabled = false;
            this.thicknessComboBox.FormattingEnabled = true;
            this.thicknessComboBox.Location = new System.Drawing.Point(69, 76);
            this.thicknessComboBox.Name = "thicknessComboBox";
            this.thicknessComboBox.Size = new System.Drawing.Size(692, 21);
            this.thicknessComboBox.TabIndex = 5;
            this.thicknessComboBox.TextChanged += new System.EventHandler(this.thicknessComboBox_TextChanged);
            // 
            // shapeComboBox
            // 
            this.shapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shapeComboBox.Enabled = false;
            this.shapeComboBox.FormattingEnabled = true;
            this.shapeComboBox.Location = new System.Drawing.Point(69, 48);
            this.shapeComboBox.Name = "shapeComboBox";
            this.shapeComboBox.Size = new System.Drawing.Size(692, 21);
            this.shapeComboBox.TabIndex = 4;
            this.shapeComboBox.TextChanged += new System.EventHandler(this.shapeComboBox_TextChanged);
            // 
            // materialComboBox
            // 
            this.materialComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBox.FormattingEnabled = true;
            this.materialComboBox.Location = new System.Drawing.Point(69, 20);
            this.materialComboBox.Name = "materialComboBox";
            this.materialComboBox.Size = new System.Drawing.Size(692, 21);
            this.materialComboBox.TabIndex = 3;
            this.materialComboBox.TextChanged += new System.EventHandler(this.materialComboBox_TextChanged);
            // 
            // thicknessLabel
            // 
            this.thicknessLabel.AutoSize = true;
            this.thicknessLabel.Location = new System.Drawing.Point(7, 79);
            this.thicknessLabel.Name = "thicknessLabel";
            this.thicknessLabel.Size = new System.Drawing.Size(56, 13);
            this.thicknessLabel.TabIndex = 2;
            this.thicknessLabel.Text = "Thickness";
            // 
            // shapeLabel
            // 
            this.shapeLabel.AutoSize = true;
            this.shapeLabel.Location = new System.Drawing.Point(7, 51);
            this.shapeLabel.Name = "shapeLabel";
            this.shapeLabel.Size = new System.Drawing.Size(38, 13);
            this.shapeLabel.TabIndex = 1;
            this.shapeLabel.Text = "Shape";
            // 
            // materialLabel
            // 
            this.materialLabel.AutoSize = true;
            this.materialLabel.Location = new System.Drawing.Point(8, 23);
            this.materialLabel.Name = "materialLabel";
            this.materialLabel.Size = new System.Drawing.Size(44, 13);
            this.materialLabel.TabIndex = 0;
            this.materialLabel.Text = "Material";
            // 
            // conversionTableGroupBox
            // 
            this.conversionTableGroupBox.Controls.Add(this.conversionTableGridView);
            this.conversionTableGroupBox.Location = new System.Drawing.Point(6, 143);
            this.conversionTableGroupBox.Name = "conversionTableGroupBox";
            this.conversionTableGroupBox.Size = new System.Drawing.Size(768, 90);
            this.conversionTableGroupBox.TabIndex = 3;
            this.conversionTableGroupBox.TabStop = false;
            this.conversionTableGroupBox.Text = "TM-1 Conversion Table";
            // 
            // conversionTableGridView
            // 
            this.conversionTableGridView.AllowUserToAddRows = false;
            this.conversionTableGridView.AllowUserToDeleteRows = false;
            this.conversionTableGridView.AllowUserToResizeColumns = false;
            this.conversionTableGridView.AllowUserToResizeRows = false;
            this.conversionTableGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.conversionTableGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.conversionTableGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conversionTableGridView.Location = new System.Drawing.Point(6, 19);
            this.conversionTableGridView.Name = "conversionTableGridView";
            this.conversionTableGridView.ReadOnly = true;
            this.conversionTableGridView.Size = new System.Drawing.Size(754, 64);
            this.conversionTableGridView.TabIndex = 0;
            // 
            // wheelTensionGroupBox
            // 
            this.wheelTensionGroupBox.Controls.Add(this.varianceComboBox);
            this.wheelTensionGroupBox.Controls.Add(this.varianceLabel);
            this.wheelTensionGroupBox.Controls.Add(this.rightSideSpokesGroupBox);
            this.wheelTensionGroupBox.Controls.Add(this.leftSideSpokesGroupBox);
            this.wheelTensionGroupBox.Location = new System.Drawing.Point(779, 27);
            this.wheelTensionGroupBox.Name = "wheelTensionGroupBox";
            this.wheelTensionGroupBox.Size = new System.Drawing.Size(533, 827);
            this.wheelTensionGroupBox.TabIndex = 4;
            this.wheelTensionGroupBox.TabStop = false;
            this.wheelTensionGroupBox.Text = "Wheel Tenion Balancing";
            // 
            // varianceComboBox
            // 
            this.varianceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.varianceComboBox.Enabled = false;
            this.varianceComboBox.FormattingEnabled = true;
            this.varianceComboBox.Items.AddRange(new object[] {
            "5%",
            "10%",
            "15%",
            "20%"});
            this.varianceComboBox.Location = new System.Drawing.Point(61, 19);
            this.varianceComboBox.Name = "varianceComboBox";
            this.varianceComboBox.Size = new System.Drawing.Size(202, 21);
            this.varianceComboBox.TabIndex = 11;
            this.varianceComboBox.TextChanged += new System.EventHandler(this.varianceComboBox_TextChanged);
            // 
            // varianceLabel
            // 
            this.varianceLabel.AutoSize = true;
            this.varianceLabel.Location = new System.Drawing.Point(6, 23);
            this.varianceLabel.Name = "varianceLabel";
            this.varianceLabel.Size = new System.Drawing.Size(49, 13);
            this.varianceLabel.TabIndex = 10;
            this.varianceLabel.Text = "Variance";
            // 
            // rightSideSpokesGroupBox
            // 
            this.rightSideSpokesGroupBox.Controls.Add(this.withinTensionLimitRightSpokesLabel);
            this.rightSideSpokesGroupBox.Controls.Add(this.tensionRightSpokesLabel);
            this.rightSideSpokesGroupBox.Controls.Add(this.rightSpokesLowerTensionLimitTextBox);
            this.rightSideSpokesGroupBox.Controls.Add(this.tm1ReadingRightSpokesLabel);
            this.rightSideSpokesGroupBox.Controls.Add(this.rightSpokesUpperTensionLimitTextBox);
            this.rightSideSpokesGroupBox.Controls.Add(this.rightSideSpokeCountComboBox);
            this.rightSideSpokesGroupBox.Controls.Add(this.standartDevRightSpokesTensionTextBox);
            this.rightSideSpokesGroupBox.Controls.Add(this.averageRightSpokesTensionTextBox);
            this.rightSideSpokesGroupBox.Controls.Add(this.rightSpokesLowerTensionLimitLabel);
            this.rightSideSpokesGroupBox.Controls.Add(this.rightSpokesUpperTensionLimitLabel);
            this.rightSideSpokesGroupBox.Controls.Add(this.standartDevRightSpokesTensionLabel);
            this.rightSideSpokesGroupBox.Controls.Add(this.averageRightSpokesTensionLabel);
            this.rightSideSpokesGroupBox.Location = new System.Drawing.Point(269, 51);
            this.rightSideSpokesGroupBox.Name = "rightSideSpokesGroupBox";
            this.rightSideSpokesGroupBox.Size = new System.Drawing.Size(257, 770);
            this.rightSideSpokesGroupBox.TabIndex = 9;
            this.rightSideSpokesGroupBox.TabStop = false;
            this.rightSideSpokesGroupBox.Text = "Right side spokes";
            // 
            // withinTensionLimitRightSpokesLabel
            // 
            this.withinTensionLimitRightSpokesLabel.AutoSize = true;
            this.withinTensionLimitRightSpokesLabel.Location = new System.Drawing.Point(171, 20);
            this.withinTensionLimitRightSpokesLabel.Name = "withinTensionLimitRightSpokesLabel";
            this.withinTensionLimitRightSpokesLabel.Size = new System.Drawing.Size(80, 13);
            this.withinTensionLimitRightSpokesLabel.TabIndex = 13;
            this.withinTensionLimitRightSpokesLabel.Text = "Within 20% limit";
            // 
            // tensionRightSpokesLabel
            // 
            this.tensionRightSpokesLabel.AutoSize = true;
            this.tensionRightSpokesLabel.Location = new System.Drawing.Point(91, 20);
            this.tensionRightSpokesLabel.Name = "tensionRightSpokesLabel";
            this.tensionRightSpokesLabel.Size = new System.Drawing.Size(69, 13);
            this.tensionRightSpokesLabel.TabIndex = 12;
            this.tensionRightSpokesLabel.Text = "Tension (kgf)";
            // 
            // rightSpokesLowerTensionLimitTextBox
            // 
            this.rightSpokesLowerTensionLimitTextBox.Enabled = false;
            this.rightSpokesLowerTensionLimitTextBox.Location = new System.Drawing.Point(196, 739);
            this.rightSpokesLowerTensionLimitTextBox.Name = "rightSpokesLowerTensionLimitTextBox";
            this.rightSpokesLowerTensionLimitTextBox.Size = new System.Drawing.Size(55, 20);
            this.rightSpokesLowerTensionLimitTextBox.TabIndex = 8;
            this.rightSpokesLowerTensionLimitTextBox.Text = "0";
            // 
            // tm1ReadingRightSpokesLabel
            // 
            this.tm1ReadingRightSpokesLabel.AutoSize = true;
            this.tm1ReadingRightSpokesLabel.Location = new System.Drawing.Point(6, 20);
            this.tm1ReadingRightSpokesLabel.Name = "tm1ReadingRightSpokesLabel";
            this.tm1ReadingRightSpokesLabel.Size = new System.Drawing.Size(75, 13);
            this.tm1ReadingRightSpokesLabel.TabIndex = 11;
            this.tm1ReadingRightSpokesLabel.Text = "TM-1 Reading";
            // 
            // rightSpokesUpperTensionLimitTextBox
            // 
            this.rightSpokesUpperTensionLimitTextBox.Enabled = false;
            this.rightSpokesUpperTensionLimitTextBox.Location = new System.Drawing.Point(196, 713);
            this.rightSpokesUpperTensionLimitTextBox.Name = "rightSpokesUpperTensionLimitTextBox";
            this.rightSpokesUpperTensionLimitTextBox.Size = new System.Drawing.Size(55, 20);
            this.rightSpokesUpperTensionLimitTextBox.TabIndex = 7;
            this.rightSpokesUpperTensionLimitTextBox.Text = "0";
            // 
            // rightSideSpokeCountComboBox
            // 
            this.rightSideSpokeCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightSideSpokeCountComboBox.Enabled = false;
            this.rightSideSpokeCountComboBox.FormattingEnabled = true;
            this.rightSideSpokeCountComboBox.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.rightSideSpokeCountComboBox.Location = new System.Drawing.Point(9, 49);
            this.rightSideSpokeCountComboBox.Name = "rightSideSpokeCountComboBox";
            this.rightSideSpokeCountComboBox.Size = new System.Drawing.Size(72, 21);
            this.rightSideSpokeCountComboBox.TabIndex = 3;
            this.rightSideSpokeCountComboBox.TextChanged += new System.EventHandler(this.rightSideSpokeCountComboBox_TextChanged);
            // 
            // standartDevRightSpokesTensionTextBox
            // 
            this.standartDevRightSpokesTensionTextBox.Enabled = false;
            this.standartDevRightSpokesTensionTextBox.Location = new System.Drawing.Point(196, 687);
            this.standartDevRightSpokesTensionTextBox.Name = "standartDevRightSpokesTensionTextBox";
            this.standartDevRightSpokesTensionTextBox.Size = new System.Drawing.Size(55, 20);
            this.standartDevRightSpokesTensionTextBox.TabIndex = 6;
            this.standartDevRightSpokesTensionTextBox.Text = "0";
            // 
            // averageRightSpokesTensionTextBox
            // 
            this.averageRightSpokesTensionTextBox.Enabled = false;
            this.averageRightSpokesTensionTextBox.Location = new System.Drawing.Point(196, 661);
            this.averageRightSpokesTensionTextBox.Name = "averageRightSpokesTensionTextBox";
            this.averageRightSpokesTensionTextBox.Size = new System.Drawing.Size(55, 20);
            this.averageRightSpokesTensionTextBox.TabIndex = 5;
            this.averageRightSpokesTensionTextBox.Text = "0";
            // 
            // rightSpokesLowerTensionLimitLabel
            // 
            this.rightSpokesLowerTensionLimitLabel.AutoSize = true;
            this.rightSpokesLowerTensionLimitLabel.Location = new System.Drawing.Point(11, 742);
            this.rightSpokesLowerTensionLimitLabel.Name = "rightSpokesLowerTensionLimitLabel";
            this.rightSpokesLowerTensionLimitLabel.Size = new System.Drawing.Size(154, 13);
            this.rightSpokesLowerTensionLimitLabel.TabIndex = 3;
            this.rightSpokesLowerTensionLimitLabel.Text = "-20% Lower Tension Limit (kgf) ";
            // 
            // rightSpokesUpperTensionLimitLabel
            // 
            this.rightSpokesUpperTensionLimitLabel.AutoSize = true;
            this.rightSpokesUpperTensionLimitLabel.Location = new System.Drawing.Point(11, 716);
            this.rightSpokesUpperTensionLimitLabel.Name = "rightSpokesUpperTensionLimitLabel";
            this.rightSpokesUpperTensionLimitLabel.Size = new System.Drawing.Size(154, 13);
            this.rightSpokesUpperTensionLimitLabel.TabIndex = 2;
            this.rightSpokesUpperTensionLimitLabel.Text = "+20% Upper Tension Limit (kgf)";
            // 
            // standartDevRightSpokesTensionLabel
            // 
            this.standartDevRightSpokesTensionLabel.AutoSize = true;
            this.standartDevRightSpokesTensionLabel.Location = new System.Drawing.Point(11, 690);
            this.standartDevRightSpokesTensionLabel.Name = "standartDevRightSpokesTensionLabel";
            this.standartDevRightSpokesTensionLabel.Size = new System.Drawing.Size(175, 13);
            this.standartDevRightSpokesTensionLabel.TabIndex = 1;
            this.standartDevRightSpokesTensionLabel.Text = "Standard Deviation of Tension (kgf)";
            // 
            // averageRightSpokesTensionLabel
            // 
            this.averageRightSpokesTensionLabel.AutoSize = true;
            this.averageRightSpokesTensionLabel.Location = new System.Drawing.Point(11, 664);
            this.averageRightSpokesTensionLabel.Name = "averageRightSpokesTensionLabel";
            this.averageRightSpokesTensionLabel.Size = new System.Drawing.Size(146, 13);
            this.averageRightSpokesTensionLabel.TabIndex = 0;
            this.averageRightSpokesTensionLabel.Text = "Average Spoke Tension (kgf)";
            // 
            // leftSideSpokesGroupBox
            // 
            this.leftSideSpokesGroupBox.Controls.Add(this.withinTensionLimitLeftSpokesLabel);
            this.leftSideSpokesGroupBox.Controls.Add(this.tensionLeftSpokesLabel);
            this.leftSideSpokesGroupBox.Controls.Add(this.tm1ReadingLeftSpokesLabel);
            this.leftSideSpokesGroupBox.Controls.Add(this.leftSpokesLowerTensionLimitTextBox);
            this.leftSideSpokesGroupBox.Controls.Add(this.leftSpokesUpperTensionLimitTextBox);
            this.leftSideSpokesGroupBox.Controls.Add(this.standartDevLeftSpokesTensionTextBox);
            this.leftSideSpokesGroupBox.Controls.Add(this.leftSideSpokeCountComboBox);
            this.leftSideSpokesGroupBox.Controls.Add(this.averageLeftSpokesTensionTextBox);
            this.leftSideSpokesGroupBox.Controls.Add(this.leftSpokesLowerTensionLimitLabel);
            this.leftSideSpokesGroupBox.Controls.Add(this.leftSpokesUpperTensionLimitLabel);
            this.leftSideSpokesGroupBox.Controls.Add(this.standartDevLeftSpokesTensionLabel);
            this.leftSideSpokesGroupBox.Controls.Add(this.averageLeftSpokesTensionLabel);
            this.leftSideSpokesGroupBox.Location = new System.Drawing.Point(6, 51);
            this.leftSideSpokesGroupBox.Name = "leftSideSpokesGroupBox";
            this.leftSideSpokesGroupBox.Size = new System.Drawing.Size(257, 770);
            this.leftSideSpokesGroupBox.TabIndex = 4;
            this.leftSideSpokesGroupBox.TabStop = false;
            this.leftSideSpokesGroupBox.Text = "Left side spokes";
            // 
            // withinTensionLimitLeftSpokesLabel
            // 
            this.withinTensionLimitLeftSpokesLabel.AutoSize = true;
            this.withinTensionLimitLeftSpokesLabel.Location = new System.Drawing.Point(171, 20);
            this.withinTensionLimitLeftSpokesLabel.Name = "withinTensionLimitLeftSpokesLabel";
            this.withinTensionLimitLeftSpokesLabel.Size = new System.Drawing.Size(80, 13);
            this.withinTensionLimitLeftSpokesLabel.TabIndex = 11;
            this.withinTensionLimitLeftSpokesLabel.Text = "Within 20% limit";
            // 
            // tensionLeftSpokesLabel
            // 
            this.tensionLeftSpokesLabel.AutoSize = true;
            this.tensionLeftSpokesLabel.Location = new System.Drawing.Point(92, 20);
            this.tensionLeftSpokesLabel.Name = "tensionLeftSpokesLabel";
            this.tensionLeftSpokesLabel.Size = new System.Drawing.Size(69, 13);
            this.tensionLeftSpokesLabel.TabIndex = 10;
            this.tensionLeftSpokesLabel.Text = "Tension (kgf)";
            // 
            // tm1ReadingLeftSpokesLabel
            // 
            this.tm1ReadingLeftSpokesLabel.AutoSize = true;
            this.tm1ReadingLeftSpokesLabel.Location = new System.Drawing.Point(7, 20);
            this.tm1ReadingLeftSpokesLabel.Name = "tm1ReadingLeftSpokesLabel";
            this.tm1ReadingLeftSpokesLabel.Size = new System.Drawing.Size(75, 13);
            this.tm1ReadingLeftSpokesLabel.TabIndex = 9;
            this.tm1ReadingLeftSpokesLabel.Text = "TM-1 Reading";
            // 
            // leftSpokesLowerTensionLimitTextBox
            // 
            this.leftSpokesLowerTensionLimitTextBox.Enabled = false;
            this.leftSpokesLowerTensionLimitTextBox.Location = new System.Drawing.Point(196, 739);
            this.leftSpokesLowerTensionLimitTextBox.Name = "leftSpokesLowerTensionLimitTextBox";
            this.leftSpokesLowerTensionLimitTextBox.Size = new System.Drawing.Size(55, 20);
            this.leftSpokesLowerTensionLimitTextBox.TabIndex = 8;
            this.leftSpokesLowerTensionLimitTextBox.Text = "0";
            // 
            // leftSpokesUpperTensionLimitTextBox
            // 
            this.leftSpokesUpperTensionLimitTextBox.Enabled = false;
            this.leftSpokesUpperTensionLimitTextBox.Location = new System.Drawing.Point(196, 713);
            this.leftSpokesUpperTensionLimitTextBox.Name = "leftSpokesUpperTensionLimitTextBox";
            this.leftSpokesUpperTensionLimitTextBox.Size = new System.Drawing.Size(55, 20);
            this.leftSpokesUpperTensionLimitTextBox.TabIndex = 7;
            this.leftSpokesUpperTensionLimitTextBox.Text = "0";
            // 
            // standartDevLeftSpokesTensionTextBox
            // 
            this.standartDevLeftSpokesTensionTextBox.Enabled = false;
            this.standartDevLeftSpokesTensionTextBox.Location = new System.Drawing.Point(196, 687);
            this.standartDevLeftSpokesTensionTextBox.Name = "standartDevLeftSpokesTensionTextBox";
            this.standartDevLeftSpokesTensionTextBox.Size = new System.Drawing.Size(55, 20);
            this.standartDevLeftSpokesTensionTextBox.TabIndex = 6;
            this.standartDevLeftSpokesTensionTextBox.Text = "0";
            // 
            // leftSideSpokeCountComboBox
            // 
            this.leftSideSpokeCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftSideSpokeCountComboBox.Enabled = false;
            this.leftSideSpokeCountComboBox.FormattingEnabled = true;
            this.leftSideSpokeCountComboBox.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.leftSideSpokeCountComboBox.Location = new System.Drawing.Point(10, 49);
            this.leftSideSpokeCountComboBox.MaxDropDownItems = 21;
            this.leftSideSpokeCountComboBox.Name = "leftSideSpokeCountComboBox";
            this.leftSideSpokeCountComboBox.Size = new System.Drawing.Size(72, 21);
            this.leftSideSpokeCountComboBox.TabIndex = 2;
            this.leftSideSpokeCountComboBox.TextChanged += new System.EventHandler(this.leftSideSpokeCountComboBox_TextChanged);
            // 
            // averageLeftSpokesTensionTextBox
            // 
            this.averageLeftSpokesTensionTextBox.Enabled = false;
            this.averageLeftSpokesTensionTextBox.Location = new System.Drawing.Point(196, 661);
            this.averageLeftSpokesTensionTextBox.Name = "averageLeftSpokesTensionTextBox";
            this.averageLeftSpokesTensionTextBox.Size = new System.Drawing.Size(55, 20);
            this.averageLeftSpokesTensionTextBox.TabIndex = 5;
            this.averageLeftSpokesTensionTextBox.Text = "0";
            // 
            // leftSpokesLowerTensionLimitLabel
            // 
            this.leftSpokesLowerTensionLimitLabel.AutoSize = true;
            this.leftSpokesLowerTensionLimitLabel.Location = new System.Drawing.Point(11, 742);
            this.leftSpokesLowerTensionLimitLabel.Name = "leftSpokesLowerTensionLimitLabel";
            this.leftSpokesLowerTensionLimitLabel.Size = new System.Drawing.Size(154, 13);
            this.leftSpokesLowerTensionLimitLabel.TabIndex = 3;
            this.leftSpokesLowerTensionLimitLabel.Text = "-20% Lower Tension Limit (kgf) ";
            // 
            // leftSpokesUpperTensionLimitLabel
            // 
            this.leftSpokesUpperTensionLimitLabel.AutoSize = true;
            this.leftSpokesUpperTensionLimitLabel.Location = new System.Drawing.Point(11, 716);
            this.leftSpokesUpperTensionLimitLabel.Name = "leftSpokesUpperTensionLimitLabel";
            this.leftSpokesUpperTensionLimitLabel.Size = new System.Drawing.Size(154, 13);
            this.leftSpokesUpperTensionLimitLabel.TabIndex = 2;
            this.leftSpokesUpperTensionLimitLabel.Text = "+20% Upper Tension Limit (kgf)";
            // 
            // standartDevLeftSpokesTensionLabel
            // 
            this.standartDevLeftSpokesTensionLabel.AutoSize = true;
            this.standartDevLeftSpokesTensionLabel.Location = new System.Drawing.Point(11, 690);
            this.standartDevLeftSpokesTensionLabel.Name = "standartDevLeftSpokesTensionLabel";
            this.standartDevLeftSpokesTensionLabel.Size = new System.Drawing.Size(175, 13);
            this.standartDevLeftSpokesTensionLabel.TabIndex = 1;
            this.standartDevLeftSpokesTensionLabel.Text = "Standard Deviation of Tension (kgf)";
            // 
            // averageLeftSpokesTensionLabel
            // 
            this.averageLeftSpokesTensionLabel.AutoSize = true;
            this.averageLeftSpokesTensionLabel.Location = new System.Drawing.Point(11, 664);
            this.averageLeftSpokesTensionLabel.Name = "averageLeftSpokesTensionLabel";
            this.averageLeftSpokesTensionLabel.Size = new System.Drawing.Size(146, 13);
            this.averageLeftSpokesTensionLabel.TabIndex = 0;
            this.averageLeftSpokesTensionLabel.Text = "Average Spoke Tension (kgf)";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1319, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // errorProviderTensionLimitError
            // 
            this.errorProviderTensionLimitError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProviderTensionLimitError.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 921);
            this.Controls.Add(this.wheelTensionGroupBox);
            this.Controls.Add(this.conversionTableGroupBox);
            this.Controls.Add(this.wheelSettingsGroupBox);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.spokeTensionChart);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wheel Tension Application";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spokeTensionChart)).EndInit();
            this.wheelSettingsGroupBox.ResumeLayout(false);
            this.wheelSettingsGroupBox.PerformLayout();
            this.conversionTableGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conversionTableGridView)).EndInit();
            this.wheelTensionGroupBox.ResumeLayout(false);
            this.wheelTensionGroupBox.PerformLayout();
            this.rightSideSpokesGroupBox.ResumeLayout(false);
            this.rightSideSpokesGroupBox.PerformLayout();
            this.leftSideSpokesGroupBox.ResumeLayout(false);
            this.leftSideSpokesGroupBox.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTensionLimitError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart spokeTensionChart;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.GroupBox wheelSettingsGroupBox;
        private System.Windows.Forms.ComboBox thicknessComboBox;
        private System.Windows.Forms.ComboBox shapeComboBox;
        private System.Windows.Forms.ComboBox materialComboBox;
        private System.Windows.Forms.Label thicknessLabel;
        private System.Windows.Forms.Label shapeLabel;
        private System.Windows.Forms.Label materialLabel;
        private System.Windows.Forms.GroupBox conversionTableGroupBox;
        private System.Windows.Forms.DataGridView conversionTableGridView;
        private System.Windows.Forms.GroupBox wheelTensionGroupBox;
        private System.Windows.Forms.ComboBox rightSideSpokeCountComboBox;
        private System.Windows.Forms.ComboBox leftSideSpokeCountComboBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox leftSideSpokesGroupBox;
        private System.Windows.Forms.TextBox leftSpokesLowerTensionLimitTextBox;
        private System.Windows.Forms.TextBox leftSpokesUpperTensionLimitTextBox;
        private System.Windows.Forms.TextBox standartDevLeftSpokesTensionTextBox;
        private System.Windows.Forms.TextBox averageLeftSpokesTensionTextBox;
        private System.Windows.Forms.Label leftSpokesLowerTensionLimitLabel;
        private System.Windows.Forms.Label leftSpokesUpperTensionLimitLabel;
        private System.Windows.Forms.Label standartDevLeftSpokesTensionLabel;
        private System.Windows.Forms.Label averageLeftSpokesTensionLabel;
        private System.Windows.Forms.GroupBox rightSideSpokesGroupBox;
        private System.Windows.Forms.TextBox rightSpokesLowerTensionLimitTextBox;
        private System.Windows.Forms.TextBox rightSpokesUpperTensionLimitTextBox;
        private System.Windows.Forms.TextBox standartDevRightSpokesTensionTextBox;
        private System.Windows.Forms.TextBox averageRightSpokesTensionTextBox;
        private System.Windows.Forms.Label rightSpokesLowerTensionLimitLabel;
        private System.Windows.Forms.Label rightSpokesUpperTensionLimitLabel;
        private System.Windows.Forms.Label standartDevRightSpokesTensionLabel;
        private System.Windows.Forms.Label averageRightSpokesTensionLabel;
        private System.Windows.Forms.Label tensionLeftSpokesLabel;
        private System.Windows.Forms.Label tm1ReadingLeftSpokesLabel;
        private System.Windows.Forms.Label tensionRightSpokesLabel;
        private System.Windows.Forms.Label tm1ReadingRightSpokesLabel;
        private System.Windows.Forms.ComboBox varianceComboBox;
        private System.Windows.Forms.Label varianceLabel;
        private System.Windows.Forms.Label withinTensionLimitLeftSpokesLabel;
        private System.Windows.Forms.Label withinTensionLimitRightSpokesLabel;
        private System.Windows.Forms.ErrorProvider errorProviderTensionLimitError;
    }
}

