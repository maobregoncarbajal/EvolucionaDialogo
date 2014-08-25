
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeEncuestaReporte
    {
        public int IdEncuestaRespuestaDialogo { get; set; }
        public string Periodo { get; set; }
        public string DesTipoEncuesta { get; set; }
        public string PrefijoIsoPais { get; set; }
        public string Rol { get; set; }
        public string CodigoUsuario { get; set; }
        public string Cub { get; set; }
        public string DesPreguntas { get; set; }
        public string Comentario { get; set; }
        public int ValorPuntaje { get; set; }
        public int PuntajeAcumulado { get; set; }
    }
}
