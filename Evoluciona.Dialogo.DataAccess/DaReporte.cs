
using System.Globalization;

namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaReporte : DaConexion
    {
        public List<BePais> ObtenerPaisesUsuario(string nivel, string codigo, string prefijoIsoPais)
        {
            var paises = new List<BePais>();

            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_ListarPaisesUsuario", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivel;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigo;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pais = new BePais
                            {
                                prefijoIsoPais = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais")),
                                NombrePais = reader.GetString(reader.GetOrdinal("vchNombrePais"))
                            };

                            paises.Add(pais);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }

            return paises;
        }
        /// <summary>
        /// Retorna la lista con todos los seguimientos de estatus
        /// </summary>
        /// <returns></returns>
        public List<BeSeguimientoStatus> ListarSeguimientoStatus(string periodo, string pais, string nivel, int estado)
        {
            var estadoAlt = estado == 4 ? 0 : estado;

            var entidades = new List<BeSeguimientoStatus>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SeguimientoStatusResumen", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrEstado", SqlDbType.Char, 1);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrEstado"].Value = estadoAlt.ToString(CultureInfo.InvariantCulture);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entidad = new BeSeguimientoStatus
                    {
                        Periodo =
                            dr.IsDBNull(dr.GetOrdinal("Periodo"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("Periodo")),
                        Pais =
                            dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais")),
                        Colaborador =
                            dr.IsDBNull(dr.GetOrdinal("Colaborador")) ? 0 : dr.GetInt32(dr.GetOrdinal("Colaborador")),
                        DialogoNoAbierto =
                            dr.IsDBNull(dr.GetOrdinal("DialogoNoAbierto"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoNoAbierto")),
                        DialogoProceso =
                            dr.IsDBNull(dr.GetOrdinal("DialogoProceso"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoProceso")),
                        DialogoAprobacion =
                            dr.IsDBNull(dr.GetOrdinal("DialogoAprobacion"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoAprobacion")),
                        DialogoCerrado =
                            dr.IsDBNull(dr.GetOrdinal("DialogoCerrado"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoCerrado")),
                        PlanAccion =
                            dr.IsDBNull(dr.GetOrdinal("PlanAccion")) ? 0 : dr.GetInt32(dr.GetOrdinal("PlanAccion")),
                        RetroAlimentacion =
                            dr.IsDBNull(dr.GetOrdinal("RetroAlimentacion"))
                                ? 0
                                : dr.GetInt32(dr.GetOrdinal("RetroAlimentacion"))
                    };

                    if (entidad.Colaborador != 0)
                    {
                        if (estado != 4)
                        {
                            entidad.DialogoNoAbierto = estado != 0 ? 0 : Math.Round((entidad.DialogoNoAbierto) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoProceso = Math.Round((entidad.DialogoProceso) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoAprobacion = Math.Round((entidad.DialogoAprobacion) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoCerrado = Math.Round((entidad.DialogoCerrado) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.AvancePlanAccion = Math.Round(Convert.ToDecimal(entidad.PlanAccion) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.AvanceRetroAlimentacion = Math.Round(Convert.ToDecimal(entidad.RetroAlimentacion) / Convert.ToDecimal(entidad.Colaborador), 2);
                        }
                        else
                        {
                            entidad.DialogoNoAbierto = Math.Round((entidad.DialogoNoAbierto) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoProceso = 0;
                            entidad.DialogoAprobacion = 0;
                            entidad.DialogoCerrado = 0;
                            entidad.AvancePlanAccion = 0;
                            entidad.AvanceRetroAlimentacion = 0;
                            entidad.PlanAccion = 0;
                            entidad.RetroAlimentacion = 0;
                        }
                    }

                    entidades.Add(entidad);
                }

                dr.Close();
            }
            return entidades;
        }

        /// <summary>
        /// Retorna la lista con todos los seguimientos de estatus segun el usuario
        /// </summary>
        /// <returns></returns>
        public List<BeSeguimientoStatus> ListarSeguimientoStatusByUsuario(string periodo, string pais, string nivel, int estado, string codigoUsuario,
                                                                          string nivelEvaluador)
        {
            var estadoAlt = estado == 4 ? 0 : estado;

            var entidades = new List<BeSeguimientoStatus>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SeguimientoStatusResumenUsuario", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrEstado", SqlDbType.Char, 1);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrEstado"].Value = estadoAlt.ToString(CultureInfo.InvariantCulture);
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entidad = new BeSeguimientoStatus
                    {
                        Periodo =
                            dr.IsDBNull(dr.GetOrdinal("Periodo"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("Periodo")),
                        Pais =
                            dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais")),
                        Colaborador =
                            dr.IsDBNull(dr.GetOrdinal("Colaborador")) ? 0 : dr.GetInt32(dr.GetOrdinal("Colaborador")),
                        DialogoNoAbierto =
                            dr.IsDBNull(dr.GetOrdinal("DialogoNoAbierto"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoNoAbierto")),
                        DialogoProceso =
                            dr.IsDBNull(dr.GetOrdinal("DialogoProceso"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoProceso")),
                        DialogoAprobacion =
                            dr.IsDBNull(dr.GetOrdinal("DialogoAprobacion"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoAprobacion")),
                        DialogoCerrado =
                            dr.IsDBNull(dr.GetOrdinal("DialogoCerrado"))
                                ? 0
                                : dr.GetDecimal(dr.GetOrdinal("DialogoCerrado")),
                        PlanAccion =
                            dr.IsDBNull(dr.GetOrdinal("PlanAccion")) ? 0 : dr.GetInt32(dr.GetOrdinal("PlanAccion")),
                        RetroAlimentacion =
                            dr.IsDBNull(dr.GetOrdinal("RetroAlimentacion"))
                                ? 0
                                : dr.GetInt32(dr.GetOrdinal("RetroAlimentacion"))
                    };

                    if (entidad.Colaborador != 0)
                    {
                        if (estado != 4)
                        {
                            entidad.DialogoNoAbierto = estado != 0 ? 0 : Math.Round((entidad.DialogoNoAbierto) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoProceso = Math.Round((entidad.DialogoProceso) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoAprobacion = Math.Round((entidad.DialogoAprobacion) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoCerrado = Math.Round((entidad.DialogoCerrado) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.AvancePlanAccion = Math.Round(Convert.ToDecimal(entidad.PlanAccion) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.AvanceRetroAlimentacion = Math.Round(Convert.ToDecimal(entidad.RetroAlimentacion) / Convert.ToDecimal(entidad.Colaborador), 2);
                        }
                        else
                        {
                            entidad.DialogoNoAbierto = Math.Round((entidad.DialogoNoAbierto) / Convert.ToDecimal(entidad.Colaborador), 2);
                            entidad.DialogoProceso = 0;
                            entidad.DialogoAprobacion = 0;
                            entidad.DialogoCerrado = 0;
                            entidad.AvancePlanAccion = 0;
                            entidad.AvanceRetroAlimentacion = 0;
                            entidad.PlanAccion = 0;
                            entidad.RetroAlimentacion = 0;
                        }
                    }

                    entidades.Add(entidad);
                }

                dr.Close();
            }
            return entidades;
        }

        ///// <summary>
        ///// Retorna la lista con todos los seguimientos de estatus Detalle
        ///// </summary>
        ///// <returns></returns>
        //public List<beSeguimientoStatusDetalle> ListarSeguimientoStatusDetalle(string periodo, string nombreColaborador, string nivel, string pais, string zona,
        //                                                                       string nombreJefe, string estado, string region)
        //{
        //    List<beSeguimientoStatusDetalle> entidades = new List<beSeguimientoStatusDetalle>();
        //    using (SqlConnection cn = daConexion.ObtieneConexionJOB())
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand("ESE_SeguimientoStatusDetalle", cn) { CommandType = CommandType.StoredProcedure };

        //        cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
        //        cmd.Parameters.Add("@chrNombreColaborador", SqlDbType.NChar, 100);
        //        cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
        //        cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
        //        cmd.Parameters.Add("@chrZona", SqlDbType.NChar, 4);
        //        cmd.Parameters.Add("@chrNombreJefe", SqlDbType.NChar, 100);
        //        cmd.Parameters.Add("@chrEstado", SqlDbType.Char, 1);
        //        cmd.Parameters.Add("@chrRegion", SqlDbType.NChar, 4);

        //        cmd.Parameters["@chrPeriodo"].Value = periodo;
        //        cmd.Parameters["@chrNombreColaborador"].Value = nombreColaborador;
        //        cmd.Parameters["@chrNivel"].Value = nivel;
        //        cmd.Parameters["@chrPais"].Value = pais;
        //        cmd.Parameters["@chrZona"].Value = zona;
        //        cmd.Parameters["@chrNombreJefe"].Value = nombreJefe;
        //        cmd.Parameters["@chrEstado"].Value = estado;
        //        cmd.Parameters["@chrRegion"].Value = region;

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            beSeguimientoStatusDetalle entidad = new beSeguimientoStatusDetalle();
        //            entidad.Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo"));
        //            entidad.Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"));
        //            entidad.Colaborador = dr.IsDBNull(dr.GetOrdinal("Colaborador")) ? default(string) : dr.GetString(dr.GetOrdinal("Colaborador"));
        //            entidad.Nivel = dr.IsDBNull(dr.GetOrdinal("Nivel")) ? default(string) : dr.GetString(dr.GetOrdinal("Nivel"));
        //            entidad.Jefe = dr.IsDBNull(dr.GetOrdinal("Jefe")) ? default(string) : dr.GetString(dr.GetOrdinal("Jefe"));
        //            entidad.Estatus = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? default(string) : dr.GetString(dr.GetOrdinal("Estatus"));
        //            entidades.Add(entidad);
        //        }
        //        dr.Close();
        //    }

        //    return entidades;
        //}


        /// <summary>
        /// Retorna la lista con todos los seguimientos de estatus Detalle
        /// </summary>
        /// <returns></returns>
        public List<BeSeguimientoStatusDetalle> ListarStatusDialogosDetalle(string periodo, string nombreColaborador, string nivel, string pais, string nombreJefe, string estado, string tipo, bool usuIna, string modeloDialogo)
        {
            var entidades = new List<BeSeguimientoStatusDetalle>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SeguimientoStatusDialogosDetalle", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrNombreColaborador", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNombreJefe", SqlDbType.VarChar, 150);
                cmd.Parameters.Add("@chrEstado", SqlDbType.Char, 1);
                cmd.Parameters.Add("@chrTipoDialogo", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitUsuarioInactivo", SqlDbType.Bit);
                cmd.Parameters.Add("@vchModeloDialogo", SqlDbType.VarChar, 20);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrNombreColaborador"].Value = nombreColaborador;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNombreJefe"].Value = nombreJefe;
                cmd.Parameters["@chrEstado"].Value = estado;
                cmd.Parameters["@chrTipoDialogo"].Value = tipo;
                cmd.Parameters["@bitUsuarioInactivo"].Value = usuIna;
                cmd.Parameters["@vchModeloDialogo"].Value = modeloDialogo;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var entidad = new BeSeguimientoStatusDetalle
                    {
                        Pais =
                            dr.IsDBNull(dr.GetOrdinal("PAIS")) ? default(string) : dr.GetString(dr.GetOrdinal("PAIS")),
                        Periodo =
                            dr.IsDBNull(dr.GetOrdinal("PERIODO"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("PERIODO")),
                        Usuario =
                            dr.IsDBNull(dr.GetOrdinal("USUARIO"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("USUARIO")),
                        Rol = dr.IsDBNull(dr.GetOrdinal("ROL")) ? default(string) : dr.GetString(dr.GetOrdinal("ROL")),
                        NombreEvaluado =
                            dr.IsDBNull(dr.GetOrdinal("NOMBRE_EVALUADO"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("NOMBRE_EVALUADO")),
                        Evaluador =
                            dr.IsDBNull(dr.GetOrdinal("EVALUADOR"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("EVALUADOR")),
                        RolEvaluador =
                            dr.IsDBNull(dr.GetOrdinal("ROL_EVALUADOR"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("ROL_EVALUADOR")),
                        NombreEvaluador =
                            dr.IsDBNull(dr.GetOrdinal("NOMBRE_EVALUADOR"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("NOMBRE_EVALUADOR")),
                        Estado =
                            dr.IsDBNull(dr.GetOrdinal("ESTADO"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("ESTADO")),
                        TipoDialogo =
                            dr.IsDBNull(dr.GetOrdinal("TIPO_DIALOGO"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("TIPO_DIALOGO")),
                        ModeloDialogo =
                            dr.IsDBNull(dr.GetOrdinal("MODELO_DIALOGO"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("MODELO_DIALOGO"))
                    };
                    entidades.Add(entidad);
                }
                dr.Close();
            }

            return entidades;
        }


        //public List<beSeguimientoStatusDetalle> ListarSeguimientoStatusDetalleUsuario(string periodo, string nombreColaborador, string nivel, string pais, string zona,
        //                                                                              string nombreJefe, string estado, string region, string codigoUsuario, string nivelEvaluador)
        //{
        //    List<beSeguimientoStatusDetalle> entidades = new List<beSeguimientoStatusDetalle>();
        //    using (SqlConnection cn = daConexion.ObtieneConexionJOB())
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand("ESE_SeguimientoStatusDetalleUsuario", cn) { CommandType = CommandType.StoredProcedure };

        //        cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
        //        cmd.Parameters.Add("@chrNombreColaborador", SqlDbType.NChar, 100);
        //        cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
        //        cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
        //        cmd.Parameters.Add("@chrZona", SqlDbType.NChar, 4);
        //        cmd.Parameters.Add("@chrNombreJefe", SqlDbType.NChar, 100);
        //        cmd.Parameters.Add("@chrEstado", SqlDbType.Char, 1);
        //        cmd.Parameters.Add("@chrRegion", SqlDbType.NChar, 4);
        //        cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
        //        cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

        //        cmd.Parameters["@chrPeriodo"].Value = periodo;
        //        cmd.Parameters["@chrNombreColaborador"].Value = nombreColaborador;
        //        cmd.Parameters["@chrNivel"].Value = nivel;
        //        cmd.Parameters["@chrPais"].Value = pais;
        //        cmd.Parameters["@chrZona"].Value = zona;
        //        cmd.Parameters["@chrNombreJefe"].Value = nombreJefe;
        //        cmd.Parameters["@chrEstado"].Value = estado;
        //        cmd.Parameters["@chrRegion"].Value = region;
        //        cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
        //        cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            beSeguimientoStatusDetalle entidad = new beSeguimientoStatusDetalle();
        //            entidad.Periodo = dr.IsDBNull(dr.GetOrdinal("Periodo")) ? default(string) : dr.GetString(dr.GetOrdinal("Periodo"));
        //            entidad.Pais = dr.IsDBNull(dr.GetOrdinal("Pais")) ? default(string) : dr.GetString(dr.GetOrdinal("Pais"));
        //            entidad.Colaborador = dr.IsDBNull(dr.GetOrdinal("Colaborador")) ? default(string) : dr.GetString(dr.GetOrdinal("Colaborador"));
        //            entidad.Nivel = dr.IsDBNull(dr.GetOrdinal("Nivel")) ? default(string) : dr.GetString(dr.GetOrdinal("Nivel"));
        //            entidad.Jefe = dr.IsDBNull(dr.GetOrdinal("Jefe")) ? default(string) : dr.GetString(dr.GetOrdinal("Jefe"));
        //            entidad.Estatus = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? default(string) : dr.GetString(dr.GetOrdinal("Estatus"));
        //            entidades.Add(entidad);
        //        }
        //        dr.Close();
        //    }

        //    return entidades;
        //}

        public List<string> ObtenerPeriodos(string prefijoPais, int rol)
        {
            var listaPeriodos = new List<string>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerTodosPeriodos", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoPais;

                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters["@codigoRolUsuario"].Value = rol;
                try
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        listaPeriodos.Add(reader.GetString(0).Trim());
                    }
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

            return listaPeriodos;
        }

        /// <summary>
        /// Retorna a todas las regiones
        /// </summary>
        /// <returns></returns>
        public List<BeComun> ListarRegiones()
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ListarRegiones", cn) { CommandType = CommandType.StoredProcedure };

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var entidad = new BeComun
                    {
                        Codigo =
                            dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("Codigo")),
                        Descripcion =
                            dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("Descripcion"))
                    };
                    entidades.Add(entidad);
                }

                dr.Close();
            }

            return entidades;
        }

        /// <summary>
        /// Retorna totos los tipos de variables
        /// </summary>
        /// <returns></returns>
        public List<BeComun> ListarTipoVariables()
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_TipoVariable", cn) { CommandType = CommandType.StoredProcedure };

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var entidad = new BeComun
                    {
                        Codigo =
                            dr.IsDBNull(dr.GetOrdinal("chrCodVariable"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("chrCodVariable")),
                        Descripcion =
                            dr.IsDBNull(dr.GetOrdinal("vchrDesVariable"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("vchrDesVariable"))
                    };
                    entidades.Add(entidad);
                }
                dr.Close();
            }

            return entidades;
        }

        /// <summary>
        /// retorna las zonas a apartir de código de la región
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public List<BeComun> ListarZonas(string region)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ListarZonas", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodRegion", SqlDbType.NChar, 4);
                cmd.Parameters["@chrCodRegion"].Value = region;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var entidad = new BeComun
                    {
                        Codigo =
                            dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("Codigo")),
                        Descripcion =
                            dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                ? default(string)
                                : dr.GetString(dr.GetOrdinal("Descripcion"))
                    };
                    entidades.Add(entidad);
                }
                dr.Close();
            }

            return entidades;
        }

        public List<BeAnalisisStatusRanking> ListarAnalisisStatusRanking(string pais, string nivel, string periodo,
                                                                         string campanha, string estado1, string estado2,
                                                                         string estado3)
        {
            var entidades = new List<BeAnalisisStatusRanking>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ResumenAnalisisStatusRanking", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@vchEstado1", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado2", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado3", SqlDbType.VarChar, 30);

                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrCampana"].Value = campanha;
                cmd.Parameters["@vchEstado1"].Value = estado1;
                cmd.Parameters["@vchEstado2"].Value = estado2;
                cmd.Parameters["@vchEstado3"].Value = estado3;

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entidad = new BeAnalisisStatusRanking
                    {
                        Periodo = dr.GetString(dr.GetOrdinal("Periodo")),
                        Pais = dr.GetString(dr.GetOrdinal("Pais")),
                        Colaboradores = dr.GetInt32(dr.GetOrdinal("Colaboradores")),
                        Estado1 = dr.GetInt32(dr.GetOrdinal("Estado1")),
                        Estado2 = dr.GetInt32(dr.GetOrdinal("Estado2")),
                        Estado3 = dr.GetInt32(dr.GetOrdinal("Estado3"))
                    };


                    if (entidad.Colaboradores != 0)
                    {
                        entidad.PorcentajeEstado1 = Math.Round(Convert.ToDecimal(entidad.Estado1) / entidad.Colaboradores, 2);
                        entidad.PorcentajeEstado2 = Math.Round(Convert.ToDecimal(entidad.Estado2) / entidad.Colaboradores, 2);
                        entidad.PorcentajeEstado3 = Math.Round(Convert.ToDecimal(entidad.Estado3) / entidad.Colaboradores, 2);
                    }

                    entidades.Add(entidad);
                }

                dr.Close();
            }

            return entidades;
        }

        public List<BeAnalisisStatusRanking> ListarAnalisisStatusRankingUsuario(string pais, string nivel, string periodo, string campanha, string estado1,
                                                                                string estado2, string estado3, string codigoUsuario, string nivelEvaluador)
        {
            var entidades = new List<BeAnalisisStatusRanking>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ResumenAnalisisStatusRankingUsuario", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@vchEstado1", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado2", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado3", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrCampana"].Value = campanha;
                cmd.Parameters["@vchEstado1"].Value = estado1;
                cmd.Parameters["@vchEstado2"].Value = estado2;
                cmd.Parameters["@vchEstado3"].Value = estado3;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entidad = new BeAnalisisStatusRanking
                    {
                        Periodo = dr.GetString(dr.GetOrdinal("Periodo")),
                        Pais = dr.GetString(dr.GetOrdinal("Pais")),
                        Colaboradores = dr.GetInt32(dr.GetOrdinal("Colaboradores")),
                        Estado1 = dr.GetInt32(dr.GetOrdinal("Estado1")),
                        Estado2 = dr.GetInt32(dr.GetOrdinal("Estado2")),
                        Estado3 = dr.GetInt32(dr.GetOrdinal("Estado3"))
                    };

                    if (entidad.Colaboradores != 0)
                    {
                        entidad.PorcentajeEstado1 = Math.Round(Convert.ToDecimal(entidad.Estado1) / entidad.Colaboradores, 2);
                        entidad.PorcentajeEstado2 = Math.Round(Convert.ToDecimal(entidad.Estado2) / entidad.Colaboradores, 2);
                        entidad.PorcentajeEstado3 = Math.Round(Convert.ToDecimal(entidad.Estado3) / entidad.Colaboradores, 2);
                    }

                    entidades.Add(entidad);
                }

                dr.Close();
            }

            return entidades;
        }

        public List<BeChartCampanha> ListarChartCampanha(string pais, string nivel, string periodo, string campanha, string estado1, string estado2, string estado3)
        {
            var entidades = new List<BeChartCampanha>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_CuadroCampanhas", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@vchEstado1", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado2", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado3", SqlDbType.VarChar, 30);

                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrCampana"].Value = campanha;
                cmd.Parameters["@vchEstado1"].Value = estado1;
                cmd.Parameters["@vchEstado2"].Value = estado2;
                cmd.Parameters["@vchEstado3"].Value = estado3;

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entidad = new BeChartCampanha
                    {
                        Campanha = dr.GetString(dr.GetOrdinal("Campanha")),
                        Estado1 = dr.GetInt32(dr.GetOrdinal("Estado1")),
                        Estado2 = dr.GetInt32(dr.GetOrdinal("Estado2")),
                        Estado3 = dr.GetInt32(dr.GetOrdinal("Estado3"))
                    };
                    entidades.Add(entidad);
                }

                dr.Close();
            }

            return entidades;
        }

        public List<BeChartCampanha> ListarChartCampanhaUsuario(string pais, string nivel, string periodo, string campanha, string estado1, string estado2, string estado3,
                                                                string codigoUsuario, string nivelEvaluador)
        {
            var entidades = new List<BeChartCampanha>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_CuadroCampanhasUsuario", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@vchEstado1", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado2", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchEstado3", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrCampana"].Value = campanha;
                cmd.Parameters["@vchEstado1"].Value = estado1;
                cmd.Parameters["@vchEstado2"].Value = estado2;
                cmd.Parameters["@vchEstado3"].Value = estado3;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entidad = new BeChartCampanha
                    {
                        Campanha = dr.GetString(dr.GetOrdinal("Campanha")),
                        Estado1 = dr.GetInt32(dr.GetOrdinal("Estado1")),
                        Estado2 = dr.GetInt32(dr.GetOrdinal("Estado2")),
                        Estado3 = dr.GetInt32(dr.GetOrdinal("Estado3"))
                    };
                    entidades.Add(entidad);
                }

                dr.Close();
            }

            return entidades;
        }

        public DataTable ObtenerListaCampana(string ddlCampana, int codigoRolUsuario, string prefijoIsoPais, string periodoActual, string connstring)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerListaCampana", conn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@codigoRolUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters[0].Value = codigoRolUsuario;
                cmd.Parameters[1].Value = prefijoIsoPais;
                cmd.Parameters[2].Value = periodoActual;

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
            return ds.Tables.Count == 0 ? null : ds.Tables[0];
        }

        public DataTable ObtenerVariablesNegocio(string connstring, string periodo, string nombreColaborador, string nivel,
                                                 string zona, string pais, string nombreJefe, string estado,
                                                 string region)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VariablesNegocio", conn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@vchNombreColaborador", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchNombreJefe", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchEstado", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@chrRegion", SqlDbType.Char, 10);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@vchNombreColaborador"].Value = nombreColaborador;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrZona"].Value = zona;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@vchNombreJefe"].Value = nombreJefe;
                cmd.Parameters["@vchEstado"].Value = estado;
                cmd.Parameters["@chrRegion"].Value = region;

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

        public DataTable ObtenerVariablesNegocioUsuario(string connstring, string periodo, string nombreColaborador,
                                                        string nivel, string zona, string pais, string nombreJefe,
                                                        string estado, string region, string codigoUsuario,
                                                        string nivelEvaluador)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VariablesNegocioUsuario", conn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@vchNombreColaborador", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchNombreJefe", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchEstado", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@chrRegion", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@vchNombreColaborador"].Value = nombreColaborador;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrZona"].Value = zona;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@vchNombreJefe"].Value = nombreJefe;
                cmd.Parameters["@vchEstado"].Value = estado;
                cmd.Parameters["@chrRegion"].Value = region;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

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

        public DataTable ObtenerPlanNegocio(string connstring, string nivel, string estado, string variable, string rangoInicio, string rangoFin, string pais, string anhio,
                                            string periodo, string perioAnteior)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_PlanNegocio", conn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEstado", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@vchVariable", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@rangoInicio", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@rangoFin", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrPeriodoActual", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPeriodoAnt", SqlDbType.Char, 8);

                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@vchEstado"].Value = estado;
                cmd.Parameters["@vchVariable"].Value = variable;
                cmd.Parameters["@rangoInicio"].Value = rangoInicio;
                cmd.Parameters["@rangoFin"].Value = rangoFin;
                cmd.Parameters["@chrAnho"].Value = anhio;
                cmd.Parameters["@chrPeriodoActual"].Value = periodo;
                cmd.Parameters["@chrPeriodoAnt"].Value = perioAnteior;

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

        public DataTable ObtenerPlanNegocioDetalle(string connstring, string periodo, string pais, string region,
                                                   string zona, string nombreColaborador, string nombreJefe,
                                                   string nivel, string ranking, string cumplimiento, string enfoque)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_PlanNegocioDetalle", conn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrRegion", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@vchNombreColaborador", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchNombreJefe", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEstado", SqlDbType.VarChar, 30);
                cmd.Parameters.Add("@decCumplimiento", SqlDbType.Decimal);
                cmd.Parameters.Add("@chrCodigoVariable", SqlDbType.VarChar, 10);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrRegion"].Value = region;
                cmd.Parameters["@chrZona"].Value = zona;
                cmd.Parameters["@vchNombreColaborador"].Value = nombreColaborador;
                cmd.Parameters["@vchNombreJefe"].Value = nombreJefe;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@vchEstado"].Value = ranking;
                cmd.Parameters["@decCumplimiento"].Value = Convert.ToDecimal(cumplimiento);
                cmd.Parameters["@chrCodigoVariable"].Value = enfoque;

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

        public DataTable ResultadoDialogo(string connstring, string anhio, string periodo, string peridoAnterior, string nivel, string pais, string tipo)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand();

                switch (tipo)
                {
                    case "dialogo":

                        cmd = new SqlCommand("ESE_ResultadoDialogo", conn) { CommandType = CommandType.StoredProcedure };
                        cmd.Parameters.Add("@chrPeriodoAnt", SqlDbType.Char, 10);
                        cmd.Parameters["@chrPeriodoAnt"].Value = peridoAnterior;

                        break;

                    case "dialogoPaises":

                        cmd = new SqlCommand("ESE_ResultadoDialogoPaises", conn) { CommandType = CommandType.StoredProcedure };

                        break;
                }

                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrPeriodoActual", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);

                cmd.Parameters["@chrAnho"].Value = anhio;
                cmd.Parameters["@chrPeriodoActual"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;

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

        public DataTable ResultadoDialogoUsuario(string connstring, string anhio, string periodo, string peridoAnterior, string nivel, string pais, string tipo, string codigoUsuario, string nivelEvaluador)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand();

                switch (tipo)
                {
                    case "dialogo":

                        cmd = new SqlCommand("ESE_ResultadoDialogoUsuario", conn) { CommandType = CommandType.StoredProcedure };
                        cmd.Parameters.Add("@chrPeriodoAnt", SqlDbType.Char, 8);
                        cmd.Parameters["@chrPeriodoAnt"].Value = peridoAnterior;
                        break;

                    case "dialogoPaises":
                        cmd = new SqlCommand("ESE_ResultadoDialogoPaisesUsuario", conn) { CommandType = CommandType.StoredProcedure };
                        break;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrPeriodoActual", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

                cmd.Parameters["@chrAnho"].Value = anhio;
                cmd.Parameters["@chrPeriodoActual"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

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

        public DataTable ResultadoDialogoDetalle(string connstring, string periodo, string pais, string region, string zona, string nivel, string codigoVariable, string tamBrecha)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ResultadoDialogoDetalle", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrRegion", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrZona", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoVariable", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchTamBrecha", SqlDbType.VarChar, 10);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrRegion"].Value = region;
                cmd.Parameters["@chrZona"].Value = zona;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrCodigoVariable"].Value = codigoVariable;
                cmd.Parameters["@vchTamBrecha"].Value = tamBrecha;

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

        public DataTable ResultadoDialogoDetalleUsuario(string connstring, string periodo, string pais, string region, string zona, string nivel, string codigoVariable, string tamBrecha, string codigoUsuario, string nivelEvaluador)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ResultadoDialogoDetalleUsuario", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrRegion", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrZona", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrNivel", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoVariable", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@vchTamBrecha", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrNivelEvaluador", SqlDbType.Char, 2);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPais"].Value = pais;
                cmd.Parameters["@chrRegion"].Value = region;
                cmd.Parameters["@chrZona"].Value = zona;
                cmd.Parameters["@chrNivel"].Value = nivel;
                cmd.Parameters["@chrCodigoVariable"].Value = codigoVariable;
                cmd.Parameters["@vchTamBrecha"].Value = tamBrecha;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrNivelEvaluador"].Value = nivelEvaluador;

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


        public DataTable ObtenerCompetencia(string codigoColaborador, string anio, string prefijoIsoPais, int idRol)
        {
            var ds = new DataSet();

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("[ESE_Obtener_Competencia]", conn)
                {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrprefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);

                cmd.Parameters["@chrCodigoColaborador"].Value = codigoColaborador;
                cmd.Parameters["@chrAnio"].Value = anio;
                cmd.Parameters["@chrprefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@intIdRol"].Value = idRol;

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