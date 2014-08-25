
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlCompetencia
    {
        private static readonly DaCompetencia DaCompetencia = new DaCompetencia();

        public List<BeCompetencia> SeleccionarGerenteNumeroDocumento()
        {
            return DaCompetencia.SeleccionarGerenteNumeroDocumento();
        }

        public void AgregarCompetencia(BeCompetencia variable)
        {
            DaCompetencia.AgregarCompetencia(variable);
        }

        #region "ALTAS Y BAJAS"

        public List<BeCompetencia> CompetenciasListarHistorico(string prefijoIsoPais, string codigoColaborador)
        {
            return DaCompetencia.CompetenciasListarHistorico(prefijoIsoPais, codigoColaborador);

        }
        #endregion


        public void InsertarLogCargaCompetencia(string anhoCub, string descripcion)
        {
            DaCompetencia.InsertarLogCargaCompetencia(anhoCub, descripcion);
        }

    }
}