
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlAccionesAcordadas
    {
        private static readonly DaAccionesAcordadas ObjAccionesAcordadas = new DaAccionesAcordadas();

        public DataTable GetVariablesCausales(BeAccionesAcordadas objAccionesAcordadasBe, string codVariablePadre)
        {
            return ObjAccionesAcordadas.GetVariablesCausales(objAccionesAcordadasBe, codVariablePadre);
        }

        public DataTable GetCodVariablePadre(BeAccionesAcordadas objAccionesAcordadasBe, string documento, string periodo)
        {
            return ObjAccionesAcordadas.GetCodVariablePadre(objAccionesAcordadasBe, documento, periodo);
        }

        public DataTable GetCodVariablePadreGz(BeAccionesAcordadas objAccionesAcordadasBe, string documento, string periodo)
        {
            return ObjAccionesAcordadas.GetCodVariablePadreGz(objAccionesAcordadasBe, documento, periodo);
        }

        public bool RecordAccionesAcordadas(BeAccionesAcordadas objAccionesAcordadasBe)
        {
            return ObjAccionesAcordadas.RecordAccionesAcordadas(objAccionesAcordadasBe);
        }

        public void ActualizarAccionesAcordadas(BeAccionesAcordadas objAccionesAcordadasBe)
        {
            ObjAccionesAcordadas.ActualizarAccionesAcordadas(objAccionesAcordadasBe);
        }

        public DataTable ObtenerAccionesAcordadasByIndicador(BeAccionesAcordadas objAccionesAcordadasBe)
        {
            return ObjAccionesAcordadas.ObtenerAccionesAcordadasByIndicador(objAccionesAcordadasBe);
        }

        public List<BeAccionesAcordadas> ObtenerAccionesAcordadas(int idProceso)
        {
            return ObjAccionesAcordadas.ObtenerAccionesAcordadas(idProceso);
        }

        public List<BeVariableCausa> ObtenerVariablesCausales(int idProceso, string codiVariable, string tipoAccion)
        {
            return ObjAccionesAcordadas.ObtenerVariablesCausales(idProceso, codiVariable, tipoAccion);
        }

        public bool InsertarAccionAcordada(BeAccionesAcordadas objAccionAcordada)
        {
            return ObjAccionesAcordadas.InsertarAccionAcordada(objAccionAcordada);
        }

        public void ActualizarAccionAcordada(BeAccionesAcordadas objAccionAcordada)
        {
            ObjAccionesAcordadas.ActualizarAccionAcordada(objAccionAcordada);
        }
    }
}
