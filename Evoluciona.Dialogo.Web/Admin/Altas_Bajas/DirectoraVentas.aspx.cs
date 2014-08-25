
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class DirectoraVentas : Page
    {
        #region "Variables Globales"

        private BlDirectoraVentas oblDirectoraVentas = new BlDirectoraVentas();
        private BeDirectoraVentas obeDirectoraVentas = new BeDirectoraVentas();
        private BeGerenteRegion obeGerenteRegion = new BeGerenteRegion();
        private BlGerenteRegion oblGerenteRegion = new BlGerenteRegion();
        private BeGerenteZona obeGerenteZona = new BeGerenteZona();
        private BlGerenteZona oblGerenteZona = new BlGerenteZona();

        #endregion "Variables Globales"

        #region "Evento de Página"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hdenCodigoPais.Value = "";
                    hfTipoMante.Value = "";
                    if (Request["ID"] != null)
                    {
                        string[] words = Request["ID"].Split('|');
                        hdenCodigoPais.Value = words[0];
                        hfTipoMante.Value = words[1];

                        ListarDirectoraVentas();
                        hfModo.Value = Constantes.AccionUnoFuenteVentas;
                        EstadoControles(hfModo.Value);
                        btnGrabar.Visible = false;
                        EstadoControlesTipoMante(hfTipoMante.Value);
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationSettings.AppSettings["MensajeAlertaPagina"].ToString());
            }
        }

        #endregion "Evento de Página"

        #region "Evento del Formulario"

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnNuevo.Visible = false;
            btnGrabar.Visible = true;
            btnCancelar.Visible = true;
            pnlDetalle.Enabled = false;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDirectoraVentas();
                ListarDirectoraVentas();
            }
            catch (Exception)
            {
                AlertaMensaje("Sucedió un error al grabar.");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            hfModo.Value = Constantes.AccionUnoFuenteVentas;
            EstadoControles(hfModo.Value);
            btnGrabar.Visible = false;
            pnlDetalle.Enabled = true;
        }

        protected void gvDirectoraVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDirectoraVentas.PageIndex = e.NewPageIndex;
            gvDirectoraVentas.DataSource = Session["SesionDirectoraVentas"];
            gvDirectoraVentas.DataBind();
        }

        protected void gvDirectoraVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    hfCodigoDirectora.Value = gvDirectoraVentas.DataKeys[index].Values[0].ToString();
                    txtDocumento.Text = gvDirectoraVentas.DataKeys[index].Values[4].ToString().Trim();
                    txtNombre.Text = gvDirectoraVentas.DataKeys[index].Values[2].ToString();
                    txtCorreo.Text = gvDirectoraVentas.DataKeys[index].Values[3].ToString();

                    hfModo.Value = Constantes.AccionDosFuenteVentas;
                    EstadoControles(hfModo.Value);
                    break;
                case "Eliminar":
                    obeDirectoraVentas.intIDDirectoraVenta = Convert.ToInt32(gvDirectoraVentas.DataKeys[index].Values[0]);
                    obeDirectoraVentas.intUsuarioCrea = 1; // xxxx

                    //Primero eliminamos a todos los GR de un DV
                    EliminarGerenteRegion(hdenCodigoPais.Value, gvDirectoraVentas.DataKeys[index].Values[4].ToString());

                    //Luego de eliminamos a los GR, eliminamos al DV
                    oblDirectoraVentas.DirectoraVentasActualizarEstado(obeDirectoraVentas);

                    hfModo.Value = Constantes.AccionUnoFuenteVentas;
                    EstadoControles(hfModo.Value);
                    ListarDirectoraVentas();
                    btnGrabar.Visible = false;
                    EstadoControlesTipoMante(hfTipoMante.Value);
                    break;
            }
        }

        protected void gvDirectoraVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CheckBox chkbox;
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    //Solo para proceso masivo - 06/12/2012
                    {
                        chkbox = ((CheckBox)e.Row.FindControl("chkEliminar"));
                        chkbox.Checked = true;
                        chkbox.Enabled = false;
                    }
                    //Fin Proceso Masivo

                    ImageButton ImgBtnEditar = (ImageButton)e.Row.FindControl("imgbtnEditar");
                    ImgBtnEditar.CommandArgument = e.Row.RowIndex.ToString();

                    ImageButton ImgBtnEliminar = (ImageButton)e.Row.FindControl("imgbtnEliminar");
                    ImgBtnEliminar.CommandArgument = e.Row.RowIndex.ToString();
                    break;
            }
        }

        protected void btnRetornar_Click(object sender, EventArgs e)
        {
            switch (hfTipoMante.Value)
            {
                case "1":
                    Response.Redirect("AltasBajas.aspx");
                    break;
                case "2":
                    Response.Redirect("Bajas.aspx");
                    break;
                case "3":
                    Response.Redirect("ProcesoMasivoAltas.aspx");
                    break;
                case "4":
                    Response.Redirect("ProcesoMasivoBajas.aspx");
                    break;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarDirectoraVentas();
        }

        #endregion "Evento del Formulario"

        #region "Evento del Desarrollador"

        private void ListarDirectoraVentas()
        {
            try
            {
                oblDirectoraVentas = new BlDirectoraVentas();
                List<BeDirectoraVentas> oList =
                    new List<BeDirectoraVentas>(oblDirectoraVentas.DirectoraVentasListar(0, hdenCodigoPais.Value, "", true));
                if (oList != null)
                {
                    gvDirectoraVentas.DataSource = oList;
                    gvDirectoraVentas.DataBind();
                    Session["SesionDirectoraVentas"] = oList;
                    if (hfTipoMante.Value == "4")
                    {
                        if (gvDirectoraVentas.Rows.Count > 0)
                            btnEliminar.Visible = true;
                        else
                            btnEliminar.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Directora de Ventas.");
            }
        }

        private void GrabarDirectoraVentas()
        {
            try
            {
                obeDirectoraVentas.chrPrefijoIsoPais = hdenCodigoPais.Value;
                obeDirectoraVentas.chrCodigoDirectoraVentas = txtDocumento.Text;// 10/01/2013
                obeDirectoraVentas.vchNombreCompleto = txtNombre.Text;
                obeDirectoraVentas.vchCorreoElectronico = txtCorreo.Text;
                obeDirectoraVentas.intUsuarioCrea = 1; // BUSCAR CODIGO DE USUARIO
                obeDirectoraVentas.vchDocumentoIndentidad = txtDocumento.Text;

                if (hfModo.Value == Constantes.AccionUnoFuenteVentas)
                {
                    oblDirectoraVentas.DirectoraVentasRegistrar(obeDirectoraVentas);
                    AlertaMensaje("Se grabó correctamente.");
                    EstadoControles(hfModo.Value);
                    Limpiar();
                }
                else
                {
                    obeDirectoraVentas.intIDDirectoraVenta = Convert.ToInt32(hfCodigoDirectora.Value);
                    oblDirectoraVentas.DirectoraVentasActualizar(obeDirectoraVentas);
                    hfModo.Value = Constantes.AccionUnoFuenteVentas;
                    EstadoControles(hfModo.Value);
                    Limpiar();
                    AlertaMensaje("Se actualizaron los datos correctamente.");
                }
                //pnlDatos.Enabled = false;
                btnGrabar.Visible = false;
            }
            catch (Exception)
            {
                AlertaMensaje("Error al grabar Directora de Ventas.");
            }
            finally
            {
                obeDirectoraVentas = null;
                oblDirectoraVentas = null;
            }
        }

        private void EstadoControles(string Accion)
        {
            if (Accion == Constantes.AccionUnoFuenteVentas)
            {
                btnNuevo.Visible = true;
                btnGrabar.Visible = true;
                btnGrabar.Text = "Grabar";
                btnCancelar.Visible = false;
                pnlDetalle.Enabled = true;
            }
            else
            {
                btnNuevo.Visible = false;
                btnGrabar.Visible = true;
                btnGrabar.Text = "Modificar";
                btnCancelar.Visible = true;
                pnlDetalle.Enabled = false;
            }
        }

        private void Limpiar()
        {
            txtDocumento.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            ListarDirectoraVentas();
            txtDocumento.Focus();
        }

        private void AlertaMensaje(string strMensaje)
        {
            string ClienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", ClienteScript, false);
        }

        private void EstadoControlesTipoMante(string tipoMante)
        {
            switch (tipoMante)
            {
                case "1":
                    lblSubTitulo.Text = "DIRECTORA DE VENTAS - ALTAS";
                    gvDirectoraVentas.Columns[0].Visible = false;
                    btnEliminar.Visible = false;
                    break;

                case "2":
                    lblSubTitulo.Text = "DIRECTORA DE VENTAS - BAJAS";
                    pnlDatos.Visible = false;
                    btnNuevo.Visible = false;
                    gvDirectoraVentas.Columns[0].Visible = false;
                    gvDirectoraVentas.Columns[6].Visible = false;
                    btnEliminar.Visible = false;
                    break;

                case "3":
                    lblSubTitulo.Text = "DIRECTORA DE VENTAS - ALTAS MASIVAS";
                    gvDirectoraVentas.Columns[0].Visible = false;
                    gvDirectoraVentas.Columns[7].Visible = false;
                    btnEliminar.Visible = false;
                    break;

                case "4":
                    lblSubTitulo.Text = "DIRECTORA DE VENTAS - BAJAS MASIVAS";
                    pnlDatos.Visible = false;
                    btnNuevo.Visible = false;
                    gvDirectoraVentas.Columns[6].Visible = false;
                    gvDirectoraVentas.Columns[7].Visible = false;
                    break;
            }
        }

        private void EliminarDirectoraVentas()
        {
            CheckBox chkbox;
            try
            {
                string codigoDirectora;
                for (int I = 0; I < gvDirectoraVentas.Rows.Count; I++)
                {
                    chkbox = (CheckBox)gvDirectoraVentas.Rows[I].FindControl("chkEliminar");
                    if (chkbox.Checked)
                    {
                        obeDirectoraVentas.intIDDirectoraVenta = Convert.ToInt32(gvDirectoraVentas.DataKeys[I].Values[0]);
                        obeDirectoraVentas.intUsuarioCrea = 1; // xxxx

                        //Primero eliminamos a todos los GR de un DV
                        codigoDirectora = gvDirectoraVentas.DataKeys[I].Values[4].ToString();
                        EliminarGerenteRegion(hdenCodigoPais.Value, codigoDirectora);

                        //Luego de eliminamos a los GR, eliminamos al DV
                        oblDirectoraVentas.DirectoraVentasActualizarEstado(obeDirectoraVentas);
                        //ListarDirectoraVentas();//07/12/2012
                    }
                }
                AlertaMensaje("Se eliminaron registros correctamente.");
                ListarDirectoraVentas();
            }
            catch (Exception)
            {
                AlertaMensaje("Sucedió un error al eliminar.");
            }
        }

        private void EliminarGerenteRegion(string codigoPais, string codigoDirector)
        {
            try
            {
                //Antes eliminamos a todos los GZ de un GR
                ListarGerenteRegion(codigoPais, codigoDirector);

                //Despues eliminamos al GR
                obeGerenteRegion.IntIDGerenteRegion = 0;
                obeGerenteRegion.ChrCodDirectorVenta = codigoDirector;
                obeGerenteRegion.IntUsuarioCrea = 1; //CAMBIAR
                obeGerenteRegion.chrIndicadorMigrado = "";
                obeGerenteRegion.chrCampaniaBaja = "";
                oblGerenteRegion.GerenteRegionActualizarEstado(obeGerenteRegion);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al eliminar Gerente Región.");
            }
        }

        private void ListarGerenteRegion(string codigoPais, string codigoDirector)
        {
            try
            {
                oblGerenteRegion = new BlGerenteRegion();
                List<BeGerenteRegion> oList =
                    new List<BeGerenteRegion>(oblGerenteRegion.GerenteRegionListar(codigoPais, "", "", codigoDirector));
                if (oList != null)
                {
                    for (int i = 0; i < oList.Count; i++)
                    {
                        EliminarGerenteZona(oList[i].IntIDGerenteRegion);
                    }
                }
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Gerente Región.");
            }
        }

        private void EliminarGerenteZona(int codigoIdGerenteRegion)
        {
            try
            {
                obeGerenteZona.intIDGerenteZona = 0;
                obeGerenteZona.intIDGerenteRegion = codigoIdGerenteRegion;
                obeGerenteZona.intUsuarioCrea = 1;
                obeGerenteZona.chrIndicadorMigrado = "";
                obeGerenteZona.chrCampaniaBaja = "";
                oblGerenteZona.GerenteZonaActualizarEstado(obeGerenteZona);
            }
            catch (Exception)
            {
                AlertaMensaje("Error al eliminar Gerente Zona.");
            }
        }

        #endregion "Evento del Desarrollador"
    }
}