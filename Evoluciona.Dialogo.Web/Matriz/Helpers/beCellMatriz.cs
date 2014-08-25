
namespace Evoluciona.Dialogo.Web.Matriz.Helpers
{
    public class beCellMatriz
    {
        private string _descripcion;
        private string _fontColor;
        private string _backColor;
        private int _mergeRow;
        private int _mergeCol;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string FontColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }

        public string BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        public int MergeRow
        {
            get { return _mergeRow; }
            set { _mergeRow = value; }
        }

        public int MergeCol
        {
            get { return _mergeCol; }
            set { _mergeCol = value; }
        }
    }
}
