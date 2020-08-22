// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace Wheel_Tension_Application
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.IconLabel = new System.Windows.Forms.Label();
            this.IconLinkLabel = new System.Windows.Forms.LinkLabel();
            this.FromLabel = new System.Windows.Forms.Label();
            this.FlaticonLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.AuthorLinkLabel = new System.Windows.Forms.LinkLabel();
            this.GirhubLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IconLabel
            // 
            this.IconLabel.AutoSize = true;
            this.IconLabel.Location = new System.Drawing.Point(12, 50);
            this.IconLabel.Name = "IconLabel";
            this.IconLabel.Size = new System.Drawing.Size(71, 13);
            this.IconLabel.TabIndex = 0;
            this.IconLabel.Text = "Icon made by";
            // 
            // IconLinkLabel
            // 
            this.IconLinkLabel.AutoSize = true;
            this.IconLinkLabel.Location = new System.Drawing.Point(80, 50);
            this.IconLinkLabel.Name = "IconLinkLabel";
            this.IconLinkLabel.Size = new System.Drawing.Size(41, 13);
            this.IconLinkLabel.TabIndex = 1;
            this.IconLinkLabel.TabStop = true;
            this.IconLinkLabel.Text = "Fleepik";
            this.IconLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IconLinkLabel_LinkClicked);
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Location = new System.Drawing.Point(118, 50);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(27, 13);
            this.FromLabel.TabIndex = 2;
            this.FromLabel.Text = "from";
            // 
            // FlaticonLinkLabel
            // 
            this.FlaticonLinkLabel.AutoSize = true;
            this.FlaticonLinkLabel.Location = new System.Drawing.Point(140, 50);
            this.FlaticonLinkLabel.Name = "FlaticonLinkLabel";
            this.FlaticonLinkLabel.Size = new System.Drawing.Size(44, 13);
            this.FlaticonLinkLabel.TabIndex = 3;
            this.FlaticonLinkLabel.TabStop = true;
            this.FlaticonLinkLabel.Text = "Flaticon";
            this.FlaticonLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FlaticonLinkLabel_LinkClicked);
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(12, 22);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(44, 13);
            this.AuthorLabel.TabIndex = 4;
            this.AuthorLabel.Text = "Author: ";
            // 
            // AuthorLinkLabel
            // 
            this.AuthorLinkLabel.AutoSize = true;
            this.AuthorLinkLabel.Location = new System.Drawing.Point(49, 22);
            this.AuthorLinkLabel.Name = "AuthorLinkLabel";
            this.AuthorLinkLabel.Size = new System.Drawing.Size(54, 13);
            this.AuthorLinkLabel.TabIndex = 5;
            this.AuthorLinkLabel.TabStop = true;
            this.AuthorLinkLabel.Text = "snikitin-de";
            this.AuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuthorLinkLabel_LinkClicked);
            // 
            // GirhubLabel
            // 
            this.GirhubLabel.AutoSize = true;
            this.GirhubLabel.Location = new System.Drawing.Point(98, 22);
            this.GirhubLabel.Name = "GirhubLabel";
            this.GirhubLabel.Size = new System.Drawing.Size(64, 13);
            this.GirhubLabel.TabIndex = 6;
            this.GirhubLabel.Text = " from Github";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 87);
            this.Controls.Add(this.GirhubLabel);
            this.Controls.Add(this.AuthorLinkLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.FlaticonLinkLabel);
            this.Controls.Add(this.FromLabel);
            this.Controls.Add(this.IconLinkLabel);
            this.Controls.Add(this.IconLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IconLabel;
        private System.Windows.Forms.LinkLabel IconLinkLabel;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.LinkLabel FlaticonLinkLabel;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.LinkLabel AuthorLinkLabel;
        private System.Windows.Forms.Label GirhubLabel;
    }
}