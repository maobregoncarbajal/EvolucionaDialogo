
using System.Web;
using Quartz;

namespace Evoluciona.Dialogo.Web
{
    public class TareaCompetencias : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            HttpContext.Current.Response.Redirect("~/Admin/Tareas/TareaCompetencias.aspx");
        }
        
    }
}