
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlDescripcionZonaAlternativa
    {
        private static readonly DaDescripcionZonaAlternativa DaDescripcionZonaAlternativa = new DaDescripcionZonaAlternativa();

        public DataTable ObtenerZonaAlternativa(string connstring)
        {
            return DaDescripcionZonaAlternativa.ObtenerZonaAlternativa(connstring);
        }

        /// <summary>
        /// Retorna una lista con las alternativas por Rol
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="codigoRol"></param>
        /// <returns></returns>
        public List<BeZonaAlternativa> ObtenerZonaAlternativaPorRol(string connstring, int codigoRol)
        {
            return DaDescripcionZonaAlternativa.ObtenerZonaAlternativaPorRol(connstring, codigoRol);
        }

        /// <summary>
        /// Retorna una lista con las alternativas que han sido guardadas
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="idProceso"></param>
        /// <returns></returns>
        public List<BeZonaAlternativa> ObtenerZonaAlternativaProcesada(string connstring, int idProceso)
        {
            return DaDescripcionZonaAlternativa.ObtenerZonaAlternativaProcesada(connstring, idProceso);
        }

        // Lista de Zonas alternativasGuardadas
        public List<BeZonaAlternativa> ObtenerZonaAlternativaProcesadaVisita(string connstring, int idProceso)
        {
            return DaDescripcionZonaAlternativa.ObtenerZonaAlternativaProcesadaVisita(connstring, idProceso);
        }

        public List<BeZonaAlternativa> ObtenerZonaAlternativaVisita(string connstring, int idProceso)
        {
            return DaDescripcionZonaAlternativa.ObtenerZonaAlternativaVisita(connstring, idProceso);
        }
    }
}
