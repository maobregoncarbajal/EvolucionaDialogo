
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BlDataMart
    {

        private static readonly DaDataMart DaDataMart = new DaDataMart();

        public void ObtenerDataMart(List<BePais> listaPaises)
        {
            try
            {
                //EscribirLog(rutaServidor, "Inicio de proceso Obtener Datos de Datamart");
                InsertarLogCarga("obtenerDataMart", "Inicio de proceso Obtener Datos de Datamart");
                DaDataMart.ObtenerDataMart(listaPaises);
                InsertarLogCarga("obtenerDataMart", "Fin de proceso Obtener Datos de Datamart");
            }
            catch (Exception ex)
            {
                EscribirLog("obtenerDataMart", "Error Obtener Datos de Datamart-" + ex.Message);
            }
        }

        public string InsertarTablasTRX()
        {
            InsertarLogCarga("insertarTablasTRX", "Inico de Procesos cargar datos a las tablas TRX");

            var resultado = DaDataMart.InsertarEnTablaTRX_GR();
            InsertarLogCarga("InsertarEnTablaTRX_GR", resultado);

            resultado = resultado + DaDataMart.InsertarEnTablaTRXDetalle_GR();
            InsertarLogCarga("InsertarEnTablaTRXDetalle_GR", resultado);

            resultado = resultado + DaDataMart.InsertarEnTablaTRX_GZ();
            InsertarLogCarga("InsertarEnTablaTRX_GZ", resultado);

            resultado = resultado + DaDataMart.InsertarEnTablaTRXDetalle_GZ();
            InsertarLogCarga("InsertarEnTablaTRXDetalle_GZ", resultado);

            resultado = resultado + DaDataMart.InsertarEnTablaTrxlet();
            InsertarLogCarga("InsertarEnTablaTRXLET", resultado);

            resultado = resultado + DaDataMart.ActualizarEstandarizacionCodigo();
            InsertarLogCarga("ActualizarEstandarizacionCodigo", "Fin de Procesos cargar datos a las tablas TRX");
            return resultado;

        }

        /// <summary>
        /// Retorna la lista de todos los países
        /// </summary>
        /// <returns></returns>
        public List<BePais> ObtenerListaPaises()
        {
            return DaDataMart.ObtenerListaPaises();
        }

        private static void EscribirLog(string ruta, string descripcionLog)
        {
            var info = new FileInfo(ruta);
            var escribir = info.AppendText();
            escribir.WriteLine(descripcionLog);
            escribir.Close();
        }

        /// <summary>
        /// Registra los logs para la carga de Informacion
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="descripcion"></param>
        public void InsertarLogCarga(string seccion, string descripcion)
        {
            DaDataMart.InsertarLogCarga(seccion, descripcion);
        }
    }
}
