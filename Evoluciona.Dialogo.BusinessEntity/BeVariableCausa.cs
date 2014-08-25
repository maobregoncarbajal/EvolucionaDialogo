
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeVariableCausa
    {
        public int IdProceso { get; set; }
        public string CodigoPadre { get; set; }
        public string Codigo { get; set; }
        public double ValorPlan { get; set; }
        public double ValorReal { get; set; }
        public double Diferencia { get; set; }
        public double Porcentaje { get; set; }
        public string DescripcionVariable { get; set; }
    }
}
