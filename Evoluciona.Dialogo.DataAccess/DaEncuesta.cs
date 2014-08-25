
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEncuesta : DaConexion
    {
        public List<BeEncuesta> ObtenerPreguntasEncuesta(int idProceso, TipoEncuesta tipoEncuesta)
        {
            var preguntasEncuesta = new List<BeEncuesta>();

            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_ObtenerEncuestaPregunta", cnn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@intTipoEncuesta", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = (int)tipoEncuesta;

                try
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var encuesta = new BeEncuesta
                        {
                            IdPregunta = reader.GetInt32(reader.GetOrdinal("intIDPregunta")),
                            Pregunta = reader.GetString(reader.GetOrdinal("vchPregunta"))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("intRespuesta")))
                            encuesta.Respuesta = reader.GetInt32(reader.GetOrdinal("intRespuesta"));

                        preguntasEncuesta.Add(encuesta);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    cnn.Close();
                }
            }

            return preguntasEncuesta;
        }

        public bool RegistrarEncuesta(List<BeEncuesta> nuevaEncuesta, TipoEncuesta tipoEncuesta)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                using (var transaccion = cnn.BeginTransaction())
                {
                    foreach (var encuesta in nuevaEncuesta)
                    {
                        #region Registrar una Encuesta

                        var cmd = new SqlCommand("ESE_RegistrarEncuesta", cnn, transaccion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                        cmd.Parameters.Add("@intIDPregunta", SqlDbType.Int);
                        cmd.Parameters.Add("@vchCodigoUsuario", SqlDbType.VarChar, 20);
                        cmd.Parameters.Add("@intRespuesta", SqlDbType.Int);
                        cmd.Parameters.Add("@intTipoEncuesta", SqlDbType.Int);

                        cmd.Parameters[0].Value = encuesta.IdProceso;
                        cmd.Parameters[1].Value = encuesta.IdPregunta;
                        cmd.Parameters[2].Value = encuesta.CodigoUsuario;
                        cmd.Parameters[3].Value = encuesta.Respuesta;
                        cmd.Parameters[4].Value = (int)tipoEncuesta;

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            transaccion.Rollback();
                        }

                        #endregion
                    }

                    transaccion.Commit();
                }

                return true;
            }
        }
    }
}
