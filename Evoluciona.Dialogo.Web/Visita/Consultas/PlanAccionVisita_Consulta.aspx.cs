
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;

    public partial class PlanAccionVisita_Consulta : Page
    {
        #region Variables

        protected BeResumenVisita objResumenVisita;

        #endregion

        #region Propiedades

        public string CadenaConexion
        {
            get
            {
                string connstring = string.Empty;
                if (string.IsNullOrEmpty(Session["connApp"].ToString()))
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
                connstring = Session["connApp"].ToString();
                return connstring;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            string periodo = Request["periodo"];

            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerCodigoVisita(objResumenVisita.codigoUsuario, objResumenVisita.codigoUsuarioEvaluador, objResumenVisita.idRolUsuario, periodo);
            if (dtVisita.Rows.Count > 0)
            {
                objResumenVisita.idVisita = Convert.ToInt32(dtVisita.Rows[0]["codigoVisita"]);
                objResumenVisita.estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                objResumenVisita.idProceso = Convert.ToInt32(dtVisita.Rows[0]["intIDProceso"]);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_PlanAccionConsulta", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }

            if (Page.IsPostBack) return;

            BlInteraccion blProceso = new BlInteraccion();
            DataTable dtGrabadas = blProceso.ObtenerInteraccionGrabadas(CadenaConexion, objResumenVisita.idVisita);
            if (dtGrabadas.Rows.Count > 0)
            {
                txtObjetivosVisita.Text = dtGrabadas.Rows[0]["vchObjetivoVisita"].ToString();
            }

            CargarIndicadores();
            DesabilitarContorles();
        }

        #endregion

        #region Metodos

        private void DesabilitarContorles()
        {
            txtPlanAccion1.Enabled = false;
            txtPlanAccion2.Enabled = false;
            txtZonas1.Enabled = false;
            txtZonas2.Enabled = false;
        }

        private void CargarIndicadores()
        {
            BlIndicadores objIndicadorBL = new BlIndicadores();
            BlVariableEnfoque objVarEnfoquesBL = new BlVariableEnfoque();

            // carga los indicadores // objResumenVisita.idProceso  4
            DataTable dtIndicadores = objIndicadorBL.ObtenerIndicadoresProcesados(objResumenVisita.idProceso);

            if (dtIndicadores.Rows.Count > 0)
            {
                string campaniaProcesada = dtIndicadores.Rows[0]["chrAnioCampanha"].ToString();
                for (int x = 0; x < dtIndicadores.Rows.Count; x++)
                {
                    if (x == 0)
                    {
                        lblVariable1.Text = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();
                        hdIdIndicador1.Value = dtIndicadores.Rows[x]["intIDIndicador"].ToString();

                        List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(hdIdIndicador1.Value));
                        if (lstVariableEnfoque.Count > 0)
                        {
                            //txtCampania1.Text = lstVariableEnfoque[0].campania;
                            txtZonas1.Text = lstVariableEnfoque[0].zonas;
                            hdIdVarEnfoque1.Value = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                            txtPlanAccion1.Text = lstVariableEnfoque[0].planAccion;
                        }
                    }
                    if (x == 1)
                    {
                        lblVariable2.Text = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();
                        hdIdIndicador2.Value = dtIndicadores.Rows[x]["intIDIndicador"].ToString();
                        //intIDIndicador
                        List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(hdIdIndicador2.Value));
                        if (lstVariableEnfoque.Count > 0)
                        {
                            //txtCampania2.Text = lstVariableEnfoque[0].campania;
                            txtZonas2.Text = lstVariableEnfoque[0].zonas;
                            hdIdVarEnfoque2.Value = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                            txtPlanAccion2.Text = lstVariableEnfoque[0].planAccion;
                        }
                    }
                }

                DataTable dtDescripcionVariableEnfoque1 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblVariable1.Text.Trim(), campaniaProcesada, objResumenVisita.periodo);
                if (dtDescripcionVariableEnfoque1 != null)
                {
                    if (dtDescripcionVariableEnfoque1.Rows.Count > 0)
                    {
                        Label1.Text = dtDescripcionVariableEnfoque1.Rows[0]["vchDesVariable"].ToString();
                    }
                }
                DataTable dtDescripcionVariableEnfoque2 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblVariable2.Text.Trim(), campaniaProcesada, objResumenVisita.periodo);
                if (dtDescripcionVariableEnfoque2 != null)
                {
                    if (dtDescripcionVariableEnfoque2.Rows.Count > 0)
                    {
                        Label2.Text = dtDescripcionVariableEnfoque2.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtVariablesCausa1 = new DataTable();
                DataTable dtVariablesCausa2 = new DataTable();

                if (objResumenVisita.codigoRolUsuario == Constantes.RolGerenteRegion)
                {
                    dtVariablesCausa1 = objIndicadorBL.ObtenerVariablesCausaVisita(objResumenVisita.idProceso, lblVariable1.Text.Trim(), objResumenVisita.codigoUsuario);
                    dtVariablesCausa2 = objIndicadorBL.ObtenerVariablesCausaVisita(objResumenVisita.idProceso, lblVariable2.Text.Trim(), objResumenVisita.codigoUsuario);
                }
                else if (objResumenVisita.codigoRolUsuario == Constantes.RolGerenteZona)
                {
                    dtVariablesCausa1 = objIndicadorBL.ObtenerVariablesCausaVisitaGz(objResumenVisita.idProceso, lblVariable1.Text.Trim(), objResumenVisita.codigoUsuario);
                    dtVariablesCausa2 = objIndicadorBL.ObtenerVariablesCausaVisitaGz(objResumenVisita.idProceso, lblVariable2.Text.Trim(), objResumenVisita.codigoUsuario);
                }

                //cargar la Grilla colocar el IDPRoceso 1era Grilla
                gvVariablesCausa1.DataSource = dtVariablesCausa1;
                gvVariablesCausa1.DataBind();

                //cargar la Grilla colocar el IDPRoceso 2da Grilla
                gvVariablesCausa2.DataSource = dtVariablesCausa2;
                gvVariablesCausa2.DataBind();
            }
        }

        #endregion
    }
}
