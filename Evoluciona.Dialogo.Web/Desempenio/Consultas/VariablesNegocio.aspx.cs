
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class VariablesNegocio : Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
        }

        #endregion Eventos

        #region Metodos

        private void CargarVariables()
        {
            BeResumenProceso objResumenProceso = new BeResumenProceso();
            BlIndicadores IndicadorBl = new BlIndicadores();
            objResumenProceso = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            string periodo = Utils.QueryString("periodo");
            string codigo = Utils.QueryString("codigo");
            string pais = objResumenProceso.prefijoIsoPais;
            int rol = objResumenProceso.codigoRolUsuario;
            string rolDescripcion = "GERENTE DE " + Utils.QueryString("rol");
            string nombre = Utils.QueryString("nombre");


            lblCabecera.Text = string.Format("Rol: {0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Codigo: {1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Utils.QueryString("rol") + ": {2}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Periodo: {3}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pais : {4}", rolDescripcion, codigo, nombre, periodo, pais);



            DataSet dsVESPP = IndicadorBl.ObtenerVariablesEnfoqueSeleccionadasPorPeriodo(periodo, codigo, pais, rol, Constantes.EstadoInactivo);
            gvVariables.DataSource = dsVESPP;
            gvVariables.DataBind();


            DataSet dsVEASPP = IndicadorBl.ObtenerVariablesEnfoqueSeleccionadasPorPeriodo(periodo, codigo, pais, rol, Constantes.EstadoActivo);
            gvVariablesAdicionales.DataSource = dsVEASPP;
            gvVariablesAdicionales.DataBind();

            OcultarCampanha(false);

        }
        
        private void OcultarCampanha(bool valor)
        {
            gvVariables.Columns[6].Visible = valor;
            gvVariables.DataBind();

            gvVariablesAdicionales.Columns[6].Visible = valor;
            gvVariablesAdicionales.DataBind();
        }

        #endregion Metodos
    }
}