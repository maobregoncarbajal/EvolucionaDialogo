
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEncuestaReporte : DaConexion
    {
        public List<BeEncuestaReporte> ListaEncuestaReporte()
        {
            var entidades = new List<BeEncuestaReporte>();

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("[ESE_ADMIN_ENCUESTA_REPORTE]", cn) { CommandType = CommandType.StoredProcedure };

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var entidad = new BeEncuestaReporte
                            {
                                IdEncuestaRespuestaDialogo =
                                    dr.IsDBNull(dr.GetOrdinal("intIdEncuestaRespuestaDialogo"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intIdEncuestaRespuestaDialogo")),
                                Periodo =
                                    dr.IsDBNull(dr.GetOrdinal("chrPeriodo"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPeriodo")).Trim(),
                                DesTipoEncuesta =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesTipoEncuesta"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesTipoEncuesta")).Trim(),
                                PrefijoIsoPais =
                                    dr.IsDBNull(dr.GetOrdinal("chrPrefijoIsoPais"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrPrefijoIsoPais")).Trim(),
                                Rol =
                                    dr.IsDBNull(dr.GetOrdinal("ROL"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("ROL")).Trim(),
                                CodigoUsuario =
                                    dr.IsDBNull(dr.GetOrdinal("chrCodigoUsuario"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("chrCodigoUsuario")).Trim(),
                                Cub =
                                    dr.IsDBNull(dr.GetOrdinal("CUB"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("CUB")).Trim(),
                                DesPreguntas =
                                    dr.IsDBNull(dr.GetOrdinal("vchDesPreguntas"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchDesPreguntas")).Trim(),
                                Comentario =
                                    dr.IsDBNull(dr.GetOrdinal("vchComentario"))
                                        ? string.Empty
                                        : dr.GetString(dr.GetOrdinal("vchComentario")).Trim(),
                                ValorPuntaje =
                                    dr.IsDBNull(dr.GetOrdinal("intValorPuntaje"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("intValorPuntaje")),
                                PuntajeAcumulado =
                                    dr.IsDBNull(dr.GetOrdinal("PUNTAJE_ACUMULADO"))
                                        ? 0
                                        : dr.GetInt32(dr.GetOrdinal("PUNTAJE_ACUMULADO"))
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
