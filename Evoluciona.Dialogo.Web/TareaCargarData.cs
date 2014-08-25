using System.Web;
using Quartz;
namespace Evoluciona.Dialogo.Web
{
    public class TareaCargarData : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            HttpContext.Current.Response.Redirect("~/Admin/Tareas/TareaCargarData.aspx");
        }
    }
}
