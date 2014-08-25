
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System;

    public class BlMatrizAdmin
    {
        private static readonly DaMatrizAdmin DaMatrizAdmin = new DaMatrizAdmin();

        #region CronogramaMatriz

        public List<BeCronogramaMatriz> ListaCronogramaMatriz(string pais)
        {
            try
            {
                var entidades = DaMatrizAdmin.ListaCronogramaMatriz(pais);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCronogramaMatriz(int idCronogramaMatriz)
        {

            try
            {
                var estado = DaMatrizAdmin.DeleteCronogramaMatriz(idCronogramaMatriz);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateCronogramaMatriz(BeCronogramaMatriz obj)
        {

            try
            {
                var estado = DaMatrizAdmin.UpdateCronogramaMatriz(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertCronogramaMatriz(BeCronogramaMatriz obj)
        {

            try
            {
                var estado = DaMatrizAdmin.InsertCronogramaMatriz(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public BeComun ObtenerPais(string codigoPais)
        {
            return DaMatrizAdmin.ObtenerPais(codigoPais);
        }

        public List<BeComun> ObtenerPaises()
        {
            return DaMatrizAdmin.ObtenerPaises();
        }

        //public List<beComun> ObtenerPeriodos(string CodigoPais)
        //{
        //    return matrizAdminDA.ObtenerPeriodos(CodigoPais);
        //}



        public BeCronogramaMatriz SelectCronograma(int id)
        {
            return DaMatrizAdmin.SelectCronograma(id);
        }


        public BeCronogramaMatriz ObtenerFechaServer()
        {
            return DaMatrizAdmin.ObtenerFechaServer();
        }



        #endregion

        #region NivelesCompetencia
        public bool InsertarNivelesCompetencia(BeNivelesCompetencia be, string usuario)
        {
            try
            {
                var nivelesCompetencia = DaMatrizAdmin.InsertarNivelesCompetencia(be, usuario);

                return nivelesCompetencia;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeNivelesCompetencia> ObtenerNivelesCompetencia(string prefijoIsoPais, string anio, int estado)
        {
            try
            {
                var nivelesCompetencia = DaMatrizAdmin.ObtenerNivelesCompetencia(prefijoIsoPais, anio, estado);

                return nivelesCompetencia;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region "Admin Matriz Zona"
        public List<BeComun> ObtenerPaisConFuenteVentas()
        {
            return DaMatrizAdmin.ObtenerPaisConFuenteVentas();
        }

        #endregion
    }
}