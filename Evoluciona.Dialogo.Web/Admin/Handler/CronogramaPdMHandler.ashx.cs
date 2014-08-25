using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.BusinessLogic;
using Evoluciona.Dialogo.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace Evoluciona.Dialogo.Web.Admin.Handler
{
    /// <summary>
    /// Descripción breve de CronogramaPdMHandler
    /// </summary>
    public class CronogramaPdMHandler : IHttpHandler
    {
        #region Variables
        private readonly BlCronogramaPdM _blCronogramaPdM = new BlCronogramaPdM();
        private readonly BlPais _blPais = new BlPais();
        private readonly BlPeriodos _blPeriodos = new BlPeriodos();
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "loadCronogramaPdM":
                    LoadCronogramaPdM(context);
                    break;
                case "loadPaises":
                    LoadPaises(context);
                    break;
                case "loadPeriodos":
                    LoadPeriodos(context);
                    break;
                case "validaCronogramaPdM":
                    ValidaCronogramaPdM(context);
                    break;
            }

            switch (context.Request["oper"])
            {
                case "add":
                    AddCronogramaPdM(context);
                    break;
                case "edit":
                    EditCronogramaPdM(context);
                    break;
                case "del":
                    DelCronogramaPdM(context);
                    break;

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void LoadCronogramaPdM(HttpContext context)
        {
            try
            {
                var listaCronogramaPdM = ListarCronogramaPdM();
                context.Response.Write(JsonConvert.SerializeObject(listaCronogramaPdM));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeCronogramaPdM> ListarCronogramaPdM()
        {
            var listaCronogramaPdM = _blCronogramaPdM.ListarCronogramaPdM();
            return listaCronogramaPdM;
        }

        private void AddCronogramaPdM(HttpContext context)
        {
            var obj = new BeCronogramaPdM
            {
                PrefijoIsoPais = context.Request["OBePais.NombrePais"].Trim(),
                Periodo = context.Request["Periodo"].Trim(),
                FechaLimite = DateTime.Parse(context.Request["FechaLimite"]),
                UsuarioCrea = context.Request["UsuarioCrea"].Trim(),
                Estado = Constantes.Activo
            };

            if (!String.IsNullOrEmpty(context.Request["FechaProrroga"])){
                obj.FechaProrroga = DateTime.Parse(context.Request["FechaProrroga"]);
            }
            
            var estado = _blCronogramaPdM.AddCronogramaPdM(obj);
            var respuesta = estado ? "Registro ingresado correctamente" : "";
            context.Response.Write(respuesta);
        }


        private void EditCronogramaPdM(HttpContext context)
        {
            var obj = new BeCronogramaPdM
            {
                IdCronogramaPdM = Int32.Parse(context.Request["IdCronogramaPdM"]),
                PrefijoIsoPais = context.Request["OBePais.NombrePais"].Trim(),
                Periodo = context.Request["Periodo"].Trim(),
                FechaLimite = DateTime.Parse(context.Request["FechaLimite"]),
                UsuarioModi = context.Request["UsuarioModi"].Trim(),
                Estado = Constantes.Activo
            };

            if (!String.IsNullOrEmpty(context.Request["FechaProrroga"]))
            {
                obj.FechaProrroga = DateTime.Parse(context.Request["FechaProrroga"]);
            }

            var estado = _blCronogramaPdM.EditCronogramaPdM(obj);
            var respuesta = estado ? "Registro modificado correctamente" : "";
            context.Response.Write(respuesta);
        }

        private void DelCronogramaPdM(HttpContext context)
        {
            var obj = new BeCronogramaPdM
            {
                IdCronogramaPdM = Int32.Parse(context.Request["IdCronogramaPdM"]),
                UsuarioModi = context.Request["UsuarioModi"].Trim(),
                Estado = Constantes.Inactivo
            };

            var estado = _blCronogramaPdM.DelCronogramaPdM(obj);
            var respuesta = estado ? "Registro eliminado correctamente" : "";
            context.Response.Write(respuesta);
        }

        private void LoadPaises(HttpContext context)
        {
            try
            {
                var listaPaises = ListarPaises();
                context.Response.Write(JsonConvert.SerializeObject(listaPaises));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BePais> ListarPaises()
        {
            var listaPaises = _blPais.ObtenerPaises();
            return listaPaises;
        }


        private void LoadPeriodos(HttpContext context)
        {
            try
            {
                var listaPaises = ListarPeriodos();
                context.Response.Write(JsonConvert.SerializeObject(listaPaises));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<string> ListarPeriodos()
        {
            const string pais = Constantes.CeroCero;
            var listaPeriodos = _blPeriodos.ObtenerPeriodos(pais);
            return listaPeriodos;
        }

        private void ValidaCronogramaPdM(HttpContext context)
        {
            var pais = context.Request["pais"];
            var periodo = context.Request["periodo"];
            int cant = _blCronogramaPdM.ValidaCronogramaPdM(pais, periodo);
            context.Response.Write(JsonConvert.SerializeObject(cant));
        }


    }
}