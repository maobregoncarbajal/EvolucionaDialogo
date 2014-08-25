
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class HeaderPaginasOperacionEvaluado : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void CargarControles(List<string> periodos, string nombreEvaluado, string periodoActual, string nombreImagen)
        {
            BlGerenteRegion blgerente = new BlGerenteRegion();
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            BeResumenProceso objResumenProceso = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            cboPeriodos.DataSource = periodos;
            cboPeriodos.DataBind();
            cboPeriodos.Visible = false;
            
            string regionZona = string.Empty;

            switch (objResumenProceso.codigoRolUsuario)
            {
                case Constantes.RolGerenteRegion:
                    regionZona = blgerente.ObtenerDescripcionRegion(objResumenProceso.idProceso, objResumenProceso.codigoUsuario, objResumenProceso.periodo, objResumenProceso.prefijoIsoPais);
                    lblTextoRZ.Text = "Región: ";
                    break;
                case Constantes.RolGerenteZona:
                    regionZona = blgerente.ObtenerDescripcionZona(objResumenProceso.idProceso, objResumenProceso.codigoUsuario, objResumenProceso.periodo, objResumenProceso.prefijoIsoPais);
                    lblTextoRZ.Text = "Zona: ";
                    break;
                default:
                    regionZona = string.Empty;
                    lblTextoRZ.Text = string.Empty;
                    break;
            }

            cboPeriodos.DataSource = periodos;
            cboPeriodos.DataBind();

            lblEvaluado.Text = nombreEvaluado.ToUpper() + " (" + objResumenProceso.codigoUsuario + ")";
            lblRegionZona.Text = regionZona;
            //lblEvaluado.Text = nombreEvaluado.ToUpper();
            cboPeriodos.SelectedValue = periodoActual;
            imgImagenDescripcion.ImageUrl = Utils.AbsoluteWebRoot + "Images/" + nombreImagen;
        }
    }
}