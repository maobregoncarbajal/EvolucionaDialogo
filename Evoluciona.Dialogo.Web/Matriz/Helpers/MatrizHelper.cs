
namespace Evoluciona.Dialogo.Web.Matriz.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class MatrizHelper
    {
        #region Metodos Generales
        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public static double CalculateAvarege( IEnumerable<double> array)
        {
            double total = 0;
            double cantidad = 0;
            double resultado;

            foreach (double d in array)
            {
                total = total + d;
                cantidad ++;
            }

            if (cantidad != 0)
            {
                resultado = total/cantidad;
            }
            else
            {
                resultado = 0;
            }
           
            return Math.Round(resultado,2);
            
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (IOException)
                {
                }
            }
        }

        #endregion
    }
}
