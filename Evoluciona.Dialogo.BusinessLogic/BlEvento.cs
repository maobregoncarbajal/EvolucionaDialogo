
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlEvento
    {
        private static readonly DaEvento DaEvento = new DaEvento();

        public List<BeEvento> ObtenerEventos(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta)
        {
            return DaEvento.ObtenerEventos(codigoUsuario, fechaDesde, fechaHasta);
        }

        public List<BeEvento> ObtenerEventosSinVisitas(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta)
        {
            return DaEvento.ObtenerEventosSinVisitas(codigoUsuario, fechaDesde, fechaHasta);
        }

        public List<BeEvento> ObtenerEventos(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta, string filtro)
        {
            return DaEvento.ObtenerEventos(codigoUsuario, fechaDesde, fechaHasta, filtro);
        }

        public List<BeEvento> ObtenerEventosSinVisitas(string codigoUsuario, DateTime fechaDesde, DateTime fechaHasta, string filtro)
        {
            return DaEvento.ObtenerEventosSinVisitas(codigoUsuario, fechaDesde, fechaHasta, filtro);
        }

        public BeEvento ObtenerEvento(int idEvento)
        {
            return DaEvento.ObtenerEvento(idEvento);
        }

        public int RegistrarEvento(BeEvento evento)
        {
            return DaEvento.RegistrarEvento(evento);
        }

        public void ActualizarEvento(BeEvento evento)
        {
            DaEvento.ActualizarEvento(evento);
        }

        public void EliminarEvento(int idEvento)
        {
            DaEvento.EliminarEvento(idEvento);
        }

        public string ObtenerCadenaConexion(string codPais, string ffvv)
        {
            return DaEvento.ObtenerCadenaConexion(codPais, ffvv);
        }

        public List<string> ObtenerAnhos(string cadenaConexion)
        {
            return string.IsNullOrEmpty(cadenaConexion) ? null : DaEvento.ObtenerAnhos(cadenaConexion);
        }

        public List<string> ObtenerNumerosCampanha(string cadenaConexion, string anho)
        {
            return string.IsNullOrEmpty(cadenaConexion) ? null : DaEvento.ObtenerNumerosCampanha(cadenaConexion, anho);
        }

        public BeEvento ObtenerCampanha(string cadenaConexion, string campanha, int rolUsuario, string codigoUsuario)
        {
            return string.IsNullOrEmpty(cadenaConexion) ? null : DaEvento.ObtenerCampanha(cadenaConexion, campanha, rolUsuario, codigoUsuario);
        }

        public List<BeComun> ObtenerFiltros(string codigoEvaluador, int rolEvaluador)
        {

            return DaEvento.ObtenerFiltros(codigoEvaluador, rolEvaluador);
        }

        public List<BeEvento> ObtenerEventosCampanha(string codigoEvaluador, int rolEvaluador, string codigoFiltro, DateTime fechaDesde, DateTime fechaHasta)
        {

            return DaEvento.ObtenerEventosCampanha(codigoEvaluador, rolEvaluador, codigoFiltro, fechaDesde, fechaHasta);
        }

        public List<BeEvento> ObtenerDetalleEventosCampanha(string codigoEvaluador, int rolEvaluador, string codigoFiltro, DateTime fecha)
        {

            return DaEvento.ObtenerDetalleEventosCampanha(codigoEvaluador, rolEvaluador, codigoFiltro, fecha);
        }

        public List<string> ObtenerCampanhasPosiblesPorFecha(string codPais, string ffvv, int rolEvaluado, string codigoEvaluado, DateTime fecha)
        {

            return DaEvento.ObtenerCampanhasPosiblesPorFecha(codPais, ffvv, rolEvaluado, codigoEvaluado, fecha);
        }
    }
}