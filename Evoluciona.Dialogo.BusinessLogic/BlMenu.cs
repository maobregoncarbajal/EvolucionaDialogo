
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using System.Data;

    public class BlMenu
    {

        private static readonly DaMenu DaMenu = new DaMenu();

        /// <summary>
        /// Obtiene las opciones para el menu del usuario
        /// </summary>
        /// <returns>la lista con el menu</returns>
        public DataTable ObtenerMenuPrincipal(int idRol, byte estado)
        {
            return DaMenu.ObtenerMenuPrincipal(idRol, estado);
        }

        public DataTable ObtenerDetalleMenu(int idMenu, int idRol, byte estado)
        {
            return DaMenu.ObtenerDetalleMenu(idMenu, idRol, estado);
        }

        public DataTable ValidarAprobacion(int intIdProceso, string chrEstadoProceso)
        {
            return DaMenu.ValidarAprobacion(intIdProceso, chrEstadoProceso);
        }

        public DataTable ObtenerMenu(int? idMenuPadre)
        {
            return DaMenu.ObtenerMenu(idMenuPadre);
        }

        public DataTable ValidarAprobacionSinLets(int intIdProceso, string chrEstadoProceso)
        {
            return DaMenu.ValidarAprobacionSinLets(intIdProceso, chrEstadoProceso);
        }

        public string ObtenerUrlMenu(string descripcionMenu, byte estado)
        {
            return DaMenu.ObtenerUrlMenu(descripcionMenu, estado);
        }
    }
}
