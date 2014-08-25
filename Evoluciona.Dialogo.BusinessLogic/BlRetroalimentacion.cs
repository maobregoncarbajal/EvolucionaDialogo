
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Data;

    public class BlRetroalimentacion
    {

        private static readonly DaRetroalimentacion DaRetroalimentacion = new DaRetroalimentacion();

        // insertar el plan anual tabla ese_trxPlanAnual
        public bool IngresarRetroalimentacion(string connstring, BeRetroalimentacion beRetroalimentacion)
        {
            return DaRetroalimentacion.IngresardaRetroalimentacion(connstring, beRetroalimentacion);
        }

        // Listar Retroalimentacion
        public DataTable ListarRetroalimentacion(string connstring, BeRetroalimentacion beRetroalimentacion)
        {
            return DaRetroalimentacion.ListarRetroalimentacion(connstring, beRetroalimentacion);
        }

        public DataTable ListarRetroalimentacionNuevas(string connstring, BeRetroalimentacion beRetroalimentacion)
        {
            return DaRetroalimentacion.ListarRetroalimentacionNuevas(connstring, beRetroalimentacion);
        }

        // Cargar Combo Competencia
        public DataTable CargarCompetencia(string connstring, BeResumenProceso beResumenProceso, String anio)
        {
            return DaRetroalimentacion.CargarCompetencia(connstring, beResumenProceso, anio);
        }

        public DataTable CargarCompetenciaNueva(string connstring, BeResumenProceso beResumenProceso, String anio)
        {
            return DaRetroalimentacion.CargarCompetenciaNueva(connstring, beResumenProceso, anio);
        }
    }
}
