
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlEvoluciona
    {
        private static readonly DaEvoluciona DaEvoluciona = new DaEvoluciona();

        // Ingresar Visita Evoluciona
        public bool IngresarVisitaEvoluciona(string connstring, BeEvoluciona beEvoluciona)
        {
            return DaEvoluciona.IngresarVisitaEvoluciona(connstring, beEvoluciona);
        }

        // Obtener  Evalucion Grabadas
        public DataTable ObtenerEvaluacionGrabadas(string connstring, int idVisita, int codigoRol)
        {
            return DaEvoluciona.ObtenerEvaluacionGrabadas(connstring, idVisita, codigoRol);
        }
    }
}
