
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaIteraccionVisita : DaConexion
    {

        // Obtener Preguntas 
        public DataTable ObtenerPreguntas(string connstring, int idProceso)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VS_Obtener_Iteraccion_Visita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);


                cmd.Parameters[0].Value = idProceso;

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
                    conn.Close();
                }
            }
            return ds.Tables[0];
        }


        // Ingresar Iteraccion Visita 

        public bool IngresarIteraccionVisita(string connstring, BeIteraccionVisita beIteraccionVisita)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_Insertar_Iteraccion_Visita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@bitOservacion", SqlDbType.Bit);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);


                cmd.Parameters[0].Value = beIteraccionVisita.IDVisita;
                cmd.Parameters[1].Value = beIteraccionVisita.CodigoPlanAnual;
                cmd.Parameters[2].Value = beIteraccionVisita.Observacion;
                cmd.Parameters[3].Value = beIteraccionVisita.bitOservacion;
                cmd.Parameters[4].Value = beIteraccionVisita.idUsuario;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    conn.Close();

                }
            }
            return true;


        }


        // Obtener Preguntas 
        public DataTable ObtenerPreguntasGrabadas(string connstring, int idVisita)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VS_Obtener_Preguntas_Grabadas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdVisita", SqlDbType.Int);


                cmd.Parameters[0].Value = idVisita;

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
                    conn.Close();
                }
            }
            return ds.Tables[0];
        }

    }
}
