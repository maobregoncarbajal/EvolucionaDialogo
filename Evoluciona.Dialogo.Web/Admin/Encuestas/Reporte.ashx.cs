
namespace Evoluciona.Dialogo.Web.Admin.Encuestas
{
    using BusinessEntity;
    using BusinessLogic;
    using CarlosAg.ExcelXmlWriter;
    using Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.Services;
    using Web.Matriz.Helpers;
    using Web.Matriz.Helpers.Excel;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Reporte1 : IHttpHandler
    {
        #region Variables

        private readonly BlEncuestaReporte _oBlEd = new BlEncuestaReporte();

        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "load":
                    LoadJQGrid(context);
                    break;
                case "export":
                    ExportJQGrid(context);
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
            List<BeEncuestaReporte> oListEncuestaDialogo = new List<BeEncuestaReporte>();

            try
            {
                oListEncuestaDialogo = ListaEncuestaDialogo(context);

                context.Response.Write(JsonConvert.SerializeObject(oListEncuestaDialogo));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private List<BeEncuestaReporte> ListaEncuestaDialogo(HttpContext context)
        {
            List<BeEncuestaReporte> oListEncuestaDialogo = new List<BeEncuestaReporte>();


            oListEncuestaDialogo = _oBlEd.ListaEncuestaReporte();

            return oListEncuestaDialogo;
        }

        private void ExportJQGrid(HttpContext context)
        {

            string id = Guid.NewGuid().ToString();
            string strFolder = context.Server.MapPath(@"~/Admin/Ftp/Temp/");
            string tipo = "xls";

            List<BeEncuestaReporte> entidades = ListaEncuestaDialogo(context);

            DataTable data = Utils.ConvertToDataTable(entidades);
            data.Columns.Remove("IdEncuestaRespuestaDialogo");

            string fileName = string.Format("{0}.{1}", "Reporte_Encuesta" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            string strFilePath = strFolder + id + fileName;

            List<string> headerTitles = new List<string>();

            data.TableName = "Reporte_Encuesta"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos
            headerTitles.Add("Periodo ");
            headerTitles.Add("Tipo Encuesta ");
            headerTitles.Add("Pais ");
            headerTitles.Add("Rol ");
            headerTitles.Add("Codigo Usuario ");
            headerTitles.Add("Cub ");
            headerTitles.Add("Preguntas ");
            headerTitles.Add("Comentario ");
            headerTitles.Add("Puntaje Preguntas ");
            headerTitles.Add("Puntaje Encuesta ");
            HeaderTitle(headerTitles, ref data);



            ExcelDatasetWriter edw = new ExcelDatasetWriter();
            DataSet ds = new DataSet();
            ds.Tables.Add(data);
            Workbook book = edw.CreateWorkbook(ds);
            book.Save(strFilePath);
            byte[] archivo = MatrizHelper.ReadFile(strFilePath);
            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            context.Response.ContentType = "application/" + tipo;
            context.Response.BinaryWrite(archivo);
            context.Response.Flush();
            MatrizHelper.DeleteFile(strFilePath);
            context.Response.End();

        }

        private void HeaderTitle(List<string> titulos, ref DataTable dt)
        {
            int i = 0;
            foreach (string titulo in titulos)
            {
                dt.Columns[i].ColumnName = titulo;
                i++;
            }
        }

        #endregion
    }
}
