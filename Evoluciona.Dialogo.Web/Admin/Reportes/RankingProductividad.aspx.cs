
namespace Evoluciona.Dialogo.Web.Admin.Reportes
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

    public partial class RankingProductividad : Page
    {
        #region Variables

        private readonly BlReporte reporteBL = new BlReporte();

        private BeAdmin objAdmin;

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
            if (IsPostBack) { return; }
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

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
            string nivel = GetRolByCodigoRol(rol);
            string periodo = cboPeriodos.SelectedValue.Trim();
            string campanha = cboCampanhasFiltro.SelectedValue.Trim();

            const string estado1 = "Critica";
            const string estado2 = "Estable";
            const string estado3 = "Productiva";
            gvResumen.Columns[3].HeaderText = estado1;
            gvResumen.Columns[4].HeaderText = estado2;
            gvResumen.Columns[5].HeaderText = estado3;

            List<BeAnalisisStatusRanking> entidades = reporteBL.ListarAnalisisStatusRanking(pais, nivel, periodo, campanha, estado1, estado2, estado3);

            if (entidades.Count != 0)
            {
                List<BeChartCampanha> charts = reporteBL.ListarChartCampanha(pais, nivel, periodo, campanha, estado1, estado2, estado3);

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

            AddMergedCells(objgridviewrow, objtablecell, 3, "% Distribución  FFVV por cuadrante", "#60497B");

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
            string nivel = GetRolByCodigoRol(Convert.ToInt32(cboRolesD.SelectedValue));
            string codPais = cboPaisesD.SelectedValue;
            string nombreJefe = txtNombreJefeD.Text;
            string estado = cboCuadranteD.Text;
            string zona = string.IsNullOrEmpty(cboZonaD.SelectedValue) ? "0" : cboZonaD.SelectedValue.Trim();
            string region = string.IsNullOrEmpty(cboRegionD.SelectedValue) ? "0" : cboRegionD.SelectedValue.Trim();
            int intIDRol = GetintIDRolByCodigoRol(Convert.ToInt32(cboRolesD.SelectedValue));

            DataTable dt = reporteBL.ObtenerVariablesNegocio(CadenaConexion, periodo, nombreColaborador, nivel, zona, codPais, nombreJefe, estado, region);

            // obtenemos los nombres de las columnas para las competencias a partir del 1 elemento
            if (dt.Rows.Count != 0)
            {

                dt.Columns.Add("Competencia", typeof(string));

                //Agegamos las competencias apartir del id del proceso y el documento de identidad del colaborador

                foreach (DataRow row in dt.Rows)
                {
                    string codUsuario = (string)row["codigoColaborador"];

                    BlReporte blRepo = new BlReporte();
                    DataTable dtCompetencia = blRepo.ObtenerCompetencia(codUsuario, "2012", codPais, intIDRol);

                    if (dtCompetencia.Rows.Count > 0)
                    {
                        row["Competencia"] = dtCompetencia.Rows[0]["vchCompetencia"];
                    }

                }

                dt.Columns.Remove("cub");
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
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void cboRolesD_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void CargarChartPeriodo(string estado1, string estado2, string estado3, decimal porcentajeEstado1, decimal porcentajeEstado2, decimal porcentajeEstado3)
        {
            // en el diagrama los estados 1 y 3 se invierten

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

        private string GetRolByCodigoRol(int codigoRol)
        {
            string rol = String.Empty;

            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    rol = Constantes.CodDirectorVentas;
                    break;
                case Constantes.RolGerenteRegion:
                    rol = Constantes.CodGerenteRegion;
                    break;
                case Constantes.RolGerenteZona:
                    rol = Constantes.CodGerenteZona;
                    break;
            }
            return rol;
        }

        private int GetintIDRolByCodigoRol(int codigoRol)
        {
            int intIDRol = codigoRol;

            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    intIDRol = Constantes.IdRolDirectorVentas;
                    break;
                case Constantes.RolGerenteRegion:
                    intIDRol = Constantes.IdRolGerenteRegion;
                    break;
                case Constantes.RolGerenteZona:
                    intIDRol = Constantes.IdRolGerenteZona;
                    break;
            }
            return intIDRol;
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
            BlPais paisBL = new BlPais();
            List<BePais> paises = new List<BePais>();

            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";

            cboPaisesD.DataTextField = "NombrePais";
            cboPaisesD.DataValueField = "prefijoIsoPais";

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    paises = paisBL.ObtenerPaises();
                    cboPaises.DataSource = paises;
                    cboPaisesD.DataSource = paises;
                    cboPaises.DataBind();
                    break;
                case Constantes.RolAdminPais:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    cboPaises.DataSource = paises;
                    cboPaises.DataBind();
                    cboPaisesD.DataSource = paises;
                    break;
                case Constantes.RolAdminEvaluciona:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    cboPaises.DataSource = paises;
                    cboPaises.DataBind();
                    cboPaisesD.DataSource = paises;
                    break;
            }

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
            var roles = rolBL.ObtenerRolesSubordinados(1);
            var subRoles = new List<BeRol>();

            foreach (var beRol in roles)
            {
                if (beRol.CodigoRol != 4)
                    subRoles.Add(beRol);
            }
            cboRoles.DataSource = subRoles;
            cboRoles.DataTextField = "Descripcion";
            cboRoles.DataValueField = "CodigoRol";
            cboRoles.DataBind();

            cboRolesD.DataSource = subRoles;
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
                                               new BeComun {Codigo = "0", Descripcion = "Todos"},
                                               new BeComun {Codigo = "Critica", Descripcion = "Critica"},
                                               new BeComun {Codigo = "Estable", Descripcion = "Estable"},
                                               new BeComun {Codigo = "Productiva", Descripcion = "Productiva"},
                                               new BeComun {Codigo = "Sin Estado", Descripcion = "Sin Estado"}
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

        #endregion Metodos
    }
}