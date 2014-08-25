
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Admin : MasterPage
    {
        private BeAdmin _objAdmin;

        protected void Page_Init(object sender, EventArgs e)
        {
            _objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
            if (_objAdmin == null)
            {
                Response.Redirect("~/error.aspx?mensaje=sesion");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CargarAdministrador();
        }

        private void CargarAdministrador()
        {
            lblUserLogeado.Text = _objAdmin.NombreCompleto;
            lblRolLogueado.Text = _objAdmin.DescripcionAdmin;
            lblDescripcionAdmin.Text = _objAdmin.DescripcionAdmin;
            imgImagenPais.ImageUrl = "~/Images/" + _objAdmin.ImagenPais;
            lblNombrePais.Text = _objAdmin.NombrePais;


            switch (_objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    imgImagenPais.Visible = false;
                    lblNombrePais.Visible = false;
                    menuCoorporativo.Visible = true;
                    menuPais.Attributes.Add("style", "display:none");
                    menuEvaluciona.Attributes.Add("style", "display:none");

                    break;
                case Constantes.RolAdminPais:
                    lblDescripcionAdmin.Visible = false;
                    //mnuOpciones.Items.RemoveAt(5);
                    //mnuOpciones.Items.RemoveAt(3);
                    menuPais.Visible = true;
                    menuCoorporativo.Attributes.Add("style", "display:none");
                    menuEvaluciona.Attributes.Add("style", "display:none");

                    break;
                case Constantes.RolAdminEvaluciona:
                    lblDescripcionAdmin.Visible = false;
                    //mnuOpciones.Items.RemoveAt(6);
                    //mnuOpciones.Items.RemoveAt(5);
                    //mnuOpciones.Items.RemoveAt(4);
                    //mnuOpciones.Items[3].ChildItems.RemoveAt(4);
                    //mnuOpciones.Items[3].ChildItems.RemoveAt(0);
                    //mnuOpciones.Items.RemoveAt(2);
                    menuEvaluciona.Visible = true;
                    menuCoorporativo.Attributes.Add("style", "display:none");
                    menuPais.Attributes.Add("style", "display:none");
                    break;
            }

        }
    }
}
