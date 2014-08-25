
namespace Evoluciona.Dialogo.BusinessLogic.Tareas
{
    using BusinessEntity;
    using Helpers;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public class BLTareaCargaData
    {
        private static readonly ILog Logger = LogManager.GetLogger("Evoluciona_TareaCargaData");

        public void IniciarProcesoCargaData()
        {
            var objDataMart = new BlDataMart();
            objDataMart.InsertarLogCarga("TareaCargarData-Execute", "Inicio de tarea IniciarCargaDataDesdeDataMart a las tablas IN");
            IniciarCargaDataDesdeDataMart();
            objDataMart.InsertarLogCarga("TareaCargarData-Execute", "Inicio de tarea IniciarCargaLocal a las tablas trx");
            IniciarCargaLocal();
        }

        private void IniciarCargaDataDesdeDataMart()
        {
            var blObtenerData = new BlDataMart();
            try
            {
                var listaPaises = blObtenerData.ObtenerListaPaises();
                blObtenerData.ObtenerDataMart(listaPaises);
            }
            catch (Exception ex)
            {
                blObtenerData.InsertarLogCarga("IniciarCargaDataDesdeDataMart", ex.Message);
            }
        }

        private void IniciarCargaLocal()
        {
            var blObtenerData = new BlDataMart();
            var resultado = blObtenerData.InsertarTablasTRX();
            if (resultado.Trim() == "")
            {
                resultado = "Te confirmamos que a la fecha: " + DateTime.Now.ToString() + " se ha procesado la carga de información en el proceso de Gestión.";
            }
            else
            {
                resultado = "Te confirmamos que a la fecha: " + DateTime.Now.ToString() + " se han obtenido estos errores: <br>" + resultado;
            }
            var objDataMart = new BlDataMart();
            objDataMart.InsertarLogCarga("Fin de TareaCargarData", resultado);
            EnviarCorreo(resultado);
        }

        private void EnviarCorreo(string resultado)
        {
            var correoFrom = new MailAddress(ConfigurationAppSettings.UsuarioEnviaMails());
            var servidorSMTP = ConfigurationAppSettings.ServidorSmtp(); ;
            var strHTML = string.Empty;
            var estiloTD = "font-family:Arial; font-size:13px; color:#6a288a;";
            strHTML += "<table align='center' border='0'>";
            strHTML += "<tr><td style='" + estiloTD + "'>" + resultado + "</td></tr>";
            strHTML += "<tr><td> </td></tr>";
            strHTML += "</table>";
            var enviar = new SmtpClient(servidorSMTP);
            try
            {
                var correoTo = new MailAddress(ConfigurationAppSettings.UsuarioSoporte());
                var msjEmail = new MailMessage(correoFrom, correoTo);
                msjEmail.Subject = "Carga de Información Procesos Gestion.";
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
