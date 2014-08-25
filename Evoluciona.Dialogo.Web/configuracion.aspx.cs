
namespace Evoluciona.Dialogo.Web
{
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class configuracion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarPaises();
                CargarPeriodo();
            }
        }

        private void CargarPaises()
        {
            BlConfiguracion objConfig = new BlConfiguracion();
            ddlPais.DataSource = objConfig.SeleccionarPaises();
            ddlPais.DataValueField = "chrPrefijoIsoPais";
            ddlPais.DataTextField = "chrPrefijoIsoPais";
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem("Seleccione País", "0"));

        }
        private void CargarPeriodo()
        {
            string periodo = DateTime.Today.Year + " ";
            ddlPeriodo.Items.Insert(0, new ListItem(periodo + "I", "1"));
            ddlPeriodo.Items.Insert(1, new ListItem(periodo + "II", "2"));
            ddlPeriodo.Items.Insert(2, new ListItem(periodo + "III", "3"));
        }

        protected void btnActivarDVaGR_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (ddlPais.SelectedValue == "0")
            {
                lblMensaje.Text = "Debe seleccionar un País";
                return;
            }
            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoDvGr);
            if (codProcesado > 0)
            {

                lblMensaje.Text = "Ya existe un inicio de dialogo de desempeñio de las DV a GR para el periodo seleccionado";
                return;
            }
            lblFechaDVaGR.Text = DateTime.Now.ToShortDateString();

            if (objConfig.InsertarConfiguracionProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, DateTime.Now, Constantes.IndicadorEvaluadoDvGr))
            {
                lblMensaje.Text = "El inicio del dialogo de desempeño para DV a GR ha sido activado";
                DataTable dtDV = objConfig.SeleccionarDVentasPorEvaluar(ddlPais.SelectedValue);
                EnviarCorreos(dtDV, Constantes.RolDirectorVentas);
                
            }
            else
            {
                lblMensaje.Text = "No se pudo iniciar el proceso";
            }
        }

        protected void btnActivarGRaGZ_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            if (ddlPais.SelectedValue == "0")
            {
                lblMensaje.Text = "Debe seleccionar un País";
                return;
            }
            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoGrGz);
            if (codProcesado > 0)
            {

                lblMensaje.Text = "Ya existe un inicio de dialogo de desempeñio de las GR a GZ para el periodo seleccionado";
                return;
            }
            lblFechaGRaGZ.Text = DateTime.Now.ToShortDateString();

            if (objConfig.InsertarConfiguracionProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, DateTime.Now, Constantes.IndicadorEvaluadoGrGz))
            {
                lblMensaje.Text = "El inicio del dialogo de desempeño para GR a GZ ha sido activado";
                DataTable dtGR = objConfig.SeleccionarGRegionPorEvaluar(ddlPais.SelectedValue);
                //EnviarCorreos(dtDV, constantes.rolDirectorVentas);
                if (dtGR!=null)
                {
                    //DataTable dtGR = objConfig.SeleccionarGRegionPorEvaluar(ddlPais.SelectedValue);
                    EnviarCorreos(dtGR, Constantes.RolGerenteRegion);

                    //int idGerenteRegion = 0;
                    //for (int x = 0; x < dtGR.Rows.Count; x++)
                    //{
                    //    idGerenteRegion = Convert.ToInt32(dtGR.Rows[x]["IDUsuario"].ToString());
                    //    //DataTable dtGZ = objConfig.SeleccionarGZonaPorEvaluar(idGerenteRegion, ddlPais.SelectedValue);
                    //    //EnviarCorreos(dtGZ, constantes.rolGerenteZona);
                        
                    //}

                }

            }
            else
            {
                lblMensaje.Text = "No se pudo iniciar el proceso";
            }
        }

        private void EnviarCorreos(DataTable dtUsuarios, int codigoRol)
        {

            MailAddress correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"].ToString()); 

            string servidorSMTP = ConfigurationManager.AppSettings["servidorSMTP"].ToString();
            byte enviado = 0;
            if (dtUsuarios.Rows.Count > 0)
            {
                string archivo = Server.MapPath("configuracion.aspx").Replace("configuracion.aspx", "KeyPublicaDDesempenio.xml");
                Encriptacion objEncriptar = new Encriptacion();
                string estiloTD = "font-family:Arial; font-size:13px; color:#6a288a;";
                for (int x = 0; x < dtUsuarios.Rows.Count; x++)
                { // vchNombreCompleto, vchCorreoElectronico as documentoIdentidad

                    string strHTML = string.Empty;
                    string paramLogeo = dtUsuarios.Rows[x]["documentoIdentidad"].ToString() + "|" + ddlPais.SelectedValue + "|" + ddlPeriodo.SelectedItem.Text + "|" + codigoRol;
                    paramLogeo = objEncriptar.Encriptar(paramLogeo, archivo);
                    paramLogeo = HttpUtility.UrlEncode(paramLogeo);
                     
                        strHTML += "<table align='center' border='0'>";
                        strHTML += "<tr><td style='"+estiloTD+"'>Belcorp es un gran equipo!, y nos sentimos orgullosos porque tú eres parte fundamental de él.</td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td style='" + estiloTD + "'>En esta etapa, estamos iniciando los <span style='" + estiloTD + "'><b> Diálogos Evoluciona,</b></span> un espacio para analizar el desempeño de tu equipo, retroalimentarlo y planificar las acciones que lo lleven al logro de sus metas.</td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td style='" + estiloTD + "'><b>Antes de reunirte con tu equipo: Prepárate</b></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td style='" + estiloTD + "'>Sólo ingresa al sistema a traves de este link y revisa el desempeño de tu equipo e identifica las variables en las que te enfocarás en la reunión</td></tr>";
                        //strHTML += "<tr><td><a style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold; text-decoration:none;' href='validacion.aspx?codigoUsuario=" + dtUsuarios.Rows[x]["documentoIdentidad"].ToString() + "&prefijoIsoPais=" + ddlPais.SelectedValue + "&periodo=" + ddlPeriodo.SelectedItem.Text + "&codigoRol=" + codigoRol + "' target='_blank'>Haz click aquí, para ingresar</a></td></tr>";
                        strHTML += "<tr><td><a style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold; text-decoration:none;' href='" + ConfigurationManager.AppSettings["URLdesempenio"].ToString() + "validacion.aspx?sson=" + paramLogeo + "' target='_blank'>Haz click aquí, para ingresar</a></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td></td></tr>";
                        strHTML += "<tr><td style='" + estiloTD + "'>Una vez que te hayas preparado, da un click en el botón <span style='" + estiloTD + "'><b>«Guardar»</b></span> y estarás lista para el diálogo con tu equipo.</td></tr>";
                        strHTML += "<tr><td style='" + estiloTD + "'>Recuerda, cuando tu equipo crece, tú creces.</td></tr>";
                        strHTML += "<tr><td style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold;'>¡Contamos contigo!</td></tr>";
                        strHTML += "</table>";
                   


                    SmtpClient enviar = new SmtpClient(servidorSMTP);
                    try
                    {
                       MailAddress correoTo = new MailAddress(dtUsuarios.Rows[x]["vchCorreoElectronico"].ToString());
                        //MailAddress correoTo = new MailAddress("dhuallanca@csigcomt.com");
                        MailMessage msjEmail = new MailMessage(correoFrom, correoTo);
                        msjEmail.Subject = "Diálogos Evoluciona";
                        msjEmail.IsBodyHtml = true;
                        msjEmail.Body = strHTML;
                        enviar.Send(msjEmail);
                        enviado = 1;
                    }
                    catch
                    {//modificar
                        enviado = 0;
                    }
                    BlConfiguracion objConfig = new BlConfiguracion();
                    objConfig.InsertarInicioDialogo(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, dtUsuarios.Rows[x]["documentoIdentidad"].ToString(), codigoRol, enviado);

                }
            }

        }


    }

}
