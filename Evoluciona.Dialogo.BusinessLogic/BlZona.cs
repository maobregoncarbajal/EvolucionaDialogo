
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlZona
    {
        private static readonly DaZona DaZona = new DaZona();

        public List<BeZona> ListarZona(string codigoPais, string codigoRegion, string codigoZona)
        {
            return DaZona.ListarZona(codigoPais, codigoRegion, codigoZona);
        }
    }
}
