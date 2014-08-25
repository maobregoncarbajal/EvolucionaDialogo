using System;
using Evoluciona.Dialogo.BusinessLogic.Tareas;

namespace Evoluciona.Dialogo.Web.Admin.Tareas
{
    public partial class TareaAlertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var blTareaAlertas = new BLTareaAlertas();
            blTareaAlertas.IniciarProcesoNotificaciones();
        }
    }
}