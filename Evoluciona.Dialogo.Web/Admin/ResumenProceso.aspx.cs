
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using System.Web.UI;

    public partial class ResumenProceso : Page
    {
        #region Variables

        private readonly BlIndicadores indicadorBL = new BlIndicadores();
        public int idProceso;
        private string codUsuarioEvaluado;
        private int rolUsuarioEvaluado;
        private string codPais;
        private string periodo;
        private string nombreEvaluado;
        private string codUsuarioEvaluador;
        public string imprimir;
        public string soloNegocio;

        #endregion Variables

        #region Propiedades

        private string CadenaConexion
        {
            get
            {
                if (Session["connApp"] == null || string.IsNullOrEmpty(Session["connApp"].ToString()))
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;

                return Session["connApp"].ToString();
            }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack) return;

            CargarVariables();

            lblEvaluado.Text = nombreEvaluado;

            CargarAntesNegocio();
            CargarAntesEquipos();
            CargarAntesCompetencias();

            ClientScript.RegisterStartupScript(GetType(), "MostrarPopup", "jQuery(document).ready(function() { modificarTexto(); });", true);
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string nombreArchivo = string.Format("Resumen_Dialogo_{0}.xls", Request.QueryString.Get("codEvaluado").Trim());
            string html = hidTitulo.Value;
            html = html.Replace("&gt;", ">");
            html = html.Replace("&lt;", "<");
            html = ConvertirTextoHtml(html);

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo);
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
        }

        #endregion Eventos

        #region Metodos

        private void CargarVariables()
        {
            codUsuarioEvaluado = Request.QueryString.Get("codEvaluado");
            rolUsuarioEvaluado = Convert.ToInt32(Request.QueryString.Get("rolEvaluado"));
            idProceso = Convert.ToInt32(Request.QueryString.Get("idProceso"));
            codPais = Request.QueryString.Get("codPais");
            periodo = Request.QueryString.Get("periodo");
            nombreEvaluado = Request.QueryString.Get("nomEvaluado");
            codUsuarioEvaluador = Request.QueryString.Get("codEvaluador");
            imprimir = Request.QueryString.Get("imprimir");
            soloNegocio = Request.QueryString.Get("soloNegocio");

            BlResumenProceso objResumenBL = new BlResumenProceso();
            BeResumenProceso objDatosGR = objResumenBL.ObtenerUsuarioGRegionEvaluado(codUsuarioEvaluado, string.Empty, periodo, Constantes.EstadoActivo);
            if (objDatosGR != null && !string.IsNullOrEmpty(objDatosGR.prefijoIsoPais))
            {
                codPais = objDatosGR.prefijoIsoPais;
            }
        }

        private void CargarAntesNegocio()
        {
            try
            {
                DataTable dtPeriodoEvaluacion = indicadorBL.ValidarPeriodoEvaluacion(periodo, codPais, rolUsuarioEvaluado, CadenaConexion);
                string anioCampana = string.Empty;

                if (dtPeriodoEvaluacion != null)
                {
                    anioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                }

                DataSet dsgrdvVariablesBase = indicadorBL.Cargarindicadoresporperiodo(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, CadenaConexion);
                if (dsgrdvVariablesBase.Tables.Count > 0)
                {
                    grdvVariablesBase.DataSource = dsgrdvVariablesBase;
                    grdvVariablesBase.DataBind();
                }

                DataSet dsgrdvVariablesAdicionales = indicadorBL.CargarindicadoresporperiodoVariablesAdicionales(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, CadenaConexion);
                if (dsgrdvVariablesAdicionales.Tables.Count > 0)
                {
                    grdvVariablesAdicionales.DataSource = dsgrdvVariablesAdicionales;
                    grdvVariablesAdicionales.DataBind();
                }

                int totalSeleccionados = 0;
                string idVariable1 = string.Empty;
                string idVariable2 = string.Empty;

                if (dsgrdvVariablesBase.Tables.Count > 0)
                {
                    foreach (DataRow row in dsgrdvVariablesBase.Tables[0].Rows)
                    {
                        bool seleccionado = bool.Parse(row.ItemArray[6].ToString());
                        if (seleccionado)
                        {
                            if (totalSeleccionados == 0)
                            {
                                idVariable1 = row.ItemArray[0].ToString();
                                lblvariable1Desc.Text = row.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else if (totalSeleccionados == 1)
                            {
                                idVariable2 = row.ItemArray[0].ToString();
                                lblvariable2Desc.Text = row.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (dsgrdvVariablesAdicionales.Tables.Count > 0)
                {
                    foreach (DataRow row in dsgrdvVariablesAdicionales.Tables[0].Rows)
                    {
                        bool seleccionado = bool.Parse(row.ItemArray[6].ToString());
                        if (seleccionado)
                        {
                            if (totalSeleccionados == 0)
                            {
                                idVariable1 = row.ItemArray[0].ToString();
                                lblvariable1Desc.Text = row.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else if (totalSeleccionados == 1)
                            {
                                idVariable2 = row.ItemArray[0].ToString();
                                lblvariable2Desc.Text = row.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                DataTable variablesCausaIndicador1 = indicadorBL.ObtenerVariablesCausa(idProceso, idVariable1);
                DataTable variablesCausaIndicador2 = indicadorBL.ObtenerVariablesCausa(idProceso, idVariable2);

                if (variablesCausaIndicador1.Rows.Count > 1)
                {
                    ddlVariableCausa1.Text = variablesCausaIndicador1.Rows[0].ItemArray[0].ToString().Trim();
                    ddlVariableCausa2.Text = variablesCausaIndicador1.Rows[1].ItemArray[0].ToString().Trim();
                }

                if (variablesCausaIndicador2.Rows.Count > 1)
                {
                    ddlVariableCausa3.Text = variablesCausaIndicador2.Rows[0].ItemArray[0].ToString().Trim();
                    ddlVariableCausa4.Text = variablesCausaIndicador2.Rows[1].ItemArray[0].ToString().Trim();
                }

                DataTable dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa1.Text, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa1.Text = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable1PlanPeriodo.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable1Real.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable1Diferencia.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa2.Text, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa2.Text = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable2PlanPeriodo.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable2Real.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable2Diferencia.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa3.Text, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa3.Text = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable3PlanPeriodo.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable3Real.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable3Diferencia.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa4.Text, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa4.Text = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable4PlanPeriodo.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable4Real.Text = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable4Diferencia.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dlstResumenCriticas.DataSource = indicadorBL.ObtenerResumen(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais);
                dlstResumenCriticas.DataBind();
            }
            catch (Exception)
            {
            }
        }

        private void CargarAntesEquipos()
        {
            try
            {
                BlCritica criticaBL = new BlCritica();

                DataTable dtPeriodoEvaluacion = criticaBL.ValidarPeriodoEvaluacion(periodo, codPais, rolUsuarioEvaluado, CadenaConexion);
                string campanha = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();

                List<BeCriticas> lstCargarCriticasProcesadas = criticaBL.ListaCargarCriticasProcesadas(codUsuarioEvaluado, periodo, rolUsuarioEvaluado, codPais, CadenaConexion, idProceso, campanha);
                gvPersonasIngresadas.DataSource = lstCargarCriticasProcesadas;
                gvPersonasIngresadas.DataBind();
            }
            catch (Exception)
            {
            }
        }

        private void CargarAntesCompetencias()
        {
            BlPlanAnual daProceso = new BlPlanAnual();
            BeResumenProceso resumenProceso = new BeResumenProceso();
            resumenProceso.idProceso = Convert.ToInt32(Request.QueryString.Get("idProceso"));
            DataTable dtGrabadas = daProceso.ObtenerPlanAnualGrabadas(CadenaConexion, resumenProceso);

            gvPlanAnual.DataSource = dtGrabadas;
            gvPlanAnual.DataBind();

            if (dtGrabadas.Rows.Count > 0)
            {
                lblObservacion.Text = dtGrabadas.Rows[0]["observacion"].ToString().ToUpper();
            }
            else
            {
                lblObservacion.Text = "";
            }
        }

        private static string ConvertirTextoHtml(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.Replace("¡", "&iexcl;");
                texto = texto.Replace("¿", "&iquest;");
                texto = texto.Replace("'", "&apos;");

                texto = texto.Replace("á", "&aacute;");
                texto = texto.Replace("é", "&eacute;");
                texto = texto.Replace("í", "&iacute;");
                texto = texto.Replace("ó", "&oacute;");
                texto = texto.Replace("ú", "&uacute;");
                texto = texto.Replace("ñ", "&ntilde;");
                texto = texto.Replace("ç", "&ccedil;");

                texto = texto.Replace("Á", "&Aacute;");
                texto = texto.Replace("É", "&Eacute;");
                texto = texto.Replace("Í", "&Iacute;");
                texto = texto.Replace("Ó", "&Oacute;");
                texto = texto.Replace("Ú", "&Uacute;");
                texto = texto.Replace("Ñ", "&Ntilde;");
                texto = texto.Replace("Ç", "&Ccedil;");

                texto = texto.Replace("à", "&agrave;");
                texto = texto.Replace("è", "&egrave;");
                texto = texto.Replace("ì", "&igrave;");
                texto = texto.Replace("ò", "&ograve;");
                texto = texto.Replace("ù", "&ugrave;");

                texto = texto.Replace("À", "&Agrave;");
                texto = texto.Replace("È", "&Egrave;");
                texto = texto.Replace("Ì", "&Igrave;");
                texto = texto.Replace("Ò", "&Ograve;");
                texto = texto.Replace("Ù", "&Ugrave;");

                texto = texto.Replace("ä", "&auml;");
                texto = texto.Replace("ë", "&euml;");
                texto = texto.Replace("ï", "&iuml;");
                texto = texto.Replace("ö", "&ouml;");
                texto = texto.Replace("ü", "&uuml;");

                texto = texto.Replace("Ä", "&Auml;");
                texto = texto.Replace("Ë", "&Euml;");
                texto = texto.Replace("Ï", "&Iuml;");
                texto = texto.Replace("Ö", "&Ouml;");
                texto = texto.Replace("Ü", "&Uuml;");

                texto = texto.Replace("â", "&acirc;");
                texto = texto.Replace("ê", "&ecirc;");
                texto = texto.Replace("î", "&icirc;");
                texto = texto.Replace("ô", "&ocirc;");
                texto = texto.Replace("û", "&ucirc;");

                texto = texto.Replace("Â", "&Acirc;");
                texto = texto.Replace("Ê", "&Ecirc;");
                texto = texto.Replace("Î", "&Icirc;");
                texto = texto.Replace("Ô", "&Ocirc;");
                texto = texto.Replace("Û", "&Ucirc;");
            }
            else
            {
                texto = "";
            }

            return texto;
        }

        #endregion Metodos
    }
}