// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    class ParameterCalculations
    {
        public List<float> CalculateSpokeAngles(List<float> tm1Reading)
        {
            var tm1ReadingLength = tm1Reading.Count;

            float angle = 0;

            List<float> angles = new List<float>(tm1ReadingLength);

            float angleStep;
            if (tm1Reading.Count > 0)
            {
                angleStep = (float)360 / tm1ReadingLength;
            }
            else
            {
                angleStep = (float)360 / 24;
            }

            for (int i = 0; i < tm1ReadingLength; i++)
            {
                angles.Add(angle);

                angle += angleStep;
            }

            return angles;
        }

        public void CalculateTensionKgf(DataGridView dataGridView, GroupBox groupBox, string controlsNameTm1Reading, string controlsNameTensionKgf)
        {
            var formControl = new FormControls();

            List<string[]> dataGridViewValues = formControl.GetDataGridViewValues(dataGridView);
            List<float> tensions = formControl.GetValuesFromGroupControls(groupBox, controlsNameTm1Reading);

            tensions.Reverse();

            var tensionKgf = new List<string>();

            foreach (float tension in tensions)
            {
                bool isFound = false;

                for (int j = 0; j < dataGridViewValues[0].Length; j++)
                {
                    if (tension == float.Parse(dataGridViewValues[0][j]))
                    {
                        isFound = true;
                        tensionKgf.Add(dataGridViewValues[1][j]);
                    }
                }

                if (!isFound)
                {
                    tensionKgf.Add("<null>");
                }
            }

            formControl.SetValuesToGroupControls(groupBox, controlsNameTensionKgf, tensionKgf.ToArray());
        }
    }
}
