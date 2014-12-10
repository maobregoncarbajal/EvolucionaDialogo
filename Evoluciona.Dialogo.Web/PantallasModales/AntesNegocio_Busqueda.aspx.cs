
namespace Evoluciona.Dialogo.Web.PantallasModales
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;

    public partial class AntesNegocio_Busqueda : Page
    {
        #region Variables

        private readonly BlIndicadores daProceso = new BlIndicadores();

        protected BeUsuario objUsuario;
        public int contadorChecksi = 0;
        public int contadorChecksu = 0;
        public int contadorChecks = 0;
        protected BeResumenProceso objResumenBE;

        public int codigoRolUsuario;
        public string prefijoIsoPais;
        public string codigoUsuario;
        public string periodoEvaluacion;
        public int idProceso;

        public string AnioCampana;
        public string PeriodoCerrado;

        #endregion

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
            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            codigoRolUsuario = objResumenBE.codigoRolUsuario;
            prefijoIsoPais = objResumenBE.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;
            periodoEvaluacion = objUsuario.periodoEvaluacion;
            idProceso = objResumenBE.idProceso;

            ValidarPeriodoEvaluacion();

            if (!IsPostBack)
            {   
                CargarCombos();
                OcultarCombosIndicadores();                
            }
        }

        protected void radioPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            OcultarCombosIndicadores();
        }

        protected void radioCampana_CheckedChanged(object sender, EventArgs e)
        {
            OcultarCombosIndicadores();
        }

        protected void ddlperiodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargarindicadores();
        }

        protected void ddlCampanahasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlCampanadesde.SelectedItem.Text) > Convert.ToInt32(ddlCampanahasta.SelectedItem.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('La campaña de inicio no puede ser mayor a la campaña final')", true);
            }
            else
            {
                Cargarindicadores();
            }
        }

        protected void ddlCampanadesde_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlCampanadesde.SelectedItem.Text) > Convert.ToInt32(ddlCampanahasta.SelectedItem.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "alert('La campaña de inicio no puede ser mayor a la campaña final')", true);
            }
            else
            {
                Cargarindicadores();
            }
        }    

        #endregion

        #region Metodos

        private void CargarCombos()
        {
            //cargar combo Periodo
            ddlperiodo.DataSource = daProceso.ObtenerPeriodo(ddlperiodo.SelectedValue, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
            ddlperiodo.DataTextField = "chrPeriodo";
            ddlperiodo.DataValueField = "chrPeriodo";
            ddlperiodo.DataBind();

            //cargar Campaña Desde
            DataTable datosCampanas = daProceso.ObtenerCampanaHasta(string.Empty, codigoRolUsuario, prefijoIsoPais, CadenaConexion);

            ddlCampanadesde.DataSource = datosCampanas;
            ddlCampanadesde.DataTextField = "chrAnioCampana";
            ddlCampanadesde.DataValueField = "chrAnioCampana";
            ddlCampanadesde.DataBind();

            //cargar Campaña Hasta
            ddlCampanahasta.DataSource = datosCampanas;
            ddlCampanahasta.DataTextField = "chrAnioCampana";
            ddlCampanahasta.DataValueField = "chrAnioCampana";
            ddlCampanahasta.DataBind();
        }

        protected void OcultarCombosIndicadores()
        {
            if (radioPeriodo.Checked == true)
            {
                lblCampanadesde.Visible = false;
                ddlCampanadesde.Visible = false;
                lblCampanahasta.Visible = false;
                ddlCampanahasta.Visible = false;

                ddlperiodo.Visible = true;
            }
            else
            {
                lblCampanadesde.Visible = true;
                ddlCampanadesde.Visible = true;
                lblCampanahasta.Visible = true;
                ddlCampanahasta.Visible = true;
                
                ddlperiodo.Visible = false;
            }

            Cargarindicadores();
        }

        private void Cargarindicadores()
        {
            GridView0.DataBind();

            string campanainicio = null;
            string campanafin = null;
            DataTable dtCampana = daProceso.SeleccionarCampana(PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
            if (dtCampana != null)
            {
                campanainicio = dtCampana.Rows[0]["INICIO_CAMPANA"].ToString();
                campanafin = dtCampana.Rows[0]["FIN_CAMPANA"].ToString();
            }
            if (radioPeriodo.Checked == true)
            {
                GridView0.Columns[6].Visible = true;
                GridView1.Columns[6].Visible = true;

                DataSet dsReporte = daProceso.Cargarindicadoresporperiodo(ddlperiodo.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
                GridView0.DataSource = dsReporte;
                GridView0.DataBind();

                DataSet dsReporteAdicional = daProceso.CargarindicadoresporperiodoVariablesAdicionales(ddlperiodo.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
                GridView1.DataSource = dsReporteAdicional;
                GridView1.DataBind();

                DataTable dt = daProceso.ObtenerCampanaDesde(string.Empty, objResumenBE.codigoRolUsuario, objResumenBE.prefijoIsoPais, ddlperiodo.SelectedValue, CadenaConexion);
                if (dt.Rows.Count == 0) return;
                if (dt.Rows.Count > 1)
                    litMensajeResultado.Text = string.Format("Datos de la campaña {0} a la campaña {1}", dt.Rows[0].ItemArray[0], dt.Rows[dt.Rows.Count - 1].ItemArray[0]);
                else
                    litMensajeResultado.Text = string.Format("Datos de la campaña {0}", dt.Rows[0].ItemArray[0]);
            }
            else
            {
                GridView0.Columns[6].Visible = false;
                GridView1.Columns[6].Visible = false;

                DataSet dsReporte = daProceso.CargarIndicadoresPorRangoCampana(ddlCampanadesde.SelectedValue.Trim(), ddlCampanahasta.SelectedValue, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais);
                GridView0.DataSource = dsReporte;
                GridView0.DataBind();

                DataSet dsReporteAdicional = daProceso.CargarindicadoresporcampanaVariablesAdicionales(ddlCampanadesde.SelectedValue.Trim(), ddlCampanahasta.SelectedValue, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
                GridView1.DataSource = dsReporteAdicional;
                GridView1.DataBind();

                litMensajeResultado.Text = string.Format("Datos de la campaña {0} a la campaña {1}", ddlCampanadesde.SelectedValue, ddlCampanahasta.SelectedValue);
            }

            if (dtCampana != null)
            {
                campanafin = dtCampana.Rows[0]["FIN_CAMPANA"].ToString();
                campanainicio = dtCampana.Rows[0]["INICIO_CAMPANA"].ToString();
            }           
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProceso.ValidarPeriodoEvaluacion(periodoEvaluacion, prefijoIsoPais, codigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        #endregion
    }
}