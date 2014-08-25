
namespace Evoluciona.Dialogo.Web
{
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Web.UI;

    public partial class UsuarioNoRegistrado : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BlMenu obj = new BlMenu();

            if (Request["mensaje"] != null)
            {
                string mns = Request["mensaje"];
                string url = obj.ObtenerUrlMenu(Constantes.Competencias, Constantes.EstadoActivo);
                string urlEvolucionaLogin = ConfigurationManager.AppSettings["UrlEvolucionaLogin"];
                string idUsuarioTk = Request["IdUsuarioTK"];

                switch (mns)
                {
                    case "NoRegistrado":

                        lblTituMensaje.Text = "Su usuario no se encuentra registrado en nuestro sistema.";
                        lblTextMensaje.Text = "Por favor comuníquese con el área de Gestión del Talento para mayor información";
                        hlLogin.Text = "Ir al Login";
                        hlLogin.NavigateUrl = urlEvolucionaLogin;
                        hlComptenecia.Text = "Ir a Competencias";
                        hlComptenecia.NavigateUrl = url + "?IdUsuario=" + idUsuarioTk + "";

                        Response.AddHeader("REFRESH", "15;URL=" + url + "?IdUsuario=" + idUsuarioTk + "");
                        break;
                    case "DialogoNoIni":

                        lblTituMensaje.Text = "Atención";
                        lblTextMensaje.Text = "No han Iniciado el Proceso de Dialogo de Desempeñio para este Periodo.";
                        hlLogin.Text = "Ir al Login";
                        hlLogin.NavigateUrl = urlEvolucionaLogin;
                        hlComptenecia.Text = "Ir a Competencias";
                        hlComptenecia.NavigateUrl = url + "?IdUsuario=" + idUsuarioTk + "";
                        Response.AddHeader("REFRESH", "15;URL=" + url + "?IdUsuario=" + idUsuarioTk + "");
                        break;

                    default:
                        Response.Redirect(urlEvolucionaLogin);
                        break;

                }
            }
        }
    }
}
