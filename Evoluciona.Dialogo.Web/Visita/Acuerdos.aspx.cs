
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Acuerdos : Page
    {
        protected BeResumenVisita objResumenVisita;
        public int indexMenuServer;
        public int indexSubMenu;
        public string mostrarPost;

        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);
            if (!Page.IsPostBack)
            {

                string connstring = string.Empty;
                if (Session["connApp"].ToString() == "")
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
                connstring = Session["connApp"].ToString();
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
                MostrarPostVisita();
                mostrarPost = "true";
                if (Constantes.EstadoVisitaActivo == objResumenVisita.estadoVisita)
                {
                    dlPreguntas1.CssClass = "checkDesabilitadoPostV";
                    mostrarPost = "false";
                }
                else if (Constantes.EstadoVisitaPostDialogo == objResumenVisita.estadoVisita)
                {
                    dlPreguntas1.CssClass = "checkDesabilitado";
                   
                }
                if (Session[Constantes.VisitaModoLectura] != null)
                {
                    dlPreguntas1.CssClass = "checkLectura";
                    btnGrabar.Text = "CONTINUAR";
                }
            }
        }
        
        private void cargarPreguntaAcuerdo()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();

            BlPreguntaAcuerdos daProceso = new BlPreguntaAcuerdos();

            dlPreguntas1.DataSource = daProceso.ObtenerPreguntaAcuerdo(connstring);
            dlPreguntas1.DataBind();

        }

        protected void dlPreguntas1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GrabarAcuerdo()
        {

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            bool inserto = false;
            BlAcuerdo daProceso = new BlAcuerdo();


            // bePlanAccion[] arreglo = new bePlanAccion[100];

            foreach (DataListItem Acuerdo in dlPreguntas1.Items)
            {
                int IDPregunta = Convert.ToInt32(((Label)Acuerdo.Controls[1]).Text);
                string Respuesta = ((TextBox)Acuerdo.Controls[5]).Text;
                //bool bitEvoluciona = ((RadioButtonList)Evoluciona.Controls[7]).SelectedItem.Value == "1";
                bool bitPostVisita = ((CheckBox)Acuerdo.Controls[9]).Checked;

                if (Respuesta.Trim() != "")
                {
                    //Grabar
                    BeAcuerdo beAcuerdo = new BeAcuerdo();

                    beAcuerdo.IDVisita = objResumenVisita.idVisita; 
                    beAcuerdo.IDPregunta = IDPregunta;
                    beAcuerdo.Respuesta = Respuesta;
                    beAcuerdo.PostVisita = bitPostVisita;
                    beAcuerdo.idUsuario = Convert.ToInt32(objResumenVisita.codigoUsuarioEvaluador);

                    inserto = daProceso.IngresarAcuerdo(connstring, beAcuerdo);

                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                GrabarAcuerdo();
                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo && objResumenVisita.porcentajeAvanceDurante == 40)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    objResumenVisita.porcentajeAvanceDurante = 60;
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 60, 2);
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                    
                }
                else if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo && objResumenVisita.porcentajeAvanceDespues == 0)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                        objResumenVisita.porcentajeAvanceDespues = 30;
                        blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 30, 3);
                        Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                }
                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaPostDialogo)
                {
                   
                    ClientScript.RegisterStartupScript(Page.GetType(), "_AcuerdosPV", "<script language='javascript'> javascript:AbrirMensajeDespues(); </script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_Acuerdos", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                }
            }
            else
            {
                if (indexMenuServer == 3)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_AcuerdosPV2", "<script language='javascript'> javascript:AbrirMensajeDespues(); </script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "_Acuerdos2", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                }

            }
           
            
        }

        private void MostrarPostVisita()
        {
            foreach (DataListItem Acuerdo in dlPreguntas1.Items)
            {
                CheckBox chkPost = (CheckBox)Acuerdo.Controls[9];

                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo)
                {
                    chkPost.Visible = false;
                }
                else if (indexMenuServer == 2)
                {
                    chkPost.Visible = false;
                }
                else
                {
                    chkPost.Visible = true;
                }
            }
        }
  
    }
}
