
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.SessionState;

    public class Evaluados : IHttpHandler, IRequiresSessionState
    {
        private static readonly BlEvento EventoBL = new BlEvento();
        private static readonly BlUsuario UsuarioBL = new BlUsuario();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.QueryString.Get("start")) ||
                string.IsNullOrEmpty(context.Request.QueryString.Get("end")) ||
                string.IsNullOrEmpty(context.Request.QueryString.Get("codEvaluado")))
                return;

            DateTime fechaInicio = Convert.ToDateTime(context.Request.QueryString.Get("start"));
            DateTime fechaFin = Convert.ToDateTime(context.Request.QueryString.Get("end"));
            string codEvaluado = context.Request.QueryString.Get("codEvaluado");
            int codigoRol = Convert.ToInt32(context.Request.QueryString.Get("codigoRol"));
            string prefijoIsoPais = context.Request.QueryString.Get("prefijoIsoPais");
            string result = "[";

            try
            {
                List<BeEvento> eventosVisitas = new List<BeEvento>();
                List<BeEvento> eventosNormales = new List<BeEvento>();

                switch (codigoRol)
                {
                    case Constantes.RolDirectorVentas:
                        {
                            BeUsuario evaluado = UsuarioBL.ObtenerDatosUsuario(prefijoIsoPais, Constantes.RolGerenteRegion, codEvaluado, Constantes.EstadoActivo);
                            if (evaluado == null) return;

                            eventosVisitas = EventoBL.ObtenerEventosCampanha(evaluado.codigoUsuario, Constantes.RolGerenteRegion, "0", fechaInicio, fechaFin);
                            eventosNormales = EventoBL.ObtenerEventosSinVisitas(evaluado.codigoUsuario, fechaInicio, fechaFin);
                        }
                        break;
                    case Constantes.RolGerenteRegion:
                        {
                            BeUsuario evaluado = UsuarioBL.ObtenerDatosUsuario(prefijoIsoPais, Constantes.RolGerenteZona, codEvaluado, Constantes.EstadoActivo);
                            if (evaluado == null) return;

                            eventosVisitas = EventoBL.ObtenerEventosCampanha(evaluado.codigoUsuario, Constantes.RolGerenteZona, "0", fechaInicio, fechaFin);
                            eventosNormales = EventoBL.ObtenerEventosSinVisitas(evaluado.codigoUsuario, fechaInicio, fechaFin);
                        }
                        break;
                }

                foreach (BeEvento evento in eventosVisitas)
                {
                    evento.BorderColor = "Gray";
                    evento.TextColor = "White";

                    result += ConvertirEventoACadena(evento);
                }

                foreach (BeEvento evento in eventosNormales)
                {
                    evento.Color = "White";
                    evento.BorderColor = "Gray";
                    evento.TextColor = "White";

                    result += ConvertirEventoACadena(evento);
                }

                if (result.EndsWith(","))
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }
            catch (Exception)
            {
            }

            result += "]";
            string output = JsonConvert.SerializeObject(result, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Write(output);
        }

        private static string ConvertirEventoACadena(BeEvento evento)
        {
            return "{" +
                       "id: '" + ((evento.IDEvento == 0) ? "_" + evento.Filtro : evento.IDEvento.ToString()) + "'," +
                       "title: '" + evento.Asunto + "'," +
                       "start:  " + ConvertToTimestamp(evento.FechaInicio) + "," +
                       "end: " + ConvertToTimestamp(evento.FechaFin.AddHours(1)) + "," +
                       "allDay: false," +
                       "description: '" + evento.Asunto + "'," +
                       "borderColor: '" + evento.BorderColor + "'," +
                       "textColor: '" + evento.TextColor + "'" +
                   "},";
        }

        private static long ConvertToTimestamp(DateTime value)
        {
            long longValue = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return longValue;
        }
    }
}