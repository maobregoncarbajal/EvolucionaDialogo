
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Web.Security;
    using System.Web.UI;

    public partial class Login : Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (IsPostBack) return;

            txtUsuario.Focus();
        }

        protected void cboTipoAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoAdmin.SelectedValue == Constantes.RolAdminCoorporativo)
            {
                cboPais.SelectedIndex = 0;
                cboPais.Enabled = false;
            }
            else
            {
                cboPais.Enabled = true;
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                BlAdmin blAdmin = new BlAdmin();
                BeAdmin objAdmin = blAdmin.VerificarAdmin(txtUsuario.Text, txtContrasenia.Text, cboTipoAdmin.SelectedValue, cboPais.SelectedValue);
                
                if (objAdmin != null)
                {
                    Session[Constantes.ObjUsuarioLogeado] = objAdmin;
                    FormsAuthentication.SetAuthCookie(objAdmin.CodigoAdmin, false);
                    Response.Redirect("~/Admin/Default.aspx");
                }
                else
                {
                    lblMensaje.Text = "No se encontro un Administrador con dichas credenciales.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void brnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/error.aspx?mensaje=sesion");
        }

        #endregion

        #region Metodos


        #endregion
    }
}
