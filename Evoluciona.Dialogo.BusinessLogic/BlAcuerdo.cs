
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlAcuerdo
    {

        private static readonly DaAcuerdo DaAcuerdo = new DaAcuerdo();

        // insertar el plan anual tabla ese_trxPlanAnual
        public bool IngresarAcuerdo(string connstring, BeAcuerdo beAcuerdo)
        {
            return DaAcuerdo.IngresarAcuerdo(connstring, beAcuerdo);
        }

        // Obtener  Acuerdos Grabadas
        public DataTable ObtenerAcuerdoGrabadas(string connstring, int idVisita)
        {
            return DaAcuerdo.ObtenerAcuerdoGrabadas(connstring, idVisita);
        }
    }
}
