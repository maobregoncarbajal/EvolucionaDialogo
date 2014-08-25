
namespace Evoluciona.Dialogo.Web.Desempenio.Encuestas
{
    using System;
    using System.Web.UI;

    public partial class Encuesta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCodTipoEncuesta.Value = Request["codTipoEncuesta"];
            hfCodigoUsuario.Value = Request["codigoUsuario"];
            hfPeriodo.Value = Request["periodo"];
        }
    }
}
