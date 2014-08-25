
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Chart
{
    using System;
    using System.IO;
    using System.Web.Caching;

    public class ChartImageDestructor
    {
        String fileName;
        public ChartImageDestructor(String fileName)
        {
            this.fileName = fileName;
        }

        public void RemovedCallback(String k, Object v, CacheItemRemovedReason r)
        {
            try
            {
                File.Delete(fileName);
            }
            catch
            {
            }
        }
    }
}
