
namespace Evoluciona.Dialogo.Web
{
    using System;
    using System.Configuration;
    using System.Web.UI;

    public partial class error : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["mensaje"] != null)
            {
                string tipoMnesaje = Request["mensaje"];
                string urlEvolucionaLogin = ConfigurationManager.AppSettings["UrlEvolucionaLogin"];

                switch (tipoMnesaje)
                {
                    case "sesion":
                        lblTituMensaje.Text = "Su sesión ha expirado por favor vuelve a iniciar sesión";
                        hlink.Text = "Cerrar la aplicación";
                        hlink.NavigateUrl = "javascript:close();";
                        break;
                    case "NoRegistrado":
                        lblTituMensaje.Text = "Su usuario no se encuentra registrado en nuestro sistema.";
                        lblTextMensaje.Text = "Por favor comuníquese con el área de Gestión del Talento para mayor información";
                        hlink.Text = "<<< Retornar";
                        hlink.NavigateUrl = urlEvolucionaLogin;
                        break;
                    case "DialogoNoIni":
                        lblTituMensaje.Text = "Alerta";
                        lblTextMensaje.Text = "No han Iniciado el Proceso de</br>Dialogo de Desempeñio para este Periodo.";
                        hlink.Text = "<<< Retornar";
                        hlink.NavigateUrl = urlEvolucionaLogin;
                        break;
                    case "CUB_D":
                        lblTituMensaje.Text = "Alerta";
                        lblTextMensaje.Text = "CUB DUPLICADO.";
                        hlink.Text = "<<< Retornar";
                        hlink.NavigateUrl = urlEvolucionaLogin;
                        break;
                    case "CUB_NE":
                        lblTituMensaje.Text = "Alerta";
                        lblTextMensaje.Text = "CUB No encontrado";
                        hlink.Text = "<<< Retornar";
                        hlink.NavigateUrl = urlEvolucionaLogin;
                        break;
                    default:
                        Response.Redirect(urlEvolucionaLogin);
                        break;
                }
            }
        }
    }
}
