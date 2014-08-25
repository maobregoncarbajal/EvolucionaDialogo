namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Default : Page
    {
        #region Variables

        private BeAdmin _objAdmin;
        public string PeriodoBuscado;

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            _objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (IsPostBack)
            {
                PeriodoBuscado = HfPeriodo.Value;
                return;
            }

            SeleccionarPais();
            CargarEvaluadores();
        }

        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {

            PeriodoBuscado = HfPeriodo.Value;
            CargarEvaluadores();
        }

        protected void cboRolEvaluador_SelectedIndexChanged(object sender, EventArgs e)
        {
            PeriodoBuscado = HfPeriodo.Value;
            CargarEvaluadores();
        }

        protected void GvPreRender(object sender, EventArgs e)
        {
            var grillaActual = (GridView)sender;
            grillaActual.UseAccessibleHeader = false;
            if (grillaActual.HeaderRow != null)
                grillaActual.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gvInactivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInactivos.PageIndex = e.NewPageIndex;
            gvInactivos.DataSource = Session["listProcesosPorIniciar"];
            gvInactivos.DataBind();
        }

        protected void gvEnProceso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnProceso.PageIndex = e.NewPageIndex;
            gvEnProceso.DataSource = Session["listProcesosEnProceso"];
            gvEnProceso.DataBind();
        }

        protected void gvEnAprobacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnAprobacion.PageIndex = e.NewPageIndex;
            gvEnAprobacion.DataSource = Session["listProcesosAprobacion"];
            gvEnAprobacion.DataBind();
        }

        protected void gvAprobados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAprobados.PageIndex = e.NewPageIndex;
            gvAprobados.DataSource = Session["listProcesosAprobados"];
            gvAprobados.DataBind();
        }

        #endregion Eventos

        #region Metodos

        private void SeleccionarPais()
        {

            switch (_objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    cboPaises.Enabled = true;
                    break;
                case Constantes.RolAdminPais:
                    cboPaises.Enabled = false;
                    break;
                case Constantes.RolAdminEvaluciona:
                    cboPaises.Enabled = false;

                    if (_objAdmin.CodigoPais == "CR")
                    {
                        cboPaises.Items.Clear();
                        cboPaises.Items.Insert(0, new ListItem("COSTA RICA", "CR"));
                        cboPaises.Items.Insert(1, new ListItem("GUATEMALA", "GT"));
                        cboPaises.Items.Insert(2, new ListItem("PANAMA", "PA"));
                        cboPaises.Items.Insert(3, new ListItem("EL SALVADOR", "SV"));
                        cboPaises.Enabled = true;
                    }

                    if (_objAdmin.CodigoPais == "DO")
                    {
                        cboPaises.Items.Clear();
                        cboPaises.Items.Insert(0, new ListItem("REP.DOMINICANA", "DO"));
                        cboPaises.Items.Insert(1, new ListItem("PUERTO RICO", "PR"));
                        cboPaises.Enabled = true;
                    }


                    if (_objAdmin.CodigoPais != "CR" && _objAdmin.CodigoPais != "DO")
                    {
                        cboPaises.Items.Clear();
                        cboPaises.Items.Insert(0, new ListItem(_objAdmin.NombrePais, _objAdmin.CodigoPais));
                        cboPaises.Enabled = true;
                    }

                    break;
            }
        }

        
        private void CargarEvaluadores()
        {
            var rolEvaluador = Convert.ToInt32(cboRolEvaluador.SelectedValue);
            var blConfig = new BlConfiguracion();

            switch (rolEvaluador)
            {
                case Constantes.RolDirectorVentas:
                    cboEvaluador.DataSource = blConfig.SeleccionarDVentasPorEvaluar(cboPaises.SelectedValue);
                    break;
                case Constantes.RolGerenteRegion:
                    cboEvaluador.DataSource = blConfig.SeleccionarGRegionPorEvaluar(cboPaises.SelectedValue);
                    break;
            }

            cboEvaluador.DataTextField = "vchNombreCompleto";
            cboEvaluador.DataValueField = "documentoIdentidad";
            cboEvaluador.DataBind();
        }

        private int ObtenerIDRolEvaluadoByCodigoRolEvaluador(int codigoRolEvaluador)
        {
            var idRolEvaluado = int.MinValue;

            switch (codigoRolEvaluador)
            {
                case Constantes.RolDirectorVentas:
                    idRolEvaluado = Constantes.IdRolGerenteRegion;
                    break;
                case Constantes.RolGerenteRegion:
                    idRolEvaluado = Constantes.IdRolGerenteZona;
                    break;
            }

            return idRolEvaluado;
        }

        private void CargarProcesos()
        {
            try
            {
                var procesoBl = new BlProceso();
                var rolEvaluador = Convert.ToInt32(cboRolEvaluador.SelectedValue);
                var rolEvaluado = ObtenerIDRolEvaluadoByCodigoRolEvaluador(rolEvaluador);

                var evaluador = cboEvaluador.SelectedValue;
                var codigoPais = cboPaises.SelectedValue;
                var periodo = HfPeriodo.Value;
                PeriodoBuscado = periodo;

                var tipoDialogo = cboTipoDialogo.SelectedValue;

                var procesos = procesoBl.ObtenerProcesos(periodo, rolEvaluado, codigoPais, tipoDialogo, evaluador);
                var procesosPorIniciar = new List<BeProceso>();
                var procesosEnProceso = new List<BeProceso>();
                var procesosAprobacion = new List<BeProceso>();
                var procesosAprobados = new List<BeProceso>();

                foreach (var proceso in procesos)
                {
                    switch (proceso.Estado.ToString(CultureInfo.InvariantCulture))
                    {
                        case Constantes.EstadoProcesoEnviado:
                            procesosPorIniciar.Add(proceso);
                            break;
                        case Constantes.EstadoProcesoActivo:
                            procesosEnProceso.Add(proceso);
                            break;
                        case Constantes.EstadoProcesoRevision:
                            procesosAprobacion.Add(proceso);
                            break;
                        case Constantes.EstadoProcesoCulminado:
                            procesosAprobados.Add(proceso);
                            break;
                    }
                }

                if (procesosPorIniciar.Count == 0)
                    procesosPorIniciar.Add(new BeProceso());
                if (procesosEnProceso.Count == 0)
                    procesosEnProceso.Add(new BeProceso());
                if (procesosAprobacion.Count == 0)
                    procesosAprobacion.Add(new BeProceso());
                if (procesosAprobados.Count == 0)
                    procesosAprobados.Add(new BeProceso());

                Session["listProcesosPorIniciar"] = procesosPorIniciar;
                Session["listProcesosEnProceso"] = procesosEnProceso;
                Session["listProcesosAprobacion"] = procesosAprobacion;
                Session["listProcesosAprobados"] = procesosAprobados;

                gvInactivos.DataSource = procesosPorIniciar;
                gvInactivos.DataBind();
                gvEnProceso.DataSource = procesosEnProceso;
                gvEnProceso.DataBind();
                gvEnAprobacion.DataSource = procesosAprobacion;
                gvEnAprobacion.DataBind();
                gvAprobados.DataSource = procesosAprobados;
                gvAprobados.DataBind();

                litMensaje.Text = string.Empty;
            }
            catch (Exception ex)
            {
                litMensaje.Text = ex.Message;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        #endregion Metodos

        protected void btnBuscarProcesosAux_Click(object sender, EventArgs e)
        {
            CargarProcesos();
        }
    }
}