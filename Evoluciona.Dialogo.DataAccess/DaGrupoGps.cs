
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaGrupoGps : DaConexion
    {
        public List<BeGrupoGps> ObtenerGruposGps(string prefijoIsoPais)
        {
            var gruposGps = new List<BeGrupoGps>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SP_ObtenerGruposGPS", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;


                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var grupos = new BeGrupoGps
                        {
                            ZonaGps =
                                !dr.IsDBNull(dr.GetOrdinal("ZonaGPS")) ? dr.GetString(dr.GetOrdinal("ZonaGPS")) : null,
                            Grupo = !dr.IsDBNull(dr.GetOrdinal("Grupo")) ? dr.GetString(dr.GetOrdinal("Grupo")) : null
                        };

                        if (grupos.ZonaGps != null)
                            gruposGps.Add(grupos);
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return gruposGps;
        }

        public int GrabarGruposGps(string prefijoIsoPais, string pXml)
        {
            int resultado;

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SP_GrabarGruposGPS", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@Pais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@xmlZonasGPS", SqlDbType.Text);

                cmd.Parameters["@Pais"].Value = prefijoIsoPais;
                cmd.Parameters["@xmlZonasGPS"].Value = pXml;


                try
                {
                    resultado = cmd.ExecuteNonQuery();

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
            return resultado;
        }
    }
}
