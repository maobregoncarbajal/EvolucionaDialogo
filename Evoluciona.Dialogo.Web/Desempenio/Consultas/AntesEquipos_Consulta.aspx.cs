
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class AntesEquipos_Consulta : Page
    {
        #region Variables

        protected BlCritica daProceso = new BlCritica();
        protected BeUsuario objUsuario;
        protected BeResumenProceso objResumenBE;

        public int codigoRolUsuario;
        public string prefijoIsoPais;
        public string codigoUsuario;
        public string PeriodoEvaluacion;
        public int idProceso;

        public string AnioCampana;
        public string PeriodoCerrado;

        public string codigoUsuarioProcesado;

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
            CargarVariables();

            if (IsPostBack) return;

            CargarEvaluados();
            CargarDescripcionesRol();
            CargarResumen();
        }

        #endregion Eventos

        #region Metodos

        private void CargarDescripcionesRol()
        {
            string ubicacionRol = string.Empty;

            if (objUsuario.codigoRol == Constantes.RolGerenteRegion)
                ubicacionRol = "Líderes";
            else
                ubicacionRol = "Gerentes de Zona";

            lblRolEvaluado.Text = lblRolEvaluado_1.Text = ubicacionRol;
        }

        private void CargarResumen()
        {
            BlIndicadores indicadorBL = new BlIndicadores();
            BlCritica criticaBL = new BlCritica();

            DataTable dtResumen = indicadorBL.ObtenerResumen(PeriodoCerrado, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais);
            ddlResumen.DataSource = dtResumen;
            ddlResumen.DataBind();

            if (dtResumen.Rows.Count > 0)
            {
                lblUltimaCampanha.Text = dtResumen.Rows[0].ItemArray[0].ToString().Trim();
            }

            switch (objResumenBE.codigoRolUsuario)
            {
                case Constantes.RolGerenteRegion:
                    {
                        lstCriticas.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "CRITICA");
                        lstEstables.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "ESTABLE");
                        lstProductivas.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "PRODUCTIVA");
                    }
                    break;
                case Constantes.RolGerenteZona:
                    {
                        lstCriticas.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "CRITICA");
                        lstEstables.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "EXITOSA");
                        lstProductivas.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "PRODUCTIVA");

                        lblNumero.Text = " Nº Líderes Nuevas";
                        lblCriticas.Text = "LÍDERES CRÍTICAS";
                        lblEstable.Text = "LÍDERES EXITOSOS";
                        lblProductivas.Text = "LÍDERES PRODUCTIVOS";
                    }
                    break;
            }

            lstCriticas.DataBind();
            lstEstables.DataBind();
            lstProductivas.DataBind();
        }

        private void CargarVariables()
        {
            BlResumenProceso objResumenBL = new BlResumenProceso();

            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            codigoRolUsuario = objResumenBE.codigoRolUsuario;
            prefijoIsoPais = objResumenBE.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;

            PeriodoEvaluacion = Utils.QueryString("periodo");

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                BeResumenProceso objResumen = objResumenBL.ObtenerResumenProcesoByUsuario(codigoUsuario, objResumenBE.rolUsuario, PeriodoEvaluacion, objResumenBE.prefijoIsoPais, tipoDialogoDesempenio);
                if (objResumen != null)
                    idProceso = objResumen.idProceso;
            }
            else
            {
                idProceso = int.Parse(txtIdProceso.Text);
            }

            codigoUsuarioProcesado = objResumenBE.codigoUsuario;

            ValidarPeriodoEvaluacion();
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProceso.ValidarPeriodoEvaluacion(PeriodoEvaluacion, objResumenBE.prefijoIsoPais, codigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void CargarEvaluados()
        {
            List<BeCriticas> lstCargarCriticasSinProcesar = daProceso.ListaCargarCriticasDisponibles(codigoUsuarioProcesado, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais);

            lblTotalElementos.Text = lstCargarCriticasSinProcesar.Count.ToString();

            List<BeCriticas> lstCargarCriticasProcesadas = daProceso.ListaCargarCriticasProcesadas(codigoUsuarioProcesado, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, CadenaConexion, idProceso, AnioCampana);

            Session["criticasProcesadas_consulta"] = lstCargarCriticasProcesadas;

            CargarMenusProcesados();
        }

        private void CargarMenusProcesados()
        {
            List<BeCriticas> procesados = (List<BeCriticas>)Session["criticasProcesadas_consulta"];
            if (procesados != null && procesados.Count > 0)
            {
                foreach (BeCriticas item in procesados)
                {
                    MenuItem itemMenu = new MenuItem();
                    itemMenu.NavigateUrl = item.documentoIdentidad;
                    itemMenu.Text = item.nombresCritica;
                    itemMenu.ToolTip = item.variableConsiderar;

                    MenuItem itemHitorial = new MenuItem();
                    itemHitorial.NavigateUrl =
                        string.Format("{0}{1}?nombre={2}&codigoEvaluador={3}&codigoEvaluado={4}&tipo={5}",
                                      Utils.AbsoluteWebRoot, "PantallasModales/HistorialPeriodoCriticidad.aspx",
                                      item.nombresCritica, codigoUsuarioProcesado, item.documentoIdentidad,
                                      (int)TipoHistorial.CriticasAntes);

                    itemHitorial.Text = "Ver Historial";

                    mnuSelecccionados.Items.Add(itemMenu);
                    mnuVerHistorial.Items.Add(itemHitorial);
                }
            }
        }

        #endregion Metodos
    }
}