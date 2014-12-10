
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using BusinessEntity;
    using BusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using WsPlanDesarrollo;

    public partial class AltasBajas1 : Page
    {
        #region "Evento de Página"

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack)
                {
                    CargarInicio();
                    ListarPais();
                    ListarTipoColaborador();
                    EstadoControles(1, false, false);
                    EstadoControles(2, false, false);
                }
            }
            catch (Exception)
            {
                //AlertaMensaje(ConfigurationSettings.AppSettings["MensajeAlertaPagina"]);
                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAlertaPagina"]);
            }
        }

        #endregion "Evento de Página"

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
                if (ddlRegion.SelectedIndex > 0)
                    ddlZona.Items.Insert(0, new ListItem("[TODOS]", "0"));
                else
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
                divLeyenda.Visible = region;
            }
        }

        private void AlertaMensaje(string strMensaje)
        {
            string clienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", clienteScript, false);
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

        private string ObtenerPais(string codigoPais)
        {
            BlPais oblPais = new BlPais();
            string respuesta = string.Empty;

            BePais obePais = oblPais.ObtenerPais(codigoPais);

            if (obePais != null)
                respuesta = obePais.CodigoPaisAdam;

            return respuesta;
        }

        private bool ValidarAnioCampnia(string anioCampania)
        {
            bool respuesta = false;

            if (string.IsNullOrEmpty(anioCampania))
                return true;

            if (anioCampania.Length == 6)
            {
                int anioAux = Convert.ToInt32(Left(anioCampania, 4));
                int campaniaAux = Convert.ToInt32(Right(anioCampania, 2));

                if (anioAux > Convert.ToInt32(hdenAnioAuxMinimo.Value) && anioAux < Convert.ToInt32(hdenAnioAuxMaximo.Value))
                {
                    if (campaniaAux >= Convert.ToInt32(hdenCampaniaAuxMinimo.Value) && campaniaAux <= Convert.ToInt32(hdenCampaniaAuxMaximo.Value))
                    {
                        respuesta = true;
                    }
                }
            }
            return respuesta;
        }

        private void CargarInicio()
        {
            hdenCodigoPais.Value = string.Empty;
            hdenCodigoUsuario.Value = string.Empty;
            hdenAnioAuxMinimo.Value = ConfigurationManager.AppSettings["AnioAuxMinimo"].ToString();
            hdenAnioAuxMaximo.Value = ConfigurationManager.AppSettings["AnioAuxMaximo"].ToString();
            hdenCampaniaAuxMinimo.Value = ConfigurationManager.AppSettings["campaniaAuxMinimo"].ToString();
            hdenCampaniaAuxMaximo.Value = ConfigurationManager.AppSettings["campaniaAuxMaximo"].ToString();
        }

        #endregion "Metodos Comunes"

        #region "Eventos Comunes"

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoColaborador.SelectedValue == "1")
                Response.Redirect("DirectoraVentas.aspx?ID=" + ddlPais.SelectedValue + "|1");
            else
            {
                ListarRegion(ddlPais.SelectedValue, string.Empty);
            }

            CargarPeriodos(ddlPais.SelectedValue);
            CargarAnhos(ddlPais.SelectedValue);
        }

        protected void ddlTipoColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            rfvRegion.ValidationGroup = string.Empty;
            rfvRegion.Visible = false;

            gvGerenteRegion.Visible = false;
            gvGerenteZona.Visible = false;
            btnCancelar.Visible = false;
            btnGrabar.Visible = false;

            EstadoControles(2, false, false);
            switch (ddlTipoColaborador.SelectedValue)
            {
                case "0":
                    EstadoControles(1, false, false);
                    break;

                case "1":
                    EstadoControles(1, false, false);
                    if (ddlPais.SelectedValue != "0")
                        Response.Redirect("DirectoraVentas.aspx?ID=" + ddlPais.SelectedValue + "|1");
                    break;

                case "2":
                    EstadoControles(1, true, false);
                    ddlRegion.SelectedIndex = 0;
                    ddlZona.Items.Clear();
                    ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    gvGerenteRegion.Visible = true;
                    break;

                case "3":
                    EstadoControles(1, true, true);
                    rfvRegion.ValidationGroup = "Buscar";
                    //rfvRegion.Visible = true; 11-01-2013
                    gvGerenteZona.Visible = true;
                    break;
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codidgoRegion = string.Empty;
            if (ddlRegion.SelectedValue != "0")
            {
                string[] words = ddlRegion.SelectedValue.Split('|');
                codidgoRegion = words[1];
            }
            ListarZona(ddlPais.SelectedValue, codidgoRegion, string.Empty);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Session["TablaGerenteRegion"] = null;
            gvGerenteRegion.DataSource = null;
            gvGerenteRegion.DataBind();
            gvGerenteRegion.EmptyDataText = string.Empty;

            Session["TablaGerenteZona"] = null;
            gvGerenteZona.DataSource = null;
            gvGerenteZona.DataBind();
            gvGerenteZona.EmptyDataText = string.Empty;

            try
            {
                if (ddlPais.SelectedValue != "0")
                {
                    if (ddlTipoColaborador.SelectedValue != "0" || ddlTipoColaborador.SelectedValue != "1")
                    {
                        if (ddlTipoColaborador.SelectedValue == "2")
                        {
                            if (ValidarAnioCampnia(txtAnioCampania.Text))
                            {
                                //LISTA GERENTE REGION

                                ListarGerenteRegionAlta(ddlPais.SelectedValue, txtAnioCampania.Text, ddlRegion.SelectedValue, txtColaborador.Text, ddlPeriodos.SelectedValue);
                            }
                            else
                                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAnioCampania"].ToString());
                        }
                        else
                        {
                            if (ValidarAnioCampnia(txtAnioCampania.Text))
                            {
                                //LISTA GERENTE ZONA
                                ListarGerenteZonaAlta(ddlPais.SelectedValue, txtAnioCampania.Text, ddlRegion.SelectedValue, ddlZona.SelectedValue, txtColaborador.Text, ddlPeriodos.SelectedValue);
                            }
                            else
                                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAnioCampania"].ToString());
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPais.SelectedValue != "0")
                {
                    if (ddlTipoColaborador.SelectedValue != "0" || ddlTipoColaborador.SelectedValue != "1")
                    {
                        if (ddlTipoColaborador.SelectedValue == "2")
                        {
                            //GRABAR GERENTE REGION
                            GrabarGerenteRegion();
                        }
                        else
                        {
                            if (ddlTipoColaborador.SelectedValue == "3")
                            {
                                //GRABAR GERENTE ZONA
                                GrabarGerenteZona();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AlertaMensaje("Error al grabar. " + ex.Message);
            }
        }

        #endregion "Eventos Comunes"

        #region "Gerente Region"

        private List<BeDirectoraVentas> ListarDirectoraVentas()
        {
            List<BeDirectoraVentas> oList = new List<BeDirectoraVentas>();
            try
            {
                BlDirectoraVentas oblDirectoraVentas = new BlDirectoraVentas();
                oList = new List<BeDirectoraVentas>(oblDirectoraVentas.DirectoraVentasListar(ddlPais.SelectedValue));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Directora de Ventas.");
            }
            return oList;
        }

        private void ListarGerenteRegionAlta(string codigoPais, string anioCampania, string codigoRegion, string nombreGerente, string periodo)
        {
            try
            {
                if (codigoRegion != "0")
                {
                    string[] words = codigoRegion.Split('|');
                    codigoRegion = words[1];
                }

                BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();

                List<BeGerenteRegion> gerenteRegionLista = new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListarAlta(codigoPais, anioCampania, codigoRegion, nombreGerente, periodo));

                DataTable dtGerenteRegion = new DataTable();
                dtGerenteRegion.Columns.Add("AnioCampana");
                dtGerenteRegion.Columns.Add("chrCodigoDataMart");
                dtGerenteRegion.Columns.Add("DesGerenteRegional");
                dtGerenteRegion.Columns.Add("DocIdentidad");
                dtGerenteRegion.Columns.Add("CorreoElectronico");
                dtGerenteRegion.Columns.Add("CodRegion");
                dtGerenteRegion.Columns.Add("CUBGR");

                string codigoPaisAdam = ObtenerPais(codigoPais);

                foreach (BeGerenteRegion gerenteRegion in gerenteRegionLista)
                {
                    dtGerenteRegion.Rows.Add(gerenteRegion.AnioCampana, gerenteRegion.ChrCodigoDataMart, gerenteRegion.DesGerenteRegional, gerenteRegion.DocIdentidad, gerenteRegion.VchCorreoElectronico, gerenteRegion.CodRegion, gerenteRegion.VchCUBGR);
                }

                /* Inicio Validación con Servicio Competencia 14/12/2012 */

                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("AnioCampana");
                dtTemp.Columns.Add("ChrCodigoDataMart");
                dtTemp.Columns.Add("DesGerenteRegional");
                dtTemp.Columns.Add("DocIdentidad");
                dtTemp.Columns.Add("CorreoElectronico");
                dtTemp.Columns.Add("CodRegion");
                dtTemp.Columns.Add("Origen"); //Para saber de donde viene: 1 = MAE, 2 = COMPETENCIA
                dtTemp.Columns.Add("CUBGR");

                int anio = Convert.ToInt32(ddlAnhoCompetencia.SelectedValue);

                foreach (DataRow row in dtGerenteRegion.Rows)
                {
                    string docIdentidad = row["DocIdentidad"].ToString().Trim();
                    string cub = row["CUBGR"].ToString().Trim();

                    string nuevoDocIdentidad = GetCodigoCompetencia(codigoPais, anio.ToString(), codigoPaisAdam, docIdentidad, cub);

                    if (!string.IsNullOrEmpty(nuevoDocIdentidad))
                    {
                        row["DocIdentidad"] = nuevoDocIdentidad;
                    }

                    dtTemp.Rows.Add(row["AnioCampana"],
                                    row["ChrCodigoDataMart"],
                                    row["DesGerenteRegional"],
                                    row["DocIdentidad"].ToString().Trim(),
                                    row["CorreoElectronico"],
                                    row["CodRegion"],
                                    string.IsNullOrEmpty(nuevoDocIdentidad) ? "1" : "2",
                                    row["CUBGR"]);
                }

                /* Fin Validación con Servicio Competencia */

                if (dtTemp.Rows.Count == 0)
                {
                    gvGerenteRegion.EmptyDataText = "Sin Registros";
                }

                Session["TablaGerenteRegion"] = dtTemp;

                gvGerenteRegion.DataSource = dtTemp; //dtGerenteRegion;
                gvGerenteRegion.DataBind();

                if (gvGerenteRegion.Rows.Count > 0)
                {
                    EstadoControles(2, true, true);
                }
                else
                    EstadoControles(2, false, false);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente Región.");
            }
        }

        private bool VerificaRegistroChecksRegion()
        {
            bool resultado = false;
            for (int i = 0; i < gvGerenteRegion.Rows.Count; i++)
            {
                CheckBox chkbox = ((CheckBox)gvGerenteRegion.Rows[i].Cells[2].FindControl("chkEliminar"));
                if (chkbox.Checked)
                {
                    DropDownList ddlDirectora = (DropDownList)gvGerenteRegion.Rows[i].Cells[2].FindControl("ddlDirectora");
                    if (ddlDirectora.SelectedValue == "0")
                    {
                        int fila = i + 1;
                        AlertaMensaje("Debe seleccionar un Director de Ventas en la fila " + fila.ToString() + ".");
                        return false;
                    }


                    //TextBox documento = (TextBox)gvGerenteRegion.Rows[i].Cells[2].FindControl("txtDocumento");
                    //if (documento.Text.IndexOf("-") != -1)
                    //{
                    //    int fila = i + 1;
                    //    AlertaMensaje("El documento de la fila " + fila.ToString() + " no debe tener guión.");
                    //    return false;

                    //}



                    resultado = true;
                }
            }
            return resultado;
        }

        private void GrabarGerenteRegion()
        {
            List<BeGerenteRegion> listaGerenteRegion = new List<BeGerenteRegion>();
            BlGerenteRegion gerenteRegionBL = new BlGerenteRegion();
            try
            {
                if (VerificaRegistroChecksRegion())
                {
                    foreach (GridViewRow row in gvGerenteRegion.Rows)
                    {
                        CheckBox chkbox = ((CheckBox)row.FindControl("chkEliminar"));

                        if (chkbox.Checked)
                        {
                            DropDownList ddlDirectora = (DropDownList)row.FindControl("ddlDirectora");
                            Label lblAnioCampana = (Label)row.FindControl("lblAnioCampana");
                            Label lblCodigoDataMart = (Label)row.FindControl("lblCodigoDatamart");
                            TextBox txtDocumento = (TextBox)row.FindControl("txtDocumento");
                            Label lblCodigoRegion = (Label)row.FindControl("lblCodigoRegion");
                            TextBox txtDesGerenteRegional = (TextBox)row.FindControl("txtDesGerenteRegional");
                            TextBox txtEmail = (TextBox)row.FindControl("txtCorreoElectronico");
                            TextBox txtCub = (TextBox)row.FindControl("txtCUB");

                            BeGerenteRegion gerenteRegion = new BeGerenteRegion();
                            gerenteRegion.AnioCampana = lblAnioCampana.Text;
                            gerenteRegion.DesGerenteRegional = txtDesGerenteRegional.Text;
                            gerenteRegion.DocIdentidad = txtDocumento.Text;
                            gerenteRegion.ChrPrefijoIsoPais = ddlPais.SelectedValue;
                            gerenteRegion.VchCorreoElectronico = txtEmail.Text;
                            gerenteRegion.IntUsuarioCrea = 1; //CAMBIAR
                            gerenteRegion.ChrCodDirectorVenta = ddlDirectora.SelectedValue;
                            gerenteRegion.ChrCodigoDataMart = lblCodigoDataMart.Text;
                            gerenteRegion.chrIndicadorMigrado = "1"; // 1=Altas
                            gerenteRegion.CodRegion = lblCodigoRegion.Text;
                            gerenteRegion.Periodo = ddlPeriodos.SelectedValue;
                            gerenteRegion.VchCUBGR = txtCub.Text;

                            listaGerenteRegion.Add(gerenteRegion);
                        }
                    }

                    gerenteRegionBL.GerenteRegionRegistrar(listaGerenteRegion);
                    AlertaMensaje("Se grabó correctamente.");
                    ListarGerenteRegionAlta(ddlPais.SelectedValue, txtAnioCampania.Text, ddlRegion.SelectedValue, txtColaborador.Text, ddlPeriodos.SelectedValue);
                }
                else
                {
                    AlertaMensaje("Debe Seleccionar para grabar.");
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Sucedió un error al grabar.");
            }
        }

        protected void gvGerenteRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteRegion.PageIndex = e.NewPageIndex;
            gvGerenteRegion.DataSource = Session["TablaGerenteRegion"];
            gvGerenteRegion.DataBind();
        }

        protected void gvGerenteRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    /* Validación WebService Competencia 14/12/2012 */
                    Label lblOrigen = (Label)e.Row.FindControl("lblOrigen");
                    Image imgOrigen = (Image)e.Row.FindControl("imgOrigen");
                    if (lblOrigen.Text != "1")
                        imgOrigen.Visible = false;
                    /* Fin validacion */

                    DropDownList ddlDirectora = (DropDownList)e.Row.FindControl("ddlDirectora");

                    ddlDirectora.Items.Clear();

                    ddlDirectora.DataSource = ListarDirectoraVentas();
                    ddlDirectora.DataValueField = "chrCodigoDirectoraVentas"; //"intIDDirectoraVenta";
                    ddlDirectora.DataTextField = "vchNombreCompleto";
                    ddlDirectora.DataBind();
                    ddlDirectora.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    break;
            }
        }

        #endregion "Gerente Region"

        #region "Gerente Zona"

        private List<BeGerenteRegion> ListarGerenteRegion()
        {
            List<BeGerenteRegion> oList = new List<BeGerenteRegion>();
            try
            {
                BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();
                oList =
                    new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListar(ddlPais.SelectedValue, string.Empty, string.Empty, string.Empty));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente Región.");
            }
            return oList;
        }

        private void ListarGerenteZonaAlta(string codigoPais, string anioCampania, string codigoRegion, string codigoZona, string nombreGerente, string periodo)
        {
            try
            {
                if (codigoRegion != "0")
                {
                    string[] words = codigoRegion.Split('|');
                    codigoRegion = words[1];
                }

                BlGerenteZona gerenteZonaBL = new BlGerenteZona();
                List<BeGerenteZona> gerenteZonaLista = new List<BeGerenteZona>(gerenteZonaBL.GerenteZonaListarAlta(codigoPais, anioCampania, codigoRegion, codigoZona, nombreGerente, periodo));

                DataTable dtGerenteZona = new DataTable();
                dtGerenteZona.Columns.Add("AnioCampana");
                dtGerenteZona.Columns.Add("chrCodigoDataMart");
                dtGerenteZona.Columns.Add("DesGerenteZona");
                dtGerenteZona.Columns.Add("DocIdentidad");
                dtGerenteZona.Columns.Add("CorreoElectronico");
                dtGerenteZona.Columns.Add("CodRegion");
                dtGerenteZona.Columns.Add("CodZona");
                dtGerenteZona.Columns.Add("CUBGZ");

                string codigoPaisAdam = ObtenerPais(codigoPais);

                foreach (BeGerenteZona gerenteZona in gerenteZonaLista)
                {
                    dtGerenteZona.Rows.Add(gerenteZona.AnioCampana, gerenteZona.chrCodigoDataMart, gerenteZona.DesGerenteZona,
                                           gerenteZona.DocIdentidad, gerenteZona.CorreoElectronico, gerenteZona.CodRegion, gerenteZona.codZona, gerenteZona.CUBGZ);
                }

                /* Inicio Validación con Servicio Competencia 14/12/2012 */
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("AnioCampana");
                dtTemp.Columns.Add("chrCodigoDataMart");
                dtTemp.Columns.Add("DesGerenteZona");
                dtTemp.Columns.Add("DocIdentidad");
                dtTemp.Columns.Add("CorreoElectronico");
                dtTemp.Columns.Add("CodRegion");
                dtTemp.Columns.Add("CodZona");
                dtTemp.Columns.Add("Origen");
                dtTemp.Columns.Add("CUBGZ");

                int anio = Convert.ToInt32(ddlAnhoCompetencia.SelectedValue);

                foreach (DataRow row in dtGerenteZona.Rows)
                {
                    string docIdentidad = row["DocIdentidad"].ToString().Trim();
                    string cub = row["CUBGZ"].ToString().Trim();

                    string nuevoDocIdentidad = GetCodigoCompetencia(codigoPais, anio.ToString(), codigoPaisAdam, docIdentidad, cub);

                    dtTemp.Rows.Add(row["AnioCampana"], row["chrCodigoDataMart"],
                                    row["DesGerenteZona"], row["DocIdentidad"].ToString().Trim(),
                                    row["CorreoElectronico"], row["CodRegion"], row["CodZona"],
                                    string.IsNullOrEmpty(nuevoDocIdentidad) ? "1" : "2", row["CUBGZ"]);
                }
                /* Fin Validación con Servicio Competencia */

                if (dtTemp.Rows.Count == 0)
                {
                    gvGerenteZona.EmptyDataText = "Sin Registros";
                }

                Session["TablaGerenteZona"] = dtTemp;

                gvGerenteZona.DataSource = dtTemp; //dtGerenteZona;
                gvGerenteZona.DataBind();

                if (gvGerenteZona.Rows.Count > 0)
                {
                    EstadoControles(2, true, true);
                }
                else
                    EstadoControles(2, false, false);
            }
            catch (Exception ex)
            {
                AlertaMensaje("Error al listar Gerente de Zona. Error: " + ex.Message);
            }
        }

        private bool VerificaRegistroChecksZona()
        {
            bool resultado = false;
            for (int i = 0; i < gvGerenteZona.Rows.Count; i++)
            {
                CheckBox chkbox = ((CheckBox)gvGerenteZona.Rows[i].Cells[2].FindControl("chkEliminar"));
                if (chkbox.Checked)
                {
                    DropDownList gerenteRegion = (DropDownList)gvGerenteZona.Rows[i].Cells[2].FindControl("ddlGerenteRegion");
                    if (gerenteRegion.SelectedValue == "0")
                    {
                        int fila = i + 1;
                        AlertaMensaje("Debe seleccionar un Gerente Región en la fila " + fila.ToString() + ".");
                        return false;
                    }
                    //TextBox documento = (TextBox)gvGerenteZona.Rows[i].Cells[2].FindControl("txtDocumento");
                    //if (documento.Text.IndexOf("-") != -1)
                    //{
                    //    int fila = i + 1;
                    //    AlertaMensaje("El documento de la fila " + fila.ToString() + " no debe tener guión.");
                    //    return false;

                    //}



                    resultado = true;
                }
            }
            return resultado;
        }

        private void GrabarGerenteZona()
        {
            List<BeGerenteZona> listaGerenteZona = new List<BeGerenteZona>();
            BlGerenteZona gerenteZonaBL = new BlGerenteZona();



            try
            {
                if (VerificaRegistroChecksZona())
                {
                    foreach (GridViewRow row in gvGerenteZona.Rows)
                    {
                        CheckBox chkbox = ((CheckBox)row.FindControl("chkEliminar"));
                        if (chkbox.Checked)
                        {
                            BeGerenteZona gerenteZona = new BeGerenteZona();

                            DropDownList gerenteRegion = (DropDownList)row.FindControl("ddlGerenteRegion");
                            Label lblAnioCampania = (Label)row.FindControl("lblAnioCampania");
                            Label lblCodigoDatamart = (Label)row.FindControl("lblCodigoDataMart");
                            TextBox txtDocumento = (TextBox)row.FindControl("txtDocumento");
                            Label lblCodigoRegion = (Label)row.FindControl("lblCodigoRegion");
                            Label lblCodigoZona = (Label)row.FindControl("lblCodigoZona");
                            TextBox txtDesGerenteZona = (TextBox)row.FindControl("txtDesGerenteZona");
                            TextBox txtEmail = (TextBox)row.FindControl("txtCorreoElectronico");
                            TextBox txtCub = (TextBox)row.FindControl("txtCUB");

                            int idGerenteRegion = Convert.ToInt32(gerenteRegion.SelectedValue);

                            gerenteZona.chrPrefijoIsoPais = ddlPais.SelectedValue;
                            gerenteZona.DocIdentidad = txtDocumento.Text;
                            gerenteZona.DesGerenteZona = txtDesGerenteZona.Text;
                            gerenteZona.vchCorreoElectronico = txtEmail.Text;
                            gerenteZona.intUsuarioCrea = 1; //CAMBIAR
                            gerenteZona.chrCodigoDataMart = lblCodigoDatamart.Text;
                            gerenteZona.chrCampaniaRegistro = lblAnioCampania.Text;
                            gerenteZona.chrIndicadorMigrado = "1"; // 1=Altas
                            gerenteZona.intIDGerenteRegion = idGerenteRegion;
                            gerenteZona.CodRegion = lblCodigoRegion.Text;
                            gerenteZona.codZona = lblCodigoZona.Text;
                            gerenteZona.Periodo = ddlPeriodos.SelectedValue;
                            gerenteZona.CUBGZ = txtCub.Text;

                            listaGerenteZona.Add(gerenteZona);
                        }
                    }

                    gerenteZonaBL.GerenteZonaRegistrar(listaGerenteZona);

                    AlertaMensaje("Se grabó correctamente.");

                    ListarGerenteZonaAlta(ddlPais.SelectedValue, txtAnioCampania.Text, ddlRegion.SelectedValue, ddlZona.SelectedValue, txtColaborador.Text, ddlPeriodos.SelectedValue);
                }
                else
                {
                    AlertaMensaje("Debe Seleccionar para grabar.");
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Sucedió un error al grabar.");
            }
        }

        protected void gvGerenteZona_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteZona.PageIndex = e.NewPageIndex;
            gvGerenteZona.DataSource = Session["TablaGerenteZona"];
            gvGerenteZona.DataBind();
        }

        protected void gvGerenteZona_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    /* Validación WebService Competencia 14/12/2012 */
                    Label lblOrigen = (Label)e.Row.FindControl("lblOrigen");
                    Image imgOrigen = (Image)e.Row.FindControl("imgOrigen");
                    if (lblOrigen.Text != "1")
                        imgOrigen.Visible = false;
                    /* Fin validacion */

                    DropDownList ddlGerenteRegion = (DropDownList)e.Row.FindControl("ddlGerenteRegion");

                    ddlGerenteRegion.Items.Clear();

                    ddlGerenteRegion.DataSource = ListarGerenteRegion();

                    ddlGerenteRegion.DataValueField = "intIDGerenteRegion";
                    ddlGerenteRegion.DataTextField = "vchNombrecompleto";
                    ddlGerenteRegion.DataBind();
                    ddlGerenteRegion.Items.Insert(0, new ListItem("[Seleccione]", "0"));

                    if (ddlGerenteRegion.SelectedValue != "0")
                        ddlGerenteRegion.Enabled = false;
                    break;
            }
        }

        #endregion "Gerente Zona"

        #region Consulta WebService Competencia

        private string GetCodigoCompetencia(string codPais, string anho, string codPaisAdam, string docIdentidad, string cub)
        {
            string codDocumentoIdentidad;
            switch (codPais.ToUpper())
            {
                case "AR":
                    codDocumentoIdentidad = GetCodigoCompetenciaAlfaNum(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "BO":
                    codDocumentoIdentidad = GetCodigoCompetenciaAlfaNum(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "BR":
                    codDocumentoIdentidad = GetCodigoCompetenciaEnteroAndCero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "CL":
                    codDocumentoIdentidad = GetCodigoCompetenciaAlfaNum(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "CO":
                    codDocumentoIdentidad = GetCodigoCompetenciaEntero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "CR":
                    codDocumentoIdentidad = GetCodigoCompetenciaEntero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "DO":
                    codDocumentoIdentidad = GetCodigoCompetenciaEnteroAndCero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "EC":
                    codDocumentoIdentidad = GetCodigoCompetenciaEnteroAndCero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "GT":
                    codDocumentoIdentidad = GetCodigoCompetenciaAlfaNum(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "MX":
                    codDocumentoIdentidad = GetCodigoCompetenciaAlfaNum(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "PA":
                    codDocumentoIdentidad = GetCodigoCompetenciaAlfaNum(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "PE":
                    codDocumentoIdentidad = GetCodigoCompetenciaEnteroAndCero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "PR":
                    codDocumentoIdentidad = GetCodigoCompetenciaEnteroAndCero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "SV":
                    codDocumentoIdentidad = GetCodigoCompetenciaEnteroAndCero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                case "VE":
                    codDocumentoIdentidad = GetCodigoCompetenciaEntero(anho, codPaisAdam, docIdentidad, cub);
                    break;
                default:
                    codDocumentoIdentidad = GetCodigoCompetenciaDefault(anho, codPaisAdam, docIdentidad, cub);
                    break;
            }

            return codDocumentoIdentidad;
        }

        private string GetCodigoCompetenciaDefault(string anho, string codPaisAdam, string docIdentidad, string cub)
        {
            WsInterfaceFFVVSoapClient wsCompetencia = new WsInterfaceFFVVSoapClient();
            string codDocumentoIdentidad = string.Empty;

            //Probamos con documento Identidad sin Ceros

            //DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPorcentajeAvanceCompetencia(Convert.ToInt32(anho), codPaisAdam, docIdentidad);
            DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPlanDesarrollo(Convert.ToInt32(anho), cub);

            if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count > 0)
            {
                //codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["NumeroDocumentoColaboradorEvaluado"]);
                codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DocumentoIdentidadColaborador"]);
            }

            return codDocumentoIdentidad;
        }

        private string GetCodigoCompetenciaEntero(string anho, string codPaisAdam, string docIdentidad, string cub)
        {
            WsInterfaceFFVVSoapClient wsCompetencia = new WsInterfaceFFVVSoapClient();
            string codDocumentoIdentidad = string.Empty;
            //string codDocumentoIdentidadAux = Convert.ToDouble(docIdentidad).ToString();
            string codDocumentoIdentidadAux = docIdentidad.Trim();

            //Probamos con documento Identidad sin Ceros
            //DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPorcentajeAvanceCompetencia(Convert.ToInt32(anho), codPaisAdam, codDocumentoIdentidadAux);
            DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPlanDesarrollo(Convert.ToInt32(anho), cub);

            if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count > 0)
            {
                //codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["NumeroDocumentoColaboradorEvaluado"]);
                codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DocumentoIdentidadColaborador"]);
            }

            return codDocumentoIdentidad;
        }

        private string GetCodigoCompetenciaAlfaNum(string anho, string codPaisAdam, string docIdentidad, string cub)
        {
            WsInterfaceFFVVSoapClient wsCompetencia = new WsInterfaceFFVVSoapClient();
            string codDocumentoIdentidad = string.Empty;

            string codDocumentoIdentidadAux = LimpiarCeros(docIdentidad);

            //Probamos con documento Identidad sin Ceros

            //DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPorcentajeAvanceCompetencia(Convert.ToInt32(anho), codPaisAdam, codDocumentoIdentidadAux);
            DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPlanDesarrollo(Convert.ToInt32(anho), cub);

            if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count > 0)
            {
                //codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["NumeroDocumentoColaboradorEvaluado"]);
                codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DocumentoIdentidadColaborador"]);
            }
            else
            {
                int contador = 0;
                //Probamos con documento Identidad con 1 Cero más hasta 3 veces

                while (contador < 3)
                {
                    codDocumentoIdentidadAux = string.Format("0{0}", codDocumentoIdentidadAux);
                    //dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPorcentajeAvanceCompetencia(Convert.ToInt32(anho), codPaisAdam, codDocumentoIdentidadAux);
                    dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPlanDesarrollo(Convert.ToInt32(anho), cub);

                    if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count > 0)
                    {
                        //codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["NumeroDocumentoColaboradorEvaluado"]);
                        codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DocumentoIdentidadColaborador"]);
                        break;
                    }

                    contador++;
                }
            }
            return codDocumentoIdentidad;
        }

        private string GetCodigoCompetenciaEnteroAndCero(string anho, string codPaisAdam, string docIdentidad, string cub)
        {
            WsInterfaceFFVVSoapClient wsCompetencia = new WsInterfaceFFVVSoapClient();
            string codDocumentoIdentidad = string.Empty;
            string codDocumentoIdentidadAux = docIdentidad.ToString();

            //Probamos con documento Identidad sin Ceros

            //DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPorcentajeAvanceCompetencia(Convert.ToInt32(anho), codPaisAdam, codDocumentoIdentidadAux);
            DataSet dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPlanDesarrollo(Convert.ToInt32(anho), cub);

            if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count > 0)
            {
                //codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["NumeroDocumentoColaboradorEvaluado"]);
                codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DocumentoIdentidadColaborador"]);
            }
            else
            {
                int contador = 0;
                //Probamos con documento Identidad con 1 Cero más hasta 3 veces

                while (contador < 3)
                {
                    codDocumentoIdentidadAux = string.Format("0{0}", codDocumentoIdentidadAux);
                    //dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPorcentajeAvanceCompetencia(Convert.ToInt32(anho), codPaisAdam, codDocumentoIdentidadAux);
                    dtConsultaPlanDesarrollo = wsCompetencia.ConsultaPlanDesarrollo(Convert.ToInt32(anho), cub);

                    if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count > 0)
                    {
                        //codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["NumeroDocumentoColaboradorEvaluado"]);
                        codDocumentoIdentidad = Convert.ToString(dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DocumentoIdentidadColaborador"]);
                        break;
                    }

                    contador++;
                }
            }

            return codDocumentoIdentidad;
        }

        private string LimpiarCeros(string cadena)
        {
            while (cadena.StartsWith("0"))
            {
                cadena = cadena.Substring(1);
            }

            return cadena;
        }

        private void CargarPeriodos(string prefijoPais)
        {
            BlPeriodos blPeriodo = new BlPeriodos();
            List<string> periodos = blPeriodo.ObtenerPeriodos(prefijoPais);

            ddlPeriodos.DataSource = periodos;
            ddlPeriodos.DataBind();
        }

        private void CargarAnhos(string prefijoPais)
        {
            BlMatriz matrizBL = new BlMatriz();

            List<BeComun> entidades = matrizBL.ListarAnhos(prefijoPais);

            entidades.Sort((x, y) => string.Compare(y.Codigo, x.Codigo));

            ddlAnhoCompetencia.DataSource = entidades;
            ddlAnhoCompetencia.DataTextField = "Descripcion";
            ddlAnhoCompetencia.DataValueField = "Codigo";
            ddlAnhoCompetencia.DataBind();
        }

        #endregion Consulta WebService Competencia
    }
}