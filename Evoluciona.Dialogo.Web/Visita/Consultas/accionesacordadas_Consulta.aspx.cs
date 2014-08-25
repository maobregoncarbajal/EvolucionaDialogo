
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class accionesacordadas_Consulta : Page
    {
        private BeResumenVisita objVisita = new BeResumenVisita();
        private readonly BlAccionesAcordadas blAccionAcordada = new BlAccionesAcordadas();

        protected void Page_Load(object sender, EventArgs e)
        {
            objVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            string periodo = Request["periodo"];

            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita=blVisita.ObtenerCodigoVisita(objVisita.codigoUsuario, objVisita.codigoUsuarioEvaluador, objVisita.idRolUsuario, periodo);
            if (dtVisita.Rows.Count > 0)
            {
                objVisita.idVisita = Convert.ToInt32(dtVisita.Rows[0]["codigoVisita"]);
                objVisita.estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                objVisita.idProceso = Convert.ToInt32(dtVisita.Rows[0]["intIDProceso"]);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_accionesConsulta", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }

            if (Page.IsPostBack) return;

            repAcciones.DataSource = blAccionAcordada.ObtenerAccionesAcordadas(objVisita.idProceso);
            repAcciones.DataBind();
        }

        protected void repAcciones_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            BeAccionesAcordadas accionesAcordada = (BeAccionesAcordadas)e.Item.DataItem;
            if (accionesAcordada == null) return;
            List<BeVariableCausa> variables = blAccionAcordada.ObtenerVariablesCausales(objVisita.idProceso,
                                                                                 accionesAcordada.CodVariablePadre1,
                                                                                 accionesAcordada.TipoAccion);

            Label lblVariableCausa1 = (Label)e.Item.FindControl("lblVariableCausa1");
            if (lblVariableCausa1 != null && variables.Count > 0)
            {
                lblVariableCausa1.Text = variables[0].DescripcionVariable;
            }

            Label lblVariableCausa2 = (Label)e.Item.FindControl("lblVariableCausa2");
            if (lblVariableCausa2 != null && variables.Count > 1)
            {
                lblVariableCausa2.Text = variables[1].DescripcionVariable;
            }

            TextBox txtaccionesAcordadas = (TextBox)e.Item.FindControl("txtaccionesAcordadas");
            txtaccionesAcordadas.CssClass = "checkDesabilitado";

            TextBox txtcampanias = (TextBox)e.Item.FindControl("txtcampanias");
            txtcampanias.CssClass = "checkDesabilitado";

            CheckBox chbxpostVenta = (CheckBox)e.Item.FindControl("chbxPostVenta");
            if (chbxpostVenta == null) return;
            chbxpostVenta.CssClass = "checkDesabilitado";
        }
    }
}