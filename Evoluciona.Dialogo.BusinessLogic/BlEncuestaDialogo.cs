
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlEncuestaDialogo
    {
        private static readonly DaEncuestaDialogo DaEncuestaDialogo = new DaEncuestaDialogo();

        public List<BeComun> ListarPreguntas()
        {
            try
            {
                return DaEncuestaDialogo.ListarPreguntas();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarTipoEncuesta()
        {
            try
            {
                return DaEncuestaDialogo.ListarTipoEncuesta();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarTipoRespuesta()
        {
            try
            {
                return DaEncuestaDialogo.ListarTipoRespuesta();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeEncuestaDialogo> ListaEncuestaDialogo()
        {
            try
            {
                var entidades = DaEncuestaDialogo.ListaEncuestaDialogo();

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteEncuestaDialogo(int id)
        {
            try
            {
                var estado = DaEncuestaDialogo.DeleteEncuestaDialogo(id);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddEncuestaDialogo(BeEncuestaDialogo obj)
        {
            try
            {
                var estado = DaEncuestaDialogo.AddEncuestaDialogo(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditEncuestaDialogo(BeEncuestaDialogo obj)
        {
            try
            {
                var estado = DaEncuestaDialogo.EditEncuestaDialogo(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeEncuestaDialogo> ListaEncuestaCompletar(BeEncuestaDialogo obj)
        {
            try
            {
                var entidades = DaEncuestaDialogo.ListaEncuestaCompletar(obj);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeEncuestaRespuesta> ListaOpcionesRspts()
        {
            try
            {
                var entidades = DaEncuestaDialogo.ListaOpcionesRspts();

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertRspts(BeEncuestaFfvv obj)
        {
            try
            {
                var estado = DaEncuestaDialogo.InsertRspts(obj);
                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CantPorAprobarDv(BeUsuario obj)
        {
            try
            {
                var estado = DaEncuestaDialogo.CantPorAprobarDv(obj);
                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CantPorAprobarGr(BeUsuario obj)
        {
            try
            {
                var estado = DaEncuestaDialogo.CantPorAprobarGr(obj);
                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool LlenoEncuesta(BeUsuario obj, string codTipoEncuesta)
        {
            try
            {
                var estado = DaEncuestaDialogo.LlenoEncuesta(obj, codTipoEncuesta);
                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
