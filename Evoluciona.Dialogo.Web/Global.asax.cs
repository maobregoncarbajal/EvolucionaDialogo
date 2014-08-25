
namespace Evoluciona.Dialogo.Web
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web;
    using BusinessLogic;
    using Quartz;
    using Quartz.Impl;

    public class Global : HttpApplication
    {
        IScheduler _schedulerEnviarCorreo;
        IScheduler _schedulerCargaData;
        IScheduler _schedulerCompetencia;
        IScheduler _schedulerPlanDeMejora;
        IScheduler _schedulerCargaDirectorio;

        protected void Application_Start(object sender, EventArgs e)
        {
            //XmlConfigurator.Configure();
            var objDataMart = new BlDataMart();

            //trabajo enviar correos
            var schedulerFactoryEnviarCorreos = new StdSchedulerFactory();
            var trabajoEnviarCorreos = new JobDetail("TareaEnviarCorreos", null, typeof(TareaEnviarCorreos));
            var triggerEnviarCorreo = TriggerUtils.MakeWeeklyTrigger("triggerEnviarCorreo", DayOfWeek.Monday, 5, 0);

            //trabajo cargar informacion
            var schedulerFactoryCaragarInformacion = new StdSchedulerFactory();
            var trabajoCargarData = new JobDetail("TareaCargarData", null, typeof(TareaCargarData));
            var horaEjecucion = Convert.ToInt32(ConfigurationManager.AppSettings["horaEjecucion"]);
            var minutoEjecucion = Convert.ToInt32(ConfigurationManager.AppSettings["minutoEjecucion"]);
            var triggerCargarData = TriggerUtils.MakeDailyTrigger("triggerCargarData", horaEjecucion, minutoEjecucion);

            //trabajo cargar Competencias
            var schedulerFactoryCompetencias = new StdSchedulerFactory();
            var trabajoCargarCompetencias = new JobDetail("TareaCompetencias", null, typeof(TareaCompetencias));
            var diaEjecucionCompetencia = Convert.ToInt32(ConfigurationManager.AppSettings["diaEjecucionProcesoCompetencia"]);
            var horaEjecucionCompetencia = Convert.ToInt32(ConfigurationManager.AppSettings["horaEjecucionProcesoCompetencia"]);
            var minutoEjecucionCompetencia = Convert.ToInt32(ConfigurationManager.AppSettings["minutoEjecucionProcesoCompetencia"]);
            var triggerTareaCompetencias = TriggerUtils.MakeMonthlyTrigger("triggerTareaCompetencias", diaEjecucionCompetencia, horaEjecucionCompetencia, minutoEjecucionCompetencia);

            //trabajo activar plan de mejora
            var schedulerFactoryActivarPlanMejora = new StdSchedulerFactory();
            var trabajoActivaPlanMejora = new JobDetail("TareaActivaPlanMejora", null, typeof(TareaActivaPlanMejora));
            var horaEjecucionActivaPlanMejora = Convert.ToInt32(ConfigurationManager.AppSettings["horaEjecucionActivaPlanMejora"]);
            var minutoEjecucionActivaPlanMejora = Convert.ToInt32(ConfigurationManager.AppSettings["minutoEjecucionActivaPlanMejora"]);
            var triggerActivaPlanMejora = TriggerUtils.MakeDailyTrigger("triggerActivaPlanMejora", horaEjecucionActivaPlanMejora, minutoEjecucionActivaPlanMejora);

            //trabajo cargar directorio
            var schedulerFactoryDirectorio = new StdSchedulerFactory();
            var trabajoCargarDirectorio = new JobDetail("TareaCargarDirectorio", null, typeof(TareaCargarDirectorio));
            var hhCargaDirectorio = Convert.ToInt32(ConfigurationManager.AppSettings["hhCargaDirectorio"].ToString(CultureInfo.InvariantCulture));
            var mmCargaDirectorio = Convert.ToInt32(ConfigurationManager.AppSettings["mmCargaDirectorio"].ToString(CultureInfo.InvariantCulture));
            var triggerCargarDirectorio = TriggerUtils.MakeDailyTrigger("triggerCargarDirectorio", hhCargaDirectorio, mmCargaDirectorio);

            try
            {

                //trabajo enviar correos
                objDataMart.InsertarLogCarga("Application_Start", "Inicio de asignación del Job: TareaEnviarCorreos");
                _schedulerEnviarCorreo = schedulerFactoryEnviarCorreos.GetScheduler();
                if (_schedulerEnviarCorreo != null)
                {
                    _schedulerEnviarCorreo.AddJob(trabajoEnviarCorreos, true);
                    if (triggerEnviarCorreo != null)
                    {
                        triggerEnviarCorreo.JobName = "TareaEnviarCorreos";
                        _schedulerEnviarCorreo.ScheduleJob(triggerEnviarCorreo);
                        _schedulerEnviarCorreo.Start();
                    }
                }

                //trabajo cargar informacion
                objDataMart.InsertarLogCarga("Application_Start", "Inicio de asignación del Job: TareaCargarData");
                _schedulerCargaData = schedulerFactoryCaragarInformacion.GetScheduler();
                if (_schedulerCargaData != null)
                {
                    _schedulerCargaData.AddJob(trabajoCargarData, true);
                    if (triggerCargarData != null)
                    {
                        triggerCargarData.JobName = "TareaCargarData";
                        _schedulerCargaData.ScheduleJob(triggerCargarData);
                        _schedulerCargaData.Start();
                    }
                }

                //trabajo cargar Competencias
                objDataMart.InsertarLogCarga("Application_Start", "Inicio de asignación del Job: TareaCompetencias");
                _schedulerCompetencia = schedulerFactoryCompetencias.GetScheduler();
                if (_schedulerCompetencia != null)
                {
                    _schedulerCompetencia.AddJob(trabajoCargarCompetencias, true);
                    if (triggerTareaCompetencias != null)
                    {
                        triggerTareaCompetencias.JobName = "TareaCompetencias";
                        _schedulerCompetencia.ScheduleJob(triggerTareaCompetencias);
                        _schedulerCompetencia.Start();
                    }
                }

                //trabajo activar plan de mejora
                objDataMart.InsertarLogCarga("Application_Start", "Inicio de asignación del Job: TareaActivaPlanMejora");
                _schedulerPlanDeMejora = schedulerFactoryActivarPlanMejora.GetScheduler();
                if (_schedulerPlanDeMejora != null)
                {
                    _schedulerPlanDeMejora.AddJob(trabajoActivaPlanMejora, true);
                    if (triggerActivaPlanMejora != null)
                    {
                        triggerActivaPlanMejora.JobName = "TareaActivaPlanMejora";
                        _schedulerPlanDeMejora.ScheduleJob(triggerActivaPlanMejora);
                        _schedulerPlanDeMejora.Start();
                    }
                }

                //trabajo cargar directorio
                objDataMart.InsertarLogCarga("Application_Start", "Inicio de asignación del Job: TareaCargarDirectorio");
                _schedulerCargaDirectorio = schedulerFactoryDirectorio.GetScheduler();
                if (_schedulerCargaDirectorio != null)
                {
                    _schedulerCargaDirectorio.AddJob(trabajoCargarDirectorio, true);
                    if (triggerCargarDirectorio != null)
                    {
                        triggerCargarDirectorio.JobName = "TareaCargarDirectorio";
                        _schedulerCargaDirectorio.ScheduleJob(triggerCargarDirectorio);
                        _schedulerCargaDirectorio.Start();
                    }
                }

            }
            catch (Exception ex)
            {
                objDataMart.InsertarLogCarga("Application_Start", "Error:" + ex.Message);
                //Response.Write(ex.Message);
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_schedulerEnviarCorreo != null)
            {
                _schedulerEnviarCorreo.Shutdown();
            }
            if (_schedulerCargaData != null)
            {
                _schedulerCargaData.Shutdown();
            }
            if (_schedulerCompetencia != null)
            {
                _schedulerCompetencia.Shutdown();
            }
            if (_schedulerPlanDeMejora != null)
            {
                _schedulerPlanDeMejora.Shutdown();
            }
            if (_schedulerCargaDirectorio != null)
            {
                _schedulerCargaDirectorio.Shutdown();
            }
        }

    }
}