
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

    public partial class VisitaEvoluciona : Page
    {
        protected BeResumenVisita objResumenVisita;
        public int indexMenuServer;
        public int indexSubMenu;
        protected BeUsuario objUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
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
                    btnGrabar.Text = "CONTINUAR";
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

       

        private void GrabarEvoluciona()
        {

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            bool inserto = false;
            BlEvoluciona daProceso = new BlEvoluciona();


            // bePlanAccion[] arreglo = new bePlanAccion[100];

            foreach (DataListItem Evoluciona in dlPreguntas1.Items)
            {
                int IDPregunta = Convert.ToInt32(((Label)Evoluciona.Controls[1]).Text);
                string Respuesta = ((TextBox)Evoluciona.Controls[9]).Text;
                //bool bitEvoluciona = ((RadioButtonList)Evoluciona.Controls[7]).SelectedItem.Value == "1";
                bool bitEvoluciona = ((RadioButton)Evoluciona.Controls[5]).Checked;

                if (Respuesta.Trim() != "")
                {
                    //Grabar
                    BeEvoluciona beEvoluciona = new BeEvoluciona();

                    beEvoluciona.IDVisita = objResumenVisita.idVisita; // ********** MODIFICAR
                    beEvoluciona.IDPregunta = IDPregunta;
                    beEvoluciona.Respuesta = Respuesta;
                    beEvoluciona.Evoluciona = bitEvoluciona;
                    beEvoluciona.idUsuario = Convert.ToInt32(objResumenVisita.codigoUsuarioEvaluador);


                    inserto = daProceso.IngresarVisitaEvoluciona(connstring, beEvoluciona);

                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                GrabarEvoluciona();
                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo && objResumenVisita.porcentajeAvanceDurante == 0)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 20, 2);
                    objResumenVisita.porcentajeAvanceDurante = 20;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                }
            }
            
            ClientScript.RegisterStartupScript(Page.GetType(), "_VisitaEvoluciona", "<script language='javascript'> javascript:AbrirMensaje(); </script>");

        }

      



    }
}
