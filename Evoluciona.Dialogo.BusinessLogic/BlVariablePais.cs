
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlVariablePais
    {
        private static readonly DaVariablePais DaVariablePais = new DaVariablePais();

        public List<BeVariablePais> ListarVariablesPorPais(string codigoPais)
        {
            return DaVariablePais.ListarVariablesPorPais(codigoPais);
        }

        public List<BeVariablePais> ListarVariablesDisponiblesPorPais(string codigoPais)
        {
            return DaVariablePais.ListarVariablesDisponiblesPorPais(codigoPais);
        }

        public BeVariablePais ObtenerVariable(int idVariablePais)
        {
            return DaVariablePais.ObtenerVariable(idVariablePais);
        }

        public void AgregarVariable(BeVariablePais variable)
        {
            DaVariablePais.AgregarVariable(variable);
        }

        public void EliminarVariable(int idVariablePais)
        {
            DaVariablePais.EliminarVariable(idVariablePais);
        }
    }
}
