
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaTipoEvento : DaConexion
    {
        public List<BeTipoEvento> ObtenerTipoEventos(int tipoReunion, int codigoRol)
        {
            var listaEventos = new List<BeTipoEvento>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Obtener_TiposEventos", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intTipoReunion", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);

                cmd.Parameters["@intTipoReunion"].Value = tipoReunion;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeTipoEvento
                            {
                                IDTipoEvento = reader.GetInt32(reader.GetOrdinal("intIDTipoEvento")),
                                Descripcion = reader.GetString(reader.GetOrdinal("vchDescTipo")),
                                IDPadre = reader.GetInt32(reader.GetOrdinal("intIDPadre")),
                                FechaCrea = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                CodigoRol = reader.GetInt32(reader.GetOrdinal("intCodigoRol"))
                            };

                            listaEventos.Add(evento);
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

            return listaEventos;
        }

        public List<BeTipoEvento> ObtenerSubEventos(int idPadre)
        {
            var listaEventos = new List<BeTipoEvento>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Obtener_SubEventos", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDPadre", SqlDbType.Int);
                cmd.Parameters["@intIDPadre"].Value = idPadre;

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeTipoEvento
                            {
                                IDTipoEvento = reader.GetInt32(reader.GetOrdinal("intIDTipoEvento")),
                                Descripcion = reader.GetString(reader.GetOrdinal("vchDescTipo")),
                                IDPadre = reader.GetInt32(reader.GetOrdinal("intIDPadre")),
                                FechaCrea = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                CodigoRol = reader.GetInt32(reader.GetOrdinal("intCodigoRol"))
                            };

                            listaEventos.Add(evento);
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

            return listaEventos;
        }

        public BeTipoEvento ObtenerTipoEvento(int idTipoEvento)
        {
            BeTipoEvento evento = null;

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Obtener_TipoEvento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDTipoEvento", SqlDbType.Int);
                cmd.Parameters["@intIDTipoEvento"].Value = idTipoEvento;

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            evento = new BeTipoEvento
                            {
                                IDTipoEvento = reader.GetInt32(reader.GetOrdinal("intIDTipoEvento")),
                                Descripcion = reader.GetString(reader.GetOrdinal("vchDescTipo")),
                                IDPadre = reader.GetInt32(reader.GetOrdinal("intIDPadre")),
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

            return evento;
        }

        public void RegistrarTipoEvento(BeTipoEvento tipoEvento)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_TipoEvento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDTipoEvento", SqlDbType.Int);
                cmd.Parameters.Add("@vchDescTipo", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@intIDPadre", SqlDbType.Int);

                cmd.Parameters["@intIDTipoEvento"].Direction = ParameterDirection.Output;
                cmd.Parameters["@vchDescTipo"].Value = tipoEvento.Descripcion;
                cmd.Parameters["@intIDPadre"].Value = tipoEvento.IDPadre;

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

                    conn.Dispose();
                }
            }
        }

        public void ActualizarTipoEvento(BeTipoEvento tipoEvento)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Actualizar_TipoEvento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDTipoEvento", SqlDbType.Int);
                cmd.Parameters.Add("@vchDescTipo", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@intIDPadre", SqlDbType.Int);

                cmd.Parameters["@intIDTipoEvento"].Value = tipoEvento.IDTipoEvento;
                cmd.Parameters["@vchDescTipo"].Value = tipoEvento.Descripcion;
                cmd.Parameters["@intIDPadre"].Value = tipoEvento.IDPadre;

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

                    conn.Dispose();
                }
            }
        }

        public void EliminarTipoEvento(int idTipoEvento)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Eliminar_TipoEvento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDTipoEvento", SqlDbType.Int);
                cmd.Parameters["@intIDTipoEvento"].Value = idTipoEvento;

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

                    conn.Dispose();
                }
            }
        }
    }
}