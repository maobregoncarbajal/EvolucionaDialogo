using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.BusinessLogic;
using Evoluciona.Dialogo.Web.Helpers;
using Evoluciona.Dialogo.Web.Matriz.Helpers;
using Evoluciona.Dialogo.Web.Matriz.Helpers.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Evoluciona.Dialogo.Web.Admin.Reportes
{
    /// <summary>
    /// Descripción breve de RepDialogos
    /// </summary>
    public class RepDialogos : IHttpHandler
    {
        #region Variables
        private readonly BlRepDialogos _blRepDialogos = new BlRepDialogos();
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "load":
                    LoadJqGrid(context);
                    break;
                case "export":
                    ExportJqGrid(context);
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

        private void LoadJqGrid(HttpContext context)
        {
            try
            {
                var listaReporte = ListaReporte(context);
                context.Response.Write(JsonConvert.SerializeObject(listaReporte));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeRepDialogos> ListaReporte(HttpContext context)
        {
            var request = context.Request;
            var listaReporte = new List<BeRepDialogos>();
            var tipoReporte = request["tipoReporte"];
            var pais = request["pais"];
            var periodo = request["anho"] + " " + request["periodo"];
            var idRol = Convert.ToInt32(request["idRol"]);
            var planMejora = Convert.ToBoolean(request["planMejora"]);

            switch (tipoReporte)
            {
                case "AntNeg":
                    listaReporte = _blRepDialogos.ListarRepDialogoAntNeg(pais, periodo, idRol, planMejora);
                    break;
                case "AntEqu":
                    listaReporte = _blRepDialogos.ListarRepDialogoAntEqu(pais, periodo, idRol, planMejora);
                    break;
                case "AntCom":
                    listaReporte = _blRepDialogos.ListarRepDialogoAntCom(pais, periodo, idRol, planMejora);
                    break;
                case "DurNeg":
                    listaReporte = _blRepDialogos.ListarRepDialogoDurNeg(pais, periodo, idRol, planMejora);
                    break;
                case "DurEqu":
                    listaReporte = _blRepDialogos.ListarRepDialogoDurEqu(pais, periodo, idRol, planMejora);
                    break;
                case "DurCom":
                    listaReporte = _blRepDialogos.ListarRepDialogoDurCom(pais, periodo, idRol, planMejora);
                    break;
            }
            return listaReporte;
        }


        private void ExportJqGrid(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Admin/Reportes/");
            const string tipo = "xls";
            var entidades = ListaReporte(context);
            var tabla = Utils.ConvertToDataTable(entidades);
            var tipoReporte = context.Request["tipoReporte"];
            var dtRc = RemoverColumnas(tabla, tipoReporte);
            var data = OrdenarColumnas(dtRc, tipoReporte);
            var fileName = string.Format("{0}.{1}", "Reporte" + tipoReporte + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), tipo);
            var strFilePath = strFolder + id + fileName;

            data.TableName = "Reporte " + TituloReporte(tipoReporte); //titulo
            data.Namespace = ""; // texto filtros

            //Crear Titulos
            var headerTitles  = CrearTitulos(tipoReporte);
            HeaderTitle(headerTitles, ref data);

            var edw = new ExcelDatasetWriter();
            var ds = new DataSet();
            ds.Tables.Add(data);
            var book = edw.CreateWorkbook(ds);
            book.Save(strFilePath);
            var archivo = MatrizHelper.ReadFile(strFilePath);
            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            context.Response.ContentType = "application/" + tipo;
            context.Response.BinaryWrite(archivo);
            context.Response.Flush();
            MatrizHelper.DeleteFile(strFilePath);
            context.Response.End();

        }


        private static void HeaderTitle(IEnumerable<string> titulos, ref DataTable dt)
        {
            var i = 0;
            foreach (var titulo in titulos)
            {
                dt.Columns[i].ColumnName = titulo;
                i++;
            }
        }

        private static DataTable RemoverColumnas(DataTable tabla, string tipoReporte)
        {
            switch (tipoReporte)
            {
                case "AntNeg":
                    tabla.Columns.Remove("CodPriorizada");
                    tabla.Columns.Remove("NombrePriorizada");
                    tabla.Columns.Remove("VariableConsiderar");
                    tabla.Columns.Remove("Competencia");
                    tabla.Columns.Remove("Comportamiento");
                    tabla.Columns.Remove("Sugerencia");
                    tabla.Columns.Remove("Observacion");
                    tabla.Columns.Remove("VariableEnfoque");
                    tabla.Columns.Remove("VariableCausales");
                    tabla.Columns.Remove("ZonasSecciones");
                    tabla.Columns.Remove("PlanNegocio");
                    tabla.Columns.Remove("PlanAccion");
                    tabla.Columns.Remove("DesPreguRetro");
                    tabla.Columns.Remove("Respuesta");
                    break;
                case "AntEqu":
                    tabla.Columns.Remove("DesVariableEnfoque");
                    tabla.Columns.Remove("DesVariableCausa");
                    tabla.Columns.Remove("Competencia");
                    tabla.Columns.Remove("Comportamiento");
                    tabla.Columns.Remove("Sugerencia");
                    tabla.Columns.Remove("Observacion");
                    tabla.Columns.Remove("VariableEnfoque");
                    tabla.Columns.Remove("VariableCausales");
                    tabla.Columns.Remove("ZonasSecciones");
                    tabla.Columns.Remove("PlanNegocio");
                    tabla.Columns.Remove("PlanAccion");
                    tabla.Columns.Remove("DesPreguRetro");
                    tabla.Columns.Remove("Respuesta");
                    break;
                case "AntCom":
                    tabla.Columns.Remove("DesVariableEnfoque");
                    tabla.Columns.Remove("DesVariableCausa");
                    tabla.Columns.Remove("CodPriorizada");
                    tabla.Columns.Remove("NombrePriorizada");
                    tabla.Columns.Remove("VariableConsiderar");
                    tabla.Columns.Remove("VariableEnfoque");
                    tabla.Columns.Remove("VariableCausales");
                    tabla.Columns.Remove("ZonasSecciones");
                    tabla.Columns.Remove("PlanNegocio");
                    tabla.Columns.Remove("PlanAccion");
                    tabla.Columns.Remove("DesPreguRetro");
                    tabla.Columns.Remove("Respuesta");
                    break;
                case "DurNeg":
                    tabla.Columns.Remove("DesVariableEnfoque");
                    tabla.Columns.Remove("DesVariableCausa");
                    tabla.Columns.Remove("CodPriorizada");
                    tabla.Columns.Remove("NombrePriorizada");
                    tabla.Columns.Remove("VariableConsiderar");
                    tabla.Columns.Remove("Competencia");
                    tabla.Columns.Remove("Comportamiento");
                    tabla.Columns.Remove("Sugerencia");
                    tabla.Columns.Remove("Observacion");
                    tabla.Columns.Remove("PlanAccion");
                    tabla.Columns.Remove("DesPreguRetro");
                    tabla.Columns.Remove("Respuesta");
                    break;
                case "DurEqu":
                    tabla.Columns.Remove("DesVariableEnfoque");
                    tabla.Columns.Remove("DesVariableCausa");
                    tabla.Columns.Remove("VariableConsiderar");
                    tabla.Columns.Remove("Competencia");
                    tabla.Columns.Remove("Comportamiento");
                    tabla.Columns.Remove("Sugerencia");
                    tabla.Columns.Remove("Observacion");
                    tabla.Columns.Remove("VariableEnfoque");
                    tabla.Columns.Remove("VariableCausales");
                    tabla.Columns.Remove("ZonasSecciones");
                    tabla.Columns.Remove("PlanNegocio");
                    tabla.Columns.Remove("DesPreguRetro");
                    tabla.Columns.Remove("Respuesta");
                    break;
                case "DurCom":
                    tabla.Columns.Remove("DesVariableEnfoque");
                    tabla.Columns.Remove("DesVariableCausa");
                    tabla.Columns.Remove("CodPriorizada");
                    tabla.Columns.Remove("NombrePriorizada");
                    tabla.Columns.Remove("VariableConsiderar");
                    tabla.Columns.Remove("Comportamiento");
                    tabla.Columns.Remove("Sugerencia");
                    tabla.Columns.Remove("Observacion");
                    tabla.Columns.Remove("VariableEnfoque");
                    tabla.Columns.Remove("VariableCausales");
                    tabla.Columns.Remove("ZonasSecciones");
                    tabla.Columns.Remove("PlanNegocio");
                    tabla.Columns.Remove("PlanAccion");
                    break;
            }

            return tabla;

        }

        private static DataTable OrdenarColumnas(DataTable tabla, string tipoReporte)
        {
            switch (tipoReporte)
            {
                case "AntNeg":
                    tabla.Columns["CodEvaluador"].SetOrdinal(0);
                    tabla.Columns["NombreEvaluador"].SetOrdinal(1);
                    tabla.Columns["CodEvaluado"].SetOrdinal(2);
                    tabla.Columns["NombreEvaluado"].SetOrdinal(3);

                    tabla.Columns["DesVariableEnfoque"].SetOrdinal(4);
                    tabla.Columns["DesVariableCausa"].SetOrdinal(5);

                    tabla.Columns["Periodo"].SetOrdinal(6);
                    tabla.Columns["TipoDialogo"].SetOrdinal(7);
                    tabla.Columns["Pais"].SetOrdinal(8);
                    break;
                case "AntEqu":
                    tabla.Columns["CodEvaluador"].SetOrdinal(0);
                    tabla.Columns["NombreEvaluador"].SetOrdinal(1);
                    tabla.Columns["CodEvaluado"].SetOrdinal(2);
                    tabla.Columns["NombreEvaluado"].SetOrdinal(3);

                    tabla.Columns["CodPriorizada"].SetOrdinal(4);
                    tabla.Columns["NombrePriorizada"].SetOrdinal(5);
                    tabla.Columns["VariableConsiderar"].SetOrdinal(6);

                    tabla.Columns["Periodo"].SetOrdinal(7);
                    tabla.Columns["TipoDialogo"].SetOrdinal(8);
                    tabla.Columns["Pais"].SetOrdinal(9);
                    break;
                case "AntCom":
                    tabla.Columns["CodEvaluador"].SetOrdinal(0);
                    tabla.Columns["NombreEvaluador"].SetOrdinal(1);
                    tabla.Columns["CodEvaluado"].SetOrdinal(2);
                    tabla.Columns["NombreEvaluado"].SetOrdinal(3);

                    tabla.Columns["Competencia"].SetOrdinal(4);
                    tabla.Columns["Comportamiento"].SetOrdinal(5);
                    tabla.Columns["Sugerencia"].SetOrdinal(6);
                    tabla.Columns["Observacion"].SetOrdinal(7);

                    tabla.Columns["Periodo"].SetOrdinal(8);
                    tabla.Columns["TipoDialogo"].SetOrdinal(9);
                    tabla.Columns["Pais"].SetOrdinal(10);
                    break;
                case "DurNeg":
                    tabla.Columns["CodEvaluador"].SetOrdinal(0);
                    tabla.Columns["NombreEvaluador"].SetOrdinal(1);
                    tabla.Columns["CodEvaluado"].SetOrdinal(2);
                    tabla.Columns["NombreEvaluado"].SetOrdinal(3);

                    tabla.Columns["VariableEnfoque"].SetOrdinal(4);
                    tabla.Columns["VariableCausales"].SetOrdinal(5);
                    tabla.Columns["ZonasSecciones"].SetOrdinal(6);
                    tabla.Columns["PlanNegocio"].SetOrdinal(7);

                    tabla.Columns["Periodo"].SetOrdinal(8);
                    tabla.Columns["TipoDialogo"].SetOrdinal(9);
                    tabla.Columns["Pais"].SetOrdinal(10);
                    break;
                case "DurEqu":
                    tabla.Columns["CodEvaluador"].SetOrdinal(0);
                    tabla.Columns["NombreEvaluador"].SetOrdinal(1);
                    tabla.Columns["CodEvaluado"].SetOrdinal(2);
                    tabla.Columns["NombreEvaluado"].SetOrdinal(3);

                    tabla.Columns["CodPriorizada"].SetOrdinal(4);
                    tabla.Columns["NombrePriorizada"].SetOrdinal(5);
                    tabla.Columns["PlanAccion"].SetOrdinal(6);

                    tabla.Columns["Periodo"].SetOrdinal(7);
                    tabla.Columns["TipoDialogo"].SetOrdinal(8);
                    tabla.Columns["Pais"].SetOrdinal(9);
                    break;
                case "DurCom":
                    tabla.Columns["CodEvaluador"].SetOrdinal(0);
                    tabla.Columns["NombreEvaluador"].SetOrdinal(1);
                    tabla.Columns["CodEvaluado"].SetOrdinal(2);
                    tabla.Columns["NombreEvaluado"].SetOrdinal(3);

                    tabla.Columns["Competencia"].SetOrdinal(4);
                    tabla.Columns["DesPreguRetro"].SetOrdinal(5);
                    tabla.Columns["Respuesta"].SetOrdinal(6);

                    tabla.Columns["Periodo"].SetOrdinal(7);
                    tabla.Columns["TipoDialogo"].SetOrdinal(8);
                    tabla.Columns["Pais"].SetOrdinal(9);
                    break;
            }

            return tabla;

        }


        private static IEnumerable<string> CrearTitulos(string tipoReporte)
        {
            var headerTitles = new List<string>();

            switch (tipoReporte)
            {
                case "AntNeg":
                    headerTitles.Add("Cod.Evaluador ");
                    headerTitles.Add("Nombre Evaluador ");
                    headerTitles.Add("Cod.Evaluado ");
                    headerTitles.Add("Nombre Evaluado ");

                    headerTitles.Add("Var.Enfoque ");
                    headerTitles.Add("Var.Causa ");

                    headerTitles.Add("Período ");
                    headerTitles.Add("Tipo Diálogo ");
                    headerTitles.Add("País ");
                    break;
                case "AntEqu":
                    headerTitles.Add("Cod.Evaluador ");
                    headerTitles.Add("Nombre Evaluador ");
                    headerTitles.Add("Cod.Evaluado ");
                    headerTitles.Add("Nombre Evaluado ");

                    headerTitles.Add("Cod.Priorizada ");
                    headerTitles.Add("Nombre Priorizada ");
                    headerTitles.Add("Var.Considerar ");

                    headerTitles.Add("Período ");
                    headerTitles.Add("Tipo Diálogo ");
                    headerTitles.Add("País ");
                    break;
                case "AntCom":
                    headerTitles.Add("Cod.Evaluador ");
                    headerTitles.Add("Nombre Evaluador ");
                    headerTitles.Add("Cod.Evaluado ");
                    headerTitles.Add("Nombre Evaluado ");

                    headerTitles.Add("Competencia ");
                    headerTitles.Add("Comportamiento ");
                    headerTitles.Add("Sugerencia ");
                    headerTitles.Add("Observación ");

                    headerTitles.Add("Período ");
                    headerTitles.Add("Tipo Diálogo ");
                    headerTitles.Add("País ");
                    break;
                case "DurNeg":
                    headerTitles.Add("CodEvaluador ");
                    headerTitles.Add("NombreEvaluador ");
                    headerTitles.Add("CodEvaluado ");
                    headerTitles.Add("NombreEvaluado ");

                    headerTitles.Add("Var.Enfoque ");
                    headerTitles.Add("Var.Causales ");
                    headerTitles.Add("Zonas/Secciones ");
                    headerTitles.Add("Plan Negocio ");

                    headerTitles.Add("Período ");
                    headerTitles.Add("Tipo Diálogo ");
                    headerTitles.Add("País ");
                    break;
                case "DurEqu":
                    headerTitles.Add("Cod.Evaluador ");
                    headerTitles.Add("Nombre Evaluador ");
                    headerTitles.Add("Cod.Evaluado ");
                    headerTitles.Add("Nombre Evaluado ");

                    headerTitles.Add("Cod.Priorizada ");
                    headerTitles.Add("Nombre Priorizada ");
                    headerTitles.Add("Plan Acción ");

                    headerTitles.Add("Período ");
                    headerTitles.Add("Tipo Diálogo ");
                    headerTitles.Add("País ");
                    break;
                case "DurCom":
                    headerTitles.Add("CodEvaluador ");
                    headerTitles.Add("NombreEvaluador ");
                    headerTitles.Add("CodEvaluado ");
                    headerTitles.Add("NombreEvaluado ");

                    headerTitles.Add("Competencia ");
                    headerTitles.Add("Retroalimentación ");
                    headerTitles.Add("Respuesta ");

                    headerTitles.Add("Período ");
                    headerTitles.Add("Tipo Diálogo ");
                    headerTitles.Add("País ");
                    break;
            }

            return headerTitles;
        }

        private static string TituloReporte(string tipoReporte)
        {
            var tituloReporte = String.Empty;

            switch (tipoReporte)
            {
                case "AntNeg":
                    tituloReporte = "Antes Negocio";
                    break;
                case "AntEqu":
                    tituloReporte = "Antes Equipo";
                    break;
                case "AntCom":
                    tituloReporte = "Antes Competencia";
                    break;
                case "DurNeg":
                    tituloReporte = "Durante Negocio";
                    break;
                case "DurEqu":
                    tituloReporte = "Durante Equipo";
                    break;
                case "DurCom":
                    tituloReporte = "Durante Competencia";
                    break;
            }

            return tituloReporte;

        }


    }
}