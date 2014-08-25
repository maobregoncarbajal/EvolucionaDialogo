
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using BusinessEntity;
    using BusinessLogic;
    using Web.Matriz.Helpers.PDF;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ReportesHistoricos : Page
    {
        #region "Variables Globales"

        private BlDirectoraVentas oblDirectoraVentas = new BlDirectoraVentas();
        private BeDirectoraVentas obeDirectoraVentas = new BeDirectoraVentas();
        private BeGerenteRegion obeGerenteRegion = new BeGerenteRegion();
        private BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();
        private BeGerenteZona obeGerenteZona = new BeGerenteZona();
        private BlGerenteZona oblGerenteZona = new BlGerenteZona();
        private BeCompetencia obeCompetencia = new BeCompetencia();
        private BlCompetencia oblCompetencia = new BlCompetencia();

        #endregion "Variables Globales"

        #region "Eventos de Página"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request["ID"] != null)
                    {
                        string[] words = Request["ID"].Split('|');
                        hdenTipo.Value = words[0];
                        lblDocumento.Text = words[2];
                        lblNombre.Text = words[3];
                        lblCampaniaIngreso.Text = words[4];
                        lblCampaniaBaja.Text = words[5];
                        lblFechaBaja.Text = (words[6] == "01/01/1900") ? "" : words[6];

                        Session["SesionCompetenciaPDF"] = null;
                        Session["SesionHistoricoPDF"] = null;
                        Session["SesionHistorico"] = null;
                        ListarHistoricoCompetencia(words[1], words[2]);

                        if (words[0] == "1")
                        {
                            //ListarHistoricoDirectoraVentas(words[1], words[2]);
                        }
                        else
                        {
                            if (words[0] == "2")
                            {
                                ListarHistoricoGerenteRegion(words[1], words[2]);
                                gvHistorico.Columns[2].Visible = false;
                                gvHistorico.Columns[5].Visible = false;
                            }
                            else
                            {
                                ListarHistoricoGerenteZona(words[1], words[2]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationSettings.AppSettings["MensajeAlertaPagina"].ToString());
            }
        }

        #endregion "Eventos de Página"

        #region "Eventos del Desarrollador"

        private void ListarHistoricoCompetencia(string codigoPais, string codigoCompetencia)
        {
            //codigoCompetencia = "1017158988";
            oblCompetencia = new BlCompetencia();
            List<BeCompetencia> oList =
                new List<BeCompetencia>(oblCompetencia.CompetenciasListarHistorico(codigoPais, codigoCompetencia));

            if (oList != null)
            {
                if (oList.Count > 0)
                {
                    gvCompetencias.DataSource = oList;
                    gvCompetencias.DataBind();

                    DataTable dtCompetencia = new DataTable();
                    dtCompetencia.Columns.Add("Anio");
                    dtCompetencia.Columns.Add("Competencia");
                    dtCompetencia.Columns.Add("Avance");
                    dtCompetencia.Columns.Add("Competencia_Sugerida");
                    dtCompetencia.Columns.Add("Descripciòn");
                    dtCompetencia.Columns.Add("Rol");

                    for (int i = 0; i < oList.Count; i++)
                    {
                        dtCompetencia.Rows.Add(
                            oList[i].Anio,
                            oList[i].Competencia,
                            oList[i].PorcentajeAvance,
                            oList[i].Sugerencia,
                            oList[i].Descripcion,
                            oList[i].Rol);
                    }
                    Session["SesionCompetenciaPDF"] = dtCompetencia;
                }
                else
                {
                    gvCompetencias.DataSource = null;
                    gvCompetencias.DataBind();
                    btnCompetenciaPDF.Visible = false;
                }
            }
        }

        private void ListarHistoricoGerenteRegion(string codigoPais, string codigoGerenteRegion)
        {
            //codigoGerenteRegion = "0031535230";//Dato prueba
            oblGerenteRegion = new BlGerenteRegion();
            List<BeGerenteRegion> oList =
                new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListarReporteHistorico(codigoPais,
                                                                                               codigoGerenteRegion));

            if (oList != null)
            {
                if (oList.Count > 0)
                {
                    gvHistorico.DataSource = oList;
                    gvHistorico.DataBind();
                    Session["SesionHistorico"] = oList;

                    DataTable dtHistorico = new DataTable();
                    dtHistorico.Columns.Add("Region");
                    //dtHistorico.Columns.Add("Zona");
                    dtHistorico.Columns.Add("Periodo");
                    dtHistorico.Columns.Add("Campania");
                    //dtHistorico.Columns.Add("Codigo_Gerente_Region");
                    dtHistorico.Columns.Add("Pto_Ranking_Campania");
                    dtHistorico.Columns.Add("Pto_Ranking_Periodo");
                    dtHistorico.Columns.Add("Nivel_Productividad");
                    dtHistorico.Columns.Add("Ultima_Informacion");

                    for (int i = 0; i < oList.Count; i++)
                    {
                        dtHistorico.Rows.Add(
                            oList[i].CodRegion,
                            //oList[i].codZona,
                            oList[i].Periodo,
                            oList[i].AnioCampana,
                            //oList[i].CodGerenteRegional,
                            oList[i].PtoRankingProdCamp,
                            oList[i].PtoRankingProdPeriodo,
                            oList[i].EstadoPeriodo,
                            oList[i].FechaActualizacion);
                    }
                    Session["SesionHistoricoPDF"] = dtHistorico;
                }
                else
                {
                    gvHistorico.DataSource = null;
                    gvHistorico.DataBind();
                    btnHistoricoPDF.Visible = false;
                }
            }
        }

        private void ListarHistoricoGerenteZona(string codigoPais, string codigoGerenteZona)
        {
            //codigoGerenteZona = "1017158988";// Dato prueba
            oblGerenteZona = new BlGerenteZona();
            List<BeGerenteZona> oList =
                new List<BeGerenteZona>(oblGerenteZona.GerenteZonaListarReporteHistorico(codigoPais,
                                                                                         codigoGerenteZona));

            if (oList != null)
            {
                if (oList.Count > 0)
                {
                    gvHistorico.DataSource = oList;
                    gvHistorico.DataBind();
                    Session["SesionHistorico"] = oList;

                    DataTable dtHistorico = new DataTable();
                    dtHistorico.Columns.Add("Region");
                    dtHistorico.Columns.Add("Zona");
                    dtHistorico.Columns.Add("Periodo");
                    dtHistorico.Columns.Add("Campania");
                    dtHistorico.Columns.Add("Codigo_Gerente_Region");
                    dtHistorico.Columns.Add("Pto_Ranking_Campania");
                    dtHistorico.Columns.Add("Pto_Ranking_Periodo");
                    dtHistorico.Columns.Add("Nivel_Productividad");
                    dtHistorico.Columns.Add("Ultima_Informacion");

                    for (int i = 0; i < oList.Count; i++)
                    {
                        dtHistorico.Rows.Add(
                            oList[i].CodRegion,
                            oList[i].codZona,
                            oList[i].Periodo,
                            oList[i].AnioCampana,
                            oList[i].CodGerenteRegional,
                            oList[i].PtoRankingProdCamp,
                            oList[i].PtoRankingProdPeriodo,
                            oList[i].EstadoPeriodo,
                            oList[i].FechaActualizacion);
                    }
                    Session["SesionHistoricoPDF"] = dtHistorico;
                }
                else
                {
                    gvHistorico.DataSource = null;
                    gvHistorico.DataBind();
                    btnHistoricoPDF.Visible = false;
                }
            }
        }

        private void AlertaMensaje(string strMensaje)
        {
            string ClienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", ClienteScript, false);
        }

        #endregion "Eventos del Desarrollador"

        #region "Eventos del Formulario"

        protected void gvHistorico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHistorico.PageIndex = e.NewPageIndex;
            gvHistorico.DataSource = Session["SesionHistorico"];
            gvHistorico.DataBind();
        }

        protected void btnCompetenciaPDF_Click(object sender, EventArgs e)
        {
            //PDF COMPETENCIA
            DataTable dt = (DataTable)Session["SesionCompetenciaPDF"];
            string fileName = string.Format("{0}", "ReporteConfiguracion" + "_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s"));
            PDFExporter pdf = new PDFExporter(dt, fileName, false);
            pdf.ExportPDF();
        }

        protected void btnHistoricoPDF_Click(object sender, EventArgs e)
        {
            if (hdenTipo.Value == "1")
            {
                //PDF DIRECTORA VENTAS
            }
            else
            {
                if (hdenTipo.Value == "2")
                {
                    //PDF GERENTE REGION
                    DataTable dt = (DataTable)Session["SesionHistoricoPDF"];
                    string fileName = string.Format("{0}", "ReporteConfiguracion" + "_" +
                                                           DateTime.Now.ToString("M_dd_yyyy_H_M_s"));
                    PDFExporter pdf = new PDFExporter(dt, fileName, false);
                    pdf.ExportPDF();
                }
                else
                {
                    //PDF GERENTE ZONA
                    DataTable dt = (DataTable)Session["SesionHistoricoPDF"];
                    string fileName = string.Format("{0}", "ReporteConfiguracion" + "_" +
                                                           DateTime.Now.ToString("M_dd_yyyy_H_M_s"));
                    PDFExporter pdf = new PDFExporter(dt, fileName, false);
                    pdf.ExportPDF();
                }
            }
        }

        #endregion "Eventos del Formulario"
    }
}