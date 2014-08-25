
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

    public partial class PlanAccionVisita : Page
    {
        #region Variables

        public int indexMenuServer;
        public int indexSubMenu;
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
            
            indexMenuServer=Convert.ToInt32(Request["indiceM"]);
            indexSubMenu=Convert.ToInt32(Request["indiceSM"]);

            if (Page.IsPostBack) return;

            BlInteraccion blProceso = new BlInteraccion();
            DataTable dtGrabadas = blProceso.ObtenerInteraccionGrabadas(CadenaConexion, objResumenVisita.idVisita);
            if (dtGrabadas.Rows.Count > 0)
            {
                txtObjetivosVisita.Text = dtGrabadas.Rows[0]["vchObjetivoVisita"].ToString();
            }

            if (objResumenVisita.codigoRolUsuario == Constantes.RolGerenteZona)
            {
                lblDescZona2.Text = "Secciones";
                lblDescZona.Text = "Secciones";
            }
            CargarIndicadores();
            DesabilitarContorles();
            if (Session[Constantes.VisitaModoLectura] != null)
            {
                btnGrabar.Text = "CONTINUAR";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);

            if (Session[Constantes.VisitaModoLectura] == null)
            {
                objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];

                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo && objResumenVisita.porcentajeAvanceAntes == 40)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 60, 1);
                    objResumenVisita.porcentajeAvanceAntes = 60;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                }
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "_indicadores", "<script language='javascript'> javascript:AbrirMensaje(); </script>");

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
