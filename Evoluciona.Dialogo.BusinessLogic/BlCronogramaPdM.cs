
using System;
using System.Collections.Generic;
using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.DataAccess;

namespace Evoluciona.Dialogo.BusinessLogic
{
    public class BlCronogramaPdM
    {
        private static readonly DaCronogramaPdM DaCronogramaPdM = new DaCronogramaPdM();

        public List<BeCronogramaPdM> ListarCronogramaPdM()
        {
            try
            {
                var entidades = DaCronogramaPdM.ListarCronogramaPdM();

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddCronogramaPdM(BeCronogramaPdM obj)
        {

            try
            {
                var estado = DaCronogramaPdM.AddCronogramaPdM(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditCronogramaPdM(BeCronogramaPdM obj)
        {

            try
            {
                var estado = DaCronogramaPdM.EditCronogramaPdM(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DelCronogramaPdM(BeCronogramaPdM obj)
        {

            try
            {
                var estado = DaCronogramaPdM.DelCronogramaPdM(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ValidaCronogramaPdM(string pais, string periodo)
        {
            try
            {
                return DaCronogramaPdM.ValidaCronogramaPdM(pais, periodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public BeCronogramaPdM BuscarCronogramaPdM(BeUsuario objUsuario)
        {
            try
            {
                return DaCronogramaPdM.BuscarCronogramaPdM(objUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ValidarFechaAcuerdo(string codPais, string periodo)
        {
            try
            {
                return DaCronogramaPdM.ValidarFechaAcuerdo(codPais, periodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
