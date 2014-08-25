
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class HeaderPaginasOperacion : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nmbrEvld = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);
            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                hlAntEquipos.Attributes.Add("disabled", "true");
                hlAntEquipos.Attributes.Add("onclick", "return false");

                hlAntCompetencias.Attributes.Add("disabled", "true");
                hlAntCompetencias.Attributes.Add("onclick", "return false");

                hlDesNegocio.Attributes.Add("disabled", "true");
                hlDesNegocio.Attributes.Add("onclick", "return false");

                hlDesEquipos.Attributes.Add("disabled", "true");
                hlDesEquipos.Attributes.Add("onclick", "return false");

                hlDesCompetencias.Attributes.Add("disabled", "true");
                hlDesCompetencias.Attributes.Add("onclick", "return false");
            }
        }

        public void CargarControles(List<string> periodos, string nombreEvaluado, string periodoActual, string nombreImagen)
        {
            BlGerenteRegion blgerente = new BlGerenteRegion();
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            BeResumenProceso objResumenProceso = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            cboPeriodos.DataSource = periodos;
            cboPeriodos.DataBind();

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

            lblEvaluado.Text = nombreEvaluado.ToUpper().Replace("GR NUEVA - ", "").Replace("GZ NUEVA - ", "") + " (" + objResumenProceso.codigoUsuario + ")";
            lblRegionZona.Text = regionZona;

            cboPeriodos.SelectedValue = periodoActual;
            imgImagenDescripcion.ImageUrl = Utils.AbsoluteWebRoot + "Images/" + nombreImagen;

            hlResumen.NavigateUrl = string.Format("javascript:CargarResumen('{0}Admin/ResumenProceso.aspx?nomEvaluado={1}&codEvaluado={2}&idProceso={3}&rolEvaluado={4}&codPais={5}&periodo={6}&codEvaluador={7}');",
                Utils.AbsoluteWebRoot, nombreEvaluado, objResumenProceso.codigoUsuario, objResumenProceso.idProceso, objResumenProceso.codigoRolUsuario, objUsuario.prefijoIsoPais, periodoActual, objUsuario.codigoUsuario);

        }
    }
}