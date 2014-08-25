
namespace Evoluciona.Dialogo.Web.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class SeguimientoStatus : Page
    {
        #region Variables

        protected BeUsuario objUsuario;

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (IsPostBack) return;
            CargarCombos();
            CargarPeriodos();
            CargarPeriodosDetalle();
            menuReporte.Reporte1 = "ui-state-active";
            txtNombreJefeD.Text = objUsuario.nombreUsuario;
            txtNombreJefeD.Enabled = false;
        }

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            gvProceso.DataSource = null;
            gvProceso.DataBind();

            BlReporte blReportedata = new BlReporte();
            string periodo = ddlPeriodos.SelectedValue.Trim();
            string pais = ddlPaises.SelectedValue.Trim();

            string nivel = GetNivelByIdRol(Convert.ToInt32(ddlNivel.SelectedValue.Trim()));

            string nivelEvaluador = GetNivelByIdRol(objUsuario.codigoRol);
            int estado = Convert.ToInt32(ddlEstatus.SelectedValue);

            List<BeSeguimientoStatus> entidades = blReportedata.ListarSeguimientoStatusByUsuario(periodo, pais, nivel, estado, objUsuario.codigoUsuario, nivelEvaluador);
            
            if (entidades.Count != 0)
            {
                int cantidad = 0;

                foreach (BeSeguimientoStatus entidad in entidades)
                {
                    cantidad = cantidad + entidad.Colaborador;
                }

                gvProceso.Columns[0].FooterText = "Belcorp";
                gvProceso.Columns[2].FooterText = cantidad.ToString();
                gvProceso.DataSource = entidades;
                gvProceso.DataBind();
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
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(1);", true);
        }

        protected void gvProceso_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            //Creating a gridview object
            GridView objGridView = (GridView)sender;
            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 7, string.Empty, System.Drawing.Color.White.Name);

            AddMergedCells(objgridviewrow, objtablecell, 4, "Detalle Diálogos en proceso", "#60497B");

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion Merge cells
        }

        protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
        {
            objtablecell = new TableCell {Text = celltext, ColumnSpan = colspan};
            objtablecell.Style.Add("background-color", backcolor);
            objtablecell.Style.Add("border-color", backcolor);
            objtablecell.HorizontalAlign = HorizontalAlign.Center;
            objgridviewrow.Cells.Add(objtablecell);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string html = HdnValue.Value;
            ExportToExcel(ref html, "SeguimientoStatus");
        }

        protected void btnBuscarDAux_Click(object sender, EventArgs e)
        {
            //gvDetalle.DataSource = null;
            //gvDetalle.DataBind();

            //blReporte blReportedata = new blReporte();
            //string periodo = ddlPeriodosD.SelectedValue.Trim();
            //string nombreColaborador = txtNombreColaboradorD.Text.Trim();
            //string nivel = GetNivelByIdRol(Convert.ToInt32(ddlNivelD.SelectedValue.Trim()));
            //string pais = ddlPaisesD.SelectedValue.Trim();
            //string zona = string.IsNullOrEmpty(ddlZonaD.SelectedValue) ? "0" : ddlZonaD.SelectedValue.Trim();
            //string region = string.IsNullOrEmpty(ddlRegionD.SelectedValue) ? "0" : ddlRegionD.SelectedValue.Trim();
            //string nombreJefe = txtNombreJefeD.Text.Trim();
            //string estado = ddlEstatusD.SelectedValue.Trim();

            //List<beSeguimientoStatusDetalle> entidades = blReportedata.ListarStatusDialogosDetalle(periodo, nombreColaborador, nivel, pais, zona, nombreJefe, estado, region, objUsuario.codigoUsuario, GetNivelByIdRol(objUsuario.codigoRol));
            
            //if (entidades.Count != 0)
            //{
            //    gvDetalle.DataSource = entidades;
            //    gvDetalle.Columns[0].FooterText = "TOTAL :";
            //    gvDetalle.Columns[2].FooterText = entidades.Count.ToString();
            //    gvDetalle.DataBind();
            //}
            //else
            //{
            //    const string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
            //    Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            //}

            //Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose(2);", true);
        }

        protected void btnBuscarD_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(2);", true);
        }

        protected void ddlRegionD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlZonaD.Items.Clear();

            if (Convert.ToInt32(ddlNivelD.SelectedValue) == 6)// gerente de zona
            {
                BlReporte blReportedata = new BlReporte();
                ddlZonaD.DataTextField = "Descripcion";
                ddlZonaD.DataValueField = "Codigo";
                ddlZonaD.DataSource = blReportedata.ListarZonas(ddlRegionD.SelectedValue);
                ddlZonaD.DataBind();
                ddlZonaD.Items.Insert(0, new ListItem("Todos", "0"));
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }

        protected void ddlNivelD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlNivelD.SelectedValue) == (objUsuario.codigoRol + 1))
            {
                txtNombreJefeD.Text = objUsuario.nombreUsuario;
                txtNombreJefeD.Enabled = false;
            }
            else
            {
                txtNombreJefeD.Text = string.Empty;
                txtNombreJefeD.Enabled = true;
            }

            ddlPaisesD_SelectedIndexChanged(sender, e);
        }

        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodos();
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(1);", true);
        }

        protected void ddlPaisesD_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodosDetalle();
            ddlRegionD_SelectedIndexChanged(sender, e);
            Page.ClientScript.RegisterStartupScript(GetType(), "PostWindows", "PosicionarVentana(2);", true);
        }
        #endregion Eventos

        #region Metodos

        private void CargarCombos()
        {
            ddlPaises.Items.Clear();
            ddlPaisesD.Items.Clear();
            ddlRegionD.Items.Clear();

            BlReporte reporteBL = new BlReporte();

            ddlPaises.DataTextField = "NombrePais";
            ddlPaises.DataValueField = "prefijoIsoPais";

            ddlPaisesD.DataTextField = "NombrePais";
            ddlPaisesD.DataValueField = "prefijoIsoPais";

            string nivelEvaluador = GetNivelByIdRol(objUsuario.codigoRol);
            List<BePais> paises = reporteBL.ObtenerPaisesUsuario(nivelEvaluador, objUsuario.codigoUsuario, objUsuario.prefijoIsoPais);
            ddlPaises.DataSource = paises;
            ddlPaises.DataBind();
            ddlPaisesD.DataSource = paises;
            ddlPaisesD.DataBind();

            BlReporte blReportedata = new BlReporte();

            ddlRegionD.DataTextField = "Descripcion";
            ddlRegionD.DataValueField = "Codigo";
            ddlRegionD.DataSource = blReportedata.ListarRegiones();
            ddlRegionD.DataBind();
            ddlRegionD.Items.Insert(0, new ListItem("Todos", "0"));

            CargarRoles();
        }

        private void CargarVariables()
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
        }

        private void CargarRoles()
        {
            ddlNivel.Items.Clear();
            ddlNivelD.Items.Clear();

            BlRol rolBL = new BlRol();
            List<BeRol> roles = rolBL.ObtenerRolesSubordinados(objUsuario.codigoRol);
            ddlNivel.DataSource = roles;
            ddlNivel.DataTextField = "Descripcion";
            ddlNivel.DataValueField = "CodigoRol";
            ddlNivel.DataBind();

            ddlNivelD.DataSource = roles;
            ddlNivelD.DataTextField = "Descripcion";
            ddlNivelD.DataValueField = "CodigoRol";
            ddlNivelD.DataBind();
        }

        private void CargarPeriodos()
        {
            ddlPeriodos.Items.Clear();
            BlReporte reporteBL = new BlReporte();
            List<string> periodos = reporteBL.ObtenerPeriodos(ddlPaises.SelectedValue, Convert.ToInt32(ddlNivel.SelectedValue));

            ddlPeriodos.DataSource = periodos;
            ddlPeriodos.DataBind();
        }

        private void CargarPeriodosDetalle()
        {
            ddlPeriodosD.Items.Clear();
            BlReporte reporteBL = new BlReporte();
            List<string> periodos = reporteBL.ObtenerPeriodos(ddlPaisesD.SelectedValue, Convert.ToInt32(ddlNivelD.SelectedValue));

            ddlPeriodosD.DataSource = periodos;
            ddlPeriodosD.DataBind();
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

        #endregion Metodos
    }
}