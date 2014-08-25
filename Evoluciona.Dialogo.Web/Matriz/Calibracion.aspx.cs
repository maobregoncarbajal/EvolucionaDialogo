
namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Calibracion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (Page.IsPostBack) return;
        }

        private void CargarVariables()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario != null)
            {
                string codigoUsuario = objUsuario.codigoUsuario;
                lblCodigoEvaluador.Text = codigoUsuario;
                lbIdlRolEvaluador.Text = objUsuario.idRol.ToString();
                lblPaisEvaluador.Text = objUsuario.prefijoIsoPais;
                lblPeriodoAcuerdo.Text = objUsuario.periodoEvaluacion;
                lblNombreEvaluador.Text = objUsuario.nombreUsuario;
            }
        }
    }     
}
