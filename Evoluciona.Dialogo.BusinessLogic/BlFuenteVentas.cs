
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlFuenteVentas
    {
        private static readonly DaFuenteVentas DaFuenteVentas = new DaFuenteVentas();

        public DataTable ListarPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            return DaFuenteVentas.ListarPaisFuenteVenta(obeFuenteVentas);
        }

        public bool RegistrarPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            return DaFuenteVentas.RegistrarPaisFuenteVenta(obeFuenteVentas);
        }

        public bool ActualizarPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            return DaFuenteVentas.ActualizarPaisFuenteVenta(obeFuenteVentas);
        }

        public bool ActualizarEstadoPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            return DaFuenteVentas.ActualizarEstadoPaisFuenteVenta(obeFuenteVentas);
        }

    }
}
