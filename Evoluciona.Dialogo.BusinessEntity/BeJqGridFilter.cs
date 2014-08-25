using Evoluciona.Dialogo.Helpers;
using System.Collections.Generic;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeJqGridFilter
    {
        public JqGridGroupOp groupOp { get; set; }
        public List<BeJqGridRule> rules { get; set; }
        public List<BeJqGridFilter> groups { get; set; }
    }
}
