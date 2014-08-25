
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessLogic;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class ReporteVisitasCampañaBuscar : Page
    {
        public int tabIndexActual = 0;

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
    }
}
