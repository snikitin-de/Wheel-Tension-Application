// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
	/*
     * Класс AppSettings для работы с конфигурационными файлами настроек программы.
     * Этот класс позволяет сохранять и загружать настройки программы из конфигурационных файлов.
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

		// Получение конфигурационного файла.
		/// <summary>
		/// Получение конфигурационного файла.
		/// </summary>
		/// <returns>Конфигурационный файл.</returns>
		private Configuration GetConfig()
		{
			// Определение пользовательской иерархии файлов конфигурации для EXE-приложения.
			var map = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
			var configFile = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			return configFile;
		}

		// Конвертация ключей старого конфигурационного файла в новый формат.
		/// <summary>
		/// Конвертация ключей старого конфигурационного файла в новый формат.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <returns>Сконвертированный ключ настройки в новый формат.</returns>
		/// <example>
		/// <code>
		/// ConverterSetting("varianceComboBoxSelectedItem");
		/// </code>
		/// </example>
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

		// Конвертация значения из старого конфигурационного файла в новый формат.
		/// <summary>
		/// Конвертация значения из старого конфигурационного файла в новый формат.
		/// </summary>
		/// <param name="value">Значение настройки.</param>
		/// <returns>Сконвертированное значение настройки в новый формат.</returns>
		/// <example>
		/// <code>
		/// ConverterValue("20%");
		/// </code>
		/// </example>
		private string ConverterValue(string value)
		{
			var newValue = value;

			if (value.Contains("%"))
            {
				newValue = value.Replace("%", "");
			}

			return newValue;
		}

		// Чтение настройки из конфигурационного файла.
		/// <summary>
		/// Чтение настройки из конфигурационного файла.
		/// </summary>
		/// <param name="key">Идентификатор настройки.</param>
		/// <returns>Значение настройки по указанному ключу.</returns>
		/// <example>
		/// <code>
		/// ReadSetting("varianceTrackBarValue");
		/// </code>
		/// </example>
		public string ReadSetting(string key)
		{
			// Считанное значение настройки из конфигурационного файла.
			string value = null;

			try
			{
				// Конфигурационный файл и настройки программы.
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				// Если настройка с указанным ключом есть в конфигурационном файле, то получааем ее.
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

		// Добавление настройки в конфигурационный файл.
		/// <summary>
		/// Добавление настройки в конфигурационный файл.
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
				// Конфигурационный файл и настройки программы.
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				// Добавление настройки в конфигурационный файл.
				settings.Add(key, value);

				// Сохранение конфигурационного файла.
				SaveSetting(configFile);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error writing app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// Обновление настройки в конфигурационном файле.
		/// <summary>
		/// Обновление настройки в конфигурационном файле.
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
				// Конфигурационный файл и настройки программы.
				var configFile = GetConfig();
				var settings = configFile.AppSettings.Settings;

				// Обновление существующей настройки в конфигурационном файле.
				settings[key].Value = value;

				// Сохранение конфигурационного файла.
				SaveSetting(configFile);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show("Error writing app settings!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		// Сохранение конфигурационного файла.
		/// <summary>
		/// Сохранение конфигурационного файла.
		/// </summary>
		/// <param name="configFile">Конфигурационный файл.</param>
		/// <example>
		/// <code>
		/// var configFile = GetConfig();
		/// SaveSetting(configFile);
		/// </code>
		/// </example>
		public void SaveSetting(Configuration configFile)
		{
			// Сохранение конфигурационного файла.
			configFile.Save(ConfigurationSaveMode.Modified);
			// Обновляние раздела с заданным именем, чтобы при следующем извлечении он повторно считывался с диска.
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}

		// Загрузка настроек из конфигурационного файла.
		/// <summary>
		/// Загрузка настроек из конфигурационного файла.
		/// </summary>
		/// <returns>Словарь настроек программы в формате настройка-значение настройки, загруженных из конфигурационного файла.</returns>
		/// <example>
		/// <code>
		/// LoadSettings();
		/// </code>
		/// </example>
		public Dictionary<string, string> LoadSettings()
		{
			// Настройки программы в формате ключ-значение.
			var settings = new Dictionary<string, string>();

			// Конфигурационный файл и настройки программы.
			var configFile = GetConfig();
			var configSettings = configFile.AppSettings.Settings;

			// Проверка, что путь к конфигурационному файлу не пустой.
			if (!String.IsNullOrEmpty(configPath))
			{
				// Добавляем все настройки из конфигурационного файла в словарь.
                foreach (var key in configSettings.AllKeys)
                {
					settings.Add(ConverterSetting(key), ConverterValue(ReadSetting(key)));
				}
			}

			return settings;
		}

		// Сохранение настроек в конфигурационный файл.
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
			// Считывание настроек и их значений из словаря.
            foreach (KeyValuePair<string, string> setting in settings)
			{
				// Конфигурационный файл и настройки программы.
				var configFile = GetConfig();
				var configSettings = configFile.AppSettings.Settings;

				// Если настройки нет в конфигурационном файле, то добавляем ее, иначе обновляем.
				if (configSettings[setting.Key] == null)
				{
					// Добавление настройки в конфигурационный файл.
					AddSetting(setting.Key, setting.Value);
				} else
                {
					// Обновление настройки в конфигурационном файле.
					UpdateSetting(setting.Key, setting.Value);
				}
			}
		}
	}
}