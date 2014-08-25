
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using System;

    public class BlCargaAdam
    {
        private static readonly DaCargaAdam CargaAdam = new DaCargaAdam();

        public bool CargarArchivoAdam(string dtsName)
        {
            try
            {
                var estado = CargaAdam.CargarArchivoAdam(dtsName);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
