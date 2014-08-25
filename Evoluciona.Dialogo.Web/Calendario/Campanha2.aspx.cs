
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Campanha2 : Page
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
            visualizador.VistaCampanha = "ui-state-active";

            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
        }

        #endregion Eventos
    }
}