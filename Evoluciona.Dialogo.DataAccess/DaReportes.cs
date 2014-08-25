
using System.Globalization;

namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaReportes : DaConexion
    {
        public string ObtenerCampanasPorAnio(string anio)
        {
            var campanas = "";
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerCampaniaxAnio", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@anio", SqlDbType.Char, 4);

                cmd.Parameters["@anio"].Value = anio;

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

        public List<BeReporteUsoTiempo> ObtenerTipoReuniones()
        {
            var dtDatos = new DataTable();
            var listTipoReuniones = new List<BeReporteUsoTiempo>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerTiposReunion", conex) {CommandType = CommandType.StoredProcedure};
                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objReunion = new BeReporteUsoTiempo
                        {
                            TipoReunion = Convert.ToInt32(drDatos["tipoReunion"]),
                            Cantidad = Convert.ToInt32(drDatos["cantidad"])
                        };
                        listTipoReuniones.Add(objReunion);
                    }
                }
                conex.Close();
            }
            return listTipoReuniones;
        }

        public int ObtenerCantidadTipoReunion(int tipoReunion, int codigoRol)
        {
            var cantidad = 0;
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerTiposReunion", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intTipoReunion", SqlDbType.Int);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);

                cmd.Parameters["@intTipoReunion"].Value = tipoReunion;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cantidad = Convert.ToInt32(dr["cantidad"]);
                }
                dr.Close();

                conex.Close();
            }
            return cantidad;
        }

        public List<BeReporteUsoTiempo> ObtenerTipoEventos()
        {
            var dtDatos = new DataTable();
            var listTipoEventos = new List<BeReporteUsoTiempo>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerTiposEventoAll", conex) {CommandType = CommandType.StoredProcedure};
                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objReunion = new BeReporteUsoTiempo
                        {
                            TipoReunion = Convert.ToInt32(drDatos["intTipoReunion"]),
                            TipoActividad = Convert.ToInt32(drDatos["intIDTipoEvento"]),
                            Actividad = drDatos["vchDescTipo"].ToString()
                        };
                        listTipoEventos.Add(objReunion);
                    }
                }
                conex.Close();
            }
            return listTipoEventos;
        }

        public List<BeReporteUsoTiempo> ObtenerReunionesCampanias(string codigoUsuario)
        {
            var dtDatos = new DataTable();
            var listReunionesCampanias = new List<BeReporteUsoTiempo>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerEventoCampania", conex) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objReunion = new BeReporteUsoTiempo
                        {
                            AnioCampana = drDatos["chrCampanha"].ToString(),
                            TipoReunion = Convert.ToInt32(drDatos["intReunion"]),
                            TipoActividad = Convert.ToInt32(drDatos["intEvento"]),
                            Cantidad = Convert.ToInt32(drDatos["cantidad"])
                        };
                        listReunionesCampanias.Add(objReunion);
                    }
                }
                conex.Close();
            }
            return listReunionesCampanias;
        }

        public List<BeReporteUsoTiempo> ObtenerTiposEventoDistinct(int codigoRol)
        {
            var dtDatos = new DataTable();
            var listTiposEventoDistinct = new List<BeReporteUsoTiempo>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerTiposEventoDistinctAll", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objReunion = new BeReporteUsoTiempo
                        {
                            TipoReunion = Convert.ToInt32(drDatos["intTipoReunion"]),
                            Actividad = drDatos["vchDescTipo"].ToString()
                        };
                        listTiposEventoDistinct.Add(objReunion);
                    }
                }
                conex.Close();
            }
            return listTiposEventoDistinct;
        }

        public List<BeReporteUsoTiempo> ObtenerTiposEventosAll()
        {
            var dtDatos = new DataTable();
            var listTiposEventosAll = new List<BeReporteUsoTiempo>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerTiposEventoAll", conex) {CommandType = CommandType.StoredProcedure};
                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objReunion = new BeReporteUsoTiempo
                        {
                            TipoReunion = Convert.ToInt32(drDatos["intTipoReunion"]),
                            TipoActividad = Convert.ToInt32(drDatos["intIDTipoEvento"]),
                            Actividad = drDatos["vchDescTipo"].ToString()
                        };
                        listTiposEventosAll.Add(objReunion);
                    }
                }
                conex.Close();
            }
            return listTiposEventosAll;
        }

        public List<BeReporteUsoTiempo> ObtenerReporteUsoTiempo(string codigoUsuario, int codigoRol)
        {
            var listaReporteUsoTiempo = new List<BeReporteUsoTiempo>();
            var listDatosReunionesCampanias = ObtenerReunionesCampanias(codigoUsuario);
            var listTiposEventoDistinct = ObtenerTiposEventoDistinct(codigoRol);
            var listTiposEventosAll = ObtenerTiposEventosAll();
            var campanasPeriodo = ObtenerCampanasPorAnio(DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));

            for (var i = 1; i < 3; i++)
            {
                var listEventosDistinct = listTiposEventoDistinct.FindAll(objTemporal => objTemporal.TipoReunion == i);
                if (listEventosDistinct.Count <= 0) continue;
                foreach (var objResumenTemp in listEventosDistinct)
                {
                    var objReporteUsoTiempo = new BeReporteUsoTiempo
                    {
                        TipoReunion = objResumenTemp.TipoReunion,
                        TipoActividad = objResumenTemp.TipoActividad,
                        Actividad = objResumenTemp.Actividad
                    };

                    var listEventosAll = listTiposEventosAll.FindAll(
                        objTemporal =>
                            objTemporal.Actividad.Trim() == objResumenTemp.Actividad.Trim() &&
                            objTemporal.TipoReunion == objResumenTemp.TipoReunion);

                    if (listEventosAll.Count <= 0) continue;
                    foreach (var objResumenTempAll in listEventosAll)
                    {
                        var campanas = campanasPeriodo.Split(',');

                        for (var j = 0; j < campanas.Length; j++)
                        {
                            var itemCampana = j + 1;

                            var listReuniones = listDatosReunionesCampanias.Find(
                                objTemporal =>
                                    objTemporal.AnioCampana == campanas[j] &&
                                    objTemporal.TipoActividad == objResumenTempAll.TipoActividad &&
                                    objTemporal.TipoReunion == objResumenTempAll.TipoReunion);

                            if (listReuniones != null)
                            {
                                RetornarCantidadesCampania(ref objReporteUsoTiempo, itemCampana, listReuniones.Cantidad, codigoUsuario);
                            }
                        }
                    }
                    listaReporteUsoTiempo.Add(objReporteUsoTiempo);
                }
            }

            return listaReporteUsoTiempo;
        }

        private void RetornarCantidadesCampania(ref BeReporteUsoTiempo objReporteUsoTiempo, int itemCampana, int cantidad, string codigoUsuario)
        {
            var listaTotales = ObtenerTotalesReuniones(codigoUsuario);
            var totalGeneral1 = listaTotales[0].TotalGeneral1;
            var totalGeneral2 = listaTotales[0].TotalGeneral2;
            var totalGeneral3 = listaTotales[0].TotalGeneral3;

            switch (itemCampana)
            {
                case 1:
                    objReporteUsoTiempo.Cantidad1 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total1 += cantidad;
                    objReporteUsoTiempo.Porcentaje1 = (totalGeneral1 == 0 ? 0 : (objReporteUsoTiempo.Total1 * 100 / totalGeneral1)) + " %";
                    break;
                case 2:
                    objReporteUsoTiempo.Cantidad2 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total1 += cantidad;
                    objReporteUsoTiempo.Porcentaje1 = (totalGeneral1 == 0 ? 0 : (objReporteUsoTiempo.Total1 * 100 / totalGeneral1)) + " %";
                    break;
                case 3:
                    objReporteUsoTiempo.Cantidad3 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total1 += cantidad;
                    objReporteUsoTiempo.Porcentaje1 = (totalGeneral1 == 0 ? 0 : (objReporteUsoTiempo.Total1 * 100 / totalGeneral1)) + " %";
                    break;
                case 4:
                    objReporteUsoTiempo.Cantidad4 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total1 += cantidad;
                    objReporteUsoTiempo.Porcentaje1 = (totalGeneral1 == 0 ? 0 : (objReporteUsoTiempo.Total1 * 100 / totalGeneral1)) + " %";
                    break;
                case 5:
                    objReporteUsoTiempo.Cantidad5 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total1 += cantidad;
                    objReporteUsoTiempo.Porcentaje1 = (totalGeneral1 == 0 ? 0 : (objReporteUsoTiempo.Total1 * 100 / totalGeneral1)) + " %";
                    break;
                case 6:
                    objReporteUsoTiempo.Cantidad6 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total1 += cantidad;
                    objReporteUsoTiempo.Porcentaje1 = (totalGeneral1 == 0 ? 0 : (objReporteUsoTiempo.Total1 * 100 / totalGeneral1)) + " %";
                    break;
                case 7:
                    objReporteUsoTiempo.Cantidad7 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total2 += cantidad;
                    objReporteUsoTiempo.Porcentaje2 = (totalGeneral2 == 0 ? 0 : (objReporteUsoTiempo.Total2 * 100 / totalGeneral2)) + " %";
                    break;
                case 8:
                    objReporteUsoTiempo.Cantidad8 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total2 += cantidad;
                    objReporteUsoTiempo.Porcentaje2 = (totalGeneral2 == 0 ? 0 : (objReporteUsoTiempo.Total2 * 100 / totalGeneral2)) + " %";
                    break;
                case 9:
                    objReporteUsoTiempo.Cantidad9 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total2 += cantidad;
                    objReporteUsoTiempo.Porcentaje2 = (totalGeneral2 == 0 ? 0 : (objReporteUsoTiempo.Total2 * 100 / totalGeneral2)) + " %";
                    break;
                case 10:
                    objReporteUsoTiempo.Cantidad10 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total2 += cantidad;
                    objReporteUsoTiempo.Porcentaje2 = (totalGeneral2 == 0 ? 0 : (objReporteUsoTiempo.Total2 * 100 / totalGeneral2)) + " %";
                    break;
                case 11:
                    objReporteUsoTiempo.Cantidad11 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total2 += cantidad;
                    objReporteUsoTiempo.Porcentaje2 = (totalGeneral2 == 0 ? 0 : (objReporteUsoTiempo.Total2 * 100 / totalGeneral2)) + " %";
                    break;
                case 12:
                    objReporteUsoTiempo.Cantidad12 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total2 += cantidad;
                    objReporteUsoTiempo.Porcentaje2 = (totalGeneral2 == 0 ? 0 : (objReporteUsoTiempo.Total2 * 100 / totalGeneral2)) + " %";
                    break;
                case 13:
                    objReporteUsoTiempo.Cantidad13 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total3 += cantidad;
                    objReporteUsoTiempo.Porcentaje3 = (totalGeneral3 == 0 ? 0 : objReporteUsoTiempo.Total3 * 100 / totalGeneral3) + " %";
                    break;
                case 14:
                    objReporteUsoTiempo.Cantidad14 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total3 += cantidad;
                    objReporteUsoTiempo.Porcentaje3 = (totalGeneral3 == 0 ? 0 : objReporteUsoTiempo.Total3 * 100 / totalGeneral3) + " %";
                    break;
                case 15:
                    objReporteUsoTiempo.Cantidad15 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total3 += cantidad;
                    objReporteUsoTiempo.Porcentaje3 = (totalGeneral3 == 0 ? 0 : objReporteUsoTiempo.Total3 * 100 / totalGeneral3) + " %";
                    break;
                case 16:
                    objReporteUsoTiempo.Cantidad16 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total3 += cantidad;
                    objReporteUsoTiempo.Porcentaje3 = (totalGeneral3 == 0 ? 0 : objReporteUsoTiempo.Total3 * 100 / totalGeneral3) + " %";
                    break;
                case 17:
                    objReporteUsoTiempo.Cantidad17 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total3 += cantidad;
                    objReporteUsoTiempo.Porcentaje3 = (totalGeneral3 == 0 ? 0 : objReporteUsoTiempo.Total3 * 100 / totalGeneral3) + " %";
                    break;
                case 18:
                    objReporteUsoTiempo.Cantidad18 += cantidad.ToString(CultureInfo.InvariantCulture);
                    objReporteUsoTiempo.Total3 += cantidad;
                    objReporteUsoTiempo.Porcentaje3 = (totalGeneral3 == 0 ? 0 : objReporteUsoTiempo.Total3 * 100 / totalGeneral3) + " %";
                    break;
            }
        }

        public List<BeReporteUsoTiempo> ObtenerCantReunionesCampania(string codigoUsuario)
        {
            var dtDatos = new DataTable();
            var listCantReuniones = new List<BeReporteUsoTiempo>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerCantReunionesxCampania", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objReunion = new BeReporteUsoTiempo
                        {
                            AnioCampana = drDatos["chrCampanha"].ToString(),
                            CantCampania = Convert.ToInt32(drDatos["cantidad"])
                        };
                        listCantReuniones.Add(objReunion);
                    }
                }
                conex.Close();
            }
            return listCantReuniones;
        }

        public List<BeReporteUsoTiempo> ObtenerTotalesReuniones(string codigoUsuario)
        {
            var listaTotales = new List<BeReporteUsoTiempo>();
            var listCantReuniones = ObtenerCantReunionesCampania(codigoUsuario);

            var campanasPeriodo = ObtenerCampanasPorAnio(DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));
            var campanas = campanasPeriodo.Split(',');
            var objReporteUsoTiempo = new BeReporteUsoTiempo();

            for (var j = 0; j < campanas.Length; j++)
            {
                var itemCampana = j + 1;

                var listReuniones = listCantReuniones.FindAll(objTemporal => objTemporal.AnioCampana == campanas[j]);

                if (listReuniones.Count > 0)
                {
                    foreach (var objResumenTemp in listReuniones)
                    {
                        RetornarTotalesCampania(ref objReporteUsoTiempo, itemCampana, objResumenTemp.CantCampania);
                    }
                }
            }
            listaTotales.Add(objReporteUsoTiempo);
            return listaTotales;
        }

        private static void RetornarTotalesCampania(ref BeReporteUsoTiempo objReporteUsoTiempo, int itemCampana, int cantidad)
        {
            switch (itemCampana)
            {
                case 1:
                    objReporteUsoTiempo.CantCampania1 = cantidad;
                    objReporteUsoTiempo.TotalGeneral1 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 2:
                    objReporteUsoTiempo.CantCampania2 = cantidad;
                    objReporteUsoTiempo.TotalGeneral1 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 3:
                    objReporteUsoTiempo.CantCampania3 = cantidad;
                    objReporteUsoTiempo.TotalGeneral1 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 4:
                    objReporteUsoTiempo.CantCampania4 = cantidad;
                    objReporteUsoTiempo.TotalGeneral1 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 5:
                    objReporteUsoTiempo.CantCampania5 = cantidad;
                    objReporteUsoTiempo.TotalGeneral1 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 6:
                    objReporteUsoTiempo.CantCampania6 = cantidad;
                    objReporteUsoTiempo.TotalGeneral1 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 7:
                    objReporteUsoTiempo.CantCampania7 = cantidad;
                    objReporteUsoTiempo.TotalGeneral2 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 8:
                    objReporteUsoTiempo.CantCampania8 = cantidad;
                    objReporteUsoTiempo.TotalGeneral2 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 9:
                    objReporteUsoTiempo.CantCampania9 = cantidad;
                    objReporteUsoTiempo.TotalGeneral2 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 10:
                    objReporteUsoTiempo.CantCampania10 = cantidad;
                    objReporteUsoTiempo.TotalGeneral2 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 11:
                    objReporteUsoTiempo.CantCampania11 = cantidad;
                    objReporteUsoTiempo.TotalGeneral2 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 12:
                    objReporteUsoTiempo.CantCampania12 = cantidad;
                    objReporteUsoTiempo.TotalGeneral2 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 13:
                    objReporteUsoTiempo.CantCampania13 = cantidad;
                    objReporteUsoTiempo.TotalGeneral3 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 14:
                    objReporteUsoTiempo.CantCampania14 = cantidad;
                    objReporteUsoTiempo.TotalGeneral3 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 15:
                    objReporteUsoTiempo.CantCampania15 = cantidad;
                    objReporteUsoTiempo.TotalGeneral3 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 16:
                    objReporteUsoTiempo.CantCampania16 = cantidad;
                    objReporteUsoTiempo.TotalGeneral3 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 17:
                    objReporteUsoTiempo.CantCampania17 = cantidad;
                    objReporteUsoTiempo.TotalGeneral3 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
                case 18:
                    objReporteUsoTiempo.CantCampania18 = cantidad;
                    objReporteUsoTiempo.TotalGeneral3 += cantidad;
                    objReporteUsoTiempo.PorcGeneral1 = "100 %";
                    objReporteUsoTiempo.PorcGeneral2 = "100 %";
                    objReporteUsoTiempo.PorcGeneral3 = "100 %";
                    break;
            }
        }

        public DataTable ObtenerReunionesCampania(string periodo, string codigoUsuario)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerReunionesCampania", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

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

        public DataTable ObtenerCantFijoVariable(string periodo, string campania, string tipo, string codigoUsuario)
        {
            var ds = new DataSet();
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_ObtenerCantFijoVariable", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCampanha", SqlDbType.Char, 6);
                cmd.Parameters.Add("@chrtipo", SqlDbType.Char, 1);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);

                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrCampanha"].Value = campania;
                cmd.Parameters["@chrtipo"].Value = tipo;
                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;

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