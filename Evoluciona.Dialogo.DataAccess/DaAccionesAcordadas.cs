
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaAccionesAcordadas : DaConexion
    {
        public DataTable GetVariablesCausales(BeAccionesAcordadas objAccionesAcordadasBe, string codVariablePadre)
        {
            var dtPlanes = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_GetVariablesCausales", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);

                cmd.Parameters["@intIdProceso"].Value = objAccionesAcordadasBe.IdProceso;
                cmd.Parameters["@chrCodVariablePadre"].Value = codVariablePadre;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public DataTable GetCodVariablePadre(BeAccionesAcordadas objAccionesAcordadasBe, string documento, string periodoEvaluacion)
        {
            var dtPlanes = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_GetCodVariablePadre", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@intIdProceso"].Value = objAccionesAcordadasBe.IdProceso;
                cmd.Parameters["@chrDocIdentidad"].Value = documento;
                cmd.Parameters["@chrPeriodo"].Value = periodoEvaluacion;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public DataTable GetCodVariablePadreGz(BeAccionesAcordadas objAccionesAcordadasBe, string documento, string periodoEvaluacion)
        {
            var dtPlanes = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_GetCodVariablePadreGZ", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@intIdProceso"].Value = objAccionesAcordadasBe.IdProceso;
                cmd.Parameters["@chrDocIdentidad"].Value = documento;
                cmd.Parameters["@chrPeriodo"].Value = periodoEvaluacion;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public bool RecordAccionesAcordadas(BeAccionesAcordadas objAccionesAcordadasBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_RecordAccionesAcordadas", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@vchAccionAcordada1", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchAccionAcordada2", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchCampanias1", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@vchCampanias2", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@bitPostVisita1", SqlDbType.Bit);
                cmd.Parameters.Add("@bitPostVisita2", SqlDbType.Bit);
                cmd.Parameters.Add("@intIDIndicador1", SqlDbType.Int);
                cmd.Parameters.Add("@intIDIndicador2", SqlDbType.Int);

                cmd.Parameters[0].Value = objAccionesAcordadasBe.IdProceso;
                cmd.Parameters[1].Value = objAccionesAcordadasBe.IdVisita;
                cmd.Parameters[2].Value = objAccionesAcordadasBe.AccionesAcordadas1;
                cmd.Parameters[3].Value = objAccionesAcordadasBe.AccionesAcordadas2;
                cmd.Parameters[4].Value = objAccionesAcordadasBe.Campanias1;
                cmd.Parameters[5].Value = objAccionesAcordadasBe.Campanias2;
                cmd.Parameters[6].Value = objAccionesAcordadasBe.PostVisita1;
                cmd.Parameters[7].Value = objAccionesAcordadasBe.PostVisita2;
                cmd.Parameters[8].Value = objAccionesAcordadasBe.IDIndicador1;
                cmd.Parameters[9].Value = objAccionesAcordadasBe.IDIndicador2;

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
                    conex.Close();
                }
            }
            return true;
        }

        public void ActualizarAccionesAcordadas(BeAccionesAcordadas objAccionesAcordadasBe)
        {

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Actualizar_AccionesAcordadas", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDAcciones1", SqlDbType.Int);
                cmd.Parameters.Add("@intIDAcciones2", SqlDbType.Int);
                cmd.Parameters.Add("@vchAccionAcordada1", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchAccionAcordada2", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchCampanias1", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@vchCampanias2", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@bitPostVisita1", SqlDbType.Bit);
                cmd.Parameters.Add("@bitPostVisita2", SqlDbType.Bit);


                cmd.Parameters["@intIDAcciones1"].Value = objAccionesAcordadasBe.IdAcciones;
                cmd.Parameters["@intIDAcciones2"].Value = objAccionesAcordadasBe.IdAcciones2;
                cmd.Parameters["@vchAccionAcordada1"].Value = objAccionesAcordadasBe.AccionesAcordadas1;
                cmd.Parameters["@vchAccionAcordada2"].Value = objAccionesAcordadasBe.AccionesAcordadas2;
                cmd.Parameters["@vchCampanias1"].Value = objAccionesAcordadasBe.Campanias1;
                cmd.Parameters["@vchCampanias2"].Value = objAccionesAcordadasBe.Campanias2;
                cmd.Parameters["@bitPostVisita1"].Value = objAccionesAcordadasBe.PostVisita1;
                cmd.Parameters["@bitPostVisita2"].Value = objAccionesAcordadasBe.PostVisita2;


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
                    conex.Close();
                }
            }

        }

        public DataTable ObtenerAccionesAcordadasByIndicador(BeAccionesAcordadas objAccionesAcordadasBe)
        {
            var dtPlanes = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_AccionesAcordadasByIndicador", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intIDIndicador", SqlDbType.Int);

                cmd.Parameters["@intIDVisita"].Value = objAccionesAcordadasBe.IdVisita;
                cmd.Parameters["@intIDIndicador"].Value = objAccionesAcordadasBe.IDIndicador1;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public List<BeAccionesAcordadas> ObtenerAccionesAcordadas(int idProceso)
        {
            var acciones = new List<BeAccionesAcordadas>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_AccionesAcordadas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var accion = new BeAccionesAcordadas
                        {
                            IdAcciones = dr.GetInt32(dr.GetOrdinal("intIDAccion")),
                            IdProceso = dr.GetInt32(dr.GetOrdinal("intIDProceso")),
                            IDIndicador1 = dr.GetInt32(dr.GetOrdinal("intIDIndicador")),
                            CodVariablePadre1 = dr.GetString(dr.GetOrdinal("vchSeleccionado")),
                            DesVariablePadre1 = dr.GetString(dr.GetOrdinal("vchrDesVariable")),
                            AccionesAcordadas1 = dr.GetString(dr.GetOrdinal("vchAccionAcordada")),
                            Campanias1 = dr.GetString(dr.GetOrdinal("vchCampanias")),
                            PostVisita1 = dr.GetBoolean(dr.GetOrdinal("bitPostVisita")),
                            TipoAccion = dr.GetString(dr.GetOrdinal("chrTipoAccion"))
                        };

                        acciones.Add(accion);
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

            return acciones;
        }

        public List<BeVariableCausa> ObtenerVariablesCausales(int idProceso, string codiVariable, string tipoAccion)
        {
            var variables = new List<BeVariableCausa>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_VariablesCausales", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters.Add("@chrTipoAccion", SqlDbType.Char, 1);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codiVariable;
                cmd.Parameters[2].Value = tipoAccion;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var variableCausa = new BeVariableCausa
                        {
                            IdProceso = idProceso,
                            Codigo = dr.GetString(dr.GetOrdinal("CodigoVariable")),
                            DescripcionVariable = dr.GetString(dr.GetOrdinal("DescVariable"))
                        };

                        variables.Add(variableCausa);
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

            return variables;
        }

        public bool InsertarAccionAcordada(BeAccionesAcordadas objAccionAcordada)
        {
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Insertar_AccionAcordada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@vchAccionAcordada", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchCampanias", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@bitPostVisita", SqlDbType.Bit);
                cmd.Parameters.Add("@intIDIndicador", SqlDbType.Int);

                cmd.Parameters[0].Value = objAccionAcordada.IdProceso;
                cmd.Parameters[1].Value = objAccionAcordada.IdVisita;
                cmd.Parameters[2].Value = objAccionAcordada.AccionesAcordadas1;
                cmd.Parameters[3].Value = objAccionAcordada.Campanias1;
                cmd.Parameters[4].Value = objAccionAcordada.PostVisita1;
                cmd.Parameters[5].Value = objAccionAcordada.IDIndicador1;

                try
                {
                    conex.Open();
                    cmd.ExecuteNonQuery();
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
            return true;
        }

        public void ActualizarAccionAcordada(BeAccionesAcordadas objAccionAcordada)
        {
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Actualizar_AccionAcordada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDAcciones", SqlDbType.Int);
                cmd.Parameters.Add("@vchAccionAcordada", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@vchCampanias", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@bitPostVisita", SqlDbType.Bit);

                cmd.Parameters["@intIDAcciones"].Value = objAccionAcordada.IdAcciones;
                cmd.Parameters["@vchAccionAcordada"].Value = objAccionAcordada.AccionesAcordadas1;
                cmd.Parameters["@vchCampanias"].Value = objAccionAcordada.Campanias1;
                cmd.Parameters["@bitPostVisita"].Value = objAccionAcordada.PostVisita1;

                try
                {
                    conex.Open();
                    cmd.ExecuteNonQuery();
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
        }
    }
}
