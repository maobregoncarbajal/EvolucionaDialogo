
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;

    public class BlHomeMatriz
    {
        private static readonly DaHomeMatriz DaHomeMatriz = new DaHomeMatriz();

        #region HomeMatriz

        public int ObtenerGRplanMejoraDv(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerGRplanMejoraDv(objUsuario);
        }

        public int ObtenerGZplanMejoraDv(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerGZplanMejoraDv(objUsuario);
        }

        public int ObtenerGRplanReasignacionDv(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerGRplanReasignacionDv(objUsuario);
        }

        public int ObtenerGZplanReasignacionDv(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerGZplanReasignacionDv(objUsuario);
        }


        public decimal ObtenerPorcentGRrecuperacionDv(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerPorcentGRrecuperacionDv(objUsuario);
        }

        public decimal ObtenerPorcentGZrecuperacionDv(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerPorcentGZrecuperacionDv(objUsuario);
        }

        public decimal ObtenerPorcntIncrGR_EstaProdDV(BeUsuario objUsuario, string periodoAnterior)
        {
            return DaHomeMatriz.ObtenerPorcntIncrGR_EstaProdDV(objUsuario, periodoAnterior);
        }

        public decimal ObtenerPorcntIncrGZ_EstaProdDV(BeUsuario objUsuario, string periodoAnterior)
        {
            return DaHomeMatriz.ObtenerPorcntIncrGZ_EstaProdDV(objUsuario, periodoAnterior);
        }

        public int ObtenerGZplanMejoraGr(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerGZplanMejoraGr(objUsuario);
        }

        public int ObtenerGZplanReasignacionGr(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerGZplanReasignacionGr(objUsuario);
        }

        public decimal ObtenerPorcentGZrecuperacionGr(BeUsuario objUsuario)
        {
            return DaHomeMatriz.ObtenerPorcentGZrecuperacionGr(objUsuario);
        }

        public decimal ObtenerPorcntIncrGZ_EstaProdGR(BeUsuario objUsuario, string periodoAnterior)
        {
            return DaHomeMatriz.ObtenerPorcntIncrGZ_EstaProdGR(objUsuario, periodoAnterior);
        }

        #endregion  HomeMatriz
    }
}
