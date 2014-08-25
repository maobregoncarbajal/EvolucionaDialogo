
namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaPreguntaRetroalimentacion : DaConexion
    {
        public DataTable ObtenerPreguntaRetroalimentacion(string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPreguntaRetroalimentacion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

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
