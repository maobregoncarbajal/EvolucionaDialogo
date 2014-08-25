
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlCondiciones
    {
        private static readonly DaCondiciones DaCondiciones = new DaCondiciones();

        public List<BeCondiciones> ObtenerCondiciones(string prefijoIsoPais, string tipoCondicion, int estado)
        {
            try
            {
                var entidades = DaCondiciones.ObtenerCondiciones(prefijoIsoPais, tipoCondicion, estado);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarCondiciones(List<BeCondiciones> be, string usuario)
        {
            try
            {
                var condiciones = DaCondiciones.ActualizarCondiciones(be, usuario);

                return condiciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class BlCondicionesDetalle
    {
        private static readonly DaCondicionesDetalle DaCondicionesDetalle = new DaCondicionesDetalle();

        public List<BeCondicionesDetalle> ObtenerCondicionesDetalle(string prefijoIsoPais, string tipoCondicion, string numeroCondicionLineamiento)
        {
            try
            {
                var entidades = DaCondicionesDetalle.ObtenerCondicionesDetalle(prefijoIsoPais, tipoCondicion, numeroCondicionLineamiento);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarCondicionesDetalle(List<BeCondicionesDetalle> be, string usuario)
        {
            try
            {
                var condiciones = DaCondicionesDetalle.ActualizarCondicionesDetalle(be, usuario);

                return condiciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
