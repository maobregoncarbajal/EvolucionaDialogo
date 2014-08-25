
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using Helpers;

    public class BlEvaluadorProgreso
    {
        private static readonly DaEvaluadorProgreso DaEvaluadorProgreso = new DaEvaluadorProgreso();

        public int CalcularAvanze(int idProceso, TipoPantalla tipo)
        {
            return DaEvaluadorProgreso.CalcularAvanze(idProceso, tipo);
        }

        public int CalcularAvanze_Eval(int idProceso, TipoPantalla tipo)
        {
            return DaEvaluadorProgreso.CalcularAvanze_Eval(idProceso, tipo);
        }
    }
}