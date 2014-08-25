
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using WsPlanDesarrollo;

    public partial class AntesCompetencias_Consulta : Page
    {
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
            BlResumenProceso objResumenBL = new BlResumenProceso();
            BeResumenProceso objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            if (IsPostBack) return;

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            string periodoEvaluacion = Utils.QueryString("periodo");

            BeResumenProceso objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlPlanAnual daProceso = new BlPlanAnual();

            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, objResumen.prefijoIsoPais);

            string anio = periodoEvaluacion.Substring(0, 4);

            //ObtenerPlanAnualAdam(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam, objResumen.codigoUsuario);
            ObtenerPlanAnualAdam(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam, objResumen.codigoUsuario, objResumen.cub);

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                BeResumenProceso objResumenNuevo = objResumenBL.ObtenerResumenProcesoByUsuario(
                    objResumen.codigoUsuario, objResumenBE.rolUsuario,
                    periodoEvaluacion, objUsuario.prefijoIsoPais, tipoDialogoDesempenio);
                if (objResumenNuevo != null)
                    objResumen.idProceso = objResumenNuevo.idProceso;
            }
            else
            {
                objResumen.idProceso = int.Parse(txtIdProceso.Text);
            }

            DataTable dt = daProceso.ObtenerPlanAnualGrabadas(CadenaConexion, objResumen);

            if (dt.Rows.Count != 0)
            {
                CargarPlanAnualGrabadas();
            }
            else
            {
                CargarGrilla();
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarPlanAnualGrabadas()
        {
            BlResumenProceso objResumenBL = new BlResumenProceso();
            BlPlanAnual daProceso = new BlPlanAnual();
            BeResumenProceso objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            string periodoEvaluacion = Utils.QueryString("periodo");

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

                BeResumenProceso objResumenNuevo = objResumenBL.ObtenerResumenProcesoByUsuario(
                    objResumen.codigoUsuario, objResumen.rolUsuario,
                    periodoEvaluacion, objResumen.prefijoIsoPais, tipoDialogoDesempenio);

                if (objResumenNuevo != null)
                    objResumen.idProceso = objResumenNuevo.idProceso;
            }
            else
            {
                objResumen.idProceso = int.Parse(txtIdProceso.Text);
            }

            DataTable dtGrabadas = daProceso.ObtenerPlanAnualGrabadas(CadenaConexion, objResumen);

            gvPlanAnual.DataSource = null;
            gvPlanAnual.DataSource = dtGrabadas;
            gvPlanAnual.DataBind();
            Session["_planAnualConsulta"] = dtGrabadas;
            txtObservacion.Text = dtGrabadas.Rows[0]["observacion"].ToString();
        }

        private void CargarGrilla()
        {
            BeResumenProceso objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            BlPlanAnual daProceso = new BlPlanAnual();
            BePlanAnual bePlanAnual = new BePlanAnual();

            bePlanAnual.PrefijoIsoPais = objResumen.prefijoIsoPais;

            string periodoEvaluacion = Utils.QueryString("periodo");

            bePlanAnual.Anio = periodoEvaluacion.Split(new char[] { ' ' })[0];
            bePlanAnual.CodigoColaborador = objResumen.codigoUsuario;

            DataTable dt = daProceso.ObtenerPlanAnual(CadenaConexion, bePlanAnual);

            gvPlanAnual.DataSource = dt;
            gvPlanAnual.DataBind();

            Session["_planAnualConsulta"] = dt;

            if (dt.Rows.Count <= 0)
            {
                //lblMensajes.Text = "Usted no tiene Registrado un Plan Anual";
                //lblObservacion.Visible = false;
                txtObservacion.Visible = false;
                //chica.Style.Add("Display", "none");
            }
        }

        private void ObtenerPlanAnualAdam(string connstring, int anio, string codigoPaisAdam, string documentoIdentidad, string cub)
        {
            BeResumenProceso objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlPlanAnual daProceso = new BlPlanAnual();

            if (!daProceso.ObtenerPlanAnualByUsuario(connstring, objResumen.rolUsuario, objResumen.prefijoIsoPais, anio.ToString(), documentoIdentidad, 1))
            {
                WsInterfaceFFVVSoapClient wsPlanAnual = new WsInterfaceFFVVSoapClient();

                try
                {
                    documentoIdentidad = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                             ? Convert.ToInt32(documentoIdentidad).ToString()
                                             : documentoIdentidad.Trim();

                    //DataSet dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, codigoPaisAdam, documentoIdentidad);
                    DataSet dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, cub);

                    if (dsPlanAnual != null)
                    {
                        if (dsPlanAnual.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = dsPlanAnual.Tables[0].Rows[0];
                            daProceso.InsertarPlanAnualAdam(connstring, objResumen.rolUsuario, objResumen.prefijoIsoPais,
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

        #endregion Metodos
    }
}