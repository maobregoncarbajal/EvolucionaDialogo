
using System.Globalization;
using System.Linq;

namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using CarlosAg.ExcelXmlWriter;
    using Dialogo.Helpers;
    using Helpers;
    using Helpers.Chart;
    using Helpers.Excel;
    using Helpers.Linq;
    using Helpers.PDF;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using Newtonsoft.Json;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Net.Mail;
    using System.Text;
    using System.Web;
    using System.Web.Services;
    using Web.Helpers;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MatrizReporte : IHttpHandler
    {
        #region Variables

        private readonly BlMatriz _matrizBl = new BlMatriz();
        private readonly BlCronogramaPdM _blCronogramaPdM = new BlCronogramaPdM();
        private static string _filtros;
        private static string _strPathImage;
        private static string _cuadrante;
        private static string _zonaMz;

        #endregion Variables

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "pais":
                    LoadPais(context);
                    break;
                case "anho":
                    LoadAnhos(context);
                    break;
                case "periodo":
                    LaodPeriodos(context);
                    break;
                case "periodosAll":
                    LoadPeriodosAll(context);
                    break;
                case "tipos":
                    LoadTipos(context);
                    break;
                case "regiones":
                    LoadRegiones(context);
                    break;
                case "regionGRporPeriodo":
                    LoadRegionGRporPeriodo(context);
                    break;
                case "zonas":
                    LoadZonas(context);
                    break;
                case "detalleInformacion":
                    DetalleInformacion(context);
                    break;
                case "descargarDetalleInformacion":
                    DescargarDetalleInformacion(context);
                    break;
                case "descargarMatrizConsolidada":
                    DescargarMatrizConsolidada(context);
                    break;
                case "descargarMatrizTalento":
                    DescargarMatrizTalento(context);
                    break;
                case "fichaPersonal":
                    FichaPersonal(context);
                    break;
                case "descargarDetalleInformacionMatrizTalento":
                    DescargarDetalleInformacionMatrizTalento(context);
                    break;
                case "gerenteRegion":
                    LoadGerenteRegion(context);
                    break;
                case "gerenteZona":
                    LoadGerenteZona(context);
                    break;
                case "variablePais":
                    LoadVariablePais(context);
                    break;
                case "verResultado":
                    VerResultado(context);
                    break;
                case "verChart":
                    CrearChart(context);
                    break;
                case "descargarResultado":
                    DescargarResultadoMatriz(context);
                    break;
                case "cuadranteUsuario":
                    CuadranteUsuario(context);
                    break;
                case "loadAllGerentesZonaByZona":
                    LoadAllGerentesZonaByZona(context);
                    break;
                case "validarFechaAcuerdo":
                    ValidarFechaAcuerdo(context);
                    break;
                case "obtenerCampanhaActual":
                    ObtenerCampanhaActual(context);
                    break;
                case "obtenerCampanhas":
                    ObtenerCampanhas(context);
                    break;
                case "obtenerZonasDisponibles":
                    ObtenerZonasDisponibles(context);
                    break;
                case "obtenerRegionesDisponibles":
                    ObtenerRegionesDisponibles(context);
                    break;
                case "insertTomaAccion":
                    InsertarTomaAccion(context);
                    break;
                case "deleteTomaAccion":
                    DeleteTomaAccion(context);
                    break;
                case "updateCalibracion":
                    UpdateCalibracion(context);
                    break;
                case "listarTomaAccion":
                    ListarTomaAccion(context);
                    break;
                case "listarCalibraciones":
                    ListarCalibraciones(context);
                    break;
                case "validarTomaAccion":
                    ValidarTomaAccion(context);
                    break;
                case "lineamientoAcuerdo":
                    LineamientoAcuerdo(context);
                    break;
                case "confirmarTomaAccion":
                    ConfirmarTomaAccion(context);
                    break;
                case "validarPeriodoPorCampanha":
                    ValidarPeriodoPorCampanha(context);
                    break;
                case "verCondiciones":
                    VerCondiciones(context);
                    break;
                case "variablesEnfoque":
                    VariablesEnfoque(context);
                    break;
                case "verResultadoSustento":
                    VerResuldatosSustento(context);
                    break;
                case "verChartSustento":
                    CrearChartSustento(context);
                    break;
                case "paisRegionporPeriodo":
                    ObtenerRegionUsuarioporPeriodo(context);
                    break;
                /*Matriz Zona*/
                case "listarTiposMZ":
                    LoadTiposMZ(context);
                    break;
                case "obtenerTipoMz":
                    ObtenerTipoMz(context);
                    break;
                case "obtenerListaMZ":
                    ObtenerListaMz(context);
                    break;
                case "descargarDetalleInformacionMz":
                    DescargarDetalleInformacionMz(context);
                    break;
                case "descargarMatrizZona":
                    DescargarMatrizZona(context);
                    break;
                case "paisMz":
                    LoadPaisMz(context);
                    break;
                case "regionesMz":
                    LoadRegionesMz(context);
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

        #region "Filtros Matriz"

        private void LoadPais(HttpContext context)
        {
            var tipoAdmin = context.Request["tipoAdmin"];
            var codPaisUsuario = context.Request["codPaisUsuario"];

            var paisBl = new BlPais();
            var paises = new List<BeComun>();

            switch (tipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    paises = paisBl.ObtenerPaisesBeComun();
                    break;
                case Constantes.RolAdminPais:
                    paises.Add(paisBl.ObtenerPaisBeComun(codPaisUsuario));
                    break;
            }

            if (paises.Count > 0)
            {
                paises.Insert(0, string.IsNullOrEmpty(context.Request["select"]) ? new BeComun { Codigo = "00", Descripcion = "Todos" } : new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
            }
            context.Response.Write(JsonConvert.SerializeObject(paises));
        }

        private void LoadAnhos(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var entidades = _matrizBl.ListarAnhos(codPais);

                if (!string.IsNullOrEmpty(context.Request["select"]))
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LaodPeriodos(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var anho = context.Request["anho"];
                var idRol = context.Request["idRol"];
                var entidades = _matrizBl.ListarPeriodos(codPais, anho, Convert.ToInt32(idRol));

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadPeriodosAll(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var anho = context.Request["anho"];
                var idRol = context.Request["idRol"];
                var entidades = _matrizBl.ListarPeriodos(codPais, anho, Convert.ToInt32(idRol));

                var descripcion = string.IsNullOrEmpty(context.Request["select"]) ? "Todos" : "[Seleccionar]";

                entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = descripcion });

                if (!string.IsNullOrEmpty(context.Request["normal"]))
                {
                    foreach (var entidad in entidades)
                    {
                        if (entidad.Codigo != "00")
                            entidad.Descripcion = entidad.Codigo;
                    }
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadTipos(HttpContext context)
        {
            try
            {
                var path = context.Server.MapPath(@"~/Matriz/XML/") + context.Request["fileName"];

                var entidades = Utils.XmlToObjectList<BeComun>(path, "//BeComuns/BeComun");

                var adicional = context.Request["adicional"];

                if (adicional.ToLower() == "si")
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "Todos" });
                }

                if (adicional.ToLower() == "select")
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadRegiones(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var entidades = _matrizBl.ListarRegiones(codPais);

                entidades.Insert(0, context.Request["select"] == null
                                     ? new BeComun { Codigo = "00", Descripcion = "Todos" }
                                     : new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void LoadRegionesMz(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var entidades = _matrizBl.ListarRegionesMz(codPais);

                entidades.Insert(0, context.Request["select"] == null
                                     ? new BeComun { Codigo = "00", Descripcion = "Todos" }
                                     : new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private void LoadRegionGRporPeriodo(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codUsuario = context.Request["codUsuario"];
                var periodo = context.Request["periodo"];

                var entidades = _matrizBl.ListarRegionGRporPeriodo(codPais, codUsuario, periodo);

                entidades.Insert(0, context.Request["select"] == null
                                     ? new BeComun { Codigo = "00", Descripcion = "Todos" }
                                     : new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void LoadZonas(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codRegion = context.Request["codRegion"];
                var entidades = _matrizBl.ListarZonas(codPais, codRegion);

                entidades.Insert(0, string.IsNullOrEmpty(context.Request["select"]) ? new BeComun { Codigo = "00", Descripcion = "Todos" } : new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadGerenteRegion(HttpContext context)
        {
            try
            {
                var codUsuario = context.Request["codUsuario"];
                var seleccionar = context.Request["seleccionar"];

                var entidades = _matrizBl.ListarGerentesRegion(codUsuario);

                if (String.IsNullOrEmpty(seleccionar))
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadGerenteZona(HttpContext context)
        {
            try
            {
                var codUsuario = context.Request["codUsuario"];
                var codPais = context.Request["codPais"];
                var nombre = context.Request["nombre"];

                var entidades = _matrizBl.ListarGerentesZona(codUsuario, codPais, nombre);
                entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadVariablePais(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var entidades = _matrizBl.ListarVariablesPais(codPais);
                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void ObtenerRegionUsuarioporPeriodo(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codUsuario = context.Request["codUsuario"];
                var periodo = context.Request["periodo"];


                var regionUsuarioporPeriodo = string.Format("{0}-{1}", codPais, _matrizBl.ObtenerRegionUsuarioporPeriodo(codPais, codUsuario, periodo).Codigo);
                context.Response.Write(JsonConvert.SerializeObject(regionUsuarioporPeriodo));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion "Filtros Matriz"

        #region DetalleInformacion

        private void DetalleInformacion(HttpContext context)
        {
            try
            {
                var entidades = ListaDetalleInformacion(context);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeDetalleInformacion> ListaDetalleInformacion(HttpContext context)
        {
            var entidades = new List<BeDetalleInformacion>();

            var periodo = context.Request["periodo"];
            var subPeriodo = context.Request["subPeriodo"];
            var region = context.Request["region"];
            var zona = context.Request["zona"];
            var nombreRegion = context.Request["nombreRegion"];
            var nombreZona = context.Request["nombreZona"];
            var codigoUsuario = context.Request["codigoUsuario"];
            var rolEvaluador = context.Request["rolEvaluador"];
            var rolEvaluado = context.Request["rolEvaluado"];
            var pais = context.Request["pais"];

            var subPeriodoAlt = subPeriodo == "00" ? "Todos" : subPeriodo;

            if (rolEvaluado == "2")//GR
            {
                _filtros = " Año: " + periodo.Substring(0, 4) + "  Periodo: " + periodo + "  SubPeriodo : " + subPeriodoAlt + "  Región : " + nombreRegion;
            }

            if (rolEvaluado == "3")//GZ
            {
                _filtros = " Año: " + periodo.Substring(0, 4) + "  Periodo: " + periodo + "  SubPeriodo : " + subPeriodoAlt + "  Región :" + nombreRegion + "  Zona: " + nombreZona;
            }

            var path = context.Server.MapPath(@"~/Matriz/XML/");

            var niveles = Utils.XmlToObjectList<BeComun>(path + "Competencia.xml", "//BeComuns/BeComun");
            var tamVentas = Utils.XmlToObjectList<BeComun>(path + "TamVenta.xml", "//BeComuns/BeComun");

            switch (rolEvaluado)
            {
                case "2":

                    if (region == "00" && zona == "00" && context.Request["formato"] == "02")
                    {
                        entidades = _matrizBl.ListaDetalleInformacionGr(pais, codigoUsuario, Convert.ToInt32(rolEvaluado), periodo, subPeriodo, tamVentas, niveles);


                        foreach (var ntdd in entidades)
                        {
                            ntdd.Nombre = "GR: " + ntdd.Nombre;
                        }

                        var entidades3 = new List<BeDetalleInformacion>();
                        foreach (var entdd in entidades)
                        {
                            var entidades2 = _matrizBl.ListaDetalleInformacionGz(pais, codigoUsuario, Convert.ToInt32(rolEvaluador), 3, periodo, subPeriodo, entdd.NombreRegion.Split('-')[0], zona, tamVentas, niveles);

                            foreach (var entidd in entidades2)
                            {
                                entidd.Nombre = "GZ: " + entidd.Nombre;
                                entidades3.Add(entidd);
                            }
                        }

                        entidades.AddRange(entidades3);


                        //  ordenar para exportar
                        entidades.Sort((x, y) => String.CompareOrdinal(y.NombreRegion, x.NombreRegion));
                        //entidades.Sort((x, y) => string.Compare(y.Nombre, x.Nombre));

                        var entidadx = entidades[0];
                        var entidadesx = new List<BeDetalleInformacion>();
                        var entidadesf = new List<BeDetalleInformacion>();
                        //foreach (var entidad in entidades)
                        for (var i = 0; i < entidades.Count; i++)
                        {
                            if (entidades[i].NombreRegion == entidadx.NombreRegion)
                            {
                                if (entidades[i].Nombre.ToLower().StartsWith("GR".ToLower()))
                                {
                                    if (entidadesx.Count > 0)
                                    {
                                        var entid = entidadesx[0];
                                        entidadesx[0] = entidades[i];
                                        entidadesx.Add(entid);
                                    }
                                    else
                                    {
                                        entidadesx.Add(entidades[i]);
                                    }
                                }
                                else
                                {
                                    entidadesx.Add(entidades[i]);
                                }

                            }
                            else
                            {
                                entidadesf.AddRange(entidadesx);
                                entidadx = entidades[i];
                                entidadesx.RemoveRange(0, entidadesx.Count);
                                i = i - 1;
                            }

                        }
                        entidadesf.AddRange(entidadesx);
                        entidades = entidadesf;
                    }
                    else
                    {
                        entidades = _matrizBl.ListaDetalleInformacionGr(pais, codigoUsuario, Convert.ToInt32(rolEvaluado), periodo, subPeriodo, tamVentas, niveles);
                    }
                    break;
                case "3":

                    if (region == "00" && zona == "" && context.Request["formato"] == "02")
                    {
                        entidades = _matrizBl.ListaDetalleInformacionGr(pais, codigoUsuario, 2, periodo, subPeriodo, tamVentas, niveles);
                    }
                    else
                    {
                        entidades = _matrizBl.ListaDetalleInformacionGz(pais, codigoUsuario, Convert.ToInt32(rolEvaluador), Convert.ToInt32(rolEvaluado), periodo, subPeriodo, region, zona, tamVentas, niveles);
                    }
                    break;
            }

            return entidades;
        }

        #endregion DetalleInformacion

        #region Resultados

        private void VerResultado(HttpContext context)
        {
            try
            {
                var entidades = GetLisResultadoMatriz(context);
                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeResultadoMatriz> GetLisResultadoMatrizSustento(HttpContext context)
        {
            var codPaisEvaluado = context.Request["codPaisEvaluado"];
            var codEvaluado = context.Request["codEvaluado"];
            var codVariable = context.Request["codVariable"];
            var periodo = context.Request["periodo"];
            var idRolEvaluado = context.Request["idRolEvaluado"];
            var idTomaAccion = context.Request["idTomaAccion"];
            var tipoTomaAccion = context.Request["codTomaAccion"];

            var entidades = _matrizBl.ListarResultadosSusteno(codPaisEvaluado, codEvaluado, codVariable, periodo, Convert.ToInt32(idRolEvaluado), Convert.ToInt32(idTomaAccion), tipoTomaAccion);

            return entidades;
        }

        private List<BeResultadoMatriz> GetLisResultadoMatriz(HttpContext context)
        {
            List<BeResultadoMatriz> entidades;

            try
            {
                var codigoUsuario = context.Request["codigoUsuario"];
                var anho = context.Request["anho"];
                var codPais = context.Request["codPais"];
                var codVariable = context.Request["codVariable"];
                var periodo = context.Request["periodo"];
                var rolEvaluado = context.Request["rolEvaluado"];
                var gerenteRegion = context.Request["gerenteRegion"];
                var gerenteZona = context.Request["gerenteZona"];
                var nombreVariable = context.Request["nombreVariable"];
                var codRegion = context.Request["codRegion"];

                var nombrePeriodo = periodo == "00" ? "Todos" : periodo;

                var esVenta = (codVariable == "VtaNet");

                if (rolEvaluado == "5") //GR
                {
                    _filtros = "GR.Region: " + gerenteRegion + "     Año: " + anho + "      Periodo: " + nombrePeriodo + "      Variable : " + nombreVariable;
                }

                if (rolEvaluado == "6") //GR
                {
                    _filtros = "GR.Region:" + gerenteRegion + "       GR.Zona:" + gerenteZona + "     Año: " + anho + "      Periodo: " + nombrePeriodo + "  Variable : " + nombreVariable;
                }

                entidades = _matrizBl.ListarResultados(codPais, anho, codigoUsuario, codVariable, periodo, rolEvaluado, codRegion, esVenta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entidades;
        }

        private void CrearChart(HttpContext context)
        {
            try
            {
                var codVariable = context.Request["codVariable"];
                var anho = context.Request["anho"];

                var esProcentaje = context.Request["isPorcentaje"] == "1";

                var entidades = GetLisResultadoMatriz(context);

                string leyenda1;
                string leyenda2;
                var text = string.Empty;

                if (esProcentaje)
                {
                    text = "%";
                }

                if (codVariable == "VtaNet")
                {
                    leyenda1 = "%Part. Venta";
                    leyenda2 = "%Logro Venta";
                    esProcentaje = true;
                }
                else
                {
                    leyenda1 = string.Format("{0}Valor Planeado", text);
                    leyenda2 = string.Format("{0}Valor Real", text);
                }

                //class that creates the Chart object
                var runChart = new RuntimeChart();
                var mChart = runChart.makeChart(leyenda1, leyenda2, GetResultadoPeriodos(entidades, anho), esProcentaje);

                var tempFileName = String.Format("Chart_{0}.png", Guid.NewGuid().ToString());

                var path = context.Server.MapPath(@"~/Charts/") + tempFileName;

                _strPathImage = path;

                mChart.SaveImage(path);
                var strImageSrc = Utils.RelativeWebRoot + "Charts/" + tempFileName;

                // set callback when item was removed from cache
                var cid = new ChartImageDestructor(tempFileName);
                System.Web.Caching.CacheItemRemovedCallback onRemove = cid.RemovedCallback;

                //insert filename into cache
                HttpContext.Current.Cache.Add(tempFileName, cid, null, DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, onRemove);

                context.Response.Write(JsonConvert.SerializeObject(strImageSrc));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CrearChartSustento(HttpContext context)
        {
            try
            {
                var entidades = GetLisResultadoMatrizSustento(context);

                var text = string.Empty;

                var leyenda1 = string.Format("{0}Valor Planeado", text);
                var leyenda2 = string.Format("{0}Valor Real", text);

                //class that creates the Chart object
                var runChart = new RuntimeChart();
                var mChart = runChart.makeChartSustento(leyenda1, leyenda2, entidades, false);

                var tempFileName = String.Format("Chart_{0}.png", Guid.NewGuid().ToString());

                var path = context.Server.MapPath(@"~/Charts/") + tempFileName;

                _strPathImage = path;

                mChart.SaveImage(path);
                var strImageSrc = Utils.RelativeWebRoot + "Charts/" + tempFileName;

                // set callback when item was removed from cache
                var cid = new ChartImageDestructor(tempFileName);
                System.Web.Caching.CacheItemRemovedCallback onRemove = cid.RemovedCallback;

                //insert filename into cache
                HttpContext.Current.Cache.Add(tempFileName, cid, null, DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, onRemove);

                context.Response.Write(JsonConvert.SerializeObject(strImageSrc));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeResultadoMatriz> GetResultadoPeriodos(List<BeResultadoMatriz> entidades, string anho)
        {
            var anhoAnt = (Convert.ToInt32(anho) - 1).ToString(CultureInfo.InvariantCulture);

            var listaAux = (from entidad in entidades
                where entidad.Anho == anhoAnt && entidad.Tipo.ToUpper() == "UC"
                select new BeResultadoMatriz
                {
                    Periodo = entidad.Periodo, ParticipacionPeriodo = entidad.ParticipacionPeriodo, LogroPeriodo = entidad.LogroPeriodo
                }).ToList();
            listaAux.AddRange(from entidad in entidades
                where entidad.Anho == anho && entidad.Tipo.ToUpper() == "UC"
                select new BeResultadoMatriz
                {
                    Periodo = entidad.Periodo, ParticipacionPeriodo = entidad.ParticipacionPeriodo, LogroPeriodo = entidad.LogroPeriodo
                });

            return listaAux;
        }

        private void CuadranteUsuario(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codigoUsuario = context.Request["codigoUsuario"];
                var idRol = context.Request["idRol"];
                var anho = context.Request["anho"];
                var periodo = context.Request["periodo"];

                var path = context.Server.MapPath(@"~/Matriz/XML/");

                var niveles = Utils.XmlToObjectList<BeComun>(path + "Competencia.xml", "//BeComuns/BeComun");

                var entidad = _matrizBl.ObtenerCuadranteUsuario(codPais, codigoUsuario, Convert.ToInt32(idRol), anho, periodo, niveles);

                context.Response.Write(JsonConvert.SerializeObject(entidad));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Resultados

        #region Toma de Accion

        private void LoadAllGerentesZonaByZona(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codRegion = context.Request["codRegion"];
                var codZona = context.Request["codZona"];
                var periodo = context.Request["periodo"];

                var entidades = _matrizBl.ListarGerenteZonaByZona(codPais, codRegion, codZona, periodo);
                entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidarFechaAcuerdo(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var periodo = context.Request["periodo"];
                context.Response.Write(JsonConvert.SerializeObject(_blCronogramaPdM.ValidarFechaAcuerdo(codPais, periodo)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidarPeriodoPorCampanha(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var campanha = context.Request["campanha"];
                context.Response.Write(JsonConvert.SerializeObject(_matrizBl.ValidarPeriodoPorCampanha(codPais, campanha)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ObtenerCampanhaActual(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codUsuario = context.Request["codUsuario"];
                var idRol = context.Request["idRol"];
                var periodo = context.Request["periodo"];

                context.Response.Write(JsonConvert.SerializeObject(_matrizBl.ObtenerCampanhaActual(codPais, codUsuario, Convert.ToInt32(idRol), periodo)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ObtenerCampanhas(HttpContext context)
        {
            try
            {
                var entidades = new List<BeComun>();

                if (!string.IsNullOrEmpty(context.Request["campanha"]))
                {
                    var anhoCampanha = context.Request["campanha"];

                    var anho = anhoCampanha.Substring(0, 4);
                    var campanha = anhoCampanha.Substring(4, 2);

                    var contador = 0;

                    while (contador < 18)
                    {
                        if (Convert.ToInt32(campanha) < 18)
                        {
                            campanha = (Convert.ToInt32(campanha) + 1).ToString(CultureInfo.InvariantCulture);

                            if (Convert.ToInt32(campanha) < 10)
                            {
                                campanha = "0" + campanha;
                            }

                        }
                        else
                        {
                            campanha = "01";
                            anho = (Convert.ToInt32(anho) + 1).ToString(CultureInfo.InvariantCulture);
                        }

                        anhoCampanha = anho + campanha;
                        var text = anho + " - " + campanha;
                        entidades.Add(new BeComun { Codigo = anhoCampanha, Descripcion = text });

                        contador++;
                    }


                    //int campanha = Convert.ToInt32(context.Request["campanha"]);
                    //campanha++;

                    //if (int.Parse(campanha.ToString().Substring(4, 2)) >= 18)
                    //{
                    //    campanha = int.Parse((int.Parse(campanha.ToString().Substring(0, 4)) + 1).ToString() + "01");
                    //}

                    //string anho = campanha.ToString().Substring(0, 4);

                    //int campanhaMax = Convert.ToInt32(anho + "18");

                    //while (campanha <= campanhaMax)
                    //{
                    //    string text = anho + " - " + campanha.ToString().Substring(4, 2);

                    //    entidades.Add(new beComun { Codigo = campanha.ToString(), Descripcion = text });
                    //    campanha++;
                    //}
                }
                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ObtenerZonasDisponibles(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codRegion = context.Request["codRegion"];
                var periodo = context.Request["periodo"];
                var estadoActivo = context.Request["estadoActivo"];

                context.Response.Write(JsonConvert.SerializeObject(_matrizBl.ListarAllZonaDisponible(codPais, codRegion, periodo, Convert.ToInt32(estadoActivo))));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ObtenerRegionesDisponibles(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var periodo = context.Request["periodo"];
                var estadoActivo = context.Request["estadoActivo"];

                context.Response.Write(JsonConvert.SerializeObject(_matrizBl.ListarAllRegionDisponible(codPais, periodo, Convert.ToInt32(estadoActivo))));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InsertarTomaAccion(HttpContext context)
        {
            try
            {
                var entidad = (BeTomaAccion)JsonConvert.DeserializeObject(context.Request["entidad"], typeof(BeTomaAccion));

                var id = _matrizBl.InsertarTomaAccion(entidad);

                if (id != 0)
                {
                    entidad.NombreRolEvaluador = Enum.GetName(typeof(TipoRol), entidad.IdRolEvaluador);
                    entidad.NombreRolEvaluado = Enum.GetName(typeof(TipoRol), entidad.IdRolEvaluado);
                    entidad.NombrePaisEvaluador = _matrizBl.ObtenerNombrePais(entidad.PrefijoIsoPaisEvaluador);
                    entidad.NombrePaisEvaluado = _matrizBl.ObtenerNombrePais(entidad.PrefijoIsoPaisEvaluado);
                    entidad.NombreRegionActual = _matrizBl.ObtenerNombreRegion(entidad.PrefijoIsoPaisEvaluado, entidad.CodRegionActual);

                    entidad.CorreoSupervisor = _matrizBl.ObtenerCorreo(entidad.PrefijoIsoPaisEvaluado, entidad.CodEvaluado, entidad.IdRolEvaluador, entidad.IdRolEvaluado);

                    if (entidad.IdRolEvaluado == 3)//GZ
                        entidad.NombreZonaActual = _matrizBl.ObtenerNombreZona(entidad.PrefijoIsoPaisEvaluado, entidad.CodRegionActual, entidad.CodZonaActual);

                    var path = context.Server.MapPath(@"~/Matriz/XML/TomaAccion.xml");

                    var entidades = Utils.XmlToObjectList<BeComun>(path, "//BeComuns/BeComun");

                    entidad.NombreTomaAccion = NombreCodXML(entidades, entidad.TomaAccion);

                    if (entidad.TomaAccion == "02")//Reasignación
                    {
                        entidad.NombreRegionReasignacion = _matrizBl.ObtenerNombreRegion(entidad.PrefijoIsoPaisEvaluado, entidad.CodRegionReasignacion);

                        if (entidad.IdRolEvaluado == 3)//GZ
                            entidad.NombreZonaReasignacion = _matrizBl.ObtenerNombreZona(entidad.PrefijoIsoPaisEvaluado, entidad.CodRegionReasignacion, entidad.CodZonaReasignacion);
                    }

                    id = EnviarCorreo(entidad);
                }

                context.Response.Write(JsonConvert.SerializeObject(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void DeleteTomaAccion(HttpContext context)
        {
            try
            {
                var idTomaAccion = context.Request["idTomaAccion"];
                var estado = _matrizBl.DeleteTomaAccion(Convert.ToInt32(idTomaAccion));
                context.Response.Write(JsonConvert.SerializeObject(estado));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCalibracion(HttpContext context)
        {
            try
            {
                var entidades = (List<BeTomaAccion>)JsonConvert.DeserializeObject(context.Request["calibraciones"], typeof(List<BeTomaAccion>));
                var usuario = context.Request["usuario"];

                var blmatriz = new BlMatriz();

                var insertado = blmatriz.UpdateCalibracion(entidades, usuario);
                context.Response.Write(JsonConvert.SerializeObject(insertado));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ListarTomaAccion(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codEvaluador = context.Request["codEvaluador"];
                var periodo = context.Request["periodo"];
                var idRolEvaluador = context.Request["idRolEvaluador"];
                var idRolEvaluado = context.Request["idRolEvaluado"];
                var estadoActivo = context.Request["estadoActivo"];
                var codTomaAccion = context.Request["codTomaAccion"];
                var path = context.Server.MapPath(@"~/Matriz/XML/");
                var tomaAcciones = Utils.XmlToObjectList<BeComun>(path + "TomaAccion.xml", "//BeComuns/BeComun");

                var entidades = new List<BeTomaAccion>();

                if (idRolEvaluado == "-1")
                {
                    switch (idRolEvaluador)
                    {
                        case "1"://DV
                            entidades = _matrizBl.ListarTomaAcciones(codPais, codEvaluador, periodo, Convert.ToInt32(idRolEvaluador), 2, tomaAcciones, Convert.ToInt32(estadoActivo), codTomaAccion);
                            entidades.AddRange(_matrizBl.ListarTomaAcciones(codPais, codEvaluador, periodo, Convert.ToInt32(idRolEvaluador), 3, tomaAcciones, Convert.ToInt32(estadoActivo), codTomaAccion));
                            break;

                        case "2"://GR

                            entidades = _matrizBl.ListarTomaAcciones(codPais, codEvaluador, periodo, Convert.ToInt32(idRolEvaluador), 3, tomaAcciones, Convert.ToInt32(estadoActivo), codTomaAccion);
                            break;
                    }
                }
                else
                {
                    entidades = _matrizBl.ListarTomaAcciones(codPais, codEvaluador, periodo, Convert.ToInt32(idRolEvaluador), Convert.ToInt32(idRolEvaluado), tomaAcciones, Convert.ToInt32(estadoActivo), codTomaAccion);
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ListarCalibraciones(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var periodo = context.Request["periodo"];
                var path = context.Server.MapPath(@"~/Matriz/XML/");

                var tomaAcciones = Utils.XmlToObjectList<BeComun>(path + "TomaAccion.xml", "//BeComuns/BeComun");
                var entidades = _matrizBl.ListarCalibraciones(codPais, periodo, tomaAcciones);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidarTomaAccion(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var codUsuario = context.Request["codUsuario"];
                var idRol = context.Request["idRol"];
                var periodo = context.Request["periodo"];
                var estadoActivo = context.Request["estadoActivo"];
                context.Response.Write(_matrizBl.ValidarTomaAcuerdo(codPais, codUsuario, Convert.ToInt32(idRol), periodo, Convert.ToInt32(estadoActivo)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int EnviarCorreo(BeTomaAccion entidad)
        {
            int enviado;

            try
            {
                if (entidad.IdRolEvaluador == (int)TipoRol.DirectoraVentas && entidad.IdRolEvaluado == (int)TipoRol.GerenteZona)//Calibracion
                {
                    var correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"]);
                    var servidorSmtp = ConfigurationManager.AppSettings["servidorSMTP"];
                    var correoDestino = ConfigurationManager.AppSettings["CorreoDestinoMatriz"];
                    var strHtml = HtmBody(entidad);

                    var enviar = new SmtpClient(servidorSmtp);

                    var correoTo = new MailAddress(correoDestino);
                    var msjEmail = new MailMessage(correoFrom, correoTo)
                    {
                        Subject = ConfigurationManager.AppSettings["AsuntoCorreoMatriz"],
                        IsBodyHtml = true,
                        Body = strHtml
                    };

                    if (!string.IsNullOrEmpty(entidad.CorreoSupervisor))
                    {
                        var copy = new MailAddress(entidad.CorreoSupervisor);
                        msjEmail.CC.Add(copy);
                    }

                    enviar.Send(msjEmail);
                }

                enviado = 3;
            }
            catch
            {
                enviado = 2;
            }

            return enviado;
        }


        private string HtmBody(BeTomaAccion entidad)
        {
            var strHtml = string.Empty;
            strHtml += "<table align='left' border='0'>";
            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 26px;'>Se ha realizado una toma de Acción  <span style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'>" + entidad.NombreTomaAccion + "</span> en la fecha :" + DateTime.Now.ToShortDateString() + "</td></tr>";
            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>De la " + entidad.NombreRolEvaluador + "  " + entidad.NombreEvaluador + "(" + entidad.CodEvaluador + ") del país :" + entidad.NombrePaisEvaluador + "</td></tr>";

            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>A una " + entidad.NombreRolEvaluado + "  " + entidad.NombreEvaluado + "(" + entidad.CodEvaluado + ") del país :" + entidad.NombrePaisEvaluado + "</td></tr>";
            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Cuyos datos son los siguientes:</td></tr>";

            if (entidad.TomaAccion == "01")// Plan Mejora
            {
                strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Campaña Inicio Critico:" + entidad.AnhoCampanhaInicioCritico + "</td></tr>";

                strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Campaña Fin Critico:" + entidad.AnhoCampanhaFinCritico + "</td></tr>";
            }

            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Región actual: " + entidad.NombreRegionActual + "</td></tr>";

            if (entidad.IdRolEvaluado == 3)//GZ
                strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Zona Actual:" + entidad.NombreZonaActual + "</td></tr>";

            if (entidad.TomaAccion == "02")// Reasignación
            {
                strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Región Reasignada :" + entidad.NombreRegionReasignacion + "</td></tr>";

                if (entidad.IdRolEvaluado == 3)//GZ
                    strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Zona Reasignada :" + entidad.NombreZonaReasignacion + "</td></tr>";
            }

            if (entidad.TomaAccion == "01")// Plan Mejora
            {
                strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Campaña Inicio :" + entidad.AnhoCampanhaInicio + "</td></tr>";

                strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Campaña Fin :" + entidad.AnhoCampanhaFin + "</td></tr>";
            }
            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Observación :" + entidad.Observaciones + "</td></tr>";
            strHtml += "<tr></tr>";
            strHtml += "<tr></tr>";
            strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold;'>Saludos</td></tr>";
            strHtml += "</table>";
            return strHtml;
        }

        private void LineamientoAcuerdo(HttpContext context)
        {
            var codPaisEvaluador = context.Request["codPaisEvaluador"];
            var codEvaluador = context.Request["codEvaluador"];
            var periodo = context.Request["periodo"];
            var tipoCondicion = context.Request["tipoCondicion"];
            var idRolEvaluado = context.Request["idRolEvaluado"];
            var entidades = new List<BeComun>();

            switch (idRolEvaluado)
            {
                case "2"://GR
                    entidades = _matrizBl.ListarGerentesRegionByLineamientos(codEvaluador, codPaisEvaluador, periodo, tipoCondicion);
                    //entidades.Add(new beComun { Codigo = "CO-0043741834-201202-201206", Descripcion = "VERONICA CUARTAS TIRADO" });
                    //entidades.Add(new beComun { Codigo = "CO-0063507803-201202-201206", Descripcion = "ELLA MILENE CABRERA VILLALOBOS" });
                    break;

                case "3"://GZ
                    entidades = _matrizBl.ListarGerentesZonaByLineamientos(codEvaluador, codPaisEvaluador, periodo, tipoCondicion);
                    //entidades.Add(new beComun { Codigo = "CO-0046377913-201202-201206", Descripcion = "CLAUDIA YANETH VEGA SERNA" });
                    //entidades.Add(new beComun { Codigo = "CO-0040020266-201202-201206", Descripcion = "DIANA MARCELA MONROY URIBE" });

                    break;
            }

            entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        private void ConfirmarTomaAccion(HttpContext context)
        {
            var entidades = (List<BeTomaAccion>)JsonConvert.DeserializeObject(context.Request["entidades"], typeof(List<BeTomaAccion>));

            context.Response.Write(JsonConvert.SerializeObject(_matrizBl.ConfirmarTomaAccion(entidades)));
        }

        #endregion Toma de Accion

        #region Ficha

        private void FichaPersonal(HttpContext context)
        {
            try
            {
                var codigoUsuario = context.Request["codigoUsuario"];
                var pais = context.Request["pais"];

                var entidad = _matrizBl.ObtenerFichaPersonal(pais, codigoUsuario);

                context.Response.Write(JsonConvert.SerializeObject(entidad));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Ficha

        #region "Descarga"

        private void DescargarDetalleInformacion(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Matriz/TempReportes/");
            var tipo = context.Request["tipo"];

            var entidades = ListaDetalleInformacion(context);


            foreach (var be in entidades)
            {
                if (be.Tipo == "N")
                {
                    be.Cuadrante = "NUEVAS";
                }
            }

            var data = Utils.ConvertToDataTable(entidades);

            var fileName = string.Format("{0}.{1}", "DetalleInformacion" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                                            tipo);
            var strFilePath = strFolder + id + fileName;
            var rolEvaluado = context.Request["rolEvaluado"];
            var headerTitles = new List<string>();

            data.TableName = "Detalle de Información"; //titulo

            data.Namespace = _filtros; // texto filtros
            //Ocultar columnas
            data.Columns.Remove("PrefijoIsoPais");
            data.Columns.Remove("DocIdentidad");
            data.Columns.Remove("NivelCompetencia");
            data.Columns.Remove("PorcentajeAvance");
            data.Columns.Remove("VentaPlanPeriodo");
            data.Columns.Remove("Tipo");
            //Crear Titulos

            headerTitles.Add("Cuadrante");

            if (rolEvaluado == "2")//GR
            {
                headerTitles.Add("Gerente Región");
                data.Columns.Remove("NombreZona");
                headerTitles.Add("Región");
            }

            if (rolEvaluado == "3")//GR
            {
                headerTitles.Add("Gerente Zona");
                headerTitles.Add("Región");
                headerTitles.Add("Zona");
            }

            headerTitles.Add("Puntos Rankig");
            headerTitles.Add("Ranking");
            headerTitles.Add("Competencia");
            headerTitles.Add("Venta Periodo");
            headerTitles.Add("% Logro");
            headerTitles.Add("%Participación Venta");
            headerTitles.Add("Tamaño Venta");

            HeaderTitle(headerTitles, ref data);

            switch (tipo)
            {
                case "pdf":
                    var pdf = new PDFExporter(data, fileName, false);
                    pdf.ExportPDF();
                    break;

                case "xls":

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

                    break;
            }
        }

        private void DescargarResultadoMatriz(HttpContext context)
        {
            var tipo = context.Request["tipo"];
            var anho = context.Request["anho"];
            var anhoAnt = (Convert.ToInt32(anho) - 1).ToString(CultureInfo.InvariantCulture);
            var varPeriodo = context.Request["periodo"];
            var rolEvaluado = context.Request["rolEvaluado"];
            const string titulo = "Reporte de Resultados";

            var esProcentaje = (context.Request["isPorcentaje"] == "1");

            var entidades = GetLisResultadoMatriz(context);

            var periodos = GetPeriodosReportMatriz(entidades, anho);

            var listaReportRow = CreateTableResumenResultado(entidades, periodos, varPeriodo, anho, anhoAnt, esProcentaje);
            var listaReportRowAnhoAnt = CreateTableAnhoResultado(entidades, periodos, anhoAnt, esProcentaje, rolEvaluado);
            var listaReportRowAnhoAct = CreateTableAnhoResultado(entidades, periodos, anho, esProcentaje, rolEvaluado);

            var fileName = string.Format("{0}.{1}", "ResultadoMatriz" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), tipo);

            //create an image from the path
            var image = System.Drawing.Image.FromFile(_strPathImage);
            var ms = new MemoryStream();
            //pull the memory stream from the image (I need this for the byte array later)
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            switch (tipo)
            {
                case "pdf":
                    var pdf = new PDFReporter(titulo, _filtros, fileName, listaReportRow, listaReportRowAnhoAnt, listaReportRowAnhoAct, _strPathImage, anho, anhoAnt);
                    pdf.ExportPDF();
                    break;

                case "xls":

                    var hssfworkbook = new HSSFWorkbook();

                    var sheet = hssfworkbook.CreateSheet("ResultadoMatriz");

                    ExcelReporter.CreateText(ref hssfworkbook, ref sheet, titulo, 1, 1, "C");

                    ExcelReporter.CreateText(ref hssfworkbook, ref sheet, _filtros, 1, 3, "L");

                    ExcelReporter.CreateText(ref hssfworkbook, ref sheet, "Ranking", 1, 5, "L");

                    var rowIndex = 10;

                    ExcelReporter.CreateTable(ref hssfworkbook, ref sheet, listaReportRow, 1, rowIndex);

                    rowIndex = rowIndex + listaReportRow.Count + 5;

                    ExcelReporter.CreateText(ref hssfworkbook, ref sheet, "Campañas del Año " + anhoAnt, 1, rowIndex, "L");

                    ExcelReporter.CreateTable(ref hssfworkbook, ref sheet, listaReportRowAnhoAnt, 1, rowIndex + 3);

                    if (listaReportRowAnhoAnt.Count > 0)
                        rowIndex = rowIndex + listaReportRowAnhoAnt.Count + 4;
                    else
                    {
                        ExcelReporter.CreateText(ref hssfworkbook, ref sheet, "No existen Datos para este año", 1, rowIndex + 2, "L");
                        rowIndex = rowIndex + 7;
                    }

                    ExcelReporter.CreateText(ref hssfworkbook, ref sheet, "Campañas del Año " + anho, 1, rowIndex, "L");

                    ExcelReporter.CreateTable(ref hssfworkbook, ref sheet, listaReportRowAnhoAct, 1, rowIndex + 2);

                    if (listaReportRowAnhoAct.Count == 0)
                    {
                        ExcelReporter.CreateText(ref hssfworkbook, ref sheet, "No existen Datos para este año", 1, rowIndex + 2, "L");
                    }

                    //map the path to the img folder
                    var rutaReporte = context.Server.MapPath(@"~/Charts/" + fileName);

                    //the drawing patriarch will hold the anchor and the master information
                    var patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                    //store the coordinates of which cell and where in the cell the image goes
                    var anchor = new HSSFClientAnchor(500, 200, 0, 0, 10, 7, 15, 17) {AnchorType = 2};
                    //types are 0, 2, and 3. 0 resizes within the cell, 2 doesn't
                    //add the byte array and encode it for the excel file
                    var index = hssfworkbook.AddPicture(ms.ToArray(), PictureType.PNG);
                    patriarch.CreatePicture(anchor, index);

                    //Write the stream data of workbook to the root directory
                    var file = new FileStream(rutaReporte, FileMode.Create);
                    hssfworkbook.Write(file);
                    file.Close();

                    var archivo = MatrizHelper.ReadFile(rutaReporte);
                    context.Response.ClearHeaders();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    context.Response.ContentType = "application/" + tipo;
                    context.Response.BinaryWrite(archivo);
                    context.Response.Flush();
                    MatrizHelper.DeleteFile(rutaReporte);
                    context.Response.End();

                    break;
            }
        }

        private void DescargarMatrizConsolidada(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Matriz/TempReportes/");
            var tipo = context.Request["tipo"];
            var formato = context.Request["formato"];
            var tipoColaborador = context.Request["tipoColaborador"];
            var periodo = context.Request["periodo"];
            var subPeriodo = context.Request["subPeriodo"];
            var nombreRegion = context.Request["nombreRegion"];
            var nombreZona = context.Request["nombreZona"];

            var subPeriodoAlt = subPeriodo == "00" ? "Todos" : subPeriodo;
            string[] data = { "" };
            var dtData = new DataTable();

            var fileName = string.Format("{0}.{1}", "Matriz consolidada" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), tipo);
            var strFilePath = strFolder + id + fileName;
            var rolEvaluado = context.Request["rolEvaluado"];
            var headerTitles = new List<string>();

            if (rolEvaluado == "2")//GR
            {
                _filtros = " Año: " + periodo.Substring(0, 4) + "  Periodo: " + periodo + "  SubPeriodo : " + subPeriodoAlt + "  Región : " + nombreRegion;
            }

            if (rolEvaluado == "3")//GZ
            {
                _filtros = " Año: " + periodo.Substring(0, 4) + "  Periodo: " + periodo + "  SubPeriodo : " + subPeriodoAlt + "  Región :" + nombreRegion + "  Zona: " + nombreZona;
            }

            if (formato == "01")
            {
                var resultados = context.Request["resultados"];
                data = resultados.Split('|');
            }
            else if (formato == "02")
            {
                var ranking = context.Request["ranking"];
                var nivelCompetencia = context.Request["nivelCompetencia"];
                var tamVenta = context.Request["tamVenta"];
                var tipoData = context.Request["tipoData"];

                var entidadesAux = ListaDetalleInformacion(context);
                var entidades = new List<BeDetalleInformacion>();

                foreach (var be in entidadesAux)
                {
                    if (tipoColaborador == "00" || tipoColaborador == "01")
                    {
                        if (tipoData == "-1")
                        {
                            if ((be.Ranking == ranking || ranking == "")
                            && (be.NivelCompetencia == nivelCompetencia || nivelCompetencia == "")
                            && (be.TamVenta == tamVenta || tamVenta == "")
                            && (be.Tipo != "N")
                            && (be.Competencia != ""))
                            {
                                entidades.Add(be);
                            }
                        }
                        else if (tipoData == "ASM")
                        {
                            if ((be.Ranking == ranking
                                && be.Tipo != "N"
                                && be.Cuadrante == ""
                                && (be.TamVenta == tamVenta || tamVenta == ""))
                                || (be.Ranking == ranking
                                && be.Tipo != "N"
                                && be.Competencia == ""
                                && (be.TamVenta == tamVenta || tamVenta == "")))
                            {
                                entidades.Add(be);
                            }
                        }
                        else
                        {
                            if ((be.Ranking == ranking || ranking == "")
                            && (be.NivelCompetencia == nivelCompetencia || nivelCompetencia == "")
                            && (be.TamVenta == tamVenta || tamVenta == "")
                            && (be.Tipo == tipoData || tipoData == ""))
                            {
                                entidades.Add(be);
                            }
                        }
                    }
                    else if (tipoColaborador == "02")
                    {
                        if ((be.Ranking == ranking || ranking == "")
                            && (be.NivelCompetencia == nivelCompetencia || nivelCompetencia == "")
                            && (be.TamVenta == tamVenta || tamVenta == "")
                            && (be.Tipo.ToUpper() != "N"))
                        {
                            entidades.Add(be);
                        }
                    }
                }


                foreach (var be in entidades)
                {
                    if (be.Tipo == "N")
                    {
                        be.Cuadrante = "NUEVAS";
                    }
                }

                dtData = Utils.ConvertToDataTable(entidades);
                dtData.TableName = "Detalle de Información"; //titulo
                dtData.Namespace = _filtros; // texto filtros
                //Ocultar columnas
                dtData.Columns.Remove("PrefijoIsoPais");
                dtData.Columns.Remove("DocIdentidad");
                dtData.Columns.Remove("NivelCompetencia");
                //dtData.Columns.Remove("Competencia");
                dtData.Columns.Remove("PorcentajeAvance");
                dtData.Columns.Remove("VentaPlanPeriodo");
                dtData.Columns.Remove("Tipo");
                //Crear Titulos

                headerTitles.Add("Cuadrante");

                if (rolEvaluado == "2")//GR
                {

                    if (nombreRegion == "Todos" && nombreZona == "Todos")
                    {
                        headerTitles.Add("Nombre GR/GZ");
                        dtData.Columns.Remove("NombreZona");
                        headerTitles.Add("Código Región");

                    }
                    else
                    {
                        headerTitles.Add("Gerente Región");
                        dtData.Columns.Remove("NombreZona");
                        headerTitles.Add("Región");
                    }
                }

                if (rolEvaluado == "3")//GR
                {
                    headerTitles.Add("Gerente Zona");
                    headerTitles.Add("Región");
                    headerTitles.Add("Zona");
                }

                headerTitles.Add("Puntos Rankig");
                headerTitles.Add("Ranking");
                headerTitles.Add("Competencia");
                headerTitles.Add("Venta Periodo");

                headerTitles.Add("% Logro");
                headerTitles.Add("%Participación Venta");
                headerTitles.Add("Tamaño Venta");

                HeaderTitle(headerTitles, ref dtData);
            }

            switch (tipo)
            {
                case "pdf":
                    var pdf = new PDFExporter(dtData, fileName, false);

                    if (formato == "01")
                    {
                        var path = context.Server.MapPath(@"~/Matriz/XML/");

                        var niveles = Utils.XmlToObjectList<BeComun>(path + "Competencia.xml", "//BeComuns/BeComun");
                        var tamVentas = Utils.XmlToObjectList<BeComun>(path + "TamVenta.xml", "//BeComuns/BeComun");

                        pdf.pdfMatriz(strFilePath, data, niveles, tamVentas, tipoColaborador, _filtros);

                        var archivoPdf = MatrizHelper.ReadFile(strFilePath);
                        context.Response.ClearHeaders();
                        context.Response.Clear();
                        context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                        context.Response.ContentType = "application/" + tipo;
                        context.Response.BinaryWrite(archivoPdf);
                        context.Response.Flush();
                        MatrizHelper.DeleteFile(strFilePath);
                        context.Response.End();
                    }
                    else if (formato == "02")
                    {
                        pdf.ExportPDF();
                    }
                    break;

                case "xls":
                    var edw = new ExcelDatasetWriter();
                    var ds = new DataSet();
                    var book = new Workbook();

                    ds.Tables.Add(dtData);

                    if (formato == "01")
                    {
                        var path = context.Server.MapPath(@"~/Matriz/XML/");

                        var niveles = Utils.XmlToObjectList<BeComun>(path + "Competencia.xml", "//BeComuns/BeComun");
                        var tamVentas = Utils.XmlToObjectList<BeComun>(path + "TamVenta.xml", "//BeComuns/BeComun");

                        book = edw.CreateWorkbookMatriz(formato, data, niveles, tamVentas, tipoColaborador, _filtros);
                    }
                    else if (formato == "02")
                    {
                        book = edw.CreateWorkbook(ds);
                    }

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

                    break;
            }
        }

        #endregion "Descarga"

        #region "Matriz Talento"

        private void DescargarMatrizTalento(HttpContext context)
        {
            var entidades = ListaDetalleInformacion(context);

            var path = context.Server.MapPath(@"~/Matriz/XML/");
            var niveles = Utils.XmlToObjectList<BeComun>(path + "Competencia.xml", "//BeComuns/BeComun");

            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Matriz/TempReportes/");
            var strImages = context.Server.MapPath(@"~/Styles/images/");

            var tipo = context.Request["tipo"];
            var periodo = context.Request["periodo"];
            var subPeriodo = context.Request["subPeriodo"];
            var nombreRegion = context.Request["nombreRegion"];
            var nombreZona = context.Request["nombreZona"];
            var rolEvaluado = context.Request["rolEvaluado"];

            var fileName = string.Format("{0}.{1}", "MatrizTalento" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), tipo);
            var strFilePath = strFolder + id + fileName;

            var subPeriodoAlt = subPeriodo == "00" ? "Todos" : subPeriodo;

            if (rolEvaluado == "2")//GR
            {
                _filtros = " Año: " + periodo.Substring(0, 4) + "  Periodo: " + periodo + "  SubPeriodo : " + subPeriodoAlt + "  Región : " + nombreRegion;
            }

            if (rolEvaluado == "3")//GZ
            {
                _filtros = " Año: " + periodo.Substring(0, 4) + "  Periodo: " + periodo + "  SubPeriodo : " + subPeriodoAlt + "  Región :" + nombreRegion + "  Zona: " + nombreZona;
            }

            switch (tipo)
            {
                case "pdf":

                    var strB = DoPdf(strFilePath, _filtros, entidades, niveles, strImages);

                    var document = new Document(PageSize.A4.Rotate(), 20, 12, 7, 16);
                    var stream = new FileStream(strFilePath, FileMode.Create);
                    var pdfWriter = PdfWriter.GetInstance(document, stream);
                    //HtmlParser.Parse(document, context.Server.MapPath("origin.html"));

                    document.Open();
                    // now read the Grid html one by one and add into the document object
                    using (TextReader sReader = new StringReader(strB.ToString()))
                    {
                        var list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                        foreach (var elm in list)
                        {
                            document.Add(elm);
                        }
                    }
                    document.Close();
                    stream.Close();
                    pdfWriter.Close();

                    var archivoPdf = MatrizHelper.ReadFile(strFilePath);
                    context.Response.ClearHeaders();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    context.Response.ContentType = "application/" + tipo;
                    context.Response.BinaryWrite(archivoPdf);
                    context.Response.Flush();
                    MatrizHelper.DeleteFile(strFilePath);
                    context.Response.End();

                    break;

                case "xls":

                    DoExcell(strFilePath, _filtros, entidades, niveles);

                    var archivo = MatrizHelper.ReadFile(strFilePath);
                    context.Response.ClearHeaders();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    context.Response.ContentType = "application/" + tipo;
                    context.Response.BinaryWrite(archivo);
                    context.Response.Flush();
                    MatrizHelper.DeleteFile(strFilePath);
                    context.Response.End();

                    break;
            }
        }

        private void DescargarDetalleInformacionMatrizTalento(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Matriz/TempReportes/");
            var tipo = context.Request["tipo"];
            _cuadrante = context.Request["cuadrante"];

            var entidades = ListaDetalleInformacion(context);

            var rpteResultados = entidades.FindAll(FiltraDetalleInformacion);

            foreach (var be in rpteResultados)
            {
                be.Cuadrante = _cuadrante;
            }

            var data = Utils.ConvertToDataTable(rpteResultados);

            var fileName = string.Format("{0}.{1}", "DetalleInformacion" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"),
                tipo);
            var strFilePath = strFolder + id + fileName;
            var rolEvaluado = context.Request["rolEvaluado"];
            var headerTitles = new List<string>();

            data.TableName = "Detalle de Información"; //titulo

            data.Namespace = _filtros; // texto filtros
            //Ocultar columnas
            data.Columns.Remove("PrefijoIsoPais");
            data.Columns.Remove("DocIdentidad");
            data.Columns.Remove("NivelCompetencia");
            data.Columns.Remove("PorcentajeAvance");
            data.Columns.Remove("VentaPlanPeriodo");
            data.Columns.Remove("Tipo");
            //Crear Titulos

            headerTitles.Add("Cuadrante");

            if (rolEvaluado == "2")//GR
            {
                headerTitles.Add("Gerente Región");
                data.Columns.Remove("NombreZona");
                headerTitles.Add("Región");
            }

            if (rolEvaluado == "3")//GR
            {
                headerTitles.Add("Gerente Zona");
                headerTitles.Add("Región");
                headerTitles.Add("Zona");
            }

            headerTitles.Add("Puntos Rankig");
            headerTitles.Add("Ranking");
            headerTitles.Add("Competencia");
            headerTitles.Add("Venta Periodo");
            headerTitles.Add("% Logro");
            headerTitles.Add("%Participación Venta");
            headerTitles.Add("Tamaño Venta");

            HeaderTitle(headerTitles, ref data);

            switch (tipo)
            {
                case "pdf":
                    var pdf = new PDFExporter(data, fileName, false);
                    pdf.ExportPDF();
                    break;

                case "xls":

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

                    break;
            }
        }

        public int DoExcell(string ruta, string filtros, List<BeDetalleInformacion> entidades, List<BeComun> niveles)
        {
            var fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite);
            var w = new StreamWriter(fs);
            var html = new StringBuilder();

            const string ancho = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            //string TotalAntSinMedicion = (int.Parse(ProdAnSM) + int.Parse(EstAnSM) + int.Parse(CritAnSM)).ToString();
            var totalAntSinMedicion = (int.Parse(ContarCuadranteSinMedicion("PRODUCTIVA-ES", entidades)) + int.Parse(ContarCuadranteSinMedicion("ESTABLE-ES", entidades)) + int.Parse(ContarCuadranteSinMedicion("CRITICA-ES", entidades))).ToString(CultureInfo.InvariantCulture);

            //string TotalNueSinMedicion = (int.Parse(ProdNuSM) + int.Parse(EstNuSM) + int.Parse(CritNuSM)).ToString();
            var totalNueSinMedicion = (int.Parse(ContarCuadranteSinMedicion("PRODUCTIVA-N", entidades)) + int.Parse(ContarCuadranteSinMedicion("ESTABLE-N", entidades)) + int.Parse(ContarCuadranteSinMedicion("CRITICA-N", entidades))).ToString(CultureInfo.InvariantCulture);

            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title></title>");
            html.Append("<style>");
            html.Append(".CuadMedi{height:97px;border-style:solid;border-width:2px;border-color:#B46EB0;text-align: center;vertical-align: middle;font-size: 20px;font-family: Arial;font-weight: bold;color: #7C7D81;}");
            html.Append(".CuadMediTotal{text-align: center;vertical-align: middle;font-size: 20px;font-family: Arial;font-weight: bold;color: #7C7D81;}");
            html.Append(".CssTdTiH{vertical-align: middle ;background-color:#652174 ;background-position:center ;border-style:solid ;border-width:2px ;border-color:#B46EB0; text-align: center;font-size: 10px;font-family: Arial;font-weight: bold;color: #FFFFFF;height:25px}");
            html.Append(".CssTdTiV{vertical-align: middle ;background-color:#652174 ;background-position:center ;border-style:solid ;border-width:2px ;border-color:#B46EB0; text-align: center;font-size: 10px;font-family: Arial;font-weight: bold;color: #FFFFFF;mso-rotate:90;}");
            html.Append("</style>");
            html.Append("</head>");
            html.Append("<body>");
            html.Append("<table>");
            html.Append("<tr><td>MATRIZ TALENTO</td></tr>");
            html.Append("<tr><td>" + filtros + "</td></tr>");
            html.Append("<tr>");
            html.Append("<td>");

            html.Append("<table>");

            html.Append("<tr>");
            html.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            for (var i = 0; i < niveles.Count; i++)
            {
                html.Append("<td>" + ancho + "</td>");
            }
            html.Append("<td></td>");
            html.Append("<td>" + ancho + "</td>");
            html.Append("<td>" + ancho + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td></td>");
            for (var i = 0; i < niveles.Count; i++)
            {
                html.Append("<td>" + ancho + "</td>");
            }
            html.Append("<td></td>");
            html.Append("<td>Antiguas</td>");
            html.Append("<td>Nuevas</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td></td>");

            for (var i = 0; i < niveles.Count; i++)
            {
                html.Append("<td>" + ancho + "</td>");
            }
            
            html.Append("<td></td>");
            html.Append("<td>Sin Medición</td>");
            html.Append("<td>Sin Medición</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td class='CssTdTiV'>Productiva</td>");
            foreach (var be in niveles)
            {
                html.Append("<td class='CuadMedi'>" + ContarCuadrante("PRODUCTIVA-" + be.Descripcion + "", entidades) + "</td>");
            }
            html.Append("<td></td>");
            html.Append("<td class='CuadMedi'>" + ContarCuadranteSinMedicion("PRODUCTIVA-ES", entidades) + "</td>");
            html.Append("<td class='CuadMedi'>" + ContarCuadranteSinMedicion("PRODUCTIVA-N", entidades) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td class='CssTdTiV'>Estable</td>");
            foreach (var be in niveles)
            {
                html.Append("<td class='CuadMedi'>" + ContarCuadrante("ESTABLE-" + be.Descripcion + "", entidades) + "</td>");
            }
            html.Append("<td></td>");
            html.Append("<td class='CuadMedi'>" + ContarCuadranteSinMedicion("ESTABLE-ES", entidades) + "</td>");
            html.Append("<td class='CuadMedi'>" + ContarCuadranteSinMedicion("ESTABLE-N", entidades) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td class='CssTdTiV'>Critica</td>");
            foreach (var be in niveles)
            {
                html.Append("<td class='CuadMedi'>" + ContarCuadrante("CRITICA-" + be.Descripcion + "", entidades) + "</td>");
            }
            html.Append("<td></td>");
            html.Append("<td class='CuadMedi'>" + ContarCuadranteSinMedicion("CRITICA-ES", entidades) + "</td>");
            html.Append("<td class='CuadMedi'>" + ContarCuadranteSinMedicion("CRITICA-N", entidades) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td></td>");
            foreach (var be in niveles)
            {
                html.Append("<td class='CssTdTiH'>" + be.Descripcion + "</td>");
            }
            html.Append("</td><td>");
            html.Append("<td class='CuadMediTotal'>" + totalAntSinMedicion + "</td>");
            html.Append("<td class='CuadMediTotal'>" + totalNueSinMedicion + "</td>");
            html.Append("</tr>");

            html.Append("</table>");

            html.Append("</td>");
            html.Append("</tr>");
            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");

            w.Write(html.ToString());
            w.Close();
            return 0;
        }

        public string ContarCuadrante(string cuadrante, List<BeDetalleInformacion> entidades)
        {
            var contador = entidades.Count(be => be.Cuadrante == cuadrante && be.Competencia != "");

            var resultado = contador.ToString(CultureInfo.InvariantCulture);

            return resultado;
        }

        public string ContarCuadranteSinMedicion(string cuadrante, List<BeDetalleInformacion> entidades)
        {
            var words = cuadrante.Split('-');
            var contador = 0;

            foreach (var be in entidades)
            {
                if (words[1] == "N")
                {
                    if ((be.Ranking == words[0] && be.Tipo == "N" && be.Cuadrante == "") || (be.Ranking == words[0] && be.Tipo == "N" && be.Competencia == ""))
                    {
                        contador = contador + 1;
                    }
                }

                if (words[1] != "N")
                {
                    if ((be.Ranking == words[0] && be.Tipo != "N" && be.Cuadrante == "") || (be.Ranking == words[0] && be.Tipo != "N" && be.Competencia == ""))
                    {
                        contador = contador + 1;
                    }
                }
            }

            var resultado = contador.ToString(CultureInfo.InvariantCulture);

            return resultado;
        }

        public StringBuilder DoPdf(string ruta, string varFiltros, List<BeDetalleInformacion> entidades, List<BeComun> niveles, string strImages)
        {
            var html = new StringBuilder();

            //string TotalAntSinMedicion = (int.Parse(ProdAnSM) + int.Parse(EstAnSM) + int.Parse(CritAnSM)).ToString();
            //string TotalNueSinMedicion = (int.Parse(ProdNuSM) + int.Parse(EstNuSM) + int.Parse(CritNuSM)).ToString();

            var totalAntSinMedicion = (int.Parse(ContarCuadranteSinMedicion("PRODUCTIVA-ES", entidades)) + int.Parse(ContarCuadranteSinMedicion("ESTABLE-ES", entidades)) + int.Parse(ContarCuadranteSinMedicion("CRITICA-ES", entidades))).ToString(CultureInfo.InvariantCulture);
            var totalNueSinMedicion = (int.Parse(ContarCuadranteSinMedicion("PRODUCTIVA-N", entidades)) + int.Parse(ContarCuadranteSinMedicion("ESTABLE-N", entidades)) + int.Parse(ContarCuadranteSinMedicion("CRITICA-N", entidades))).ToString(CultureInfo.InvariantCulture);

            const string styleTable = "border='0' cellspacing='0' cellpadding='0'";
            const string styleTd = "border='1' align='center'";
            const string styleTd2 = "border='0' align='center'";
            const string styleTdInt = "border='1' align='center' bgcolor='#F5ECF4'";
            
            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title></title>");
            html.Append("</head>");
            html.Append("<body>");
            html.Append("<table " + styleTable + ">");
            html.Append("<tr><td colspan='7'>MATRIZ TALENTO</td></tr>");
            html.Append("<tr><td colspan='7'>" + varFiltros + "</td></tr>");
            html.Append("<tr>");

            html.Append("<td " + styleTd2 + "></td>");
            for (var i = 0; i < niveles.Count; i++)
            {
                html.Append("<td " + styleTd2 + "></td>");
            }
            html.Append("<td " + styleTd2 + "></td>");
            html.Append("<td " + styleTd2 + ">Antiguas</td>");
            html.Append("<td " + styleTd2 + ">Nuevas</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td " + styleTd2 + "></td>");
            for (var i = 0; i < niveles.Count; i++)
            {
                html.Append("<td " + styleTd2 + "></td>");
            }
            html.Append("<td " + styleTd2 + "></td>");
            html.Append("<td " + styleTd2 + ">Sin Medición</td>");
            html.Append("<td " + styleTd2 + ">Sin Medición</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td " + styleTd2 + "><img src='" + strImages + "imgProductiva.png' alt='' align='right'/></td>");
            foreach (var be in niveles)
            {
                html.Append("<td " + styleTdInt + ">" + ContarCuadrante("PRODUCTIVA-" + be.Descripcion + "", entidades) + "</td>");
            }
            html.Append("<td " + styleTd2 + "></td>");
            html.Append("<td " + styleTdInt + ">" + ContarCuadranteSinMedicion("PRODUCTIVA-ES", entidades) + "</td>");
            html.Append("<td " + styleTdInt + ">" + ContarCuadranteSinMedicion("PRODUCTIVA-N", entidades) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td " + styleTd2 + "><img src='" + strImages + "imgEstable.png' alt='' align='right'/></td>");
            foreach (var be in niveles)
            {
                html.Append("<td " + styleTdInt + ">" + ContarCuadrante("ESTABLE-" + be.Descripcion + "", entidades) + "</td>");
            }
            html.Append("<td " + styleTd2 + "></td>");
            html.Append("<td " + styleTdInt + ">" + ContarCuadranteSinMedicion("ESTABLE-ES", entidades) + "</td>");
            html.Append("<td " + styleTdInt + ">" + ContarCuadranteSinMedicion("ESTABLE-N", entidades) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td " + styleTd2 + "><img src='" + strImages + "imgCritica.png' alt='' align='right'/></td>");
            foreach (var be in niveles)
            {
                html.Append("<td " + styleTdInt + ">" + ContarCuadrante("CRITICA-" + be.Descripcion + "", entidades) + "</td>");
            }
            html.Append("<td " + styleTd2 + "></td>");
            html.Append("<td " + styleTdInt + ">" + ContarCuadranteSinMedicion("CRITICA-ES", entidades) + "</td>");
            html.Append("<td " + styleTdInt + ">" + ContarCuadranteSinMedicion("CRITICA-N", entidades) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td " + styleTd2 + "></td>");
            foreach (var be in niveles)
            {
                html.Append("<td color='#ffffff' bgcolor='#652174' " + styleTd + "><p style='font-size:small;'>" + be.Descripcion + "</p></td>");
            }
            html.Append("<td " + styleTd2 + "></td>");
            html.Append("<td " + styleTd2 + ">" + totalAntSinMedicion + "</td>");
            html.Append("<td " + styleTd2 + ">" + totalNueSinMedicion + "</td>");
            html.Append("</tr>");

            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");

            return html;
        }

        private bool FiltraDetalleInformacion(BeDetalleInformacion be)
        {
            var estadoCompetencia = _cuadrante.Split('-');

            if (estadoCompetencia[1].Trim().Equals("Antiguas Sin Medición"))
            {
                return (be.Ranking == estadoCompetencia[0].Trim() && be.Tipo != "N" && be.Cuadrante == "") || (be.Ranking == estadoCompetencia[0].Trim() && be.Tipo != "N" && be.Competencia == "");
            }
            if (estadoCompetencia[1].Trim().Equals("Nuevas Sin Medición"))
            {
                return (be.Ranking == estadoCompetencia[0].Trim() && be.Tipo == "N" && be.Cuadrante == "") || (be.Ranking == estadoCompetencia[0].Trim() && be.Tipo == "N" && be.Competencia == "");
            }
            return be.Cuadrante == _cuadrante && be.Ranking == estadoCompetencia[0].Trim() && be.Competencia != "";
        }

        #endregion "Matriz Talento"

        #region Sustento Toma Acción

        private void VariablesEnfoque(HttpContext context)
        {
            var codPais = context.Request["codPais"];
            var codEvaluado = context.Request["codEvaluado"];
            var idRolEvaluado = context.Request["idRolEvaluado"];
            var periodo = context.Request["periodo"];
            var entidades = _matrizBl.ListarVariablesEnfoque(codPais, codEvaluado, Convert.ToInt32(idRolEvaluado), periodo);
            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        private void VerCondiciones(HttpContext context)
        {
            try
            {
                //string idTomaAccion = context.Request["idTomaAcc"];
                var prefijoIsoPaisEvaluado = context.Request["prefijoIsoPaisEval"];
                var periodo = context.Request["perio"];
                var codEvaluado = context.Request["codEval"];
                var tomaAccion = context.Request["tomaAcc"];
                var idRolEval = context.Request["idRolEval"];

                context.Response.Write(JsonConvert.SerializeObject(_matrizBl.ObtenerVerCondiciones(codEvaluado, prefijoIsoPaisEvaluado, periodo, tomaAccion, idRolEval)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void VerResuldatosSustento(HttpContext context)
        {
            try
            {
                var entidades = GetLisResultadoMatrizSustento(context);
                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Sustento Toma Acción

        #region "Otros"

        private void HeaderTitle(IEnumerable<string> titulos, ref DataTable dt)
        {
            var i = 0;
            foreach (var titulo in titulos)
            {
                dt.Columns[i].ColumnName = titulo;
                i++;
            }
        }

        private List<string> GetPeriodosReportMatriz(IEnumerable<BeResultadoMatriz> entidades, string anhoActual)
        {
            var anhoAnt = (Convert.ToInt32(anhoActual) - 1).ToString(CultureInfo.InvariantCulture);

            var periodosAuxFinal = new List<string>();

            var periodos = (from entidad in entidades where entidad.Tipo == "UC" select entidad.Periodo).ToList();

            var periodosAux = new List<bePeriodoResultado>
            {
                new bePeriodoResultado {Anho = anhoAnt, Descripcion = anhoAnt + " I", Existe = true, Activo = true},
                new bePeriodoResultado
                {
                    Anho = anhoActual,
                    Descripcion = anhoActual + " I",
                    Existe = true,
                    Activo = true
                },
                new bePeriodoResultado {Anho = anhoAnt, Descripcion = anhoAnt + " II", Existe = true, Activo = true},
                new bePeriodoResultado
                {
                    Anho = anhoActual,
                    Descripcion = anhoActual + " II",
                    Existe = true,
                    Activo = true
                },
                new bePeriodoResultado {Anho = anhoAnt, Descripcion = anhoAnt + " III", Existe = true, Activo = true},
                new bePeriodoResultado
                {
                    Anho = anhoActual,
                    Descripcion = anhoActual + " III",
                    Existe = true,
                    Activo = true
                }
            };

            foreach (var v in periodosAux)
            {
                var pass = false;

                foreach (var w in periodos)
                {
                    if (v.Descripcion == w)
                    {
                        pass = true;
                    }
                }
                v.Existe = pass;
            }

            if (periodosAux[0].Existe || periodosAux[1].Existe)
            {
                periodosAuxFinal.Add(periodosAux[0].Descripcion);
                periodosAuxFinal.Add(periodosAux[1].Descripcion);
            }

            if (periodosAux[2].Existe || periodosAux[3].Existe)
            {
                periodosAuxFinal.Add(periodosAux[2].Descripcion);
                periodosAuxFinal.Add(periodosAux[3].Descripcion);
            }
            if (periodosAux[4].Existe || periodosAux[5].Existe)
            {
                periodosAuxFinal.Add(periodosAux[4].Descripcion);
                periodosAuxFinal.Add(periodosAux[5].Descripcion);
            }
            return periodosAuxFinal;
        }

        private List<ReportRow> CreateTableResumenResultado(List<BeResultadoMatriz> entidades, IEnumerable<string> periodos, string varPeriodo, string anho, string anhoAnt, bool esPorcentaje)
        {
            var listaReportRow = new List<ReportRow>();

            var cabecera = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = string.Empty,
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var ranking = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = "Ranking",
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var participacion = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = esPorcentaje ? "%Participación" : "Participación",
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var logro = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = esPorcentaje ? "%Logro" : "Logro",
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            foreach (var periodo in periodos)
            {
                cabecera.Cells.Add(new beCellMatriz { Descripcion = periodo, FontColor = "#ffffff", BackColor = "#5C1D6C" });

                var result = entidades.Find(obj => obj.Periodo == periodo && obj.Tipo == "UC");

                if (result != null)
                {
                    if (result.EstadoPeriodo == "CRITICA")
                    {
                        ranking.Cells.Add(new beCellMatriz { Descripcion = result.EstadoPeriodo, BackColor = "red", FontColor = "#ffffff" });
                    }

                    if (result.EstadoPeriodo == "ESTABLE")
                    {
                        ranking.Cells.Add(new beCellMatriz { Descripcion = result.EstadoPeriodo, BackColor = "yellow" });
                    }

                    if (result.EstadoPeriodo == "PRODUCTIVA")
                    {
                        ranking.Cells.Add(new beCellMatriz { Descripcion = result.EstadoPeriodo, BackColor = "green" });
                    }

                    participacion.Cells.Add(new beCellMatriz { Descripcion = result.ParticipacionPeriodo.ToString(CultureInfo.InvariantCulture) });

                    logro.Cells.Add(new beCellMatriz { Descripcion = result.LogroPeriodo.ToString(CultureInfo.InvariantCulture) });
                }
                else
                {
                    ranking.Cells.Add(new beCellMatriz { Descripcion = "-" });
                    participacion.Cells.Add(new beCellMatriz { Descripcion = "-" });
                    logro.Cells.Add(new beCellMatriz { Descripcion = "-" });
                }
            }

            if (varPeriodo == "00")
            {
                cabecera.Cells.Add(new beCellMatriz { Descripcion = anhoAnt, FontColor = "#ffffff", BackColor = "#5C1D6C" });
                cabecera.Cells.Add(new beCellMatriz { Descripcion = anho, FontColor = "#ffffff", BackColor = "#5C1D6C" });

                IEnumerable<double> varAnt = Enumerable.From(entidades).Where(
                    entidad => entidad.Tipo == "UC" && entidad.Anho == anhoAnt).Select(book => book.ValorRanking);
                var avgValAnt = MatrizHelper.CalculateAvarege(varAnt);

                IEnumerable<double> varAct = Enumerable.From(entidades).Where(
                    entidad => entidad.Tipo == "UC" && entidad.Anho == anho).Select(book => book.ValorRanking);
                var avgValAct = MatrizHelper.CalculateAvarege(varAct);

                ranking.Cells.Add(CalcularEstadoRanking(avgValAnt));

                ranking.Cells.Add(CalcularEstadoRanking(avgValAct));

                varAnt = Enumerable.From(entidades).Where(entidad => entidad.Tipo == "UC" && entidad.Anho == anhoAnt).Select(
                    book => book.ParticipacionPeriodo);
                avgValAnt = MatrizHelper.CalculateAvarege(varAnt);

                varAct = Enumerable.From(entidades).Where(entidad => entidad.Tipo == "UC" && entidad.Anho == anho).Select(
                    book => book.ParticipacionPeriodo);
                avgValAct = MatrizHelper.CalculateAvarege(varAct);

                participacion.Cells.Add(new beCellMatriz { Descripcion = avgValAnt.ToString(CultureInfo.InvariantCulture) });
                participacion.Cells.Add(new beCellMatriz { Descripcion = avgValAct.ToString(CultureInfo.InvariantCulture) });

                varAnt = Enumerable.From(entidades).Where(entidad => entidad.Tipo == "UC" && entidad.Anho == anhoAnt).Select(
                    book => book.LogroPeriodo);
                avgValAnt = MatrizHelper.CalculateAvarege(varAnt);

                varAct = Enumerable.From(entidades).Where(entidad => entidad.Tipo == "UC" && entidad.Anho == anho).Select(
                    book => book.LogroPeriodo);
                avgValAct = MatrizHelper.CalculateAvarege(varAct);

                logro.Cells.Add(new beCellMatriz { Descripcion = avgValAnt.ToString(CultureInfo.InvariantCulture) });
                logro.Cells.Add(new beCellMatriz { Descripcion = avgValAct.ToString(CultureInfo.InvariantCulture) });
            }

            listaReportRow.Add(cabecera);
            listaReportRow.Add(ranking);
            listaReportRow.Add(participacion);
            listaReportRow.Add(logro);

            if (cabecera.Cells.Count == 0)
            {
                listaReportRow.Clear();
            }

            return listaReportRow;
        }

        private beCellMatriz CalcularEstadoRanking(double valor)
        {
            var cell = new beCellMatriz();

            if (valor >= 0 && valor < 3.5)
            {
                cell = new beCellMatriz { Descripcion = "CRITICA", BackColor = "red", FontColor = "#ffffff" };
            }

            if (valor >= 3.5 && valor < 6.5)
            {
                cell = new beCellMatriz { Descripcion = "ESTABLE", BackColor = "yellow" };
            }

            if (valor >= 6.5 && valor <= 9)
            {
                cell = new beCellMatriz { Descripcion = "PRODUCTIVA", BackColor = "green" };
            }

            return cell;
        }

        private List<ReportRow> CreateTableAnhoResultado(List<BeResultadoMatriz> entidades, IEnumerable<string> periodos, string anho, bool esPorcentaje, string codRol)
        {
            var listaReportRow = new List<ReportRow>();

            var cabeceraPeriodo = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = anho,
                MergeRow = 2,
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var cabeceraCampanha = new ReportRow {Cells = new List<beCellMatriz>(), RowName = "X"};

            var ranking = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = "Ranking",
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var participacion = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = esPorcentaje ? "%Participación" : "Participación",
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var logro = new ReportRow
            {
                Cells = new List<beCellMatriz>(),
                RowName = esPorcentaje ? "%Logro" : "Logro",
                BackColor = "#5C1D6C",
                FontColor = "#ffffff"
            };

            var region = new ReportRow {Cells = new List<beCellMatriz>()};

            if (codRol == "5")
                region.RowName = "Región";

            if (codRol == "6")
                region.RowName = "Zona";

            region.BackColor = "#5C1D6C";
            region.FontColor = "#ffffff";

            foreach (var periodo in periodos)
            {
                var datos = entidades.FindAll(obj => obj.Periodo == periodo && obj.Anho == anho);

                if (datos.Count != 0)
                {
                    cabeceraPeriodo.Cells.Add(new beCellMatriz { Descripcion = periodo, MergeCol = datos.Count, FontColor = "#ffffff", BackColor = "#5C1D6C" });

                    foreach (var dato in datos)
                    {
                        cabeceraCampanha.Cells.Add(new beCellMatriz { Descripcion = dato.NombreCampanha, FontColor = "#ffffff", BackColor = "#5C1D6C" });

                        if (dato.EstadoCampana == "CRITICA")
                        {
                            ranking.Cells.Add(new beCellMatriz { Descripcion = dato.EstadoCampana, BackColor = "red", FontColor = "#ffffff" });
                        }

                        if (dato.EstadoCampana == "ESTABLE")
                        {
                            ranking.Cells.Add(new beCellMatriz { Descripcion = dato.EstadoCampana, BackColor = "yellow" });
                        }

                        if (dato.EstadoCampana == "PRODUCTIVA")
                        {
                            ranking.Cells.Add(new beCellMatriz { Descripcion = dato.EstadoCampana, BackColor = "green" });
                        }

                        participacion.Cells.Add(new beCellMatriz { Descripcion = dato.ParticipacionCampana.ToString(CultureInfo.InvariantCulture) });
                        logro.Cells.Add(new beCellMatriz { Descripcion = dato.LogroCampana.ToString(CultureInfo.InvariantCulture) });
                        region.Cells.Add(new beCellMatriz { Descripcion = dato.CodRegionZona });
                    }
                }
            }

            listaReportRow.Add(cabeceraPeriodo);
            listaReportRow.Add(cabeceraCampanha);
            listaReportRow.Add(ranking);
            listaReportRow.Add(participacion);
            listaReportRow.Add(logro);
            listaReportRow.Add(region);

            if (cabeceraPeriodo.Cells.Count == 0)
            {
                listaReportRow.Clear();
            }

            return listaReportRow;
        }

        private string NombreCodXML(IEnumerable<BeComun> lista, string codigo)
        {
            var nombre = string.Empty;

            foreach (var item in lista)
            {
                if (item.Codigo == codigo)
                {
                    nombre = item.Descripcion;
                    break;
                }
            }
            return nombre;
        }

        #endregion "Otros"

        #region "Matriz Zona"

        private void LoadTiposMZ(HttpContext context)
        {
            try
            {
                var path = context.Server.MapPath(@"~/Matriz/XML/") + context.Request["fileName"];

                var entidades = Utils.XmlToObjectList<BeComun>(path, "//BeComuns/BeComun");

                var adicional = context.Request["adicional"];

                if (adicional.ToLower() == "si")
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "Todos" });
                }

                if (adicional.ToLower() == "select")
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ObtenerTipoMz(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                const byte estado = Constantes.EstadoActivo;

                var tipoMz = _matrizBl.ObtenerTipoMz(codPais, estado);
                context.Response.Write(JsonConvert.SerializeObject(tipoMz));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ObtenerListaMz(HttpContext context)
        {
            try
            {
                var entidades = ListaMatrizZona(context);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                context.Response.Write(JsonConvert.SerializeObject("Error: " + ex.Message));
            }
        }
        private List<BeMatrizZona> ListaMatrizZona(HttpContext context)
        {
            var entidades = new List<BeMatrizZona>();
            var obj = new BeMatrizZonaVariables
            {
                Tipo = context.Request["tipoMz"],
                Pais = context.Request["paisMz"],
                Region = context.Request["regionMz"],
                Anho = context.Request["anhoMz"],
                Periodos = context.Request["periodosMz"],
                Benchmark = Constantes.Benchmark
            };
            var path = context.Server.MapPath(@"~/Matriz/XML/");
            var tamPoblacionMz = Utils.XmlToObjectList<BeComun>(path + "TamPoblacionMz.xml", "//BeComuns/BeComun");
            var tamVentaMz = Utils.XmlToObjectList<BeComun>(path + "TamVentaMz.xml", "//BeComuns/BeComun");
            var ranGap = Utils.XmlToObjectList<BeComun>(path + "RanGap.xml", "//BeComuns/BeComun");
            var ranFacGap = Utils.XmlToObjectList<BeComun>(path + "RanFacGap.xml", "//BeComuns/BeComun");

            foreach (var item in tamPoblacionMz)
            {
                if (item.Codigo == Constantes.Pequenho)
                {
                    obj.PequenhoTP = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Mediano)
                {
                    obj.MedianoTP = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Grande)
                {
                    obj.GrandeTP = decimal.Parse(item.Descripcion);
                }
            }

            foreach (var item in tamVentaMz)
            {
                if (item.Codigo == Constantes.Pequenho)
                {
                    obj.PequenhoTV = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Mediano)
                {
                    obj.MedianoTV = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Grande)
                {
                    obj.GrandeTV = decimal.Parse(item.Descripcion);
                }
            }

            foreach (var item in ranGap)
            {
                if (item.Codigo == Constantes.Bajo)
                {
                    obj.BajoRG = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Medio)
                {
                    obj.MedioRG = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Alto)
                {
                    obj.AltoRG = decimal.Parse(item.Descripcion);
                }
            }

            foreach (var item in ranFacGap)
            {
                if (item.Codigo == Constantes.Bajo)
                {
                    obj.BajoRFG = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Medio)
                {
                    obj.MedioRFG = decimal.Parse(item.Descripcion);
                }
                if (item.Codigo == Constantes.Alto)
                {
                    obj.AltoRFG = decimal.Parse(item.Descripcion);
                }
            }

            switch (obj.Tipo)
            {
                case "01":
                    entidades = _matrizBl.ListaMatrizZonaSinFuenteVentas(obj);
                    break;
                case "02":
                    entidades = _matrizBl.ListaMatrizZonaConFuenteVentas(obj);
                    break;
            }

            return entidades;
        }


        private void DescargarDetalleInformacionMz(HttpContext context)
        {
            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Matriz/TempReportes/");
            _zonaMz = context.Request["zonaMz"];
            var tipoMz = context.Request["tipoMz"];

            var entidades = ListaMatrizZona(context);


            var rpteResultadoMz = entidades.FindAll(FiltraDetalleInformacionMz);



            var data = Utils.ConvertToDataTable(rpteResultadoMz);

            var fileName = string.Format("{0}.{1}", "DetalleInformacionMz" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), "xls");
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();

            data.TableName = "Detalle de Información Matriz Zona"; //titulo

            //Crear Titulos


            switch (tipoMz)
            {
                case "01":
                    headerTitles.Add("Gerente Zona");
                    headerTitles.Add("Zona");
                    headerTitles.Add("Activas Finales");
                    headerTitles.Add("Población");
                    headerTitles.Add("T. Población");
                    headerTitles.Add("Penetración");
                    headerTitles.Add("Benchmark 1");
                    headerTitles.Add("GAP Penetracion");
                    headerTitles.Add("Rangos GAp");
                    headerTitles.Add("Factor ");
                    headerTitles.Add("Benchmark 2");
                    headerTitles.Add("PenetracionFactor");
                    headerTitles.Add("GAP Factor");
                    headerTitles.Add("Rangos GAP2");
                    headerTitles.Add("Ventas MN");
                    headerTitles.Add("Tamaño Venta");
                    headerTitles.Add("Cuadrante ");
                    headerTitles.Add("Participación");

                    break;
                case "02":
                    headerTitles.Add("Gerente Zona");
                    headerTitles.Add("Zona");
                    headerTitles.Add("Activas Finales");
                    headerTitles.Add("T. Población");
                    headerTitles.Add("Población");
                    headerTitles.Add("GPS");
                    headerTitles.Add("Penetración");
                    headerTitles.Add("Benchmark 1");
                    headerTitles.Add("GAP Penetracion");
                    headerTitles.Add("Rangos GAp");
                    headerTitles.Add("Grupo ");
                    headerTitles.Add("Factor ");
                    headerTitles.Add("Benchmark 2");
                    headerTitles.Add("PenetracionFactor");
                    headerTitles.Add("GAP Factor");
                    headerTitles.Add("Rangos GAP2");
                    headerTitles.Add("Ventas MN");
                    headerTitles.Add("Tamaño Venta");
                    headerTitles.Add("Cuadrante ");
                    headerTitles.Add("Participación");

                    break;
            }



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
            context.Response.ContentType = "application/xls";
            context.Response.BinaryWrite(archivo);
            context.Response.Flush();
            MatrizHelper.DeleteFile(strFilePath);
            context.Response.End();
        }

        private bool FiltraDetalleInformacionMz(BeMatrizZona be)
        {
            return be.CodZona.Trim() == _zonaMz;
        }




        public StringBuilder DoPdfMz(string ruta, string varFiltros, List<BeMatrizZona> entidades, string strImages)
        {
            var html = new StringBuilder();

            //string TotalAntSinMedicion = (int.Parse(ContarCuadranteSinMedicion("PRODUCTIVA-ES", entidades)) + int.Parse(ContarCuadranteSinMedicion("ESTABLE-ES", entidades)) + int.Parse(ContarCuadranteSinMedicion("CRITICA-ES", entidades))).ToString();
            //string TotalNueSinMedicion = (int.Parse(ContarCuadranteSinMedicion("PRODUCTIVA-N", entidades)) + int.Parse(ContarCuadranteSinMedicion("ESTABLE-N", entidades)) + int.Parse(ContarCuadranteSinMedicion("CRITICA-N", entidades))).ToString();

            //string styleTABLE = "border='0' cellspacing='0' cellpadding='0'";
            //string styleTABLE1 = "border='1' cellspacing='0' cellpadding='0' HEIGHT='100'";
            //string styleTD = "align='center'";
            //string styleTDInt = "align='center' bgcolor='#F5ECF4'";
            //string spacio = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title></title>");
            html.Append("</head>");
            html.Append("<body>");
            html.Append("<table border='0' cellspacing='0' cellpadding='0'>");
            html.Append("<tr><td>MATRIZ ZONA</td></tr>");
            html.Append("<tr><td>" + varFiltros + "</td></tr>");
            html.Append("<tr>");
            html.Append("<td>");
            html.Append("<table border='1'>");
            html.Append("<tr>");
            html.Append("<td border='0' align='right'><img src='" + strImages + "altoMz.png' alt='' align='right'/></td>");
            //Inicio Cuadrante 7
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante7 = entidades.FindAll(FiltraMzxCuadrante7);
            var contar7 = 0;
            foreach (var be in cuadrante7)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar7 = contar7 + 1;
                var residuo7 = contar7 % 4;

                if (residuo7 == 0)
                {
                    html.Append("<br/>");
                }
            }
            html.Append("</td>");
            //Fin Cuadrante 7
            //Inicio Cuadrante 8
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante8 = entidades.FindAll(FiltraMzxCuadrante8);
            var contar8 = 0;
            foreach (var be in cuadrante8)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar8 = contar8 + 1;
                var residuo8 = contar8 % 4;

                if (residuo8 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 8
            //Inicio Cuadrante 9
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante9 = entidades.FindAll(FiltraMzxCuadrante9);
            var contar9 = 0;
            foreach (var be in cuadrante9)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar9 = contar9 + 1;
                var residuo9 = contar9 % 4;

                if (residuo9 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 9
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td border='0' align='right'><img src='" + strImages + "medioMz.png' alt='' align='right'/></td>");
            //Inicio Cuadrante 4
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante4 = entidades.FindAll(FiltraMzxCuadrante4);
            var contar4 = 0;
            foreach (var be in cuadrante4)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar4 = contar4 + 1;
                var residuo4 = contar4 % 4;

                if (residuo4 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 4
            //Inicio Cuadrante 5
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante5 = entidades.FindAll(FiltraMzxCuadrante5);
            var contar5 = 0;
            foreach (var be in cuadrante5)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar5 = contar5 + 1;
                var residuo5 = contar5 % 4;

                if (residuo5 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 5
            //Inicio Cuadrante 6
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante6 = entidades.FindAll(FiltraMzxCuadrante6);
            var contar6 = 0;
            foreach (var be in cuadrante6)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar6 = contar6 + 1;
                var residuo6 = contar6 % 4;

                if (residuo6 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 6
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td border='0' align='right'><img src='" + strImages + "bajoMz.png' alt='' align='right'/></td>");
            //Inicio Cuadrante 1
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante1 = entidades.FindAll(FiltraMzxCuadrante1);
            var contar1 = 0;
            foreach (var be in cuadrante1)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar1 = contar1 + 1;
                var residuo1 = contar1 % 4;

                if (residuo1 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 1
            //Inicio Cuadrante 2
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante2 = entidades.FindAll(FiltraMzxCuadrante2);
            var contar2 = 0;
            foreach (var be in cuadrante2)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar2 = contar2 + 1;
                var residuo2 = contar2 % 4;

                if (residuo2 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 2
            //Inicio Cuadrante 3
            html.Append("<td align='center' bgcolor='#C0DEF6'>");
            var cuadrante3 = entidades.FindAll(FiltraMzxCuadrante3);
            var contar3 = 0;
            foreach (var be in cuadrante3)
            {
                html.Append("<img src='" + strImages + "burbuja" + (Math.Truncate(be.Participacion)) + ".png' alt=''/>");
                contar3 = contar3 + 1;
                var residuo3 = contar3 % 4;

                if (residuo3 == 0)
                {
                    html.Append("<br/>");
                }

            }
            html.Append("</td>");
            //Fin Cuadrante 3
            html.Append("</tr>");
            html.Append("<tr>");
            html.Append("<td border='0'></td>");
            html.Append("<td color='#ffffff' bgcolor='#037CB3' align='center' valign='middle'><p style='font-size:large;'>Pequeño</p></td>");
            html.Append("<td color='#ffffff' bgcolor='#037CB3' align='center' valign='middle'><p style='font-size:large;'>Mediano</p></td>");
            html.Append("<td color='#ffffff' bgcolor='#037CB3' align='center' valign='middle'><p style='font-size:large;'>Grande</p></td>");
            html.Append("</tr>");
            html.Append("</table>");
            html.Append("</td>");
            html.Append("</tr>");
            html.Append("</table>");
            html.Append("</body>");
            html.Append("</html>");

            return html;
        }

        private bool FiltraMzxCuadrante9(BeMatrizZona be)
        {
            return be.Cuadrante == 9;
        }
        private bool FiltraMzxCuadrante8(BeMatrizZona be)
        {
            return be.Cuadrante == 8;
        }
        private bool FiltraMzxCuadrante7(BeMatrizZona be)
        {
            return be.Cuadrante == 7;
        }
        private bool FiltraMzxCuadrante6(BeMatrizZona be)
        {
            return be.Cuadrante == 6;
        }
        private bool FiltraMzxCuadrante5(BeMatrizZona be)
        {
            return be.Cuadrante == 5;
        }
        private bool FiltraMzxCuadrante4(BeMatrizZona be)
        {
            return be.Cuadrante == 4;
        }
        private bool FiltraMzxCuadrante3(BeMatrizZona be)
        {
            return be.Cuadrante == 3;
        }
        private bool FiltraMzxCuadrante2(BeMatrizZona be)
        {
            return be.Cuadrante == 2;
        }
        private bool FiltraMzxCuadrante1(BeMatrizZona be)
        {
            return be.Cuadrante == 1;
        }


        private void DescargarMatrizZona(HttpContext context)
        {
            var entidades = ListaMatrizZona(context);

            var id = Guid.NewGuid().ToString();
            var strFolder = context.Server.MapPath(@"~/Matriz/TempReportes/");
            var strImages = context.Server.MapPath(@"~/Styles/images/burbuja/");

            var nomTipoMz = context.Request["nomTipoMz"];
            var nomPais = context.Request["nomPais"];
            var anhoMz = context.Request["anhoMz"];
            var nomPeriodo = context.Request["nomPeriodo"];
            var nomRegion = context.Request["nomRegion"];

            var fileName = string.Format("{0}.{1}", "MatrizZona" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), "pdf");
            var strFilePath = strFolder + id + fileName;

            _filtros = " Tipo de Matriz: " + nomTipoMz + " | País: " + nomPais + " | Año: " + anhoMz + " | Periodo: " + nomPeriodo + " | Región: " + nomRegion + "";

            var strB = DoPdfMz(strFilePath, _filtros, entidades, strImages);

            var document = new Document(PageSize.A4_LANDSCAPE.Rotate(), 15, 15, 5, 5);
            var stream = new FileStream(strFilePath, FileMode.Create);
            var pdfWriter = PdfWriter.GetInstance(document, stream);
            //HtmlParser.Parse(document, context.Server.MapPath("origin.html"));

            document.Open();
            // now read the Grid html one by one and add into the document object
            using (TextReader sReader = new StringReader(strB.ToString()))
            {
                var list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                foreach (var elm in list)
                {
                    document.Add(elm);
                }
            }
            document.Close();
            stream.Close();
            pdfWriter.Close();

            var archivoPdf = MatrizHelper.ReadFile(strFilePath);
            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            context.Response.ContentType = "application/" + "pdf";
            context.Response.BinaryWrite(archivoPdf);
            context.Response.Flush();
            MatrizHelper.DeleteFile(strFilePath);
            context.Response.End();
        }




        private void LoadPaisMz(HttpContext context)
        {
            var codPaisUsuario = context.Request["codPaisUsuario"];

            var paisBl = new BlPais();
            
            var paises = paisBl.ObtenerPaisesBeComunMz(codPaisUsuario);



            context.Response.Write(JsonConvert.SerializeObject(paises));
        }

        #endregion "Matriz Zona"

        #region MatrizConsolidada



        #endregion

    }
}