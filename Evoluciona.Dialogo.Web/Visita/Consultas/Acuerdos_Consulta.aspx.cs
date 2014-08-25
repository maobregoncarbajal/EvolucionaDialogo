
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;

    public partial class Acuerdos_Consulta : Page
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
                connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
                BlAcuerdo daProceso = new BlAcuerdo();
                DataTable dtAcuerdoGrabadas = new DataTable();
            
                //Devuelve los acuerdos grabados segun el codigo de la visita
                dtAcuerdoGrabadas = daProceso.ObtenerAcuerdoGrabadas(connstring, objResumenVisita.idVisita);

                if (dtAcuerdoGrabadas.Rows.Count != 0)
                {
                    dlPreguntas1.DataSource = dtAcuerdoGrabadas;
                    dlPreguntas1.DataBind();
                }
                else
                {
                    cargarPreguntaAcuerdo();
                }

                //Habilitar Controles
                if (Constantes.EstadoVisitaActivo == objResumenVisita.estadoVisita)
                {
                    dlPreguntas1.CssClass = "checkDesabilitadoPostV";
                }
                else if (Constantes.EstadoVisitaPostDialogo == objResumenVisita.estadoVisita)
                {
                    dlPreguntas1.CssClass = "checkDesabilitado";
                }
                if (Session[Constantes.VisitaModoLectura] != null)
                {
                    dlPreguntas1.CssClass = "checkLectura";
                }
            }
        }
        
        private void cargarPreguntaAcuerdo()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            BlPreguntaAcuerdos daProceso = new BlPreguntaAcuerdos();

            dlPreguntas1.DataSource = daProceso.ObtenerPreguntaAcuerdo(connstring);
            dlPreguntas1.DataBind();
        }

        protected void dlPreguntas1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
