
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Excel
{
    using BusinessEntity;
    using CarlosAg.ExcelXmlWriter;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ExcelDatasetWriter
    {
        public Workbook CreateWorkbook(DataSet data)
        {
            //ensure valid data
            if (data == null)
            {
                throw new ArgumentNullException("data", "Data cannot be null.");
            }
            ensureTables(data);
            //Variable declarations
            //our workbook
            Workbook wb = new Workbook();

            //styles
            defineStyle(ref wb);

            //Our worksheet container
            //Our DataTableWriter
            ExcelDataTableWriter edtw = new ExcelDataTableWriter();
            //Our sheet name
            //Our counter
            int tCnt = 0;
            //Loop through datatables and create worksheets
            foreach (DataTable dt in data.Tables)
            {
                //set the name of the worksheet
                string wsName;
                if (!string.IsNullOrEmpty(dt.TableName) && dt.TableName != "Table")
                {
                    wsName = dt.TableName;
                }
                else
                {
                    //Go to generic Sheet1 . . . SheetN
                    wsName = "Sheet" + (tCnt + 1).ToString();
                }
                //Instantiate the worksheet
                Worksheet ws = wb.Worksheets.Add(wsName);
                //Populate the worksheet
                edtw.PopulateWorksheet(dt, ws, dt.TableName, dt.Namespace);

                tCnt++;
            }
            return wb;
        }

        public Workbook CreateWorkbookMatriz(string formato, string[] data, List<BeComun> niveles, List<BeComun> tamVentas, string tipoColaborador, string filtros)
        {
            Workbook wb = new Workbook();
            int tCnt = 0;

            defineStyle(ref wb);

            ExcelDataTableWriter edtw = new ExcelDataTableWriter();
            Worksheet ws = wb.Worksheets.Add("Matriz consolidada");

            edtw.hojaMatrizConsolidada01(ws, formato, data, niveles, tamVentas, tipoColaborador, filtros);
            tCnt++;

            return wb;
        }

        private void defineStyle(ref Workbook m_wBook)
        {

            WorksheetStyleCollection style = m_wBook.Styles;
            
            WorksheetStyle titleStyle = style.Add("Title");
            titleStyle.Font.FontName = "Tahoma";
            titleStyle.Font.Size = 10;
            titleStyle.Font.Bold = true;
            titleStyle.Alignment.Vertical = StyleVerticalAlignment.Top;
            titleStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            titleStyle.Alignment.WrapText = true;
            

            // -----------------------------------------------
            //  HeaderStyle
            // -----------------------------------------------
            WorksheetStyle headerStyle = style.Add("HeaderStyle");
            headerStyle.Font.FontName = "Tahoma";
            headerStyle.Font.Size = 10;
            headerStyle.Font.Bold = true;
            headerStyle.Alignment.Vertical = StyleVerticalAlignment.Top;
            headerStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            headerStyle.Alignment.WrapText = true;
            headerStyle.Font.Color = "White";
            headerStyle.Interior.Color = "#5E1E6F";
            headerStyle.Interior.Pattern = StyleInteriorPattern.DiagCross;

            // -----------------------------------------------
            //  LastRowStyle
            // -----------------------------------------------
            
            WorksheetStyle lastRowStyle = m_wBook.Styles.Add("LastSpRow");
            lastRowStyle.Font.FontName = "Tahoma";
            lastRowStyle.Font.Size = 10;
            lastRowStyle.Alignment.Vertical = StyleVerticalAlignment.Center;
            lastRowStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            lastRowStyle.Alignment.WrapText = true;
            lastRowStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            lastRowStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            lastRowStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            lastRowStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            
            //_____________________________________________________________

            // Matriz consolidada - Formato 01
            WorksheetStyle cabeceraPrincipal01 = style.Add("cabeceraPrincipal01");
            cabeceraPrincipal01.Font.FontName = "Tahoma";
            cabeceraPrincipal01.Font.Size = 10;
            cabeceraPrincipal01.Font.Bold = true;
            cabeceraPrincipal01.Alignment.Vertical = StyleVerticalAlignment.Center;
            cabeceraPrincipal01.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            cabeceraPrincipal01.Alignment.WrapText = true;
            cabeceraPrincipal01.Font.Color = "White";
            cabeceraPrincipal01.Interior.Color = "#FF4646";
            cabeceraPrincipal01.Interior.Pattern = StyleInteriorPattern.Solid;
            cabeceraPrincipal01.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            cabeceraPrincipal01.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            cabeceraPrincipal01.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            cabeceraPrincipal01.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            
            WorksheetStyle cabeceraPrincipal02 = style.Add("cabeceraPrincipal02");
            cabeceraPrincipal02.Font.FontName = "Tahoma";
            cabeceraPrincipal02.Font.Size = 10;
            cabeceraPrincipal02.Font.Bold = true;
            cabeceraPrincipal02.Alignment.Vertical = StyleVerticalAlignment.Center;
            cabeceraPrincipal02.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            cabeceraPrincipal02.Alignment.WrapText = true;
            cabeceraPrincipal02.Font.Color = "black";
            cabeceraPrincipal02.Interior.Color = "#FFFF66";
            cabeceraPrincipal02.Interior.Pattern = StyleInteriorPattern.Solid;
            cabeceraPrincipal02.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            cabeceraPrincipal02.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            cabeceraPrincipal02.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            cabeceraPrincipal02.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

            WorksheetStyle cabeceraPrincipal03 = style.Add("cabeceraPrincipal03");
            cabeceraPrincipal03.Font.FontName = "Tahoma";
            cabeceraPrincipal03.Font.Size = 10;
            cabeceraPrincipal03.Font.Bold = true;
            cabeceraPrincipal03.Alignment.Vertical = StyleVerticalAlignment.Center;
            cabeceraPrincipal03.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            cabeceraPrincipal03.Alignment.WrapText = true;
            cabeceraPrincipal03.Font.Color = "black";
            cabeceraPrincipal03.Interior.Color = "#51D739";
            cabeceraPrincipal03.Interior.Pattern = StyleInteriorPattern.Solid;
            cabeceraPrincipal03.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            cabeceraPrincipal03.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            cabeceraPrincipal03.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            cabeceraPrincipal03.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            
            WorksheetStyle cabeceraSecundaria = style.Add("cabeceraSecundaria");
            cabeceraSecundaria.Font.FontName = "Tahoma";
            cabeceraSecundaria.Font.Size = 8;
            cabeceraSecundaria.Font.Bold = true;
            cabeceraSecundaria.Alignment.Vertical = StyleVerticalAlignment.Center;
            cabeceraSecundaria.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            cabeceraSecundaria.Alignment.WrapText = true;
            cabeceraSecundaria.Font.Color = "black";
            cabeceraSecundaria.Interior.Color = "#E9ECED";
            cabeceraSecundaria.Interior.Pattern = StyleInteriorPattern.Solid;
            cabeceraSecundaria.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            cabeceraSecundaria.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            cabeceraSecundaria.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            cabeceraSecundaria.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            
            WorksheetStyle resaltado01 = style.Add("resaltado01");
            resaltado01.Font.FontName = "Tahoma";
            resaltado01.Font.Size = 10;
            resaltado01.Font.Bold = true;
            resaltado01.Alignment.Vertical = StyleVerticalAlignment.Center;
            resaltado01.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            resaltado01.Alignment.WrapText = true;
            resaltado01.Font.Color = "White";
            resaltado01.Interior.Color = "#0F243E";
            resaltado01.Interior.Pattern = StyleInteriorPattern.Solid;
            resaltado01.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            resaltado01.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            resaltado01.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            resaltado01.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

            WorksheetStyle resaltado02 = style.Add("resaltado02");
            resaltado02.Font.FontName = "Tahoma";
            resaltado02.Font.Size = 10;
            resaltado02.Font.Bold = true;
            resaltado02.Alignment.Vertical = StyleVerticalAlignment.Center;
            resaltado02.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            resaltado02.Alignment.WrapText = true;
            resaltado02.Font.Color = "#FFFFFF";
            resaltado02.Interior.Color = "#8DB4E2";
            resaltado02.Interior.Pattern = StyleInteriorPattern.Solid;
            resaltado02.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            resaltado02.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            resaltado02.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            resaltado02.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

            WorksheetStyle data = style.Add("data");
            data.Font.FontName = "Tahoma";
            data.Font.Size = 10;
            data.Alignment.Vertical = StyleVerticalAlignment.Center;
            data.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            data.Alignment.WrapText = true;
            data.Font.Color = "#0066CC";
            data.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            data.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            data.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            data.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

            WorksheetStyle resultados = style.Add("resultados");
            resultados.Font.FontName = "Tahoma";
            resultados.Font.Size = 10;
            resultados.Font.Bold = true;
            resultados.Alignment.Vertical = StyleVerticalAlignment.Center;
            resultados.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            resultados.Alignment.WrapText = true;
            resultados.Font.Color = "#0066CC";
            resultados.Interior.Color = "#C8C8C8";
            resultados.Interior.Pattern = StyleInteriorPattern.Solid;
            resultados.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            resultados.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
            resultados.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
            resultados.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
            //_____________________________________________________________
        }

        private void ensureTables(DataSet data)
        {
            if (data.Tables.Count == 0)
            {
                throw new ArgumentOutOfRangeException("data", "DataSet does not contain any tables.");
            }
        }
    }
}
