using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WSEvoluciona
{
    public class DaConexion
    {

        public static SqlConnection GetConexion()
        {
            SqlConnection conex = null;

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

    }
}