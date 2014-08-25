
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaGerenteRegion : DaConexion
    {
        #region "BELCORP - ESE_DIALOGODESEMPENIO3"

        public List<BeGerenteRegion> ObtenerGr(int intUsuarioCrea, bool bitEstado)
        {
            var gerentes = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_Altas", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters[0].Value = intUsuarioCrea;
                cmd.Parameters[1].Value = bitEstado;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var gerente = new BeGerenteRegion
                        {
                            IntIDGerenteRegion = dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                            ChrCodigoGerenteRegion = dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion")),
                            ChrPrefijoIsoPais = dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            VchNombrecompleto = dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            VchCorreoElectronico = dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            BitEstado = dr.GetBoolean(dr.GetOrdinal("bitEstado")),
                            IntUsuarioCrea = dr.GetInt32(dr.GetOrdinal("intUsuarioCrea")),
                            ChrCodDirectorVenta = dr.GetString(dr.GetOrdinal("chrCodDirectorVenta")),
                            ChrCodigoDataMart = dr.GetString(dr.GetOrdinal("chrCodigoDataMart"))
                        };

                        gerentes.Add(gerente);
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

            return gerentes;
        }

        public List<BeGerenteRegion> ObtenerEvaluadores(int intUsuarioCrea, bool bitEstado, string prefijoIsoPais,
                                                        int rol, string dni)
        {
            var gerentes = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_Altas", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@rol", SqlDbType.Int);

                cmd.Parameters["@intUsuarioCrea"].Value = intUsuarioCrea;
                cmd.Parameters["@bitEstado"].Value = bitEstado;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrDocIdentidad"].Value = dni;
                cmd.Parameters["@rol"].Value = rol;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var gerente = new BeGerenteRegion
                        {
                            IntIDGerenteRegion = dr.IsDBNull(dr.GetOrdinal("ID")) ? 0 : dr.GetInt32(dr.GetOrdinal("ID")),
                            ChrCodigoGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("CODIGO")) ? "" : dr.GetString(dr.GetOrdinal("CODIGO")),
                            ChrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            VchNombrecompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            VchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            BitEstado = dr.GetBoolean(dr.GetOrdinal("bitEstado")),
                            IntUsuarioCrea =
                                dr.IsDBNull(dr.GetOrdinal("intUsuarioCrea"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intUsuarioCrea")),
                            ChrCodDirectorVenta =
                                dr.IsDBNull(dr.GetOrdinal("SUPERIOR")) ? "" : dr.GetString(dr.GetOrdinal("SUPERIOR")),
                            ChrCodigoDataMart =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDataMart"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDataMart"))
                        };

                        gerentes.Add(gerente);
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

            return gerentes;
        }

        public List<BeColaborador> ObtenerColaborador(string nombre, string codpais, int rol, string dni)
        {
            var colaboradores = new List<BeColaborador>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_OBTENER_COLABORADOR_MAE", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
                cmd.Parameters.Add("@CodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@Rol", SqlDbType.Int);
                cmd.Parameters.Add("@Doc", SqlDbType.Char, 20);

                cmd.Parameters["@Nombre"].Value = nombre;
                cmd.Parameters["@CodPais"].Value = codpais;
                cmd.Parameters["@Rol"].Value = rol;
                cmd.Parameters["@Doc"].Value = dni;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var colaborador = new BeColaborador
                        {
                            Documento =
                                dr.IsDBNull(dr.GetOrdinal("Documento")) ? "" : dr.GetString(dr.GetOrdinal("Documento")),
                            Nombre = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Nombre")),
                            Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? "" : dr.GetString(dr.GetOrdinal("Pais")),
                            Cargo = dr.IsDBNull(dr.GetOrdinal("Cargo")) ? "" : dr.GetString(dr.GetOrdinal("Cargo")),
                            Jefe = dr.IsDBNull(dr.GetOrdinal("Jefe")) ? "" : dr.GetString(dr.GetOrdinal("Jefe")),
                            DocJefe =
                                dr.IsDBNull(dr.GetOrdinal("DocJefe")) ? "" : dr.GetString(dr.GetOrdinal("DocJefe")),
                            CodPais =
                                dr.IsDBNull(dr.GetOrdinal("CodPais")) ? "" : dr.GetString(dr.GetOrdinal("CodPais")),
                            BitEstado = dr.GetBoolean(dr.GetOrdinal("Estado")),
                            Enlace = dr.IsDBNull(dr.GetOrdinal("Enlace")) ? "" : dr.GetString(dr.GetOrdinal("Enlace")),
                            Anio = dr.IsDBNull(dr.GetOrdinal("Anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("Anio")),
                            Cub = dr.IsDBNull(dr.GetOrdinal("CUB")) ? "" : dr.GetString(dr.GetOrdinal("CUB"))
                        };
                        colaboradores.Add(colaborador);
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

            return colaboradores;
        }

        public bool UpdateMaes(string prefijoIsoPais, int rol, string dni, string dniAnterior)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_UPDATE_MAES_ALTA", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrDocIdentidadAnt", SqlDbType.Char, 20);
                cmd.Parameters.Add("@rol", SqlDbType.Int);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrDocIdentidad"].Value = dni;
                cmd.Parameters["@chrDocIdentidadAnt"].Value = dniAnterior;
                cmd.Parameters["@rol"].Value = rol;

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

        public bool UpdateMaesBaja(string prefijoIsoPais, int rol, string dni)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_UPDATE_MAES_BAJA", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@rol", SqlDbType.Int);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrDocIdentidad"].Value = dni;
                cmd.Parameters["@rol"].Value = rol;

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

        public string ObtenerDescripcionRegion(int idProceso, string codigoUsuario, string periodo,
                                               string prefijoIsoPais)
        {
            var descripcion = string.Empty;

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_DescripcionRegion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@idProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@periodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@prefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = periodo;
                cmd.Parameters[3].Value = prefijoIsoPais;

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        descripcion = dr.GetString(dr.GetOrdinal("DesRegion"));
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

            return descripcion;
        }

        public string ObtenerDescripcionZona(int idProceso, string codigoUsuario, string periodo, string prefijoIsoPais)
        {
            var descripcion = string.Empty;

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_DescripcionZona", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@idProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@periodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@prefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = periodo;
                cmd.Parameters[3].Value = prefijoIsoPais;

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        descripcion = dr.GetString(dr.GetOrdinal("DesZona"));
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

            return descripcion;
        }

        public int ObtenerAnio()
        {
            var anio = 0;

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_OBTENER_FECHA", conn) {CommandType = CommandType.StoredProcedure};

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        anio = dr.GetInt32(dr.GetOrdinal("Fecha"));
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

            return anio;
        }

        #endregion "BELCORP - ESE_DIALOGODESEMPENIO3"

        #region "Mantenimiento BELCORP - DATAMART"

        //public List<beGerenteRegion> GerenteRegionListarID(string prefijoIsoPais, string codigoGerenteRegion)
        public int GerenteRegionListarId(string prefijoIsoPais, string codigoGerenteRegion)
        {
            var obeGerenteRegion = new List<BeGerenteRegion>();
            var respuesta = 0;
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_LISTAR_ID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20).Value = codigoGerenteRegion;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteRegion = new BeGerenteRegion
                        {
                            IntIDGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion"))
                        };

                        obeGerenteRegion.Add(ibeGerenteRegion);
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
            if (obeGerenteRegion.Count > 0)
                respuesta = obeGerenteRegion[0].IntIDGerenteRegion;
            return respuesta;
        }

        public List<BeGerenteRegion> GerenteRegionListar(string prefijoIsoPais, string nombreCompleto, string codigoGerenteRegion, string codigoDirectorVenta)
        {
            var obeGerenteRegion = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_LISTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20).Value = codigoGerenteRegion;
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20).Value = codigoDirectorVenta;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteRegion = new BeGerenteRegion
                        {
                            IntIDGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                            ChrCodigoGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion")),
                            ChrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            VchNombrecompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            VchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            ChrCodDirectorVenta =
                                dr.IsDBNull(dr.GetOrdinal("chrCodDirectorVenta"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodDirectorVenta")),
                            obeDirectoraVentas =
                                new BeDirectoraVentas
                                {
                                    vchNombreCompleto =
                                        dr.IsDBNull(dr.GetOrdinal("DirectorVentas"))
                                            ? string.Empty
                                            : dr.GetString(dr.GetOrdinal("DirectorVentas"))
                                },
                            ChrCodigoDataMart =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDataMart"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDataMart")),
                            IdAndCodigoGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("IdAndCodigoGerenteRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("IdAndCodigoGerenteRegion")),
                            CodigoGerenteRegionForDatamart =
                                dr.IsDBNull(dr.GetOrdinal("CodigoGerenteRegionForDatamart"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("CodigoGerenteRegionForDatamart")),
                            obePais = new BePais
                            {
                                CodigoPaisAdam =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPaisAdam"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPaisAdam"))
                            }
                        };

                        obeGerenteRegion.Add(ibeGerenteRegion);
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

            return obeGerenteRegion;
        }

        public List<BeGerenteRegion> GerenteRegionGetAll(string prefijoIsoPais, string nombreCompleto, string codigoRegion, string periodo)
        {
            var obeGerenteRegion = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_REGION_LISTAR_ALL", conn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = codigoRegion;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteRegion = new BeGerenteRegion
                        {
                            IntIDGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                            ChrCodigoGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion")),
                            ChrPrefijoIsoPais =
                                dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            VchNombrecompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            VchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            ChrCodDirectorVenta =
                                dr.IsDBNull(dr.GetOrdinal("chrCodDirectorVenta"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodDirectorVenta")),
                            ChrNombreCodDirectorVenta =
                                dr.IsDBNull(dr.GetOrdinal("DirectorVentas"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("DirectorVentas")),
                            AnioCampana =
                                dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrAnioCampana"))
                        };

                        obeGerenteRegion.Add(ibeGerenteRegion);
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

            return obeGerenteRegion;
        }

        public List<BeGerenteRegion> GerenteRegionListarAlta(string prefijoIsoPais, string anioCampania, string codigoRegion, string nombreCompleto, string periodo)
        {
            var entidades = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_LISTAR_ALTAS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = codigoRegion;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6).Value = anioCampania;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var entidad = new BeGerenteRegion
                        {
                            DocIdentidad =
                                dr.IsDBNull(dr.GetOrdinal("chrDocIdentidad"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrDocIdentidad")),
                            AnioCampana =
                                dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                            DesGerenteRegional =
                                dr.IsDBNull(dr.GetOrdinal("vchDesGerenteRegional"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchDesGerenteRegional")),
                            VchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            ChrCodigoDataMart =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoDataMart"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoDataMart")),
                            CodRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodRegion")),
                            VchCUBGR =
                                dr.IsDBNull(dr.GetOrdinal("vchCUBGR"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCUBGR"))
                        };

                        entidades.Add(entidad);
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

            return entidades;
        }

        public bool GerenteRegionRegistrar(List<BeGerenteRegion> listaGerenteRegion)
        {
            var resultado = true;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var tran = cn.BeginTransaction();

                try
                {
                    foreach (var gerenteRegion in listaGerenteRegion)
                    {
                        var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_INSERTAR_ALTA", cn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = tran
                        };

                        cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20).Value = gerenteRegion.DocIdentidad;
                        cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = gerenteRegion.ChrPrefijoIsoPais;
                        cmd.Parameters.Add("@vchDesGerenteRegional", SqlDbType.VarChar, 100).Value = gerenteRegion.DesGerenteRegional;
                        cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value = gerenteRegion.VchCorreoElectronico;
                        cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = gerenteRegion.IntUsuarioCrea;
                        cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20).Value = gerenteRegion.ChrCodDirectorVenta;
                        cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 20).Value = gerenteRegion.ChrCodigoDataMart;
                        cmd.Parameters.Add("@chrCampaniaRegistro", SqlDbType.Char, 6).Value = gerenteRegion.AnioCampana;
                        cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value = gerenteRegion.chrIndicadorMigrado;
                        cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = gerenteRegion.CodRegion;
                        cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = gerenteRegion.Periodo;
                        cmd.Parameters.Add("@vchCUBGR", SqlDbType.Char, 20).Value = gerenteRegion.VchCUBGR;
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

        public bool GerenteRegionRegistrar(BeGerenteRegion obeGerenteRegion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_INSERTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20).Value = obeGerenteRegion.ChrCodigoGerenteRegion;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeGerenteRegion.ChrPrefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = obeGerenteRegion.VchNombrecompleto;
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value = obeGerenteRegion.VchCorreoElectronico;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeGerenteRegion.IntUsuarioCrea;
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20).Value = obeGerenteRegion.ChrCodDirectorVenta;
                cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 20).Value = obeGerenteRegion.ChrCodigoDataMart;
                cmd.Parameters.Add("@chrCampaniaRegistro", SqlDbType.Char, 6).Value = obeGerenteRegion.chrCampaniaRegistro;
                cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value = obeGerenteRegion.chrIndicadorMigrado;
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

        public bool GerenteRegionActualizar(BeGerenteRegion obeGerenteRegion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20).Value = obeGerenteRegion.ChrCodigoGerenteRegion;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeGerenteRegion.ChrPrefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = obeGerenteRegion.VchNombrecompleto;
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100).Value = obeGerenteRegion.VchCorreoElectronico;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeGerenteRegion.IntUsuarioCrea;
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20).Value = obeGerenteRegion.ChrCodDirectorVenta;
                cmd.Parameters.Add("@chrCodigoDataMart", SqlDbType.Char, 20).Value = obeGerenteRegion.ChrCodigoDataMart;
                cmd.Parameters.Add("@chrCampaniaRegistro", SqlDbType.Char, 6).Value = obeGerenteRegion.chrCampaniaRegistro;
                cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value = obeGerenteRegion.chrIndicadorMigrado;

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

        public bool GerenteRegionActualizarEstado(BeGerenteRegion obeGerenteRegion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_ACTUALIZAR_ESTADO", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int).Value = obeGerenteRegion.IntIDGerenteRegion;
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20).Value =
                    obeGerenteRegion.ChrCodDirectorVenta;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeGerenteRegion.IntUsuarioCrea;
                cmd.Parameters.Add("@chrIndicadorMigrado", SqlDbType.Char, 1).Value =
                    obeGerenteRegion.chrIndicadorMigrado;
                cmd.Parameters.Add("@chrCampaniaBaja", SqlDbType.Char, 6).Value =
                    obeGerenteRegion.chrCampaniaBaja;

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

        public List<BeGerenteRegion> GerenteRegionListarReporte(string prefijoIsoPais, string nombreCompleto, bool estado, string codigoRegion, string periodo)
        {
            var listaGerenteRegion = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_LISTAR_REPORTE_ALTA_BAJA", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100).Value = nombreCompleto;
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit).Value = estado;
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5).Value = codigoRegion;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var gerenteRegion = new BeGerenteRegion
                        {
                            IntIDGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                    ? 0
                                    : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                            ChrCodigoGerenteRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion")),
                            ChrPrefijoIsoPais =
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
                            VchNombrecompleto =
                                dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchNombrecompleto")),
                            VchCorreoElectronico =
                                dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                            ChrCodDirectorVenta =
                                dr.IsDBNull(dr.GetOrdinal("chrCodDirectorVenta"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodDirectorVenta")),
                            obeDirectoraVentas =
                                new BeDirectoraVentas
                                {
                                    vchNombreCompleto =
                                        dr.IsDBNull(dr.GetOrdinal("DirectorVentas"))
                                            ? string.Empty
                                            : dr.GetString(dr.GetOrdinal("DirectorVentas"))
                                },
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
                            DescripcionRegion =
                                dr.IsDBNull(dr.GetOrdinal("DescripcionRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("DescripcionRegion"))
                        };

                        listaGerenteRegion.Add(gerenteRegion);
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

            return listaGerenteRegion;
        }

        public List<BeGerenteRegion> GerenteRegionListarReporteHistorico(string prefijoIsoPais, string codigoGerenteRegion)
        {
            var obeGerenteRegion = new List<BeGerenteRegion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_GERENTE_REGION_LISTAR_HISTORICO", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10).Value = codigoGerenteRegion;
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ibeGerenteRegion = new BeGerenteRegion
                        {
                            CodRegion =
                                dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodRegion")),
                            codZona =
                                dr.IsDBNull(dr.GetOrdinal("chrCodZona"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrCodZona")),
                            Periodo =
                                dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                            AnioCampana =
                                dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                            CodGerenteRegional =
                                dr.IsDBNull(dr.GetOrdinal("chrCodGerenteRegional"))
                                    ? string.Empty
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
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("vchEstadoCamp")),
                            FechaActualizacion =
                                dr.IsDBNull(dr.GetOrdinal("FechaActualizacion"))
                                    ? string.Empty
                                    : dr.GetString(dr.GetOrdinal("FechaActualizacion"))
                        };

                        obeGerenteRegion.Add(ibeGerenteRegion);
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

            return obeGerenteRegion;
        }

        #endregion "Mantenimiento BELCORP - DATAMART"

        public List<BeGerenteRegion> ListaGr(string prefijoIsoPais)
        {
            var entidades = new List<BeGerenteRegion>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MANT_USU_LISTAR_GR", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeGerenteRegion
                            {
                                IntIDGerenteRegion =
                                    dr.IsDBNull(dr.GetOrdinal("intIDGerenteRegion"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIDGerenteRegion")),
                                ChrCodigoGerenteRegion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion")).Trim(),
                                ChrPrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                VchNombrecompleto =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto")),
                                VchCorreoElectronico =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCorreoElectronico")),
                                ChrCodDirectorVenta =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodDirectorVenta"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodDirectorVenta")),
                                VchCUBGR =
                                    dr.IsDBNull(dr.GetOrdinal("vchCUBGR"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCUBGR")),
                                ChrCodigoPlanilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")),
                                VchCodigoRegion =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoRegion"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodigoRegion")),
                                VchObservacion =
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

        public bool DeleteGr(int id)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_ELIMINAR_GR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters["@intIDGerenteRegion"].Value = id;

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

        public bool AddGr(BeGerenteRegion obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_INSERTAR_GR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchCUBGR", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 200);

                cmd.Parameters["@chrCodigoGerenteRegion"].Value = obj.ChrCodigoGerenteRegion;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.ChrPrefijoIsoPais;
                cmd.Parameters["@vchNombreCompleto"].Value = obj.VchNombrecompleto;
                cmd.Parameters["@vchCorreoElectronico"].Value = obj.VchCorreoElectronico;
                cmd.Parameters["@chrCodDirectorVenta"].Value = obj.ChrCodDirectorVenta;
                cmd.Parameters["@vchCUBGR"].Value = obj.VchCUBGR;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.ChrCodigoPlanilla;
                cmd.Parameters["@vchCodigoRegion"].Value = obj.VchCodigoRegion;
                cmd.Parameters["@vchObservacion"].Value = obj.VchObservacion;

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

        public bool EditGr(BeGerenteRegion obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MANT_USU_ACTUALIZAR_GR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchCUBGR", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar, 200);

                cmd.Parameters["@intIDGerenteRegion"].Value = obj.IntIDGerenteRegion;
                cmd.Parameters["@chrCodigoGerenteRegion"].Value = obj.ChrCodigoGerenteRegion;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.ChrPrefijoIsoPais;
                cmd.Parameters["@vchNombreCompleto"].Value = obj.VchNombrecompleto;
                cmd.Parameters["@vchCorreoElectronico"].Value = obj.VchCorreoElectronico;
                cmd.Parameters["@chrCodDirectorVenta"].Value = obj.ChrCodDirectorVenta;
                cmd.Parameters["@vchCUBGR"].Value = obj.VchCUBGR;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.ChrCodigoPlanilla;
                cmd.Parameters["@vchCodigoRegion"].Value = obj.VchCodigoRegion;
                cmd.Parameters["@vchObservacion"].Value = obj.VchObservacion;

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
                var cmd = new SqlCommand("ESE_OBTENER_REGION_POR_PAIS", cn)
                                     {CommandType = CommandType.StoredProcedure};
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