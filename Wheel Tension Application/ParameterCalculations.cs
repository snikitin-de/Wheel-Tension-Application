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
            // Массив углов спиц.
            var angles = new List<float>(spokeCount);

            // Угол спицы в градусах.
            float angle = 0;
            // Шаг в градусах между спицами.
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
            // Объект FormControls.
            var formControl = new FormControls();

            // Массив натяжения спиц в кгс.
            var tensionKgf = new List<float>();

            // Значения из таблицы DataGridView.
            List<string[]> dataGridViewValues = formControl.GetDataGridViewValues(dataGridView);

            // Массив TM-1 Reading.
            List<string> tensions = formControl.GetValuesFromGroupControls(groupBox, controlsNameTm1Reading).Values.ToList();

            // Определение силы натяжения спицы по TM-1 Reading в таблице DataGridView.
            foreach (string tension in tensions)
            {
                // Найдено ли натяжение в таблице DataGridView.
                bool isFound = false;

                for (int j = 0; j < dataGridViewValues[0].Length; j++)
                {
                    // TM-1 Reading из таблицы DataGridView.
                    string tensionFromTable = dataGridViewValues[0][j];

                    // Если указанный TM-1 Reading найден в таблице DataGridView, то добавляем натяжение в кгс в массив.
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
            // Среднее арифметическое натяжения спиц.
            double mean = values.Sum() / values.Count();

            // Квадратичные значения.
            var squares_query =
                from float value in values
                select (value - mean) * (value - mean);

            // Сумма квадратичных значений.
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
            // Граница интервала натяжения спицы.
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
            // Попадает ли сила натяжения спицы в допустимый интервал.
            bool isWithinTensionLimit = false;

            // Если натяжение спицы в кгс находится между нижней и верхней границами допустимого интервала натяжения
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

            // Объект FormControls.
            FormControls formControl = new FormControls();

            // Массив натяжения спиц в кгс.
            Dictionary<Control, string> tensionsKgf = formControl.GetValuesFromGroupControls(groupBox, controlsName);

            // Иконка для отображения выхода натяжения за границы интервала.
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
