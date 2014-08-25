
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeCriticas
    {
        public string campania { get; set; }
        public string estadoCriticidad { get; set; }
        public string periodo { get; set; }
        public string documentoIdentidad { get; set; }
        public string nombresCritica { get; set; }
        public string variableConsiderar { get; set; }
        public string codigoGRegion { get; set; }
        public string codigoGZona { get; set; }
        public int idProceso { get; set; }
        public int idCritica { get; set; }
        public decimal? Porcentaje { get; set; }
        public bool EstadoSeleccionado { get; set; }
        public string NombreEquipo { get; set; }
        public string PlanAccion { get; set; }
    }
}
