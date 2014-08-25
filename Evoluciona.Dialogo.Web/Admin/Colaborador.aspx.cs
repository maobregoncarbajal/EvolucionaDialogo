
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using WsPlanDesarrollo;

    public partial class Colaborador : Page
    {
        #region Propiedades

        private BeAdmin objAdmin;

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            hlColaborador.NavigateUrl = string.Format("javascript:CargarVista('{0}Admin/Adam.aspx?avanzada=1');", Utils.AbsoluteWebRoot);
            if (IsPostBack) return;
            CargarPaises();
            CargarColaboradores();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CargarColaboradores();
        }

        #endregion Eventos

        #region Métodos

        private void CargarPaises()
        {
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
            List<BePais> paises = new List<BePais>();
            BlPais paisBL = new BlPais();
            paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";
            cboPaises.DataSource = paises;
            cboPaises.DataBind();
        }

        private void CargarColaboradores()
        {
            WsInterfaceFFVVSoapClient ws = new WsInterfaceFFVVSoapClient();
            BlGerenteRegion blgRegion = new BlGerenteRegion();

            var colaboradores = blgRegion.ObtenerColaborador(txtNombres.Text.Trim(), cboPaises.SelectedValue, Convert.ToInt32(cboCargo.SelectedValue), txtDoc.Text);
            gvColaborador.DataSource = colaboradores;
            gvColaborador.DataBind();
        }

        #endregion Métodos

        protected void gvColaborador_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvColaborador.PageIndex = e.NewPageIndex;
            CargarColaboradores();
        }

        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}