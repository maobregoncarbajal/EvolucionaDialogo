
namespace Evoluciona.Dialogo.Web.Desempenio.Encuestas
{
    using BusinessEntity;
    using BusinessLogic;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Services;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Encuesta1 : IHttpHandler
    {
        #region Variables

        private readonly BlEncuestaDialogo blEncuesta = new BlEncuestaDialogo();

        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "loadEncuestaCompletar":
                    LoadEncuestaCompletar(context);
                    break;
                case "loadOpcionesRspts":
                    LoadOpcionesRspts(context);
                    break;
                case "insertRspts":
                    InsertRspts(context);
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


        private void LoadEncuestaCompletar(HttpContext context)
        {
            List<BeEncuestaDialogo> entidades = new List<BeEncuestaDialogo>();

            try
            {
                entidades = ListaEncuestaCompletar(context);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private List<BeEncuestaDialogo> ListaEncuestaCompletar(HttpContext context)
        {
            List<BeEncuestaDialogo> entidades = new List<BeEncuestaDialogo>();
            BeEncuestaDialogo obj = new BeEncuestaDialogo();

            obj.Periodo = context.Request["periodo"].Trim();
            obj.CodTipoEncuesta = context.Request["codTipoEncuesta"].Trim();


            entidades = blEncuesta.ListaEncuestaCompletar(obj);

            return entidades;
        }


        private void LoadOpcionesRspts(HttpContext context)
        {
            try
            {
                var entidades = blEncuesta.ListaOpcionesRspts();

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private void InsertRspts(HttpContext context)
        {
            var obj = new BeEncuestaFfvv
                          {
                              IdEncuestaDialogo = Int32.Parse(context.Request["IdEncuestaDialogo"]),
                              CodPuntaje = context.Request["codPuntaje"],
                              Comentario = context.Request["comentario"],
                              CodTipoEncuesta = context.Request["codTipoEncuesta"],
                              CodigoUsuario = context.Request["codigoUsuario"],
                              Periodo = context.Request["periodo"]
                          };


            var estado = blEncuesta.InsertRspts(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));
        }
    }
}
