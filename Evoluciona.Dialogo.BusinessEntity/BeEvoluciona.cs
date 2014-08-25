
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeEvoluciona
    {
        public int IDVisita { get; set; }
        public int IDPregunta { get; set; }
        public string Respuesta { get; set; }
        public bool Evoluciona { get; set; }
        public int idUsuario { get; set; }
    }
}
