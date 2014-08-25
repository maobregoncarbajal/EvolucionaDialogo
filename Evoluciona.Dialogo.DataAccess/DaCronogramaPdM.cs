using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Evoluciona.Dialogo.DataAccess
{
    public class DaCronogramaPdM : DaConexion
    {

        public List<BeCronogramaPdM> ListarCronogramaPdM()
        {
            var entidades = new List<BeCronogramaPdM>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_LISTAR", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = Constantes.Activo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeCronogramaPdM
                            {
                                IdCronogramaPdM =
                                    dr.IsDBNull(dr.GetOrdinal("intIdCronogramaPdM"))
                                        ? default(Int32)
                                        : dr.GetInt32(dr.GetOrdinal("intIdCronogramaPdM")),
                                PrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")).Trim(),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")).Trim(),
                                FechaLimite =
                                    dr.IsDBNull(dr.GetOrdinal("datFechaLimite"))
                                        ? default(DateTime)
                                        : dr.GetDateTime(dr.GetOrdinal("datFechaLimite")),
                                FechaProrroga =
                                    dr.IsDBNull(dr.GetOrdinal("datFechaProrroga"))
                                        ? default(DateTime?)
                                        : dr.GetDateTime(dr.GetOrdinal("datFechaProrroga")),
                                UsuarioCrea =
                                    dr.IsDBNull(dr.GetOrdinal("chrUsuarioCrea"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrUsuarioCrea")).Trim(),
                                FechaCrea =
                                    dr.IsDBNull(dr.GetOrdinal("datFechaCrea"))
                                        ? default(DateTime)
                                        : dr.GetDateTime(dr.GetOrdinal("datFechaCrea")),
                                UsuarioModi =
                                    dr.IsDBNull(dr.GetOrdinal("chrUsuarioModi"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrUsuarioModi")).Trim(),
                                FechaModi =
                                    dr.IsDBNull(dr.GetOrdinal("datFechaModi"))
                                        ? default(DateTime)
                                        : dr.GetDateTime(dr.GetOrdinal("datFechaModi")),
                                Estado =
                                    dr.IsDBNull(dr.GetOrdinal("bitEstado"))
                                        ? default(bool)
                                        : dr.GetBoolean(dr.GetOrdinal("bitEstado")),
                                OBePais = new BePais
                                {
                                    NombrePais = dr.IsDBNull(dr.GetOrdinal("vchNombrePais"))
                                    ? default(string)
                                    : dr.GetString(dr.GetOrdinal("vchNombrePais"))
                                }
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

        public bool AddCronogramaPdM(BeCronogramaPdM obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_ADD", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@datFechaLimite", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaProrroga", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrUsuarioCrea", SqlDbType.Char,20);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.PrefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = obj.Periodo;
                cmd.Parameters["@datFechaLimite"].Value = obj.FechaLimite;
                cmd.Parameters["@datFechaProrroga"].Value = obj.FechaProrroga;
                cmd.Parameters["@chrUsuarioCrea"].Value = obj.UsuarioCrea;
                cmd.Parameters["@bitEstado"].Value = obj.Estado;

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


        public bool EditCronogramaPdM(BeCronogramaPdM obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_EDIT", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@intIdCronogramaPdM", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@datFechaLimite", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaProrroga", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrUsuarioModi", SqlDbType.Char, 20);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIdCronogramaPdM"].Value = obj.IdCronogramaPdM;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.PrefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = obj.Periodo;
                cmd.Parameters["@datFechaLimite"].Value = obj.FechaLimite;
                cmd.Parameters["@datFechaProrroga"].Value = obj.FechaProrroga;
                cmd.Parameters["@chrUsuarioModi"].Value = obj.UsuarioModi;
                cmd.Parameters["@bitEstado"].Value = obj.Estado;

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

        public bool DelCronogramaPdM(BeCronogramaPdM obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_DEL", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@intIdCronogramaPdM", SqlDbType.Int);
                cmd.Parameters.Add("@chrUsuarioModi", SqlDbType.Char,20);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIdCronogramaPdM"].Value = obj.IdCronogramaPdM;
                cmd.Parameters["@chrUsuarioModi"].Value = obj.UsuarioModi;
                cmd.Parameters["@bitEstado"].Value = obj.Estado;

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

        public int ValidaCronogramaPdM(string pais, string periodo)
        {
            int cant;

            using (var cn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_VALIDA_PAIS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char,8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@intCant", SqlDbType.Int);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais.Trim();
                cmd.Parameters["@chrPeriodo"].Value = periodo.Trim();
                cmd.Parameters["@bitEstado"].Value = Constantes.Activo;
                cmd.Parameters["@intCant"].Direction = ParameterDirection.Output;

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cant = Convert.ToInt32(cmd.Parameters["@intCant"].Value);

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
            return cant;
        }


        public BeCronogramaPdM BuscarCronogramaPdM(BeUsuario objUsuario)
        {
            BeCronogramaPdM cronogramaMatriz;

            using (var cnn = ObtieneConexion())
            {
                cnn.Open();
                cronogramaMatriz = new BeCronogramaPdM();
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_BUSCAR", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = Constantes.Activo;
                var reader = cmd.ExecuteReader();
                if (!reader.Read()) return cronogramaMatriz;
                cronogramaMatriz.FechaLimite = reader.IsDBNull(reader.GetOrdinal("datFechaLimite")) ? (DateTime.MinValue) : reader.GetDateTime(reader.GetOrdinal("datFechaLimite"));
                cronogramaMatriz.FechaProrroga = reader.IsDBNull(reader.GetOrdinal("datFechaProrroga")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("datFechaProrroga"));
            }

            return cronogramaMatriz;
        }


        public string ValidarFechaAcuerdo(string codPais, string periodo)
        {
            var fechaAcuerdo = string.Empty;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_CRONOGRAMA_PDM_VALIDAR_FECHA", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = codPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.Activo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            fechaAcuerdo = dr.IsDBNull(dr.GetOrdinal("value")) ? default(string) : dr.GetString(dr.GetOrdinal("value"));
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

            return fechaAcuerdo;
        }


    }
}
