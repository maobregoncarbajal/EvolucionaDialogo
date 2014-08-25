
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Excel
{
    using CarlosAg.ExcelXmlWriter;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for ExcelCreator.
    /// </summary>
    public class ExcelCreator
    {

        // Excel variables
        private static Workbook m_wBook;
        private static Worksheet m_wSheet;
        private static string m_destinationDir;



        public static string destinationDir
        {
            get { return m_destinationDir; }
            set { m_destinationDir = value; }
        }

        public ExcelCreator()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        /// <summary>
        /// crea l'intestazione delle colonne del file excel
        /// </summary>
        private static void createHeader()
        {
            WorksheetCell cell;

            WorksheetRow row = m_wSheet.Table.Rows.Add();
            row.Cells.Add(new WorksheetCell("NOME", "HeaderStyle"));
            row.Cells.Add(new WorksheetCell("DESCRIZIONE", "HeaderStyle"));
            cell = row.Cells.Add("DATA CREAZIONE");
            cell.StyleID = "HeaderStyle";
            row.Cells.Add(new WorksheetCell("AUTORE", "HeaderStyle"));
            row.Cells.Add(new WorksheetCell("VERSIONE", "HeaderStyle"));
            cell = row.Cells.Add("PARAMETRI INPUT");
            cell.MergeAcross = 2;            // Merge two cells together
            cell.StyleID = "HeaderStyle";
            cell = row.Cells.Add("PARAMETRI OUTPUT");
            cell.MergeAcross = 2;            // Merge two cells together
            cell.StyleID = "HeaderStyle";
            row.Cells.Add(new WorksheetCell("FILE CHE LA RICHIAMA", "HeaderStyle"));
        }

        /// <summary>
        /// crea un nuovo file excel
        /// </summary>
        public static void NewFile()
        {

            m_wBook = new Workbook();

            // Specify which Sheet should be opened and the size of window by default
            m_wBook.ExcelWorkbook.ActiveSheetIndex = 1;
            m_wBook.ExcelWorkbook.WindowTopX = 100;
            m_wBook.ExcelWorkbook.WindowTopY = 200;
            m_wBook.ExcelWorkbook.WindowHeight = 7000;
            m_wBook.ExcelWorkbook.WindowWidth = 8000;

            // Some optional properties of the Document
            m_wBook.Properties.Author = "Stored Procedure DOC";
            m_wBook.Properties.Title = "Documentazione SP";
            m_wBook.Properties.Created = DateTime.Now;

            defineStyle(m_wBook.Styles);

            // create new Sheet
            m_wSheet = m_wBook.Worksheets.Add("dati");

            m_wSheet.Table.Columns.Add(new WorksheetColumn(248));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(200));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(65));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(70));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(70));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(64));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(70));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(200));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(64));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(70));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(200));
            m_wSheet.Table.Columns.Add(new WorksheetColumn(100));

            m_wSheet.Options.PageSetup.Layout.Orientation = CarlosAg.ExcelXmlWriter.Orientation.Landscape;


            createHeader();
        }


        /// <summary>
        /// Genera gli stili utilizzati per la generazione del file Excel
        /// </summary>
        /// <param name="style"></param>
        private static void defineStyle(WorksheetStyleCollection style)
        {


            //______________Generate Styles______________________________________

            // -----------------------------------------------
            //  Default
            // -----------------------------------------------
            WorksheetStyle DefaultStyle = style.Add("Default");
            DefaultStyle.Name = "Default";
            DefaultStyle.Font.FontName = "Tahoma";
            DefaultStyle.Font.Size = 10;
            DefaultStyle.Alignment.WrapText = true;
            DefaultStyle.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            DefaultStyle.Alignment.Vertical = StyleVerticalAlignment.Top;


            // -----------------------------------------------
            //  HeaderStyle
            // -----------------------------------------------
            WorksheetStyle HeaderStyle = style.Add("HeaderStyle");
            HeaderStyle.Font.FontName = "Tahoma";
            HeaderStyle.Font.Size = 10;
            HeaderStyle.Font.Bold = true;
            HeaderStyle.Alignment.Vertical = StyleVerticalAlignment.Top;
            HeaderStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            HeaderStyle.Alignment.WrapText = true;
            HeaderStyle.Font.Color = "White";
            HeaderStyle.Interior.Color = "Blue";
            HeaderStyle.Interior.Pattern = StyleInteriorPattern.DiagCross;

            // -----------------------------------------------
            //  LastRowStyle
            // -----------------------------------------------
            WorksheetStyle LastRowStyle = m_wBook.Styles.Add("LastSpRow");
            LastRowStyle.Font.FontName = "Tahoma";
            LastRowStyle.Font.Size = 10;
            LastRowStyle.Alignment.Vertical = StyleVerticalAlignment.Top;
            LastRowStyle.Alignment.WrapText = true;
            LastRowStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
            //_____________________________________________________________

        }




        /// <summary>
        /// salva il file Excel sul disco nella path specificata
        /// </summary>
        public static void saveFile(string filename)
        {
            try
            {
                m_wBook.Save(filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static void saveTable(DataTable MyDataTable)
        {

            WorksheetRow row;

            try
            {
                NewFile();
                foreach (DataRow riga in MyDataTable.Rows)
                {
                    row = m_wSheet.Table.Rows.Add(); // dichirazione della nuova riga

                    for (int i = 0; i < MyDataTable.Columns.Count; i++)
                    {
                        //row.Cells.Add(new WorksheetCell("NOME", "HeaderStyle"));

                        WorksheetCell cella = new WorksheetCell(riga[i].ToString(), "LastSpRow");
                        row.Cells.Add(cella);
                    }

                }

                saveFile(m_destinationDir); // Crea un nuovo file Excel);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException + " - " + ex.Message);
            }

        }

    }
}
