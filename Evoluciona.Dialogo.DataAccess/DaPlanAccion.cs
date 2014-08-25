
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaPlanAccion : DaConexion
    {
        // Cargar Datos Obtener las Gerente de Zona Criticas
        public DataTable ObtenerCriticasGerenteZona2(string connstring, BeUsuario beUsuario, BeResumenProceso objResumen, string periodoCerrado)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_CriticasGerenteZonaPlanAccion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                //cmd.Parameters["@chrPeriodo"].Value = beUsuario.periodoEvaluacion;
                cmd.Parameters["@chrPeriodo"].Value = periodoCerrado;
                cmd.Parameters[1].Value = objResumen.prefijoIsoPais;
                cmd.Parameters["@chrCodGerenteRegional"].Value = objResumen.codigoUsuario;
                cmd.Parameters["@intIDProceso"].Value = objResumen.idProceso;

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
            return ds.Tables[0];
        }

        public List<BePlanAccion> ObtenerCriticas(BeUsuario beUsuario, BeResumenProceso objResumen, string periodoCerrado, int rolLogueado)
        {
            var planesAccion = new List<BePlanAccion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                SqlCommand cmd;

                if (rolLogueado == Constantes.RolGerenteRegion)
                {
                    #region Evaluar Gerentes de Zona

                    cmd = new SqlCommand("ESE_Obtener_CriticasGerenteZonaPlanAccion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                    cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                    cmd.Parameters["@chrPeriodo"].Value = periodoCerrado;
                    cmd.Parameters[1].Value = objResumen.prefijoIsoPais;
                    cmd.Parameters["@chrCodGerenteRegional"].Value = objResumen.codigoUsuario;
                    cmd.Parameters["@intIDProceso"].Value = objResumen.idProceso;

                    #endregion Evaluar Gerentes de Zona
                }
                else
                {
                    #region Evaluar Lets

                    cmd = new SqlCommand("ESE_Obtener_CriticasLetsPlanAccion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                    cmd.Parameters["@chrPeriodo"].Value = periodoCerrado;
                    cmd.Parameters["@chrCodGerenteZona"].Value = objResumen.codigoUsuario;
                    cmd.Parameters["@intIDProceso"].Value = objResumen.idProceso;

                    #endregion Evaluar Lets
                }

                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var planAccion = new BePlanAccion();

                        if (!reader.IsDBNull(reader.GetOrdinal("CodCritica")))
                            planAccion.DocuIdentidad = reader.GetString(reader.GetOrdinal("CodCritica"));

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDPlanAccion")))
                            planAccion.idPlanAcccion = reader.GetInt32(reader.GetOrdinal("intIDPlanAccion"));

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDProceso")))
                            planAccion.IDProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso"));

                        if (!reader.IsDBNull(reader.GetOrdinal("nombreCritica")))
                            planAccion.NombreCritica = reader.GetString(reader.GetOrdinal("nombreCritica"));

                        if (!reader.IsDBNull(reader.GetOrdinal("PlanAccion")))
                            planAccion.PlanAccion = reader.GetString(reader.GetOrdinal("PlanAccion"));

                        if (!reader.IsDBNull(reader.GetOrdinal("PreDialogo")))
                            planAccion.PreDialogo = bool.Parse(reader["PreDialogo"].ToString());

                        planesAccion.Add(planAccion);
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
            return planesAccion;
        }

        // Cargar Datos Obtener las lets Criticas*****
        public DataTable ObtenerCriticasLets2(string connstring, BeUsuario beUsuario, BeResumenProceso objResumen, string periodoCerrado)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_CriticasLetsPlanAccion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                //cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters["@chrPeriodo"].Value = periodoCerrado;
                //cmd.Parameters[1].Value = objResumen.prefijoIsoPais;
                cmd.Parameters["@chrCodGerenteZona"].Value = objResumen.codigoUsuario;
                cmd.Parameters["@intIDProceso"].Value = objResumen.idProceso;

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
            return ds.Tables[0];
        }

        // Ingresar Plan accion  ingresarPlanAccion
        public bool IngresarPlanAccion(BePlanAccion bePlanAccion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarPlanAccionCriticas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocuIdentidad", SqlDbType.Char, 20);

                cmd.Parameters.Add("@vchPlanAccion", SqlDbType.VarChar, 600);
                cmd.Parameters.Add("@bitPreDialogo", SqlDbType.Bit);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);

                cmd.Parameters[0].Value = bePlanAccion.IDProceso;
                cmd.Parameters[1].Value = bePlanAccion.DocuIdentidad;

                cmd.Parameters[2].Value = bePlanAccion.PlanAccion;
                cmd.Parameters[3].Value = bePlanAccion.PreDialogo;
                cmd.Parameters[4].Value = bePlanAccion.idUsuario;

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

        // Obtener Criticas grabadas
        public DataTable ObtenerCriticasGrabadas(string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_obtenerCriticasGrabadas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

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
            return ds.Tables[0];
        }

        // Buscar Plan de Accion Criticas
        public DataTable ObtenerPlanAccionCriticas(string connstring, BeResumenProceso beResumenProceso)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SP_ObtenerPlanAccionCriticas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = beResumenProceso.idProceso;

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
            return ds.Tables[0];
        }

        //Actualizar Plan Acciion
        public bool ActualizarPlanAccion(BePlanAccion bePlanAccion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ActualizarPlanAccionCriticas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocuIdentidad", SqlDbType.Char, 20);

                cmd.Parameters.Add("@vchPlanAccion", SqlDbType.VarChar, 600);
                cmd.Parameters.Add("@bitPreDialogo", SqlDbType.Bit);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);

                cmd.Parameters[0].Value = bePlanAccion.IDProceso;
                cmd.Parameters[1].Value = bePlanAccion.DocuIdentidad;

                cmd.Parameters[2].Value = bePlanAccion.PlanAccion;
                cmd.Parameters[3].Value = bePlanAccion.PreDialogo;
                cmd.Parameters[4].Value = bePlanAccion.idUsuario;

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

        // Listar Gerente de Zona Criticas (No lo uitlizo)
        public DataTable ObtenerCriticasGerenteZona(string connstring, BeUsuario beUsuario, BeResumenProceso objResumen)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SP_ObtenerCriticasGerenteZona", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char);

                cmd.Parameters[0].Value = beUsuario.periodoEvaluacion;
                cmd.Parameters[1].Value = objResumen.codigoUsuario;

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
            return ds.Tables[0];
        }

        // Listar Lets Criticas (No lo uitlizo)
        public DataTable ObtenerCriticasLets(string connstring, BeUsuario beUsuario, BeResumenProceso beResumenProceso)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SP_ObtenerCriticasLets", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char);

                cmd.Parameters[0].Value = beUsuario.periodoEvaluacion;
                cmd.Parameters[1].Value = beResumenProceso.prefijoIsoPais;
                cmd.Parameters[2].Value = beResumenProceso.codigoGZona;

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
            return ds.Tables[0];
        }

        // Para Saber el Plan Cerrado
        public DataTable ValidarPeriodoEvaluacion(string connstring, string periodoEvaluacion, string prefijoIsoPais)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VALIDAR_PERIODOEVALUACIONGERENTEZONA", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = periodoEvaluacion;

                cmd.Parameters[1].Value = prefijoIsoPais;

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
            return ds.Tables[0];
        }

        public bool InsertarPlanAccionVisita(BePlanAccion bePlanAccion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_PlanAccionCriticasVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intIDPlanAcccion", SqlDbType.Int);
                cmd.Parameters.Add("@bitPostVisita", SqlDbType.Bit);
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDVisita"].Value = bePlanAccion.idVisita;
                cmd.Parameters["@intIDPlanAcccion"].Value = bePlanAccion.idPlanAcccion;

                cmd.Parameters["@bitPostVisita"].Value = bePlanAccion.postVisita;
                cmd.Parameters["@intUsuarioCrea"].Value = bePlanAccion.idUsuario;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

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

        public bool ActualizarPlanAccionVisita(BePlanAccion bePlanAccion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Actualizar_PlanAccionCriticasVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDPlanAcccionVisita", SqlDbType.Int);
                cmd.Parameters.Add("@bitPostVisita", SqlDbType.Bit);

                cmd.Parameters["@intIDPlanAcccionVisita"].Value = bePlanAccion.idPlanAccionVisita;
                cmd.Parameters["@bitPostVisita"].Value = bePlanAccion.postVisita;

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

        public List<BePlanAccion> ObtenerCriticas_Visita(BeUsuario beUsuario, BeResumenProceso objResumen, string periodoCerrado, int rolLogueado, int idVisita)
        {
            var planesAccion = new List<BePlanAccion>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                SqlCommand cmd;

                if (rolLogueado == Constantes.RolGerenteRegion)
                {
                    #region Evaluar Gerentes de Zona

                    cmd = new SqlCommand("ESE_Obtener_CriticasGerenteZonaPlanAccionVisita", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                    cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);

                    cmd.Parameters["@chrPeriodo"].Value = periodoCerrado;
                    cmd.Parameters[1].Value = objResumen.prefijoIsoPais;
                    cmd.Parameters["@chrCodGerenteRegional"].Value = objResumen.codigoUsuario;
                    cmd.Parameters["@intIDProceso"].Value = objResumen.idProceso;
                    cmd.Parameters["@intIDVisita"].Value = idVisita;

                    #endregion Evaluar Gerentes de Zona
                }
                else
                {
                    #region Evaluar Lets

                    cmd = new SqlCommand("ESE_Obtener_CriticasLetsPlanAccionVisita", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);

                    cmd.Parameters["@chrPeriodo"].Value = periodoCerrado;
                    cmd.Parameters["@chrCodGerenteZona"].Value = objResumen==null?"":objResumen.codigoUsuario;
                    cmd.Parameters["@intIDProceso"].Value = objResumen==null?0:objResumen.idProceso;
                    cmd.Parameters["@intIDVisita"].Value = idVisita;

                    #endregion Evaluar Lets
                }

                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var planAccion = new BePlanAccion();

                        if (!reader.IsDBNull(reader.GetOrdinal("CodCritica")))
                            planAccion.DocuIdentidad = reader.GetString(reader.GetOrdinal("CodCritica"));

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDPlanAccion")))
                            planAccion.idPlanAcccion = reader.GetInt32(reader.GetOrdinal("intIDPlanAccion"));

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDProceso")))
                            planAccion.IDProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso"));

                        if (!reader.IsDBNull(reader.GetOrdinal("nombreCritica")))
                            planAccion.NombreCritica = reader.GetString(reader.GetOrdinal("nombreCritica"));

                        if (!reader.IsDBNull(reader.GetOrdinal("PlanAccion")))
                            planAccion.PlanAccion = reader.GetString(reader.GetOrdinal("PlanAccion"));

                        if (!reader.IsDBNull(reader.GetOrdinal("PreDialogo")))
                            planAccion.PreDialogo = false;
                        planAccion.idPlanAccionVisita = !reader.IsDBNull(reader.GetOrdinal("intIDPlanAcccionVisita")) ? Convert.ToInt32(reader["intIDPlanAcccionVisita"]) : 0;
                        planAccion.postVisita = !reader.IsDBNull(reader.GetOrdinal("bitPostVisita")) && Convert.ToBoolean(reader["bitPostVisita"]);
                        planesAccion.Add(planAccion);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            return planesAccion;
        }
    }
}