
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaCondiciones : DaConexion
    {
        #region Condiciones
        public List<BeCondiciones> ObtenerCondiciones(string prefijoIsoPais, string tipoCondicion, int estado)
        {
            var condiciones = new List<BeCondiciones>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_ListarCondiciones", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);
                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int, 4);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrTipoCondicion"].Value = tipoCondicion;
                cmd.Parameters["@intEstadoActivo"].Value = estado;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var condicion = new BeCondiciones
                        {
                            prefijoIsoPais = dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                            tipoCondicion = dr.GetString(dr.GetOrdinal("chrTipoCondicion")),
                            descripcionCondicion = dr.GetString(dr.GetOrdinal("vchDescripcionCondicion")),
                            numeroCondicionLineamiento = dr.GetString(dr.GetOrdinal("chrNumeroCondicionLineamiento")),
                            estadoActivo = dr.GetInt32(dr.GetOrdinal("intEstadoActivo"))
                        };

                        condiciones.Add(condicion);
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return condiciones;
        }

        public bool ActualizarCondiciones(List<BeCondiciones> be, string usuario)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var tran = conn.BeginTransaction();

                try
                {
                    foreach (var filaBe in be)
                    {
                        var cmd = new SqlCommand("ESE_MATRIZ_ActualizarCondiciones", conn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = tran
                        };

                        cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@vchDescripcionCondicion", SqlDbType.VarChar, 100);
                        cmd.Parameters.Add("@chrNumeroCondicionLineamiento", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int, 4);
                        cmd.Parameters.Add("@chrUsuario", SqlDbType.Char, 20);

                        cmd.Parameters["@chrPrefijoIsoPais"].Value = filaBe.prefijoIsoPais;
                        cmd.Parameters["@chrTipoCondicion"].Value = filaBe.tipoCondicion;
                        cmd.Parameters["@vchDescripcionCondicion"].Value = filaBe.descripcionCondicion;
                        cmd.Parameters["@chrNumeroCondicionLineamiento"].Value = filaBe.numeroCondicionLineamiento;
                        cmd.Parameters["@intEstadoActivo"].Value = filaBe.estadoActivo;
                        cmd.Parameters["@chrUsuario"].Value = usuario;

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
                    conn.Close();
                }
            }
            return true;
        }
        #endregion
    }

    public class DaCondicionesDetalle : DaConexion
    {
        #region CondicionesDetalle
        public List<BeCondicionesDetalle> ObtenerCondicionesDetalle(string prefijoIsoPais, string tipoCondicion, string numeroCondicionLineamiento)
        {
            var condicionesDetalle = new List<BeCondicionesDetalle>();

            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_ListarCondicionesDetalle", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrNumeroCondicionLineamiento", SqlDbType.Char, 2);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrTipoCondicion"].Value = tipoCondicion;
                cmd.Parameters["@chrNumeroCondicionLineamiento"].Value = numeroCondicionLineamiento;

                try
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var condiciones = new BeCondicionesDetalle
                        {
                            descripcionVariables = dr.GetString(dr.GetOrdinal("vchDescripcionVariables")),
                            valorVariable = dr.GetDecimal(dr.GetOrdinal("decValorVariable")),
                            tipoVariable = dr.GetString(dr.GetOrdinal("chrTipoVariable"))
                        };

                        condicionesDetalle.Add(condiciones);
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return condicionesDetalle;
        }

        public bool ActualizarCondicionesDetalle(List<BeCondicionesDetalle> be, string usuario)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    foreach (var bed in be)
                    {
                        var cmd = new SqlCommand("ESE_MATRIZ_ActualizarCondicionesDetalle", conn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = tran
                        };

                        cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@chrTipoCondicion", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@chrNumeroCondicionLineamiento", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@decValorVariable", SqlDbType.Decimal, 9);
                        cmd.Parameters.Add("@chrTipoVariable", SqlDbType.Char, 2);
                        cmd.Parameters.Add("@chrUsuario", SqlDbType.Char, 20);

                        cmd.Parameters["@chrPrefijoIsoPais"].Value = bed.prefijoIsoPais;
                        cmd.Parameters["@chrTipoCondicion"].Value = bed.tipoCondicion;
                        cmd.Parameters["@chrNumeroCondicionLineamiento"].Value = bed.numeroCondicionLineamiento;
                        cmd.Parameters["@decValorVariable"].Value = bed.valorVariable;
                        cmd.Parameters["@chrTipoVariable"].Value = bed.tipoVariable;
                        cmd.Parameters["@chrUsuario"].Value = usuario;

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
                    conn.Close();
                }
            }
            return true;
        }
        #endregion
    }
}