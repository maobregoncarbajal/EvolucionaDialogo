
namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class SustentoMatriz : Page
    {
        private string _datos;
        private int _codProcesado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MainView.ActiveViewIndex = 0;
                _datos = Request.QueryString["Datos"];
                this.lblIdTomaAccion.Text = _datos.Split('-')[0].ToString().Trim();
                this.lblPrefijoIsoPaisEvaluado.Text = _datos.Split('-')[1].ToString().Trim();
                this.lblPeriodo.Text = _datos.Split('-')[2].ToString().Trim();
                this.lblCodEvaluado.Text = _datos.Split('-')[3].ToString().Trim();
                this.lblTomaAccion.Text = _datos.Split('-')[4].ToString().Trim();
                this.lblIdRolEvaluado.Text = _datos.Split('-')[5].ToString().Trim();
                this.lblNom.Text = _datos.Split('-')[6].ToString().Trim();
                this.lblRol.Text = string.Format("({0})", _datos.Split('-')[7].ToString().Trim());
                this.lblCorreo.Text = _datos.Split('-')[8].ToString().Trim();
                this.lblCodEvaluador.Text = _datos.Split('-')[9].ToString().Trim();

                BlUsuario obBLUsuario = new BlUsuario();
                BeUsuario objUsuario = obBLUsuario.ObtenerDatosUsuario(this.lblPrefijoIsoPaisEvaluado.Text, Convert.ToInt32(this.lblIdRolEvaluado.Text) + 3, this.lblCodEvaluado.Text, Constantes.EstadoActivo);

                BlConfiguracion objConfig = new BlConfiguracion();
                _codProcesado = objConfig.ValidarInicioProceso(this.lblPrefijoIsoPaisEvaluado.Text, this.lblPeriodo.Text, Constantes.IndicadorEvaluadoDvGr);

                if (objUsuario != null)
                {
                    this.lblDatosFichaPersonal.Text = objUsuario.nombreUsuario + "|" + objUsuario.codigoUsuario + "|" + _codProcesado + "|" + objUsuario.codigoRol + "|" +
                                                      objUsuario.prefijoIsoPais + "|" + this.lblPeriodo.Text + "|" + objUsuario.codigoUsuario;
                }
            }
        }

        protected void btnVerResultados_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 0;
            ActualizarCurrentTab(0);
            Page.ClientScript.RegisterStartupScript(GetType(), "inicializarView", "inicializarView(0);", true);
        }

        protected void btnDialogoDesempeno_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 1;
            ActualizarCurrentTab(1);

            Page.ClientScript.RegisterStartupScript(GetType(), "inicializarView", "inicializarView(1);", true);
        }

        protected void btnVisitas_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 2;
            ActualizarCurrentTab(2);
            Page.ClientScript.RegisterStartupScript(GetType(), "inicializarView", "inicializarView(2);", true);
        }

        protected void btnCondiciones_Click(object sender, EventArgs e)
        {
            MainView.ActiveViewIndex = 3;
            ActualizarCurrentTab(3);
            Page.ClientScript.RegisterStartupScript(GetType(), "inicializarView", "inicializarView(3);", true);
        }

        private void ActualizarCurrentTab(int activeIndex)
        {
            btnVerResultados.CssClass = "";
            btnDialogoDesempeno.CssClass = "";
            btnVisitas.CssClass = "";
            btnCondiciones.CssClass = "";

            switch (activeIndex)
            {
                case 0:
                    btnVerResultados.CssClass = "current";
                    break;
                case 1:
                    btnDialogoDesempeno.CssClass = "current";
                    break;
                case 2:
                    btnVisitas.CssClass = "current";
                    break;
                case 3:
                    btnCondiciones.CssClass = "current";
                    break;
            }
        }
    }
}