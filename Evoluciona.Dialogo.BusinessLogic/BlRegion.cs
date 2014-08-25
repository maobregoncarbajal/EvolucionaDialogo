using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.DataAccess;
using System.Collections.Generic;

namespace Evoluciona.Dialogo.BusinessLogic
{
    public class BlRegion
    {

        private static readonly DaRegion DaRegion = new DaRegion();

        public List<BeRegion> ListarRegion(string codigoPais, string codigoRegion)
        {
            return DaRegion.ListarRegion(codigoPais, codigoRegion);
        }

        public List<BeRegion> ListarRegionesPorPais(string codigoPais)
        {
            return DaRegion.ListarRegionesPorPais(codigoPais);
        }
    }
}
