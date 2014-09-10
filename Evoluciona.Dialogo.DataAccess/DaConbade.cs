using System;
using System.Data;
using System.Data.SqlClient;

namespace Evoluciona.Dialogo.DataAccess
{
    public class DaConbade : DaConexion
    {
        public DataSet Combade(string stquery)
        {

            var ds = new DataSet();
            using (var conex = ObtieneConexion())
            {

                try
                {
                    conex.Open();
                    var cmd = new SqlCommand(stquery, conex) { CommandType = CommandType.Text, CommandTimeout = 300 };

                    var da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                }
                catch (Exception ex)
                {
                    var dt = ds.Tables.Add("Error");
                    dt.Columns.Add("Error");

                    var dr = dt.NewRow();
                    dr["Error"] = ex.Message;
                    dt.Rows.Add(dr);
                }
                finally
                {
                    conex.Close();
                }
            }
            return ds;
        }

    }
}
