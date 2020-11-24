// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Wheel_Tension_Application
{
    /*
     * Класс TensionChart для построение диаграммы натяжения спиц.
     */
    /// <summary>
    /// Класс <c>TensionChart</c> для построение диаграммы натяжения спиц.
    /// </summary>
    class TensionChart
    {
        // Отрисовка диаграммы натяжения спиц.
        /// <summary>
        /// Отрисовка диаграммы натяжения спиц.
        /// </summary>
        /// <param name="chart">Элемент формы типа <c>Chart</c> на котором будет отрисована диаграмма натяжения спиц.</param>
        /// <param name="SeriesName">Имя серии. Используется для отображения легенды.</param>
        /// <param name="spokesAngles">Список значений углов спиц в полярной системе координат.</param>
        /// <param name="tm1Reading">Список значений <c>tm1Reading</c>.</param>
        public void DrawTension(Chart chart, string SeriesName, List<float> spokesAngles, List<float> tm1Reading)
        {
            float angle;
            float tm1;

            // Добавление конечных значений углов и tm1Reading для создания замкнутой диаграммы.
            spokesAngles.Add(360);
            tm1Reading.Add(tm1Reading[0]);

            // Установка параметров диаграммы.
            chart.ChartAreas["ChartArea"].BackColor = ColorTranslator.FromHtml("#E5ECF6");
            chart.ChartAreas["ChartArea"].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["ChartArea"].AxisY.MajorGrid.LineColor = Color.White;
            chart.Series.Add(SeriesName);
            chart.Series[SeriesName].BorderWidth = 2;
            chart.Series[SeriesName].ChartType = SeriesChartType.Polar;
            chart.Series[SeriesName].MarkerStyle = MarkerStyle.Circle;
            chart.Series[SeriesName].MarkerSize = 5;

            // Отрисовка натяжения спиц.
            for (var i = 0; i < spokesAngles.Count; i++)
            {
                // Номер спицы (начинается с 1).
                int spoke = i + 1;

                // Текущее значение угла и tm1 reading.
                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                // Добавление точки на диаграмму.
                chart.Series[SeriesName].Points.AddXY(angle, tm1);

                /* Если спица не последняя, то добавляется номер спицы и всплывающую подсказку.
                 * Для последней спицы подсказка не добавляется, т.к. она дублирует первую спицу.
                 */
                if (i != spokesAngles.Count - 1)
                {
                    chart.Series[SeriesName].Points[i].ToolTip = $"TM-1 Reading: #VALY \nAngle: #VALX \nSpoke: {spoke}";
                    chart.Series[SeriesName].Points[i].Label = spoke.ToString();
                }
            }

            // Отрисовка "спиц".
            for (var i = 0; i < spokesAngles.Count; i++)
            {
                // Текущее значение угла и tm1 reading.
                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                // Добавление точки на диаграмму.
                chart.Series[SeriesName].Points.AddXY(0, 0);
                chart.Series[SeriesName].Points.AddXY(angle, tm1);
            }
        }
    }
}
