
namespace Evoluciona.Dialogo.Web.Admin.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ResultadoDialogo : Page
    {
        #region Variables

        private readonly BlReporte reporteBL = new BlReporte();
        private static List<string> headers;
        private static List<string> headersDetalle;
        private static List<BeComun> variablesUsadas;

        protected BeAdmin objAdmin;

        #endregion Variables

        #region Propiedades

        private string CadenaConexion
        {
            get
            {
                if (Session["connApp"] == null || string.IsNullOrEmpty(Session["connApp"].ToString()))
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;

                return Session["connApp"].ToString();
            }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (Page.IsPostBack) return;
            CargarCombos();
            CargarRolesValidos();
            CargarTipoVariable();
            CargarPeriodos();
            CargarPeriodosDetalle();
            menuReporte.Reporte3 = "ui-state-active";
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodos();
            lblMensaje.Visible = false;
            lblMensaje.Text = string.Empty;
            txtPeriodoAnterior.Text = string.Empty;
            rbListResumen.SelectedIndex = 0;
            ddlPeriodoActual.Enabled = false;
            txtAnho.Enabled = true;

            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string html = HdnValue.Value;
            ExportToExcel(ref html, "ResultadoDialogo");
        }

        protected void gvResumen_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gvResumen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int j = 0; j < e.Row.Cells.Count; j++)
            {
                e.Row.Cells[j].Style.Add("BORDER-BOTTOM", "white 1px solid");
                e.Row.Cells[j].Style.Add("BORDER-TOP", "white 1px solid");
                e.Row.Cells[j].Style.Add("BORDER-RIGHT", "white 1px solid");
                e.Row.Cells[j].Style.Add("padding-left", "5px");
                e.Row.Cells[j].Style.Add("vertical-align", "middle");
            }
        }

        protected void gvResumenPais_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            GridView objGridView = (GridView)sender;

            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2, string.Empty, System.Drawing.Color.White.Name);

            foreach (string header in headers)
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, header, "#60497B");
            }

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrowH = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecellH = new TableCell();
            AddMergedCells(objgridviewrowH, objtablecellH, 2, string.Empty, System.Drawing.Color.White.Name);
            AddMergedCells(objgridviewrowH, objtablecellH, headers.Count * 2, "BRECHA POR VARIABLES", "#60497B");
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrowH);

            #endregion Merge cells
        }

        protected void ddlPeriodoActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            lblMensaje.Text = string.Empty;
            txtPeriodoAnterior.Text = string.Empty;

            if (ddlPeriodoActual.SelectedIndex == 0) return;
            string[] periodo = ddlPeriodoActual.SelectedItem.Text.Trim().Split(' ');
            int anho = Convert.ToInt32(periodo[0]);
            anho = anho - 1;
            string periodoAnterior = anho.ToString() + " " + periodo[1];

            ListItem item = ddlPeriodoActual.Items.FindByText(periodoAnterior);

            if (item == null)
            {
                lblMensaje.Text = "El período anterior no existe. Solo se mostrarán datos del período seleccionado.";
                lblMensaje.Visible = true;
            }
            else
            {
                txtPeriodoAnterior.Text = periodoAnterior;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
        }

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            gvResumen.DataSource = null;
            gvResumen.Columns.Clear();
            gvResumen.DataBind();

            gvResumenPais.DataSource = null;
            gvResumenPais.Columns.Clear();
            gvResumenPais.DataBind();

            string anho = string.IsNullOrEmpty(txtAnho.Text) ? "0" : txtAnho.Text;
            string nivel = GetNivelByIdRol(Convert.ToInt32(ddlRoles.SelectedValue));
            string pais = ddlPaises.SelectedValue;
            string periodo = ddlPeriodoActual.SelectedValue.Trim();
            string perioAnteior = txtPeriodoAnterior.Text;

            DataTable dt = new DataTable();
            DataTable dtPais = new DataTable();

            dt = reporteBL.ResultadoDialogo(CadenaConexion, anho, periodo, perioAnteior, nivel, pais, "dialogo");

            if (dt.Rows.Count != 0)
            {
                CrearColumnas(dt.Columns);
                gvResumen.DataSource = dt;
                gvResumen.DataBind();

                dtPais = reporteBL.ResultadoDialogo(CadenaConexion, anho, periodo, perioAnteior, nivel, pais, "dialogoPaises");
                CrearColumnasPais(dtPais.Columns);

                gvResumenPais.DataSource = dtPais;
                gvResumenPais.DataBind();
            }
            else
            {
                const string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose(1);", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            lblMensaje.Visible = false;

            if (rbListResumen.Items[0].Selected && string.IsNullOrEmpty(txtAnho.Text))
            {
                lblMensaje.Text = "Ingrese un año";
                lblMensaje.Visible = true;
                return;
            }

            if (rbListResumen.Items[1].Selected && ddlPeriodoActual.SelectedIndex == 0)
            {
                lblMensaje.Text = "Seleccione un período";
                lblMensaje.Visible = true;
                return;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(1);", true);
        }

        protected void ddlRegionD_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlReporte blReportedata = new BlReporte();

            ddlZonaD.DataTextField = "Descripcion";
            ddlZonaD.DataValueField = "Codigo";
            ddlZonaD.DataSource = blReportedata.ListarZonas(ddlRegionD.SelectedValue);
            ddlZonaD.DataBind();
            ddlZonaD.Items.Insert(0, new ListItem("Todos", "0"));
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void ddlPaisesD_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodosDetalle();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void btnBuscarDAux_Click(object sender, EventArgs e)
        {
            gvDetalle.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            gvDetalle.DataSource = null;
            gvDetalle.Columns.Clear();
            gvDetalle.DataBind();

            string periodo = ddlPeriodosD.SelectedItem.Value;
            string pais = ddlPaisesD.SelectedValue;
            string zona = string.IsNullOrEmpty(ddlZonaD.SelectedValue) ? "0" : ddlZonaD.SelectedValue.Trim();
            string region = string.IsNullOrEmpty(ddlRegionD.SelectedValue) ? "0" : ddlRegionD.SelectedValue.Trim();
            string varialbe = ddlVariableD.SelectedValue;
            string tamBrecha = string.IsNullOrEmpty(txtBrechaTamD.Text) ? "-1" : (Convert.ToDecimal(txtBrechaTamD.Text) / 100).ToString();
            string nivel = GetNivelByIdRol(Convert.ToInt32(ddlRolesD.SelectedValue)).ToUpper();

            DataTable dtDetalle = reporteBL.ResultadoDialogoDetalle(CadenaConexion, periodo, pais, region.Trim(), zona.Trim(), nivel.Trim(), varialbe.Trim(), tamBrecha.Trim());

            if (dtDetalle.Rows.Count != 0)
            {
                CrearColumnasDetalle(dtDetalle.Columns);

                gvDetalle.DataSource = dtDetalle;
                gvDetalle.DataBind();
            }
            else
            {
                const string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose(2);", true);
        }

        protected void btnBuscarD_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(2);", true);
        }

        protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;
            GridView objGridView = (GridView)sender;

            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 6, string.Empty, System.Drawing.Color.White.Name);

            foreach (string header in headersDetalle)
            {
                AddMergedCells(objgridviewrow, objtablecell, 3, header, "#60497B");
            }

            GridViewRow objgridviewrowH = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecellH = new TableCell();
            AddMergedCells(objgridviewrowH, objtablecellH, 6, string.Empty, System.Drawing.Color.White.Name);
            AddMergedCells(objgridviewrowH, objtablecellH, headersDetalle.Count * 3, "BRECHA POR VARIABLES", "#60497B");
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrowH);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow);

            #endregion Merge cells
        }

        protected void rbListResumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAnho.Text = string.Empty;
            txtPeriodoAnterior.Text = string.Empty;
            lblMensaje.Text = string.Empty;

            if (rbListResumen.SelectedValue == "Anho")
            {
                txtAnho.Enabled = true;
                ddlPeriodoActual.Enabled = false;
                ddlPeriodoActual.SelectedIndex = 0;
            }
            else
            {
                txtAnho.Enabled = false;
                ddlPeriodoActual.Enabled = true;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
        }

        #endregion Eventos

        #region Metodos

        private void CargarPeriodos()
        {
            ddlPeriodoActual.Items.Clear();
            List<string> periodos = reporteBL.ObtenerPeriodos(ddlPaises.SelectedValue, Convert.ToInt32(ddlRoles.SelectedValue));
            periodos.Insert(0, "-Seleccione-");
            ddlPeriodoActual.DataSource = periodos;
            ddlPeriodoActual.DataBind();
        }

        private void CargarPeriodosDetalle()
        {
            ddlPeriodosD.Items.Clear();
            List<string> periodos = reporteBL.ObtenerPeriodos(ddlPaisesD.SelectedValue, Convert.ToInt32(ddlRolesD.SelectedValue));

            ddlPeriodosD.DataSource = periodos;
            ddlPeriodosD.DataBind();
        }

        private void CargarCombos()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = new List<BePais>();

            ddlPaises.DataTextField = "NombrePais";
            ddlPaises.DataValueField = "prefijoIsoPais";

            ddlPaisesD.DataTextField = "NombrePais";
            ddlPaisesD.DataValueField = "prefijoIsoPais";

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    paises = paisBL.ObtenerPaises();
                    ddlPaises.DataSource = paises;
                    ddlPaisesD.DataSource = paises;
                    ddlPaises.DataBind();
                    ddlPaisesD.DataBind();
                    break;

                case Constantes.RolAdminPais:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    ddlPaises.DataSource = paises;
                    ddlPaises.DataBind();
                    ddlPaisesD.DataSource = paises;
                    ddlPaisesD.DataBind();
                    break;

                case Constantes.RolAdminEvaluciona:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    ddlPaises.DataSource = paises;
                    ddlPaises.DataBind();
                    ddlPaisesD.DataSource = paises;
                    ddlPaisesD.DataBind();
                    break;
            }

            BlReporte blReportedata = new BlReporte();

            ddlRegionD.DataTextField = "Descripcion";
            ddlRegionD.DataValueField = "Codigo";
            ddlRegionD.DataSource = blReportedata.ListarRegiones();
            ddlRegionD.DataBind();
            ddlRegionD.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CargarRolesValidos()
        {
            BlRol rolBL = new BlRol();

            var roles = rolBL.ObtenerRolesSubordinados(1);
            var subRoles = new List<BeRol>();

            foreach (var beRol in roles)
            {
                if (beRol.CodigoRol != 4)
                    subRoles.Add(beRol);
            }

            ddlRoles.DataSource = subRoles;
            ddlRoles.DataTextField = "Descripcion";
            ddlRoles.DataValueField = "CodigoRol";
            ddlRoles.DataBind();

            ddlRolesD.DataSource = subRoles;
            ddlRolesD.DataTextField = "Descripcion";
            ddlRolesD.DataValueField = "CodigoRol";
            ddlRolesD.DataBind();
        }

        private string GetNivelByIdRol(int idRol)
        {
            string nivel = string.Empty;

            if (idRol == 4)
            {
                nivel = "dv";
            }

            if (idRol == 5)
            {
                nivel = "gr";
            }

            if (idRol == 6)
            {
                nivel = "gz";
            }

            return nivel;
        }

        private void CargarTipoVariable()
        {
            ddlVariableD.DataSource = reporteBL.ListarTipoVariables();
            ddlVariableD.DataTextField = "Descripcion";
            ddlVariableD.DataValueField = "Codigo";
            ddlVariableD.DataBind();
            ddlVariableD.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
        {
            objtablecell = new TableCell { Text = celltext, ColumnSpan = colspan };
            objtablecell.Style.Add("background-color", backcolor);
            objtablecell.Style.Add("border-color", "white");
            objtablecell.Style.Add("BORDER-BOTTOM", "white 1px solid");
            objtablecell.Style.Add("BORDER-TOP", "white 1px solid");
            objtablecell.Style.Add("BORDER-RIGHT", "white 1px solid");
            objtablecell.Style.Add("CellPadding", "3");
            objtablecell.Style.Add("CellSpacing", "1");
            objtablecell.HorizontalAlign = HorizontalAlign.Center;
            objtablecell.VerticalAlign = VerticalAlign.Middle;
            objgridviewrow.Cells.Add(objtablecell);
        }

        public void ExportToExcel(ref string html, string fileName)
        {
            html = html.Replace("&gt;", ">");
            html = html.Replace("&lt;", "<");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
        }

        private void CrearColumnas(DataColumnCollection columColection)
        {
            List<BeGridColumns> columnas = new List<BeGridColumns>();

            foreach (DataColumn col in columColection)
            {
                string colName = col.ColumnName;
                string formato = string.Empty;
                switch (col.ColumnName)
                {
                    case "Periodo":
                        colName = rbListResumen.SelectedItem.Text;
                        break;

                    case "XPais":
                        colName = "País";
                        break;

                    case "Cod":
                        colName = "Variables de Negocio";
                        break;

                    case "Cump":
                        colName = "FFVV que cumplió con objetivos(%)";
                        break;

                    case "Brecha":
                        colName = "FFVV con brecha (sumatoria de brechas)(%)";
                        break;

                    case "Prom":
                        colName = "Tamaño de brecha promedio";
                        break;

                    case "PuntProm":
                        colName = "Puntaje promedio";
                        break;

                    case "Avance":
                        colName = "Avance brecha vs. AA(%)";
                        break;
                }

                columnas.Add(new BeGridColumns
                {
                    DataField = col.ColumnName,
                    HederText = colName,
                    Width = 100,
                    Visible = true,
                    Formato = formato
                });
            }

            gvResumen.AutoGenerateColumns = false;

            foreach (BeGridColumns columna in columnas)
            {
                BoundField columnBound = new BoundField
                {
                    DataField = columna.DataField,
                    HeaderText = columna.HederText,
                    DataFormatString = columna.Formato
                };

                columnBound.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                columnBound.Visible = columna.Visible;

                if (columna.Width != 0)
                {
                    columnBound.ItemStyle.Width = Unit.Pixel(columna.Width);
                    columnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }

                gvResumen.Columns.Add(columnBound);
            }
        }

        private void CrearColumnasPais(DataColumnCollection columColection)
        {
            List<BeComun> variables = reporteBL.ListarTipoVariables();
            headers = new List<string>();
            List<BeGridColumns> columnas = new List<BeGridColumns>();

            foreach (DataColumn col in columColection)
            {
                string columnaNombre = col.ColumnName;
                if (!col.ColumnName.StartsWith("X") && !col.ColumnName.StartsWith("A") && !col.ColumnName.StartsWith("P") && !col.ColumnName.StartsWith("E"))
                {
                    BeComun variable = variables.Find(p => p.Codigo == col.ColumnName.Substring(1, col.ColumnName.Length - 1));

                    if (variable != null)
                    {
                        headers.Add(variable.Descripcion);
                    }
                }

                if (col.ColumnName.StartsWith("X"))
                {
                    columnaNombre = "País";
                }

                if (col.ColumnName.StartsWith("A"))
                {
                    columnaNombre = rbListResumen.SelectedItem.Text;
                }

                if (col.ColumnName.StartsWith("B"))
                {
                    columnaNombre = "FFVV con brecha(%)";
                }

                if (col.ColumnName.StartsWith("P"))
                {
                    columnaNombre = "Tamaño de brecha promedio";
                }

                if (col.ColumnName == "E1")
                {
                    columnaNombre = "Enfoque E1";
                }

                if (col.ColumnName == "E2")
                {
                    columnaNombre = "Enfoque E2";
                }

                columnas.Add(new BeGridColumns
                {
                    DataField = col.ColumnName,
                    HederText = columnaNombre,
                    Width = 80,
                    Visible = true
                });
            }

            gvResumenPais.AutoGenerateColumns = false;

            foreach (BeGridColumns columna in columnas)
            {
                BoundField columnBound = new BoundField
                {
                    DataField = columna.DataField,
                    HeaderText = columna.HederText
                };

                columnBound.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                columnBound.Visible = columna.Visible;

                if (columna.Width != 0)
                {
                    columnBound.ItemStyle.Width = Unit.Pixel(columna.Width);
                    columnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }

                gvResumenPais.Columns.Add(columnBound);
            }
        }

        private void CrearColumnasDetalle(DataColumnCollection columColection)
        {
            List<BeComun> variables = reporteBL.ListarTipoVariables();
            headersDetalle = new List<string>();
            List<BeGridColumns> columnas = new List<BeGridColumns>();

            //Variables Usadas
            variablesUsadas = new List<BeComun>();

            foreach (DataColumn col in columColection)
            {
                string columnaNombre = col.ColumnName;
                bool visbleState = true;
                int width = 100;

                if (!col.ColumnName.StartsWith("C") && !col.ColumnName.StartsWith("Z") && !col.ColumnName.StartsWith("R") && !col.ColumnName.StartsWith("A") && !col.ColumnName.StartsWith("P"))
                {
                    BeComun variable = variables.Find(p => p.Codigo == col.ColumnName.Substring(1, col.ColumnName.Length - 1));

                    if (variable != null)
                    {
                        headersDetalle.Add(variable.Descripcion);
                        variablesUsadas.Add(variable);
                    }
                }

                string value = col.ColumnName.Substring(0, 1);

                switch (value)
                {
                    case "I":
                        columnaNombre = "IdProceso";
                        visbleState = false;
                        break;
                    case "T":
                        columnaNombre = "Periodo";
                        break;
                    case "X":
                        columnaNombre = "País";
                        break;
                    case "N":
                        columnaNombre = "Nivel  Jerárquico";
                        break;
                    case "B":
                        columnaNombre = "Brecha(%)";
                        break;
                    case "R":
                        columnaNombre = "Región";
                        break;
                    case "P":
                        columnaNombre = "Plan de Acción";
                        width = 200;
                        break;
                    case "W":
                        columnaNombre = "Nombre Colaborador";
                        width = 200;
                        break;
                    case "C":
                        columnaNombre = "Variable Causa";
                        break;
                    case "E":
                        columnaNombre = "Enfoque 1";
                        break;
                    case "F":
                        columnaNombre = "Enfoque 2";
                        break;
                }

                columnas.Add(new BeGridColumns
                {
                    DataField = col.ColumnName,
                    HederText = columnaNombre,
                    Width = width,
                    Visible = visbleState
                });
            }

            gvDetalle.AutoGenerateColumns = false;

            foreach (BeGridColumns columna in columnas)
            {
                BoundField columnBound = new BoundField
                {
                    DataField = columna.DataField,
                    HeaderText = columna.HederText
                };

                columnBound.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                columnBound.Visible = columna.Visible;

                if (columna.Width != 0)
                {
                    if (columna.HederText == "Brecha(%)")
                        columnBound.DataFormatString = "{0:P2}";

                    columnBound.ItemStyle.Width = Unit.Pixel(columna.Width);
                    columnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }

                gvDetalle.Columns.Add(columnBound);
            }
        }

        #endregion Metodos
    }
}