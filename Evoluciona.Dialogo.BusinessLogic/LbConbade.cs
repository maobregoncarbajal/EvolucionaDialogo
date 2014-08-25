using Evoluciona.Dialogo.DataAccess;
using System.Data;

namespace Evoluciona.Dialogo.BusinessLogic
{
    public  class LbConbade
    {
        private static readonly DaConbade DaConbade = new DaConbade();

        public DataSet Conbade(string stquery)
        {
            return DaConbade.Combade(stquery);
        }

    }
}
