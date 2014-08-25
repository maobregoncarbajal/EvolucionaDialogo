using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.Helpers;
using System;

namespace Evoluciona.Dialogo.Web.Admin
{
    public partial class CronogramaPdM : System.Web.UI.Page
    {

        #region Variables
        private BeAdmin _objAdmin;
        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
        }

        #region Metodos

        private void CargarVariables()
        {
            _objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (_objAdmin == null) return;
            var pais = _objAdmin.CodigoPais;
            lblPais.Text = pais;

            var usuario = _objAdmin.CodigoAdmin;
            lblUsuario.Text = usuario;
        }

        #endregion Metodos


    }
}