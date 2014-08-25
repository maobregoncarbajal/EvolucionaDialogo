
namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaPreguntaAcuerdo : DaConexion
    {

        public DataTable ObtenerPreguntaAcuerdo(string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VS_Obtener_Pregunta_Acuerdo", conn)
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
