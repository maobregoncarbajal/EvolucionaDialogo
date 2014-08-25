
namespace Evoluciona.Dialogo.Web.Matriz.Helpers
{
    public class bePeriodoResultado
    {
        private string _anho;
        private string _descripcion;
        private bool _existe;
        private bool _activo;

        public string Anho
        {
            get { return _anho; }
            set { _anho = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public bool Existe
        {
            get { return _existe; }
            set { _existe = value; }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }
}
