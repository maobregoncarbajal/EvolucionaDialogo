
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlEncuestaReporte
    {
        private static readonly DaEncuestaReporte DaEncuestaReporte = new DaEncuestaReporte();

        public List<BeEncuestaReporte> ListaEncuestaReporte()
        {
            try
            {
                var entidades = DaEncuestaReporte.ListaEncuestaReporte();

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
