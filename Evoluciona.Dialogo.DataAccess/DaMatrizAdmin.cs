
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaMatrizAdmin : DaConexion
    {
        #region CronogramaMatriz

        /// <summary>
        /// Este método lista los cronogramas de la Tabla ESE_MAE_CRONOGRAMA_MATRIZ segun el pais usuario logeado
        /// </summary>
        /// <returns>cronogramas</returns>
        public List<BeCronogramaMatriz> ListaCronogramaMatriz(string pais)
        {
            var entidades = new List<BeCronogramaMatriz>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_SELECT_ESE_MAE_CRONOGRAMA_MATRIZ", cn) { CommandType = CommandType.StoredProcedure };

                if (!String.IsNullOrEmpty(pais)) {
                    cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                    cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                }
                
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeCronogramaMatriz
                            {
                                IdCronogramaMatriz =
                                    dr.IsDBNull(dr.GetOrdinal("intSEQIDCronogramaMatriz"))
                                        ? default(int)
                                        : dr.GetInt32(dr.GetOrdinal("intSEQIDCronogramaMatriz")),
                                PrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                FechaLimite =
                                    dr.IsDBNull(dr.GetOrdinal("datFechaLimite"))
                                        ? default(DateTime?)
                                        : dr.GetDateTime(dr.GetOrdinal("datFechaLimite")),
                                FechaProrroga =
                                    dr.IsDBNull(dr.GetOrdinal("datFechaProrroga"))
                                        ? default(DateTime?)
                                        : dr.GetDateTime(dr.GetOrdinal("datFechaProrroga")),
                                EsUltimo =
                                    dr.IsDBNull(dr.GetOrdinal("bitEsUltimo"))
                                        ? default(bool)
                                        : dr.GetBoolean(dr.GetOrdinal("bitEsUltimo"))
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


        public bool DeleteCronogramaMatriz(int idCronogramaMatriz)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_DELETE_ESE_MAE_CRONOGRAMA_MATRIZ", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intSEQIDCronogramaMatriz", SqlDbType.Int);

                cmd.Parameters["@intSEQIDCronogramaMatriz"].Value = idCronogramaMatriz;

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


        public bool UpdateCronogramaMatriz(BeCronogramaMatriz obj)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_UPDATE_ESE_MAE_CRONOGRAMA_MATRIZ", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intSEQIDCronogramaMatriz", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char,2);
                cmd.Parameters.Add("@datFechaLimite", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaProrroga", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrUsuarioModi", SqlDbType.Char,20);
                cmd.Parameters.Add("@EsUltimo", SqlDbType.Bit);

                cmd.Parameters["@intSEQIDCronogramaMatriz"].Value = obj.IdCronogramaMatriz;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.PrefijoIsoPais;
                cmd.Parameters["@datFechaLimite"].Value = obj.FechaLimite;
                cmd.Parameters["@datFechaProrroga"].Value = obj.FechaProrroga;
                cmd.Parameters["@chrUsuarioModi"].Value = obj.UsuarioModi;
                cmd.Parameters["@EsUltimo"].Value = obj.EsUltimo;

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

        public bool InsertCronogramaMatriz(BeCronogramaMatriz obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_INSERT_ESE_MAE_CRONOGRAMA_MATRIZ", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char,2);
                cmd.Parameters.Add("@datFechaLimite", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaProrroga", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrUsuarioCrea", SqlDbType.Char,20);
                cmd.Parameters.Add("@bitEsUltimo", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.PrefijoIsoPais;
                cmd.Parameters["@datFechaLimite"].Value = obj.FechaLimite;
                cmd.Parameters["@datFechaProrroga"].Value = obj.FechaProrroga;
                cmd.Parameters["@chrUsuarioCrea"].Value = obj.UsuarioCrea;
                cmd.Parameters["@bitEsUltimo"].Value = obj.EsUltimo;

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

        public BeComun ObtenerPais(string codigoPais)
        {
            var entidad = new BeComun();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_OBTENER_PAIS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codigoPais;

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        entidad.Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo")) ? default(string) : dr.GetString(dr.GetOrdinal("Codigo"));
                        entidad.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? default(string) : dr.GetString(dr.GetOrdinal("Descripcion"));
                    }

                    dr.Close();

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

            return entidad;
        }


        public List<BeComun> ObtenerPaises()
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_SELECCIONAR_PAISES", cn) { CommandType = CommandType.StoredProcedure };
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
                                    dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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

        public BeCronogramaMatriz SelectCronograma(int id)
        {
            var entidad = new BeCronogramaMatriz();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_SELECT_CRONOGRAMA_MATRIZ", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@intSEQIDCronogramaMatriz", SqlDbType.Int);
                cmd.Parameters["@intSEQIDCronogramaMatriz"].Value = id;

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {                        
                        entidad.IdCronogramaMatriz = dr.IsDBNull(dr.GetOrdinal("intSEQIDCronogramaMatriz")) ? default(int) : dr.GetInt32(dr.GetOrdinal("intSEQIDCronogramaMatriz"));
                        entidad.PrefijoIsoPais = dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais")) ? default(string) : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais"));
                        //entidad.Periodo = dr.IsDBNull(dr.GetOrdinal("chrPeriodo")) ? default(string) : dr.GetString(dr.GetOrdinal("chrPeriodo"));
                        entidad.FechaLimite = dr.IsDBNull(dr.GetOrdinal("datFechaLimite")) ? default(DateTime?) : dr.GetDateTime(dr.GetOrdinal("datFechaLimite"));
                        entidad.FechaProrroga = dr.IsDBNull(dr.GetOrdinal("datFechaProrroga")) ? default(DateTime?) : dr.GetDateTime(dr.GetOrdinal("datFechaProrroga"));
                        entidad.FechaServer = dr.IsDBNull(dr.GetOrdinal("datFechaServer")) ? default(DateTime?) : dr.GetDateTime(dr.GetOrdinal("datFechaServer"));
                    }

                    dr.Close();

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

            return entidad;
        }


        public BeCronogramaMatriz ObtenerFechaServer()
        {
            var entidad = new BeCronogramaMatriz();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MATRIZ_ADMIN_SELECT_FECHA_SERVIDOR", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        entidad.FechaServer = dr.IsDBNull(dr.GetOrdinal("datFechaServer")) ? default(DateTime?) : dr.GetDateTime(dr.GetOrdinal("datFechaServer"));
                    }

                    dr.Close();

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

            return entidad;
        }


        #endregion

        #region NivelCompetencia
        public List<BeNivelesCompetencia> ObtenerNivelesCompetencia(string prefijoIsoPais, string anio, int estado)
        {
            var nivelesCompetencia = new List<BeNivelesCompetencia>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_Buscar_NivelesCompetencia", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int, 4);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrAnio"].Value = anio;
                cmd.Parameters["@intEstadoActivo"].Value = estado;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var nivelCompetencia = new BeNivelesCompetencia
                        {
                            prefijoIsoPais = dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            anio = dr.GetString(dr.GetOrdinal("chrAnio")),
                            MaxValue = dr.GetDecimal(dr.GetOrdinal("decPorcentajeMaximo")),
                            MinValue = dr.GetDecimal(dr.GetOrdinal("decPorcentajeMinimo")),
                            CodCompetencia = dr.GetString(dr.GetOrdinal("chrCodCompetencia"))
                        };

                        nivelesCompetencia.Add(nivelCompetencia);
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

            return nivelesCompetencia;
        }

        public bool InsertarNivelesCompetencia(BeNivelesCompetencia be, string usuario)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_MATRIZ_Insertar_NivelesCompetencia", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@decPorcentajeMaximo", SqlDbType.Decimal, 9);
                cmd.Parameters.Add("@decPorcentajeMinimo", SqlDbType.Decimal, 9);
                cmd.Parameters.Add("@chrCodCompetencia", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = be.prefijoIsoPais;
                cmd.Parameters["@chrAnio"].Value = be.anio;
                cmd.Parameters["@chrUsuario"].Value = usuario;
                cmd.Parameters["@decPorcentajeMaximo"].Value = be.MaxValue;
                cmd.Parameters["@decPorcentajeMinimo"].Value = be.MinValue;
                cmd.Parameters["@chrCodCompetencia"].Value = be.CodCompetencia;
                cmd.Parameters["@intEstadoActivo"].Value = be.estadoActivo;

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
        #endregion

        #region "Matriz Zona"


       public List<BeComun> ObtenerPaisConFuenteVentas()
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MZ_ADMIN_OBTENER_PAIS_CON_FUENTE_VENTAS", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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


        #endregion
    }
}
