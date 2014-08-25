
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Configuracion : Page
    {
        #region Variables

        private BeAdmin objAdmin;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (Page.IsPostBack) return;

            CargarPaises();
            CargarPeriodo();
        }

        protected void btnActivarDVaGR_Click(object sender, EventArgs e)
        {
            //01
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(1);", true);
        }

        protected void btnActivarGRaGZ_Click(object sender, EventArgs e)
        {
            //02
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(2);", true);
        }

        protected void btnEnvCorDV_Click(object sender, EventArgs e)
        {
            //03
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(3);", true);
        }

        protected void btnEnvCorGR_Click(object sender, EventArgs e)
        {
            //04
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(4);", true);
        }

        protected void btnEnvCorGrEvaluados_Click(object sender, EventArgs e)
        {
            //05
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(5);", true);
        }

        protected void btnEnvCorGzEvaluados_Click(object sender, EventArgs e)
        {
            //06
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(6);", true);
        }

        protected void btnEnvCorAdeeDv_Click(object sender, EventArgs e)
        {
            //07
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(7);", true);
        }

        protected void btnEnvCorAdeeGr_Click(object sender, EventArgs e)
        {
            //08
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(8);", true);
        }

        protected void btnEnvPlaAcorDiaGr_Click(object sender, EventArgs e)
        {
            //09
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(9);", true);
        }

        protected void btnEnvPlaAcorDiaGz_Click(object sender, EventArgs e)
        {
            //10
            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(10);", true);
        }

        #endregion

        #region Metodos

        private void CargarPaises()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = new List<BePais>();

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    paises = paisBL.ObtenerPaises();
                    ddlPais.DataTextField = "NombrePais";
                    ddlPais.DataValueField = "prefijoIsoPais";
                    ddlPais.DataSource = paises;
                    ddlPais.DataBind();
                    ddlPais.Items.Insert(0, new ListItem("Seleccione País", "0"));

                    break;
                case Constantes.RolAdminPais:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    ddlPais.DataTextField = "NombrePais";
                    ddlPais.DataValueField = "prefijoIsoPais";
                    ddlPais.DataSource = paises;
                    ddlPais.DataBind();

                    break;
            }
        }

        private void CargarPeriodo()
        {
            string periodo = DateTime.Today.Year + " ";
            string periodony = DateTime.Today.Year + 1 + " ";
            ddlPeriodo.Items.Insert(0, new ListItem(periodo + "I", "1"));
            ddlPeriodo.Items.Insert(1, new ListItem(periodo + "II", "2"));
            ddlPeriodo.Items.Insert(2, new ListItem(periodo + "III", "3"));
            //ddlPeriodo.Items.Insert(3, new ListItem(periodony + "I", "4"));
        }

        private void EnviarCorreos(DataTable dtUsuarios, int codigoRol)
        {
            MailAddress correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"]);

            string servidorSMTP = ConfigurationManager.AppSettings["servidorSMTP"];
            byte enviado = 0;

            if (dtUsuarios.Rows.Count > 0)
            {
                string archivo = Server.MapPath("~/") + "KeyPublicaDDesempenio.xml";
                Encriptacion objEncriptar = new Encriptacion();
                string estiloTD = "font-family:Arial; font-size:13px; color:#6a288a;";

                for (int x = 0; x < dtUsuarios.Rows.Count; x++)
                {
                    string strHTML = string.Empty;
                    string paramLogeo = dtUsuarios.Rows[x]["documentoIdentidad"] + "|" + ddlPais.SelectedValue + "|" + ddlPeriodo.SelectedItem.Text + "|" + codigoRol;
                    paramLogeo = objEncriptar.Encriptar(paramLogeo, archivo);
                    paramLogeo = HttpUtility.UrlEncode(paramLogeo);

                    strHTML += "<table align='center' border='0'>";
                    strHTML += "<tr><td style='" + estiloTD + "'>Belcorp es un gran equipo!, y nos sentimos orgullosos porque tú eres parte fundamental de él.</td></tr>";
                    strHTML += "<tr><td></td></tr>";
                    strHTML += "<tr><td style='" + estiloTD + "'>En esta etapa, estamos iniciando los <span style='" + estiloTD + "'><b> Diálogos Evoluciona,</b></span> un espacio para analizar el desempeño de tu equipo, retroalimentarlo y planificar las acciones que lo lleven al logro de sus metas.</td></tr>";
                    strHTML += "<tr><td></td></tr>";
                    strHTML += "<tr><td style='" + estiloTD + "'><b>Antes de reunirte con tu equipo: Prepárate</b></td></tr>";
                    strHTML += "<tr><td></td></tr>";
                    strHTML += "<tr><td></td></tr>";
                    strHTML += "<tr><td></td></tr>";
                    strHTML += "<tr><td></td></tr>";
                    strHTML += "<tr><td style='" + estiloTD + "'>Sólo ingresa al sistema a traves de este link y revisa el desempeño de tu equipo e identifica las variables en las que te enfocarás en la reunión</td></tr>";
                    strHTML += "<tr><td><a style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold; text-decoration:none;' href='" + ConfigurationManager.AppSettings["URLdesempenio"] + "validacion.aspx?sson=" + paramLogeo + "' target='_blank'>Haz click aquí, para ingresar</a></td></tr>";
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
                        MailMessage msjEmail = new MailMessage(correoFrom, correoTo);
                        msjEmail.Subject = "Diálogos Evoluciona";
                        msjEmail.IsBodyHtml = true;
                        msjEmail.Body = strHTML;
                        enviar.Port = 25;
                        enviar.Send(msjEmail);
                     
                        enviado = 1;

                    }
                    catch(SmtpException ex)
                    {
                        BlConfiguracion objConfig = new BlConfiguracion();
                        objConfig.InsertarLogEnvioCorreo("Diálogos Evoluciona", dtUsuarios.Rows[x]["vchCorreoElectronico"].ToString(), ex.Message);
                        enviado = 0;


                    }
                    finally
                    {
                        BlConfiguracion objConfig = new BlConfiguracion();
                        objConfig.ActualizarInicioDialogo(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, dtUsuarios.Rows[x]["documentoIdentidad"].ToString(), codigoRol, enviado);
                    }
                }
            }
        }

        #endregion

        private void EnviarCorreosEvaluados(DataTable dtUsuariosEvaluados, int codigoRolUsuario, string periodo, string prefijoIsoPais)
        {
            MailAddress correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"]);

            string servidorSMTP = ConfigurationManager.AppSettings["servidorSMTP"];

            if (dtUsuariosEvaluados.Rows.Count > 0)
            {
                //string archivo = Server.MapPath("../configuracion.aspx").Replace("configuracion.aspx", "KeyPublicaDDesempenio.xml");
                string archivo = Server.MapPath("~/") + "KeyPublicaDDesempenio.xml";
                Encriptacion objEncriptar = new Encriptacion();

                for (int x = 0; x < dtUsuariosEvaluados.Rows.Count; x++)
                {
                    string strHtml = string.Empty;
                    string paramLogeo = dtUsuariosEvaluados.Rows[x]["documentoIdentidad"] + "|" + prefijoIsoPais + "|" + periodo + "|" + codigoRolUsuario;
                    paramLogeo = objEncriptar.Encriptar(paramLogeo, archivo);
                    paramLogeo = HttpUtility.UrlEncode(paramLogeo);

                    if (Constantes.RolGerenteRegion == codigoRolUsuario)
                    {
                        strHtml += "<table align='center' border='0'>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 26px;'>Belcorp es una gran equipo!, y nos sentimos orgullosos porque tú eres parte fundamental de él.</td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>En esta etapa, estamos iniciando los <span style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'> Diálogos Evoluciona, </span> un espacio durante el cual, recibirás retroalimentación de tu jefe, sobre los avances hacia el logro de tus objetivos de negocio y desarrollo de tus competencias.</td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'>Antes de reunirte con tu Jefe: Prepárate</td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a;'>Revisa tu desempeño del período, identifica aquellas variables «causa» que podrías mejorar y las acciones que podrías emprender para ello.</td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a;'>Luego de tu preparación, ya estarás lista para tu Diálogo Evoluciona.</td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a;'>Sólo ingresa al sistema a traves de este link e identifica las variables en las que te enfocarás en la reunión</td></tr>";
                        strHtml += "<tr><td><a style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold; text-decoration:none;' href='" + ConfigurationManager.AppSettings["URLdesempenio"].ToString() + "validacion.aspx?sson=" + paramLogeo + "' target='_blank'>Haz click aquí, para ingresar</a></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold;'>¡Contamos contigo!</td></tr>";
                        strHtml += "</table>";
                    }
                    else if (Constantes.RolGerenteZona == codigoRolUsuario)
                    {
                        strHtml += "<table align='center' border='0'>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 26px;'>Belcorp es una gran equipo!, y nos sentimos orgullosos porque tú eres parte fundamental de él.</td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>En esta etapa, estamos iniciando los <span style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'> Diálogos Evoluciona, </span> un espacio durante el cual, recibirás retroalimentación de tu jefe, sobre los avances hacia el logro de tus objetivos de negocio y desarrollo de tus competencias.</td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'>Antes de reunirte con tu Jefe: Prepárate</td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a;'>Revisa tu desempeño del periodo, identifica aquellas variables «causa» que podrías mejorar y las acciones que podrías emprender para ello.</td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a;'>Luego de tu preparación, ya estarás lista para tu Diálogo Evoluciona.</td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a;'>Sólo ingresa al sistema a traves de este link e identifica las variables en las que te enfocarás en la reunión</td></tr>";
                        strHtml += "<tr><td><a style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold; text-decoration:none;' href='" + ConfigurationManager.AppSettings["URLdesempenio"].ToString() + "validacion.aspx?sson=" + paramLogeo + "' target='_blank'>Haz click aquí, para ingresar</a></td></tr>";
                        strHtml += "<tr><td style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold;'>¡Contamos contigo!</td></tr>";
                        strHtml += "</table>";
                    }

                    SmtpClient enviar = new SmtpClient(servidorSMTP);
                    try
                    {
                        MailAddress correoTo = new MailAddress(dtUsuariosEvaluados.Rows[x]["vchCorreoElectronico"].ToString());
                        MailMessage msjEmail = new MailMessage(correoFrom, correoTo);
                        msjEmail.Subject = "Inicio de tu Diálogo Evoluciona";
                        msjEmail.IsBodyHtml = true;
                        msjEmail.Body = strHtml;
                        enviar.Send(msjEmail);
                    }
                    catch
                    {
                    }
                }
            }
        }


        private void EnviarCorreoAprobacionDialogoEvolucionadeTuEquipo(string correoEvaluador, int codigoRol, int cantDialogosAprobados, List<BeUsuario> lstUsuariosProcesados)
        {
            MailAddress correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"].ToString());
            string descripcionRol = "";
            if (Constantes.RolGerenteRegion == codigoRol)
            {
                descripcionRol = " Gerentes de Región ";
            }
            else if (Constantes.RolGerenteZona == codigoRol)
            {
                descripcionRol = " Gerentes de Zona ";
            }
            string servidorSMTP = ConfigurationManager.AppSettings["servidorSMTP"].ToString();
            string strHTML = string.Empty;
            string estiloTD = "font-family:Arial; font-size:13px; color:#6a288a;";
            strHTML += "<table align='center' border='0'>";
            strHTML += "<tr><td style='" + estiloTD + "'>Te confirmamos que a la fecha " + cantDialogosAprobados.ToString() + descripcionRol + " de tu equipo han cerrado su Diálogo evoluciona.</td></tr>";
            strHTML += "<tr><td> </td></tr>";

            if (lstUsuariosProcesados != null && lstUsuariosProcesados.Count > 0)
            {
                strHTML += "<tr><td style='" + estiloTD + "'>Quedan pendientes:</td></tr>";
                int contador = 1;
                foreach (BeUsuario objUsuario in lstUsuariosProcesados)
                {
                    strHTML += "<tr><td style='" + estiloTD + "'>" + contador.ToString() + ". " + objUsuario.nombreUsuario + "</td></tr>";
                    contador = contador + 1;
                }
                strHTML += "<tr><td></td></tr>";
            }
            strHTML += "<tr><td style='" + estiloTD + "'><b>Recuerda realizar el seguimiento a los planes acordados para asegurarte que se <br>lleven a cabo.</b></td></tr>";

            strHTML += "<tr><td style='" + estiloTD + "'>Gracias por participar!</td></tr>";

            strHTML += "</table>";
            SmtpClient enviar = new SmtpClient(servidorSMTP);
            try
            {
                MailAddress correoTo = new MailAddress(correoEvaluador);
                //MailAddress correoTo = new MailAddress(correoEvaluador);
                MailMessage msjEmail = new MailMessage(correoFrom, correoTo);
                msjEmail.Subject = "Aprobación del Diálogo Evoluciona de tu equipo.";
                msjEmail.IsBodyHtml = true;
                msjEmail.Body = strHTML;
                enviar.Send(msjEmail);
            }
            catch
            {
                //modificar
            }
        }

        private void IniciarProcesoNotificacionDirectoraVenta(string prefijoIsoPais, string periodo)
        {
            BlDataMart objDataMart = new BlDataMart();
            BlConfiguracion objConfig = new BlConfiguracion();

            objDataMart.InsertarLogCarga("IniciarProcesoNotificacionDirectoraVenta", "Inicio de tarea TareaEnviarCorreos a las DV");
            DataTable dtDv = objConfig.SeleccionarDVentasPorEvaluar(ddlPais.SelectedValue);
            int codigoRol = Constantes.RolGerenteRegion;
            int idRol = ObtenerIDRol(Constantes.RolGerenteRegion);

            if (dtDv != null)
            {
                if (dtDv.Rows.Count > 0)
                {
                    for (int x = 0; x < dtDv.Rows.Count; x++)
                    {
                        List<BeUsuario> lstUsuariosGr = objConfig.SeleccionarGRegionProcesadasPorDv(dtDv.Rows[x]["documentoIdentidad"].ToString(), idRol, prefijoIsoPais, periodo);

                        if (lstUsuariosGr.Count > 0)
                        {
                            int cantDialogosAprobados = 0;
                            List<BeUsuario> lstUsuariosGrProcesados = lstUsuariosGr.FindAll(delegate(BeUsuario objUsuarioGrpFind) { return objUsuarioGrpFind.estadoProceso == Constantes.EstadoProcesoRevision; });
                            List<BeUsuario> lstUsuariosGrProcesoAprobado = lstUsuariosGr.FindAll(delegate(BeUsuario objUsuarioGrpFind) { return objUsuarioGrpFind.estadoProceso == Constantes.EstadoProcesoCulminado; });
                            if (lstUsuariosGrProcesoAprobado != null)
                            {
                                cantDialogosAprobados = lstUsuariosGrProcesoAprobado.Count;
                            }
                            EnviarCorreoAprobacionDialogoEvolucionadeTuEquipo(dtDv.Rows[x]["vchCorreoElectronico"].ToString(), codigoRol, cantDialogosAprobados, lstUsuariosGrProcesados);
                        }
                    }
                }
            }
        }

        private void IniciarProcesoNotificacionGerenteRegion(string prefijoIsoPais, string periodo)
        {
            BlDataMart objDataMart = new BlDataMart();
            BlConfiguracion objConfig = new BlConfiguracion();

            objDataMart.InsertarLogCarga("IniciarProcesoNotificacionGerenteRegion", "Inicio de tarea TareaEnviarCorreos a las GR");
            DataTable dtGr = objConfig.SeleccionarGRegionPorEvaluar(ddlPais.SelectedValue);
            int codigoRol = Constantes.RolGerenteZona;
            int idRol = ObtenerIDRol(Constantes.RolGerenteZona);

            if (dtGr != null)
            {
                if (dtGr.Rows.Count > 0)
                {
                    for (int x = 0; x < dtGr.Rows.Count; x++)
                    {
                        List<BeUsuario> lstUsuariosGz = objConfig.SeleccionarGZonaProcesadasPorGr(dtGr.Rows[x]["documentoIdentidad"].ToString(), idRol, prefijoIsoPais, periodo);

                        if (lstUsuariosGz.Count > 0)
                        {
                            int cantDialogosAprobados = 0;
                            List<BeUsuario> lstUsuariosGzProcesados = lstUsuariosGz.FindAll(delegate(BeUsuario objUsuarioGzpFind) { return objUsuarioGzpFind.estadoProceso == Constantes.EstadoProcesoRevision; });
                            List<BeUsuario> lstUsuariosGzProcesoAprobado = lstUsuariosGz.FindAll(delegate(BeUsuario objUsuarioGzpFind) { return objUsuarioGzpFind.estadoProceso == Constantes.EstadoProcesoCulminado; });
                            if (lstUsuariosGzProcesoAprobado != null)
                            {
                                cantDialogosAprobados = lstUsuariosGzProcesoAprobado.Count;
                            }
                            EnviarCorreoAprobacionDialogoEvolucionadeTuEquipo(dtGr.Rows[x]["vchCorreoElectronico"].ToString(), codigoRol, cantDialogosAprobados, lstUsuariosGzProcesados);
                        }
                    }
                }
            }
        }

        private int ObtenerIDRol(int codigoRolEvaluado)
        {
            int idRol = 0;
            BlUsuario objRol = new BlUsuario();
            DataTable dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
            }
            return idRol;
        }

        protected void btnTrggr01ActivarDVaGR_Click(object sender, EventArgs e)
        {
            //01
            lblMensaje.Text = string.Empty;
            lblFechaDVaGR.Text = string.Empty;
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

                DataTable dtDv = objConfig.SeleccionarDVentasPorEvaluar(ddlPais.SelectedValue);

                if (dtDv != null)
                {
                    if (dtDv.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtDv.Rows.Count; x++)
                        {
                            objConfig.InsertarInicioDialogo(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, dtDv.Rows[x]["documentoIdentidad"].ToString(), Constantes.RolDirectorVentas, 0);
                        }
                    }
                }

                //DataTable dtDV = objConfig.SeleccionarDVentasPorEvaluar(ddlPais.SelectedValue);
                //EnviarCorreos(dtDV, constantes.rolDirectorVentas);
            }
            else
            {
                lblMensaje.Text = "No se pudo iniciar el proceso";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr02ActivarGRaGZ_Click(object sender, EventArgs e)
        {
            //02
            lblMensaje.Text = string.Empty;
            lblFechaGRaGZ.Text = string.Empty;

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

                DataTable dtGr = objConfig.SeleccionarGRegionPorEvaluar(ddlPais.SelectedValue);

                if (dtGr != null)
                {
                    if (dtGr.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtGr.Rows.Count; x++)
                        {
                            objConfig.InsertarInicioDialogo(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, dtGr.Rows[x]["documentoIdentidad"].ToString(), Constantes.RolGerenteRegion, 0);
                        }
                    }
                }
            }
            else
            {
                lblMensaje.Text = "No se pudo iniciar el proceso";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr03EnvCorDV_Click(object sender, EventArgs e)
        {
            //03
            lblMensajeDe.Text = string.Empty;
            lblDeDV.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajeDe.Text = "Debe seleccionar un País";
                return;
            }

            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoDvGr);

            if (codProcesado > 0)
            {
                DataTable dtDv = objConfig.SeleccionarDVentasPorEvaluar(ddlPais.SelectedValue);

                if (dtDv != null)
                {
                    EnviarCorreos(dtDv, Constantes.RolDirectorVentas);
                    lblDeDV.Text = "El correo ha sido enviado a la DV evaluadora";
                }
            }
            else
            {
                lblMensajeDe.Text = "No existe un inicio de dialogo de desempeñio de las DV a GR para el periodo seleccionado";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr04EnvCorGR_Click(object sender, EventArgs e)
        {
            //04
            lblMensajeDe.Text = string.Empty;
            lblDeGR.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajeDe.Text = "Debe seleccionar un País";
                return;
            }

            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoGrGz);

            if (codProcesado > 0)
            {
                DataTable dtGr = objConfig.SeleccionarGRegionPorEvaluar(ddlPais.SelectedValue);

                if (dtGr != null)
                {
                    EnviarCorreos(dtGr, Constantes.RolGerenteRegion);
                    lblDeGR.Text = "El correo ha sido enviado a las GRs evaluadoras";
                }
            }
            else
            {
                lblMensajeDe.Text = "No existe un inicio de dialogo de desempeñio de las GR a GZ para el periodo seleccionado";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr05EnvCorGrEvaluados_Click(object sender, EventArgs e)
        {
            //05
            lblMensajeIde.Text = string.Empty;
            lblIdeGR.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajeIde.Text = "Debe seleccionar un País";
                return;
            }

            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoDvGr);

            if (codProcesado > 0)
            {
                DataTable dtGr = objConfig.SeleccionarGRegionPorEvaluar(ddlPais.SelectedValue);

                if (dtGr != null)
                {
                    EnviarCorreosEvaluados(dtGr, Constantes.RolGerenteRegion, ddlPeriodo.SelectedItem.Text, ddlPais.SelectedValue);
                    lblIdeGR.Text = "El correo ha sido enviado a las GRs evaluadas";
                }
            }
            else
            {
                lblMensajeIde.Text = "No existe un inicio de dialogo de desempeñio de las DV a GR para el periodo seleccionado";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr06EnvCorGzEvaluados_Click(object sender, EventArgs e)
        {
            //06
            lblMensajeIde.Text = string.Empty;
            lblIdeGZ.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajeIde.Text = "Debe seleccionar un País";
                return;
            }

            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoGrGz);
            if (codProcesado > 0)
            {
                DataTable dtGz = objConfig.SeleccionarGZonaPorPais(ddlPais.SelectedValue);

                if (dtGz != null)
                {
                    EnviarCorreosEvaluados(dtGz, Constantes.RolGerenteZona, ddlPeriodo.SelectedItem.Text, ddlPais.SelectedValue);
                    lblIdeGZ.Text = "El correo ha sido enviado a las GZs evaluadas";
                }
            }
            else
            {
                lblMensajeIde.Text = "No existe un inicio de dialogo de desempeñio de las GR a GZ para el periodo seleccionado";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr07EnvCorAdeeDv_Click(object sender, EventArgs e)
        {
            //07
            lblAdeeDV.Text = string.Empty;
            lblMensajeAdee.Text = string.Empty;


            if (ddlPais.SelectedValue == "0")
            {
                lblMensajeAdee.Text = "Debe seleccionar un País";
                return;
            }

            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoDvGr);
            if (codProcesado > 0)
            {
                BlDataMart objDataMart = new BlDataMart();
                objDataMart.InsertarLogCarga("TareaEnviarCorreos-Execute",
                                             "Inicio de tarea TareaEnviarCorreos a las Evaluadoras");
                string prefijoIsoPais = ddlPais.SelectedValue;
                string periodo = ddlPeriodo.SelectedItem.Text;
                IniciarProcesoNotificacionDirectoraVenta(prefijoIsoPais, periodo);
                lblAdeeDV.Text = "El correo ha sido enviado a la DV evaluadora";
            }
            else
            {
                lblMensajeAdee.Text = "No existe un inicio de dialogo de desempeñio de las DV a GR para el periodo seleccionado";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr08EnvCorAdeeGr_Click(object sender, EventArgs e)
        {
            //08
            lblAdeeGR.Text = string.Empty;
            lblMensajeAdee.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajeAdee.Text = "Debe seleccionar un País";
                return;
            }

            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(ddlPais.SelectedValue, ddlPeriodo.SelectedItem.Text, Constantes.IndicadorEvaluadoGrGz);
            if (codProcesado > 0)
            {
                BlDataMart objDataMart = new BlDataMart();
                objDataMart.InsertarLogCarga("IniciarProcesoNotificacionGerenteRegion",
                                             "Inicio de tarea TareaEnviarCorreos a las GR");
                string prefijoIsoPais = ddlPais.SelectedValue;
                string periodo = ddlPeriodo.SelectedItem.Text;
                IniciarProcesoNotificacionGerenteRegion(prefijoIsoPais, periodo);
                lblAdeeGR.Text = "El correo ha sido enviado a las GR evaluadoras";
            }
            else
            {
                lblMensajeAdee.Text = "No existe un inicio de dialogo de desempeñio de las GR a GZ para el periodo seleccionado";
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        protected void btnTrggr09EnvPlaAcorDiaGr_Click(object sender, EventArgs e)
        {
            //09
            lblMensajePlaAcorDia.Text = string.Empty;
            lblPlaAcorDiaGr.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajePlaAcorDia.Text = "Debe seleccionar un País";
                return;
            }

            var objConfig = new BlConfiguracion();
            var prefijoIsoPais = ddlPais.SelectedValue;
            var periodo = ddlPeriodo.SelectedItem.Text;
            const int idRol = 2;//5
            const string estadoProceso = "3";
            var dtUsuariosPa = objConfig.ObtenerCorreosPlanesAcordados(prefijoIsoPais, periodo, idRol, estadoProceso);

            EnviaCorreoPlanesAcordados(prefijoIsoPais, periodo, idRol, dtUsuariosPa);
            lblPlaAcorDiaGr.Text = "El correo ha sido enviado a las GRs evaluadas";

        }

        protected void btnTrggr10EnvPlaAcorDiaGz_Click(object sender, EventArgs e)
        {
            //10
            lblMensajePlaAcorDia.Text = string.Empty;
            lblPlaAcorDiaGz.Text = string.Empty;

            if (ddlPais.SelectedValue == "0")
            {
                lblMensajePlaAcorDia.Text = "Debe seleccionar un País";
                return;
            }
            var objConfig = new BlConfiguracion();
            var prefijoIsoPais = ddlPais.SelectedValue;
            var periodo = ddlPeriodo.SelectedItem.Text;
            const int idRol = 3;//6
            const string estadoProceso = "3";
            var dtUsuariosPa = objConfig.ObtenerCorreosPlanesAcordados(prefijoIsoPais, periodo, idRol, estadoProceso);

            EnviaCorreoPlanesAcordados(prefijoIsoPais, periodo, idRol, dtUsuariosPa);
            lblPlaAcorDiaGz.Text = "El correo ha sido enviado a las GZs evaluadas";
        }

        private void EnviaCorreoPlanesAcordados(string prefijoIsoPais, string periodo, int idRol, DataTable dtUsuariosPa)
        {
            string rolEvaluado = String.Empty;

            switch (idRol)
            {
                case Constantes.IdRolGerenteRegion:
                    rolEvaluado = Constantes.RolGerenteRegion.ToString();

                    break;
                case Constantes.IdRolGerenteZona:
                    rolEvaluado = Constantes.RolGerenteZona.ToString();
                    break;
            }

            if (dtUsuariosPa.Rows.Count <= 0) return;
            for (var x = 0; x < dtUsuariosPa.Rows.Count; x++)
            {
                var idProceso = dtUsuariosPa.Rows[x]["intIDProceso"].ToString();
                var codEvaluado = dtUsuariosPa.Rows[x]["chrCodigoUsuario"].ToString();
                var codEvaluador = dtUsuariosPa.Rows[x]["chrCodigoUsuarioEvaluador"].ToString();
                var correoPara = dtUsuariosPa.Rows[x]["vchCorreoElectronico"].ToString();
                var correoDe = ConfigurationManager.AppSettings["usuarioEnviaMails"];
                const string asunto = "Planes acordados en diálogos";
                var url = ConfigurationManager.AppSettings["URLdesempenio"] +
                          "Admin/Controls/Resumen_Durante_Despues.aspx";
                var cuerpo =
                    GetWebHtmlSourceCode(url +
                                         "?codPais=" + prefijoIsoPais + "&periodo=" + periodo + "&rolEvaluado=" + rolEvaluado + "&idProceso=" + idProceso + "&codEvaluado=" + codEvaluado + "&codEvaluador=" + codEvaluador);
                var smtp = ConfigurationManager.AppSettings["servidorSMTP"];
                //var smtp = "127.0.0.1";
                EnviarCorreo(correoPara, correoDe, asunto, cuerpo, smtp);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose();", true);
        }

        public static string GetWebHtmlSourceCode(string url)
        {
            var content = String.Empty;
            try
            {
                var objWebRequest = WebRequest.Create(@url);
                var objWebResponse = objWebRequest.GetResponse();
                var objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                content = objStreamReader.ReadToEnd();
                objStreamReader.Close();
            }
            catch
            {
                return content;
            }
            return content;
        }

        private void EnviarCorreo(string correoPara, string correoDe, string asunto, string cuerpo, string smtp)
        {
            var enviar = new SmtpClient(smtp);
            var correoFrom = new MailAddress(correoDe);
            var correoTo = new MailAddress(correoPara);
            var msjEmail = new MailMessage(correoFrom, correoTo) { Subject = asunto, IsBodyHtml = true, Body = cuerpo };
            enviar.Send(msjEmail);
        }

    }
}
