
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEvoluciona : DaConexion
    {
        // Ingresar Plan accion  ingresarPlanAccion
        public bool IngresarVisitaEvoluciona(string connstring, BeEvoluciona beEvoluciona)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_Insertar_Visita_Evoluciona", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intIDPregunta", SqlDbType.Int);

                cmd.Parameters.Add("@vchRespuesta", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@bitEvoluciona", SqlDbType.Bit);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);

                cmd.Parameters[0].Value = beEvoluciona.IDVisita;
                cmd.Parameters[1].Value = beEvoluciona.IDPregunta;

                cmd.Parameters[2].Value = beEvoluciona.Respuesta;
                cmd.Parameters[3].Value = beEvoluciona.Evoluciona;
                cmd.Parameters[4].Value = beEvoluciona.idUsuario;

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


        // Obtener visita Evoluciona Grabadas
        public DataTable ObtenerEvaluacionGrabadas(string connstring, int idVisita, int codigoRol)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VS_Obtener_Evoluciona_Procesado", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);


                cmd.Parameters["@intIdVisita"].Value = idVisita;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;

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
