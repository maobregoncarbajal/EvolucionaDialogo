using System;
using Evoluciona.Dialogo.BusinessLogic.Tareas;

namespace Evoluciona.Dialogo.Web.Admin.Tareas
{
    public partial class TareaEnviarCorreos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var blTareaEnviarCorreos = new BLTareaEnviarCorreos();
            blTareaEnviarCorreos.IniciarProcesoEnviarCorreos();
        }
    }
}