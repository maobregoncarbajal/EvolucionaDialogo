
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeAcuerdo
    {
        public int IDAcuerdo { get; set; }
        public int IDVisita { get; set; }
        public int IDPregunta { get; set; }
        public string Respuesta { get; set; }
        public bool PostVisita { get; set; }
        public int idUsuario { get; set; }
    }
}
