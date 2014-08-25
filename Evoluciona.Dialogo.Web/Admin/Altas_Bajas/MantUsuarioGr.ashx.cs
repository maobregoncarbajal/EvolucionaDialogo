
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
                case "loadPais":
                    LoadPais(context);
                    break;
                case "loadRegion":
                    LoadRegion(context);
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
            var ids = intId.Split(',');
            var idsNoDel = (from id in ids let estado = _oBlGr.DeleteGr(Int32.Parse(id)) where !estado select id).Aggregate("", (current, id) => current + id + ",");

            context.Response.Write(!String.IsNullOrEmpty(idsNoDel)
                ? JsonConvert.SerializeObject("No se pudo eliminar las filas" + idsNoDel)
                : JsonConvert.SerializeObject(""));
        }

        private void AddJqGrid(HttpContext context)
        {
            var obj = new BeGerenteRegion
            {
                ChrCodigoGerenteRegion = context.Request["ChrCodigoGerenteRegion"],
                ChrPrefijoIsoPais = context.Request["ChrPrefijoIsoPais"],
                VchNombrecompleto = context.Request["VchNombrecompleto"],
                VchCorreoElectronico = context.Request["VchCorreoElectronico"],
                ChrCodDirectorVenta = context.Request["ChrCodDirectorVenta"],
                VchCUBGR = context.Request["VchCUBGR"],
                ChrCodigoPlanilla = context.Request["ChrCodigoPlanilla"],
                VchCodigoRegion = context.Request["VchCodigoRegion"],
                VchObservacion = context.Request["VchObservacion"]
            };

            var estado = _oBlGr.AddGr(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        private void EditJqGrid(HttpContext context)
        {
            var obj = new BeGerenteRegion
            {
                IntIDGerenteRegion = Int32.Parse(context.Request["IntID"]),
                ChrCodigoGerenteRegion = context.Request["ChrCodigoGerenteRegion"],
                ChrPrefijoIsoPais = context.Request["ChrPrefijoIsoPais"],
                VchNombrecompleto = context.Request["VchNombrecompleto"],
                VchCorreoElectronico = context.Request["VchCorreoElectronico"],
                ChrCodDirectorVenta = context.Request["ChrCodDirectorVenta"],
                VchCUBGR = context.Request["VchCUBGR"],
                ChrCodigoPlanilla = context.Request["ChrCodigoPlanilla"],
                VchCodigoRegion = context.Request["VchCodigoRegion"],
                VchObservacion = context.Request["VchObservacion"]
            };

            var estado = _oBlGr.EditGr(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }

        private void ExportJqGrid(HttpContext context)
        {

            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Admin/Altas_Bajas/Temp/");
            const string tipo = "xls";

            var entidades = ListaGr(context);

            var data = Utils.ToDataTable(entidades);

            data.Columns.Remove("bitEstado");
            data.Columns.Remove("intUsuarioCrea");
            data.Columns.Remove("obeDirectoraVentas");
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


            data.Columns["ChrCodigoGerenteRegion"].SetOrdinal(0);
            data.Columns["ChrPrefijoIsoPais"].SetOrdinal(1);
            data.Columns["VchNombrecompleto"].SetOrdinal(2);
            data.Columns["VchCorreoElectronico"].SetOrdinal(3);
            data.Columns["ChrCodDirectorVenta"].SetOrdinal(4);
            data.Columns["VchCUBGR"].SetOrdinal(5);
            data.Columns["ChrCodigoPlanilla"].SetOrdinal(6);
            data.Columns["VchCodigoRegion"].SetOrdinal(7);
            data.Columns["VchObservacion"].SetOrdinal(8);


            var fileName = string.Format("{0}.{1}", "Gerente_Region" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();

            data.TableName = "Gerente Region"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos
            headerTitles.Add("C. G. Region ");
            headerTitles.Add("Pais ");
            headerTitles.Add("Nombre Completo ");
            headerTitles.Add("Correo Electronico ");
            headerTitles.Add("C. Director Venta ");
            headerTitles.Add("CUB ");
            headerTitles.Add("C. Planilla ");
            headerTitles.Add("C. Region ");
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



        private void LoadRegion(HttpContext context)
        {
            var cadena = string.Empty; //"<select>";
            const string puntoComa = ","; //"</select>";
            var cont = 0;

            try
            {
                var oListReg = ListaRegion(context);
                var countList = oListReg.Count;
                var pais = context.Request["pais"];

                foreach (var reg in oListReg)
                {
                    cont++;

                    var cod = reg.Codigo;
                    var des = reg.Descripcion;
                    cadena = cadena + pais + cod + ":" + des;

                    if (cont != countList)
                    {
                        cadena = cadena + puntoComa;
                    }
                }
                context.Response.Write(JsonConvert.SerializeObject(cadena));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private List<BeComun> ListaRegion(HttpContext context)
        {
            var pais = context.Request["pais"];

            var oListRegion = _oBlGr.ListarRegiones(pais);

            return oListRegion;
        }


        private void LoadPais(HttpContext context)
        {
            try
            {
                var oListPais = ListaPais(context);
                context.Response.Write(JsonConvert.SerializeObject(oListPais));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private List<BeComun> ListaPais(HttpContext context)
        {
            var pais = context.Request["pais"];

            var oListPais = _oBlGr.ListarPaises(pais);

            return oListPais;
        }






        #endregion
    }
}
