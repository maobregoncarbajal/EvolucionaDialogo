
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Altas : Page
    {
        #region Variables

        BlGerenteRegion objBlGerenteRegion = new BlGerenteRegion();

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
            if (IsPostBack) return;

            CargarPaises();
            CargarEvaluadores();
            CargarAltas();
            CargarDocIdentidad();
        }

        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEvaluadores();
            CargarDocIdentidad();
            CargarAltas();
        }

        protected void cboRolEvaluador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEvaluadores();
            CargarDocIdentidad();
            CargarAltas();
        }

        protected void cboEvaluador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDocIdentidad();
            CargarAltas();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var evaluador = cboEvaluador.SelectedValue;
            if (cboPaises.SelectedValue != "0" && evaluador != "0")
            {
                if (objBlGerenteRegion.UpdateMaes(cboPaises.SelectedValue, Convert.ToInt32(cboRolEvaluador.SelectedValue),
                                              txtDocumento.Text, evaluador))
                {
                    CargarEvaluadores();
                    txtDocumento.Text = "";
                    CargarAltas();
                }
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarPaises()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = paisBL.ObtenerPaises();

            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";
            cboPaises.DataSource = paises;
            cboPaises.DataBind();
            cboPaises.Items.Insert(0, new ListItem("BELCORP", "0"));
        }

        private void CargarEvaluadores()
        {
            int rolEvaluador = Convert.ToInt32(cboRolEvaluador.SelectedValue);
            BlConfiguracion blConfig = new BlConfiguracion();

            cboEvaluador.DataSource = blConfig.ObtenerEvaluadores(cboPaises.SelectedValue, rolEvaluador);

            cboEvaluador.DataTextField = "vchNombreCompleto";
            cboEvaluador.DataValueField = "documentoIdentidad";
            cboEvaluador.DataBind();
            cboEvaluador.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        private void CargarAltas()
        {
            //var gerentes = objBlGerenteRegion.ObtenerGR(2, true);
            var gerentes = objBlGerenteRegion.ObtenerEvaluadores(0, true, cboPaises.SelectedValue,
                                                             Convert.ToInt32(cboRolEvaluador.SelectedValue),
                                                             cboEvaluador.SelectedValue);
            gvAltas.DataSource = gerentes;
            gvAltas.DataBind();
        }

        private void CargarDocIdentidad()
        {
            txtDocumento.Text = "";
            if (cboPaises.SelectedValue != "0" && cboEvaluador.SelectedValue != "0")
            {
                int rolEvaluador = Convert.ToInt32(cboRolEvaluador.SelectedValue);
                BlConfiguracion blConfig = new BlConfiguracion();
                txtDocumento.Text = blConfig.ObtenerDocIdentidadMae(cboPaises.SelectedValue, rolEvaluador,
                                                                    cboEvaluador.SelectedValue);
            }
        }

        #endregion Metodos

        protected void gvAltas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAltas.PageIndex = e.NewPageIndex;
            CargarAltas();
        }
    }
}