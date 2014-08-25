
namespace Evoluciona.Dialogo.Web.PantallasModales
{
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;

    public partial class HistorialPeriodoCriticidadEval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            BlCritica criticaBL = new BlCritica();

            string nombreEvaluada = Utils.QueryString("nombre");
            string codigoUsuarioEvaluador = Utils.QueryString("codigoEvaluador");
            string codigoEvaluado = Utils.QueryString("codigoEvaluado");
            string tipoHistorial = Utils.QueryString("tipo");

            TipoHistorial tipo = (TipoHistorial)Enum.Parse(typeof(TipoHistorial), tipoHistorial);

            lblNombreEvaluadora.Text = nombreEvaluada;

            dlHistorico.DataSource = criticaBL.ObtenerHistoricoPeriodosCriticidadEval(codigoUsuarioEvaluador, codigoEvaluado, tipo);
            dlHistorico.DataBind();
        }
    }
}