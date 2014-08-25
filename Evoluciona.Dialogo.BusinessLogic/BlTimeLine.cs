
using System.Globalization;

namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlTimeLine
    {
        private static readonly DaTimeLine DaTimeLine = new DaTimeLine();

        /// <summary>
        /// este método obtiene las gr/gz a las cuales se le hizo una toma de acción con sus respectivos estados en cada campaña del periodo
        /// </summary>
        /// <param name="codPaisEvaluador">código del país evaluador</param>
        /// <param name="codUsuarioEvaluador">código del usuario evaluador</param>
        /// <param name="idRolEvaluador">id rol del evaluador</param>
        /// <param name="idRolEvaluado">id del rol evaluado</param>
        /// <param name="codTomaAccion">código de la toma de acción</param>
        /// <param name="periodo">periodo</param>
        /// <param name="urlImage"></param>
        /// <param name="rutaRelativa"></param>
        /// <returns>detalle Time Line</returns>
        public List<BeTimeLine> ListarTimeLine(string codPaisEvaluador, string codUsuarioEvaluador, int idRolEvaluador, int idRolEvaluado, string codTomaAccion, string periodo, string urlImage, string rutaRelativa)
        {
            try
            {
                var entidadesDetalle = DaTimeLine.ListarTimeLine(codPaisEvaluador, codUsuarioEvaluador, idRolEvaluador, idRolEvaluado, codTomaAccion, periodo);
                return ProcesarTimeLine(entidadesDetalle, periodo, urlImage, rutaRelativa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<BeTimeLine> ProcesarTimeLine(List<BeTimeLineDetetalle> entidadesDetalle, string periodo, string urlImage, string rutaRelativa)
        {
            var entidades = new List<BeTimeLine>();

            if (entidadesDetalle.Count <= 0) return entidades;
            var usuarios = GetUsuarios(entidadesDetalle);

            foreach (var usuario in usuarios)
            {
                var evento = GetDatosEvent(entidadesDetalle.FindAll(p => p.CodDocIdentidad == usuario.Codigo), periodo, urlImage);

                string imgNomIcon;

                switch (evento.Referencia)
                {
                    case "01":
                        imgNomIcon = "red-circle.png";
                        break;
                    case "02":
                        imgNomIcon = "green-circle.png";
                        break;
                    case "03":
                        imgNomIcon = "blue-circle.png";
                        break;
                    default:
                        imgNomIcon = "blue-circle.png";
                        break;
                }

                entidades.Add(new BeTimeLine
                {
                    title = usuario.Descripcion,
                    start = evento.Codigo,
                    description = evento.Descripcion,
                    icon = rutaRelativa + "Jscripts/TimeLine/timeline_js/images/" + imgNomIcon + ""

                });
            }

            return entidades;
        }

        private static IEnumerable<BeComun> GetCampanhasAnho(string anho)
        {
            var campanhas = new List<BeComun>();

            var campFecha = string.Empty;

            for (var i = 1; i <= 18; i++)
            {
                switch (i)
                {
                    case 1:
                        campFecha = anho + "-01-" + "10";
                        break;
                    case 2:
                        campFecha = anho + "-01-" + "30";
                        break;
                    case 3:
                        campFecha = anho + "-02-" + "19";
                        break;
                    case 4:
                        campFecha = anho + "-03-" + "11";
                        break;
                    case 5:
                        campFecha = anho + "-03-" + "31";
                        break;
                    case 6:
                        campFecha = anho + "-04-" + "20";
                        break;
                    case 7:
                        campFecha = anho + "-05-" + "10";
                        break;
                    case 8:
                        campFecha = anho + "-05-" + "30";
                        break;
                    case 9:
                        campFecha = anho + "-06-" + "20";
                        break;
                    case 10:
                        campFecha = anho + "-07-" + "09";
                        break;
                    case 11:
                        campFecha = anho + "-07-" + "29";
                        break;
                    case 12:
                        campFecha = anho + "-08-" + "18";
                        break;
                    case 13:
                        campFecha = anho + "-09-" + "07";
                        break;
                    case 14:
                        campFecha = anho + "-09-" + "27";
                        break;
                    case 15:
                        campFecha = anho + "-10-" + "16";
                        break;
                    case 16:
                        campFecha = anho + "-11-" + "05";
                        break;
                    case 17:
                        campFecha = anho + "-11-" + "25";
                        break;
                    case 18:
                        campFecha = anho + "-12-" + "15";
                        break;
                }

                var campanha = new BeComun
                {
                    Codigo = campFecha,
                    Descripcion = string.Format("{0}{1}", anho, i.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'))
                };
                campanhas.Add(campanha);
            }

            return campanhas;
        }

        private IEnumerable<BeComun> GetUsuarios(List<BeTimeLineDetetalle> entidadesDetalle)
        {
            var entidades = new List<BeComun>();

            if (entidadesDetalle.Count <= 0) return entidades;
            foreach (var detalle in entidadesDetalle)
            {
                var value = entidades.Find(p => p.Codigo == detalle.CodDocIdentidad);

                if (value == null)
                {
                    entidades.Add(new BeComun
                    {
                        Codigo = detalle.CodDocIdentidad,
                        Descripcion = detalle.Nombre
                    });
                }
            }

            return entidades;
        }

        private BeComun GetDatosEvent(List<BeTimeLineDetetalle> entidadesDetalleUsuario, string periodo, string urlImage)
        {
            var evento = new BeComun();
            var anho = periodo.Substring(0, 4);

            if (entidadesDetalleUsuario.Count <= 0) return evento;
            var descripcion = "<table id='tablaCriticas' border='0' cellpadding='0' width='100%'>";
            descripcion += string.Format("<tr><td align='center' colspan='19' class='tituloSeguimiento'>Seguimiento Año : {0}</td></tr><tr>", anho);
            var fechaEvento = string.Empty;
            var campanhaTom = string.Empty;
            var observaciones = string.Empty;
            var tomAccion = string.Empty;

            var listaCampAnho = GetCampanhasAnho(anho);

            foreach (var camp in listaCampAnho)
            {
                descripcion += "<td class='texto_campania'>";
                var nombreImagen = "gris.jpg";
                foreach (var detalle in entidadesDetalleUsuario)
                {
                    if (detalle.CodTomaAccion != "01")//Plan de Mejora
                    {
                        detalle.CampanhaToma = GetCampanhaTomaAccion(detalle.PeriodoToma.Trim());
                    }


                    //fecha evento
                    if (camp.Descripcion == detalle.CampanhaToma)
                    {
                        fechaEvento = camp.Codigo;
                    }
                    //estado Camp

                    if (camp.Descripcion != detalle.AnhoCampanha) continue;
                    switch (detalle.EstadoCampanha)
                    {
                        case "CRITICA":
                            nombreImagen = "rojo.jpg";
                            break;
                        case "ESTABLE":
                            nombreImagen = "amarillo.jpg";
                            break;
                        case "PRODUCTIVA":
                            nombreImagen = "verde_oscuro.jpg";
                            break;
                    }
                    campanhaTom = detalle.CampanhaToma;
                    observaciones = detalle.Observaciones;

                    switch (detalle.CodTomaAccion)
                    {
                        case "01":
                            tomAccion = "Plan de Mejora";
                            break;
                        case "02":
                            tomAccion = "Reasignación";
                            break;
                        case "03":
                            tomAccion = "Rotación Saludable";
                            break;
                    }
                }

                descripcion += string.Format("<img src='{0}Images/{1}' alt='' width='16px' height='16px' /><br/> C {2} </td>", urlImage, nombreImagen, camp.Descripcion.Substring(4, 2));
            }

            descripcion += string.Format("<td style='padding-left:10px;'><p style='font-size:x-small;'>Toma Acción: {0}</p><br/><p style='font-size:x-small;'>Fecha Inicio: {1}</p><br/><p style='font-size:x-small;'>Campaña: {2}</p></td>", tomAccion, ((DateTime.Parse(fechaEvento))).ToString("dd/MM/yyyy"), campanhaTom);
            descripcion += string.Format("<td style='padding-left:10px'><img alt='leyenda' src='{0}Images/leyenda.jpg' complete='complete'/></td></tr><tr>", urlImage);
            descripcion += string.Format("<td align='left' colspan='19' style='padding-left:10px'><p style='font-size:small;'>Observaciones: {0}</p></td></tr></table>", observaciones);
            evento.Codigo = fechaEvento;
            evento.Descripcion = descripcion;

            switch (tomAccion)
            {
                case "Plan de Mejora":
                    evento.Referencia = "01";
                    break;
                case "Reasignación":
                    evento.Referencia = "02";
                    break;
                case "Rotación Saludable":
                    evento.Referencia = "03 Saludable";
                    break;
            }

            return evento;
        }

        private string GetCampanhaTomaAccion(string periodo)
        {
            var anho = periodo.Substring(0, 4);
            var campanha = string.Empty;

            switch (periodo.Substring(5, periodo.Length - 5))
            {
                case "I":
                    campanha = anho + "01";
                    break;

                case "II":
                    campanha = anho + "07";
                    break;
                case "III":

                    campanha = anho + "13";
                    break;
            }

            return campanha;
        }
    }
}