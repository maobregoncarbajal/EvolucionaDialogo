
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.PDF
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PDFReporter
    {
        private readonly List<ReportRow> tablaResumen;
        private readonly List<ReportRow> tablaAnhoAnt;
        private readonly List<ReportRow> tablaAnhoActual;
        private readonly string fileName;
        private readonly string strPathImg;
        private readonly string titulo;
        private readonly string filtros;
        private readonly string anhoAct;
        private readonly string anhoAnt;

        public PDFReporter( string tituloReport, string filtroReport,string fileNameReport, List<ReportRow> tableResumen, List<ReportRow> tableAnhoAnt, List<ReportRow> tableAnhoActual, string strPathImage,string anhoActReport,string anhoAntReport)
        {
            this.tablaResumen = tableResumen;
            this.tablaAnhoAnt = tableAnhoAnt;
            this.tablaAnhoActual = tableAnhoActual;
            this.fileName = fileNameReport;
            this.strPathImg = strPathImage;
            this.titulo = tituloReport;
            this.filtros = filtroReport;
            this.anhoAct = anhoActReport;
            this.anhoAnt = anhoAntReport;
        }

        public void ExportPDF()
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");

            // step 1: creation of a document-object
            Document document = new Document(PageSize.A4, 10, 10, 90, 10);

            // step 2: we create a writer that listens to the document
            PdfWriter.GetInstance(document, Response.OutputStream);

            //set some header stuff
            document.AddTitle(fileName);
            document.AddSubject(String.Format("Table of {0}", fileName));
            document.AddCreator("Belcorp");
            document.AddAuthor("Belcorp");

            // step 3: we open the document
            document.Open();

            // step 4: we add content to the document
            CreatePages(document);

            // step 5: we close the document
            document.Close();
        }

        private void CreatePages(Document document)
        {
            document.NewPage();
            document.Add(FormatTitlePhrase(titulo + "\n\n",1));
            document.Add(FormatPageHeaderPhrase(filtros));
            document.Add(FormatTitlePhrase("\n\n",1));

            if (!string.IsNullOrEmpty(strPathImg))
            {
                Image jpg = Image.GetInstance(new Uri(strPathImg));
                jpg.ScaleToFit(150f, 150f);
                jpg.Alignment = Image.ALIGN_RIGHT;
                jpg.SetAbsolutePosition(document.PageSize.Width - 190f, document.PageSize.Height - 286.6f);
                document.Add(jpg);
            }

            if (tablaResumen.Count > 0)
            {
                document.Add(FormatTitlePhrase("Ranking \n\n",3));
                document.Add(CreateTable(tablaResumen, 60));
                document.Add(FormatTitlePhrase("\n\n",1));
            }

            document.Add(FormatTitlePhrase("Campañas del año " + anhoAnt + "\n\n", 3));
            document.Add(FormatTitlePhrase("\n\n", 1));
           
            if (tablaAnhoAnt.Count > 0)
            {

                document.Add(CreateTable(tablaAnhoAnt, 100));
                document.Add(FormatTitlePhrase("\n\n",1));
            }
            else
            {
                document.Add(FormatTitlePhrase("No existen datos en este año", 3));
                document.Add(FormatTitlePhrase("\n\n", 1));
            }


            document.Add(FormatTitlePhrase("Campañas del año " + anhoAct + "\n\n", 3));
            document.Add(FormatTitlePhrase("\n\n", 1));
           
            if (tablaAnhoActual.Count > 0)
            {    
                document.Add(CreateTable(tablaAnhoActual, 100));
            }
            else
            {
                document.Add(FormatTitlePhrase("No existen datos en este año", 3));
                document.Add(FormatTitlePhrase("\n\n", 1));
            }
        }

        private PdfPTable CreateTable( List<ReportRow> rows, int percentage)
        {
            int cant = rows[rows.Count -1].Cells.Count + 1;
            PdfPTable pdfTable = new PdfPTable(cant);
            System.Drawing.Color myColor = new System.Drawing.Color();
           
            foreach (ReportRow row in rows)
            {
                PdfPCell cellCurr = new PdfPCell();
               
                if (row.RowName != "X")
                {
                     cellCurr = new PdfPCell(FormatPhrase(row.RowName,row.FontColor));
                     cellCurr.HorizontalAlignment = Element.ALIGN_CENTER;
                     cellCurr.VerticalAlignment = Element.ALIGN_MIDDLE;

                     if (!string.IsNullOrEmpty(row.BackColor))
                     {
                         myColor = System.Drawing.ColorTranslator.FromHtml(row.BackColor);

                         cellCurr.BackgroundColor = new BaseColor(myColor.R, myColor.G, myColor.B);
                     }

                    if (row.MergeCol != 0)
                        cellCurr.Colspan = row.MergeCol;

                    if (row.MergeRow != 0)
                        cellCurr.Rowspan = row.MergeRow;

                    pdfTable.AddCell(cellCurr);
                }

                foreach (beCellMatriz cell in row.Cells)
                {
                    cellCurr = new PdfPCell(FormatPhrase(cell.Descripcion,cell.FontColor));
                   
                    if (!string.IsNullOrEmpty(cell.BackColor))
                    {
                        myColor = System.Drawing.ColorTranslator.FromHtml(cell.BackColor);

                        cellCurr.BackgroundColor = new BaseColor(myColor.R, myColor.G, myColor.B);
                    }
 
                    if (cell.MergeCol != 0)
                        cellCurr.Colspan = cell.MergeCol;

                    if (cell.MergeRow != 0)
                        cellCurr.Rowspan = cell.MergeRow;

                    cellCurr.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellCurr.VerticalAlignment = Element.ALIGN_MIDDLE;

                    pdfTable.AddCell(cellCurr);
                }
            }

            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage =  percentage; // percentage
            pdfTable.DefaultCell.BorderWidth = 2;
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            return pdfTable;

        }

        private static Phrase FormatPageHeaderPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES, 10, Font.BOLD, new BaseColor(0, 0, 0)));
        }

        private static Paragraph FormatTitlePhrase(string value,int align)
        {
            Paragraph paragraph = new Paragraph(value, FontFactory.GetFont(FontFactory.TIMES, 12, Font.BOLD, new BaseColor(0, 0, 0)));
            paragraph.Alignment = align; //Center
            return paragraph;
        }

        private Phrase FormatPhrase(string value, string fontColor)
        {

            if (string.IsNullOrEmpty(fontColor))
            {
                fontColor = "#000000";
            }
            System.Drawing.Color myColor = System.Drawing.ColorTranslator.FromHtml(fontColor);

            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES, 8, new BaseColor(myColor.R, myColor.G, myColor.B)));
        }
    }
}
