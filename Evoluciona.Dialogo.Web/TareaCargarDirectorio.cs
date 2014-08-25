using System.Web;
using Quartz;
namespace Evoluciona.Dialogo.Web
{
    public class TareaCargarDirectorio : IJob
    {

        public void Execute(JobExecutionContext context)
        {
            HttpContext.Current.Response.Redirect("~/Admin/Tareas/TareaCargarDirectorio.aspx");
        }
    }
}