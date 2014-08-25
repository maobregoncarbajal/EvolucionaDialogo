
using Evoluciona.Dialogo.BusinessEntity;

namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaPeriodo : DaConexion
    {
        public List<string> ObtenerPeriodos(string prefijoPais)
        {
            var listaPeriodos = new List<string>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPeriodos", conn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@chrPrefijoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoPais"].Value = prefijoPais;
                try
                {
                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        listaPeriodos.Add(reader.GetString(0).Trim());
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

            return listaPeriodos;
        }


        public List<BeComun> ObtenerListaDePeriodos(string prefijoPais)
        {
            var listaPeriodos = new List<BeComun>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPeriodos", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoPais"].Value = prefijoPais;
                try
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var entidad = new BeComun
                        {
                            Codigo =
                                reader.IsDBNull(reader.GetOrdinal("chrPeriodo"))
                                    ? default(string)
                                    : reader.GetString(reader.GetOrdinal("chrPeriodo")).Trim(),
                            Descripcion =
                                reader.IsDBNull(reader.GetOrdinal("chrPeriodo"))
                                    ? default(string)
                                    : reader.GetString(reader.GetOrdinal("chrPeriodo")).Trim()
                        };
                        listaPeriodos.Add(entidad);
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

            return listaPeriodos;
        }

    }
}
