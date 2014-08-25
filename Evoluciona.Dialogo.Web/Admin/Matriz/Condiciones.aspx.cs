
namespace Evoluciona.Dialogo.Web.Admin.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class Condiciones : Page
    {
        private BeAdmin objAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            menuReporte.Condiciones = "ui-state-active";
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            lblCodUsuario.Text=objAdmin.CodigoAdmin;

            if (!IsPostBack)
            {
                CargarPaises();
            }
        }

        private void CargarPaises()
        {
            BlMatrizAdmin matrizAdminBL = new BlMatrizAdmin();
            List<BeComun> entidades = new List<BeComun>();

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    entidades = matrizAdminBL.ObtenerPaises();
                    break;
                case Constantes.RolAdminPais:
                    entidades.Add(matrizAdminBL.ObtenerPais(objAdmin.CodigoPais));
                    break;
                case Constantes.RolAdminEvaluciona:
                    entidades.Add(matrizAdminBL.ObtenerPais(objAdmin.CodigoPais));
                    break;
            }

            cboPaises.DataTextField = "Descripcion";
            cboPaises.DataValueField = "Codigo";
            cboPaises.DataSource = entidades;
            cboPaises.DataBind();
        }
    }
}
