
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlVisitaEvoluciona
    {
        private static readonly DaVisitaEvoluviona DaVisitaEvoluviona = new DaVisitaEvoluviona();

        public DataTable ObtenerPreguntasXRol(string connstring, int codigoRol)
        {
            return DaVisitaEvoluviona.ObtenerPreguntasXRol(connstring, codigoRol);
        }

        public DataTable ObtenerVisitasCampana(string prefijoPais, string periodo, int codigoRol)
        {
            return DaVisitaEvoluviona.ObtenerVisitasCampana(prefijoPais, periodo, codigoRol);
        }

        public List<BeDatosVisitaReporte> ObtenerVisitasDetalle(string periodo, string prefijo, string estadoEvaluada, int codigoRol)
        {
            return DaVisitaEvoluviona.ObtenerDatosVisitaDetalle(periodo, prefijo, estadoEvaluada, codigoRol);
        }

        public DataSet ObtenerPeriodosxAnio(string anio)
        {
            return DaVisitaEvoluviona.ObtenerPeriodosxAnio(anio);
        }
    }
}