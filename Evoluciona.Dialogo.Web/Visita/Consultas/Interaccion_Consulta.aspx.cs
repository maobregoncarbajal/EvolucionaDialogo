
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using WsPlanDesarrollo;

    public partial class Interaccion_Consulta : Page
    {
        #region Variables

        public int esCorrecto = 0;
        protected BeUsuario objUsuario;
        protected BeResumenVisita objResumenVisita;

        #endregion Variables

        #region Propiedades

        public string CadenaConexion
        {
            get
            {
                string connstring = string.Empty;
                if (string.IsNullOrEmpty(Session["connApp"].ToString()))
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
                connstring = Session["connApp"].ToString();
                return connstring;
            }
        }

        #endregion Propiedades

        #region Eventos

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
                ClientScript.RegisterStartupScript(Page.GetType(), "_InteraccionConsulta", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }

            if (Page.IsPostBack)
                return;

            // si existe informacion me muestre para actualizar
            BlInteraccion daProceso = new BlInteraccion();

            // carga las alternativas
            CargarZonaAlternativoPorRol(objResumenVisita.idProceso);

            // carga resumen de competencias
            CargarMedicionCompetencias();

            //Obtiene los datos grabados
            DataTable dtGrabadas = daProceso.ObtenerInteraccionGrabadas(CadenaConexion, objResumenVisita.idVisita);

            if (dtGrabadas.Rows.Count > 0)
            {
                txtObjetivosVisita.Text = dtGrabadas.Rows[0]["vchObjetivoVisita"].ToString();
                txtOtros.Text = dtGrabadas.Rows[0]["vchOtrasAlternativas"].ToString();
            }

            HabilitarControles(false);
        }

        #endregion Eventos

        #region Metodos

        private void CargarZonaAlternativoPorRol(int idProceso)
        {
            BlDescripcionZonaAlternativa daAlternativa = new BlDescripcionZonaAlternativa();
            List<BeZonaAlternativa> listaAlternativasProcesadasDialogo = daAlternativa.ObtenerZonaAlternativaProcesadaVisita(CadenaConexion, idProceso);

            if (listaAlternativasProcesadasDialogo.Count > 0)
                hidPlanAnual.Value = listaAlternativasProcesadasDialogo[0].CodigoPlanAnual.ToString();
            else
                return;

            List<BeZonaAlternativa> listaAlternativasProcesadas = daAlternativa.ObtenerZonaAlternativaVisita(CadenaConexion, idProceso);
            List<BeZonaAlternativa> listaAlternativas = daAlternativa.ObtenerZonaAlternativaPorRol(CadenaConexion, objResumenVisita.codigoRolUsuario);

            List<BeZonaAlternativa> listaNodosPrincipales = listaAlternativas.FindAll(delegate(BeZonaAlternativa objFiltro) { return objFiltro.Nivel == 1; });
            foreach (BeZonaAlternativa objAlternativa in listaNodosPrincipales)
            {
                TreeNode nodoPrincipal = new TreeNode(objAlternativa.Alternativa, objAlternativa.CodZonaAlternativa.ToString());
                nodoPrincipal.SelectAction = TreeNodeSelectAction.Expand;

                BeZonaAlternativa objSeleccionado = listaAlternativasProcesadas.Find(delegate(BeZonaAlternativa objFiltro) { return objFiltro.CodZonaAlternativa == objAlternativa.CodZonaAlternativa; });
                if (objSeleccionado != null && objSeleccionado.Seleccionado.Trim() == "1")
                {
                    nodoPrincipal.Checked = true;
                    if (nodoPrincipal.Text.Trim().ToUpper() == "OTRO")
                    {
                        txtOtros.Text = objSeleccionado.AlternativaOtro;
                        txtOtros.Enabled = true;
                    }
                }

                List<BeZonaAlternativa> listaNodosSecundarios = listaAlternativas.FindAll(delegate(BeZonaAlternativa objFiltro) { return objFiltro.Nivel == 2 && objFiltro.CodigoMaestro == objAlternativa.CodZonaAlternativa; });
                foreach (BeZonaAlternativa objAlternativaNivel2 in listaNodosSecundarios)
                {
                    TreeNode nodoSecundario = new TreeNode(objAlternativaNivel2.Alternativa, objAlternativaNivel2.CodZonaAlternativa.ToString());
                    nodoSecundario.SelectAction = TreeNodeSelectAction.Expand;

                    BeZonaAlternativa objSeleccionadoNivel2 = listaAlternativasProcesadas.Find(delegate(BeZonaAlternativa objFiltroN2) { return objFiltroN2.CodZonaAlternativa == objAlternativaNivel2.CodZonaAlternativa; });
                    if (objSeleccionadoNivel2 != null && objSeleccionadoNivel2.Seleccionado.Trim() == "1")
                    {
                        nodoSecundario.Checked = true;
                    }
                    nodoPrincipal.ChildNodes.Add(nodoSecundario);
                }
                TreeViewCheck.Nodes.Add(nodoPrincipal);
            }

            TreeViewCheck.CssClass = "checkdesabilitado";
        }

        private void CargarMedicionCompetencias()
        {
            BlMedicionCompetencia daProceso = new BlMedicionCompetencia();
            DataTable dtTemporal = daProceso.ObtenerMedicionCompetencia(CadenaConexion, objResumenVisita.idProceso);

            ConsultaWebServices(dtTemporal);
        }

        private void ConsultaWebServices(DataTable dtPlanAnual)
        {
            BlPlanAnual daProceso = new BlPlanAnual();

            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, objResumenVisita.prefijoIsoPais);
            int anio = Convert.ToInt32(objUsuario.periodoEvaluacion.Substring(0, 4));
            string documentoIdentidad = objResumenVisita.codigoUsuario;
            string cub = objResumenVisita.cub;

            WsInterfaceFFVVSoapClient wsPlanAnual = new WsInterfaceFFVVSoapClient();

            try
            {
                documentoIdentidad = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                             ? Convert.ToInt32(documentoIdentidad).ToString()
                                             : documentoIdentidad.Trim();

                //DataSet dsPlanAnual = wsPlanAnual.ConsultaPorcentajeAvanceCompetencia(anio, codigoPaisAdam, documentoIdentidad);
                DataSet dsPlanAnual = wsPlanAnual.ConsultaPorcentajeAvanceCompetencia(anio, cub);

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

                        grvMedicionCompetencia.DataSource = dtCompetencia;
                        grvMedicionCompetencia.DataBind();
                        grvMedicionCompetencia.CssClass = "checkdesabilitado";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "_msj2", "<script language='javascript'> javascript:AbrirMensajeError(); </script>");
                    }
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_msj1", "<script language='javascript'> javascript:AbrirMensajeError(); </script>");
            }
        }

        protected string GetDescripcion(object porcentaje)
        {
            int totalPorcentaje = Convert.ToInt32(porcentaje);
            string strHtml = null;

            if (totalPorcentaje >= 0 && totalPorcentaje <= 20)
            {
                strHtml = "<img src='../../Images/CompeRojo.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 21 && totalPorcentaje <= 40)
            {
                strHtml = "<img src='../../Images/CompeRosado.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 41 && totalPorcentaje <= 60)
            {
                strHtml = "<img src='../../Images/CompeAmarillo.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 61 && totalPorcentaje <= 80)
            {
                strHtml = "<img src='../../Images/CompeVerde_claro.jpg' alt=''/>";
            }
            else if (totalPorcentaje >= 81 && totalPorcentaje <= 100)
            {
                strHtml = "<img src='../../Images/CompeVerde_oscuro.jpg' alt=''/>";
            }

            return strHtml;
        }

        private void HabilitarControles(bool p)
        {
            txtObjetivosVisita.Enabled = p;
            txtOtros.Enabled = p;
        }

        #endregion Metodos
    }
}