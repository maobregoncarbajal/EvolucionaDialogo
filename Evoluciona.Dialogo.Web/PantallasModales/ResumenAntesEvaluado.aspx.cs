
namespace Evoluciona.Dialogo.Web.PantallasModales
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

    public partial class ResumenAntesEvaluado : Page
    {
        BlIndicadores indicadorBL = new BlIndicadores();
        public int Imprimir = 0;
        public string descRol;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["NombreEvaluado"] == null) return;

            lblEvaluado.Text = Session["NombreEvaluado"].ToString();

            CargarAntesNegocio();
            CargarAntesEquipos();
            CargarPlanAnualGrabadas();

            string seImprime = Utils.QueryString("imp");

            if (!string.IsNullOrEmpty(seImprime))
                Imprimir = int.Parse(seImprime);
        }

        private void CargarAntesEquipos()
        {
            BlCritica criticaBL = new BlCritica();
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            BeResumenProceso objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            DataTable dtPeriodoEvaluacion = criticaBL.ValidarPeriodoEvaluacion(objUsuario.periodoEvaluacion, objResumenBE.prefijoIsoPais, objResumenBE.codigoRolUsuario, connstring);

            List<BeCriticas> lstCargarCriticasProcesadas = criticaBL.ListaCargarCriticasProcesadasResumenEvaluado(objResumenBE.codigoUsuario,
                dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString(), objResumenBE.codigoRolUsuario, objResumenBE.prefijoIsoPais, connstring, objResumenBE.idProceso, dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString());

            gvPersonasIngresadas.DataSource = lstCargarCriticasProcesadas;
            gvPersonasIngresadas.DataBind();

            BlIndicadores indicadorBL = new BlIndicadores();
            string periodoCerrado = ValidarPeriodoEvaluacion();
            dlstResumenCriticas.DataSource = indicadorBL.ObtenerResumen(periodoCerrado, objResumenBE.codigoUsuario, objResumenBE.idProceso, objResumenBE.codigoRolUsuario, objResumenBE.prefijoIsoPais);
            dlstResumenCriticas.DataBind();
        }

        private void CargarAntesNegocio()
        {
            BlIndicadores indicadorBL = new BlIndicadores();
            BeResumenProceso objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            if (objResumenBE.codigoRolUsuario == Constantes.RolGerenteRegion)
            {
                descRol = "GR";
            }

            else if (objResumenBE.codigoRolUsuario == Constantes.RolGerenteZona)
            {
                descRol = "GZ";
            }
            //cargar indicadores
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            DataTable dtPeriodoEvaluacion = indicadorBL.ValidarPeriodoEvaluacion(objResumenBE.periodo, objResumenBE.prefijoIsoPais, objResumenBE.codigoRolUsuario, connstring);
            string PeriodoCerrado = "";
            if (dtPeriodoEvaluacion != null)
            {
                //AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
            GetVariablesBase(objResumenBE, PeriodoCerrado);
            GetVariablesAdicionales(objResumenBE, PeriodoCerrado);

            DataTable variablesUtilizadas = indicadorBL.ObtenerVariablesCausaPorProcesoEvaluado(objResumenBE.idProceso);
            if (variablesUtilizadas.Rows.Count > 0)
            {
                DataRow filaVariable1 = variablesUtilizadas.Rows[0];
                DataRow filaVariable2 = variablesUtilizadas.Rows[1];
                DataRow filaVariable3 = variablesUtilizadas.Rows[2];
                DataRow filaVariable4 = variablesUtilizadas.Rows[3];

                lblvariable1Desc.Text = filaVariable1.ItemArray[1].ToString();
                lblvariable2Desc.Text = filaVariable3.ItemArray[1].ToString();

                ddlVariableCausa1.Text = filaVariable1.ItemArray[2].ToString();
                txtVariable1PlanPeriodo.Text = filaVariable1.ItemArray[3].ToString();
                txtVariable1Real.Text = filaVariable1.ItemArray[4].ToString();
                txtVariable1Diferencia.Text = filaVariable1.ItemArray[5].ToString();


                ddlVariableCausa2.Text = filaVariable2.ItemArray[2].ToString();
                txtVariable2PlanPeriodo.Text = filaVariable2.ItemArray[3].ToString();
                txtVariable2Real.Text = filaVariable2.ItemArray[4].ToString();
                txtVariable2Diferencia.Text = filaVariable2.ItemArray[5].ToString();

                ddlVariableCausa3.Text = filaVariable3.ItemArray[2].ToString();
                txtVariable3PlanPeriodo.Text = filaVariable3.ItemArray[3].ToString();
                txtVariable3Real.Text = filaVariable3.ItemArray[4].ToString();
                txtVariable3Diferencia.Text = filaVariable3.ItemArray[5].ToString();

                ddlVariableCausa4.Text = filaVariable4.ItemArray[2].ToString();
                txtVariable4PlanPeriodo.Text = filaVariable4.ItemArray[3].ToString();
                txtVariable4Real.Text = filaVariable4.ItemArray[4].ToString();
                txtVariable4Diferencia.Text = filaVariable4.ItemArray[5].ToString();
            }

        }
        private void GetVariablesBase(BeResumenProceso objResumenBE, string PeriodoCerrado)
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            BlIndicadores blIndi = new BlIndicadores();
            DataSet dsgrdvVariablesBase = blIndi.CargarindicadoresporperiodoEvaluado(PeriodoCerrado, objResumenBE.codigoUsuario, objResumenBE.idProceso, objResumenBE.codigoRolUsuario, objResumenBE.prefijoIsoPais, connstring);
            grdvVariablesBase.DataSource = dsgrdvVariablesBase;
            grdvVariablesBase.DataBind();
        }

        private void GetVariablesAdicionales(BeResumenProceso objResumenBE, string PeriodoCerrado)
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            BlIndicadores blIndi = new BlIndicadores();
            DataSet dsgrdvVariablesAdicionales = blIndi.CargarindicadoresporperiodoVariablesAdicionalesEvaluado(PeriodoCerrado, objResumenBE.codigoUsuario, objResumenBE.idProceso, objResumenBE.codigoRolUsuario, objResumenBE.prefijoIsoPais, connstring);
            grdvVariablesAdicionales.DataSource = dsgrdvVariablesAdicionales;
            grdvVariablesAdicionales.DataBind();
        }
        private void CargarPlanAnualGrabadas()
        {
            BlPlanAnual daProceso = new BlPlanAnual();
            BeResumenProceso objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            DataTable dtGrabadas = new DataTable();

            dtGrabadas = daProceso.ObtenerPlanAnualGrabadasEvaluado(connstring, objResumen);

            gvPlanAnual.DataSource = null;
            gvPlanAnual.DataSource = dtGrabadas;
            gvPlanAnual.DataBind();
            Session["_planAnual"] = dtGrabadas;
            if (dtGrabadas.Rows.Count > 0)
            {
                lblObservacion.Text = dtGrabadas.Rows[0]["observacion"].ToString().ToUpper();
            }
            else
            {
                lblObservacion.Text = "";
            }

        }

        private string ValidarPeriodoEvaluacion()
        {
            string PeriodoCerrado = string.Empty;
            BeResumenProceso objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlCritica criticaBL = new BlCritica();
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            DataTable dtPeriodoEvaluacion = criticaBL.ValidarPeriodoEvaluacion(objResumenBE.periodo, objResumenBE.prefijoIsoPais, objResumenBE.codigoRolUsuario, connstring);
            if (dtPeriodoEvaluacion != null)
            {
                //AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
            return PeriodoCerrado;
        }
    }
}