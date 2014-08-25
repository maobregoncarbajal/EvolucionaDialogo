
namespace Evoluciona.Dialogo.Web.Reportes
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
    public class TimeLineHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["accion"])
            {
                case "loadPais":
                    LoadPais(context);
                    break;
                case "loadRol":
                    LoadRol(context);
                    break;
                case "periodo":
                    LaodPeriodos(context);
                    break;
                case "loadTimeLine":
                    LoadTimeLine(context);
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
                List<BeComun> entidades = new List<BeComun>();
                BlPais paisBL = new BlPais();
                List<BePais> paises = paisBL.ObtenerPaises();

                foreach (BePais pais in paises)
                {
                    entidades.Add(new BeComun
                                      {
                                          Codigo = pais.prefijoIsoPais,
                                          Descripcion = pais.NombrePais
                                      });
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
                string codPais = context.Request["codPais"];
                string anho = context.Request["anho"];
                string idRol = context.Request["idRol"];
                BlMatriz matrizBL = new BlMatriz();
                List<BeComun> entidades = matrizBL.ListarPeriodos(codPais, anho, Convert.ToInt32(idRol));
                string descripcion = "Todos";

                entidades.Insert(0, new BeComun { Codigo = "00", Descripcion = descripcion });

                foreach (BeComun entidad in entidades)
                {
                    if (entidad.Codigo != "00")
                    entidad.Descripcion = entidad.Codigo;
                }

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadRol(HttpContext context)
        {
            try
            {
                string idRolEvaluador = context.Request["idRolEvaluador"];

                List<BeComun> entidades = new List<BeComun>();

                if (Convert.ToInt32(idRolEvaluador) == (int)TipoRol.DirectoraVentas)
                {
                    entidades.Add(new BeComun
                    {
                        Codigo = ((int)TipoRol.GerenteRegion).ToString(),
                        Descripcion = "Gerente Región"
                    });
                }

                entidades.Add(new BeComun
                {
                    Codigo = ((int)TipoRol.GerenteZona).ToString(),
                    Descripcion = "Gerente Zona"
                });

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadTimeLine(HttpContext context)
        {
            try
            {
                string codPaisEvaluador = context.Request["codPaisEvaluador"];
                string codUsuarioEvaluador = context.Request["codUsuarioEvaluador"];
                string idRolEvaluador = context.Request["idRolEvaluador"];
                string idRolEvaluado = context.Request["idRolEvaluado"];
                string periodo = context.Request["periodo"];
                string codTomaAccion = context.Request["codTomaAccion"];
                string urlImage = context.Request["urlImage"];
                string rutaRelativa = context.Request["rutaRelativa"];

                BlTimeLine timeLineBL = new BlTimeLine();
                List<BeTimeLine> entidades = timeLineBL.ListarTimeLine(codPaisEvaluador, codUsuarioEvaluador, Convert.ToInt32(idRolEvaluador),
                                                                       Convert.ToInt32(idRolEvaluado), codTomaAccion, periodo, urlImage, rutaRelativa);

                context.Response.Write(JsonConvert.SerializeObject(entidades));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}