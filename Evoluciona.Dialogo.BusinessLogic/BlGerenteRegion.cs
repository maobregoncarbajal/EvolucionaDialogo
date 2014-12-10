
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlGerenteRegion
    {
        private static readonly DaGerenteRegion DaGerenteRegion = new DaGerenteRegion();
        private static readonly DaRegion DaRegion = new DaRegion();

        public List<BeGerenteRegion> ObtenerGr(int intUsuarioCrea, bool bitEstado)
        {
            return DaGerenteRegion.ObtenerGr(intUsuarioCrea, bitEstado);
        }

        public List<BeGerenteRegion> ObtenerEvaluadores(int intUsuarioCrea, bool bitEstado, string prefijoIsoPais,
                                                        int rol, string dni)
        {
            return DaGerenteRegion.ObtenerEvaluadores(intUsuarioCrea, bitEstado, prefijoIsoPais, rol, dni);
        }

        public List<BeColaborador> ObtenerColaborador(string nombre, string codpais, int rol, string dni)
        {
            return DaGerenteRegion.ObtenerColaborador(nombre, codpais, rol, dni);
        }

        public bool UpdateMaes(string prefijoIsoPais, int rol, string dni, string dniAnterior)
        {
            return DaGerenteRegion.UpdateMaes(prefijoIsoPais, rol, dni, dniAnterior);
        }

        public bool UpdateMaesBaja(string prefijoIsoPais, int rol, string dni)
        {
            return DaGerenteRegion.UpdateMaesBaja(prefijoIsoPais, rol, dni);
        }

        public string ObtenerDescripcionRegion(int idProceso, string codigoUsuario, string periodo,
                                               string prefijoIsoPais)
        {
            return DaGerenteRegion.ObtenerDescripcionRegion(idProceso, codigoUsuario, periodo, prefijoIsoPais);
        }

        public string ObtenerDescripcionZona(int idProceso, string codigoUsuario, string periodo, string prefijoIsoPais)
        {
            return DaGerenteRegion.ObtenerDescripcionZona(idProceso, codigoUsuario, periodo, prefijoIsoPais);
        }

        public int ObtenerAnio()
        {
            return DaGerenteRegion.ObtenerAnio();
        }

        #region "Mantenimiento BELCORP - DATAMART"

        public int GerenteRegionListarId(string prefijoIsoPais, string codigoGerenteRegion)
        {
            return DaGerenteRegion.GerenteRegionListarId(prefijoIsoPais, codigoGerenteRegion);
        }

        public List<BeGerenteRegion> GerenteRegionListar(string prefijoIsoPais, string nombreCompleto,
                                                         string codigoGerenteRegion, string codigoDirectorVenta)
        {
            return DaGerenteRegion.GerenteRegionListar(prefijoIsoPais, nombreCompleto, codigoGerenteRegion,
                                                        codigoDirectorVenta);
        }

        public List<BeGerenteRegion> GerenteRegionListarAlta(string prefijoIsoPais, string anioCampania,
                                                                 string codigoRegion,
                                                                 string nombreCompleto, string periodo)
        {
            return DaGerenteRegion.GerenteRegionListarAlta(prefijoIsoPais, anioCampania, codigoRegion,
                                                                nombreCompleto, periodo);
        }

        public bool GerenteRegionRegistrar(List<BeGerenteRegion> listaGerenteRegion)
        {
            return DaGerenteRegion.GerenteRegionRegistrar(listaGerenteRegion);
        }

        public bool GerenteRegionRegistrar(BeGerenteRegion obeGerenteRegion)
        {
            return DaGerenteRegion.GerenteRegionRegistrar(obeGerenteRegion);
        }

        public bool GerenteRegionActualizar(BeGerenteRegion obeGerenteRegion)
        {
            return DaGerenteRegion.GerenteRegionActualizar(obeGerenteRegion);
        }

        public bool GerenteRegionActualizarEstado(BeGerenteRegion obeGerenteRegion)
        {
            return DaGerenteRegion.GerenteRegionActualizarEstado(obeGerenteRegion);
        }

        public List<BeGerenteRegion> GerenteRegionListarReporte(string prefijoIsoPais, string nombreCompleto, bool estado, string codigoRegion, string periodo)
        {
            return DaGerenteRegion.GerenteRegionListarReporte(prefijoIsoPais, nombreCompleto, estado, codigoRegion, periodo);
        }

        public List<BeGerenteRegion> GerenteRegionListarReporteHistorico(string prefijoIsoPais, string codigoGerenteRegion)
        {
            return DaGerenteRegion.GerenteRegionListarReporteHistorico(prefijoIsoPais, codigoGerenteRegion);
        }

        public List<BeGerenteRegion> GerenteRegionGetAll(string prefijoIsoPais, string nombreCompleto, string codigoRegion, string periodo)
        {
            return DaGerenteRegion.GerenteRegionGetAll(prefijoIsoPais, nombreCompleto, codigoRegion, periodo);
        }

        #endregion "Mantenimiento BELCORP - DATAMART"

        public List<BeGerenteRegion> ListaGr(string prefijoIsoPais)
        {
            try
            {
                var entidades = DaGerenteRegion.ListaGr(prefijoIsoPais);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteGr(int id)
        {
            try
            {
                var estado = DaGerenteRegion.DeleteGr(id);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddGr(BeGerenteRegion obj)
        {
            try
            {
                var estado = DaGerenteRegion.AddGr(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditGr(BeGerenteRegion obj)
        {
            try
            {
                var estado = DaGerenteRegion.EditGr(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeRegion> ListarRegiones(string codPais)
        {
            try
            {
                return DaRegion.ListarRegionesPorPais(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarPaises(string codPais)
        {
            try
            {
                return DaGerenteRegion.ListarPaises(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ValidaCodGr(string pais, string region, string codGr)
        {
            try
            {
                return DaGerenteRegion.ValidaCodGr(pais, region, codGr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ValidaCubGr(string pais, string region,string cub)
        {
            try
            {
                return DaGerenteRegion.ValidaCubGr(pais, region, cub);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}