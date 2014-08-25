
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using Helpers;
    using System.Collections.Generic;

    public class BlEncuesta
    {
        private static readonly DaEncuesta DaEncuesta = new DaEncuesta();

        public List<BeEncuesta> ObtenerPreguntasEncuesta(int idProceso, TipoEncuesta tipoEncuesta)
        {
            return DaEncuesta.ObtenerPreguntasEncuesta(idProceso, tipoEncuesta);
        }

        public bool RegistrarEncuesta(List<BeEncuesta> nuevaEncuesta, TipoEncuesta tipoEncuesta)
        {
            return DaEncuesta.RegistrarEncuesta(nuevaEncuesta, tipoEncuesta);
        }
    }
}
