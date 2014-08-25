using System.Collections.Generic;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeGerenteZonaPaginacion
    {   //valores  que utilizan el jqgrid
        public List<BeGerenteZona> rows { get; set; }
        public int page { get; set; }
        public int total { get; set; }
        public int records { get; set; }
    
    }
}
