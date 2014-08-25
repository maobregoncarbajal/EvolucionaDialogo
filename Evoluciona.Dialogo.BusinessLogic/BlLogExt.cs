
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using System.Data;

    public class BlLogExt
    {
        private static readonly DaLogExt DaLogExt = new DaLogExt();

        public DataTable ObtenerParametrosParaLogueoporCub(string cub)
        {
            return DaLogExt.ObtenerParametrosParaLogueoporCub(cub);
        }
    }
}
