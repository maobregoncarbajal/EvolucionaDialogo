
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using BusinessEntity;
    using BusinessLogic;
    using Web.Matriz.Helpers.PDF;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Reportes : Page
    {
        #region "Variables Globales"

        private BlDirectoraVentas oblDirectoraVentas = new BlDirectoraVentas();
        private BeDirectoraVentas obeDirectoraVentas = new BeDirectoraVentas();
        private BeGerenteZona obeGerenteZona = new BeGerenteZona();
        private BlGerenteZona oblGerenteZona = new BlGerenteZona();
        private BeGerenteRegion obeGerenteRegion = new BeGerenteRegion();
        private BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();

        #endregion "Variables Globales"

        #region "Eventos de Página"

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack)
                {
                    ListarPais();
                    ListarTipoColaborador();
                    EstadoControles(1, false, false);
                    EstadoControles(2, false, false);
                    Session["SesionTablaGerenteRegion"] = null;
                    Session["SesionTablaGerenteZona"] = null;
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAlertaPagina"].ToString());
            }
        }

        #endregion "Eventos de Página"

        #region "Metodos Comunes"

        private void ListarTipoColaborador()
        {
            try
            {
                ddlTipoColaborador.Items.Clear();
                ddlTipoColaborador.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                ddlTipoColaborador.Items.Insert(1, new ListItem("DV", "1"));
                ddlTipoColaborador.Items.Insert(2, new ListItem("GR", "2"));
                ddlTipoColaborador.Items.Insert(3, new ListItem("GZ", "3"));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar tipo de colaborador.");
            }
        }

        private void ListarPais()
        {
            try
            {
                BlPais oblPais = new BlPais();
                List<BePais> oList = new List<BePais>();
                oList = oblPais.ObtenerPaises();
                ddlPais.Items.Clear();
                ddlPais.DataSource = oList;
                ddlPais.DataValueField = "prefijoIsoPais";
                ddlPais.DataTextField = "NombrePais";
                ddlPais.DataBind();
                ddlPais.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                ddlRegion.Items.Clear();
                ddlRegion.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                ddlZona.Items.Clear();
                ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar los paices.");
            }
        }

        private void ListarRegion(string codigoPais, string codigoRegion)
        {
            try
            {
                BlRegion oblRegion = new BlRegion();
                List<BeRegion> oList = new List<BeRegion>();
                oList = oblRegion.ListarRegion(codigoPais, codigoRegion);
                ddlRegion.Items.Clear();
                ddlRegion.DataSource = oList;
                ddlRegion.DataValueField = "IdMaeCodidgoRegion";
                ddlRegion.DataTextField = "DesRegion";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("[Todos]", "0"));
                ddlZona.Items.Clear();
                ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar regiones.");
            }
        }

        private void ListarZona(string codigoPais, string codigoRegion, string codigoZona)
        {
            try
            {
                BlZona oblZona = new BlZona();
                List<BeZona> oList = new List<BeZona>();
                oList = oblZona.ListarZona(codigoPais, codigoRegion, codigoZona);
                ddlZona.Items.Clear();
                ddlZona.DataSource = oList;
                ddlZona.DataValueField = "codZona";
                ddlZona.DataTextField = "DesZona";
                ddlZona.DataBind();
                ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar zonas.");
            }
        }

        private void EstadoControles(int tipo, bool region, bool zona)
        {
            if (tipo == 1)
            {
                ddlRegion.AutoPostBack = zona;
                divRegion.Visible = region;
                divZona.Visible = zona;
            }
            else
            {
                btnCancelar.Visible = region;
                btnGrabar.Visible = region;
            }
        }

        private void AlertaMensaje(string strMensaje)
        {
            string ClienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", ClienteScript, false);
        }

        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public static string Right(string param, int length)
        {
            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }

        #endregion "Metodos Comunes"

        #region "Eventos Comunes"

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarRegion(ddlPais.SelectedValue, string.Empty);
            CargarPeriodos(ddlPais.SelectedValue);
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvDirectoraVentas.Visible = false;
            gvGerenteRegion.Visible = false;
            gvGerenteZona.Visible = false;
            btnCancelar.Visible = false;
            btnGrabar.Visible = false;
            EstadoControles(2, false, false);
            ddlPeriodos.Visible = true;
            lblPeriodo.Visible = true;

            switch (ddlTipoColaborador.SelectedValue)
            {
                case "0":
                    EstadoControles(1, false, false);
                    break;
                case "1":
                    ddlPeriodos.Visible = false;
                    lblPeriodo.Visible = false;

                    EstadoControles(1, false, false);
                    if (ddlPais.SelectedValue != "0")
                    {
                        ddlRegion.Items.Clear();
                        ddlRegion.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                        ddlZona.Items.Clear();
                        ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                        gvDirectoraVentas.Visible = true;
                    }
                    break;
                case "2":
                    EstadoControles(1, true, false);
                    if (ddlPais.SelectedValue == "0")
                    {
                        ddlRegion.SelectedIndex = 0;
                        ddlZona.Items.Clear();
                        ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    }
                    else
                        ListarRegion(ddlPais.SelectedValue, string.Empty);
                    gvGerenteRegion.Visible = true;
                    break;
                case "3":
                    EstadoControles(1, true, true);

                    if (ddlPais.SelectedValue == "0")
                    {
                        ddlRegion.SelectedIndex = 0;
                        ddlZona.Items.Clear();
                        ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    }
                    else
                        ListarRegion(ddlPais.SelectedValue, "");
                    gvGerenteZona.Visible = true;
                    break;
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codidgoRegion = "";
            if (ddlRegion.SelectedValue != "0")
            {
                string[] words = ddlRegion.SelectedValue.Split('|');
                codidgoRegion = words[1];
            }
            ListarZona(ddlPais.SelectedValue, codidgoRegion, "");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //LISTA DIRECTORA VENTAS
            Session["SesionTablaDirectoraVentas"] = null;
            gvDirectoraVentas.DataSource = null;
            gvDirectoraVentas.DataBind();
            gvDirectoraVentas.EmptyDataText = string.Empty;

            //LISTA GERENTE REGION
            Session["SesionTablaDirectoraVentasGR"] = null;
            gvGerenteZona.DataSource = null;
            gvGerenteZona.DataBind();
            gvGerenteZona.EmptyDataText = string.Empty;

            //LISTA GERENTE ZONA
            Session["SesionGerenteRegion"] = null;
            gvGerenteRegion.DataSource = null;
            gvGerenteRegion.DataBind();
            gvGerenteRegion.EmptyDataText = string.Empty;

            try
            {
                if (ddlPais.SelectedValue != "0")
                {
                    if (ddlTipoColaborador.SelectedValue != "0")
                    {
                        if (ddlTipoColaborador.SelectedValue == "1")
                        {
                            ListarDirectoraVentas();
                        }
                        else
                        {
                            if (ddlTipoColaborador.SelectedValue == "2")
                            {
                                ListarGerenteRegionReporte(ddlPais.SelectedValue, ddlRegion.SelectedValue, txtColaborador.Text, ddlPeriodos.SelectedValue);
                            }
                            else
                            {
                                if (ddlTipoColaborador.SelectedValue == "3")
                                {
                                    ListarGerenteZonaReporte(ddlPais.SelectedValue, ddlRegion.SelectedValue, ddlZona.SelectedValue, txtColaborador.Text, ddlPeriodos.SelectedValue);
                                }
                            }
                        }
                    }
                }

                Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al buscar.");
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e) //BOTON PDF
        {
            try
            {
                if (ddlPais.SelectedValue != "0")
                {
                    if (ddlTipoColaborador.SelectedValue != "0")
                    {
                        if (ddlTipoColaborador.SelectedValue == "1")
                        {
                            //PDF DIRECTORA VENTAS
                            DataTable dt = (DataTable)Session["SesionTablaDirectoraVentas"];
                            string fileName = string.Format("{0}", "ReporteConfiguracion" + "_" +
                                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s"));
                            PDFExporter pdf = new PDFExporter(dt, fileName, false);
                            pdf.ExportPDF();
                        }
                        else
                        {
                            if (ddlTipoColaborador.SelectedValue == "2")
                            {
                                //PDF GERENTE REGION
                                DataTable dt = (DataTable)Session["SesionTablaGerenteRegion"];
                                string fileName = string.Format("{0}", "ReporteConfiguracion" + "_" +
                                                                       DateTime.Now.ToString("M_dd_yyyy_H_M_s"));
                                PDFExporter pdf = new PDFExporter(dt, fileName, false);
                                pdf.ExportPDF();
                            }
                            else
                            {
                                //PDF GERENTE ZONA
                                DataTable dt = (DataTable)Session["SesionTablaGerenteZona"];
                                string fileName = string.Format("{0}", "ReporteConfiguracion" + "_" +
                                                                       DateTime.Now.ToString("M_dd_yyyy_H_M_s"));
                                PDFExporter pdf = new PDFExporter(dt, fileName, false);
                                pdf.ExportPDF();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Error al generar archivo PDF.");
            }
        }

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen();", true);
        }

        #endregion "Eventos Comunes"

        #region "Directora Ventas"

        private void ListarDirectoraVentas()
        {
            try
            {
                Session["SesionTablaDirectoraVentas"] = null;
                bool estado = (ddlEstado.SelectedValue != "0");
                oblDirectoraVentas = new BlDirectoraVentas();
                List<BeDirectoraVentas> oList = new List<BeDirectoraVentas>(oblDirectoraVentas.DirectoraVentasListar(0, ddlPais.SelectedValue, txtColaborador.Text, estado));

                if (oList.Count > 0)
                {
                    DataTable dtDirectoraVentas = new DataTable();
                    dtDirectoraVentas.Columns.Add("ID");
                    dtDirectoraVentas.Columns.Add("Pais");
                    dtDirectoraVentas.Columns.Add("TipoColaborador");
                    dtDirectoraVentas.Columns.Add("CodigoDirectora");
                    dtDirectoraVentas.Columns.Add("Directora");
                    dtDirectoraVentas.Columns.Add("Correo");
                    dtDirectoraVentas.Columns.Add("Estado");

                    foreach (BeDirectoraVentas item in oList)
                    {
                        dtDirectoraVentas.Rows.Add(item.intIDDirectoraVenta.ToString(), item.obePais.NombrePais,
                                                   ddlTipoColaborador.SelectedItem.Text, item.chrCodigoDirectoraVentas,
                                                   item.vchNombreCompleto, item.vchCorreoElectronico, item.EstadoDirectora);
                    }

                    if (dtDirectoraVentas.Rows.Count == 0)
                    {
                        gvDirectoraVentas.EmptyDataText = "Sin Registros";
                    }

                    gvDirectoraVentas.DataSource = dtDirectoraVentas;
                    gvDirectoraVentas.DataBind();
                    EstadoControles(2, true, true);
                    Session["SesionTablaDirectoraVentas"] = dtDirectoraVentas;
                }
                else
                    EstadoControles(2, false, false);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Directora de Ventas.");
            }
        }

        protected void gvDirectoraVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDirectoraVentas.PageIndex = e.NewPageIndex;
            gvDirectoraVentas.DataSource = Session["SesionTablaDirectoraVentas"];
            gvDirectoraVentas.DataBind();
        }

        protected void gvDirectoraVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string codigo;
            switch (e.CommandName)
            {
                case "Ver":
                    codigo = gvDirectoraVentas.DataKeys[index].Values[0].ToString();
                    AlertaMensaje("No hay registros para DV " + codigo);
                    break;
            }
        }

        protected void gvDirectoraVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    ImageButton ImgBtnVer = (ImageButton)e.Row.FindControl("imgbtnVer");
                    ImgBtnVer.CommandArgument = e.Row.RowIndex.ToString();
                    break;
            }
        }

        #endregion "Directora Ventas"

        #region "Gerente Region"

        private void ListarGerenteRegionReporte(string codigoPais, string codigoRegion, string nombreGerente, string periodo)
        {
            try
            {
                if (codigoRegion != "0")
                {
                    string[] words = codigoRegion.Split('|');
                    codigoRegion = words[1];
                }

                Session["SesionTablaGerenteRegion"] = null;
                bool estado = (ddlEstado.SelectedValue != "0");
                oblGerenteRegion = new BlGerenteRegion();
                List<BeGerenteRegion> oListBelcorp = new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListarReporte(codigoPais, nombreGerente, estado, codigoRegion, periodo));

                DataTable dtGerenteRegion = new DataTable();
                dtGerenteRegion.Columns.Add("ID");
                dtGerenteRegion.Columns.Add("Pais");
                dtGerenteRegion.Columns.Add("TipoColaborador");
                dtGerenteRegion.Columns.Add("CodigoGerente");
                dtGerenteRegion.Columns.Add("Gerente");
                dtGerenteRegion.Columns.Add("Correo");
                dtGerenteRegion.Columns.Add("Estado");
                dtGerenteRegion.Columns.Add("Region");
                dtGerenteRegion.Columns.Add("Jefe");
                dtGerenteRegion.Columns.Add("CodigoJefe");
                dtGerenteRegion.Columns.Add("chrCampaniaRegistro");
                dtGerenteRegion.Columns.Add("chrCampaniaBaja");
                dtGerenteRegion.Columns.Add("FechaBaja");

                foreach (BeGerenteRegion gerenteRegion in oListBelcorp)
                {
                    dtGerenteRegion.Rows.Add(gerenteRegion.IntIDGerenteRegion.ToString(), gerenteRegion.obePais.NombrePais,
                                             ddlTipoColaborador.SelectedItem.Text, gerenteRegion.ChrCodigoGerenteRegion,
                                             gerenteRegion.VchNombrecompleto, gerenteRegion.VchCorreoElectronico,
                                             gerenteRegion.EstadoGerente, gerenteRegion.DescripcionRegion,
                                             gerenteRegion.obeDirectoraVentas.vchNombreCompleto, gerenteRegion.ChrCodDirectorVenta,
                                             gerenteRegion.chrCampaniaRegistro, gerenteRegion.chrCampaniaBaja, gerenteRegion.FechaBaja);
                }

                if (dtGerenteRegion.Rows.Count == 0)
                {
                    gvGerenteRegion.EmptyDataText = "Sin Registros";
                }

                gvGerenteRegion.DataSource = dtGerenteRegion;
                gvGerenteRegion.DataBind();
                if (gvGerenteRegion.Rows.Count > 0)
                {
                    EstadoControles(2, true, true);
                    Session["SesionTablaGerenteRegion"] = dtGerenteRegion;
                }
                else
                    EstadoControles(2, false, false);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente Región.");
            }
        }

        protected void gvGerenteRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteRegion.PageIndex = e.NewPageIndex;
            gvGerenteRegion.DataSource = Session["SesionTablaGerenteRegion"];
            gvGerenteRegion.DataBind();
        }

        protected void gvGerenteRegion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Ver":
                    string codigo = "2|" + ddlPais.SelectedValue + "|" +
                                    gvGerenteRegion.DataKeys[index].Values[1].ToString() + "|" +
                                    gvGerenteRegion.Rows[index].Cells[4].Text + "|" +
                                    gvGerenteRegion.DataKeys[index].Values[2].ToString() + "|" +
                                    gvGerenteRegion.DataKeys[index].Values[3].ToString() + "|" +
                                    gvGerenteRegion.DataKeys[index].Values[4].ToString();
                    Response.Redirect("ReportesHistoricos.aspx?ID=" + codigo);
                    break;
            }
        }

        protected void gvGerenteRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    HyperLink hlinkCodigo = (HyperLink)e.Row.FindControl("hlinkCodigo");
                    Label lblCodigoGerente = (Label)e.Row.FindControl("lblCodigoGerente");
                    Label lblGerente = (Label)e.Row.FindControl("lblGerente");
                    Label lblchrCampaniaRegistro = (Label)e.Row.FindControl("lblchrCampaniaRegistro");
                    Label lblchrCampaniaBaja = (Label)e.Row.FindControl("lblchrCampaniaBaja");
                    Label lblFechaBaja = (Label)e.Row.FindControl("lblFechaBaja");

                    hlinkCodigo.NavigateUrl = "ReportesHistoricos.aspx?ID=" + "2|" + ddlPais.SelectedValue + "|" +
                                              lblCodigoGerente.Text + "|" +
                                              lblGerente.Text + "|" +
                                              lblchrCampaniaRegistro.Text + "|" +
                                              lblchrCampaniaBaja.Text + "|" +
                                              lblFechaBaja.Text;
                    hlinkCodigo.Target = "_blank";
                    //ImageButton ImgBtnVer = (ImageButton) e.Row.FindControl("imgbtnVer");
                    //ImgBtnVer.CommandArgument = e.Row.RowIndex.ToString();
                    break;
            }
        }

        private string BuscarRegion(string codigoPais, string codigoRegion)
        {
            string Respuesta = "";
            try
            {
                BlRegion oblRegion = new BlRegion();
                List<BeRegion> oList = new List<BeRegion>();
                oList = oblRegion.ListarRegion(codigoPais, codigoRegion);

                if (oList != null)
                {
                    if (oList.Count > 0)
                        Respuesta = oList[0].DesRegion;
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Error al buscar regiones.");
            }
            return Respuesta;
        }

        private void CargarPeriodos(string prefijoPais)
        {
            BlPeriodos blPeriodo = new BlPeriodos();
            List<string> periodos = blPeriodo.ObtenerPeriodos(prefijoPais);

            ddlPeriodos.DataSource = periodos;
            ddlPeriodos.DataBind();
        }

        #endregion "Gerente Region"

        #region "Gerente Zona"

        private void ListarGerenteZonaReporte(string codigoPais, string codigoRegion, string codigoZona, string nombreGerente, string periodo)
        {
            try
            {
                BlGerenteZona oblGerenteZona = new BlGerenteZona();

                Session["SesionTablaGerenteZona"] = null;
                bool estado = (ddlEstado.SelectedValue != "0");

                oblGerenteZona = new BlGerenteZona();
                List<BeGerenteZona> oListBelcorp = new List<BeGerenteZona>(oblGerenteZona.GerenteZonaListarReporte(codigoPais, nombreGerente, estado, codigoRegion, codigoZona, periodo));

                DataTable dtGerenteZona = new DataTable();
                dtGerenteZona.Columns.Add("ID");
                dtGerenteZona.Columns.Add("Pais");
                dtGerenteZona.Columns.Add("TipoColaborador");
                dtGerenteZona.Columns.Add("CodigoGerente");
                dtGerenteZona.Columns.Add("Gerente");
                dtGerenteZona.Columns.Add("Correo");
                dtGerenteZona.Columns.Add("Estado");
                dtGerenteZona.Columns.Add("Region");
                dtGerenteZona.Columns.Add("Zona");
                dtGerenteZona.Columns.Add("Jefe");
                dtGerenteZona.Columns.Add("CodigoJefe");
                dtGerenteZona.Columns.Add("chrCampaniaRegistro");
                dtGerenteZona.Columns.Add("chrCampaniaBaja");
                dtGerenteZona.Columns.Add("FechaBaja");

                foreach (BeGerenteZona gerenteZona in oListBelcorp)
                {
                    string region = BuscarRegion(codigoPais, gerenteZona.CodRegion);

                    dtGerenteZona.Rows.Add(gerenteZona.intIDGerenteZona.ToString(), gerenteZona.obePais.NombrePais,
                                           ddlTipoColaborador.SelectedItem.Text, gerenteZona.chrCodigoGerenteZona,
                                           gerenteZona.vchNombreCompleto, gerenteZona.vchCorreoElectronico,
                                           gerenteZona.EstadoGerente, region, gerenteZona.DescripcionZona,
                                           gerenteZona.obeGerenteRegion.VchNombrecompleto,
                                           gerenteZona.obeGerenteRegion.ChrCodigoGerenteRegion,
                                           gerenteZona.chrCampaniaRegistro, gerenteZona.chrCampaniaBaja,
                                           gerenteZona.FechaBaja);
                }

                if (dtGerenteZona.Rows.Count == 0)
                {
                    gvGerenteZona.EmptyDataText = "Sin Registros";
                }

                gvGerenteZona.DataSource = dtGerenteZona;
                gvGerenteZona.DataBind();

                if (gvGerenteZona.Rows.Count > 0)
                {
                    Session["SesionTablaGerenteZona"] = dtGerenteZona;
                    EstadoControles(2, true, true);
                }
                else
                    EstadoControles(2, false, false);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente de Zona.");
            }
        }

        protected void gvGerenteZona_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteZona.PageIndex = e.NewPageIndex;
            gvGerenteZona.DataSource = Session["SesionTablaGerenteZona"];
            gvGerenteZona.DataBind();
        }

        protected void gvGerenteZona_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string codigo;
            switch (e.CommandName)
            {
                case "Ver":
                    codigo = "3|" + ddlPais.SelectedValue + "|" +
                             gvGerenteZona.DataKeys[index].Values[1].ToString() + "|" +
                             gvGerenteZona.Rows[index].Cells[4].Text + "|" +
                             gvGerenteZona.DataKeys[index].Values[2].ToString() + "|" +
                             gvGerenteZona.DataKeys[index].Values[3].ToString() + "|" +
                             gvGerenteZona.DataKeys[index].Values[4].ToString();
                    //AlertaMensaje("No hay registros para GZ " + codigo);
                    Response.Redirect("ReportesHistoricos.aspx?ID=" + codigo);
                    break;
            }
        }

        protected void gvGerenteZona_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    HyperLink hlinkCodigo = (HyperLink)e.Row.FindControl("hlinkCodigo");
                    Label lblCodigoGerente = (Label)e.Row.FindControl("lblCodigoGerente");
                    Label lblGerente = (Label)e.Row.FindControl("lblGerente");
                    Label lblchrCampaniaRegistro = (Label)e.Row.FindControl("lblchrCampaniaRegistro");
                    Label lblchrCampaniaBaja = (Label)e.Row.FindControl("lblchrCampaniaBaja");
                    Label lblFechaBaja = (Label)e.Row.FindControl("lblFechaBaja");

                    hlinkCodigo.NavigateUrl = "ReportesHistoricos.aspx?ID=" + "3|" + ddlPais.SelectedValue + "|" +
                                              lblCodigoGerente.Text + "|" +
                                              lblGerente.Text + "|" +
                                              lblchrCampaniaRegistro.Text + "|" +
                                              lblchrCampaniaBaja.Text + "|" +
                                              lblFechaBaja.Text;
                    hlinkCodigo.Target = "_blank";
                    //ImageButton ImgBtnVer = (ImageButton) e.Row.FindControl("imgbtnVer");
                    //ImgBtnVer.CommandArgument = e.Row.RowIndex.ToString();
                    break;
            }
        }

        #endregion "Gerente Zona"
    }
}