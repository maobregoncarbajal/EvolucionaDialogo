using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeVisitaIndicador
    {
        public int IdIdicador { get; set; }
        public int IdProceso { get; set; }
        public string Seleccioando { get; set; }
        public bool Estado { get; set; }
        public string AnhoCampanha { get; set; }
        public int NumeroIngresado { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
