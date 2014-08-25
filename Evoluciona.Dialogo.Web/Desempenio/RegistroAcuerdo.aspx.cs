using System.Globalization;
using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.Helpers;
using System;
using System.Web.UI;

namespace Evoluciona.Dialogo.Web.Desempenio
{
    public partial class RegistroAcuerdo : Page
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