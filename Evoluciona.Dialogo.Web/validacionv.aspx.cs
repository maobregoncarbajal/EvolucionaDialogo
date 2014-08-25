
namespace Evoluciona.Dialogo.Web
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI;

    public partial class validacionv : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string connstring = null;
                if (Session["connApp"] != null)
                    connstring = Session["connApp"].ToString();
                Session.RemoveAll();
                if (connstring != null)
                    Session["connApp"] = connstring;
                Session[Constantes.ObjUsuarioLogeado] = null;
                //ValidarUsuarioPrueba();
                ValidarUsuario();
            }
        }

        private void ValidarUsuario()
        {
            string connstring = null;
            if (Session["connApp"] != null)
                connstring = Session["connApp"].ToString();
            Session.RemoveAll();
            if (connstring != null)
                Session["connApp"] = connstring;

            BeUsuario objUsuario = null;
            //string paramLogeo = dtUsuarios.Rows[x]["documentoIdentidad"].ToString() + "-" + ddlPais.SelectedValue + "-" + ddlPeriodo.SelectedItem.Text + "-" + codigoRol;
            string urlInicio = "Visita/resumenVisita.aspx";
            //if (Request["app"] != null)
            //{
            //    urlInicio = "Visita/resumenVisita.aspx";
            //}

            if (Request["sson"] != null)
            {
                string archivo = Server.MapPath("validacionv.aspx").Replace("validacionv.aspx", "KeyPrivadaDDesempenio.xml");
                Encriptacion objEncriptar = new Encriptacion();
                string parametroDesEncriptado = objEncriptar.Desencriptar(Request["sson"], archivo);
                string[] arrParametros = parametroDesEncriptado.Split('|');
                if (arrParametros.Length < 3)
                {
                    Response.Redirect("error.aspx?mensaje=Usuario no registrado");
                }
                string codigoUsuario = arrParametros[0];
                string prefijoPais = arrParametros[1];
                //string periodo = arrParametros[2];
                string codigoRol = arrParametros[3];
                //string IdUsuarioTK = arrParametros[4];
                BlUsuario obBLUsuario = new BlUsuario();
                objUsuario = obBLUsuario.ObtenerDatosUsuario(prefijoPais, Convert.ToInt32(codigoRol), codigoUsuario, Constantes.EstadoActivo);
                if (objUsuario == null)
                {
                    Response.Redirect("error.aspx?mensaje=NoRegistrado");
                }

                objUsuario.prefijoIsoPais = prefijoPais;
                objUsuario.codigoRol = Convert.ToInt32(codigoRol);
                //objUsuario.IdUsuarioTK = Convert.ToString(IdUsuarioTK);


                BlConfiguracion objConfig = new BlConfiguracion();
                DataTable dtperiodo = objConfig.SeleccionarPeriodo(prefijoPais);
                if (dtperiodo.Rows.Count > 0)
                {
                    objUsuario.periodoEvaluacion = dtperiodo.Rows[0]["chrPeriodo"].ToString();
                    Session["periodoActual"] = objUsuario.periodoEvaluacion;
                }
                else
                {
                    objUsuario.periodoEvaluacion = "";
                }

                if (!ValidarInicioConfiguracion(objUsuario))
                {
                    //lblMensajes.Text = "No han Iniciado el Proceso de Dialogo de Desempeñio para este Periodo";
                    Response.Redirect("error.aspx?mensaje=DialogoNoIni");
                }
                Session[Constantes.ObjUsuarioLogeado] = objUsuario;
                ObtenerMenu(objUsuario.idRol);
                FormsAuthentication.SetAuthCookie(objUsuario.codigoUsuario, false);
                Response.Redirect(urlInicio);
            }
            else
            {
                ValidarUsuarioPorURL(urlInicio);
            }
        }

        private void ValidarUsuarioPorURL(string urlInicio)
        {
            string connstring = null;
            if (Session["connApp"] != null)
                connstring = Session["connApp"].ToString();
            Session.RemoveAll();
            if (connstring != null)
                Session["connApp"] = connstring;
            
            //string paramLogeo = dtUsuarios.Rows[x]["documentoIdentidad"].ToString() + "-" + ddlPais.SelectedValue + "-" + ddlPeriodo.SelectedItem.Text + "-" + codigoRol;

            string idUsuarioTk = Request["IdUsuario"];

            if (Request["codigoUsuario"] != null)
            {
                BlUsuario obBLUsuario = new BlUsuario();
                BeUsuario objUsuario = obBLUsuario.ObtenerDatosUsuario(Request["prefijoIsoPais"], Convert.ToInt32(Request["codigoRol"]), Request["codigoUsuario"], Constantes.EstadoActivo);
                if (objUsuario == null)
                {
                    Response.Redirect("UsuarioNoRegistrado.aspx?mensaje=NoRegistrado&IdUsuarioTK=" + idUsuarioTk + "", true);
                }

                objUsuario.prefijoIsoPais = Request["prefijoIsoPais"];
                objUsuario.codigoRol = Convert.ToInt32(Request["codigoRol"]);
                objUsuario.IdUsuarioTK = idUsuarioTk;

                BlConfiguracion objConfig = new BlConfiguracion();
                DataTable dtperiodo = objConfig.SeleccionarPeriodo(Request["prefijoIsoPais"]);
                if (dtperiodo.Rows.Count > 0)
                {
                    objUsuario.periodoEvaluacion = dtperiodo.Rows[0]["chrPeriodo"].ToString();
                    Session["periodoActual"] = objUsuario.periodoEvaluacion;
                }
                else
                {
                    objUsuario.periodoEvaluacion = "";
                }

                if (!ValidarInicioConfiguracion(objUsuario))
                {
                    if (!String.IsNullOrEmpty(idUsuarioTk))
                    {
                        Response.Redirect("UsuarioNoRegistrado.aspx?mensaje=DialogoNoIni&IdUsuarioTK=" + idUsuarioTk + "", true);
                    }
                    else
                    {
                        Response.Redirect("error.aspx?mensaje=DialogoNoIni", true);
                    }
                }
                Session[Constantes.ObjUsuarioLogeado] = objUsuario;
                ObtenerMenu(objUsuario.idRol);
                FormsAuthentication.SetAuthCookie(objUsuario.codigoUsuario, false);
                Response.Redirect(urlInicio);
            }
            else
            {
                Response.Redirect("error.aspx?mensaje=sesion");
            }
        }

        private bool ValidarInicioConfiguracion(BeUsuario objUsuario)
        {
            bool inicioRealizado = false;
            BlConfiguracion objConfig = new BlConfiguracion();
            int codProcesado = objConfig.ValidarInicioProceso(objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.IndicadorEvaluadoDvGr);
            if (codProcesado > 0)
            {
                inicioRealizado = true;
            }
            return inicioRealizado;
        }

        private void ObtenerMenu(int idRol)
        {
            BlMenu objMenu = new BlMenu();
            DataTable dt = new DataTable();
            dt = objMenu.ObtenerMenu(null);
            //if (Request["app"] != null)
            //{
            //    dt = objMenu.ObtenerMenu(null);
            //}
            //else
            //{
            //    dt = objMenu.ObtenerMenuPrincipal(idRol, constantes.estadoActivo);
            //}
            //DataTable dt = objMenu.ObtenerMenuPrincipal(idRol, constantes.estadoActivo);

            Session["MenuPrincipal"] = dt;
        }
    }
}
