using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    [Serializable]
    public class BeTablaMapeo
    {
        public int IntSEQIDRegion { get; set; }
        public string NUEVASCR { get; set; }
        public string CRND { get; set; }
        public string CRED { get; set; }
        public string CRDESA { get; set; }
        public string CRDESTA { get; set; }
        public string NUEVASES { get; set; }
        public string ESND { get; set; }
        public string ESED { get; set; }
        public string ESDESA { get; set; }
        public string ESDESTA { get; set; }
        public string NUEVASPR { get; set; }
        public string PRND { get; set; }
        public string PRED { get; set; }
        public string PRDESA { get; set; }
        public string PRDESTA { get; set; }
        public decimal DecValorRealPeriodo { get; set; }
    }
}