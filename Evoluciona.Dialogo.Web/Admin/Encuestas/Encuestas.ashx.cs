
using System.Linq;

namespace Evoluciona.Dialogo.Web.Admin.Encuestas
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
    public class Encuestas : IHttpHandler
    {
        #region Variables

        private readonly BlEncuestaDialogo _oBlEd = new BlEncuestaDialogo();

        #endregion

        public void ProcessRequest(HttpContext context)
        {

            switch (context.Request["accion"])
            {
                case "load":
                    LoadJqGrid(context);
                    break;
            }

            switch (context.Request["oper"])
            {
                case "del":
                    DelJqGrid(context);
                    break;
                case "add":
                    AddJqGrid(context);
                    break;
                case "edit":
                    EditJqGrid(context);
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

        #region JQGrid

        private void LoadJqGrid(HttpContext context)
        {
            try
            {
                var oListEncuestaDialogo = ListaEncuestaDialogo();

                context.Response.Write(JsonConvert.SerializeObject(oListEncuestaDialogo));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private List<BeEncuestaDialogo> ListaEncuestaDialogo()
        {
            var oListEncuestaDialogo = _oBlEd.ListaEncuestaDialogo();

            return oListEncuestaDialogo;
        }


        private void DelJqGrid(HttpContext context)
        {
            var intId = context.Request["IntID"];
            var ids = intId.Split(',');
            var idsNoDel = (from id in ids let estado = _oBlEd.DeleteEncuestaDialogo(Int32.Parse(id)) where !estado select id).Aggregate("", (current, id) => current + id + ",");


            context.Response.Write(!String.IsNullOrEmpty(idsNoDel)
                ? JsonConvert.SerializeObject("No se pudo eliminar las filas" + idsNoDel)
                : JsonConvert.SerializeObject(""));
        }

        private void AddJqGrid(HttpContext context)
        {
            var obj = new BeEncuestaDialogo();


            var pregunta = context.Request["DesPreguntas"].Split('|');
            var tipoEncuesta = context.Request["DesTipoEncuesta"].Split('|');
            var tipoRespuesta = context.Request["DesTipoRespuesta"].Split('|');

            obj.IdPregunta = Int32.Parse(pregunta[1]);
            obj.CodTipoEncuesta = tipoEncuesta[1];
            obj.CodTipoRespuesta = tipoRespuesta[1];
            obj.Periodo = context.Request["Periodo"].Trim();

            var estado = _oBlEd.AddEncuestaDialogo(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        private void EditJqGrid(HttpContext context)
        {
            var obj = new BeEncuestaDialogo();

            var pregunta = context.Request["DesPreguntas"].Split('|');
            var tipoEncuesta = context.Request["DesTipoEncuesta"].Split('|');
            var tipoRespuesta = context.Request["DesTipoRespuesta"].Split('|');

            obj.IdEncuestaDialogo = Int32.Parse(context.Request["IntID"]);
            obj.IdPregunta = Int32.Parse(pregunta[1]);
            obj.CodTipoEncuesta = tipoEncuesta[1];
            obj.CodTipoRespuesta = tipoRespuesta[1];
            obj.Periodo = context.Request["Periodo"].Trim();

            var estado = _oBlEd.EditEncuestaDialogo(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        #endregion
    }
}
