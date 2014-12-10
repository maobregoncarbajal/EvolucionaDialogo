
using System.Web.Script.Serialization;

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
    public class MantUsuarioGz : IHttpHandler
    {
        #region Variables

        private readonly BlGerenteZona _oBlGz = new BlGerenteZona();
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
                case "loadZonas":
                    LoadZonas(context);
                    break;
                case "validaCodGz":
                    ValidaCodGz(context);
                    break;
                case "validaCub":
                    ValidaCubGz(context);
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
                var resultadoPaginado = ListaGzPaginacion(context);
                context.Response.Write(JsonConvert.SerializeObject(resultadoPaginado));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeGerenteZona> ListaGz(HttpContext context)
        {
            var pais = context.Request["pais"];
            var oListGz = _oBlGz.ListaGz(pais);
            return oListGz;
        }



        private BeGerenteZonaPaginacion ListaGzPaginacion(HttpContext context)
        {
            var serializer = new JavaScriptSerializer();

            var request = context.Request;

            var pais = request["pais"];
            var numberOfRows = Convert.ToInt32(request["rows"]);
            var pageIndex = Convert.ToInt32(request["page"]);
            var sortColumnName = request["sidx"];
            var sortOrderBy = request["sord"];
            var isSearch = request["_search"];
            var searchField = request["searchField"];
            var searchString = request["searchString"];
            var searchOper = request["searchOper"];
            var filters = request["filters"];

            var buscar = isSearch != null && isSearch == "true";
            List<BeGerenteZona> oListGz;
            int total;


            if (buscar)
            {
                if (!String.IsNullOrEmpty(filters))
                {
                    var whereString = BlJqGridWhereClauseGenerator.ParseFilter(serializer.Deserialize<BeJqGridFilter>(filters));
                    oListGz = _oBlGz.ListaGzPaginacionBusquedaAvanzada(whereString.ToString(), sortColumnName, sortOrderBy, pais, (pageIndex - 1), numberOfRows);
                    total = _oBlGz.GetTotalBusquedaAvanzada(whereString.ToString(), pais);
                }
                else
                {
                    oListGz = _oBlGz.ListaGzPaginacionBusqueda(searchField, searchString, searchOper, sortColumnName, sortOrderBy, pais, (pageIndex - 1), numberOfRows);
                    total = _oBlGz.GetTotalBusqueda(pais, searchField, searchString, searchOper);
                }
            }
            else
            {
                oListGz = _oBlGz.ListaGzPaginacion(sortColumnName, sortOrderBy, pais, pageIndex - 1, numberOfRows);
                total = _oBlGz.GetTotal(pais);
            }

            //luego a blGerenteZona
            var respuesta1 = new BeGerenteZonaPaginacion
            {
                rows = oListGz,
                records = total,
                page = pageIndex,
                total = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(total) / Convert.ToDouble(numberOfRows)))
            };

            return respuesta1;
        }

        private void DelJqGrid(HttpContext context)
        {
            var intId = context.Request["IntID"];
            var estado = _oBlGz.DeleteGz(Int32.Parse(intId));
            var respuesta = estado ? "Registro retirado con éxito" : "";

            _oBlAltas.ActualizarEstandarizacionCodigo();

            context.Response.Write(respuesta);
        }

        private void AddJqGrid(HttpContext context)
        {
            var obj = new BeGerenteZona
            {
                chrPrefijoIsoPais = context.Request["chrPrefijoIsoPais"].Trim(),
                chrCodigoGerenteZona = context.Request["chrCodigoGerenteZona"].Trim(),
                vchNombreCompleto = context.Request["vchNombreCompleto"].Trim(),
                vchCorreoElectronico = context.Request["vchCorreoElectronico"].Trim(),
                vchCUBGZ = context.Request["vchCUBGZ"].Trim(),
                chrCodigoPlanilla = context.Request["chrCodigoPlanilla"].Trim(),
                vchCodigoRegion = context.Request["vchCodigoRegion"].Trim(),
                vchCodigoZona = context.Request["vchCodigoZona"].Trim(),
                vchObservacion = context.Request["vchObservacion"].Trim()
            };

            var estado = _oBlGz.AddGr(obj);
            var respuesta = estado ? "Registro ingresado correctamente" : "";

            _oBlAltas.ActualizarEstandarizacionCodigo();

            context.Response.Write(respuesta);


        }

        private void EditJqGrid(HttpContext context)
        {
            var obj = new BeGerenteZona
            {
                intIDGerenteZona = Int32.Parse(context.Request["intIDGerenteZona"]),
                chrPrefijoIsoPais = context.Request["chrPrefijoIsoPais"].Trim(),
                chrCodigoGerenteZona = context.Request["chrCodigoGerenteZona"].Trim(),
                vchNombreCompleto = context.Request["vchNombreCompleto"].Trim(),
                vchCorreoElectronico = context.Request["vchCorreoElectronico"].Trim(),
                vchCUBGZ = context.Request["vchCUBGZ"].Trim(),
                chrCodigoPlanilla = context.Request["chrCodigoPlanilla"].Trim(),
                vchCodigoRegion = context.Request["vchCodigoRegion"].Trim(),
                vchCodigoZona = context.Request["vchCodigoZona"].Trim(),
                vchObservacion = context.Request["vchObservacion"].Trim()
            };

            var estado = _oBlGz.EditGz(obj);
            var respuesta = estado ? "Registro actualizado correctamente" : "";

            _oBlAltas.ActualizarEstandarizacionCodigo();

            context.Response.Write(respuesta);

        }

        private void ExportJqGrid(HttpContext context)
        {

            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Admin/Altas_Bajas/Temp/");
            const string tipo = "xls";

            var entidades = ListaGz(context);

            var data = Utils.ConvertToDataTable(entidades);

            data.Columns.Remove("intIDGerenteZona");
            data.Columns.Remove("bitEstado");
            data.Columns.Remove("intUsuarioCrea");
            data.Columns.Remove("datFechaCrea");
            data.Columns.Remove("intUsuarioModi");
            data.Columns.Remove("datFechaModi");
            data.Columns.Remove("chrCodigoDataMart");
            data.Columns.Remove("chrCampaniaRegistro");
            data.Columns.Remove("chrIndicadorMigrado");
            data.Columns.Remove("chrCampaniaBaja");
            data.Columns.Remove("obeGerenteRegion");
            data.Columns.Remove("CodigoGerenteRegion");
            data.Columns.Remove("obePais");
            data.Columns.Remove("EstadoGerente");
            data.Columns.Remove("FechaActualizacion");
            data.Columns.Remove("FechaBaja");
            data.Columns.Remove("DescripcionRegion");
            data.Columns.Remove("DescripcionZona");
            data.Columns.Remove("CUBGZ");
            data.Columns.Remove("AnioCampana");
            data.Columns.Remove("CodPais");
            data.Columns.Remove("CodRegion");
            data.Columns.Remove("CodGerenteRegional");
            data.Columns.Remove("codZona");
            data.Columns.Remove("CodGerenteZona");
            data.Columns.Remove("DesGerenteZona");
            data.Columns.Remove("DocIdentidad");
            data.Columns.Remove("CorreoElectronico");
            data.Columns.Remove("EstadoCamp");
            data.Columns.Remove("PtoRankingProdPeriodo");
            data.Columns.Remove("FlagProceso");
            data.Columns.Remove("FlagControl");
            data.Columns.Remove("FlagControl_CSFyGH");
            data.Columns.Remove("PtoRankingProdCamp");
            data.Columns.Remove("Periodo");
            data.Columns.Remove("EstadoPeriodo");
            data.Columns.Remove("FechaUltAct");
            data.Columns.Remove("IntIDGerenteRegion");
            data.Columns.Remove("VchCUBGR");


            data.Columns["chrPrefijoIsoPais"].SetOrdinal(0);
            data.Columns["chrCodigoGerenteZona"].SetOrdinal(1);
            data.Columns["vchNombreCompleto"].SetOrdinal(2);
            data.Columns["vchCorreoElectronico"].SetOrdinal(3);
            data.Columns["vchCUBGZ"].SetOrdinal(4);
            data.Columns["chrCodigoPlanilla"].SetOrdinal(5);
            data.Columns["vchCodigoRegion"].SetOrdinal(6);
            data.Columns["vchCodigoZona"].SetOrdinal(7);
            data.Columns["NombreGerenteRegion"].SetOrdinal(8);
            data.Columns["vchObservacion"].SetOrdinal(9);

            var fileName = string.Format("{0}.{1}", "Gerente_Zonas" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();

            data.TableName = "Gerente_Zonas"; //titulo

            data.Namespace = ""; // texto filtros

            //Crear Titulos

            //headerTitles.Add("ID GerenteRegion ");
            headerTitles.Add("País ");
            headerTitles.Add("Doc. Identidad ");
            headerTitles.Add("Nombre Completo ");
            headerTitles.Add("Correo Electrónico ");
            //headerTitles.Add("CUBGR ");
            headerTitles.Add("CUB ");
            headerTitles.Add("C. Planilla");
            headerTitles.Add("C. Región");
            headerTitles.Add("C. Zona");
            headerTitles.Add("G. Región");
            headerTitles.Add("Observación");

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


        private void HeaderTitle(IEnumerable<string> titulos, ref DataTable dt)
        {
            var i = 0;
            foreach (var titulo in titulos)
            {
                dt.Columns[i].ColumnName = titulo;
                i++;
            }
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
            var oListPais = _oBlGz.ListarPaises(pais);

            return oListPais;
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
            var oListRegion = _oBlGz.ListarRegiones(pais);

            return oListRegion;
        }

        private void LoadZonas(HttpContext context)
        {
            try
            {
                var oListReg = ListaZonas(context);
                context.Response.Write(JsonConvert.SerializeObject(oListReg));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private List<BeZona> ListaZonas(HttpContext context)
        {
            var pais = context.Request["pais"];
            var region = context.Request["region"];
            var oListZona = _oBlGz.ListarZonas(pais,region);

            return oListZona;
        }

        private void ValidaCodGz(HttpContext context)
        {
            var pais = context.Request["pais"];
            var region = context.Request["region"];
            var zona = context.Request["zona"];
            var codGz = context.Request["codGz"];

            var cantCodGz = _oBlGz.ValidaCodGz(pais, region, zona, codGz);


            context.Response.Write(JsonConvert.SerializeObject(cantCodGz));
        }


        private void ValidaCubGz(HttpContext context)
        {
            var pais = context.Request["pais"];
            var region = context.Request["region"];
            var zona = context.Request["zona"];
            var cub = context.Request["cub"];

            var cantCub = _oBlGz.ValidaCubGz(pais, region, zona, cub);


            context.Response.Write(JsonConvert.SerializeObject(cantCub));
        }


        #endregion
    }
}


