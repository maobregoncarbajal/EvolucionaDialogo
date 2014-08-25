
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaCritica : DaConexion
    {
        public DataSet Cargarcriticas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
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
            return ds;
        }

        public bool InsertarIndicadores(string dni, int intIdProceso, string variableConsiderar, int codigoRolUsuario, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_TRX_CRITICIDAD", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@DNI", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchVariableConsiderar", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);

                cmd.Parameters["@DNI"].Value = dni;
                cmd.Parameters["@intIDProceso"].Value = intIdProceso;
                cmd.Parameters["@vchVariableConsiderar"].Value = variableConsiderar;
                cmd.Parameters["@codigoRolUsuario"].Value = codigoRolUsuario;

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


        public bool InsertarIndicadoresPreDialogo(string dni, int intIdProceso, string variableConsiderar, int codigoRolUsuario, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_TRX_CRITICIDADPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@DNI", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchVariableConsiderar", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);

                cmd.Parameters["@DNI"].Value = dni;
                cmd.Parameters["@intIDProceso"].Value = intIdProceso;
                cmd.Parameters["@vchVariableConsiderar"].Value = variableConsiderar;
                cmd.Parameters["@codigoRolUsuario"].Value = codigoRolUsuario;

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


        public bool InsertarCriticasEvaluado(string dni, int intIdProceso, string variableConsiderar, int codigoRolUsuario, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_TRX_CRITICIDAD_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@DNI", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchVariableConsiderar", SqlDbType.VarChar, 600);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);

                cmd.Parameters["@DNI"].Value = dni;
                cmd.Parameters["@intIDProceso"].Value = intIdProceso;
                cmd.Parameters["@vchVariableConsiderar"].Value = variableConsiderar;
                cmd.Parameters["@codigoRolUsuario"].Value = codigoRolUsuario;

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

        public DataTable ValidarPeriodoEvaluacion(string periodoEvaluacion, string prefijoIsoPais, int codigoRolUsuario, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VALIDAR_PERIODOEVALUACIONGERENTEREGION", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);

                cmd.Parameters[0].Value = periodoEvaluacion;
                cmd.Parameters[1].Value = prefijoIsoPais;
                cmd.Parameters[2].Value = codigoRolUsuario;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
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
            return ds.Tables.Count == 0 ? null : ds.Tables[0];
        }

        #region CampaniaCritica

        public DataTable CargarCampaniasCriticas_GR(string codigoUsuarioProcesado, string periodo, string prefijoIsoPais, string connstring)
        {
            var dtCritica = new DataTable();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Cargar_Criticas_GR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrDocIdentidad"].Value = codigoUsuarioProcesado;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(dtCritica);
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
            return dtCritica;
        }

        public DataTable CargarCampaniasCriticas_GZ(string codigoUsuarioProcesado, string periodo, string prefijoIsoPais, string documentoEvaluador, string connstring)
        {
            var dtCritica = new DataTable();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Cargar_Criticas_GZ", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrDocIdentidadEvaluador", SqlDbType.Char, 20);

                cmd.Parameters["@chrDocIdentidad"].Value = codigoUsuarioProcesado;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrDocIdentidadEvaluador"].Value = documentoEvaluador;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(dtCritica);
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
            return dtCritica;
        }

        #endregion CampaniaCritica

        #region SeleccionarCriticas

        public List<BeCriticas> ListaCargarCriticas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, string anioCampana)
        {
            var lstCriticas = new List<BeCriticas>();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = anioCampana;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString()
                        };
                        lstCriticas.Add(objCriticas);
                    }
                    dr.Close();
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
            return lstCriticas;
        }

        public List<BeCriticas> ListaCargarCriticasProcesadas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            var lstProcesadas = new List<BeCriticas>();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas_Procesadas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@anioCampana", SqlDbType.Char, 6);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = idProceso;
                cmd.Parameters[5].Value = anioCampana;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            idCritica = Convert.ToInt32(dr["intIDCriticidad"].ToString()),
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString(),
                            variableConsiderar = dr["vchVariableConsiderar"].ToString()
                        };
                        lstProcesadas.Add(objCriticas);
                    }
                    dr.Close();
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
            return lstProcesadas;
        }


        public List<BeCriticas> ListaCargarCriticasProcesadasPreDialogo(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            var lstProcesadas = new List<BeCriticas>();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas_ProcesadasPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@anioCampana", SqlDbType.Char, 6);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = idProceso;
                cmd.Parameters[5].Value = anioCampana;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            idCritica = Convert.ToInt32(dr["intIDCriticidad"].ToString()),
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString(),
                            variableConsiderar = dr["vchVariableConsiderar"].ToString()
                        };
                        lstProcesadas.Add(objCriticas);
                    }
                    dr.Close();
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
            return lstProcesadas;
        }


        public List<BeCriticas> ListaCargarCriticasProcesadasEvaluado(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            var lstProcesadas = new List<BeCriticas>();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas_Procesadas_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@anioCampana", SqlDbType.Char, 6);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = idProceso;
                cmd.Parameters[5].Value = anioCampana;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            idCritica = Convert.ToInt32(dr["intIDCriticidad"].ToString()),
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString(),
                            variableConsiderar = dr["vchVariableConsiderar"].ToString()
                        };
                        lstProcesadas.Add(objCriticas);
                    }
                    dr.Close();
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
            return lstProcesadas;
        }

        public List<BeCriticas> ListaCargarCriticasProcesadasResumen(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            var lstProcesadas = new List<BeCriticas>();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas_Procesadas_Resumen", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@anioCampana", SqlDbType.Char, 6);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = idProceso;
                cmd.Parameters[5].Value = anioCampana;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            idCritica = Convert.ToInt32(dr["intIDCriticidad"].ToString()),
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString(),
                            variableConsiderar = dr["vchVariableConsiderar"].ToString()
                        };
                        lstProcesadas.Add(objCriticas);
                    }
                    dr.Close();
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
            return lstProcesadas;
        }

        public List<BeCriticas> ListaCargarCriticasProcesadasResumenEvaluado(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            var lstProcesadas = new List<BeCriticas>();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas_Procesadas_Resumen_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@anioCampana", SqlDbType.Char, 6);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = idProceso;
                cmd.Parameters[5].Value = anioCampana;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            idCritica = Convert.ToInt32(dr["intIDCriticidad"].ToString()),
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString(),
                            variableConsiderar = dr["vchVariableConsiderar"].ToString()
                        };
                        lstProcesadas.Add(objCriticas);
                    }
                    dr.Close();
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
            return lstProcesadas;
        }

        public DataTable CargarCriticasProcesadas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso)
        {
            var dtProcesadas = new DataTable();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCriticas_Procesadas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = idProceso;

                try
                {
                    var ad = new SqlDataAdapter(cmd);
                    ad.Fill(dtProcesadas);
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
            return dtProcesadas;
        }

        public void EliminarCritica(string documentoIdentidad, int idProceso, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Eliminar_TRX_CRITICIDAD", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@DNI", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters["@DNI"].Value = documentoIdentidad;
                cmd.Parameters["@intIDProceso"].Value = idProceso;

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
        }

        #endregion SeleccionarCriticas

        public DataTable ObtenerHistoricoPeriodosCriticidad(string codigoUsuarioEvaluador, string codigoUsuarioRequerido, TipoHistorial tipo)
        {
            var ds = new DataTable();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerHistoricoCriticidad", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrDocIdentidadEvaluada", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intTipoHistorial", SqlDbType.Int);

                cmd.Parameters[0].Value = codigoUsuarioEvaluador;
                cmd.Parameters[1].Value = codigoUsuarioRequerido;
                cmd.Parameters[2].Value = (int)tipo;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
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
            return ds;
        }

        public DataTable ObtenerHistoricoPeriodosCriticidadEval(string codigoUsuarioEvaluador, string codigoUsuarioRequerido, TipoHistorial tipo)
        {
            var ds = new DataTable();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerHistoricoCriticidadEval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrDocIdentidadEvaluada", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intTipoHistorial", SqlDbType.Int);

                cmd.Parameters[0].Value = codigoUsuarioEvaluador;
                cmd.Parameters[1].Value = codigoUsuarioRequerido;
                cmd.Parameters[2].Value = (int)tipo;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
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
            return ds;
        }

        public BeCriticas ObtenerCritica(int idProceso, string documentoIdentidad)
        {
            BeCriticas critica;

            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                critica = new BeCriticas();
                //
                var cmd = new SqlCommand("ESE_ObtenerVariableConsiderarPorEvaluado", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocumentoIdentidad", SqlDbType.Char, 20);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = documentoIdentidad;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    critica.idCritica = reader.GetInt32(reader.GetOrdinal("intIDCriticidad"));
                    critica.idProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso"));
                    critica.variableConsiderar = reader.GetString(reader.GetOrdinal("vchVariableConsiderar"));
                    critica.documentoIdentidad = reader.GetString(reader.GetOrdinal("chrDocumentoIdentidad"));
                }
            }

            return critica;
        }

        public List<BeCriticas> ListaEstadosEquipo(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string estadoPeriodo)
        {
            var lstCriticas = new List<BeCriticas>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_CargarEstadosEquipo", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 100);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;
                cmd.Parameters[4].Value = estadoPeriodo;

                try
                {
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var objCriticas = new BeCriticas
                            {
                                documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                                nombresCritica = dr["vchDesGerenteZona"].ToString()
                            };
                            lstCriticas.Add(objCriticas);
                        }
                    }
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
            return lstCriticas;
        }

        public List<BeCriticas> ListaSeguimientosEquipo(string dniUsuario, string dniUsuarioEvaluado, int rolUsuario, string periodo, string prefijoIsoPais)
        {
            var lstCriticas = new List<BeCriticas>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ObtenerSeguimientoPorPeriodo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrDocIdentidadEvaluado", SqlDbType.Char, 20);

                cmd.Parameters[0].Value = prefijoIsoPais;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = rolUsuario;
                cmd.Parameters[3].Value = dniUsuario;
                cmd.Parameters[4].Value = dniUsuarioEvaluado;

                try
                {
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var objCriticas = new BeCriticas
                            {
                                campania = dr.GetString(dr.GetOrdinal("Campanha")),
                                estadoCriticidad = dr.GetString(dr.GetOrdinal("Estado")),
                                Porcentaje = dr.IsDBNull(dr.GetOrdinal("Porcentaje"))
                                    ? (decimal?) null
                                    : dr.GetDecimal(dr.GetOrdinal("Porcentaje"))
                            };
                            lstCriticas.Add(objCriticas);
                        }
                    }
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
            return lstCriticas;
        }

        public List<BeCriticas> ListaCargarCriticasDisponibles(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais)
        {
            var lstCriticas = new List<BeCriticas>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerCriticasDisponibles", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoUsuarioProcesado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = codigoUsuarioProcesado;
                cmd.Parameters[1].Value = periodo;
                cmd.Parameters[2].Value = codigoRolUsuario;
                cmd.Parameters[3].Value = prefijoIsoPais;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var objCriticas = new BeCriticas
                        {
                            documentoIdentidad = dr["chrDocIdentidadGZ"].ToString(),
                            nombresCritica = dr["vchDesGerenteZona"].ToString()
                        };
                        lstCriticas.Add(objCriticas);
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
            return lstCriticas;
        }

        public void InsertarCriticidadEquipo(BeCriticas criticidadEquipo)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_CriticidadEquipo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchNombreEquipo", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchVariableConsiderar", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@vchPlanAccion", SqlDbType.VarChar, 2000);

                cmd.Parameters["@intIDProceso"].Value = criticidadEquipo.idProceso;
                cmd.Parameters["@vchNombreEquipo"].Value = criticidadEquipo.NombreEquipo;
                cmd.Parameters["@vchVariableConsiderar"].Value = criticidadEquipo.variableConsiderar;
                cmd.Parameters["@vchPlanAccion"].Value = criticidadEquipo.PlanAccion;

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
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public void ActualizarCriticidadEquipo(BeCriticas criticidadEquipo)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Actualizar_CriticidadEquipo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDEquipo", SqlDbType.Int);
                cmd.Parameters.Add("@vchNombreEquipo", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchVariableConsiderar", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@vchPlanAccion", SqlDbType.VarChar, 2000);

                cmd.Parameters["@intIDEquipo"].Value = criticidadEquipo.idCritica;
                cmd.Parameters["@vchNombreEquipo"].Value = criticidadEquipo.NombreEquipo;
                cmd.Parameters["@vchVariableConsiderar"].Value = criticidadEquipo.variableConsiderar;
                cmd.Parameters["@vchPlanAccion"].Value = criticidadEquipo.PlanAccion;

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
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public void EliminarCriticidadEquipo(int idEquipo)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Eliminar_CriticidadEquipo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDEquipo", SqlDbType.Int);

                cmd.Parameters["@intIDEquipo"].Value = idEquipo;

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
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public BeCriticas ObtenerCriticidadEquipo(int idEquipo)
        {
            BeCriticas equipo = null;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_CriticidadEquipo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDEquipo", SqlDbType.Int);

                cmd.Parameters[0].Value = idEquipo;

                try
                {
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            equipo = new BeCriticas
                            {
                                idCritica = dr.GetInt32(dr.GetOrdinal("intIDEquipo")),
                                idProceso = dr.GetInt32(dr.GetOrdinal("intIDProceso")),
                                NombreEquipo = dr.GetString(dr.GetOrdinal("vchNombreEquipo")),
                                variableConsiderar = dr.GetString(dr.GetOrdinal("vchVariableConsiderar")),
                                PlanAccion = dr.GetString(dr.GetOrdinal("vchPlanAccion"))
                            };
                        }
                    }
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
            return equipo;
        }

        public List<BeCriticas> ObtenerCriticidadEquipos(int idProceso)
        {
            var lstCriticas = new List<BeCriticas>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_CriticidadEquipos", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;

                try
                {
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var objCriticas = new BeCriticas
                            {
                                idCritica = dr.GetInt32(dr.GetOrdinal("intIDEquipo")),
                                idProceso = dr.GetInt32(dr.GetOrdinal("intIDProceso")),
                                NombreEquipo = dr.GetString(dr.GetOrdinal("vchNombreEquipo")),
                                variableConsiderar = dr.GetString(dr.GetOrdinal("vchVariableConsiderar")),
                                PlanAccion = dr.GetString(dr.GetOrdinal("vchPlanAccion"))
                            };

                            lstCriticas.Add(objCriticas);
                        }
                    }
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
            return lstCriticas;
        }
    }
}