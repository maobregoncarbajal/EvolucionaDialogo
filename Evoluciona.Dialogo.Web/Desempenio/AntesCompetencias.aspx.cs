
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
    using System.Globalization;
    using System.Web.UI;
    using WsPlanDesarrollo;

    public partial class AntesCompetencias : Page
    {
        #region Variables

        public int indexMenuServer = 1;
        public int indexSubMenu = 3;
        public int estadoProceso = 0;
        public int readOnly = 0;
        public int porcentaje = 0;
        public int noExisteData = 0;
        public int esCorrecto = 0;
        private BeResumenProceso _objResumen;
        private BeUsuario _objUsuario;
        public string NmbrEvld;

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
            _objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            _objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            estadoProceso = int.Parse(_objResumen.estadoProceso);
            NmbrEvld = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);

            CalcularAvanze();
            CrearLinksResumen();

            if (Session["_soloLectura"] != null)
                readOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;

            if (IsPostBack) return;
            if (Session["NombreEvaluado"] == null) return;

            var periodoEvaluacion = Session["periodoActual"] != null ? Session["periodoActual"].ToString() : _objUsuario.periodoEvaluacion;

            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                                            Session["NombreEvaluado"].ToString(), periodoEvaluacion,
                                            "dialogo_antes_competencias.jpg");

            var daProceso = new BlPlanAnual();
            var codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, _objResumen.prefijoIsoPais);
            var anio = periodoEvaluacion.Substring(0, 4);


            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                ObtenerPlanAnualNuevas(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam, _objResumen.codigoUsuario);
            }
            else
            {
                ObtenerPlanAnualAdam(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam, _objResumen.codigoUsuario, _objResumen.cub);
            }
            
            var dt = daProceso.ObtenerPlanAnualGrabadas(CadenaConexion, _objResumen);

            if (dt.Rows.Count != 0)
            {
                CargarPlanAnualGrabadas(dt);
            }
            else
            {
                CargarGrilla();
            }

            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = NmbrEvld;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GrabarPlanAnual();

            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = "triggerBtnguardar";
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarPlanAnualGrabadas(DataTable dtGrabadas)
        {
            gvPlanAnual.DataSource = null;
            gvPlanAnual.DataSource = dtGrabadas;
            gvPlanAnual.DataBind();
            Session["_planAnual"] = dtGrabadas;
            txtObservacion.Text = dtGrabadas.Rows[0]["observacion"].ToString();
        }

        private void CargarGrilla()
        {
            var daProceso = new BlPlanAnual();
            var bePlanAnual = new BePlanAnual {PrefijoIsoPais = _objResumen.prefijoIsoPais};

            var periodoEvaluacion = Session["periodoActual"] != null ? Session["periodoActual"].ToString() : _objUsuario.periodoEvaluacion;

            bePlanAnual.Anio = periodoEvaluacion.Split(new[] { ' ' })[0];
            bePlanAnual.CodigoColaborador = _objResumen.codigoUsuario;

            var dt = String.Equals(NmbrEvld, Constantes.Nueva) ? daProceso.ObtenerPlanAnualNuevas(CadenaConexion, bePlanAnual) : daProceso.ObtenerPlanAnual(CadenaConexion, bePlanAnual);

            gvPlanAnual.DataSource = dt;
            gvPlanAnual.DataBind();

            Session["_planAnual"] = dt;

            if (dt.Rows.Count > 0) return;
            noExisteData = 1;
            lblObservacion.Visible = false;
            txtObservacion.Visible = false;
        }

        private void ObtenerPlanAnualAdam(string connstring, int anio, string codigoPaisAdam, string documentoIdentidad, string cub)
        {
            var daProceso = new BlPlanAnual();

            var wsPlanAnual = new WsInterfaceFFVVSoapClient();

                var documentoIdentidadConsulta = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                         ? Convert.ToInt32(documentoIdentidad).ToString(CultureInfo.InvariantCulture)
                                         : documentoIdentidad.Trim();

                var dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, cub);


            if (dsPlanAnual == null) return;
            if (dsPlanAnual.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow fls in dsPlanAnual.Tables[0].Rows)
            {
                daProceso.InsertarPlanAnualAdam(connstring, _objResumen.rolUsuario,
                    _objResumen.prefijoIsoPais,
                    anio.ToString(CultureInfo.InvariantCulture), documentoIdentidad,
                    fls["NombresEvaluado"].ToString(),
                    fls["DescripcionCompetencia"].ToString(),
                    fls["DescripcionComportamiento"].ToString(),
                    fls["ActividadesPlanDesarrollo"].ToString(),
                    fls["DescripcionSugerencia"].ToString(), 1,
                    Convert.ToInt32(fls["CodigoCompetencia"]));
            }
        }


        private void ObtenerPlanAnualNuevas(string connstring, int anio, string codigoPaisAdam, string documentoIdentidad)
        {
            var daProceso = new BlPlanAnual();

            if (daProceso.ObtenerPlanAnualByUsuario(connstring, _objResumen.rolUsuario, _objResumen.prefijoIsoPais,
                anio.ToString(CultureInfo.InvariantCulture), documentoIdentidad, 1)) return;
            var documentoIdentidadConsulta = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                ? Convert.ToInt32(documentoIdentidad).ToString(CultureInfo.InvariantCulture)
                : documentoIdentidad.Trim();


            var planAnualBL = new BlPlanAnual();

            var dsPlanAnual = planAnualBL.ConsultaPlanDesarrollo(connstring, anio, codigoPaisAdam, documentoIdentidadConsulta, _objResumen);

            if (dsPlanAnual == null) return;
            if (dsPlanAnual.Tables[0].Rows.Count <= 0) return;
            var dr = dsPlanAnual.Tables[0].Rows[0];

            daProceso.InsertarPlanAnualAdam(connstring, _objResumen.rolUsuario,
                _objResumen.prefijoIsoPais,
                anio.ToString(CultureInfo.InvariantCulture), documentoIdentidad,
                dr["NombresEvaluado"].ToString(),
                dr["DescripcionCompetencia"].ToString(),
                dr["DescripcionComportamiento"].ToString(),
                dr["ActividadesPlanDesarrollo"].ToString(),
                dr["DescripcionSugerencia"].ToString(), 1,
                Convert.ToInt32(dr["CodigoCompetencia"]));
        }



        private void GrabarPlanAnual()
        {
            var planAnualBL = new BlPlanAnual();

            try
            {
                var dt = (DataTable)Session["_planAnual"];

                foreach (DataRow var in dt.Rows)
                {
                    var bePlanAnual = new BePlanAnual
                    {
                        idProceso = _objResumen.idProceso,
                        CodigoPlanAnual = int.Parse(var[3].ToString()),
                        Observacion = txtObservacion.Text,
                        idUsuario = _objUsuario.idUsuario
                    };

                    planAnualBL.IngresarPlanAnual(CadenaConexion, bePlanAnual);
                }

                esCorrecto = 1;
            }
            catch (Exception)
            {
                esCorrecto = 0;
            }
        }

        private void CalcularAvanze()
        {
            porcentaje = ProgresoHelper.CalcularAvanze(_objResumen.idProceso, TipoPantalla.Antes);
        }

        private void CrearLinksResumen()
        {
            hlkResumen.NavigateUrl =
                string.Format(
                    "{0}Admin/ResumenProceso.aspx?nomEvaluado={1}&codEvaluado={2}&idProceso={3}&rolEvaluado={4}&codPais={5}&periodo={6}&codEvaluador={7}&imprimir=NO&soloNegocio=SI",
                    Utils.RelativeWebRoot, _objResumen.nombreEvaluado.Trim(), _objResumen.codigoUsuario,
                    _objResumen.idProceso, _objResumen.codigoRolUsuario, _objResumen.prefijoIsoPais, _objUsuario.periodoEvaluacion,
                    _objResumen.codigoUsuarioEvaluador);
        }

        #endregion Metodos
    }
}