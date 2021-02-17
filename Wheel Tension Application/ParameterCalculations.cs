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
        public List<float> CalculateSpokeAngles(byte spokeCount)
        {
            var angles = new List<float>(spokeCount);

            float angle = 0;
            float angleStep;

            angleStep = (float)360 / spokeCount;

            for (int i = 0; i < spokeCount; i++)
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
            List<string> tensions = formControl.GetValuesFromGroupControls(groupBox, controlsNameTm1Reading).Values.ToList();

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

        public void SetWithinTensionLimit(
            GroupBox groupBox,
            Control controlOffset,
            ErrorProvider errorProvider,
            string controlsName,
            double lowerTensionLimit,
            double upperTensionLimit)
        {
            errorProvider.Icon = icons.ErrorProviderError;

            FormControls formControl = new FormControls();
            Dictionary<Control, string> tensionsKgf = formControl.GetValuesFromGroupControls(groupBox, controlsName);

            errorProvider.Icon = icons.ErrorProviderError;

            foreach (KeyValuePair<Control, string> tensionKgf in tensionsKgf)
            {
                var withinTensionLimit = isWithinTensionLimit(int.Parse(tensionKgf.Value), lowerTensionLimit, upperTensionLimit);

                errorProvider.SetIconPadding(tensionKgf.Key, +(controlOffset.Size.Width / 2));

                var errorMessage = "";

                if (!withinTensionLimit)
                {
                    errorMessage = "Outside limit";
                }

                errorProvider.SetError(tensionKgf.Key, errorMessage);
            }
        }
    }
}
