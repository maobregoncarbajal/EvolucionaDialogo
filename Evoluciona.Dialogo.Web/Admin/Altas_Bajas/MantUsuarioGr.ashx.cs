
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
    public class MantUsuarioGr : IHttpHandler
    {
        #region Variables

        private readonly BlGerenteRegion _oBlGr = new BlGerenteRegion();
        private readonly BlAltas _oBlAltas = new BlAltas();

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
                case "loadPaises":
                    LoadPaises(context);
                    break;
                case "loadRegiones":
                    LoadRegiones(context);
                    break;
                case "validaCodGr":
                    ValidaCodGr(context);
                    break;
                case "validaCub":
                    ValidaCubGr(context);
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
                var oListGr = ListaGr(context);

                context.Response.Write(JsonConvert.SerializeObject(oListGr));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private List<BeGerenteRegion> ListaGr(HttpContext context)
        {
            var pais = context.Request["pais"];
            var oListGr = _oBlGr.ListaGr(pais);

            return oListGr;
        }

        private void DelJqGrid(HttpContext context)
        {
            var intId = context.Request["IntID"];
            var estado = _oBlGr.DeleteGr(Int32.Parse(intId));
            var respuesta = estado ? "Registro retirado con éxito" : "";

            _oBlAltas.ActualizarEstandarizacionCodigo();

            context.Response.Write(respuesta);
        }

        private void AddJqGrid(HttpContext context)
        {
            var obj = new BeGerenteRegion
            {
                ChrCodigoGerenteRegion = context.Request["ChrCodigoGerenteRegion"],
                ChrPrefijoIsoPais = context.Request["ChrPrefijoIsoPais"],
                VchNombrecompleto = context.Request["VchNombrecompleto"],
                VchCorreoElectronico = context.Request["VchCorreoElectronico"],
                VchCUBGR = context.Request["VchCUBGR"],
                ChrCodigoPlanilla = context.Request["ChrCodigoPlanilla"],
                VchCodigoRegion = context.Request["VchCodigoRegion"],
                VchObservacion = context.Request["VchObservacion"]
            };

            var estado = _oBlGr.AddGr(obj);
            var respuesta = estado ? "Registro ingresado correctamente" : "";
            _oBlAltas.ActualizarEstandarizacionCodigo();

            context.Response.Write(respuesta);

        }

        private void EditJqGrid(HttpContext context)
        {
            var obj = new BeGerenteRegion
            {
                IntIDGerenteRegion = Int32.Parse(context.Request["IntIDGerenteRegion"]),
                ChrCodigoGerenteRegion = context.Request["ChrCodigoGerenteRegion"],
                ChrPrefijoIsoPais = context.Request["ChrPrefijoIsoPais"],
                VchNombrecompleto = context.Request["VchNombrecompleto"],
                VchCorreoElectronico = context.Request["VchCorreoElectronico"],
                VchCUBGR = context.Request["VchCUBGR"],
                ChrCodigoPlanilla = context.Request["ChrCodigoPlanilla"],
                VchCodigoRegion = context.Request["VchCodigoRegion"],
                VchObservacion = context.Request["VchObservacion"]
            };

            var estado = _oBlGr.EditGr(obj);
            var respuesta = estado ? "Registro actualizado correctamente" : "";

            _oBlAltas.ActualizarEstandarizacionCodigo();

            context.Response.Write(respuesta);

        }

        private void ExportJqGrid(HttpContext context)
        {

            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Admin/Altas_Bajas/Temp/");
            const string tipo = "xls";

            var entidades = ListaGr(context);

            var data = Utils.ConvertToDataTable(entidades);

            data.Columns.Remove("bitEstado");
            data.Columns.Remove("intUsuarioCrea");
            data.Columns.Remove("chrCampaniaRegistro");
            data.Columns.Remove("chrIndicadorMigrado");
            data.Columns.Remove("chrCampaniaBaja");
            data.Columns.Remove("IdAndCodigoGerenteRegion");
            data.Columns.Remove("CodigoGerenteRegionForDatamart");
            data.Columns.Remove("obePais");
            data.Columns.Remove("EstadoGerente");
            data.Columns.Remove("FechaActualizacion");
            data.Columns.Remove("codZona");
            data.Columns.Remove("FechaBaja");
            data.Columns.Remove("DescripcionRegion");
            data.Columns.Remove("AnioCampana");
            data.Columns.Remove("CodPais");
            data.Columns.Remove("CodRegion");
            data.Columns.Remove("CodGerenteRegional");
            data.Columns.Remove("DesGerenteRegional");
            data.Columns.Remove("DocIdentidad");
            data.Columns.Remove("CorreoElectronico");
            data.Columns.Remove("EstadoCamp");
            data.Columns.Remove("PtoRankingProdCamp");
            data.Columns.Remove("Periodo");
            data.Columns.Remove("EstadoPeriodo");
            data.Columns.Remove("PtoRankingProdPeriodo");
            data.Columns.Remove("FechaUltAct");
            data.Columns.Remove("FlagProceso");
            data.Columns.Remove("FlagControl");
            data.Columns.Remove("FlagControl_CSFyGH");
            data.Columns.Remove("ChrCodigoDataMart");
            data.Columns.Remove("ChrNombreCodDirectorVenta");
            data.Columns.Remove("IntIDGerenteRegion");
            data.Columns.Remove("obeDirectoraVentas");
            data.Columns.Remove("ChrCodDirectorVenta");
            
            data.Columns["ChrPrefijoIsoPais"].SetOrdinal(0);
            data.Columns["ChrCodigoGerenteRegion"].SetOrdinal(1);
            data.Columns["VchNombrecompleto"].SetOrdinal(2);
            data.Columns["VchCorreoElectronico"].SetOrdinal(3);
            data.Columns["VchCUBGR"].SetOrdinal(4);
            data.Columns["ChrCodigoPlanilla"].SetOrdinal(5);
            data.Columns["VchCodigoRegion"].SetOrdinal(6);
            data.Columns["NombreDirectoraVentas"].SetOrdinal(7);
            data.Columns["VchObservacion"].SetOrdinal(8);


            var fileName = string.Format("{0}.{1}", "Gerente_Region" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();

            data.TableName = "Gerente Region"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos
            headerTitles.Add("País ");
            headerTitles.Add("Doc. Identidad ");
            headerTitles.Add("Nombre Completo ");
            headerTitles.Add("Correo Electrónico ");
            headerTitles.Add("CUB ");
            headerTitles.Add("C. Planilla ");
            headerTitles.Add("C. Region ");
            headerTitles.Add("Directora Venta ");
            headerTitles.Add("Observación ");

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



        private void LoadRegiones(HttpContext context)
        {
            try
            {
                var oListReg = ListaRegiones(context);
                context.Response.Write(JsonConvert.SerializeObject(oListReg));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private List<BeRegion> ListaRegiones(HttpContext context)
        {
            var pais = context.Request["pais"];
            var oListRegion = _oBlGr.ListarRegiones(pais);

            return oListRegion;
        }


        private void LoadPaises(HttpContext context)
        {
            try
            {
                var oListPais = ListaPaises(context);
                context.Response.Write(JsonConvert.SerializeObject(oListPais));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private List<BeComun> ListaPaises(HttpContext context)
        {
            var pais = context.Request["pais"];
            var oListPais = _oBlGr.ListarPaises(pais);

            return oListPais;
        }

        private void ValidaCodGr(HttpContext context)
        {
            var pais = context.Request["pais"];
            var region = context.Request["region"];
            var codGz = context.Request["codGr"];

            var cantCodGz = _oBlGr.ValidaCodGr(pais, region, codGz);


            context.Response.Write(JsonConvert.SerializeObject(cantCodGz));
        }


        private void ValidaCubGr(HttpContext context)
        {
            var pais = context.Request["pais"];
            var region = context.Request["region"];
            var cub = context.Request["cub"];

            var cantCub = _oBlGr.ValidaCubGr(pais, region, cub);


            context.Response.Write(JsonConvert.SerializeObject(cantCub));
        }




        #endregion
    }
}
