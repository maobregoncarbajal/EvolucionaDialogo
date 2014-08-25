
namespace Evoluciona.Dialogo.Web.Ajax
{
    using BusinessEntity;
    using BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.UI;

    public partial class AjaxPlanAnual : Page
    {
        protected string mensaje, error, nombreUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            InsertarDatos();
        }

        private void InsertarDatos()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            BlPlanAnual daProceso = new BlPlanAnual();
            if (Session["arrPlanAnual"] != null)
            {
                List<BePlanAnual> arregloPlanAnual = new List<BePlanAnual>();

                arregloPlanAnual = (List<BePlanAnual>)Session["arrPlanAnual"];

                foreach (BePlanAnual var in arregloPlanAnual)
                {
                    if (var.idPlanAnual > 0)
                    {
                        // actualizar
                        bool ActualizarPlanAnual = daProceso.ActualizarPlanAnual(connstring, var);
                    }
                    else
                    {
                        bool GrabarPlanAnual = daProceso.IngresarPlanAnual(connstring, var);
                    }

                }
                Session["arrPlanAnual"] = null;
            }

        }
    }
}
