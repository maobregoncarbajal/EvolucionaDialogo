
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlReportes
    {
        private static readonly DaReportes DaReportes = new DaReportes();

        public List<BeReporteUsoTiempo> ObtenerDatosReunionReporte(string codigoUsuario, int codigoRol)
        {
            return DaReportes.ObtenerReporteUsoTiempo(codigoUsuario, codigoRol);
        }

        public int ObtenerCantidadTipoReunion(int tipoReunion, int codigoRol)
        {
            return DaReportes.ObtenerCantidadTipoReunion(tipoReunion, codigoRol);
        }

        public List<BeReporteUsoTiempo> ObtenerTotalesReunionReporte(string codigoUsuario)
        {
            return DaReportes.ObtenerTotalesReuniones(codigoUsuario);
        }

        public DataTable ObtenerReunionesCampania(string periodo, string codigoUsuario)
        {
            return DaReportes.ObtenerReunionesCampania(periodo, codigoUsuario);
        }

        public DataTable ObtenerCantFijoVariable(string periodo, string campania, string tipo, string codigoUsuario)
        {
            return DaReportes.ObtenerCantFijoVariable(periodo, campania, tipo, codigoUsuario);
        }
    }
}
