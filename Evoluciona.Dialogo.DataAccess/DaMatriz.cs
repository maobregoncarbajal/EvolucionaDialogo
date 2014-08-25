
using System.Globalization;

namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaMatriz : DaConexion
    {
        /// <summary>
        /// este procedimiento almacenado obtiene la región actual por gernete de region
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código de usuario</param>
        /// <returns>región de gernete de region </returns>
        public BeComun ObtenerRegionUsuario(string codPais, string codigoUsuario)
        {
            var entidad = new BeComun();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerRegionUsuario", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodUsuario"].Value = codigoUsuario;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad.Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo")) ? default(string) : dr.GetString(dr.GetOrdinal("Codigo"));
                            entidad.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? default(string) : dr.GetString(dr.GetOrdinal("Descripcion"));
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

            return entidad;
        }


        /// <summary>
        /// este procedimiento almacenado obtiene la región actual por gernete de region
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código de usuario</param>
        /// <param name="periodo">periodo</param>
        /// <returns>región de gernete de region </returns>
        public BeComun ObtenerRegionUsuarioporPeriodo(string codPais, string codigoUsuario, string periodo)
        {
            var entidad = new BeComun();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerRegionUsuarioporPeriodo", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodUsuario"].Value = codigoUsuario;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad.Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo")) ? default(string) : dr.GetString(dr.GetOrdinal("Codigo"));
                            entidad.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? default(string) : dr.GetString(dr.GetOrdinal("Descripcion"));
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

            return entidad;
        }


        /// <summary>
        /// Este procedimiento almacenado devuelve los años
        /// </summary>
        /// <param name="codPais"> código del país</param>
        /// <returns>lsita de años</returns>
        public List<BeComun> ListarAnhos(string codPais)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListarAnhos", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("Anhos"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Anhos")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("Anhos"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Anhos"))
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

        /// <summary>
        /// este método devuelve una lista de periodos según el país y el año
        /// </summary>
        /// <param name="codPais">código país</param>
        /// <param name="anho">año</param>
        /// <param name="idRol">Id Rol</param>
        /// <returns>lista de periodos</returns>
        public List<BeComun> ListarPeriodos(string codPais, string anho, int idRol)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListarPeriodos", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters["@chrAnho"].Value = anho;

                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);
                cmd.Parameters["@intIdRol"].Value = idRol;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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

        /// <summary>
        /// Este método devuelve los genretes de región de un director de ventas
        /// </summary>
        /// <param name="codUsuario"> código de Usuario</param>
        /// <returns> lista de gerentes de regiones</returns>
        public List<BeComun> ListarGerentesRegion(string codUsuario)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetAllGerentesRegion", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuario"].Value = codUsuario;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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
                                        : dr.GetString(dr.GetOrdinal("Descripcion")),
                                Referencia =
                                    dr.IsDBNull(dr.GetOrdinal("CodigoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CodigoPais"))
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

        /// <summary>
        /// este método les devuelve las variables por país
        /// </summary>
        /// <param name="codPais"> código del país </param>
        /// <returns>lista de variables</returns>
        public List<BeComun> ListarVariablesPais(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_VariablePais", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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
                                        : dr.GetString(dr.GetOrdinal("Descripcion")),
                                Referencia =
                                    dr.IsDBNull(dr.GetOrdinal("Referencia"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Referencia"))
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

        /// <summary>
        /// este método devuelve los genretes de zona de un detrerminado gerente de zona
        /// </summary>
        /// <param name="codUsuario">codigo de usuario</param>
        /// <param name="codPais">código del país</param>
        /// <param name="nombre">nombre de la gerenta de zona</param>
        /// <returns>lista de gerentes de zona</returns>
        public List<BeComun> ListarGerentesZona(string codUsuario, string codPais, string nombre)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetAllGerenteZona", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchNombre", SqlDbType.VarChar, 100);

                cmd.Parameters["@chrCodigoUsuario"].Value = codUsuario;
                cmd.Parameters["@chrCodPais"].Value = codPais;
                cmd.Parameters["@vchNombre"].Value = nombre;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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
                                        : dr.GetString(dr.GetOrdinal("Descripcion")),
                                Referencia =
                                    dr.IsDBNull(dr.GetOrdinal("CodPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CodPais"))
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

        public List<BeNivelesCompetencia> ListaNivelesCompetencia(string codPais, string anho)
        {
            var entidades = new List<BeNivelesCompetencia>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_NivelesCompetencia", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters["@chrAnho"].Value = anho;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeNivelesCompetencia
                            {
                                CodCompetencia =
                                    dr.IsDBNull(dr.GetOrdinal("CodCompetencia"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CodCompetencia")),
                                MaxValue =
                                    dr.IsDBNull(dr.GetOrdinal("MaxValue"))
                                        ? 0
                                        : dr.GetDecimal(dr.GetOrdinal("MaxValue")),
                                MinValue =
                                    dr.IsDBNull(dr.GetOrdinal("MinValue"))
                                        ? 0
                                        : dr.GetDecimal(dr.GetOrdinal("MinValue"))
                            };
                            entidades.Add(entidad);
                        }

                        dr.Close();
                    }

                    return entidades;
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
        }

        /// <summary>
        /// Este método lista Detalle de información de los Gerente de Region
        /// </summary>
        /// <param name="chrCodPais">código del País</param>
        /// <param name="codigoUsuario"> código del evaulador</param>
        /// <param name="rol"> rol del evaluado</param>
        /// <param name="periodo">perido</param>
        /// <param name="subPeriodo">micro Periodo</param>
        /// <returns>detalle de Información</returns>
        public List<BeDetalleInformacion> ListaDetalleInformacionGr(string chrCodPais, string codigoUsuario, int rol, string periodo, string subPeriodo)
        {
            var entidades = new List<BeDetalleInformacion>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_DetalleInformacionGR", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrSubPeriodo", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodPais"].Value = chrCodPais;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@intRol"].Value = rol;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrSubPeriodo"].Value = subPeriodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeDetalleInformacion
                            {
                                PrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("PrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("PrefijoIsoPais")),
                                DocIdentidad =
                                    dr.IsDBNull(dr.GetOrdinal("DocIdentidad"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("DocIdentidad")),
                                NombreRegion =
                                    dr.IsDBNull(dr.GetOrdinal("NombreRegion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("NombreRegion")),
                                Nombre =
                                    dr.IsDBNull(dr.GetOrdinal("Nombre"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Nombre")),
                                Competencia =
                                    dr.IsDBNull(dr.GetOrdinal("Competencia"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Competencia")),
                                PuntoRanking =
                                    dr.IsDBNull(dr.GetOrdinal("PuntoRanking"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("PuntoRanking"))),
                                Ranking =
                                    dr.IsDBNull(dr.GetOrdinal("Ranking"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Ranking")),
                                VentaPeriodo =
                                    dr.IsDBNull(dr.GetOrdinal("VentaPeriodo"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("VentaPeriodo"))),
                                VentaPlanPeriodo =
                                    dr.IsDBNull(dr.GetOrdinal("VentaPlanPeriodo"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("VentaPlanPeriodo"))),
                                PorcentajeAvance =
                                    dr.IsDBNull(dr.GetOrdinal("PorcentajeAvance"))
                                        ? 0
                                        : dr.GetDecimal(dr.GetOrdinal("PorcentajeAvance")),
                                Tipo =
                                    dr.IsDBNull(dr.GetOrdinal("Tipo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Tipo"))
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

        /// <summary>
        /// Este método lista Detalle de información de los Gerente de Zona
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código usuario</param>
        /// <param name="rolEvaluador">rol del evaluador</param>
        /// <param name="rolEvaluado">rol del evaluado</param>
        /// <param name="periodo">periodo</param>
        /// <param name="subPeriodo">microperiodo</param>
        /// <param name="codRegion">código de región</param>
        /// <param name="codZona">código de zona</param>
        /// <returns>detalle de información</returns>
        public List<BeDetalleInformacion> ListaDetalleInformacionGz(string codPais, string codigoUsuario, int rolEvaluador, int rolEvaluado, string periodo, string subPeriodo, string codRegion, string codZona)
        {
            var entidades = new List<BeDetalleInformacion>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_DetalleInformacionGZ", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intRolEvaluador", SqlDbType.Int);
                cmd.Parameters.Add("@intRolEvaluado", SqlDbType.Int);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrSubPeriodo", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 10);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = codPais;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@intRolEvaluador"].Value = rolEvaluador;
                cmd.Parameters["@intRolEvaluado"].Value = rolEvaluado;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrSubPeriodo"].Value = subPeriodo;
                cmd.Parameters["@chrCodRegion"].Value = codRegion;
                cmd.Parameters["@chrCodZona"].Value = codZona;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeDetalleInformacion
                            {
                                PrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("PrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("PrefijoIsoPais")),
                                DocIdentidad =
                                    dr.IsDBNull(dr.GetOrdinal("DocIdentidad"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("DocIdentidad")),
                                NombreRegion =
                                    dr.IsDBNull(dr.GetOrdinal("NombreRegion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("NombreRegion")),
                                NombreZona =
                                    dr.IsDBNull(dr.GetOrdinal("NombreZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("NombreZona")),
                                Nombre =
                                    dr.IsDBNull(dr.GetOrdinal("Nombre"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Nombre")),
                                Competencia =
                                    dr.IsDBNull(dr.GetOrdinal("Competencia"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Competencia")),
                                PuntoRanking =
                                    dr.IsDBNull(dr.GetOrdinal("PuntoRanking"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("PuntoRanking"))),
                                Ranking =
                                    dr.IsDBNull(dr.GetOrdinal("Ranking"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Ranking")),
                                VentaPeriodo =
                                    dr.IsDBNull(dr.GetOrdinal("VentaPeriodo"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("VentaPeriodo"))),
                                VentaPlanPeriodo =
                                    dr.IsDBNull(dr.GetOrdinal("VentaPlanPeriodo"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("VentaPlanPeriodo"))),
                                PorcentajeAvance =
                                    dr.IsDBNull(dr.GetOrdinal("PorcentajeAvance"))
                                        ? 0
                                        : dr.GetDecimal(dr.GetOrdinal("PorcentajeAvance")),
                                Tipo =
                                    dr.IsDBNull(dr.GetOrdinal("Tipo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Tipo"))
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

        /// <summary>
        /// Este store procedure lista todas las regiones activas según el país
        /// </summary>
        /// <param name="codPais">Código del país</param>
        /// <returns>lista de regiones</returns>
        public List<BeComun> ListarRegiones(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListarRegiones", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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



        public List<BeComun> ListarRegionesMz(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListarRegionesMz", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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



        public List<BeComun> ListarRegionGRporPeriodo(string codPais, string codUsuario, string periodo)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("[ESE_Matriz_ListarRegionesGRporPeriodo]", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodPais"].Value = codPais;
                cmd.Parameters["@chrCodigoUsuario"].Value = codUsuario;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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



        /// <summary>
        /// Este store procedure lista todas las zonas activas según el país y la región
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <returns>lista zonas</returns>
        public List<BeComun> ListarZonas(string codPais, string codRegion)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListarZonas", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                cmd.Parameters["@chrCodRegion"].Value = codRegion;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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

        /// <summary>
        /// este método devuelve la ficha personal
        /// </summary>
        /// <param name="codigoPais"> código del país</param>
        /// <param name="codigoUsuario">código del usuario</param>
        /// <returns></returns>
        public BeFichaPersonal ObtenerFichaPersonal(string codigoPais, string codigoUsuario)
        {
            var entidad = new BeFichaPersonal();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerFichaPersonal", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 4);
                cmd.Parameters["@chrCodPais"].Value = codigoPais;

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad.CodigoPlanilla = dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla"));
                            entidad.CodigoPaisADAM = dr.IsDBNull(dr.GetOrdinal("chrCodigoPaisADAM")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodigoPaisADAM"));
                            entidad.DescripcionPais = dr.IsDBNull(dr.GetOrdinal("vchDescripcionPais")) ? default(string) : dr.GetString(dr.GetOrdinal("vchDescripcionPais"));
                            entidad.Cub = dr.IsDBNull(dr.GetOrdinal("vchCUB")) ? default(string) : dr.GetString(dr.GetOrdinal("vchCUB"));
                            entidad.EstadoCarga = dr.IsDBNull(dr.GetOrdinal("chrEstadoCarga")) ? default(string) : dr.GetString(dr.GetOrdinal("chrEstadoCarga"));
                            entidad.NombresApellidos = dr.IsDBNull(dr.GetOrdinal("vchNombresApellidos")) ? default(string) : dr.GetString(dr.GetOrdinal("vchNombresApellidos"));
                            entidad.FechaNacimiento = dr.IsDBNull(dr.GetOrdinal("chrFechaNacimiento")) ? default(string) : dr.GetString(dr.GetOrdinal("chrFechaNacimiento"));
                            entidad.EstadoCivil = dr.IsDBNull(dr.GetOrdinal("chrEstadoCivil")) ? default(string) : dr.GetString(dr.GetOrdinal("chrEstadoCivil"));
                            entidad.CantidadHijos = dr.IsDBNull(dr.GetOrdinal("intCantidadHijos")) ? 0 : dr.GetInt32(dr.GetOrdinal("intCantidadHijos"));
                            entidad.Domicilio = dr.IsDBNull(dr.GetOrdinal("vchDomicilio")) ? default(string) : dr.GetString(dr.GetOrdinal("vchDomicilio"));
                            entidad.TelefonoFijo = dr.IsDBNull(dr.GetOrdinal("chrTelefonoFijo")) ? default(string) : dr.GetString(dr.GetOrdinal("chrTelefonoFijo"));
                            entidad.Formacion = dr.IsDBNull(dr.GetOrdinal("vchFormacion")) ? default(string) : dr.GetString(dr.GetOrdinal("vchFormacion"));
                            entidad.ExperienciaProfesional = dr.IsDBNull(dr.GetOrdinal("vchExperienciaProfesional")) ? default(string) : dr.GetString(dr.GetOrdinal("vchExperienciaProfesional"));
                            entidad.FechaIngreso = dr.IsDBNull(dr.GetOrdinal("chrFechaIngreso")) ? default(string) : dr.GetString(dr.GetOrdinal("chrFechaIngreso"));
                            entidad.PuestoActual = dr.IsDBNull(dr.GetOrdinal("vchPuestoActual")) ? default(string) : dr.GetString(dr.GetOrdinal("vchPuestoActual"));
                            entidad.CodigoRegionoZona = dr.IsDBNull(dr.GetOrdinal("chrCodigoRegionoZona")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodigoRegionoZona"));
                            entidad.DescripcionRegionoZona = dr.IsDBNull(dr.GetOrdinal("vchDescripcionRegionoZona")) ? default(string) : dr.GetString(dr.GetOrdinal("vchDescripcionRegionoZona"));
                            entidad.CodigoGerenteRegionoZona = dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteRegionoZona")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodigoGerenteRegionoZona"));
                            entidad.FechaAsignacionActual = dr.IsDBNull(dr.GetOrdinal("chrFechaAsignacionActual")) ? default(string) : dr.GetString(dr.GetOrdinal("chrFechaAsignacionActual"));

                            entidad.CorreoElectronico = dr.IsDBNull(dr.GetOrdinal("vchCorreoElectronico")) ? default(string) : dr.GetString(dr.GetOrdinal("vchCorreoElectronico"));
                            entidad.NombreJefe = dr.IsDBNull(dr.GetOrdinal("vchNombreJefe")) ? default(string) : dr.GetString(dr.GetOrdinal("vchNombreJefe"));
                            entidad.CantidadPersonas = dr.IsDBNull(dr.GetOrdinal("intCantidadPersonas")) ? 0 : dr.GetInt32(dr.GetOrdinal("intCantidadPersonas"));

                            entidad.PersonaCargo = new BePersonaCargo
                            {
                                PersonasACargo =
                                    dr.IsDBNull(dr.GetOrdinal("Personas"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Personas"))
                            };

                            entidad.PuestosOcupados = new BePuestosOcupados
                            {
                                PosicionAnterior =
                                    dr.IsDBNull(dr.GetOrdinal("Puestos"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Puestos"))
                            };

                            entidad.CentroEstudios = new BeCentroEstudios
                            {
                                CentroDeEstudios =
                                    dr.IsDBNull(dr.GetOrdinal("CentroEstudios"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CentroEstudios"))
                            };
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

            return entidad;
        }

        /// <summary>
        /// este método devuelve los resultados de los gerentes
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="anho">año</param>
        /// <param name="codigoUsuario">código del usuario</param>
        /// <param name="codVariable">código de variable</param>
        /// <param name="periodo">periodo</param>
        /// <param name="codRol">código Rol</param>
        /// <returns>lista de resultados</returns>
        public List<BeResultadoMatriz> ListarResultados(string codPais, string anho, string codigoUsuario, string codVariable, string periodo, string codRol)
        {
            var entidades = new List<BeResultadoMatriz>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand();

                if (codRol == "5")

                    cmd = new SqlCommand("ESE_Matriz_VerResultadoGR", cn) { CommandType = CommandType.StoredProcedure };

                if (codRol == "6")

                    cmd = new SqlCommand("ESE_Matriz_VerResultadoGZ", cn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrVariable", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodPais"].Value = codPais;
                cmd.Parameters["@chrAnho"].Value = anho;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrVariable"].Value = codVariable;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeResultadoMatriz
                            {
                                Anho =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnho"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnho")),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                                CodRegionZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodRegion"))
                            };

                            if (codRol == "6")
                                entidad.CodRegionZona = dr.IsDBNull(dr.GetOrdinal("chrCodZona")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodZona"));

                            entidad.DocIdentidad = dr.IsDBNull(dr.GetOrdinal("chrDocIdentidad")) ? default(string) : dr.GetString(dr.GetOrdinal("chrDocIdentidad"));
                            entidad.AnioCampana = dr.IsDBNull(dr.GetOrdinal("chrAnioCampana")) ? default(string) : dr.GetString(dr.GetOrdinal("chrAnioCampana"));
                            entidad.EstadoCampana = dr.IsDBNull(dr.GetOrdinal("vchEstadoCampana")) ? default(string) : dr.GetString(dr.GetOrdinal("vchEstadoCampana"));
                            entidad.EstadoPeriodo = dr.IsDBNull(dr.GetOrdinal("vchEstadoPeriodo")) ? default(string) : dr.GetString(dr.GetOrdinal("vchEstadoPeriodo"));
                            entidad.ValorPlanCampana = dr.IsDBNull(dr.GetOrdinal("decValorPlanCampana")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorPlanCampana")));
                            entidad.ValorRealCampana = dr.IsDBNull(dr.GetOrdinal("decValorRealCampana")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorRealCampana")));
                            entidad.ValorPlanPeriodo = dr.IsDBNull(dr.GetOrdinal("decValorPlanPeriodo")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorPlanPeriodo")));
                            entidad.ValorRealPeriodo = dr.IsDBNull(dr.GetOrdinal("decValorRealPeriodo")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorRealPeriodo")));
                            entidad.ValorRanking = dr.IsDBNull(dr.GetOrdinal("decRanking")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decRanking")));
                            entidad.Tipo = dr.IsDBNull(dr.GetOrdinal("Tipo")) ? default(string) : dr.GetString(dr.GetOrdinal("Tipo"));
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

        /// <summary>
        /// este método lista todas las ventas de los gerentes
        /// </summary>
        /// <param name="codPais">código país</param>
        /// <param name="anho">año</param>
        /// <param name="codVariable">código de la variable vtaNet</param>
        /// <param name="periodo">periodo</param>
        /// <param name="codRegion">código Región</param>
        /// <param name="codRol">código Rol</param>
        /// <returns>ventas</returns>
        public List<BeResultadoMatriz> ListarVentas(string codPais, string anho, string codVariable, string periodo, string codRegion, string codRol)
        {
            var entidades = new List<BeResultadoMatriz>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand();

                if (codRol == "5")
                {
                    cmd = new SqlCommand("ESE_Matriz_VerResultadoVentaGR", cn) { CommandType = CommandType.StoredProcedure };
                }

                if (codRol == "6")
                {
                    cmd = new SqlCommand("ESE_Matriz_VerResultadoVentaGZ", cn) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5);
                    cmd.Parameters["@chrCodRegion"].Value = codRegion;
                }

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrVariable", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodPais"].Value = codPais;
                cmd.Parameters["@chrAnho"].Value = anho;
                cmd.Parameters["@chrVariable"].Value = codVariable;
                cmd.Parameters["@chrPeriodo"].Value = periodo;


                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeResultadoMatriz
                            {
                                Anho =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnho"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnho")),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                                AnioCampana =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                                ValorRealCampana =
                                    dr.IsDBNull(dr.GetOrdinal("decValorRealCampana"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorRealCampana"))),
                                ValorRealPeriodo =
                                    dr.IsDBNull(dr.GetOrdinal("decValorRealPeriodo"))
                                        ? 0
                                        : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorRealPeriodo"))),
                                Tipo =
                                    dr.IsDBNull(dr.GetOrdinal("Tipo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Tipo"))
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

        public List<BeTablaMapeo> ObtenerMatrizConsolidada()
        {
            var lista = new List<BeTablaMapeo>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerMatrizConsolidada", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeTablaMapeo
                            {
                                IntSEQIDRegion =
                                    dr.IsDBNull(dr.GetOrdinal("IntSEQIDRegion"))
                                        ? default(int)
                                        : dr.GetInt32(dr.GetOrdinal("IntSEQIDRegion")),
                                NUEVASCR =
                                    dr.IsDBNull(dr.GetOrdinal("NUEVASCR"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("NUEVASCR")),
                                CRND =
                                    dr.IsDBNull(dr.GetOrdinal("CRND"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CRND")),
                                CRED =
                                    dr.IsDBNull(dr.GetOrdinal("CRED"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CRED")),
                                CRDESA =
                                    dr.IsDBNull(dr.GetOrdinal("CRDESA"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CRDESA")),
                                CRDESTA =
                                    dr.IsDBNull(dr.GetOrdinal("CRDESTA"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CRDESTA")),
                                NUEVASES =
                                    dr.IsDBNull(dr.GetOrdinal("NUEVASES"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("NUEVASES")),
                                ESND =
                                    dr.IsDBNull(dr.GetOrdinal("ESND"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("ESND")),
                                ESED =
                                    dr.IsDBNull(dr.GetOrdinal("ESED"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("ESED")),
                                ESDESA =
                                    dr.IsDBNull(dr.GetOrdinal("ESDESA"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("ESDESA")),
                                ESDESTA =
                                    dr.IsDBNull(dr.GetOrdinal("ESDESTA"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("ESDESTA")),
                                NUEVASPR =
                                    dr.IsDBNull(dr.GetOrdinal("NUEVASPR"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("NUEVASPR")),
                                PRND =
                                    dr.IsDBNull(dr.GetOrdinal("PRND"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("PRND")),
                                PRED =
                                    dr.IsDBNull(dr.GetOrdinal("PRED"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("PRED")),
                                PRDESA =
                                    dr.IsDBNull(dr.GetOrdinal("PRDESA"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("PRDESA")),
                                PRDESTA =
                                    dr.IsDBNull(dr.GetOrdinal("PRDESTA"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("PRDESTA")),
                                DecValorRealPeriodo =
                                    dr.IsDBNull(dr.GetOrdinal("DecValorRealPeriodo"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("DecValorRealPeriodo"))
                            };

                            lista.Add(entidad);
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

            return lista;
        }

        /// <summary>
        /// este método obtiene el cuadrante por Gerente
        /// </summary>
        /// <param name="codPais">código País</param>
        /// <param name="codigoUsuario">código Usuario</param>
        /// <param name="idRol">id Rol</param>
        /// <param name="anho">año</param>
        /// <param name="periodo">periodo</param>
        /// <returns>cuadrante usuario</returns>
        public BeCuadranteUsuario ObtenerCuadranteUsuario(string codPais, string codigoUsuario, int idRol, string anho, string periodo)
        {
            var entidad = new BeCuadranteUsuario();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand();

                if (idRol == 2)//GR
                    cmd = new SqlCommand("ESE_Matriz_CuadranteUsuarioGR", cn) { CommandType = CommandType.StoredProcedure };

                if (idRol == 3)//GZ
                    cmd = new SqlCommand("ESE_Matriz_CuadranteUsuarioGZ", cn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

                cmd.Parameters.Add("@intRol", SqlDbType.Int);
                cmd.Parameters["@intRol"].Value = idRol;

                cmd.Parameters.Add("@chrAnho", SqlDbType.Char, 4);
                cmd.Parameters["@chrAnho"].Value = anho;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad.CodPais = dr.IsDBNull(dr.GetOrdinal("CodPais")) ? default(string) : dr.GetString(dr.GetOrdinal("CodPais"));
                            entidad.DocIdentidad = dr.IsDBNull(dr.GetOrdinal("docIdentidad")) ? default(string) : dr.GetString(dr.GetOrdinal("docIdentidad"));
                            entidad.CodRegion = dr.IsDBNull(dr.GetOrdinal("codRegion")) ? default(string) : dr.GetString(dr.GetOrdinal("codRegion"));
                            entidad.NombreRegion = dr.IsDBNull(dr.GetOrdinal("nombreRegion")) ? default(string) : dr.GetString(dr.GetOrdinal("nombreRegion"));

                            if (idRol == 3)//GZ
                            {
                                entidad.CodZona = dr.IsDBNull(dr.GetOrdinal("codZona")) ? default(string) : dr.GetString(dr.GetOrdinal("codZona"));
                                entidad.NombreZona = dr.IsDBNull(dr.GetOrdinal("nombreZona")) ? default(string) : dr.GetString(dr.GetOrdinal("nombreZona"));
                            }

                            entidad.EstadoPeriodo = dr.IsDBNull(dr.GetOrdinal("estadoPeriodo")) ? default(string) : dr.GetString(dr.GetOrdinal("estadoPeriodo"));
                            entidad.PorcentajeAvance = dr.IsDBNull(dr.GetOrdinal("porcentajeAvance")) ? 0 : dr.GetDecimal(dr.GetOrdinal("porcentajeAvance"));
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

            return entidad;
        }

        /// <summary>
        /// este método lista los gerentes de zona de una determinada zona
        /// </summary>
        /// <param name="codPais"> código país</param>
        /// <param name="codRegion">código región</param>
        /// <param name="codZona"> código zona</param>
        /// <param name="periodo"> periodo</param>
        /// <returns>gerentes de zona</returns>
        public List<BeComun> ListarGerenteZonaByZona(string codPais, string codRegion, string codZona, string periodo)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetAllGerenteZonaByZona", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5);
                cmd.Parameters["@chrCodRegion"].Value = codRegion;

                cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 10);
                cmd.Parameters["@chrCodZona"].Value = codZona;

                cmd.Parameters.Add("@chrPeriodoAcuerdo", SqlDbType.Char, 10);
                cmd.Parameters["@chrPeriodoAcuerdo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoGerenteZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodigoGerenteZona")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreCompleto"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreCompleto"))
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



        /// <summary>
        /// este método verifica si el periodo de la campaña dada está activo para un país
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="campanha">código de la campaña</param>
        /// <returns>estado </returns>
        public string ValidarPeriodoPorCampanha(string codPais, string campanha)
        {
            var estado = 0;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ValidarPeriodoPorCampanha", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCampanha", SqlDbType.Char, 20);
                cmd.Parameters["@chrCampanha"].Value = campanha;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            estado = dr.IsDBNull(dr.GetOrdinal("value")) ? 0 : dr.GetInt32(dr.GetOrdinal("value"));
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

            return estado.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// este método inserta una toma de acción
        /// </summary>
        /// <param name="tomaAccion">toma de acción</param>
        /// <returns>id toma de acción</returns>
        public int InsertarTomaAccion(BeTomaAccion tomaAccion)
        {
            int id;
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new SqlCommand("ESE_Matriz_InsertTomaAccion", cn) { CommandType = CommandType.StoredProcedure };

                        var parm = new SqlParameter("@intSEQIDTomaAccion", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(parm);

                        cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                        cmd.Parameters["@chrPrefijoIsoPais"].Value = tomaAccion.PrefijoIsoPaisEvaluado;

                        cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                        cmd.Parameters["@chrPeriodo"].Value = tomaAccion.Periodo;

                        cmd.Parameters.Add("@intSEQIDTomaAccionRef", SqlDbType.Int);
                        cmd.Parameters["@intSEQIDTomaAccionRef"].Value = tomaAccion.IdTomaAccionRef;

                        cmd.Parameters.Add("@intIdRolEvaluador", SqlDbType.Int);
                        cmd.Parameters["@intIdRolEvaluador"].Value = tomaAccion.IdRolEvaluador;

                        cmd.Parameters.Add("@intIdRolEvaluado", SqlDbType.Int);
                        cmd.Parameters["@intIdRolEvaluado"].Value = tomaAccion.IdRolEvaluado;

                        cmd.Parameters.Add("@chrCodEvaluado", SqlDbType.Char, 20);
                        cmd.Parameters["@chrCodEvaluado"].Value = tomaAccion.CodEvaluado;

                        cmd.Parameters.Add("@chrCodEvaluador", SqlDbType.Char, 20);
                        cmd.Parameters["@chrCodEvaluador"].Value = tomaAccion.CodEvaluador;

                        cmd.Parameters.Add("@chrCodRegionActual", SqlDbType.Char, 3);
                        cmd.Parameters["@chrCodRegionActual"].Value = tomaAccion.CodRegionActual;

                        cmd.Parameters.Add("@chrCodZonaActual", SqlDbType.Char, 6);
                        cmd.Parameters["@chrCodZonaActual"].Value = (string.IsNullOrEmpty(tomaAccion.CodZonaActual)) ? string.Empty : tomaAccion.CodZonaActual;

                        cmd.Parameters.Add("@chrTomaAccion", SqlDbType.Char, 2);
                        cmd.Parameters["@chrTomaAccion"].Value = tomaAccion.TomaAccion;

                        cmd.Parameters.Add("@chrCodZonaReasignacion", SqlDbType.Char, 6);
                        cmd.Parameters["@chrCodZonaReasignacion"].Value = (string.IsNullOrEmpty(tomaAccion.CodZonaReasignacion)) ? string.Empty : tomaAccion.CodZonaReasignacion;

                        cmd.Parameters.Add("@chrCodRegionReasignacion", SqlDbType.Char, 3);
                        cmd.Parameters["@chrCodRegionReasignacion"].Value = (string.IsNullOrEmpty(tomaAccion.CodRegionReasignacion)) ? string.Empty : tomaAccion.CodRegionReasignacion;

                        cmd.Parameters.Add("@chrAnioCampanaInicio", SqlDbType.Char, 6);
                        cmd.Parameters["@chrAnioCampanaInicio"].Value = (string.IsNullOrEmpty(tomaAccion.AnhoCampanhaInicio)) ? string.Empty : tomaAccion.AnhoCampanhaInicio;

                        cmd.Parameters.Add("@chrAnioCampanaFin", SqlDbType.Char, 6);
                        cmd.Parameters["@chrAnioCampanaFin"].Value = (string.IsNullOrEmpty(tomaAccion.AnhoCampanhaFin)) ? string.Empty : tomaAccion.AnhoCampanhaFin;

                        cmd.Parameters.Add("@chrAnioCampanaInicioCritico", SqlDbType.Char, 6);
                        cmd.Parameters["@chrAnioCampanaInicioCritico"].Value = (string.IsNullOrEmpty(tomaAccion.AnhoCampanhaInicioCritico)) ? string.Empty : tomaAccion.AnhoCampanhaInicioCritico;

                        cmd.Parameters.Add("@chrAnioCampanaFinCritico", SqlDbType.Char, 6);
                        cmd.Parameters["@chrAnioCampanaFinCritico"].Value = (string.IsNullOrEmpty(tomaAccion.AnhoCampanhaFinCritico)) ? string.Empty : tomaAccion.AnhoCampanhaFinCritico;

                        cmd.Parameters.Add("@vchObservaciones", SqlDbType.VarChar, 200);
                        cmd.Parameters["@vchObservaciones"].Value = tomaAccion.Observaciones;

                        cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);
                        cmd.Parameters["@intEstadoActivo"].Value = tomaAccion.EstadoActivo;

                        cmd.Parameters.Add("@chrUsuarioCrea", SqlDbType.Char, 20);
                        cmd.Parameters["@chrUsuarioCrea"].Value = tomaAccion.CodEvaluador;

                        cmd.Parameters.Add("@chrUsuarioModi", SqlDbType.Char, 20);
                        cmd.Parameters["@chrUsuarioModi"].Value = tomaAccion.CodEvaluador;

                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                        cmd.Transaction.Commit();
                        id = Convert.ToInt32(cmd.Parameters["@intSEQIDTomaAccion"].Value.ToString());

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
            }

            return id;
        }

        /// <summary>
        /// este método actualiza una calibración
        /// </summary>
        /// <param name="listaCalibracion">lista Calibraciones</param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <returns>id toma de acción</returns>
        public bool UpdateCalibracion(List<BeTomaAccion> listaCalibracion, string usuario)
        {
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var tran = cn.BeginTransaction();

                try
                {
                    foreach (var tomaAccion in listaCalibracion)
                    {
                        var cmd = new SqlCommand("ESE_Matriz_UpdateTomaAccion", cn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = tran
                        };

                        cmd.Parameters.Add("@intSEQIDTomaAccion", SqlDbType.Int, 4);
                        cmd.Parameters["@intSEQIDTomaAccion"].Value = tomaAccion.IdTomaAccion;

                        cmd.Parameters.Add("@chrUsuarioModi", SqlDbType.Char, 20);
                        cmd.Parameters["@chrUsuarioModi"].Value = usuario;

                        cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int, 4);
                        cmd.Parameters["@intEstadoActivo"].Value = tomaAccion.EstadoActivo;

                        cmd.ExecuteNonQuery();
                    }
                }

                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    tran.Commit();
                    if (cn.State == ConnectionState.Open)
                        cn.Close();

                    cn.Dispose();
                }
            }

            return true;
        }

        /// <summary>
        ///  este procedimiento obtiene la campaña actual del usuario evaluado
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código del uusario</param>
        /// <param name="idRol">id rol</param>
        /// <param name="periodo">periodo</param>
        /// <returns> campaña actual</returns>
        public string ObtenerCampanhaActual(string codPais, string codigoUsuario, int idRol, string periodo)
        {
            var fechaAcuerdo = string.Empty;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerCampanhaActual", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);
                cmd.Parameters["@intIdRol"].Value = idRol;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

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

        /// <summary>
        /// este método Obtiene el nombre de un país
        /// </summary>
        /// <param name="prefijoIsoPais">código del país</param>
        /// <returns>nombre del país</returns>
        public string ObtenerNombrePais(string prefijoIsoPais)
        {
            var nombre = string.Empty;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetPais", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? default(string) : dr.GetString(dr.GetOrdinal("nombre"));
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

            return nombre;
        }

        /// <summary>
        /// este método obtiene el nombre de una región
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <returns>nombre de la región</returns>
        public string ObtenerNombreRegion(string codPais, string codRegion)
        {
            var nombre = string.Empty;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetRegion", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                cmd.Parameters["@chrCodRegion"].Value = codRegion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? default(string) : dr.GetString(dr.GetOrdinal("nombre"));
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

            return nombre;
        }

        /// <summary>
        /// este método obtiene el nombre de una región
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <param name="codZona">código de zona</param>
        /// <returns>nombre de la zona</returns>
        public string ObtenerNombreZona(string codPais, string codRegion, string codZona)
        {
            var nombre = string.Empty;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetZona", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                cmd.Parameters["@chrCodRegion"].Value = codRegion;

                cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 6);
                cmd.Parameters["@chrCodZona"].Value = codZona;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nombre = dr.IsDBNull(dr.GetOrdinal("nombre")) ? default(string) : dr.GetString(dr.GetOrdinal("nombre"));
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

            return nombre;
        }

        /// <summary>
        /// este método lista todas las tomas de acciones
        /// </summary>
        /// <param name="codPais">código del país del evaluador </param>
        /// <param name="codEvaluador">código del evaluador</param>
        /// <param name="periodo">periodo del evaluador</param>
        /// <param name="idRolEvaluador">id rol del evaluador</param>
        /// <param name="idRolEvaluado">id rol del evaluado</param>
        /// <param name="estadoActivo">estadoActivo</param>
        /// <param name="codTomaAccion">código toma de acción</param>
        /// <returns>toma de acciones</returns>
        public List<BeTomaAccion> ListarTomaAcciones(string codPais, string codEvaluador, string periodo, int idRolEvaluador, int idRolEvaluado, int estadoActivo, string codTomaAccion)
        {
            var entidades = new List<BeTomaAccion>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_SelectTomaAccion", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPaisEvaluador", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPaisEvaluador"].Value = codPais;

                cmd.Parameters.Add("@ChrCodEvaluador", SqlDbType.Char, 20);
                cmd.Parameters["@ChrCodEvaluador"].Value = codEvaluador;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                cmd.Parameters.Add("@intIdRolEvaluador", SqlDbType.Int);
                cmd.Parameters["@intIdRolEvaluador"].Value = idRolEvaluador;

                cmd.Parameters.Add("@intIdRolEvaluado", SqlDbType.Int);
                cmd.Parameters["@intIdRolEvaluado"].Value = idRolEvaluado;

                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);
                cmd.Parameters["@intEstadoActivo"].Value = estadoActivo;

                cmd.Parameters.Add("@chrCodTomaAccion", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodTomaAccion"].Value = codTomaAccion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeTomaAccion
                            {
                                IdTomaAccion =
                                    dr.IsDBNull(dr.GetOrdinal("intSEQIDTomaAccion"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intSEQIDTomaAccion")),
                                PrefijoIsoPaisEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                                IdRolEvaluador =
                                    dr.IsDBNull(dr.GetOrdinal("intIdRolEvaluador"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdRolEvaluador")),
                                IdRolEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("intIdRolEvaluado"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdRolEvaluado")),
                                CodEvaluador =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodEvaluador"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodEvaluador")),
                                CodEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodEvaluado"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodEvaluado")),
                                NombreEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreEvaluado"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreEvaluado")),
                                CodRegionActual =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodRegionActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodRegionActual")),
                                NombreRegionActual =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreRegionActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreRegionActual")),
                                CodZonaActual =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodZonaActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodZonaActual")),
                                NombreZonaActual =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreZonaActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreZonaActual")),
                                TomaAccion =
                                    dr.IsDBNull(dr.GetOrdinal("chrTomaAccion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrTomaAccion")),
                                CodRegionReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodRegionReasignacion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodRegionReasignacion")),
                                NombreRegionReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreRegionReasignada"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreRegionReasignada")),
                                CodZonaReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodZonaReasignacion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodZonaReasignacion")),
                                NombreZonaReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreZonaReasignada"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreZonaReasignada")),
                                AnhoCampanhaInicio =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampanaInicio"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampanaInicio")),
                                AnhoCampanhaFin =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampanaFin"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampanaFin")),
                                Observaciones =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservaciones"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchObservaciones")),
                                Estatus =
                                    !dr.IsDBNull(dr.GetOrdinal("bitEstatus")) &&
                                    dr.GetBoolean(dr.GetOrdinal("bitEstatus")),
                                IdProceso =
                                    dr.IsDBNull(dr.GetOrdinal("intIdDProceso"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdDProceso")),
                                CorreoEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("vchCorreoEvaluado"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchCorreoEvaluado")),
                                AnhoCampanhaInicioCritico =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampanaInicioCritico"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampanaInicioCritico")),
                                AnhoCampanhaFinCritico =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampanaFinCritico"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampanaFinCritico"))
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

        /// <summary>
        /// este método lista todas las tomas de acciones
        /// </summary>
        /// <param name="codPais">código del país del evaluador </param>
        /// <param name="periodo">periodo del evaluador</param>
        /// <returns>toma de acciones</returns>
        public List<BeTomaAccion> ListarCalibraciones(string codPais, string periodo)
        {
            var entidades = new List<BeTomaAccion>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListaCalibracion", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPaisEvaluado", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPaisEvaluado"].Value = codPais;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeTomaAccion
                            {
                                IdTomaAccion =
                                    dr.IsDBNull(dr.GetOrdinal("intSEQIDTomaAccion"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intSEQIDTomaAccion")),
                                PrefijoIsoPaisEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                                IdRolEvaluador =
                                    dr.IsDBNull(dr.GetOrdinal("intIdRolEvaluador"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdRolEvaluador")),
                                IdRolEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("intIdRolEvaluado"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdRolEvaluado")),
                                CodEvaluador =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodEvaluador"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodEvaluador")),
                                NombreEvaluador =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreEvaluador"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreEvaluador")),
                                CodEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodEvaluado"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodEvaluado")),
                                NombreEvaluado =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreEvaluado"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreEvaluado")),
                                CodRegionActual =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodRegionActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodRegionActual")),
                                NombreRegionActual =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreRegionActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreRegionActual")),
                                CodZonaActual =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodZonaActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodZonaActual")),
                                NombreZonaActual =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreZonaActual"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreZonaActual")),
                                TomaAccion =
                                    dr.IsDBNull(dr.GetOrdinal("chrTomaAccion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrTomaAccion")),
                                CodRegionReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodRegionReasignacion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodRegionReasignacion")),
                                NombreRegionReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreRegionReasignada"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreRegionReasignada")),
                                CodZonaReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodZonaReasignacion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodZonaReasignacion")),
                                NombreZonaReasignacion =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombreZonaReasignada"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombreZonaReasignada")),
                                AnhoCampanhaInicio =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampanaInicio"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampanaInicio")),
                                AnhoCampanhaFin =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampanaFin"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampanaFin")),
                                Observaciones =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservaciones"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchObservaciones")),
                                Estatus =
                                    !dr.IsDBNull(dr.GetOrdinal("bitEstatus")) &&
                                    dr.GetBoolean(dr.GetOrdinal("bitEstatus")),
                                EstadoActivo =
                                    dr.IsDBNull(dr.GetOrdinal("intEstadoActivo"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intEstadoActivo"))
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

        /// <summary>
        /// este método confirma las tomas de acciones
        /// </summary>
        /// <param name="entidades">tomas de acción</param>
        /// <returns>estado </returns>
        public bool ConfirmarTomaAccion(List<BeTomaAccion> entidades)
        {
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new SqlCommand();
                        foreach (var entidad in entidades)
                        {
                            if (entidad.Estatus)
                            {
                                cmd = new SqlCommand("ESE_Matriz_ConfirmarTomaAccion", cn) { CommandType = CommandType.StoredProcedure };

                                cmd.Parameters.Add("@intSEQIDTomaAccion", SqlDbType.Int);
                                cmd.Parameters["@intSEQIDTomaAccion"].Value = entidad.IdTomaAccion;

                                cmd.Parameters.Add("@chrTomaAccion", SqlDbType.Char, 2);
                                cmd.Parameters["@chrTomaAccion"].Value = entidad.TomaAccion;


                                cmd.Parameters.Add("@chrAnhoCampanhaInicio", SqlDbType.Char, 6);
                                cmd.Parameters["@chrAnhoCampanhaInicio"].Value = entidad.AnhoCampanhaInicio;

                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        cmd.Transaction = transaction;
                        cmd.Transaction.Commit();

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
            }

            return true;
        }

        /// <summary>
        /// este método lista todas las zonas disponibles
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <param name="periodo">periodo</param>
        /// <param name="estadoActivo">estado Activo</param>
        /// <returns>zonas disponibles</returns>
        public List<BeComun> ListarAllZonaDisponible(string codPais, string codRegion, string periodo, int estadoActivo)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetAllZonaDisponible", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5);
                cmd.Parameters["@chrCodRegion"].Value = codRegion;

                cmd.Parameters.Add("@chrPeriodoAcuerdo", SqlDbType.Char, 10);
                cmd.Parameters["@chrPeriodoAcuerdo"].Value = periodo;

                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);
                cmd.Parameters["@intEstadoActivo"].Value = estadoActivo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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

        /// <summary>
        ///  este procedimiento obtiene la campaña actual del usuario evaluado
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código del uusario</param>
        /// <param name="idRol">id rol</param>
        /// <param name="periodo">periodo</param>
        /// <param name="estadoActivo">estado Activo</param>
        /// <returns> estado</returns>
        public string ValidarTomaAcuerdo(string codPais, string codigoUsuario, int idRol, string periodo, int estadoActivo)
        {
            var estado = 0;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ValidarTomaAcuerdo", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodUsuario"].Value = codigoUsuario;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);
                cmd.Parameters["@intIdRol"].Value = idRol;

                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);
                cmd.Parameters["@intEstadoActivo"].Value = estadoActivo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            estado = dr.IsDBNull(dr.GetOrdinal("estado")) ? 0 : dr.GetInt32(dr.GetOrdinal("estado"));
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

            return estado.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// este método lista todas las regiones disponibles
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="periodo">periodo</param>
        /// <param name="estadoActivo">estado Activo</param>
        /// <returns>zonas disponibles</returns>
        public List<BeComun> ListarAllRegionDisponible(string codPais, string periodo, int estadoActivo)
        {
            var entidades = new List<BeComun>();
            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_GetAllRegionDisponible", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrPeriodoAcuerdo", SqlDbType.Char, 10);
                cmd.Parameters["@chrPeriodoAcuerdo"].Value = periodo;

                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);
                cmd.Parameters["@intEstadoActivo"].Value = estadoActivo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Referencia =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
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

        /// <summary>
        /// método para obtener el correo de un supervisor
        /// </summary>
        /// <param name="codPaisEvaluado">código del país del evaluador</param>
        /// <param name="codUsuarioEvaluado">código del usuario evaluado</param>
        /// <param name="idRolEvaluador">id rol evaluador</param>
        /// <param name="idRolEvaluado">id rol evaluado</param>
        /// <returns></returns>
        public string ObtenerCorreo(string codPaisEvaluado, string codUsuarioEvaluado, int idRolEvaluador, int idRolEvaluado)
        {
            var correo = string.Empty;


            #region Registrar Acuerdos

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerCorreoSupervisor", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPaisEvaluado", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPaisEvaluado"].Value = codPaisEvaluado;

                cmd.Parameters.Add("@chrCodUsuarioEvaluado", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodUsuarioEvaluado"].Value = codUsuarioEvaluado;

                cmd.Parameters.Add("@intIdRolEvaluador", SqlDbType.Int);
                cmd.Parameters["@intIdRolEvaluador"].Value = idRolEvaluador;

                cmd.Parameters.Add("@intIdRolEvaluado", SqlDbType.Int);
                cmd.Parameters["@intIdRolEvaluado"].Value = idRolEvaluado;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            correo = dr.IsDBNull(dr.GetOrdinal("Correo")) ? default(string) : dr.GetString(dr.GetOrdinal("Correo"));
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

            return correo;
        }

        /// <summary>
        /// este método devuelve los gerentes de zona según los lineamientos establecidos para una toma de acción
        /// </summary>
        /// <param name="codUsuario">codigo de usuario</param>
        /// <param name="codPais">código del país</param>
        /// <param name="periodo">periodo actual</param>
        /// <param name="condicion">condición de la toma de acción</param>
        /// <returns>lista de gerentes de zona</returns>
        public List<BeUsuarioLineamiento> ListarGerentesZonaByLineamientos(string codUsuario, string codPais, string periodo, string condicion)
        {
            var entidades = new List<BeUsuarioLineamiento>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand();

                switch (condicion)
                {
                    case Constantes.MatrizConstPlanMejora:
                        cmd = new SqlCommand("ESE_MATRIZ_GetGerentesZonaByPlanMejora", cn);
                        break;
                    case Constantes.MatrizConstReasignacion:
                        cmd = new SqlCommand("ESE_MATRIZ_GetGerentesZonaByReasignacion", cn);
                        break;
                    case Constantes.MatrizConstRotacionSaludable:
                        cmd = new SqlCommand("ESE_MATRIZ_GetGerentesZonaByRotacionSaludable", cn);
                        break;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodoAcuerdo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodigoGerenteRegion"].Value = codUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = codPais;
                cmd.Parameters["@chrPeriodoAcuerdo"].Value = periodo;
                cmd.Parameters["@chrTipoCondicion"].Value = condicion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeUsuarioLineamiento
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Descripcion")),
                                Referencia =
                                    dr.IsDBNull(dr.GetOrdinal("CodigoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CodigoPais"))
                            };

                            if (condicion == Constantes.MatrizConstPlanMejora)
                            {
                                entidad.CampanaMinima = dr.IsDBNull(dr.GetOrdinal("CampanaMinima")) ? default(string) : dr.GetString(dr.GetOrdinal("CampanaMinima"));
                                entidad.CampanaMaxima = dr.IsDBNull(dr.GetOrdinal("CampanaMaxima")) ? default(string) : dr.GetString(dr.GetOrdinal("CampanaMaxima"));
                            }

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

        /// <summary>
        /// este método devuelve los gerentes de región según los lineamientos establecidos para una toma de acción
        /// </summary>
        /// <param name="codUsuario">codigo de usuario</param>
        /// <param name="codPais">código del país</param>
        /// <param name="periodo">periodo actual</param>
        /// <param name="condicion">condición de la toma de acción</param>
        /// <returns>lista de gerentes de región</returns>
        public List<BeUsuarioLineamiento> ListarGerentesRegionByLineamientos(string codUsuario, string codPais, string periodo, string condicion)
        {
            var entidades = new List<BeUsuarioLineamiento>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand();

                switch (condicion)
                {
                    case Constantes.MatrizConstPlanMejora:
                        cmd = new SqlCommand("ESE_MATRIZ_GetGerentesRegionByPlanMejora", cn);
                        break;
                    case Constantes.MatrizConstReasignacion:
                        cmd = new SqlCommand("ESE_MATRIZ_GetGerentesRegionByReasignacion", cn);
                        break;
                    case Constantes.MatrizConstRotacionSaludable:
                        cmd = new SqlCommand("ESE_MATRIZ_GetGerentesRegionByRotacionSaludable", cn);
                        break;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                cmd.Parameters.Add("@chrCodigoDirectoraVentas", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodoAcuerdo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodigoDirectoraVentas"].Value = codUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = codPais;
                cmd.Parameters["@chrPeriodoAcuerdo"].Value = periodo;
                cmd.Parameters["@chrTipoCondicion"].Value = condicion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeUsuarioLineamiento
                            {
                                Codigo =
                                    dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion =
                                    dr.IsDBNull(dr.GetOrdinal("Descripcion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Descripcion")),
                                Referencia =
                                    dr.IsDBNull(dr.GetOrdinal("CodigoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("CodigoPais"))
                            };

                            if (condicion == Constantes.MatrizConstPlanMejora)
                            {
                                entidad.CampanaMinima = dr.IsDBNull(dr.GetOrdinal("CampanaMinima")) ? default(string) : dr.GetString(dr.GetOrdinal("CampanaMinima"));
                                entidad.CampanaMaxima = dr.IsDBNull(dr.GetOrdinal("CampanaMaxima")) ? default(string) : dr.GetString(dr.GetOrdinal("CampanaMaxima"));
                            }

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

        /// <summary>
        /// Este store procedure lista todas las variables de enfoque de un colaborador
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codEvaluado">código del evaluado</param>
        /// <param name="idRol">rol de evaluado</param>
        /// <param name="periodo">periodo</param>
        /// <returns>variables enfoque</returns>
        public List<BeComun> ListarVariablesEnfoque(string codPais, string codEvaluado, int idRol, string periodo)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ObtenerVariableEnfoque", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPais"].Value = codPais;

                cmd.Parameters.Add("@chrCodEvaluado", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodEvaluado"].Value = codEvaluado;

                cmd.Parameters.Add("@intIdRolEvaluado", SqlDbType.Int);
                cmd.Parameters["@intIdRolEvaluado"].Value = idRol;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
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
            #endregion

        #region tomaAccion
        /// <summary>
        ///  este procedimiento obtiene las condiciones de toma de accion
        /// </summary>
        /// <param name="codGerenteZona">código de gerente de zona</param>
        /// <param name="prefijoPais">prefijo del pais</param>
        /// <param name="prefijoEvaluacion"> prefijo de la evaluacion</param>
        /// <param name="tipoCondicion">tipo de condicion</param>
        /// <returns> lista de beVerCondiciones</returns>
        public List<BeVerCondiciones> ObtenerVerCondicionesGerentesZona(string codGerenteZona, string prefijoPais, string prefijoEvaluacion, string tipoCondicion)
        {
            var lista = new List<BeVerCondiciones>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand();

                switch (tipoCondicion)
                {
                    case Constantes.MatrizConstPlanMejora:
                        cmd = new SqlCommand("ESE_MATRIZ_GetDetalleGerenteZonaByPlanMejora", cn);
                        break;
                    case Constantes.MatrizConstReasignacion:
                        cmd = new SqlCommand("ESE_MATRIZ_GetDetalleGerenteZonaByReasignacion", cn);
                        break;
                    case Constantes.MatrizConstRotacionSaludable:
                        cmd = new SqlCommand("ESE_MATRIZ_GetDetalleGerenteZonaByRotacionSaludable", cn);
                        break;
                }


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                cmd.Parameters.Add("@chrCodigoGerenteZona", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoGerenteZona"].Value = codGerenteZona;

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoPais;

                cmd.Parameters.Add("@chrPeriodoEvaluacion", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodoEvaluacion"].Value = prefijoEvaluacion;

                cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);
                cmd.Parameters["@chrTipoCondicion"].Value = tipoCondicion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        //if (dr.Read())
                        //{

                        while (dr.Read())
                        {
                            var entidad = new BeVerCondiciones
                            {
                                DescripcionLineamiento =
                                    dr.IsDBNull(dr.GetOrdinal("vchDescripcionLineamiento"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchDescripcionLineamiento")),
                                CumpleLineamiento =
                                    dr.IsDBNull(dr.GetOrdinal("chrCumpleLineamiento"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCumpleLineamiento"))
                            };
                            lista.Add(entidad);
                        }

                        //}

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

            return lista;
        }

        /// <summary>
        ///  este procedimiento obtiene las condiciones de toma de accion
        /// </summary>
        /// <param name="codGerenteRegion">código de gerente de region</param>
        /// <param name="prefijoPais">prefijo del pais</param>
        /// <param name="prefijoEvaluacion"> prefijo de la evaluacion</param>
        /// <param name="tipoCondicion">tipo de condicion</param>
        /// <returns> lista de beVerCondiciones</returns>
        public List<BeVerCondiciones> ObtenerVerCondicionesGerentesRegion(string codGerenteRegion, string prefijoPais, string prefijoEvaluacion, string tipoCondicion)
        {
            var lista = new List<BeVerCondiciones>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand();

                switch (tipoCondicion)
                {
                    case Constantes.MatrizConstPlanMejora:
                        cmd = new SqlCommand("ESE_MATRIZ_GetDetalleGerenteRegionByPlanMejora", cn);
                        break;
                    case Constantes.MatrizConstReasignacion:
                        cmd = new SqlCommand("ESE_MATRIZ_GetDetalleGerenteRegionByReasignacion", cn);
                        break;
                    case Constantes.MatrizConstRotacionSaludable:
                        cmd = new SqlCommand("ESE_MATRIZ_GetDetalleGerenteRegionByRotacionSaludable", cn);
                        break;
                }


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoGerenteRegion"].Value = codGerenteRegion;

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoPais;

                cmd.Parameters.Add("@chrPeriodoEvaluacion", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodoEvaluacion"].Value = prefijoEvaluacion;

                cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);
                cmd.Parameters["@chrTipoCondicion"].Value = tipoCondicion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        //if (dr.Read())
                        //{

                        while (dr.Read())
                        {
                            var entidad = new BeVerCondiciones
                            {
                                DescripcionLineamiento =
                                    dr.IsDBNull(dr.GetOrdinal("vchDescripcionLineamiento"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchDescripcionLineamiento")),
                                CumpleLineamiento =
                                    dr.IsDBNull(dr.GetOrdinal("chrCumpleLineamiento"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCumpleLineamiento"))
                            };
                            lista.Add(entidad);
                        }

                        //}

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

            return lista;
        }

        /// <summary>
        /// este método lista los resultados del sustento de una GR
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código de usuario</param>
        /// <param name="codVariable">código de variable</param>
        /// <param name="periodo">periodo</param>
        /// <param name="idRol">id rol evaluado</param>
        /// <param name="idTomaAccion">id secuencial toma de acción</param>
        /// <param name="tipoTomaAccion">tipo de toma de acción</param>
        /// <returns></returns>
        public List<BeResultadoMatriz> ListarResultadosSusteno(string codPais, string codigoUsuario, string codVariable, string periodo, int idRol, int idTomaAccion, string tipoTomaAccion)
        {
            var entidades = new List<BeResultadoMatriz>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                var cmd = new SqlCommand();

                if (idRol == (int)TipoRol.GerenteRegion)

                    cmd = new SqlCommand("ESE_Matriz_VerResultadoGRSustento", cn) { CommandType = CommandType.StoredProcedure };

                if (idRol == (int)TipoRol.GerenteZona)

                    cmd = new SqlCommand("ESE_Matriz_VerResultadoGZSustento", cn) { CommandType = CommandType.StoredProcedure };


                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrVariable", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intIdTomaAccion", SqlDbType.Int);
                cmd.Parameters.Add("@chrTipoTomaAccion", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodPais"].Value = codPais;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrVariable"].Value = codVariable;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intIdTomaAccion"].Value = idTomaAccion;
                cmd.Parameters["@chrTipoTomaAccion"].Value = tipoTomaAccion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeResultadoMatriz
                            {
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")),
                                CodRegionZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodRegion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodRegion"))
                            };

                            if (idRol == (int)TipoRol.GerenteZona)
                                entidad.CodRegionZona = dr.IsDBNull(dr.GetOrdinal("chrCodZona")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodZona"));

                            entidad.DocIdentidad = dr.IsDBNull(dr.GetOrdinal("chrDocIdentidad")) ? default(string) : dr.GetString(dr.GetOrdinal("chrDocIdentidad"));
                            entidad.AnioCampana = dr.IsDBNull(dr.GetOrdinal("chrAnioCampana")) ? default(string) : dr.GetString(dr.GetOrdinal("chrAnioCampana"));
                            entidad.EstadoCampana = dr.IsDBNull(dr.GetOrdinal("vchEstadoCampana")) ? default(string) : dr.GetString(dr.GetOrdinal("vchEstadoCampana"));
                            entidad.EstadoPeriodo = dr.IsDBNull(dr.GetOrdinal("vchEstadoPeriodo")) ? default(string) : dr.GetString(dr.GetOrdinal("vchEstadoPeriodo"));
                            entidad.ValorPlanCampana = dr.IsDBNull(dr.GetOrdinal("decValorPlanCampana")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorPlanCampana")));
                            entidad.ValorRealCampana = dr.IsDBNull(dr.GetOrdinal("decValorRealCampana")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorRealCampana")));
                            entidad.ValorPlanPeriodo = dr.IsDBNull(dr.GetOrdinal("decValorPlanPeriodo")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorPlanPeriodo")));
                            entidad.ValorRealPeriodo = dr.IsDBNull(dr.GetOrdinal("decValorRealPeriodo")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("decValorRealPeriodo")));
                            entidad.Tipo = dr.IsDBNull(dr.GetOrdinal("Tipo")) ? default(string) : dr.GetString(dr.GetOrdinal("Tipo"));
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

        /// <summary>
        /// Este método elimina una toma de acción
        /// </summary>
        /// <param name="idTomaAccion"> id toma de acción</param>
        /// <returns> estado</returns>
        public int DeleteTomaAccion(int idTomaAccion)
        {
            var estado = 0;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_DeleteTomaAcuerdo", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@idTomaAccion", SqlDbType.Int);
                cmd.Parameters["@idTomaAccion"].Value = idTomaAccion;

                try
                {
                    if (cmd.ExecuteNonQuery() > 0)
                        estado = 1;
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

            return estado;
        }

        #endregion


        #region "Matriz Zona"
        /// <summary>
        /// este método Obtiene el tipo de Matriz de Zona
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// /// <param name="estado">código del país</param>
        /// <returns>nombre del país</returns>
        public string ObtenerTipoMz(string codPais, byte estado)
        {
            var nombre = string.Empty;

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MZ_OBTENER_TIPO_FUENTE_VENTAS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters["@chrPrefijoIsoPais"].Value = codPais;

                cmd.Parameters.Add("@bitEstadoActivo", SqlDbType.Bit);
                cmd.Parameters["@bitEstadoActivo"].Value = estado;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nombre = dr.IsDBNull(dr.GetOrdinal("chrCodFuenteVentas")) ? default(string) : dr.GetString(dr.GetOrdinal("chrCodFuenteVentas"));
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

            return nombre;
        }


        /// <summary>
        /// Este método lista Matriz Zona Sin FuenteVentas
        /// </summary>
        /// <param name="obj">Variables Matriz Zona</param>
        /// <returns>lista Matriz Zona</returns>
        public List<BeMatrizZona> ListaMatrizZonaSinFuenteVentas(BeMatrizZonaVariables obj)
        {
            var entidades = new List<BeMatrizZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MZ_PotencialCrecimientoSinFuenteVentas", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@anho", SqlDbType.Char, 4);
                cmd.Parameters.Add("@periodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@codPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@codRegion", SqlDbType.Char, 5);
                cmd.Parameters.Add("@PPTamanhoPoblacion", SqlDbType.Decimal);
                cmd.Parameters.Add("@PMTamanhoPoblacion", SqlDbType.Decimal);
                cmd.Parameters.Add("@PGTamanhoPoblacion", SqlDbType.Decimal);
                cmd.Parameters.Add("@PGBenchMarck", SqlDbType.Decimal);
                cmd.Parameters.Add("@PPGap", SqlDbType.Decimal);
                cmd.Parameters.Add("@PMGap", SqlDbType.Decimal);
                cmd.Parameters.Add("@PGGap", SqlDbType.Decimal);
                cmd.Parameters.Add("@PPGap2", SqlDbType.Decimal);
                cmd.Parameters.Add("@PMGap2", SqlDbType.Decimal);
                cmd.Parameters.Add("@PGGap2", SqlDbType.Decimal);
                cmd.Parameters.Add("@PPTamanhoVentas", SqlDbType.Decimal);
                cmd.Parameters.Add("@PMTamanhoVentas", SqlDbType.Decimal);
                cmd.Parameters.Add("@PGTamanhoVentas", SqlDbType.Decimal);

                cmd.Parameters["@anho"].Value = obj.Anho;
                cmd.Parameters["@periodo"].Value = obj.Periodos;
                cmd.Parameters["@codPais"].Value = obj.Pais;
                cmd.Parameters["@codRegion"].Value = obj.Region;
                cmd.Parameters["@PPTamanhoPoblacion"].Value = obj.PequenhoTP;
                cmd.Parameters["@PMTamanhoPoblacion"].Value = obj.MedianoTP;
                cmd.Parameters["@PGTamanhoPoblacion"].Value = obj.GrandeTP;
                cmd.Parameters["@PGBenchMarck"].Value = obj.Benchmark;
                cmd.Parameters["@PPGap"].Value = obj.BajoRG;
                cmd.Parameters["@PMGap"].Value = obj.MedioRG;
                cmd.Parameters["@PGGap"].Value = obj.AltoRG;
                cmd.Parameters["@PPGap2"].Value = obj.BajoRFG;
                cmd.Parameters["@PMGap2"].Value = obj.MedioRFG;
                cmd.Parameters["@PGGap2"].Value = obj.AltoRFG;
                cmd.Parameters["@PPTamanhoVentas"].Value = obj.PequenhoTV;
                cmd.Parameters["@PMTamanhoVentas"].Value = obj.MedianoTV;
                cmd.Parameters["@PGTamanhoVentas"].Value = obj.GrandeTV;
                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeMatrizZona
                            {
                                DesGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("DesGerenteZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("DesGerenteZona")),
                                CodZona =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodZona")),
                                ActivasFinales =
                                    dr.IsDBNull(dr.GetOrdinal("activasFinales"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("activasFinales")),
                                Poblacion =
                                    dr.IsDBNull(dr.GetOrdinal("poblacion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("poblacion")),
                                TamPoblacion =
                                    dr.IsDBNull(dr.GetOrdinal("tamPoblacion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("tamPoblacion")),
                                Penetracion =
                                    dr.IsDBNull(dr.GetOrdinal("penetracion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("penetracion")),
                                Benchmark =
                                    dr.IsDBNull(dr.GetOrdinal("Benchmark"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Benchmark")),
                                GapPenentracion =
                                    dr.IsDBNull(dr.GetOrdinal("gapPenentracion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("gapPenentracion")),
                                RangosGAp =
                                    dr.IsDBNull(dr.GetOrdinal("rangosGAP"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("rangosGAP")),
                                Factor =
                                    dr.IsDBNull(dr.GetOrdinal("Factor"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Factor")),
                                Benchmark2 =
                                    dr.IsDBNull(dr.GetOrdinal("Benchmark2"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Benchmark2")),
                                PentFactor =
                                    dr.IsDBNull(dr.GetOrdinal("PentxFactor"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("PentxFactor")),
                                GapFactor =
                                    dr.IsDBNull(dr.GetOrdinal("gapFactor"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("gapFactor")),
                                RangosGAp2 =
                                    dr.IsDBNull(dr.GetOrdinal("RangosGAP2"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("RangosGAP2")),
                                VentaMn =
                                    dr.IsDBNull(dr.GetOrdinal("ventaMN"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("ventaMN")),
                                TamanhoVenta =
                                    dr.IsDBNull(dr.GetOrdinal("TamanhodeVenta"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("TamanhodeVenta")),
                                Cuadrante =
                                    dr.IsDBNull(dr.GetOrdinal("Cuadrante"))
                                        ? default(int)
                                        : dr.GetInt32(dr.GetOrdinal("Cuadrante")),
                                Participacion =
                                    dr.IsDBNull(dr.GetOrdinal("Participacion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Participacion"))
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


        /// <summary>
        /// Este método lista Matriz Zona Con FuenteVentas
        /// </summary>
        /// <param name="obj">Variables Matriz Zona</param>
        /// <returns>lista Matriz Zona</returns>
        public List<BeMatrizZona> ListaMatrizZonaConFuenteVentas(BeMatrizZonaVariables obj)
        {
            var entidades = new List<BeMatrizZona>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_MZ_PotencialCrecimientoConFuenteVentas", cn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodVariableActFin", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodVariableVtaNet", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 5);
                cmd.Parameters.Add("@MinimoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@MedianoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@MaximoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@MinimoVentaPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@MedianoVentaPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@MaximoVentaPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@GapBajoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@GapMedioPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@GapAltoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@FactorBajoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@FactorMedioPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@FactorAltoPercentil", SqlDbType.Decimal);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrAnio"].Value = obj.Anho;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = obj.Pais;
                cmd.Parameters["@chrCodVariableActFin"].Value = Constantes.ActivasFinales;
                cmd.Parameters["@chrCodVariableVtaNet"].Value = Constantes.VentaMn;
                cmd.Parameters["@chrCodRegion"].Value = obj.Region;
                cmd.Parameters["@MinimoPercentil"].Value = obj.PequenhoTP;
                cmd.Parameters["@MedianoPercentil"].Value = obj.MedianoTP;
                cmd.Parameters["@MaximoPercentil"].Value = obj.GrandeTP;
                cmd.Parameters["@MinimoVentaPercentil"].Value = obj.PequenhoTV;
                cmd.Parameters["@MedianoVentaPercentil"].Value = obj.MedianoTV;
                cmd.Parameters["@MaximoVentaPercentil"].Value = obj.GrandeTV;
                cmd.Parameters["@GapBajoPercentil"].Value = obj.BajoRG;
                cmd.Parameters["@GapMedioPercentil"].Value = obj.MedioRG;
                cmd.Parameters["@GapAltoPercentil"].Value = obj.AltoRG;
                cmd.Parameters["@FactorBajoPercentil"].Value = obj.BajoRFG;
                cmd.Parameters["@FactorMedioPercentil"].Value = obj.MedioRFG;
                cmd.Parameters["@FactorAltoPercentil"].Value = obj.AltoRFG;
                cmd.Parameters["@chrPeriodo"].Value = obj.Periodos;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeMatrizZona
                            {
                                DesGerenteZona =
                                    dr.IsDBNull(dr.GetOrdinal("DesGerenteZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("DesGerenteZona")),
                                CodZona =
                                    dr.IsDBNull(dr.GetOrdinal("Zona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Zona")),
                                ActivasFinales =
                                    dr.IsDBNull(dr.GetOrdinal("ActivasFinales"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("ActivasFinales")),
                                Poblacion =
                                    dr.IsDBNull(dr.GetOrdinal("Poblacion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Poblacion")),
                                TamPoblacion =
                                    dr.IsDBNull(dr.GetOrdinal("TamPoblacion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("TamPoblacion")),
                                GPs =
                                    dr.IsDBNull(dr.GetOrdinal("GPS"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("GPS")),
                                Penetracion =
                                    dr.IsDBNull(dr.GetOrdinal("Penetracion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Penetracion")),
                                Benchmark =
                                    dr.IsDBNull(dr.GetOrdinal("Benchmark"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Benchmark")),
                                GapPenentracion =
                                    dr.IsDBNull(dr.GetOrdinal("GAPPenetracion"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("GAPPenetracion")),
                                RangosGAp2 =
                                    dr.IsDBNull(dr.GetOrdinal("RangosGAp"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("RangosGAp")),
                                Grupo =
                                    dr.IsDBNull(dr.GetOrdinal("Grupo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("Grupo")),
                                Factor =
                                    dr.IsDBNull(dr.GetOrdinal("Factor"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Factor")),
                                Benchmark2 =
                                    dr.IsDBNull(dr.GetOrdinal("Benchmark2"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("Benchmark2")),
                                PentFactor =
                                    dr.IsDBNull(dr.GetOrdinal("PenetracionFactor"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("PenetracionFactor")),
                                GapFactor =
                                    dr.IsDBNull(dr.GetOrdinal("GAPFactor"))
                                        ? default(decimal)
                                        : dr.GetDecimal(dr.GetOrdinal("GAPFactor"))
                            };
                            entidad.RangosGAp2 = dr.IsDBNull(dr.GetOrdinal("RangosGAP2")) ? default(string) : dr.GetString(dr.GetOrdinal("RangosGAP2"));
                            entidad.VentaMn = dr.IsDBNull(dr.GetOrdinal("VentasNetas")) ? default(decimal) : dr.GetDecimal(dr.GetOrdinal("VentasNetas"));
                            entidad.TamanhoVenta = dr.IsDBNull(dr.GetOrdinal("TamanioVenta")) ? default(string) : dr.GetString(dr.GetOrdinal("TamanioVenta"));
                            entidad.Cuadrante = dr.IsDBNull(dr.GetOrdinal("Cuadrante")) ? default(int) : dr.GetInt32(dr.GetOrdinal("Cuadrante"));
                            entidad.Participacion = dr.IsDBNull(dr.GetOrdinal("Participacion")) ? default(decimal) : dr.GetDecimal(dr.GetOrdinal("Participacion"));

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



        #endregion
    }
}