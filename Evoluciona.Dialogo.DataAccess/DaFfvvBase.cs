
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaFfvvBase : DaConexion
    {
        public List<BeFfvvBase> Lista_in_ffvv_base()
        {
            var entidades = new List<BeFfvvBase>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_LISTAR_IN_FFVV_BASE", cn) { CommandType = CommandType.StoredProcedure };


                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeFfvvBase
                            {
                                _id =
                                    dr.IsDBNull(dr.GetOrdinal("intId"))
                                        ? default(int)
                                        : dr.GetInt32(dr.GetOrdinal("intId")),
                                Cub =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodCUB"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchCodCUB")),
                                DocIdentidad =
                                    dr.IsDBNull(dr.GetOrdinal("vchDocIdentidad"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchDocIdentidad")),
                                Planilla =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanilla"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanilla")),
                                CodRol =
                                    dr.IsDBNull(dr.GetOrdinal("chrROL"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrROL")),
                                DesRol =
                                    dr.IsDBNull(dr.GetOrdinal("vchRol"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchRol")),
                                Nombres =
                                    dr.IsDBNull(dr.GetOrdinal("vchNombres"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchNombres")),
                                ApePaterno =
                                    dr.IsDBNull(dr.GetOrdinal("vchApellidoPaterno"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchApellidoPaterno")),
                                ApeMaterno =
                                    dr.IsDBNull(dr.GetOrdinal("vchApellidoMaterno"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchApellidoMaterno")),
                                CodPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPais")),
                                CodRegion =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoRegion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchCodigoRegion")),
                                CodZona =
                                    dr.IsDBNull(dr.GetOrdinal("vchCodigoZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchCodigoZona")),
                                Email =
                                    dr.IsDBNull(dr.GetOrdinal("vchEmail"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchEmail")),
                                Sexo =
                                    dr.IsDBNull(dr.GetOrdinal("chrSexo"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrSexo")),
                                Estado =
                                    dr.IsDBNull(dr.GetOrdinal("chrEstadoColaborador"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrEstadoColaborador")),
                                CubJefe =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodCUBJefe"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodCUBJefe")),
                                PlanillaJefe =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoPlanillaJefe"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodigoPlanillaJefe"))
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



        public bool Delete_in_ffvv_base(int id)
        {
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ELIMINAR_IN_FFVV_BASE", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters["@Id"].Value = id;

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



        public bool Add_in_ffvv_base(BeFfvvBase obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_INSERTAR_IN_FFVV_BASE", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@vchCodCUB", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@vchRol", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchCodigoZona", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchApellidoPaterno", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchApellidoMaterno", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoPlanillaJefe", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodCUBJefe", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrROL", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEmail", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrSexo", SqlDbType.Char, 1);
                cmd.Parameters.Add("@vchDocIdentidad", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrEstadoColaborador", SqlDbType.Char, 1);

                cmd.Parameters["@vchCodCUB"].Value = obj.Cub;
                cmd.Parameters["@vchRol"].Value = obj.DesRol;
                cmd.Parameters["@vchCodigoRegion"].Value = obj.CodRegion;
                cmd.Parameters["@vchCodigoZona"].Value = obj.CodZona;
                cmd.Parameters["@vchNombres"].Value = obj.Nombres;
                cmd.Parameters["@vchApellidoPaterno"].Value = obj.ApePaterno;
                cmd.Parameters["@vchApellidoMaterno"].Value = obj.ApeMaterno;
                cmd.Parameters["@chrCodigoPais"].Value = obj.CodPais;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.Planilla;
                cmd.Parameters["@chrCodigoPlanillaJefe"].Value = obj.PlanillaJefe;
                cmd.Parameters["@chrCodCUBJefe"].Value = obj.CubJefe;
                cmd.Parameters["@chrROL"].Value = obj.CodRol;
                cmd.Parameters["@vchEmail"].Value = obj.Email;
                cmd.Parameters["@chrSexo"].Value = obj.Sexo;
                cmd.Parameters["@vchDocIdentidad"].Value = obj.DocIdentidad;
                cmd.Parameters["@chrEstadoColaborador"].Value = obj.Estado;

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


        public bool Edit_in_ffvv_base(BeFfvvBase obj)
        {
            bool resultado;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_ACTUALIZAR_IN_FFVV_BASE", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intId", SqlDbType.Int);
                cmd.Parameters.Add("@vchCodCUB", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@vchRol", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchCodigoRegion", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@vchCodigoZona", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchApellidoPaterno", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchApellidoMaterno", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodigoPlanilla", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoPlanillaJefe", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodCUBJefe", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrROL", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEmail", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrSexo", SqlDbType.Char, 1);
                cmd.Parameters.Add("@vchDocIdentidad", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrEstadoColaborador", SqlDbType.Char, 1);

                cmd.Parameters["@intId"].Value = obj._id;
                cmd.Parameters["@vchCodCUB"].Value = obj.Cub;
                cmd.Parameters["@vchRol"].Value = obj.DesRol;
                cmd.Parameters["@vchCodigoRegion"].Value = obj.CodRegion;
                cmd.Parameters["@vchCodigoZona"].Value = obj.CodZona;
                cmd.Parameters["@vchNombres"].Value = obj.Nombres;
                cmd.Parameters["@vchApellidoPaterno"].Value = obj.ApePaterno;
                cmd.Parameters["@vchApellidoMaterno"].Value = obj.ApeMaterno;
                cmd.Parameters["@chrCodigoPais"].Value = obj.CodPais;
                cmd.Parameters["@chrCodigoPlanilla"].Value = obj.Planilla;
                cmd.Parameters["@chrCodigoPlanillaJefe"].Value = obj.PlanillaJefe;
                cmd.Parameters["@chrCodCUBJefe"].Value = obj.CubJefe;
                cmd.Parameters["@chrROL"].Value = obj.CodRol;
                cmd.Parameters["@vchEmail"].Value = obj.Email;
                cmd.Parameters["@chrSexo"].Value = obj.Sexo;
                cmd.Parameters["@vchDocIdentidad"].Value = obj.DocIdentidad;
                cmd.Parameters["@chrEstadoColaborador"].Value = obj.Estado;

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
