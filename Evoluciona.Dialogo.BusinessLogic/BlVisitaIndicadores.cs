
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlVisitaIndicadores
    {

        private static readonly DaVisitaIndicadores DaVisitaIndicadores = new DaVisitaIndicadores();

        public DataSet GetVariablesBase(BeResumenVisita objResumenVisitaBe)
        {
            return DaVisitaIndicadores.GetVariablesBase(objResumenVisitaBe);
        }

        public DataSet GetVariablesAdicionales(BeResumenVisita objResumenVisitaBe)
        {
            return DaVisitaIndicadores.GetVariablesAdicionales(objResumenVisitaBe);
        }

        public DataSet GetEstadosporPeriodo(BeResumenVisita objResumenVisitaBe)
        {
            return DaVisitaIndicadores.GetEstadosporPeriodo(objResumenVisitaBe);
        }

        public DataSet GetCriticas(BeResumenVisita objResumenVisitaBe)
        {
            return DaVisitaIndicadores.GetCriticas(objResumenVisitaBe);
        }

        public DataSet ObtenerIndicadoresVisita(int idProceso)
        {
            return DaVisitaIndicadores.ObtenerIndicadoresVisita(idProceso);
        }

        public void InsertarIndicadorVisita(BeVisitaIndicador indicador)
        {
            DaVisitaIndicadores.InsertarIndicadorVisita(indicador);
        }

        public void EliminarIndicadoresVisita(int idProceso)
        {
            DaVisitaIndicadores.EliminarIndicadoresVisita(idProceso);
        }
    }
}
