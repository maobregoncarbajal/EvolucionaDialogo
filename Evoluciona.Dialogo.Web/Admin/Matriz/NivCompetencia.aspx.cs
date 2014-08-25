
namespace Evoluciona.Dialogo.Web.Admin.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class NivCompetencia : Page
    {
        private BeAdmin objAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            menuReporte.NivCompetencia = "ui-state-active";
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (!IsPostBack)
            {
                lblCodUsuario.Text = objAdmin.CodigoAdmin;
                CargarPaises();
                CargarAnhos();
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

        private void CargarAnhos()
        {
            List<string> anhos = new List<string>();
            int anhito = DateTime.Now.Year;

            for (int x = -2; x < 3; x++)
            {
                anhos.Add(Convert.ToString(anhito + x));
            }

            cboAnho.DataSource = anhos;
            cboAnho.DataBind();
            cboAnho.SelectedValue = Convert.ToString(anhito);
        }
    }
}
