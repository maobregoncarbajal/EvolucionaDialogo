
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlLet
    {
        private static readonly DaLet OdaLet = new DaLet();

        public List<BeLet> ObtenerLetsPorZona(string codPais, string codigoGerenteZona, string codigoDataMart, string periodo)
        {
            return OdaLet.ObtenerLetsPorZona(codPais, codigoGerenteZona, codigoDataMart, periodo);
        }
    }
}
