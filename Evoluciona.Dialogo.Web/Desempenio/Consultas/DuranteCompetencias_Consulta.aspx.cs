
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Evoluciona.Dialogo.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class DuranteCompetencias_Consulta : Page
    {
        #region Variables

        protected BeResumenProceso objResumen;
        string PeriodoEvaluacion;
        int idProceso;

        #endregion Variables

        #region Propiedades

        private string CadenaConexion
        {
            get
            {
                if (Session["connApp"] == null || string.IsNullOrEmpty(Session["connApp"].ToString()))
                    Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;

                return Session["connApp"].ToString();
            }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            BlResumenProceso objResumenBL = new BlResumenProceso();

            objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            PeriodoEvaluacion = Utils.QueryString("periodo");

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();
                BeResumenProceso objResumenNuevo = objResumenBL.ObtenerResumenProcesoByUsuario(
                    objResumen.codigoUsuario, objResumen.rolUsuario,
                    PeriodoEvaluacion, objResumen.prefijoIsoPais, tipoDialogoDesempenio);

                if (objResumenNuevo != null)
                    idProceso = objResumenNuevo.idProceso;
            }
            else
            {
                idProceso = int.Parse(txtIdProceso.Text);
            }

            if (IsPostBack) return;

            CargarCompetencia();

            if (ddlCompetencia.Items.Count != 0)
            {
                Session["idPlanAnual_Consulta"] = ddlCompetencia.SelectedValue;

                CargarRetroalimentcionGrabadas();

                objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
                BeRetroalimentacion beRetroalimentacion = new BeRetroalimentacion();
                beRetroalimentacion.CodigoPlanAnual = Convert.ToInt32(ddlCompetencia.SelectedValue);

                CargarZonaAlternativoPorRol(idProceso);
            }
        }

        protected void ddlCompetencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idAsignado = Session["idPlanAnual_Consulta"].ToString();
            Session["idPlanAnual_Consulta"] = ddlCompetencia.SelectedValue;

            if (ddlCompetencia.SelectedValue != "-1")
            {
                Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro_Consulta"];

                AsignarValores(idAsignado);

                if (valoresRetroAlimentacion == null || !valoresRetroAlimentacion.ContainsKey(ddlCompetencia.SelectedValue))
                    CargarRetroalimentcionGrabadas();
                else
                    CargarDatos(ddlCompetencia.SelectedValue);
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarZonaAlternativoPorRol(int idProceso)
        {
            BlDescripcionZonaAlternativa daAlternativa = new BlDescripcionZonaAlternativa();
            List<BeZonaAlternativa> listaAlternativasProcesadas = daAlternativa.ObtenerZonaAlternativaProcesada(CadenaConexion, idProceso);

            List<BeZonaAlternativa> listaAlternativas = daAlternativa.ObtenerZonaAlternativaPorRol(CadenaConexion, objResumen.codigoRolUsuario);
            List<BeZonaAlternativa> listaNodosPrincipales = listaAlternativas.FindAll(delegate(BeZonaAlternativa objFiltro) { return objFiltro.Nivel == 1; });
            if (listaNodosPrincipales != null)
            {
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
                        }
                    }

                    List<BeZonaAlternativa> listaNodosSecundarios = listaAlternativas.FindAll(delegate(BeZonaAlternativa objFiltro) { return objFiltro.Nivel == 2 && objFiltro.CodigoMaestro == objAlternativa.CodZonaAlternativa; });
                    if (listaNodosSecundarios != null)
                    {
                        foreach (BeZonaAlternativa objAlternativaNivel2 in listaNodosSecundarios)
                        {
                            TreeNode nodoSecundario = new TreeNode(objAlternativaNivel2.Alternativa, objAlternativaNivel2.CodZonaAlternativa.ToString());
                            BeZonaAlternativa objSeleccionadoNivel2 = listaAlternativasProcesadas.Find(delegate(BeZonaAlternativa objFiltroN2) { return objFiltroN2.CodZonaAlternativa == objAlternativaNivel2.CodZonaAlternativa; });
                            if (objSeleccionadoNivel2 != null && objSeleccionadoNivel2.Seleccionado.Trim() == "1")
                            {
                                nodoSecundario.Checked = true;
                            }
                            nodoSecundario.SelectAction = TreeNodeSelectAction.Expand;
                            nodoPrincipal.ChildNodes.Add(nodoSecundario);
                        }
                    }

                    TreeViewCheck.Nodes.Add(nodoPrincipal);
                }
            }
        }

        private void CargarDatos(string idAsignado)
        {
            Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro_Consulta"];

            List<DatosPreguntas> valores = valoresRetroAlimentacion[idAsignado];

            int index = 0;
            foreach (DataListItem item in dlPreguntas1.Items)
            {
                TextBox txtDato = (TextBox)item.Controls[5];
                txtDato.Text = valores[index++].Valor;
            }
        }

        private void AsignarValores(string idPregunta)
        {
            Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro_Consulta"];

            if (valoresRetroAlimentacion == null)
                valoresRetroAlimentacion = new Dictionary<string, List<DatosPreguntas>>();

            List<DatosPreguntas> valores = new List<DatosPreguntas>();

            foreach (DataListItem item in dlPreguntas1.Items)
            {
                Label lblPregunta = (Label)item.Controls[1];
                TextBox txtDato = (TextBox)item.Controls[5];

                DatosPreguntas nuevoDatoPregunta = new DatosPreguntas();
                nuevoDatoPregunta.Valor = txtDato.Text;
                nuevoDatoPregunta.IdPregunta = int.Parse(lblPregunta.Text);

                valores.Add(nuevoDatoPregunta);
            }

            if (valoresRetroAlimentacion.ContainsKey(idPregunta))
                valoresRetroAlimentacion[idPregunta] = valores;
            else
                valoresRetroAlimentacion.Add(idPregunta, valores);

            Session["valoresRetro_Consulta"] = valoresRetroAlimentacion;
        }

        private void CargarCompetencia()
        {
            BeResumenProceso beReseumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlRetroalimentacion daProceso = new BlRetroalimentacion();

            String anio = PeriodoEvaluacion.Substring(0, 4);

            DataTable dtCompetencia = daProceso.CargarCompetencia(CadenaConexion, beReseumen, anio);

            if (dtCompetencia.Rows.Count == 0)
            {
                lblEtiqueta.Visible = false;
                ddlCompetencia.Visible = false;
            }

            ddlCompetencia.DataSource = dtCompetencia;
            ddlCompetencia.DataTextField = "Competencia";
            ddlCompetencia.DataValueField = "CodigoPlanAnual";
            ddlCompetencia.DataBind();
        }

        private void CargarRetroalimentcionGrabadas()
        {
            BlRetroalimentacion daProceso = new BlRetroalimentacion();
            objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BeRetroalimentacion beRetroalimentacion = new BeRetroalimentacion();
            beRetroalimentacion.idProceso = idProceso;

            beRetroalimentacion.CodigoPlanAnual = Convert.ToInt32(ddlCompetencia.SelectedValue);

            DataTable resultadoPreguntas = daProceso.ListarRetroalimentacion(CadenaConexion, beRetroalimentacion);

            dlPreguntas1.DataSource = resultadoPreguntas;
            dlPreguntas1.DataBind();
        }

        #endregion Metodos
    }
}