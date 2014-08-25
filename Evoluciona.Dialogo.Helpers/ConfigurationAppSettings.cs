
namespace Evoluciona.Dialogo.Helpers
{
    using System.Configuration;

    public static class ConfigurationAppSettings
    {
        public static string ServidorSmtp()
        {
            return ConfigurationManager.ConnectionStrings["servidorSMTP"].ConnectionString;
        }

        public static string UsuarioEnviaMails()
        {
            return ConfigurationManager.ConnectionStrings["usuarioEnviaMails"].ConnectionString;
        }

        public static string UsuarioSoporte()
        {
            return ConfigurationManager.ConnectionStrings["usuarioSoporte"].ConnectionString;
        }
    }
}
