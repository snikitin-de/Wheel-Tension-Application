// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
	class AppSettings
	{
		private string configPath;

		public AppSettings(string appSettingsPath)
		{
			configPath = appSettingsPath;
		}

		private Configuration GetConfig()
        {
			var map = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
			var configFile = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			return configFile;
		}

		public string ReadSetting(string key)
		{
			string value = null;

			try
			{
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				if (settings[key] != null)
                {
					value = settings[key].Value;
				} else
                {
					if (key.Contains("SideSpokesTm1ReadingNumericUpDown"))
					{
						Regex regex = new Regex(@"(.*)SideSpokesTm1ReadingNumericUpDown(\d+)");
						Match match = regex.Match(key);

						string side = match.Groups[1].Value;
						string number = match.Groups[2].Value;

						value = settings[$"{side}SideSpokesNumericUpDown{number}"].Value;
					} else
                    {
						MessageBox.Show($"Error reading app settings!\nValue for key {key} not found!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error reading app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			return value;
		}

		public void AddUpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}

				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error writing app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}