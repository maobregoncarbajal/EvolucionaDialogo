
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlZonaAlternativa
    {

        private static readonly DaZonaAlternativa DaZonaAlternativa = new DaZonaAlternativa();

        public bool IngresarZonaAlternativa(string connstring, BeZonaAlternativa beZonaAlternativa, BeUsuario beUsuario)
        {
            return DaZonaAlternativa.IngresarZonaAlternativa(connstring, beZonaAlternativa, beUsuario);
        }

        //Lisar zona alternativa grabada
        public DataTable ObtenerZonaAlternativaGrabada(string connstring, BeResumenProceso objResumen, BeRetroalimentacion beRetroalimentacion)
        {
            return DaZonaAlternativa.ObtenerZonaAlternativaGrabada(connstring, objResumen, beRetroalimentacion);
        }

        public bool IngresarZonaAlternativaVisita(string connstring, BeZonaAlternativa beZonaAlternativa, BeUsuario beUsuario)
        {
            return DaZonaAlternativa.IngresarZonaAlternativaVisita(connstring, beZonaAlternativa, beUsuario);
        }
    }
}
