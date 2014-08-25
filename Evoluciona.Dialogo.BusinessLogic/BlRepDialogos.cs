using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.DataAccess;
using System;
using System.Collections.Generic;

namespace Evoluciona.Dialogo.BusinessLogic
{
    public class BlRepDialogos
    {

        private static readonly DaRepDialogos DaRepDialogos = new DaRepDialogos();

        public List<BeRepDialogos> ListarRepDialogoAntNeg(string pais, string periodo, int idRol, bool planMejora)
        {
            try
            {
                var entidades = DaRepDialogos.ListarRepDialogoAntNeg(pais, periodo, idRol, planMejora);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeRepDialogos> ListarRepDialogoAntEqu(string pais, string periodo, int idRol, bool planMejora)
        {
            try
            {
                var entidades = DaRepDialogos.ListarRepDialogoAntEqu(pais, periodo, idRol, planMejora);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeRepDialogos> ListarRepDialogoAntCom(string pais, string periodo, int idRol, bool planMejora)
        {
            try
            {
                var entidades = DaRepDialogos.ListarRepDialogoAntCom(pais, periodo, idRol, planMejora);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<BeRepDialogos> ListarRepDialogoDurNeg(string pais, string periodo, int idRol, bool planMejora)
        {
            try
            {
                var entidades = DaRepDialogos.ListarRepDialogoDurNeg(pais, periodo, idRol, planMejora);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeRepDialogos> ListarRepDialogoDurEqu(string pais, string periodo, int idRol, bool planMejora)
        {
            try
            {
                var entidades = DaRepDialogos.ListarRepDialogoDurEqu(pais, periodo, idRol, planMejora);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeRepDialogos> ListarRepDialogoDurCom(string pais, string periodo, int idRol, bool planMejora)
        {
            try
            {
                var entidades = DaRepDialogos.ListarRepDialogoDurCom(pais, periodo, idRol, planMejora);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
