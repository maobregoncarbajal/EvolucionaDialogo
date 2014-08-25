
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlUsuario
    {

        private static readonly DaUsuario DaUsuario = new DaUsuario();

        /// <summary>
        /// Obtiene los datos del usuario
        /// </summary>
        /// <returns>el objeto beUsuario</returns>
        public BeUsuario ObtenerDatosUsuario(string prefijoIsoPais, int codigoRol, string codigoUsuario, byte estado)
        {
            var objUsuario = DaUsuario.ObtenerDatosUsuario(prefijoIsoPais, codigoRol, codigoUsuario, estado);
            return objUsuario;
        }

        /// <summary>
        /// Obtiene los datos del Rol
        /// </summary>
        /// <param name="codigoRol"></param>
        /// <param name="estado"></param>
        /// <returns>Los datos del rol</returns>
        public DataTable ObtenerDatosRol(int codigoRol, byte estado)
        {
            return DaUsuario.ObtenerDatosRol(codigoRol, estado);
        }
    }
}
