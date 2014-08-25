
namespace Evoluciona.Dialogo.BusinessLogic.Tareas
{
    using BusinessEntity;
    using DataAccess;
    using Helpers;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net.Mail;

    public class BLTareaAlertas
    {
        private static readonly ILog Logger = LogManager.GetLogger("Evoluciona_TareaAlertas");

        public void IniciarProcesoNotificaciones()
        {
            IniciarProcesoNotificacionesGR();
            IniciarProcesoNotificacionesGZ();
            IniciarProcesoNotificacionesLET();
        }

        private List<BeEventosAlertas> SeleccionarEventosAlertas()
        {
            var lstEventosAlertas = new List<BeEventosAlertas>();

            using (var cn = DaConexion.ObtieneConexionJob())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ObtenerEventosActual", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objEventosAlertas = new BeEventosAlertas();
                    objEventosAlertas.RolUsuario = Convert.ToInt32(dr["intIDRolUsuario"]);
                    objEventosAlertas.CodUsuario = dr["chrCodUsuario"].ToString();
                    objEventosAlertas.FechaInicio = dr["datFechaInicio"].ToString();
                    objEventosAlertas.FechaFin = dr["datFechaFin"].ToString();
                    objEventosAlertas.Asunto = dr["vchAsunto"].ToString();
                    lstEventosAlertas.Add(objEventosAlertas);
                }
                dr.Close();
            }

