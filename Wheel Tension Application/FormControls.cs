// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    /*
     * Класс FormControls для работы с элементами управления формы.
     * Этот класс позволяет работать с элементами управления формы.
     */
    /// <summary>
    /// Класс <c>FormControls</c> для работы с элементами управления формы.
    /// </summary>
    /// <remarks>
    /// Этот класс позволяет работать с элементами управления формы.
    /// </remarks>
    class FormControls
    {
        // Добавление группы элементов управления формы в GroupBox.
        /// <summary>
        /// Добавление группы элементов управления формы в GroupBox.
        /// </summary>
        /// <param name="groupBox">GroupBox на который будут добавлены элементы управления формы.</param>
        /// <param name="controlForAdding">Элемент управления формы, который будет добавлен в GroupBox.</param>
        /// <param name="controlProperties">Свойства добавляемого элемента управления формы.</param>
        /// <param name="controlName">Имя добавляемого элемента управления формы.</param>
        /// <param name="controlHeight">Высота добавляемого элемента управления формы.</param>
        /// <param name="controlCount">Количество добавляемых элементов управления формы.</param>
        /// <param name="stepBetweenControls">Отступ между добавляемыми элементами управления формы.</param>
        /// <param name="offsetX">Отступ по горизонтали от элемента управления формы.</param>
        /// <param name="offsetY">Отступ по вертикали от элемента управления формы.</param>
        public void AddGroupControlsToGroupBox(
            GroupBox groupBox,
            Control controlForAdding,
            List<string> controlProperties,
            string controlName,
            int controlHeight,
            int controlCount,
            int stepBetweenControls,
            int offsetX,
            int offsetY)
        {
            var itemsCount = groupBox.Controls.OfType<Control>().Count();

            int indent = offsetY;

            if (controlCount < itemsCount)
            {
                foreach (Control item in groupBox.Controls.OfType<Control>().Reverse())
                {
                    if (item.Name.IndexOf(controlName) > -1)
                    {
                        groupBox.Controls.Remove(item);
                    }
                }
            }

            for (int i = 0; i < controlCount; i++)
            {
                var indexAdd = i + 1;

                Type type = controlForAdding.GetType();
                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                Control newControl = (Control)ctor.Invoke(null);

                newControl.Name = controlName + indexAdd;
                newControl.Location = new Point(offsetX, indent);

                // Установка свойств добавляемого элемента управления формы из указанного элемента с заданными свойствами
                foreach (string property in controlProperties)
                {
                    PropertyInfo propertyInfo = controlForAdding.GetType().GetProperty(property);
                    propertyInfo.SetValue(newControl, Convert.ChangeType(propertyInfo.GetValue(controlForAdding), propertyInfo.PropertyType), null);
                }

                groupBox.Controls.Add(newControl);

                indent += controlHeight + stepBetweenControls;
            }
        }

        // Добавление группы NumericUpDown в GroupBox.
        /// <summary>
        /// Добавление группы NumericUpDown в GroupBox.
        /// </summary>
        /// <param name="groupBox">GroupBox на который будут добавлены NumericUpDown.</param>
        /// <param name="controlName">Имя добавляемых NumericUpDown.</param>
        /// <param name="controlProperties">Свойства добавляемого NumericUpDown.</param>
        /// <param name="controlHeight">Высота добавляемого NumericUpDown.</param>
        /// <param name="controlCount">Количество добавляемых NumericUpDown.</param>
        /// <param name="stepBetweenControls">Отступ между добавляемыми NumericUpDown.</param>
        /// <param name="offsetX">Отступ по горизонтали от элемента управления формы до NumericUpDown.</param>
        /// <param name="offsetY">Отступ по вертикали от элемента управления формы до NumericUpDown.</param>
        public void AddNumericUpDownToGroupBox(
            GroupBox groupBox,
            string controlName,
            List<string> controlProperties,
            int controlWidth,
            int controlHeight,
            int controlCount,
            int stepBetweenControls,
            int offsetX,
            int offsetY)
        {
            NumericUpDown numericUpDown = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 44,
                DecimalPlaces = 0,
                Increment = 1M,
                Size = new Size(controlWidth, controlHeight)
            };

            AddGroupControlsToGroupBox(
                groupBox,
                numericUpDown,
                controlProperties,
                controlName,
                controlHeight,
                controlCount,
                stepBetweenControls,
                offsetX,
                offsetY
            );
        }

        // Добавление группы TextBox в GroupBox.
        /// <summary>
        /// Добавление группы TextBox в GroupBox.
        /// </summary>
        /// <param name="groupBox">GroupBox на который будут добавлены TextBox.</param>
        /// <param name="controlName">Имя добавляемых TextBox.</param>
        /// <param name="controlProperties">Свойства добавляемого TexBox.</param>
        /// <param name="controlHeight">Высота добавляемого TextBox.</param>
        /// <param name="controlCount">Количество добавляемых TextBox.</param>
        /// <param name="stepBetweenControls">Отступ между добавляемыми TextBox.</param>
        /// <param name="offsetX">Отступ по горизонтали от элемента управления формы до TextBox.</param>
        /// <param name="offsetY">Отступ по вертикали от элемента управления формы до TextBox.</param>
        public void AddTextBoxToGroupBox(
            GroupBox groupBox,
            string controlName,
            List<string> controlProperties,
            int controlWidth,
            int controlHeight,
            int controlCount,
            int stepBetweenControls,
            int offsetX,
            int offsetY)
        {
            TextBox textBox = new TextBox()
            {
                Enabled = false,
                Size = new Size(controlWidth, controlHeight)
            };

            AddGroupControlsToGroupBox(
                groupBox,
                textBox,
                controlProperties,
                controlName,
                controlHeight,
                controlCount,
                stepBetweenControls,
                offsetX,
                offsetY
            );
        }

        // Получение отступа от элемента формы.
        /// <summary>
        /// Получение отступа от элемента формы.
        /// </summary>
        /// <param name="control">Элемент управления формы отступ от которого будет рассчитан.</param>
        /// <returns>Отступ от элемента управления формы.</returns>
        public int GetOffsetFromControl(Control control)
        {
            return control.Location.Y + control.Size.Height;
        }

        // Получение значений указанных элементов из GroupBox.
        /// <summary>
        /// Получение значений указанных элементов из GroupBox.
        /// </summary>
        /// <param name="groupBox">GroupBox, значения элементов управления формы которого будут получены.</param>
        /// <param name="controlsName">Имя элементов управления формы значения которых надо получить.</param>
        /// <returns>Словарь значений указанных элементов из GroupBox.</returns>
        public Dictionary<Control, string> GetValuesFromGroupControls(GroupBox groupBox, string controlsName)
        {
            var values = new Dictionary<Control, string>() { };

            foreach (Control item in groupBox.Controls.OfType<Control>())
            {
                if (item.Name.IndexOf(controlsName) > -1)
                {
                    values.Add(item, item.Text);
                }
            }

            return values;
        }

        // Установка значений выпадающего списка ComboBox.
        /// <summary>
        /// Установка значений выпадающего списка ComboBox.
        /// </summary>
        /// <param name="comboBox">ComboBox в который будут добавлены значения.</param>
        /// <param name="values">Значения, которые будут добавлены в выпадающий список ComboBox.</param>
        /// <param name="isAddRange">Добавлять ли значения в выпадающий список ComboBox.</param>
        /// <param name="isEnabled">Флаг доступности ComboBox.</param>
        public void SetValuesToGroupControlsText(GroupBox groupBox, string controlsName, string[] valuesForAdding)
        {
            int index = 0;

            foreach (Control item in groupBox.Controls.OfType<Control>())
            {
                if (item.Name.IndexOf(controlsName) > -1)
                {
                    item.Text = valuesForAdding[index];

                    index++;
                }
            }
        }

        // Установка значений выпадающего списка ComboBox.
        /// <summary>
        /// Установка значений выпадающего списка ComboBox.
        /// </summary>
        /// <param name="comboBox">ComboBox в который будут добавлены значения.</param>
        /// <param name="values">Значения, которые будут добавлены в выпадающий список ComboBox.</param>
        /// <param name="isAddRange">Добавлять ли значения в выпадающий список ComboBox.</param>
        /// <param name="isEnabled">Флаг доступности ComboBox.</param>
        public void SetComboBoxValue(ComboBox comboBox, List<string> values, bool isAddRange, bool isEnabled)
        {
            comboBox.Items.Clear();

            if (isAddRange)
            {
                comboBox.Items.AddRange(values.ToArray());
            }

            comboBox.Enabled = isEnabled;
        }

        // Получение значений строк из DataGridView.
        /// <summary>
        /// Получение значений строк из DataGridView.
        /// </summary>
        /// <param name="dataGridView">DataGridView из которого будут считаны значения строк.</param>
        /// <returns>Массив значений строк из DataGridView.</returns>
        public List<string[]> GetDataGridViewValues(DataGridView dataGridView)
        {
            var values = new List<string[]>();

            for (var i = 0; i < dataGridView.Rows.Count; i++)
            {
                var columnValues = new string[dataGridView.Columns.Count];

                for (var j = 0; j < dataGridView.Columns.Count; j++)
                {
                    columnValues[j] = dataGridView.Rows[i].Cells[j].Value.ToString();
                }

                values.Add(columnValues);
            }

            return values;
        }

        // Заполнение DataGridView.
        /// <summary>
        /// Заполнение DataGridView.
        /// </summary>
        /// <param name="dataGridView">DataGridView в который будут добавлены строки.</param>
        /// <param name="columnCount">Количество добавляемых столбцов.</param>
        /// <param name="rowHeaders">Массив заголовков добавляемых строк.</param>
        /// <param name="rows">Массив значений добавляемых строк.</param>
        public void SetDataGridViewValues(DataGridView dataGridView, int columnCount, string[] rowHeaders, List<string[]> rows)
        {
            dataGridView.ColumnCount = columnCount;
            dataGridView.RowHeadersWidth = 165;

            try
            {
                for (var i = 0; i < columnCount; i++)
                {
                    dataGridView.Columns[i].Name = "";
                }

                dataGridView.Rows.Clear();

                if (rows.Count != rowHeaders.Length)
                {
                    MessageBox.Show("Number of row headers must be equal number of rows!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    for (var i = 0; i < rows.Count; i++)
                    {
                        dataGridView.Rows.Add(rows[i]);
                        dataGridView.Rows[i].HeaderCell.Value = rowHeaders[i];
                    }
                }
            }
            catch (System.DivideByZeroException)
            {
                MessageBox.Show("There are no such parameters in the database!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
