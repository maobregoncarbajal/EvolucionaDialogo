
namespace Evoluciona.Dialogo.Web.Ajax
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class AjaxResumenProceso : Page
    {
        protected string mensaje, error, nombreUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Visible = false;
            if (Request["accion"] == "buscar")
            {
                BuscarUsuarioParaEvaluacion();
            }
        }

        private void BuscarUsuarioParaEvaluacion()
        {
            BeResumenProceso objResumenBE = new BeResumenProceso();
            BlResumenProceso ojResumenBL = new BlResumenProceso();
            string periodo = Request["periodo"];
            byte estado = Constantes.EstadoActivo;
            switch (Convert.ToInt32(Request["codigoRol"]))
            {
                case Constantes.RolGerenteRegion:
                    objResumenBE = ojResumenBL.ObtenerUsuarioGRegionEvaluado(Request["codigoUsuario"], Request["prefijoISO"], periodo, estado);
                    break;
                case Constantes.RolGerenteZona:
                    objResumenBE = ojResumenBL.ObtenerUsuarioGZonaEvaluado(Convert.ToInt32(Request["idGRegion"]), Request["codigoUsuarioGRegion"], Request["codigoUsuario"], Request["prefijoISO"], periodo, estado);
                    break;
                case Constantes.RolLideres:
                    objResumenBE = ojResumenBL.ObtenerUsuarioLiderEvaluado(Request["codigoUsuarioGZona"], Request["codigoUsuario"], Request["prefijoISO"], periodo, estado);
                    break;
            }
            error = "si";
            if (objResumenBE != null)
            {
                error = "no";
                nombreUsuario = objResumenBE.nombreEvaluado;

            }
            else
            {
                mensaje = "No se puede evaluar a este usuario";
            }

        }
    }
}
