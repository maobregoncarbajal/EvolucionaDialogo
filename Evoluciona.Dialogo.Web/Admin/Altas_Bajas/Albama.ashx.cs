

using System.Linq;

namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Web;
    using System.Web.Configuration;
    using BusinessEntity;
    using BusinessLogic;
    using Newtonsoft.Json;
    using System.Web.Services;
    using WsDirectorio;
    /// <summary>
    /// Descripción breve de $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Albama : IHttpHandler
    {
        #region Variables
        private readonly BlAlbama _blAlbama = new BlAlbama();
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "loadPais":
                    LoadPais(context);
                    break;
                case "loadRegion":
                    LoadRegion(context);
                    break;
                case "loadZona":
                    LoadZona(context);
                    break;
                case "loadCargo":
                    LoadCargo(context);
                    break;
                case "loadEstadoCargo":
                    LoadEstadoCargo(context);
                    break;
                case "loadDirectorio":
                    LoadDirectorio(context);
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

        private void LoadPais(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var oListPais = _blAlbama.ListarPaises(codPais);
                oListPais.Insert(0, new BeComun { Codigo = "00", Descripcion = "[TODOS]" });

                context.Response.Write(JsonConvert.SerializeObject(oListPais));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        private void LoadRegion(HttpContext context)
        {
            try
            {
                var codPais = context.Request["codPais"];
                var oListRegion = _blAlbama.ListarRegiones(codPais);
                oListRegion.Insert(0, new BeComun { Codigo = codPais + "|", Descripcion = "[TODOS]" });

                context.Response.Write(JsonConvert.SerializeObject(oListRegion));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void LoadZona(HttpContext context)
        {
            try
            {
                var codPaisRegion = context.Request["codPaisRegion"].Split('|');
                var codPais = codPaisRegion[0].Trim();
                var codRegion = codPaisRegion[1].Trim();
                var oListZona = new List<BeComun>();

                if (Equals(codRegion, ""))
                {
                    oListZona.Insert(0, new BeComun { Codigo = codPais + "||", Descripcion = "[TODOS]" });
                }
                else
                {
                    oListZona = _blAlbama.ListarZonas(codPais, codRegion);
                    oListZona.Insert(0, new BeComun { Codigo = codPais + "||", Descripcion = "[TODOS]" });
                }

                context.Response.Write(JsonConvert.SerializeObject(oListZona));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadCargo(HttpContext context)
        {
            try
            {
                var codCargo = context.Request["codCargo"];
                var oListCargo = _blAlbama.ListarCargo(codCargo);

                oListCargo.Insert(0, new BeComun { Codigo = "", Descripcion = "[TODOS]" });

                context.Response.Write(JsonConvert.SerializeObject(oListCargo));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void LoadEstadoCargo(HttpContext context)
        {
            try
            {
                var codEstadoCargo = context.Request["codEstadoCargo"];
                var oListEstadoCargo = _blAlbama.ListarEstadoCargo(codEstadoCargo);

                oListEstadoCargo.Insert(0, new BeComun { Codigo = "", Descripcion = "[TODOS]" });

                context.Response.Write(JsonConvert.SerializeObject(oListEstadoCargo));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void LoadDirectorio(HttpContext context)
        {
            var paisRegionZona = context.Request["paisRegionZona"].Split('|');
            var pais = paisRegionZona[0].Trim();
            var region = paisRegionZona[1].Trim();
            var zona = paisRegionZona[2].Trim();
            var cargo = context.Request["cargo"];
            var periodo = context.Request["periodo"];
            var estadoCargo = context.Request["estadoCargo"];


            var oListPaises = new List<BeComun>();

            if (String.Equals(pais, "00"))
            {
                oListPaises = _blAlbama.ListarPaises("00");
            }
            else
            {
                var oPais = new BeComun { Codigo = pais, Descripcion = String.Empty, Referencia = String.Empty };
                oListPaises.Add(oPais);
            }


            var resultado = ConsumirWsObtenerClientesDirectorio(oListPaises, region, zona, cargo, periodo, estadoCargo);


            context.Response.Write(JsonConvert.SerializeObject(resultado));

        }


        private static bool DeleteClientesDirectorio(List<BeComun> oListPaises)
        {
            var blWsDirectorio = new BlWsDirectorio();
            var cantPaises = oListPaises.Count;
            var resultado = false;

            if (cantPaises > 0)
            {
                resultado = blWsDirectorio.DeleteClientesDirectorio("00");

            }
            else
            {
                foreach (var oPais in oListPaises)
                {
                    resultado = blWsDirectorio.DeleteClientesDirectorio(oPais.Codigo);
                }
            }

            return resultado;
        }

        private string InsertarClientesDirectorio(ProcesoDIRWebServiceResultado resultado)
        {
            var dtClienteDirWebService = ConvertToDataTable(resultado.clienteDIRWebService);
            var blWsDirectorio = new BlWsDirectorio();

            return blWsDirectorio.InsertClientesDirectorio(dtClienteDirWebService);

        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            var properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }



        private ProcesoDIRWebServiceResultado ConsumirWsObtenerClientesDirectorio(List<BeComun> oListPaises, string region, string zona, string cargo, string periodo, string estadoCargo)
        {
            var tablaLimpiada = false;
            var resultado = new ProcesoDIRWebServiceResultado();

            var lstClienteDirWebService = new List<ClienteDIRWebService>();
            var mensaje = String.Empty;
            var codigo = String.Empty;
            var ultPais = oListPaises[oListPaises.Count - 1];
            var blWsDirectorio = new BlWsDirectorio();

            foreach (var oPais in oListPaises)
            {
                var separador = String.Empty;

                if (oPais.Codigo != ultPais.Codigo)
                {
                    separador = "|";
                }


                var wsDirectorio = new ProcesoDIRWebServiceImplService { Url = WebConfigurationManager.AppSettings.Get("WsDirectorio" + oPais.Codigo) };

                try
                {
                    resultado = wsDirectorio.obtenerClientesDirectorio(oPais.Codigo, region, zona, cargo, periodo, estadoCargo);

                    switch (resultado.codigo)
                    {
                        case "0":
                            {
                                if (!tablaLimpiada)
                                {
                                    tablaLimpiada = DeleteClientesDirectorio(oListPaises);
                                }

                                var strInsert = InsertarClientesDirectorio(resultado);

                                if (strInsert == "1")
                                {
                                    var lstPaisClienteDirWebService = ListarDirectorio(resultado);
                                    lstClienteDirWebService.AddRange(lstPaisClienteDirWebService);
                                    mensaje = mensaje + oPais.Codigo + " : " + "Correcto " + resultado.mensaje + separador;
                                    codigo = codigo + oPais.Codigo + " : " + resultado.codigo + separador;
                                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, "Correcto " + resultado.mensaje);
                                }
                                else
                                {
                                    mensaje = mensaje + oPais.Codigo + " : " + strInsert + separador;
                                    codigo = codigo + oPais.Codigo + " : " + "-1" + separador;
                                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, strInsert);
                                }
                            }
                            break;
                        default:
                            mensaje = mensaje + oPais.Codigo + " : " + resultado.mensaje + separador;
                            codigo = codigo + oPais.Codigo + " : " + resultado.codigo + separador;
                            blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, resultado.mensaje);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    mensaje = mensaje + oPais.Codigo + " : " + exception.Message + separador;
                    codigo = codigo + oPais.Codigo + " : " + "-1" + separador;
                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, exception.Message);
                }
            }

            resultado.clienteDIRWebService = lstClienteDirWebService.ToArray();
            resultado.codigo = codigo;
            resultado.mensaje = mensaje;


            return resultado;
        }
        
        private static IEnumerable<ClienteDIRWebService> ListarDirectorio(ProcesoDIRWebServiceResultado resultado)
        {
            return resultado.clienteDIRWebService.Select(clienteDirWebService => new ClienteDIRWebService
            {
                apeMat = clienteDirWebService.apeMat
                , apePat = clienteDirWebService.apePat
                , cargo = clienteDirWebService.cargo
                , codCargo = clienteDirWebService.codCargo
                , codCliente = clienteDirWebService.codCliente
                , codGrupoFuncional = clienteDirWebService.codGrupoFuncional
                , codigoPeriodo = clienteDirWebService.codigoPeriodo
                , codNovedad = clienteDirWebService.codNovedad
                , codPais = clienteDirWebService.codPais
                , codPerfil = clienteDirWebService.codPerfil
                , codPlanilla = clienteDirWebService.codPlanilla
                , codRegion = clienteDirWebService.codRegion
                , codRol = clienteDirWebService.codRol
                , codZona = clienteDirWebService.codZona
                , CUB = clienteDirWebService.CUB
                , CUBJefe = clienteDirWebService.CUBJefe
                , desGrupoFuncional = clienteDirWebService.desGrupoFuncional
                , desRegion = clienteDirWebService.desRegion
                , desRelContractual = clienteDirWebService.desRelContractual
                , desZona = clienteDirWebService.desZona
                , estado = clienteDirWebService.estado
                , estadoCargo = clienteDirWebService.estadoCargo
                , fechaFin = clienteDirWebService.fechaFin
                , fechaInicio = clienteDirWebService.fechaInicio
                , fecIngreso = clienteDirWebService.fecIngreso
                , genero = clienteDirWebService.genero
                , jefeDirecto = clienteDirWebService.jefeDirecto
                , mailBelcorp = clienteDirWebService.mailBelcorp
                , nombres = clienteDirWebService.nombres
                , nroDoc = clienteDirWebService.nroDoc
                , perfil = clienteDirWebService.perfil
                , puestoOrg = clienteDirWebService.puestoOrg
                , rol = clienteDirWebService.rol
                , telefCasa = clienteDirWebService.telefCasa
                , telefMovil = clienteDirWebService.telefMovil
                , usuarioRed = clienteDirWebService.usuarioRed
            }).ToList();
        }
    }
}

    

