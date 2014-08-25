
namespace Evoluciona.Dialogo.DataAccess
{
    using Helpers;
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaVisitaEvoluviona : DaConexion
    {
        // Cargar preguntas por rol
        public DataTable ObtenerPreguntasXRol(string connstring, int codigoRol)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_Obtener_Pregunta_PorRol", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);

                cmd.Parameters[0].Value = codigoRol;

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

        #region MetodosReporteVisitasCampana

        public DataTable ObtenerVisitasCampana(string prefijoPais, string periodo, int codigoRol)
        {
            var ds = new DataSet();
            string nombreSp;
            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    nombreSp = "ESE_REPORTE_VISITAS_CAMPANA";
                    break;
                case Constantes.RolGerenteRegion:
                    nombreSp = "ESE_REPORTE_VISITAS_CAMPANAGZ";
                    break;
                case Constantes.RolGerenteZona:
                    nombreSp = "ESE_REPORTE_VISITAS_CAMPANALET";
                    break;
                default:
                    return ds.Tables[0];
            }

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand(nombreSp, conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 10);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

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

        public List<BeDatosVisitaReporte> ObtenerDatosVisitaEstado(string periodo, string prefijoPais, string estadoEvaluada, int codigoRol)
        {
            var dtDatos = new DataTable();
            var listDatosVisitaEstado = new List<BeDatosVisitaReporte>();

            string nombreSp;
            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    nombreSp = "ESE_ObtenerDatosVisita2";
                    break;
                case Constantes.RolGerenteRegion:
                    nombreSp = "ESE_ObtenerDatosVisita2GZ";
                    break;
                case Constantes.RolGerenteZona:
                    nombreSp = "ESE_ObtenerDatosVisita2LET";
                    break;
                default:
                    return listDatosVisitaEstado;
            }

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand(nombreSp, conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@periodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@prefijoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 30);

                cmd.Parameters["@periodo"].Value = periodo;
                cmd.Parameters["@prefijoPais"].Value = prefijoPais;
                cmd.Parameters["@vchEstadoPeriodo"].Value = estadoEvaluada;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objVisita = new BeDatosVisitaReporte
                        {
                            Codigo = drDatos["chrCodGerenteRegional"].ToString(),
                            AnioCampana = drDatos["chrAnioCampana"].ToString(),
                            EstadoVisita = drDatos["chrEstadoVisita"].ToString(),
                            Cantidad = Convert.ToInt32(drDatos["cantidad"])
                        };
                        listDatosVisitaEstado.Add(objVisita);
                    }
                }
                conex.Close();
            }
            return listDatosVisitaEstado;
        }

        public List<BeDatosVisitaReporte> ObtenerDatosVisitaRanking(string periodo, string prefijoPais, string estadoEvaluada, int codigoRol)
        {
            var dtDatos = new DataTable();
            var listDatosVisitaRanking = new List<BeDatosVisitaReporte>();

            string nombreSp;
            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    nombreSp = "ESE_ObtenerDatosVisita3";
                    break;
                case Constantes.RolGerenteRegion:
                    nombreSp = "ESE_ObtenerDatosVisita3GZ";
                    break;
                case Constantes.RolGerenteZona:
                    nombreSp = "ESE_ObtenerDatosVisita3LET";
                    break;
                default:
                    return listDatosVisitaRanking;
            }

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand(nombreSp, conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@periodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@prefijoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 30);

                cmd.Parameters["@periodo"].Value = periodo;
                cmd.Parameters["@prefijoPais"].Value = prefijoPais;
                cmd.Parameters["@vchEstadoPeriodo"].Value = estadoEvaluada;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objVisita = new BeDatosVisitaReporte
                        {
                            Codigo = drDatos["chrCodGerenteRegional"].ToString(),
                            Ranking = Convert.ToInt32(drDatos["intPtoRankingProdCamp"]),
                            AnioCampana = drDatos["chrAnioCampana"].ToString()
                        };
                        listDatosVisitaRanking.Add(objVisita);
                    }
                }
                conex.Close();
            }
            return listDatosVisitaRanking;
        }

        public string ObtenerCampanasPorPeriodo(string periodo)
        {
            var campanas = "";
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerCampaniaxPeriodo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrPeriodo"].Value = periodo;

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    campanas = dr["CAMPANIA"].ToString();
                }
                dr.Close();

                conex.Close();
            }
            return campanas;
        }

        public DataSet ObtenerPeriodosxAnio(string anio)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_ObtenerPerioxAnio", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrAnio", SqlDbType.Char, 4);
                cmd.Parameters["@chrAnio"].Value = anio;

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
            return ds;
        }

        public List<BeDatosVisitaReporte> ObtenerDatosVisitaDetalle(string periodo, string prefijoPais, string estadoEvaluada, int codigoRol)
        {
            var dtDatosProceso = new DataTable();
            var listaDatosVisitas = new List<BeDatosVisitaReporte>();
            var listDatosVisitaRanking = ObtenerDatosVisitaRanking(periodo, prefijoPais, estadoEvaluada, codigoRol);
            var listDatosVisitaEstado = ObtenerDatosVisitaEstado(periodo, prefijoPais, estadoEvaluada, codigoRol);
            var campanasPeriodo = ObtenerCampanasPorPeriodo(periodo);

            string nombreSp;
            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    nombreSp = "ESE_ObtenerDatosVisita";
                    break;
                case Constantes.RolGerenteRegion:
                    nombreSp = "ESE_ObtenerDatosVisitaGZ";
                    break;
                case Constantes.RolGerenteZona:
                    nombreSp = "ESE_ObtenerDatosVisitaLET";
                    break;
                default:
                    return listaDatosVisitas;
            }

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand(nombreSp, conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@periodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@prefijoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 30);

                cmd.Parameters["@periodo"].Value = periodo;
                cmd.Parameters["@prefijoPais"].Value = prefijoPais;
                cmd.Parameters["@vchEstadoPeriodo"].Value = estadoEvaluada;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count > 0)
                {
                    //rrecorrer los datos de la tabla principal para obtener los codigo de documentos
                    for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                    {
                        var objVisitaDetalle = new BeDatosVisitaReporte
                        {
                            CodZona = dtDatosProceso.Rows[i]["chrCodRegion"].ToString(),
                            Codigo = dtDatosProceso.Rows[i]["chrCodGerenteRegional"].ToString(),
                            Descripcion = dtDatosProceso.Rows[i]["vchDesGerenteRegional"].ToString(),
                            Variable1 = dtDatosProceso.Rows[i]["variable1"].ToString(),
                            Variable2 = dtDatosProceso.Rows[i]["variable2"].ToString()
                        };

                        //recorrer todas las campañas por periodo
                        var campanas = campanasPeriodo.Split(',');

                        for (var j = 0; j < campanas.Length; j++)
                        {
                            var itemCampana = j + 1;

                            //buscar x docidentidad y campaña para obtener los estados de las visitas
                            var listVisitasEstados = listDatosVisitaEstado.FindAll(
                                objTemporal =>
                                    objTemporal.AnioCampana == campanas[j] &&
                                    objTemporal.Codigo.Trim() == objVisitaDetalle.Codigo.Trim());

                            if (listVisitasEstados.Count > 0)
                            {
                                foreach (var objResumenTemp in listVisitasEstados)
                                {
                                    RetornarEstadoVariableCampania(ref objVisitaDetalle, itemCampana, objResumenTemp.EstadoVisita, objResumenTemp.Cantidad);
                                }
                            }

                            //buscar x doc y campaña para buscar el ranking
                            var listVisitasRanking = listDatosVisitaRanking.FindAll(
                                objTemporal =>
                                    objTemporal.AnioCampana == campanas[j] &&
                                    objTemporal.Codigo.Trim() == objVisitaDetalle.Codigo.Trim());

                            if (listVisitasRanking.Count <= 0) continue;
                            foreach (var objResumenTemp in listVisitasRanking)
                            {
                                RetornarRankingVariableCampania(ref objVisitaDetalle, itemCampana, objResumenTemp.Ranking);
                            }
                        }

                        //fin de recorrido campañas

                        listaDatosVisitas.Add(objVisitaDetalle);
                    }
                }
                conex.Close();
            }

            return listaDatosVisitas;
        }

        private void RetornarEstadoVariableCampania(ref BeDatosVisitaReporte objVisitaDetalle, int itemCampana, string estadoVisita, int cantidad)
        {
            switch (itemCampana)
            {
                case 1:
                    if (objVisitaDetalle.CampanaEstado1.Length < 2)
                        objVisitaDetalle.CampanaEstado1 += estadoVisita;
                    objVisitaDetalle.CantidadEstado1 = cantidad;
                    objVisitaDetalle.EstadoCantidad1 = "(" + objVisitaDetalle.CantidadEstado1 + ")";
                    break;
                case 2:
                    if (objVisitaDetalle.CampanaEstado2.Length < 2)
                        objVisitaDetalle.CampanaEstado2 += estadoVisita;
                    objVisitaDetalle.CantidadEstado2 = cantidad;
                    objVisitaDetalle.EstadoCantidad2 = "(" + objVisitaDetalle.CantidadEstado2 + ")";
                    break;
                case 3:
                    if (objVisitaDetalle.CampanaEstado3.Length < 2)
                        objVisitaDetalle.CampanaEstado3 += estadoVisita;
                    objVisitaDetalle.CantidadEstado3 = cantidad;
                    objVisitaDetalle.EstadoCantidad3 = "(" + objVisitaDetalle.CantidadEstado3 + ")";
                    break;
                case 4:
                    if (objVisitaDetalle.CampanaEstado4.Length < 2)
                        objVisitaDetalle.CampanaEstado4 += estadoVisita;
                    objVisitaDetalle.CantidadEstado4 = cantidad;
                    objVisitaDetalle.EstadoCantidad4 = "(" + objVisitaDetalle.CantidadEstado4 + ")";
                    break;
                case 5:
                    if (objVisitaDetalle.CampanaEstado5.Length < 2)
                        objVisitaDetalle.CampanaEstado5 += estadoVisita;
                    objVisitaDetalle.CantidadEstado5 = cantidad;
                    objVisitaDetalle.EstadoCantidad5 = "(" + objVisitaDetalle.CantidadEstado5 + ")";
                    break;
                case 6:
                    if (objVisitaDetalle.CampanaEstado6.Length < 2)
                        objVisitaDetalle.CampanaEstado6 += estadoVisita;
                    objVisitaDetalle.CantidadEstado6 = cantidad;
                    objVisitaDetalle.EstadoCantidad6 = "(" + objVisitaDetalle.CantidadEstado6 + ")";
                    break;
            }
        }

        private void RetornarRankingVariableCampania(ref BeDatosVisitaReporte objVisitaDetalle, int itemCampana, int ranking)
        {
            switch (itemCampana)
            {
                case 1:
                    objVisitaDetalle.RankingCampana1 = ranking;
                    break;
                case 2:

                    objVisitaDetalle.RankingCampana2 = ranking;
                    break;
                case 3:

                    objVisitaDetalle.RankingCampana3 = ranking;
                    break;
                case 4:

                    objVisitaDetalle.RankingCampana4 = ranking;
                    break;
                case 5:

                    objVisitaDetalle.RankingCampana5 = ranking;
                    break;
                case 6:

                    objVisitaDetalle.RankingCampana6 = ranking;
                    break;
            }
        }

        #endregion MetodosReporteVisitasCampana
    }
}