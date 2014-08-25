
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEncuestaPregunta : DaConexion
    {
        public List<BeEncuestaPregunta> ListaEncuestaPregunta()
        {
            var entidades = new List<BeEncuestaPregunta>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_PREGUNTA_LISTAR", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeEncuestaPregunta
                            {
                                IdPregunta =
                                    dr.IsDBNull(dr.GetOrdinal("intIdPregunta"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdPregunta")),
                                DesPreguntas =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesPreguntas"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesPreguntas")).Trim()
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



        public bool DeleteEncuestaPregunta(int id)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_PREGUNTA_ELIMINAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdPregunta", SqlDbType.Int);
                cmd.Parameters["@intIdPregunta"].Value = id;

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
            return true;
        }



        public bool AddEncuestaPregunta(BeEncuestaPregunta obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_PREGUNTA_INSERTAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@vchDesPreguntas", SqlDbType.VarChar, 400);
                cmd.Parameters["@vchDesPreguntas"].Value = obj.DesPreguntas;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
                catch (Exception)
                {
                    resultado = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }


        public bool EditEncuestaPregunta(BeEncuestaPregunta obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ADMIN_ENCUESTA_PREGUNTA_ACTUALIZAR", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdPregunta", SqlDbType.Int);
                cmd.Parameters.Add("@vchDesPreguntas", SqlDbType.VarChar, 400);

                cmd.Parameters["@intIdPregunta"].Value = obj.IdPregunta;
                cmd.Parameters["@vchDesPreguntas"].Value = obj.DesPreguntas;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
                catch (Exception)
                {
                    resultado = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }

    }
}
