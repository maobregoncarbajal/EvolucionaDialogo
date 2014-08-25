
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlEncuestaPregunta
    {
        private static readonly DaEncuestaPregunta DaEncuestaPregunta = new DaEncuestaPregunta();

        public List<BeEncuestaPregunta> ListaEncuestaPregunta()
        {
            try
            {
                var entidades = DaEncuestaPregunta.ListaEncuestaPregunta();

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteEncuestaPregunta(int id)
        {
            try
            {
                var estado = DaEncuestaPregunta.DeleteEncuestaPregunta(id);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddEncuestaPregunta(BeEncuestaPregunta obj)
        {
            try
            {
                var estado = DaEncuestaPregunta.AddEncuestaPregunta(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditEncuestaPregunta(BeEncuestaPregunta obj)
        {
            try
            {
                var estado = DaEncuestaPregunta.EditEncuestaPregunta(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
