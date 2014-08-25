
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Excel
{
    using BusinessEntity;
    using CarlosAg.ExcelXmlWriter;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ExcelDataTableWriter
    {

        public void PopulateWorksheet(DataTable dt, Worksheet toPopulate, string titulo, string parrafoInicial)
        {
            PopulateWorksheet(dt, toPopulate, true, titulo, parrafoInicial);
        }
        public void PopulateWorksheet(DataTable dt, Worksheet toPopulate, bool makeHeader, string titulo, string parrafoInicial)
        {
            //check valid input
            if (toPopulate == null)
            {
                throw new ArgumentNullException("toPopulate", "Worksheet cannot be null.");
            }
            if (dt == null)
            {
                throw new ArgumentNullException("dt", "DataTable cannot be null");
            }
            //Parse the columns
            ColumnType[] colDesc = parseColumns(dt);

            //Create titulo y filtros
            WorksheetRow wsrTitulo = new WorksheetRow();

            wsrTitulo.Cells.Add(titulo);
            wsrTitulo.Cells[0].MergeAcross = 10;
            wsrTitulo.Cells[0].StyleID = "Title";

            toPopulate.Table.Rows.Insert(0, wsrTitulo);


            WorksheetRow wsrFiltro = new WorksheetRow();

            wsrFiltro.Cells.Add(parrafoInicial);
            wsrFiltro.Cells[0].MergeAcross = 10;

            toPopulate.Table.Rows.Insert(1, wsrFiltro);
            toPopulate.Table.Rows.Insert(2, new WorksheetRow());

            //Create header row
            if (makeHeader)
            {
                toPopulate.Table.Rows.Insert(3, makeHeaderRow(colDesc));
            }
            //Create rows
            foreach (DataRow row in dt.Rows)
            {
                toPopulate.Table.Rows.Add(makeDataRow(colDesc, row));
            }
        }

        public void hojaMatrizConsolidada01(Worksheet hojaTrabajo, string formato, string[] data, List<BeComun> niveles, List<BeComun> tamVentas, string tipoColaborador, string filtros)
        {
            if (hojaTrabajo == null)
            {
                throw new ArgumentNullException("hojaTrabajo", "La hoja de trabajo no puede ser nula.");
            }

            WorksheetRow wsrTitulo = new WorksheetRow();
            WorksheetRow texto = new WorksheetRow();
            WorksheetRow resaltado01 = new WorksheetRow();
            WorksheetColumn column = new WorksheetColumn();

            string[] ranking = { "Crítica", "Estable", "Productiva" };
            string[] styleRanking = { "cabeceraPrincipal01", "cabeceraPrincipal02", "cabeceraPrincipal03" };
            int colSpan = 0;

            if (tipoColaborador == "00")
            {
                colSpan = 4;
            }
            else if (tipoColaborador == "01")
            {
                colSpan = 0;
            }
            else if (tipoColaborador == "02")
            {
                colSpan = 3;
            }

            wsrTitulo.Cells.Add();
            wsrTitulo.Cells.Add("Matriz consolidada");
            wsrTitulo.Cells[1].MergeAcross = 12;
            wsrTitulo.Cells[1].StyleID = "Title";
            hojaTrabajo.Table.Rows.Insert(0, wsrTitulo);

            texto.Cells.Add();
            texto.Cells.Add(filtros);
            texto.Cells[1].MergeAcross = 12;
            hojaTrabajo.Table.Rows.Insert(1, texto);

            hojaTrabajo.Table.Rows.Insert(2, new WorksheetRow());

            WorksheetCell tmp = new WorksheetCell();
            tmp.Data.Text = "";
            resaltado01.Cells.Add(tmp);

            tmp = new WorksheetCell();
            tmp.Data.Text = "Tamaño";
            tmp.StyleID = "resaltado01";
            resaltado01.Cells.Add(tmp);

            for (int x = 0; x < ranking.Length; x++)
            {
                tmp = new WorksheetCell();
                tmp.Data.Text = ranking[x];
                tmp.StyleID = styleRanking[x];
                tmp.MergeAcross = colSpan;
                resaltado01.Cells.Add(tmp);
            }

            tmp = new WorksheetCell();
            tmp.Data.Text = "Total";
            tmp.StyleID = "resaltado01";
            tmp.MergeDown = 1;
            resaltado01.Cells.Add(tmp);

            hojaTrabajo.Table.Rows.Insert(3, resaltado01);

            resaltado01 = new WorksheetRow();
            resaltado01.Cells.Add();

            tmp = new WorksheetCell();
            tmp.Data.Text = "venta";
            tmp.StyleID = "resaltado01";
            resaltado01.Cells.Add(tmp);

            for (int x = 0; x < ranking.Length; x++)
            {
                if (tipoColaborador == "00" || tipoColaborador == "01")
                {
                    tmp = new WorksheetCell();
                    tmp.Data.Text = "Nuevas";
                    tmp.StyleID = "cabeceraSecundaria";
                    resaltado01.Cells.Add(tmp);
                }
                if (tipoColaborador == "00" || tipoColaborador == "02")
                {
                    foreach (BeComun b in niveles)
                    {
                        b.Descripcion = b.Descripcion.Substring(0, 1).ToUpper() + b.Descripcion.Substring(1, b.Descripcion.Length - 1).ToLower();

                        tmp = new WorksheetCell();
                        tmp.Data.Text = b.Descripcion;
                        tmp.StyleID = "cabeceraSecundaria";
                        resaltado01.Cells.Add(tmp);
                    }
                    tmp = new WorksheetCell();
                    tmp.Data.Text = "Sin Medición";
                    tmp.StyleID = "cabeceraSecundaria";
                    resaltado01.Cells.Add(tmp);

                }
            }
            hojaTrabajo.Table.Rows.Insert(4, resaltado01);

            resaltado01 = new WorksheetRow();

            int t = 1;
            int p = 0;
            foreach (BeComun b in tamVentas)
            {
                b.Descripcion = b.Descripcion.Substring(0, 1).ToUpper() + b.Descripcion.Substring(1, b.Descripcion.Length - 1).ToLower();

                resaltado01.Cells.Add();
                tmp = new WorksheetCell();
                tmp.Data.Text = b.Descripcion;
                tmp.StyleID = "resaltado02";
                resaltado01.Cells.Add(tmp);

                for (int x = 0; x < ranking.Length; x++)
                {
                    if (tipoColaborador == "00" || tipoColaborador == "01")
                    {
                        tmp = new WorksheetCell();
                        tmp.Data.Text = data[p].ToString();
                        tmp.StyleID = "data";
                        resaltado01.Cells.Add(tmp);
                        p += 1;
                    }
                    if (tipoColaborador == "00" || tipoColaborador == "02")
                    {
                        foreach (BeComun n in niveles)
                        {
                            tmp = new WorksheetCell();
                            tmp.Data.Text = data[p].ToString();
                            tmp.StyleID = "data";
                            resaltado01.Cells.Add(tmp);
                            p += 1;
                        }
                        tmp = new WorksheetCell();
                        tmp.Data.Text = data[p].ToString();
                        tmp.StyleID = "data";
                        resaltado01.Cells.Add(tmp);
                        p += 1;

                    }
                }

                tmp = new WorksheetCell();
                tmp.Data.Text = data[p].ToString();
                tmp.StyleID = "resultados";
                resaltado01.Cells.Add(tmp);

                p += 1;
                hojaTrabajo.Table.Rows.Insert(4 + t, resaltado01);
                t += 1;
                resaltado01 = new WorksheetRow();
            }

            resaltado01.Cells.Add();
            tmp = new WorksheetCell();
            tmp.Data.Text = "Total";
            tmp.StyleID = "resaltado01";
            resaltado01.Cells.Add(tmp);

            for (int x = 0; x < ranking.Length; x++)
            {
                if (tipoColaborador == "00" || tipoColaborador == "01")
                {
                    tmp = new WorksheetCell();
                    tmp.Data.Text = data[p].ToString();
                    tmp.StyleID = "resultados";
                    resaltado01.Cells.Add(tmp);
                    p += 1;
                }
                if (tipoColaborador == "00" || tipoColaborador == "02")
                {
                    foreach (BeComun n in niveles)
                    {
                        tmp = new WorksheetCell();
                        tmp.Data.Text = data[p].ToString();
                        tmp.StyleID = "resultados";
                        resaltado01.Cells.Add(tmp);
                        p += 1;
                    }
                    tmp = new WorksheetCell();
                    tmp.Data.Text = data[p].ToString();
                    tmp.StyleID = "resultados";
                    resaltado01.Cells.Add(tmp);
                    p += 1;

                }
            }

            tmp = new WorksheetCell();
            tmp.Data.Text = data[p].ToString();
            tmp.StyleID = "resultados";
            resaltado01.Cells.Add(tmp);
            p += 1;

            hojaTrabajo.Table.Rows.Insert(8, resaltado01);

            WorksheetColumn col = new WorksheetColumn();
            col.Width = 10;
            hojaTrabajo.Table.Columns.Insert(0, col);

            col = new WorksheetColumn();
            col.Width = 47;
            hojaTrabajo.Table.Columns.Insert(1, col);

            for (int x = 0; x < ranking.Length; x++)
            {
                if (tipoColaborador == "00" || tipoColaborador == "01")
                {
                    col = new WorksheetColumn();
                    col.Width = 62;
                    hojaTrabajo.Table.Columns.Insert(x + 2, col);
                }
                if (tipoColaborador == "00" || tipoColaborador == "02")
                {
                    foreach (BeComun n in niveles)
                    {
                        col = new WorksheetColumn();
                        col.Width = 62;
                        hojaTrabajo.Table.Columns.Insert(x + 2, col);
                    }
                    col = new WorksheetColumn();
                    col.Width = 62;
                    hojaTrabajo.Table.Columns.Insert(x + 2, col);
                }
            }
        }

        #region row + cell making
        private WorksheetRow makeHeaderRow(ColumnType[] cols)
        {
            WorksheetRow ret = new WorksheetRow();

            foreach (ColumnType ctd in cols)
            {
                ret.Cells.Add(ctd.GetHeaderCell());
            }
            return ret;
        }
        private WorksheetRow makeDataRow(ColumnType[] ctds, DataRow row)
        {
            WorksheetRow ret = new WorksheetRow();

            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                WorksheetCell tmp = ctds[i].GetDataCell(row[i]);
                tmp.StyleID = "LastSpRow";
                ret.Cells.Add(tmp);
            }
            return ret;
        }
        #endregion

        #region column parsing
        private ColumnType[] parseColumns(DataTable dt)
        {
            ColumnType[] ret = new ColumnType[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ColumnType ctd = new ColumnType();
                ctd.Name = dt.Columns[i].ColumnName;
                getDataType(dt.Columns[i], ctd);
                ret[i] = ctd;
            }
            return ret;
        }
        private void getDataType(DataColumn col, ColumnType desc)
        {
            if (col.DataType == typeof(DateTime))
            {
                desc.ExcelType = DataType.DateTime;
            }
            else if (col.DataType == typeof(string))
            {
                desc.ExcelType = DataType.String;
            }
            else if (col.DataType == typeof(sbyte)
                || col.DataType == typeof(byte)
                || col.DataType == typeof(short)
                || col.DataType == typeof(ushort)
                || col.DataType == typeof(int)
                || col.DataType == typeof(uint)
                || col.DataType == typeof(long)
                || col.DataType == typeof(ulong)
                || col.DataType == typeof(float)
                || col.DataType == typeof(double)
                || col.DataType == typeof(decimal)
                )
            {
                desc.ExcelType = DataType.Number;
            }
            else
            {
                desc.ExcelType = DataType.String;
            }
        }
        #endregion
    }
}
