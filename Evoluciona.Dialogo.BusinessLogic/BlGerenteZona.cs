
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlGerenteZona
    {
        private static readonly DaGerenteZona DaGerenteZona = new DaGerenteZona();
        private static readonly DaRegion DaRegion = new DaRegion();
        private static readonly DaZona DaZona = new DaZona();

        public List<BeGerenteZona> GerenteZonaListar(string prefijoIsoPais, int idGerenteRegion, string nombreCompleto,
                                                     int idGerenteZona)
        {
            return DaGerenteZona.GerenteZonaListar(prefijoIsoPais, idGerenteRegion, nombreCompleto, idGerenteZona);
        }

        public List<BeGerenteZona> GerenteZonaListarAlta(string prefijoIsoPais, string anioCampania,
                                                             string codigoRegion, string codigoZona,
                                                             string nombreGerente, string periodo)
        {
            return DaGerenteZona.GerenteZonaListarAlta(prefijoIsoPais, anioCampania, codigoRegion, codigoZona,
                                                            nombreGerente, periodo);
        }

        public bool GerenteZonaRegistrar(List<BeGerenteZona> listaGerenteZona)
        {
            return DaGerenteZona.GerenteZonaRegistrar(listaGerenteZona);
        }

        public bool GerenteZonaActualizar(BeGerenteZona obeGerenteZona)
        {
            return DaGerenteZona.GerenteZonaActualizar(obeGerenteZona);
        }

        public bool GerenteZonaActualizarEstado(BeGerenteZona obeGerenteZona)
        {
            return DaGerenteZona.GerenteZonaActualizarEstado(obeGerenteZona);
        }

        public bool GerenteZonaActualizarGerenteRegion(BeGerenteZona obeGerenteZona)
        {
            return DaGerenteZona.GerenteZonaActualizarGerenteRegion(obeGerenteZona);
        }

        public List<BeGerenteZona> GerenteZonaListarReporte(string prefijoIsoPais, string nombreCompleto, bool estado, string codigoRegion, string codigoZona, string periodo)
        {
            return DaGerenteZona.GerenteZonaListarReporte(prefijoIsoPais, nombreCompleto, estado, codigoRegion, codigoZona, periodo);
        }

        public List<BeGerenteZona> GerenteZonaListarReporteHistorico(string prefijoIsoPais, string codigoGerenteZona)
        {
            return DaGerenteZona.GerenteZonaListarReporteHistorico(prefijoIsoPais, codigoGerenteZona);
        }

        public List<BeGerenteZona> GerenteZonaGetAll(string prefijoIsoPais, string nombreCompleto, string codigoRegion, string codigoZona, string periodo)
        {
            return DaGerenteZona.GerenteZonaGetAll(prefijoIsoPais, nombreCompleto, codigoRegion, codigoZona, periodo);
        }


        public List<BeGerenteZona> ListaGz(string prefijoIsoPais)
        {
            try
            {
                var entidades = DaGerenteZona.ListaGz(prefijoIsoPais);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeGerenteZona> ListaGzPaginacion(string sortColumnName, string sortOrderBy, string prefijoIsoPais, int filaInicio, int numFilas)
        {
            try
            {
                var entidades = DaGerenteZona.ListaGzPaginacion(sortColumnName, sortOrderBy, prefijoIsoPais, filaInicio, numFilas);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeGerenteZona> ListaGzPaginacionBusqueda(string columnaBuscar, string valorBuscar, string operadorBuscar,string sortColumnName, string sortOrderBy, string prefijoIsoPais, int filaInicio, int numFilas)
        {
            try
            {
                var entidades = DaGerenteZona.ListaGzPaginacionBusqueda(columnaBuscar, valorBuscar, operadorBuscar, sortColumnName, sortOrderBy, prefijoIsoPais, filaInicio, numFilas);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeGerenteZona> ListaGzPaginacionBusquedaAvanzada(string filters, string sortColumnName, string sortOrderBy, string prefijoIsoPais, int filaInicio, int numFilas)
        {
            try
            {
                var entidades = DaGerenteZona.ListaGzPaginacionBusquedaAvanzada(filters, sortColumnName, sortOrderBy, prefijoIsoPais, filaInicio, numFilas);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotal(string prefijoIsoPais)
        {
            try
            {
                var total = DaGerenteZona.GetTotal(prefijoIsoPais);

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotalBusqueda(string prefijoIsoPais, string columnaBuscar, string valorBuscar, string operadorBuscar)
        {
            try
            {
                var totalBusqueda = DaGerenteZona.GetTotalBusqueda(prefijoIsoPais, columnaBuscar, valorBuscar, operadorBuscar);

                return totalBusqueda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotalBusquedaAvanzada(string filters, string prefijoIsoPais)
        {
            try
            {
                var totalBusqueda = DaGerenteZona.GetTotalBusquedaAvanzada(filters, prefijoIsoPais);

                return totalBusqueda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool DeleteGz(int id)
        {

            try
            {
                var estado = DaGerenteZona.DeleteGz(id);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool AddGr(BeGerenteZona obj)
        {

            try
            {
                var estado = DaGerenteZona.AddGz(obj);

                return estado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool EditGz(BeGerenteZona obj)
        {
            try
            {
                var estado = DaGerenteZona.EditGz(obj);

                return estado;
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
                return DaGerenteZona.ListarPaises(codPais);
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

        public List<BeZona> ListarZonas(string codPais, string codRegion)
        {
            try
            {
                return DaZona.ListarZonasPorRegion(codPais, codRegion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ValidaCodGz(string pais, string region, string zona, string codGz)
        {
            try
            {
                return DaZona.ValidaCodGz(pais, region, zona, codGz);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ValidaCubGz(string pais, string region, string zona, string cub)
        {
            try
            {
                return DaZona.ValidaCubGz(pais, region, zona, cub);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}