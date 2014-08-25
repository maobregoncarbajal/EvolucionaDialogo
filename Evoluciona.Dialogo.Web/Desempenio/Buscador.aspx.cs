
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Buscador : Page
    {
        #region Variables

        public int tabIndexActual;
        private BeUsuario objUsuario;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (objUsuario == null) return;
            if (IsPostBack) return;

            CargarPeriodos();
            CargarRolesValidos();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BlProceso procesoBL = new BlProceso();

            if (string.IsNullOrEmpty(cboRoles.SelectedValue))
                return;
            if (string.IsNullOrEmpty(cboPeriodos.SelectedValue))
                return;

            string tipoProceso = cboProceso.SelectedValue;
            int idRol = int.Parse(cboRoles.SelectedValue);
            string status = txtStatus.Text;
            string periodoActual = cboPeriodos.SelectedItem.Text;

            TipoProceso tipoProcesoEnum = (TipoProceso)Enum.Parse(typeof(TipoProceso), tipoProceso);

            List<BeProceso> listaProcesos = procesoBL.FiltrarProcesos(tipoProcesoEnum,
                idRol, status, periodoActual, objUsuario.idUsuario, objUsuario.idRol);

            ctlContenedor.MostrarValoresFiltro(listaProcesos, tipoProcesoEnum);

            if (status == "-1" || status == "1")
                tabIndexActual = 0;
            else
                tabIndexActual = 1;
        }

        protected void ctlContenedor_MostrarDetalle(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            string[] valores = btn.CommandArgument.Split(',');
            string nombreUsuario = valores[0];
            string codigoUsuario = valores[1];
            string idProceso = valores[2];
            int idRolGR = 2;
            int idRolGZ = 3;

            if (objUsuario.idRol == 1) //DV
            {
                switch (cboRoles.SelectedValue)
                {
                    case "2": //GR
                        Response.Redirect(string.Format("~/Desempenio/resumenProcesoIniciar.aspx?codDI={0}&Nombre={1}&rolID={2}&cod=", codigoUsuario, nombreUsuario, idRolGR));
                        break;
                    case "3": //GZ
                        BlProceso procesoBL = new BlProceso();
                        BeProceso proceso = procesoBL.ObtenerProceso(Convert.ToInt32(idProceso));
                        if (proceso.Estado == Constantes.EstadoInactivo)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "MostrarPopup", "jQuery(document).ready(function() { alert('No existen datos para del Proceso.'); });", true);
                            return;
                        }

                        string parametros = string.Format("'{0}','{1}', {2}, {3}, '{4}', '{5}', '{6}'", nombreUsuario, codigoUsuario, idProceso, Constantes.RolGerenteZona, objUsuario.prefijoIsoPais, proceso.CodigoUsuarioEvaluador, proceso.Periodo);
                        ClientScript.RegisterStartupScript(GetType(), "MostrarPopup", "jQuery(document).ready(function() { CargarResumen(" + parametros + "); });", true);
                        break;
                }
            }
            else //GR
            {
                Response.Redirect(string.Format("~/Desempenio/resumenProcesoIniciar.aspx?codDI={0}&Nombre={1}&rolID={2}&cod=", codigoUsuario, nombreUsuario, idRolGZ));
            }
        }

        #endregion

        #region Metodos

        private void CargarRolesValidos()
        {
            BlRol rolBL = new BlRol();

            cboRoles.DataSource = rolBL.ObtenerRolesSubordinados(objUsuario.codigoRol);
            cboRoles.DataTextField = "Descripcion";
            cboRoles.DataValueField = "IdRol";
            cboRoles.DataBind();
        }

        private void CargarPeriodos()
        {
            List<string> periodosActuales = (List<string>)Session["periodosValidos"];

            if (periodosActuales == null) return;

            cboPeriodos.DataSource = periodosActuales;
            cboPeriodos.DataBind();

            cboPeriodos.SelectedValue = objUsuario.periodoEvaluacion;
        }

        #endregion
    }
}