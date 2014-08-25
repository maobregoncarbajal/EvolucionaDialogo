
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using WsPlanDesarrollo;

    public partial class MedicionCompetencia : Page
    {
        protected BeResumenVisita objResumenVisita;
        protected BeUsuario objUsuario;
        public int indexMenuServer;
        public int indexSubMenu;

        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);
            if (!Page.IsPostBack)
            {
                cargarGrilla();
                if (Session[Constantes.VisitaModoLectura] != null)
                {
                    btnGrabar.Text = "CONTINUAR";
                }
            }
        }

        private void cargarGrilla()
        {
            BlMedicionCompetencia daProceso = new BlMedicionCompetencia();

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            DataTable dtTemporal = new DataTable();
            //le el IDProceso por duro 3
            dtTemporal = daProceso.ObtenerMedicionCompetencia(connstring, objResumenVisita.idProceso);

            ConsultaWebServices(dtTemporal);
        }

        private void ConsultaWebServices(DataTable dtPlanAnual)
        {
            BlPlanAnual daProceso = new BlPlanAnual();

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            string codigoPaisAdam = daProceso.ObtenerPaisAdam(connstring, objUsuario.prefijoIsoPais);
            int anio = Convert.ToInt32(objUsuario.periodoEvaluacion.Substring(0, 4)); //2011;// objResumen.periodo.Substring(0, 4);
            string documentoIdentidad = objResumenVisita.codigoUsuario;//"6100139838" ysell Coromoto;
            string cub = objResumenVisita.cub;

            WsInterfaceFFVVSoapClient wsPlanAnual = new WsInterfaceFFVVSoapClient();

            try
            {
                documentoIdentidad = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                                ? Convert.ToInt32(documentoIdentidad).ToString()
                                                : documentoIdentidad.Trim();

                //DataSet dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, codigoPaisAdam, documentoIdentidad);
                DataSet dsPlanAnual = wsPlanAnual.ConsultaPlanDesarrollo(anio, cub);

                if (dsPlanAnual != null)
                {
                    if (dsPlanAnual.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtCompetencia = new DataTable();
                        dtCompetencia.Columns.Add("intCodigoCompetencia");
                        dtCompetencia.Columns.Add("vchCompetencia");
                        dtCompetencia.Columns.Add("PorcentajeAvance");
                        dtCompetencia.Columns.Add("Enfoque");
                        foreach (DataRow dr in dsPlanAnual.Tables[0].Rows)
                        {
                            DataRow drCompetencia = dtCompetencia.NewRow();
                            drCompetencia["intCodigoCompetencia"] = dr["CodigoCompetencia"].ToString();
                            drCompetencia["vchCompetencia"] = dr["DescripcionCompetencia"].ToString();
                            drCompetencia["PorcentajeAvance"] = dr["PorcentajeAvance"].ToString();
                            string esCompetencia = "false";
                            for (int i = 0; i < dtPlanAnual.Rows.Count; i++)
                            {
                                if (dr["CodigoCompetencia"].ToString() == dtPlanAnual.Rows[i]["intCodigoCompetencia"].ToString())
                                {
                                    esCompetencia = "true";
                                }
                            }
                            drCompetencia["Enfoque"] = esCompetencia;
                            dtCompetencia.Rows.Add(drCompetencia);
                        }
                        //llenar la grila con dtCompetencia
                        grvMedicionCompetencia.DataSource = dtCompetencia;
                        grvMedicionCompetencia.DataBind();

                        // TreeViewCheck estilo desabilitado
                        grvMedicionCompetencia.CssClass = "checkdesabilitado";
                    }

                    else
                    {
                        lblMensajes.Text = "No Tiene Informacion registrada";
                        ClientScript.RegisterStartupScript(Page.GetType(), "_msj2", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                    }
                }
            }
            catch
            {
                lblMensajes.Text = "El servicio no se encuentra disponible, por favor intente luego";
                ClientScript.RegisterStartupScript(Page.GetType(), "_msj1", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
            }
        }

        // Metodo que carga la imagen de las Grillas

        protected string GetDescripcion(object porcentaje)
        {
            int totalPorcentaje = Convert.ToInt32(porcentaje);
            string strHtml = null;

            if (totalPorcentaje >= 0 && totalPorcentaje <= 20)
            {
                strHtml = "<img src='../Images/CompeRojo.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 21 && totalPorcentaje <= 40)
            {
                strHtml = "<img src='../Images/CompeRosado.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 41 && totalPorcentaje <= 60)
            {
                strHtml = "<img src='../Images/CompeAmarillo.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 61 && totalPorcentaje <= 80)
            {
                strHtml = "<img src='../Images/CompeVerde_claro.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 81 && totalPorcentaje <= 100)
            {
                strHtml = "<img src='../Images/CompeVerde_oscuro.jpg' alt=''/>";
            }

            return strHtml;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo && objResumenVisita.porcentajeAvanceAntes == 20)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 40, 1);
                    objResumenVisita.porcentajeAvanceAntes = 40;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                }
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "_competencia", "<script language='javascript'> javascript:AbrirNavegacion(); </script>");
        }
    }
}