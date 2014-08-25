
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

    public partial class IteraccionVisita : Page
    {
        protected BeResumenVisita objResumenVisita;
        public int indexMenuServer;
        public int indexSubMenu;
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
                connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

                BlIteraccionVisita daProceso = new BlIteraccionVisita();

                DataTable dtIteraccionVisitaGrabadas = new DataTable();

                //***************** codigo de la visita por duro 
                // directora Ventas visita 1  -  Gerente de Reginon Visita 2
                dtIteraccionVisitaGrabadas = daProceso.ObtenerPreguntasGrabadas(connstring,objResumenVisita.idVisita);

                if (dtIteraccionVisitaGrabadas.Rows.Count > 0)
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
                    Button1.Text = "CONTINUAR";
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                GrabarIteraccionVisita();
                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo && objResumenVisita.porcentajeAvanceDurante == 20)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 40, 2);
                    objResumenVisita.porcentajeAvanceDurante = 40;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                }
            }
            
            ClientScript.RegisterStartupScript(Page.GetType(), "_IteraccionVisita", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
        }


        private void GrabarIteraccionVisita()
        {

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            bool inserto = false;
            BlIteraccionVisita daProceso = new BlIteraccionVisita();

            foreach (DataListItem IteraccionVisita in dlPreguntas1.Items)
            {
                int CodigoPlanAnual = Convert.ToInt32(((Label)IteraccionVisita.Controls[1]).Text);
                string Observacion = ((TextBox)IteraccionVisita.Controls[11]).Text;
                //bool bitEvoluciona = ((RadioButtonList)Evoluciona.Controls[7]).SelectedItem.Value == "1";
                bool bitOservacion = ((RadioButton)IteraccionVisita.Controls[7]).Checked;

                if (Observacion.Trim() != "")
                {
                    //Grabar
                    BeIteraccionVisita beIteraccionVisita = new BeIteraccionVisita();

                    beIteraccionVisita.IDVisita = objResumenVisita.idVisita; // ********** MODIFICAR ID Visita 2
                    beIteraccionVisita.CodigoPlanAnual = CodigoPlanAnual;
                    beIteraccionVisita.Observacion = Observacion;
                    beIteraccionVisita.bitOservacion = bitOservacion;
                    beIteraccionVisita.idUsuario = Convert.ToInt32(objResumenVisita.codigoUsuarioEvaluador);//objUsuario.idUsuario;
                    
                    inserto = daProceso.IngresarIteraccionVisita(connstring, beIteraccionVisita);

                }

            }

        }

    }
}
