
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaCompetencia : DaConexion
    {
        /// <summary>
        /// Selecciona todos los Gerentes de Region y zona y trae sus documentos
        /// </summary>
        /// <returns></returns>
        public List<BeCompetencia> SeleccionarGerenteNumeroDocumento()
        {
            var lstGerenteNumeroDocumento = new List<BeCompetencia>();
            using (var cn = ObtieneConexion())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_Matriz_ListarGerenteNumeroDocumento", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objGerente = new BeCompetencia
                    {
                        CodigoPaisAdam = dr["pCodigoPaisADAM"].ToString(),
                        Anio = dr["pAnioCurso"].ToString(),
                        DocIdentidad = dr["pNumeroDocumentoIdentidad"].ToString(),
                        PrefijoIsoPais = dr["chrPrefijoIsoPais"].ToString(),
                        IdRol = int.Parse(dr["intIDRol"].ToString()),
                        Cub = dr["CUB"].ToString()
                    };
                    lstGerenteNumeroDocumento.Add(objGerente);
                }
                dr.Close();
            }
            return lstGerenteNumeroDocumento;
        }

        public void AgregarCompetencia(BeCompetencia variable)
        {
            using (var conn = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Matriz_Insertar_Competencia", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add("@intCodigoCompetencia", SqlDbType.Int);
                cmd.Parameters.Add("@chrprefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchCompetencia", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters.Add("@intEstadoActivo", SqlDbType.Int);
                cmd.Parameters.Add("@vchDescripcion", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchSugerencia", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@decPorcentajeAvance", SqlDbType.Decimal);
                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);

                cmd.Parameters["@intCodigoCompetencia"].Value = variable.CodigoCompetencia;
                cmd.Parameters["@chrprefijoIsoPais"].Value = variable.PrefijoIsoPais;
                cmd.Parameters["@vchCompetencia"].Value = variable.Competencia;
                cmd.Parameters["@chrCodigoColaborador"].Value = variable.CodigoColaborador;
                cmd.Parameters["@chrAnio"].Value = variable.Anio;
                cmd.Parameters["@intEstadoActivo"].Value = variable.EstadoActivo;
                cmd.Parameters["@vchDescripcion"].Value = variable.Descripcion;
                cmd.Parameters["@vchSugerencia"].Value = variable.Sugerencia;
                cmd.Parameters["@decPorcentajeAvance"].Value = variable.PorcentajeAvance;
                cmd.Parameters["@intIdRol"].Value = variable.IdRol;

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

        #region "ALTAS Y BAJAS"

        public List<BeCompetencia> CompetenciasListarHistorico(string prefijoIsoPais, string codigoColaborador)
        {
            var lstGerenteNumeroDocumento = new List<BeCompetencia>();
            using (var cn = ObtieneConexion())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_SP_COMPETENCIAS_LISTAR_HISTORICO", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = prefijoIsoPais;
                cmd.Parameters.Add("@chrCodigoColaborador", SqlDbType.Char, 20).Value = codigoColaborador;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objGerente = new BeCompetencia
                    {
                        Anio = dr["chrAnio"].ToString(),
                        Competencia = dr["vchCompetencia"].ToString(),
                        PorcentajeAvance = Convert.ToDecimal((dr["decPorcentajeAvance"].ToString())),
                        Sugerencia = dr["vchSugerencia"].ToString(),
                        Descripcion = dr["vchDescripcion"].ToString(),
                        Rol = dr["Rol"].ToString()
                    };
                    //objGerente.PorcentajeAvance = int.Parse(dr["decPorcentajeAvance"].ToString());

                    lstGerenteNumeroDocumento.Add(objGerente);
                }
                dr.Close();
            }
            return lstGerenteNumeroDocumento;
        }

        #endregion



        public void InsertarLogCargaCompetencia(string anhoCub, string descripcion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_COMPETENCIA_INSERT_LOG_CARGA", conn) { CommandTimeout = 300, CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@vchAnhoCub", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@vchDescripcion", SqlDbType.VarChar, 1000);
                cmd.Parameters["@vchAnhoCub"].Value = anhoCub;
                cmd.Parameters["@vchDescripcion"].Value = descripcion;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                conn.Close();
            }
        }

    }
}