
namespace Evoluciona.Dialogo.BusinessLogic.Tareas
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net.Mail;

    public class BLTareaEnviarCorreos
    {
        private static readonly ILog Logger = LogManager.GetLogger("Evoluciona_TareaEnviarCorreos");

        public void IniciarProcesoEnviarCorreos()
        {
            var objDataMart = new BlDataMart();
            objDataMart.InsertarLogCarga("TareaEnviarCorreos-Execute", "Inicio de tarea TareaEnviarCorreos a las Evaluadoras");
            IniciarProcesoNotificacionDirectoraVenta();
        }

        private void IniciarProcesoNotificacionDirectoraVenta()
        {
            var objDataMart = new BlDataMart();
            objDataMart.InsertarLogCarga("IniciarProcesoNotificacionDirectoraVenta", "Inicio de tarea TareaEnviarCorreos a las DV");
            var objBLConfig = new BlConfiguracion();
            var dtPaises = objBLConfig.SeleccionarPaisesProcesados();
            if (dtPaises != null)
            {
                if (dtPaises.Rows.Count > 0)
                {
                    var lstUsuariosDV = objBLConfig.SeleccionarDv();
                    if (lstUsuariosDV.Count > 0)
                    {
                        var idRol = ObtenerIDRol(Constantes.RolGerenteRegion);
                        for (var x = 0; x < dtPaises.Rows.Count; x++)
                        {
                            //chrPrefijoIsoPais, I.chrPeriodo
                            var lstUsuariosDVPorPais = lstUsuariosDV.FindAll(delegate(BeUsuario objUsuarioFind) { return objUsuarioFind.prefijoIsoPais == dtPaises.Rows[x]["chrPrefijoIsoPais"].ToString(); });
                            if (lstUsuariosDVPorPais.Count > 0)
                            {
                                foreach (var objUsuarioDV in lstUsuariosDVPorPais)
                                {
                                    var lstUsuariosGR = objBLConfig.SeleccionarGRegionProcesadasPorDv(objUsuarioDV.codigoUsuario, idRol, objUsuarioDV.prefijoIsoPais, dtPaises.Rows[x]["chrPeriodo"].ToString());
                                    if (lstUsuariosGR.Count > 0)
                                    {
                                        var codigoRol = Constantes.RolGerenteRegion;
                                        var cantDialogosAprobados = 0;
                                        var lstUsuariosGRProcesados = lstUsuariosGR.FindAll(delegate(BeUsuario objUsuarioGRPFind) { return objUsuarioGRPFind.estadoProceso == Constantes.EstadoProcesoRevision; });
                                        var lstUsuariosGRProcesoAprobado = lstUsuariosGR.FindAll(delegate(BeUsuario objUsuarioGRPFind) { return objUsuarioGRPFind.estadoProceso == Constantes.EstadoProcesoCulminado; });
                                        if (lstUsuariosGRProcesoAprobado != null)
                                        {
                                            cantDialogosAprobados = lstUsuariosGRProcesoAprobado.Count;
                                        }
                                        //Enviar el email
                                        EnviarCorreo(objUsuarioDV.correoElectronico, codigoRol, cantDialogosAprobados, lstUsuariosGRProcesados);
                                    }
                                }
                            }
                        }
                    }

                    //enviar las notificaciones a las gr
                    objDataMart.InsertarLogCarga("IniciarProcesoNotificacionGerenteRegion", "Inicio de tarea TareaEnviarCorreos a las GR");
                    IniciarProcesoNotificacionGerenteRegion(dtPaises);
                }
            }

        }

        private void IniciarProcesoNotificacionGerenteRegion(DataTable dtPaises)
        {
            //blConfiguracion objBLConfig = new blConfiguracion();

            //List<beUsuario> lstUsuariosGR = objBLConfig.SeleccionarGRegion();
            //if (lstUsuariosGR.Count > 0)
            //{
            //    int idRol = ObtenerIDRol(constantes.rolGerenteZona);
            //    for (int x = 0; x < dtPaises.Rows.Count; x++)
            //    {
            //        //chrPrefijoIsoPais, I.chrPeriodo
            //        List<beUsuario> lstUsuariosGRPorPais = lstUsuariosGR.FindAll(delegate(beUsuario objUsuarioFind) { return objUsuarioFind.prefijoIsoPais == dtPaises.Rows[x]["chrPrefijoIsoPais"].ToString(); });

            //        if (lstUsuariosGRPorPais.Count > 0)
            //        {

            //            List<beUsuario> lstUsuariosGZPais = objBLConfig.SeleccionarGZonaProcesadasPorGR(idRol, dtPaises.Rows[x]["chrPrefijoIsoPais"].ToString(), dtPaises.Rows[x]["chrPeriodo"].ToString());
            //            foreach (beUsuario objUsuarioDV in lstUsuariosGRPorPais)
            //            {

            //                List<beUsuario> lstUsuariosGZ = lstUsuariosGZPais.FindAll(delegate(beUsuario objUsuarioFind) { return objUsuarioFind.idUsuario == objUsuarioDV.idUsuario; });
            //                if (lstUsuariosGZ.Count > 0)
            //                {
            //                    int codigoRol = constantes.rolGerenteZona;
            //                    int cantDialogosAprobados = 0;
            //                    List<beUsuario> lstUsuariosGZProcesados = lstUsuariosGZ.FindAll(delegate(beUsuario objUsuarioGRPFind) { return objUsuarioGRPFind.estadoProceso == constantes.estadoProcesoRevision; });
            //                    List<beUsuario> lstUsuariosGZProcesoAprobado = lstUsuariosGZ.FindAll(delegate(beUsuario objUsuarioGRPFind) { return objUsuarioGRPFind.estadoProceso == constantes.estadoProcesoCulminado; });
            //                    if (lstUsuariosGZProcesoAprobado != null)
            //                    {
            //                        cantDialogosAprobados = lstUsuariosGZProcesoAprobado.Count;
            //                    }
            //                    //Enviar el email
            //                    EnviarCorreo(objUsuarioDV.correoElectronico, codigoRol, cantDialogosAprobados, lstUsuariosGZProcesados);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private int ObtenerIDRol(int codigoRolEvaluado)
        {
            var idRol = 0;
            var objRol = new BlUsuario();
            var dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
            }
            return idRol;
        }

        private void EnviarCorreo(string correoEvaluador, int codigoRol, int cantDialogosAprobados, List<BeUsuario> lstUsuariosProcesados)
        {

            var correoFrom = new MailAddress(ConfigurationAppSettings.UsuarioEnviaMails());
            var descripcionRol = "";
            if (Constantes.RolGerenteRegion == codigoRol)
            {
                descripcionRol = " Gerentes de Región ";
            }
            else if (Constantes.RolGerenteZona == codigoRol)
            {
                descripcionRol = " Gerentes de Zona ";
            }
            var servidorSMTP = ConfigurationAppSettings.ServidorSmtp();
            var strHTML = string.Empty;
            var estiloTD = "font-family:Arial; font-size:13px; color:#6a288a;";
            strHTML += "<table align='center' border='0'>";
            strHTML += "<tr><td style='" + estiloTD + "'>Te confirmamos que a la fecha " + cantDialogosAprobados.ToString() + descripcionRol + " de tu equipo han cerrado su Diálogo evoluciona.</td></tr>";
            strHTML += "<tr><td> </td></tr>";

            if (lstUsuariosProcesados != null && lstUsuariosProcesados.Count > 0)
            {
                strHTML += "<tr><td style='" + estiloTD + "'>Quedan pendientes:</td></tr>";
                var contador = 1;
                foreach (var objUsuario in lstUsuariosProcesados)
                {
                    strHTML += "<tr><td style='" + estiloTD + "'>" + contador.ToString() + ". " + objUsuario.nombreUsuario + "</td></tr>";
                    contador = contador + 1;
                }
                strHTML += "<tr><td></td></tr>";
            }
            strHTML += "<tr><td style='" + estiloTD + "'><b>Recuerda realizar el seguimiento a los planes acordados para asegurarte que se <br>lleven a cabo.</b></td></tr>";

            strHTML += "<tr><td style='" + estiloTD + "'>Gracias por participar!</td></tr>";

            strHTML += "</table>";
            var enviar = new SmtpClient(servidorSMTP);
            try
            {
                var correoTo = new MailAddress(correoEvaluador);
                //MailAddress correoTo = new MailAddress(correoEvaluador);
                var msjEmail = new MailMessage(correoFrom, correoTo);
                msjEmail.Subject = "Cierre del Diálogo Evoluciona de tu equipo.";
                msjEmail.IsBodyHtml = true;
                msjEmail.Body = strHTML;
                enviar.Send(msjEmail);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
