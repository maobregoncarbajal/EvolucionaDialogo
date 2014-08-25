
namespace Evoluciona.Dialogo.Web.Ajax
{
    using BusinessEntity;
    using BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.UI;

    public partial class AjaxRetroalimentacion : Page
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
            BlRetroalimentacion daProceso = new BlRetroalimentacion();
            if (Session["arrRetroalimentacion"] != null)
            {
                List<BeRetroalimentacion> arregloRetroalimentacion = new List<BeRetroalimentacion>();

                arregloRetroalimentacion = (List<BeRetroalimentacion>)Session["arrRetroalimentacion"];

                foreach (BeRetroalimentacion var in arregloRetroalimentacion)
                {
                    if (var.idRetroalimentacion > 0)
                    {
                        // actualizar
                       // bool ActualizarPlanAccion = daProceso.ActualizarPlanAccion(connstring, var);
                    }
                    else
                    {
                        bool GrabarPlanAccion = daProceso.IngresarRetroalimentacion(connstring, var);
                    }

                }
                Session["arrRetroalimentacion"] = null;
            }

        }
    }
}
