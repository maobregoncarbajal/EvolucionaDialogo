
namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaCargaAdam : DaConexion
    {

        public bool CargarArchivoAdam(string dtsName)
        {
            bool resultado;
            using (var conn = ObtieneConexionJobDes())
            {
                var cmd = new SqlCommand("sp_EjecutarDTS", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@Paquete", SqlDbType.VarChar, 500);
                cmd.Parameters["@Paquete"].Value = dtsName;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
                catch (Exception)
                {
                    resultado = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }
    }
}
