
using System.Linq;

namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaResumenVisita : DaConexion
    {
        public List<BeResumenVisita> BuscarResumenProcesoVisitaGr(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            var listResumenVisita = new List<BeResumenVisita>();
            var dtDatosProceso = new DataTable();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Buscar_ResumenProceso_Visita_GR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = Constantes.EstadoProcesoCulminado;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                    {
                        var drProceso = dtDatosProceso.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            codigoUsuario = drProceso["codigoUsuario"].ToString(),
                            nombreEvaluado = drProceso["Nombres"].ToString(),
                            idProceso = Convert.ToInt32(drProceso["intIDProceso"])
                        };

                        //calculamos cantidad de visitas
                        var esPostVisita = false;
                        var cantidadVisitas = 0;
                        var cantidadVisitasCerradas = 0;
                        var listaVisitasProcesadas = ObtenerVisitasPorEvaluador(codigoUsuarioEvaluador, idRol, periodo);
                        var listVisitas = listaVisitasProcesadas.FindAll(
                            objTemporal =>
                                objTemporal.codigoUsuario == objVisita.codigoUsuario &&
                                (objTemporal.estadoVisita == Constantes.EstadoVisitaActivo ||
                                 objTemporal.estadoVisita == Constantes.EstadoVisitaCerrado));
                        if (listVisitas.Count > 0)
                        {
                            foreach (var objResumenTemp in listVisitas)
                            {
                                cantidadVisitas = 1 + cantidadVisitas;

                                if (objResumenTemp.estadoVisita == Constantes.EstadoVisitaCerrado)
                                {
                                    cantidadVisitasCerradas = cantidadVisitasCerradas + 1;
                                }
                            }
                            objVisita.estadoVisita = cantidadVisitas > cantidadVisitasCerradas ? Constantes.EstadoVisitaActivo : Constantes.EstadoVisitaCerrado;
                        }
                        objVisita.cantVisitasIniciadas = cantidadVisitas;
                        objVisita.cantidadVisitasCerradas = cantidadVisitasCerradas;

                        var listVisitasPostVisita = listaVisitasProcesadas.FindAll(
                            objTemporal =>
                                objTemporal.codigoUsuario == objVisita.codigoUsuario &&
                                (objTemporal.estadoVisita == Constantes.EstadoVisitaPostDialogo));
                        if (listVisitasPostVisita.Count > 0)
                        {
                            esPostVisita = true;
                        }
                        if (!esPostVisita)
                        {
                            listResumenVisita.Add(objVisita);
                        }
                    }
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public List<BeResumenVisita> ObtenerVisitasPorEvaluador(string codigoEvaluador, int idRol, string periodo)
        {
            var dtDatos = new DataTable();
            var listResumenVisita = new List<BeResumenVisita>();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Seleccionar_Visitas_PorEvaluador", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            idVisita = Convert.ToInt32(drDatos["intIDVisita"]),
                            codigoUsuario = drDatos["chrCodigoUsuario"].ToString(),
                            idProceso = Convert.ToInt32(drDatos["intIDProceso"]),
                            estadoVisita = drDatos["chrEstadoVisita"].ToString(),
                            prefijoIsoPais = drDatos["chrPrefijoIsoPais"].ToString(),
                            cantVisitasIniciadas = Convert.ToInt32(drDatos["intCantVisitasIniciadas"]),
                            cantidadVisitasCerradas = Convert.ToInt32(drDatos["intCantidadVisitasCerradas"]),
                            fechaPostVisita =
                                drDatos["datFechaPostVisita"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(drDatos["datFechaPostVisita"])
                        };
                        listResumenVisita.Add(objVisita);
                    }
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public List<BeResumenVisita> BuscarResumenProcesoVisitaGz(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            var listResumenVisita = new List<BeResumenVisita>();
            var dtDatosProceso = new DataTable();
            var listaVisitasProcesadas = ObtenerResumenVisita(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo);
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Buscar_ResumenProceso_Visita_GZ", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = Constantes.EstadoProcesoCulminado;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                    {
                        var esPostVisita = false;
                        var drProceso = dtDatosProceso.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            codigoUsuario = drProceso["codigoUsuario"].ToString(),
                            nombreEvaluado = drProceso["Nombres"].ToString(),
                            idProceso = Convert.ToInt32(drProceso["intIDProceso"])
                        };

                        //calculamos cantidad de visitas
                        var cantidadVisitas = 0;
                        var cantidadVisitasCerradas = 0;
                        var listVisitas = listaVisitasProcesadas.FindAll(
                            objTemporal =>
                                objTemporal.codigoUsuario == objVisita.codigoUsuario &&
                                (objTemporal.estadoVisita == Constantes.EstadoVisitaActivo ||
                                 objTemporal.estadoVisita == Constantes.EstadoVisitaCerrado));
                        if (listVisitas.Count > 0)
                        {
                            foreach (var objResumenTemp in listVisitas)
                            {
                                cantidadVisitas = 1 + cantidadVisitas;

                                if (objResumenTemp.estadoVisita == Constantes.EstadoVisitaCerrado)
                                {
                                    cantidadVisitasCerradas = cantidadVisitasCerradas + 1;
                                }
                            }
                            objVisita.estadoVisita = cantidadVisitas > cantidadVisitasCerradas ? Constantes.EstadoVisitaActivo : Constantes.EstadoVisitaCerrado;
                        }
                        var listVisitasPostVisita = listaVisitasProcesadas.FindAll(
                            objTemporal =>
                                objTemporal.codigoUsuario == objVisita.codigoUsuario &&
                                (objTemporal.estadoVisita == Constantes.EstadoVisitaPostDialogo));
                        if (listVisitasPostVisita.Count > 0)
                        {
                            esPostVisita = true;
                        }
                        objVisita.cantVisitasIniciadas = cantidadVisitas;
                        objVisita.cantidadVisitasCerradas = cantidadVisitasCerradas;
                        if (!esPostVisita)
                        {
                            listResumenVisita.Add(objVisita);
                        }
                    }
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public List<BeResumenVisita> BuscarPostVisitaGr(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            var listResumenVisita = new List<BeResumenVisita>();
            var dtDatosProceso = new DataTable();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Buscar_PostVisita_GR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoVisita", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoVisita"].Value = Constantes.EstadoVisitaPostDialogo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                    {
                        var drProceso = dtDatosProceso.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            codigoUsuario = drProceso["codigoUsuario"].ToString(),
                            nombreEvaluado = drProceso["Nombres"].ToString(),
                            idProceso = Convert.ToInt32(drProceso["intIDProceso"])
                        };

                        //calculamos cantidad de visitas
                        var listaVisitasProcesadas = ObtenerVisitasPorEvaluador(codigoUsuarioEvaluador, idRol, periodo);
                        var listVisitas = listaVisitasProcesadas.FindAll(
                            objTemporal => objTemporal.codigoUsuario == objVisita.codigoUsuario);

                        var cantidadVisitas = listVisitas.Count();

                        objVisita.cantVisitasIniciadas = cantidadVisitas;
                        listResumenVisita.Add(objVisita);
                    }
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public List<BeResumenVisita> BuscarPostVisitaGz(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            var listResumenVisita = new List<BeResumenVisita>();
            var dtDatosProceso = new DataTable();
            var listaVisitasProcesadas = ObtenerResumenVisita(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo);
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Buscar_PostVisita_GZ", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoVisita", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoVisita"].Value = Constantes.EstadoVisitaPostDialogo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                    {
                        var drProceso = dtDatosProceso.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            codigoUsuario = drProceso["codigoUsuario"].ToString(),
                            nombreEvaluado = drProceso["Nombres"].ToString(),
                            idProceso = Convert.ToInt32(drProceso["intIDProceso"])
                        };

                        //calculamos cantidad de visitas
                        var listVisitas = listaVisitasProcesadas.FindAll(
                            objTemporal => objTemporal.codigoUsuario == objVisita.codigoUsuario);

                        var cantidadVisitas = listVisitas.Count();

                        objVisita.cantVisitasIniciadas = cantidadVisitas;
                        listResumenVisita.Add(objVisita);
                    }
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public List<BeResumenVisita> BuscarVisitaGr(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            var listResumenVisita = new List<BeResumenVisita>();
            var dtDatosProceso = new DataTable();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Buscar_Visita_GR", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count <= 0) return listResumenVisita;
                for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                {
                    var drProceso = dtDatosProceso.Rows[i];
                    var objVisita = new BeResumenVisita
                    {
                        codigoUsuario = drProceso["codigoUsuario"].ToString(),
                        nombreEvaluado = drProceso["Nombres"].ToString(),
                        idProceso = Convert.ToInt32(drProceso["intIDProceso"])
                    };

                    //calculamos cantidad de visitas
                    var cantidadVisitas = 0;
                    var cantidadVisitasCerradas = 0;
                    var listaVisitasProcesadas = ObtenerVisitasPorEvaluador(codigoUsuarioEvaluador, idRol, periodo);
                    var listVisitas = listaVisitasProcesadas.FindAll(
                        objTemporal => objTemporal.codigoUsuario == objVisita.codigoUsuario);

                    foreach (var objResumenTemp in listVisitas)
                    {
                        cantidadVisitas = 1 + cantidadVisitas;
                        if (objResumenTemp.estadoVisita == Constantes.EstadoVisitaCerrado)
                        {
                            cantidadVisitasCerradas = cantidadVisitasCerradas + 1;
                        }
                    }

                    objVisita.cantVisitasIniciadas = cantidadVisitas;
                    objVisita.cantidadVisitasCerradas = cantidadVisitasCerradas;
                    listResumenVisita.Add(objVisita);
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public List<BeResumenVisita> BuscarVisitaGz(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            var listResumenVisita = new List<BeResumenVisita>();
            var dtDatosProceso = new DataTable();
            var listaVisitasProcesadas = ObtenerResumenVisita(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo);
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Buscar_Visita_GZ", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosProceso);
                if (dtDatosProceso.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatosProceso.Rows.Count; i++)
                    {
                        var drProceso = dtDatosProceso.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            codigoUsuario = drProceso["codigoUsuario"].ToString(),
                            nombreEvaluado = drProceso["Nombres"].ToString(),
                            idProceso = Convert.ToInt32(drProceso["intIDProceso"])
                        };

                        //calculamos cantidad de visitas
                        var cantidadVisitas = 0;
                        var cantidadVisitasCerradas = 0;
                        var listVisitas = listaVisitasProcesadas.FindAll(
                            objTemporal => objTemporal.codigoUsuario == objVisita.codigoUsuario);
                        foreach (var objResumenTemp in listVisitas)
                        {
                            cantidadVisitas = 1 + cantidadVisitas;
                            if (objResumenTemp.estadoVisita == Constantes.EstadoVisitaCerrado)
                            {
                                cantidadVisitasCerradas = cantidadVisitasCerradas + 1;
                            }
                        }
                        objVisita.cantVisitasIniciadas = cantidadVisitas;
                        objVisita.cantidadVisitasCerradas = cantidadVisitasCerradas;
                        listResumenVisita.Add(objVisita);
                    }
                }
                conex.Close();
            }

            return listResumenVisita;
        }

        public List<BeResumenVisita> ObtenerResumenVisita(string codigoEvaluador, int idRol, string prefijoIsoPais, string periodo)
        {
            var dtDatos = new DataTable();
            var listResumenVisita = new List<BeResumenVisita>();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Seleccionar_ResumenVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 10);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
                if (dtDatos.Rows.Count > 0)
                {
                    for (var i = 0; i < dtDatos.Rows.Count; i++)
                    {
                        var drDatos = dtDatos.Rows[i];
                        var objVisita = new BeResumenVisita
                        {
                            idVisita = Convert.ToInt32(drDatos["intIDVisita"]),
                            codigoUsuario = drDatos["chrCodigoUsuario"].ToString(),
                            idProceso = Convert.ToInt32(drDatos["intIDProceso"]),
                            estadoVisita = drDatos["chrEstadoVisita"].ToString(),
                            cantVisitasIniciadas = Convert.ToInt32(drDatos["intCantVisitasIniciadas"]),
                            cantidadVisitasCerradas = Convert.ToInt32(drDatos["intCantidadVisitasCerradas"]),
                            fechaPostVisita =
                                drDatos["datFechaPostVisita"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(drDatos["datFechaPostVisita"])
                        };
                        listResumenVisita.Add(objVisita);
                    }
                }
                conex.Close();
            }
            return listResumenVisita;
        }

        public int ObtenerCorrelativoVisita(string codigoUsuario, int idRol, string prefijoIsoPais, string periodo)
        {
            var cantidadVisitas = 0;

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_CorrelativoVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cantidadVisitas = dr["cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(dr["cantidad"]);
                }
                dr.Close();
                conex.Close();
            }
            return cantidadVisitas;
        }

        public int CrearVisita(BeResumenVisita objVisita)
        {
            //ESE_VS_Insertar_ProcesoVisita
            var idProcesoVisita = 0;

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Insertar_ProcesoVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIdRolEvaluador", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrEstadoVisita", SqlDbType.Char, 1);
                cmd.Parameters.Add("@intCantVisitasIniciadas", SqlDbType.Int);
                cmd.Parameters.Add("@intCantidadVisitasCerradas", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@datFechaPostVisita", SqlDbType.DateTime);

                cmd.Parameters["@intIDRol"].Value = objVisita.idRolUsuario;
                cmd.Parameters["@chrCodigoUsuario"].Value = objVisita.codigoUsuario;
                cmd.Parameters["@chrPeriodo"].Value = objVisita.periodo;
                cmd.Parameters["@chrAnioCampana"].Value = objVisita.campania;
                cmd.Parameters["@intIDProceso"].Value = objVisita.idProceso;
                cmd.Parameters["@chrCodigoEvaluador"].Value = objVisita.codigoUsuarioEvaluador;
                cmd.Parameters["@intIdRolEvaluador"].Value = objVisita.idRolUsuarioEvaluador;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objVisita.prefijoIsoPais;
                cmd.Parameters["@chrEstadoVisita"].Value = objVisita.estadoVisita;
                cmd.Parameters["@intCantVisitasIniciadas"].Value = 1;
                cmd.Parameters["@intCantidadVisitasCerradas"].Value = 0;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@datFechaPostVisita"].Value = objVisita.fechaPostVisita;

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    idProcesoVisita = Convert.ToInt32(dr.GetValue(0));
                }
                dr.Close();
                conex.Close();
            }
            return idProcesoVisita;
        }

        public DataTable ObtenerCodigoVisita(string codigoUsuario, string codigoEvaluador, int idRol, string periodo)
        {
            var dt = new DataTable();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_CodigoVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                var ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                conex.Close();
            }
            return dt;
        }

        public BeResumenVisita ObtenerVisitaGr(string codigoUsuario, int idVisita, string prefijoIsoPais, string periodo)
        {
            var objVisita = new BeResumenVisita();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_Visita_GR", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@intIDVisita"].Value = idVisita;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    objVisita.codigoUsuario = codigoUsuario;
                    objVisita.nombreEvaluado = dr["nombres"].ToString();
                    objVisita.idRolUsuario = Convert.ToInt32(dr["intIDRol"]);
                    objVisita.email = dr["email"].ToString();
                    objVisita.codigoUsuarioEvaluador = dr["codigoUsuarioEvaluador"].ToString();
                    objVisita.idRolUsuarioEvaluador = Convert.ToInt32(dr["intIdRolEvaluador"]);
                    objVisita.campania = dr["chrAnioCampana"].ToString();
                    objVisita.estadoVisita = dr["chrEstadoVisita"].ToString();
                    objVisita.fechaPostVisita = dr["datFechaPostVisita"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["datFechaPostVisita"]);
                    objVisita.idProceso = Convert.ToInt32(dr["intIDProceso"]);
                    objVisita.porcentajeAvanceAntes = Convert.ToInt32(dr["intPorcentajeAvanceAntes"]);
                    objVisita.porcentajeAvanceDurante = Convert.ToInt32(dr["intPorcentajeAvanceDurante"]);
                    objVisita.porcentajeAvanceDespues = Convert.ToInt32(dr["intPorcentajeAvanceDespues"]);
                    objVisita.prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString();
                    objVisita.cub = dr["cub"].ToString();
                }
                dr.Close();
                conex.Close();
            }
            //ESE_VS_Obtener_Visita_GR
            return objVisita;
        }

        public BeResumenVisita ObtenerVisitaGz(string codigoUsuario, int idVisita, string prefijoIsoPais, string periodo)
        {
            var objVisita = new BeResumenVisita();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_Visita_GZ", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@intIDVisita"].Value = idVisita;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    objVisita.codigoUsuario = codigoUsuario;
                    objVisita.nombreEvaluado = dr["nombres"].ToString();
                    objVisita.idRolUsuario = Convert.ToInt32(dr["intIDRol"]);
                    objVisita.email = dr["email"].ToString();
                    objVisita.codigoUsuarioEvaluador = dr["codigoUsuarioEvaluador"].ToString();
                    objVisita.idRolUsuarioEvaluador = Convert.ToInt32(dr["intIdRolEvaluador"]);
                    objVisita.campania = dr["chrAnioCampana"].ToString();
                    objVisita.estadoVisita = dr["chrEstadoVisita"].ToString();
                    objVisita.fechaPostVisita = dr["datFechaPostVisita"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["datFechaPostVisita"]);
                    objVisita.idProceso = Convert.ToInt32(dr["intIDProceso"]);
                    objVisita.porcentajeAvanceAntes = Convert.ToInt32(dr["intPorcentajeAvanceAntes"]);
                    objVisita.porcentajeAvanceDurante = Convert.ToInt32(dr["intPorcentajeAvanceDurante"]);
                    objVisita.porcentajeAvanceDespues = Convert.ToInt32(dr["intPorcentajeAvanceDespues"]);
                    objVisita.prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString();
                    objVisita.cub = dr["cub"].ToString();
                }
                dr.Close();
                conex.Close();
            }

            return objVisita;
        }

        public DataTable SeleccionarVisitaUsuario(string codigoUsuario, int idRol, string prefijoIsoPais, string periodo)
        {
            var dtVisita = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Seleccionar_Visita_Usuario", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var ad = new SqlDataAdapter(cmd);
                ad.Fill(dtVisita);
                conex.Close();
            }
            return dtVisita;
        }

        public void IniciarPostVisita(int idVisita)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Actualizar_EstadoPostVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);

                cmd.Parameters["@intIDVisita"].Value = idVisita;

                cmd.ExecuteNonQuery();
                conex.Close();
            }
        }

        public void ActualizarEstadoVisita(int idVisita, string estadoVisita)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Actualizar_EstadoVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@chrEstadoVisita", SqlDbType.Char, 1);
                cmd.Parameters["@intIDVisita"].Value = idVisita;
                cmd.Parameters["@chrEstadoVisita"].Value = estadoVisita;

                cmd.ExecuteNonQuery();
                conex.Close();
            }
        }

        public void ActualizarAvanceVisita(int idVisita, int porcentajeAvance, int areaAvance)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Actualizar_AvanceVisita", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intPorcentajeAvance", SqlDbType.Int);
                cmd.Parameters.Add("@areaAvance", SqlDbType.Int);
                cmd.Parameters["@intIDVisita"].Value = idVisita;
                cmd.Parameters["@intPorcentajeAvance"].Value = porcentajeAvance;
                cmd.Parameters["@areaAvance"].Value = areaAvance;

                cmd.ExecuteNonQuery();
                conex.Close();
            }
        }

        public DataTable ObtenerPeriodoVisitasByUsuario(string codigoUsuario, string prefijoIsoPais)
        {
            var dtPeriodos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_VS_Obtener_PeriodosVisita_ByUsuario", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;

                var ad = new SqlDataAdapter(cmd);
                ad.Fill(dtPeriodos);
                conex.Close();
            }
            return dtPeriodos;
        }

        public List<BeResumenVisita> ListarVisitas(string codigoEvaluador, string codigoEvaluado, string pais, string periodo)
        {
            var listaVisitas = new List<BeResumenVisita>();

            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Listar_Visitas", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodigoEvaluado", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);

                cmd.Parameters["@chrCodigoEvaluador"].Value = codigoEvaluador;
                cmd.Parameters["@chrCodigoEvaluado"].Value = codigoEvaluado;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = pais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;

                try
                {
                    conex.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var objVisita = new BeResumenVisita
                            {
                                campania = dr["chrAnioCampana"].ToString(),
                                cantVisitasIniciadas = Convert.ToInt32(dr["intCantVisitasIniciadas"]),
                                cantidadVisitasCerradas = Convert.ToInt32(dr["intCantidadVisitasCerradas"]),
                                idRolUsuario = Convert.ToInt32(dr["intIDRol"]),
                                codigoUsuario = dr["chrCodigoUsuario"].ToString(),
                                idRolUsuarioEvaluador = Convert.ToInt32(dr["intIdRolEvaluador"]),
                                codigoUsuarioEvaluador = dr["chrCodigoEvaluador"].ToString(),
                                estadoVisita = dr["chrEstadoVisita"].ToString(),
                                fechaPostVisita =
                                    dr["datFechaPostVisita"] == DBNull.Value
                                        ? DateTime.Now
                                        : Convert.ToDateTime(dr["datFechaPostVisita"]),
                                idProceso = Convert.ToInt32(dr["intIDProceso"]),
                                idVisita = Convert.ToInt32(dr["intIDVisita"]),
                                periodo = dr["chrPeriodo"].ToString(),
                                prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString()
                            };

                            listaVisitas.Add(objVisita);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conex.State == ConnectionState.Open)
                    {
                        conex.Close();
                    }
                }
            }

            return listaVisitas;
        }

        public List<BeResumenVisita> ListarVisitasPorProceso(int idProceso)
        {
            var listaVisitas = new List<BeResumenVisita>();

            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Listar_VisitasPorProceso", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdProceso", SqlDbType.Int);

                cmd.Parameters["@intIdProceso"].Value = idProceso;

                try
                {
                    conex.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var objVisita = new BeResumenVisita
                            {
                                campania = dr["chrAnioCampana"].ToString(),
                                cantVisitasIniciadas = Convert.ToInt32(dr["intCantVisitasIniciadas"]),
                                cantidadVisitasCerradas = Convert.ToInt32(dr["intCantidadVisitasCerradas"]),
                                idRolUsuario = Convert.ToInt32(dr["intIDRol"]),
                                codigoUsuario = dr["chrCodigoUsuario"].ToString(),
                                idRolUsuarioEvaluador = Convert.ToInt32(dr["intIdRolEvaluador"]),
                                codigoUsuarioEvaluador = dr["chrCodigoEvaluador"].ToString(),
                                estadoVisita = dr["chrEstadoVisita"].ToString(),
                                fechaPostVisita =
                                    dr["datFechaPostVisita"] == DBNull.Value
                                        ? DateTime.Now
                                        : Convert.ToDateTime(dr["datFechaPostVisita"]),
                                idProceso = Convert.ToInt32(dr["intIDProceso"]),
                                idVisita = Convert.ToInt32(dr["intIDVisita"]),
                                periodo = dr["chrPeriodo"].ToString(),
                                prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString()
                            };

                            listaVisitas.Add(objVisita);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conex.State == ConnectionState.Open)
                    {
                        conex.Close();
                    }
                }
            }

            return listaVisitas;
        }
    }
}