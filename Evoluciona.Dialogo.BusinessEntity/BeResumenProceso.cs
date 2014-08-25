using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeResumenProceso
    {
        public int idProceso { get; set; }
        public string codigoUsuario { get; set; }
        public string periodo { get; set; }
        public DateTime fechaLimiteProceso { get; set; }
        public string estadoProceso { get; set; }
        public int rolUsuario { get; set; }
        public int usuarioSistema { get; set; }
        public string nombreEvaluado { get; set; }
        public string email { get; set; }
        public string rolDescripcion { get; set; }
        public string codigoGRegion { get; set; }
        public int codigoRolUsuario { get; set; }
        public string codigoGZona { get; set; }
        public string prefijoIsoPais { get; set; }
        public string codigoUsuarioEvaluador { get; set; }
        public int rolUsuarioEvaluador { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int NuevasIngresadas { get; set; }
        public BeResumenProceso()
        {
            fechaLimiteProceso = DateTime.MinValue;
        }
        public string cub { get; set; }
        public string modeloDialogo { get; set; }
    }
}