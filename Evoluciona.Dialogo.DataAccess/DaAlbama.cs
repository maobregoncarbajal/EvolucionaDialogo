

namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using BusinessEntity;

    public class DaAlbama : DaConexion
    {
        public List<BeComun> ListarPaises(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("[ESE_SP_DIRECTORIO_LISTAR_PAISES]", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodigoPaisComercial", SqlDbType.VarChar, 3);
                cmd.Parameters["@chrCodigoPaisComercial"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                             ? default(string)
                                             : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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



        public List<BeComun> ListarRegiones(string codPais)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_LISTAR_REGIONES", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodigoPaisComercial", SqlDbType.VarChar, 3);
                cmd.Parameters["@chrCodigoPaisComercial"].Value = codPais;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                             ? default(string)
                                             : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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


        public List<BeComun> ListarZonas(string codPais, string codRegion)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_LISTAR_ZONAS", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodigoPaisComercial", SqlDbType.VarChar, 3);
                cmd.Parameters.Add("@CodRegion", SqlDbType.Char, 3);
                cmd.Parameters["@chrCodigoPaisComercial"].Value = codPais;
                cmd.Parameters["@CodRegion"].Value = codRegion;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                             ? default(string)
                                             : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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


        public List<BeComun> ListarCargo(string codCargo)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_LISTAR_CARGO", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@CodigoCargo", SqlDbType.Char, 2);
                cmd.Parameters["@CodigoCargo"].Value = codCargo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                             ? default(string)
                                             : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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


        public List<BeComun> ListarEstadoCargo(string codEstadoCargo)
        {
            var entidades = new List<BeComun>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_LISTAR_ESTADO_CARGO", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@CodigoEstadoCargo", SqlDbType.Char, 2);
                cmd.Parameters["@CodigoEstadoCargo"].Value = codEstadoCargo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeComun
                            {
                                Codigo = dr.IsDBNull(dr.GetOrdinal("Codigo"))
                                             ? default(string)
                                             : dr.GetString(dr.GetOrdinal("Codigo")),
                                Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion"))
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



    }
}
