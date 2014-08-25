
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
        private readonly BlMatrizAdmin _matrizAdminBL = new BlMatrizAdmin();
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

            List<BeCronogramaMatriz> entidades = _matrizAdminBL.ListaCronogramaMatriz(pais);

            return entidades;
        }

        private void DeleteCronograma(HttpContext context)
        {

            int idCronogramaMatriz = Int32.Parse(context.Request["id"]);

            bool estado = _matrizAdminBL.DeleteCronogramaMatriz(idCronogramaMatriz);

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

            bool estado = _matrizAdminBL.UpdateCronogramaMatriz(obj);

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

            bool estado = _matrizAdminBL.InsertCronogramaMatriz(obj);

            context.Response.Write(JsonConvert.SerializeObject(estado));

        }


        private void CargarPaises(HttpContext context)
        {
            string tipoAdmin = context.Request["tipo"];
            string codigoPais = context.Request["codPais"];

            var matrizAdminBL = new BlMatrizAdmin();
            var entidades = new List<BeComun>();



            switch (tipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    entidades = matrizAdminBL.ObtenerPaises();
                    break;
                case Constantes.RolAdminPais:
                    entidades.Add(matrizAdminBL.ObtenerPais(codigoPais));
                    break;
                case Constantes.RolAdminEvaluciona:
                    entidades.Add(matrizAdminBL.ObtenerPais(codigoPais));
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
            int id = Int32.Parse(context.Request["id"]);

            var matrizAdminBL = new BlMatrizAdmin();

            var entidades = matrizAdminBL.SelectCronograma(id);


            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        private void ObtenerFechaServer(HttpContext context)
        {
            var matrizAdminBL = new BlMatrizAdmin();

            var entidades = matrizAdminBL.ObtenerFechaServer();

            context.Response.Write(JsonConvert.SerializeObject(entidades));
        }

        #endregion

        #region NivelCompetencia

        public void InsertarNivelesCompetencia(HttpContext context)
        {
            try
            {
                string codpais = context.Request["codpais"];
                string anio = context.Request["anio"];
                string usuario = context.Request["usuario"];
                string maximo = context.Request["maximo"];
                string minimo = context.Request["minimo"];
                string competencia = context.Request["competencia"];

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
                string codPais = context.Request["codPais"];
                string anio = context.Request["anio"];

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
                string codPais = context.Request["codPais"];
                string tipoCondicion = context.Request["tipoCondicion"];
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
                string usuario = context.Request["usuario"];

                var bl = new BlCondiciones();

                bool insertado = bl.ActualizarCondiciones(be, usuario);
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
                string tipoCondicion = context.Request["tipoCondicion"];
                string numeroCondicionLineamiento = context.Request["numeroCondicionLineamiento"];
                string prefijoIsoPais = context.Request["prefijoIsoPais"];

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
                string usuario = context.Request["usuario"];

                bool insertado = bld.ActualizarCondicionesDetalle(bed, usuario);
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
                string codPais = context.Request["pPais"];


                List<BeGrupoGps> listGruposGps = bl.ObtenerGruposGps(codPais);

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
                string codPais = context.Request["pPais"];
                string xml = context.Request["pXml"];


                int resul = bl.GrabarGruposGps(codPais, xml);

                context.Response.Write(JsonConvert.SerializeObject(resul));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void obtenerPaisConFuenteVentas(HttpContext context)
        {
            var matrizAdminBL = new BlMatrizAdmin();

            List<BeComun> entidades = matrizAdminBL.ObtenerPaisConFuenteVentas();

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
