
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlPais
    {

        private static readonly DaPais DaPais = new DaPais();

        public BePais ObtenerPais(string prefijoIsoPais)
        {
            return DaPais.ObtenerPais(prefijoIsoPais);
        }

        public List<BePais> ObtenerPaises()
        {
            return DaPais.ObtenerPaises();
        }

        public BeComun ObtenerPaisBeComun(string prefijoIsoPais)
        {
            return DaPais.ObtenerPaisBeComun(prefijoIsoPais);
        }

        public List<BeComun> ObtenerPaisesBeComun()
        {
            return DaPais.ObtenerPaisesBeComun();
        }

        public List<BeComun> ObtenerPaisesBeComunMz(string codPais)
        {
            return DaPais.ObtenerPaisesBeComunMz(codPais);
        }
    }
}
