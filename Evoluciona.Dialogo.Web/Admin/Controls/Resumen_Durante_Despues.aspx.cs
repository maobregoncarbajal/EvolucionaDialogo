
namespace Evoluciona.Dialogo.Web.Admin.Controls
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Resumen_Durante_Despues1 : Page
    {
        #region Variables

        public int idProceso;
        private string codUsuarioEvaluado;
        private int rolUsuarioEvaluado;
        private string codPais;
        private string periodo;
        private string codUsuarioEvaluador;
        private string estadoProceso;

        #endregion Variables

        #region Propiedades

        private string CadenaConexion
        {
            get
            {
                if (string.IsNullOrEmpty(Session["connApp"].ToString()))
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;

                return Session["connApp"].ToString();
            }
        }

        public string EstadoProceso
        {
            get { return estadoProceso; }
            set { estadoProceso = value; }
        }

        #endregion Propiedades

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CargarVariables();

            CargarNegocio();
            CargarEquipos();
            CargarCompetencias();

            if (EstadoProceso == "DESPUES")
            {
                litEstadoNegocio.Text = "DESPUÉS";
                litEstadoEquipo.Text = "DESPUÉS";
                litEstadoCompetencias.Text = "DESPUÉS";
            }
        }

        private void CargarVariables()
        {
            codUsuarioEvaluado = Request.QueryString.Get("codEvaluado");
            rolUsuarioEvaluado = Convert.ToInt32(Request.QueryString.Get("rolEvaluado"));
            idProceso = Convert.ToInt32(Request.QueryString.Get("idProceso"));
            codPais = Request.QueryString.Get("codPais");
            periodo = Request.QueryString.Get("periodo");
            codUsuarioEvaluador = Request.QueryString.Get("codEvaluador");

            BlResumenProceso objResumenBL = new BlResumenProceso();
            BeResumenProceso objDatosGR = objResumenBL.ObtenerUsuarioGRegionEvaluado(codUsuarioEvaluado, string.Empty, periodo, Constantes.EstadoActivo);
            if (objDatosGR != null && !string.IsNullOrEmpty(objDatosGR.prefijoIsoPais))
            {
                codPais = objDatosGR.prefijoIsoPais;
            }

            litPeriodo.Text = periodo;
        }

        private void CargarNegocio()
        {
            string idsVariablesIndicador = string.Empty;
            string idsVariablesEnfoque = string.Empty;

            BlIndicadores objIndicadorBL = new BlIndicadores();
            BlVariableEnfoque objVarEnfoquesBL = new BlVariableEnfoque();
            DataTable dtIndicadores = objIndicadorBL.ObtenerIndicadoresProcesados(idProceso);

            if (dtIndicadores.Rows.Count <= 0) return;

            string campaniaProcesada = dtIndicadores.Rows[0]["chrAnioCampanha"].ToString();
            string variable1 = string.Empty;
            string variable2 = string.Empty;
            string variableCausa1 = string.Empty;
            string variableCausa2 = string.Empty;
            string variableCausa3 = string.Empty;
            string variableCausa4 = string.Empty;

            #region Iterar DT Indicadores

            for (int x = 0; x < dtIndicadores.Rows.Count; x++)
            {
                string idVariable = dtIndicadores.Rows[x]["intIDIndicador"].ToString();
                string idVarEnfoque = string.Empty;

                if (x == 0)
                {
                    variable1 = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();

                    List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(idVariable));

                    if (lstVariableEnfoque.Count > 0)
                    {
                        txtZonas1.Text = lstVariableEnfoque[0].zonas.Replace("\n", "<br/>");
                        idVarEnfoque = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                        txtPlanAccion1.Text = lstVariableEnfoque[0].planAccion.Replace("\n", "<br/>");

                        lblEstadoIndicador1.Text = lstVariableEnfoque[0].postDialogo ? "(Completado)" : "(En Proceso)";
                    }
                }
                if (x == 1)
                {
                    variable2 = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();
                    idVariable = dtIndicadores.Rows[x]["intIDIndicador"].ToString();

                    List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(idVariable));
                    if (lstVariableEnfoque.Count > 0)
                    {
                        txtZonas2.Text = lstVariableEnfoque[0].zonas.Replace("\n", "<br/>");
                        idVarEnfoque = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                        txtPlanAccion2.Text = lstVariableEnfoque[0].planAccion.Replace("\n", "<br/>");

                        lblEstadoIndicador2.Text = lstVariableEnfoque[0].postDialogo ? "(Completado)" : "(En Proceso)";
                    }
                }

                idsVariablesIndicador += "," + idVariable;
                idsVariablesEnfoque += "," + idVarEnfoque;
            }

            #endregion Iterar DT Indicadores

            #region Cargar Variables de Enfoque

            DataTable dtDescripcionVariableEnfoque1 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variable1.Trim(), campaniaProcesada, periodo);
            if (dtDescripcionVariableEnfoque1 != null)
            {
                if (dtDescripcionVariableEnfoque1.Rows.Count > 0)
                {
                    lblVariableGeneral1.Text = dtDescripcionVariableEnfoque1.Rows[0]["vchDesVariable"].ToString();
                }
            }

            DataTable dtDescripcionVariableEnfoque2 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variable2.Trim(), campaniaProcesada, periodo);
            if (dtDescripcionVariableEnfoque2 != null)
            {
                if (dtDescripcionVariableEnfoque2.Rows.Count > 0)
                {
                    lblVariableGeneral2.Text = dtDescripcionVariableEnfoque2.Rows[0]["vchDesVariable"].ToString();
                }
            }

            DataTable dtVariablesCausa1 = objIndicadorBL.ObtenerVariablesCausa(idProceso, variable1.Trim());
            if (dtVariablesCausa1.Rows.Count > 0)
            {
                variableCausa1 = dtVariablesCausa1.Rows[0]["chrCodVariableHija"].ToString();

                if (dtVariablesCausa1.Rows.Count > 1)
                {
                    variableCausa2 = dtVariablesCausa1.Rows[1]["chrCodVariableHija"].ToString();
                }
            }

            DataTable dtDescripcionVariableEnfoque3 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa1.Trim(), campaniaProcesada, periodo);
            if (dtDescripcionVariableEnfoque3 != null)
            {
                if (dtDescripcionVariableEnfoque3.Rows.Count > 0)
                {
                    Label3.Text = dtDescripcionVariableEnfoque3.Rows[0]["vchDesVariable"].ToString();
                }
            }

            DataTable dtDescripcionVariableEnfoque4 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa2.Trim(), campaniaProcesada, periodo);
            if (dtDescripcionVariableEnfoque4 != null)
            {
                if (dtDescripcionVariableEnfoque4.Rows.Count > 0)
                {
                    Label7.Text = dtDescripcionVariableEnfoque4.Rows[0]["vchDesVariable"].ToString();
                }
            }

            DataTable dtVariablesCausa2 = objIndicadorBL.ObtenerVariablesCausa(idProceso, variable2.Trim());
            if (dtVariablesCausa2.Rows.Count > 0)
            {
                variableCausa3 = dtVariablesCausa2.Rows[0]["chrCodVariableHija"].ToString();

                if (dtVariablesCausa2.Rows.Count > 1)
                {
                    variableCausa4 = dtVariablesCausa2.Rows[1]["chrCodVariableHija"].ToString();
                }
            }

            DataTable dtDescripcionVariableEnfoque5 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa3.Trim(), campaniaProcesada, periodo);
            if (dtDescripcionVariableEnfoque5 != null)
            {
                if (dtDescripcionVariableEnfoque5.Rows.Count > 0)
                {
                    Label8.Text = dtDescripcionVariableEnfoque5.Rows[0]["vchDesVariable"].ToString();
                }
            }

            DataTable dtDescripcionVariableEnfoque6 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa4.Trim(), campaniaProcesada, periodo);
            if (dtDescripcionVariableEnfoque6 != null)
            {
                if (dtDescripcionVariableEnfoque6.Rows.Count > 0)
                {
                    Label9.Text = dtDescripcionVariableEnfoque6.Rows[0]["vchDesVariable"].ToString();
                }
            }

            #endregion Cargar Variables de Enfoque

            //blResumenVisita blResumenVisita = new blResumenVisita();
            //List<beResumenVisita> visitas = blResumenVisita.ListarVisitas(codUsuarioEvaluador, codUsuarioEvaluado, codPais, periodo);
            //gvVisitas.DataSource = visitas;
            //gvVisitas.DataBind();

            string ubicacionRol = string.Empty;

            if (rolUsuarioEvaluado == Constantes.RolGerenteZona)
            {
                ubicacionRol = "Secciones";
                lblDescripcionRol.Text = "Gerente de Zona";
            }
            else
            {
                ubicacionRol = "Zonas";
                lblDescripcionRol.Text = "Gerente de Region";
            }


            lblDescripcionLugarRol.Text = lblDescripcionLugarRol1.Text = ubicacionRol;
        }

        private void CargarEquipos()
        {
            BeResumenProceso objResumenBE = new BeResumenProceso();
            objResumenBE.idProceso = idProceso;
            objResumenBE.codigoUsuario = codUsuarioEvaluado;
            objResumenBE.prefijoIsoPais = codPais;

            BlPlanAccion planAccionBL = new BlPlanAccion();
            BeUsuario usuario = new BeUsuario();
            usuario.prefijoIsoPais = codPais;

            List<BePlanAccion> procesados = planAccionBL.ObtenerCriticas(usuario, objResumenBE, periodo, rolUsuarioEvaluado);
            BlCritica criticaBL = new BlCritica();

            foreach (BePlanAccion planAccion in procesados)
            {
                BeCriticas criticaActual = criticaBL.ObtenerCritica(idProceso, planAccion.DocuIdentidad);
                planAccion.Variable = criticaActual.variableConsiderar;
                planAccion.Estado = planAccion.PreDialogo ? "Completado" : "En Proceso";
            }

            gvEquipo.Columns[3].Visible = EstadoProceso == "DESPUES";
            gvEquipo.DataSource = procesados;
            gvEquipo.DataBind();
        }

        private void CargarCompetencias()
        {
            BeResumenProceso beReseumen = new BeResumenProceso();
            BlRetroalimentacion daProceso = new BlRetroalimentacion();

            if (string.IsNullOrEmpty(periodo)) return;

            string anio = periodo.Substring(0, 4);
            beReseumen.codigoUsuario = codUsuarioEvaluado;
            beReseumen.prefijoIsoPais = codPais;

            DataTable dtCompetencia = daProceso.CargarCompetencia(CadenaConexion, beReseumen, anio);
            if (dtCompetencia.Rows.Count == 0)
            {
                lblEtiqueta.Visible = false;
            }

            DropDownList ddlCompetencia = new DropDownList();
            ddlCompetencia.DataSource = dtCompetencia;
            ddlCompetencia.DataTextField = "Competencia";
            ddlCompetencia.DataValueField = "CodigoPlanAnual";
            ddlCompetencia.DataBind();

            if (ddlCompetencia.Items.Count != 0)
            {
                CargarRetroalimentcionGrabadas(ddlCompetencia.SelectedValue);
                //CargarZonaAlternativoPorRol(idProceso);
                lblCompentencia.Text = ddlCompetencia.SelectedItem.Text;
            }
        }

        private void CargarRetroalimentcionGrabadas(string codigoPlanAnual)
        {
            BlRetroalimentacion daProceso = new BlRetroalimentacion();
            BeRetroalimentacion beRetroalimentacion = new BeRetroalimentacion();
            beRetroalimentacion.idProceso = idProceso;
            beRetroalimentacion.CodigoPlanAnual = Convert.ToInt32(codigoPlanAnual);

            DataTable resultadoPreguntas = daProceso.ListarRetroalimentacion(CadenaConexion, beRetroalimentacion);

            gvPreguntas.DataSource = resultadoPreguntas;
            gvPreguntas.DataBind();

            string estadoProcesoRetroalimentacion = resultadoPreguntas.Rows[0].ItemArray[4].ToString();

            if (!string.IsNullOrEmpty(estadoProcesoRetroalimentacion))
                lblEstadoIndicador3.Text = bool.Parse(estadoProcesoRetroalimentacion.Trim()) ? " (Completado)" : " (En Proceso)";
            else
                lblEstadoIndicador3.Text = " (En Proceso)";
        }
    }
}
