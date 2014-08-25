
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
    public class Preguntas1 : IHttpHandler
    {
        #region Variables

        private readonly BlEncuestaPregunta _oBlEd = new BlEncuestaPregunta();

        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "load":
                    LoadJQGrid(context);
                    break;
            }

            switch (context.Request["oper"])
            {
                case "del":
                    DelJQGrid(context);
                    break;
                case "add":
                    AddJQGrid(context);
                    break;
                case "edit":
                    EditJQGrid(context);
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

        private void LoadJQGrid(HttpContext context)
        {
            List<BeEncuestaPregunta> oListEncuestaPregunta = new List<BeEncuestaPregunta>();

            try
            {
                oListEncuestaPregunta = ListaEncuestaPregunta(context);

                context.Response.Write(JsonConvert.SerializeObject(oListEncuestaPregunta));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private List<BeEncuestaPregunta> ListaEncuestaPregunta(HttpContext context)
        {
            List<BeEncuestaPregunta> oListEncuestaPregunta = _oBlEd.ListaEncuestaPregunta();

            return oListEncuestaPregunta;
        }

        private void DelJQGrid(HttpContext context)
        {
            string IntID = context.Request["IntID"];
            string[] Ids = IntID.Split(',');
            bool estado = false;
            string IdsNoDel = "";


            foreach (string Id in Ids)
            {
                estado = _oBlEd.DeleteEncuestaPregunta(Int32.Parse(Id));

                if (!estado)
                {
                    IdsNoDel = IdsNoDel + Id + ",";
                }
            }

            if (!String.IsNullOrEmpty(IdsNoDel))
            {
                context.Response.Write(JsonConvert.SerializeObject("No se pudo eliminar las filas" + IdsNoDel));
            }
            else
            {
                context.Response.Write(JsonConvert.SerializeObject(""));
            }
        }

        private void AddJQGrid(HttpContext context)
        {
            BeEncuestaPregunta obj = new BeEncuestaPregunta();


            obj.DesPreguntas = context.Request["DesPreguntas"].Trim();

            bool estado = _oBlEd.AddEncuestaPregunta(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        private void EditJQGrid(HttpContext context)
        {
            BeEncuestaPregunta obj = new BeEncuestaPregunta();

            obj.IdPregunta = Int32.Parse(context.Request["IntID"]);
            obj.DesPreguntas = context.Request["DesPreguntas"].Trim();

            bool estado = _oBlEd.EditEncuestaPregunta(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        #endregion
    }
}
