
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web;
    using System.Web.SessionState;

    public class Metodos : IHttpHandler, IRequiresSessionState
    {
        private const string ParametroFFVV = "FS";
        private static readonly BlEvento EventoBL = new BlEvento();
        private static readonly BlTipoEvento TipoEventoBL = new BlTipoEvento();

        public void ProcessRequest(HttpContext context)
        {
            string accion = context.Request["accion"];
            Object resultado = null;

            switch (accion)
            {
                case "CargarAnhos":
                    resultado = CargarAnhos(context);
                    break;
                case "CargarNumerosCampanha":
                    resultado = CargarNumerosCampanha(context);
                    break;
                case "CargarRangoFechas":
                    resultado = CargarRangoFechas(context);
                    break;
                case "MostrarDetalleVisitas":
                    resultado = MostrarDetalleVisitas(context);
                    break;
                case "MostrarDetalleEventos":
                    resultado = MostrarDetalleEventos(context);
                    break;
                case "CargarTiposEvento":
                    resultado = CargarTiposEvento(context);
                    break;
                case "CargarCampanhas":
                    resultado = CargarCampanhas(context);
                    break;
                case "CargarSubEvento":
                    resultado = CargarSubEvento(context);
                    break;
                case "CargarGerentesRegion":
                    resultado = CargarGerentesRegion(context);
                    break;
                case "CargarGerentesZona":
                    resultado = CargarGerentesZona(context);
                    break;
                case "CargarLideres":
                    resultado = CargarLideres(context);
                    break;
                case "AgregarEvento":
                    resultado = AgregarEvento(context);
                    break;
                case "ObtenerEvento":
                    resultado = ObtenerEvento(context);
                    break;
                case "ActualizarEvento":
                    resultado = ActualizarEvento(context);
                    break;
                case "EliminarEvento":
                    resultado = EliminarEvento(context);
                    break;
                case "CargarEvaluados":
                    resultado = CargarEvaluados(context);
                    break;
                case "CargarFechasReuniones":
                    resultado = CargarFechasReuniones(context);
                    break;
                case "MostrarDetalleVisitasEval":
                    resultado = MostrarDetalleVisitasEval(context);
                    break;
                case "MostrarDetalleEventosEval":
                    resultado = MostrarDetalleEventosEval(context);
                    break;
                case "CargarFiltros":
                    resultado = CargarFiltros(context);
                    break;
                case "CargarFechasReunionesFiltro":
                    resultado = CargarFechasReunionesFiltro(context);
                    break;
            }

            EnviarRespuestaJSON(resultado, context);
        }

        private string CargarAnhos(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            string cadenaConexion = ObtenerCadenaConexion(prefijoIsoPais);

            List<string> listaAnhos = EventoBL.ObtenerAnhos(cadenaConexion);
            string combo = BuilderComboBox(listaAnhos);
            return combo;
        }

        private string CargarNumerosCampanha(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            string anhoCampanha = context.Request["anho"];
            string cadenaConexion = ObtenerCadenaConexion(prefijoIsoPais);

            List<string> listaNumerosCampanha = EventoBL.ObtenerNumerosCampanha(cadenaConexion, anhoCampanha);
            string combo = BuilderComboBox(listaNumerosCampanha);
            return combo;
        }

        private BeEvento CargarRangoFechas(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            string anhoCampanha = context.Request["anho"];
            string numeroCampanha = context.Request["numero"];
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            string codigoUsuario = context.Request["codigoUsuario"];

            string campanha = anhoCampanha + numeroCampanha;
            string cadenaConexion = ObtenerCadenaConexion(prefijoIsoPais);

            BeEvento beEvento = EventoBL.ObtenerCampanha(cadenaConexion, campanha, codigoRol, codigoUsuario);
            return beEvento;
        }

        private List<BeEvento> MostrarDetalleVisitas(HttpContext context)
        {
            string id = context.Request["id"];
            string fecha = context.Request["fecha"];
            string codigoUsuario = context.Request["codigoUsuario"];
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);

            DateTime fechaEvento = Convert.ToDateTime(fecha);

            List<BeEvento> eventos = EventoBL.ObtenerDetalleEventosCampanha(codigoUsuario, codigoRol, id, fechaEvento);
            return eventos;
        }

        private List<BeEvento> MostrarDetalleEventos(HttpContext context)
        {
            string id = context.Request["id"];

            BeEvento evento = EventoBL.ObtenerEvento(Convert.ToInt32(id));
            List<BeEvento> eventos = new List<BeEvento>();
            eventos.Add(evento);

            return eventos;
        }

        private string CargarTiposEvento(HttpContext context)
        {
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            int tipoReunion = Convert.ToInt32(context.Request["reunion"]);

            List<BeTipoEvento> tiposEvento = TipoEventoBL.ObtenerTipoEventos(tipoReunion, codigoRol);
            string combo = BuilderComboBox(tiposEvento, "IDTipoEvento", "Descripcion");
            return combo;
        }

        private string CargarCampanhas(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            string codigoUsuario = context.Request["codigoUsuario"];

            DateTime fecha = Convert.ToDateTime(context.Request["fecha"]);
            List<string> campanhas = EventoBL.ObtenerCampanhasPosiblesPorFecha(prefijoIsoPais, ParametroFFVV, codigoRol, codigoUsuario, fecha);
            string combo = BuilderComboBox(campanhas);
            return combo;
        }

        private string CargarSubEvento(HttpContext context)
        {
            int tipoEvento = Convert.ToInt32(context.Request["tipoEvento"]);
            List<BeTipoEvento> subEventos = TipoEventoBL.ObtenerSubEventos(tipoEvento);
            string combo = BuilderComboBox(subEventos, "IDTipoEvento", "Descripcion");
            return combo;
        }

        private string CargarGerentesRegion(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];

            BlConfiguracion blConfiguracion = new BlConfiguracion();
            List<BeUsuario> gerentesRegion = blConfiguracion.ObtenerGerentesVenta(prefijoIsoPais);
            string combo = BuilderComboBox(gerentesRegion, "codigoUsuario", "nombreUsuario");
            return combo;
        }

        private string CargarGerentesZona(HttpContext context)
        {
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            string codigoUsuario = context.Request["codigoUsuario"];

            BlConfiguracion blConfiguracion = new BlConfiguracion();
            List<BeUsuario> gerentesZona = blConfiguracion.ObtenerGerentesZona(codigoUsuario, prefijoIsoPais);
            string combo = BuilderComboBox(gerentesZona, "codigoUsuario", "nombreUsuario");
            return combo;
        }

        private string CargarLideres(HttpContext context)
        {
            string codigoUsuario = context.Request["codigoUsuario"];
            string prefijoIsoPais = context.Request["prefijoIsoPais"];

            BlConfiguracion blConfiguracion = new BlConfiguracion();
            List<BeUsuario> gerentesZona = blConfiguracion.ObtenerLideres(codigoUsuario, prefijoIsoPais);
            string combo = BuilderComboBox(gerentesZona, "codigoUsuario", "nombreUsuario");
            return combo;
        }

        public ResponseData AgregarEvento(HttpContext context)
        {
            ResponseData respuesta = new ResponseData();

            try
            {
                BeEvento evento = new BeEvento();
                evento.Asunto = context.Request["Asunto"];
                evento.Fecha = context.Request["Fecha"];
                evento.Campanha = context.Request["Campanha"];
                evento.Evaluado = context.Request["Evaluado"];
                evento.Evento = Convert.ToInt32(context.Request["Evento"]);
                evento.Reunion = Convert.ToInt32(context.Request["Reunion"]);
                evento.SubEvento = Convert.ToInt32(context.Request["SubEvento"]);
                evento.FechaInicio = Convert.ToDateTime(evento.Fecha);
                evento.FechaFin = evento.FechaInicio.AddHours(1);
                evento.CodigoUsuario = context.Request["codigoUsuario"];
                evento.RolUsuario = Convert.ToInt32(context.Request["codigoRol"]);

                int idEvento = EventoBL.RegistrarEvento(evento);
                evento.IDEvento = idEvento;

                respuesta.Success = true;
                respuesta.Data = evento;
            }
            catch (Exception)
            {
                respuesta.Success = false;
            }

            return respuesta;
        }

        public BeEvento ObtenerEvento(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["id"]);
            BeEvento evento = EventoBL.ObtenerEvento(id);
            if (evento != null)
            {
                return evento;
            }
            return new BeEvento();
        }

        public ResponseData ActualizarEvento(HttpContext context)
        {
            ResponseData respuesta = new ResponseData();

            try
            {
                BeEvento evento = new BeEvento();
                evento.IDEvento = Convert.ToInt32(context.Request["IDEvento"]);
                evento.Asunto = context.Request["Asunto"];
                evento.Fecha = context.Request["Fecha"];
                evento.Campanha = context.Request["Campanha"];
                evento.Evaluado = context.Request["Evaluado"];
                evento.Evento = Convert.ToInt32(context.Request["Evento"]);
                evento.Reunion = Convert.ToInt32(context.Request["Reunion"]);
                evento.SubEvento = Convert.ToInt32(context.Request["SubEvento"]);

                BeEvento eventoActual = EventoBL.ObtenerEvento(evento.IDEvento);

                if (eventoActual != null)
                {
                    evento.FechaInicio = Convert.ToDateTime(evento.Fecha);
                    evento.FechaFin = evento.FechaInicio.AddHours(1);

                    EventoBL.ActualizarEvento(evento);

                    respuesta.Success = true;
                    respuesta.Data = evento;
                    respuesta.Message = string.Format("Evento: '{0}' Actualizado!", evento.Asunto);
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.Message = string.Format("No se pudo Actualizar el Evento: '{0}'", evento.Asunto);
                }
            }
            catch (Exception)
            {
                respuesta.Success = false;
                respuesta.Message = "Ocurrio un error al Actualizar el Evento";
            }
            return respuesta;
        }

        public ResponseData EliminarEvento(HttpContext context)
        {
            ResponseData respuesta = new ResponseData();

            try
            {
                int id = Convert.ToInt32(context.Request["id"]);
                BeEvento evento = EventoBL.ObtenerEvento(id);

                if (evento != null)
                {
                    EventoBL.EliminarEvento(id);

                    respuesta.Success = true;
                    respuesta.Message = string.Format("Evento: '{0}' Eliminado!", evento.Asunto);
                }
                else
                {
                    respuesta.Success = false;
                    respuesta.Message = string.Format("No se pudo Eliminar el Evento con ID: '{0}'", id);
                }
            }
            catch (Exception)
            {
                respuesta.Success = false;
                respuesta.Message = "Ocurrio un error al Eliminar el Evento.";
            }
            return respuesta;
        }

        private string CargarEvaluados(HttpContext context)
        {
            string combo = string.Empty;
            string prefijoIsoPais = context.Request["prefijoIsoPais"];
            string codigoUsuario = context.Request["codigoUsuario"];
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);

            BlConfiguracion ConfiguracionBL = new BlConfiguracion();

            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    List<BeUsuario> gerentesRegion = ConfiguracionBL.ObtenerGerentesVenta(prefijoIsoPais);
                    combo = BuilderComboBox(gerentesRegion, "codigoUsuario", "nombreUsuario");
                    break;
                case Constantes.RolGerenteRegion:
                    List<BeUsuario> gerentesZona = ConfiguracionBL.ObtenerGerentesZona(codigoUsuario, prefijoIsoPais);
                    combo = BuilderComboBox(gerentesZona, "codigoUsuario", "nombreUsuario");
                    break;
            }
            return combo;
        }

        private List<BeEvento> CargarFechasReuniones(HttpContext context)
        {
            string codEvaluado = context.Request["idEvaluado"];
            DateTime fechaInicio = Convert.ToDateTime(context.Request["fechaInicio"]);
            DateTime fechaFin = Convert.ToDateTime(context.Request["fechaFin"]);
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            string prefijoIsoPais = context.Request["prefijoIsoPais"];

            List<BeEvento> eventos = new List<BeEvento>();
            List<BeEvento> eventosVisitas = new List<BeEvento>();
            List<BeEvento> eventosNormales = new List<BeEvento>();
            BlUsuario UsuarioBL = new BlUsuario();

            try
            {
                switch (codigoRol)
                {
                    case Constantes.RolDirectorVentas:
                        {
                            BeUsuario evaluado = UsuarioBL.ObtenerDatosUsuario(prefijoIsoPais, Constantes.RolGerenteRegion, codEvaluado, Constantes.EstadoActivo);
                            if (evaluado != null)
                            {
                                eventosVisitas = EventoBL.ObtenerEventosCampanha(evaluado.codigoUsuario, Constantes.RolGerenteRegion, "0", fechaInicio, fechaFin);
                                eventosNormales = EventoBL.ObtenerEventosSinVisitas(evaluado.codigoUsuario, fechaInicio, fechaFin);
                            }
                        }
                        break;
                    case Constantes.RolGerenteRegion:
                        {
                            BeUsuario evaluado = UsuarioBL.ObtenerDatosUsuario(prefijoIsoPais, Constantes.RolGerenteZona, codEvaluado, Constantes.EstadoActivo);
                            if (evaluado != null)
                            {
                                eventosVisitas = EventoBL.ObtenerEventosCampanha(evaluado.codigoUsuario, Constantes.RolGerenteZona, "0", fechaInicio, fechaFin);
                                eventosNormales = EventoBL.ObtenerEventosSinVisitas(evaluado.codigoUsuario, fechaInicio, fechaFin);
                            }
                        }
                        break;
                }
            }
            catch (Exception)
            {
            }

            eventos.AddRange(eventosVisitas);
            eventos.AddRange(eventosNormales);

            return eventos;
        }

        private List<BeEvento> MostrarDetalleVisitasEval(HttpContext context)
        {
            string id = context.Request["id"];
            string fecha = context.Request["fecha"];
            string codEvaluado = context.Request["idEvaluado"];
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            string prefijoIsoPais = context.Request["prefijoIsoPais"];

            DateTime fechaEvento = Convert.ToDateTime(fecha);
            List<BeEvento> eventos = new List<BeEvento>();
            BlUsuario UsuarioBL = new BlUsuario();

            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    {
                        BeUsuario evaluado = UsuarioBL.ObtenerDatosUsuario(prefijoIsoPais, Constantes.RolGerenteRegion, codEvaluado, Constantes.EstadoActivo);
                        if (evaluado != null)
                        {
                            eventos = EventoBL.ObtenerDetalleEventosCampanha(evaluado.codigoUsuario, Constantes.RolGerenteRegion, id, fechaEvento);
                        }
                    }
                    break;
                case Constantes.RolGerenteRegion:
                    {
                        BeUsuario evaluado = UsuarioBL.ObtenerDatosUsuario(prefijoIsoPais, Constantes.RolGerenteZona, codEvaluado, Constantes.EstadoActivo);
                        if (evaluado != null)
                        {
                            eventos = EventoBL.ObtenerDetalleEventosCampanha(evaluado.codigoUsuario, Constantes.RolGerenteZona, id, fechaEvento);
                        }
                    }
                    break;
            }

            return eventos;
        }

        private List<BeEvento> MostrarDetalleEventosEval(HttpContext context)
        {
            string id = context.Request["id"];

            List<BeEvento> eventos = new List<BeEvento>();
            BeEvento evento = EventoBL.ObtenerEvento(Convert.ToInt32(id));
            eventos.Add(evento);

            return eventos;
        }

        private List<BeComun> CargarFiltros(HttpContext context)
        {
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            string codigoUsuario = context.Request["codigoUsuario"];

            List<BeComun> listadoFiltros = EventoBL.ObtenerFiltros(codigoUsuario, codigoRol);
            return listadoFiltros;
        }

        private List<BeEvento> CargarFechasReunionesFiltro(HttpContext context)
        {
            DateTime fechaInicio = Convert.ToDateTime(context.Request["fechaInicio"]);
            DateTime fechaFin = Convert.ToDateTime(context.Request["fechaFin"]);
            int codigoRol = Convert.ToInt32(context.Request["codigoRol"]);
            string codigoUsuario = context.Request["codigoUsuario"];

            List<BeEvento> eventos = new List<BeEvento>();

            bool mostrarFechaVisitas = Convert.ToBoolean(context.Request["mostrarFechaVisitas"]);

            if (mostrarFechaVisitas)
            {
                List<BeEvento> eventosVisitas = EventoBL.ObtenerEventosCampanha(codigoUsuario, codigoRol, "0", fechaInicio, fechaFin);
                List<BeEvento> eventosNormales = EventoBL.ObtenerEventos(codigoUsuario, fechaInicio, fechaFin);

                eventos.AddRange(eventosVisitas);
                eventos.AddRange(eventosNormales);
            }
            else
            {
                List<BeEvento> eventosNormales = EventoBL.ObtenerEventos(codigoUsuario, fechaInicio, fechaFin);

                eventos.AddRange(eventosNormales);
            }

            return eventos;
        }

        private string ObtenerCadenaConexion(string prefijoIsoPais)
        {
            if (string.IsNullOrEmpty(prefijoIsoPais)) return string.Empty;

            string cadenaConexion = EventoBL.ObtenerCadenaConexion(prefijoIsoPais, ParametroFFVV);
            return cadenaConexion;
        }

        private static string BuilderComboBox(List<string> lista)
        {
            string opciones = string.Empty;

            if (lista != null)
            {
                foreach (string item in lista)
                {
                    opciones += string.Format("<option value='{0}'>{0}</option>", item);
                }
            }

            return opciones;
        }

        public static string BuilderComboBox<T>(List<T> list, string valueMember, string displayMember)
        {
            string formatString = string.Empty;
            try
            {
                foreach (T entity in list)
                {
                    PropertyInfo p1 = entity.GetType().GetProperty(valueMember);
                    object v1 = p1.GetValue(entity, null);

                    PropertyInfo p2 = entity.GetType().GetProperty(displayMember);
                    object v2 = p2.GetValue(entity, null);

                    formatString += string.Format("<option value='{0}'>{1}</option>", v1, v2);
                }
            }
            catch (Exception) { }

            return formatString;
        }

        private void EnviarRespuestaJSON(Object objecto, HttpContext context)
        {
            string res = JsonConvert.SerializeObject(objecto, Formatting.Indented);

            context.Response.ContentType = "application/json";
            context.Response.Write(res);
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }

    #region Clase Interna

    public class ResponseData
    {
        public bool Success;
        public string Message;
        public object Data;
    }

    #endregion Clase Interna
}