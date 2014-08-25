
namespace Evoluciona.Dialogo.Web.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Microsoft.Reporting.WebForms;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.DataVisualization.Charting;
    using System.Web.UI.WebControls;
    using WsPlanDesarrollo;

    public partial class RankingProductividad : Page
    {
        #region Variables

        private readonly BlReporte reporteBL = new BlReporte();

        protected BeUsuario objUsuario;

        private static int numCompetencia;

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

            CargarVariables();

            if (IsPostBack) { return; }

            CargarPaises();
            CargarRolesValidos();
            CargarCuadrante();
            CargarPeriodos();
            CargarPeriodosDetalle();
            CargarRegiones();
            CargarCampañasFiltro();
            menuReporte.Reporte2 = "ui-state-active";
            ChartPeriodo.Visible = false;
            ChartCampanha.Visible = false;
            rpvResumen.LocalReport.EnableExternalImages = true;
            GenerarControlReport(null);
            txtNombreJefeD.Text = objUsuario.nombreUsuario;
            txtNombreJefeD.Enabled = false;

            // Eliminar Archivos Anteriores de Reportes
            foreach (string file in Directory.GetFiles(Server.MapPath(@"~/Charts/")))
            {
                FileInfo theFile = new FileInfo(file);

                if (DateTime.Parse(theFile.CreationTime.ToShortDateString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    File.Delete(file);
                }
            }
        }

        protected void cboPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCampañasFiltro();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
        }

        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodos();
            CargarRolesValidos();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
        }

        protected void cboPaisesD_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodosDetalle();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            gvResumen.Visible = true;
            ChartCampanha.Visible = true;
            ChartPeriodo.Visible = true;
            string pais = cboPaises.SelectedValue.Trim();
            int rol = Convert.ToInt32(cboRoles.SelectedValue);
            string nivel = GetNivelByIdRol(rol);
            string periodo = cboPeriodos.SelectedValue.Trim();
            string campanha = cboCampanhasFiltro.SelectedValue.Trim();

            const string estado1 = "Critica";
            const string estado2 = "Estable";
            const string estado3 = "Productiva";

            gvResumen.Columns[3].HeaderText = estado1;
            gvResumen.Columns[4].HeaderText = estado2;
            gvResumen.Columns[5].HeaderText = estado3;

            List<BeAnalisisStatusRanking> entidades = reporteBL.ListarAnalisisStatusRankingUsuario(pais, nivel, periodo, campanha, estado1, estado2, estado3, objUsuario.codigoUsuario, GetNivelByIdRol(objUsuario.codigoRol));

            if (entidades.Count != 0)
            {
                List<BeChartCampanha> charts = reporteBL.ListarChartCampanhaUsuario(pais, nivel, periodo, campanha, estado1, estado2, estado3, objUsuario.codigoUsuario, GetNivelByIdRol(objUsuario.codigoRol));

                ChartPeriodo.Visible = true;

                ChartPeriodo.Series["Estado1"].Points.Clear();
                ChartPeriodo.Series["Estado2"].Points.Clear();
                ChartPeriodo.Series["Estado3"].Points.Clear();

                ChartCampanha.Visible = true;

                ChartCampanha.Series["Estado1"].Points.Clear();
                ChartCampanha.Series["Estado2"].Points.Clear();
                ChartCampanha.Series["Estado3"].Points.Clear();

                decimal totalEstado1 = 0;
                decimal totalEstado2 = 0;
                decimal totalEstado3 = 0;

                int cantidad = 0;

                foreach (BeAnalisisStatusRanking entidad in entidades)
                {
                    cantidad = cantidad + entidad.Colaboradores;
                    totalEstado1 += entidad.Estado1;
                    totalEstado2 += entidad.Estado2;
                    totalEstado3 += entidad.Estado3;
                }

                CargarChartPeriodo(estado1, estado2, estado3, totalEstado1, totalEstado2, totalEstado3);

                gvResumen.Columns[0].FooterText = "Belcorp";
                gvResumen.Columns[2].FooterText = cantidad.ToString();
                gvResumen.DataSource = entidades;
                gvResumen.DataBind();

                CargarChartCampanha(charts);
                GenerarControlReport(entidades);
            }
            else
            {
                gvResumen.Visible = false;
                ChartCampanha.Visible = false;
                ChartPeriodo.Visible = false;
                const string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose(1);", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(1);", true);
        }

        protected void gvResumen_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            GridView objGridView = (GridView)sender;

            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 3, string.Empty, System.Drawing.Color.White.Name);

            AddMergedCells(objgridviewrow, objtablecell, 3, "% DIstribución  FFVV por cuadrante", "#60497B");

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion Merge cells
        }

        protected void cboRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPeriodos_SelectedIndexChanged(sender, e);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportToExcelResumen();
        }

        protected void btnBuscarD_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(2);", true);
        }

        protected void btnBuscarDAux_Click(object sender, EventArgs e)
        {
            gvDetalle.DataSource = null;
            gvDetalle.Columns.Clear();
            gvDetalle.DataBind();

            string periodo = cboPeriodosD.SelectedValue;
            string anhio = periodo.Substring(0, 4);
            string nombreColaborador = txtNombreColaboradorD.Text;
            string nivel = GetNivelByIdRol(Convert.ToInt32(cboRolesD.SelectedValue));
            string codPais = cboPaisesD.SelectedValue;
            string nombreJefe = txtNombreJefeD.Text;
            string estado = cboCuadranteD.Text;
            string zona = string.IsNullOrEmpty(cboZonaD.SelectedValue) ? "0" : cboZonaD.SelectedValue.Trim();
            string region = string.IsNullOrEmpty(cboRegionD.SelectedValue) ? "0" : cboRegionD.SelectedValue.Trim();

            DataTable dt = reporteBL.ObtenerVariablesNegocioUsuario(CadenaConexion, periodo, nombreColaborador, nivel, zona, codPais, nombreJefe, estado, region, objUsuario.codigoUsuario, GetNivelByIdRol(objUsuario.codigoRol));

            // obtenemos los nombres de las columnas para las competencias a partir del 1 elemento
            if (dt.Rows.Count != 0)
            {
                DataTable dtColumnsComp = CargarMedicionCompetencias(Convert.ToInt32(dt.Rows[0]["intIdProceso"]),
                                                                     codPais, anhio,
                    //(string) dt.Rows[0]["codigoColaborador"]);
                                                                     (string)dt.Rows[0]["codigoColaborador"], (string)dt.Rows[0]["cub"]);

                numCompetencia = dtColumnsComp.Rows.Count;

                foreach (DataRow rowColComp in dtColumnsComp.Rows)
                {
                    dt.Columns.Add((string)rowColComp["vchCompetencia"], typeof(string));
                }

                dt.Columns.Add("EnfoqueComp", typeof(string));
                //  dt.Columns.Add("PlanAccionComp", typeof(string));

                // Agegamos las competencias apartir del id del proceso y el documento de identidad del colaborador

                foreach (DataRow row in dt.Rows)
                {
                    int idProceso = (int)row["intIdProceso"];
                    string codUsuario = (string)row["codigoColaborador"];
                    string cub = (string)row["cub"];

                    //DataTable dtCompetencia = CargarMedicionCompetencias(idProceso, codPais, anhio, codUsuario);
                    DataTable dtCompetencia = CargarMedicionCompetencias(idProceso, codPais, anhio, codUsuario, cub);

                    foreach (DataRow rowC in dtCompetencia.Rows)
                    {
                        if (rowC["Enfoque"].ToString().ToUpper() == "TRUE")
                        {
                            row["EnfoqueComp"] = rowC["vchCompetencia"];
                            // row["PlanAccionComp"] = rowC["Sugerencia"];
                        }

                        foreach (DataColumn cols in dt.Columns)
                        {
                            if (cols.ColumnName == (string)rowC["vchCompetencia"])
                            {
                                row[cols.ColumnName] = rowC["PorcentajeAvance"];
                            }
                        }
                    }
                }

                DataColumnCollection columColection = dt.Columns;

                CrearColumnas(columColection);
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

        protected void cboRegionD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboZonaD.Items.Clear();
            cboZonaD.DataTextField = "Descripcion";
            cboZonaD.DataValueField = "Codigo";
            cboZonaD.DataSource = reporteBL.ListarZonas(cboRegionD.SelectedValue);
            cboZonaD.DataBind();
            cboZonaD.Items.Insert(0, new ListItem("Todos", "0"));
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana();", true);
        }

        protected void cboRolesD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboRolesD.SelectedValue) == (objUsuario.codigoRol + 1))
            {
                txtNombreJefeD.Text = objUsuario.nombreUsuario;
                txtNombreJefeD.Enabled = false;
            }
            else
            {
                txtNombreJefeD.Text = string.Empty;
                txtNombreJefeD.Enabled = true;
            }

            CargarCuadrante();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        public void ExportToExcelResumen()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = rpvResumen.LocalReport.Render(
               "Excel", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            string filename = string.Format("{0}.{1}", "RankingProductividad" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), "xls");
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            GridView objGridView = (GridView)sender;

            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell objtablecell = new TableCell();

            #region Merge cells

            const int colsDato = 5;
            const int colsOcultos = 2;
            const int colsEnfoques = 5;
            const int colsCompetenciaAdd = 1;

            int colsVariableNegocio = objGridView.Columns.Count - (colsDato + colsOcultos + numCompetencia + colsEnfoques + colsCompetenciaAdd);

            AddMergedCells(objgridviewrow, objtablecell, colsDato, string.Empty, System.Drawing.Color.White.Name);

            if (colsVariableNegocio > 0)
                AddMergedCells(objgridviewrow, objtablecell, colsVariableNegocio, "Resultado por variable <br/>de Negocio(%logro)", "#60497B");

            AddMergedCells(objgridviewrow, objtablecell, colsEnfoques, string.Empty, System.Drawing.Color.White.Name);

            if (numCompetencia > 0)
                AddMergedCells(objgridviewrow, objtablecell, numCompetencia, "Resultado por Competencias <br/>(% de Desarrollo)", "#60497B");

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion Merge cells
        }

        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void btnExportarExcelDetalle_Click(object sender, EventArgs e)
        {
            string html = HdnValue.Value;
            ExportToExcel(ref html, "RankingProductividad");
        }
        #endregion Eventos

        #region Metodos

        private void CargarVariables()
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
        }

        private void CargarChartPeriodo(string estado1, string estado2, string estado3, decimal porcentajeEstado1, decimal porcentajeEstado2, decimal porcentajeEstado3)
        {
            ChartPeriodo.Series["Estado1"].Points.AddY(porcentajeEstado3);
            ChartPeriodo.Series["Estado2"].Points.AddY(porcentajeEstado2);
            ChartPeriodo.Series["Estado3"].Points.AddY(porcentajeEstado1);

            ChartPeriodo.Series["Estado1"].LabelFormat = "{#}%";
            ChartPeriodo.Series["Estado2"].LabelFormat = "{#}%";
            ChartPeriodo.Series["Estado3"].LabelFormat = "{#}%";

            ChartPeriodo.Series["Estado1"].LabelForeColor = System.Drawing.Color.White;

            ChartPeriodo.Series["Estado3"].LabelForeColor = System.Drawing.Color.White;

            ChartPeriodo.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "{#}%";

            // en el diagrama los estados 1 y 3 se invierten
            ChartPeriodo.Series["Estado1"].LegendText = estado3;
            ChartPeriodo.Series["Estado2"].LegendText = estado2;
            ChartPeriodo.Series["Estado3"].LegendText = estado1;

            ChartPeriodo.Series["Estado1"].IsValueShownAsLabel = true;
            ChartPeriodo.Series["Estado3"].IsValueShownAsLabel = true;
            ChartPeriodo.Series["Estado2"].IsValueShownAsLabel = true;

            ChartPeriodo.Series["Estado1"].Points[0].AxisLabel = cboPeriodos.SelectedValue;
        }

        private void CargarChartCampanha(List<BeChartCampanha> charts)
        {
            int index = 0;

            foreach (BeChartCampanha chart in charts)
            {
                ChartCampanha.Series["Estado1"].Points.AddY(chart.Estado3);
                ChartCampanha.Series["Estado2"].Points.AddY(chart.Estado2);
                ChartCampanha.Series["Estado3"].Points.AddY(chart.Estado1);
                ChartCampanha.Series["Estado1"].Points[index].AxisLabel = chart.Campanha;
                index++;
            }

            ChartCampanha.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            ChartCampanha.Series["Estado1"].IsValueShownAsLabel = true;

            ChartCampanha.Series["Estado1"].IsValueShownAsLabel = true;
            ChartCampanha.Series["Estado3"].IsValueShownAsLabel = true;
            ChartCampanha.Series["Estado2"].IsValueShownAsLabel = true;

            ChartCampanha.Series["Estado1"].LabelFormat = "{#}%";
            ChartCampanha.Series["Estado2"].LabelFormat = "{#}%";
            ChartCampanha.Series["Estado3"].LabelFormat = "{#}%";

            ChartCampanha.Series["Estado1"].LabelForeColor = System.Drawing.Color.White;

            ChartCampanha.Series["Estado3"].LabelForeColor = System.Drawing.Color.White;

            ChartCampanha.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "{#}%";
        }

        private void CargarPeriodos()
        {
            cboPeriodos.Items.Clear();
            List<string> periodos = reporteBL.ObtenerPeriodos(cboPaises.SelectedValue, Convert.ToInt32(cboRoles.SelectedValue));

            cboPeriodos.DataSource = periodos;
            cboPeriodos.DataBind();
        }

        private void CargarPeriodosDetalle()
        {
            cboPeriodosD.Items.Clear();
            List<string> periodos = reporteBL.ObtenerPeriodos(cboPaisesD.SelectedValue, Convert.ToInt32(cboRolesD.SelectedValue));

            cboPeriodosD.DataSource = periodos;
            cboPeriodosD.DataBind();
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

        private void CargarCampañasFiltro()
        {
            if (string.IsNullOrEmpty(cboPeriodos.SelectedValue))
            {
                cboCampanhasFiltro.DataSource = null;
                cboCampanhasFiltro.DataBind();
                cboCampanhasFiltro.Items.Insert(0, new ListItem("[TODOS]", "0"));
                return;
            }

            cboCampanhasFiltro.Items.Clear();
            cboCampanhasFiltro.DataSource = reporteBL.ObtenerListaCampana(string.Empty, Convert.ToInt32(cboRoles.SelectedValue),
            cboPaises.SelectedValue, cboPeriodos.SelectedValue, CadenaConexion);

            cboCampanhasFiltro.DataTextField = "chrAnioCampana";
            cboCampanhasFiltro.DataValueField = "chrAnioCampana";
            cboCampanhasFiltro.DataBind();

            cboCampanhasFiltro.Items.Insert(0, new ListItem("[TODOS]", "0"));
        }

        private void CargarPaises()
        {
            cboPaises.Items.Clear();
            cboPaisesD.Items.Clear();

            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";

            cboPaisesD.DataTextField = "NombrePais";
            cboPaisesD.DataValueField = "prefijoIsoPais";

            string nivelEvaluador = GetNivelByIdRol(objUsuario.codigoRol);
            List<BePais> paises = reporteBL.ObtenerPaisesUsuario(nivelEvaluador, objUsuario.codigoUsuario, objUsuario.prefijoIsoPais);
            cboPaises.DataSource = paises;
            cboPaises.DataBind();
            cboPaisesD.DataSource = paises;
            cboPaisesD.DataBind();
        }

        private void CargarRegiones()
        {
            cboRegionD.DataTextField = "Descripcion";
            cboRegionD.DataValueField = "Codigo";
            cboRegionD.DataSource = reporteBL.ListarRegiones();
            cboRegionD.DataBind();
            cboRegionD.Items.Insert(0, new ListItem("Todos", "0"));
        }

        private void CargarRolesValidos()
        {
            BlRol rolBL = new BlRol();
            List<BeRol> roles = rolBL.ObtenerRolesSubordinados(objUsuario.codigoRol);
            cboRoles.DataSource = roles;
            cboRoles.DataTextField = "Descripcion";
            cboRoles.DataValueField = "CodigoRol";
            cboRoles.DataBind();

            cboRolesD.DataSource = roles;
            cboRolesD.DataTextField = "Descripcion";
            cboRolesD.DataValueField = "CodigoRol";
            cboRolesD.DataBind();
        }

        protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
        {
            objtablecell = new TableCell { Text = celltext, ColumnSpan = colspan };
            objtablecell.Style.Add("background-color", backcolor);
            objtablecell.Style.Add("border-color", backcolor);
            objtablecell.HorizontalAlign = HorizontalAlign.Center;
            objtablecell.VerticalAlign = VerticalAlign.Middle;
            objgridviewrow.Cells.Add(objtablecell);
        }

        private void CargarCuadrante()
        {
            List<BeComun> cuadrantes = new List<BeComun>
                                           {
                                               new BeComun {Codigo = "Critica", Descripcion = "Critica"},
                                               new BeComun {Codigo = "Estable", Descripcion = "Estable"},
                                               new BeComun {Codigo = "Productiva", Descripcion = "Productiva"}
                                           };

            cboCuadranteD.DataSource = cuadrantes;
            cboCuadranteD.DataTextField = "Descripcion";
            cboCuadranteD.DataValueField = "Codigo";
            cboCuadranteD.DataBind();
        }
        private void GenerarControlReport(List<BeAnalisisStatusRanking> entidades)
        {
            string strPeridoFilePath = string.Empty;
            string strCampanhaFilePath = string.Empty;
            ReportDataSource source;

            if (ChartPeriodo.Visible)
            {
                strPeridoFilePath = Path.Combine("Charts", "ChartPeriodo" + Guid.NewGuid().ToString() + ".png");
                ChartPeriodo.SaveImage(Server.MapPath(@"~/" + strPeridoFilePath), ChartImageFormat.Png);
            }

            if (ChartCampanha.Visible)
            {
                strCampanhaFilePath = Path.Combine("Charts", "ChartCampanha" + Guid.NewGuid().ToString() + ".png");
                ChartCampanha.SaveImage(Server.MapPath(@"~/" + strCampanhaFilePath), ChartImageFormat.Png);
            }

            rpvResumen.LocalReport.DataSources.Clear();

            if (entidades == null)
            {
                List<BeAnalisisStatusRanking> byDefault = new List<BeAnalisisStatusRanking>();
                source = new ReportDataSource("beAnalisisStatusRanking", byDefault);
            }
            else
            {
                source = new ReportDataSource("beAnalisisStatusRanking", entidades);
            }

            rpvResumen.LocalReport.DataSources.Add(source);

            string pais = cboPaises.SelectedItem.Text.Trim();
            string rol = cboRoles.SelectedItem.Text;
            string periodo = cboPeriodos.SelectedValue.Trim();
            string campanha = cboCampanhasFiltro.SelectedItem.Text.Trim();

            const string estado1 = "Critica";
            const string estado2 = "Estable";
            const string estado3 = "Productiva";

            List<ReportParameter> parametros = new List<ReportParameter>();
            string rutaPeriodo = "file:///" + Server.MapPath(@"~/" + strPeridoFilePath);
            string rutaCampanha = "file:///" + Server.MapPath(@"~/" + strCampanhaFilePath);

            parametros.Add(new ReportParameter("PathCampanha", rutaCampanha));
            parametros.Add(new ReportParameter("PathPeriodo", rutaPeriodo));
            parametros.Add(new ReportParameter("Estado1", estado1));
            parametros.Add(new ReportParameter("Estado2", estado2));
            parametros.Add(new ReportParameter("Estado3", estado3));
            parametros.Add(new ReportParameter("Pais", pais));
            parametros.Add(new ReportParameter("Rol", rol));
            parametros.Add(new ReportParameter("Periodo", periodo));
            parametros.Add(new ReportParameter("Campanha", campanha));

            rpvResumen.LocalReport.SetParameters(parametros);

            rpvResumen.LocalReport.Refresh();
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
                switch (col.ColumnName)
                {
                    case "intIDProceso":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "intIdProceso",
                            Width = 60,
                            Visible = false
                        });
                        break;

                    case "codigoColaborador":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "codigoColaborador",
                            Width = 60,
                            Visible = false
                        });
                        break;

                    case "chrPeriodo":
                        columnas.Add(new BeGridColumns { DataField = col.ColumnName, HederText = "Período", Width = 60, Visible = true });
                        break;

                    case "pais":
                        columnas.Add(new BeGridColumns { DataField = col.ColumnName, HederText = "País", Width = 60, Visible = true });
                        break;

                    case "nombreColaborador":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Nombre Colaborador",
                            Width = 100,
                            Visible = true
                        });
                        break;

                    case "FechaCierre":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Fecha de Cierre de Diálogo",
                            Width = 60,
                            Visible = true
                        });
                        break;

                    case "enf1":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Enfoque 1",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "plan1":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Plan de Acción 1",
                            Width = 200,
                            Visible = true
                        });
                        break;

                    case "enf2":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Enfoque 2",
                            Width = 80,
                            Visible = true
                        });
                        break;

                    case "plan2":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Plan de Acción 2",
                            Width = 200,
                            Visible = true
                        });
                        break;

                    case "EnfoqueComp":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Comp Enfoque",
                            Width = 100,
                            Visible = true
                        });
                        break;

                    case "PlanAccionComp":
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = "Plan de Acción",
                            Width = 100,
                            Visible = true
                        });
                        break;

                    default:
                        columnas.Add(new BeGridColumns
                        {
                            DataField = col.ColumnName,
                            HederText = col.ColumnName,
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
                    HeaderText = columna.HederText,
                    HtmlEncode = false,
                    HtmlEncodeFormatString = false,
                    ConvertEmptyStringToNull = true
                };

                columnBound.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                columnBound.Visible = columna.Visible;

                if (columna.Width != 0)
                {
                    if (columna.DataField == "FechaCierre")
                        columnBound.DataFormatString = "{0:d}";

                    columnBound.ItemStyle.Width = Unit.Pixel(columna.Width);

                    if (columna.DataField == "nombreColaborador" || columna.DataField == "plan1" || columna.DataField == "plan2" || columna.DataField == "PlanAccionComp")
                    {
                        columnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    }
                    else
                    {
                        columnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                }

                gvDetalle.Columns.Add(columnBound);
            }
        }

        //private DataTable CargarMedicionCompetencias(int idProceso, string codPais, string anhio, string codUsuario)
        private DataTable CargarMedicionCompetencias(int idProceso, string codPais, string anhio, string codUsuario, string cub)
        {
            BlMedicionCompetencia daProceso = new BlMedicionCompetencia();
            DataTable dtTemporal = daProceso.ObtenerMedicionCompetencia(CadenaConexion, idProceso);
            //return ConsultaWebServices(dtTemporal, codPais, anhio, codUsuario);
            return ConsultaWebServices(dtTemporal, codPais, anhio, codUsuario, cub);
        }

        //private DataTable ConsultaWebServices(DataTable dtPlanAnual, string codPais, string anhio, string codUsuario)
        private DataTable ConsultaWebServices(DataTable dtPlanAnual, string codPais, string anhio, string codUsuario, string cub)
        {
            string mensaje;

            BlPlanAnual daProceso = new BlPlanAnual();

            // string sugerencia = (string) dtPlanAnual.Rows[0]["vchSugerencia"];
            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, codPais);
            int anio = Convert.ToInt32(anhio);
            string documentoIdentidad = codUsuario;

            DataTable dtCompetencia = new DataTable();

            WsInterfaceFFVVSoapClient wsPlanAnual = new WsInterfaceFFVVSoapClient();

            try
            {
                documentoIdentidad = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                             ? Convert.ToInt32(documentoIdentidad).ToString()
                                             : documentoIdentidad.Trim();

                //DataSet dsPlanAnual = wsPlanAnual.ConsultaPorcentajeAvanceCompetencia(anio, codigoPaisAdam, documentoIdentidad);
                DataSet dsPlanAnual = wsPlanAnual.ConsultaPorcentajeAvanceCompetencia(anio, cub);

                mensaje = "El Proceso se realizó con éxito";
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);

                if (dsPlanAnual != null)
                {
                    if (dsPlanAnual.Tables[0].Rows.Count > 0)
                    {
                        dtCompetencia.Columns.Add("intCodigoCompetencia");
                        dtCompetencia.Columns.Add("vchCompetencia");
                        dtCompetencia.Columns.Add("PorcentajeAvance");
                        dtCompetencia.Columns.Add("Enfoque");
                        // dtCompetencia.Columns.Add("Sugerencia");
                        foreach (DataRow dr in dsPlanAnual.Tables[0].Rows)
                        {
                            DataRow drCompetencia = dtCompetencia.NewRow();
                            drCompetencia["intCodigoCompetencia"] = dr["CodigoCompetencia"].ToString();
                            drCompetencia["vchCompetencia"] = dr["DescripcionCompetencia"].ToString();
                            drCompetencia["PorcentajeAvance"] = dr["PorcentajeAvance"].ToString();
                            string esCompetencia = "false";

                            for (int i = 0; i < dtPlanAnual.Rows.Count; i++)
                            {
                                if (dr["CodigoCompetencia"].ToString() == dtPlanAnual.Rows[i]["intCodigoCompetencia"].ToString())
                                {
                                    esCompetencia = "true";
                                    // drCompetencia["Sugerencia"] = sugerencia;
                                }
                            }

                            drCompetencia["Enfoque"] = esCompetencia;
                            dtCompetencia.Rows.Add(drCompetencia);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Falla Web Service " + ex.Message;
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            }

            return dtCompetencia;
        }

        #endregion Metodos
    }
}