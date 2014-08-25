
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;

    public class BlLogueo
    {
        private static readonly DaLogueo DaLogueo = new DaLogueo();

        public int ValidarUsuario(string uid, string pwd, string connstring)
        {
            return DaLogueo.ValidarUsuario(uid, pwd, connstring);
        }
    }
}
