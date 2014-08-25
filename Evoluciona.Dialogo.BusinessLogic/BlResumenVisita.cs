
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlResumenVisita
    {

        private static readonly DaResumenVisita DaResumenVisita = new DaResumenVisita();

        /// <summary>
        /// Busca los procesos de dialogo aprobados para iniciar la visitas o muestra las visitas iniciadas o cerradas
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BeResumenVisita> BuscarResumenProcesoVisitaGr(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenVisita.BuscarResumenProcesoVisitaGr(nombres, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Busca los procesos de dialogo aprobados para iniciar la visitas o muestra las visitas iniciadas o cerradas
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BeResumenVisita> BuscarResumenProcesoVisitaGz(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenVisita.BuscarResumenProcesoVisitaGz(nombres, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Busca los usuarios GR que estan listos para realizar la post visita
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BeResumenVisita> BuscarPostVisitaGr(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenVisita.BuscarPostVisitaGr(nombres, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Busca los usuarios GZ que estan listos para realizar la post visita
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BeResumenVisita> BuscarPostVisitaGz(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenVisita.BuscarPostVisitaGz(nombres, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Busca los usuarios GR con visita
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BeResumenVisita> BuscarVisitaGr(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenVisita.BuscarVisitaGr(nombres, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Busca los usuarios GZ con visita
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BeResumenVisita> BuscarVisitaGz(string nombres, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenVisita.BuscarVisitaGz(nombres, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Obtiene el correlativo de la visita
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public int ObtenerCorrelativoVisita(string codigoUsuario, int idRol, string prefijoIsoPais, string periodo)
        {
            return DaResumenVisita.ObtenerCorrelativoVisita(codigoUsuario, idRol, prefijoIsoPais, periodo);
        }

        /// <summary>
        /// Inicia el proceso de visita
        /// </summary>
        /// <param name="objVisita"></param>
        /// <returns></returns>
        public int CrearVisita(BeResumenVisita objVisita)
        {
            return DaResumenVisita.CrearVisita(objVisita);
        }

        /// <summary>
        /// Obtiene el id de la visita
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="codigoEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public DataTable ObtenerCodigoVisita(string codigoUsuario, string codigoEvaluador, int idRol, string periodo)
        {
            return DaResumenVisita.ObtenerCodigoVisita(codigoUsuario, codigoEvaluador, idRol, periodo);
        }

        /// <summary>
        /// Obtiene el resumen de la visita para Gerente de Region
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="idVisita"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public BeResumenVisita ObtenerVisitaGr(string codigoUsuario, int idVisita, string prefijoIsoPais, string periodo)
        {
            return DaResumenVisita.ObtenerVisitaGr(codigoUsuario, idVisita, prefijoIsoPais, periodo);
        }

        /// <summary>
        /// Obtiene el resumen de la visita para Gerente de Zona
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="idVisita"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public BeResumenVisita ObtenerVisitaGz(string codigoUsuario, int idVisita, string prefijoIsoPais, string periodo)
        {
            return DaResumenVisita.ObtenerVisitaGz(codigoUsuario, idVisita, prefijoIsoPais, periodo);
        }

        /// <summary>
        /// Selecciona las visitas de un usuario
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public DataTable SeleccionarVisitaUsuario(string codigoUsuario, int idRol, string prefijoIsoPais, string periodo)
        {
            return DaResumenVisita.SeleccionarVisitaUsuario(codigoUsuario, idRol, prefijoIsoPais, periodo);
        }

        /// <summary>
        /// Metodo para Iniciar la post visita
        /// </summary>
        /// <param name="idVisita"></param>
        public void IniciarPostVisita(int idVisita)
        {
            DaResumenVisita.IniciarPostVisita(idVisita);
        }

        /// <summary>
        /// Actualiza el estado de la visita en proceso
        /// </summary>
        /// <param name="idVisita"></param>
        /// <param name="estadoVisita"></param>
        public void ActualizarEstadoVisita(int idVisita, string estadoVisita)
        {
            DaResumenVisita.ActualizarEstadoVisita(idVisita, estadoVisita);
        }

        /// <summary>
        /// Actualiza el porcentaje de avance de la visita
        /// </summary>
        /// <param name="idVisita"></param>
        /// <param name="porcentajeAvance"></param>
        /// <param name="areaAvance"></param>
        public void ActualizarAvanceVisita(int idVisita, int porcentajeAvance, int areaAvance)
        {
            DaResumenVisita.ActualizarAvanceVisita(idVisita, porcentajeAvance, areaAvance);
        }

        /// <summary>
        /// Obtiene los periodos en los cuales el usuario ha sido evaluado
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable ObtenerPeriodoVisitasByUsuario(string codigoUsuario, string prefijoIsoPais)
        {
            return DaResumenVisita.ObtenerPeriodoVisitasByUsuario(codigoUsuario, prefijoIsoPais);
        }

        public List<BeResumenVisita> ListarVisitas(string codigoEvaluador, string codigoEvaluado, string pais, string periodo)
        {
            return DaResumenVisita.ListarVisitas(codigoEvaluador, codigoEvaluado, pais, periodo);
        }

        public List<BeResumenVisita> ListarVisitasPorProceso(int idProceso)
        {
            return DaResumenVisita.ListarVisitasPorProceso(idProceso);
        }
    }
}