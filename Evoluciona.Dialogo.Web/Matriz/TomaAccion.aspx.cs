
namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class TomaAccion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (Page.IsPostBack) return;
        }

        #region Metodos

        private void CargarVariables()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario == null) return;

            lblCodigoEvaluador.Text = objUsuario.codigoUsuario;
            lbIdlRolEvaluador.Text = objUsuario.idRol.ToString();
            lblPaisEvaluador.Text = objUsuario.prefijoIsoPais;
            lblPeriodoAcuerdo.Text = objUsuario.periodoEvaluacion;
            lblNombreEvaluador.Text = objUsuario.nombreUsuario;
        }

        #endregion Metodos
    }
}
