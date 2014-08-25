
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaPlanAnual : DaConexion
    {
        public DataTable ObtenerPlanAnual(string connstring, BePlanAnual bePlanAnual)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPlanAnual", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char);
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);

                cmd.Parameters[0].Value = bePlanAnual.PrefijoIsoPais;
                cmd.Parameters[1].Value = bePlanAnual.Anio;
                cmd.Parameters[2].Value = bePlanAnual.CodigoColaborador;


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

        public DataTable ObtenerPlanAnualNuevas(string connstring, BePlanAnual bePlanAnual)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPlanAnualNuevas", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char);
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);

                cmd.Parameters[0].Value = bePlanAnual.PrefijoIsoPais;
                cmd.Parameters[1].Value = bePlanAnual.Anio;
                cmd.Parameters[2].Value = bePlanAnual.CodigoColaborador;


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


        // Ingresar Plan Anual
        public bool IngresarPlanAnual(string connstring, BePlanAnual bePlanAnual)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarPlanAnual", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.VarChar);

                cmd.Parameters[0].Value = bePlanAnual.idProceso;
                cmd.Parameters[1].Value = bePlanAnual.CodigoPlanAnual;
                cmd.Parameters[2].Value = bePlanAnual.Observacion;
                cmd.Parameters[3].Value = bePlanAnual.idUsuario;

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


        public bool IngresarPlanAnualPreDialogo(string connstring, BePlanAnual bePlanAnual)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarPlanAnualPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.VarChar);

                cmd.Parameters[0].Value = bePlanAnual.idProceso;
                cmd.Parameters[1].Value = bePlanAnual.CodigoPlanAnual;
                cmd.Parameters[2].Value = bePlanAnual.Observacion;
                cmd.Parameters[3].Value = bePlanAnual.idUsuario;

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

        public bool IngresarPlanAnualEvaluado(string connstring, BePlanAnual bePlanAnual)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarPlanAnual_Eval", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.VarChar);

                cmd.Parameters[0].Value = bePlanAnual.idProceso;
                cmd.Parameters[1].Value = bePlanAnual.CodigoPlanAnual;
                cmd.Parameters[2].Value = bePlanAnual.Observacion;
                cmd.Parameters[3].Value = bePlanAnual.idUsuario;

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

        // Actualizar Plan Anual
        public bool ActualizarPlanAnual(string connstring, BePlanAnual bePlanAnual)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ActualizarPlanAnual", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchObservacion", SqlDbType.VarChar);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.VarChar);

                cmd.Parameters[0].Value = bePlanAnual.idProceso;

                cmd.Parameters[1].Value = bePlanAnual.Observacion;
                cmd.Parameters[2].Value = bePlanAnual.idUsuario;

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

        // Obtener Grabadas
        public DataTable ObtenerPlanAnualGrabadas(string connstring, BeResumenProceso objResumen)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPlanAnualGrabadas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);


                cmd.Parameters[0].Value = objResumen.idProceso;



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


        public DataTable ObtenerPlanAnualGrabadasPreDialogo(string connstring, BeResumenProceso objResumen)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPlanAnualGrabadasPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);


                cmd.Parameters[0].Value = objResumen.idProceso;



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


        public DataTable ObtenerPlanAnualGrabadasEvaluado(string connstring, BeResumenProceso objResumen)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerPlanAnualGrabadas_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);


                cmd.Parameters[0].Value = objResumen.idProceso;



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

        public string ObtenerPaisAdam(string connstring, string prefijoIsoPais)
        {
            var paisAdam = "";

            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_Pais_Adam", conn) {CommandType = CommandType.StoredProcedure};


                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters[0].Value = prefijoIsoPais;
                cmd.Parameters[1].Value = Constantes.EstadoActivo;


                try
                {
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        paisAdam = dr["chrCodigoPaisAdam"].ToString();
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
            return paisAdam;
        }

        /// <summary>
        /// Inserta los datos obtenidos del web service a las tablas ESE_MAE_PLAN
        /// </summary>
        /// <param name="connstring"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="anio"></param>
        /// <param name="codigoColaborador"></param>
        /// <param name="nombreColaborador"></param>
        /// <param name="competencia"></param>
        /// <param name="compartamiento"></param>
        /// <param name="accionAcordada"></param>
        /// <param name="sugerencia"></param>
        /// <param name="estado"></param>
        /// <param name="codigoCompetencia"></param>
        /// <returns></returns>
        public bool InsertarPlanAnualAdam(string connstring, int idRol, string prefijoIsoPais, string anio, string codigoColaborador, string nombreColaborador, string competencia, string compartamiento, string accionAcordada, string sugerencia, int estado, int codigoCompetencia)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Insertar_PlanAnualAdam", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombreColaborador", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchCompetencia", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchCompartamiento", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchAccionAcordada", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchSugerencia", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@intCodigoCompetencia", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.TinyInt);

                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrAnio"].Value = anio;
                cmd.Parameters["@chrCodigoColaborador"].Value = codigoColaborador;
                cmd.Parameters["@vchNombreColaborador"].Value = nombreColaborador;
                cmd.Parameters["@vchCompetencia"].Value = competencia;
                cmd.Parameters["@vchCompartamiento"].Value = compartamiento;
                cmd.Parameters["@vchAccionAcordada"].Value = accionAcordada;
                cmd.Parameters["@vchSugerencia"].Value = sugerencia;
                cmd.Parameters["@intCodigoCompetencia"].Value = codigoCompetencia;
                cmd.Parameters["@bitEstado"].Value = estado;


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

        public bool ObtenerPlanAnualByUsuario(string connstring, int idRol, string prefijoIsoPais, string anio, string codigoColaborador, int estado)
        {
            var existe = false;
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_PlanAnualAdam", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@bitEstado", SqlDbType.TinyInt);

                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrAnio"].Value = anio;
                cmd.Parameters["@chrCodigoColaborador"].Value = codigoColaborador;
                cmd.Parameters["@bitEstado"].Value = estado;


                try
                {
                    var n = Convert.ToInt32(cmd.ExecuteScalar());
                    if (n > 0)
                    {
                        existe = true;
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

            return existe;
        }


        public DataSet ConsultaPlanDesarrollo(string connstring, int anio, string codigoPaisAdam, string documentoIdentidadConsulta, BeResumenProceso objResumen)
        {
            var dtPlanes = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ConsultaPlanDesarrollo", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombreColaborador", SqlDbType.VarChar, 100);

                cmd.Parameters["@intIDRol"].Value = objResumen.rolUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumen.prefijoIsoPais;
                cmd.Parameters["@chrAnio"].Value = anio;
                cmd.Parameters["@chrCodigoColaborador"].Value = objResumen.codigoUsuario;
                cmd.Parameters["@vchNombreColaborador"].Value = objResumen.nombreEvaluado;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }
    }
}