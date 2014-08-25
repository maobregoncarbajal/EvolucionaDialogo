
namespace Evoluciona.Dialogo.Web.Desempenio.Handlers
{
    using BusinessEntity;
    using BusinessLogic;
    using Newtonsoft.Json;
    using System;
    using System.Web;
    using System.Web.SessionState;

    public class CriticasHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int idProceso = int.Parse(context.Request["idProceso"]);
            string documentoIdentidad = context.Request["documentoIdentidad"];

            BlCritica criticaBL = new BlCritica();
            BeCriticas criticaActual = criticaBL.ObtenerCritica(idProceso, documentoIdentidad);

            EnviarRespuestaJSON(criticaActual, context);
        }

        private void EnviarRespuestaJSON(Object objecto, HttpContext context)
        {
            string res = JsonConvert.SerializeObject(objecto, Formatting.Indented);

            context.Response.ContentType = "application/json";
            context.Response.Write(res);

            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}