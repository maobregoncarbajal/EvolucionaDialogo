using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeVariablePais
    {
        public int IDVariablePais { get; set; }
        public string CodigoVariable { get; set; }
        public string CodigoPais { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string DescripcionVariable { get; set; }
    }
}
