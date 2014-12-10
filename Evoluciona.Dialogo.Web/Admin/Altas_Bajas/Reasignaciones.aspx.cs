
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Reasignaciones : Page
    {
        #region "Variables Globales"

        private BeGerenteZona obeGerenteZona = new BeGerenteZona();
        private BlGerenteZona oblGerenteZona = new BlGerenteZona();
        private BeGerenteRegion obeGerenteRegion = new BeGerenteRegion();
        private BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();
        private BeProceso obeProceso = new BeProceso();
        private BlProceso oblProceso = new BlProceso();
        private BeAdmin _objAdmin;

        private string IDProceso,
                       IDRol,
                       CodigoUsuario,
                       Usuario,
                       Periodo,
                       FechaLimiteProceso,
                       EstadoProceso,
                       intIDRolEvaluador,
                       CodigoEvaluador,
                       PrefijoIsoPais,
                       NuevasIngresadas,
                       RegionZona;

        #endregion "Variables Globales"

        #region "Evento de Página"

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            try
            {
                if (!IsPostBack)
                {
                    _objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
                    Session["SesionDirectoraVentas"] = null;
                    Session["SesionGerenteRegion"] = null;
                    Session["SesionGerenteZona"] = null;
                    SeleccionarPais();
                    ListarPais();
                    rblReasignar.SelectedValue = "1";
                    divAsignacion.Visible = false;
                    divProceso.Visible = false;
                    List<BeGerenteZona> oList = new List<BeGerenteZona>();

                    BeGerenteRegion gr = new BeGerenteRegion();
                    oList.Add(new BeGerenteZona { obeGerenteRegion = gr });
                    gvGerenteZona.DataSource = oList;
                    gvGerenteZona.DataBind();
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAlertaPagina"].ToString());
            }
        }

        #endregion "Evento de Página"

        #region "Eventos Comunes"

        private void ListarPais()
        {
            try
            {
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

        private void ListarGerenteRegion(string codigoPais, string nombreGerente)
        {
            try
            {
                BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();
                List<BeGerenteRegion> oList =
                    new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListar(codigoPais, nombreGerente, "", ""));
                if (oList != null)
                {
                    ddlRegion.Items.Clear();
                    ddlRegion.DataSource = oList;
                    ddlRegion.DataValueField = "IntIDGerenteRegion";
                    ddlRegion.DataTextField = "VchNombrecompleto";
                    ddlRegion.DataBind();
                    ddlZona.Items.Clear();
                    ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    if (oList.Count > 0)
                        ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue), "", 1);
                    else
                    {
                        ddlRegion.Items.Clear();
                        ddlRegion.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar gerentes de region.");
            }
        }

        private void ListarGerenteZona(string codigoPais, int codigoRegion, string nombreGerente, int tipo)
        {
            try
            {
                BlGerenteZona oblGerenteZona = new BlGerenteZona();
                List<BeGerenteZona> oList =
                    new List<BeGerenteZona>(oblGerenteZona.GerenteZonaListar(codigoPais, codigoRegion, nombreGerente, 0)); //07/12/2012

                if (oList != null)
                {
                    if (tipo == 1)
                    {
                        ddlZona.Items.Clear();
                        ddlZona.DataSource = oList;
                        ddlZona.DataValueField = "intIDGerenteZona";
                        ddlZona.DataTextField = "vchNombreCompleto";
                        ddlZona.DataBind();
                        ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    }
                    else
                    {
                        gvGerenteZona.DataSource = oList;

                        gvGerenteZona.DataBind();
                        Session["SesionGerenteZona"] = oList;
                        if (gvGerenteZona.Rows.Count > 0)
                        {
                            gvGerenteZona.Columns[6].Visible = (rblReasignar.SelectedValue == "1") ? true : false;
                            divAsignacion.Visible = true;
                        }
                        else
                            divAsignacion.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar gerente de zonas.");
            }
        }

        private void AlertaMensaje(string strMensaje)
        {
            string ClienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", ClienteScript, false);
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarGerenteRegion(ddlPais.SelectedValue, "");
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue), "", 1);
        }

        #endregion "Eventos Comunes"

        #region "Gerente Zona"

        protected void gvGerenteZona_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenteZona.PageIndex = e.NewPageIndex;
            gvGerenteZona.DataSource = Session["SesionGerenteZona"];
            gvGerenteZona.DataBind();
        }

        protected void gvGerenteZona_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    Label lblIDGerenteRegion = (Label)e.Row.FindControl("lblIDGerenteRegion");
                    Label lblCodigoGerenteRegion = (Label)e.Row.FindControl("lblCodigoGerenteRegion");
                    Label lblGerenteRegion = (Label)e.Row.FindControl("lblGerenteRegion");
                    DropDownList ddlGerenteRegion = (DropDownList)e.Row.FindControl("ddlGerenteRegion");
                    DropDownList DdlDirectora = (DropDownList)e.Row.FindControl("ddlDirectora");

                    if (rblReasignar.SelectedValue == "1")
                    {
                        lblGerenteRegion.Visible = true;
                        ddlGerenteRegion.Visible = false;
                        DdlDirectora.Visible = true;

                        //Directora Ventas
                        DdlDirectora.Items.Clear();
                        if (Session["SesionDirectoraVentas"] == null)
                        {
                            Session["SesionDirectoraVentas"] = ListarDirectoraVentas();
                            DdlDirectora.DataSource = Session["SesionDirectoraVentas"];
                        }
                        else
                            DdlDirectora.DataSource = Session["SesionDirectoraVentas"];

                        DdlDirectora.DataValueField = "chrCodigoDirectoraVentas";
                        DdlDirectora.DataTextField = "vchNombreCompleto";
                        DdlDirectora.DataBind();
                        DdlDirectora.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                    }
                    else
                    {
                        lblGerenteRegion.Visible = false;
                        ddlGerenteRegion.Visible = true;
                        DdlDirectora.Visible = false;

                        //Gerente Región
                        ddlGerenteRegion.Items.Clear();
                        if (Session["SesionGerenteRegion"] == null)
                        {
                            Session["SesionGerenteRegion"] = ListarGerenteRegion();
                            ddlGerenteRegion.DataSource = Session["SesionGerenteRegion"];
                        }
                        else
                            ddlGerenteRegion.DataSource = Session["SesionGerenteRegion"];

                        ddlGerenteRegion.DataValueField = "IdAndCodigoGerenteRegion"; //"intIDGerenteRegion";
                        ddlGerenteRegion.DataTextField = "vchNombrecompleto";
                        ddlGerenteRegion.DataBind();
                        ddlGerenteRegion.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                        ddlGerenteRegion.SelectedValue = lblIDGerenteRegion.Text.Trim() + "|" +
                                                         lblCodigoGerenteRegion.Text;
                    }
                    break;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = Convert.ToString(ddlZona.SelectedValue) == "0" ? txtColaborador.Text : ddlZona.SelectedItem.Text;
            Session["SesionGerenteRegion"] = null;
            Session["SesionDirectoraVentas"] = null;
            ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue), nombre, 2);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ddlPais.SelectedValue = "0";
            ddlRegion.Items.Clear();
            ddlRegion.Items.Insert(0, new ListItem("[Seleccione]", "0"));
            ddlZona.Items.Clear();
            ddlZona.Items.Insert(0, new ListItem("[Seleccione]", "0"));
            txtColaborador.Text = "";
            rblReasignar.SelectedValue = "1";
            gvGerenteZona.DataSource = null;
            gvGerenteZona.DataBind();
            Session["SesionGerenteRegion"] = null;
            Session["SesionDirectoraVentas"] = null;
            Session["SesionGerenteZona"] = null;
            divAsignacion.Visible = false;
            //divProceso.Visible = false;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (VerificaRegistroChecksZona())
            {
                if (CargarListaProcesos())
                {
                    if (rblReasignar.SelectedValue != "1")
                    {
                        divAsignacion.Visible = false;
                        divProceso.Visible = true;
                    }
                    else
                    {
                        divAsignacion.Visible = true;
                        divProceso.Visible = false;
                        GrabarGerenteZona();
                    }
                }
                else
                {
                    divAsignacion.Visible = true;
                    divProceso.Visible = false;
                    GrabarGerenteZona();
                }
            }
            else
            {
                AlertaMensaje("Debe seleccionar al menos un registro.");
            }
        }

        private List<BeGerenteRegion> ListarGerenteRegion()
        {
            List<BeGerenteRegion> oList = new List<BeGerenteRegion>();
            try
            {
                BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();
                oList =
                    new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListar(ddlPais.SelectedValue, "", "", ""));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar gerente Region.");
            }
            return oList;
        }

        private List<BeDirectoraVentas> ListarDirectoraVentas()
        {
            List<BeDirectoraVentas> oList = new List<BeDirectoraVentas>();
            try
            {
                BlDirectoraVentas oblDirectoraVentas = new BlDirectoraVentas();
                oList =
                    new List<BeDirectoraVentas>(oblDirectoraVentas.DirectoraVentasListar(0, ddlPais.SelectedValue, "", true));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Directora de Ventas.");
            }
            return oList;
        }

        private bool VerificaRegistroChecksZona()
        {
            CheckBox chkbox;
            bool Resultado = false;
            string codigoUsuario = "", codigoEvaluador = "";
            for (int i = 0; i < gvGerenteZona.Rows.Count; i++)
            {
                chkbox = (CheckBox)gvGerenteZona.Rows[i].FindControl("chkEliminar");
                if (chkbox.Checked == true)
                {
                    if (rblReasignar.SelectedValue == "1")
                    {
                        DropDownList DdlDirectora =
                            (DropDownList)gvGerenteZona.Rows[i].FindControl("ddlDirectora");
                        if (DdlDirectora.SelectedValue == "0")
                        {
                            int fila = i + 1;
                            AlertaMensaje("Debe seleccionar un Director de Ventas en la fila " + fila.ToString() + ".");
                            return false;
                        }
                        else
                        {
                            // Para Proceso
                            //codigoUsuario = codigoUsuario + "1075223929" + "|"; //solo pruebas
                            codigoUsuario = codigoUsuario + gvGerenteZona.DataKeys[i].Values[3].ToString() + "|";
                            codigoEvaluador = codigoEvaluador + DdlDirectora.SelectedValue + "|";
                            // Fin
                        }
                    }
                    else
                    {
                        DropDownList GerenteRegion =
                            (DropDownList)gvGerenteZona.Rows[i].Cells[2].FindControl("ddlGerenteRegion");
                        if (GerenteRegion.SelectedValue == "0")
                        {
                            int fila = i + 1;
                            AlertaMensaje("Debe seleccionar un Gerente Región en la fila " + fila.ToString() + ".");
                            return false;
                        }
                        else
                        {
                            // Para Proceso
                            //codigoUsuario = codigoUsuario + "1075223929" + "|"; //solo pruebas
                            codigoUsuario = codigoUsuario + gvGerenteZona.DataKeys[i].Values[3].ToString() + "|";
                            string[] wordsGerenteRegion = GerenteRegion.SelectedValue.Split('|');
                            //codigoEvaluador = codigoEvaluador + GerenteRegion.SelectedValue + "|";
                            codigoEvaluador = codigoEvaluador + wordsGerenteRegion[1] + "|";
                            // Fin
                        }
                    }
                    Resultado = true;
                }
            }
            hdenCodigoUsuario.Value = codigoUsuario;
            hdenCodigoEvaluador.Value = codigoEvaluador;
            return Resultado;
        }

        private void GrabarGerenteZona()
        {
            DropDownList DdlDirectora, ddlGerenteRegion;
            CheckBox chkbox;
            try
            {
                if (VerificaRegistroChecksZona())
                {
                    for (int i = 0; i < gvGerenteZona.Rows.Count; i++)
                    {
                        chkbox = (CheckBox)gvGerenteZona.Rows[i].FindControl("chkEliminar");
                        if (chkbox.Checked == true)
                        {
                            if (rblReasignar.SelectedValue == "1")
                            {
                                // Dar de baja a un GZ (Eliminar GZ)
                                obeGerenteZona.intIDGerenteZona =
                                    Convert.ToInt32(gvGerenteZona.DataKeys[i].Values[0].ToString());
                                obeGerenteZona.intIDGerenteRegion = 0;
                                obeGerenteZona.intUsuarioCrea = 1;
                                obeGerenteZona.chrIndicadorMigrado = "";
                                obeGerenteZona.chrCampaniaBaja = "";
                                oblGerenteZona.GerenteZonaActualizarEstado(obeGerenteZona);

                                // Dar de alta al GZ (Registrar un GR con todos los datos del GZ eliminado)
                                DdlDirectora = (DropDownList)gvGerenteZona.Rows[i].FindControl("ddlDirectora");

                                obeGerenteRegion.ChrCodigoGerenteRegion = gvGerenteZona.DataKeys[i].Values[3].ToString();
                                obeGerenteRegion.ChrPrefijoIsoPais = gvGerenteZona.DataKeys[i].Values[2].ToString();
                                obeGerenteRegion.VchNombrecompleto = gvGerenteZona.DataKeys[i].Values[4].ToString();
                                obeGerenteRegion.VchCorreoElectronico = gvGerenteZona.DataKeys[i].Values[5].ToString();
                                obeGerenteRegion.IntUsuarioCrea = 1; //CAMBIAR
                                obeGerenteRegion.ChrCodDirectorVenta = DdlDirectora.SelectedValue;
                                obeGerenteRegion.ChrCodigoDataMart = gvGerenteZona.DataKeys[i].Values[6].ToString();
                                obeGerenteRegion.chrCampaniaRegistro = gvGerenteZona.DataKeys[i].Values[7].ToString();
                                obeGerenteRegion.chrIndicadorMigrado = gvGerenteZona.DataKeys[i].Values[8].ToString();
                                oblGerenteRegion.GerenteRegionRegistrar(obeGerenteRegion);

                                string gerenteRegion = ddlRegion.SelectedValue;
                                string gerenteZona = ddlZona.SelectedValue;

                                ListarGerenteRegion(ddlPais.SelectedValue, "");
                                ddlRegion.SelectedValue = gerenteRegion;
                                ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue), "", 1);
                                //ddlZona.SelectedValue = gerenteZona;

                                string nombre = Convert.ToString(ddlZona.SelectedValue) == "0"
                                                    ? txtColaborador.Text
                                                    : ddlZona.SelectedItem.Text;
                                //ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue),nombre, 2);
                            }
                            else
                            {
                                ddlGerenteRegion = (DropDownList)gvGerenteZona.Rows[i].FindControl("ddlGerenteRegion");

                                //Actualiza solo el Id de Gerente region
                                obeGerenteZona.intIDGerenteZona =
                                    Convert.ToInt32(gvGerenteZona.DataKeys[i].Values[0].ToString());
                                string[] words = ddlGerenteRegion.SelectedValue.Split('|');
                                obeGerenteZona.intIDGerenteRegion = Convert.ToInt32(words[0].Trim());
                                obeGerenteZona.intUsuarioCrea = 1; // CAMBIAR
                                obeGerenteZona.chrCodigoDataMart = "";
                                oblGerenteZona.GerenteZonaActualizarGerenteRegion(obeGerenteZona);

                                string gerenteRegion = ddlRegion.SelectedValue;
                                string gerenteZona = ddlZona.SelectedValue;

                                ListarGerenteRegion(ddlPais.SelectedValue, "");
                                ddlRegion.SelectedValue = gerenteRegion;
                                ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue), "", 1);
                                //ddlZona.SelectedValue = gerenteZona;

                                string nombre = Convert.ToString(ddlZona.SelectedValue) == "0"
                                                    ? txtColaborador.Text
                                                    : ddlZona.SelectedItem.Text;
                                //ListarGerenteZona(ddlPais.SelectedValue, Convert.ToInt32(ddlRegion.SelectedValue),nombre, 2);
                            }
                        }
                    }
                    AlertaMensaje("Se procesó correctamente.");
                }
                else
                {
                    AlertaMensaje("Debe Seleccionar para grabar.");
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Sucedió un error en el proceso de asignación.");
            }
        }

        #endregion "Gerente Zona"

        #region "Proceso Dialogo"

        protected void gvProceso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    RadioButtonList rblProceso = (RadioButtonList)e.Row.FindControl("rblProceso");
                    break;
            }
        }

        protected void btnProcesoCancelar_Click(object sender, EventArgs e)
        {
            gvProceso.DataSource = null;
            gvProceso.DataBind();
            divProceso.Visible = false;
            divAsignacion.Visible = true;
        }

        protected void btnProcesoContinuar_Click(object sender, EventArgs e)
        {
            int grabarProcesoDialogo;

            grabarProcesoDialogo = GrabarProcesoDialogo();

            if (grabarProcesoDialogo == 0)
            {
                GrabarGerenteZona();
                divAsignacion.Visible = true;
                divProceso.Visible = false;
                //AlertaMensaje("Se procesó sólo Gerente Zona.");
            }
            else
            {
                if (grabarProcesoDialogo == 1)
                {
                    GrabarGerenteZona();
                    divAsignacion.Visible = true;
                    divProceso.Visible = false;
                    //AlertaMensaje("Se grabó en proceso de dialogo y se proceso Gerente Zona correctamente.");
                }
                else
                {
                    divAsignacion.Visible = true;
                    divProceso.Visible = false;
                    AlertaMensaje("No se puede continuar porque sucedio un error en proceso dialogo.");
                }
            }
        }

        private List<BeProceso> ListarProceso(string codigoUsuario, string codigoPais)
        {
            List<BeProceso> oList = new List<BeProceso>();
            try
            {
                oblProceso = new BlProceso();
                oList =
                    new List<BeProceso>(oblProceso.ProcesoListarCodigoUsuario(codigoUsuario, codigoPais));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar procesos.");
            }
            return oList;
        }

        private bool CargarListaProcesos()
        {
            bool Respuesta = false;
            string[] wordsUsuario = hdenCodigoUsuario.Value.Split('|');
            string[] wordsEvaluador = hdenCodigoEvaluador.Value.Split('|');
            List<BeProceso> oList = new List<BeProceso>();

            DataTable dtProceso = new DataTable();
            dtProceso.Columns.Add("IDProceso");
            dtProceso.Columns.Add("IDRol");
            dtProceso.Columns.Add("CodigoUsuario"); //Código de usuario (GZ)
            dtProceso.Columns.Add("Usuario"); // Nombre de usuario (GZ)
            dtProceso.Columns.Add("Periodo");
            dtProceso.Columns.Add("FechaLimiteProceso");
            dtProceso.Columns.Add("EstadoProceso");
            dtProceso.Columns.Add("IDRolEvaluador");
            dtProceso.Columns.Add("CodigoEvaluador"); //Código de evaluador (GR)
            dtProceso.Columns.Add("PrefijoIsoPais");
            dtProceso.Columns.Add("NuevasIngresadas");
            dtProceso.Columns.Add("RegionZona");
            //Session["SesionProceso"] = dtProceso;

            for (int i = 0; i < wordsUsuario.Length - 1; i++)
            {
                oblProceso = new BlProceso();
                oList =
                    new List<BeProceso>(oblProceso.ProcesoListarCodigoUsuario(wordsUsuario[i], ddlPais.SelectedValue));
                if (oList != null)
                {
                    FechaLimiteProceso = "";
                    if (oList.Count > 0)
                    {
                        IDProceso = oList[0].IdProceso.ToString();
                        IDRol = oList[0].IdRol.ToString();
                        CodigoUsuario = oList[0].CodigoUsuario;
                        Usuario = oList[0].obeGerenteZona.vchNombreCompleto;
                        Periodo = oList[0].Periodo;
                        FechaLimiteProceso = oList[0].datFechaLimiteProceso.ToString();
                        EstadoProceso = oList[0].chrEstadoProceso;
                        intIDRolEvaluador = oList[0].intIDRolEvaluador.ToString();
                        CodigoEvaluador = wordsEvaluador[0];
                        PrefijoIsoPais = oList[0].chrPrefijoIsoPais;
                        NuevasIngresadas = oList[0].nuevasIngresadas.ToString();
                        RegionZona = oList[0].chrRegionZona;

                        dtProceso.Rows.Add(IDProceso,
                                            IDRol,
                                           CodigoUsuario,
                                           Usuario,
                                           Periodo,
                                           FechaLimiteProceso,
                                           EstadoProceso,
                                           intIDRolEvaluador,
                                           CodigoEvaluador,
                                           PrefijoIsoPais,
                                           NuevasIngresadas,
                                           RegionZona);
                    }
                }
            }
            if (dtProceso.Rows.Count > 0)
            {
                gvProceso.DataSource = dtProceso;
                gvProceso.DataBind();
                Respuesta = true;
            }
            return Respuesta;
        }

        private int GrabarProcesoDialogo()
        {
            int Respuesta = 0;
            try
            {
                if (gvProceso.Rows.Count > 0)
                {
                    for (int i = 0; i < gvProceso.Rows.Count; i++)
                    {
                        RadioButtonList rblProceso = (RadioButtonList)gvProceso.Rows[i].FindControl("rblProceso");

                        obeProceso.IdProceso = Convert.ToInt32(gvProceso.DataKeys[i].Values[0]); //quitar
                        obeProceso.IdRol = Convert.ToInt32(gvProceso.DataKeys[i].Values[1]);
                        obeProceso.CodigoUsuario = gvProceso.DataKeys[i].Values[2].ToString();
                        obeProceso.Periodo = gvProceso.DataKeys[i].Values[3].ToString();
                        if (gvProceso.DataKeys[i].Values[4].ToString() != "")
                            obeProceso.datFechaLimiteProceso = Convert.ToDateTime(gvProceso.DataKeys[i].Values[4]);
                        else
                            obeProceso.datFechaLimiteProceso = DateTime.Now;
                        obeProceso.chrEstadoProceso = gvProceso.DataKeys[i].Values[5].ToString();
                        obeProceso.intIDRolEvaluador = Convert.ToInt32(gvProceso.DataKeys[i].Values[6]);
                        obeProceso.CodigoUsuarioEvaluador = gvProceso.DataKeys[i].Values[7].ToString();
                        obeProceso.chrPrefijoIsoPais = gvProceso.DataKeys[i].Values[8].ToString();
                        obeProceso.chrRegionZona = gvProceso.DataKeys[i].Values[10].ToString();
                        obeProceso.datFechaInicioProceso = DateTime.Now;
                        obeProceso.bitPlanMejora = false;
                        obeProceso.intUsuarioCrea = 1; //CAMBIAR

                        if (rblReasignar.SelectedValue == "1")
                        {
                            obeProceso.tipo = Convert.ToInt32(rblReasignar.SelectedValue); //=1
                            //Asciende a GR, el IdRol será 2 y el IdRolEvaluador ahora será 3
                            obeProceso.IdRol = 2; // antes 1
                            obeProceso.intIDRolEvaluador = 3; // antes 2
                            if (rblProceso.SelectedValue == "1")
                            {
                                //Mantine su proceso, pero ahora es GR y su jefe es un DV
                                oblProceso.ProcesoActualizar(obeProceso);
                            }
                            else
                            {
                                //Elimina su proceso actual y crea uno nuevo ahora como(GR) con su nuevo jefe(DV)
                                oblProceso.ProcesoActualizarEstado(obeProceso);
                                oblProceso.ProcesoRegistrar(obeProceso);
                            }
                        }
                        else
                        {
                            obeProceso.tipo = Convert.ToInt32(rblReasignar.SelectedValue); //=2
                            //obeProceso.IdRol = 1;
                            //obeProceso.intIDRolEvaluador = 2;
                            if (rblProceso.SelectedValue == "1")
                            {
                                //Se mantiene como GZ y solo cambia de GR
                                oblProceso.ProcesoActualizar(obeProceso);
                            }
                            else
                            {
                                //Elimina su proceso actual y crea un nuevo pero, se mantiene como GZ y solo cambia de GR
                                oblProceso.ProcesoActualizarEstado(obeProceso);
                                oblProceso.ProcesoRegistrar(obeProceso);
                            }
                        }
                        Respuesta = 1;
                    }
                }
            }
            catch (Exception)
            {
                Respuesta = 2;
            }
            return Respuesta;
        }


        private void SeleccionarPais()
        {

            switch (_objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    ddlPais.Enabled = true;
                    break;
                case Constantes.RolAdminPais:
                    ddlPais.Enabled = false;
                    break;
                case Constantes.RolAdminEvaluciona:
                    ddlPais.Enabled = false;
                    //ListarGerenteRegion(ddlPais.SelectedValue, "");

                    if (_objAdmin.CodigoPais == "CR")
                    {
                        ddlPais.Items.Clear();
                        ddlPais.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                        ddlPais.Items.Insert(1, new ListItem("COSTA RICA", "CR"));
                        ddlPais.Items.Insert(2, new ListItem("GUATEMALA", "GT"));
                        ddlPais.Items.Insert(3, new ListItem("PANAMA", "PA"));
                        ddlPais.Items.Insert(4, new ListItem("EL SALVADOR", "SV"));
                        ddlPais.Enabled = true;
                    }

                    if (_objAdmin.CodigoPais == "DO")
                    {
                        ddlPais.Items.Clear();
                        ddlPais.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                        ddlPais.Items.Insert(1, new ListItem("REP.DOMINICANA", "DO"));
                        ddlPais.Items.Insert(2, new ListItem("PUERTO RICO", "PR"));
                        ddlPais.Enabled = true;
                    }


                    if (_objAdmin.CodigoPais != "CR" && _objAdmin.CodigoPais != "DO")
                    {
                        ddlPais.Items.Clear();
                        ddlPais.Items.Insert(0, new ListItem("[Seleccione]", "0"));
                        ddlPais.Items.Insert(1, new ListItem(_objAdmin.NombrePais, _objAdmin.CodigoPais));
                        ddlPais.Enabled = true;
                    }

                    break;
            }
        }


        #endregion "Proceso Dialogo"
    }
}