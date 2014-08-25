
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlRol
    {
        private static readonly DaRol DaRol = new DaRol();

        public List<BeRol> ObtenerRolesSubordinados(int idRolActual)
        {
            return DaRol.ObtenerRolesSubordinados(idRolActual);
        }
    }
}
