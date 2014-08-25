
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaTimeLine : DaConexion
    {
        /// <summary>
        /// este método obtiene las gr/gz a las cuales se le hizo una toma de acción con sus respectivos estados en cada campaña del periodo
        /// </summary>
        /// <param name="codPaisEvaluador">código del país evaluador</param>
        /// <param name="codUsuarioEvaluador">código del usuario evaluador</param>
        /// <param name="idRolEvaluador">id rol del evaluador</param>
        /// <param name="idRolEvaluado">id del rol evaluado</param>
        /// <param name="codTomaAccion">código de la toma de acción</param>
        /// <param name="periodo">periodo</param>
        /// <returns>detalle Time Line</returns>
        public List<BeTimeLineDetetalle> ListarTimeLine(string codPaisEvaluador, string codUsuarioEvaluador, int idRolEvaluador, int idRolEvaluado, string codTomaAccion, string periodo)
        {
            var entidades = new List<BeTimeLineDetetalle>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_TimeLine_Listar", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodPaisEvaluador", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodPaisEvaluador"].Value = codPaisEvaluador;

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codUsuarioEvaluador;

                cmd.Parameters.Add("@intRolEvaluador", SqlDbType.Int);
                cmd.Parameters["@intRolEvaluador"].Value = idRolEvaluador;

                cmd.Parameters.Add("@intIdRolEvaluado", SqlDbType.Int);
                cmd.Parameters["@intIdRolEvaluado"].Value = idRolEvaluado;

                cmd.Parameters.Add("@chrCodTomaAccion", SqlDbType.Char, 2);
                cmd.Parameters["@chrCodTomaAccion"].Value = codTomaAccion;

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeTimeLineDetetalle
                            {
                                CodPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")),
                                AnhoCampanha =
                                    dr.IsDBNull(dr.GetOrdinal("chrAnioCampana"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrAnioCampana")),
                                CodDocIdentidad =
                                    dr.IsDBNull(dr.GetOrdinal("chrDocIdentidad"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrDocIdentidad")),
                                Nombre =
                                    dr.IsDBNull(dr.GetOrdinal("chrNombreColaborador"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrNombreColaborador")),
                                IdRol =
                                    dr.IsDBNull(dr.GetOrdinal("intIdRolEvaluado"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdRolEvaluado")),
                                CodRegion =
                                    dr.IsDBNull(dr.GetOrdinal("nomRegion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("nomRegion")),
                                CodZona =
                                    dr.IsDBNull(dr.GetOrdinal("nomZona"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("nomZona")),
                                EstadoCampanha =
                                    dr.IsDBNull(dr.GetOrdinal("vchEstadoCamp"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchEstadoCamp")),
                                PeriodoToma =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodoToma"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrPeriodoToma")),
                                CampanhaToma =
                                    dr.IsDBNull(dr.GetOrdinal("chrCampanaToma"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCampanaToma")),
                                CodTomaAccion =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodTomaAccion"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("chrCodTomaAccion")),
                                Observaciones =
                                    dr.IsDBNull(dr.GetOrdinal("vchObservaciones"))
                                        ? default(string)
                                        : dr.GetString(dr.GetOrdinal("vchObservaciones"))
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