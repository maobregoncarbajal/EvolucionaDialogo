
namespace Evoluciona.Dialogo.Web.Admin.Matriz
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class CondicionesE : Page
    {
        private BeAdmin objAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            lblCodUsuario.Text = objAdmin.CodigoAdmin;
            cargarLabel();
        }

        private void cargarLabel()
        {
            txtCodTipoCondicion.Text = Request["codTipoCondicion"];
            lblTipoCondicion.Text = Request["desTipoCondicion"];
            txtCodPais.Text = Request["codPais"];
            lblPais.Text = Request["desPais"];
            txtCodNumCondicion.Text = Request["codNumCondicion"];
            lblNumeroCondicion.Text = Request["desNumCondicion"];
        }
    }
}
