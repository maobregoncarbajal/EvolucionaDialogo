
namespace Evoluciona.Dialogo.Web.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class rptUsoTiempo : Page
    {
        private BeUsuario objUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario == null)
                Response.Redirect("~/error.aspx?mensaje=sesion");

            lblUserLogeado.Text = objUsuario.nombreUsuario;
            lblRolLogueado.Text = objUsuario.rolDescripcion;

            if (!Page.IsPostBack)
            {
                try
                {
                    LLenarGrilla();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?mensaje=" + ex.Message, true);
                }
            }
        }

        private void LLenarGrilla()
        {
            BlReportes daReporte = new BlReportes();
            ArrayList cabecera = new ArrayList();

            DataTable dtReuniones = new DataTable();
            List<BeReporteUsoTiempo> datosReuniones = daReporte.ObtenerDatosReunionReporte(objUsuario.codigoUsuario, objUsuario.codigoRol);

            dtReuniones = GenericListToDataTable(datosReuniones);

            if (dtReuniones.Rows.Count > 0)
            {
                dgReuniones.DataSource = datosReuniones;
                ltReuniones.Text = CastDataTable(dgReuniones, dtReuniones, cabecera, "", 100, "", 1);

                DataTable dtTotales = new DataTable();
                List<BeReporteUsoTiempo> totalesReuniones = daReporte.ObtenerTotalesReunionReporte(objUsuario.codigoUsuario);

                dtTotales = GenericListToDataTable(totalesReuniones);
                dgTotales.DataSource = totalesReuniones;
                ltTotales.Text = CastDataTable(dgTotales, dtTotales, cabecera, "", 100, "", 2);
            }
            else
            {
                lblMensaje.Text = "No existen datos para mostrar.";
                lblMensaje.Visible = true;
            }
        }

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

        public string CastDataTable(DataGrid dg, DataTable dt, ArrayList alCabecera, string strTitulo, int numItems, string cultura, int tipo)
        {
            StringBuilder sbResultado = new StringBuilder();
            string lstNomColumna = string.Empty;
            string lstTexColumna = string.Empty;
            string strHeader = string.Empty;
            string strTable = string.Empty;

            GetColumnasValidas(dg, ref lstNomColumna, ref lstTexColumna);
            dt = GetDataTableColumnasValidas(dt, lstNomColumna);

            if (tipo == 1)
            {
                strTable = this.CastDataTableRuniones(dt, lstNomColumna, numItems);
            }
            else
            {
                strTable = this.CastDataTableTotales(dt, lstNomColumna, numItems);
            }

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
                    if (dgCol.GetType().Equals(typeof(BoundColumn)))
                    {
                        strNomColumna = ((BoundColumn)dgCol).DataField;
                        strTexColumna = ((BoundColumn)dgCol).HeaderText;

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

        private string CastDataTableRuniones(DataTable dt, string lstNomColumna, int numItems)
        {
            StringBuilder sbCSV = new StringBuilder();
            int entro = 0;
            int entro2 = 0;
            int cantrow;
            string[] nombreColumnas = lstNomColumna.Split('|');

            if (dt.Rows.Count > 0)
            {
                int numRows = dt.Rows.Count;

                if (numItems != -1 && numRows > numItems)
                {
                    numRows = numItems;
                }

                sbCSV.Append("<table width='1300' border='0' style='border-bottom:none 0px; border-top:0' cellspacing='0' cellpadding='0'>");
                sbCSV.Append("<tr><th colspan='2'></th><th style='border-color:Black;border-style:solid;border-width:1px;background:#60497B;color:white;' colspan='8' align='center'>Periodo 1</th>");
                sbCSV.Append("<th style='border-color:Black;border-style:solid;border-width:1px;background:#60497B;color:white;' colspan='8' align='center'>Periodo 2</th>");
                sbCSV.Append("<th style='border-color:Black;border-style:solid;border-width:1px;background:#60497B;color:white;' colspan='8' align='center'>Periodo 3</th>");
                sbCSV.Append("</tr><tr style='background:#CCC0DA'><td style='border-color:Black;border-style:solid;border-width:1px;width:60px;' align='center'>Reuniones</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:250px;' align='center'>Actividades</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>1</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>2</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>3</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>4</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>5</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>6</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>Total</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>%</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>7</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>8</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>9</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>10</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>11</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>12</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>Total</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>%</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>13</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>14</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>15</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>16</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>17</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>18</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>Total</td>");
                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px;' align='center'>%</td></tr>");

                for (int j = 0; j < numRows; j++)
                {
                    sbCSV.Append("<tr>");

                    for (int k = 0; k < nombreColumnas.Length; k++)
                    {
                        if (dt.Rows[j][nombreColumnas[k]].ToString() != string.Empty)
                        {
                            BlReportes daReporte = new BlReportes();

                            if (nombreColumnas[k] == "TipoReunion" && dt.Rows[j][nombreColumnas[k]].ToString().Trim() == "1")
                            {
                                entro += 1;

                                if (entro == 1)
                                {
                                    cantrow = daReporte.ObtenerCantidadTipoReunion(Convert.ToInt32(dt.Rows[j][nombreColumnas[k]]), objUsuario.codigoRol);
                                    sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:60px;' rowspan='" + cantrow.ToString() + "' align='center' >");
                                    sbCSV.Append("Reuniones Individuales");
                                    sbCSV.Append("</td>");
                                }
                            }
                            else if (nombreColumnas[k] == "TipoReunion" && dt.Rows[j][nombreColumnas[k]].ToString().Trim() == "2")
                            {
                                entro2 += 1;

                                if (entro2 == 1)
                                {
                                    cantrow = daReporte.ObtenerCantidadTipoReunion(Convert.ToInt32(dt.Rows[j][nombreColumnas[k]]), objUsuario.codigoRol);
                                    sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:60px;' rowspan='" + cantrow.ToString() + "' align='center' >");
                                    sbCSV.Append("Reuniones Grupales");
                                    sbCSV.Append("</td>");
                                }
                            }
                            else if (nombreColumnas[k] == "Actividad")
                            {
                                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:250px'>");
                                sbCSV.Append(dt.Rows[j][nombreColumnas[k]].ToString().Trim());
                                sbCSV.Append("</td>");
                            }
                            else
                            {
                                sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px' align='center'>");
                                sbCSV.Append(dt.Rows[j][nombreColumnas[k]].ToString().Trim());
                                sbCSV.Append("</td>");
                            }
                        }
                        else
                        {
                            sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px' align='center'>");
                            sbCSV.Append("0");
                            sbCSV.Append("</td>");
                        }
                    }
                    sbCSV.Append("</tr>");
                }
                sbCSV.Append("</table>");
            }
            return sbCSV.ToString();
        }

        private string CastDataTableTotales(DataTable dt, string lstNomColumna, int numItems)
        {
            StringBuilder sbCSV = new StringBuilder();
            string[] nombreColumnas = lstNomColumna.Split('|');

            if (dt.Rows.Count > 0)
            {
                int numRows = dt.Rows.Count;

                if (numItems != -1 && numRows > numItems)
                {
                    numRows = numItems;
                }

                sbCSV.Append("<table width='1300' border='0' style='border-bottom:none 0px; border-top:0' cellspacing='0' cellpadding='0'>");

                for (int j = 0; j < numRows; j++)
                {
                    sbCSV.Append("<tr>");
                    sbCSV.Append("<td>&nbsp;");
                    sbCSV.Append("</td>");
                    sbCSV.Append("</tr>");
                    sbCSV.Append("<tr style='background:#CCC0DA'>");
                    sbCSV.Append("<td style='width:76px'>");
                    sbCSV.Append("</td>");
                    sbCSV.Append("<td style='width:250px;font-weight:bold'>TOTAL");
                    sbCSV.Append("</td>");
                    for (int k = 0; k < nombreColumnas.Length; k++)
                    {
                        if (dt.Rows[j][nombreColumnas[k]].ToString() != string.Empty)
                        {
                            sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px' align='center'>");
                            sbCSV.Append(dt.Rows[j][nombreColumnas[k]].ToString().Trim());
                            sbCSV.Append("</td>");
                        }
                        else
                        {
                            sbCSV.Append("<td style='border-color:Black;border-style:solid;border-width:1px;width:45px' align='center'>");
                            sbCSV.Append(" ");
                            sbCSV.Append("</td>");
                        }
                    }
                    sbCSV.Append("</tr>");
                }
                sbCSV.Append("</table>");
            }
            return sbCSV.ToString();
        }
    }
}