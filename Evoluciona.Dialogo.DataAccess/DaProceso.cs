
using System.Globalization;

namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaProceso : DaConexion
    {
        public List<BeProceso> FiltrarProcesos(TipoProceso tipoProceso, int idRol, string status, string periodo,
                                               int idUsuario, int idRolEvaluador)
        {
            var listaProcesos = new List<BeProceso>();

            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_FiltrarProcesos", cnn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intTipoProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrStatus", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIDUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@intIDRolEvaluador", SqlDbType.Int);

                cmd.Parameters[0].Value = (int)tipoProceso;
                cmd.Parameters[1].Value = idRol;
                cmd.Parameters[2].Value = status;
                cmd.Parameters[3].Value = periodo;
                cmd.Parameters[4].Value = idUsuario;
                cmd.Parameters[5].Value = idRolEvaluador;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var nuevoProceso = new BeProceso();

                    #region Cargando Valores en Objeto

                    if (!reader.IsDBNull(reader.GetOrdinal("intIDProceso")))
                        nuevoProceso.IdProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso"));

                    if (!reader.IsDBNull(reader.GetOrdinal("intIDRol")))
                        nuevoProceso.IdRol = reader.GetInt32(reader.GetOrdinal("intIDRol"));

                    if (!reader.IsDBNull(reader.GetOrdinal("chrEstado")))
                        nuevoProceso.Estado = int.Parse(reader.GetString(reader.GetOrdinal("chrEstado")));

                    nuevoProceso.NombrePersona = reader.GetString(reader.GetOrdinal("vchNombrePersona"));

                    nuevoProceso.Tipo =
                        (TipoProceso)
                            Enum.Parse(typeof(TipoProceso),
                                reader.GetInt32(reader.GetOrdinal("intTipoProceso")).ToString(CultureInfo.InvariantCulture));

                    if (!reader.IsDBNull(reader.GetOrdinal("chrPeriodo")))
                        nuevoProceso.Periodo = reader.GetString(reader.GetOrdinal("chrPeriodo"));

                    if (!reader.IsDBNull(reader.GetOrdinal("CantidadVisitasIniciadas")))
                        nuevoProceso.CantidadVisitasIniciadas =
                            reader.GetInt32(reader.GetOrdinal("CantidadVisitasIniciadas"));

                    if (!reader.IsDBNull(reader.GetOrdinal("CantidadVisitasCerradas")))
                        nuevoProceso.CantidadVisitasCerradas =
                            reader.GetInt32(reader.GetOrdinal("CantidadVisitasCerradas"));

                    nuevoProceso.CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodigoUsuario"));

                    #endregion Cargando Valores en Objeto

                    listaProcesos.Add(nuevoProceso);
                }
            }

            return listaProcesos;
        }

        public bool RegistrarNuevasIngresadas(int idProceso, int cantidadIngresadas)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_RegistrarNuevasIngresadas", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intCantidadNuevas", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = cantidadIngresadas;

                if (cmd.ExecuteNonQuery() > 0)
                    return true;

                return false;
            }
        }



        public List<BeProceso> ObtenerProcesos(string periodo, int rolEvaluado, string codigoPais, string tipoDialogo, string evaluador)
        {
            var listaProcesos = new List<BeProceso>();

            using (var cnn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ObtenerProcesosAdmin", cnn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrTipoDialogo", SqlDbType.Char, 1);
                cmd.Parameters.Add("@chrEvaluador", SqlDbType.Char, 20);

                cmd.Parameters[0].Value = periodo;
                cmd.Parameters[1].Value = rolEvaluado;
                cmd.Parameters[2].Value = codigoPais;
                cmd.Parameters[3].Value = tipoDialogo;
                cmd.Parameters[4].Value = evaluador;

                try
                {
                    cnn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var proceso = new BeProceso
                            {
                                IdProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso")),
                                IdRol = reader.GetInt32(reader.GetOrdinal("intCodigoRol")),
                                Estado = Convert.ToInt32(reader.GetString(reader.GetOrdinal("chrEstado"))),
                                Periodo = reader.GetString(reader.GetOrdinal("chrPeriodo")),
                                NombrePersona = reader.GetString(reader.GetOrdinal("vchNombrePersona")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodigoUsuario"))
                            };

                            listaProcesos.Add(proceso);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }

            return listaProcesos;
        }



        public BeProceso ObtenerProceso(int idProceso)
        {
            var proceso = new BeProceso();

            using (var cnn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ObtenerProceso", cnn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;

                try
                {
                    cnn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proceso = new BeProceso
                            {
                                IdProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso")),
                                IdRol = reader.GetInt32(reader.GetOrdinal("intIDRol")),
                                Periodo = reader.GetString(reader.GetOrdinal("chrPeriodo")),
                                CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodigoUsuario")),
                                CodigoUsuarioEvaluador =
                                    reader.GetString(reader.GetOrdinal("chrCodigoUsuarioEvaluador")),
                                Estado = Convert.ToInt32(reader.GetString(reader.GetOrdinal("chrEstadoProceso")))
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
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }

            return proceso;
        }

        #region "Mantenimiento BELCORP - DATAMART"

        public List<BeProceso> ProcesoListarCodigoUsuario(string codigoUsuario, string prefijoIsoPais)
        {
            var obeProceso = new List<BeProceso>();

            try
            {
                using (var cnn = ObtieneConexion())
                {
                    cnn.Open();

                    var cmd = new SqlCommand("ESE_SP_PROCESO_LISTAR_CODIGOUSUARIO", cnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20).Value = codigoUsuario;
                    cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var ibeProceso = new BeProceso();

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDProceso")))
                            ibeProceso.IdProceso = reader.GetInt32(reader.GetOrdinal("intIDProceso"));

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDRol")))
                            ibeProceso.IdRol = reader.GetInt32(reader.GetOrdinal("intIDRol"));

                        ibeProceso.CodigoUsuario = reader.GetString(reader.GetOrdinal("chrCodigoUsuario"));

                        ibeProceso.obeGerenteZona = new BeGerenteZona
                        {
                            vchNombreCompleto = reader.GetString(reader.GetOrdinal("vchNombreCompleto"))
                        };

                        ibeProceso.Periodo = reader.GetString(reader.GetOrdinal("chrPeriodo"));

                        if (!reader.IsDBNull(reader.GetOrdinal("datFechaLimiteProceso")))
                            ibeProceso.datFechaLimiteProceso =
                                reader.GetDateTime(reader.GetOrdinal("datFechaLimiteProceso"));

                        ibeProceso.chrEstadoProceso = reader.GetString(reader.GetOrdinal("chrEstadoProceso"));

                        if (!reader.IsDBNull(reader.GetOrdinal("intIDRolEvaluador")))
                            ibeProceso.intIDRolEvaluador = reader.GetInt32(reader.GetOrdinal("intIDRolEvaluador"));

                        ibeProceso.CodigoUsuarioEvaluador =
                            reader.GetString(reader.GetOrdinal("chrCodigoUsuarioEvaluador"));

                        ibeProceso.chrPrefijoIsoPais = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais"));
                        if (!reader.IsDBNull(reader.GetOrdinal("intNuevasIngresadas")))
                            ibeProceso.nuevasIngresadas = reader.GetInt32(reader.GetOrdinal("intNuevasIngresadas"));

                        if (!reader.IsDBNull(reader.GetOrdinal("chrRegionZona")))
                            ibeProceso.chrRegionZona = reader.GetString(reader.GetOrdinal("chrRegionZona"));



                        obeProceso.Add(ibeProceso);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return obeProceso;
        }

        public bool ProcesoRegistrar(BeProceso obeProceso)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_PROCESO_INSERTAR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int).Value = obeProceso.IdRol;
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20).Value = obeProceso.CodigoUsuario;
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8).Value = obeProceso.Periodo;
                cmd.Parameters.Add("@datFechaLimiteProceso", SqlDbType.DateTime).Value =
                    obeProceso.datFechaLimiteProceso;
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1).Value = obeProceso.chrEstadoProceso;
                cmd.Parameters.Add("@intIDRolEvaluador", SqlDbType.Int).Value = obeProceso.intIDRolEvaluador;
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20).Value =
                    obeProceso.CodigoUsuarioEvaluador;
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeProceso.chrPrefijoIsoPais;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeProceso.intUsuarioCrea;
                cmd.Parameters.Add("@intNuevasIngresadas", SqlDbType.Int).Value = obeProceso.nuevasIngresadas;
                cmd.Parameters.Add("@chrRegionZona", SqlDbType.Char, 10).Value = obeProceso.chrRegionZona;
                cmd.Parameters.Add("@datFechaInicioProceso", SqlDbType.DateTime).Value =
                    obeProceso.datFechaInicioProceso;
                cmd.Parameters.Add("@bitPlanMejora", SqlDbType.Bit).Value = obeProceso.bitPlanMejora;

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

        public bool ProcesoActualizar(BeProceso obeProceso)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_PROCESO_ACTUALIZAR", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int).Value = obeProceso.IdProceso;
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int).Value = obeProceso.IdRol;
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20).Value = obeProceso.CodigoUsuario;
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1).Value = obeProceso.chrEstadoProceso;
                cmd.Parameters.Add("@intIDRolEvaluador", SqlDbType.Int).Value = obeProceso.intIDRolEvaluador;
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20).Value =
                    obeProceso.CodigoUsuarioEvaluador;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeProceso.intUsuarioCrea;
                cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = obeProceso.tipo;

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

        public bool ProcesoActualizarEstado(BeProceso obeProceso)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_PROCESO_ACTUALIZAR_ESTADO", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int).Value = obeProceso.IdProceso;
                cmd.Parameters.Add("@intUsuarioCrea", SqlDbType.Int).Value = obeProceso.intUsuarioCrea;

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

        #endregion "Mantenimiento BELCORP - DATAMART"
    }
}