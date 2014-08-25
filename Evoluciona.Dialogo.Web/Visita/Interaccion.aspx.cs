
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

    public partial class Interaccion : Page
    {
        #region Variables

        public int indexMenuServer = 1;
        public int indexSubMenu = 1;
        public int esCorrecto = 0;
        protected BeResumenVisita objResumenVisita;
        protected BeUsuario objUsuario;

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
            if (Constantes.EstadoVisitaActivo == objResumenVisita.estadoVisita)
            {
                HabilitarControles(true);
            }
            else
            {
                HabilitarControles(false);
            }
            if (Session[Constantes.VisitaModoLectura] != null)
            {
                HabilitarControles(false);
                btnGrabar.Text = "CONTINUAR";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            bool correcto = false;

            if (Session[Constantes.VisitaModoLectura] == null)
            {
                correcto &= GrabarZonasAlternativas();
                correcto &= GrabarInteracion();

                if (objResumenVisita.estadoVisita == Constantes.EstadoVisitaActivo &&
                    objResumenVisita.porcentajeAvanceAntes == 0)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objResumenVisita.idVisita, 40, 1);
                    objResumenVisita.porcentajeAvanceAntes = 40;
                    Session[Constantes.ObjUsuarioVisitado] = objResumenVisita;
                }
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "_Iteraccion", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
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
                        lblMensajes.Text = "No se encontraron registros de Compentencias.";
                        ClientScript.RegisterStartupScript(Page.GetType(), "_msj2", "<script language='javascript'> javascript:AbrirMensajeError(); </script>");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Se produjo un error: " + ex.Message;
                ClientScript.RegisterStartupScript(Page.GetType(), "_msj1", "<script language='javascript'> javascript:AbrirMensajeError(); </script>");
            }
        }

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

        private bool GrabarZonasAlternativas()
        {
            bool correcto = true;
            BeZonaAlternativa beZonaAlternativa = new BeZonaAlternativa();

            try
            {
                BlZonaAlternativa daProceso = new BlZonaAlternativa();

                beZonaAlternativa.IDProceso = objResumenVisita.idProceso;
                beZonaAlternativa.CodigoPlanAnual = Convert.ToInt32(hidPlanAnual.Value);

                foreach (TreeNode nodo in TreeViewCheck.Nodes)
                {
                    beZonaAlternativa.CodZonaAlternativa = Convert.ToInt32(nodo.Value);
                    beZonaAlternativa.AlternativaOtro = string.Empty;

                    if (nodo.Checked)
                    {
                        beZonaAlternativa.Seleccionado = "1";
                        if (nodo.Text.Trim().ToUpper() == "OTRO")
                        {
                            beZonaAlternativa.AlternativaOtro = txtOtros.Text.Trim();
                        }
                    }
                    else
                    {
                        beZonaAlternativa.Seleccionado = "0";
                    }

                    correcto &= daProceso.IngresarZonaAlternativaVisita(CadenaConexion, beZonaAlternativa, objUsuario);

                    if (nodo.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode nodoSec in nodo.ChildNodes)
                        {
                            beZonaAlternativa.CodZonaAlternativa = Convert.ToInt32(nodoSec.Value);
                            beZonaAlternativa.AlternativaOtro = string.Empty;

                            if (nodoSec.Checked)
                            {
                                beZonaAlternativa.Seleccionado = "1";
                            }
                            else
                            {
                                beZonaAlternativa.Seleccionado = "0";
                            }

                            correcto &= daProceso.IngresarZonaAlternativaVisita(CadenaConexion, beZonaAlternativa, objUsuario);
                        }
                    }
                }

                return correcto;
            }
            catch
            {
                return false;
            }
        }

        private bool GrabarInteracion()
        {
            bool inserto = false;
            BlInteraccion daProceso = new BlInteraccion();
            BeInteraccion beInteraccion = new BeInteraccion();

            beInteraccion.IDVisita = objResumenVisita.idVisita;
            beInteraccion.ObjetivoVisita = txtObjetivosVisita.Text;
            beInteraccion.OtrasAlternativas = txtOtros.Text;
            beInteraccion.idUsuario = Convert.ToInt32(objResumenVisita.codigoUsuarioEvaluador);

            if (txtObjetivosVisita.Text != "")
            {
                inserto = daProceso.IngresarInteraccion(CadenaConexion, beInteraccion);
            }

            return inserto;
        }

        private void HabilitarControles(bool p)
        {
            txtObjetivosVisita.Enabled = p;
            txtOtros.Enabled = p;
            TreeViewCheck.CssClass = p ? string.Empty : "checkdesabilitado";
        }

        #endregion Metodos
    }
}