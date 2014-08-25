
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

    public partial class VariablesAnalizar : Page
    {
        #region Variables

        protected BlCritica daProceso = new BlCritica();
        protected BeUsuario objUsuario;

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
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

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

            switch (codigoRolUsuario)
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
            BeResumenProceso objResumenProceso = new BeResumenProceso();
            BlResumenProceso objResumenBL = new BlResumenProceso();
            objResumenProceso = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            //objResumenBE = (beResumenProceso)Session[constantes.objUsuarioProcesado];
            //objUsuario = (beUsuario)Session[constantes.objUsuarioLogeado];

            codigoRolUsuario = objResumenProceso.codigoRolUsuario;  //objResumenBE.codigoRolUsuario;
            prefijoIsoPais = objResumenProceso.prefijoIsoPais; //objResumenBE.prefijoIsoPais;
            codigoUsuario = Utils.QueryString("codigo"); //objResumenBE.codigoUsuario;

            PeriodoEvaluacion = Utils.QueryString("periodo");

            string periodo = Utils.QueryString("periodo");
            string codigo = Utils.QueryString("codigo");
            string pais = objResumenProceso.prefijoIsoPais;
            int rol = objResumenProceso.codigoRolUsuario;
            string rolDescripcion = "GERENTE DE " + Utils.QueryString("rol");
            string nombre = Utils.QueryString("nombre");
            lblCabecera.Text = string.Format("Rol: {0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Codigo: {1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Utils.QueryString("rol") + ": {2}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Periodo: {3}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pais : {4}", rolDescripcion, codigo, nombre, periodo, pais);


            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                BeResumenProceso objResumen = objResumenBL.ObtenerResumenProcesoByUsuario(codigoUsuario, objResumenProceso.rolUsuario, PeriodoEvaluacion, prefijoIsoPais, tipoDialogoDesempenio);
                if (objResumen != null)
                    idProceso = objResumen.idProceso;
            }
            else
            {
                idProceso = int.Parse(txtIdProceso.Text);
            }

            codigoUsuarioProcesado = codigoUsuario; //objResumenBE.codigoUsuario;

            ValidarPeriodoEvaluacion();
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProceso.ValidarPeriodoEvaluacion(PeriodoEvaluacion, prefijoIsoPais, codigoRolUsuario, CadenaConexion);
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
