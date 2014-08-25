using Evoluciona.Dialogo.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Evoluciona.Dialogo.DataAccess
{


    public class DaRegion : DaConexion
    {
        public List<BeRegion> ListarRegion(string codigoPais, string codigoRegion)
        {
            var obeRegion = new List<BeRegion>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_SP_REGION_LISTAR", conn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2).Value = codigoPais;
                cmd.Parameters.Add("@CodRegion", SqlDbType.Char, 3).Value = codigoRegion;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ibeRegion = new BeRegion
                            {
                                intIDMaeRegion = reader.GetInt32(reader.GetOrdinal("intIDMaeRegion")),
                                CodPais = reader.GetString(reader.GetOrdinal("CodPais")),
                                obePais = new BePais {NombrePais = reader.GetString(reader.GetOrdinal("vchNombrePais"))},
                                CodRegion = reader.GetString(reader.GetOrdinal("CodRegion")),
                                DesRegion = reader.GetString(reader.GetOrdinal("DesRegion")),
                                IdMaeCodidgoRegion = reader.GetString(reader.GetOrdinal("IdMaeCodidgoRegion"))
                            };

                            obeRegion.Add(ibeRegion);
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

            return obeRegion;
        }


        public List<BeRegion> ListarRegionesPorPais(string codPais)
        {
            var entidades = new List<BeRegion>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_LISTAR_REGIONES_POR_PAIS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2);
                cmd.Parameters["@CodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRegion
                            {
                                CodPais = dr.IsDBNull(dr.GetOrdinal("CodPais"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("CodPais")).Trim(),
                                CodRegion = dr.IsDBNull(dr.GetOrdinal("CodRegion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("CodRegion")).Trim(),
                                CodigoPaisComercial = dr.IsDBNull(dr.GetOrdinal("chrCodigoPaisComercial"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("chrCodigoPaisComercial")).Trim(),
                                DirCodRegion = dr.IsDBNull(dr.GetOrdinal("DirCodRegion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("DirCodRegion")).Trim(),
                                DesRegion = dr.IsDBNull(dr.GetOrdinal("DesRegion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("DesRegion")).Trim()
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




    }
}
