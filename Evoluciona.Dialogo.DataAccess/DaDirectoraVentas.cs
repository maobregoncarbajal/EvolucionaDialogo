
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaDirectoraVentas : DaConexion
    {
        public List<BeDirectoraVentas> DirectoraVentasListar(int idDirectoraVenta, string prefijoIsoPais, string nombreCompleto, bool estado)
        {
            var obeDirectoraVentas = new List<BeDirectoraVentas>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORA_VENTAS_LISTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@intIDDirectoraVenta", SqlDbType.Int).Value = idDirectoraVenta;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit).Value = estado;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeDirectoraVentas = new BeDirectoraVentas
                        {
                            intIDDirectoraVenta =
                                dr.IsDBNull(dr.GetOrdinal("intIDDirectoraVenta"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDDirectoraVenta")),
                            chrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            obePais =
                                new BePais
                                {
                                    NombrePais =
                                        dr.IsDBNull(dr.GetOrdinal("vchNombrePais"))
                                            ? string.Empty
                                            : dr.GetString(dr.GetOrdinal("vchNombrePais"))
                                },
                            chrCodigoDirectoraVentas =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDirectoraVentas"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDirectoraVentas")),
                            vchNombreCompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombreCompleto")),
                            vchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            vchDocumentoIndentidad =
                                dr.IsDBNull(dr.GetOrdinal("vchDocumentoIndentidad"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchDocumentoIndentidad")),
                            EstadoDirectora =
                                dr.IsDBNull(dr.GetOrdinal("Estado"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("Estado"))
                        };

                        obeDirectoraVentas.Add(ibeDirectoraVentas);
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

            return obeDirectoraVentas;
        }

        public List<BeDirectoraVentas> DirectoraVentasListar(string prefijoIsoPais)
        {
            var obeDirectoraVentas = new List<BeDirectoraVentas>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_LISTAR_DIRECTORA_VENTAS_PARA_GR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeDirectoraVentas = new BeDirectoraVentas
                        {
                            chrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            chrCodigoDirectoraVentas =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDirectoraVentas"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDirectoraVentas")),
                            vchNombreCompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombreCompleto"))
                        };
                        obeDirectoraVentas.Add(ibeDirectoraVentas);
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

            return obeDirectoraVentas;
        }

        public bool DirectoraVentasRegistrar(BeDirectoraVentas obeDirectoraVentas)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORA_VENTAS_INSERTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeDirectoraVentas.chrPrefijoIsoPais;
                cmd.Parameters.Add("@chrCodigoDirectoraVentas", SqlDbType.Char, 20).Value =
                    obeDirectoraVentas.chrCodigoDirectoraVentas;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value =
                    obeDirectoraVentas.vchNombreCompleto;
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value =
                    obeDirectoraVentas.vchCorreoElectronico;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeDirectoraVentas.intUsuarioCrea;
                cmd.Parameters.Add("@vchDocumentoIndentidad", SqlDbType.VarChar, 20).Value =
                    obeDirectoraVentas.vchDocumentoIndentidad;
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

        public bool DirectoraVentasActualizar(BeDirectoraVentas obeDirectoraVentas)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORA_VENTAS_ACTUALIZAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@intIDDirectoraVenta", SqlDbType.Int).Value = obeDirectoraVentas.intIDDirectoraVenta;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeDirectoraVentas.chrPrefijoIsoPais;
                cmd.Parameters.Add("@chrCodigoDirectoraVentas", SqlDbType.Char, 20).Value =
                    obeDirectoraVentas.chrCodigoDirectoraVentas;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value =
                    obeDirectoraVentas.vchNombreCompleto;
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value =
                    obeDirectoraVentas.vchCorreoElectronico;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeDirectoraVentas.intUsuarioCrea;
                cmd.Parameters.Add("@vchDocumentoIndentidad", SqlDbType.VarChar, 20).Value =
                    obeDirectoraVentas.vchDocumentoIndentidad;
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

        public bool DirectoraVentasActualizarEstado(BeDirectoraVentas obeDirectoraVentas)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORA_VENTAS_ACTUALIZAR_ESTADO", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@intIDDirectoraVenta", SqlDbType.Int).Value = obeDirectoraVentas.intIDDirectoraVenta;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeDirectoraVentas.intUsuarioCrea;
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

        public List<BeDirectoraVentas> ListaDv(string prefijoIsoPais)
        {
            var entidades = new List<BeDirectoraVentas>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_DV", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeDirectoraVentas
                            {
                                intIDDirectoraVenta =
                                    dr.IsDBNull(dr.GetOrdinal("intIDDirectoraVenta"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDDirectoraVenta")),
                                chrPrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                chrCodigoDirectoraVentas =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoDirectoraVentas"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoDirectoraVentas")).Trim(),
                                vchNombreCompleto =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto")),
                                vchCorreoElectronico =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                                vchCUBDV =
                                    dr.IsDBNull(dr.GetOrdinal("vchCUBDV"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCUBDV")),
                                chrCodigoPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")),
                                vchObservacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservacion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchObservacion"))
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

        public bool DeleteDv(int id)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_ELIMINAR_DV", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDDirectoraVenta", SqlDbType.Int);
                cmd.Parameters["@intIDDirectoraVenta"].Value = id;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return true;
        }

        public bool AddDv(BeDirectoraVentas obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_INSERTAR_DV", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoDirectoraVentas", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchCUBDV", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 200);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.chrPrefijoIsoPais;
                cmd.Parameters["@chrCodigoDirectoraVentas"].Value = obj.chrCodigoDirectoraVentas;
                cmd.Parameters["@vchNombreCompleto"].Value = obj.vchNombreCompleto;
                cmd.Parameters["@vchCorreoElectronico"].Value = obj.vchCorreoElectronico;
                cmd.Parameters["@vchCUBDV"].Value = obj.vchCUBDV;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.chrCodigoPlanilla;
                cmd.Parameters["@vchObservacion"].Value = obj.vchObservacion;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
                catch (Exception)
                {
                    resultado = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }

        public bool EditDv(BeDirectoraVentas obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_ACTUALIZAR_DV", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDDirectoraVenta", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoDirectoraVentas", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchCUBDV", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 200);

                cmd.Parameters["@intIDDirectoraVenta"].Value = obj.intIDDirectoraVenta;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.chrPrefijoIsoPais;
                cmd.Parameters["@chrCodigoDirectoraVentas"].Value = obj.chrCodigoDirectoraVentas;
                cmd.Parameters["@vchNombreCompleto"].Value = obj.vchNombreCompleto;
                cmd.Parameters["@vchCorreoElectronico"].Value = obj.vchCorreoElectronico;
                cmd.Parameters["@vchCUBDV"].Value = obj.vchCUBDV;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.chrCodigoPlanilla;
                cmd.Parameters["@vchObservacion"].Value = obj.vchObservacion;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
                catch (Exception)
                {
                    resultado = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }
    }
}