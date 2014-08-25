
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;

    public partial class IteraccionVisita_Consulta : Page
    {
        protected BeResumenVisita objResumenVisita;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];

            string periodo = Request["periodo"];

            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerCodigoVisita(objResumenVisita.codigoUsuario, objResumenVisita.codigoUsuarioEvaluador, objResumenVisita.idRolUsuario, periodo);
            if (dtVisita.Rows.Count > 0)
            {
                objResumenVisita.idVisita = Convert.ToInt32(dtVisita.Rows[0]["codigoVisita"]);
                objResumenVisita.estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                objResumenVisita.idProceso = Convert.ToInt32(dtVisita.Rows[0]["intIDProceso"]);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_accionesConsulta", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }
          
            if (!Page.IsPostBack)
            {
                string connstring = string.Empty;
                if (Session["connApp"].ToString() == "")
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
                connstring = Session["connApp"].ToString();

                BlIteraccionVisita daProceso = new BlIteraccionVisita();

                DataTable dtIteraccionVisitaGrabadas = new DataTable();

                //***************** codigo de la visita por duro 
                // directora Ventas visita 1  -  Gerente de Reginon Visita 2
                dtIteraccionVisitaGrabadas = daProceso.ObtenerPreguntasGrabadas(connstring,objResumenVisita.idVisita);

                if (dtIteraccionVisitaGrabadas.Rows.Count != 0)
                {
                    dlPreguntas1.DataSource = dtIteraccionVisitaGrabadas;
                    dlPreguntas1.DataBind();
                }
                else
                {
                    cargarDatos();
                }

                if (Constantes.EstadoVisitaActivo == objResumenVisita.estadoVisita)
                {
                    dlPreguntas1.CssClass = "checkHabilitado";
                }
                else
                {
                    dlPreguntas1.CssClass = "checkDesabilitado";
                }
                if (Session[Constantes.VisitaModoLectura] != null)
                {
                    dlPreguntas1.CssClass = "checkDesabilitado";

                    //Button1.Text = "CONTINUAR";
                }
            }
        }
                 
        private void cargarDatos()
        {
            BlIteraccionVisita daProceso = new BlIteraccionVisita();

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            DataTable dtTemporal = new DataTable();
 
            dtTemporal = daProceso.ObtenerPreguntas(connstring, objResumenVisita.idProceso);

            dlPreguntas1.DataSource = dtTemporal;
            dlPreguntas1.DataBind();
        }
    }
}
