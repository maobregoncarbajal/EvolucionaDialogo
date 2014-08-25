
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlReporte
    {
        private static readonly DaReporte DaReporte = new DaReporte();

        /// <summary>
        /// Retorna la lista con todos los seguimientos de estatus
        /// </summary>
        /// <returns></returns>
        public List<BeSeguimientoStatus> ListarSeguimientoStatus(string periodo, string pais, string nivel, int estado)
        {
            return DaReporte.ListarSeguimientoStatus(periodo, pais, nivel, estado);
        }

        public List<BeSeguimientoStatus> ListarSeguimientoStatusByUsuario(string periodo, string pais, string nivel, int estado, string codigoUsuario, string nivelEvaluador)
        {
            return DaReporte.ListarSeguimientoStatusByUsuario(periodo, pais, nivel, estado, codigoUsuario, nivelEvaluador);
        }

        /// <summary>
        /// obtener una lista de paises según el usuario y su nivel
        /// </summary>
        /// <param name="nivel"></param>
        /// <param name="codigo"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public List<BePais> ObtenerPaisesUsuario(string nivel, string codigo, string prefijoIsoPais)
        {
            return DaReporte.ObtenerPaisesUsuario(nivel, codigo, prefijoIsoPais);
        }

        /// <summary>
        /// Retorna todos los periodos
        /// </summary>
        /// <returns></returns>
        public List<string> ObtenerPeriodos(string prefijoPais, int rol)
        {
            return DaReporte.ObtenerPeriodos(prefijoPais, rol);
        }

        /// <summary>
        /// Retorna la lista con todos los seguimientos de estatus Detalle
        /// </summary>
        /// <returns></returns>
        public List<BeSeguimientoStatusDetalle> ListarStatusDialogosDetalle(string periodo, string nombreColaborador, string nivel, string pais, string nombreJefe, string estado, string tipo, bool usuIna, string modeloDialogo)
        {
            return DaReporte.ListarStatusDialogosDetalle(periodo, nombreColaborador, nivel, pais, nombreJefe, estado, tipo, usuIna, modeloDialogo);
        }

        /// <summary>
        /// Retorna a todas las regiones
        /// </summary>
        /// <returns></returns>
        public List<BeComun> ListarRegiones()
        {
            return DaReporte.ListarRegiones();
        }

        /// <summary>
        /// retorna las zonas a apartir de código de la región
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public List<BeComun> ListarZonas(string region)
        {
            return DaReporte.ListarZonas(region);
        }

        public List<BeAnalisisStatusRanking> ListarAnalisisStatusRanking(string pais, string nivel, string periodo, string campanha, string estado1, string estado2, string estado3)
        {
            return DaReporte.ListarAnalisisStatusRanking(pais, nivel, periodo, campanha, estado1, estado2, estado3);
        }

        public List<BeAnalisisStatusRanking> ListarAnalisisStatusRankingUsuario(string pais, string nivel, string periodo,
                                                                       string campanha, string estado1, string estado2,
                                                                       string estado3, string codigoUsuario, string nivelEvaluador)
        {
            return DaReporte.ListarAnalisisStatusRankingUsuario(pais, nivel, periodo, campanha, estado1, estado2, estado3, codigoUsuario, nivelEvaluador);
        }

        public List<BeChartCampanha> ListarChartCampanha(string pais, string nivel, string periodo, string campanha, string estado1, string estado2, string estado3)
        {
            return DaReporte.ListarChartCampanha(pais, nivel, periodo, campanha, estado1, estado2, estado3);
        }

        public List<BeChartCampanha> ListarChartCampanhaUsuario(string pais, string nivel, string periodo, string campanha,
                                                       string estado1, string estado2, string estado3, string codigoUsuario, string nivelEvaluador)
        {
            return DaReporte.ListarChartCampanhaUsuario(pais, nivel, periodo, campanha, estado1, estado2, estado3, codigoUsuario, nivelEvaluador);
        }

        public DataTable ObtenerListaCampana(string ddlCampana, int codigoRolUsuario, string prefijoIsoPais, string periodoActual, string connstring)
        {
            return DaReporte.ObtenerListaCampana(ddlCampana, codigoRolUsuario, prefijoIsoPais, periodoActual, connstring);
        }

        public DataTable ObtenerVariablesNegocio(string connstring, string periodo, string nombreColaborador, string nivel, string zona, string pais, string nombreJefe, string estado, string region)
        {
            return DaReporte.ObtenerVariablesNegocio(connstring, periodo, nombreColaborador, nivel, zona, pais, nombreJefe, estado, region);
        }

        public DataTable ObtenerVariablesNegocioUsuario(string connstring, string periodo, string nombreColaborador, string nivel, string zona, string pais, string nombreJefe, string estado, string region, string codigoUsuario, string nivelEvaluador)
        {
            return DaReporte.ObtenerVariablesNegocioUsuario(connstring, periodo, nombreColaborador, nivel, zona, pais, nombreJefe, estado, region, codigoUsuario, nivelEvaluador);
        }

        public DataTable ObtenerPlanNegocio(string connstring, string nivel, string estado, string variable, string rangoInicio, string rangoFin, string pais, string anhio, string periodo, string perioAnteior)
        {
            return DaReporte.ObtenerPlanNegocio(connstring, nivel, estado, variable, rangoInicio, rangoFin, pais, anhio, periodo, perioAnteior);
        }

        public List<BeComun> ListarTipoVariables()
        {
            return DaReporte.ListarTipoVariables();
        }

        public DataTable ObtenerPlanNegocioDetalle(string connstring, string periodo, string pais, string region, string zona, string nombreColaborador, string nombreJefe, string nivel, string ranking, string cumplimiento, string enfoque)
        {
            return DaReporte.ObtenerPlanNegocioDetalle(connstring, periodo, pais, region, zona, nombreColaborador, nombreJefe, nivel, ranking, cumplimiento, enfoque);
        }

        public DataTable ResultadoDialogo(string connstring, string anhio, string periodo, string peridoAnterior, string nivel, string pais, string tipo)
        {
            return DaReporte.ResultadoDialogo(connstring, anhio, periodo, peridoAnterior, nivel, pais, tipo);
        }

        public DataTable ResultadoDialogoUsuario(string connstring, string anhio, string periodo, string peridoAnterior, string nivel, string pais, string tipo, string codigoUsuario, string nivelEvaluador)
        {
            return DaReporte.ResultadoDialogoUsuario(connstring, anhio, periodo, peridoAnterior, nivel, pais, tipo, codigoUsuario, nivelEvaluador);
        }

        public DataTable ResultadoDialogoDetalle(string connstring, string periodo, string pais, string region, string zona, string nivel, string codigoVariable, string tamBrecha)
        {
            return DaReporte.ResultadoDialogoDetalle(connstring, periodo, pais, region, zona, nivel, codigoVariable, tamBrecha);
        }

        public DataTable ResultadoDialogoDetalleUsuario(string connstring, string periodo, string pais, string region, string zona, string nivel, string codigoVariable, string tamBrecha, string codigoUsuario, string nivelEvaluador)
        {
            return DaReporte.ResultadoDialogoDetalleUsuario(connstring, periodo, pais, region, zona, nivel, codigoVariable, tamBrecha, codigoUsuario, nivelEvaluador);
        }

        public DataTable ObtenerCompetencia(string codigoColaborador, string anio, string prefijoIsoPais, int idRol)
        {
            return DaReporte.ObtenerCompetencia(codigoColaborador, anio, prefijoIsoPais, idRol);
        }
    }
}