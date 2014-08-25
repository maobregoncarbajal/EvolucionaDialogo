
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
    using System.Web.UI.WebControls;

    public partial class PlanAccionVsResultadoNegocio : System.Web.UI.Page
    {
        #region Variables

        private readonly BlReporte reporteBL = new BlReporte();

        public static string variable;
        public static string variableTexto;
        public static string variableTextoDetalle;

        public static List<string> listaCabeceraResumen;
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
            menuReporte.Reporte4 = "ui-state-active";
            CargarPaises();
            CargarRolesValidos();
            CargarRegiones();
            CargarTipoVariable();
            CargarPeriodos();
            CargarPeriodosDetalle();
            CargarRanking(ddlRanking);
            CargarRanking(ddlRankingD);
        }

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            gvResumen.DataSource = null;
            gvResumen.Columns.Clear();
            gvResumen.DataBind();

            string pais = ddlPais.SelectedValue;
            string nivel = GetNivelByIdRol(Convert.ToInt32(ddlRoles.SelectedValue));
            string ranking = ddlRanking.SelectedValue;
            variable = ddlVariable.SelectedValue;
            string rangoInicio = txtRangoInicio.Text.Trim();
            string rangoFin = txtRangoFin.Text.Trim();
            string anho = string.IsNullOrEmpty(txtAnho.Text) ? "0" : txtAnho.Text;

            string periodo = ddlPeriodoActual.SelectedValue.Trim();
            string perioAnteior = txtPeriodoAnterior.Text;

            variableTexto = ddlVariable.SelectedItem.Text;


            DataTable dt = reporteBL.ObtenerPlanNegocio(CadenaConexion, nivel, ranking, variable, rangoInicio, rangoFin, pais, anho, periodo, perioAnteior);

            if (dt.Rows.Count != 0)
            {
                CrearColumnas(dt.Columns);

                gvResumen.DataSource = dt;
                gvResumen.DataBind();
            }
            else
            {
                string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
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

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodos();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
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

        protected void gvResumen_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView objGridView = (GridView)sender;

                GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell objtablecell = new TableCell();

                #region Merge cells

                const int inicial = 2;

                AddMergedCells(objgridviewrow, objtablecell, inicial, string.Empty, System.Drawing.Color.White.Name);

                if (variable == "0")
                {

                    foreach (string item in listaCabeceraResumen)
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 3, item, "#60497B");
                    }
                }
                else
                {
                    AddMergedCells(objgridviewrow, objtablecell, 3, variableTexto, "#60497B");
                }

                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

                #endregion Merge cells
            }
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

        protected void btnBuscarDAux_Click(object sender, EventArgs e)
        {
            gvDetalle.DataSource = null;
            gvDetalle.Columns.Clear();
            gvDetalle.DataBind();

            string periodo = ddlPeriodosD.SelectedValue;
            string pais = ddlPaisesD.SelectedValue;
            string zona = string.IsNullOrEmpty(ddlZonaD.SelectedValue) ? "0" : ddlZonaD.SelectedValue.Trim();
            string region = string.IsNullOrEmpty(ddlRegionD.SelectedValue) ? "0" : ddlRegionD.SelectedValue.Trim();
            string nombreColaborador = txtNombreColaboradorD.Text;
            string nombreJefe = txtNombreJefeD.Text;
            string nivel = GetNivelByIdRol(Convert.ToInt32(ddlRolesD.SelectedValue));
            string ranking = ddlRankingD.SelectedValue;
            variableTextoDetalle = ddlEnfoqueD.SelectedItem.Text;
            string cumplimiento = string.IsNullOrEmpty(txtCumplimientoD.Text) ? "-1" : txtCumplimientoD.Text;
            string enfoque = ddlEnfoqueD.SelectedValue;

            DataTable dt = reporteBL.ObtenerPlanNegocioDetalle(CadenaConexion, periodo, pais, region, zona,
                                                               nombreColaborador, nombreJefe, nivel, ranking,
                                                               cumplimiento, enfoque);

            if (dt.Rows.Count != 0)
            {
                CrearColumnasDetalle(dt.Columns);
                gvDetalle.DataSource = dt;
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

        protected void ddlRegionD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlZonaD.DataTextField = "Descripcion";
            ddlZonaD.DataValueField = "Codigo";
            ddlZonaD.DataSource = reporteBL.ListarZonas(ddlRegionD.SelectedValue);
            ddlZonaD.DataBind();
            ddlZonaD.Items.Insert(0, new ListItem("Todos", "0"));
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void ddlRolesD_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodosDetalle();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            GridView objGridView = (GridView)sender;

            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 5, string.Empty, System.Drawing.Color.White.Name);

            AddMergedCells(objgridviewrow, objtablecell, 4, variableTextoDetalle, "#60497B");

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell2 = new TableCell();

            AddMergedCells(objgridviewrow2, objtablecell2, 5, string.Empty, System.Drawing.Color.White.Name);

            AddMergedCells(objgridviewrow2, objtablecell2, 4, "Resultado de Negocio", "#60497B");

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow2);

            #endregion Merge cells
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string html = HdnValue.Value;
            ExportToExcel(ref html, "PlanAccionVsResultadoNegocio");
        }

        #endregion Eventos

        #region Metodos

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

        private void CargarPaises()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = new List<BePais>();

            ddlPais.DataTextField = "NombrePais";
            ddlPais.DataValueField = "prefijoIsoPais";

            ddlPaisesD.DataTextField = "NombrePais";
            ddlPaisesD.DataValueField = "prefijoIsoPais";

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    paises = paisBL.ObtenerPaises();
                    ddlPais.DataSource = paises;
                    ddlPais.DataSource = paises;
                    ddlPais.DataBind();
                    ddlPaisesD.DataSource = paises;

                    break;
                case Constantes.RolAdminPais:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    ddlPais.DataSource = paises;
                    ddlPais.DataBind();

                    ddlPaisesD.DataSource = paises;
                    break;
                case Constantes.RolAdminEvaluciona:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    ddlPais.DataSource = paises;
                    ddlPais.DataBind();

                    ddlPaisesD.DataSource = paises;
                    break;
            }

            ddlPaisesD.DataBind();
        }

        private void CargarPeriodos()
        {
            ddlPeriodoActual.Items.Clear();
            List<string> periodos = reporteBL.ObtenerPeriodos(ddlPais.SelectedValue, Convert.ToInt32(ddlRoles.SelectedValue));
            periodos.Insert(0, "-Seleccione-");
            ddlPeriodoActual.DataSource = periodos;
            ddlPeriodoActual.DataBind();
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

        private void CargarRanking(DropDownList ddlRankingControl)
        {
            List<BeComun> cuadrantes = new List<BeComun>
                                           {
                                               new BeComun {Codigo = "Critica", Descripcion = "Critica"},
                                               new BeComun {Codigo = "Estable", Descripcion = "Estable"},
                                               new BeComun {Codigo = "Productiva", Descripcion = "Productiva"}
                                           };

            ddlRankingControl.DataSource = cuadrantes;
            ddlRankingControl.DataTextField = "Descripcion";
            ddlRankingControl.DataValueField = "Codigo";
            ddlRankingControl.DataBind();
        }

        private void CargarTipoVariable()
        {
            ddlVariable.DataSource = reporteBL.ListarTipoVariables();
            ddlVariable.DataTextField = "Descripcion";
            ddlVariable.DataValueField = "Codigo";
            ddlVariable.DataBind();
            ddlVariable.Items.Insert(0, new ListItem("Todos", "0"));

            ddlEnfoqueD.DataSource = reporteBL.ListarTipoVariables();
            ddlEnfoqueD.DataTextField = "Descripcion";
            ddlEnfoqueD.DataValueField = "Codigo";
            ddlEnfoqueD.DataBind();
        }

        private void CrearColumnas(DataColumnCollection columColection)
        {
            List<BeGridColumns> columnas = new List<BeGridColumns>();

            List<string> listaCodVariables = new List<string>();

            foreach (DataColumn col in columColection)
            {
                string headerText = col.ColumnName;

                if (col.ColumnName.StartsWith("C"))
                {
                    headerText = "Cumplimiento(%)";
                }

                if (col.ColumnName.StartsWith("I"))
                {
                    listaCodVariables.Add(col.ColumnName.Substring(1));
                    headerText = "Incremento vs AA(%)";
                }

                if (col.ColumnName.StartsWith("E"))
                {
                    headerText = "Enfoque(%)";
                }

                if (col.ColumnName == "Pa")
                {
                    headerText = "País";
                }

                if (col.ColumnName == "Periodo")
                {
                    headerText = rbListResumen.SelectedItem.Text;
                }

                columnas.Add(new BeGridColumns
                {
                    DataField = col.ColumnName,
                    HederText = headerText,
                    Width = 80,
                    Visible = true
                });
            }

            gvResumen.AutoGenerateColumns = false;

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

                gvResumen.Columns.Add(columnBound);
            }

            // obtenemos los nombres de las variables a utilizar para colocarlos en la cabecera

            ListItemCollection items = ddlVariable.Items;
            listaCabeceraResumen = new List<string>();

            foreach (string codVariable in listaCodVariables)
            {
                ListItem item = items.FindByValue(codVariable);
                if (item != null)
                {
                    listaCabeceraResumen.Add(item.Text);
                }
            }
        }

        private void CrearColumnasDetalle(DataColumnCollection columColection)
        {
            List<BeGridColumns> columnas = new List<BeGridColumns>();

            foreach (DataColumn col in columColection)
            {
                switch (col.ColumnName)
                {
                    case "intIdProceso":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "intIdProceso",
                            Visible = false
                        });
                        break;

                    case "pais":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "País",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "region":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Región",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "zona":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Zona",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "chrPeriodo":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Periodo",
                            Width = 80,
                            Visible = true
                        });
                        break;
                    case "subperiodo":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Sub Periodo",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "codigoColaborador":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "codigoColaborador",
                            Width = 80,
                            Visible = false
                        });
                        break;

                    case "nivel":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Nivel",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "nombreColaborador":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Nombre Colaborador",
                            Width = 200,
                            Visible = true
                        });
                        break;

                    case "nombreJefe":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Jefe",
                            Width = 80,
                            Visible = true
                        });
                        break;
                    case "estadoPedido":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "estadoPedido",
                            Width = 80,
                            Visible = false
                        });
                        break;
                    case "codigoVariable":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "codigoVariable",
                            Width = 80,
                            Visible = false
                        });
                        break;
                    case "descCodigoVariable":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "descCodigoVariable",
                            Width = 80,
                            Visible = false
                        });
                        break;
                    case "decValorPlanPeriodo":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Objetivos",
                            Width = 80,
                            Visible = true
                        });
                        break;
                    case "decValorRealPeriodo":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Resultados",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "Plan":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Plan de acción",
                            Width = 200,
                            Visible = true
                        });
                        break;

                    case "Seguimiento":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Seguimiento",
                            Width = 80,
                            Visible = true
                        });
                        break;
                }
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
                    columnBound.ItemStyle.Width = Unit.Pixel(columna.Width);
                    columnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }

                gvDetalle.Columns.Add(columnBound);
            }
        }

        private void CargarRegiones()
        {
            ddlRegionD.DataTextField = "Descripcion";
            ddlRegionD.DataValueField = "Codigo";
            ddlRegionD.DataSource = reporteBL.ListarRegiones();
            ddlRegionD.DataBind();
            ddlRegionD.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CargarPeriodosDetalle()
        {
            ddlPeriodosD.Items.Clear();
            List<string> periodos = reporteBL.ObtenerPeriodos(ddlPaisesD.SelectedValue, Convert.ToInt32(ddlRolesD.SelectedValue));

            ddlPeriodosD.DataSource = periodos;
            ddlPeriodosD.DataBind();
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

        #endregion Metodos
    }
}