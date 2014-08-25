
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Data;

    public class BlIteraccionVisita
    {

        private static readonly DaIteraccionVisita DaIteraccionVisita = new DaIteraccionVisita();
        // Obtener  Preguntas 
        public DataTable ObtenerPreguntas(string connstring, int idProceso)
        {
            return DaIteraccionVisita.ObtenerPreguntas(connstring, idProceso);
        }

        // insertar Iteraccion Visita
        public bool IngresarIteraccionVisita(string connstring, BeIteraccionVisita beIteraccionVisita)
        {
            return DaIteraccionVisita.IngresarIteraccionVisita(connstring, beIteraccionVisita);
        }

        // Obtener  Preguntas Grabadas
        public DataTable ObtenerPreguntasGrabadas(string connstring, int idVisita)
        {
            return DaIteraccionVisita.ObtenerPreguntasGrabadas(connstring, idVisita);
        }
    }
}
