
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;

    public partial class VisitaEvoluciona_Consulta : Page
    {
        protected BeResumenVisita objResumenVisita;

        protected BeUsuario objUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            string periodo = Request["periodo"];

            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerCodigoVisita(objResumenVisita.codigoUsuario, objUsuario.codigoUsuario, objResumenVisita.idRolUsuario, periodo);
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

                BlEvoluciona daProceso = new BlEvoluciona();

                DataTable dtEvalucionGrabadas = new DataTable();

                //***************** codigo de la visita por duro 
                // directora Ventas visita 1  -  Gerente de Reginon Visita 2
                dtEvalucionGrabadas = daProceso.ObtenerEvaluacionGrabadas(connstring, objResumenVisita.idVisita, objUsuario.codigoRol);

                if (dtEvalucionGrabadas.Rows.Count != 0)
                {
                    dlPreguntas1.DataSource = dtEvalucionGrabadas;
                    dlPreguntas1.DataBind();
                }
                else
                {
                    cargarPreguntas();
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
                }
            }
        }


        // Cargar Preguntas
        private void cargarPreguntas()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            BlVisitaEvoluciona daProceso = new BlVisitaEvoluciona();

            //**********  CODIGO DEL ROL EN DURO

            DataTable dtPreguntas = new DataTable();
            //-4 directora Ventas   -5 Gerente REgion
            dtPreguntas = daProceso.ObtenerPreguntasXRol(connstring, objUsuario.codigoRol);
            dlPreguntas1.DataSource = dtPreguntas;
            dlPreguntas1.DataBind();
        }
    }
}
