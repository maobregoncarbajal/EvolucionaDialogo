
namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using Web.Helpers;

    public partial class FichaPersonal : Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string pais = Page.Request["pais"];
            string codigoUsuario = Page.Request["codigoUsuario"];
            string codigoRol = Page.Request["codigoRol"];
            string periodoEvaluacion = string.Empty;

            BlPlanAnual daProceso = new BlPlanAnual();

            // string sugerencia = (string) dtPlanAnual.Rows[0]["vchSugerencia"];
            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, pais);

            BlUsuario obBLUsuario = new BlUsuario();
            BeUsuario objUsuario = obBLUsuario.ObtenerDatosUsuario(pais, Convert.ToInt32(codigoRol), codigoUsuario, Constantes.EstadoActivo);

            BlConfiguracion objConfig = new BlConfiguracion();
            DataTable dtperiodo = objConfig.SeleccionarPeriodo(pais);

            if (dtperiodo.Rows.Count > 0)
            {
                periodoEvaluacion = dtperiodo.Rows[0]["chrPeriodo"].ToString();

            }

            if (objUsuario != null)
            {
                int codProcesado = objConfig.ValidarInicioProceso(objUsuario.prefijoIsoPais, periodoEvaluacion,
                                                                  Constantes.IndicadorEvaluadoDvGr);

                MainView.ActiveViewIndex = 0;

                hlResumen.NavigateUrl = string.Format(
                        "javascript:CargarResumen('{0}Admin/ResumenProceso.aspx?nomEvaluado={1}&codEvaluado={2}&idProceso={3}&rolEvaluado={4}&codPais={5}&periodo={6}&codEvaluador={7}');",
                        Utils.AbsoluteWebRoot, objUsuario.nombreUsuario, objUsuario.codigoUsuario, codProcesado,
                        objUsuario.codigoRol, objUsuario.prefijoIsoPais, periodoEvaluacion, objUsuario.codigoUsuario);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ficha", "ficha('" + codigoUsuario + "','" + codigoPaisAdam + "');", true);
        }

        protected void btnDatosPersonales_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;
            ActualizarCurrentTab(0);
        }

        protected void btnDatosBelcorp_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 1;
            ActualizarCurrentTab(1);
        }

        protected void btnOtrosDatos_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 2;
            ActualizarCurrentTab(2);
        }

        private void ActualizarCurrentTab(int activeIndex)
        {
            btnDatosPersonales.CssClass = "";
            btnDatosBelcorp.CssClass = "";
            btnOtrosDatos.CssClass = "";

            switch (activeIndex)
            {
                case 0:
                    btnDatosPersonales.CssClass = "current";
                    break;
                case 1:
                    btnDatosBelcorp.CssClass = "current";
                    break;
                case 2:
                    btnOtrosDatos.CssClass = "current";
                    break;
            }
        }
    }
}
