using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeCronogramaMatriz
    {
        public int IdCronogramaMatriz { get; set; }
        public string PrefijoIsoPais { get; set; }
        public string Periodo { get; set; }
        public DateTime? FechaLimite { get; set; }
        public DateTime? FechaProrroga { get; set; }
        public string UsuarioCrea { get; set; }
        public DateTime? FechaCrea { get; set; }
        public string UsuarioModi { get; set; }
        public DateTime? FechaModi { get; set; }
        public DateTime? FechaServer { get; set; }
        public bool EsUltimo { get; set; }
    }
}
