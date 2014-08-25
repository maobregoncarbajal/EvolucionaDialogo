
namespace Evoluciona.Dialogo.Web
{
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Web.UI;

    public partial class VerificarSesion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool existeSesion = Session[Constantes.ObjUsuarioLogeado] == null;
            string res = JsonConvert.SerializeObject(existeSesion);

            Response.ContentType = "application/json";
            Response.Write(res);
            Response.End();
        }
    }
}
