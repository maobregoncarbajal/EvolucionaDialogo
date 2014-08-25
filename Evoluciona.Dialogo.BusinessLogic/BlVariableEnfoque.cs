
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlVariableEnfoque
    {
        private static readonly DaVariableEnfoque DaVariableEnfoque = new DaVariableEnfoque();

        public List<BeVariableEnfoque> ObtenerVariablesEnfoqueProcesadas(int idIndicador)
        {
            return DaVariableEnfoque.ObtenerVariablesEnfoqueProcesadas(idIndicador);
        }

        public DataTable ObtenerPlanesByVariablesEnfoqueProcesadas(int idVariableEnfoque)
        {
            return DaVariableEnfoque.ObtenerPlanesByVariablesEnfoqueProcesadas(idVariableEnfoque);
        }

        public int InsertarVariableEnfoque(BeVariableEnfoque objVarEnfoqueBe)
        {
            return DaVariableEnfoque.InsertarVariableEnfoque(objVarEnfoqueBe);
        }
        public bool ActualizarVariableEnfoque(BeVariableEnfoque objVarEnfoqueBe)
        {
            return DaVariableEnfoque.ActualizarVariableEnfoque(objVarEnfoqueBe);
        }

        public void InsertarVariableEnfoquePlanes(BeVariableEnfoque objVarEnfoqueBe)
        {
            DaVariableEnfoque.InsertarVariableEnfoquePlanes(objVarEnfoqueBe);
        }

        public void ActualizarVariableEnfoquePlanes(BeVariableEnfoque objVarEnfoqueBe)
        {
            DaVariableEnfoque.ActualizarVariableEnfoquePlanes(objVarEnfoqueBe);
        }

        public void EliminarVariableEnfoquePlanes(int idVariableEnfoquePlan)
        {
            DaVariableEnfoque.EliminarVariableEnfoquePlanes(idVariableEnfoquePlan);
        }

        public DataTable ObtenerDescripcionVariableEnfoque(string variable, string anioCampana, string periodoEvaluacion)
        {
            return DaVariableEnfoque.ObtenerDescripcionVariableEnfoque(variable, anioCampana, periodoEvaluacion);
        }
    }
}
