
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using CarlosAg.ExcelXmlWriter;
    using Dialogo.Helpers;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ReporteVisitasCampaña : Page
    {
        public string periodo;
        public string prefijoPais;
        public string estadoEvaluada;
        protected BeUsuario objUsuario;
        public int codigoRol;

        protected void Page_Load(object sender, EventArgs e)
        {
            periodo = Request["periodo"];
            litPeriodoTitulo.Text = " " + periodo.Trim();
            estadoEvaluada = Request["estado"];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario == null)
                Response.Redirect("~/error.aspx?mensaje=sesion");

            codigoRol = objUsuario.codigoRol;
            prefijoPais = objUsuario.prefijoIsoPais;

            lblUserLogeado.Text = objUsuario.nombreUsuario;
            lblRolLogueado.Text = objUsuario.rolDescripcion;

            if (!Page.IsPostBack)
            {
                try
                {
                    LlenarGrilla(prefijoPais, periodo, objUsuario.codigoRol);
                    CargarDetalles(estadoEvaluada, objUsuario.codigoRol);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?mensaje=" + ex.Message, true);
                }
            }
        }

        private void LlenarGrilla(string prefijoPais, string periodo, int codigoRol)
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            DataTable dtVisitas = new DataTable();
            dtVisitas = daProceso.ObtenerVisitasCampana(prefijoPais, periodo, codigoRol);
            grResumenVisitas.DataSource = dtVisitas;
            grResumenVisitas.DataBind();
        }

        private void CargarDetalles(string estado, int codigoRol)
        {
            if (estado == "0")
            {
                LlenarGrillaDetalleNuevas(codigoRol);
            }
            else if (estado == "1")
            {
                LlenarGrillaDetalleCriticas(codigoRol);
            }
            else if (estado == "2")
            {
                LlenarGrillaDetalleEstables(codigoRol);
            }
            else if (estado == "3")
            {
                LlenarGrillaDetalleProductivas(codigoRol);
            }
            else
            {
                LlenarGrillaDetalleNuevas(codigoRol);
                LlenarGrillaDetalleCriticas(codigoRol);
                LlenarGrillaDetalleEstables(codigoRol);
                LlenarGrillaDetalleProductivas(codigoRol);
            }
        }

        private void LlenarGrillaDetalleNuevas(int codigoRol)
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            grNuevas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "NUEVA", codigoRol);
            grNuevas.DataBind();

            if (grNuevas != null && grNuevas.Rows.Count > 0)
            {
                nuevas.Visible = true;
                lblNuevas.Visible = true;
            }
        }

        private void LlenarGrillaDetalleCriticas(int codigoRol)
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            grCriticas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "CRITICA", codigoRol);
            grCriticas.DataBind();

            if (grCriticas != null && grCriticas.Rows.Count > 0)
            {
                criticas.Visible = true;
                lblCriticas.Visible = true;
            }
        }

        private void LlenarGrillaDetalleEstables(int codigoRol)
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            grEstables.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "ESTABLE", codigoRol);
            grEstables.DataBind();

            if (grEstables != null && grEstables.Rows.Count > 0)
            {
                estables.Visible = true;
                lblEstables.Visible = true;
            }
        }

        private void LlenarGrillaDetalleProductivas(int codigoRol)
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            grProductivas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "PRODUCTIVA", codigoRol);
            grProductivas.DataBind();

            if (grProductivas != null && grProductivas.Rows.Count > 0)
            {
                productivas.Visible = true;
                lblProductivas.Visible = true;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected string GetUrl(string imagepath)
        {
            string[] splits = Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }
                return url + imagepath;
            }
            return imagepath;
        }

        protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            DataTable dtVisitas = new DataTable();
            dtVisitas = daProceso.ObtenerVisitasCampana(prefijoPais, periodo, codigoRol);
            dgResumen.DataSource = dtVisitas;
            ArrayList cabecera = new ArrayList();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "ANSI"; //No Unicode
            Response.ContentEncoding = System.Text.Encoding.Default;

            EnableViewState = false;

            //Resumen de Visitas
            Response.Write(CastDataTable(dgResumen, dtVisitas, cabecera, "TITULO", 100, "", 1));

            //Nuevas
            DataTable dtNuevas = new DataTable();
            dtNuevas = GenericListToDataTable(daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "NUEVA", codigoRol));
            dgNuevas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "NUEVA", codigoRol);
            if (dtNuevas != null && dtNuevas.Rows.Count > 0)
            {
                Response.Write(CastDataTable(dgNuevas, dtNuevas, cabecera, "NUEVAS", 100, "", 2));
            }

            //Criticas
            DataTable dtCriticas = new DataTable();
            dtCriticas =
                GenericListToDataTable(daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "CRITICA", codigoRol));
            dgCriticas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "CRITICA", codigoRol);
            if (dtCriticas != null && dtCriticas.Rows.Count > 0)
            {
                Response.Write(CastDataTable(dgCriticas, dtCriticas, cabecera, "CRITICAS", 100, "", 2));
            }

            //Estables
            DataTable dtEstables = new DataTable();
            dtEstables =
                GenericListToDataTable(daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "ESTABLE", codigoRol));
            dgEstables.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "ESTABLE", codigoRol);
            if (dtEstables != null && dtEstables.Rows.Count > 0)
            {
                Response.Write(CastDataTable(dgEstables, dtEstables, cabecera, "ESTABLES", 100, "", 2));
            }

            //Productivas
            DataTable dtProductivas = new DataTable();
            dtProductivas =
                GenericListToDataTable(daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "PRODUCTIVA", codigoRol));
            dgProductivas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "PRODUCTIVA", codigoRol);
            if (dtProductivas != null && dtProductivas.Rows.Count > 0)
            {
                Response.Write(CastDataTable(dgProductivas, dtProductivas, cabecera, "PRODUCTIVAS", 100, "", 2));
            }
            Response.End();
        }

        #region Excel

        public static DataTable GenericListToDataTable(object list)
        {
            DataTable dt = null;
            Type listType = list.GetType();
            if (listType.IsGenericType)
            {
                //determine the underlying type the List<> contains
                Type elementType = listType.GetGenericArguments()[0];

                //create empty table -- give it a name in case
                //it needs to be serialized
                dt = new DataTable(elementType.Name + "List");

                //define the table -- add a column for each public
                //property or field
                MemberInfo[] miArray = elementType.GetMembers(
                    BindingFlags.Public | BindingFlags.Instance);
                foreach (MemberInfo mi in miArray)
                {
                    if (mi.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo pi = mi as PropertyInfo;
                        dt.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else if (mi.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fi = mi as FieldInfo;
                        dt.Columns.Add(fi.Name, fi.FieldType);
                    }
                }

                //populate the table
                IList il = list as IList;
                foreach (object record in il)
                {
                    int i = 0;
                    object[] fieldValues = new object[dt.Columns.Count];
                    foreach (DataColumn c in dt.Columns)
                    {
                        MemberInfo mi = elementType.GetMember(c.ColumnName)[0];
                        if (mi.MemberType == MemberTypes.Property)
                        {
                            PropertyInfo pi = mi as PropertyInfo;
                            fieldValues[i] = pi.GetValue(record, null);
                        }
                        else if (mi.MemberType == MemberTypes.Field)
                        {
                            FieldInfo fi = mi as FieldInfo;
                            fieldValues[i] = fi.GetValue(record);
                        }
                        i++;
                    }
                    dt.Rows.Add(fieldValues);
                }
            }
            return dt;
        }

        public string CastDataTable(DataGrid dg, DataTable dt, ArrayList alCabecera, string strTitulo, int numItems,
                                    string cultura, int tipo)
        {
            StringBuilder sbResultado = new StringBuilder();
            string lstNomColumna = string.Empty;
            string lstTexColumna = string.Empty;
            string strHeader = string.Empty;
            string strTable = string.Empty;

            GetColumnasValidas(dg, ref lstNomColumna, ref lstTexColumna);

            dt = GetDataTableColumnasValidas(dt, lstNomColumna);

            // strHeader = this.CastArrayListTableToCSV(alCabecera);
            strTable = tipo == 1
                           ? this.CastDataTableResumenVisitas(dt, lstNomColumna, numItems)
                           : this.CastDataTableDetalles(dt, lstNomColumna, numItems);

            strTable = strTable.Replace("&gt;", ">");
            strTable = strTable.Replace("&lt;", "<");
            strTable = strTable.Replace("(", string.Empty);
            strTable = strTable.Replace(")", string.Empty);
            sbResultado.Append(strTitulo);
            sbResultado.Append("\n\n");
            sbResultado.Append(strHeader);
            sbResultado.Append("\n\n");
            sbResultado.Append(strTable);

            return sbResultado.ToString();
        }

        private void GetColumnasValidas(DataGrid dg, ref string lstNomColumna, ref string lstTexColumna)
        {
            DataGridColumn dgCol;
            StringBuilder sbNomColumna = new StringBuilder();
            StringBuilder sbTexColumna = new StringBuilder();

            string strNomColumna, strTexColumna;

            for (int i = 0; i < dg.Columns.Count; i++)
            {
                dgCol = dg.Columns[i];

                try
                {
                    if (dgCol.GetType().Equals(typeof(System.Web.UI.WebControls.BoundColumn)))
                    {
                        strNomColumna = ((System.Web.UI.WebControls.BoundColumn)dgCol).DataField;
                        strTexColumna = ((System.Web.UI.WebControls.BoundColumn)dgCol).HeaderText;

                        if (sbNomColumna.Length > 0)
                        {
                            sbNomColumna.Append("|");
                            sbTexColumna.Append("|");
                        }
                        sbNomColumna.Append(strNomColumna.Trim());
                        sbTexColumna.Append(strTexColumna.Trim());
                    }
                }
                catch (Exception)
                {
                }
            }
            lstNomColumna = sbNomColumna.ToString();
            lstTexColumna = sbTexColumna.ToString();
        }

        private DataTable GetDataTableColumnasValidas(DataTable dt, string lstNomColumna)
        {
            string[] nombreColumnas = lstNomColumna.Split('|');

            string nombreColumna;
            StringBuilder lstColumnasEliminar = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                nombreColumna = dt.Columns[i].ColumnName;

                bool bolExiste = false;

                for (int j = 0; j < nombreColumnas.Length; j++)
                {
                    if (nombreColumna.ToUpper() == nombreColumnas[j].ToUpper())
                    {
                        bolExiste = true;
                        break;
                    }
                }

                if (!bolExiste)
                {
                    if (lstColumnasEliminar.Length > 0)
                    {
                        lstColumnasEliminar.Append("|");
                    }
                    lstColumnasEliminar.Append(nombreColumna);
                }
            }

            if (lstColumnasEliminar.ToString() != string.Empty)
            {
                string[] columnasEliminar = lstColumnasEliminar.ToString().Split('|');

                for (int k = 0; k < columnasEliminar.Length; k++)
                {
                    dt.Columns.Remove(columnasEliminar[k]);
                }
            }
            return dt;
        }

        private string CastDataTableResumenVisitas(DataTable dt, string lstNomColumna, int numItems)
        {
            StringBuilder sbCSV = new StringBuilder();
            string[] nombreColumnas = lstNomColumna.Split('|');

            sbCSV.Append(
                "<table border='0' style='border-bottom:none 0px; border-top:0' cellspacing='0' cellpadding='0'>");
            sbCSV.Append(
                "<tr><td style='border-color:Black;border-style:solid;border-width:1px;width:181px;background:#D9D9D9'>");
            sbCSV.Append(
                "Resumen de Visitas</td><td style='border-color:Black;border-style:solid;border-width:1px;width:101px;text-align:center' colspan='2'>");
            sbCSV.Append(
                "Nuevas</td><td style='border-color:Black;border-style:solid;border-width:1px;width:101px;background:red;text-align:center' colspan='2'>");
            sbCSV.Append(
                "Criticas</td><td style='border-color:Black;border-style:solid;border-width:1px;width:101px;background:yellow;text-align:center' colspan='2'>");
            sbCSV.Append(
                "Estables</td><td style='border-color:Black;border-style:solid;border-width:1px;width:101px;background:green;text-align:center' colspan='2'>");
            sbCSV.Append(
                "Productivas</td><td style='border-color:Black;border-style:solid;border-width:1px;width:101px;text-align:center' colspan='2'>");
            sbCSV.Append("TOTAL</td></tr></table>");

            if (dt.Rows.Count > 0)
            {
                int numRows = dt.Rows.Count;

                if (numItems != -1 && numRows > numItems)
                {
                    numRows = numItems;
                }

                for (int j = 0; j < numRows; j++)
                {
                    sbCSV.Append(
                        "<table border='0' style='border-bottom:none 0px; border-top:0' cellspacing='0' cellpadding='0'><tr><td style='border-color:Black;border-style:solid;border-width:1px;width:150px'>");
                    for (int k = 0; k < nombreColumnas.Length; k++)
                    {
                        if (dt.Rows[j][nombreColumnas[k]].ToString() != string.Empty)
                        {
                            if (nombreColumnas[k] != "Imagen")
                            {
                                sbCSV.Append(dt.Rows[j][nombreColumnas[k]].ToString().Trim());
                            }
                        }
                        else
                        {
                            sbCSV.Append(" ");
                        }
                        if (k + 1 != nombreColumnas.Length)
                        {
                            if (nombreColumnas[k] != "Imagen")
                            {
                                sbCSV.Append(
                                    "<td style='border-color:Black;border-style:solid;border-width:1px;width:150px'>");
                            }
                        }
                    }
                    sbCSV.Append("</td></tr></table>");
                }
            }

            return sbCSV.ToString();
        }

        private string CastDataTableDetalles(DataTable dt, string lstNomColumna, int numItems)
        {
            string textPeriodo = "Período : " + Request["periodo"];
            StringBuilder sbCSV = new StringBuilder();

            string[] nombreColumnas = lstNomColumna.Split('|');

            sbCSV.Append(
                "<table width='1100px' border='0' style='border-bottom:none 0px;border-top:0' cellspacing='0' cellpadding='0'>");
            sbCSV.Append(
                "<tr style='background:#D9D9D9'><td style='border-color:Black;border-style:solid;border-width:1px;width:34px'>");
            sbCSV.Append("Zona</td><td style='border-color:Black;border-style:solid;border-width:1px;width:365px'>");
            sbCSV.Append("GZ</td><td style='border-color:Black;border-style:solid;border-width:1px;width:101px'>");
            sbCSV.Append(
                "Variable01</td><td style='border-color:Black;border-style:solid;border-width:1px;width:100px'>");
            sbCSV.Append(
                "Variable02</td><td colspan='6' style='border-color:Black;border-style:solid;border-width:1px;width:510px;text-align:center'>");
            sbCSV.Append(textPeriodo +
                "<table border='0' style='border-bottom:none 0px; border-top:0' cellspacing='0' cellpadding='0'>");
            sbCSV.Append(
                "<tr><td style='border-color:Black;border-style:solid;border-width:1px;width:85px;text-align:center'colspan='3' >C01</td>");
            sbCSV.Append(
                "<td style='border-color:Black;border-style:solid;border-width:1px;width:85px;text-align:center' colspan='3'>C02</td>");
            sbCSV.Append(
                "<td style='border-color:Black;border-style:solid;border-width:1px;width:85px;text-align:center' colspan='3'>C03</td>");
            sbCSV.Append(
                "<td style='border-color:Black;border-style:solid;border-width:1px;width:85px;text-align:center' colspan='3'>C04</td>");
            sbCSV.Append(
                "<td style='border-color:Black;border-style:solid;border-width:1px;width:85px;text-align:center' colspan='3'>C05</td>");
            sbCSV.Append(
                "<td style='border-color:Black;border-style:solid;border-width:1px;width:85px;text-align:center' colspan='3'>C06</td></tr></table></td></tr></table>");

            if (dt.Rows.Count > 0)
            {
                int numRows = dt.Rows.Count;

                if (numItems != -1 && numRows > numItems)
                {
                    numRows = numItems;
                }

                for (int j = 0; j < numRows; j++)
                {
                    sbCSV.Append("\n");
                    sbCSV.Append("\n");
                    sbCSV.Append(
                        "<table border='0' style='border-bottom:none 0px; border-top:0' cellspacing='0' cellpadding='0'><tr><td style='border-color:Black;border-style:solid;border-width:1px;width:50px'>");
                    for (int k = 0; k < nombreColumnas.Length; k++)
                    {
                        if (dt.Rows[j][nombreColumnas[k]].ToString() != string.Empty)
                        {
                            sbCSV.Append(dt.Rows[j][nombreColumnas[k]].ToString().Trim());
                        }
                        else
                        {
                            sbCSV.Append(" ");
                        }
                        if (k + 1 != nombreColumnas.Length)
                        {
                            sbCSV.Append(
                                "<td style='border-color:Black;border-style:solid;border-width:1px;width:150px'>");
                        }
                    }
                    sbCSV.Append("</td></tr></table>");
                }
                sbCSV.Append("<table><tr><td></td></tr></table>");
            }
            sbCSV.Append("\n");

            return sbCSV.ToString();
        }

        #endregion Excel

        protected void btnEmail_Click(object sender, ImageClickEventArgs e)
        {
            string textPeriodo = "Período : " + Request["periodo"];
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();
            DataTable dtVisitas = new DataTable();
            dtVisitas = daProceso.ObtenerVisitasCampana(prefijoPais, periodo, codigoRol);
            DataSourceSelectArguments arg = new DataSourceSelectArguments();
            string Title = "Reporte Visitas por Campaña";
            MemoryStream MemStreamResult = new MemoryStream();
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets.Add("Visitas por Campaña");

            WorksheetRow TitleRow = sheet.Table.Rows.Add();
            WorksheetCell TitleCell = TitleRow.Cells.Add(Title);

            WorksheetStyle style = book.Styles.Add("HeaderStyle");
            style.Font.FontName = "Tahoma";
            //style.Font.Size = 14;
            style.Font.Bold = true;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Font.Color = "White";
            style.Interior.Color = "Blue";
            style.Interior.Pattern = StyleInteriorPattern.DiagCross;

            sheet.Table.Rows.Add(); //--- Blank Row

            //--- Add Column Headers
            WorksheetRow HeaderRow = sheet.Table.Rows.Add();

            HeaderRow.AutoFitHeight = true;
            HeaderRow.Cells.Add("Resumen de Visitas");
            HeaderRow.Cells.Add("Nuevas");
            HeaderRow.Cells.Add("");
            HeaderRow.Cells.Add("Criticas");
            HeaderRow.Cells.Add("");
            HeaderRow.Cells.Add("Estables");
            HeaderRow.Cells.Add("");
            HeaderRow.Cells.Add("Productivas");
            HeaderRow.Cells.Add("");
            HeaderRow.Cells.Add("TOTAL");
            HeaderRow.Cells.Add("");

            //HeaderRow.Cells.Add(sbCSV.ToString());

            // For each row, print the values of each column.
            for (int RowNum = 0; RowNum < dtVisitas.Rows.Count; RowNum++)
            {
                WorksheetRow ValueRow = sheet.Table.Rows.Add();
                for (int ColNum = 0; ColNum < dtVisitas.Columns.Count; ColNum++)
                {
                    if (dtVisitas.Columns[ColNum].ColumnName != "Imagen")
                    {
                        string StrVal = dtVisitas.Rows[RowNum][ColNum].ToString();
                        int nOut = 0;
                        float fOut = 0;

                        //--- Add numbers with appropriate data type so you don't see messy warnings in your workbook
                        if (int.TryParse(StrVal, out nOut) || float.TryParse(StrVal, out fOut))
                            ValueRow.Cells.Add(new WorksheetCell(StrVal, DataType.Number));
                        else
                            ValueRow.Cells.Add(StrVal);
                    }
                }
            }

            /****************************************************************/

            //Nuevas
            dgNuevas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "NUEVA", codigoRol);
            dgNuevas.DataBind();

            if (dgNuevas != null && dgNuevas.Items.Count > 0)
            {
                WorksheetRow EspacioNuevas = sheet.Table.Rows.Add();
                WorksheetRow TituloNuevas = sheet.Table.Rows.Add();
                WorksheetRow FirstHeaderNuevas = sheet.Table.Rows.Add();
                WorksheetRow HeaderRowNuevas = sheet.Table.Rows.Add();

                EspacioNuevas.Cells.Add(" ");
                TituloNuevas.Cells.Add("NUEVAS");
                FirstHeaderNuevas.Cells.Add("");
                FirstHeaderNuevas.Cells.Add("");
                FirstHeaderNuevas.Cells.Add("");
                FirstHeaderNuevas.Cells.Add("");
                FirstHeaderNuevas.Cells.Add(textPeriodo);
                HeaderRowNuevas.Cells.Add("Zona");
                HeaderRowNuevas.Cells.Add("GZ");
                HeaderRowNuevas.Cells.Add("Variable 01");
                HeaderRowNuevas.Cells.Add("Variable 02");
                HeaderRowNuevas.Cells.Add("C01");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("C02");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("C03");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("C04");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("C05");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("C06");
                HeaderRowNuevas.Cells.Add("");
                HeaderRowNuevas.Cells.Add("");

                foreach (DataGridItem dgi in dgNuevas.Items)
                {
                    WorksheetRow ValueRowNuevas = sheet.Table.Rows.Add();
                    for (int i = 0; i < dgNuevas.Columns.Count; i++)
                    {
                        if (dgi.Cells[i].Text != "&nbsp;")
                        {
                            ValueRowNuevas.Cells.Add(dgi.Cells[i].Text);
                        }
                        else
                        {
                            ValueRowNuevas.Cells.Add("");
                        }
                    }
                }
            }

            /****************************************************************/

            //Criticas
            dgCriticas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "CRITICA", codigoRol);
            dgCriticas.DataBind();

            if (dgCriticas != null && dgCriticas.Items.Count > 0)
            {
                WorksheetRow EspacioCriticas = sheet.Table.Rows.Add();
                WorksheetRow TituloCriticas = sheet.Table.Rows.Add();
                WorksheetRow FirstHeaderCriticas = sheet.Table.Rows.Add();
                WorksheetRow HeaderRowCriticas = sheet.Table.Rows.Add();

                EspacioCriticas.Cells.Add(" ");
                TituloCriticas.Cells.Add("CRITICAS");
                FirstHeaderCriticas.Cells.Add("");
                FirstHeaderCriticas.Cells.Add("");
                FirstHeaderCriticas.Cells.Add("");
                FirstHeaderCriticas.Cells.Add("");
                FirstHeaderCriticas.Cells.Add(textPeriodo);
                HeaderRowCriticas.Cells.Add("Zona");
                HeaderRowCriticas.Cells.Add("GZ");
                HeaderRowCriticas.Cells.Add("Variable 01");
                HeaderRowCriticas.Cells.Add("Variable 02");
                HeaderRowCriticas.Cells.Add("C01");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("C02");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("C03");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("C04");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("C05");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("C06");
                HeaderRowCriticas.Cells.Add("");
                HeaderRowCriticas.Cells.Add("");

                foreach (DataGridItem dgi in dgCriticas.Items)
                {
                    WorksheetRow ValueRowCriticas = sheet.Table.Rows.Add();
                    for (int i = 0; i < dgCriticas.Columns.Count; i++)
                    {
                        if (dgi.Cells[i].Text != "&nbsp;")
                        {
                            ValueRowCriticas.Cells.Add(dgi.Cells[i].Text);
                        }
                        else
                        {
                            ValueRowCriticas.Cells.Add("");
                        }
                    }
                }
            }
            /****************************************************************/

            //Estables
            dgEstables.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "ESTABLE", codigoRol);
            dgEstables.DataBind();

            if (dgEstables != null && dgEstables.Items.Count > 0)
            {
                WorksheetRow EspacioEstables = sheet.Table.Rows.Add();
                WorksheetRow TituloEstables = sheet.Table.Rows.Add();
                WorksheetRow FirstHeaderEstables = sheet.Table.Rows.Add();
                WorksheetRow HeaderRowEstables = sheet.Table.Rows.Add();

                EspacioEstables.Cells.Add(" ");
                TituloEstables.Cells.Add("ESTABLES");
                FirstHeaderEstables.Cells.Add("");
                FirstHeaderEstables.Cells.Add("");
                FirstHeaderEstables.Cells.Add("");
                FirstHeaderEstables.Cells.Add("");
                FirstHeaderEstables.Cells.Add(textPeriodo);
                HeaderRowEstables.Cells.Add("Zona");
                HeaderRowEstables.Cells.Add("GZ");
                HeaderRowEstables.Cells.Add("Variable 01");
                HeaderRowEstables.Cells.Add("Variable 02");
                HeaderRowEstables.Cells.Add("C01");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("C02");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("C03");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("C04");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("C05");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("C06");
                HeaderRowEstables.Cells.Add("");
                HeaderRowEstables.Cells.Add("");

                foreach (DataGridItem dgi in dgEstables.Items)
                {
                    WorksheetRow ValueRowEstables = sheet.Table.Rows.Add();
                    for (int i = 0; i < dgEstables.Columns.Count; i++)
                    {
                        if (dgi.Cells[i].Text != "&nbsp;")
                        {
                            ValueRowEstables.Cells.Add(dgi.Cells[i].Text);
                        }
                        else
                        {
                            ValueRowEstables.Cells.Add("");
                        }
                    }
                }
            }
            /****************************************************************/

            //Estables
            dgProductivas.DataSource = daProceso.ObtenerVisitasDetalle(periodo, prefijoPais, "PRODUCTIVA", codigoRol);
            dgProductivas.DataBind();
            if (dgProductivas != null && dgProductivas.Items.Count > 0)
            {
                WorksheetRow EspacioProductivas = sheet.Table.Rows.Add();
                WorksheetRow TituloProductivas = sheet.Table.Rows.Add();
                WorksheetRow FirstHeaderProductivas = sheet.Table.Rows.Add();
                WorksheetRow HeaderRowProductivas = sheet.Table.Rows.Add();

                EspacioProductivas.Cells.Add(" ");
                TituloProductivas.Cells.Add("PRODUCTIVAS");
                FirstHeaderProductivas.Cells.Add("");
                FirstHeaderProductivas.Cells.Add("");
                FirstHeaderProductivas.Cells.Add("");
                FirstHeaderProductivas.Cells.Add("");
                FirstHeaderProductivas.Cells.Add(textPeriodo);
                HeaderRowProductivas.Cells.Add("Zona");
                HeaderRowProductivas.Cells.Add("GZ");
                HeaderRowProductivas.Cells.Add("Variable 01");
                HeaderRowProductivas.Cells.Add("Variable 02");
                HeaderRowProductivas.Cells.Add("C01");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("C02");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("C03");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("C04");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("C05");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("C06");
                HeaderRowProductivas.Cells.Add("");
                HeaderRowProductivas.Cells.Add("");

                foreach (DataGridItem dgi in dgProductivas.Items)
                {
                    WorksheetRow ValueRowProductivas = sheet.Table.Rows.Add();
                    for (int i = 0; i < dgProductivas.Columns.Count; i++)
                    {
                        if (dgi.Cells[i].Text != "&nbsp;")
                        {
                            ValueRowProductivas.Cells.Add(dgi.Cells[i].Text);
                        }
                        else
                        {
                            ValueRowProductivas.Cells.Add("");
                        }
                    }
                }
            }

            /***********************************************/

            book.Save(MemStreamResult);
            Byte[] bytearray = MemStreamResult.ToArray();
            MemStreamResult.Close();

            MailAddress correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"].ToString());
            string servidorSMTP = ConfigurationManager.AppSettings["servidorSMTP"].ToString();
            SmtpClient enviar = new SmtpClient(servidorSMTP);
            try
            {
                MailAddress correoTo = new MailAddress(ConfigurationManager.AppSettings["usuarioSoporte"].ToString());
                //MailAddress correoTo = new MailAddress(correoEvaluador);
                MailMessage objMailMsg = new MailMessage(correoFrom, correoTo);
                objMailMsg.BodyEncoding = Encoding.UTF8;
                objMailMsg.Subject = "Reporte Visitas por Campaña";
                objMailMsg.Body = "";

                MemoryStream StreamToAttach = new MemoryStream(MemStreamResult.ToArray());
                Attachment at = new Attachment(StreamToAttach, "Reporte Visitas por Campaña.xls");
                objMailMsg.Attachments.Add(at);
                objMailMsg.Priority = MailPriority.Normal;
                objMailMsg.IsBodyHtml = true;

                // msjEmail.Attachments(strHTML);
                enviar.Send(objMailMsg);
            }
            catch
            {
                //modificar
            }
        }

        protected void grNuevas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label etiquetaPerioro = (Label)e.Row.FindControl("Label1");

                if (etiquetaPerioro != null)
                    etiquetaPerioro.Text = "Período : " + Request["periodo"];
            }
        }
    }
}