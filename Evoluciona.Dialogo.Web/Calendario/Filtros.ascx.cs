
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Filtros : UserControl
    {
        #region Variables

        public bool cambiarMeses;
        private BeUsuario objUsuario;
        private bool mostrarFechasVisitas;

        #endregion Variables

        #region Propiedades

        public bool MostrarFechaVisitas
        {
            get { return mostrarFechasVisitas; }
            set { mostrarFechasVisitas = value; }
        }

        public bool CambiarMeses
        {
            get { return cambiarMeses; }
            set { cambiarMeses = value; }
        }

        public bool MostrarFiltros
        {
            get { return panFiltros.Visible; }
            set { panFiltros.Visible = value; }
        }

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
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (objUsuario == null) return;
        }

        #endregion Eventos

        #region Metodos

        #endregion Metodos
    }
}