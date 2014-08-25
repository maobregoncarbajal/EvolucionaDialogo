
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessLogic;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.SessionState;

    public class Metodos2 : IHttpHandler, IRequiresSessionState
    {
        private const string ParametroFFVV = "FS";
        private static readonly BlEvento EventoBL = new BlEvento();

        public void ProcessRequest(HttpContext context)
        {
            string accion = context.Request["accion"];
            Object resultado = null;

            switch (accion)
            {
                case "CargarAnhos":
                    resultado = CargarAnhos();
                    EnviarRespuestaJSON(resultado, context);
                    break;
                case "CargarAnhos2":
                    resultado = CargarAnhos2(context);
                    EnviarRespuestaJSON(resultado, context);
                    break;
                case "CargarAnhos3":
                    resultado = CargarAnhos3(context);
                    EnviarRespuestaJSON2(resultado.ToString(), context);
                    break;
            }
        }

        private string CargarAnhos()
        {
            List<string> listaAnhos = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                listaAnhos.Add((2000 + i).ToString());
            }

            string combo = BuilderComboBox(listaAnhos);
            return combo;
        }

        private List<string> CargarAnhos2(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            string cadenaConexion = ObtenerCadenaConexion(prefijoIsoPais);
            List<string> listaAnhos = EventoBL.ObtenerAnhos(cadenaConexion);
            return listaAnhos;
        }

        private string CargarAnhos3(HttpContext context)
        {
            string combo = string.Empty;

            try
            {
                string prefijoIsoPais = context.Request["prefijoIsoPais"];
                string cadenaConexion = ObtenerCadenaConexion(prefijoIsoPais);

                List<string> listaAnhos = EventoBL.ObtenerAnhos(cadenaConexion);
                combo = BuilderComboBox2(listaAnhos);
            }
            catch (Exception ex)
            {
                context.Response.Redirect("~/Error.aspx?mensaje=Error al llamar BD, Detalle:" + ex.Message, true);
            }
            return combo;
        }

        private string ObtenerCadenaConexion(string prefijoIsoPais)
        {
            if (string.IsNullOrEmpty(prefijoIsoPais)) return string.Empty;

            string cadenaConexion = EventoBL.ObtenerCadenaConexion(prefijoIsoPais, ParametroFFVV);
            return cadenaConexion;
        }

        private string BuilderComboBox(List<string> lista)
        {
            string opciones = string.Empty;

            if (lista != null)
            {
                foreach (string item in lista)
                {
                    opciones += string.Format("<option value='{0}'>{0}</option>", item);
                }
            }

            return opciones;
        }

        private string BuilderComboBox2(List<string> lista)
        {
            string opciones = "{";

            if (lista != null)
            {
                int index = 0;
                foreach (string item in lista)
                {
                    opciones += string.Format("\"demo" + index + "\":\"{0}\",", item);
                    index++;
                }

                if (opciones.EndsWith(","))
                {
                    opciones = opciones.Substring(0, opciones.Length - 1);
                }
            }

            return opciones + "}";
        }

        private void EnviarRespuestaJSON(Object objecto, HttpContext context)
        {
            string res = string.Empty;
            try
            {
                res = JsonConvert.SerializeObject(objecto, Formatting.Indented);
            }
            catch (Exception ex)
            {
                context.Response.Redirect("~/Error.aspx?mensaje=Error en Serializar, Detalle:" + ex.Message, true);
            }

            context.Response.ContentType = "application/json";
            context.Response.Write(res);
            context.Response.End();
        }

        private void EnviarRespuestaJSON2(string resultado, HttpContext context)
        {
            string output = JsonConvert.SerializeObject(resultado, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Write(output);
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}