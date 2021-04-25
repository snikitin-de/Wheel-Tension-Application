// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    /*
     * Класс UpdaterForm. Окно обновления программы.
     */
    /// <summary>
    /// Класс <c>UpdaterForm</c>. Окно обновления программы.
    /// </summary>
    public partial class UpdaterForm : Form
    {
        // Объект FOTA.
        private FOTA FOTA = Updater.FOTA;

        // Конструктор класса UpdaterForm.
        /// <summary>
        /// Конструктор класса UpdaterForm.
        /// </summary>
        public UpdaterForm()
        {
            InitializeComponent();

            // Название программы.
            string appName = FOTA.AppName;
            // Название тега последнего релиза.
            string latestTagName = FOTA.getLatestTagName();

            // Добавление обработчика события DownloadProgressChanged.
            FOTA.DownloadProgressChanged += FOTA_DownloadProgressChanged;
            // Добавление обработчика события DownloadProgressChanged.
            FOTA.DownloadFileCompleted += FOTA_DownloadFileCompleted;

            // Заполнение меток текущей версии и версии обновления.
            currentVersionLabel.Text += " " + Application.ProductVersion;
            updateVersionLabel.Text += " " + latestTagName.Replace("v", "");
        }

        // Обработчик события DownloadProgressChanged.
        /// <summary>
        /// Обработчик события DownloadProgressChanged.
        /// </summary>
        /// <param name="downloadFileCompleted">Загружен ли файл.</param>
        private void FOTA_DownloadProgressChanged(int bytesReceived, int totalBytesToDownload, int progressPercentage)
        {
            if (IsHandleCreated)
            {
                Invoke((Action)delegate
                {
                    // Количество Кбайт доступно для загрузки.
                    float totalKBytesToDownload = totalBytesToDownload / 1000;
                    // Количество Кбайт получено.
                    float KBytesReceived = bytesReceived / 1000;

                    // Установка максимального значения ProgressBar равное количеству доступных Кбайт для загрузки.
                    progressBarDownload.Maximum = (int)totalKBytesToDownload;
                    // Установка текущего значения ProgressBar равное количеству полученных Кбайт.
                    progressBarDownload.Value = (int)KBytesReceived;

                    // Заполнение меток информацией о прогрессе выполнения загрузки.
                    bytesDownloadLabel.Text = $"Downloaded {KBytesReceived} KB of {totalKBytesToDownload} KB";
                    progressPercentageLabel.Text = $"{progressPercentage}%";
                    downloadFileNameLabel.Text = FOTA.getLatestAssetsName();
                });
            }
        }

        // Обработчик события DownloadFileCompleted.
        /// <summary>
        /// Обработчик события DownloadFileCompleted.
        /// </summary>
        /// <param name="downloadFileCompleted">Загружен ли файл.</param>
        private void FOTA_DownloadFileCompleted(bool downloadFileCompleted)
        {
            // Запуск установки обновления.
            FOTA.InstallUpdate();

            // Закрытие программы.
            Application.Exit();
        }

        // Обработчик события клика по кнопке "Update".
        /// <summary>
        /// Обработчик события клика по кнопке "Update".
        /// </summary>
        /// <param name="sender">Ссылка на элемент/объект, который вызвал это событие.</param>
        /// <param name="e">Данные события.</param>
        private void updateButton_Click(object sender, EventArgs e)
        {
            // Название тега последнего релиза.
            string latestTagName = FOTA.getLatestTagName();
            // Объект ассетов последнего релиза.
            dynamic latestAssetsName = FOTA.getAssetsByTagName(latestTagName);
            // Ссылка на скачивание последнего релиза.
            string downloadUrl = FOTA.getDownloadUrl(latestAssetsName);

            // Отключение кнопки "Update".
            updateButton.Enabled = false;

            // Скачивание последнего релиза.
            FOTA.DownloadLatestUpdate(downloadUrl);
        }

        // Обработчик события клика по ссылке на Changelog.
        /// <summary>
        /// Обработчик события клика по ссылке на Changelog.
        /// </summary>
        /// <param name="downloadFileCompleted">Загружен ли файл.</param>
        private void changelogLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Открытие браузера с последним релизом.
            System.Diagnostics.Process.Start(FOTA.getLatestReleaseUrl());
        }
    }
}
