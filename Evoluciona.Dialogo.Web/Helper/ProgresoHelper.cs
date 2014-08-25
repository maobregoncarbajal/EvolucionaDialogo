
namespace Evoluciona.Dialogo.Web.Helper
{
    using BusinessLogic;
    using Evoluciona.Dialogo.Helpers;

    public class ProgresoHelper
    {
        public static int CalcularAvanze(int idProceso, TipoPantalla tipo)
        {
            int porcentajeAvanze = 0;
            BlEvaluadorProgreso evaluador = new BlEvaluadorProgreso();

            porcentajeAvanze = evaluador.CalcularAvanze(idProceso, tipo);

            return porcentajeAvanze;
        }

        public static int CalcularAvanze_Eval(int idProceso, TipoPantalla tipo)
        {
            int porcentajeAvanze = 0;
            BlEvaluadorProgreso evaluador = new BlEvaluadorProgreso();

            porcentajeAvanze = evaluador.CalcularAvanze_Eval(idProceso, tipo);

            return porcentajeAvanze;
        }
    }
}