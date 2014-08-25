
namespace Evoluciona.Dialogo.Web.Admin.Controls
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

        public string Reporte5
        {
            get { return reporte5.Attributes["class"]; }
            set { reporte5.Attributes["class"] += value; }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            BeAdmin _objAdmin = new BeAdmin();
            _objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];


            switch (_objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    reporte1.Visible = true;
                    reporte2.Visible = true;
                    reporte3.Visible = true;
                    reporte4.Visible = true;
                    reporte5.Visible = true;
                    break;
                case Constantes.RolAdminPais:
                    reporte1.Visible = true;
                    reporte2.Visible = true;
                    reporte3.Visible = true;
                    reporte4.Visible = false;
                    reporte5.Visible = false;
                    break;
                case Constantes.RolAdminEvaluciona:
                    reporte1.Visible = true;
                    reporte2.Visible = false;
                    reporte3.Visible = false;
                    reporte4.Visible = false;
                    reporte5.Visible = false;
                    break;
            }

            if (IsPostBack)
                return;
        }

        #endregion Eventos

        #region Metodos

        #endregion Metodos
    }
}