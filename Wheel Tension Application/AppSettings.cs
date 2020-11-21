// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
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
				}
				else
				{
					if (key.Contains("SideSpokesTm1ReadingNumericUpDown"))
					{
						Regex regex = new Regex(@"(.*)SideSpokesTm1ReadingNumericUpDown(\d+)");
						Match match = regex.Match(key);

						string side = match.Groups[1].Value;
						string number = match.Groups[2].Value;

						value = settings[$"{side}SideSpokesNumericUpDown{number}"].Value;
					}
					else if (key == "varianceTrackBarValue")
					{
						value = settings["varianceComboBoxSelectedItem"].Value;
						value = value.Remove(value.Length - 1);
					}
					else
					{
						MessageBox.Show(
							$"Error reading app settings!\nValue for key {key} not found!",
							Application.ProductName,
							MessageBoxButtons.OK,
							MessageBoxIcon.Warning);
					}
				}
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error reading app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			return value;
		}

		public void AddAppSettings(string key, string value)
		{
			try
			{
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				settings.Add(key, value);

				SaveSetting(configFile);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error writing app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		public void UpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				settings[key].Value = value;

				SaveSetting(configFile);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error writing app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		public void SaveSetting(Configuration configFile)
		{
			configFile.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}

		public Dictionary<string, string> LoadSettings(string appSettingPath)
		{
			var settings = new Dictionary<string, string>();

			if (!String.IsNullOrEmpty(appSettingPath))
			{
				var appSettings = new AppSettings(appSettingPath);

				settings.Add("materialComboBoxSelectedItem", ReadSetting("materialComboBoxSelectedItem"));
				settings.Add("shapeComboBoxSelectedItem", ReadSetting("shapeComboBoxSelectedItem"));
				settings.Add("thicknessComboBoxSelectedItem", ReadSetting("thicknessComboBoxSelectedItem"));
				settings.Add("varianceTrackBarValue", ReadSetting("varianceTrackBarValue"));
				settings.Add("leftSideSpokeCountComboBoxSelectedItem", ReadSetting("leftSideSpokeCountComboBoxSelectedItem"));
				settings.Add("rightSideSpokeCountComboBoxSelectedItem", ReadSetting("rightSideSpokeCountComboBoxSelectedItem"));

				if (!String.IsNullOrEmpty(appSettings.ReadSetting("leftSideSpokeCountComboBoxSelectedItem")))
				{
					var itemCount = int.Parse(appSettings.ReadSetting("leftSideSpokeCountComboBoxSelectedItem"));

					for (int i = 0; i < itemCount; i++)
					{
						var key = $"leftSideSpokesTm1ReadingNumericUpDown{i + 1}";
						settings.Add(key, appSettings.ReadSetting(key));
					}
				}

				if (!String.IsNullOrEmpty(appSettings.ReadSetting("rightSideSpokeCountComboBoxSelectedItem")))
				{
					var itemCount = int.Parse(appSettings.ReadSetting("rightSideSpokeCountComboBoxSelectedItem"));

					for (int i = 0; i < itemCount; i++)
					{
						var key = $"rightSideSpokesTm1ReadingNumericUpDown{i + 1}";
						settings.Add(key, appSettings.ReadSetting(key));
					}
				}
			}

			return settings;
		}

		public void SaveSettings(string appSettingPath, Dictionary<string, string> settings)
		{
            foreach (KeyValuePair<string, string> setting in settings)
			{
				var configFile = GetConfig();
				var configSettings = configFile.AppSettings.Settings;

				if (configSettings[setting.Key] == null)
				{
					AddAppSettings(setting.Key, setting.Value);
				} else
                {
					UpdateAppSettings(setting.Key, setting.Value);
				}
			}
		}
	}
}