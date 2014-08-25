
namespace Evoluciona.Dialogo.BusinessEntity
{
    using Helpers;
    using System;

    public class BeProceso
    {
        public int CantidadVisitasIniciadas { get; set; }
        public int CantidadVisitasCerradas { get; set; }
        public int IdProceso { get; set; }
        public int IdRol { get; set; }
        public int Estado { get; set; }
        public string Periodo { get; set; }
        public string NombrePersona { get; set; }
        public TipoProceso Tipo { get; set; }
        public string CodigoUsuario { get; set; }

        public string EstadoDescripcion
        {
            get
            {
                if (Tipo == TipoProceso.Dialogo)
                {
                    switch (Estado.ToString())
                    {

                        case Constantes.EstadoProcesoActivo: return "Activo";
                        case Constantes.EstadoProcesoEnviado: return "Enviado";
                        case Constantes.EstadoProcesoRevision: return "En Revision";
                        case Constantes.EstadoProcesoCulminado: return "Aprobado";
                        default:
                            return "Sin Evaluacion";
                    }
                }
                switch (Estado.ToString())
                {
                    case Constantes.EstadoVisitaActivo: return "Activo";
                    case Constantes.EstadoVisitaPostDialogo: return "Post Visita";
                    case Constantes.EstadoVisitaCerrado: return "Cerrado";
                    default:
                        return "Sin Visita";
                }
            }
        }

        public int nuevasIngresadas { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string CodigoUsuarioEvaluador { get; set; }
        public DateTime datFechaLimiteProceso { get; set; }
        public string chrEstadoProceso { get; set; }
        public int intIDRolEvaluador { get; set; }
        public string chrPrefijoIsoPais { get; set; }
        public int intUsuarioCrea { get; set; }
        public string chrRegionZona { get; set; }
        public DateTime datFechaInicioProceso { get; set; }
        public bool bitPlanMejora { get; set; }
        public int tipo { get; set; }
        public BeGerenteZona obeGerenteZona { get; set; }
    }
}
