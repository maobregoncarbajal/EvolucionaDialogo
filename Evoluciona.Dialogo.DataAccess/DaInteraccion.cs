
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaInteraccion : DaConexion
    {
        // Ingresar Plan Anual
        public bool IngresarInteraccion(string connstring, BeInteraccion beInteraccion)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_Insertar_Interaccion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@vchObjetivoVisita", SqlDbType.VarChar, 600);
                cmd.Parameters.Add("@vchOtrasAlternativas", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);

                cmd.Parameters[0].Value = beInteraccion.IDVisita;
                cmd.Parameters[1].Value = beInteraccion.ObjetivoVisita;
                cmd.Parameters[2].Value = beInteraccion.OtrasAlternativas;
                cmd.Parameters[3].Value = beInteraccion.idUsuario;

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

        public DataTable ObtenerInteraccionGrabadas(string connstring, int idVisita)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VS_Obtener_Interaccion_Grabadas", conn)
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
