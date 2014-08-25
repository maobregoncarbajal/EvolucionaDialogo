
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
                        lblTituMensaje.Text = "Su sesi�n ha expirado por favor vuelve a iniciar sesi�n";
                        hlink.Text = "Cerrar la aplicaci�n";
                        hlink.NavigateUrl = "javascript:close();";
                        break;
                    case "NoRegistrado":
                        lblTituMensaje.Text = "Su usuario no se encuentra registrado en nuestro sistema.";
                        lblTextMensaje.Text = "Por favor comun�quese con el �rea de Gesti�n del Talento para mayor informaci�n";
                        hlink.Text = "<<< Retornar";
                        hlink.NavigateUrl = urlEvolucionaLogin;
                        break;
                    case "DialogoNoIni":
                        lblTituMensaje.Text = "Alerta";
                        lblTextMensaje.Text = "No han Iniciado el Proceso de</br>Dialogo de Desempe�io para este Periodo.";
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
