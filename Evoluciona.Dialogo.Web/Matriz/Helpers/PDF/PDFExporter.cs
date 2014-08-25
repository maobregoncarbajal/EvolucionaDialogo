
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.PDF
{
    using BusinessEntity;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using Font = iTextSharp.text.Font;

    public class PDFExporter
    {
        private readonly DataTable dataTable;
        private readonly string fileName;
        private readonly bool timeStamp;

        public PDFExporter(DataTable dataTable, string fileName, bool timeStamp)
        {
            this.dataTable = dataTable;
            this.fileName = timeStamp ? String.Format("{0}-{1}", fileName, GetTimeStamp(DateTime.Now)) : fileName;
            this.timeStamp = timeStamp;
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
            CreatePages(document, dataTable.TableName, dataTable.Namespace);

            // step 5: we close the document
            document.Close();
        }

        public void pdfMatriz(string strFilePath, string[] data, List<BeComun> niveles, List<BeComun> tamVentas, string tipoColaborador, string filtros)
        {
            StringBuilder strB = new StringBuilder();

            string cabeceraPrincipal01 = "align='center' color='White' bgcolor='#ff4646' height='25'";
            string cabeceraPrincipal02 = "align='center' bgcolor='#ffff66' height='25'";
            string cabeceraPrincipal03 = "align='center' bgcolor='#51d739' height='25'";
            string cabeceraSecundaria = "bgcolor='#E9ECED' height='30'";
            string resaltado01 = "align='center' bgcolor='#0F243E' color='White'";
            string resaltado02 = "align='center' bgcolor='#8DB4E2' color='#FFFFFF'";
            string resultados = "align='center' bgcolor='#C8C8C8' color='#0066CC' height='20' width='10'";
            string styleData = "color='#0066CC' align='center'";
            string tblMatrizConsolidada = "cellpadding='0' cellspacing='0' border='1'";
            string[] ranking = {"Crítica", "Estable", "Productiva"};
            string colSpan = "";

            if (tipoColaborador == "00") {
                colSpan = "5";
            }
            else if (tipoColaborador == "01") {
                colSpan = "1";
            }
            else if (tipoColaborador == "02") {
                colSpan = "4";
            }
            strB.Append("<html>");
            strB.Append("<head>");
            strB.Append("<title><b><u>Matriz consolidada</u></b><br /><b>" + filtros + "</b></title>");
            strB.Append("<br />.");
            strB.Append("</head>");

            strB.Append("<body>");
            strB.Append("<table id='tblMatrizConsolidada' " + tblMatrizConsolidada + ">");
            strB.Append("<tbody>");
            strB.Append("<tr>");
            
            strB.Append("<td " + resaltado01 + " rowSpan=2>Tamaño venta</td>");
            strB.Append("<td " + cabeceraPrincipal01 + " colSpan=" + colSpan + "><b>" + ranking[0] + "</b></td>");
            strB.Append("<td " + cabeceraPrincipal02 + " colSpan=" + colSpan + "><b>" + ranking[1] + "</b></td>");
            strB.Append("<td " + cabeceraPrincipal03 + " colSpan=" + colSpan + "><b>" + ranking[2] + "</b></td>");
            strB.Append("<td " + resaltado01 + " rowSpan=2>Total</td>");
          
            strB.Append("</tr>");
            
            strB.Append("<tr " + cabeceraSecundaria + ">");

            for (int x = 0; x < ranking.Length; x++)
            {
                if (tipoColaborador == "00" || tipoColaborador == "01")
                {
                    strB.Append("<td align='center'>Nuevas</td>");
                }

                if (tipoColaborador == "00" || tipoColaborador == "02")
                {
                    foreach (BeComun n in niveles)
                    {
                        n.Descripcion = n.Descripcion.Substring(0, 1).ToUpper() + n.Descripcion.Substring(1, n.Descripcion.Length - 1).ToLower();
                        strB.Append("<td align='center'>" + n.Descripcion + "</td>");
                    }
                    strB.Append("<td align='center'>Sin Medición</td>");
                }
            }
            strB.Append("</tr>");

            int u = 0;
            foreach (BeComun b in tamVentas)
            {
                b.Descripcion = b.Descripcion.Substring(0, 1).ToUpper() + b.Descripcion.Substring(1, b.Descripcion.Length - 1).ToLower();

                strB.Append("<tr " + styleData + ">");
                strB.Append("<td " + resaltado02 + "><b>" + b.Descripcion + "</b></td>");

                for (int x = 0; x < ranking.Length; x++)
                {
                    if (tipoColaborador == "00" || tipoColaborador == "01")
                    {
                        strB.Append("<td>" + data[u].ToString() + "</td>");
                        u += 1;
                    }
                    if (tipoColaborador == "00" || tipoColaborador == "02")
                    {
                        foreach (BeComun n in niveles)
                        {
                            strB.Append("<td>" + data[u].ToString() + "</td>");
                            u += 1;
                        }
                        strB.Append("<td>" + data[u].ToString() + "</td>");
                        u += 1;
                    }
                }
                strB.Append("<td " + resultados + ">" + data[u].ToString() + "</td></tr>");
                u += 1;
            }
            
            strB.Append("<tr " + resultados + ">");
            strB.Append("<td " + resaltado01 + ">Total</td>");

            for (int x = 0; x < ranking.Length; x++)
            {
                if (tipoColaborador == "00" || tipoColaborador == "01")
                {
                    strB.Append("<td>" + data[u].ToString() + "</td>");
                    u += 1;
                }
                if (tipoColaborador == "00" || tipoColaborador == "02")
                {
                    foreach (BeComun n in niveles)
                    {
                        strB.Append("<td>" + data[u].ToString() + "</td>");
                        u += 1;
                    }
                    strB.Append("<td>" + data[u].ToString() + "</td>");
                    u += 1;
                }
            }
            strB.Append("<td>" + data[u].ToString() + "</td></tr>");
            strB.Append("</tbody>");
            strB.Append("</table>");

            strB.Append("</body>");
            strB.Append("</html>");

            Document document = new Document(PageSize.A4.Rotate(),10, 10, 30, 30);

            FileStream stream = new FileStream(strFilePath, FileMode.Create);
            PdfWriter PDFWriter = PdfWriter.GetInstance(document, stream);

            document.Open();
            using (TextReader sReader = new StringReader(strB.ToString()))
            {
                List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                foreach (IElement elm in list)
                {
                    document.Add(elm);
                }
            }
            document.Close();
            stream.Close();
            PDFWriter.Close();
        }

        private void CreatePages(Document document, string titulo, string parrafoInicial)
        {
            document.NewPage();
            document.Add(FormatTitlePhrase(titulo + "\n\n"));
            document.Add(FormatPageHeaderPhrase(parrafoInicial));
            PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100; // percentage
            pdfTable.DefaultCell.BorderWidth = 2;
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            foreach (DataColumn column in dataTable.Columns)
            {
                PdfPCell cellCurr = new PdfPCell(FormatHeaderPhrase(column.ColumnName));
                cellCurr.BackgroundColor = new BaseColor(94, 30, 111);
                pdfTable.AddCell(cellCurr);
            }
            pdfTable.HeaderRows = 1;  // this is the end of the table header
            pdfTable.DefaultCell.BorderWidth = 1;

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (object cell in row.ItemArray)
                {
                    //assume toString produces valid output
                    pdfTable.AddCell(FormatPhrase(cell.ToString()));
                }
            }

            document.Add(pdfTable);
        }

        private static Phrase FormatPageHeaderPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES, 10, Font.BOLD, new BaseColor(0, 0, 0)));
        }

        private static Paragraph FormatTitlePhrase(string value)
        {
            Paragraph paragraph = new Paragraph(value, FontFactory.GetFont(FontFactory.TIMES, 12, Font.BOLD, new BaseColor(0, 0, 0)));
            paragraph.Alignment = 1; //Center
            return paragraph;
        }

        private static Phrase FormatHeaderPhrase(string value)
        {
            Phrase phrase = new Phrase(value, FontFactory.GetFont(FontFactory.TIMES, 8, Font.BOLD, BaseColor.WHITE));

            return phrase;
        }

        private Phrase FormatPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES, 8));
        }

        private string GetTimeStamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
