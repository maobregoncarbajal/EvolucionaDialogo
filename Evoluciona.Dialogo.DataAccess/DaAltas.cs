using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Evoluciona.Dialogo.BusinessEntity;

namespace Evoluciona.Dialogo.DataAccess
{
    public class DaAltas : DaConexion
    {
        public List<BeAltas> ListaAltas(string prefijoIsoPais)
        {
            var entidades = new List<BeAltas>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ALTAS_MASIVAS_LISTAR", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeAltas
                            {
                                CodCargo =
                                    dr.IsDBNull(dr.GetOrdinal("codCargo"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("codCargo")),
                                CodigoPaisComercial =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPaisComercial"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPaisComercial")),
                                Nombres =
                                    dr.IsDBNull(dr.GetOrdinal("nombres"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("nombres")).Trim(),
                                MailBelcorp =
                                    dr.IsDBNull(dr.GetOrdinal("mailBelcorp"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("mailBelcorp")).Trim(),
                                Cub =
                                    dr.IsDBNull(dr.GetOrdinal("CUB"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("CUB")),
                                DirCodRegion =
                                    dr.IsDBNull(dr.GetOrdinal("DirCodRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("DirCodRegion")),
                                DircodZona =
                                    dr.IsDBNull(dr.GetOrdinal("DircodZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("DircodZona")),
                                NroDoc =
                                    dr.IsDBNull(dr.GetOrdinal("nroDoc"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("nroDoc")),
                                CodPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("codPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("codPlanilla")),
                                PrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                CodRegion =
                                    dr.IsDBNull(dr.GetOrdinal("CodRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("CodRegion")),
                                CodZona =
                                    dr.IsDBNull(dr.GetOrdinal("codZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("codZona"))
                            };
                            entidades.Add(entidad);
                        }

                        dr.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();

                    cn.Dispose();
                }
            }

            return entidades;
        }


        public bool InsertarAltas(BeAltas obeAltas)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ALTAS_MASIVAS_INSERT", conn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@codCargo", SqlDbType.VarChar, 3).Value = obeAltas.CodCargo;
                cmd.Parameters.Add("@chrCodigoPaisComercial", SqlDbType.VarChar, 3).Value = obeAltas.CodigoPaisComercial;
                cmd.Parameters.Add("@nombres", SqlDbType.VarChar, 150).Value = obeAltas.Nombres;
                cmd.Parameters.Add("@mailBelcorp", SqlDbType.VarChar, 100).Value = obeAltas.MailBelcorp;
                cmd.Parameters.Add("@CUB", SqlDbType.VarChar,25).Value = obeAltas.Cub;
                cmd.Parameters.Add("@DirCodRegion", SqlDbType.VarChar, 2).Value = obeAltas.DirCodRegion;
                cmd.Parameters.Add("@DircodZona", SqlDbType.VarChar, 4).Value = obeAltas.DircodZona;
                cmd.Parameters.Add("@nroDoc", SqlDbType.VarChar, 30).Value = obeAltas.NroDoc;
                cmd.Parameters.Add("@codPlanilla", SqlDbType.VarChar, 10).Value = obeAltas.CodPlanilla;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeAltas.PrefijoIsoPais;
                cmd.Parameters.Add("@CodRegion", SqlDbType.VarChar, 15).Value = obeAltas.CodRegion;
                cmd.Parameters.Add("@codZona", SqlDbType.VarChar, 40).Value = obeAltas.CodZona;
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


    }
}
