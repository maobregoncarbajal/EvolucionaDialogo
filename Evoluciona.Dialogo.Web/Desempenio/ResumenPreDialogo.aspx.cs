using System;
using System.Configuration;
using System.Data;
using System.Web;
using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.BusinessLogic;

namespace Evoluciona.Dialogo.Web.Desempenio
{
    public partial class ResumenPreDialogo : System.Web.UI.Page
    {
        #region Variables

        private readonly BlIndicadores _indicadorBl = new BlIndicadores();
        public int IdProceso;
        private string _codUsuarioEvaluado;
        private int _rolUsuarioEvaluado;
        private string _codPais;
        private string _periodo;
        private string _nombreEvaluado;
        private string _codUsuarioEvaluador;
        public string Imprimir;
        public string SoloNegocio;

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

            lblEvaluado.Text = _nombreEvaluado;

            CargarAntesNegocio();
            CargarAntesEquipos();
            CargarAntesCompetencias();

            ClientScript.RegisterStartupScript(GetType(), "MostrarPopup", "jQuery(document).ready(function() { modificarTexto(); });", true);
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            var nombreArchivo = string.Format("Resumen_Dialogo_{0}.xls", Request.QueryString.Get("codEvaluado").Trim());
            var html = hidTitulo.Value;
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
            _codUsuarioEvaluado = Request.QueryString.Get("codEvaluado");
            _rolUsuarioEvaluado = Convert.ToInt32(Request.QueryString.Get("rolEvaluado"));
            IdProceso = Convert.ToInt32(Request.QueryString.Get("idProceso"));
            _codPais = Request.QueryString.Get("codPais");
            _periodo = Request.QueryString.Get("periodo");
            _nombreEvaluado = Request.QueryString.Get("nomEvaluado");
            _codUsuarioEvaluador = Request.QueryString.Get("codEvaluador");
            Imprimir = Request.QueryString.Get("imprimir");
            SoloNegocio = Request.QueryString.Get("soloNegocio");

            //var objResumenBL = new blResumenProceso();


            //beUsuario objUsuario = (beUsuario)Session[constantes.objUsuarioLogeado];

            //var objDatosGR = objResumenBL.ObtenerUsuarioGRegionEvaluado(_codUsuarioEvaluado, string.Empty, _periodo, constantes.estadoActivo);
            //var objDatosFFVV = objResumenBL.ObtenerUsuarioGZonaEvaluado(objUsuario.idUsuario, objUsuario.codigoUsuario, _codUsuarioEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, constantes.estadoActivo);

            //if (objDatosFFVV != null && !string.IsNullOrEmpty(objDatosFFVV.prefijoIsoPais))
            //{
            //    _codPais = objDatosFFVV.prefijoIsoPais;
            //}
        }

        private void CargarAntesNegocio()
        {
            try
            {
                DataTable dtPeriodoEvaluacion = _indicadorBl.ValidarPeriodoEvaluacion(_periodo, _codPais, _rolUsuarioEvaluado, CadenaConexion);
                string anioCampana = string.Empty;

                if (dtPeriodoEvaluacion != null)
                {
                    anioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                }

                DataSet dsgrdvVariablesBase = _indicadorBl.CargarindicadoresporperiodoPreDialogo(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais, CadenaConexion);
                if (dsgrdvVariablesBase.Tables.Count > 0)
                {
                    grdvVariablesBase.DataSource = dsgrdvVariablesBase;
                    grdvVariablesBase.DataBind();
                }

                DataSet dsgrdvVariablesAdicionales = _indicadorBl.CargarindicadoresporperiodoVariablesAdicionalesPreDialogo(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais, CadenaConexion);
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

                DataTable variablesCausaIndicador1 = _indicadorBl.ObtenerVariablesCausaPreDialogo(IdProceso, idVariable1);
                DataTable variablesCausaIndicador2 = _indicadorBl.ObtenerVariablesCausaPreDialogo(IdProceso, idVariable2);

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

                DataTable dtCampana = _indicadorBl.CargarDatosVariableCausaEvaluadoPreDialogo(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais, anioCampana, ddlVariableCausa1.Text, CadenaConexion);
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

                dtCampana = _indicadorBl.CargarDatosVariableCausaEvaluadoPreDialogo(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais, anioCampana, ddlVariableCausa2.Text, CadenaConexion);
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

                dtCampana = _indicadorBl.CargarDatosVariableCausaEvaluadoPreDialogo(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais, anioCampana, ddlVariableCausa3.Text, CadenaConexion);
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

                dtCampana = _indicadorBl.CargarDatosVariableCausaEvaluadoPreDialogo(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais, anioCampana, ddlVariableCausa4.Text, CadenaConexion);
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

                dlstResumenCriticas.DataSource = _indicadorBl.ObtenerResumen(_periodo, _codUsuarioEvaluado, IdProceso, _rolUsuarioEvaluado, _codPais);
                dlstResumenCriticas.DataBind();
            }
            catch (Exception)
            {
            }
        }

        //private void OcultarCampanha(bool valor)
        //{
        //    gvVariables.Columns[6].Visible = valor;
        //    gvVariables.DataBind();

        //    gvVariablesAdicionales.Columns[6].Visible = valor;
        //    gvVariablesAdicionales.DataBind();
        //}

        private void CargarAntesEquipos()
        {
            try
            {
                var criticaBL = new BlCritica();

                var dtPeriodoEvaluacion = criticaBL.ValidarPeriodoEvaluacion(_periodo, _codPais, _rolUsuarioEvaluado, CadenaConexion);
                var campanha = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();

                var lstCargarCriticasProcesadas = criticaBL.ListaCargarCriticasProcesadasPreDialogo(_codUsuarioEvaluado, _periodo, _rolUsuarioEvaluado, _codPais, CadenaConexion, IdProceso, campanha);
                gvPersonasIngresadas.DataSource = lstCargarCriticasProcesadas;
                gvPersonasIngresadas.DataBind();
            }
            catch (Exception e)
            {
                var mnsj = e.Message;
            }
        }

        private void CargarAntesCompetencias()
        {
            var daProceso = new BlPlanAnual();
            var resumenProceso = new BeResumenProceso
            {
                idProceso = Convert.ToInt32(Request.QueryString.Get("idProceso"))
            };
            var dtGrabadas = daProceso.ObtenerPlanAnualGrabadasPreDialogo(CadenaConexion, resumenProceso);

            gvPlanAnual.DataSource = dtGrabadas;
            gvPlanAnual.DataBind();

            lblObservacion.Text = dtGrabadas.Rows.Count > 0 ? dtGrabadas.Rows[0]["observacion"].ToString().ToUpper() : "";
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