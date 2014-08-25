
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaRetroalimentacion : DaConexion
    {
        // Ingresar
        public bool IngresardaRetroalimentacion(string connstring, BeRetroalimentacion beRetroalimentacion)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarRetroalimentacion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intPreguntaRetroalimentacion", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@vhcRespuesta", SqlDbType.VarChar, 600);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@bitPostDialogo", SqlDbType.Bit);

                cmd.Parameters[0].Value = beRetroalimentacion.idProceso;
                cmd.Parameters[1].Value = beRetroalimentacion.idPreguntaRetroalimentacion;
                cmd.Parameters[2].Value = beRetroalimentacion.CodigoPlanAnual;
                cmd.Parameters[3].Value = beRetroalimentacion.respuesta;
                cmd.Parameters[4].Value = beRetroalimentacion.idUsuario;
                cmd.Parameters[5].Value = beRetroalimentacion.PostDialogo;

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


        public DataTable ListarRetroalimentacion(string connstring, BeRetroalimentacion beRetroalimentacion)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ListarRetroalimentacion", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters[0].Value = beRetroalimentacion.idProceso;

                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters[1].Value = beRetroalimentacion.CodigoPlanAnual;

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


        public DataTable ListarRetroalimentacionNuevas(string connstring, BeRetroalimentacion beRetroalimentacion)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ListarRetroalimentacionNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters[0].Value = beRetroalimentacion.idProceso;

                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters[1].Value = beRetroalimentacion.CodigoPlanAnual;

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

        // Cargar Combo Competencia

        public DataTable CargarCompetencia(string connstring, BeResumenProceso beResumenProceso, string anio)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCompetencia", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters[0].Value = beResumenProceso.prefijoIsoPais;

                cmd.Parameters.Add("@chrAnio", SqlDbType.Char);
                cmd.Parameters[1].Value = anio;

                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);
                cmd.Parameters[2].Value = beResumenProceso.codigoUsuario;


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


        public DataTable CargarCompetenciaNueva(string connstring, BeResumenProceso beResumenProceso, string anio)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_CargarCompetenciaNuevas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char);
                cmd.Parameters[0].Value = beResumenProceso.prefijoIsoPais;

                cmd.Parameters.Add("@chrAnio", SqlDbType.Char);
                cmd.Parameters[1].Value = anio;

                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);
                cmd.Parameters[2].Value = beResumenProceso.codigoUsuario;


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


    }
}
