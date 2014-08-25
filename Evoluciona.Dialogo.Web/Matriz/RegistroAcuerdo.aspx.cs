
using System.Globalization;

namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;

    public partial class RegistroAcuerdo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (Page.IsPostBack) return;
        }

        private void CargarVariables()
        {
            var objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario == null) return;

            var codigoUsuario = objUsuario.codigoUsuario;
            lblCodigoEvaluador.Text = codigoUsuario;
            lbIdlRolEvaluador.Text = objUsuario.idRol.ToString(CultureInfo.InvariantCulture);
            lblPaisEvaluador.Text = objUsuario.prefijoIsoPais;
            lblPeriodoAcuerdo.Text = objUsuario.periodoEvaluacion;
            lblNombreEvaluador.Text = objUsuario.nombreUsuario;
        }
    }
}
