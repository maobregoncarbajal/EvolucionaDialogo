
using System.Web;
using Quartz;

namespace Evoluciona.Dialogo.Web
{


    public class TareaAlertas : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            HttpContext.Current.Response.Redirect("~/Admin/Tareas/TareaAlertas.aspx");
        }
    }
}
