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

		private string ConverterReadSetting(string key)
		{
			string newKey = key;

			if (key.Contains("SideSpokesTm1ReadingNumericUpDown"))
			{
				newKey = key.Replace("SideSpokesTm1ReadingNumericUpDown", "SideSpokesNumericUpDown");
			}
			else if (key == "varianceTrackBarValue")
			{
				newKey = "varianceComboBoxSelectedItem";
			}

			return newKey;
		}

		private string ConverterLoadSetting(string key)
        {
			string newKey = key;

			if (key.Contains("SideSpokesNumericUpDown"))
			{
				newKey = key.Replace("SideSpokesNumericUpDown", "SideSpokesTm1ReadingNumericUpDown");
			}

			if (key.Contains("varianceComboBoxSelectedItem"))
			{
				newKey = key.Replace("varianceComboBoxSelectedItem", "varianceTrackBarValue");
			}

			return newKey;
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
					value = settings[ConverterReadSetting(key)].Value;
				}
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error reading app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			return value;
		}

		public void AddSetting(string key, string value)
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

		public void UpdateSetting(string key, string value)
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

		public Dictionary<string, string> LoadSettings()
		{
			var settings = new Dictionary<string, string>();
			var configFile = GetConfig();
			var configSettings = configFile.AppSettings.Settings;

			if (!String.IsNullOrEmpty(configPath))
			{
                foreach (var key in configSettings.AllKeys)
                {
					settings.Add(ConverterLoadSetting(key), ReadSetting(key));
				}
			}

			return settings;
		}

		public void SaveSettings(Dictionary<string, string> settings)
		{
            foreach (KeyValuePair<string, string> setting in settings)
			{
				var configFile = GetConfig();
				var configSettings = configFile.AppSettings.Settings;

				if (configSettings[setting.Key] == null)
				{
					AddSetting(setting.Key, setting.Value);
				} else
                {
					UpdateSetting(setting.Key, setting.Value);
				}
			}
		}
	}
}