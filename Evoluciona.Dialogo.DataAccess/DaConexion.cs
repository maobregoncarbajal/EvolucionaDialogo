
namespace Evoluciona.Dialogo.DataAccess
{
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Web;

    public class DaConexion
    {
        public static SqlConnection ObtieneConexion()
        {
            SqlConnection conex;

            if (HttpContext.Current == null)
            {
                conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString);
            }
            else
            {
                if (HttpContext.Current.Session == null)
                {
                    conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString);
                }
                else
                {
                    if (HttpContext.Current.Session["connApp"] == null)
                        HttpContext.Current.Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;

                    conex = new SqlConnection(HttpContext.Current.Session["connApp"].ToString());
                }
            }
            return conex;
        }

        public static SqlConnection ObtieneConexionJob()
        {
            var conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString);

            return conex;
        }

        public static SqlConnection ObtieneConexionDataMart()
        {
            var conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxDataMart"].ConnectionString);
            return conex;
        }

        public static SqlConnection ObtieneConexionESE_SK_MAIN()
        {
            //SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxEseSkMain"].ConnectionString);
            var conex = new SqlConnection();

            return conex;
        }

        public static SqlConnection ObtieneConexionJobDes()
        {
            //SqlConnection conex = new SqlConnection(ConfigurationManager.ConnectionStrings["cnxDialogoDesempenioDes"].ConnectionString);
            var conex = new SqlConnection();

            return conex;
        }

    }
}
