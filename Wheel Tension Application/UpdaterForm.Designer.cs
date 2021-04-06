
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
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.panelUpdate = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.panelUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAppUpdateVersion
            // 
            this.labelAppUpdateVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAppUpdateVersion.AutoSize = true;
            this.labelAppUpdateVersion.Location = new System.Drawing.Point(34, 76);
            this.labelAppUpdateVersion.Name = "labelAppUpdateVersion";
            this.labelAppUpdateVersion.Size = new System.Drawing.Size(0, 13);
            this.labelAppUpdateVersion.TabIndex = 1;
            // 
            // labelBytesDownload
            // 
            this.labelBytesDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBytesDownload.AutoSize = true;
            this.labelBytesDownload.Location = new System.Drawing.Point(34, 112);
            this.labelBytesDownload.Name = "labelBytesDownload";
            this.labelBytesDownload.Size = new System.Drawing.Size(0, 13);
            this.labelBytesDownload.TabIndex = 4;
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(36, 128);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(419, 26);
            this.progressBarDownload.TabIndex = 12;
            // 
            // panelUpdate
            // 
            this.panelUpdate.BackColor = System.Drawing.SystemColors.Window;
            this.panelUpdate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelUpdate.Controls.Add(this.label2);
            this.panelUpdate.Controls.Add(this.label1);
            this.panelUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelUpdate.Location = new System.Drawing.Point(-5, -3);
            this.panelUpdate.Name = "panelUpdate";
            this.panelUpdate.Size = new System.Drawing.Size(506, 62);
            this.panelUpdate.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Do you want to update?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Updater";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(377, 177);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(104, 30);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 219);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.panelUpdate);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.labelAppUpdateVersion);
            this.Controls.Add(this.labelBytesDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.panelUpdate.ResumeLayout(false);
            this.panelUpdate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelAppUpdateVersion;
        private System.Windows.Forms.Label labelBytesDownload;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Panel panelUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button updateButton;
    }
}