
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Default : Page
    {
        #region Clase Interna

        public class ResponseData
        {
            public bool Success;
            public string Message;
            public object Data;
        }

        #endregion Clase Interna

        #region Variables

        public string View;
        public int RolEvaluador;
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
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (objUsuario == null) return;

            RolEvaluador = objUsuario.codigoRol;
            CargarVisualizador();
        }

        #endregion Eventos

        #region Metodos

        private void CargarVisualizador()
        {
            View = Request.QueryString.Get("view");
            View = string.IsNullOrEmpty(View) ? "month" : View;

            switch (View)
            {
                case "month":
                    visualizador.VistaMes = "ui-state-active";
                    filtros.CambiarMeses = true;
                    break;
                case "agendaWeek":
                    visualizador.VistaSemana = "ui-state-active";
                    filtros.CambiarMeses = false;
                    break;
                case "agendaDay":
                    visualizador.VistaDia = "ui-state-active";
                    filtros.CambiarMeses = false;
                    break;
            }
        }

        #endregion Metodos
    }
}