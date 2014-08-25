
namespace Evoluciona.Dialogo.Web.Visita
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

    public partial class visitaindicadores2 : Page
    {
        #region Variables

        BeResumenVisita objVisita = new BeResumenVisita();
        BeVisitaIndicador objVisitaIndicadores = new BeVisitaIndicador();
        BlVisitaIndicadores daProceso = new BlVisitaIndicadores();
        public int indexMenuServer;
        public int indexSubMenu;
        public string descRol;
        BlCritica daProcesoCritica = new BlCritica();
        BlPlanAccion planAccionBL = new BlPlanAccion();
        protected BeResumenProceso objResumenBE;
        protected BeUsuario objUsuario;
        public int estadoProceso = 1;
        public int codigoRolUsuario;
        public string prefijoIsoPais;
        public string codigoUsuario;
        public string periodoEvaluacion;
        public int idProceso;
        public string AnioCampana;
        public string PeriodoCerrado;
        public string codigoUsuarioProcesado;
        public int readOnly = 0;

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
            objVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);

            BlResumenProceso objResumenBL = new BlResumenProceso();
            idProceso = objVisita.idProceso;

            if (Page.IsPostBack) return;

            BlInteraccion blProceso = new BlInteraccion();
            DataTable dtGrabadas = blProceso.ObtenerInteraccionGrabadas(CadenaConexion, objVisita.idVisita);
            if (dtGrabadas.Rows.Count > 0)
            {
                txtObjetivosVisita.Text = dtGrabadas.Rows[0]["vchObjetivoVisita"].ToString();
            }

                //string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();
            string tipoDialogoDesempenio = Constantes.Normal;



                objResumenBE = objResumenBL.ObtenerResumenProcesoByUsuario(objVisita.codigoUsuario, objVisita.idRolUsuario, objVisita.periodo, objVisita.prefijoIsoPais, tipoDialogoDesempenio);
                Session["objResumen"] = objResumenBE;
            
            CargarVariables();
            GetEstadosporPeriodo();
            CargarEvaluados();

            if (objVisita.codigoRolUsuario == Constantes.RolGerenteRegion)
            {
                lblRol.Text = "GZ CRÍTICAS";
                lblRolCritica.Text = "Nº GZ Nuevas: ";
                descRol = "GR";
            }
            else if (objVisita.codigoRolUsuario == Constantes.RolGerenteZona)
            {
                lblRol.Text = "LETS CRÍTICAS";
                lblRolCritica.Text = "Nº LETS Nuevas: ";
                lblRolEvaluado.Text = "Lets ";
                descRol = "GZ";
            }
            if (Session[Constantes.VisitaModoLectura] != null)
            {
                btnGrabar.Text = "CONTINUAR";
            }

            readOnly = 1;
            estadoProceso = 0;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                if (objVisita.estadoVisita == Constantes.EstadoVisitaActivo && objVisita.porcentajeAvanceAntes == 80)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objVisita.idVisita, 100, 1);
                    objVisita.porcentajeAvanceAntes = 100;
                    Session[Constantes.ObjUsuarioVisitado] = objVisita;

                }
            }
            ClientScript.RegisterStartupScript(Page.GetType(), "_VisitaIndicadores2", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
        }

        protected void lnkAgregar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Metodos

        private void GetEstadosporPeriodo()
        {
            //objVisita.periodo = PeriodoCerrado;
            BeResumenVisita objVisitaPeriodo = new BeResumenVisita();

            objVisitaPeriodo.codigoUsuario = objVisita.codigoUsuario;
            objVisitaPeriodo.idProceso = objVisita.idProceso;
            objVisitaPeriodo.codigoRolUsuario = objVisita.codigoRolUsuario;
            objVisitaPeriodo.prefijoIsoPais = objVisita.prefijoIsoPais;

            objVisitaPeriodo.periodo = PeriodoCerrado;
            DataSet dsgrdvEstadosporPeriodos = daProceso.GetEstadosporPeriodo(objVisitaPeriodo);

            grdvEstadosporPeriodos.DataSource = dsgrdvEstadosporPeriodos;
            grdvEstadosporPeriodos.DataBind();
            //obtiene cantidad de nuevas
            BlResumenProceso objIndicadores = new BlResumenProceso();
            DataTable dtIndicadores = objIndicadores.ObtenerResumenProcesoById(objVisita.idProceso);
            if (dtIndicadores.Rows.Count > 0)
            {
                lblCantidadNuevas.Text = dtIndicadores.Rows[0]["intNuevasIngresadas"] == DBNull.Value ? "0" : dtIndicadores.Rows[0]["intNuevasIngresadas"].ToString();
            }
            else
            {
                lblCantidadNuevas.Text = "0";
            }
        }

        private void CargarVariables()
        {
            objResumenBE = (BeResumenProceso)Session["objResumen"];

            if (objResumenBE == null)
            {
                BlResumenProceso objResumenBL = new BlResumenProceso();
                //string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                string tipoDialogoDesempenio = Constantes.Normal;
                objResumenBE = objResumenBL.ObtenerResumenProcesoByUsuario(objVisita.codigoUsuario, objVisita.idRolUsuario, objUsuario.periodoEvaluacion, objUsuario.prefijoIsoPais, tipoDialogoDesempenio);
            
                Session["objResumen"] = objResumenBE;
            }


            codigoRolUsuario = objVisita.codigoRolUsuario;
            prefijoIsoPais = objVisita.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;
            periodoEvaluacion = objVisita.periodo;
            idProceso = objResumenBE.idProceso;
            codigoUsuarioProcesado = objResumenBE.codigoUsuario;
           
                                 

            ValidarPeriodoEvaluacion();
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProcesoCritica.ValidarPeriodoEvaluacion(objVisita.periodo, objVisita.prefijoIsoPais, codigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void CargarEvaluados()
        {
            #region Cargar Listas de Planes Criticos

            List<BePlanAccion> lstCriticasDisponibles = planAccionBL.ObtenerCriticas_Visita(objUsuario, objResumenBE,
                objVisita.periodo, codigoRolUsuario, objVisita.idVisita);

            lblTotalElementos.Text = lstCriticasDisponibles.Count.ToString();

            #endregion

            Session["criticasProcesadas"] = lstCriticasDisponibles;

            CargarMenusProcesados();
        }

        private void CargarMenusProcesados()
        {
            List<BePlanAccion> procesados = (List<BePlanAccion>)Session["criticasProcesadas"];
            if (procesados != null && procesados.Count > 0)
            {
                foreach (BePlanAccion item in procesados)
                {
                    MenuItem itemMenu = new MenuItem();
                    //Al momento de Grabar se tiene que hacer un split de este elemento para obtener los dos valores deseados
                    itemMenu.NavigateUrl = item.DocuIdentidad + "," + item.idPlanAccionVisita.ToString();
                    itemMenu.Text = item.NombreCritica;
                    itemMenu.ToolTip = item.PlanAccion;
                    itemMenu.Value = item.idPlanAcccion.ToString();

                    MenuItem itemHitorial = new MenuItem();
                    itemHitorial.NavigateUrl = string.Format("{0}{1}?nombre={2}&codigoEvaluador={3}&codigoEvaluado={4}&tipo={5}",
                        Utils.AbsoluteWebRoot, "PantallasModales/HistorialPeriodoCriticidad.aspx",
                        item.NombreCritica, codigoUsuarioProcesado, item.DocuIdentidad, (int)TipoHistorial.CriticasDurante);

                    itemHitorial.Text = "Ver Historial";
                    itemHitorial.ToolTip = item.postVisita ? "1" : "0";

                    mnuSelecccionados.Items.Add(itemMenu);
                    mnuVerHistorial.Items.Add(itemHitorial);
                }
            }
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

        #endregion
    }
}
