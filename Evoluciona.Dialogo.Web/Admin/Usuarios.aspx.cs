
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Usuarios : Page
    {
        #region Variables

        private BeAdmin objAdmin;
        private readonly BlAdmin adminBL = new BlAdmin();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (Page.IsPostBack)
                return;

            CargarPaises();
            CargarAdministradores();
        }

        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAdministradores();
        }

        protected void gvUsuariosPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            CargarAdministradores();
        }

        protected void gvUsuariosRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idAdmin = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "cmd_editar":
                    CargarAdminsitrador(idAdmin);
                    break;
                case "cmd_eliminar":
                    EliminarAdministrador(idAdmin);
                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarAdministrador();
        }

        #endregion

        #region Metodos

        private void CargarPaises()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = paisBL.ObtenerPaises();
            BePais pais = new BePais();
            pais.NombrePais = "[TODOS]";
            pais.prefijoIsoPais = "0";
            paises.Insert(0, pais);

            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";
            cboPaises.DataSource = paises;
            cboPaises.DataBind();

            paises = paisBL.ObtenerPaises();

            cboPais.DataTextField = "NombrePais";
            cboPais.DataValueField = "prefijoIsoPais";
            cboPais.DataSource = paises;
            cboPais.DataBind();
        }

        private void CargarAdministradores()
        {
            string codigoPais = cboPaises.SelectedValue;
            List<BeAdmin> administradores = adminBL.ListarAdministradores(codigoPais, Constantes.RolAdminPais);

            gvUsuarios.DataSource = administradores;
            gvUsuarios.DataBind();

            lblMensajeError.Text = string.Empty;
        }

        private void CargarAdminsitrador(int idAdmin)
        {
            BeAdmin administrador = adminBL.ObtenerAdministrador(idAdmin);

            hidIDAdmin.Value = administrador.IDAdmin.ToString();
            txtCodigo.Text = administrador.CodigoAdmin;
            txtNombre.Text = administrador.NombreCompleto;
            txtClave.Text = administrador.ClaveAdmin;
            cboPais.SelectedValue = administrador.CodigoPais;
            cboEstado.SelectedValue = administrador.Estado.ToString();
            cboAdmin.SelectedValue = administrador.Admin.ToString();
            txtCodigo.Focus();


            ClientScript.RegisterStartupScript(GetType(), "MostrarPopup", "jQuery(document).ready(function() { $('#adminsitradorPopup').dialog('open'); });", true);
        }

        private void EliminarAdministrador(int idAdmin)
        {
            adminBL.EliminarAdministrador(idAdmin);
            CargarAdministradores();
        }

        private void GuardarAdministrador()
        {
            try
            {

                int idAdmin = Convert.ToInt32(hidIDAdmin.Value);

                if (idAdmin == 0)
                {
                    BeAdmin administrador = new BeAdmin();

                    administrador.CodigoAdmin = txtCodigo.Text.Trim();
                    administrador.NombreCompleto = txtNombre.Text.Trim();
                    administrador.ClaveAdmin = txtClave.Text;
                    administrador.CodigoPais = cboPais.SelectedValue;
                    administrador.Estado = Convert.ToBoolean(cboEstado.SelectedValue);

                    if (cboAdmin.SelectedIndex == 0)
                    {
                        administrador.TipoAdmin = Constantes.RolAdminEvaluciona;
                        administrador.Admin = Convert.ToBoolean(cboAdmin.SelectedValue);
                    }
                    else
                    {
                        administrador.TipoAdmin = Constantes.RolAdminPais;
                        administrador.Admin = Convert.ToBoolean(cboAdmin.SelectedValue);
                    }

                    administrador.UsuarioCrea = objAdmin.IDAdmin;
                    administrador.UsuarioModi = objAdmin.IDAdmin;

                    adminBL.AgregarAdminsitrador(administrador);

                    lblMensajeError.Text = "Se registró Satisfactoriamente el Administrador.";
                }
                else
                {
                    BeAdmin administrador = adminBL.ObtenerAdministrador(idAdmin);

                    administrador.CodigoAdmin = txtCodigo.Text.Trim();
                    administrador.NombreCompleto = txtNombre.Text.Trim();
                    administrador.ClaveAdmin = txtClave.Text;
                    administrador.CodigoPais = cboPais.SelectedValue;
                    administrador.Estado = Convert.ToBoolean(cboEstado.SelectedValue);

                    if (cboAdmin.SelectedIndex == 0)
                    {
                        administrador.TipoAdmin = Constantes.RolAdminEvaluciona;
                        administrador.Admin = Convert.ToBoolean(cboAdmin.SelectedValue);

                    }
                    else
                    {
                        administrador.TipoAdmin = Constantes.RolAdminPais;
                        administrador.Admin = Convert.ToBoolean(cboAdmin.SelectedValue);
                    }

                    administrador.FechaModi = DateTime.Now;
                    administrador.UsuarioModi = objAdmin.IDAdmin;

                    adminBL.ActualizarAdministrador(administrador);

                    lblMensajeError.Text = "Se actualizó Satisfactoriamente el Administrador.";
                }


                CargarAdministradores();
            }
            catch (Exception)
            {
                lblMensajeError.Text = "Ocurrio un error al intentar guardar los Datos.";
            }
        }

        #endregion
    }
}
