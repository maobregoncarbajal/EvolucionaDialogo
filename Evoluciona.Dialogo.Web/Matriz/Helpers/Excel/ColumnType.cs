
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Excel
{
    using System;
    using CarlosAg.ExcelXmlWriter;

    /// <summary>
    /// Creates a Column for CarlosAg.ExcelXmlWriter
    /// </summary>
    internal class ColumnType
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private DataType excelType;
        public DataType ExcelType
        {
            get { return excelType; }
            set { excelType = value; }
        }
        public WorksheetCell GetHeaderCell()
        {
            WorksheetCell head = new WorksheetCell(Name, DataType.String);
            head.StyleID = "HeaderStyle";
            return head;
        }
        private string getDataTypeFormatString()
        {
            if (ExcelType == DataType.DateTime)
            {
                return "s";
            }
            return null;
        }
        public WorksheetCell GetDataCell(object data)
        {
            WorksheetCell dc = new WorksheetCell();
            dc.Data.Type = ExcelType;
            if (ExcelType == DataType.DateTime && data is DateTime)
            {
                DateTime dt = (DateTime)data;
                dc.Data.Text = dt.ToString("s");
            }
            else
            {
                string dataString = data.ToString();
                if (dataString == null || dataString.Length == 0)
                {
                    dc.Data.Type = DataType.String;
                    dc.Data.Text = string.Empty;
                }
                else
                {
                    dc.Data.Text = dataString;
                }
            }
            return dc;
        }
    }
}
