
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    
    public class BlConfiguracion
    {

        private static readonly DaConfiguracion ObjConfig = new DaConfiguracion();

        /// <summary>
        /// Lista los paises activos
        /// </summary>
        /// <returns></returns>
        public DataTable SeleccionarPaises()
        {
            return ObjConfig.SeleccionarPaises();
        }

        /// <summary>
        /// Selecciona las Directoras de ventas para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable SeleccionarDVentasPorEvaluar(string prefijoIsoPais)
        {
            return ObjConfig.SeleccionarDVentasPorEvaluar(prefijoIsoPais);
        }

        /// <summary>
        /// Selecciona las Gerente de region para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable SeleccionarGRegionPorEvaluar(string prefijoIsoPais)
        {
            return ObjConfig.SeleccionarGRegionPorEvaluar(prefijoIsoPais);
        }

        public DataTable ObtenerEvaluadores(string prefijoIsoPais, int rol)
        {
            return ObjConfig.ObtenerEvaluadores(prefijoIsoPais, rol);
        }

        public string ObtenerDocIdentidadMae(string prefijoIsoPais, int rol, string docIdentidad)
        {
            return ObjConfig.ObtenerDocIdentidadMae(prefijoIsoPais, rol, docIdentidad);
        }

        /// <summary>
        /// Selecciona las Gerente de zona para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="idGerenteRegion"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <returns></returns>
        public DataTable SeleccionarGZonaPorEvaluar(int idGerenteRegion, string prefijoIsoPais)
        {
            return ObjConfig.SeleccionarGZonaPorEvaluar(idGerenteRegion, prefijoIsoPais);
        }

        public DataTable SeleccionarGZonaPorPais(string prefijoIsoPais)
        {
            return ObjConfig.SeleccionarGZonaPorPais(prefijoIsoPais);
        }

        /// <summary>
        /// Inserta el registro del usuario a ser evaluado
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="documentoIdentidad"></param>
        /// <param name="codigoRol"></param>
        /// <param name="enviado"></param>
        public void InsertarInicioDialogo(string prefijoIsoPais, string periodo, string documentoIdentidad, int codigoRol, byte enviado)
        {
            ObjConfig.InsertarInicioDialogo(prefijoIsoPais, periodo, documentoIdentidad, codigoRol, enviado);
        }


        public void ActualizarInicioDialogo(string prefijoIsoPais, string periodo, string documentoIdentidad, int codigoRol, byte enviado)
        {
            ObjConfig.ActualizarInicioDialogo(prefijoIsoPais, periodo, documentoIdentidad, codigoRol, enviado);
        }

        /// <summary>
        /// Inserta el registro para iniciar el proceso de Dialogo
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="indicadorEvaluados"></param>
        /// <returns></returns>
        public bool InsertarConfiguracionProceso(string prefijoIsoPais, string periodo, DateTime fechaInicio, string indicadorEvaluados)
        {
            return ObjConfig.InsertarConfiguracionProceso(prefijoIsoPais, periodo, fechaInicio, indicadorEvaluados);
        }

        /// <summary>
        /// Obtiene el id del Inicio del proceso activo en un determinado Periodo
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="indicadorEvaluados"></param>
        /// <returns>el idProcesoInicio</returns>
        public int ValidarInicioProceso(string prefijoIsoPais, string periodo, string indicadorEvaluados)
        {
            return ObjConfig.ValidarInicioProceso(prefijoIsoPais, periodo, indicadorEvaluados);
        }

        public DataTable SeleccionarPeriodo(string prefijoIsoPais)
        {
            return ObjConfig.SeleccionarPeriodo(prefijoIsoPais);
        }

        /// <summary>
        /// Lista los paises que han sido procesados con sus respectivos periodos
        /// </summary>
        /// <returns></returns>
        public DataTable SeleccionarPaisesProcesados()
        {
            return ObjConfig.SeleccionarPaisesProcesados();
        }

        /// <summary>
        /// Retorna la lista con todos los directores de ventas
        /// </summary>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarDv()
        {
            return ObjConfig.SeleccionarDv();
        }

        /// <summary>
        /// Retorna la lista con los GR que han sido procesados por un DV en un determinado periodo
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarGRegionProcesadasPorDv(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo)
        {
            return ObjConfig.SeleccionarGRegionProcesadasPorDv(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo);
        }

        /// <summary>
        /// Selecciona todos los Gerente de Region
        /// </summary>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarGRegion()
        {
            return ObjConfig.SeleccionarGRegion();
        }

        /// <summary>
        /// Retorna la lista con los GZ que han sido procesados por las GR en un determinado periodo,País
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public List<BeUsuario> SeleccionarGZonaProcesadasPorGr(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo)
        {
            return ObjConfig.SeleccionarGZonaProcesadasPorGr(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo);
        }

        public List<BeUsuario> ObtenerGerentesVenta(string prefijoIsoPais)
        {
            return ObjConfig.ObtenerGerentesVenta(prefijoIsoPais);
        }

        public List<BeUsuario> ObtenerGerentesZona(string codGerenteRegion, string prefijoIsoPais)
        {
            return ObjConfig.ObtenerGerentesZona(codGerenteRegion, prefijoIsoPais);
        }

        public List<BeUsuario> ObtenerLideres(string codGerenteZona, string prefijoIsoPais)
        {
            return ObjConfig.ObtenerLideres(codGerenteZona, prefijoIsoPais);
        }

        public DataTable ObtenerCorreosPlanesAcordados(string prefijoIsoPais, string periodo, int idRol, string estadoProceso)
        {
            return ObjConfig.ObtenerCorreosPlanesAcordados(prefijoIsoPais, periodo, idRol, estadoProceso);
        }

        public void InsertarLogEnvioCorreo(string tipoCorreo, string correo, string descripcion)
        {
            ObjConfig.InsertarLogEnvioCorreo(tipoCorreo, correo, descripcion);
        }
    }
}