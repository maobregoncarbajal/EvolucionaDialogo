
namespace Evoluciona.Dialogo.Web.Ajax
{
    using BusinessEntity;
    using BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.UI;

    public partial class AjaxPlanAccion : Page
    {
        protected string mensaje, error, nombreUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Visible = false;
            InsertarDatos();
        }

        private void InsertarDatos()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            BlPlanAccion daProceso = new BlPlanAccion();
            if (Session["arrPlanAccion"] != null)
            {
                List<BePlanAccion> arregloEntidades = new List<BePlanAccion>();

                arregloEntidades = (List<BePlanAccion>)Session["arrPlanAccion"];

                foreach (BePlanAccion var in arregloEntidades)
                {
                    if (var.idPlanAcccion > 0)
                    {
                        // actualizar
                        bool ActualizarPlanAccion = daProceso.ActualizarPlanAccion(var);
                    }
                    else
                    {
                        bool GrabarPlanAccion = daProceso.IngresarPlanAccion(var);
                    }

                }
                Session["arrPlanAccion"] = null;
            }

        }
    }
}
