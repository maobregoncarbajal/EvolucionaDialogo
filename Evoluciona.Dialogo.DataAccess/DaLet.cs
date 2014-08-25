
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaLet : DaConexion
    {
        public List<BeLet> ObtenerLetsPorZona(string codPais, string codigoGerenteZona, string codigoDataMart, string periodo)
        {
            var lstLets = new List<BeLet>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_OBTENER_LETS_POR_ZONA", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2).Value = codPais;
                cmd.Parameters.Add("@chrCodigoGerenteZona", SqlDbType.Char, 20).Value = codigoGerenteZona;
                cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 10).Value = codigoDataMart;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objLet = new BeLet
                                         {
                                             IdLet =
                                                 dr.IsDBNull(dr.GetOrdinal("intIDLet"))
                                                     ? 0
                                                     : dr.GetInt32(dr.GetOrdinal("intIDLet")),
                                             AnioCampana =
                                                 dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                                             CodPais =
                                                 dr.IsDBNull(dr.GetOrdinal("chrCodPais"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("chrCodPais")),
                                             CodRegion =
                                                 dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("chrCodRegion")),
                                             CodGerenteRegional =
                                                 dr.IsDBNull(dr.GetOrdinal("chrCodGerenteRegional"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("chrCodGerenteRegional")),
                                             CodGerenteZona =
                                                 dr.IsDBNull(dr.GetOrdinal("chrCodGerenteZona"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("chrCodGerenteZona")),
                                             CodigoConsultoraLet =
                                                 dr.IsDBNull(dr.GetOrdinal("chrCodigoConsultoraLET"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("chrCodigoConsultoraLET")),
                                             DesNombreLet =
                                                 dr.IsDBNull(dr.GetOrdinal("vchDesNombreLET"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("vchDesNombreLET")),
                                             CorreoElectronico =
                                                 dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                                             EstadoCamp =
                                                 dr.IsDBNull(dr.GetOrdinal("vchEstadoCamp"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("vchEstadoCamp")),
                                             EstadoPeriodo =
                                                 dr.IsDBNull(dr.GetOrdinal("vchEstadoPeriodo"))
                                                     ? string.Empty
                                                     : dr.GetString(dr.GetOrdinal("vchEstadoPeriodo")),
                                             FechaUltAct =
                                                 dr.IsDBNull(dr.GetOrdinal("FechaUltAct"))
                                                     ? DateTime.MinValue
                                                     : dr.GetDateTime(dr.GetOrdinal("FechaUltAct")),
                                             FlagProceso =
                                                 dr.IsDBNull(dr.GetOrdinal("intFlagProceso"))
                                                     ? 0
                                                     : dr.GetInt32(dr.GetOrdinal("intFlagProceso")),
                                             FlagControl =
                                                 dr.IsDBNull(dr.GetOrdinal("intFlagControl"))
                                                     ? 0
                                                     : dr.GetInt32(dr.GetOrdinal("intFlagControl"))
                                         };

                        lstLets.Add(objLet);
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

            return lstLets;
        }


    }
}
