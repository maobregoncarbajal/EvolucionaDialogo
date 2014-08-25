
using System.Linq;

namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using Helpers;
    using System;
    using System.Collections.Generic;

    public class BlMatriz
    {
        private static readonly DaMatriz MatrizDa = new DaMatriz();

        /// <summary>
        /// este método Obtiene el nombre de un país
        /// </summary>
        /// <param name="prefijoIsoPais">código del país</param>
        /// <returns>nombre del país</returns>
        public string ObtenerNombrePais(string prefijoIsoPais)
        {
            try
            {
                return MatrizDa.ObtenerNombrePais(prefijoIsoPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método obtiene el nombre de una región
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <returns>nombre de la región</returns>
        public string ObtenerNombreRegion(string codPais, string codRegion)
        {
            try
            {
                return MatrizDa.ObtenerNombreRegion(codPais, codRegion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método obtiene el nombre de una región
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <param name="codZona">código de zona</param>
        /// <returns>nombre de la zona</returns>
        public string ObtenerNombreZona(string codPais, string codRegion, string codZona)
        {
            try
            {
                return MatrizDa.ObtenerNombreZona(codPais, codRegion, codZona);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este procedimiento almacenado obtiene la región actual por gernete de region
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código de usuario</param>
        /// <returns>región de gernete de region </returns>
        public BeComun ObtenerRegionUsuario(string codPais, string codigoUsuario)
        {
            try
            {
                return MatrizDa.ObtenerRegionUsuario(codPais, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este procedimiento almacenado obtiene la región actual por gernete de region
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código de usuario</param>
        /// <param name="periodo"></param>
        /// <returns>región de gernete de region </returns>
        public BeComun ObtenerRegionUsuarioporPeriodo(string codPais, string codigoUsuario, string periodo)
        {
            try
            {
                return MatrizDa.ObtenerRegionUsuarioporPeriodo(codPais, codigoUsuario, periodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Este procedimiento almacenado devuelve los años
        /// </summary>
        /// <param name="codPais"> código del país</param>
        /// <returns>lsita de años</returns>
        public List<BeComun> ListarAnhos(string codPais)
        {
            try
            {
                return MatrizDa.ListarAnhos(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método devuelve una lista de periodos según el país y el año
        /// </summary>
        /// <param name="codPais">código país</param>
        /// <param name="anho">año</param>
        /// <param name="idRol">Id Rol</param>
        /// <returns>lista de periodos</returns>
        public List<BeComun> ListarPeriodos(string codPais, string anho, int idRol)
        {
            return MatrizDa.ListarPeriodos(codPais, anho, idRol);
        }

        /// <summary>
        /// Este store procedure lista todas las regiones activas según el país
        /// </summary>
        /// <param name="codPais">Código del país</param>
        /// <returns>lista de regiones</returns>
        public List<BeComun> ListarRegiones(string codPais)
        {
            try
            {
                return MatrizDa.ListarRegiones(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BeComun> ListarRegionesMz(string codPais)
        {
            try
            {
                return MatrizDa.ListarRegionesMz(codPais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<BeComun> ListarRegionGRporPeriodo(string codPais, string codUsuario, string periodo)
        {
            try
            {
                return MatrizDa.ListarRegionGRporPeriodo(codPais, codUsuario, periodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este store procedure lista todas las zonas activas según el país y la región
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <returns>lista zonas</returns>
        public List<BeComun> ListarZonas(string codPais, string codRegion)
        {
            try
            {
                return MatrizDa.ListarZonas(codPais, codRegion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método lista Detalle de información de los Gerente de Region
        /// </summary>
        /// <param name="chrCodPais">código del País</param>
        /// <param name="codigoUsuario"> código del evaulador</param>
        /// <param name="rol"> rol del evaluado</param>
        /// <param name="periodo">perido</param>
        /// <param name="subPeriodo">micro Periodo</param>
        /// <param name="listaTamVenta">lista de nombre de Tamaño de venta</param>
        /// <param name="listaNiveles">lista de Nombres de Niveles</param>
        /// <returns>lista Detalle de Infromación</returns>
        public List<BeDetalleInformacion> ListaDetalleInformacionGr(string chrCodPais, string codigoUsuario, int rol, string periodo, string subPeriodo, List<BeComun> listaTamVenta, List<BeComun> listaNiveles)
        {
            try
            {
                var entidades = MatrizDa.ListaDetalleInformacionGr(chrCodPais, codigoUsuario, rol,
                                                                                          periodo, subPeriodo);

                return ProcesarDetalleInformacion(entidades, chrCodPais, periodo, listaTamVenta, listaNiveles);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método lista Detalle de información de los Gerente de Zona
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código usuario</param>
        /// <param name="rolEvaluador">rol del evaluador</param>
        /// <param name="rolEvaluado">rol del evaluado</param>
        /// <param name="periodo">periodo</param>
        /// <param name="subPeriodo">microperiodo</param>
        /// <param name="codRegion">código de región</param>
        /// <param name="codZona">código de zona</param>
        ///  <param name="listaTamVenta">lista de nombre de Tamaño de venta</param>
        /// <param name="listaNiveles">lista de Nombres de Niveles</param>
        /// <returns>detalle de información</returns>
        public List<BeDetalleInformacion> ListaDetalleInformacionGz(string codPais, string codigoUsuario, int rolEvaluador, int rolEvaluado, string periodo, string subPeriodo, string codRegion, string codZona, List<BeComun> listaTamVenta, List<BeComun> listaNiveles)
        {
            try
            {
                var entidades = MatrizDa.ListaDetalleInformacionGz(codPais, codigoUsuario, rolEvaluador, rolEvaluado, periodo, subPeriodo, codRegion, codZona);

                return ProcesarDetalleInformacion(entidades, codPais, periodo, listaTamVenta, listaNiveles);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método devuelve la ficha personal
        /// </summary>
        /// <param name="codigoPais"> código del país</param>
        /// <param name="codigoUsuario">código del usuario</param>
        /// <returns></returns>
        public BeFichaPersonal ObtenerFichaPersonal(string codigoPais, string codigoUsuario)
        {
            try
            {
                return MatrizDa.ObtenerFichaPersonal(codigoPais, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método les devuelve las variables por país
        /// </summary>
        /// <param name="codPais"> código del país </param>
        /// <returns>lista de variables</returns>
        public List<BeComun> ListarVariablesPais(string codPais)
        {
            try
            {
                var entidades = MatrizDa.ListarVariablesPais(codPais);

                foreach (var entidad in entidades)
                {
                    entidad.Codigo = entidad.Codigo + "-" + entidad.Referencia;
                }
                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método devuelve los genretes de región de un director de ventas
        /// </summary>
        /// <param name="codUsuario"> código de Usuario</param>
        /// <returns> lista de gerentes de regiones</returns>
        public List<BeComun> ListarGerentesRegion(string codUsuario)
        {
            try
            {
                var entidades = MatrizDa.ListarGerentesRegion(codUsuario);

                foreach (var entidad in entidades)
                {
                    entidad.Codigo = entidad.Referencia + "-" + entidad.Codigo;
                }
                return entidades;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método devuelve los genretes de zona de un detrerminado gerente de zona
        /// </summary>
        /// <param name="codUsuario">codigo de usuario</param>
        /// <param name="codPais">código del país</param>
        /// <param name="nombre">nombre de la gerenta de zona</param>
        /// <returns>lista de gerentes de zona</returns>
        public List<BeComun> ListarGerentesZona(string codUsuario, string codPais, string nombre)
        {
            try
            {
                var entidades = MatrizDa.ListarGerentesZona(codUsuario, codPais, nombre);

                foreach (var entidad in entidades)
                {
                    entidad.Codigo = entidad.Referencia + "-" + entidad.Codigo;
                }

                return entidades;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método devuelve los resultados de lso gerentes
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="anho">año</param>
        /// <param name="codigoUsuario">código del usuario</param>
        /// <param name="codVariable">código de variable</param>
        /// <param name="periodo">periodo</param>
        /// <param name="esVenta">Venta</param>
        /// <param name="codRol">código Rol</param>
        /// <param name="codRegion">código región</param>
        /// <returns>lista de resultados</returns>
        public List<BeResultadoMatriz> ListarResultados(string codPais, string anho, string codigoUsuario, string codVariable, string periodo, string codRol, string codRegion, bool esVenta)
        {
            try
            {
                var entidades = MatrizDa.ListarResultados(codPais, anho, codigoUsuario, codVariable, periodo, codRol);
                return ProcesarResultadoMatriz(entidades, esVenta, codPais, anho, codVariable, periodo, codRegion, codRol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método obtiene el cuadrante por Gerente
        /// </summary>
        /// <param name="codPais">código País</param>
        /// <param name="codigoUsuario">código Usuario</param>
        /// <param name="idRol">id Rol</param>
        /// <param name="anho">año</param>
        /// <param name="periodo">periodo</param>
        /// <param name="listaNiveles"> niveles xml</param>
        /// <returns>cuadrante usuario</returns>
        public BeCuadranteUsuario ObtenerCuadranteUsuario(string codPais, string codigoUsuario, int idRol, string anho, string periodo, List<BeComun> listaNiveles)
        {
            try
            {
                var entidad = MatrizDa.ObtenerCuadranteUsuario(codPais, codigoUsuario, idRol, anho,
                                                                              periodo);

                if (!string.IsNullOrEmpty(entidad.DocIdentidad))
                {
                    var niveles = MatrizDa.ListaNivelesCompetencia(codPais, anho);

                    // poner nombre a las competencias según el xml de lista de competencias
                    foreach (var item in listaNiveles)
                    {
                        foreach (var nivel in niveles)
                        {
                            if (item.Codigo != nivel.CodCompetencia) continue;
                            nivel.NombreCompetencia = item.Descripcion;
                            break;
                        }
                    }

                    var count = 0;
                    foreach (var nivel in niveles)
                    {
                        var pass = false;
                        if (count == 0) // es el primer item para poner >= , >=
                        {
                            if (entidad.PorcentajeAvance >= nivel.MinValue &&
                                entidad.PorcentajeAvance <= nivel.MaxValue)
                            {
                                entidad.NivelCompetencia = nivel.NombreCompetencia;
                                pass = true;
                            }
                        }
                        else
                        {
                            if (entidad.PorcentajeAvance > nivel.MinValue &&
                                entidad.PorcentajeAvance <= nivel.MaxValue)
                            {
                                entidad.NivelCompetencia = nivel.NombreCompetencia;
                            }
                        }

                        if (string.IsNullOrEmpty(entidad.NivelCompetencia))
                        {
                            entidad.Cuadrante = entidad.EstadoPeriodo;
                        }
                        else
                        {
                            entidad.Cuadrante = entidad.EstadoPeriodo + "-" + entidad.NivelCompetencia;
                        }

                        if (pass)
                        {
                            break;
                        }

                        count++;
                    }
                }

                return entidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método lista los gerentes de zona de una determinada zona
        /// </summary>
        /// <param name="codPais"> código país</param>
        /// <param name="codRegion">código región</param>
        /// <param name="codZona"> código zona</param>
        /// <param name="periodo"> periodo</param>
        /// <returns>gerentes de zona</returns>
        public List<BeComun> ListarGerenteZonaByZona(string codPais, string codRegion, string codZona, string periodo)
        {
            try
            {
                var entidades = MatrizDa.ListarGerenteZonaByZona(codPais, codRegion, codZona, periodo);
                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        ///  este procedimiento obtiene la campaña actual del usuario evaluado
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código del uusario</param>
        /// <param name="idRol">id rol</param>
        /// <param name="periodo">periodo</param>
        /// <returns> campaña actual</returns>
        public string ObtenerCampanhaActual(string codPais, string codigoUsuario, int idRol, string periodo)
        {
            try
            {
                return MatrizDa.ObtenerCampanhaActual(codPais, codigoUsuario, idRol, periodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método lista todas las zonas disponibles
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codRegion">código de la región</param>
        /// <param name="periodo">periodo</param>
        /// <param name="estadoActivo">estado Activo</param>
        /// <returns>zonas disponibles</returns>
        public List<BeComun> ListarAllZonaDisponible(string codPais, string codRegion, string periodo, int estadoActivo)
        {
            try
            {
                return MatrizDa.ListarAllZonaDisponible(codPais, codRegion, periodo, estadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método inserta una toma de acción
        /// </summary>
        /// <param name="tomaAccion">toma de acción</param>
        /// <returns>id toma de acción</returns>
        public int InsertarTomaAccion(BeTomaAccion tomaAccion)
        {
            try
            {
                return MatrizDa.InsertarTomaAccion(tomaAccion);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método confirma las tomas de acciones
        /// </summary>
        /// <param name="entidades">tomas de acción</param>
        /// <returns>estado </returns>
        public bool ConfirmarTomaAccion(List<BeTomaAccion> entidades)
        {
            try
            {
                return MatrizDa.ConfirmarTomaAccion(entidades);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método actualiza una calibración
        /// </summary>
        /// <param name="listaCalibracion">lista Calibraciones</param>
        /// <param name="usuario">lista Calibraciones</param>
        /// <returns>estado</returns>
        public bool UpdateCalibracion(List<BeTomaAccion> listaCalibracion, string usuario)
        {
            try
            {
                return MatrizDa.UpdateCalibracion(listaCalibracion, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método lista todas las tomas de acciones
        /// </summary>
        /// <param name="codPais">código del país del evaluador </param>
        /// <param name="codEvaluador">código del evaluador</param>
        /// <param name="periodo">periodo del evaluador</param>
        /// <param name="idRolEvaluador">id rol del evaluador</param>
        /// <param name="idRolEvaluado">id rol del evaluado</param>
        /// <param name="tomaAcciones">lista Toma Acciones</param> 
        /// <param name="codTomaAccion">código toma de acción</param>
        /// <param name="estadoActivo">estadoActivo</param>
        /// <returns>toma de acciones</returns>
        public List<BeTomaAccion> ListarTomaAcciones(string codPais, string codEvaluador, string periodo, int idRolEvaluador, int idRolEvaluado, List<BeComun> tomaAcciones, int estadoActivo, string codTomaAccion)
        {
            try
            {
                var entidades = MatrizDa.ListarTomaAcciones(codPais, codEvaluador, periodo,
                                                                           idRolEvaluador, idRolEvaluado, estadoActivo,
                                                                           codTomaAccion);

                foreach (var entidad in entidades)
                {
                    entidad.NombreRolEvaluado = Enum.GetName(typeof(TipoRol), entidad.IdRolEvaluado);
                    entidad.EstatusClickVer = entidad.Estatus.ToString().ToLower();
                    entidad.EstatusChangeDdl = entidad.Estatus.ToString().ToLower();

                    foreach (var tomaAcion in tomaAcciones)
                    {
                        if (entidad.TomaAccion != tomaAcion.Codigo) continue;
                        entidad.NombreTomaAccion = tomaAcion.Descripcion;

                        break;
                    }
                }

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método lista todas las tomas de acciones
        /// </summary>
        /// <param name="codPais">código del país del evaluador </param>
        /// <param name="periodo">periodo del evaluador</param>
        /// <param name="tomaAcciones">toma de acciones</param>
        /// <returns>toma de acciones</returns>
        public List<BeTomaAccion> ListarCalibraciones(string codPais, string periodo, List<BeComun> tomaAcciones)
        {
            try
            {
                var entidades = MatrizDa.ListarCalibraciones(codPais, periodo);

                foreach (var entidad in entidades)
                {
                    foreach (var tomaAcion in tomaAcciones)
                    {
                        if (entidad.TomaAccion != tomaAcion.Codigo) continue;
                        entidad.NombreTomaAccion = tomaAcion.Descripcion;
                        break;
                    }
                }

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método lista todas las regiones disponibles
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="periodo">periodo</param>
        /// <param name="estadoActivo">estado Activo</param>
        /// <returns>zonas disponibles</returns>
        public List<BeComun> ListarAllRegionDisponible(string codPais, string periodo, int estadoActivo)
        {
            try
            {
                var entidades = MatrizDa.ListarAllRegionDisponible(codPais, periodo, estadoActivo);

                foreach (var entidad in entidades)
                {
                    entidad.Codigo = entidad.Referencia.Trim() + "-" + entidad.Codigo.Trim();
                }

                return entidades;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este store procedure lista todas las variables de enfoque de un colaborador
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codEvaluado">código del evaluado</param>
        /// <param name="idRol">rol de evaluado</param>
        /// <param name="periodo">periodo</param>
        /// <returns>variables enfoque</returns>
        public List<BeComun> ListarVariablesEnfoque(string codPais, string codEvaluado, int idRol, string periodo)
        {
            try
            {
                var entidades = MatrizDa.ListarVariablesEnfoque(codPais, codEvaluado, idRol, periodo);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método lista los resultados del sustento de una GR
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código de usuario</param>
        /// <param name="codVariable">código de variable</param>
        /// <param name="periodo">periodo</param>
        /// <param name="idRol">id rol evaluado</param>
        /// <param name="idTomaAccion">id secuencial toma de acción</param>
        /// <param name="tipoTomaAccion">tipo de toma de acción</param>
        /// <returns>resultados </returns>
        public List<BeResultadoMatriz> ListarResultadosSusteno(string codPais, string codigoUsuario, string codVariable, string periodo, int idRol, int idTomaAccion, string tipoTomaAccion)
        {
            try
            {
                var entidades = MatrizDa.ListarResultadosSusteno(codPais, codigoUsuario, codVariable, periodo, idRol, idTomaAccion, tipoTomaAccion);

                return ProcesarResultadoMatrizSustento(entidades);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  este procedimiento obtiene la campaña actual del usuario evaluado
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="codigoUsuario">código del uusario</param>
        /// <param name="idRol">id rol</param>
        /// <param name="periodo">periodo</param>
        /// <param name="estadoActivo">estado Activo</param>
        /// <returns> estado</returns>
        public string ValidarTomaAcuerdo(string codPais, string codigoUsuario, int idRol, string periodo, int estadoActivo)
        {
            try
            {
                return MatrizDa.ValidarTomaAcuerdo(codPais, codigoUsuario, idRol, periodo, estadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método verifica si el periodo de la campaña dada está activo para un país
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="campanha">código de la campaña</param>
        /// <returns>estado </returns>
        public string ValidarPeriodoPorCampanha(string codPais, string campanha)
        {
            try
            {
                return MatrizDa.ValidarPeriodoPorCampanha(codPais, campanha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// método para obtener el correo de un supervisor
        /// </summary>
        /// <param name="codPaisEvaluado">código del país del evaluador</param>
        /// <param name="codUsuarioEvaluado">código del usuario evaluado</param>
        /// <param name="idRolEvaluador">id rol evaluador</param>
        /// <param name="idRolEvaluado">id rol evaluado</param>
        /// <returns></returns>
        public string ObtenerCorreo(string codPaisEvaluado, string codUsuarioEvaluado, int idRolEvaluador, int idRolEvaluado)
        {
            try
            {
                return MatrizDa.ObtenerCorreo(codPaisEvaluado, codUsuarioEvaluado, idRolEvaluador, idRolEvaluado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Calculate percentile of a sorted data set
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="excelPercentile"></param>
        /// <returns></returns>
        private static double Percentile(double[] sequence, double excelPercentile)
        {
            Array.Sort(sequence);
            var m = sequence.Length;
            var n = (m - 1) * excelPercentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (Math.Abs(n - 1d) <= 0) return sequence[0];
            if (Math.Abs(n - m) <= 0) return sequence[m - 1];
            var k = (int)n;
            var d = n - k;
            return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
        } // end of internal function percentile

        /// <summary>
        /// este método realiza cálculos para obtener el detalle de información
        /// </summary>
        /// <param name="entidades">detalle de informacion</param>
        /// <param name="chrCodPais">código del país</param>
        /// <param name="periodo">periodo</param>
        /// <param name="listaTamVenta">xml tamaño venta</param>
        /// <param name="listaNiveles">xml niveles</param>
        /// <returns>lista de detalle de información</returns>
        private List<BeDetalleInformacion> ProcesarDetalleInformacion(List<BeDetalleInformacion> entidades, string chrCodPais, string periodo, IReadOnlyList<BeComun> listaTamVenta, IEnumerable<BeComun> listaNiveles)
        {
            var niveles = MatrizDa.ListaNivelesCompetencia(chrCodPais, periodo.Substring(0, 4));

            // poner nombre a las competencias según el xml de lista de competencias
            foreach (var item in listaNiveles)
            {
                foreach (var nivel in niveles)
                {
                    if (item.Codigo == nivel.CodCompetencia)
                    {
                        nivel.NombreCompetencia = item.Descripcion;
                        break;
                    }
                }
            }

            if (entidades.Count > 0)
            {
                var ventas = new List<double>();

                double totalSumaVenta = 0;

                foreach (var entidad in entidades)
                {
                    ventas.Add(entidad.VentaPeriodo);
                    totalSumaVenta = totalSumaVenta + entidad.VentaPeriodo;
                    entidad.Cuadrante = string.Empty;
                    entidad.LogroPeriodo = (Math.Abs(entidad.VentaPlanPeriodo) > 0) ? Convert.ToInt32(Math.Ceiling((1 - (entidad.VentaPlanPeriodo - entidad.VentaPeriodo) / entidad.VentaPlanPeriodo) * 100)) : 0;
                    entidad.NivelCompetencia = string.Empty;

                    // obtenemos los niveles de competecias según valores mínimos y máximos

                    var count = 0;
                    foreach (var nivel in niveles)
                    {
                        var pass = false;
                        if (count == 0)// es el primer item para poner >= , >=
                        {
                            if (entidad.PorcentajeAvance >= nivel.MinValue && entidad.PorcentajeAvance <= nivel.MaxValue)
                            {
                                entidad.NivelCompetencia = nivel.NombreCompetencia;
                                pass = true;
                            }
                        }
                        else
                        {
                            if (entidad.PorcentajeAvance > nivel.MinValue && entidad.PorcentajeAvance <= nivel.MaxValue)
                            {
                                entidad.NivelCompetencia = nivel.NombreCompetencia;
                            }
                        }

                        if (string.IsNullOrEmpty(entidad.NivelCompetencia))
                        {
                            entidad.Cuadrante = entidad.Ranking;
                        }
                        else
                        {
                            entidad.Cuadrante = entidad.Ranking + "-" + entidad.NivelCompetencia;
                        }

                        if (pass)
                        {
                            break;
                        }

                        count++;
                    }
                }

                var minVenta = Percentile(ventas.ToArray(), 0);
                var maxValue = Percentile(ventas.ToArray(), 1);
                var maxPercentilPequeno = Percentile(ventas.ToArray(), 0.33);
                var maxPercentilMediano = Percentile(ventas.ToArray(), 0.66);

                foreach (var entidad in entidades)
                {
                    if (entidad.VentaPeriodo >= minVenta && entidad.VentaPeriodo < maxPercentilPequeno)
                    {
                        entidad.TamVenta = listaTamVenta[0].Descripcion;
                    }

                    else if (entidad.VentaPeriodo >= maxPercentilPequeno && entidad.VentaPeriodo < maxPercentilMediano)
                    {
                        entidad.TamVenta = listaTamVenta[1].Descripcion;
                    }
                    else if (entidad.VentaPeriodo >= maxPercentilMediano && entidad.VentaPeriodo <= maxValue)
                    {
                        entidad.TamVenta = listaTamVenta[2].Descripcion;
                    }

                    entidad.ParticipacionVenta = Math.Abs(totalSumaVenta) > 0 ? Math.Round(entidad.VentaPeriodo * 100 / totalSumaVenta, 2) : 0;
                }
            }
            return entidades;
        }

        private List<BeResultadoMatriz> ProcesarResultadoMatriz(List<BeResultadoMatriz> entidades, bool esVenta, string codPais, string anho, string codVariable, string periodo, string codRegion, string codRol)
        {
            if (entidades.Count > 0)
            {
                if (esVenta)
                {
                    var ventas = MatrizDa.ListarVentas(codPais, anho, codVariable, periodo, codRegion, codRol);

                    foreach (var entidad in entidades)
                    {
                        //Participación
                        foreach (var venta in ventas)
                        {
                            if (entidad.AnioCampana == venta.AnioCampana)
                            {
                                entidad.ParticipacionCampana = (Math.Abs(venta.ValorRealCampana) > 0) ? Math.Round(entidad.ValorRealCampana * 100 / venta.ValorRealCampana, 2) : 0;
                                entidad.ParticipacionPeriodo = (Math.Abs(venta.ValorRealPeriodo) > 0) ? Math.Round(entidad.ValorRealPeriodo * 100 / venta.ValorRealPeriodo, 2) : 0;
                            }
                        }

                        //Logro
                        entidad.LogroCampana = (Math.Abs(entidad.ValorPlanCampana) > 0) ? Convert.ToInt32(Math.Ceiling((1 - (entidad.ValorPlanCampana - entidad.ValorRealCampana) / entidad.ValorPlanCampana) * 100)) : 0;
                        entidad.LogroPeriodo = (Math.Abs(entidad.ValorPlanPeriodo) > 0) ? Convert.ToInt32(Math.Ceiling((1 - (entidad.ValorPlanPeriodo - entidad.ValorRealPeriodo) / entidad.ValorPlanPeriodo) * 100)) : 0;

                        //Nombre Campaña

                        entidad.NombreCampanha = string.Format("C{0}", entidad.AnioCampana.Substring(4, 2));
                    }
                }
                else
                {
                    foreach (var entidad in entidades)
                    {
                        entidad.ParticipacionCampana = entidad.ValorPlanCampana;
                        entidad.LogroCampana = entidad.ValorRealCampana;
                        entidad.ParticipacionPeriodo = entidad.ValorPlanPeriodo;
                        entidad.LogroPeriodo = entidad.ValorRealPeriodo;
                        entidad.NombreCampanha = string.Format("C{0}", entidad.AnioCampana.Substring(4, 2));
                    }
                }
            }

            return entidades;
        }

        private List<BeResultadoMatriz> ProcesarResultadoMatrizSustento(List<BeResultadoMatriz> entidades)
        {
            if (entidades.Count > 0)
            {
                foreach (var entidad in entidades)
                {
                    entidad.ParticipacionCampana = entidad.ValorPlanCampana;
                    entidad.LogroCampana = entidad.ValorRealCampana;
                    entidad.ParticipacionPeriodo = entidad.ValorPlanPeriodo;
                    entidad.LogroPeriodo = entidad.ValorRealPeriodo;
                    entidad.NombreCampanha = string.Format("{0}-C{1}", entidad.AnioCampana.Substring(0, 4), entidad.AnioCampana.Substring(4, 2));
                }
            }
            return entidades;
        }
        #region Registrar Acuerdos

        /// <summary>
        /// este método devuelve los gerentes de zona según los lineamientos establecidos para una toma de acción
        /// </summary>
        /// <param name="codUsuario">codigo de usuario</param>
        /// <param name="codPais">código del país</param>
        /// <param name="periodo">periodo actual</param>
        /// <param name="condicion">condición de la toma de acción</param>
        /// <returns>lista de gerentes de zona</returns>
        public List<BeComun> ListarGerentesZonaByLineamientos(string codUsuario, string codPais, string periodo, string condicion)
        {
            try
            {
                var lista = new List<BeComun>();
                var entidades = MatrizDa.ListarGerentesZonaByLineamientos(codUsuario, codPais, periodo, condicion);

                if (condicion == Constantes.MatrizConstPlanMejora)
                {
                    lista.AddRange(entidades.Select(entidad => new BeComun
                    {
                        Codigo = entidad.Referencia + "-" + entidad.Codigo.Trim() + "-" + entidad.CampanaMinima.Trim() + "-" + entidad.CampanaMaxima.Trim(), Descripcion = entidad.Descripcion
                    }));
                }
                else
                {
                    lista.AddRange(entidades.Select(entidad => new BeComun
                    {
                        Codigo = entidad.Referencia + "-" + entidad.Codigo, Descripcion = entidad.Descripcion
                    }));
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// este método devuelve los gerentes de región segun los lineamientos establecidos para una toma de acción
        /// </summary>
        /// <param name="codUsuario">codigo de usuario</param>
        /// <param name="codPais">código del país</param>
        /// <param name="periodo">periodo actual</param>
        /// <param name="condicion">condición de la toma de acción</param>
        /// <returns>lista de gerentes de región</returns>
        public List<BeComun> ListarGerentesRegionByLineamientos(string codUsuario, string codPais, string periodo, string condicion)
        {
            try
            {
                var lista = new List<BeComun>();
                var entidades = MatrizDa.ListarGerentesRegionByLineamientos(codUsuario, codPais, periodo, condicion);

                if (condicion == Constantes.MatrizConstPlanMejora)
                {
                    lista.AddRange(entidades.Select(entidad => new BeComun
                    {
                        Codigo = entidad.Referencia + "-" + entidad.Codigo.Trim() + "-" + entidad.CampanaMinima.Trim() + "-" + entidad.CampanaMaxima.Trim(), Descripcion = entidad.Descripcion
                    }));
                }
                else
                {
                    lista.AddRange(entidades.Select(entidad => new BeComun
                    {
                        Codigo = entidad.Referencia + "-" + entidad.Codigo, Descripcion = entidad.Descripcion
                    }));
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region tomaAccion

        /// <summary>
        ///  este procedimiento obtiene las condiciones de toma de accion
        /// </summary>
        /// <param name="codGerenteZona">código de gerente de zona</param>
        /// <param name="prefijoPais">prefijo del pais</param>
        /// <param name="prefijoEvaluacion"> prefijo de la evaluacion</param>
        /// <param name="tipoCondicion">tipo de condicion</param>
        /// /// <param name="idRolEval">id del rol para filtrar gerente de zona o gerene de region</param>
        /// <returns> lista de beVerCondiciones</returns>
        public List<BeVerCondiciones> ObtenerVerCondiciones(string codGerenteZona, string prefijoPais, string prefijoEvaluacion, string tipoCondicion, string idRolEval)
        {
            try
            {
                return int.Parse(idRolEval) == (int)TipoRol.GerenteZona ? MatrizDa.ObtenerVerCondicionesGerentesZona(codGerenteZona, prefijoPais, prefijoEvaluacion, tipoCondicion) : MatrizDa.ObtenerVerCondicionesGerentesRegion(codGerenteZona, prefijoPais, prefijoEvaluacion, tipoCondicion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método elimina una toma de acción
        /// </summary>
        /// <param name="idTomaAccion"> id toma de acción</param>
        /// <returns> estado</returns>
        public int DeleteTomaAccion(int idTomaAccion)
        {
            try
            {
                return MatrizDa.DeleteTomaAccion(idTomaAccion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region "Matriz Zona"
        /// <summary>
        /// método para obtener el Tipo de Matriz Zona
        /// </summary>
        /// <param name="codPais">código del país</param>
        /// <param name="estado">estado</param>
        /// <returns></returns>
        public string ObtenerTipoMz(string codPais, byte estado)
        {
            try
            {
                return MatrizDa.ObtenerTipoMz(codPais, estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método lista Matriz Zona Sin FuenteVentas
        /// </summary>
        /// <param name="obj">Variables Matriz Zona</param>
        /// <returns>lista Matriz Zona</returns>
        public List<BeMatrizZona> ListaMatrizZonaSinFuenteVentas(BeMatrizZonaVariables obj)
        {
            try
            {
                var entidades = MatrizDa.ListaMatrizZonaSinFuenteVentas(obj);

                return entidades;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método lista Matriz Zona Sin FuenteVentas
        /// </summary>
        /// <param name="obj">Variables Matriz Zona</param>
        /// <returns>lista Matriz Zona</returns>
        public List<BeMatrizZona> ListaMatrizZonaConFuenteVentas(BeMatrizZonaVariables obj)
        {
            try
            {
                var entidades = MatrizDa.ListaMatrizZonaConFuenteVentas(obj);

                return entidades;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        #endregion
    }
}