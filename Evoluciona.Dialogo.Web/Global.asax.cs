using Evoluciona.Dialogo.BusinessLogic;
using Evoluciona.Dialogo.Web.Helper;
using System;
using System.Configuration;
using System.Web;

namespace Evoluciona.Dialogo.Web
{
    public class Global : HttpApplication
    {
        readonly BlDataMart _objDataMart = new BlDataMart();

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                _objDataMart.InsertarLogCarga("Application_Start", "Inicio de asignación del Job");

                var jobCore = new QuartzJobCore();

                //01 trabajo enviar correos
                //jobCore.AddCronTrigger<TareaEnviarCorreos>(ConfigurationManager.AppSettings["frecJobEnviarCorreos"],
                //    "triggerEnviarCorreo", "triggerEnviarCorreo", "TareaEnviarCorreos", "TareaEnviarCorreos");

                //02 trabajo cargar informacion
                jobCore.AddCronTrigger<TareaCargarData>(ConfigurationManager.AppSettings["frecJobCargarData"],
                    "triggerCargarData", "triggerCargarData", "TareaCargarData", "TareaCargarData");


                //03 trabajo cargar Competencias
                jobCore.AddCronTrigger<TareaCompetencias>(ConfigurationManager.AppSettings["frecJobCompetencias"],
                    "triggerTareaCompetencias", "triggerTareaCompetencias", "TareaCompetencias", "TareaCompetencias");

                //04 trabajo cargar directorio

                jobCore.AddCronTrigger<TareaCargarDirectorio>(ConfigurationManager.AppSettings["frecJobCargaDirectorio"],
                    "triggerCargarDirectorio", "triggerCargarDirectorio", "TareaCargarDirectorio","TareaCargarDirectorio");

                jobCore.Start();

                _objDataMart.InsertarLogCarga("Application_Start", "Fin de asignación del Job");

            }
            catch (Exception ex)
            {
                _objDataMart.InsertarLogCarga("Application_Start", "Error:" + ex.Message);
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }

    }
}