
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaDescripcionZonaAlternativa
    {
       public DataTable ObtenerZonaAlternativa(string connstring)
       {
           var ds = new DataSet();
           using (var conn = new SqlConnection(connstring))
           {
               conn.Open();

               var cmd = new SqlCommand("ESE_ObtenerZonaAlternativa", conn) {CommandType = CommandType.StoredProcedure};

               try
               {
                   var dap = new SqlDataAdapter(cmd);
                   dap.Fill(ds);
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message, ex.InnerException);
               }
               finally
               {
                   if (conn.State == ConnectionState.Open)
                       conn.Close();
               }
           }
           return ds.Tables[0];
       }

       /// <summary>
       /// Retorna una lista con las alternativas por Rol
       /// </summary>
       /// <param name="connstring"></param>
       /// <param name="codigoRol"></param>
       /// <returns></returns>
       public List<BeZonaAlternativa> ObtenerZonaAlternativaPorRol(string connstring, int codigoRol)
       {
           var listZonaAlternativas = new List<BeZonaAlternativa>();
           using (var conn = new SqlConnection(connstring))
           {
               conn.Open();

               var cmd = new SqlCommand("ESE_ObtenerZonaAlternativaPorRol", conn)
               {
                   CommandType = CommandType.StoredProcedure
               };

               cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
               cmd.Parameters["@intCodigoRol"].Value = codigoRol;

               try
               {
                   var dr = cmd.ExecuteReader();
                   while (dr.Read())
                   {
                       var objZonaAlter = new BeZonaAlternativa
                       {
                           CodZonaAlternativa = Convert.ToInt32(dr["intCodZonaAlternativa"]),
                           Nivel = Convert.ToInt32(dr["intNivel"]),
                           CodigoMaestro = Convert.ToInt32(dr["intIdMaestroAlternativa"]),
                           Alternativa = dr["vchDescripcionZonaAlternativa"].ToString()
                       };
                       listZonaAlternativas.Add(objZonaAlter);
                   }
                   dr.Close();
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message, ex.InnerException);
               }
               finally
               {
                   if (conn.State == ConnectionState.Open)
                       conn.Close();
               }
           }
           return listZonaAlternativas;
       }

        /// <summary>
        /// Retorna una lista con las alternativas que han sido guardadas
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="idProceso"></param>
        /// <returns></returns>
        public List<BeZonaAlternativa> ObtenerZonaAlternativaProcesada(string connstring, int idProceso)
       {
           var listZonaAlternativas = new List<BeZonaAlternativa>();
           using (var conn = new SqlConnection(connstring))
           {
               conn.Open();

               var cmd = new SqlCommand("ESE_Obtener_ZonaAlternativaProcesada", conn)
               {
                   CommandType = CommandType.StoredProcedure
               };

               cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

               cmd.Parameters[0].Value = idProceso;
               
               try
               {
                   var dr = cmd.ExecuteReader();
                   while (dr.Read())
                   {
                       var objZonaAlter = new BeZonaAlternativa
                       {
                           CodZonaAlternativa = Convert.ToInt32(dr["intCodZonaAlternativa"]),
                           Seleccionado = dr["chrSeleccionado"].ToString(),
                           AlternativaOtro = dr["vchOtro"].ToString()
                       };
                       listZonaAlternativas.Add(objZonaAlter);
                   }
                   dr.Close();
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message, ex.InnerException);
               }
               finally
               {
                   if (conn.State == ConnectionState.Open)
                       conn.Close();
               }
           }
           return listZonaAlternativas;
       }

       //Lista de las Zona de Alternativas Grabadas para las Visitas
       public List<BeZonaAlternativa> ObtenerZonaAlternativaProcesadaVisita(string connstring, int idProceso)
       {
           var listZonaAlternativas = new List<BeZonaAlternativa>();
           using (var conn = new SqlConnection(connstring))
           {
               conn.Open();

               var cmd = new SqlCommand("ESE_Obtener_ZonaAlternativaProcesadaVisita", conn)
               {
                   CommandType = CommandType.StoredProcedure
               };

               cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
               cmd.Parameters[0].Value = idProceso;

               try
               {
                   var dr = cmd.ExecuteReader();
                   while (dr.Read())
                   {
                       var objZonaAlter = new BeZonaAlternativa
                       {
                           CodZonaAlternativa = Convert.ToInt32(dr["intCodZonaAlternativa"]),
                           Seleccionado = dr["chrSeleccionado"].ToString(),
                           AlternativaOtro = dr["vchOtro"].ToString()
                       };
                       listZonaAlternativas.Add(objZonaAlter);
                   }
                   dr.Close();
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message, ex.InnerException);
               }
               finally
               {
                   if (conn.State == ConnectionState.Open)
                       conn.Close();
               }
           }
           return listZonaAlternativas;
       }

       public List<BeZonaAlternativa> ObtenerZonaAlternativaVisita(string connstring, int idProceso)
       {
           var listZonaAlternativas = new List<BeZonaAlternativa>();
           using (var conn = new SqlConnection(connstring))
           {
               conn.Open();

               var cmd = new SqlCommand("ESE_Obtener_ZonaAlternativaProcesada_Visita", conn)
               {
                   CommandType = CommandType.StoredProcedure
               };

               cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
               cmd.Parameters[0].Value = idProceso;

               try
               {
                   var dr = cmd.ExecuteReader();
                   while (dr.Read())
                   {
                       var objZonaAlter = new BeZonaAlternativa
                       {
                           CodZonaAlternativa = Convert.ToInt32(dr["intCodZonaAlternativa"]),
                           Seleccionado = dr["chrSeleccionado"].ToString(),
                           AlternativaOtro = dr["vchOtro"].ToString()
                       };
                       listZonaAlternativas.Add(objZonaAlter);
                   }
                   dr.Close();
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message, ex.InnerException);
               }
               finally
               {
                   if (conn.State == ConnectionState.Open)
                       conn.Close();
               }
           }
           return listZonaAlternativas;
       }
    }
}
