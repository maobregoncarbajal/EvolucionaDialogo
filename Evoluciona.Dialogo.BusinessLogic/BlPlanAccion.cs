
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlPlanAccion
    {

        private static readonly DaPlanAccion DaPlanAccion = new DaPlanAccion();


        public DataTable ObtenerCriticasGerenteZona2(string connstring, BeUsuario beUsuario, BeResumenProceso beResumenProceso, string periodoCerrado)
        {
            if (string.IsNullOrEmpty(connstring) || string.IsNullOrEmpty(periodoCerrado))
                return null;

            return DaPlanAccion.ObtenerCriticasGerenteZona2(connstring, beUsuario, beResumenProceso, periodoCerrado);
        }

        public List<BePlanAccion> ObtenerCriticas(BeUsuario beUsuario, BeResumenProceso objResumen, string periodoCerrado, int rolLogueado)
        {
            return string.IsNullOrEmpty(periodoCerrado) ? new List<BePlanAccion>() : DaPlanAccion.ObtenerCriticas(beUsuario, objResumen, periodoCerrado, rolLogueado);
        }

        // Criticas Lets
        public DataTable ObtenerCriticasLets2(string connstring, BeUsuario beUsuario, BeResumenProceso beResumenProceso, string periodoCerrado)
        {
            return DaPlanAccion.ObtenerCriticasLets2(connstring, beUsuario, beResumenProceso, periodoCerrado);
        }

        // Ingresar Plan Accion
        public bool IngresarPlanAccion(BePlanAccion bePlanAccion)
        {
            return DaPlanAccion.IngresarPlanAccion(bePlanAccion);
        }

        //Obtener Criticas Grabadas!
        public DataTable ObtenerCriticasGrabadas(string connstring)
        {
            return DaPlanAccion.ObtenerCriticasGrabadas(connstring);
        }

        // Obtener Plan de Accion Criticas
        public DataTable ObtenerPlanAccionCriticas(string connstring, BeResumenProceso beResumenProceso)
        {
            return DaPlanAccion.ObtenerPlanAccionCriticas(connstring, beResumenProceso);
        }

        // Actualizar Plan Accion
        public bool ActualizarPlanAccion(BePlanAccion bePlanAccion)
        {
            return DaPlanAccion.ActualizarPlanAccion(bePlanAccion);
        }

        // Listar Lets Criticas (No lo Utilizo)
        public DataTable ObtenerCriticasLets(string connstring, BeUsuario beUsuario, BeResumenProceso beResumenProceso)
        {
            return DaPlanAccion.ObtenerCriticasLets(connstring, beUsuario, beResumenProceso);
        }

        // Listar Gerente de Zona Criticas(No lo utilizo)
        public DataTable ObtenerCriticasGerenteZona(string connstring, BeUsuario beUsuario, BeResumenProceso beResumenProceso)
        {
            return DaPlanAccion.ObtenerCriticasGerenteZona(connstring, beUsuario, beResumenProceso);
        }

        //Para Saber el Periodo Cerrado
        public DataTable ValidarPeriodoEvaluacion(string connstring, string periodoEvaluacion, string prefijoIsoPais)
        {
            return DaPlanAccion.ValidarPeriodoEvaluacion(connstring, periodoEvaluacion, prefijoIsoPais);
        }

        /// <summary>
        /// Inserta un registro para el plan de accion en postVisita
        /// </summary>
        /// <param name="bePlanAccion"></param>
        /// <returns></returns>
        public bool InsertarPlanAccionVisita(BePlanAccion bePlanAccion)
        {
            return DaPlanAccion.InsertarPlanAccionVisita(bePlanAccion);
        }

        /// <summary>
        /// Actualiza un registro para el plan de accion en postVisita
        /// </summary>
        /// <param name="bePlanAccion"></param>
        /// <returns></returns>
        public bool ActualizarPlanAccionVisita(BePlanAccion bePlanAccion)
        {
            return DaPlanAccion.ActualizarPlanAccionVisita(bePlanAccion);
        }

        public List<BePlanAccion> ObtenerCriticas_Visita(BeUsuario beUsuario, BeResumenProceso objResumen, string periodoCerrado, int rolLogueado, int idVisita)
        {
            return DaPlanAccion.ObtenerCriticas_Visita(beUsuario, objResumen, periodoCerrado, rolLogueado, idVisita);
        }
    }
}