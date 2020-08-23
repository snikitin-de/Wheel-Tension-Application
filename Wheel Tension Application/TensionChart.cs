// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Wheel_Tension_Application
{
    class TensionChart
    {
        public void DrawTension(Chart chart, string SeriesName, List<float> spokesAngles, List<float> tm1Reading)
        {
            spokesAngles.Add(360);
            tm1Reading.Add(tm1Reading[0]);

            chart.ChartAreas["ChartArea"].BackColor = ColorTranslator.FromHtml("#E5ECF6");
            chart.ChartAreas["ChartArea"].AxisX.MajorGrid.LineColor = Color.White;
            chart.ChartAreas["ChartArea"].AxisY.MajorGrid.LineColor = Color.White;

            chart.Series.Add(SeriesName);
            chart.Series[SeriesName].BorderWidth = 2;
            chart.Series[SeriesName].ChartType = SeriesChartType.Polar;
            chart.Series[SeriesName].MarkerStyle = MarkerStyle.Circle;
            chart.Series[SeriesName].MarkerSize = 5;

            float angle;
            float tm1;

            for (var i = 0; i < spokesAngles.Count; i++)
            {
                int spoke = i + 1;

                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                chart.Series[SeriesName].Points.AddXY(angle, tm1);
                chart.Series[SeriesName].Points[i].ToolTip = $"TM-1 Reading: #VALY \nAngle: #VALX \nSpoke: {spoke}";
                chart.Series[SeriesName].Points[i].Label = spoke.ToString();
            }

            for (var i = 0; i < spokesAngles.Count; i++)
            {
                angle = spokesAngles[i];
                tm1 = tm1Reading[i];

                chart.Series[SeriesName].Points.AddXY(0, 0);
                chart.Series[SeriesName].Points.AddXY(angle, tm1);
            }
        }
    }
}
