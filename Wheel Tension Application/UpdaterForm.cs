using System;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    public partial class UpdaterForm : Form
    {
        private FOTA FOTA = Updater.FOTA;

        public UpdaterForm()
        {
            InitializeComponent();

            string appName = FOTA.AppName;
            string latestTagName = FOTA.getLatestTagName();

            FOTA.DownloadProgressChanged += FOTA_DownloadProgressChanged;
            FOTA.DownloadFileCompleted += FOTA_DownloadFileCompleted;

            currentVersionLabel.Text += " " + Application.ProductVersion;
            updateVersionLabel.Text += " " + latestTagName.Replace("v", "");
        }

        private void FOTA_DownloadProgressChanged(int bytesReceived, int totalBytesToDownload, int progressPercentage)
        {
            if (IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    float totalKBytesToDownload = totalBytesToDownload / 1000;
                    float KBytesReceived = bytesReceived / 1000;

                    progressBarDownload.Maximum = (int)totalKBytesToDownload;
                    progressBarDownload.Value = (int)KBytesReceived;

                    bytesDownloadLabel.Text = $"Downloaded {KBytesReceived} KB of {totalKBytesToDownload} KB";
                    progressPercentageLabel.Text = $"{progressPercentage}%";
                    downloadFileNameLabel.Text = FOTA.getLatestAssetsName();
                });
            }
        }

        private void FOTA_DownloadFileCompleted(bool downloadFileCompleted)
        {
            updateButton.Enabled = false;

            FOTA.InstallUpdate();

            Application.Exit();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string latestTagName = FOTA.getLatestTagName();
            dynamic latestAssetsName = FOTA.getAssetsByTagName(latestTagName);
            string downloadUrl = FOTA.getDownloadUrl(latestAssetsName);

            FOTA.DownloadLatestUpdate(downloadUrl);
        }

        private void changelogLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(FOTA.getLatestReleaseUrl());
        }
    }
}
