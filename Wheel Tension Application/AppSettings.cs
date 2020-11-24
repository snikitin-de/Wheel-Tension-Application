// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Configuration;
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

		private string ConverterSetting(string key)
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

		private string ConverterValue(string value)
		{
			var newValue = value;

			if (value.Contains("%"))
            {
				newValue = value.Replace("%", "");
			}

			return newValue;
		}

		// Чтение настройки из конфигурационного файла
		/// <summary>
		/// Чтение настройки из конфигурационного файла.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <returns>Значение настройки по указанному ключу.</returns>
		/// <example>
		/// <code>
		/// ReadSetting("varianceTrackBarValue")
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
					MessageBox.Show(
					   $"Error reading app settings!\nValue for key {key} not found!",
					   Application.ProductName,
					   MessageBoxButtons.OK,
					   MessageBoxIcon.Warning);
				}
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error reading app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			return value;
		}

		// Добавление настройки в конфигурационный файл
		/// <summary>
		///Добавление настройки в конфигурационный файл.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <param name="value">Значение настройки.</param>
		/// <example>
		/// <code>
		/// AddSettings("varianceTrackBarValue", "10");
		/// </code>
		/// </example>
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

		// Обновление настройки в конфигурационном файле
		/// <summary>
		///Обновление настройки в конфигурационном файле.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <param name="value">Значение настройки.</param>
		/// <example>
		/// <code>
		/// UpdateSettings("varianceTrackBarValue", "15");
		/// </code>
		/// </example>
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

		// Загрузка настроек из конфигурационного файла
		/// <summary>
		/// Загрузка настроек из конфигурационного файла.
		/// </summary>
		/// <returns>Словарь настроек программы в формате настройка-значение настройки, загруженных из конфигурационного файла.</returns>
		/// <example>
		/// <code>
		/// var appSettingPath = saveFileDialog.FileName;
		/// LoadSettings(appSettingPath)
		/// </code>
		/// </example>
		public Dictionary<string, string> LoadSettings()
		{
			var settings = new Dictionary<string, string>();
			var configFile = GetConfig();
			var configSettings = configFile.AppSettings.Settings;

			if (!String.IsNullOrEmpty(configPath))
			{
                foreach (var key in configSettings.AllKeys)
                {
					settings.Add(ConverterSetting(key), ConverterValue(ReadSetting(key)));
				}
			}

			return settings;
		}

		// Сохранение настроек в конфигурационный файл
		/// <summary>
		/// Сохранение настроек в конфигурационный файл.
		/// </summary>
		/// <param name="settings">Словарь настроек в формате настройка-значение.</param>
		/// <example>
		/// <code>
		/// var settings = new Dictionary<string, string>();
		/// settings.Add("varianceTrackBarValue", "20");
		/// SaveSettings(settings)
		/// </code>
		/// </example>
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