
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Evaluado : Page
    {
        #region Variables

        private BeUsuario objUsuario;

        #endregion Variables

        #region Propiedades

        public string PrefijoIsoPais
        {
            get { return objUsuario == null ? string.Empty : objUsuario.prefijoIsoPais; }
        }

        public int CodigoRol
        {
            get { return objUsuario == null ? 0 : objUsuario.codigoRol; }
        }

        public string CodigoUsuario
        {
            get { return objUsuario == null ? string.Empty : objUsuario.codigoUsuario; }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            visualizador.VistaEvaluado = "ui-state-active";

            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (objUsuario == null) return;
            CargarRolEvaluado();
        }

        #endregion Eventos

        #region Metodos

        private void CargarRolEvaluado()
        {
            switch (objUsuario.codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    litEvaluado.Text = "Gerente de Región";
                    break;
                case Constantes.RolGerenteRegion:
                    litEvaluado.Text = "Gerente de Zona";
                    break;
            }
        }

        #endregion Metodos
    }
}