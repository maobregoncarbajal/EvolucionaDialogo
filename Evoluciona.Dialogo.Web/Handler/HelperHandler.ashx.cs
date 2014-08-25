using Evoluciona.Dialogo.BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Web;

namespace Evoluciona.Dialogo.Web.Handler
{
    /// <summary>
    /// Descripción breve de HelperHandler
    /// </summary>
    public class HelperHandler : IHttpHandler
    {
        private readonly BlPeriodos _blPeriodos = new BlPeriodos();

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "cargarPeriodos":
                    ObtenerListaDePeriodos(context);
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

        private void ObtenerListaDePeriodos(HttpContext context)
        {
            try
            {
                var pais = context.Request["pais"];
                var periodos = _blPeriodos.ObtenerListaDePeriodos(pais);
                
                context.Response.Write(JsonConvert.SerializeObject(periodos));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}