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
            var angles = new List<float>(tm1ReadingLength);

            float angle = 0;
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

            var tensionKgf = new List<string>();

            List<string[]> dataGridViewValues = formControl.GetDataGridViewValues(dataGridView);
            List<string> tensions = formControl.GetValuesFromGroupControls(groupBox, controlsNameTm1Reading);

            tensions.Reverse();

            foreach (string tension in tensions)
            {
                bool isFound = false;

                for (int j = 0; j < dataGridViewValues[0].Length; j++)
                {
                    if (tension == dataGridViewValues[0][j])
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
