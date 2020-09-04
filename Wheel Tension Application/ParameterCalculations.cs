// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
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

        public float[] CalculateTensionKgf(DataGridView dataGridView, GroupBox groupBox, string controlsNameTm1Reading)
        {
            var formControl = new FormControls();

            var tensionKgf = new List<float>();

            List<string[]> dataGridViewValues = formControl.GetDataGridViewValues(dataGridView);
            List<string> tensions = formControl.GetValuesFromGroupControls(groupBox, controlsNameTm1Reading);

            tensions.Reverse();

            foreach (string tension in tensions)
            {
                bool isFound = false;

                for (int j = 0; j < dataGridViewValues[0].Length; j++)
                {
                    string tensionFromTable = dataGridViewValues[0][j];

                    if (tension == tensionFromTable)
                    {
                        isFound = true;
                        tensionKgf.Add(float.Parse(dataGridViewValues[1][j]));
                    }
                }

                if (!isFound)
                {
                    tensionKgf.Add(0);
                }
            }

            return tensionKgf.ToArray();
        }

        public double StdDev(List<float> values)
        {
            double mean = values.Sum() / values.Count();

            var squares_query =
                from float value in values
                select (value - mean) * (value - mean);

            double sum_of_squares = squares_query.Sum();

            return Math.Sqrt(sum_of_squares / (values.Count() - 1));
        }

        public double TensionLimit(double averageSpokeTension, int variance, bool isLower)
        {
            double tensionLimit = 0;

            switch (isLower)
            {
                case true:
                    tensionLimit = averageSpokeTension - averageSpokeTension / 100 * variance;
                    break;
                case false:
                    tensionLimit = averageSpokeTension + averageSpokeTension / 100 * variance;
                    break;
            }

            return tensionLimit;
        }

        public bool isWithinTensionLimit(int tensionKgf, double lowerTensionLimit, double upperTensionLimit)
        {
            bool isWithinTensionLimit = false;

            if (tensionKgf >= lowerTensionLimit && tensionKgf <= upperTensionLimit)
            {
                isWithinTensionLimit = true;
            }

            return isWithinTensionLimit;
        }
    }
}
