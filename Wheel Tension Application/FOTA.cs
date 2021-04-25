// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Newtonsoft.Json;
using System;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syroot.Windows.IO;

namespace Wheel_Tension_Application
{
    /*
     * Класс FOTA для обновления програмы "по воздуху" с GitHub.
     */
    /// <summary>
    /// Класс <c>FOTA</c> для обновления програмы "по воздуху" с GitHub.
    /// </summary>
    class FOTA
    {
        // Автор репозитория на GitHub с обновлением программы.
        private string author;
        // Название репозитория на GitHub с обновлением программы.
        private string repositoryName;
        // Папка, куда будет загружена новая версия программы (по умолчанию папка "Загрузки").
        private string downloadDirectory = new KnownFolder(KnownFolderType.Downloads).Path;
        // UserAgent, который будет использован при отправке запроса на GitHub.
        private string userAgent;

        // Объект списка релизов с GitHub.
        private dynamic releases = null;

        // Событие DownloadProgressChanged.
        public event Action<int, int, int> DownloadProgressChanged;
        // Событие DownloadFileCompleted.
        public event Action<bool> DownloadFileCompleted;

        // Имя программы.
        public string AppName { get; set; }

        // Конструктор класса FOTA.
        /// <summary>
        /// Конструктор класса FOTA.
        /// </summary>
        /// <param name="author">Автор репозитория на GitHub с обновлением программы.</param>
        /// <param name="repositoryName">Название репозитория на GitHub с обновлением программы.</param>
        /// <param name="userAgent">UserAgent, который будет использован при отправке запроса на GitHub.</param>
        /// <param name="appName">Имя программы.</param>
        public FOTA(string author, string repositoryName, string userAgent, string appName)
        {
            this.author = author;
            this.repositoryName = repositoryName;
            this.userAgent = userAgent;

            AppName = appName;
        }

        // Вызов события DownloadProgressChanged, которое срабатывает когда операция асинхронной загрузки успешно передает некоторые данные.
        /// <summary>
        /// Вызов события DownloadProgressChanged, которое срабатывает когда операция асинхронной загрузки успешно передает некоторые данные.
        /// </summary>
        /// <param name="bytesReceived">Количество полученных байт с начала загрузки.</param>
        /// <param name="totalBytesToDownload">Количество байт доступных для загрузки.</param>
        /// <param name="progressPercentage">Процент прогресса загрузки.</param>
        private void OnDownloadProgressChanged(int bytesReceived, int totalBytesToDownload, int progressPercentage)
        {
            // Событие DownloadProgressChanged.
            var eh = DownloadProgressChanged;

            if (eh != null)
            {
                eh(bytesReceived, totalBytesToDownload, progressPercentage);
            }
        }

        // Вызов события DownloadFileCompleted, которое срабатывает когда операция асинхронной загрузки файла успешно завершена.
        /// <summary>
        /// Вызов события DownloadFileCompleted, которое срабатывает когда операция асинхронной загрузки файла успешно завершена.
        /// </summary>
        /// <param name="downloadFileCompleted">Завершена ли загрузка файла.</param>
        private void OnDownloadFileCompleted(bool downloadFileCompleted)
        {
            // Событие DownloadFileCompleted.
            var eh = DownloadFileCompleted;

            if (eh != null)
            {
                eh(downloadFileCompleted);
            }
        }

