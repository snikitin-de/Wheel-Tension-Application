// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    /*
     * Класс Database для работы с БД SQLite.
     * Этот класс позволяет выполнять SQL запросы к БД SQLite.
     */
    /// <summary>
    /// Класс <c>Database</c> для работы с БД SQLite.
    /// </summary>
    /// <remarks>
    /// Этот класс может выполнять SQL запросы к БД SQLite.
    /// </remarks>
    class Database
    {
        // Выполнение SQL запроса к БД SQLite.
        /// <summary>
        /// Выполнение SQL запроса к БД SQLite.
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД SQLite.</param>
        /// <param name="command">Текст SQL запроса.</param>
        /// <param name="parameters">Словарь параметров в формате (параметр, значение) для подстановки их значений в тело SQL запроса.</param>
        /// <returns>Список значений первого столбца в конструкции SELECT SQL запроса.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// var shapesListCommand = @"
        ///     SELECT shape
        ///     FROM tm1_conversion_table
        ///     WHERE material = @material
        ///     GROUP BY shape";
        /// parameters = new Dictionary<string, string>()
        ///    {
        ///        { "@material", "Steel" }
        ///    };
        /// List<string> shapesList = database.ExecuteSelectQuery(connectionString, shapesListCommand, parameters);
        /// ]]>
        /// </code>
        /// </example>
        public List<string> ExecuteSelectQuery(string connectionString, string command, Dictionary<string, string> parameters)
        {
            // Список значений первого столбца из SQL запроса.
            var text = new List<string>();

            try
            {
                using (var objConnection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand objCommand = objConnection.CreateCommand())
                    {
                        objConnection.Open();
                        objCommand.CommandText = command;

                        // Если число параметров больше 0, то добавляем их в объект подключения.
                        if (parameters.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> parameter in parameters)
                            {
                                var param = new SQLiteParameter(parameter.Key) { Value = parameter.Value };

                                objCommand.Parameters.Add(param);
                            }
                        }

                        using (SQLiteDataReader reader = objCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Получаем значение только первого поля из SQL запроса.
                                var row = reader.GetValue(0).ToString();

                                text.Add(row);
                            }
                        }

                        objConnection.Close();
                    }
                }
            }
            catch (SQLiteException exception)
            {
                MessageBox.Show(exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return text;
        }
    }
}
