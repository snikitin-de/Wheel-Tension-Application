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
        public void AddGroupControlsToGroupBox(GroupBox groupBox, Control controlForAdding, List<string> controlProperties, string controlName, int controlHeight, int controlCount, int stepBetweenControls, int offsetX, int offsetY)
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

                foreach (string property in controlProperties)
                {
                    PropertyInfo propertyInfo = controlForAdding.GetType().GetProperty(property);
                    propertyInfo.SetValue(newControl, Convert.ChangeType(propertyInfo.GetValue(controlForAdding), propertyInfo.PropertyType), null);
                }

                groupBox.Controls.Add(newControl);

                indent += controlHeight + stepBetweenControls;
            }
        }

        public void AddNumericUpDownToGroupBox(GroupBox groupBox, string controlName, List<string> controlProperties, int controlWidth, int controlHeight, int controlCount, int stepBetweenControls, int offsetX, int offsetY)
        {
            NumericUpDown numericUpDown = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 44,
                DecimalPlaces = 1,
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

        public int GetOffsetFromControl(Control control)
        {
            return control.Location.Y + control.Size.Height;
        }

        public List<float> GetValuesFromGroupControls(GroupBox groupBox, string controlsName)
        {
            var values = new List<float>() { };

            foreach (Control item in groupBox.Controls.OfType<NumericUpDown>().Reverse())
            {
                if (item.Name.IndexOf(controlsName) > -1)
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
