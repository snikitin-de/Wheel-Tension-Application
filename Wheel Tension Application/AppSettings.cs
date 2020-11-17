// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
	/*
     * Класс AppSettings для работы с конфигурационными файлами настроек программы
     * Этот класс позволяет сохранять и загружать настройки программы из конфигурационных файлов
     */
	/// <summary>
	/// Класс <c>AppSettings</c> для работы с конфигурационными файлами настроек программы.
	/// </summary>
	/// <remarks>
	/// Этот класс позволяет сохранять и загружать настройки программы из конфигурационных файлов.
	/// </remarks>
	class AppSettings
	{
		private string configPath;

		public AppSettings(string appSettingsPath)
		{
			configPath = appSettingsPath;
		}

		// Получение конфигурационного файла
		/// <summary>
		/// Получение конфигурационного файла.
		/// </summary>
		/// <returns>Конфигурационный файл.</returns>
		private Configuration GetConfig()
		{
			var map = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
			var configFile = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			return configFile;
		}

		// Чтение настройки из конфигурационного файла
		/// <summary>
		/// Чтение настройки из конфигурационного файла.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <returns>Значение настройки по указанному ключу.</returns>
		/// <example>
		/// <code>
		/// ReadSetting("materialComboBoxSelectedItem")
		/// </code>
		/// </example>
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
					// Конвертер настроек со старых версий
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

		// Добавление или обновление настройки в конфигурационном файле
		/// <summary>
		///Добавление или обновление настройки в конфигурационном файле.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <param name="value">Значение настройки.</param>
		/// <example>
		/// <code>
		/// AddUpdateAppSettings("varianceTrackBarValue", "10");
		/// </code>
		/// </example>
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

		// Загрузка настроек из конфигурационного файла
		/// <summary>
		/// Загрузка настроек из конфигурационного файла.
		/// </summary>
		/// <param name="appSettingPath">Путь к конфигурационному файлу.</param>
		/// <returns>Словарь настроек программы в формате настройка-значение настройки, загруженных из конфигурационного файла.</returns>
		/// <example>
		/// <code>
		/// var appSettingPath = saveFileDialog.FileName;
		/// LoadSettings(appSettingPath)
		/// </code>
		/// </example>
		public Dictionary<string, string> LoadSettings(string appSettingPath)
		{
			var settings = new Dictionary<string, string>();

			if (!String.IsNullOrEmpty(appSettingPath))
			{
				var appSettings = new AppSettings(appSettingPath);

				// Чтение настроек программы
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

		// Сохранение настроек в конфигурационный файл
		/// <summary>
		/// Сохранение настроек в конфигурационный файл.
		/// </summary>
		/// <param name="appSettingPath">Путь к конфигурационному файлу.</param>
		/// <param name="settings">Словарь настроек в формате настройка-значение.</param>
		/// <example>
		/// <code>
		/// var appSettingPath = saveFileDialog.FileName;
		/// var settings = new Dictionary<string, string>();
		/// settings.Add("materialComboBoxSelectedItem", materialComboBoxSelected);
		/// SaveSettings(appSettingPath, settings)
		/// </code>
		/// </example>
		public void SaveSettings(string appSettingPath, Dictionary<string, string> settings)
		{
			// Добавление или обновление настроек программы
            foreach (KeyValuePair<string, string> setting in settings)
			{
				AddUpdateAppSettings(setting.Key, setting.Value);
			}
		}
	}
}