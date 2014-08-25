
namespace Evoluciona.Dialogo.Web.Matriz.Helpers
{
    using System.Collections.Generic;

    public class ReportRow
    {

        private string _rowName;
        private List<beCellMatriz> _cells;
        private string _fontColor;
        private string _backColor;
        private int _mergeRow;
        private int _mergeCol;

        public string RowName
        {
            get { return _rowName; }
            set { _rowName = value; }
        }

        public List<beCellMatriz> Cells
        {
            get { return _cells; }
            set { _cells = value; }
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
