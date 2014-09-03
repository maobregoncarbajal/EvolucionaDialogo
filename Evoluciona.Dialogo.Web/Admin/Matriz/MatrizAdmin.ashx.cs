
namespace Evoluciona.Dialogo.Web.Admin.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Services;

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MatrizAdmin : IHttpHandler
    {
        #region Variables
        private readonly BlMatrizAdmin _matrizAdminBl = new BlMatrizAdmin();
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "cronograma":
                    LoadCronograma(context);
                    break;
                case "deleteCronograma":
                    DeleteCronograma(context);
                    break;
                case "updateCronograma":
                    UpdateCronograma(context);
                    break;
                case "insertCronograma":
                    InsertCronograma(context);
                    break;
                case "pais":
                    CargarPaises(context);
                    break;
                case "selectCronograma":
                    SelectCronograma(context);
                    break;
                case "obtenerFechaServer":
                    ObtenerFechaServer(context);
                    break;
                case "insertarNivelesCompetencia":
                    InsertarNivelesCompetencia(context);
                    break;
                case "obtenerNivelesCompetencia":
                    ObtenerNivelesCompetencia(context);
                    break;
                case "obtenerCondiciones":
                    ObtenerCondiciones(context);
                    break;
                case "actualizarCondiciones":
                    ActualizarCondiciones(context);
                    break;
                case "obtenerCondicionesDetalle":
                    ObtenerCondicionesDetalle(context);
                    break;
                case "actualizarCondicionesDetalle":
                    ActualizarCondicionesDetalle(context);
                    break;
                case "buscarZonaGPS":
                    BuscarZonaGps(context);
                    break;
                case "grabarZonaGPS":
                    GrabarZonaGps(context);
                    break;
                case "paisConFuenteVentas":
                    obtenerPaisConFuenteVentas(context);
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

        #region Cronograma

        private void LoadCronograma(HttpContext context)
        {
            try
            {
                var entidades = ListaCronogramaMatriz(context);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private List<BeCronogramaMatriz> ListaCronogramaMatriz(HttpContext context)
        {
            string pais = null;

            if (!String.IsNullOrEmpty(context.Request["pais"].Trim()))
            {
                pais = context.Request["pais"].Trim();
            }

            var entidades = _matrizAdminBl.ListaCronogramaMatriz(pais);

            return entidades;
        }

        private void DeleteCronograma(HttpContext context)
        {

            var idCronogramaMatriz = Int32.Parse(context.Request["id"]);

            var estado = _matrizAdminBl.DeleteCronogramaMatriz(idCronogramaMatriz);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }


        private void UpdateCronograma(HttpContext context)
        {
            var obj = new BeCronogramaMatriz
            {
                IdCronogramaMatriz = Int32.Parse(context.Request["id"]),
                PrefijoIsoPais = context.Request["PrefijoIsoPais"],
                FechaLimite = DateTime.Parse(context.Request["FechaLimite"])
            };

            //obj.Periodo = context.Request["Periodo"];

            if (!String.IsNullOrEmpty(context.Request["FechaProrroga"]))
            {
                obj.FechaProrroga = DateTime.Parse(context.Request["FechaProrroga"]);
            }

            obj.UsuarioModi = context.Request["Usuario"];

            obj.EsUltimo = true;

            var estado = _matrizAdminBl.UpdateCronogramaMatriz(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }


        private void InsertCronograma(HttpContext context)
        {
            var obj = new BeCronogramaMatriz
            {
                PrefijoIsoPais = context.Request["PrefijoIsoPais"],
                FechaLimite = DateTime.Parse(context.Request["FechaLimite"])
            };

            //obj.Periodo = context.Request["Periodo"];

            if (!String.IsNullOrEmpty(context.Request["FechaProrroga"]))
            {
                obj.FechaProrroga = DateTime.Parse(context.Request["FechaProrroga"]);
            }

            obj.UsuarioCrea = context.Request["Usuario"];

            obj.EsUltimo = true;

            var estado = _matrizAdminBl.InsertCronogramaMatriz(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }


        private void CargarPaises(HttpContext context)
        {
            var tipoAdmin = context.Request["tipo"];
            var codigoPais = context.Request["codPais"];

            var matrizAdminBl = new BlMatrizAdmin();
            var entidades = new List<BeComun>();



            switch (tipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    entidades = matrizAdminBl.ObtenerPaises();
                    break;
                case Constantes.RolAdminPais:
                    entidades.Add(matrizAdminBl.ObtenerPais(codigoPais));
                    break;
                case Constantes.RolAdminEvaluciona:
                    entidades.Add(matrizAdminBl.ObtenerPais(codigoPais));
                    break;
            }


            if (entidades.Count > 0)
            {
                if (!string.IsNullOrEmpty(context.Request["select"]))
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                }
            }


            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        private void SelectCronograma(HttpContext context)
        {
            var id = Int32.Parse(context.Request["id"]);

            var matrizAdminBl = new BlMatrizAdmin();

            var entidades = matrizAdminBl.SelectCronograma(id);


            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        private void ObtenerFechaServer(HttpContext context)
        {
            var matrizAdminBl = new BlMatrizAdmin();

            var entidades = matrizAdminBl.ObtenerFechaServer();

            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        #endregion

        #region NivelCompetencia

        public void InsertarNivelesCompetencia(HttpContext context)
        {
            try
            {
                var codpais = context.Request["codpais"];
                var anio = context.Request["anio"];
                var usuario = context.Request["usuario"];
                var maximo = context.Request["maximo"];
                var minimo = context.Request["minimo"];
                var competencia = context.Request["competencia"];

                var blma = new BlMatrizAdmin();
                var benc = new BeNivelesCompetencia
                {
                    prefijoIsoPais = codpais,
                    anio = anio,
                    MaxValue = Convert.ToDecimal(maximo),
                    MinValue = Convert.ToDecimal(minimo),
                    CodCompetencia = competencia,
                    estadoActivo = 1
                };

                var insertado = blma.InsertarNivelesCompetencia(benc, usuario);
                context.Response.Write(JsonConvert.SerializeObject(insertado));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ObtenerNivelesCompetencia(HttpContext context)
        {
            try
            {
                var bl = new BlMatrizAdmin();
                var codPais = context.Request["codPais"];
                var anio = context.Request["anio"];

                var nivelesCompetencia = bl.ObtenerNivelesCompetencia(codPais, anio, 1);

                context.Response.Write(JsonConvert.SerializeObject(nivelesCompetencia));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Condiciones

        public void ObtenerCondiciones(HttpContext context)
        {
            try
            {
                var bl = new BlCondiciones();
                var codPais = context.Request["codPais"];
                var tipoCondicion = context.Request["tipoCondicion"];
                const int estado = -1;

                var listCondiciones = bl.ObtenerCondiciones(codPais, tipoCondicion, estado);

                context.Response.Write(JsonConvert.SerializeObject(listCondiciones));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarCondiciones(HttpContext context)
        {
            try
            {
                var be = (List<BeCondiciones>)JsonConvert.DeserializeObject(context.Request["condiciones"], typeof(List<BeCondiciones>));
                var usuario = context.Request["usuario"];

                var bl = new BlCondiciones();

                var insertado = bl.ActualizarCondiciones(be, usuario);
                context.Response.Write(JsonConvert.SerializeObject(insertado));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ObtenerCondicionesDetalle(HttpContext context)
        {
            try
            {
                var bl = new BlCondicionesDetalle();
                var tipoCondicion = context.Request["tipoCondicion"];
                var numeroCondicionLineamiento = context.Request["numeroCondicionLineamiento"];
                var prefijoIsoPais = context.Request["prefijoIsoPais"];

                var listCondicionesDetalle = bl.ObtenerCondicionesDetalle(prefijoIsoPais, tipoCondicion, numeroCondicionLineamiento);

                context.Response.Write(JsonConvert.SerializeObject(listCondicionesDetalle));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarCondicionesDetalle(HttpContext context)
        {
            try
            {
                var bld = new BlCondicionesDetalle();

                var bed = (List<BeCondicionesDetalle>)JsonConvert.DeserializeObject(context.Request["condicionesDetalle"], typeof(List<BeCondicionesDetalle>));
                var usuario = context.Request["usuario"];

                var insertado = bld.ActualizarCondicionesDetalle(bed, usuario);
                context.Response.Write(JsonConvert.SerializeObject(insertado));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Matriz Zona

        public void BuscarZonaGps(HttpContext context)
        {
            try
            {
                var bl = new BlGrupoGps();
                var codPais = context.Request["pPais"];


                var listGruposGps = bl.ObtenerGruposGps(codPais);

                context.Response.Write(JsonConvert.SerializeObject(listGruposGps));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GrabarZonaGps(HttpContext context)
        {
            try
            {
                var bl = new BlGrupoGps();
                var codPais = context.Request["pPais"];
                var xml = context.Request["pXml"];


                var resul = bl.GrabarGruposGps(codPais, xml);

                context.Response.Write(JsonConvert.SerializeObject(resul));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void obtenerPaisConFuenteVentas(HttpContext context)
        {
            var matrizAdminBl = new BlMatrizAdmin();

            var entidades = matrizAdminBl.ObtenerPaisConFuenteVentas();

            if (entidades.Count > 0)
            {
                if (!string.IsNullOrEmpty(context.Request["select"]))
                {
                    entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = "[Seleccionar]" });
                }
            }

            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }
        #endregion
    }
}
