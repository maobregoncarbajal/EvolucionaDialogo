
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeTomaAccion
    {
        //GIANCARLO ILLESCAS.- VARIABLE QUE INDICA SI SE HIZO CLICK EN VER
        //
        public int IdTomaAccion { get; set; }
        public int IdTomaAccionRef { get; set; }
        public string PrefijoIsoPaisEvaluador { get; set; }
        public string NombrePaisEvaluador { get; set; }
        public string PrefijoIsoPaisEvaluado { get; set; }
        public string NombrePaisEvaluado { get; set; }
        public string CorreoEvaluado { get; set; }
        public string Periodo { get; set; }
        public int IdRolEvaluador { get; set; }
        public string NombreRolEvaluador { get; set; }
        public int IdRolEvaluado { get; set; }
        public string NombreRolEvaluado { get; set; }
        public string CodEvaluador { get; set; }
        public string CorreoSupervisor { get; set; }
        public string NombreEvaluador { get; set; }
        public string CodEvaluado { get; set; }
        public string NombreEvaluado { get; set; }
        public string CodRegionActual { get; set; }
        public string NombreRegionActual { get; set; }
        public string CodZonaActual { get; set; }
        public string NombreZonaActual { get; set; }
        public string TomaAccion { get; set; }
        public string NombreTomaAccion { get; set; }
        public string CodRegionReasignacion { get; set; }
        public string NombreRegionReasignacion { get; set; }
        public string CodZonaReasignacion { get; set; }
        public string NombreZonaReasignacion { get; set; }
        public string AnhoCampanhaInicio { get; set; }
        public string AnhoCampanhaFin { get; set; }
        public string AnhoCampanhaInicioCritico { get; set; }
        public string AnhoCampanhaFinCritico { get; set; }
        public bool Estatus { get; set; }
        public string Observaciones { get; set; }
        public int EstadoActivo { get; set; }
        public bool EstadoUltimo { get; set; }
        public string EstatusClickVer { get; set; }
        public string EstatusChangeDdl { get; set; }
        public int IdProceso { get; set; }
    }
}