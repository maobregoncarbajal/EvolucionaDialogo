
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlInteraccion
    {

        private static readonly DaInteraccion DaInteraccion = new DaInteraccion();

        // insertar el plan anual tabla ese_trxPlanAnual
        public bool IngresarInteraccion(string connstring, BeInteraccion beInteraccion)
        {
            return DaInteraccion.IngresarInteraccion(connstring, beInteraccion);
        }

        public DataTable ObtenerInteraccionGrabadas(string connstring, int IdVisita)
        {
            return DaInteraccion.ObtenerInteraccionGrabadas(connstring, IdVisita);
        }
    }
}
