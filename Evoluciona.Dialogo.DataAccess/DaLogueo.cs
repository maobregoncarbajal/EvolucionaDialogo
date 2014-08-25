
namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaLogueo : DaConexion
    {
        public int ValidarUsuario(string uid, string pwd, string connstring)
        {
            var flag = 0;
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SeleccionarUsuario_LogInAdmin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrUID", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPWD", SqlDbType.Char, 40);

                cmd.Parameters[0].Value = uid;

                if (pwd == null) cmd.Parameters[1].Value = DBNull.Value;
                else cmd.Parameters[1].Value = pwd;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        flag = ds.Tables[0].Rows.Count > 0 ? 1 : 0;

                    }
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
            return flag;
        }
    }
}
