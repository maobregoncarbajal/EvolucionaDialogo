
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using BusinessLogic;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.SessionState;

    public class Calendario : IHttpHandler, IRequiresSessionState
    {
        private static readonly BlEvento EventoBL = new BlEvento();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.QueryString.Get("start")) ||
                 string.IsNullOrEmpty(context.Request.QueryString.Get("end")))
                return;

            DateTime fechaInicio = Convert.ToDateTime(context.Request.QueryString.Get("start"));
            DateTime fechaFin = Convert.ToDateTime(context.Request.QueryString.Get("end"));
            string codigoUsuario = context.Request.QueryString.Get("codigoUsuario");

            string resultado = "[";
            List<BeEvento> eventos = EventoBL.ObtenerEventos(codigoUsuario, fechaInicio, fechaFin);

            foreach (BeEvento evento in eventos)
            {
                resultado += ConvertirEventoACadena(evento);
            }

            if (resultado.EndsWith(","))
            {
                resultado = resultado.Substring(0, resultado.Length - 1);
            }

            resultado += "]";

            string output = JsonConvert.SerializeObject(resultado, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Write(output);
        }

        private static string ConvertirEventoACadena(BeEvento evento)
        {
            return "{" +
                       "id: '" + evento.IDEvento + "'," +
                       "title: '" + evento.Descripcion + "'," +
                       "start:  " + ConvertToTimestamp(evento.FechaInicio) + "," +
                       "end: " + ConvertToTimestamp(evento.FechaFin) + "," +
                       "allDay: false," +
                       "description: '" + evento.Descripcion + "'" +
                   "},";
        }

        private static long ConvertToTimestamp(DateTime value)
        {
            long longValue = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return longValue;
        }
    }
}