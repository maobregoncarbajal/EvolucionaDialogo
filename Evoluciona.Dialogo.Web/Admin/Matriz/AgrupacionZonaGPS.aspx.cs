
namespace Evoluciona.Dialogo.Web.Admin.Matriz
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class AgrupacionZonaGPS : Page
    {
        #region Variables

        private BeAdmin objAdmin;

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            menuReporte.Agrupacion = "ui-state-active";
            CargarVariables();
        }

        #region Metodos

        private void CargarVariables()
        {

            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (objAdmin != null)
            {
                string tipo = objAdmin.TipoAdmin;
                lblTipo.Text = tipo;

                string pais = objAdmin.CodigoPais;
                lblPais.Text = pais;

                string usuario = objAdmin.IDAdmin.ToString();
                lblUsuario.Text = usuario;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "crearCombos", "crearCombos();", true);
        }

        #endregion Metodos
    }
}
