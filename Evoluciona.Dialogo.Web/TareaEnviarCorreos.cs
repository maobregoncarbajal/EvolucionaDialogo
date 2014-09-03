
using Evoluciona.Dialogo.BusinessLogic.Tareas;
using Quartz;

namespace Evoluciona.Dialogo.Web
{
    public class TareaEnviarCorreos : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var blTareaEnviarCorreos = new BLTareaEnviarCorreos();
            blTareaEnviarCorreos.IniciarProcesoEnviarCorreos();
        }
    }
}
