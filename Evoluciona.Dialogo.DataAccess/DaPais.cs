
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaPais : DaConexion
    {
        public BePais ObtenerPais(string prefijoIsoPais)
        {
            var pais = new BePais();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_Pais", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;

                try
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        pais.codigoPais = reader.GetString(reader.GetOrdinal("chrCodPais"));
                        pais.prefijoIsoPais = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais"));
                        pais.codigoComercial = reader.GetString(reader.GetOrdinal("chrCodigoPaisComercial"));
                        pais.CodigoPaisAdam = reader.GetString(reader.GetOrdinal("chrCodigoPaisAdam"));
                        pais.NombrePais = reader.GetString(reader.GetOrdinal("vchNombrePais"));
                        pais.Imagen = reader.GetString(reader.GetOrdinal("vchImagenPais"));
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

            return pais;
        }

        public List<BePais> ObtenerPaises()
        {
            var paises = new List<BePais>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Seleccionar_Paises", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pais = new BePais
                            {
                                prefijoIsoPais = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais")),
                                NombrePais = reader.GetString(reader.GetOrdinal("vchNombrePais"))
                            };

                            paises.Add(pais);
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

            return paises;
        }

        public BeComun ObtenerPaisBeComun(string prefijoIsoPais)
        {
            var pais = new BeComun();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_Pais", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;

                try
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        pais.Codigo = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais"));
                        pais.Descripcion = reader.GetString(reader.GetOrdinal("vchNombrePais"));
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

            return pais;
        }





        public List<BeComun> ObtenerPaisesBeComun()
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexion())
            {
                cn.Open();

                var cmd = new SqlCommand("ESE_Seleccionar_Paises", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombrePais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombrePais"))
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

        public List<BeComun> ObtenerPaisesBeComunMz(string codPais)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexion())
            {
                cn.Open();

                var cmd = new SqlCommand("ESE_Obtener_PaisMz", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombrePais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombrePais"))
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
