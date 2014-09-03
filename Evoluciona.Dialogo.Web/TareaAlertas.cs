
using Evoluciona.Dialogo.BusinessLogic.Tareas;
using Quartz;

namespace Evoluciona.Dialogo.Web
{
    public class TareaAlertas : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var blTareaAlertas = new BLTareaAlertas();
            blTareaAlertas.IniciarProcesoNotificaciones();
        }
    }
}
