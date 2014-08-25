using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeLet
    {
        public int IdLet { get; set; }
        public string AnioCampana { get; set; }
        public string CodPais { get; set; }
        public string CodRegion { get; set; }
        public string CodGerenteRegional { get; set; }
        public string CodGerenteZona { get; set; }
        public string CodigoConsultoraLet { get; set; }
        public string DesNombreLet { get; set; }
        public string CorreoElectronico { get; set; }
        public string EstadoCamp { get; set; }
        public string EstadoPeriodo { get; set; }
        public DateTime FechaUltAct { get; set; }
        public int FlagProceso { get; set; }
        public int FlagControl { get; set; }
    }
}
