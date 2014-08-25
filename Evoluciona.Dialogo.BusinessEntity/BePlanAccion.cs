
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BePlanAccion
    {
        public int idPlanAccionVisita { get; set; }
        public int idVisita { get; set; }
        public bool postVisita { get; set; }
        public int idPlanAcccion { get; set; }
        public int IDProceso { get; set; }
        public string DocuIdentidad { get; set; }
        public string PlanAccion { get; set; }
        public bool PreDialogo { get; set; }
        public int idUsuario { get; set; }
        public string NombreCritica { get; set; }
        public string Variable { get; set; }
        public string Estado { get; set; }
    }
}
