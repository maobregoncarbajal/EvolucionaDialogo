
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Excel
{
    using System.Collections.Generic;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;

    public static class ExcelReporter
    {

        public static void CreateText(ref HSSFWorkbook hssfworkbook, ref ISheet sheet, string text, int x, int y,string align)
        {
            //create a cell style
            ICellStyle styleText = hssfworkbook.CreateCellStyle();
         
            if(align == "L")
            styleText.Alignment = HorizontalAlignment.LEFT;

            if (align == "C")
                styleText.Alignment = HorizontalAlignment.CENTER;

            styleText.VerticalAlignment = VerticalAlignment.CENTER;
     
            IRow rowExcel = sheet.CreateRow(y);

            ICell cellLeftExcel = rowExcel.CreateCell(x);
            cellLeftExcel.SetCellValue(text);

            CellRangeAddress region = new CellRangeAddress(y, y, x, x+15);
            sheet.AddMergedRegion(region);

            IFont font = hssfworkbook.CreateFont();
            font.Boldweight = 20;
            font.IsItalic = true;
            font.FontHeightInPoints = 13;
            styleText.SetFont(font);
            cellLeftExcel.CellStyle = styleText;
        }

        public static void CreateTable(ref HSSFWorkbook hssfworkbook,ref ISheet sheet, List<ReportRow> rows, int x, int y)
        {
            int indexY = y;
           
            foreach (ReportRow row in rows)
            {
                //create a cell style
                ICellStyle styleHeader = hssfworkbook.CreateCellStyle();
                styleHeader.Alignment = HorizontalAlignment.CENTER;
                styleHeader.VerticalAlignment = VerticalAlignment.CENTER;
                styleHeader.BorderBottom = BorderStyle.THIN;
                styleHeader.BorderLeft = BorderStyle.THIN;
                styleHeader.BorderRight = BorderStyle.THIN;
                styleHeader.BorderTop = BorderStyle.THIN;
                styleHeader.BottomBorderColor = HSSFColor.BLACK.index;
                styleHeader.LeftBorderColor = HSSFColor.BLACK.index;
                styleHeader.RightBorderColor = HSSFColor.BLACK.index;
                styleHeader.TopBorderColor = HSSFColor.BLACK.index;

                IFont font = hssfworkbook.CreateFont();

                int indexX = x;

                IRow rowExcel = sheet.CreateRow(indexY);

                ICell cellLeftExcel = rowExcel.CreateCell(indexX);
                cellLeftExcel.SetCellValue(row.RowName);

                HSSFPalette palette = hssfworkbook.GetCustomPalette();
                System.Drawing.Color myColor;
                
                if (!string.IsNullOrEmpty(row.BackColor))
                {              
                    myColor = System.Drawing.ColorTranslator.FromHtml(row.BackColor);
                    styleHeader.FillForegroundColor = palette.FindSimilarColor(myColor.R,myColor.G, myColor.B ).GetIndex();
                    styleHeader.FillPattern = FillPatternType.SOLID_FOREGROUND;
                }

                if (!string.IsNullOrEmpty(row.FontColor))
                {
                    palette = hssfworkbook.GetCustomPalette();
                    myColor = System.Drawing.ColorTranslator.FromHtml(row.FontColor);
                    font.Color = palette.FindSimilarColor(myColor.R, myColor.G, myColor.B).GetIndex();
                    styleHeader.SetFont(font);
                }

                cellLeftExcel.CellStyle = styleHeader;

                if (row.MergeRow != 0)
                {
                    CellRangeAddress region = new CellRangeAddress(indexY, indexY + row.MergeRow - 1, indexX, indexX);
                    sheet.AddMergedRegion(region);
                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, BorderStyle.THIN, HSSFColor.BLACK.index);
                }

                foreach (beCellMatriz cell in row.Cells)
                {
                    //create a cell style
                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.CENTER;
                    style.VerticalAlignment = VerticalAlignment.CENTER;
                    style.BorderBottom = BorderStyle.THIN;
                    style.BorderLeft = BorderStyle.THIN;
                    style.BorderRight = BorderStyle.THIN;
                    style.BorderTop = BorderStyle.THIN;
                    style.BottomBorderColor = HSSFColor.BLACK.index;
                    style.LeftBorderColor = HSSFColor.BLACK.index;
                    style.RightBorderColor = HSSFColor.BLACK.index;
                    style.TopBorderColor = HSSFColor.BLACK.index;
                  
                    indexX++;

                    ICell cellExcel = rowExcel.CreateCell(indexX);
                    cellExcel.SetCellValue(cell.Descripcion);

                    if (!string.IsNullOrEmpty(cell.BackColor))
                    {
                        myColor = System.Drawing.ColorTranslator.FromHtml(cell.BackColor);
                        style.FillForegroundColor = palette.FindSimilarColor(myColor.R, myColor.G, myColor.B).GetIndex();
                        style.FillPattern = FillPatternType.SOLID_FOREGROUND;
                    }

                    if (!string.IsNullOrEmpty(cell.FontColor))
                    {
                        palette = hssfworkbook.GetCustomPalette();
                        myColor = System.Drawing.ColorTranslator.FromHtml(cell.FontColor);
                        font.Color = palette.FindSimilarColor(myColor.R, myColor.G, myColor.B).GetIndex();
                        style.SetFont(font);
                    }

                    cellExcel.CellStyle = style;
                    
                    if (cell.MergeCol == 0) continue;
                    CellRangeAddress region = new CellRangeAddress(indexY, indexY, indexX, indexX + cell.MergeCol - 1);
                    sheet.AddMergedRegion(region);
                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, BorderStyle.THIN, HSSFColor.BLACK.index);
                    indexX = indexX - 1 + cell.MergeCol;
                   
                }
             
                indexY++;
            }
        }
    }
}
