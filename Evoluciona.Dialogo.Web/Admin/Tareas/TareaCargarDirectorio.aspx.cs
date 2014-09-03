using Evoluciona.Dialogo.Helpers;
using Evoluciona.Dialogo.Web.Helpers;
using System;

namespace Evoluciona.Dialogo.Web.Admin.Tareas
{

    public partial class TareaCargarDirectorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            const string pais = Constantes.CeroCero; //"00" = Todos
            var region = String.Empty;
            var zona = String.Empty;
            var cargo = String.Empty;
            var periodo = String.Empty;
            var estadoCargo = String.Empty;

            Utils.ConsumirWsObtenerClientesDirectorio(pais, region, zona, cargo, periodo, estadoCargo);
        }
    }
}