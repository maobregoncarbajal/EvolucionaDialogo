
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
    using WsPlanDesarrollo;

    public partial class AntesCompetenciasEvaluado : Page
    {
        #region Variables

        public int indexMenuServer = 1;
        public int indexSubMenu = 3;
        public int estadoProceso = 0;
        public int readOnly = 0;
        public int porcentaje = 0;
        public int noExisteData = 0;
        public int esCorrecto = 0;
        private BeResumenProceso objResumen;
        private BeUsuario objUsuario;
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
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            estadoProceso = int.Parse(objResumen.estadoProceso);
            nmbrEvld = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);

            CalcularAvanze();
            CrearLinksResumen();

            if (Session["_soloLectura"] != null)
                readOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;

            if (IsPostBack) return;
            if (Session["NombreEvaluado"] == null) return;

            string periodoEvaluacion = string.Empty;
            if (Session["periodoActual"] != null)
                periodoEvaluacion = Session["periodoActual"].ToString();
            else
                periodoEvaluacion = objUsuario.periodoEvaluacion;

            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                                            Session["NombreEvaluado"].ToString(), periodoEvaluacion,
                                            "dialogo_antes_competencias.jpg");

            BlPlanAnual daProceso = new BlPlanAnual();

            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, objResumen.prefijoIsoPais);

            string anio = periodoEvaluacion.Substring(0, 4);


            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                ObtenerPlanAnualNuevas(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam, objResumen.codigoUsuario);
            }
            else
            {
                ObtenerPlanAnualAdam(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam, objResumen.codigoUsuario, objResumen.cub);
            }


            DataTable dt = daProceso.ObtenerPlanAnualGrabadasPreDialogo(CadenaConexion, objResumen);

            if (dt.Rows.Count != 0)
            {
                CargarPlanAnualGrabadas(dt);
            }
            else
            {
                CargarGrilla();
            }

            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = nmbrEvld;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GrabarPlanAnual();

            if (String.Equals(nmbrEvld, Constantes.Nueva))
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
            BlPlanAnual daProceso = new BlPlanAnual();
            BePlanAnual bePlanAnual = new BePlanAnual();

            bePlanAnual.PrefijoIsoPais = objResumen.prefijoIsoPais;

            string periodoEvaluacion = string.Empty;

            if (Session["periodoActual"] != null)
                periodoEvaluacion = Session["periodoActual"].ToString();
            else
                periodoEvaluacion = objUsuario.periodoEvaluacion;

            bePlanAnual.Anio = periodoEvaluacion.Split(new char[] { ' ' })[0];
            bePlanAnual.CodigoColaborador = objResumen.codigoUsuario;

            DataTable dt = new DataTable();

            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                dt = daProceso.ObtenerPlanAnualNuevas(CadenaConexion, bePlanAnual);
            }
            else
            {
                dt = daProceso.ObtenerPlanAnual(CadenaConexion, bePlanAnual);
            }

            gvPlanAnual.DataSource = dt;
            gvPlanAnual.DataBind();

            Session["_planAnual"] = dt;

            if (dt.Rows.Count <= 0)
            {
                noExisteData = 1;
                lblObservacion.Visible = false;
                txtObservacion.Visible = false;
            }
        }

        private void ObtenerPlanAnualAdam(string connstring, int anio, string codigoPaisAdam, string documentoIdentidad, string cub)
        {
            BlPlanAnual daProceso = new BlPlanAnual();

            //if (!daProceso.ObtenerPlanAnualByUsuario(connstring, objResumen.rolUsuario, objResumen.prefijoIsoPais, anio.ToString(), documentoIdentidad, 1))
            //{
            WsInterfaceFFVVSoapClient wsPlanAnual = new WsInterfaceFFVVSoapClient();
            //wsPlanAnual.Timeout = 60000;

            try
            {
                string documentoIdentidadConsulta = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                         ? Convert.ToInt32(documentoIdentidad).ToString()
                                         : documentoIdentidad.Trim();




                //DataSet dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, codigoPaisAdam, documentoIdentidadConsulta);
                DataSet dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, cub);


                if (dsPlanAnual != null)
                {
                    if (dsPlanAnual.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fls in dsPlanAnual.Tables[0].Rows)
                        {
                            daProceso.InsertarPlanAnualAdam(connstring, objResumen.rolUsuario,
                            objResumen.prefijoIsoPais,
                            anio.ToString(), documentoIdentidad,
                            fls["NombresEvaluado"].ToString(),
                            fls["DescripcionCompetencia"].ToString(),
                            fls["DescripcionComportamiento"].ToString(),
                            fls["ActividadesPlanDesarrollo"].ToString(),
                            fls["DescripcionSugerencia"].ToString(), 1,
                            Convert.ToInt32(fls["CodigoCompetencia"]));
                        }
                    }
                }
            }
            catch (Exception)
            {
                //lblMensajes.Text = "El servicio no se encuentra disponible, por favor intente luego";
                //lblObservacion.Visible = false;
                //txtObservacion.Visible = false;
            }
            //}
        }


        private void ObtenerPlanAnualNuevas(string connstring, int anio, string codigoPaisAdam, string documentoIdentidad)
        {
            BlPlanAnual daProceso = new BlPlanAnual();

            if (!daProceso.ObtenerPlanAnualByUsuario(connstring, objResumen.rolUsuario, objResumen.prefijoIsoPais, anio.ToString(), documentoIdentidad, 1))
            {
                try
                {
                    string documentoIdentidadConsulta = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                             ? Convert.ToInt32(documentoIdentidad).ToString()
                                             : documentoIdentidad.Trim();


                    BlPlanAnual planAnualBL = new BlPlanAnual();

                    DataSet dsPlanAnual = planAnualBL.ConsultaPlanDesarrollo(connstring, anio, codigoPaisAdam, documentoIdentidadConsulta, objResumen);

                    if (dsPlanAnual != null)
                    {
                        if (dsPlanAnual.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = dsPlanAnual.Tables[0].Rows[0];

                            daProceso.InsertarPlanAnualAdam(connstring, objResumen.rolUsuario,
                                                            objResumen.prefijoIsoPais,
                                                            anio.ToString(), documentoIdentidad,
                                                            dr["NombresEvaluado"].ToString(),
                                                            dr["DescripcionCompetencia"].ToString(),
                                                            dr["DescripcionComportamiento"].ToString(),
                                                            dr["ActividadesPlanDesarrollo"].ToString(),
                                                            dr["DescripcionSugerencia"].ToString(), 1,
                                                            Convert.ToInt32(dr["CodigoCompetencia"]));
                        }
                    }
                }
                catch (Exception)
                {
                    //lblMensajes.Text = "El servicio no se encuentra disponible, por favor intente luego";
                    //lblObservacion.Visible = false;
                    //txtObservacion.Visible = false;
                }
            }
        }



        private void GrabarPlanAnual()
        {
            BlPlanAnual planAnualBL = new BlPlanAnual();

            try
            {
                DataTable dt = (DataTable)Session["_planAnual"];

                foreach (DataRow var in dt.Rows)
                {
                    BePlanAnual bePlanAnual = new BePlanAnual();

                    bePlanAnual.idProceso = objResumen.idProceso;
                    bePlanAnual.CodigoPlanAnual = int.Parse(var[3].ToString());
                    bePlanAnual.Observacion = txtObservacion.Text;
                    bePlanAnual.idUsuario = objUsuario.idUsuario;

                    planAnualBL.IngresarPlanAnualPreDialogo(CadenaConexion, bePlanAnual);
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
            porcentaje = ProgresoHelper.CalcularAvanze(objResumen.idProceso, TipoPantalla.Antes);
        }

        private void CrearLinksResumen()
        {
            hlkResumen.NavigateUrl =
                string.Format(
                    "{0}Desempenio/ResumenPreDialogo.aspx?nomEvaluado={1}&codEvaluado={2}&idProceso={3}&rolEvaluado={4}&codPais={5}&periodo={6}&codEvaluador={7}&imprimir=NO&soloNegocio=SI",
                    Utils.RelativeWebRoot, objResumen.nombreEvaluado.Trim(), objResumen.codigoUsuario,
                    objResumen.idProceso, objResumen.codigoRolUsuario, objResumen.prefijoIsoPais, objUsuario.periodoEvaluacion,
                    objResumen.codigoUsuarioEvaluador);
        }

        #endregion Metodos
    }
}