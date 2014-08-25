
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using Helpers;
    using System.Collections.Generic;

    public class BlProceso
    {
        private static readonly DaProceso DaProceso = new DaProceso();

        public List<BeProceso> FiltrarProcesos(TipoProceso tipoProceso, int idRol, string status, string periodo,
                                               int idUsuario, int idRolEvaluador)
        {
            return DaProceso.FiltrarProcesos(tipoProceso, idRol, status, periodo, idUsuario, idRolEvaluador);
        }

        public bool RegistrarNuevasIngresadas(int idProceso, int cantidadIngresadas)
        {
            return DaProceso.RegistrarNuevasIngresadas(idProceso, cantidadIngresadas);
        }

        public List<BeProceso> ObtenerProcesos(string periodo, int rolEvaluado, string codigoPais, string tipoDialogo, string evaluador)
        {
            return DaProceso.ObtenerProcesos(periodo, rolEvaluado, codigoPais, tipoDialogo, evaluador);
        }

        public BeProceso ObtenerProceso(int idProceso)
        {
            return DaProceso.ObtenerProceso(idProceso);
        }

        #region "Mantenimiento BELCORP - DATAMART"

        public List<BeProceso> ProcesoListarCodigoUsuario(string codigoUsuario, string prefijoIsoPais)
        {
            return DaProceso.ProcesoListarCodigoUsuario(codigoUsuario, prefijoIsoPais);
        }

        public bool ProcesoRegistrar(BeProceso obeProceso)
        {
            return DaProceso.ProcesoRegistrar(obeProceso);
        }

        public bool ProcesoActualizar(BeProceso obeProceso)
        {
            return DaProceso.ProcesoActualizar(obeProceso);
        }

        public bool ProcesoActualizarEstado(BeProceso obeProceso)
        {
            return DaProceso.ProcesoActualizarEstado(obeProceso);
        }

        #endregion
    }
}
