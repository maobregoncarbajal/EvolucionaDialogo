
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeTimeLine
    {
        public string start { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class BeTimeLineDetetalle
    {
        public string CodPais { get; set; }
        public string AnhoCampanha { get; set; }
        public string CodDocIdentidad { get; set; }
        public string Nombre { get; set; }
        public int IdRol { get; set; }
        public string CodRegion { get; set; }
        public string CodZona { get; set; }
        public string EstadoCampanha { get; set; }
        public string PeriodoToma { get; set; }
        public string CampanhaToma { get; set; }
        public string CodTomaAccion { get; set; }
        public string Observaciones { get; set; }
    }
}