
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeRetroalimentacion
    {
        public int idRetroalimentacion { get; set; }
        public int idProceso { get; set; }
        public int CodigoPlanAnual { get; set; }
        public int idPreguntaRetroalimentacion { get; set; }
        public string respuesta { get; set; }
        //auditoria
        public int idUsuario { get; set; }
        public bool PostDialogo { get; set; }
    }
}
