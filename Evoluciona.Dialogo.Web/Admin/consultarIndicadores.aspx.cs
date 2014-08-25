
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class consultarIndicadores : Page
    {
        public string connstring = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarCombos();
            }
        }
        private void cargarCombos()
        {
            BlDataMart blObtenerData = new BlDataMart();
            List<BePais> listaPaises = blObtenerData.ObtenerListaPaises();
            ddlPaises.DataSource = listaPaises;
            ddlPaises.DataTextField = "prefijoIsoPais";
            ddlPaises.DataBind();

            ddlPaises.DataSource = null;
            ddlPaises.DataBind();
            ddlPaises.Items.Insert(0, new ListItem("[Seleccionar]", "0"));

            ddlCampana.DataSource = null;
            ddlCampana.DataBind();
            ddlCampana.Items.Insert(0, new ListItem("[Seleccionar]", ""));
        }

        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            connstring = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            BlIndicadores indicadorBL = new BlIndicadores();

            ddlCampana.DataSource = indicadorBL.ObtenerCampanaDesde("", Convert.ToInt32(ddlRoles.SelectedValue), ddlPaises.SelectedValue, "", connstring);

            ddlCampana.DataTextField = "chrAnioCampana";
            ddlCampana.DataValueField = "chrAnioCampana";
            ddlCampana.DataBind();

            BlConfiguracion blConfig = new BlConfiguracion();
            DataTable dtGR = blConfig.SeleccionarGRegionPorEvaluar(ddlPaises.SelectedValue);
            ddlGR.DataSource = dtGR;
            ddlGR.DataTextField = "vchNombreCompleto";
            if (ddlRoles.SelectedValue == Constantes.RolGerenteRegion.ToString())
                ddlGR.DataValueField = "documentoIdentidad";
            else
                ddlGR.DataValueField = "IDUsuario";
            ddlGR.DataBind();

            ddlGR.Items.Insert(0, new ListItem("[Seleccionar]", "0"));

            ddlGZ.Items.Clear();
            ddlGZ.DataSource = null;
            ddlGZ.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            connstring = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            BlIndicadores indicadorBL = new BlIndicadores();
            DataSet dsReporte;
            if (ddlRoles.SelectedValue == Constantes.RolGerenteRegion.ToString())
                dsReporte = indicadorBL.Cargarindicadoresporcampana(ddlCampana.SelectedValue.Trim(), ddlGR.SelectedValue, 0, Convert.ToInt32(ddlRoles.SelectedValue.Trim()), ddlPaises.SelectedValue.Trim(), connstring);
            else
                dsReporte = indicadorBL.Cargarindicadoresporcampana(ddlCampana.SelectedValue.Trim(), ddlGZ.SelectedValue, 0, Convert.ToInt32(ddlRoles.SelectedValue.Trim()), ddlPaises.SelectedValue.Trim(), connstring);

            gvVariables.DataSource = dsReporte;
            gvVariables.DataBind();

            DataSet dsReporteA;
            if (ddlRoles.SelectedValue == Constantes.RolGerenteRegion.ToString())
                dsReporteA = indicadorBL.CargarindicadoresporcampanaVariablesAdicionales2(ddlCampana.SelectedValue, ddlGR.SelectedValue, 0, Convert.ToInt32(ddlRoles.SelectedValue.Trim()), ddlPaises.SelectedValue.Trim(), connstring);
            else
                dsReporteA = indicadorBL.CargarindicadoresporcampanaVariablesAdicionales2(ddlCampana.SelectedValue, ddlGZ.SelectedValue, 0, Convert.ToInt32(ddlRoles.SelectedValue.Trim()), ddlPaises.SelectedValue.Trim(), connstring);

            gvVariablesA.DataSource = dsReporteA;
            gvVariablesA.DataBind();

        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoles.SelectedValue != "0")
            {
                if (ddlPaises.SelectedValue != "0")
                {
                    BlConfiguracion blConfig = new BlConfiguracion();
                    DataTable dtGR = blConfig.SeleccionarGRegionPorEvaluar(ddlPaises.SelectedValue);
                    ddlGR.DataSource = dtGR;
                    ddlGR.DataTextField = "vchNombreCompleto";
                    if (ddlRoles.SelectedValue == Constantes.RolGerenteRegion.ToString())
                        ddlGR.DataValueField = "documentoIdentidad";
                    else
                        ddlGR.DataValueField = "IDUsuario";
                    ddlGR.DataBind();

                    ddlGR.Items.Insert(0, new ListItem("[Seleccionar]", "0"));

                    ddlGZ.Items.Clear();
                    ddlGZ.DataSource = null;
                    ddlGZ.DataBind();
                }
            }
        }

        protected void ddlGR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoles.SelectedValue == Constantes.RolGerenteZona.ToString())
            {
                BlConfiguracion blConfig = new BlConfiguracion();
                DataTable dtGZ = blConfig.SeleccionarGZonaPorEvaluar(Convert.ToInt32(ddlGR.SelectedValue), ddlPaises.SelectedValue);
                ddlGZ.DataSource = dtGZ;
                ddlGZ.DataTextField = "vchNombreCompleto";
                ddlGZ.DataValueField = "documentoIdentidad";
                ddlGZ.DataBind();
                ddlGZ.Items.Insert(0, new ListItem("[Seleccionar]", "0"));
            }
        }
    }
}
