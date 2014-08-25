
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Visualizador : UserControl
    {
        #region Variables

        private BeUsuario ObjUsuario;

        #endregion

        #region Propiedades

        public string VistaDia
        {
            get { return vistaDia.Attributes["class"]; }
            set { vistaDia.Attributes["class"] += value; }
        }

        public string VistaSemana
        {
            get { return vistaSemana.Attributes["class"]; }
            set { vistaSemana.Attributes["class"] += value; }
        }

        public string VistaMes
        {
            get { return vistaMes.Attributes["class"]; }
            set { vistaMes.Attributes["class"] += value; }
        }

        public string VistaCampanha
        {
            get { return vistaCampanha.Attributes["class"]; }
            set { vistaCampanha.Attributes["class"] += value; }
        }

        public string VistaAgenda
        {
            get { return vistaAgenda.Attributes["class"]; }
            set { vistaAgenda.Attributes["class"] += value; }
        }

        public string VistaEvaluado
        {
            get { return vistaEvaluado.Attributes["class"]; }
            set { vistaEvaluado.Attributes["class"] += value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarRolEvaluado();

            if(IsPostBack)
                return;
        }

        #endregion

        #region Metodos

        private void MostrarRolEvaluado()
        {
            ObjUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (ObjUsuario == null) return;   

            switch (ObjUsuario.codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    litEvaluado.Text = "Gerente de Región";
                    break;
                case Constantes.RolGerenteRegion:
                    litEvaluado.Text = "Gerente de Zona";
                    break;
                case Constantes.RolGerenteZona:
                    vistaEvaluado.Visible = false;
                    break;
                default:
                    vistaEvaluado.Visible = false;
                    break;
            }
        }

        #endregion
    }
}