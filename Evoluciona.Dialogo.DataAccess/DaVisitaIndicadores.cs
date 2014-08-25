
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaVisitaIndicadores : DaConexion
    {
        public DataSet GetVariablesBase(BeResumenVisita objResumenVisitaBe)
        {
            var dtPlanes = new DataSet();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = objResumenVisitaBe.periodo.Trim();
                cmd.Parameters["@codigoUsuario"].Value = objResumenVisitaBe.codigoUsuario;
                cmd.Parameters["@intIDProceso"].Value = objResumenVisitaBe.idProceso;
                cmd.Parameters["@codigoRolUsuario"].Value = objResumenVisitaBe.codigoRolUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumenVisitaBe.prefijoIsoPais;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public DataSet GetVariablesAdicionales(BeResumenVisita objResumenVisitaBe)
        {
            var dtPlanes = new DataSet();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_CargarIndicadoresporPeriodoVariablesAdicionales", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = objResumenVisitaBe.periodo.Trim();
                cmd.Parameters["@codigoUsuario"].Value = objResumenVisitaBe.codigoUsuario;
                cmd.Parameters["@intIDProceso"].Value = objResumenVisitaBe.idProceso;
                cmd.Parameters["@codigoRolUsuario"].Value = objResumenVisitaBe.codigoRolUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumenVisitaBe.prefijoIsoPais;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public DataSet GetEstadosporPeriodo(BeResumenVisita objResumenVisitaBe)
        {
            var dtPlanes = new DataSet();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_GetEstadosporPeriodo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = objResumenVisitaBe.periodo.Trim();
                cmd.Parameters["@codigoUsuario"].Value = objResumenVisitaBe.codigoUsuario;
                cmd.Parameters["@intIDProceso"].Value = objResumenVisitaBe.idProceso;
                cmd.Parameters["@codigoRolUsuario"].Value = objResumenVisitaBe.codigoRolUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumenVisitaBe.prefijoIsoPais;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public DataSet GetCriticas(BeResumenVisita objResumenVisitaBe)
        {
            var dtPlanes = new DataSet();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_GetCriticas", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = objResumenVisitaBe.periodo.Trim();
                cmd.Parameters["@codigoUsuario"].Value = objResumenVisitaBe.codigoUsuario;
                cmd.Parameters["@intIDProceso"].Value = objResumenVisitaBe.idProceso;
                cmd.Parameters["@codigoRolUsuario"].Value = objResumenVisitaBe.codigoRolUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumenVisitaBe.prefijoIsoPais;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);

            }
            return dtPlanes;
        }

        public DataSet ObtenerIndicadoresVisita(int idProceso)
        {
            var dtPlanes = new DataSet();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_CargarIndicadoresVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters["@intIDProceso"].Value = idProceso;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);
                conex.Close();
            }
            return dtPlanes;
        }

        public void InsertarIndicadorVisita(BeVisitaIndicador indicador)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_InsertarIndicadorVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@vchSeleccionado", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@chrAnioCampanha", SqlDbType.Char, 6);
                cmd.Parameters.Add("@intNumeroIngresado", SqlDbType.Int);
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int);

                cmd.Parameters["@intIDProceso"].Value = indicador.IdProceso;
                cmd.Parameters["@vchSeleccionado"].Value = indicador.Seleccioando;
                cmd.Parameters["@bitEstado"].Value = indicador.Estado;
                cmd.Parameters["@chrAnioCampanha"].Value = indicador.AnhoCampanha;
                cmd.Parameters["@intNumeroIngresado"].Value = indicador.NumeroIngresado;
                cmd.Parameters["@intUsuarioCrea"].Value = indicador.UsuarioCreacion;

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
        }

        public void EliminarIndicadoresVisita(int idProceso)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_EliminarIndicadoresVisita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);
                cmd.Parameters["@intIdProceso"].Value = idProceso;

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
        }
    }
}
