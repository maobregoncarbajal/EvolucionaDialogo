using System;
using Evoluciona.Dialogo.Helpers;
using Evoluciona.Dialogo.Web.Helpers;
using Quartz;

namespace Evoluciona.Dialogo.Web
{
    public class TareaCargarDirectorio : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            const string pais = Constantes.CeroCero; //"00" = Todos
            var region = String.Empty;
            var zona = String.Empty;
            var cargo = String.Empty;
            var periodo = String.Empty;
            var estadoCargo = String.Empty;

            Utils.ConsumirWsObtenerClientesDirectorio(pais, region, zona, cargo, periodo, estadoCargo);
        }
    }
}