            return lstEventosAlertas;
        }

        private List<BeUsuario> SeleccionarGR()
        {
            var lstUsuariosGR = new List<BeUsuario>();
            
            using (var cn = DaConexion.ObtieneConexion())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ObtenerDatosGR", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario();
                    objUser.nombreUsuario = dr["vchDesGerenteRegional"].ToString();
                    objUser.correoElectronico = dr["vchCorreoElectronico"].ToString();
                    objUser.codigoUsuario = dr["chrDocIdentidad"].ToString();
                    objUser.prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString();
                    lstUsuariosGR.Add(objUser);
                }
                dr.Close();
            }
            return lstUsuariosGR;
        }

        private void IniciarProcesoNotificacionesGR()
        {
            var dtDatosGR = new DataTable();
            var listDatosEventosAlertas = SeleccionarEventosAlertas();

            using (var conex = DaConexion.ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerDatosGR", conex);

                cmd.CommandType = CommandType.StoredProcedure;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosGR);
                if (dtDatosGR.Rows.Count > 0)
                {
                    //rrecorrer los datos de la tabla principal para obtener los codigo de documentos
                    for (var i = 0; i < dtDatosGR.Rows.Count; i++)
                    {
                        var objUser = new BeUsuario();
                        objUser.nombreUsuario = dtDatosGR.Rows[i]["vchDesGerenteRegional"].ToString();
                        objUser.correoElectronico = dtDatosGR.Rows[i]["vchCorreoElectronico"].ToString();
                        objUser.codigoUsuario = dtDatosGR.Rows[i]["chrDocIdentidad"].ToString();
                        objUser.prefijoIsoPais = dtDatosGR.Rows[i]["chrPrefijoIsoPais"].ToString();

                        var listUsuariosEnvio = listDatosEventosAlertas.FindAll(delegate(BeEventosAlertas objTemporal) { return objTemporal.CodUsuario == objUser.codigoUsuario.Trim(); });

                        if (listUsuariosEnvio != null && listUsuariosEnvio.Count > 0)
                        {
                            foreach (var objResumenTemp in listUsuariosEnvio)
                            {
                                var asunto = objResumenTemp.Asunto;
                                var inicio = objResumenTemp.FechaInicio;
                                var fin = objResumenTemp.FechaFin;
                                EnviarCorreo(ref objUser, asunto, inicio, fin);
                            }
                        }
                    }
                }
                conex.Close();
            }
        }

        private void IniciarProcesoNotificacionesGZ()
        {
            var dtDatosGZ = new DataTable();
            var listDatosEventosAlertas = SeleccionarEventosAlertas();

            using (var conex = DaConexion.ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerDatosGZ", conex);

                cmd.CommandType = CommandType.StoredProcedure;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosGZ);
                if (dtDatosGZ.Rows.Count > 0)
                {
                    //rrecorrer los datos de la tabla principal para obtener los codigo de documentos
                    for (var i = 0; i < dtDatosGZ.Rows.Count; i++)
                    {
                        var objUser = new BeUsuario();
                        objUser.nombreUsuario = dtDatosGZ.Rows[i]["vchDesGerenteZona"].ToString();
                        objUser.correoElectronico = dtDatosGZ.Rows[i]["vchCorreoElectronico"].ToString();
                        objUser.codigoUsuario = dtDatosGZ.Rows[i]["chrDocIdentidad"].ToString();
                        objUser.prefijoIsoPais = dtDatosGZ.Rows[i]["chrPrefijoIsoPais"].ToString();

                        var listUsuariosEnvio = listDatosEventosAlertas.FindAll(delegate(BeEventosAlertas objTemporal) { return objTemporal.CodUsuario == objUser.codigoUsuario.Trim(); });

                        if (listUsuariosEnvio != null && listUsuariosEnvio.Count > 0)
                        {
                            foreach (var objResumenTemp in listUsuariosEnvio)
                            {
                                var asunto = objResumenTemp.Asunto;
                                var inicio = objResumenTemp.FechaInicio;
                                var fin = objResumenTemp.FechaFin;
                                EnviarCorreo(ref objUser, asunto, inicio, fin);
                            }
                        }
                    }
                }
                conex.Close();
            }
        }

        private void IniciarProcesoNotificacionesLET()
        {
            var dtDatosLET = new DataTable();
            var listDatosEventosAlertas = SeleccionarEventosAlertas();

            using (var conex = DaConexion.ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_ObtenerDatosLET", conex);

                cmd.CommandType = CommandType.StoredProcedure;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatosLET);
                if (dtDatosLET.Rows.Count > 0)
                {
                    //rrecorrer los datos de la tabla principal para obtener los codigo de documentos
                    for (var i = 0; i < dtDatosLET.Rows.Count; i++)
                    {
                        var objUser = new BeUsuario();
                        objUser.nombreUsuario = dtDatosLET.Rows[i]["vchDesNombreLET"].ToString();
                        objUser.correoElectronico = dtDatosLET.Rows[i]["vchCorreoElectronico"].ToString();
                        objUser.codigoUsuario = dtDatosLET.Rows[i]["chrCodigoConsultoraLET"].ToString();
                        objUser.prefijoIsoPais = dtDatosLET.Rows[i]["chrCodPais"].ToString();

                        var listUsuariosEnvio = listDatosEventosAlertas.FindAll(delegate(BeEventosAlertas objTemporal) { return objTemporal.CodUsuario == objUser.codigoUsuario.Trim(); });

                        if (listUsuariosEnvio != null && listUsuariosEnvio.Count > 0)
                        {
                            foreach (var objResumenTemp in listUsuariosEnvio)
                            {
                                var asunto = objResumenTemp.Asunto;
                                var inicio = objResumenTemp.FechaInicio;
                                var fin = objResumenTemp.FechaFin;
                                EnviarCorreo(ref objUser, asunto, inicio, fin);
                            }
                        }
                    }
                }
                conex.Close();
            }
        }

        private void EnviarCorreo(ref BeUsuario objUsuario, string asunto, string inicio, string fin)
        {
            var correoFrom = new MailAddress(ConfigurationAppSettings.UsuarioEnviaMails());
            var correoTO = objUsuario.correoElectronico;
            var nombre = objUsuario.nombreUsuario;
            var servidorSMTP = ConfigurationAppSettings.ServidorSmtp();
            var strHTML = string.Empty;

            var estiloTD = "font-family:Arial; font-size:13px; color:#6a288a;";
            strHTML += "<table border='0'>";
            strHTML += "<tr><td>Estimada: " + nombre + " </td></tr>";
            strHTML += "<tr><td style='" + estiloTD + "'>Asunto:" + asunto + "</td></tr>";
            strHTML += "<tr><td style='" + estiloTD + "'>Fecha Inicio: " + inicio + " </td></tr>";
            strHTML += "<tr><td style='" + estiloTD + "'>Fecha Fin: " + fin + " </td></tr>";
            strHTML += "<tr><td> </td></tr>";
            strHTML += "</table>";

            var enviar = new SmtpClient(servidorSMTP);
            try
            {
                var correoTo = new MailAddress(correoTO);
                var msjEmail = new MailMessage(correoFrom, correoTo);
                msjEmail.Subject = "Visitas";
                msjEmail.IsBodyHtml = true;
                msjEmail.Body = strHTML;
                enviar.Send(msjEmail);
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
