
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaVariableEnfoque : DaConexion
    {
        /// <summary>
        /// Retorna las variables de enfoque procesadas
        /// </summary>
        /// <returns></returns>
        public List<BeVariableEnfoque> ObtenerVariablesEnfoqueProcesadas(int idIndicador)
        {
            var lstVariableEnfoque = new List<BeVariableEnfoque>();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_VariableEnfoqueProcesados", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDIndicador", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDIndicador"].Value = idIndicador;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var objVarEnfoque = new BeVariableEnfoque
                        {
                            idVariableEnfoque = Convert.ToInt32(dr["intIDVariableEnfoque"]),
                            campania = dr["chrCampania"].ToString(),
                            zonas = dr["vchZonas"].ToString(),
                            planAccion = dr["vchPlan"].ToString(),
                            postDialogo =
                                dr["bitPostDialogo"] != DBNull.Value && dr.GetBoolean(dr.GetOrdinal("bitPostDialogo"))
                        };

                        lstVariableEnfoque.Add(objVarEnfoque);
                    }
                    dr.Close();
                }
            }
            return lstVariableEnfoque;
        }

        /// <summary>
        /// Retorna los planes de la variable de enfoque procesadas
        /// </summary>
        /// <param name="idVariableEnfoque"></param>
        /// <returns></returns>
        public DataTable ObtenerPlanesByVariablesEnfoqueProcesadas(int idVariableEnfoque)
        {
            var dtPlanes = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_VariableEnfoqueProcesadosPlanes", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVariableEnfoque", SqlDbType.Int);

                cmd.Parameters["@intIDVariableEnfoque"].Value = idVariableEnfoque;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);
            }
            return dtPlanes;
        }

        /// <summary>
        /// Inserta las variables de enfoque
        /// </summary>
        /// <param name="objVarEnfoqueBe"></param>
        /// <returns></returns>
        public int InsertarVariableEnfoque(BeVariableEnfoque objVarEnfoqueBe)
        {
            var idVariable = 0;
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Insertar_VariableEnfoque", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDIndicador", SqlDbType.Int);
                cmd.Parameters.Add("@chrCampania", SqlDbType.Char, 6);
                cmd.Parameters.Add("@vchZonas", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@vchPlan", SqlDbType.VarChar, 2000);
                cmd.Parameters.Add("@bitPostDialogo", SqlDbType.Bit);

                cmd.Parameters["@intIDIndicador"].Value = objVarEnfoqueBe.idIndicador;
                cmd.Parameters["@chrCampania"].Value = objVarEnfoqueBe.campania;
                cmd.Parameters["@vchZonas"].Value = objVarEnfoqueBe.zonas;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@vchPlan"].Value = objVarEnfoqueBe.planAccion;
                cmd.Parameters["@bitPostDialogo"].Value = objVarEnfoqueBe.postDialogo ? 1 : 0;

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    idVariable = Convert.ToInt32(dr["idVariable"]);
                }
                dr.Close();
            }
            return idVariable;
        }

        /// <summary>
        /// Actualiza las variables de enfoque
        /// </summary>
        /// <param name="objVarEnfoqueBe"></param>
        /// <returns></returns>
        public bool ActualizarVariableEnfoque(BeVariableEnfoque objVarEnfoqueBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Actualizar_VariableEnfoque", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVariableEnfoque", SqlDbType.Int);
                cmd.Parameters.Add("@chrCampania", SqlDbType.Char, 6);
                cmd.Parameters.Add("@vchZonas", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@vchPlan", SqlDbType.VarChar, 2000);
                cmd.Parameters.Add("@bitPostDialogo", SqlDbType.Bit);

                cmd.Parameters["@intIDVariableEnfoque"].Value = objVarEnfoqueBe.idVariableEnfoque;
                cmd.Parameters["@chrCampania"].Value = objVarEnfoqueBe.campania;
                cmd.Parameters["@vchZonas"].Value = objVarEnfoqueBe.zonas;
                cmd.Parameters["@vchPlan"].Value = objVarEnfoqueBe.planAccion;
                cmd.Parameters["@bitPostDialogo"].Value = objVarEnfoqueBe.postDialogo;

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        /// <summary>
        /// Inserta los planes de accion de la variable de enfoque
        /// </summary>
        /// <param name="objVarEnfoqueBe"></param>
        public void InsertarVariableEnfoquePlanes(BeVariableEnfoque objVarEnfoqueBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Insertar_VariableEnfoquePlanes", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVariableEnfoque", SqlDbType.Int);
                cmd.Parameters.Add("@vchPlanAccion", SqlDbType.VarChar, 300);
                cmd.Parameters.Add("@bitPostDialogo", SqlDbType.Bit);

                cmd.Parameters["@intIDVariableEnfoque"].Value = objVarEnfoqueBe.idVariableEnfoque;
                cmd.Parameters["@vchPlanAccion"].Value = objVarEnfoqueBe.planAccion;
                cmd.Parameters["@bitPostDialogo"].Value = objVarEnfoqueBe.postDialogo;

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Actualiza los planes de accion de la variable de enfoque
        /// </summary>
        /// <param name="objVarEnfoqueBe"></param>
        public void ActualizarVariableEnfoquePlanes(BeVariableEnfoque objVarEnfoqueBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Actualizar_VariableEnfoquePlanes", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVariableEnfoquePlan", SqlDbType.Int);
                cmd.Parameters.Add("@vchPlanAccion", SqlDbType.VarChar, 300);
                cmd.Parameters.Add("@bitPostDialogo", SqlDbType.Bit);

                cmd.Parameters["@intIDVariableEnfoquePlan"].Value = objVarEnfoqueBe.idVariableEnfoquePlan;
                cmd.Parameters["@vchPlanAccion"].Value = objVarEnfoqueBe.planAccion;
                cmd.Parameters["@bitPostDialogo"].Value = objVarEnfoqueBe.postDialogo;

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Elimina los planes de las variable de enfoque
        /// </summary>
        /// <param name="idVariableEnfoquePlan"></param>
        public void EliminarVariableEnfoquePlanes(int idVariableEnfoquePlan)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Eliminar_VariableEnfoquePlanes", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVariableEnfoquePlan", SqlDbType.Int);

                cmd.Parameters["@intIDVariableEnfoquePlan"].Value = idVariableEnfoquePlan;

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ObtenerDescripcionVariableEnfoque(string variable, string anioCampana, string periodoEvaluacion)
        {
            var ds = new DataSet();
            using (var conex = ObtieneConexion())
            //using (SqlConnection conn = new SqlConnection(connstring))
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerDescripcionVariableEnfoque", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodVariable", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodVariable"].Value = variable;
                cmd.Parameters["@chrAnioCampana"].Value = anioCampana;
                cmd.Parameters["@chrPeriodo"].Value = periodoEvaluacion;

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
                    conex.Close();
                }
            }
            return ds.Tables[0];
        }
    }
}