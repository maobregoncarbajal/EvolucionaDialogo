
using System.Linq;

namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using BusinessEntity;
    using BusinessLogic;
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
    public class MantUsuarioDv : IHttpHandler
    {
        #region Variables

        private readonly BlDirectoraVentas _oBlDv = new BlDirectoraVentas();

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
                var oListDv = ListaDv(context);

                context.Response.Write(JsonConvert.SerializeObject(oListDv));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private List<BeDirectoraVentas> ListaDv(HttpContext context)
        {
            var pais = context.Request["pais"];
            var oListDv = _oBlDv.ListaDv(pais);

            return oListDv;
        }

        private void DelJqGrid(HttpContext context)
        {
            var intId = context.Request["IntID"];
            var ids = intId.Split(',');
            var idsNoDel = (from id in ids let estado = _oBlDv.DeleteDv(Int32.Parse(id)) where !estado select id).Aggregate("", (current, id) => current + id + ",");

            context.Response.Write(String.IsNullOrEmpty(idsNoDel)
                ? JsonConvert.SerializeObject("")
                : JsonConvert.SerializeObject("No se pudo eliminar las filas" + idsNoDel));
        }

        private void AddJqGrid(HttpContext context)
        {
            var obj = new BeDirectoraVentas
            {
                chrPrefijoIsoPais = context.Request["chrPrefijoIsoPais"],
                chrCodigoDirectoraVentas = context.Request["chrCodigoDirectoraVentas"],
                vchNombreCompleto = context.Request["vchNombreCompleto"],
                vchCorreoElectronico = context.Request["vchCorreoElectronico"],
                vchCUBDV = context.Request["vchCUBDV"],
                chrCodigoPlanilla = context.Request["chrCodigoPlanilla"],
                vchObservacion = context.Request["vchObservacion"]
            };

            var estado = _oBlDv.AddDv(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));
        }

        private void EditJqGrid(HttpContext context)
        {
            var obj = new BeDirectoraVentas
            {
                intIDDirectoraVenta = Int32.Parse(context.Request["IntID"]),
                chrPrefijoIsoPais = context.Request["chrPrefijoIsoPais"],
                chrCodigoDirectoraVentas = context.Request["chrCodigoDirectoraVentas"],
                vchNombreCompleto = context.Request["vchNombreCompleto"],
                vchCorreoElectronico = context.Request["vchCorreoElectronico"],
                vchCUBDV = context.Request["vchCUBDV"],
                chrCodigoPlanilla = context.Request["chrCodigoPlanilla"],
                vchObservacion = context.Request["vchObservacion"]
            };

            var estado = _oBlDv.EditDv(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));
        }

        private void ExportJqGrid(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Admin/Altas_Bajas/Temp/");
            const string tipo = "xls";

            var entidades = ListaDv(context);

            var data = Utils.ToDataTable(entidades);

            data.Columns.Remove("intIDDirectoraVenta");
            data.Columns.Remove("bitEstado");
            data.Columns.Remove("obePais");
            data.Columns.Remove("intUsuarioCrea");
            data.Columns.Remove("datFechaCrea");
            data.Columns.Remove("intUsuarioModi");
            data.Columns.Remove("datFechaModi");
            data.Columns.Remove("vchDocumentoIndentidad");
            data.Columns.Remove("EstadoDirectora");

            var fileName = string.Format("{0}.{1}", "Directora_Ventas" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();

            data.TableName = "Directora_Ventas"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos
            //headerTitles.Add("intIDDirectoraVenta_ ");
            headerTitles.Add("Pais ");
            headerTitles.Add("C. Directora Ventas ");
            headerTitles.Add("Nombre Completo ");
            headerTitles.Add("Correo Electronico ");
            headerTitles.Add("CUB ");
            headerTitles.Add("C. Planilla ");
            headerTitles.Add("Observacion ");
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
