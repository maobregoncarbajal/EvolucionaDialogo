using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    [Serializable]
    public class BeTipoEvento
    {
        public int IDTipoEvento { get; set; }
        public string Descripcion { get; set; }
        public int IDPadre { get; set; }
        public DateTime FechaCrea { get; set; }
        public int CodigoRol { get; set; }
    }
}