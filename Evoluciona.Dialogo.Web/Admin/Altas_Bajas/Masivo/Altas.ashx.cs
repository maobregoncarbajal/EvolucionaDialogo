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

namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas.Masivo
{
    /// <summary>
    /// Descripción breve de Altas1
    /// </summary>
    public class Altas1 : IHttpHandler
    {
        #region Variables

        private readonly BlAltas _oBlAltas = new BlAltas();

        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "load":
                    LoadJqGrid(context);
                    break;
                case "altas":
                    AltasJqGrid(context);
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

        #region JqGrid

        private void LoadJqGrid(HttpContext context)
        {
            try
            {
                var oList = ListaAltas(context);
                context.Response.Write(JsonConvert.SerializeObject(oList));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeAltas> ListaAltas(HttpContext context)
        {
            var pais = context.Request["pais"];
            var oList = _oBlAltas.ListaAltas(pais);
            return oList;
        }

        private void AltasJqGrid(HttpContext context)
        {
            var lista = context.Request["listaAltas"];
            var altas = JsonConvert.DeserializeObject<List<BeAltas>>(lista);
            var varBool = false;

            foreach (var beAltase in altas)
            {
                varBool = _oBlAltas.InsertarAltas(beAltase);

                if (varBool.Equals(false)) break;
            }

            _oBlAltas.ActualizarEstandarizacionCodigo();
            context.Response.Write(JsonConvert.SerializeObject(varBool));
        }


        private void ExportJqGrid(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Admin/Altas_Bajas/Temp/");
            const string tipo = "xls";

            var entidades = ListaAltas(context);

            var data = Utils.ConvertToDataTable(entidades);

            data.Columns.Remove("CodigoPaisComercial");
            data.Columns.Remove("DirCodRegion");
            data.Columns.Remove("DircodZona");

            var fileName = string.Format("{0}.{1}", "AltasMasivas" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();

            data.TableName = "AltasMasivas"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos
            headerTitles.Add("Rol ");
            headerTitles.Add("Nombre Completo ");
            headerTitles.Add("Mail ");
            headerTitles.Add("CUB ");
            headerTitles.Add("Doc Identidad ");
            headerTitles.Add("Cod. Planilla ");
            headerTitles.Add("Pais ");
            headerTitles.Add("Cod. Region ");
            headerTitles.Add("Cod. Zona ");
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

        #endregion
    }
}