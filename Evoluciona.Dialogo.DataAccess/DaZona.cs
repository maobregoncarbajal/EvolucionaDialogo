
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaZona : DaConexion
    {
        public List<BeZona> ListarZona(string codigoPais, string codigoRegion, string codigoZona)
        {
            var obeZona = new List<BeZona>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_SP_ZONA_LISTAR", conn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2).Value = codigoPais;
                cmd.Parameters.Add("@CodRegion", SqlDbType.Char, 3).Value = codigoRegion;
                cmd.Parameters.Add("@codZona", SqlDbType.Char, 6).Value = codigoZona;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ibeZona = new BeZona
                            {
                                intIDZona = reader.GetInt32(reader.GetOrdinal("intIDZona")),
                                CodPais = reader.GetString(reader.GetOrdinal("CodPais")),
                                CodRegion = reader.GetString(reader.GetOrdinal("CodRegion")),
                                obeRegion = new BeRegion
                                {
                                    DesRegion = reader.GetString(reader.GetOrdinal("DesRegion"))
                                },
                                codZona = reader.GetString(reader.GetOrdinal("codZona")),
                                DesZona = reader.GetString(reader.GetOrdinal("DesZona"))
                            };

                            obeZona.Add(ibeZona);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }

            return obeZona;
        }


        public List<BeZona> ListarZonasPorRegion(string codPais, string codRegion)
        {
            var entidades = new List<BeZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_LISTAR_ZONAS_POR_REGION", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2);
                cmd.Parameters["@CodPais"].Value = codPais;
                cmd.Parameters.Add("@CodRegion", SqlDbType.Char, 3);
                cmd.Parameters["@CodRegion"].Value = codRegion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeZona
                            {
                                CodPais = dr.IsDBNull(dr.GetOrdinal("CodPais"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("CodPais")).Trim(),
                                CodRegion = dr.IsDBNull(dr.GetOrdinal("CodRegion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("CodRegion")).Trim(),
                                codZona = dr.IsDBNull(dr.GetOrdinal("codZona"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("codZona")).Trim(),
                                CodigoPaisComercial = dr.IsDBNull(dr.GetOrdinal("chrCodigoPaisComercial"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("chrCodigoPaisComercial")).Trim(),
                                DirCodRegion = dr.IsDBNull(dr.GetOrdinal("DirCodRegion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("DirCodRegion")).Trim(),
                                DircodZona = dr.IsDBNull(dr.GetOrdinal("DircodZona"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("DircodZona")).Trim(),
                                DesZona = dr.IsDBNull(dr.GetOrdinal("DesZona"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("DesZona")).Trim()
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



        public int ValidaCodGz(string pais, string region, string zona, string codGz)
        {
            int cant;

            using (var cn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_EXISTE_CODGZ", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchCodigoZona", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@chrCodigoGerenteZona", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intCant", SqlDbType.Int);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais.Trim();
                cmd.Parameters["@vchCodigoRegion"].Value = region.Trim();
                cmd.Parameters["@vchCodigoZona"].Value = zona.Trim();
                cmd.Parameters["@chrCodigoGerenteZona"].Value = codGz.Trim();
                cmd.Parameters["@intCant"].Direction = ParameterDirection.Output; 

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cant = Convert.ToInt32(cmd.Parameters["@intCant"].Value);
                    
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
            return cant;
        }

        public int ValidaCubGz(string pais, string region, string zona, string cub)
        {
            int cant;

            using (var cn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_EXISTE_CUBGZ", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchCodigoZona", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@vchCUBGZ", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@intCant", SqlDbType.Int);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais.Trim();
                cmd.Parameters["@vchCodigoRegion"].Value = region.Trim();
                cmd.Parameters["@vchCodigoZona"].Value = zona.Trim();
                cmd.Parameters["@vchCUBGZ"].Value = cub.Trim();
                cmd.Parameters["@intCant"].Direction = ParameterDirection.Output;

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cant = Convert.ToInt32(cmd.Parameters["@intCant"].Value);

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
            return cant;
        }


    }
}
