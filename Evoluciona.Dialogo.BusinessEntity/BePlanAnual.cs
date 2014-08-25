
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BePlanAnual
    {
        // Mae_planAnual
        public int idPlanAnual { get; set; }
        public int idProceso { get; set; }
        public int CodigoPlanAnual { get; set; }
        public string Observacion { get; set; }
        // auditoria
        public int idUsuario { get; set; }
        // Mae_planAnual
        public string PrefijoIsoPais { get; set; }
        public string Anio { get; set; }
        public string CodigoColaborador { get; set; }
    }
}
