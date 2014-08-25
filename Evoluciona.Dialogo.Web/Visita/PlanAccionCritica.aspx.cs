
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

    public partial class PlanAccionCritica : Page
    {
        public int indexMenuServer;
        public int indexSubMenu;
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
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);
            
            BlResumenProceso objResumenBL = new BlResumenProceso();
            idProceso = objResumenVisita.idProceso;
           
            if (!Page.IsPostBack)
            {
                if (Session["tipoDialogoDesempenio"] != null)
                {
                    string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                    objResumenBE = objResumenBL.ObtenerResumenProcesoByUsuario(objResumenVisita.codigoUsuario, objResumenVisita.idRolUsuario, objResumenVisita.periodo, objResumenVisita.prefijoIsoPais, tipoDialogoDesempenio);
                    Session["objResumen"] = objResumenBE;
                    CargarVariables();
                }
                if (objResumenVisita.codigoRolUsuario == Constantes.RolGerenteZona)
                {
                    lblRolEvaluado.Text = "Lideres";
                }
                //DesabilitarContorles();
                if (Session[Constantes.VisitaModoLectura] != null)
                {
                    readOnly = 1;
                    estadoProceso = 0;
                    btnGuardar.Text = "CONTINUAR";
                }


                CargarEvaluados();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                string[] estados = null;

                BlCritica daProceso = new BlCritica();
                bool esCorrecto = true;
                int index = 0;

                string estadosSeleccionados = txtEstadosEvaluados.Text;
                if (indexMenuServer == 3)
                {
                    if (string.IsNullOrEmpty(estadosSeleccionados)) return;
                    estados = estadosSeleccionados.Substring(1).Split(',');
                }
                
                foreach (MenuItem item in mnuSelecccionados.Items)
                {
                    BePlanAccion planAccion = new BePlanAccion();
                    planAccion.idVisita = objResumenVisita.idVisita;
                    planAccion.idPlanAcccion = int.Parse(item.Value);
                    planAccion.IDProceso = idProceso;
                    planAccion.DocuIdentidad = item.NavigateUrl.Split(',').GetValue(0).ToString();
                    planAccion.PlanAccion = item.ToolTip;
                    planAccion.idPlanAccionVisita = Convert.ToInt32(item.NavigateUrl.Split(',').GetValue(1));
                                        

                    #region Verificar Estado del Registro

                    if (indexMenuServer == 3)
                    {
                        planAccion.postVisita = estados[index] == "1";
                    }
                    else
                    {
                        planAccion.postVisita = false;
                    }

                    #endregion

                    if (planAccion.idPlanAcccion > 0)
                    {
                        esCorrecto = planAccionBL.ActualizarPlanAccion(planAccion);
                        if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo)
                        {
                           
                            if (planAccion.idPlanAccionVisita <1)
                            {
                                esCorrecto = planAccionBL.InsertarPlanAccionVisita(planAccion);
                            }
                            else
                            {
                                esCorrecto = planAccionBL.ActualizarPlanAccionVisita(planAccion);
                            }
                            
                        }

                    }


                    index++;
                }
                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo && objResumenVisita.porcentajeAvanceDespues == 60)
                {//
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 100, 3);
                    blResumen.ActualizarEstadoVisita(objResumenVisita.idVisita, Constantes.EstadoVisitaCerrado);
                    objResumenVisita.estadoVisita = Constantes.EstadoVisitaCerrado;
                    objResumenVisita.porcentajeAvanceDespues = 100;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                    
                }
                else if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo && objResumenVisita.porcentajeAvanceDurante == 80)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 100, 2);
                    blResumen.ActualizarEstadoVisita(objResumenVisita.idVisita, Constantes.EstadoVisitaPostDialogo);
                    objResumenVisita.estadoVisita = Constantes.EstadoVisitaPostDialogo;
                    objResumenVisita.porcentajeAvanceDurante = 100;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;

                    
                }
                if (indexMenuServer == 3)
                {//
                    ClientScript.RegisterStartupScript(Page.GetType(), "_PAccionCriticaD", "<script language='javascript'> javascript:AbrirMensajeDespues(); </script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_PAccionCritica", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                }
            }
            else
            {
                if (indexMenuServer == 3)
                {//
                    ClientScript.RegisterStartupScript(Page.GetType(), "_PAccionCriticaD2", "<script language='javascript'> javascript:AbrirMensajeDespues(); </script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_PAccionCritica2", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                }
            }

            //this.esCorrecto = esCorrecto ? 1 : 0;
        }

        private void CargarVariables()
        {
            
            objResumenBE = (BeResumenProceso)Session["objResumen"];
            if (objResumenBE == null)
            {
                BlResumenProceso objResumenBL = new BlResumenProceso();
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();
                objResumenBE = objResumenBL.ObtenerResumenProcesoByUsuario(objResumenVisita.codigoUsuario, objResumenVisita.idRolUsuario, objUsuario.periodoEvaluacion, objUsuario.prefijoIsoPais, tipoDialogoDesempenio);
                Session["objResumen"] = objResumenBE;
            }

            
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            codigoRolUsuario = objResumenVisita.codigoRolUsuario;
            prefijoIsoPais = objResumenVisita.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;
            periodoEvaluacion = objResumenBE.periodo;
            idProceso = objResumenBE.idProceso;
            codigoUsuarioProcesado = objResumenVisita.codigoUsuario;

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
                objResumenVisita.periodo, codigoRolUsuario, objResumenVisita.idVisita);

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

        private void Crear_Actualizar(string valor, string textoIngresado)
        {
            foreach (MenuItem item in mnuSelecccionados.Items)
            {
                if (item.NavigateUrl == valor)
                {
                    item.ToolTip = textoIngresado;
                    return;
                }
            }
        }

        protected void lnkAgregar_Click(object sender, EventArgs e)
        {
            string valorSeleccionado = txtValorCritica.Text;

            Crear_Actualizar(valorSeleccionado, txtTextoIngresado.Text);

            txtTextoIngresado.Text = string.Empty;
            txtValorCritica.Text = string.Empty;
        }

    }
}
