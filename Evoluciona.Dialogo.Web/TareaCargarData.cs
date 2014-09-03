using Evoluciona.Dialogo.BusinessLogic.Tareas;
using Quartz;
namespace Evoluciona.Dialogo.Web
{
    public class TareaCargarData : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var blTareaCargaData = new BLTareaCargaData();
            blTareaCargaData.IniciarProcesoCargaData();
        }
    }
}
