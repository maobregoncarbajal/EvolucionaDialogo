
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaConfiguracion : DaConexion
    {
        /// <summary>
        /// Lista los paises activos
        /// </summary>
        /// <returns></returns>
        public DataTable SeleccionarPaises()
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_Paises", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Selecciona las Directoras de ventas para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable SeleccionarDVentasPorEvaluar(string prefijoIsoPais)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_DV_PorEvaluar", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Selecciona las Gerente de region para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable SeleccionarGRegionPorEvaluar(string prefijoIsoPais)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GR_PorEvaluar", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        public DataTable ObtenerEvaluadores(string prefijoIsoPais, int rol)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerEvaluadores", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@rol", SqlDbType.Int);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@rol"].Value = rol;
                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        public string ObtenerDocIdentidadMae(string prefijoIsoPais, int rol, string docIdentidad)
        {
            var dni = string.Empty;
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerDocIdentidadMae", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@rol", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@rol"].Value = rol;
                cmd.Parameters["@chrDocIdentidad"].Value = docIdentidad;

                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                        dni = dr.GetString(dr.GetOrdinal("documentoIdentidad"));
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conex.State == ConnectionState.Open)
                        conex.Close();
                }
            }
            return dni;
        }

        /// <summary>
        /// Selecciona las Gerente de zona para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="idGerenteRegion"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable SeleccionarGZonaPorEvaluar(int idGerenteRegion, string prefijoIsoPais)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GZ_PorEvaluar", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDGerenteRegion"].Value = idGerenteRegion;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        public DataTable SeleccionarGZonaPorPais(string prefijoIsoPais)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GZ_PorPais", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }


        /// <summary>
        /// Inserta el registro del usuario a ser evaluado
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="documentoIdentidad"></param>
        /// <param name="codigoRol"></param>
        /// <param name="enviado"></param>
        public void InsertarInicioDialogo(string prefijoIsoPais, string periodo, string documentoIdentidad, int codigoRol, byte enviado)
        {
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Insertar_InicioDialogoByUsuario", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrDocumentoIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrDocumentoIdentidad"].Value = documentoIdentidad;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = enviado;

                cmd.ExecuteNonQuery();
            }
        }



        public void ActualizarInicioDialogo(string prefijoIsoPais, string periodo, string documentoIdentidad, int codigoRol, byte enviado)
        {
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Actualizar_InicioDialogoByUsuario", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrDocumentoIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrDocumentoIdentidad"].Value = documentoIdentidad;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = enviado;

                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Inserta el registro para iniciar el proceso de Dialogo
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="indicadorEvaluados"></param>
        /// <returns></returns>
        public bool InsertarConfiguracionProceso(string prefijoIsoPais, string periodo, DateTime fechaInicio, string indicadorEvaluados)
        {
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Insertar_ProcesoInicioDialogo", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@datFechaInicio", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrIndicadorEvaluados", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@datFechaInicio"].Value = fechaInicio;
                cmd.Parameters["@chrIndicadorEvaluados"].Value = indicadorEvaluados;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        /// <summary>
        /// Obtiene el id del Inicio del proceso activo en un determinado Periodo
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="indicadorEvaluados"></param>
        /// <returns>el idProcesoInicio</returns>
        public int ValidarInicioProceso(string prefijoIsoPais, string periodo, string indicadorEvaluados)
        {
            var idProcesoInicio = 0;
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Obtener_ProcesoInicioDialogo", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrIndicadorEvaluados", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrIndicadorEvaluados"].Value = indicadorEvaluados;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    idProcesoInicio = Convert.ToInt32(dr["intIDProcesoInicio"]);
                }
                dr.Close();
            }

            return idProcesoInicio;
        }

        public DataTable SeleccionarPeriodo(string prefijoIsoPais)
        {
            var ds = new DataSet();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ObtenerPeriodo", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = prefijoIsoPais;

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
                    cn.Close();
                }
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }

        /// <summary>
        /// Lista los paises que han sido procesados con sus respectivos periodos
        /// </summary>
        /// <returns></returns>
        public DataTable SeleccionarPaisesProcesados()
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_PaisesConPeriodos_Procesados", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Retorna la lista con todos los directores de ventas
        /// </summary>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarDv()
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_DV", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["documentoIdentidad"].ToString(),
                        prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString()
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }

            return lstUsuarios;
        }

        /// <summary>
        /// Retorna la lista con los GR que han sido procesados por un DV en un determinado periodo, País
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarGRegionProcesadasPorDv(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo)
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GR_Procesadas_PorDV", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["chrCodigoUsuario"].ToString(),
                        estadoProceso = dr["chrEstadoProceso"].ToString()
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }

            return lstUsuarios;
        }

        /// <summary>
        /// Selecciona todos los Gerente de Region
        /// </summary>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarGRegion()
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GR", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["chrCodigoGerenteRegion"].ToString(),
                        prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString(),
                        idUsuario = Convert.ToInt32(dr["intIDGerenteRegion"].ToString())
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }

            return lstUsuarios;
        }

        /// <summary>
        /// Retorna la lista con los GZ que han sido procesados por las GR en un determinado periodo,País
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarGZonaProcesadasPorGr(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo)
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GZ_Procesadas_PorGR", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["chrCodigoUsuario"].ToString(),
                        estadoProceso = dr["chrEstadoProceso"].ToString(),
                        idUsuario = Convert.ToInt32(dr["intIDGerenteRegion"].ToString())
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }

            return lstUsuarios;
        }

        public List<BeUsuario> ObtenerGerentesVenta(string prefijoIsoPais)
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GR_PorEvaluar", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        idUsuario = dr.GetInt32(dr.GetOrdinal("IDUsuario")),
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["documentoIdentidad"].ToString(),
                        prefijoIsoPais = prefijoIsoPais
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }
            return lstUsuarios;
        }

        public List<BeUsuario> ObtenerGerentesZona(string codGerenteRegion, string prefijoIsoPais)
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_GZ", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodGerenteRegion", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodGerenteRegion"].Value = codGerenteRegion;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        idUsuario = dr.GetInt32(dr.GetOrdinal("IDUsuario")),
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["documentoIdentidad"].ToString(),
                        prefijoIsoPais = prefijoIsoPais
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }
            return lstUsuarios;
        }

        public List<BeUsuario> ObtenerLideres(string codGerenteZona, string prefijoIsoPais)
        {
            var lstUsuarios = new List<BeUsuario>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_LET", cn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@charCodGerenteZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@charCodGerenteZona"].Value = codGerenteZona;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        nombreUsuario = dr["vchNombreCompleto"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["documentoIdentidad"].ToString(),
                        prefijoIsoPais = prefijoIsoPais
                    };
                    lstUsuarios.Add(objUser);
                }
                dr.Close();
            }
            return lstUsuarios;
        }

        public DataTable ObtenerCorreosPlanesAcordados(string prefijoIsoPais, string periodo, int idRol, string estadoProceso)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_LISTAR_CORREOS_PLANES_ACORDADOS", conex)
                              {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }
            return dt;
        }


        public void InsertarLogEnvioCorreo(string tipoCorreo, string correo, string descripcion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("[ESE_SP_INSERT_LOG_ENVIO_CORREO]", conn) { CommandTimeout = 300, CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@vchTipoCorreo", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchCorreo", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchDescripcion", SqlDbType.VarChar, 500);

                cmd.Parameters["@vchTipoCorreo"].Value = tipoCorreo;
                cmd.Parameters["@vchCorreo"].Value = correo;
                cmd.Parameters["@vchDescripcion"].Value = descripcion;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                conn.Close();
            }
        }
    
    
    }
}