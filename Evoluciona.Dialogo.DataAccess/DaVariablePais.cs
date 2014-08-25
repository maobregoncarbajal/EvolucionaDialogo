
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaVariablePais : DaConexion
    {
        public List<BeVariablePais>  ListarVariablesPorPais(string codigoPais)
        {          
            var variables = new List<BeVariablePais>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_VariablesPais", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodigoPais"].Value = codigoPais;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var variable = new BeVariablePais
                            {
                                IDVariablePais = reader.GetInt32(reader.GetOrdinal("intIDVariablePais")),
                                CodigoVariable = reader.GetString(reader.GetOrdinal("chrCodigoVariable")),
                                DescripcionVariable = reader.GetString(reader.GetOrdinal("vchrDesVariable")),
                                CodigoPais = reader.GetString(reader.GetOrdinal("chrCodigoPais")),
                                FechaCrea = reader.GetDateTime(reader.GetOrdinal("datFechaCrea"))
                            };

                            variables.Add(variable);
                        }
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

                    conn.Dispose();
                }
            }

            return variables;
        }

        public List<BeVariablePais> ListarVariablesDisponiblesPorPais(string codigoPais)
        {
            var variables = new List<BeVariablePais>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_VariablesDisponibles_PorPais", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodigoPais"].Value = codigoPais;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var variable = new BeVariablePais
                            {
                                CodigoVariable = reader.GetString(reader.GetOrdinal("chrCodVariable")),
                                DescripcionVariable = reader.GetString(reader.GetOrdinal("vchrDesVariable"))
                            };

                            variables.Add(variable);
                        }
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

                    conn.Dispose();
                }
            }

            return variables;
        }

        public BeVariablePais ObtenerVariable(int idVariablePais)
        {
            BeVariablePais variable = null;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_VariablePais", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDVariablePais", SqlDbType.Int);

                cmd.Parameters["@intIDVariablePais"].Value = idVariablePais;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            variable = new BeVariablePais
                            {
                                IDVariablePais = reader.GetInt32(reader.GetOrdinal("intIDVariablePais")),
                                CodigoVariable = reader.GetString(reader.GetOrdinal("chrCodigoVariable")),
                                DescripcionVariable = reader.GetString(reader.GetOrdinal("vchrDesVariable")),
                                CodigoPais = reader.GetString(reader.GetOrdinal("chrCodigoPais")),
                                FechaCrea = reader.GetDateTime(reader.GetOrdinal("datFechaCrea"))
                            };
                        }
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

                    conn.Dispose();
                }
            }

            return variable;
        }

        public void AgregarVariable(BeVariablePais variable)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Insertar_VariablePais", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoVariable", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int);

                cmd.Parameters["@chrCodigoVariable"].Value = variable.CodigoVariable;
                cmd.Parameters["@chrCodigoPais"].Value = variable.CodigoPais;
                cmd.Parameters["@intUsuarioCrea"].Value = variable.UsuarioCrea;

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

        public void EliminarVariable(int idVariablePais)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Eliminar_VariablePais", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDVariablePais", SqlDbType.Int);

                cmd.Parameters["@intIDVariablePais"].Value = idVariablePais;

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
