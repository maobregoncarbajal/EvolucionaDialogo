
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using System.Data;

    public class BlMedicionCompetencia
    {
        private static readonly DaMedicionCompetencia DaMedicionCompetencia = new DaMedicionCompetencia();

        public DataTable ObtenerMedicionCompetencia(string connstring, int intIdProceso)
        {
            return DaMedicionCompetencia.ObtenerMedicionCompetencia(connstring, intIdProceso);
        }
    }
}
