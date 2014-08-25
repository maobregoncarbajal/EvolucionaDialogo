
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Chart
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.UI.DataVisualization.Charting;
    using System.Web.UI.WebControls;

    public class RuntimeChart
    {
        private System.Web.UI.DataVisualization.Charting.Chart m_chart;

        public RuntimeChart()
        {
            m_chart = new System.Web.UI.DataVisualization.Charting.Chart();
        }

        public System.Web.UI.DataVisualization.Charting.Chart makeChart(string leyenda1, string leyenda2,
                                                                        List<BeResultadoMatriz> resultados,
                                                                        bool esProcentaje)
        {

            //Chart setting 

            m_chart.Height = Unit.Pixel(250);
            m_chart.Width = Unit.Pixel(412);
            m_chart.Palette = ChartColorPalette.BrightPastel;
            m_chart.BackColor =Color.WhiteSmoke;
            m_chart.BackGradientStyle = GradientStyle.TopBottom;
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BorderlineColor = Color.Black;
            m_chart.BorderlineWidth = 1;
            m_chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;


            //Chart Area
            ChartArea mainArea = new ChartArea();
            mainArea.Name = "mainArea";
            mainArea.BackColor = Color.OldLace;
            mainArea.BorderDashStyle = ChartDashStyle.Solid;
            mainArea.ShadowColor = Color.Transparent;
            mainArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.BackGradientStyle = GradientStyle.TopBottom;
            mainArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisY.LabelStyle.Format = "{#}"; 

            List<double> valores = new List<double>();

            foreach (BeResultadoMatriz result in resultados)
            {
              valores.Add(result.LogroPeriodo);
              valores.Add(result.ParticipacionPeriodo); 
            }

            mainArea.AxisY.Maximum = Math.Round(110 * Percentile(valores.ToArray(), 1) / 100, 2);
            mainArea.AxisY.Minimum = Math.Round(90 * Percentile(valores.ToArray(), 0)/100,2);

            if (mainArea.AxisY.Maximum == mainArea.AxisY.Minimum)
            {
                mainArea.AxisY.Maximum = mainArea.AxisY.Minimum + 1;
            }

            mainArea.AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            mainArea.AxisY.LabelStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            mainArea.AxisX.Title = "Periodos";
            mainArea.AxisY.Title = esProcentaje ? "Porcentaje" : "Cantidad";

            m_chart.ChartAreas.Add(mainArea);

            //Legend
            Legend mainLegend = new Legend();
            mainLegend.Name = "mainLegend";
            mainLegend.DockedToChartArea = "mainArea";
            mainLegend.Docking = Docking.Top;
            mainLegend.HeaderSeparator = LegendSeparatorStyle.Line;
            mainLegend.IsDockedInsideChartArea = false;
            mainLegend.BackColor = Color.Transparent;
            mainLegend.Font = new Font("Arial", 10, FontStyle.Bold);

            m_chart.Legends.Add(mainLegend);

            Series sr = new Series();
            sr.ChartArea = "mainArea";
            sr.Legend = "mainLegend";
            sr.ChartType = SeriesChartType.Spline;
            sr.Color = Color.DarkBlue;
            sr.BorderWidth = 3;
            sr.MarkerStyle = MarkerStyle.Diamond;
            sr.MarkerSize = 10;
            sr.MarkerBorderColor = Color.Black;
            sr.MarkerColor = Color.White;

            sr.Name = leyenda1;
            sr.XValueType = ChartValueType.String;

            Series sr2 = new Series();
            sr2.ChartArea = "mainArea";
            sr2.Legend = "mainLegend";
            sr2.ChartType = SeriesChartType.Spline;
            sr2.Name = leyenda2;
            sr2.XValueType = ChartValueType.String;
            sr2.Color = Color.DarkRed;
            sr2.BorderWidth = 3;
            sr2.MarkerStyle = MarkerStyle.Diamond;
            sr2.MarkerSize = 10;
            sr2.MarkerColor = Color.White;
            sr2.MarkerBorderColor = Color.Black;

            foreach (var result in resultados)
            {
                sr.Points.AddXY(result.Periodo, result.ParticipacionPeriodo);
                sr2.Points.AddXY(result.Periodo, result.LogroPeriodo);
            }

            m_chart.Series.Add(sr);
            m_chart.Series.Add(sr2);

            return m_chart;
        }


        public System.Web.UI.DataVisualization.Charting.Chart makeChartSustento(string leyenda1, string leyenda2,
                                                                       List<BeResultadoMatriz> resultados,
                                                                       bool esProcentaje)
        {

            //Chart setting 

            m_chart.Height = Unit.Pixel(250);
            m_chart.Width = Unit.Pixel(812);
            m_chart.Palette = ChartColorPalette.BrightPastel;
            m_chart.BackColor = Color.WhiteSmoke;
            m_chart.BackGradientStyle = GradientStyle.TopBottom;
            m_chart.BorderlineDashStyle = ChartDashStyle.Solid;
            m_chart.BorderlineColor = Color.Black;
            m_chart.BorderlineWidth = 1;
            m_chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;


            //Chart Area
            ChartArea mainArea = new ChartArea();
            mainArea.Name = "mainArea";
            mainArea.BackColor = Color.OldLace;
            mainArea.BorderDashStyle = ChartDashStyle.Solid;
            mainArea.ShadowColor = Color.Transparent;
            mainArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.BackGradientStyle = GradientStyle.TopBottom;
            mainArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            mainArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            //mainArea.AxisY.LabelStyle.Format = "{0:#,##0.00}";
            mainArea.AxisY.LabelStyle.Format = "{#}";

            List<double> valores = new List<double>();

            foreach (BeResultadoMatriz result in resultados)
            {
                valores.Add(result.LogroCampana);
                valores.Add(result.ParticipacionCampana);
            }

            mainArea.AxisY.Maximum = Math.Round(110 * Percentile(valores.ToArray(), 1) / 100, 2);
            mainArea.AxisY.Minimum = Math.Round(90 * Percentile(valores.ToArray(), 0) / 100, 2);

            if (mainArea.AxisY.Maximum == mainArea.AxisY.Minimum)
            {
                mainArea.AxisY.Maximum = mainArea.AxisY.Minimum + 1;
            }

            mainArea.AxisX.LabelStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            mainArea.AxisY.LabelStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            mainArea.AxisX.Title = "Campañas";
            mainArea.AxisX.Interval = 1;
            mainArea.AxisY.Title = esProcentaje ? "Porcentaje" : "Cantidad";

            m_chart.ChartAreas.Add(mainArea);

            //Legend
            Legend mainLegend = new Legend();
            mainLegend.Name = "mainLegend";
            mainLegend.DockedToChartArea = "mainArea";
            mainLegend.Docking = Docking.Top;
            mainLegend.HeaderSeparator = LegendSeparatorStyle.Line;
            mainLegend.IsDockedInsideChartArea = false;
            mainLegend.BackColor = Color.Transparent;
            mainLegend.Font = new Font("Arial", 10, FontStyle.Bold);

            m_chart.Legends.Add(mainLegend);

            Series sr = new Series();
            sr.ChartArea = "mainArea";
            sr.Legend = "mainLegend";
            sr.ChartType = SeriesChartType.Spline;
            sr.Color = Color.DarkBlue;
            sr.BorderWidth = 3;
            sr.MarkerStyle = MarkerStyle.Diamond;
            sr.MarkerSize = 10;
            sr.MarkerBorderColor = Color.Black;
            sr.MarkerColor = Color.White;

            sr.Name = leyenda1;
            sr.XValueType = ChartValueType.String;

            Series sr2 = new Series();
            sr2.ChartArea = "mainArea";
            sr2.Legend = "mainLegend";
            sr2.ChartType = SeriesChartType.Spline;
            sr2.Name = leyenda2;
            sr2.XValueType = ChartValueType.String;
            sr2.Color = Color.DarkRed;
            sr2.BorderWidth = 3;
            sr2.MarkerStyle = MarkerStyle.Diamond;
            sr2.MarkerSize = 10;
            sr2.MarkerColor = Color.White;
            sr2.MarkerBorderColor = Color.Black;

            foreach (var result in resultados)
            {
                sr.Points.AddXY(result.NombreCampanha, result.ParticipacionCampana);
                sr2.Points.AddXY(result.NombreCampanha, result.LogroCampana);
            }

            m_chart.Series.Add(sr);
            m_chart.Series.Add(sr2);

            return m_chart;
        }

        /// <summary>
        /// Calculate percentile of a sorted data set
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="excelPercentile"></param>
        /// <returns></returns>
        private double Percentile(double[] sequence, double excelPercentile)
        {
            Array.Sort(sequence);
            int N = sequence.Length;
            double n = (N - 1) * excelPercentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1d) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        } // end of internal function percentile
    }
}
