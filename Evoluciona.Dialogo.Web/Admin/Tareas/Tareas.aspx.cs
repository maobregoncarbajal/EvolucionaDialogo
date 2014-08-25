using System;

namespace Evoluciona.Dialogo.Web.Admin.Tareas
{
    public partial class Tareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bTareas_Click(object sender, EventArgs e)
        {
            switch (ddlTareas.SelectedValue)
            {
                case "0":
                    Response.Redirect("~/Admin/Tareas/Tareas.aspx");
                    break;
                case "1":
                    Response.Redirect("~/Admin/Tareas/TareaAlertas.aspx");
                    break;
                case "2":
                    Response.Redirect("~/Admin/Tareas/TareaCargarData.aspx");
                    break;
                case "3":
                    Response.Redirect("~/Admin/Tareas/TareaCargarDirectorio.aspx");
                    break;
                case "4":
                    Response.Redirect("~/Admin/Tareas/TareaCompetencias.aspx");
                    break;
                case "5":
                    Response.Redirect("~/Admin/Tareas/TareaEnviarCorreos.aspx");
                    break;
            }
        }
    }
}