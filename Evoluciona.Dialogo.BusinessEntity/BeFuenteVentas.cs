using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeFuenteVentas
    {
        public string CodigoPais { get; set; }
        public string CodigoFuenteVenta { get; set; }
        public string FuenteVentas { get; set; }
        public bool Estado { get; set; }
        public string CodigoUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CodigoUsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
