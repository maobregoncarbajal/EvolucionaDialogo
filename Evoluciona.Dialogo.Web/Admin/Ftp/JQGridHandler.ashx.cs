
namespace Evoluciona.Dialogo.Web.Admin.Ftp
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
    public class JQGridHandler : IHttpHandler
    {
        #region Variables
        private readonly BlFfvvBase _ffvvBase = new BlFfvvBase();
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
                case "sinc":
                    SincJQGrid(context);
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
            List<BeFfvvBase> entidades = new List<BeFfvvBase>();

            try
            {
                entidades = Lista_in_ffvv_base(context);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private List<BeFfvvBase> Lista_in_ffvv_base(HttpContext context)
        {
            List<BeFfvvBase> entidades = _ffvvBase.Lista_in_ffvv_base();

            return entidades;
        }


        private void DelJQGrid(HttpContext context)
        {
            string IntID = context.Request["IntID"];
            string[] Ids = IntID.Split(',');
            bool estado = false;
            string IdsNoDel = "";

            foreach (string Id in Ids)
            {
                estado = _ffvvBase.Delete_in_ffvv_base(Int32.Parse(Id));

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
            BeFfvvBase obj = new BeFfvvBase();

            obj.Cub = context.Request["Cub"];
            obj.DocIdentidad = context.Request["DocIdentidad"];
            obj.Planilla = context.Request["Planilla"];
            obj.CodRol = context.Request["CodRol"];
            obj.DesRol = context.Request["DesRol"];
            obj.Nombres = context.Request["Nombres"];
            obj.ApePaterno = context.Request["ApePaterno"];
            obj.ApeMaterno = context.Request["ApeMaterno"];
            obj.CodPais = context.Request["CodPais"];
            obj.CodRegion = context.Request["CodRegion"];
            obj.CodZona = context.Request["CodZona"];
            obj.Email = context.Request["Email"];
            obj.Sexo = context.Request["Sexo"];
            obj.Estado = context.Request["Estado"];
            obj.CubJefe = context.Request["CubJefe"];
            obj.PlanillaJefe = context.Request["PlanillaJefe"];

            bool estado = _ffvvBase.Add_in_ffvv_base(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        private void EditJQGrid(HttpContext context)
        {
            BeFfvvBase obj = new BeFfvvBase();

            obj._id = Int32.Parse(context.Request["IntID"]);
            obj.Cub = context.Request["Cub"];
            obj.DocIdentidad = context.Request["DocIdentidad"];
            obj.Planilla = context.Request["Planilla"];
            obj.CodRol = context.Request["CodRol"];
            obj.DesRol = context.Request["DesRol"];
            obj.Nombres = context.Request["Nombres"];
            obj.ApePaterno = context.Request["ApePaterno"];
            obj.ApeMaterno = context.Request["ApeMaterno"];
            obj.CodPais = context.Request["CodPais"];
            obj.CodRegion = context.Request["CodRegion"];
            obj.CodZona = context.Request["CodZona"];
            obj.Email = context.Request["Email"];
            obj.Sexo = context.Request["Sexo"];
            obj.Estado = context.Request["Estado"];
            obj.CubJefe = context.Request["CubJefe"];
            obj.PlanillaJefe = context.Request["PlanillaJefe"];

            bool estado = _ffvvBase.Edit_in_ffvv_base(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        private void ExportJQGrid(HttpContext context)
        {

            string id = Guid.NewGuid().ToString();
            string strFolder = context.Server.MapPath(@"~/Admin/Ftp/Temp/");
            string tipo = "xls";

            List<BeFfvvBase> entidades = Lista_in_ffvv_base(context);



            DataTable data = Utils.ToDataTable(entidades);

            string fileName = string.Format("{0}.{1}", "FFVV_base_evoluciona" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            string strFilePath = strFolder + id + fileName;

            List<string> headerTitles = new List<string>();

            data.TableName = "FFVV_base_evoluciona"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos
            headerTitles.Add("Id ");
            headerTitles.Add("CUB ");
            headerTitles.Add("D.Identidad ");
            headerTitles.Add("C.Planilla ");
            headerTitles.Add("C.Rol ");
            headerTitles.Add("D.Rol ");
            headerTitles.Add("Nombres ");
            headerTitles.Add("A.Paterno ");
            headerTitles.Add("A.Materno ");
            headerTitles.Add("Pais ");
            headerTitles.Add("Region ");
            headerTitles.Add("Zona ");
            headerTitles.Add("Email ");
            headerTitles.Add("Sexo ");
            headerTitles.Add("Estado ");
            headerTitles.Add("C.CUB Jefe");
            headerTitles.Add("C. Planilla Jefe");
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




        private void SincJQGrid(HttpContext context)
        {
            BlCargaAdam obj = new BlCargaAdam();

            string dts = "ESE_Evoluciona_Sincronizacion";

            bool estado = obj.CargarArchivoAdam(dts);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }


        #endregion

    }
}
