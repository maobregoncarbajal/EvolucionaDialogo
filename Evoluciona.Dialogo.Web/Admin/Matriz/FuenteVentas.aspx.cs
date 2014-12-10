
namespace Evoluciona.Dialogo.Web.Admin.Matriz
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

    public partial class FuenteVentas : Page
    {
        #region Variables

        private BeAdmin objAdmin;

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    menuReporte.FuenteVentas = "ui-state-active";
                    CargarVariables();
                    ListarFuenteVentas();
                    ListarPais();
                    ListarDatoFuente();
                    ListarFuenteVentas();
                    hfModo.Value = Constantes.AccionUnoFuenteVentas; //1=Graba, 2=Modifica, 3=Elimina
                    EstadoControles(hfModo.Value);
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationManager.AppSettings["MensajeAlertaPagina"].ToString());
            }

        }

        #region Metodos

        private void CargarVariables()
        {
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
            if (objAdmin != null)
            {
                string tipo = objAdmin.TipoAdmin;
                lblTipo.Text = tipo;
                string pais = objAdmin.CodigoPais;
                lblPais.Text = pais;
                string usuario = objAdmin.IDAdmin.ToString();
                lblUsuario.Text = usuario;
            }
            //Page.ClientScript.RegisterStartupScript(GetType(), "crearCombos", "crearCombos();", true);
        }

        #endregion Metodos

        private void ListarFuenteVentas()
        {
            try
            {
                BeFuenteVentas obeFuenteVentas = new BeFuenteVentas();
                BlFuenteVentas oblFuenteVentas = new BlFuenteVentas();
                DataTable dt = new DataTable();
                obeFuenteVentas.CodigoPais = "";
                obeFuenteVentas.CodigoFuenteVenta = "";
                dt = oblFuenteVentas.ListarPaisFuenteVenta(obeFuenteVentas);
                gvFuenteVentas.DataSource = dt;
                gvFuenteVentas.DataBind();
                Session["SesionFuenteVentas"] = dt;
            }
            catch (Exception)
            {
                AlertaMensaje("Error al listar Fuente de Ventas.");
            }
        }

        private void ListarPais()
        {
            try
            {
                BlPais oblPais = new BlPais();
                List<BePais> oList = new List<BePais>();
                oList = oblPais.ObtenerPaises();
                ddlPais.DataSource = oList;
                ddlPais.DataValueField = "prefijoIsoPais";
                ddlPais.DataTextField = "NombrePais";
                ddlPais.DataBind();
                ddlPais.Items.Insert(0, new ListItem("[Seleccione]", "0"));
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar los paices.");
            }
        }

        private void ListarDatoFuente()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo");
                dt.Columns.Add("Nombre");
                dt.Rows.Add("01", "Fuente Ventas");
                dt.Rows.Add("02", "Nueva Fuente Ventas");
                ddlFuenteVenta.DataSource = dt;
                ddlFuenteVenta.DataValueField = "Codigo";
                ddlFuenteVenta.DataTextField = "Nombre";
                ddlFuenteVenta.DataBind();
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar Fuente de Ventas.");
            }
        }

        protected void gvFuenteVentas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Editar":
                    ddlPais.SelectedValue = gvFuenteVentas.DataKeys[index].Values[0].ToString();
                    ddlFuenteVenta.SelectedValue = gvFuenteVentas.DataKeys[index].Values[1].ToString();
                    hfModo.Value = Constantes.AccionDosFuenteVentas; // "2";
                    btnGrabar.Text = "Modificar";
                    EstadoControles(hfModo.Value);
                    btnNuevo.Visible = false;
                    break;
                case "Eliminar":
                    bool insert = false;
                    BeFuenteVentas obeFuenteVentas = new BeFuenteVentas();
                    BlFuenteVentas oblFuenteVentas = new BlFuenteVentas();
                    obeFuenteVentas.CodigoPais = gvFuenteVentas.DataKeys[index].Values[0].ToString();
                    obeFuenteVentas.CodigoUsuarioCreacion = gvFuenteVentas.DataKeys[index].Values[3].ToString();
                    insert = oblFuenteVentas.ActualizarEstadoPaisFuenteVenta(obeFuenteVentas);
                    hfModo.Value = Constantes.AccionUnoFuenteVentas; // "1";
                    EstadoControles(hfModo.Value);

                    if (insert)
                    {
                        AlertaMensaje("Registro eliminado.");
                    }
                    break;
            }
        }

        protected void gvFuenteVentas_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            BeFuenteVentas obeFuenteVentas = new BeFuenteVentas();
            BlFuenteVentas oblFuenteVentas = new BlFuenteVentas();
            try
            {
                obeFuenteVentas.CodigoPais = ddlPais.SelectedValue;
                obeFuenteVentas.CodigoFuenteVenta = "";
                obeFuenteVentas.FuenteVentas = ddlFuenteVenta.SelectedItem.Text;
                obeFuenteVentas.CodigoUsuarioCreacion = ddlPais.SelectedValue;

                if (hfModo.Value == Constantes.AccionUnoFuenteVentas)
                {
                    if (ddlPais.SelectedIndex > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = oblFuenteVentas.ListarPaisFuenteVenta(obeFuenteVentas);
                        if (dt.Rows.Count == 0)
                        {
                            obeFuenteVentas.CodigoFuenteVenta = ddlFuenteVenta.SelectedValue;
                            oblFuenteVentas.ActualizarEstadoPaisFuenteVenta(obeFuenteVentas);
                            oblFuenteVentas.RegistrarPaisFuenteVenta(obeFuenteVentas);
                            AlertaMensaje("El país se registró correctamente.");
                            EstadoControles(hfModo.Value);
                        }
                        else
                            AlertaMensaje("El país ya se registró.");
                        dt.Dispose();
                    }
                    else
                        AlertaMensaje("Debe seleccionar un país.");
                }
                else
                {
                    obeFuenteVentas.CodigoFuenteVenta = ddlFuenteVenta.SelectedValue;
                    oblFuenteVentas.ActualizarPaisFuenteVenta(obeFuenteVentas);
                    hfModo.Value = Constantes.AccionUnoFuenteVentas;
                    AlertaMensaje("El país se actualizó correctamente.");
                    EstadoControles(hfModo.Value);
                }
                btnNuevo.Visible = true;
            }
            catch (Exception)
            {
                AlertaMensaje("El país ya se registró.");
            }
            finally
            {
                obeFuenteVentas = null;
                oblFuenteVentas = null;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                EstadoControles(Constantes.AccionUnoFuenteVentas);
                btnNuevo.Visible = true;
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cancelar.");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                EstadoControles(Constantes.AccionDosFuenteVentas);
                ddlPais.Enabled = true;
                ddlPais.SelectedIndex = 0;
                ddlFuenteVenta.SelectedIndex = 0;
                btnGrabar.Text = "Grabar";
                btnNuevo.Visible = false;
                hfModo.Value = Constantes.AccionUnoFuenteVentas;
            }
            catch (Exception)
            {
                AlertaMensaje("Error al cargar ventana de registro.");
            }
        }

        private void EstadoControles(string Accion)
        {
            if (Accion == Constantes.AccionUnoFuenteVentas)
            {
                pnlDatosFuenteVentas.Visible = false;
                pnlListaFuenteVenta.Visible = true;
                ListarFuenteVentas();
            }
            else
            {
                pnlDatosFuenteVentas.Visible = true;
                pnlListaFuenteVenta.Visible = false;
                ddlPais.Enabled = false;
                ListarFuenteVentas();
            }
        }

        private void AlertaMensaje(string strMensaje)
        {
            string ClienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", ClienteScript, false);
        }

        protected void gvFuenteVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFuenteVentas.PageIndex = e.NewPageIndex;
            gvFuenteVentas.DataSource = Session["SesionFuenteVentas"];
            gvFuenteVentas.DataBind();
        }
    }
}
