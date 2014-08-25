
namespace Evoluciona.Dialogo.Web.Admin.Matriz
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Calibracion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            menuReporte.Calibracion = "ui-state-active";
            if (!IsPostBack)
            {
                BeAdmin objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
                lblTipoAdmin.Text = objAdmin.TipoAdmin;
                lblCodPaisUsuario.Text = objAdmin.CodigoPais;
                lblCodigoUsuario.Text = objAdmin.CodigoAdmin;
            }
        }
    }
}
