
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlResumenProceso
    {
        private static readonly DaResumenProceso DaResumenProceso = new DaResumenProceso();

        /// <summary>
        /// Obtiene el resumen del proceso
        /// </summary>
        /// <returns></returns>
        public BeResumenProceso ObtenerResumenProcesoByUsuario(string codigoUsuarioEvaluado, int idRol, string periodo, string prefijoIsoPais, string tipoDialogoDesempenio)
        {
            return DaResumenProceso.ObtenerResumenProcesoByUsuario(codigoUsuarioEvaluado, idRol, periodo, prefijoIsoPais, tipoDialogoDesempenio);
        }

        public BeResumenProceso ObtenerResumenProcesoByUsuarioPlanDeMejora(string codigoUsuarioEvaluado, int idRol, string periodo, string prefijoIsoPais)
        {
            return DaResumenProceso.ObtenerResumenProcesoByUsuarioPlanDeMejora(codigoUsuarioEvaluado, idRol, periodo, prefijoIsoPais);
        }

        public DataTable BuscarGRegionParaInicioDialogo(string codigoGRegion, string docuIdentidad, string nombres, string prefijoIsoPais, string periodo, int codigoRol)
        {
            return DaResumenProceso.BuscarGRegionParaInicioDialogo(codigoGRegion, docuIdentidad, nombres, prefijoIsoPais, periodo, codigoRol);
        }

        public DataTable BuscarGZonaParaInicioDialogo(string docuIdentidadGRegion, string docuIdentidad, string codGerenteZona, string nombres, string prefijoIsoPais, string periodo, int codigoRol)
        {
            return DaResumenProceso.BuscarGZonaParaInicioDialogo(docuIdentidadGRegion, docuIdentidad, codGerenteZona, nombres, prefijoIsoPais, periodo, codigoRol);
        }

        public DataTable BuscarResumenProcesoGr(string docuIdentidad, string nombres, string codGerenteRegion, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.BuscarResumenProcesoGr(docuIdentidad, nombres, codGerenteRegion, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        public DataTable BuscarResumenProcesoGz(string docuIdentidad, string nombres, string codGerenteZona, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.BuscarResumenProcesoGz(docuIdentidad, nombres, codGerenteZona, codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        /// <summary>
        /// Selecciona los GR que estan listos para iniciar el dialogo
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="codigoRol"></param>
        /// <param name="codigoDv"></param>
        /// <returns></returns>
        public DataTable SeleccionarGRegionParaInicioDialogo(string prefijoIsoPais, string periodo, int codigoRol, string codigoDv)
        {
            return DaResumenProceso.SeleccionarGRegionParaInicioDialogo(prefijoIsoPais, periodo, codigoRol, codigoDv);
        }

        public DataTable SeleccionarGRegionParaInicioDialogoPlanDeMejora(string prefijoIsoPais, string periodo, int codigoRol, string codigoDv)
        {
            return DaResumenProceso.SeleccionarGRegionParaInicioDialogoPlanDeMejora(prefijoIsoPais, periodo, codigoRol, codigoDv);
        }

        /// <summary>
        /// Selecciona los GZ que estan listos para iniciar el dialogo
        /// </summary>
        /// <param name="codigoGRegion"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="codigoRol"></param>
        /// <returns></returns>
        public DataTable SeleccionarGZonaParaInicioDialogo(string codigoGRegion, string prefijoIsoPais, string periodo, int codigoRol)
        {
            return DaResumenProceso.SeleccionarGZonaParaInicioDialogo(codigoGRegion, prefijoIsoPais, periodo, codigoRol);
        }

        public DataTable SeleccionarGZonaParaInicioDialogoPlanDeMejora(string codigoGRegion, string prefijoIsoPais, string periodo, int codigoRol)
        {
            return DaResumenProceso.SeleccionarGZonaParaInicioDialogoPlanDeMejora(codigoGRegion, prefijoIsoPais, periodo, codigoRol);
        }


        public DataTable SeleccionarLideresParaInicioDialogo(string codigoGZona, string prefijoIsoPais, string periodo)
        {
            return DaResumenProceso.SeleccionarLideresParaInicioDialogo(codigoGZona, prefijoIsoPais, periodo);
        }

        /// <summary>
        /// Selecciona los procesos realizados por el usuario GR
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns>Lista con todos los procesos realizados</returns>
        public DataTable SeleccionarResumenProcesoGr(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoGr(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        public DataTable SeleccionarResumenProcesoGrPlanDeMejora(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoGrPlanDeMejora(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        public List<BeComun> SeleccionarResumenProcesoGrList(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoGrList(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        /// <summary>
        /// Selecciona los procesos realizados por el usuario GZ
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns>Lista con todos los procesos realizados</returns>
        public DataTable SeleccionarResumenProcesoGz(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoGz(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        public DataTable SeleccionarResumenProcesoGzPlanDeMejora(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoGzPlanDeMejora(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        public List<BeComun> SeleccionarResumenProcesoGzList(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoGzList(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        /// <summary>
        /// Selecciona los procesos realizados por el usuario Lider
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns>Lista con todos los procesos realizados</returns>
        public DataTable SeleccionarResumenProcesoLider(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            return DaResumenProceso.SeleccionarResumenProcesoLider(codigoUsuarioEvaluador, idRol, prefijoIsoPais, periodo, estadoProceso, estado);
        }

        /// <summary>
        /// Obtiene los datos del usuario GR a ser evaluado
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GR</returns>
        public BeResumenProceso ObtenerUsuarioGRegionEvaluado(string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenProceso.ObtenerUsuarioGRegionEvaluado(codigoUsuario, prefijoIsoPais, periodo, estado);
        }


        /// <summary>
        /// Obtiene los datos del usuario GR a ser evaluado
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GR</returns>
        public BeResumenProceso ObtenerUsuarioNuevaGRegionEvaluado(string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenProceso.ObtenerUsuarioNuevaGRegionEvaluado(codigoUsuario, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Obtiene los datos del usuario GZ a ser evaluado
        /// </summary>
        /// <param name="idUsuarioGRegion"></param>
        /// <param name="codigoUsuarioGRegion"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GZ</returns>
        public BeResumenProceso ObtenerUsuarioGZonaEvaluado(int idUsuarioGRegion, string codigoUsuarioGRegion, string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenProceso.ObtenerUsuarioGZonaEvaluado(idUsuarioGRegion, codigoUsuarioGRegion, codigoUsuario, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Obtiene los datos del usuario GZ a ser evaluado
        /// </summary>
        /// <param name="idUsuarioGRegion"></param>
        /// <param name="codigoUsuarioGRegion"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GZ</returns>
        public BeResumenProceso ObtenerUsuarioNuevaGZonaEvaluado(int idUsuarioGRegion, string codigoUsuarioGRegion, string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenProceso.ObtenerUsuarioNuevaGZonaEvaluado(idUsuarioGRegion, codigoUsuarioGRegion, codigoUsuario, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Obtiene los datos de la lider evaluada
        /// </summary>
        /// <param name="codigoUsuarioGerenteZona"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>Los datos de la lider</returns>
        public BeResumenProceso ObtenerUsuarioLiderEvaluado(string codigoUsuarioGerenteZona, string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            return DaResumenProceso.ObtenerUsuarioLiderEvaluado(codigoUsuarioGerenteZona, codigoUsuario, prefijoIsoPais, periodo, estado);
        }

        /// <summary>
        /// Inserta el resumen del proceso para el usuario evaluado
        /// </summary>
        /// <param name="objResumenBe"></param>
        /// <returns></returns>
        public bool InsertarProceso(BeResumenProceso objResumenBe)
        {
            return DaResumenProceso.InsertarProceso(objResumenBe);
        }

        public bool InsertarProcesoPlanDeMejora(BeResumenProceso objResumenBe)
        {
            return DaResumenProceso.InsertarProcesoPlanDeMejora(objResumenBe);
        }

        /// <summary>
        /// Actualiza el estado del proceso
        /// </summary>
        /// <param name="objResumenBe"></param>
        /// <returns></returns>
        public bool ActualizarProceso(BeResumenProceso objResumenBe)
        {
            return DaResumenProceso.ActualizarProceso(objResumenBe);
        }

        /// <summary>
        /// Obtiene el resumen del proceso por ID
        /// </summary>
        /// <param name="idProceso"></param>
        /// <returns></returns>
        public DataTable ObtenerResumenProcesoById(int idProceso)
        {
            return DaResumenProceso.ObtenerResumenProcesoById(idProceso);
        }
    }
}