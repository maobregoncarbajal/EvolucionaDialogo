
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaAdmin : DaConexion
    {
        public List<BeAdmin> ListarAdministradores(string codigoPais, string tipoAdmin)
        {
            var administradores = new List<BeAdmin>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ObtenerAdministradores", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrTipoAdmin", SqlDbType.Char, 1);

                cmd.Parameters["@chrCodigoPais"].Value = codigoPais;
                cmd.Parameters["@chrTipoAdmin"].Value = tipoAdmin;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var administrador = new BeAdmin
                            {
                                ClaveAdmin = reader.GetString(reader.GetOrdinal("vchClaveAdmin")),
                                CodigoAdmin = reader.GetString(reader.GetOrdinal("chrCodigoAdmin")),
                                CodigoPais = reader.GetString(reader.GetOrdinal("chrCodigoPais")),
                                Estado = reader.GetBoolean(reader.GetOrdinal("bitEstado")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("bitAdmin")),
                                FechaCrea =
                                    reader.IsDBNull(reader.GetOrdinal("datFechaCrea"))
                                        ? default(DateTime?)
                                        : reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                FechaModi =
                                    reader.IsDBNull(reader.GetOrdinal("datFechaModi"))
                                        ? default(DateTime?)
                                        : reader.GetDateTime(reader.GetOrdinal("datFechaModi")),
                                IDAdmin = reader.GetInt32(reader.GetOrdinal("intIDAdmin")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("vchNombreCompleto")),
                                TipoAdmin = reader.GetString(reader.GetOrdinal("chrTipoAdmin")),
                                UsuarioCrea =
                                    reader.IsDBNull(reader.GetOrdinal("intUsuarioCrea"))
                                        ? default(int?)
                                        : reader.GetInt32(reader.GetOrdinal("intUsuarioCrea")),
                                UsuarioModi =
                                    reader.IsDBNull(reader.GetOrdinal("intUsuarioModi"))
                                        ? default(int?)
                                        : reader.GetInt32(reader.GetOrdinal("intUsuarioModi"))
                            };

                            administradores.Add(administrador);
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

            return administradores;
        }

        public BeAdmin ObtenerAdministrador(int idAdmin)
        {
            BeAdmin administrador = null;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ObtenerAdministrador", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDAdmin", SqlDbType.Int);

                cmd.Parameters["@intIDAdmin"].Value = idAdmin;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            administrador = new BeAdmin
                            {
                                ClaveAdmin = reader.GetString(reader.GetOrdinal("vchClaveAdmin")),
                                CodigoAdmin = reader.GetString(reader.GetOrdinal("chrCodigoAdmin")),
                                CodigoPais = reader.GetString(reader.GetOrdinal("chrCodigoPais")),
                                Estado = reader.GetBoolean(reader.GetOrdinal("bitEstado")),
                                Admin = reader.GetBoolean(reader.GetOrdinal("bitAdmin")),
                                FechaCrea =
                                    reader.IsDBNull(reader.GetOrdinal("datFechaCrea"))
                                        ? default(DateTime?)
                                        : reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                FechaModi =
                                    reader.IsDBNull(reader.GetOrdinal("datFechaModi"))
                                        ? default(DateTime?)
                                        : reader.GetDateTime(reader.GetOrdinal("datFechaModi")),
                                IDAdmin = reader.GetInt32(reader.GetOrdinal("intIDAdmin")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("vchNombreCompleto")),
                                TipoAdmin = reader.GetString(reader.GetOrdinal("chrTipoAdmin")),
                                UsuarioCrea =
                                    reader.IsDBNull(reader.GetOrdinal("intUsuarioCrea"))
                                        ? default(int?)
                                        : reader.GetInt32(reader.GetOrdinal("intUsuarioCrea")),
                                UsuarioModi =
                                    reader.IsDBNull(reader.GetOrdinal("intUsuarioModi"))
                                        ? default(int?)
                                        : reader.GetInt32(reader.GetOrdinal("intUsuarioModi"))
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

            return administrador;
        }

        public void AgregarAdminsitrador(BeAdmin administrador)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_InsertarAdministrador", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoAdmin", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchClaveAdmin", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrTipoAdmin", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@bitAdmin", SqlDbType.Bit);
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int);

                cmd.Parameters["@chrCodigoAdmin"].Value = administrador.CodigoAdmin;
                cmd.Parameters["@chrCodigoPais"].Value = administrador.CodigoPais;
                cmd.Parameters["@vchClaveAdmin"].Value = administrador.ClaveAdmin;
                cmd.Parameters["@vchNombreCompleto"].Value = administrador.NombreCompleto;
                cmd.Parameters["@chrTipoAdmin"].Value = administrador.TipoAdmin;
                cmd.Parameters["@bitEstado"].Value = administrador.Estado;
                cmd.Parameters["@bitAdmin"].Value = administrador.Admin;
                cmd.Parameters["@intUsuarioCrea"].Value = administrador.UsuarioCrea;

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

        public void ActualizarAdministrador(BeAdmin administrador)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ActualizarAdministrador", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDAdmin", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoAdmin", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchClaveAdmin", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@vchNombreCompleto", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrTipoAdmin", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@bitAdmin", SqlDbType.Bit);
                cmd.Parameters.Add("@intUsuarioModi", SqlDbType.Int);
                cmd.Parameters.Add("@datFechaModi", SqlDbType.DateTime);

                cmd.Parameters["@intIDAdmin"].Value = administrador.IDAdmin;
                cmd.Parameters["@chrCodigoAdmin"].Value = administrador.CodigoAdmin;
                cmd.Parameters["@chrCodigoPais"].Value = administrador.CodigoPais;
                cmd.Parameters["@vchClaveAdmin"].Value = administrador.ClaveAdmin;
                cmd.Parameters["@vchNombreCompleto"].Value = administrador.NombreCompleto;
                cmd.Parameters["@chrTipoAdmin"].Value = administrador.TipoAdmin;
                cmd.Parameters["@bitEstado"].Value = administrador.Estado;
                cmd.Parameters["@bitAdmin"].Value = administrador.Admin;
                cmd.Parameters["@intUsuarioModi"].Value = administrador.UsuarioModi;
                cmd.Parameters["@datFechaModi"].Value = administrador.FechaModi;

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

        public void EliminarAdministrador(int idAdmin)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_EliminarAdministrador", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDAdmin", SqlDbType.Int);

                cmd.Parameters["@intIDAdmin"].Value = idAdmin;

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

        public BeAdmin VerificarAdmin(string codigo, string clave, string tipo, string codPais)
        {
            BeAdmin admin = null;

            using (var cnn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_VerificarAdmin", cnn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigo", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchClave", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrTipoAdmin", SqlDbType.Char, 1);
                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);

                cmd.Parameters[0].Value = codigo;
                cmd.Parameters[1].Value = clave;
                cmd.Parameters[2].Value = tipo;
                cmd.Parameters[3].Value = codPais;

                try
                {
                    cnn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        admin = new BeAdmin
                        {
                            IDAdmin = reader.GetInt32(reader.GetOrdinal("intIDAdmin")),
                            CodigoAdmin = reader.GetString(reader.GetOrdinal("chrCodigoAdmin")),
                            ClaveAdmin = reader.GetString(reader.GetOrdinal("vchClaveAdmin")),
                            NombreCompleto = reader.GetString(reader.GetOrdinal("vchNombreCompleto")),
                            TipoAdmin = reader.GetString(reader.GetOrdinal("chrTipoAdmin")),
                            CodigoPais = reader.GetString(reader.GetOrdinal("chrCodigoPais")),
                            Estado = reader.GetBoolean(reader.GetOrdinal("bitEstado")),
                            UsuarioCrea =
                                reader.IsDBNull(reader.GetOrdinal("intUsuarioCrea"))
                                    ? (int?) null
                                    : reader.GetInt32(reader.GetOrdinal("intUsuarioCrea")),
                            FechaCrea =
                                reader.IsDBNull(reader.GetOrdinal("datFechaCrea"))
                                    ? (DateTime?) null
                                    : reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                            UsuarioModi =
                                reader.IsDBNull(reader.GetOrdinal("intUsuarioModi"))
                                    ? (int?) null
                                    : reader.GetInt32(reader.GetOrdinal("intUsuarioModi")),
                            FechaModi =
                                reader.IsDBNull(reader.GetOrdinal("datFechaModi"))
                                    ? (DateTime?) null
                                    : reader.GetDateTime(reader.GetOrdinal("datFechaModi")),
                            NombrePais = reader.GetString(reader.GetOrdinal("vchNombrePais")),
                            DescripcionAdmin = reader.GetString(reader.GetOrdinal("vchDescripcionAdmin")),
                            ImagenPais = reader.GetString(reader.GetOrdinal("vchImagenPais"))
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }

            return admin;
        }
    }
}
