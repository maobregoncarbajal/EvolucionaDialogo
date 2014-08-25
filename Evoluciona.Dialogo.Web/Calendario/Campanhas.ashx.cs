
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using BusinessLogic;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web;
    using System.Web.SessionState;

    public class Campanhas : IHttpHandler, IRequiresSessionState
    {
        private static readonly BlEvento EventoBL = new BlEvento();
        private readonly Dictionary<string, string> ColoresAgrupadas = new Dictionary<string, string>();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.QueryString.Get("start")) ||
                string.IsNullOrEmpty(context.Request.QueryString.Get("end")) ||
                string.IsNullOrEmpty(context.Request.QueryString.Get("codFiltro")))
                return;

            DateTime fechaInicio = Convert.ToDateTime(context.Request.QueryString.Get("start"));
            DateTime fechaFin = Convert.ToDateTime(context.Request.QueryString.Get("end"));
            string codigoFiltro = context.Request.QueryString.Get("codFiltro");
            int codigoRol = Convert.ToInt32(context.Request.QueryString.Get("codigoRol"));
            string codigoUsuario = context.Request.QueryString.Get("codigoUsuario");
            string result = "[";

            CargarColores(codigoUsuario, codigoRol);

            try
            {
                List<BeEvento> eventosVisitas = EventoBL.ObtenerEventosCampanha(codigoUsuario, codigoRol, codigoFiltro, fechaInicio, fechaFin);
                List<BeEvento> eventosNormales = EventoBL.ObtenerEventosSinVisitas(codigoUsuario, fechaInicio, fechaFin, codigoFiltro);

                foreach (BeEvento evento in eventosVisitas)
                {
                    evento.Color = ColoresAgrupadas[evento.Filtro.Trim()];
                    evento.BorderColor = "Gray";
                    evento.TextColor = "White";

                    result += ConvertirEventoACadena(evento);
                }

                foreach (BeEvento evento in eventosNormales)
                {
                    if (string.IsNullOrEmpty(evento.Filtro) ||
                        string.IsNullOrEmpty(evento.Filtro.Trim()))
                    {
                        evento.Color = "White";
                        evento.BorderColor = "Gray";
                        evento.TextColor = "Black";
                    }
                    else
                    {
                        evento.Color = ColoresAgrupadas[evento.Filtro.Trim()];
                        evento.BorderColor = "Gray";
                        evento.TextColor = "White";
                    }

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
                       "color: '" + evento.Color + "'," +
                       "borderColor: '" + evento.BorderColor + "'," +
                       "textColor: '" + evento.TextColor + "'" +
                   "},";
        }

        private static long ConvertToTimestamp(DateTime value)
        {
            long longValue = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return longValue;
        }

        private void CargarColores(string codigoUsuario, int codigoRol)
        {
            int index = 25;
            List<string> colores = ObtenerColores();
            BlEvento blEvento = new BlEvento();
            List<BeComun> listadoFiltros = blEvento.ObtenerFiltros(codigoUsuario, codigoRol);

            foreach (BeComun filtro in listadoFiltros)
            {
                ColoresAgrupadas.Add(filtro.Codigo, colores[index]);

                if (index == colores.Count)
                    index = 0;
                index += 1;
            }
        }

        private static List<string> ObtenerColores()
        {
            List<string> colors = new List<string>();
            string[] colorNames = Enum.GetNames(typeof(KnownColor));
            foreach (string colorName in colorNames)
            {
                KnownColor knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorName);
                if (knownColor > KnownColor.Transparent)
                {
                    colors.Add(colorName);
                }
            }
            return colors;
        }
    }
}