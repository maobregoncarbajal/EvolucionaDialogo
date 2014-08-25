
namespace Evoluciona.Dialogo.Web.Admin.Reportes
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using Helpers;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.Configuration;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web.UI;
    using Document = iTextSharp.text.Document;
    using PageSize = iTextSharp.text.PageSize;

    public partial class ReporteTimeLine : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BeAdmin objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];
                    lblTipoAdmin.Text = objAdmin.TipoAdmin;
                    lblPais.Text = objAdmin.CodigoPais;
                    lblCodigoUsuario.Text = "00";
                    lblIdRol.Text = ((int)TipoRol.DirectoraVentas).ToString();
                    lblRutaImage.Text = Utils.RelativeWebRoot;
                    lblPeriodoEvaluacion.Text = "201218";
                    menuReporte.Reporte5 = "ui-state-active";
                    lblRutaRelativa.Text = Utils.RelativeWebRoot;
                    btnPDF.Text = ConfigurationSettings.AppSettings["NombreBotonPDF"].ToString();
                }
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationSettings.AppSettings["MensajeAlertaPagina"].ToString());
            }
        }

        #region "Reporte TimeLine"

        private Document documentPDF;

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            GenerarImagen();
        }

        private void GenerarImagen()
        {
            try
            {
                //Declaramos una instancia de nuestra clase
                ScreenCapture sc = new ScreenCapture();

                //Capturamos toda la pantalla y la asignamos a una variable de tipo Image
                System.Drawing.Image img = sc.CaptureScreen();

                //Asignamos la variable de imagen a un PictureBox
                //this.Pantalla.ImageUrl = img;

                //Ahora capturamos esta ventana y la grabamos en un archivo GIF
                //sc.CaptureWindowToFile(StatementType, "C:\\temp1.gif", ImageFormat.Gif);

                //También podemos capturar la pantalla completa y grabarla en un archivo GIF
                Random oRandom = new Random(DateTime.Now.Millisecond); //Genera numeros aleatorios
                int Minimo = Convert.ToInt32(ConfigurationSettings.AppSettings["NumeroMinimo"].ToString());
                int Maximo = Convert.ToInt32(ConfigurationSettings.AppSettings["NumeroMaximo"].ToString());
                string NombreImagen = ConfigurationSettings.AppSettings["NombreReporteTimeLine"].ToString() +
                                      oRandom.Next(Minimo, Maximo).ToString() +
                                      ConfigurationSettings.AppSettings["ExtensionImagen"].ToString();
                string rutaImagen = Server.MapPath("~") +
                                    ConfigurationSettings.AppSettings["RutaImagenTimeLine"].ToString() +
                                    NombreImagen;
                sc.CaptureScreenToFile(rutaImagen, ImageFormat.Jpeg);
                GenerarPDF(rutaImagen);
            }
            catch (Exception)
            {
                AlertaMensaje(ConfigurationSettings.AppSettings["MensajeAlerta"].ToString());
            }
        }

        private void GenerarPDF(string rutaImagen)
        {
            MemoryStream m = new MemoryStream();
            int Left = Convert.ToInt32(ConfigurationSettings.AppSettings["PageSizeMarginLeftAdmin"].ToString());
            int Right = Convert.ToInt32(ConfigurationSettings.AppSettings["PageSizeMarginRight"].ToString());
            int Top = Convert.ToInt32(ConfigurationSettings.AppSettings["PageSizeMarginTop"].ToString());
            int Bottom = Convert.ToInt32(ConfigurationSettings.AppSettings["PageSizeMarginBottom"].ToString());

            documentPDF = new Document(PageSize.A4.Rotate(), Left, Right, Top, Bottom);
            PdfWriter writer = PdfWriter.GetInstance(documentPDF, m);
            writer.CloseStream = false;
            documentPDF.Open();


            Paragraph oParagraph = new Paragraph();
            oParagraph.Add("Reporte TimeLine");
            oParagraph.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            //documentPDF.Add(oParagraph);

            string rutaLogo = Server.MapPath("~") + ConfigurationSettings.AppSettings["RutaImagenLogo"].ToString() +
                              ConfigurationSettings.AppSettings["NombreImagenLogo"].ToString();

            iTextSharp.text.Image imgLogo = iTextSharp.text.Image.GetInstance(rutaLogo);
            imgLogo.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
            imgLogo.ScaleToFit(80, 40);
            //documentPDF.Add(imgLogo);


            iTextSharp.text.Image imgTimeLine = iTextSharp.text.Image.GetInstance(rutaImagen);
            imgTimeLine.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
            imgTimeLine.Alignment = iTextSharp.text.Image.SECTION; //quieta la selección a la imagen
            //imgTimeLine.Rotation = iTextSharp.text.ImgCCITT.CCITT_ENDOFLINE;//gira en diagonal a izquierda a derecha

            Size desktopSize = default(Size);
            desktopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
            int width = desktopSize.Width;
            int height = desktopSize.Height;

            int PercentWidth = Convert.ToInt32(ConfigurationSettings.AppSettings["PercentWidth"].ToString());
            int PercentHeight = Convert.ToInt32(ConfigurationSettings.AppSettings["PercentHeight"].ToString());
            int PercentCien = Convert.ToInt32(ConfigurationSettings.AppSettings["PercentCien"].ToString());

            width = ((width * PercentWidth) / PercentCien);
            height = ((height * PercentHeight) / PercentCien);
            imgTimeLine.ScaleToFit(width, height);

            imgTimeLine.BorderColor = iTextSharp.text.BaseColor.WHITE;
            int BorderWidthLeft = Convert.ToInt32(ConfigurationSettings.AppSettings["BorderWidthLeft"].ToString());
            int BorderWidthRight = Convert.ToInt32(ConfigurationSettings.AppSettings["BorderWidthRight"].ToString());
            int BorderWidthTop = Convert.ToInt32(ConfigurationSettings.AppSettings["BorderWidthTopAdmin"].ToString());
            int BorderWidthBottom =
                Convert.ToInt32(ConfigurationSettings.AppSettings["BorderWidthBottomAdmin"].ToString());

            imgTimeLine.BorderWidthLeft = BorderWidthLeft;
            imgTimeLine.BorderWidthRight = BorderWidthRight;
            imgTimeLine.BorderWidthTop = BorderWidthTop;
            imgTimeLine.BorderWidthBottom = BorderWidthBottom;
            documentPDF.Add(imgTimeLine);

            string attachment = "attachment; filename=Article.pdf";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/pdf";
            documentPDF.Close();
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            m.Close();
            EliminarArchivo(rutaImagen);
        }

        private void EliminarArchivo(string RutaArchivo)
        {
            LogEventos oLogEventos = new LogEventos();
            if (oLogEventos.ValidFileExist(RutaArchivo))
                oLogEventos.DeleteFile(RutaArchivo);
        }

        private void AlertaMensaje(string strMensaje)
        {
            string ClienteScript = "<script language='javascript'>alert('" + strMensaje + "')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "WOpen", ClienteScript, false);
        }

        #endregion

    }
}