using Evoluciona.Dialogo.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Evoluciona.Dialogo.DataAccess
{
    public class DaRepDialogos : DaConexion
    {

        public List<BeRepDialogos> ListarRepDialogoAntNeg(string pais, string periodo, int idRol, bool planMejora)
        {
            var entidades = new List<BeRepDialogos>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_REP_DIALOGO_ANT_NEG", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitPlanMejora"].Value = planMejora;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRepDialogos
                            {
                                CodEvaluador = dr.IsDBNull(dr.GetOrdinal("CodEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluador")),
                                NombreEvaluador = dr.IsDBNull(dr.GetOrdinal("NombreEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluador")),
                                CodEvaluado = dr.IsDBNull(dr.GetOrdinal("CodEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluado")),
                                NombreEvaluado = dr.IsDBNull(dr.GetOrdinal("NombreEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluado")),

                                DesVariableEnfoque = dr.IsDBNull(dr.GetOrdinal("DesVariableEnfoque")) ? default(string) : dr.GetString(dr.GetOrdinal("DesVariableEnfoque")),
                                DesVariableCausa = dr.IsDBNull(dr.GetOrdinal("DesVariableCausa")) ? default(string) : dr.GetString(dr.GetOrdinal("DesVariableCausa")),

                                Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo")),
                                TipoDialogo = dr.IsDBNull(dr.GetOrdinal("TipoDialogo")) ? default(string) : dr.GetString(dr.GetOrdinal("TipoDialogo")),
                                Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"))

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


        public List<BeRepDialogos> ListarRepDialogoAntEqu(string pais, string periodo, int idRol, bool planMejora)
        {
            var entidades = new List<BeRepDialogos>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_REP_DIALOGO_ANT_EQU", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitPlanMejora"].Value = planMejora;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRepDialogos
                            {
                                CodEvaluador = dr.IsDBNull(dr.GetOrdinal("CodEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluador")),
                                NombreEvaluador = dr.IsDBNull(dr.GetOrdinal("NombreEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluador")),
                                CodEvaluado = dr.IsDBNull(dr.GetOrdinal("CodEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluado")),
                                NombreEvaluado = dr.IsDBNull(dr.GetOrdinal("NombreEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluado")),

                                CodPriorizada = dr.IsDBNull(dr.GetOrdinal("CodPriorizada")) ? default(string) : dr.GetString(dr.GetOrdinal("CodPriorizada")),
                                NombrePriorizada = dr.IsDBNull(dr.GetOrdinal("NombrePriorizada")) ? default(string) : dr.GetString(dr.GetOrdinal("NombrePriorizada")),
                                VariableConsiderar = dr.IsDBNull(dr.GetOrdinal("VariableConsiderar")) ? default(string) : dr.GetString(dr.GetOrdinal("VariableConsiderar")),

                                Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo")),
                                TipoDialogo = dr.IsDBNull(dr.GetOrdinal("TipoDialogo")) ? default(string) : dr.GetString(dr.GetOrdinal("TipoDialogo")),
                                Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"))

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

        public List<BeRepDialogos> ListarRepDialogoAntCom(string pais, string periodo, int idRol, bool planMejora)
        {
            var entidades = new List<BeRepDialogos>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_REP_DIALOGO_ANT_COM", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitPlanMejora"].Value = planMejora;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRepDialogos
                            {
                                CodEvaluador = dr.IsDBNull(dr.GetOrdinal("CodEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluador")),
                                NombreEvaluador = dr.IsDBNull(dr.GetOrdinal("NombreEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluador")),
                                CodEvaluado = dr.IsDBNull(dr.GetOrdinal("CodEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluado")),
                                NombreEvaluado = dr.IsDBNull(dr.GetOrdinal("NombreEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluado")),

                                Competencia = dr.IsDBNull(dr.GetOrdinal("Competencia")) ? default(string) : dr.GetString(dr.GetOrdinal("Competencia")),
                                Comportamiento = dr.IsDBNull(dr.GetOrdinal("Comportamiento")) ? default(string) : dr.GetString(dr.GetOrdinal("Comportamiento")),
                                VariableConsiderar = dr.IsDBNull(dr.GetOrdinal("Sugerencia")) ? default(string) : dr.GetString(dr.GetOrdinal("Sugerencia")),
                                Observacion = dr.IsDBNull(dr.GetOrdinal("Observacion")) ? default(string) : dr.GetString(dr.GetOrdinal("Observacion")),

                                Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo")),
                                TipoDialogo = dr.IsDBNull(dr.GetOrdinal("TipoDialogo")) ? default(string) : dr.GetString(dr.GetOrdinal("TipoDialogo")),
                                Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"))

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

        public List<BeRepDialogos> ListarRepDialogoDurNeg(string pais, string periodo, int idRol, bool planMejora)
        {
            var entidades = new List<BeRepDialogos>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_REP_DIALOGO_DUR_NEG", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitPlanMejora"].Value = planMejora;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRepDialogos
                            {
                                CodEvaluador = dr.IsDBNull(dr.GetOrdinal("CodEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluador")),
                                NombreEvaluador = dr.IsDBNull(dr.GetOrdinal("NombreEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluador")),
                                CodEvaluado = dr.IsDBNull(dr.GetOrdinal("CodEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluado")),
                                NombreEvaluado = dr.IsDBNull(dr.GetOrdinal("NombreEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluado")),

                                VariableEnfoque = dr.IsDBNull(dr.GetOrdinal("VariableEnfoque")) ? default(string) : dr.GetString(dr.GetOrdinal("VariableEnfoque")),
                                VariableCausales = dr.IsDBNull(dr.GetOrdinal("VariableCausales")) ? default(string) : dr.GetString(dr.GetOrdinal("VariableCausales")),
                                ZonasSecciones = dr.IsDBNull(dr.GetOrdinal("ZonasSecciones")) ? default(string) : dr.GetString(dr.GetOrdinal("ZonasSecciones")),
                                PlanNegocio = dr.IsDBNull(dr.GetOrdinal("PlanNegocio")) ? default(string) : dr.GetString(dr.GetOrdinal("PlanNegocio")),

                                Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo")),
                                TipoDialogo = dr.IsDBNull(dr.GetOrdinal("TipoDialogo")) ? default(string) : dr.GetString(dr.GetOrdinal("TipoDialogo")),
                                Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"))

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


        public List<BeRepDialogos> ListarRepDialogoDurEqu(string pais, string periodo, int idRol, bool planMejora)
        {
            var entidades = new List<BeRepDialogos>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_REP_DIALOGO_DUR_EQU", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitPlanMejora"].Value = planMejora;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRepDialogos
                            {
                                CodEvaluador = dr.IsDBNull(dr.GetOrdinal("CodEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluador")),
                                NombreEvaluador = dr.IsDBNull(dr.GetOrdinal("NombreEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluador")),
                                CodEvaluado = dr.IsDBNull(dr.GetOrdinal("CodEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluado")),
                                NombreEvaluado = dr.IsDBNull(dr.GetOrdinal("NombreEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluado")),

                                CodPriorizada = dr.IsDBNull(dr.GetOrdinal("CodPriorizada")) ? default(string) : dr.GetString(dr.GetOrdinal("CodPriorizada")),
                                NombrePriorizada = dr.IsDBNull(dr.GetOrdinal("NombrePriorizada")) ? default(string) : dr.GetString(dr.GetOrdinal("NombrePriorizada")),
                                PlanAccion = dr.IsDBNull(dr.GetOrdinal("PlanAccion")) ? default(string) : dr.GetString(dr.GetOrdinal("PlanAccion")),

                                Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo")),
                                TipoDialogo = dr.IsDBNull(dr.GetOrdinal("TipoDialogo")) ? default(string) : dr.GetString(dr.GetOrdinal("TipoDialogo")),
                                Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"))

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


        public List<BeRepDialogos> ListarRepDialogoDurCom(string pais, string periodo, int idRol, bool planMejora)
        {
            var entidades = new List<BeRepDialogos>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_REP_DIALOGO_DUR_COM", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitPlanMejora"].Value = planMejora;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeRepDialogos
                            {
                                CodEvaluador = dr.IsDBNull(dr.GetOrdinal("CodEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluador")),
                                NombreEvaluador = dr.IsDBNull(dr.GetOrdinal("NombreEvaluador")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluador")),
                                CodEvaluado = dr.IsDBNull(dr.GetOrdinal("CodEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("CodEvaluado")),
                                NombreEvaluado = dr.IsDBNull(dr.GetOrdinal("NombreEvaluado")) ? default(string) : dr.GetString(dr.GetOrdinal("NombreEvaluado")),

                                Competencia = dr.IsDBNull(dr.GetOrdinal("Competencia")) ? default(string) : dr.GetString(dr.GetOrdinal("Competencia")),
                                DesPreguRetro = dr.IsDBNull(dr.GetOrdinal("DesPreguRetro")) ? default(string) : dr.GetString(dr.GetOrdinal("DesPreguRetro")),
                                Respuesta = dr.IsDBNull(dr.GetOrdinal("Respuesta")) ? default(string) : dr.GetString(dr.GetOrdinal("Respuesta")),

                                Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo")),
                                TipoDialogo = dr.IsDBNull(dr.GetOrdinal("TipoDialogo")) ? default(string) : dr.GetString(dr.GetOrdinal("TipoDialogo")),
                                Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"))

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
