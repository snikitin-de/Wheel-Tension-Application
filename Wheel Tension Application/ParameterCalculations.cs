// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Wheel_Tension_Application
{
    /*
     * Класс ParameterCalculations для расчета различных параметров спиц.
     * Этот класс позволяет расчитывать различные параметры спиц.
     */
    /// <summary>
    /// Класс <c>ParameterCalculations</c> для расчета различных параметров спиц.
    /// </summary>
    /// <remarks>
    /// Этот класс позволяет расчитывать различные параметры спиц.
    /// </remarks>
    class ParameterCalculations
    {
        // Расчет углов положения спиц.
        /// <summary>
        /// Расчет углов положения спиц.
        /// </summary>
        /// <param name="spokeCount">Количество спиц.</param>
        /// <returns>Массив углов положения спиц.</returns>
        public List<float> CalculateSpokeAngles(byte spokeCount)
        {
            var angles = new List<float>(spokeCount);

            float angle = 0;
            float angleStep;

            // Расчет шага в градусах между спицами.
            angleStep = (float)360 / spokeCount;

            // Формирование массива углов положения спиц.
            for (int i = 0; i < spokeCount; i++)
            {
                angles.Add(angle);

                angle += angleStep;
            }

            return angles;
        }

        // Расчет натяжения спиц.
        /// <summary>
        /// Расчет натяжения спиц.
        /// </summary>
        /// <param name="dataGridView">DataGridView на котором расположены элементы с Tm1Reading.</param>
        /// <param name="groupBox">GroupBox на котором расположены элементы с Tm1Reading.</param>
        /// <param name="controlsNameTm1Reading">Имя элементов с Tm1Reading.</param>
        /// <returns>Массив натяжения спиц.</returns>
        public float[] CalculateTensionKgf(DataGridView dataGridView, GroupBox groupBox, string controlsNameTm1Reading)
        {
            var formControl = new FormControls();

            var tensionKgf = new List<float>();

            // Получение сил натяжения спиц
            List<string[]> dataGridViewValues = formControl.GetDataGridViewValues(dataGridView);
            List<string> tensions = formControl.GetValuesFromGroupControls(groupBox, controlsNameTm1Reading).Values.ToList();

            foreach (string tension in tensions)
            {
                bool isFound = false;

                // Определение силы натяжения спиц по таблице.
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

        // Расчет стандартного отклонения натяжения спиц.
        /// <summary>
        /// Расчет стандартного отклонения натяжения спиц.
        /// </summary>
        /// <param name="values">Массив значений натяжения спиц.</param>
        /// <returns>Стандартное отклонение натяжения спиц.</returns>
        public double StdDev(List<float> values)
        {
            // Расчет среднего арифметического натяжения спиц.
            double mean = values.Sum() / values.Count();

            // Расчет суммы квадратов.
            var squares_query =
                from float value in values
                select (value - mean) * (value - mean);

            double sum_of_squares = squares_query.Sum();

            // Расчет стандартного отклонения натяжения спиц.
            return Math.Sqrt(sum_of_squares / (values.Count() - 1));
        }

        // Расчет границы натяжения спицы.
        /// <summary>
        /// Расчет границы натяжения спицы.
        /// </summary>
        /// <param name="averageSpokeTension">Среднее натяжение спиц.</param>
        /// <param name="variance">Допуск натяжения спицы.</param>
        /// <param name="isLower">Нижняя или верхняя граница интервала натяжения спицы.</param>
        /// <returns>Граница интервала натяжения спицы.</returns>
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

        // Определение допуска в различии натяжения.
        /// <summary>
        /// Определение допуска в различии натяжения.
        /// </summary>
        /// <param name="tensionKgf">Сила натяжения спицы.</param>
        /// <param name="lowerTensionLimit">Нижняя граница допустимого интервала натяжения.</param>
        /// <param name="upperTensionLimit">Верхняя граница допустимого интервала натяжения.</param>
        /// <returns>Попадает ли сила натяжения спицы в допустимый интервал.</returns>
        public bool isWithinTensionLimit(int tensionKgf, double lowerTensionLimit, double upperTensionLimit)
        {
            bool isWithinTensionLimit = false;

            if (tensionKgf >= lowerTensionLimit && tensionKgf <= upperTensionLimit)
            {
                isWithinTensionLimit = true;
            }

            return isWithinTensionLimit;
        }

        // Определение выхода натяжения спицы за границы допустимого интервала.
        /// <summary>
        /// Определение выхода натяжения спицы за границы допустимого интервала.
        /// </summary>
        /// <param name="groupBox">GroupBox на котором размещены элементы с TensionKgf.</param>
        /// <param name="controlOffset">Элемент от которого будет произведен отступ иконки выхода натяжения спицы за границы допустимого интервала.</param>
        /// <param name="errorProvider">ErrorProvider.</param>
        /// <param name="controlsName">Имя элементов с натяжением спиц.</param>
        /// <param name="lowerTensionLimit">Нижняя граница допустимого интервала натяжения.</param>
        /// <param name="upperTensionLimit">Верхняя граница допустимого интервала натяжения.</param>
        public void SetWithinTensionLimit(
            GroupBox groupBox,
            Control controlOffset,
            ErrorProvider errorProvider,
            string controlsName,
            double lowerTensionLimit,
            double upperTensionLimit)
        {
            // Установка иконки выхода натяжения спицы за границы допустимого интервала.
            errorProvider.Icon = icons.ErrorProviderError;

            FormControls formControl = new FormControls();
            Dictionary<Control, string> tensionsKgf = formControl.GetValuesFromGroupControls(groupBox, controlsName);

            errorProvider.Icon = icons.ErrorProviderError;

            foreach (KeyValuePair<Control, string> tensionKgf in tensionsKgf)
            {
                // Определение допуска натяжения спиц.
                var withinTensionLimit = isWithinTensionLimit(int.Parse(tensionKgf.Value), lowerTensionLimit, upperTensionLimit);

                // Установка отступа иконки от контрола.
                errorProvider.SetIconPadding(tensionKgf.Key, +(controlOffset.Size.Width / 2));

                // Формирование сообщения об ошибке.
                var errorMessage = "";

                if (!withinTensionLimit)
                {
                    errorMessage = "Outside limit";
                }

                // Установка ошибки.
                errorProvider.SetError(tensionKgf.Key, errorMessage);
            }
        }
    }
}
