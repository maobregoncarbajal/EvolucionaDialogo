
namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaLogExt : DaConexion
    {
        public DataTable ObtenerParametrosParaLogueoporCub(string cub)
        {
            var ds = new DataTable();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SELECT_DATA_LOGIN_FFVV", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@vchCUB", SqlDbType.VarChar, 20);

                cmd.Parameters[0].Value = cub;

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
            return ds;
        }
    }
}
