// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    class Database
    {
        public List<string> ExecuteSelectQuery(string connectionString, string command, Dictionary<string, string> parameters)
        {
            var text = new List<string>();

            try
            {
                using (var objConnection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand objCommand = objConnection.CreateCommand())
                    {
                        objConnection.Open();
                        objCommand.CommandText = command;

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
                MessageBox.Show(exception.Message, "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return text;
        }
    }
}
