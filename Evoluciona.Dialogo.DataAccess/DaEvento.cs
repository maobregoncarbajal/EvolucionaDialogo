
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DaEvento : DaConexion
    {
        public List<BeEvento> ObtenerEventos(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta)
        {
            var listaEventos = new List<BeEvento>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_Eventos", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime);
                cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime);
                cmd.Parameters.Add("@CodUsuario", SqlDbType.Char, 10);

                cmd.Parameters["@FechaDesde"].Value = fechaDesde.Date;
                cmd.Parameters["@FechaHasta"].Value = fechaHasta.Date;
                cmd.Parameters["@CodUsuario"].Value = codigoUsuario;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeEvento
                            {
                                IDEvento = reader.GetInt32(reader.GetOrdinal("intIDEvento")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("datFechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("datFechaFin")),
                                Campanha = reader.GetString(reader.GetOrdinal("chrCampanha")),
                                Reunion = reader.GetInt32(reader.GetOrdinal("intReunion")),
                                Evento = reader.GetInt32(reader.GetOrdinal("intEvento")),
                                SubEvento = reader.GetInt32(reader.GetOrdinal("intSubEvento")),
                                Evaluado = reader.GetString(reader.GetOrdinal("chrEvaluado")),
                                Asunto = reader.GetString(reader.GetOrdinal("vchAsunto")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodUsuario")),
                                RolUsuario = reader.GetInt32(reader.GetOrdinal("intIDRolUsuario")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                Filtro = reader.GetString(reader.GetOrdinal("Filtro"))
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

        public List<BeEvento> ObtenerEventosSinVisitas(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta)
        {
            var listaEventos = new List<BeEvento>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_Eventos_SinVisitas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime);
                cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime);
                cmd.Parameters.Add("@CodUsuario", SqlDbType.Char, 10);

                cmd.Parameters["@FechaDesde"].Value = fechaDesde.Date;
                cmd.Parameters["@FechaHasta"].Value = fechaHasta.Date;
                cmd.Parameters["@CodUsuario"].Value = codigoUsuario;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeEvento
                            {
                                IDEvento = reader.GetInt32(reader.GetOrdinal("intIDEvento")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("datFechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("datFechaFin")),
                                Campanha = reader.GetString(reader.GetOrdinal("chrCampanha")),
                                Reunion = reader.GetInt32(reader.GetOrdinal("intReunion")),
                                Evento = reader.GetInt32(reader.GetOrdinal("intEvento")),
                                SubEvento = reader.GetInt32(reader.GetOrdinal("intSubEvento")),
                                Evaluado = reader.GetString(reader.GetOrdinal("chrEvaluado")),
                                Asunto = reader.GetString(reader.GetOrdinal("vchAsunto")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodUsuario")),
                                RolUsuario = reader.GetInt32(reader.GetOrdinal("intIDRolUsuario")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                Filtro = reader.GetString(reader.GetOrdinal("Filtro"))
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

        public List<BeEvento> ObtenerEventos(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta, string filtro)
        {
            var listaEventos = new List<BeEvento>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_Eventos_Filtro", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime);
                cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime);
                cmd.Parameters.Add("@CodUsuario", SqlDbType.Char, 10);
                cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 5);

                cmd.Parameters["@FechaDesde"].Value = fechaDesde.Date;
                cmd.Parameters["@FechaHasta"].Value = fechaHasta.Date;
                cmd.Parameters["@CodUsuario"].Value = codigoUsuario;
                cmd.Parameters["@Filtro"].Value = filtro;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeEvento
                            {
                                IDEvento = reader.GetInt32(reader.GetOrdinal("intIDEvento")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("datFechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("datFechaFin")),
                                Campanha = reader.GetString(reader.GetOrdinal("chrCampanha")),
                                Reunion = reader.GetInt32(reader.GetOrdinal("intReunion")),
                                Evento = reader.GetInt32(reader.GetOrdinal("intEvento")),
                                SubEvento = reader.GetInt32(reader.GetOrdinal("intSubEvento")),
                                Evaluado = reader.GetString(reader.GetOrdinal("chrEvaluado")),
                                Asunto = reader.GetString(reader.GetOrdinal("vchAsunto")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodUsuario")),
                                RolUsuario = reader.GetInt32(reader.GetOrdinal("intIDRolUsuario")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                Filtro = reader.GetString(reader.GetOrdinal("Filtro"))
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

        public List<BeEvento> ObtenerEventosSinVisitas(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta, string filtro)
        {
            var listaEventos = new List<BeEvento>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_Eventos_FiltroSinVisitas", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime);
                cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime);
                cmd.Parameters.Add("@CodUsuario", SqlDbType.Char, 10);
                cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 5);

                cmd.Parameters["@FechaDesde"].Value = fechaDesde.Date;
                cmd.Parameters["@FechaHasta"].Value = fechaHasta.Date;
                cmd.Parameters["@CodUsuario"].Value = codigoUsuario;
                cmd.Parameters["@Filtro"].Value = filtro;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeEvento
                            {
                                IDEvento = reader.GetInt32(reader.GetOrdinal("intIDEvento")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("datFechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("datFechaFin")),
                                Campanha = reader.GetString(reader.GetOrdinal("chrCampanha")),
                                Reunion = reader.GetInt32(reader.GetOrdinal("intReunion")),
                                Evento = reader.GetInt32(reader.GetOrdinal("intEvento")),
                                SubEvento = reader.GetInt32(reader.GetOrdinal("intSubEvento")),
                                Evaluado = reader.GetString(reader.GetOrdinal("chrEvaluado")),
                                Asunto = reader.GetString(reader.GetOrdinal("vchAsunto")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodUsuario")),
                                RolUsuario = reader.GetInt32(reader.GetOrdinal("intIDRolUsuario")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                Filtro = reader.GetString(reader.GetOrdinal("Filtro"))
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

        public BeEvento ObtenerEvento(int idEvento)
        {
            BeEvento evento = null;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_Evento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDEvento", SqlDbType.Int);
                cmd.Parameters["@intIDEvento"].Value = idEvento;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            evento = new BeEvento
                            {
                                IDEvento = reader.GetInt32(reader.GetOrdinal("intIDEvento")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("datFechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("datFechaFin")),
                                Campanha = reader.GetString(reader.GetOrdinal("chrCampanha")),
                                Reunion = reader.GetInt32(reader.GetOrdinal("intReunion")),
                                Evento = reader.GetInt32(reader.GetOrdinal("intEvento")),
                                SubEvento = reader.GetInt32(reader.GetOrdinal("intSubEvento")),
                                Evaluado = reader.GetString(reader.GetOrdinal("chrEvaluado")),
                                Asunto = reader.GetString(reader.GetOrdinal("vchAsunto")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodUsuario")),
                                RolUsuario = reader.GetInt32(reader.GetOrdinal("intIDRolUsuario")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("datFechaCrea")),
                                Filtro = reader.GetString(reader.GetOrdinal("Filtro"))
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

        public int RegistrarEvento(BeEvento evento)
        {
            int idEvento;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Insertar_Evento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDEvento", SqlDbType.Int);
                cmd.Parameters.Add("@datFechaInicio", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaFin", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrCampanha", SqlDbType.Char, 6);
                cmd.Parameters.Add("@intReunion", SqlDbType.Int);
                cmd.Parameters.Add("@intEvento", SqlDbType.Int);
                cmd.Parameters.Add("@intSubEvento", SqlDbType.Int);
                cmd.Parameters.Add("@chrEvaluado", SqlDbType.Char, 10);
                cmd.Parameters.Add("@vchAsunto", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@chrCodUsuario", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intIDRolUsuario", SqlDbType.Int);

                cmd.Parameters["@intIDEvento"].Direction = ParameterDirection.Output;
                cmd.Parameters["@datFechaInicio"].Value = evento.FechaInicio;
                cmd.Parameters["@datFechaFin"].Value = evento.FechaFin;
                cmd.Parameters["@chrCampanha"].Value = evento.Campanha;
                cmd.Parameters["@intReunion"].Value = evento.Reunion;
                cmd.Parameters["@intEvento"].Value = evento.Evento;
                cmd.Parameters["@intSubEvento"].Value = evento.SubEvento;
                cmd.Parameters["@chrEvaluado"].Value = evento.Evaluado;
                cmd.Parameters["@vchAsunto"].Value = evento.Asunto;
                cmd.Parameters["@chrCodUsuario"].Value = evento.CodigoUsuario;
                cmd.Parameters["@intIDRolUsuario"].Value = evento.RolUsuario;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    idEvento = Convert.ToInt32(cmd.Parameters["@intIDEvento"].Value);
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

            return idEvento;
        }

        public void ActualizarEvento(BeEvento evento)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Actualizar_Evento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDEvento", SqlDbType.Int);
                cmd.Parameters.Add("@datFechaInicio", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaFin", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrCampanha", SqlDbType.Char, 6);
                cmd.Parameters.Add("@intReunion", SqlDbType.Int);
                cmd.Parameters.Add("@intEvento", SqlDbType.Int);
                cmd.Parameters.Add("@intSubEvento", SqlDbType.Int);
                cmd.Parameters.Add("@chrEvaluado", SqlDbType.Char, 10);
                cmd.Parameters.Add("@vchAsunto", SqlDbType.VarChar, 200);

                cmd.Parameters["@intIDEvento"].Value = evento.IDEvento;
                cmd.Parameters["@datFechaInicio"].Value = evento.FechaInicio;
                cmd.Parameters["@datFechaFin"].Value = evento.FechaFin;
                cmd.Parameters["@chrCampanha"].Value = evento.Campanha;
                cmd.Parameters["@intReunion"].Value = evento.Reunion;
                cmd.Parameters["@intEvento"].Value = evento.Evento;
                cmd.Parameters["@intSubEvento"].Value = evento.SubEvento;
                cmd.Parameters["@chrEvaluado"].Value = evento.Evaluado;
                cmd.Parameters["@vchAsunto"].Value = evento.Asunto;

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

        public void EliminarEvento(int idEvento)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Eliminar_Evento", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDEvento", SqlDbType.Int);
                cmd.Parameters["@intIDEvento"].Value = idEvento;

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

        public string ObtenerCadenaConexion(string codPais, string ffvv)
        {
            var cadenaConexion = string.Empty;

            using (var conn = ObtieneConexionESE_SK_MAIN())
            {
                var cmd = new SqlCommand("ESE_obtenerConexionPaisCluster", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrFFVV", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodigoPais"].Value = codPais;
                cmd.Parameters["@chrFFVV"].Value = ffvv;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cadenaConexion = reader.GetString(reader.GetOrdinal("vchCadenaConexion"));
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

            return cadenaConexion;
        }

        public List<string> ObtenerAnhos(string cadenaConexion)
        {
            var listaAnhos = new List<string>();

            using (var conn = new SqlConnection(cadenaConexion))
            {
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText =
                        "SELECT DISTINCT SUBSTRING(A.chrAnoCampana,1,4) AS anho FROM ESE_MAE_CRONOGRAMA A ORDER BY 1"
                };

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var anho = reader.GetString(reader.GetOrdinal("anho"));
                            listaAnhos.Add(anho);
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

            return listaAnhos;
        }

        public List<string> ObtenerNumerosCampanha(string cadenaConexion, string anho)
        {
            var listaNumerosCampanhas = new List<string>();

            using (var conn = new SqlConnection(cadenaConexion))
            {
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText =
                        "SELECT DISTINCT SUBSTRING(A.chrAnoCampana,5,2) AS campanha FROM ESE_MAE_CRONOGRAMA A WHERE SUBSTRING(A.chrAnoCampana,1,4) = '" +
                        anho + "' ORDER BY 1"
                };

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var campanha = reader.GetString(reader.GetOrdinal("campanha"));
                            listaNumerosCampanhas.Add(campanha);
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

            return listaNumerosCampanhas;
        }

        public BeEvento ObtenerCampanha(string cadenaConexion, string campanha, int rolUsuario, string codigoUsuario)
        {
            BeEvento evento = null;

            var codigoZonas = string.Empty;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ESE_ObtenerZonasPorRol"
                };

                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 10);

                cmd.Parameters["@intCodigoRol"].Value = rolUsuario;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            codigoZonas += "," + reader.GetString(reader.GetOrdinal("chrCodZona"));
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
                codigoZonas = string.IsNullOrEmpty(codigoZonas) ? codigoZonas : codigoZonas.Substring(1);
            }

            using (var conn = new SqlConnection(cadenaConexion))
            {
                var query = "DECLARE @zonas VARCHAR(100) SET @zonas = '" + codigoZonas + "' DECLARE @ListaZonas TABLE (codZona VARCHAR(6)) DECLARE @codigo VARCHAR(6) WHILE CHARINDEX(',',@zonas,0) <> 0 BEGIN SET @codigo=RTRIM(LTRIM(SUBSTRING(@zonas,1,CHARINDEX(',',@zonas,0)-1))) SET @zonas=RTRIM(LTRIM(SUBSTRING(@zonas,CHARINDEX(',',@zonas,0)+LEN(','),LEN(@zonas)))) IF LEN(@codigo) > 0 INSERT INTO @ListaZonas SELECT @codigo END IF LEN(@zonas) > 0 INSERT INTO @ListaZonas SELECT @zonas ";
                query += "SELECT C.campanha, MIN(C.FechaInicio) + 1 AS FechaInicio, MAX(C.FechaFin) AS FechaFin FROM (SELECT DISTINCT A.chrAnoCampana AS campanha, ISNULL((SELECT MAX(B.datFechaFacturacion) FROM ESE_MAE_CRONOGRAMA B INNER JOIN ESE_MAE_ZONA AS Z1 ON  Z1.intSEQIDZona = B.intSEQIDZona WHERE B.datFechaFacturacion < A.datFechaFacturacion ";
                if (rolUsuario != Constantes.RolDirectorVentas)
                {
                    query += "AND Z1.chrCodigoZona IN (SELECT codZona FROM @ListaZonas) ";
                }
                query += "), A.datFechaFacturacion - 1) AS FechaInicio, A.datFechaFacturacion AS FechaFin FROM ESE_MAE_CRONOGRAMA A INNER JOIN ESE_MAE_ZONA AS Z2 ON  Z2.intSEQIDZona = A.intSEQIDZona WHERE A.chrAnoCampana = '" + campanha + "' ";
                if (rolUsuario != Constantes.RolDirectorVentas)
                {
                    query += "AND Z2.chrCodigoZona IN (SELECT codZona FROM @ListaZonas) ";
                }
                query += ") AS C GROUP BY C.campanha ORDER BY 1 ASC";

                var cmd = new SqlCommand {Connection = conn, CommandType = CommandType.Text, CommandText = query};

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            evento = new BeEvento
                            {
                                Campanha = reader.GetString(reader.GetOrdinal("campanha")),
                                FechaInicio = reader.GetDateTime(reader.GetOrdinal("fechaInicio")),
                                FechaFin = reader.GetDateTime(reader.GetOrdinal("fechaFin"))
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

        public List<BeComun> ObtenerFiltros(string codigoEvaluador, int rolEvaluador)
        {
            var listaFiltros = new List<BeComun>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Seleccionar_FiltrosCampanha", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodEvaluador", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intRolEvaluador", SqlDbType.Int);

                cmd.Parameters["@chrCodEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@intRolEvaluador"].Value = rolEvaluador;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeComun
                            {
                                Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
                            };

                            listaFiltros.Add(evento);
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

            return listaFiltros;
        }

        public List<BeEvento> ObtenerEventosCampanha(string codigoEvaluador, int rolEvaluador, string codigoFiltro, DateTime fechaDesde, DateTime fechaHasta)
        {
            var listaEventos = new List<BeEvento>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Seleccionar_Eventos_Campanha", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@datFechaDesde", SqlDbType.DateTime);
                cmd.Parameters.Add("@datFechaHasta", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrCodEvaluador", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodFiltro", SqlDbType.VarChar, 5);
                cmd.Parameters.Add("@intRolEvaluador", SqlDbType.Int);

                cmd.Parameters["@datFechaDesde"].Value = fechaDesde.Date;
                cmd.Parameters["@datFechaHasta"].Value = fechaHasta.Date;
                cmd.Parameters["@chrCodEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@chrCodFiltro"].Value = codigoFiltro;
                cmd.Parameters["@intRolEvaluador"].Value = rolEvaluador;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeEvento
                            {
                                Filtro = reader.GetString(reader.GetOrdinal("codigo")),
                                FechaInicio = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("fecha"))),
                                FechaFin = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("fecha"))),
                                Asunto = reader.GetString(reader.GetOrdinal("asunto"))
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

        public List<BeEvento> ObtenerDetalleEventosCampanha(string codigoEvaluador, int rolEvaluador, string codigoFiltro, DateTime fecha)
        {
            var listaEventos = new List<BeEvento>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Seleccionar_Eventos_Campanha_Detalle", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@datFecha", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrCodEvaluador", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodFiltro", SqlDbType.VarChar, 5);
                cmd.Parameters.Add("@intRolEvaluador", SqlDbType.Int);

                cmd.Parameters["@datFecha"].Value = fecha.Date;
                cmd.Parameters["@chrCodEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@chrCodFiltro"].Value = codigoFiltro;
                cmd.Parameters["@intRolEvaluador"].Value = rolEvaluador;

                try
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new BeEvento
                            {
                                Filtro = reader.GetString(reader.GetOrdinal("codigo")),
                                FechaInicio = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("fecha"))),
                                FechaFin = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("fecha"))),
                                Asunto = reader.GetString(reader.GetOrdinal("asunto")),
                                Descripcion = reader.GetString(reader.GetOrdinal("filtro"))
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

        public List<string> ObtenerCampanhasPosiblesPorFecha(string codPais, string ffvv, int rolEvaluado, string codigoEvaluado, DateTime fecha)
        {
            var campanhas = new List<string>();
            var codigoZonas = string.Empty;

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ESE_ObtenerZonasPorRol"
                };

                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 10);

                cmd.Parameters["@intCodigoRol"].Value = rolEvaluado;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoEvaluado;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            codigoZonas += "," + reader.GetString(reader.GetOrdinal("chrCodZona"));
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
                codigoZonas = string.IsNullOrEmpty(codigoZonas) ? codigoZonas : codigoZonas.Substring(1);
            }

            var cadenaConexion = ObtenerCadenaConexion(codPais, ffvv);

            using (var conn = new SqlConnection(cadenaConexion))
            {
                var query = new StringBuilder();
                query.Append("DECLARE @zonas VARCHAR(100) ");
                query.Append("DECLARE @fecha datetime ");
                query.Append("SET @zonas = '" + codigoZonas + "' ");
                query.Append("SET @fecha = CONVERT(datetime,'" + fecha.ToString("dd/MM/yyyy") + "',103) ");
                query.Append("DECLARE @ListaZonas TABLE (codZona VARCHAR(6)) ");
                query.Append("DECLARE @codigo VARCHAR(6) ");
                query.Append("WHILE CHARINDEX(',',@zonas,0) <> 0 ");
                query.Append("BEGIN SET @codigo=RTRIM(LTRIM(SUBSTRING(@zonas,1,CHARINDEX(',',@zonas,0)-1))) ");
                query.Append("SET @zonas=RTRIM(LTRIM(SUBSTRING(@zonas,CHARINDEX(',',@zonas,0)+LEN(','),LEN(@zonas)))) ");
                query.Append("IF LEN(@codigo) > 0 INSERT INTO @ListaZonas SELECT @codigo END ");
                query.Append("IF LEN(@zonas) > 0 INSERT INTO @ListaZonas SELECT @zonas ");
                query.Append("SELECT DISTINCT MIN(chrAnoCampana) AS Campanha ");
                query.Append("FROM ESE_MAE_CRONOGRAMA A ");
                query.Append("WHERE CONVERT(CHAR(8),A.datFechaFacturacion,112) >= CONVERT(CHAR(8),@fecha,112) AND  A.tinSYSEstadoActivo = 1 ");
                if (rolEvaluado != Constantes.RolDirectorVentas)
                {
                    query.Append("AND intSEQIDZona IN (SELECT intSEQIDZona FROM ESE_MAE_ZONA WHERE chrCodigoZona IN (SELECT codZona FROM @ListaZonas)) ");
                }
                query.Append("GROUP BY intSEQIDZona ");
                query.Append("ORDER BY 1 DESC");

                var cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = query.ToString()
                };

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campanhas.Add(reader.GetString(reader.GetOrdinal("Campanha")));
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

            return campanhas;
        }
    }
}