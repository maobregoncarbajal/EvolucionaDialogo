
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeSeguimientoStatus
    {
        public string Periodo { get; set; }
        public string Pais { get; set; }
        public int Colaborador { get; set; }
        public decimal DialogoNoAbierto { get; set; }
        public decimal DialogoProceso { get; set; }
        public decimal DialogoAprobacion { get; set; }
        public decimal DialogoCerrado { get; set; }
        public int PlanAccion { get; set; }
        public decimal AvancePlanAccion { get; set; }
        public int RetroAlimentacion { get; set; }
        public decimal AvanceRetroAlimentacion { get; set; }
    }
}