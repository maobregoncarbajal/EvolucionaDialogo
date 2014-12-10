
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BlCritica
    {

        private static readonly DaCritica DaCritica = new DaCritica();

        public DataSet Cargarcriticas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            return DaCritica.Cargarcriticas(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring);
        }

        public Boolean InsertarCriticas(string dni, int idProceso, string variableConsiderar, int codigoRolUsuario, string connstring)
        {
            return DaCritica.InsertarIndicadores(dni, idProceso, variableConsiderar, codigoRolUsuario, connstring);
        }

        public Boolean InsertarCriticasPreDialogo(string dni, int idProceso, string variableConsiderar, int codigoRolUsuario, string connstring)
        {
            return DaCritica.InsertarIndicadoresPreDialogo(dni, idProceso, variableConsiderar, codigoRolUsuario, connstring);
        }

        public Boolean InsertarCriticasEvaluado(string dni, int idProceso, string variableConsiderar, int codigoRolUsuario, string connstring)
        {
            return DaCritica.InsertarCriticasEvaluado(dni, idProceso, variableConsiderar, codigoRolUsuario, connstring);
        }

        public DataTable ValidarPeriodoEvaluacion(string periodoEvaluacion, string prefijoIsoPais, int codigoRolUsuario, string connstring)
        {
            return DaCritica.ValidarPeriodoEvaluacion(periodoEvaluacion, prefijoIsoPais, codigoRolUsuario, connstring);
        }

        #region CargarCriticas

        public DataTable CargarCampaniasCriticas_GR(string codigoUsuarioProcesado, string periodo, string prefijoIsoPais, string connstring)
        {
            return DaCritica.CargarCampaniasCriticas_GR(codigoUsuarioProcesado, periodo, prefijoIsoPais, connstring);
        }

        public DataTable CargarCampaniasCriticas_GZ(string codigoUsuarioProcesado, string periodo, string prefijoIsoPais, string documentoEvaluador, string connstring)
        {
            return DaCritica.CargarCampaniasCriticas_GZ(codigoUsuarioProcesado, periodo, prefijoIsoPais, documentoEvaluador, connstring);
        }

        #endregion CargarCriticas

        #region SeleccionarCriticas

        public List<BeCriticas> ListaCargarCriticas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, string anioCampana)
        {
            return DaCritica.ListaCargarCriticas(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, anioCampana);
        }

        public List<BeCriticas> ListaCargarCriticasProcesadas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            if (string.IsNullOrEmpty(codigoUsuarioProcesado) || string.IsNullOrEmpty(periodo) || string.IsNullOrEmpty(prefijoIsoPais) || string.IsNullOrEmpty(connstring))
                return new List<BeCriticas>();

            return DaCritica.ListaCargarCriticasProcesadas(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, idProceso, anioCampana);
        }

        public List<BeCriticas> ListaCargarCriticasProcesadasPreDialogo(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            if (string.IsNullOrEmpty(codigoUsuarioProcesado) || string.IsNullOrEmpty(periodo) || string.IsNullOrEmpty(prefijoIsoPais) || string.IsNullOrEmpty(connstring))
                return new List<BeCriticas>();

            return DaCritica.ListaCargarCriticasProcesadasPreDialogo(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, idProceso, anioCampana);
        }

        public List<BeCriticas> ListaCargarCriticasProcesadasEvaluado(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            if (string.IsNullOrEmpty(codigoUsuarioProcesado) || string.IsNullOrEmpty(periodo) || string.IsNullOrEmpty(prefijoIsoPais) || string.IsNullOrEmpty(connstring))
                return new List<BeCriticas>();

            return DaCritica.ListaCargarCriticasProcesadasEvaluado(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, idProceso, anioCampana);
        }

        public List<BeCriticas> ListaCargarCriticasProcesadasResumen(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            return DaCritica.ListaCargarCriticasProcesadasResumen(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, idProceso, anioCampana);
        }

        public List<BeCriticas> ListaCargarCriticasProcesadasResumenEvaluado(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso, string anioCampana)
        {
            return DaCritica.ListaCargarCriticasProcesadasResumenEvaluado(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, idProceso, anioCampana);
        }

        public DataTable CargarCriticasProcesadas(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string connstring, int idProceso)
        {
            return DaCritica.CargarCriticasProcesadas(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, connstring, idProceso);
        }

        public void EliminarCritica(string documentoIdentidad, int idProceso, string connstring)
        {
            DaCritica.EliminarCritica(documentoIdentidad, idProceso, connstring);
        }

        public void EliminarCriticaPreDialogo(string documentoIdentidad, int idProceso, string connstring)
        {
            DaCritica.EliminarCriticaPreDialogo(documentoIdentidad, idProceso, connstring);
        }

        #endregion SeleccionarCriticas

        public DataTable ObtenerHistoricoPeriodosCriticidad(string codigoUsuarioEvaluador, string codigoUsuarioRequerido, TipoHistorial tipo)
        {
            return DaCritica.ObtenerHistoricoPeriodosCriticidad(codigoUsuarioEvaluador, codigoUsuarioRequerido, tipo);
        }

        public DataTable ObtenerHistoricoPeriodosCriticidadEval(string codigoUsuarioEvaluador, string codigoUsuarioRequerido, TipoHistorial tipo)
        {
            return DaCritica.ObtenerHistoricoPeriodosCriticidadEval(codigoUsuarioEvaluador, codigoUsuarioRequerido, tipo);
        }

        public BeCriticas ObtenerCritica(int idProceso, string documentoIdentidad)
        {
            return DaCritica.ObtenerCritica(idProceso, documentoIdentidad);
        }

        public List<BeCriticas> ListaEstadosEquipo(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais, string estadoPeriodo)
        {
            if (string.IsNullOrEmpty(codigoUsuarioProcesado) || string.IsNullOrEmpty(periodo) || string.IsNullOrEmpty(prefijoIsoPais))
                return new List<BeCriticas>();

            return DaCritica.ListaEstadosEquipo(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais, estadoPeriodo);
        }

        public List<BeCriticas> ListaSeguimientosEquipo(string dniUsuario, string dniUsuarioEvaluado, int rolUsuario, string periodo, string prefijoIsoPais)
        {
            return DaCritica.ListaSeguimientosEquipo(dniUsuario, dniUsuarioEvaluado, rolUsuario, periodo, prefijoIsoPais);
        }

        public List<BeCriticas> ListaCargarCriticasDisponibles(string codigoUsuarioProcesado, string periodo, int codigoRolUsuario, string prefijoIsoPais)
        {
            if (string.IsNullOrEmpty(codigoUsuarioProcesado) || string.IsNullOrEmpty(periodo) || string.IsNullOrEmpty(prefijoIsoPais))
                return new List<BeCriticas>();

            return DaCritica.ListaCargarCriticasDisponibles(codigoUsuarioProcesado, periodo, codigoRolUsuario, prefijoIsoPais);
        }

        public void InsertarCriticidadEquipo(BeCriticas criticidadEquipo)
        {
            DaCritica.InsertarCriticidadEquipo(criticidadEquipo);
        }

        public void ActualizarCriticidadEquipo(BeCriticas criticidadEquipo)
        {
            DaCritica.ActualizarCriticidadEquipo(criticidadEquipo);
        }

        public void EliminarCriticidadEquipo(int idEquipo)
        {
            DaCritica.EliminarCriticidadEquipo(idEquipo);
        }

        public BeCriticas ObtenerCriticidadEquipo(int idProceso)
        {
            return DaCritica.ObtenerCriticidadEquipo(idProceso);
        }

        public List<BeCriticas> ObtenerCriticidadEquipos(int idProceso)
        {
            return DaCritica.ObtenerCriticidadEquipos(idProceso);
        }
    }
}