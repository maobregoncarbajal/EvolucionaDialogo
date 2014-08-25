
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

    public partial class PlanAccionCritica_Consulta : Page
    {
        protected BeResumenVisita objResumenVisita;
        protected BeResumenProceso objResumenBE;
        protected BeUsuario objUsuario;
        public int estadoProceso = 1;
        public string connstring = string.Empty;
        public int codigoRolUsuario;
        public string prefijoIsoPais;
        public string codigoUsuario;
        public string periodoEvaluacion;
        public int idProceso;
        public string AnioCampana;
        public string PeriodoCerrado;
        public string codigoUsuarioProcesado;
        public int readOnly = 0;

        BlCritica daProceso = new BlCritica();
        BlPlanAccion planAccionBL = new BlPlanAccion();


        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            string periodo = Request["periodo"];

            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerCodigoVisita(objResumenVisita.codigoUsuario, objUsuario.codigoUsuario, objResumenVisita.idRolUsuario, periodo);
            if (dtVisita.Rows.Count > 0)
            {
                objResumenVisita.idVisita = Convert.ToInt32(dtVisita.Rows[0]["codigoVisita"]);
                objResumenVisita.estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                objResumenVisita.idProceso = Convert.ToInt32(dtVisita.Rows[0]["intIDProceso"]);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_accionesCriticaConsulta", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }
            
            BlResumenProceso objResumenBL = new BlResumenProceso();
            idProceso = objResumenVisita.idProceso;
           
            if (!Page.IsPostBack)
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                objResumenBE = objResumenBL.ObtenerResumenProcesoByUsuario(objResumenVisita.codigoUsuario, objResumenVisita.idRolUsuario, objResumenVisita.periodo, objResumenVisita.prefijoIsoPais, tipoDialogoDesempenio);
                Session["objResumen"] = objResumenBE;
                CargarVariables();

                //DesabilitarContorles();
                if (Session[Constantes.VisitaModoLectura] != null)
                {
                    readOnly = 1;
                    estadoProceso = 0;
                }
                CargarEvaluados();
            }
        }

      
        private void CargarVariables()
        {
            objResumenBE = (BeResumenProceso)Session["objResumen"];
            
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            codigoRolUsuario = objResumenVisita.codigoRolUsuario;
            prefijoIsoPais = objResumenVisita.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;
            periodoEvaluacion = objResumenVisita.periodo;
            idProceso = objResumenBE.idProceso;
            codigoUsuarioProcesado = objResumenBE.codigoUsuario;

            ValidarPeriodoEvaluacion();
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProceso.ValidarPeriodoEvaluacion(objResumenVisita.periodo, objResumenVisita.prefijoIsoPais, codigoRolUsuario, connstring);
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
                PeriodoCerrado, codigoRolUsuario,objResumenVisita.idVisita);

            lblTotalElementos.Text = lstCriticasDisponibles.Count.ToString();

            #endregion

            List<BePlanAccion> listaCriticasDisponibles = ObtenerCriticasRestantes(lstCriticasDisponibles, true);
            List<BePlanAccion> listaCriticasUtilizadas = ObtenerCriticasRestantes(lstCriticasDisponibles, false);

            Session["criticasProcesadas"] = listaCriticasUtilizadas;

            CargarMenusProcesados();

            BePlanAccion criticaSeleccione = new BePlanAccion();
            criticaSeleccione.NombreCritica = "[SELECCIONE]";
            criticaSeleccione.DocuIdentidad = string.Empty;

            listaCriticasDisponibles.Insert(0, criticaSeleccione);

            Session["criticasDisponibles"] = listaCriticasDisponibles;

            CargarCriticasDisponibles(listaCriticasDisponibles);
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

        private void CargarCriticasDisponibles(List<BePlanAccion> listaCriticasDisponibles)
        {
            cboEvaluados.DataSource = listaCriticasDisponibles;
            cboEvaluados.DataTextField = "NombreCritica";
            cboEvaluados.DataValueField = "DocuIdentidad";
            cboEvaluados.DataBind();
            cboEvaluados.Enabled = false;
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

        private BePlanAccion BuscarCriticas(string valorBuscar)
        {
            List<BePlanAccion> criticas = (List<BePlanAccion>)Session["criticasDisponibles"];
            if (criticas != null)
            {
                foreach (BePlanAccion item in criticas)
                {
                    if (item.DocuIdentidad == valorBuscar)
                        return item;
                }
            }

            return null;
        }
    }
}
