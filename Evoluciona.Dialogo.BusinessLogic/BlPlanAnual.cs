
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlPlanAnual
    {
        private static readonly DaPlanAnual DaPlanAnual = new DaPlanAnual();

        //Listar el plan anual de la tabla ese_mae_plan_anual
        public DataTable ObtenerPlanAnual(string connstring, BePlanAnual bePlanAnual)
        {
            return DaPlanAnual.ObtenerPlanAnual(connstring, bePlanAnual);
        }

        public DataTable ObtenerPlanAnualNuevas(string connstring, BePlanAnual bePlanAnual)
        {
            return DaPlanAnual.ObtenerPlanAnualNuevas(connstring, bePlanAnual);
        }

        // insertar el plan anual tabla ese_trxPlanAnual
        public bool IngresarPlanAnual(string connstring, BePlanAnual bePlanAnual)
        {
            return DaPlanAnual.IngresarPlanAnual(connstring, bePlanAnual);
        }

        public bool IngresarPlanAnualPreDialogo(string connstring, BePlanAnual bePlanAnual)
        {
            return DaPlanAnual.IngresarPlanAnualPreDialogo(connstring, bePlanAnual);
        }

        public bool IngresarPlanAnualEvaluado(string connstring, BePlanAnual bePlanAnual)
        {
            return DaPlanAnual.IngresarPlanAnualEvaluado(connstring, bePlanAnual);
        }

        // Actualizar el plan anual tabla ese_trxPlanAnual
        public bool ActualizarPlanAnual(string connstring, BePlanAnual bePlanAnual)
        {
            return DaPlanAnual.ActualizarPlanAnual(connstring, bePlanAnual);
        }

        //Listar el plan anual Grabadas
        public DataTable ObtenerPlanAnualGrabadas(string connstring, BeResumenProceso objResumen)
        {
            return DaPlanAnual.ObtenerPlanAnualGrabadas(connstring, objResumen);
        }

        public DataTable ObtenerPlanAnualGrabadasPreDialogo(string connstring, BeResumenProceso objResumen)
        {
            return DaPlanAnual.ObtenerPlanAnualGrabadasPreDialogo(connstring, objResumen);
        }

        public DataTable ObtenerPlanAnualGrabadasEvaluado(string connstring, BeResumenProceso objResumen)
        {
            return DaPlanAnual.ObtenerPlanAnualGrabadasEvaluado(connstring, objResumen);
        }

        public string ObtenerPaisAdam(string connstring, string prefijoIsoPais)
        {
            return DaPlanAnual.ObtenerPaisAdam(connstring, prefijoIsoPais);
        }

        public bool InsertarPlanAnualAdam(string connstring, int idRol, string prefijoIsoPais, string anio, string codigoColaborador, string nombreColaborador, string competencia, string compartamiento, string accionAcordada, string sugerencia, int estado, int codigoCompetencia)
        {
            return DaPlanAnual.InsertarPlanAnualAdam(connstring, idRol, prefijoIsoPais, anio, codigoColaborador, nombreColaborador, competencia, compartamiento, accionAcordada, sugerencia, estado, codigoCompetencia);
        }

        public bool ObtenerPlanAnualByUsuario(string connstring, int idRol, string prefijoIsoPais, string anio, string codigoColaborador, int estado)
        {
            return DaPlanAnual.ObtenerPlanAnualByUsuario(connstring, idRol, prefijoIsoPais, anio, codigoColaborador, estado);
        }

        public DataSet ConsultaPlanDesarrollo(string connstring, int anio, string codigoPaisAdam, string documentoIdentidadConsulta, BeResumenProceso objResumen)
        {
            return DaPlanAnual.ConsultaPlanDesarrollo(connstring, anio, codigoPaisAdam, documentoIdentidadConsulta, objResumen);
        }
    }
}
