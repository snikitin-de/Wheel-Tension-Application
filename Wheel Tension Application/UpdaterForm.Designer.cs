
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
            this.labelAppUpdateVersion = new System.Windows.Forms.Label();
            this.labelBytesDownload = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.linkLabelSite = new System.Windows.Forms.LinkLabel();
            this.linkLabelQA = new System.Windows.Forms.LinkLabel();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelAppUpdateVersion
            // 
            this.labelAppUpdateVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAppUpdateVersion.AutoSize = true;
            this.labelAppUpdateVersion.Location = new System.Drawing.Point(9, 9);
            this.labelAppUpdateVersion.Name = "labelAppUpdateVersion";
            this.labelAppUpdateVersion.Size = new System.Drawing.Size(0, 13);
            this.labelAppUpdateVersion.TabIndex = 1;
            // 
            // labelBytesDownload
            // 
            this.labelBytesDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBytesDownload.AutoSize = true;
            this.labelBytesDownload.Location = new System.Drawing.Point(8, 34);
            this.labelBytesDownload.Name = "labelBytesDownload";
            this.labelBytesDownload.Size = new System.Drawing.Size(0, 13);
            this.labelBytesDownload.TabIndex = 4;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(452, 59);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(104, 30);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // linkLabelSite
            // 
            this.linkLabelSite.AutoSize = true;
            this.linkLabelSite.Location = new System.Drawing.Point(11, 102);
            this.linkLabelSite.Name = "linkLabelSite";
            this.linkLabelSite.Size = new System.Drawing.Size(25, 13);
            this.linkLabelSite.TabIndex = 9;
            this.linkLabelSite.TabStop = true;
            this.linkLabelSite.Text = "Site";
            this.linkLabelSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSite_LinkClicked);
            // 
            // linkLabelQA
            // 
            this.linkLabelQA.AutoSize = true;
            this.linkLabelQA.Location = new System.Drawing.Point(42, 102);
            this.linkLabelQA.Name = "linkLabelQA";
            this.linkLabelQA.Size = new System.Drawing.Size(22, 13);
            this.linkLabelQA.TabIndex = 11;
            this.linkLabelQA.TabStop = true;
            this.linkLabelQA.Text = "QA";
            this.linkLabelQA.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelQA_LinkClicked);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(11, 59);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(435, 30);
            this.progressBarDownload.TabIndex = 12;
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 125);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.linkLabelQA);
            this.Controls.Add(this.linkLabelSite);
            this.Controls.Add(this.labelAppUpdateVersion);
            this.Controls.Add(this.labelBytesDownload);
            this.Controls.Add(this.updateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelAppUpdateVersion;
        private System.Windows.Forms.Label labelBytesDownload;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.LinkLabel linkLabelSite;
        private System.Windows.Forms.LinkLabel linkLabelQA;
        private System.Windows.Forms.ProgressBar progressBarDownload;
    }
}