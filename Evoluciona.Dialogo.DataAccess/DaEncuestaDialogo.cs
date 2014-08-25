
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEncuestaDialogo : DaConexion
    {
        public List<BeComun> ListarPreguntas()
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_PREGUNTAS_LISTAR", cn) { CommandType = CommandType.StoredProcedure };

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

        public List<BeComun> ListarTipoEncuesta()
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_TIPO_LISTAR", cn) { CommandType = CommandType.StoredProcedure };

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

        public List<BeComun> ListarTipoRespuesta()
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_TIPO_RESPUESTA_LISTAR", cn) { CommandType = CommandType.StoredProcedure };

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


        public List<BeEncuestaDialogo> ListaEncuestaDialogo()
        {
            var entidades = new List<BeEncuestaDialogo>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_DIALOGO_LISTAR", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeEncuestaDialogo
                            {
                                IdEncuestaDialogo =
                                    dr.IsDBNull(dr.GetOrdinal("intIdEncuestaDialogo"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdEncuestaDialogo")),
                                DesPreguntas =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesPreguntas"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesPreguntas")).Trim(),
                                DesTipoEncuesta =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesTipoEncuesta"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesTipoEncuesta")).Trim(),
                                DesTipoRespuesta =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesTipoRespuesta"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesTipoRespuesta")).Trim(),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")).Trim()
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



        public bool DeleteEncuestaDialogo(int id)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_DIALOGO_ELIMINAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDEncuestaDialogo", SqlDbType.Int);
                cmd.Parameters["@intIDEncuestaDialogo"].Value = id;

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



        public bool AddEncuestaDialogo(BeEncuestaDialogo obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_DIALOGO_INSERTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdPregunta", SqlDbType.Int);
                cmd.Parameters.Add("@vchCodTipoEncuesta", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchCodTipoRespuesta", SqlDbType.VarChar, 3);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);


                cmd.Parameters["@intIdPregunta"].Value = obj.IdPregunta;
                cmd.Parameters["@vchCodTipoEncuesta"].Value = obj.CodTipoEncuesta;
                cmd.Parameters["@vchCodTipoRespuesta"].Value = obj.CodTipoRespuesta;
                cmd.Parameters["@chrPeriodo"].Value = obj.Periodo;

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


        public bool EditEncuestaDialogo(BeEncuestaDialogo obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_DIALOGO_ACTUALIZAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdEncuestaDialogo", SqlDbType.Int);
                cmd.Parameters.Add("@intIdPregunta", SqlDbType.Int);
                cmd.Parameters.Add("@vchCodTipoEncuesta", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchCodTipoRespuesta", SqlDbType.VarChar, 3);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@intIdEncuestaDialogo"].Value = obj.IdEncuestaDialogo;
                cmd.Parameters["@intIdPregunta"].Value = obj.IdPregunta;
                cmd.Parameters["@vchCodTipoEncuesta"].Value = obj.CodTipoEncuesta;
                cmd.Parameters["@vchCodTipoRespuesta"].Value = obj.CodTipoRespuesta;
                cmd.Parameters["@chrPeriodo"].Value = obj.Periodo;

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


        public List<BeEncuestaDialogo> ListaEncuestaCompletar(BeEncuestaDialogo obj)
        {
            var entidades = new List<BeEncuestaDialogo>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_DIALOGO_ENCUESTA_LISTAR", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@vchCodTipoEncuesta", SqlDbType.VarChar, 10);

                cmd.Parameters["@chrPeriodo"].Value = obj.Periodo;
                cmd.Parameters["@vchCodTipoEncuesta"].Value = obj.CodTipoEncuesta;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeEncuestaDialogo
                            {
                                IdEncuestaDialogo =
                                    dr.IsDBNull(dr.GetOrdinal("intIdEncuestaDialogo"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdEncuestaDialogo")),
                                DesPreguntas =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesPreguntas"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesPreguntas")).Trim(),
                                CodTipoEncuesta =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodTipoEncuesta"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodTipoEncuesta")).Trim(),
                                CodTipoRespuesta =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodTipoRespuesta"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchCodTipoRespuesta")).Trim(),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")).Trim()
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

        public List<BeEncuestaRespuesta> ListaOpcionesRspts()
        {
            var entidades = new List<BeEncuestaRespuesta>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_DIALOGO_ENCUESTA_OPCIONES_RSPTS", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeEncuestaRespuesta
                                              {
                                                  CodPuntaje =
                                                      dr.IsDBNull(dr.GetOrdinal("vchCodPuntaje"))
                                                          ? string.Empty
                                                          : dr.GetString(dr.GetOrdinal("vchCodPuntaje")).Trim(),
                                                  DesPuntaje =
                                                      dr.IsDBNull(dr.GetOrdinal("vchDesPuntaje"))
                                                          ? string.Empty
                                                          : dr.GetString(dr.GetOrdinal("vchDesPuntaje")).Trim(),
                                                  ValorPuntaje =
                                                      dr.IsDBNull(dr.GetOrdinal("intValorPuntaje"))
                                                          ? 0
                                                          : dr.GetInt32(dr.GetOrdinal("intValorPuntaje"))
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

        public bool InsertRspts(BeEncuestaFfvv obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_DIALOGO_ENCUESTA_RESPUESTA_DIALOGO", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@intIdEncuestaDialogo", SqlDbType.Int);
                cmd.Parameters.Add("@vchCodPuntaje", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchComentario", SqlDbType.VarChar, 400);
                cmd.Parameters.Add("@vchCodTipoEncuesta", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);


                cmd.Parameters["@intIdEncuestaDialogo"].Value = obj.IdEncuestaDialogo;
                cmd.Parameters["@vchCodPuntaje"].Value = obj.CodPuntaje;
                cmd.Parameters["@vchComentario"].Value = obj.Comentario;
                cmd.Parameters["@vchCodTipoEncuesta"].Value = obj.CodTipoEncuesta;
                cmd.Parameters["@chrCodigoUsuario"].Value = obj.CodigoUsuario;
                cmd.Parameters["@chrPeriodo"].Value = obj.Periodo;


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

        public int CantPorAprobarDv(BeUsuario obj)
        {
            int resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_DIALOGO_CANT_POR_APROBAR_DV", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intDiferencia", SqlDbType.Int);

                cmd.Parameters["@chrCodDirectorVenta"].Value = obj.codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = obj.periodoEvaluacion;
                cmd.Parameters["@intDiferencia"].Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@intDiferencia"].Value);
                }
                catch (Exception)
                {
                    resultado = 0;
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

        public int CantPorAprobarGr(BeUsuario obj)
        {
            int resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_DIALOGO_CANT_POR_APROBAR_GR", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intDiferencia", SqlDbType.Int);

                cmd.Parameters["@intIDGerenteRegion"].Value = obj.idUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = obj.periodoEvaluacion;
                cmd.Parameters["@chrCodigoGerenteRegion"].Value = obj.codigoUsuario;
                cmd.Parameters["@intDiferencia"].Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["@intDiferencia"].Value);
                }
                catch (Exception)
                {
                    resultado = 0;
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


        public bool LlenoEncuesta(BeUsuario obj, string codTipoEncuesta)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_DIALOGO_LLENO_ENCUESTA", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@vchCodTipoEncuesta", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEncuesta", SqlDbType.Bit);

                cmd.Parameters["@vchCodTipoEncuesta"].Value = codTipoEncuesta;
                cmd.Parameters["@chrCodigoUsuario"].Value = obj.codigoUsuario;
                cmd.Parameters["@chrPeriodo"].Value = obj.periodoEvaluacion;
                cmd.Parameters["@bitEncuesta"].Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["@bitEncuesta"].Value);
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
