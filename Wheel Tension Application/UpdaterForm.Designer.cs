
namespace Wheel_Tension_Application
{
    partial class UpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterForm));
            this.bytesDownloadLabel = new System.Windows.Forms.Label();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.updateButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.changelogLinkLabel = new System.Windows.Forms.LinkLabel();
            this.updateVersionLabel = new System.Windows.Forms.Label();
            this.currentVersionLabel = new System.Windows.Forms.Label();
            this.progressPercentageLabel = new System.Windows.Forms.Label();
            this.downloadFileNameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bytesDownloadLabel
            // 
            this.bytesDownloadLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.bytesDownloadLabel.AutoSize = true;
            this.bytesDownloadLabel.Location = new System.Drawing.Point(83, 86);
            this.bytesDownloadLabel.Name = "bytesDownloadLabel";
            this.bytesDownloadLabel.Size = new System.Drawing.Size(67, 13);
            this.bytesDownloadLabel.TabIndex = 4;
            this.bytesDownloadLabel.Text = "Downloaded";
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(119, 102);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(419, 15);
            this.progressBarDownload.TabIndex = 12;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(500, 47);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(117, 27);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.changelogLinkLabel);
            this.panel1.Controls.Add(this.updateVersionLabel);
            this.panel1.Controls.Add(this.currentVersionLabel);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Location = new System.Drawing.Point(-5, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 95);
            this.panel1.TabIndex = 14;
            // 
            // changelogLinkLabel
            // 
            this.changelogLinkLabel.AutoSize = true;
            this.changelogLinkLabel.Location = new System.Drawing.Point(16, 61);
            this.changelogLinkLabel.Name = "changelogLinkLabel";
            this.changelogLinkLabel.Size = new System.Drawing.Size(58, 13);
            this.changelogLinkLabel.TabIndex = 11;
            this.changelogLinkLabel.TabStop = true;
            this.changelogLinkLabel.Text = "Changelog";
            this.changelogLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changelogLinkLabel_LinkClicked);
            // 
            // updateVersionLabel
            // 
            this.updateVersionLabel.AutoSize = true;
            this.updateVersionLabel.Location = new System.Drawing.Point(14, 36);
            this.updateVersionLabel.Name = "updateVersionLabel";
            this.updateVersionLabel.Size = new System.Drawing.Size(82, 13);
            this.updateVersionLabel.TabIndex = 10;
            this.updateVersionLabel.Text = "Update version:";
            // 
            // currentVersionLabel
            // 
            this.currentVersionLabel.AutoSize = true;
            this.currentVersionLabel.Location = new System.Drawing.Point(15, 12);
            this.currentVersionLabel.Name = "currentVersionLabel";
            this.currentVersionLabel.Size = new System.Drawing.Size(81, 13);
            this.currentVersionLabel.TabIndex = 9;
            this.currentVersionLabel.Text = "Current version:";
            // 
            // progressPercentageLabel
            // 
            this.progressPercentageLabel.AutoSize = true;
            this.progressPercentageLabel.Location = new System.Drawing.Point(83, 104);
            this.progressPercentageLabel.Name = "progressPercentageLabel";
            this.progressPercentageLabel.Size = new System.Drawing.Size(21, 13);
            this.progressPercentageLabel.TabIndex = 15;
            this.progressPercentageLabel.Text = "0%";
            // 
            // downloadFileNameLabel
            // 
            this.downloadFileNameLabel.AutoSize = true;
            this.downloadFileNameLabel.Location = new System.Drawing.Point(83, 120);
            this.downloadFileNameLabel.Name = "downloadFileNameLabel";
            this.downloadFileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.downloadFileNameLabel.TabIndex = 16;
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 309);
            this.Controls.Add(this.downloadFileNameLabel);
            this.Controls.Add(this.progressPercentageLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.bytesDownloadLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label bytesDownloadLabel;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label updateVersionLabel;
        private System.Windows.Forms.Label currentVersionLabel;
        private System.Windows.Forms.LinkLabel changelogLinkLabel;
        private System.Windows.Forms.Label progressPercentageLabel;
        private System.Windows.Forms.Label downloadFileNameLabel;
    }
}