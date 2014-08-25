
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaIndicadores : DaConexion
    {
        public DataTable ObtenerPeriodo(string ddlperiodo, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SeleccionarPeriodoDialogoDesempeno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = codigoRolUsuario;
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
            return ds.Tables.Count == 0 ? null : ds.Tables[0];
        }

        public DataTable ObtenerCampanaDesde(string ddlCampana, int codigoRolUsuario, string prefijoIsoPais, string periodoActual, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SeleccionarCampanaDesdeDialogoDesempeno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters[0].Value = codigoRolUsuario;
                cmd.Parameters[1].Value = prefijoIsoPais;
                cmd.Parameters[2].Value = periodoActual;

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

        public DataTable ObtenerCampanaHasta(string ddlCampana, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_SeleccionarCampanaHastaDialogoDesempeno", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = codigoRolUsuario;
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
            return ds.Tables.Count == 0 ? null : ds.Tables[0];
        }

        public DataSet Cargarindicadoresporperiodo(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodo", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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



        public DataSet CargarindicadoresporperiodoPreDialogo(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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


        public DataSet ObtenerVariablesEnfoqueSeleccionadasPorPeriodo(string periodo, string codigo, string pais, int rol, byte variablesAdicionales)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerVariablesEnfoqueSeleccionadasPorPeriodo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitvariablesAdicionales", SqlDbType.Bit);

                cmd.Parameters[0].Value = periodo;
                cmd.Parameters[1].Value = codigo;
                cmd.Parameters[2].Value = pais;
                cmd.Parameters[3].Value = rol;
                cmd.Parameters[4].Value = variablesAdicionales;

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

        public DataSet ObtenerVariablesEnfoqueSeleccionadasPorPeriodoPreDialogo(string periodo, string codigo, string pais, int rol, byte variablesAdicionales)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerVariablesEnfoqueSeleccionadasPorPeriodoPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitvariablesAdicionales", SqlDbType.Bit);

                cmd.Parameters[0].Value = periodo;
                cmd.Parameters[1].Value = codigo;
                cmd.Parameters[2].Value = pais;
                cmd.Parameters[3].Value = rol;
                cmd.Parameters[4].Value = variablesAdicionales;

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

        public DataSet CargarindicadoresporperiodoNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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




        public DataSet CargarindicadoresporperiodoEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodo_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;


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

        public DataSet Cargarindicadoresporcampana(string ddlCampana, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampana", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlCampana;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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

        public DataSet CargarindicadoresporcampanaNuevas(string ddlCampana, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampanaNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlCampana;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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


        public DataSet CargarindicadoresporcampanaEvaluado(string ddlCampana, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampana_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlCampana;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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

        public DataSet CargarIndicadoresPorRangoCampana(string campanaDesde, string campanaHasta, string codigoUsuario, int idProceso, int codigoRolUsuario, string prefijoIsoPais)
        {
            var ds = new DataSet();
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampanaRango", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampanaDesde", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrAnioCampanaHasta", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = campanaDesde;
                cmd.Parameters[1].Value = campanaHasta;
                cmd.Parameters[2].Value = codigoUsuario;
                cmd.Parameters[3].Value = idProceso;
                cmd.Parameters[4].Value = codigoRolUsuario;
                cmd.Parameters[5].Value = prefijoIsoPais;

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
                    cnn.Close();
                }
            }
            return ds;
        }

        public DataSet CargarindicadoresporperiodoVariablesAdicionales(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoVariablesAdicionales", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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


        public DataSet CargarindicadoresporperiodoVariablesAdicionalesPreDialogo(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoVariablesAdicionalesPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;


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


        public DataSet CargarindicadoresporperiodoVariablesAdicionalesNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoVariablesAdicionalesNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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


        public DataSet CargarindicadoresporperiodoVariablesAdicionalesEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoVariablesAdicionales_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;


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

        public DataSet CargarindicadoresporcampanaVariablesAdicionales(string ddlCampanadesde, string ddlCampanahasta, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampanaVariablesAdicionales", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampanadesde", SqlDbType.Char, 6);
                cmd.Parameters.Add("@chrAnioCampanahasta", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = ddlCampanadesde;
                cmd.Parameters[1].Value = ddlCampanahasta;
                cmd.Parameters[2].Value = codigoUsuario;
                cmd.Parameters[3].Value = intIdProceso;
                cmd.Parameters[4].Value = codigoRolUsuario;
                cmd.Parameters[5].Value = prefijoIsoPais;

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

        public DataSet CargarindicadoresporcampanaVariablesAdicionales2(string anioCampana, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampanaVariablesAdicionales2", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = anioCampana;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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


        public DataSet CargarindicadoresporcampanaVariablesAdicionales2Nuevas(string anioCampana, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampanaVariablesAdicionales2Nuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = anioCampana;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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




        public DataSet CargarindicadoresporcampanaVariablesAdicionales2Evaluado(string anioCampana, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarIndicadoresporCampanaVariablesAdicionales2_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = anioCampana;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;

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

        public DataTable SeleccionarCampana(string chrPeriodo, int codigoRolUsuario, string chrPrefijoIsoPais, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ObtenerAnioCampana", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = codigoRolUsuario;
                cmd.Parameters[1].Value = chrPrefijoIsoPais;

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

        public bool InsertarIndicadores(string codVariable1, string codVariable2, int intIdProceso, string anioCampana, int numeroIngresado, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_TRX_INDICADOR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodVariable1", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodVariable2", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrAnioCampanha", SqlDbType.Char, 6);
                cmd.Parameters.Add("@intNumeroIngresado", SqlDbType.Int);

                cmd.Parameters["@chrCodVariable1"].Value = codVariable1;
                cmd.Parameters["@chrCodVariable2"].Value = codVariable2;
                cmd.Parameters["@intIDProceso"].Value = intIdProceso;
                cmd.Parameters["@chrAnioCampanha"].Value = anioCampana;
                cmd.Parameters["@intNumeroIngresado"].Value = numeroIngresado;

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

        public bool InsertarIndicadoresEvaluado(string codVariable1, string codVariable2, int intIdProceso, string anioCampana, int numeroIngresado, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_TRX_INDICADOR_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodVariable1", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodVariable2", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrAnioCampanha", SqlDbType.Char, 6);
                cmd.Parameters.Add("@intNumeroIngresado", SqlDbType.Int);

                cmd.Parameters["@chrCodVariable1"].Value = codVariable1;
                cmd.Parameters["@chrCodVariable2"].Value = codVariable2;
                cmd.Parameters["@intIDProceso"].Value = intIdProceso;
                cmd.Parameters["@chrAnioCampanha"].Value = anioCampana;
                cmd.Parameters["@intNumeroIngresado"].Value = numeroIngresado;

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

        public DataTable ObtenerIndicadoresProcesados(int idProceso)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Obtener_IndicadoresProcesados", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = Constantes.EstadoActivo;

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

        public DataTable CargarCombosVariablesCausa(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variable1, string variable2, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCombosVariablesCausa", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@Variable1", SqlDbType.Char, 15);
                cmd.Parameters.Add("@Variable2", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variable1;
                cmd.Parameters[7].Value = variable2;

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

        public DataTable CargarCombosVariablesCausaNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variable1, string variable2, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCombosVariablesCausaNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@Variable1", SqlDbType.Char, 15);
                cmd.Parameters.Add("@Variable2", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variable1;
                cmd.Parameters[7].Value = variable2;

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




        public DataTable CargarCombosVariablesCausaEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variable1, string variable2, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCombosVariablesCausa_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@Variable1", SqlDbType.Char, 15);
                cmd.Parameters.Add("@Variable2", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variable1;
                cmd.Parameters[7].Value = variable2;

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

        public DataTable CargarDatosVariableCausa(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variableCausa, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("CargarDatosVariableCausa", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@VariableCausa", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variableCausa;

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


        public DataTable CargarDatosVariableCausaNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variableCausa, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("CargarDatosVariableCausaNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@VariableCausa", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variableCausa;

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



        public DataTable CargarDatosVariableCausaEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variableCausa, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("CargarDatosVariableCausa_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@VariableCausa", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variableCausa;

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



        public DataTable CargarDatosVariableCausaEvaluadoPreDialogo(string ddlperiodo, string codigoUsuario, int intIdProceso, int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado, string variableCausa, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("CargarDatosVariableCausa_EvalPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@VariableCausa", SqlDbType.Char, 15);

                cmd.Parameters[0].Value = ddlperiodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = intIdProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoIsoPais;
                cmd.Parameters[5].Value = anoCampanaCerrado;
                cmd.Parameters[6].Value = variableCausa;

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




        public bool InsertarVariablesCausa(int idProceso, string variable1, string variable2, string variableCausa1,
                                           string variableCausa2, string variableCausa3, string variableCausa4,
                                           string objetivo1, string real1, string diferencia1, string objetivo2,
                                           string real2, string diferencia2, string objetivo3, string real3,
                                           string diferencia3, string objetivo4, string real4, string diferencia4,
                                           string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarVariablesCausa", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters.Add("@chrCodVariablePadre1", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariablePadre2", SqlDbType.VarChar, 15);

                cmd.Parameters.Add("@chrCodVariableHija1", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariableHija2", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariableHija3", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariableHija4", SqlDbType.VarChar, 15);

                cmd.Parameters.Add("@decValorPlanPeriodo1", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo1", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia1", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@decValorPlanPeriodo2", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo2", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia2", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@decValorPlanPeriodo3", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo3", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia3", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@decValorPlanPeriodo4", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo4", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia4", SqlDbType.VarChar, 20);

                cmd.Parameters["@intIDProceso"].Value = idProceso;

                cmd.Parameters["@chrCodVariablePadre1"].Value = variable1;
                cmd.Parameters["@chrCodVariablePadre2"].Value = variable2;

                cmd.Parameters["@chrCodVariableHija1"].Value = variableCausa1;
                cmd.Parameters["@chrCodVariableHija2"].Value = variableCausa2;
                cmd.Parameters["@chrCodVariableHija3"].Value = variableCausa3;
                cmd.Parameters["@chrCodVariableHija4"].Value = variableCausa4;

                cmd.Parameters["@decValorPlanPeriodo1"].Value = objetivo1;
                cmd.Parameters["@decValorRealPeriodo1"].Value = real1;
                cmd.Parameters["@Diferencia1"].Value = diferencia1;

                cmd.Parameters["@decValorPlanPeriodo2"].Value = objetivo2;
                cmd.Parameters["@decValorRealPeriodo2"].Value = real2;
                cmd.Parameters["@Diferencia2"].Value = diferencia2;

                cmd.Parameters["@decValorPlanPeriodo3"].Value = objetivo3;
                cmd.Parameters["@decValorRealPeriodo3"].Value = real3;
                cmd.Parameters["@Diferencia3"].Value = diferencia3;

                cmd.Parameters["@decValorPlanPeriodo4"].Value = objetivo4;
                cmd.Parameters["@decValorRealPeriodo4"].Value = real4;
                cmd.Parameters["@Diferencia4"].Value = diferencia4;

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

        public bool InsertarVariablesCausaEvaluado(int idProceso, string variable1, string variable2,
                                                   string variableCausa1, string variableCausa2, string variableCausa3,
                                                   string variableCausa4, string objetivo1, string real1,
                                                   string diferencia1, string objetivo2, string real2,
                                                   string diferencia2, string objetivo3, string real3,
                                                   string diferencia3, string objetivo4, string real4,
                                                   string diferencia4, string connstring)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarVariablesCausa_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters.Add("@chrCodVariablePadre1", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariablePadre2", SqlDbType.VarChar, 15);

                cmd.Parameters.Add("@chrCodVariableHija1", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariableHija2", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariableHija3", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@chrCodVariableHija4", SqlDbType.VarChar, 15);

                cmd.Parameters.Add("@decValorPlanPeriodo1", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo1", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia1", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@decValorPlanPeriodo2", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo2", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia2", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@decValorPlanPeriodo3", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo3", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia3", SqlDbType.VarChar, 20);

                cmd.Parameters.Add("@decValorPlanPeriodo4", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@decValorRealPeriodo4", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@Diferencia4", SqlDbType.VarChar, 20);

                cmd.Parameters["@intIDProceso"].Value = idProceso;

                cmd.Parameters["@chrCodVariablePadre1"].Value = variable1;
                cmd.Parameters["@chrCodVariablePadre2"].Value = variable2;

                cmd.Parameters["@chrCodVariableHija1"].Value = variableCausa1;
                cmd.Parameters["@chrCodVariableHija2"].Value = variableCausa2;
                cmd.Parameters["@chrCodVariableHija3"].Value = variableCausa3;
                cmd.Parameters["@chrCodVariableHija4"].Value = variableCausa4;

                cmd.Parameters["@decValorPlanPeriodo1"].Value = objetivo1;
                cmd.Parameters["@decValorRealPeriodo1"].Value = real1;
                cmd.Parameters["@Diferencia1"].Value = diferencia1;

                cmd.Parameters["@decValorPlanPeriodo2"].Value = objetivo2;
                cmd.Parameters["@decValorRealPeriodo2"].Value = real2;
                cmd.Parameters["@Diferencia2"].Value = diferencia2;

                cmd.Parameters["@decValorPlanPeriodo3"].Value = objetivo3;
                cmd.Parameters["@decValorRealPeriodo3"].Value = real3;
                cmd.Parameters["@Diferencia3"].Value = diferencia3;

                cmd.Parameters["@decValorPlanPeriodo4"].Value = objetivo4;
                cmd.Parameters["@decValorRealPeriodo4"].Value = real4;
                cmd.Parameters["@Diferencia4"].Value = diferencia4;

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

        public DataTable ObtenerVariablesCausa(int idProceso, string codVariablePadre)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("Ese_ObtenerVariablesCausa", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codVariablePadre;

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


        public DataTable ObtenerVariablesCausaPreDialogo(int idProceso, string codVariablePadre)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("Ese_ObtenerVariablesCausaPreDialogo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codVariablePadre;

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



        public DataTable ObtenerVariablesCausaEvaluado(int idProceso, string codVariablePadre)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("Ese_ObtenerVariablesCausa_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codVariablePadre;

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

        public DataTable ValidarPeriodoEvaluacion(string periodoEvaluacion, string prefijoIsoPais, int codigoRolUsuario, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VALIDAR_PERIODOEVALUACIONGERENTEREGION", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
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

        public DataTable CargaDatosVariableEnfoque(string variable, int intIdProceso, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_CARGARDATOSGRABADOS_VARIABLEENFOQUE", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = variable;
                cmd.Parameters[1].Value = intIdProceso;

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

        public DataTable ObtenerDescripcionVariableEnfoque(string variable, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ObtenerDescripcionVariableEnfoque", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodVariable", SqlDbType.VarChar, 15);

                cmd.Parameters[0].Value = variable;

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

        public DataTable ObtenerVariablesCausaPorProceso(int idProceso)
        {
            var ds = new DataTable();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ObtenerVariablesCausaPorProceso", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;

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

        public DataTable ObtenerVariablesCausaPorProcesoEvaluado(int idProceso)
        {
            var ds = new DataTable();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ObtenerVariablesCausaPorProceso_Eval", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;

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

        public DataTable ObtenerResumen(string periodo, string codigoUsuario, int idProceso, int codigoRolUsuario, string prefijoPais)
        {
            var ds = new DataTable();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_GetEstadosporPeriodo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = periodo;
                cmd.Parameters[1].Value = codigoUsuario;
                cmd.Parameters[2].Value = idProceso;
                cmd.Parameters[3].Value = codigoRolUsuario;
                cmd.Parameters[4].Value = prefijoPais;

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

        // Obtener variables causa plan accion Visita (Grilla)
        public DataTable ObtenerVariablesCausaVisita(int idProceso, string codVariablePadre, string documentoIdentidad)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_Variables_Causa", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 10);

                cmd.Parameters["@intIdProceso"].Value = idProceso;
                cmd.Parameters["@chrCodVariablePadre"].Value = codVariablePadre;
                cmd.Parameters["@chrDocIdentidad"].Value = documentoIdentidad;

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

        public DataTable ObtenerVariablesCausaVisitaGz(int idProceso, string codVariablePadre, string documentoIdentidad)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_Variables_Causa_GZ", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 10);

                cmd.Parameters["@intIdProceso"].Value = idProceso;
                cmd.Parameters["@chrCodVariablePadre"].Value = codVariablePadre;
                cmd.Parameters["@chrDocIdentidad"].Value = documentoIdentidad;

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

        public DataTable ObtenerVariablesCausaVisita(int idProceso, string codVariablePadre)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ObtenerVariablesCausaVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = codVariablePadre;

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
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }

        public void InsertarVariableCausaVista(BeVariableCausa variableCausa)
        {
            using (var conn = ObtieneConexion())
            {
                var cmdInsertar = new SqlCommand("ESE_InsertarVariablesCausaVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmdInsertar.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmdInsertar.Parameters.Add("@chrCodVariablePadre", SqlDbType.Char, 15);
                cmdInsertar.Parameters.Add("@chrCodVariableHija", SqlDbType.Char, 15);
                cmdInsertar.Parameters.Add("@decValorPlanPeriodo", SqlDbType.VarChar, 20);
                cmdInsertar.Parameters.Add("@decValorRealPeriodo", SqlDbType.VarChar, 20);
                cmdInsertar.Parameters.Add("@Diferencia", SqlDbType.VarChar, 20);

                cmdInsertar.Parameters["@intIDProceso"].Value = variableCausa.IdProceso;
                cmdInsertar.Parameters["@chrCodVariablePadre"].Value = variableCausa.CodigoPadre;
                cmdInsertar.Parameters["@chrCodVariableHija"].Value = variableCausa.Codigo;
                cmdInsertar.Parameters["@decValorPlanPeriodo"].Value = variableCausa.ValorPlan;
                cmdInsertar.Parameters["@decValorRealPeriodo"].Value = variableCausa.ValorReal;
                cmdInsertar.Parameters["@Diferencia"].Value = variableCausa.Diferencia;

                try
                {
                    conn.Open();
                    cmdInsertar.ExecuteNonQuery();
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
        }

        public void EliminarVariableCausaVista(int idProceso)
        {
            using (var conn = ObtieneConexion())
            {
                var cmdEliminar = new SqlCommand("ESE_EliminarVariablesCausaVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmdEliminar.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmdEliminar.Parameters["@intIDProceso"].Value = idProceso;

                try
                {
                    conn.Open();
                    cmdEliminar.ExecuteNonQuery();
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
        }

        public List<BeComun> CargarGerentesRegion(string prefijoIsoPais, string periodo)
        {
            var listaGerentesRegion = new List<BeComun>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_OBTENER_GERENTE_REGION_BY_PAIS", conn)
                { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var gerente = new BeComun
                        {
                            Codigo = dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegion"))
                                ? string.Empty
                                : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegion")),
                            Descripcion = dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                ? string.Empty
                                : dr.GetString(dr.GetOrdinal("vchNombrecompleto"))
                        };
                        listaGerentesRegion.Add(gerente);
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
            return listaGerentesRegion;
        }



        public List<BeComun> CargarGerentesZona(string prefijoIsoPais, string codigoUsuario, string periodo)
        {
            var listaGerentesZona = new List<BeComun>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_OBTENER_GERENTE_ZONA_BY_GR", conn)
                { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20).Value = codigoUsuario;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = periodo;
                    
                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var gerente = new BeComun
                        {
                            Codigo = dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                ? string.Empty
                                : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")),
                            Descripcion = dr.IsDBNull(dr.GetOrdinal("vchNombrecompleto"))
                                ? string.Empty
                                : dr.GetString(dr.GetOrdinal("vchNombrecompleto"))
                        };
                        listaGerentesZona.Add(gerente);
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
            return listaGerentesZona;
        }


        public bool ActualizaModeloDialogo(int idProceso, string modeloDialogo)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Actualiza_ModeloDialogo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@idProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchModeloDialogo", SqlDbType.VarChar,20);

                cmd.Parameters["@idProceso"].Value = idProceso;
                cmd.Parameters["@vchModeloDialogo"].Value = modeloDialogo;

                cmd.ExecuteNonQuery();
            }
            return true;
        }


    }
}