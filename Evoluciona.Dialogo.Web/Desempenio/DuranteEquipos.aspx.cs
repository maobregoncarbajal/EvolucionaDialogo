
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helper;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class DuranteEquipos : Page
    {
        #region Variables

        public int indexMenuServer = 2;
        public int indexSubMenu = 2;
        public int esCorrecto = 0;
        public int readOnly = 0;
        public int porcentaje = 0;
        public string noValidar;

        public int estadoProceso = 0;

        BlCritica daProceso = new BlCritica();
        BlPlanAccion planAccionBL = new BlPlanAccion();

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
        public string nmbrEvld;

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
            nmbrEvld = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);
            CargarVariables();
            estadoProceso = int.Parse(objResumenBE.estadoProceso);
            CalcularAvanze();

            if (Session["_soloLectura"] != null)
            {
                readOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;
            }

            string nombreImagen = string.Empty;

            #region Asignando los Valores correctos al Mensaje

            if (Utils.QueryStringInt("aprobacion") == 1)
            {
                indexMenuServer = 3;
                lblAccion.Text = "DESPUÉS";
                hlkUrl.Text = "DIÁLOGO/DESPUÉS/COMPETENCIAS";
                hlkUrl.NavigateUrl = Utils.AbsoluteWebRoot + "Desempenio/DuranteCompetencias.aspx?aprobacion=1";
                nombreImagen = "dialogo_despues_equipos.jpg";
            }
            else
            {
                lblAccion.Text = "DURANTE";
                hlkUrl.Text = "DIÁLOGO/DURANTE/COMPETENCIAS";
                hlkUrl.NavigateUrl = Utils.AbsoluteWebRoot + "Desempenio/DuranteCompetencias.aspx";
                nombreImagen = "dialogo_durante_equipos.jpg";
            }

            #endregion Asignando los Valores correctos al Mensaje

            if (IsPostBack) return;

            if (Session["NombreEvaluado"] == null) return;

            #region Cargando Header y Asignando Periodo de Evaluacion

            string periodoEvaluacion = string.Empty;
            if (Session["periodoActual"] != null)
                periodoEvaluacion = Session["periodoActual"].ToString();
            else
                periodoEvaluacion = objUsuario.periodoEvaluacion;

            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                Session["NombreEvaluado"].ToString(), periodoEvaluacion, nombreImagen);

            #endregion Cargando Header y Asignando Periodo de Evaluacion

            CargarEvaluados();

            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                txtVariablesAnalizar.Enabled = false;
                txtTextoIngresado.Enabled = false;
                lnkAgregar.Enabled = false;
                dvPlanEquipos.Visible = false;
            }
        }

        protected void lnkAgregar_Click(object sender, EventArgs e)
        {
            string valorSeleccionado = txtValorCritica.Text;

            Crear_Actualizar(valorSeleccionado, txtTextoIngresado.Text);

            txtTextoIngresado.Text = string.Empty;
            txtValorCritica.Text = string.Empty;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string[] estados = null;
            bool esCorrecto = true;
            int index = 0;

            if (noValidar != "si")
            {
                string estadosSeleccionados = txtEstadosEvaluados.Text;
                if (estadoProceso == 3)
                {
                    if (string.IsNullOrEmpty(estadosSeleccionados)) return;
                    estados = estadosSeleccionados.Substring(1).Split(',');
                }

                foreach (MenuItem item in mnuSelecccionados.Items)
                {
                    BePlanAccion planAccion = new BePlanAccion();

                    planAccion.idPlanAcccion = int.Parse(item.Value);
                    planAccion.IDProceso = idProceso;
                    planAccion.DocuIdentidad = item.NavigateUrl;
                    planAccion.PlanAccion = item.ToolTip;

                    #region Verificar Estado del Registro

                    if (estadoProceso == 3)
                    {
                        planAccion.PreDialogo = estados[index] == "1";
                    }
                    else
                    {
                        planAccion.PreDialogo = false;
                    }

                    #endregion Verificar Estado del Registro

                    if (planAccion.idPlanAcccion > 0)
                    {
                        esCorrecto &= planAccionBL.ActualizarPlanAccion(planAccion);
                    }
                    else
                    {
                        esCorrecto &= planAccionBL.IngresarPlanAccion(planAccion);
                    }
                    index++;
                }
            }
            this.esCorrecto = esCorrecto ? 1 : 0;
        }

        protected void gvEquiposPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipos.PageIndex = e.NewPageIndex;
            CargarCriticaEquipos();
        }

        protected void gvEquiposRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idEquipo = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "cmd_editar":
                    CargarCriticaEquipo(idEquipo);
                    break;
                case "cmd_eliminar":
                    EliminarCriticaEquipo(idEquipo);
                    break;
            }
        }

        protected void btnGuardarEquipo_Click(object sender, EventArgs e)
        {
            GuardarCriticaEquipo();
        }

        #endregion Eventos

        #region Metodos

        private void CargarVariables()
        {
            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            codigoRolUsuario = objResumenBE.codigoRolUsuario;
            prefijoIsoPais = objResumenBE.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;
            periodoEvaluacion = objUsuario.periodoEvaluacion;
            idProceso = objResumenBE.idProceso;
            codigoUsuarioProcesado = objResumenBE.codigoUsuario;
            if (objResumenBE.codigoRolUsuario == Constantes.RolGerenteZona)
            {
                lblRolEvaluado.Text = "Líder ";
            }
            ValidarPeriodoEvaluacion();
            CargarCriticaEquipos();
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = daProceso.ValidarPeriodoEvaluacion(objUsuario.periodoEvaluacion, objResumenBE.prefijoIsoPais, codigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void CargarEvaluados()
        {
            #region Cargar Listas de Planes Criticos

            List<BePlanAccion> lstCriticasDisponibles = planAccionBL.ObtenerCriticas(objUsuario, objResumenBE,
                PeriodoCerrado, codigoRolUsuario);

            lblTotalElementos.Text = lstCriticasDisponibles.Count.ToString();

            #endregion Cargar Listas de Planes Criticos

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
                    itemMenu.NavigateUrl = item.DocuIdentidad;

                    string textoImagen = "<img src='" + Utils.RelativeWebRoot + "Images/ok.png' alt='ok' class='imagenOk'/>";

                    if (item.PreDialogo)
                        itemMenu.Text = item.NombreCritica + textoImagen;
                    else
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

        private void CalcularAvanze()
        {
            noValidar = "no";
            porcentaje = ProgresoHelper.CalcularAvanze(objResumenBE.idProceso, TipoPantalla.Durante);
        }

        private void CargarCriticaEquipos()
        {
            List<BeCriticas> equipos = daProceso.ObtenerCriticidadEquipos(objResumenBE.idProceso);

            gvEquipos.DataSource = equipos;
            gvEquipos.DataBind();

            lblMensajeError.Text = string.Empty;
        }

        private void CargarCriticaEquipo(int idEquipo)
        {
            BeCriticas equipo = daProceso.ObtenerCriticidadEquipo(idEquipo);

            hidIDEquipo.Value = equipo.idCritica.ToString();
            txtNombre.Text = equipo.NombreEquipo;
            txtVariableConsiderar.Text = equipo.variableConsiderar;
            txtPlanAccion.Text = equipo.PlanAccion;
            txtNombre.Focus();

            ClientScript.RegisterStartupScript(GetType(), "MostrarPopup", "jQuery(document).ready(function() { $('#equiposPopup').dialog('open'); });", true);
        }

        private void EliminarCriticaEquipo(int idEquipo)
        {
            daProceso.EliminarCriticidadEquipo(idEquipo);
            CargarCriticaEquipos();
        }

        private void GuardarCriticaEquipo()
        {
            try
            {
                int idEquipo = Convert.ToInt32(hidIDEquipo.Value);

                if (idEquipo == 0)
                {
                    BeCriticas equipo = new BeCriticas();

                    equipo.idProceso = objResumenBE.idProceso;
                    equipo.NombreEquipo = txtNombre.Text.Trim();
                    equipo.variableConsiderar = txtVariableConsiderar.Text;
                    equipo.PlanAccion = txtPlanAccion.Text;

                    daProceso.InsertarCriticidadEquipo(equipo);

                    lblMensajeError.Text = "Se registró satisfactoriamente.";
                }
                else
                {
                    BeCriticas equipo = daProceso.ObtenerCriticidadEquipo(idEquipo);

                    equipo.NombreEquipo = txtNombre.Text.Trim();
                    equipo.variableConsiderar = txtVariableConsiderar.Text;
                    equipo.PlanAccion = txtPlanAccion.Text;

                    daProceso.ActualizarCriticidadEquipo(equipo);

                    lblMensajeError.Text = "Se actualizó satisfactoriamente.";
                }

                CargarCriticaEquipos();
            }
            catch (Exception)
            {
                lblMensajeError.Text = "Ocurrio un error al intentar guardar los Datos.";
            }
        }

        #endregion Metodos
    }
}