
using Evoluciona.Dialogo.BusinessEntity;

namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using System.Collections.Generic;

    public class BlPeriodos
    {

        private static readonly DaPeriodo DaPeriodo = new DaPeriodo();

        public List<string> ObtenerPeriodos(string prefijoPais)
        {
            return DaPeriodo.ObtenerPeriodos(prefijoPais);
        }

        public List<BeComun> ObtenerListaDePeriodos(string prefijoPais)
        {
            return DaPeriodo.ObtenerListaDePeriodos(prefijoPais);
        }

    }
}
