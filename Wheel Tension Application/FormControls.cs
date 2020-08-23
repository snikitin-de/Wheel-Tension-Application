using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    class FormControls
    {
        public void AddGroupControlsToGroupBox(GroupBox groupBox, Control control, List<string> controlProperties, string name, int indentX, int indentY, int controlHeight, int step, int count)
        {
            var itemsCount = groupBox.Controls.OfType<Control>().Count();

            int indent = indentY;

            if (count < itemsCount)
            {
                foreach (Control item in groupBox.Controls.OfType<Control>().Reverse())
                {
                    if (item.Name.IndexOf(name) > -1)
                    {
                        groupBox.Controls.Remove(item);
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                var indexAdd = i + 1;

                Type type = control.GetType();
                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                Control newControl = (Control)ctor.Invoke(null);

                newControl.Name = name + indexAdd;
                newControl.Location = new Point(indentX, indent);

                foreach (string property in controlProperties)
                {
                    PropertyInfo propertyInfo = control.GetType().GetProperty(property);
                    propertyInfo.SetValue(newControl, Convert.ChangeType(propertyInfo.GetValue(control), propertyInfo.PropertyType), null);
                }

                groupBox.Controls.Add(newControl);

                indent += controlHeight + step;
            }
        }

        public void AddNumericUpDownToGroupBox(ComboBox comboBox, GroupBox groupBox, string name, int stepControl, List<string> properties)
        {
            var selected = comboBox.GetItemText(comboBox.SelectedItem);

            int indentFromComboBox = GetIndentFromComboBox(comboBox, stepControl);

            NumericUpDown numericUpDown = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 44,
                DecimalPlaces = 1,
                Increment = 1M,
                Size = new Size(comboBox.Size.Width, comboBox.Size.Height)
            };

            AddGroupControlsToGroupBox(
                groupBox,
                numericUpDown,
                properties,
                name,
                comboBox.Location.X,
                indentFromComboBox,
                comboBox.Size.Height,
                stepControl,
                Int16.Parse(selected)
            );
        }

        public int GetIndentFromComboBox(ComboBox comboBox, int stepControl)
        {
            return comboBox.Location.Y + comboBox.Size.Height + stepControl;
        }

        public List<float> GetWheelTensions(string name, GroupBox groupBox)
        {
            var values = new List<float>() { };

            foreach (Control item in groupBox.Controls.OfType<NumericUpDown>().Reverse())
            {
                if (item.Name.IndexOf(name) > -1)
                {
                    values.Add(float.Parse(item.Text));
                }
            }

            return values;
        }

        public void SetComboBoxValue(ComboBox comboBox, List<string> values, bool isAddRange, bool isEnabled)
        {
            comboBox.Items.Clear();

            if (isAddRange)
            {
                comboBox.Items.AddRange(values.ToArray());
            }

            comboBox.Enabled = isEnabled;
        }

        public void SetDataGridViewValues(DataGridView dataGridView, int columnCount, string[] rowHeaders, List<string[]> rows)
        {
            dataGridView.ColumnCount = columnCount;
            dataGridView.RowHeadersWidth = 165;

            try
            {
                int columnsWidth = (dataGridView.Width - dataGridView.RowHeadersWidth) / dataGridView.ColumnCount;

                for (var i = 0; i < columnCount; i++)
                {
                    dataGridView.Columns[i].Name = "";
                    dataGridView.Columns[i].Width = columnsWidth;
                }

                dataGridView.Rows.Clear();

                if (rows.Count != rowHeaders.Length)
                {
                    MessageBox.Show("Number of row headers must be equal number of rows!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("There are no such parameters in the database!", "Wheel Tension Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
