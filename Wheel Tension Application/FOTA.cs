using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    class FOTA
    {
        private string author;
        private string repositoryName;
        private string downloadDirectory;
        private string userAgent;

        private dynamic releases = null;

        public event Action<int, int, int> DownloadProgressChanged;
        public event Action<bool> DownloadFileCompleted;

        public string AppName { get; set; }

        public FOTA(string author, string repositoryName, string userAgent, string appName, string downloadDirectory)
        {
            this.author = author;
            this.repositoryName = repositoryName;
            this.downloadDirectory = downloadDirectory;
            this.userAgent = userAgent;

            AppName = appName;
        }

        private void OnDownloadProgressChanged(int bytesReceived, int totalBytesToDownload, int progressPercentage)
        {
            var eh = DownloadProgressChanged;

            if (eh != null)
            {
                eh(bytesReceived, totalBytesToDownload, progressPercentage);
            }
        }

        private void OnDownloadFileCompleted(bool downloadFileCompleted)
        {
            var eh = DownloadFileCompleted;

            if (eh != null)
            {
                eh(downloadFileCompleted);
            }
        }

        async public Task<dynamic> getReleases()
        {
            string URL = "https://api.github.com/repos/" + author + "/" + repositoryName + "/releases";

            using (WebClient webClient = new WebClient())
            {
                webClient.Headers["User-Agent"] = userAgent;

                dynamic response = null;

                try
                {
                    response = await webClient.DownloadStringTaskAsync(URL);
                } catch (Exception e)
                {
                    MessageBox.Show($"Error retrieving update information: { e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                releases = JsonConvert.DeserializeObject(response); 
            }

            return releases;
        }

        public string getLatestAssetsName()
        {
            string latestAssetsName = null;

            if (!object.ReferenceEquals(null, releases))
            {
                latestAssetsName = releases[0].assets[0].name;
            }

            return latestAssetsName;
        }

        public string getLatestTagName()
        {
            string latestTagName = null;

            if (!object.ReferenceEquals(null, releases))
            {
                latestTagName = releases[0].tag_name;
            }

            return latestTagName;
        }

        public string getLatestReleaseUrl()
        {
            string latestReleaseUrl = null;

            if (!object.ReferenceEquals(null, releases))
            {
                latestReleaseUrl = releases[0].html_url;
            }

            return latestReleaseUrl;
        }

        public dynamic getAssetsByTagName(string tagName)
        {
            dynamic assets = null;

            if (!object.ReferenceEquals(null, releases))
            {
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

        public string getDownloadUrl(dynamic assets)
        {
            string downloadUrl = null;

            if (!object.ReferenceEquals(null, assets))
            {
                downloadUrl = assets[0].browser_download_url;
            }

            return downloadUrl;
        }

        public bool isUpdateNeeded()
        {
            bool isUpdate = false;

            if (!string.IsNullOrEmpty(getLatestTagName()))
            {
                string latestTagName = getLatestTagName().Replace("v", "");

                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                if (!currentVersion.Contains(latestTagName))
                {
                    isUpdate = true;
                }
            }

            return isUpdate;
        }

        public void DownloadLatestUpdate(string downloadUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(downloadUrl))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string latestAssetsName = getLatestAssetsName();

                        webClient.Headers["User-Agent"] = userAgent;

                        CreateDownloadDir();

                        webClient.DownloadProgressChanged += (s, e) =>
                        {
                            OnDownloadProgressChanged((int)e.BytesReceived, (int)e.TotalBytesToReceive, e.ProgressPercentage);
                        };
                        webClient.DownloadFileCompleted += (s, e) =>
                        {
                            OnDownloadFileCompleted(false);
                        };
     
                        webClient.DownloadFileAsync(new Uri(downloadUrl), $"{downloadDirectory}\\{latestAssetsName}");
                    }
                }
            }
            catch (Exception e){
                MessageBox.Show($"Update download error: { e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void InstallUpdate()
        {
            string latestAssetsName = getLatestAssetsName();

            var process = new ProcessStartInfo();

            process.FileName = $"{downloadDirectory}\\{latestAssetsName}";
            process.Arguments = "/SILENT";

           //Process.Start(process);
        }

        private void CreateDownloadDir()
        {
            Directory.CreateDirectory(downloadDirectory);
        }
    }
}
