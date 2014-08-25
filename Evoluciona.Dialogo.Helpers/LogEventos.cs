
namespace Evoluciona.Dialogo.Helpers
{
    using System;
    using System.IO;

    public class LogEventos
    {
        public void EscribirLog(string ruta, string descripcionLog)
        {
            var info = new FileInfo(ruta);
            //FileStream stream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            var escribir = info.AppendText();//( info.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //escribir.ap
            //StreamWriter escribir = new StreamWriter(ruta, false, System.Text.Encoding.UTF8);

            escribir.WriteLine(descripcionLog);
            escribir.Close();
        }

        /// <summary>
        /// Descripción:    Metodo que valida si existe un archivo en un directorio.
        /// Autor:          Daniel Huamán
        /// Fecha:          31/10/2012 
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <returns></returns>
        public bool ValidFileExist(string rutaArchivo)
        {
            try
            {
                return File.Exists(rutaArchivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Descripción:    Metodo que elimina un archivo de un directorio.
        /// Autor:          Daniel Huamán
        /// Fecha:          31/10/2012 
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <returns></returns>
        public bool DeleteFile(string rutaArchivo)
        {
            try
            {
                File.Delete(rutaArchivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

    }
}