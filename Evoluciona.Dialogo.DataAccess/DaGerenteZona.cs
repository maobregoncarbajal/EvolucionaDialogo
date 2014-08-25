using Evoluciona.Dialogo.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Evoluciona.Dialogo.DataAccess
{


    public class DaGerenteZona : DaConexion
    {
        public List<BeGerenteZona> GerenteZonaListar(string prefijoIsoPais, int idGerenteRegion, string nombreCompleto, int idGerenteZona)
        {
            var obeGerenteZona = new List<BeGerenteZona>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_LISTAR", conn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int).Value = idGerenteRegion;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@intIDGerenteZona", SqlDbType.Int).Value = idGerenteZona;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteZona = new BeGerenteZona
                        {
                            intIDGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                            intIDGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                            obeGerenteRegion = new BeGerenteRegion
                            {
                                VchNombrecompleto =
                                    dr.IsDBNull(dr.GetOrdinal("GerenteRegion"))
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("GerenteRegion")),
                                ChrCodigoGerenteRegion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion"))
                            },
                            chrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            chrCodigoGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")),
                            vchNombreCompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            vchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            chrCodigoDataMart =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDataMart"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDataMart")),
                            chrCampaniaRegistro =
                                dr.IsDBNull(dr.GetOrdinal("chrCampaniaRegistro"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCampaniaRegistro")),
                            chrIndicadorMigrado =
                                dr.IsDBNull(dr.GetOrdinal("chrIndicadorMigrado"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrIndicadorMigrado")),
                            obePais = new BePais
                            {
                                CodigoPaisAdam =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPaisAdam"))
                                        ? ""
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPaisAdam"))
                            }
                        };

                        obeGerenteZona.Add(ibeGerenteZona);
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

            return obeGerenteZona;
        }

        public List<BeGerenteZona> GerenteZonaGetAll(string prefijoIsoPais, string nombreCompleto, string codigoRegion, string codigoZona, string periodo)
        {
            var obeGerenteZona = new List<BeGerenteZona>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_LISTAR_ALL", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = codigoRegion;
                cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 10).Value = codigoZona;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteZona = new BeGerenteZona
                        {
                            intIDGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                            chrCodigoGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")),
                            chrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            vchNombreCompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            AnioCampana =
                                dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                            vchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico"))
                        };

                        var obeGerenteRegion = new BeGerenteRegion
                        {
                            VchNombrecompleto =
                                dr.IsDBNull(dr.GetOrdinal("chrNombreGerenteRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrNombreGerenteRegion"))
                        };
                        ibeGerenteZona.obeGerenteRegion = obeGerenteRegion;

                        obeGerenteZona.Add(ibeGerenteZona);
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

            return obeGerenteZona;
        }

        public List<BeGerenteZona> GerenteZonaListarAlta(string prefijoIsoPais, string anioCampania, string codigoRegion, string codigoZona, string nombreGerente, string periodo)
        {
            var listaGerenteZona = new List<BeGerenteZona>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_LISTAR_ALTAS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreGerente;
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = codigoRegion;
                cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 10).Value = codigoZona;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6).Value = anioCampania;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var gerenteZona = new BeGerenteZona
                        {
                            DocIdentidad =
                                dr.IsDBNull(dr.GetOrdinal("chrDocIdentidad"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrDocIdentidad")),
                            DesGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("vchDesGerenteZona"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchDesGerenteZona")),
                            CodRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodRegion")),
                            codZona =
                                dr.IsDBNull(dr.GetOrdinal("chrCodZona"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodZona")),
                            CorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            chrCodigoDataMart =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDataMart"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDataMart")),
                            AnioCampana =
                                dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                            CUBGZ =
                                dr.IsDBNull(dr.GetOrdinal("vchCUBGZ"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCUBGZ"))
                        };

                        listaGerenteZona.Add(gerenteZona);
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

            return listaGerenteZona;
        }

        public bool GerenteZonaRegistrar(List<BeGerenteZona> listaGerenteZona)
        {
            var resultado = true;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var tran = cn.BeginTransaction();

                try
                {
                    foreach (var gerenteZona in listaGerenteZona)
                    {
                        var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_INSERTAR_ALTA", cn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = tran
                        };

                        cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20).Value = gerenteZona.DocIdentidad;
                        cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int).Value = gerenteZona.intIDGerenteRegion;
                        cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = gerenteZona.chrPrefijoIsoPais;
                        cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 20).Value = gerenteZona.chrCodigoDataMart;
                        cmd.Parameters.Add("@vchDesGerenteZona", SqlDbType.VarChar, 100).Value = gerenteZona.DesGerenteZona;
                        cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value = gerenteZona.vchCorreoElectronico;
                        cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = gerenteZona.intUsuarioCrea;
                        cmd.Parameters.Add("@chrCampaniaRegistro", SqlDbType.Char, 6).Value = gerenteZona.chrCampaniaRegistro;
                        cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value = gerenteZona.chrIndicadorMigrado;
                        cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = gerenteZona.CodRegion;
                        cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 10).Value = gerenteZona.codZona;
                        cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = gerenteZona.Periodo;
                        cmd.Parameters.Add("@vchCUBGZ", SqlDbType.Char, 8).Value = gerenteZona.CUBGZ;
                        cmd.ExecuteNonQuery();
                    }
                }

                catch (Exception)
                {
                    resultado = false;
                    tran.Rollback();
                }
                finally
                {
                    tran.Commit();
                    if (cn.State == ConnectionState.Open)
                        cn.Close();

                    cn.Dispose();
                }
            }

            return resultado;
        }

        public bool GerenteZonaActualizar(BeGerenteZona obeGerenteZona)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_INSERTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int).Value = obeGerenteZona.intIDGerenteRegion;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeGerenteZona.chrPrefijoIsoPais;
                cmd.Parameters.Add("@chrCodigoGerenteZona", SqlDbType.Char, 2).Value =
                    obeGerenteZona.chrCodigoGerenteZona;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value =
                    obeGerenteZona.vchNombreCompleto;
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value =
                    obeGerenteZona.vchCorreoElectronico;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeGerenteZona.intUsuarioCrea;
                cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 20).Value = obeGerenteZona.chrCodigoDataMart;
                cmd.Parameters.Add("@chrCampaniaRegistro", SqlDbType.Char, 6).Value = obeGerenteZona.chrCampaniaRegistro;
                cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value = obeGerenteZona.chrIndicadorMigrado;
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

        public bool GerenteZonaActualizarEstado(BeGerenteZona obeGerenteZona)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_ACTUALIZAR_ESTADO", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDGerenteZona", SqlDbType.Int).Value = obeGerenteZona.intIDGerenteZona;
                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int).Value = obeGerenteZona.intIDGerenteRegion;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeGerenteZona.intUsuarioCrea;
                cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value =
                    obeGerenteZona.chrIndicadorMigrado;
                cmd.Parameters.Add("@chrCampaniaBaja", SqlDbType.Char, 6).Value =
                    obeGerenteZona.chrCampaniaBaja;

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

        public bool GerenteZonaActualizarGerenteRegion(BeGerenteZona obeGerenteZona)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_ACTUALIZAR_GERENTE_REGION", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDGerenteZona", SqlDbType.Int).Value = obeGerenteZona.intIDGerenteZona;
                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int).Value = obeGerenteZona.intIDGerenteRegion;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeGerenteZona.intUsuarioCrea;
                cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 20).Value = obeGerenteZona.chrCodigoDataMart;

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

        public List<BeGerenteZona> GerenteZonaListarReporte(string prefijoIsoPais, string nombreCompleto, bool estado, string codigoRegion, string codigoZona, string periodo)
        {
            var obeGerenteZona = new List<BeGerenteZona>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_LISTAR_REPORTE_ALTA_BAJA", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit).Value = estado;
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = codigoRegion;
                cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 10).Value = codigoZona;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteZona = new BeGerenteZona
                        {
                            intIDGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                            intIDGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                            obeGerenteRegion = new BeGerenteRegion
                            {
                                VchNombrecompleto =
                                    dr.IsDBNull(dr.GetOrdinal("GerenteRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("GerenteRegion")),
                                ChrCodigoGerenteRegion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion"))
                            },
                            chrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            CodRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodRegion")),
                            obePais = new BePais
                            {
                                NombrePais =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombrePais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombrePais"))
                            },
                            chrCodigoGerenteZona =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")),
                            vchNombreCompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            vchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            EstadoGerente =
                                dr.IsDBNull(dr.GetOrdinal("Estado"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("Estado")),
                            chrCampaniaRegistro =
                                dr.IsDBNull(dr.GetOrdinal("chrCampaniaRegistro"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCampaniaRegistro")),
                            chrCampaniaBaja =
                                dr.IsDBNull(dr.GetOrdinal("chrCampaniaBaja"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCampaniaBaja")),
                            FechaBaja =
                                dr.IsDBNull(dr.GetOrdinal("FechaBaja"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("FechaBaja")),
                            DescripcionZona =
                                dr.IsDBNull(dr.GetOrdinal("DescripcionZona"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("DescripcionZona"))
                        };

                        obeGerenteZona.Add(ibeGerenteZona);
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

            return obeGerenteZona;
        }

        public List<BeGerenteZona> GerenteZonaListarReporteHistorico(string prefijoIsoPais, string codigoGerenteZona)
        {
            var obeGerenteZona = new List<BeGerenteZona>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_ZONA_LISTAR_HISTORICO", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10).Value = codigoGerenteZona;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteZona = new BeGerenteZona
                        {
                            CodRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCodRegion")),
                            codZona =
                                dr.IsDBNull(dr.GetOrdinal("chrCodZona"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCodZona")),
                            Periodo =
                                dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                            AnioCampana =
                                dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                            CodGerenteRegional =
                                dr.IsDBNull(dr.GetOrdinal("chrCodGerenteRegional"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCodGerenteRegional")),
                            PtoRankingProdCamp =
                                dr.IsDBNull(dr.GetOrdinal("intPtoRankingProdCamp"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intPtoRankingProdCamp")),
                            PtoRankingProdPeriodo =
                                dr.IsDBNull(dr.GetOrdinal("decPtoRankingProdPeriodo"))
                                    ? 0
                                    : dr.GetDecimal(dr.GetOrdinal("decPtoRankingProdPeriodo")),
                            EstadoPeriodo =
                                dr.IsDBNull(dr.GetOrdinal("vchEstadoCamp"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("vchEstadoCamp")),
                            FechaActualizacion =
                                dr.IsDBNull(dr.GetOrdinal("FechaActualizacion"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("FechaActualizacion"))
                        };

                        obeGerenteZona.Add(ibeGerenteZona);
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

            return obeGerenteZona;
        }

        public List<BeGerenteZona> ListaGz(string prefijoIsoPais)
        {
            var entidades = new List<BeGerenteZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeGerenteZona
                            {
                                intIDGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                                intIDGerenteRegion =
                                        dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                                chrPrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")).Trim(),
                                chrCodigoGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")).Trim(),
                                vchNombreCompleto =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto")).Trim(),
                                vchCorreoElectronico =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")).Trim(),
                                vchCUBGZ =
                                    dr.IsDBNull(dr.GetOrdinal("vchCUBGZ"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCUBGZ")).Trim(),
                                chrCodigoPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")).Trim(),
                                obeGerenteRegion =
                                    new BeGerenteRegion
                                    {
                                        VchNombrecompleto = dr.IsDBNull(dr.GetOrdinal("NombreGerenteRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("NombreGerenteRegion")).Trim()
                                    },
                                vchCodigoRegion =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoRegion")).Trim(),
                                vchCodigoZona =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoZona")).Trim(),
                                vchObservacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservacion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchObservacion")).Trim()
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

        public List<BeGerenteZona> ListaGzPaginacion(string sortColumnName, string sortOrderBy, string prefijoIsoPais, int pageNumber, int pageSize)
        {
            var entidades = new List<BeGerenteZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ_PAGINACION", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@sortColumnName", SqlDbType.NVarChar, 500).Value = sortColumnName;
                cmd.Parameters.Add("@sortOrderBy", SqlDbType.NVarChar, 500).Value = sortOrderBy;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var entidad = new BeGerenteZona
                            {
                                intIDGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                                intIDGerenteRegion =
                                    dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                                chrPrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")).Trim(),
                                chrCodigoGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")).Trim(),
                                vchNombreCompleto =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto")).Trim(),
                                vchCorreoElectronico =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")).Trim(),
                                vchCUBGZ =
                                    dr.IsDBNull(dr.GetOrdinal("vchCUBGZ"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCUBGZ")).Trim(),
                                chrCodigoPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")).Trim(),
                                vchCodigoRegion =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoRegion")).Trim(),
                                vchCodigoZona =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoZona")).Trim(),
                                vchObservacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservacion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchObservacion")).Trim(),
                                obeGerenteRegion = new BeGerenteRegion
                                {
                                    VchNombrecompleto =
                                       dr.IsDBNull(dr.GetOrdinal("NombreGerenteRegion"))
                                           ? string.Empty
                                           : dr.GetString(dr.GetOrdinal("NombreGerenteRegion"))
                                }
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

        public List<BeGerenteZona> ListaGzPaginacionBusqueda(string columnaBuscar, string valorBuscar, string operadorBuscar, string sortColumnName, string sortOrderBy, string prefijoIsoPais, int pageNumber, int pageSize)
        {
            var entidades = new List<BeGerenteZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ_PAGINACION_BUSQUEDA", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@columnaBuscar", SqlDbType.NVarChar, 1000).Value = columnaBuscar;
                cmd.Parameters.Add("@valorBuscar", SqlDbType.NVarChar, 1000).Value = valorBuscar.Trim();
                cmd.Parameters.Add("@operadorBuscar", SqlDbType.NVarChar, 1000).Value = operadorBuscar;
                cmd.Parameters.Add("@sortColumnName", SqlDbType.NVarChar, 500).Value = sortColumnName;
                cmd.Parameters.Add("@sortOrderBy", SqlDbType.NVarChar, 500).Value = sortOrderBy;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var entidad = new BeGerenteZona
                            {
                                intIDGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                                chrPrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")).Trim(),
                                chrCodigoGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")).Trim(),
                                vchNombreCompleto =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto")).Trim(),
                                vchCorreoElectronico =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")).Trim(),
                                vchCUBGZ =
                                    dr.IsDBNull(dr.GetOrdinal("vchCUBGZ"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCUBGZ")).Trim(),
                                chrCodigoPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")).Trim(),
                                vchCodigoRegion =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoRegion")).Trim(),
                                vchCodigoZona =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoZona")).Trim(),
                                vchObservacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservacion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchObservacion")).Trim(),
                                obeGerenteRegion = new BeGerenteRegion
                                {
                                    VchNombrecompleto =
                                       dr.IsDBNull(dr.GetOrdinal("NombreGerenteRegion"))
                                           ? string.Empty
                                           : dr.GetString(dr.GetOrdinal("NombreGerenteRegion"))
                                }
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

        public List<BeGerenteZona> ListaGzPaginacionBusquedaAvanzada(string filters, string sortColumnName, string sortOrderBy, string prefijoIsoPais, int pageNumber, int pageSize)
        {
            var entidades = new List<BeGerenteZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ_PAGINACION_BUSQUEDA_AVANZADA", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@filters", SqlDbType.NVarChar, 2000).Value = filters;
                cmd.Parameters.Add("@sortColumnName", SqlDbType.NVarChar, 500).Value = sortColumnName;
                cmd.Parameters.Add("@sortOrderBy", SqlDbType.NVarChar, 500).Value = sortOrderBy;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var entidad = new BeGerenteZona
                            {
                                intIDGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("intIDGerenteZona"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteZona")),
                                chrPrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")).Trim(),
                                chrCodigoGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")).Trim(),
                                vchNombreCompleto =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto")).Trim(),
                                vchCorreoElectronico =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")).Trim(),
                                vchCUBGZ =
                                    dr.IsDBNull(dr.GetOrdinal("vchCUBGZ"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCUBGZ")).Trim(),
                                chrCodigoPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")).Trim(),
                                vchCodigoRegion =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoRegion")).Trim(),
                                vchCodigoZona =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoZona"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoZona")).Trim(),
                                vchObservacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservacion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchObservacion")).Trim(),
                                obeGerenteRegion = new BeGerenteRegion
                                {
                                    VchNombrecompleto =
                                       dr.IsDBNull(dr.GetOrdinal("NombreGerenteRegion"))
                                           ? string.Empty
                                           : dr.GetString(dr.GetOrdinal("NombreGerenteRegion"))
                                }
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



        public int GetTotal(string prefijoIsoPais)
        {
            var total = 0;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ_TOTAL", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            total = dr.IsDBNull(dr.GetOrdinal("TOTAL")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTAL"));
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

            return total;
        }


        public int GetTotalBusqueda(string prefijoIsoPais, string columnaBuscar, string valorBuscar, string operadorBuscar)
        {
            var total = 0;


            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ_Total_BUSQUEDA", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@columnaBuscar", SqlDbType.NVarChar, 1000).Value = columnaBuscar;
                cmd.Parameters.Add("@valorBuscar", SqlDbType.NVarChar, 1000).Value = valorBuscar;
                cmd.Parameters.Add("@operadorBuscar", SqlDbType.NVarChar, 1000).Value = operadorBuscar;
                
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        //bool hay = dr.NextResult();
                        while (dr.Read())
                        {
                            total = dr.IsDBNull(dr.GetOrdinal("TOTAL")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTAL"));
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

            return total;
        }


        public int GetTotalBusquedaAvanzada(string filters, string prefijoIsoPais)
        {
            var total = 0;


            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GZ_TOTAL_BUSQUEDA_AVANZADA", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@filters", SqlDbType.NVarChar, 2000).Value = filters;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            total = dr.IsDBNull(dr.GetOrdinal("TOTAL")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTAL"));
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

            return total;
        }



        public bool DeleteGz(int id)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_ELIMINAR_GZ", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDGerenteZona", SqlDbType.Int);
                cmd.Parameters["@intIDGerenteZona"].Value = id;

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



        public bool AddGz(BeGerenteZona obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_INSERTAR_GZ", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoGerenteZona", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchCUBGZ", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchCodigoZona", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 200);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.chrPrefijoIsoPais;
                cmd.Parameters["@chrCodigoGerenteZona"].Value = obj.chrCodigoGerenteZona;
                cmd.Parameters["@vchNombreCompleto"].Value = obj.vchNombreCompleto;
                cmd.Parameters["@vchCorreoElectronico"].Value = obj.vchCorreoElectronico;
                cmd.Parameters["@vchCUBGZ"].Value = obj.vchCUBGZ;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.chrCodigoPlanilla;
                cmd.Parameters["@vchCodigoRegion"].Value = obj.vchCodigoRegion;
                cmd.Parameters["@vchCodigoZona"].Value = obj.vchCodigoZona;
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


        public bool EditGz(BeGerenteZona obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_ACTUALIZAR_GZ", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDGerenteZona", SqlDbType.Int);
                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoGerenteZona", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchCUBGR", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@vchCUBGZ", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchCodigoZona", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 200);

                cmd.Parameters["@intIDGerenteZona"].Value = obj.intIDGerenteZona;
                cmd.Parameters["@intIDGerenteRegion"].Value = obj.intIDGerenteRegion;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.chrPrefijoIsoPais;
                cmd.Parameters["@chrCodigoGerenteZona"].Value = obj.chrCodigoGerenteZona;
                cmd.Parameters["@vchNombreCompleto"].Value = obj.vchNombreCompleto;
                cmd.Parameters["@vchCorreoElectronico"].Value = obj.vchCorreoElectronico;
                cmd.Parameters["@vchCUBGR"].Value = obj.vchCUBGR;
                cmd.Parameters["@vchCUBGZ"].Value = obj.vchCUBGZ;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.chrCodigoPlanilla;
                cmd.Parameters["@vchCodigoRegion"].Value = obj.vchCodigoRegion;
                cmd.Parameters["@vchCodigoZona"].Value = obj.vchCodigoZona;
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


        public List<BeComun> ListarRegiones(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_OBTENER_REGION_POR_PAIS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2);
                cmd.Parameters["@CodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("Descripcion"))
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



        public List<BeComun> ListarPaises(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_LISTAR_PAIS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2);
                cmd.Parameters["@CodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("Descripcion"))
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