        // Получение списка релизов .
        /// <summary>
        /// Получение списка релизов.
        /// </summary>
        /// <returns>Список релизов.</returns>
        async public Task<dynamic> getReleases()
        {
            // Адрес списка релизов.
            string URL = "https://api.github.com/repos/" + author + "/" + repositoryName + "/releases";

            // Создание WebClient.
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers["User-Agent"] = userAgent;

                // Ответ от GitHub.
                dynamic response = null;

                try
                {
                    // Отправка запроса и получение ответа.
                    response = await webClient.DownloadStringTaskAsync(URL);
                } catch (Exception e)
                {
                    MessageBox.Show($"Error retrieving update information: {e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Десериализация ответа JSON в объект.
                releases = JsonConvert.DeserializeObject(response); 
            }

            return releases;
        }

        // Получение название первого ассета последнего релиза.
        /// <summary>
        /// Получение название первого ассета последнего релиза.
        /// </summary>
        /// <returns>Название первого ассета последнего релиза.</returns>
        public string getLatestAssetsName()
        {
            // Название ассета последнего релиза.
            string latestAssetsName = null;

            // Проверка объекта на равенство null.
            if (!object.ReferenceEquals(null, releases))
            {
                latestAssetsName = releases[0].assets[0].name;
            }

            return latestAssetsName;
        }

        // Получение название тега последнего релиза.
        /// <summary>
        /// Получение название тега последнего релиза.
        /// </summary>
        /// <returns>Название тега последнего релиза.</returns>
        public string getLatestTagName()
        {
            // Название тега последнего релиза.
            string latestTagName = null;

            // Проверка объекта на равенство null.
            if (!object.ReferenceEquals(null, releases))
            {
                latestTagName = releases[0].tag_name;
            }

            return latestTagName;
        }

        // Получение ссылки на последний релиз.
        /// <summary>
        /// Получение ссылки на последний релиз.
        /// </summary>
        /// <returns>Ссылка на последний релиз.</returns>
        public string getLatestReleaseUrl()
        {
            // Ссылка на последний релиз.
            string latestReleaseUrl = null;

            // Проверка объекта на равенство null.
            if (!object.ReferenceEquals(null, releases))
            {
                latestReleaseUrl = releases[0].html_url;
            }

            return latestReleaseUrl;
        }

        // Получение ассетов по названию тега.
        /// <summary>
        /// Получение ассетов по названию тега.
        /// </summary>
        /// <param name="tagName">Название тега.</param>
        /// <returns>Объект ассетов.</returns>
        public dynamic getAssetsByTagName(string tagName)
        {
            // Объект ассетов релиза.
            dynamic assets = null;

            // Проверка объекта на равенство null.
            if (!object.ReferenceEquals(null, releases))
            {

                // Поиск релиза с нужным именем тега.
                foreach (var release in releases)
                {
                    if (release.tag_name == tagName)
                    {
                        assets = release.assets;
                    }
                }
            }

            return assets;
        }

        // Получение ссылки на релиз из первого ассета.
        /// <summary>
        /// Получение ссылки на релиз из первого ассета.
        /// </summary>
        /// <param name="assets">Объект ассетов релиза.</param>
        /// <returns>Ссылка на релиз из первого ассета.</returns>
        public string getDownloadUrl(dynamic assets)
        {
            // Ссылка на релиз из первого ассета.
            string downloadUrl = null;

            // Проверка объекта на равенство null.
            if (!object.ReferenceEquals(null, assets))
            {
                downloadUrl = assets[0].browser_download_url;
            }

            return downloadUrl;
        }

        // Проверка необходимости обновления программы.
        /// <summary>
        /// Проверка необходимости обновления программы.
        /// </summary>
        /// <returns>Обновлять ли программу.</returns>
        public bool isUpdateNeeded()
        {
            // Требуется ли обновление программы.
            bool isUpdate = false;

            if (!string.IsNullOrEmpty(getLatestTagName()))
            {
                // Последняя версия программы на GitHub.
                int latestVersion = int.Parse(getLatestTagName().Replace("v", "").Replace(".", ""));
                // Текущая версия программы.
                int currentVersion = int.Parse(Application.ProductVersion.Replace(".", ""));
                
                // Если на GitHub есть свежая версия программы, то обновление необходимо.
                if (currentVersion < latestVersion)
                {
                    isUpdate = true;
                }
            }

            return isUpdate;
        }

        // Скачивание последнего обновления.
        /// <summary>
        /// Скачивание последнего обновления.
        /// </summary>
        /// <param name="downloadUrl">Ссылка на скачивание релиза.</param>
        public void DownloadLatestUpdate(string downloadUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(downloadUrl))
                {
                    // Создание WebClient.
                    using (WebClient webClient = new WebClient())
                    {
                        // Название первого ассета последнего релиза.
                        string latestAssetsName = getLatestAssetsName();

                        webClient.Headers["User-Agent"] = userAgent;

                        // Добавление обработчика успешной передачи данных.
                        webClient.DownloadProgressChanged += (s, e) =>
                        {
                            OnDownloadProgressChanged((int)e.BytesReceived, (int)e.TotalBytesToReceive, e.ProgressPercentage);
                        };

                        // Добавление обработчика успешного завершение передачи данных.
                        webClient.DownloadFileCompleted += (s, e) =>
                        {
                            OnDownloadFileCompleted(false);
                        };

                        // Скачивание обновления.
                        webClient.DownloadFileAsync(new Uri(downloadUrl), $"{downloadDirectory}\\{latestAssetsName}");
                    }
                }
            }
            catch (Exception e){
                MessageBox.Show($"Update download error: {e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Установка обновления программы.
        /// <summary>
        /// Установка обновления программы.
        /// </summary>
        public void InstallUpdate()
        {
            // Название последнего ассета.
            string latestAssetsName = getLatestAssetsName();

            var process = new ProcessStartInfo();

            process.FileName = $"{downloadDirectory}\\{latestAssetsName}"; // Путь к установщику.
            process.Arguments = "/SILENT"; // Тихая установка.

            // Запуск установщика обновления программы.
            Process.Start(process);
        }
    }
}
