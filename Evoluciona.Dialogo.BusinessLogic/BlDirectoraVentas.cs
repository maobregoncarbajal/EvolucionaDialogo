
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlDirectoraVentas
    {
        private static readonly DaDirectoraVentas DaDirectoraVentas = new DaDirectoraVentas();

        public List<BeDirectoraVentas> DirectoraVentasListar(int idDirectoraVenta, string prefijoIsoPais,
                                                             string nombreCompleto, bool estado)
        {
            return DaDirectoraVentas.DirectoraVentasListar(idDirectoraVenta, prefijoIsoPais, nombreCompleto, estado);
        }

        public bool DirectoraVentasRegistrar(BeDirectoraVentas obeDirectoraVentas)
        {
            return DaDirectoraVentas.DirectoraVentasRegistrar(obeDirectoraVentas);
        }

        public bool DirectoraVentasActualizar(BeDirectoraVentas obeDirectoraVentas)
        {
            return DaDirectoraVentas.DirectoraVentasActualizar(obeDirectoraVentas);
        }

        public bool DirectoraVentasActualizarEstado(BeDirectoraVentas obeDirectoraVentas)
        {
            return DaDirectoraVentas.DirectoraVentasActualizarEstado(obeDirectoraVentas);
        }

        public List<BeDirectoraVentas> DirectoraVentasListar(string prefijoIsoPais)
        {
            return DaDirectoraVentas.DirectoraVentasListar(prefijoIsoPais);
        }


        public List<BeDirectoraVentas> ListaDv(string prefijoIsoPais)
        {
            try
            {
                var entidades = DaDirectoraVentas.ListaDv(prefijoIsoPais);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteDv(int id)
        {

            try
            {
                var estado = DaDirectoraVentas.DeleteDv(id);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddDv(BeDirectoraVentas obj)
        {

            try
            {
                var estado = DaDirectoraVentas.AddDv(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditDv(BeDirectoraVentas obj)
        {
            try
            {
                var estado = DaDirectoraVentas.EditDv(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}