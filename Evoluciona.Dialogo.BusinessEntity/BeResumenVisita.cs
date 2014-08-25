using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeResumenVisita
    {
        public int idVisita { get; set; }
        public int idProceso { get; set; }
        public string codigoUsuario { get; set; }
        public string periodo { get; set; }
        public string campania { get; set; }
        public DateTime fechaLimiteProceso { get; set; }
        public string estadoVisita { get; set; }
        public int cantVisitasIniciadas { get; set; }
        public int cantidadVisitasCerradas { get; set; }
        public DateTime fechaPostVisita { get; set; }
        public int porcentajeAvanceAntes { get; set; }
        public int porcentajeAvanceDurante { get; set; }
        public int porcentajeAvanceDespues { get; set; }
        public int idRolUsuario { get; set; }
        public int usuarioSistema { get; set; }
        public string nombreEvaluado { get; set; }
        public string email { get; set; }
        public string rolDescripcion { get; set; }
        public string codigoGRegion { get; set; }
        public int codigoRolUsuario { get; set; }
        public string codigoGZona { get; set; }
        public string prefijoIsoPais { get; set; }
        public string codigoUsuarioEvaluador { get; set; }
        public int idRolUsuarioEvaluador { get; set; }

        public BeResumenVisita()
        {
            fechaLimiteProceso = DateTime.MinValue;
        }

        public string cub { get; set; }
    }
}