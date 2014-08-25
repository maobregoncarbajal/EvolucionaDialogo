using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeDirectoraVentas
    {
        public int intIDDirectoraVenta { get; set; }
        public string chrPrefijoIsoPais { get; set; }
        public string chrCodigoDirectoraVentas { get; set; }
        public string vchNombreCompleto { get; set; }
        public string vchCorreoElectronico { get; set; }
        public bool bitEstado { get; set; }
        public string vchCUBDV { get; set; }
        public string chrCodigoPlanilla { get; set; }
        public BePais obePais { get; set; }
        public int intUsuarioCrea { get; set; }
        public DateTime datFechaCrea { get; set; }
        public int intUsuarioModi { get; set; }
        public DateTime datFechaModi { get; set; }
        public string vchDocumentoIndentidad { get; set; }
        public string EstadoDirectora { get; set; }
        public string vchObservacion { get; set; }
    }
}
