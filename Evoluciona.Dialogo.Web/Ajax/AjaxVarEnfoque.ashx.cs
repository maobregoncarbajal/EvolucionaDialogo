
namespace Evoluciona.Dialogo.Web.Ajax
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.Services;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxVarEnfoque : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.ContentType = "application/json";
            if (context.Request["accion"] != null)
            {
                if (context.Request["accion"] == "insertar")
                {
                    InsertarVariableEnfoques(context);
                }
                else if (context.Request["accion"] == "cargar")
                {
                    if (context.Request["idVariableEnfoque"] == "")
                    {
                        return;
                    }
                    CargarDataVariablesEnfoque(context);
                }
                else if (context.Request["accion"] == "eliminar")
                {
                    EliminarPlanVariablesEnfoque(context);
                }

            }

        }
        private void InsertarVariableEnfoques(HttpContext context)
        {
            if (context.Request["idIndicador1"] == "" || context.Request["idIndicador2"] == "")
            {
                return;
            }
            BlVariableEnfoque objVarEnfoqueBL = new BlVariableEnfoque();
            BeVariableEnfoque objVarEnfoqueBE = new BeVariableEnfoque();

            objVarEnfoqueBE.idIndicador = Convert.ToInt32(context.Request["idIndicador1"]);
            objVarEnfoqueBE.campania = "";
            objVarEnfoqueBE.zonas = context.Request["zonas1"];
            objVarEnfoqueBE.planAccion = context.Request["plan1"];

            if (context.Request["idVarEnfoque1"] == "")
            {
                objVarEnfoqueBE.idVariableEnfoque = 0;
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = Convert.ToInt32(context.Request["idVarEnfoque1"]);
            }

            objVarEnfoqueBE.estado = Constantes.EstadoActivo;

            if (objVarEnfoqueBE.idVariableEnfoque > 0)
            {//actualizar 

                objVarEnfoqueBL.ActualizarVariableEnfoque(objVarEnfoqueBE);
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = objVarEnfoqueBL.InsertarVariableEnfoque(objVarEnfoqueBE);
            }

            objVarEnfoqueBE = new BeVariableEnfoque();
            objVarEnfoqueBE.idIndicador = Convert.ToInt32(context.Request["idIndicador2"]);
            objVarEnfoqueBE.campania = "";// context.Request["campania2"];
            objVarEnfoqueBE.zonas = context.Request["zonas2"];
            objVarEnfoqueBE.planAccion = context.Request["plan2"];
            if (context.Request["idVarEnfoque2"] == "")
            {
                objVarEnfoqueBE.idVariableEnfoque = 0;
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = Convert.ToInt32(context.Request["idVarEnfoque2"]);
            }

            objVarEnfoqueBE.estado = Constantes.EstadoActivo;

            if (objVarEnfoqueBE.idVariableEnfoque > 0)
            {//actualizar 
                objVarEnfoqueBL.ActualizarVariableEnfoque(objVarEnfoqueBE);
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = objVarEnfoqueBL.InsertarVariableEnfoque(objVarEnfoqueBE);
            }
        }

        private void CargarDataVariablesEnfoque(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string datos = "";
            BlVariableEnfoque objVarEnfoqueBL = new BlVariableEnfoque();
            int idVariableEnfoque = 0;
            if (context.Request["idVariableEnfoque"] != "")
            {
                idVariableEnfoque = Convert.ToInt32(context.Request["idVariableEnfoque"]);
            }
            DataTable dt = objVarEnfoqueBL.ObtenerPlanesByVariablesEnfoqueProcesadas(idVariableEnfoque);
            List<BeVariableEnfoque> listVarEnfoque = new List<BeVariableEnfoque>();
            List<BeVariableEnfoque> d = new List<BeVariableEnfoque>();
            BeVariableEnfoque objVarEnfoqueBE = null;
            if (dt.Rows.Count > 0)
            { //, , 
                Newtonsoft.Json.JsonSerializer jsonColeccion = new JsonSerializer();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    objVarEnfoqueBE = new BeVariableEnfoque();
                    objVarEnfoqueBE.idVariableEnfoquePlan = Convert.ToInt32(dt.Rows[x]["intIDVariableEnfoquePlan"]);
                    objVarEnfoqueBE.planAccion = dt.Rows[x]["vchPlanAccion"].ToString();
                    objVarEnfoqueBE.postDialogo = Convert.ToBoolean(dt.Rows[x]["bitPostDialogo"]);
                    listVarEnfoque.Add(objVarEnfoqueBE);
                }
                datos = JsonConvert.SerializeObject(listVarEnfoque, Formatting.Indented);

                context.Response.Write(datos);
            }
            else
            {
                context.Response.Write("");
            }
        }

        private void EliminarPlanVariablesEnfoque(HttpContext context)
        {
            BlVariableEnfoque objVarEnfoqueBL = new BlVariableEnfoque();
            int idVarEnfoquePlan = Convert.ToInt32(context.Request["idVariablePlanEnfoque"]);
            objVarEnfoqueBL.EliminarVariableEnfoquePlanes(idVarEnfoquePlan);
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



