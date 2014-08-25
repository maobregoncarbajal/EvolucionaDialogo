
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeVariableEnfoque
    {
        public int idVariableEnfoque { get; set; }
        public int idIndicador { get; set; }
        public string campania { get; set; }
        public string zonas { get; set; }
        public byte estado { get; set; }
        public int idVariableEnfoquePlan { get; set; }
        public string planAccion { get; set; }
        public bool postDialogo { get; set; }
    }
}
