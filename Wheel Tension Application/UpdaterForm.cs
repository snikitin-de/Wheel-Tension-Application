﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    public partial class UpdaterForm : Form
    {
        private string linkSite = "https://github.com/snikitin-de/Wheel-Tension-Application";
        private string linkQA = "https://github.com/snikitin-de/Wheel-Tension-Application/discussions/categories/q-a";

        private FOTA FOTA = Updater.FOTA;

        public UpdaterForm()
        {
            string appName = FOTA.AppName;
            string latestTagName = FOTA.getLatestTagName();

            InitializeComponent();

            FOTA.DownloadProgressChanged += FOTA_DownloadProgressChanged;
            FOTA.DownloadFileCompleted += FOTA_DownloadFileCompleted;

            labelAppUpdateVersion.Text = $"{appName} {latestTagName} is available";
        }

        private void FOTA_DownloadProgressChanged(int bytesReceived, int totalBytesToDownload, int progressPercentage)
        {
            Invoke((Action)delegate
            {
                float totalKBytesToDownload = totalBytesToDownload / 1000;
                float KBytesReceived = bytesReceived / 1000;

                progressBarDownload.Maximum = (int)totalKBytesToDownload;
                progressBarDownload.Value = (int)KBytesReceived;

                labelBytesDownload.Text = $"Downloaded {KBytesReceived} KB of {totalKBytesToDownload} KB ({progressPercentage}%)";
            });
        }

        private void FOTA_DownloadFileCompleted(bool downloadFileCompleted)
        {
            updateButton.Enabled = false;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string latestTagName = FOTA.getLatestTagName();
            dynamic latestAssetsName = FOTA.getAssetsByTagName(latestTagName);
            string downloadUrl = FOTA.getDownloadUrl(latestAssetsName);

            FOTA.DownloadLatestUpdate(downloadUrl);
            FOTA.InstallUpdate();
        }

        private void linkLabelSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkSite);
        }

        private void linkLabelQA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkQA);
        }
    }
}
