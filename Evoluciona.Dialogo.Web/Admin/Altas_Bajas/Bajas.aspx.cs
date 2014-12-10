
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI.WebControls;
    using BusinessLogic;
    using BusinessEntity;

    public partial class Bajas : System.Web.UI.Page
    {
        #region "Variables Globales"

        private BeGerenteZona obeGerenteZona = new BeGerenteZona();
        private BlGerenteZona oblGerenteZona = new BlGerenteZona();
        private BeGerenteRegion obeGerenteRegion = new BeGerenteRegion();
        private BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();

        #endregion "Variables Globales"

        #region "Evento de Página"

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack)
                {
                    ListarPais();
                    ListarTipoColaborador();
                    AccionControles();
                    btnCancelar.Visible = false;
                    btnGrabar.Visible = false;
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAlertaPagina"].ToString());
            }
        }

        #endregion "Evento de Página"

        #region "Eventos Comunes"

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
                ddlRegion.DataValueField = "IdMaeCodidgoRegion"; //"CodRegion";
                ddlRegion.DataTextField = "DesRegion";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("[TODOS]", "0"));
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

        private void AccionControles()
        {
            bool f = false;
            bool t = true;
            gvGerenteRegion.Visible = f;
            gvGerenteZona.Visible = f;
            //btnCancelar.Visible = f;
            //btnEliminar.Visible = f;//btnGrabar
            EstadoControles(2, f, f, f, f);
            switch (ddlTipoColaborador.SelectedValue)
            {
                case "0":
                    EstadoControles(1, f, f, f, f);
                    break;

                case "1":
                    EstadoControles(1, f, f, t, t);
                    if (ddlPais.SelectedValue != "0")
                        Response.Redirect("DirectoraVentas.aspx?ID=" + ddlPais.SelectedValue + "|2");
                    break;

                case "2":
                    EstadoControles(1, t, f, t, t);
                    ddlRegion.SelectedIndex = 0;
                    ddlZona.Items.Clear();
                    ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    gvGerenteRegion.Visible = true;
                    break;

                case "3":
                    EstadoControles(1, t, t, t, t);
                    gvGerenteZona.Visible = true;
                    break;
            }
        }

        private void EstadoControles(int tipo, bool region, bool zona, bool colaborador, bool boton)
        {
            if (tipo == 1)
            {
                ddlRegion.AutoPostBack = zona;
                divRegion.Visible = region;
                divZona.Visible = zona;
                //divColaborador.Visible = colaborador;
                //btnBuscar.Enabled = boton;
            }
            else
            {
                //btnCancelar.Visible = region;
                //btnEliminar.Visible = region; //btnGrabar
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

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarRegion(ddlPais.SelectedValue, string.Empty);

            CargarPeriodos(ddlPais.SelectedValue);
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccionControles();
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
            try
            {
                if (ddlPais.SelectedValue != "0")
                {
                    if (ddlTipoColaborador.SelectedValue != "0" || ddlTipoColaborador.SelectedValue != "1")
                    {
                        if (ddlTipoColaborador.SelectedValue == "2")
                        {
                            //LISTAR MAE GERENTE REGION
                            gvGerenteZona.DataSource = null;
                            gvGerenteZona.DataBind();
                            Session["SesionGerenteRegion"] = null;
                            ListarGerenteRegionAll(ddlPais.SelectedValue, txtColaborador.Text, ddlRegion.SelectedValue, ddlPeriodos.SelectedValue);
                        }
                        else
                        {
                            //LISTA MAE GERENTE ZONA
                            gvGerenteRegion.DataSource = null;
                            gvGerenteRegion.DataBind();
                            Session["SesionGerenteZona"] = null;
                            //string codigoZona = (ddlZona.SelectedValue == "0") ? "" : ddlZona.SelectedValue;
                            ListarGerenteZonaAll(ddlPais.SelectedValue, txtColaborador.Text, ddlRegion.SelectedValue, ddlZona.SelectedValue, ddlPeriodos.SelectedValue);
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

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen();", true);
        }

        #endregion "Eventos Comunes"

        #region "Gerente Region"

        private void ListarGerenteRegionAll(string codigoPais, string nombreGerente, string codigoRegion, string periodo)
        {
            try
            {
                /*
                 * El filtro trae data de Datamart
                 * Listamos toda la data de Belcorp por país
                 * BELCORP BUSCA EN DATAMART
                 * LOS QUE EXISTEN SE AÑADE A TABLA
                 * Se busca Region y Zona en las MAE's de GR y GZ
                 */
                if (codigoRegion == "0")
                    codigoRegion = string.Empty;
                else
                {
                    string[] words = codigoRegion.Split('|');
                    codigoRegion = words[1];
                }

                Session["SesionGerenteRegion"] = null;
                oblGerenteRegion = new BlGerenteRegion();
                List<BeGerenteRegion> oListBelcorp = new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionGetAll(codigoPais, nombreGerente, codigoRegion, periodo));

                DataTable dtGerenteRegion = new DataTable();
                dtGerenteRegion.Columns.Add("IntIDGerenteRegion");
                dtGerenteRegion.Columns.Add("ChrCodigoGerenteRegion");
                dtGerenteRegion.Columns.Add("VchNombrecompleto");
                dtGerenteRegion.Columns.Add("VchCorreoElectronico");
                dtGerenteRegion.Columns.Add("vchNombreCompletoDV");
                dtGerenteRegion.Columns.Add("AnioCampana");

                foreach (var beGerenteRegion in oListBelcorp)
                {
                    dtGerenteRegion.Rows.Add(beGerenteRegion.IntIDGerenteRegion, beGerenteRegion.ChrCodigoGerenteRegion, beGerenteRegion.VchNombrecompleto, beGerenteRegion.VchCorreoElectronico, beGerenteRegion.ChrNombreCodDirectorVenta, beGerenteRegion.AnioCampana);
                }

                gvGerenteRegion.DataSource = dtGerenteRegion;
                gvGerenteRegion.DataBind();

                if (gvGerenteRegion.Rows.Count > 0)
                {
                    EstadoControles(2, true, true, true, true);
                    Session["SesionGerenteRegion"] = dtGerenteRegion;
                }
                else
                    EstadoControles(2, false, false, true, true);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente Región.");
            }
        }

        protected void gvGerenteRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteRegion.PageIndex = e.NewPageIndex;
            gvGerenteRegion.DataSource = Session["SesionGerenteRegion"];
            gvGerenteRegion.DataBind();
        }

        protected void gvGerenteRegion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Editar":

                    break;

                case "Eliminar":

                    int codigoRegion = Convert.ToInt32(gvGerenteRegion.DataKeys[index].Values[0].ToString());

                    Label lblAnhoCampanha = (Label)gvGerenteRegion.Rows[index].FindControl("lblAnioCampana");

                    obeGerenteZona.intIDGerenteZona = 0;
                    obeGerenteZona.intIDGerenteRegion = codigoRegion;
                    obeGerenteZona.intUsuarioCrea = 1;
                    obeGerenteZona.chrIndicadorMigrado = "1";
                    obeGerenteZona.chrCampaniaBaja = lblAnhoCampanha.Text;
                    oblGerenteZona.GerenteZonaActualizarEstado(obeGerenteZona);

                    obeGerenteRegion.IntIDGerenteRegion = codigoRegion;
                    obeGerenteRegion.ChrCodDirectorVenta = "";
                    obeGerenteRegion.IntUsuarioCrea = 1; //CAMBIAR
                    obeGerenteRegion.chrIndicadorMigrado = "1";
                    obeGerenteRegion.chrCampaniaBaja = lblAnhoCampanha.Text;
                    oblGerenteRegion.GerenteRegionActualizarEstado(obeGerenteRegion);

                    ListarGerenteRegionAll(ddlPais.SelectedValue, txtColaborador.Text, ddlRegion.SelectedValue, ddlPeriodos.SelectedValue);
                    break;
            }
        }

        protected void gvGerenteRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    ImageButton ImgBtnEditar = (ImageButton)e.Row.FindControl("imgbtnEditar");
                    ImgBtnEditar.CommandArgument = e.Row.RowIndex.ToString();

                    ImageButton ImgBtnEliminar = (ImageButton)e.Row.FindControl("imgbtnEliminar");
                    ImgBtnEliminar.CommandArgument = e.Row.RowIndex.ToString();
                    break;
            }
        }

        #endregion "Gerente Region"

        #region "Gerente Zona"

        private void ListarGerenteZonaAll(string codigoPais, string nombreGerente, string codigoRegion, string codigoZona, string periodo)
        {
            try
            {
                if (codigoRegion == "0")
                    codigoRegion = string.Empty;
                else
                {
                    string[] words = codigoRegion.Split('|');
                    codigoRegion = words[1];
                }

                if (codigoZona == "0")
                    codigoZona = "";
                BlGerenteZona oblGerenteZona = new BlGerenteZona();

                Session["SesionGerenteZona"] = null;
                oblGerenteZona = new BlGerenteZona();
                List<BeGerenteZona> oListBelcorp = new List<BeGerenteZona>(oblGerenteZona.GerenteZonaGetAll(codigoPais, nombreGerente, codigoRegion, codigoZona, periodo));
                //Trae todos los G.Zonas de es País (única solución) // //07/12/2012

                DataTable dtGerenteZona = new DataTable();
                dtGerenteZona.Columns.Add("intIDGerenteZona");
                dtGerenteZona.Columns.Add("chrCodigoGerenteZona");
                dtGerenteZona.Columns.Add("vchNombreCompleto");
                dtGerenteZona.Columns.Add("vchCorreoElectronico");
                dtGerenteZona.Columns.Add("VchNombrecompletoGR");
                dtGerenteZona.Columns.Add("AnioCampana");

                foreach (var beGerenteZona in oListBelcorp)
                {
                    dtGerenteZona.Rows.Add(beGerenteZona.intIDGerenteZona, beGerenteZona.chrCodigoGerenteZona, beGerenteZona.vchNombreCompleto, beGerenteZona.vchCorreoElectronico, beGerenteZona.NombreGerenteRegion, beGerenteZona.AnioCampana);
                }

                gvGerenteZona.DataSource = dtGerenteZona;
                gvGerenteZona.DataBind();
                if (gvGerenteZona.Rows.Count > 0)
                {
                    Session["SesionGerenteZona"] = dtGerenteZona;
                    EstadoControles(2, true, true, true, true);
                }
                else
                    EstadoControles(2, false, false, false, false);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente de Zona.");
            }
        }

        protected void gvGerenteZona_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteZona.PageIndex = e.NewPageIndex;
            gvGerenteZona.DataSource = Session["SesionGerenteZona"];
            gvGerenteZona.DataBind();
        }

        protected void gvGerenteZona_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                case "Editar":

                    break;

                case "Eliminar":

                    Label lblAnhoCampanha = (Label)gvGerenteZona.Rows[index].FindControl("lblAnioCampana");

                    obeGerenteZona.intIDGerenteZona = Convert.ToInt32(gvGerenteZona.DataKeys[index].Values[0].ToString());
                    obeGerenteZona.intIDGerenteRegion = 0;
                    obeGerenteZona.intUsuarioCrea = 1;
                    obeGerenteZona.chrIndicadorMigrado = "1";
                    obeGerenteZona.chrCampaniaBaja = lblAnhoCampanha.Text; //FALTA DATO
                    oblGerenteZona.GerenteZonaActualizarEstado(obeGerenteZona);
                    ListarGerenteZonaAll(ddlPais.SelectedValue, txtColaborador.Text, ddlRegion.SelectedValue, ddlZona.SelectedValue, ddlPeriodos.SelectedValue);
                    break;
            }
        }

        protected void gvGerenteZona_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    ImageButton ImgBtnEditar = (ImageButton)e.Row.FindControl("imgbtnEditar");
                    ImgBtnEditar.CommandArgument = e.Row.RowIndex.ToString();

                    ImageButton ImgBtnEliminar = (ImageButton)e.Row.FindControl("imgbtnEliminar");
                    ImgBtnEliminar.CommandArgument = e.Row.RowIndex.ToString();
                    break;
            }
        }

        #endregion "Gerente Zona"

        private void CargarPeriodos(string prefijoPais)
        {
            BlPeriodos blPeriodo = new BlPeriodos();
            List<string> periodos = blPeriodo.ObtenerPeriodos(prefijoPais);

            ddlPeriodos.DataSource = periodos;
            ddlPeriodos.DataBind();
        }
    }
}