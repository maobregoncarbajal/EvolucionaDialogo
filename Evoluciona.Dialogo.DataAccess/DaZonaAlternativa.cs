
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaZonaAlternativa : DaConexion
    {
        public bool IngresarZonaAlternativa(string connstring, BeZonaAlternativa beZonaAlternativa, BeUsuario beUsuario)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarZonaAlternativa", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@CodZonaAlternativa", SqlDbType.Int);
                cmd.Parameters.Add("@IDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@Seleccionado", SqlDbType.Char,1);
                cmd.Parameters.Add("@vchOtro", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@intUsuarioModi", SqlDbType.Int);
                
                cmd.Parameters["@CodZonaAlternativa"].Value = beZonaAlternativa.CodZonaAlternativa;
                cmd.Parameters["@IDProceso"].Value = beZonaAlternativa.IDProceso;
                cmd.Parameters["@intCodigoPlanAnual"].Value = beZonaAlternativa.CodigoPlanAnual;
                cmd.Parameters["@Seleccionado"].Value = beZonaAlternativa.Seleccionado;
                if (beZonaAlternativa.AlternativaOtro.Trim() == "")
                {
                    cmd.Parameters["@vchOtro"].Value = DBNull.Value;
                }
                else {
                    cmd.Parameters["@vchOtro"].Value = beZonaAlternativa.AlternativaOtro.Trim() ;
                }
                
                cmd.Parameters["@intUsuarioModi"].Value = beUsuario.idUsuario;

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
            return true;
        }

        public bool IngresarZonaAlternativaVisita(string connstring, BeZonaAlternativa beZonaAlternativa, BeUsuario beUsuario)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_InsertarZonaAlternativa_Visita", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@CodZonaAlternativa", SqlDbType.Int);
                cmd.Parameters.Add("@IDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);
                cmd.Parameters.Add("@Seleccionado", SqlDbType.Char, 1);
                cmd.Parameters.Add("@vchOtro", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@intUsuarioModi", SqlDbType.Int);

                cmd.Parameters["@CodZonaAlternativa"].Value = beZonaAlternativa.CodZonaAlternativa;
                cmd.Parameters["@IDProceso"].Value = beZonaAlternativa.IDProceso;
                cmd.Parameters["@intCodigoPlanAnual"].Value = beZonaAlternativa.CodigoPlanAnual;
                cmd.Parameters["@Seleccionado"].Value = beZonaAlternativa.Seleccionado;
                if (beZonaAlternativa.AlternativaOtro.Trim() == "")
                {
                    cmd.Parameters["@vchOtro"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@vchOtro"].Value = beZonaAlternativa.AlternativaOtro.Trim();
                }

                cmd.Parameters["@intUsuarioModi"].Value = beUsuario.idUsuario;

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
            return true;
        }

        //Listar Zona Alternativas Grbadas
        public DataTable ObtenerZonaAlternativaGrabada(string connstring, BeResumenProceso objResumen, BeRetroalimentacion beRetroalimentacion)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ListarZonaAlternativaGrabada", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoPlanAnual", SqlDbType.Int);

                cmd.Parameters[0].Value = objResumen.idProceso;
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
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return ds.Tables[0];
        }
    }
}
