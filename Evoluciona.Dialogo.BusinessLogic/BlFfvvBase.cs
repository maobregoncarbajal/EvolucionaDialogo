
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlFfvvBase
    {
        private static readonly DaFfvvBase DaFfvvBase = new DaFfvvBase();

        #region CronogramaMatriz

        public List<BeFfvvBase> Lista_in_ffvv_base()
        {
            try
            {
                var entidades = DaFfvvBase.Lista_in_ffvv_base();

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete_in_ffvv_base(int id)
        {

            try
            {
                var estado = DaFfvvBase.Delete_in_ffvv_base(id);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Add_in_ffvv_base(BeFfvvBase obj)
        {

            try
            {
                var estado = DaFfvvBase.Add_in_ffvv_base(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Edit_in_ffvv_base(BeFfvvBase obj)
        {

            try
            {
                var estado = DaFfvvBase.Edit_in_ffvv_base(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
