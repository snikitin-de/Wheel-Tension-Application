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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            this.rightSideComboBox = new System.Windows.Forms.ComboBox();
            this.leftSideComboBox = new System.Windows.Forms.ComboBox();
            this.rightSideLabel = new System.Windows.Forms.Label();
            this.leftSideLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.wheelSettingsGroupBox.SuspendLayout();
            this.conversionTableGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conversionTableGridView)).BeginInit();
            this.wheelTensionGroupBox.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.Name = "ChartArea";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(7, 247);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(767, 667);
            this.chart.TabIndex = 0;
            this.chart.Text = "Wheel Tension Balancing";
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(778, 864);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(315, 50);
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
            this.wheelTensionGroupBox.Controls.Add(this.rightSideComboBox);
            this.wheelTensionGroupBox.Controls.Add(this.leftSideComboBox);
            this.wheelTensionGroupBox.Controls.Add(this.rightSideLabel);
            this.wheelTensionGroupBox.Controls.Add(this.leftSideLabel);
            this.wheelTensionGroupBox.Location = new System.Drawing.Point(779, 27);
            this.wheelTensionGroupBox.Name = "wheelTensionGroupBox";
            this.wheelTensionGroupBox.Size = new System.Drawing.Size(314, 827);
            this.wheelTensionGroupBox.TabIndex = 4;
            this.wheelTensionGroupBox.TabStop = false;
            this.wheelTensionGroupBox.Text = "Wheel Tenion Balancing";
            // 
            // rightSideComboBox
            // 
            this.rightSideComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightSideComboBox.FormattingEnabled = true;
            this.rightSideComboBox.Items.AddRange(new object[] {
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
            this.rightSideComboBox.Location = new System.Drawing.Point(162, 47);
            this.rightSideComboBox.Name = "rightSideComboBox";
            this.rightSideComboBox.Size = new System.Drawing.Size(146, 21);
            this.rightSideComboBox.TabIndex = 3;
            this.rightSideComboBox.TextChanged += new System.EventHandler(this.rightSideComboBox_TextChanged);
            // 
            // leftSideComboBox
            // 
            this.leftSideComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftSideComboBox.FormattingEnabled = true;
            this.leftSideComboBox.Items.AddRange(new object[] {
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
            this.leftSideComboBox.Location = new System.Drawing.Point(7, 47);
            this.leftSideComboBox.MaxDropDownItems = 21;
            this.leftSideComboBox.Name = "leftSideComboBox";
            this.leftSideComboBox.Size = new System.Drawing.Size(145, 21);
            this.leftSideComboBox.TabIndex = 2;
            this.leftSideComboBox.TextChanged += new System.EventHandler(this.leftSideComboBox_TextChanged);
            // 
            // rightSideLabel
            // 
            this.rightSideLabel.AutoSize = true;
            this.rightSideLabel.Location = new System.Drawing.Point(159, 23);
            this.rightSideLabel.Name = "rightSideLabel";
            this.rightSideLabel.Size = new System.Drawing.Size(95, 13);
            this.rightSideLabel.TabIndex = 1;
            this.rightSideLabel.Text = "Right Side Spokes";
            // 
            // leftSideLabel
            // 
            this.leftSideLabel.AutoSize = true;
            this.leftSideLabel.Location = new System.Drawing.Point(6, 23);
            this.leftSideLabel.Name = "leftSideLabel";
            this.leftSideLabel.Size = new System.Drawing.Size(88, 13);
            this.leftSideLabel.TabIndex = 0;
            this.leftSideLabel.Text = "Left Side Spokes";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1100, 24);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 921);
            this.Controls.Add(this.wheelTensionGroupBox);
            this.Controls.Add(this.conversionTableGroupBox);
            this.Controls.Add(this.wheelSettingsGroupBox);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wheel Tension Application v1.0.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.wheelSettingsGroupBox.ResumeLayout(false);
            this.wheelSettingsGroupBox.PerformLayout();
            this.conversionTableGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conversionTableGridView)).EndInit();
            this.wheelTensionGroupBox.ResumeLayout(false);
            this.wheelTensionGroupBox.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
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
        private System.Windows.Forms.ComboBox rightSideComboBox;
        private System.Windows.Forms.ComboBox leftSideComboBox;
        private System.Windows.Forms.Label rightSideLabel;
        private System.Windows.Forms.Label leftSideLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

