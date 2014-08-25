
namespace Evoluciona.Dialogo.Web.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class RptParticipacionTiempoBuscar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CargarPeriodos();
        }

        private void CargarPeriodos()
        {
            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();

            DataSet dsPeriodos = daProceso.ObtenerPeriodosxAnio(cboAnio.SelectedValue);

            cboPeriodos.DataSource = dsPeriodos;
            cboPeriodos.DataTextField = "periodo";
            cboPeriodos.DataValueField = "periodo";
            cboPeriodos.DataBind();
        }

        protected void cboAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPeriodos();
        }

        protected void btnVerReporte_Click(object sender, EventArgs e)
        {
            try
            {
                string periodo = cboPeriodos.SelectedValue;
                BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
                BlReportes daReporte = new BlReportes();
                DataTable dtPorcentajes = daReporte.ObtenerReunionesCampania(periodo, objUsuario.codigoUsuario);

                if (dtPorcentajes.Rows.Count > 0)
                    ClientScript.RegisterStartupScript(GetType(), "abrirVentana", "abrirVentana();", true);
                else
                    ClientScript.RegisterStartupScript(GetType(), "abrirMensaje", "abrirMensaje();", true);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?mensaje=" + ex.Message, true);
            }
        }
    }
}