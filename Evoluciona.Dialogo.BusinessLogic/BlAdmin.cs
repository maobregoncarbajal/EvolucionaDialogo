
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlAdmin
    {
        private static readonly DaAdmin DaAdmin = new DaAdmin();

        public List<BeAdmin> ListarAdministradores(string codigoPais, string tipoAdmin)
        {
            return DaAdmin.ListarAdministradores(codigoPais, tipoAdmin);
        }

        public BeAdmin ObtenerAdministrador(int idAdmin)
        {
            return DaAdmin.ObtenerAdministrador(idAdmin);
        }

        public void AgregarAdminsitrador(BeAdmin administrador)
        {
            DaAdmin.AgregarAdminsitrador(administrador);
        }

        public void ActualizarAdministrador(BeAdmin administrador)
        {
            DaAdmin.ActualizarAdministrador(administrador);
        }

        public void EliminarAdministrador(int idAdmin)
        {
            DaAdmin.EliminarAdministrador(idAdmin);
        }

        public BeAdmin VerificarAdmin(string codigo, string clave, string tipo, string codPais)
        {
            return DaAdmin.VerificarAdmin(codigo, clave, tipo, codPais);
        }
    }
}
