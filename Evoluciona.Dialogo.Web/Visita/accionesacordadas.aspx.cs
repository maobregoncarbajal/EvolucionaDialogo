
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class accionesacordadas : Page
    {
        private BeResumenVisita objVisita = new BeResumenVisita();
        private readonly BlAccionesAcordadas blAccionAcordada = new BlAccionesAcordadas();
        public int indexMenuServer;
        public int indexSubMenu;

        protected void Page_Load(object sender, EventArgs e)
        {
            objVisita = (BeResumenVisita) Session[Constantes.ObjUsuarioVisitado];
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);

            if (Page.IsPostBack) return;

            if (Session[Constantes.VisitaModoLectura] != null)
            {
                btngrabar.Text = "CONTINUAR";
            }

            repAcciones.DataSource = blAccionAcordada.ObtenerAccionesAcordadas(objVisita.idProceso);
            repAcciones.DataBind();
        }

        protected void btngrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                foreach (RepeaterItem item in repAcciones.Items)
                {
                    BeAccionesAcordadas accionAcordada = new BeAccionesAcordadas();
                    accionAcordada.IdVisita = objVisita.idVisita;

                    HiddenField hidIdAccion = (HiddenField)item.FindControl("hidIdAccion");
                    if (hidIdAccion != null)
                        accionAcordada.IdAcciones = Convert.ToInt32(hidIdAccion.Value);

                    HiddenField hidIdIndicador = (HiddenField)item.FindControl("hidIdIndicador");
                    if (hidIdIndicador != null)
                        accionAcordada.IDIndicador1 = Convert.ToInt32(hidIdIndicador.Value);

                    TextBox txtaccionesAcordadas = (TextBox) item.FindControl("txtaccionesAcordadas");
                    if (txtaccionesAcordadas != null)
                        accionAcordada.AccionesAcordadas1 = txtaccionesAcordadas.Text;

                    TextBox txtcampanias = (TextBox)item.FindControl("txtcampanias");
                    if (txtcampanias != null)
                        accionAcordada.Campanias1 = txtcampanias.Text;

                    CheckBox chbxPostVenta = (CheckBox) item.FindControl("chbxPostVenta");
                    if (chbxPostVenta != null)
                        accionAcordada.PostVisita1 = chbxPostVenta.Checked;

                    HiddenField hidTipoAccion = (HiddenField) item.FindControl("hidTipoAccion");
                    if (hidTipoAccion != null)
                        accionAcordada.TipoAccion = hidTipoAccion.Value;

                    if(accionAcordada.IdAcciones == 0)
                    {
                        blAccionAcordada.InsertarAccionAcordada(accionAcordada);
                    }
                    else
                    {
                        blAccionAcordada.ActualizarAccionAcordada(accionAcordada);
                    }
                }

                if (objVisita.estadoVisita == Constantes.EstadoVisitaActivo && objVisita.porcentajeAvanceDurante == 60)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objVisita.idVisita, 80, 2);
                    objVisita.porcentajeAvanceDurante = 80;
                    Session[Constantes.ObjUsuarioVisitado] = objVisita;
                }
                else if (objVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo && objVisita.porcentajeAvanceDespues == 30)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objVisita.idVisita, 60, 3);
                    objVisita.porcentajeAvanceDespues = 60;
                    Session[Constantes.ObjUsuarioVisitado] = objVisita;
                }

                if (objVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_AccAcordadasD", "<script language='javascript'> javascript:AbrirMensajeDespues(); </script>");
                }
                else if (objVisita.estadoVisita == Constantes.EstadoVisitaActivo)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_AccAcordadas", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                }
            }
            else
            {
                if (indexMenuServer == 3)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_AccAcordadasD2", "<script language='javascript'> javascript:AbrirMensajeDespues(); </script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_IAccAcordadas2", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                }
            }
        }

        protected void repAcciones_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            BeAccionesAcordadas accionesAcordada = (BeAccionesAcordadas) e.Item.DataItem;
            if (accionesAcordada == null) return;
            List<BeVariableCausa> variables = blAccionAcordada.ObtenerVariablesCausales(objVisita.idProceso,
                                                                                 accionesAcordada.CodVariablePadre1,
                                                                                 accionesAcordada.TipoAccion);

            Label lblVariableCausa1 = (Label) e.Item.FindControl("lblVariableCausa1");
            if (lblVariableCausa1 != null && variables.Count > 0)
            {
                lblVariableCausa1.Text = variables[0].DescripcionVariable;
            }

            Label lblVariableCausa2 = (Label) e.Item.FindControl("lblVariableCausa2");
            if (lblVariableCausa2 != null && variables.Count > 1)
            {
                lblVariableCausa2.Text = variables[1].DescripcionVariable;
            }

            TextBox txtaccionesAcordadas = (TextBox)e.Item.FindControl("txtaccionesAcordadas");
            TextBox txtcampanias = (TextBox)e.Item.FindControl("txtcampanias");

            CheckBox chbxpostVenta = (CheckBox)e.Item.FindControl("chbxPostVenta");
            if (chbxpostVenta == null) return;
            chbxpostVenta.CssClass = "checkDesabilitado";

            if (objVisita.estadoVisita == Constantes.EstadoVisitaActivo)
            {
                chbxpostVenta.Visible = false;
            }
            else if (objVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo)
            {
                txtaccionesAcordadas.CssClass = "checkDesabilitado";
                txtcampanias.CssClass = "checkDesabilitado";
                chbxpostVenta.Visible = true;
                chbxpostVenta.CssClass = "";

                if (indexMenuServer == 2)
                {
                    chbxpostVenta.Visible = false;
                }
            }

            if (Session[Constantes.VisitaModoLectura] != null)
            {
                txtaccionesAcordadas.CssClass = "checkDesabilitado";
                txtcampanias.CssClass = "checkDesabilitado";
            }
        }
    }
}