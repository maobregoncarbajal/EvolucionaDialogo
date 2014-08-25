
namespace Evoluciona.Dialogo.BusinessLogic
{
    using DataAccess;
    using System.Data;

    public class BlPreguntaAcuerdos
    {
        private static readonly DaPreguntaAcuerdo DaPreguntaAcuerdo = new DaPreguntaAcuerdo();

        public DataTable ObtenerPreguntaAcuerdo(string connstring)
        {
            return DaPreguntaAcuerdo.ObtenerPreguntaAcuerdo(connstring);
        }
    }
}
