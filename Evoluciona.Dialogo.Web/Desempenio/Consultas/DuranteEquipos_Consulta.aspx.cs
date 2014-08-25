
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

    public partial class DuranteEquipos_Consulta : Page
    {
        #region Variables

        private BlCritica daProceso = new BlCritica();
        private BlPlanAccion planAccionBL = new BlPlanAccion();

        protected BeUsuario objUsuario;
        protected BeResumenProceso objResumenBE;
        public int codigoRolUsuario;
        public string prefijoIsoPais;
        public string codigoUsuario;
        public string periodoEvaluacion;
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
        }

        #endregion Eventos

        #region Metodos

        private void CargarEvaluados()
        {
            #region Cargar Listas de Planes Criticos

            objResumenBE.idProceso = idProceso;

            List<BePlanAccion> lstCriticasDisponibles = planAccionBL.ObtenerCriticas(objUsuario, objResumenBE,
                PeriodoCerrado, codigoRolUsuario);

            lblTotalElementos.Text = lstCriticasDisponibles.Count.ToString();

            #endregion Cargar Listas de Planes Criticos

            //List<bePlanAccion> listaCriticasDisponibles = ObtenerCriticasRestantes(lstCriticasDisponibles, true);
            //List<bePlanAccion> listaCriticasUtilizadas = ObtenerCriticasRestantes(lstCriticasDisponibles, false);

            Session["criticasProcesadas_Consulta2"] = lstCriticasDisponibles;

            CargarMenusProcesados();

            BePlanAccion criticaSeleccione = new BePlanAccion();
            criticaSeleccione.NombreCritica = "[SELECCIONE]";
            criticaSeleccione.DocuIdentidad = string.Empty;

            //listaCriticasDisponibles.Insert(0, criticaSeleccione);
        }

        private List<BePlanAccion> ObtenerCriticasRestantes(List<BePlanAccion> criticasTotales, bool sinUtilizar)
        {
            List<BePlanAccion> criticasRestantes = new List<BePlanAccion>();
            foreach (BePlanAccion item in criticasTotales)
            {
                if (sinUtilizar)
                {
                    if (string.IsNullOrEmpty(item.PlanAccion))
                        criticasRestantes.Add(item);
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.PlanAccion))
                        criticasRestantes.Add(item);
                }
            }

            return criticasRestantes;
        }

        private void CargarMenusProcesados()
        {
            List<BePlanAccion> procesados = (List<BePlanAccion>)Session["criticasProcesadas_Consulta2"];
            if (procesados != null && procesados.Count > 0)
            {
                foreach (BePlanAccion item in procesados)
                {
                    MenuItem itemMenu = new MenuItem();
                    itemMenu.NavigateUrl = item.DocuIdentidad;
                    itemMenu.Text = item.NombreCritica;
                    itemMenu.ToolTip = item.PlanAccion;
                    itemMenu.Value = item.idPlanAcccion.ToString();

                    MenuItem itemHitorial = new MenuItem();
                    itemHitorial.NavigateUrl = string.Format("{0}{1}?nombre={2}&codigoEvaluador={3}&codigoEvaluado={4}&tipo={5}",
                        Utils.AbsoluteWebRoot, "PantallasModales/HistorialPeriodoCriticidad.aspx",
                        item.NombreCritica, codigoUsuarioProcesado, item.DocuIdentidad, (int)TipoHistorial.CriticasDurante);

                    itemHitorial.Text = "Ver Historial";
                    itemHitorial.ToolTip = item.PreDialogo ? "1" : "0";

                    mnuSelecccionados.Items.Add(itemMenu);
                    mnuVerHistorial.Items.Add(itemHitorial);
                }
            }
        }

        private void CargarVariables()
        {
            BlResumenProceso objResumenBL = new BlResumenProceso();

            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            codigoRolUsuario = objResumenBE.codigoRolUsuario;
            prefijoIsoPais = objResumenBE.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;
            periodoEvaluacion = Utils.QueryString("periodo");

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                BeResumenProceso objResumenNuevo =
                    objResumenBL.ObtenerResumenProcesoByUsuario(objResumenBE.codigoUsuario, objResumenBE.rolUsuario,
                                                                periodoEvaluacion, objResumenBE.prefijoIsoPais, tipoDialogoDesempenio);
                if (objResumenNuevo != null)
                    idProceso = objResumenNuevo.idProceso;
            }
            else
            {
                idProceso = int.Parse(txtIdProceso.Text);
            }

            codigoUsuarioProcesado = objResumenBE.codigoUsuario;

            ValidarPeriodoEvaluacion();
            CargarCriticaEquipos();
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProceso.ValidarPeriodoEvaluacion(periodoEvaluacion, objResumenBE.prefijoIsoPais, codigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void CargarCriticaEquipos()
        {
            List<BeCriticas> equipos = daProceso.ObtenerCriticidadEquipos(objResumenBE.idProceso);

            gvEquipos.DataSource = equipos;
            gvEquipos.DataBind();

        }


        #endregion Metodos
    }
}