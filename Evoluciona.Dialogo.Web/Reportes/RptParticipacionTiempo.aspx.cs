
namespace Evoluciona.Dialogo.Web.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.DataVisualization.Charting;
    using System.Web.UI.WebControls;

    public partial class RptParticipacionTiempo : Page
    {
        public string periodo;
        private BeUsuario objUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["test"] = null;
            periodo = Request["periodo"];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario == null)
                Response.Redirect("~/error.aspx?mensaje=sesion");

            lblUserLogeado.Text = objUsuario.nombreUsuario;
            lblRolLogueado.Text = objUsuario.rolDescripcion;

            Dictionary<string, int> chartData = new Dictionary<string, int>();
            BlReportes daReporte = new BlReportes();
            DataTable dtPorcentajes = daReporte.ObtenerReunionesCampania(periodo, objUsuario.codigoUsuario);

            foreach (DataRow row in dtPorcentajes.Rows)
            {
                chartData.Add(Convert.ToString(row.ItemArray[0]), Convert.ToInt32(row.ItemArray[1]));
            }

            Chart1.Series[0].Points.DataBindXY(chartData.Keys, chartData.Values);
            Chart1.Series[0].PostBackValue = "#INDEX";
            Chart1.Series[0].LegendPostBackValue = "#INDEX";
            Chart1.Series[0].Label = "#VALX (#PERCENT{P0})";
            Chart1.Series[0].PostBackValue = "#INDEX";
            Chart1.Series[0].LegendPostBackValue = "#INDEX";
            Chart1.Series[0].Label = "#VALX (#PERCENT{P0})";
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            int _clickedPoint = int.Parse(e.PostBackValue);

            Series defaultSeries = Chart1.Series[0];

            if (_clickedPoint >= 0 && _clickedPoint < defaultSeries.Points.Count)
            {
                Session["test"] = _clickedPoint;
            }
        }

        protected void Chart1_PostPaint(object sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is Series)
            {
                if (Session["test"] != null)
                {
                    string variable = "0";
                    string fijo = "0";
                    string campania = "";

                    PointF position = PointF.Empty;
                    position.X = 430f;
                    position.Y = 120f;
                    Session["test"] = Session["test"];

                    BlReportes daReporte = new BlReportes();
                    DataTable dtCampanias = new DataTable();
                    dtCampanias = daReporte.ObtenerReunionesCampania(periodo, objUsuario.codigoUsuario);
                    if (dtCampanias != null && dtCampanias.Rows.Count > 0)
                    {
                        campania = dtCampanias.Rows[(int)Session["test"]].ItemArray[0].ToString();
                    }
                    BlReportes daReporte1 = new BlReportes();
                    DataTable dtVariable = new DataTable();
                    DataTable dtFijo = new DataTable();

                    dtVariable = daReporte1.ObtenerCantFijoVariable(periodo, campania, "V", objUsuario.codigoUsuario);
                    if (dtVariable != null && dtVariable.Rows.Count > 0)
                    {
                        variable = dtVariable.Rows[0].ItemArray[2].ToString();
                    }

                    dtFijo = daReporte1.ObtenerCantFijoVariable(periodo, campania, "F", objUsuario.codigoUsuario);
                    if (dtFijo != null && dtFijo.Rows.Count > 0)
                    {
                        fijo = dtFijo.Rows[0].ItemArray[2].ToString();
                    }

                    e.ChartGraphics.Graphics.DrawRectangle(Pens.LightSeaGreen, position.X - 30 / 2, position.Y - 30 / 2, 110, 75);
                    e.ChartGraphics.Graphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), position.X - 30 / 2, position.Y - 30 / 2, 110, 75);
                    e.ChartGraphics.Graphics.DrawRectangle(Pens.LightSalmon, position.X - 30 / 2, 195 - 30 / 2, 110, 75);
                    e.ChartGraphics.Graphics.FillRectangle(new SolidBrush(Color.LightSalmon), position.X - 30 / 2, 195 - 30 / 2, 110, 75);

                    e.ChartGraphics.Graphics.DrawString("Variable", new Font("Tahoma", 10), Brushes.Black, 420, 135);
                    e.ChartGraphics.Graphics.DrawString("Fijo", new Font("Tahoma", 10), Brushes.Black, 435, 210);

                    e.ChartGraphics.Graphics.DrawString(variable + "%", new Font("Tahoma", 9), Brushes.Black, 470, 135);
                    e.ChartGraphics.Graphics.DrawString(fijo + "%", new Font("Tahoma", 9), Brushes.Black, 460, 210);
                }
            }
        }
    }
}