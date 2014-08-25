
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class MenuReportes : UserControl
    {
        #region Variables

        #endregion Variables

        #region Propiedades

        public string Reporte1
        {
            get { return reporte1.Attributes["class"]; }
            set { reporte1.Attributes["class"] += value; }
        }

        public string Reporte2
        {
            get { return reporte2.Attributes["class"]; }
            set { reporte2.Attributes["class"] += value; }
        }

        public string Reporte3
        {
            get { return reporte3.Attributes["class"]; }
            set { reporte3.Attributes["class"] += value; }
        }

        public string Reporte4
        {
            get { return reporte4.Attributes["class"]; }
            set { reporte4.Attributes["class"] += value; }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            var objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario.codigoRol != 4)
                this.reporte1.Visible = false;

            if (objUsuario.codigoRol != 4 && objUsuario.codigoRol != 5)
            {
                this.reporte2.Visible = false;
                this.reporte3.Visible = false;
                this.reporte4.Visible = false;
            }

            if (IsPostBack)
                return;
        }

        #endregion Eventos

        #region Metodos

        #endregion Metodos
    }
}