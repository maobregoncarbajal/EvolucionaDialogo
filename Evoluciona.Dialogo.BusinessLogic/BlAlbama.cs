

namespace Evoluciona.Dialogo.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using DataAccess;
    using BusinessEntity;

    public class BlAlbama
    {
        private static readonly DaAlbama DaAlbama = new DaAlbama();

        public List<BeComun> ListarPaises(string codPais)
        {
            try
            {
                return DaAlbama.ListarPaises(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarRegiones(string codPais)
        {
            try
            {
                return DaAlbama.ListarRegiones(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarZonas(string codPais, string codRegion)
        {
            try
            {
                return DaAlbama.ListarZonas(codPais, codRegion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarCargo(string codCargo)
        {
            try
            {
                return DaAlbama.ListarCargo(codCargo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarEstadoCargo(string codEstadoCargo)
        {
            try
            {
                return DaAlbama.ListarEstadoCargo(codEstadoCargo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
