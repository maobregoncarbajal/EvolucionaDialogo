
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helper;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    internal struct DatosPreguntas
    {
        public string Valor;
        public string Estado;
        public int IdPregunta;
    }

    public partial class DuranteCompetencias : Page
    {
        #region Variables

        public int indexMenuServer = 2;
        public int indexSubMenu = 3;
        public int esCorrecto = 0;
        public int huboError = 0;
        public int estadoProceso = 0;
        public int readOnly = 0;
        public int porcentaje = 0;
        public string noValidar;
        private const string ParametroFFVV = "FS";
        public int idProceso = 0;

        protected BeResumenProceso objResumen;
        private static List<BeResumenVisita> visitas;
        private static List<BeEvento> eventos = new List<BeEvento>();
        private static int contador;
        public string nmbrEvld;

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
            objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            idProceso = objResumen.idProceso;
            estadoProceso = int.Parse(objResumen.estadoProceso);
            nmbrEvld = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);

            int diferencia = IsLast();
            hfDiferencia.Value = diferencia.ToString();

            CalcularAvanze();

            #region No permitir acceso cuando estado es Activo

            if (Session["_soloLectura"] != null)
            {
                readOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;
            }

            if (objResumen.estadoProceso == Constantes.EstadoProcesoCulminado || objResumen.estadoProceso == Constantes.EstadoProcesoRevision || objResumen.estadoProceso == Constantes.EstadoProcesoEnviado)
                divEnlace.Visible = false;

            #endregion No permitir acceso cuando estado es Activo

            #region Asignando Imagen y Resaltando Menu Adecuado

            string nombreImagen = string.Empty;

            if (Utils.QueryStringInt("aprobacion") == 1)
            {
                indexMenuServer = 3;
                nombreImagen = "dialogo_despues_competencias.jpg";
                btnGuardar.CssClass = "btnGuardarStyle";
                btnGuardar.Text = "Aceptar";
            }
            else
            {
                nombreImagen = "dialogo_durante_competencias.jpg";
            }

            #endregion Asignando Imagen y Resaltando Menu Adecuado

            if (IsPostBack) return;

            CargarEncuesta();
            if (Session["NombreEvaluado"] == null) return;

            if (objResumen.estadoProceso == Constantes.EstadoProcesoRevision ||
                objResumen.estadoProceso == Constantes.EstadoProcesoCulminado)
            {
                panVisita.Style.Add("display", "none");

                gvVisitas.Columns[2].Visible = false;
                gvVisitas.Columns[3].Visible = false;
            }

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            #region Asignando Header y Periodo de Evaluacion

            string periodoEvaluacion = string.Empty;
            if (Session["periodoActual"] != null)
                periodoEvaluacion = Session["periodoActual"].ToString();
            else
                periodoEvaluacion = objUsuario.periodoEvaluacion;

            //--
            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                TreeViewCheck.Enabled = false;
                txtOtros.Enabled = false;

                if (estadoProceso == 1)
                {

                    BlPlanAnual daProceso = new BlPlanAnual();
                    string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, objResumen.prefijoIsoPais);
                    string anio = periodoEvaluacion.Substring(0, 4);
                    ObtenerPlanAnualNuevas(CadenaConexion, Convert.ToInt32(anio), codigoPaisAdam,
                                           objResumen.codigoUsuario);
                }
            }
            //--


            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                                            Session["NombreEvaluado"].ToString(), periodoEvaluacion, nombreImagen);

            #endregion Asignando Header y Periodo de Evaluacion

            CargarCompetencia();

            if (ddlCompetencia.Items.Count != 0)
            {
                #region Cargando Competencias para el Proceso Actual

                Session["idPlanAnual"] = ddlCompetencia.SelectedValue;

                CargarRetroalimentcionGrabadas();

                objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
                BeRetroalimentacion beRetroalimentacion = new BeRetroalimentacion();
                beRetroalimentacion.CodigoPlanAnual = Convert.ToInt32(ddlCompetencia.SelectedValue);

                CargarZonaAlternativoPorRol(objResumen.idProceso);

                #endregion Cargando Competencias para el Proceso Actual
            }
            else
            {
                lnkMarcarTodos.Visible = false;
                txtOtros.Visible = false;
                cboEstadoIndicador1.Visible = false;
            }

            CargarHorasMinutos();
            CargarDescripcionesRol();
            CargarVisitas();
            RefrescarVisitas();
            LimpiarControlesVisita();


        }

        protected void ddlCompetencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idAsignado = Session["idPlanAnual"].ToString();
            Session["idPlanAnual"] = ddlCompetencia.SelectedValue;

            if (ddlCompetencia.SelectedValue != "-1")
            {
                Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro"];

                AsignarValores(idAsignado);

                if (valoresRetroAlimentacion == null || !valoresRetroAlimentacion.ContainsKey(ddlCompetencia.SelectedValue))
                    CargarRetroalimentcionGrabadas();
                else
                    CargarDatos(ddlCompetencia.SelectedValue);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool correcto = true;
            BlMenu objMenu = new BlMenu();

            #region Proceso Durante Competencia

            AsignarValores(ddlCompetencia.SelectedValue);

            correcto &= GrabarRetroalimentacion();
            correcto &= GrabarZonaAlternativa();
            correcto &= GrabarVistas();

            #endregion Proceso Durante Competencia

            string proceso = string.Empty;

            if (indexMenuServer != 3)
                proceso = Constantes.EstadoProcesoRevision;
            else
                proceso = estadoProceso.ToString();

            DataTable dt = new DataTable();
            if (noValidar != "si")
            {
                dt = objMenu.ValidarAprobacion(objResumen.idProceso, proceso);
            }
            else
            {
                dt = objMenu.ValidarAprobacionSinLets(objResumen.idProceso, proceso);
            }

            Session["Mensaje"] = dt.Rows[0]["mensaje"].ToString();

            if (Session["Mensaje"].ToString() == " La aprobación ha sido enviada")
            {
                #region Envio de Correo

                if (proceso != Constantes.EstadoProcesoCulminado)
                {
                    if (Constantes.RolGerenteRegion == objResumen.codigoRolUsuario || Constantes.RolGerenteZona == objResumen.codigoRolUsuario)
                    {
                        BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
                        EnviarCorreos(objUsuario.correoElectronico, objResumen.email, objResumen.codigoRolUsuario, objResumen.periodo, objResumen.codigoUsuario, objResumen.prefijoIsoPais);
                    }
                }

                #endregion Envio de Correo

                estadoProceso = int.Parse(proceso);
                esCorrecto = correcto ? 1 : 0;
            }
            else
            {
                #region Construyendo Mensaje A Mostrar

                lblMensajes.Text = Session["Mensaje"].ToString();
                string mensajeTotal = lblMensajes.Text;
                if (mensajeTotal != "")
                {
                    string[] strMensaje = mensajeTotal.Split(',');

                    if (strMensaje.Length > 0)
                    {
                        mensajeTotal = "";
                        foreach (string valor in strMensaje)
                        {
                            mensajeTotal = mensajeTotal + valor + "<br/>";
                        }
                    }

                    lblMensajes.Text = mensajeTotal;
                }

                #endregion Construyendo Mensaje A Mostrar

                huboError = 1;
                esCorrecto = 0;
            }
        }

        protected void btnCargarCampanhas_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha = Convert.ToDateTime(txtFechaVista.Text);
                CargarCampañas(fecha);
                btnAgregarActualizarVisita.Focus();
            }
            catch (Exception) { }
        }

        protected void btnAgregarActualizarVisita_Click(object sender, EventArgs e)
        {
            BeEvento objEvento = new BeEvento();
            if (txtFechaVista.Text == "")
                ClientScript.RegisterStartupScript(Page.GetType(), "_resProcesoEvaluado", "<script language='javascript'> javascript:validarFecha(); </script>");
            else
            {
                if (string.IsNullOrEmpty(hidIdVisita.Value) || hidIdVisita.Value == "0")
                {
                    BeResumenVisita objVisita = new BeResumenVisita();
                    objVisita.idVisita = ++contador;
                    objVisita.campania = ddlCampania.SelectedValue;
                    objVisita.fechaPostVisita = Convert.ToDateTime(txtFechaVista.Text + " " + ddlHoras.SelectedValue + ":" + ddlMinutos.SelectedValue);
                    objVisita.codigoUsuario = objResumen.codigoUsuario;
                    objVisita.idRolUsuario = objResumen.rolUsuario;
                    objVisita.codigoUsuarioEvaluador = objResumen.codigoUsuarioEvaluador;
                    objVisita.idRolUsuarioEvaluador = objResumen.rolUsuarioEvaluador;
                    objVisita.periodo = ObtenerPeriodo(ddlCampania.SelectedValue);
                    objVisita.prefijoIsoPais = objResumen.prefijoIsoPais;
                    objVisita.idProceso = objResumen.idProceso;
                    objVisita.estadoVisita = Constantes.EstadoVisitaActivo;
                    visitas.Add(objVisita);

                    objEvento.FechaInicio = Convert.ToDateTime(txtFechaVista.Text + " " + ddlHoras.SelectedValue + ":" + ddlMinutos.SelectedValue);
                    objEvento.FechaFin = objEvento.FechaInicio.AddHours(1);
                    objEvento.Campanha = ddlCampania.SelectedValue;
                }
                else
                {
                    int idVisita = Convert.ToInt32(hidIdVisita.Value);
                    BeResumenVisita objVisita = visitas.Find(delegate(BeResumenVisita obj)
                    {
                        return obj.idVisita == idVisita;
                    });

                    objVisita.campania = ddlCampania.SelectedValue;
                    objVisita.fechaPostVisita = Convert.ToDateTime(txtFechaVista.Text + " " + ddlHoras.SelectedValue + ":" + ddlMinutos.SelectedValue);

                    objEvento.FechaInicio = objVisita.fechaPostVisita;
                    objEvento.FechaFin = objVisita.fechaPostVisita.AddHours(1);
                    objEvento.Campanha = objVisita.campania;
                }

                objEvento.Reunion = Constantes.ReunionIndividual;
                objEvento.Evaluado = objResumen.codigoUsuario;
                objEvento.CodigoUsuario = objResumen.codigoUsuarioEvaluador;
                objEvento.RolUsuario = objResumen.rolUsuarioEvaluador;
                objEvento.FechaCreacion = DateTime.Now;
                objEvento.Asunto = "Visita Planificada";

                if (objResumen.codigoRolUsuario == Constantes.RolGerenteRegion)
                {
                    objEvento.Evento = 58;      //Visitas Evoluciona
                    objEvento.SubEvento = 60;   //Gerente de Región
                }
                else if (objResumen.codigoRolUsuario == Constantes.RolGerenteZona)
                {
                    objEvento.Evento = 31;      //Visitas Evoluciona
                    objEvento.SubEvento = 46;   //Gerente de Zona
                }

                eventos.Add(objEvento);

                RefrescarVisitas();
                LimpiarControlesVisita();
                txtFechaVista.Focus();
            }
        }

        protected void gvVisitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var idVisita = Convert.ToInt32(e.CommandArgument);
            BeResumenVisita objVisita = visitas.Find(delegate(BeResumenVisita obj)
            {
                return obj.idVisita == idVisita;
            });

            switch (e.CommandName)
            {
                case "Modificar":
                    CargarCampañas(objVisita.fechaPostVisita);
                    txtFechaVista.Text = objVisita.fechaPostVisita.ToShortDateString();
                    ddlCampania.SelectedValue = objVisita.campania;
                    ddlHoras.SelectedValue = objVisita.fechaPostVisita.Hour < 10
                                                 ? "0" + objVisita.fechaPostVisita.Hour
                                                 : objVisita.fechaPostVisita.Hour.ToString();
                    ddlMinutos.SelectedValue = objVisita.fechaPostVisita.Minute < 10
                                                 ? "0" + objVisita.fechaPostVisita.Minute
                                                 : objVisita.fechaPostVisita.Minute.ToString();
                    hidIdVisita.Value = objVisita.idVisita.ToString();
                    break;
                case "Eliminar":
                    visitas.Remove(objVisita);
                    break;
            }
            RefrescarVisitas();
            txtFechaVista.Focus();
        }

        #endregion Eventos

        #region Metodos

        private void CargarEncuesta()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            switch (objUsuario.codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    CambiarUrlDV(objUsuario.prefijoIsoPais.ToUpper());
                    break;
                case Constantes.RolGerenteRegion:
                    cambiarUrlGR(objUsuario.prefijoIsoPais.ToUpper());
                    break;
            }
        }

        private void CambiarUrlDV(string pais)
        {
            string ruta = Server.MapPath("./") + "Encuestas.xml";
            XmlDocument documento = new XmlDocument();
            documento.Load(ruta);
            XmlNodeList nodoEnlace = documento.GetElementsByTagName("Enlaces");

            XmlNodeList listaEnlaces =
                ((XmlElement)nodoEnlace[0]).GetElementsByTagName("Enlace");

            foreach (XmlElement nodo in listaEnlaces)
            {
                int i = 0;

                XmlNodeList rolE =
                nodo.GetElementsByTagName("Rol");

                XmlNodeList paisE =
                nodo.GetElementsByTagName("Pais");

                XmlNodeList tipoE =
                nodo.GetElementsByTagName("Tipo");

                XmlNodeList urlE =
                nodo.GetElementsByTagName("Url");

                XmlNodeList enunciadoInicialE =
                nodo.GetElementsByTagName("EnunciadoInicial");

                XmlNodeList enunciadoFinalE =
                nodo.GetElementsByTagName("EnunciadoFinal");

                XmlNodeList comentarioE =
                nodo.GetElementsByTagName("Comentario");

                if (pais == paisE[i].InnerText && rolE[i].InnerText == "DV" && tipoE[i].InnerText == "1")
                {
                    lblEnunciado.InnerText = enunciadoInicialE[i].InnerText;
                    lnkEncuesta.HRef = urlE[i].InnerText;
                    lnkEncuesta.InnerHtml = enunciadoFinalE[i].InnerText;
                    lblInformacionDni.InnerText = comentarioE[i].InnerText;
                }
            }
        }

        private void cambiarUrlGR(string pais)
        {
            string ruta = Server.MapPath("./") + "Encuestas.xml";
            XmlDocument documento = new XmlDocument();
            documento.Load(ruta);
            XmlNodeList nodoEnlace = documento.GetElementsByTagName("Enlaces");

            XmlNodeList listaEnlaces =
                ((XmlElement)nodoEnlace[0]).GetElementsByTagName("Enlace");

            foreach (XmlElement nodo in listaEnlaces)
            {
                int i = 0;

                XmlNodeList rolE =
                nodo.GetElementsByTagName("Rol");

                XmlNodeList paisE =
                nodo.GetElementsByTagName("Pais");

                XmlNodeList tipoE =
                nodo.GetElementsByTagName("Tipo");

                XmlNodeList urlE =
                nodo.GetElementsByTagName("Url");

                XmlNodeList enunciadoInicialE =
                nodo.GetElementsByTagName("EnunciadoInicial");

                XmlNodeList enunciadoFinalE =
                nodo.GetElementsByTagName("EnunciadoFinal");

                XmlNodeList comentarioE =
                nodo.GetElementsByTagName("Comentario");

                if (pais == paisE[i].InnerText && rolE[i].InnerText == "GR" && tipoE[i].InnerText == "1")
                {
                    lblEnunciado.InnerText = enunciadoInicialE[i].InnerText;
                    lnkEncuesta.HRef = urlE[i].InnerText;
                    lnkEncuesta.InnerHtml = enunciadoFinalE[i].InnerText;
                    lblInformacionDni.InnerText = comentarioE[i].InnerText;
                }
            }
        }

        private void CargarDescripcionesRol()
        {
            string ubicacionRol = string.Empty;

            if (objResumen.codigoRolUsuario == Constantes.RolGerenteRegion)
                ubicacionRol = "Gerente de Región";
            else
                ubicacionRol = "Gerente de Zona";

            lblDescripcionRol.Text = ubicacionRol;
        }

        private void CargarCompetencia()
        {
            BeResumenProceso beReseumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlRetroalimentacion daProceso = new BlRetroalimentacion();

            String anio = objResumen.periodo.Substring(0, 4).ToString();

            DataTable dtCompetencia = new DataTable();

            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                dtCompetencia = daProceso.CargarCompetenciaNueva(CadenaConexion, beReseumen, anio);
            }
            else
            {
                dtCompetencia = daProceso.CargarCompetencia(CadenaConexion, beReseumen, anio);
            }



            if (dtCompetencia.Rows.Count == 0)
            {
                lblMensajes.Text = "Usted no tiene registrada sus competencias";
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
            beRetroalimentacion.idProceso = objResumen.idProceso;
            beRetroalimentacion.CodigoPlanAnual = Convert.ToInt32(ddlCompetencia.SelectedValue);


            DataTable resultadoPreguntas = new DataTable();

            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                resultadoPreguntas = daProceso.ListarRetroalimentacionNuevas(CadenaConexion, beRetroalimentacion);
            }
            else
            {
                resultadoPreguntas = daProceso.ListarRetroalimentacion(CadenaConexion, beRetroalimentacion);
            }

            dlPreguntas1.DataSource = resultadoPreguntas;
            dlPreguntas1.DataBind();

            string estadoProceso = resultadoPreguntas.Rows[0].ItemArray[4].ToString();

            if (!string.IsNullOrEmpty(estadoProceso.Trim()))
                cboEstadoIndicador1.SelectedValue = bool.Parse(estadoProceso.Trim()) ? "1" : "0";
            else
                cboEstadoIndicador1.SelectedValue = "0";
        }

        private void CargarZonaAlternativoPorRol(int idProceso)
        {
            BlDescripcionZonaAlternativa daAlternativa = new BlDescripcionZonaAlternativa();
            List<BeZonaAlternativa> listaAlternativasProcesadas = daAlternativa.ObtenerZonaAlternativaProcesada(CadenaConexion, idProceso);

            List<BeZonaAlternativa> listaAlternativas = daAlternativa.ObtenerZonaAlternativaPorRol(CadenaConexion, objResumen.codigoRolUsuario);
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

        private void CargarDatos(string idAsignado)
        {
            Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro"];

            List<DatosPreguntas> valores = valoresRetroAlimentacion[idAsignado];

            int index = 0;
            foreach (DataListItem item in dlPreguntas1.Items)
            {
                TextBox txtDato = (TextBox)item.Controls[5];
                txtDato.Text = valores[index++].Valor;
            }

            cboEstadoIndicador1.SelectedValue = valores[0].Estado;
        }

        private void AsignarValores(string idPregunta)
        {
            Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro"];

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
                nuevoDatoPregunta.Estado = cboEstadoIndicador1.SelectedValue;

                valores.Add(nuevoDatoPregunta);
            }

            if (valoresRetroAlimentacion.ContainsKey(idPregunta))
                valoresRetroAlimentacion[idPregunta] = valores;
            else
                valoresRetroAlimentacion.Add(idPregunta, valores);

            Session["valoresRetro"] = valoresRetroAlimentacion;
        }

        private bool GrabarZonaAlternativa()
        {
            bool correcto = true;
            BeZonaAlternativa beZonaAlternativa = new BeZonaAlternativa();

            if (string.IsNullOrEmpty(ddlCompetencia.SelectedValue))
                return correcto;

            try
            {
                BeResumenProceso beReseumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
                BlZonaAlternativa daProceso = new BlZonaAlternativa();

                BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

                beZonaAlternativa.IDProceso = beReseumen.idProceso;
                beZonaAlternativa.CodigoPlanAnual = Convert.ToInt32(ddlCompetencia.SelectedValue);

                foreach (TreeNode nodo in TreeViewCheck.Nodes)
                {
                    beZonaAlternativa.CodZonaAlternativa = Convert.ToInt32(nodo.Value);
                    beZonaAlternativa.AlternativaOtro = "";

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

                    correcto &= daProceso.IngresarZonaAlternativa(CadenaConexion, beZonaAlternativa, objUsuario);

                    if (nodo.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode nodoSec in nodo.ChildNodes)
                        {
                            beZonaAlternativa.CodZonaAlternativa = Convert.ToInt32(nodoSec.Value);
                            beZonaAlternativa.AlternativaOtro = "";

                            if (nodoSec.Checked)
                            {
                                beZonaAlternativa.Seleccionado = "1";
                            }
                            else
                            {
                                beZonaAlternativa.Seleccionado = "0";
                            }

                            correcto &= daProceso.IngresarZonaAlternativa(CadenaConexion, beZonaAlternativa, objUsuario);
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

        private bool GrabarRetroalimentacion()
        {
            bool correcto = true;

            BeResumenProceso beReseumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlRetroalimentacion daProceso = new BlRetroalimentacion();

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            Dictionary<string, List<DatosPreguntas>> valoresRetroAlimentacion = (Dictionary<string, List<DatosPreguntas>>)Session["valoresRetro"];

            foreach (string clave in valoresRetroAlimentacion.Keys)
            {
                foreach (DatosPreguntas datoPregunta in valoresRetroAlimentacion[clave])
                {
                    BeRetroalimentacion retroAlimentacion = new BeRetroalimentacion();

                    retroAlimentacion.idProceso = beReseumen.idProceso;
                    retroAlimentacion.idPreguntaRetroalimentacion = datoPregunta.IdPregunta;
                    retroAlimentacion.CodigoPlanAnual = int.Parse(clave);
                    retroAlimentacion.respuesta = datoPregunta.Valor;
                    retroAlimentacion.idUsuario = objUsuario.idUsuario;
                    retroAlimentacion.PostDialogo = datoPregunta.Estado == "1";

                    correcto &= daProceso.IngresarRetroalimentacion(CadenaConexion, retroAlimentacion);
                    if (!correcto) return false;
                }
            }

            return correcto;
        }

        private void EnviarCorreos(string correocopia, string email, int codigoRolUsuario, string periodo, string codigoUsuario, string prefijoIsoPais)
        {
            MailAddress correoFrom = new MailAddress(ConfigurationManager.AppSettings["usuarioEnviaMails"]);
            string servidorSMTP = ConfigurationManager.AppSettings["servidorSMTP"];
            string archivo = Server.MapPath("../configuracion.aspx").Replace("configuracion.aspx", "KeyPublicaDDesempenio.xml");
            string strHTML = string.Empty;
            Encriptacion objEncriptar = new Encriptacion();
            string paramLogeo = codigoUsuario + "|" + prefijoIsoPais + "|" + periodo + "|" + codigoRolUsuario;
            paramLogeo = objEncriptar.Encriptar(paramLogeo, archivo);
            paramLogeo = HttpUtility.UrlEncode(paramLogeo);

            strHTML += "<table align='center' border='0'>";
            strHTML += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 26px;'>Durante el Diálogo evoluciona se definieron planes para el periodo. El siguiente paso es <span style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'>cerrar tu compromiso</span> con dichos planes</td></tr>";
            strHTML += "<tr></tr>";
            strHTML += "<tr><td style='font-family:Arial; font-size:13px; color:#6a288a; height: 44px;'>Ingresa al sistema con los siguientes datos, y haz click en  <span style='font-family:Arial; font-size:13px; color:#6a288a; font-weight:bold;'> «Aprueba tu diálogo»  </span>.</td></tr>";
            strHTML += "<tr></tr>";
            strHTML += "<tr><td><a style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold; text-decoration:none;' href='" + ConfigurationManager.AppSettings["URLdesempenio"] + "validacion.aspx?sson=" + paramLogeo + "' target='_blank'>Haz click aquí, para ingresar</a></td></tr>";
            strHTML += "<tr></tr>";
            strHTML += "<tr></tr>";
            strHTML += "<tr></tr>";
            strHTML += "<tr></tr>";
            strHTML += "<tr><td style='font-family:Arial; font-size:13px; color:#00acee; font-weight:bold;'>¡Gracias por Participar!</td></tr>";
            strHTML += "</table>";

            SmtpClient enviar = new SmtpClient(servidorSMTP);
            try
            {
                MailAddress correoTo = new MailAddress(email);
                MailMessage msjEmail = new MailMessage(correoFrom, correoTo);
                msjEmail.Subject = "Aprueba tu diálogo";
                msjEmail.IsBodyHtml = true;
                msjEmail.Body = strHTML;
                MailAddress copy = new MailAddress(correocopia);
                msjEmail.CC.Add(copy);
                enviar.Send(msjEmail);
            }
            catch
            {
            }
        }

        private void CalcularAvanze()
        {
            noValidar = "no";
            porcentaje = ProgresoHelper.CalcularAvanze(objResumen.idProceso, TipoPantalla.Durante);
            if (objResumen.codigoRolUsuario == Constantes.RolGerenteZona && Constantes.ListaPaisesSinLets.Contains(objResumen.prefijoIsoPais))
            {
                noValidar = "si";
            }
        }

        private void CargarCampañas(DateTime fecha)
        {
            BlEvento eventoBL = new BlEvento();
            string codigoEvaluado = objResumen.rolUsuario == Constantes.RolLideres
                                     ? objResumen.codigoUsuarioEvaluador
                                     : objResumen.codigoUsuario;

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            List<string> campanhas = new List<string>();

            if (String.Equals(nmbrEvld, Constantes.Nueva))
            {
                campanhas = new List<string>() { "201301", "201302", "201303", "201304", "201305", "201306" };
            }
            else
            {
                campanhas = eventoBL.ObtenerCampanhasPosiblesPorFecha(objResumen.prefijoIsoPais, ParametroFFVV, objUsuario.codigoRol, codigoEvaluado, fecha);
                //List<string> campanhas = eventoBL.ObtenerCampanhasPosiblesPorFecha(objResumen.prefijoIsoPais, ParametroFFVV, objResumen.codigoRolUsuario, codigoEvaluado, fecha);
            }

            ddlCampania.DataSource = campanhas;
            ddlCampania.DataBind();
            //repCampanias.DataSource = campanhas;
            //repCampanias.DataBind();

            //ClientScript.RegisterStartupScript(GetType(), "MostrarCampanhas", "jQuery(document).ready(function() {jQuery('#divCampanias').dialog('open'); });", true);
        }

        private void CargarVisitas()
        {
            BlResumenVisita blResumenVisita = new BlResumenVisita();
            visitas = blResumenVisita.ListarVisitasPorProceso(objResumen.idProceso);
        }

        private void CargarHorasMinutos()
        {
            string[] horas = new string[24];
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                    horas[i] = "0" + i;
                else
                    horas[i] = i.ToString();
            }
            ddlHoras.DataSource = horas;
            ddlHoras.DataBind();

            int k = 0;
            string[] minutos = new string[12];
            for (int i = 0; i < 60; i = i + 5)
            {
                if (i < 10)
                    minutos[k++] = "0" + i;
                else
                    minutos[k++] = i.ToString();
            }
            ddlMinutos.DataSource = minutos;
            ddlMinutos.DataBind();
        }

        private void RefrescarVisitas()
        {
            if (visitas == null)
                visitas = new List<BeResumenVisita>();

            gvVisitas.DataSource = visitas;
            gvVisitas.DataBind();
        }

        private void LimpiarControlesVisita()
        {
            txtFechaVista.Text = string.Empty;
            ddlCampania.DataSource = null;
            ddlCampania.DataBind();
            ddlHoras.SelectedIndex = -1;
            ddlMinutos.SelectedIndex = -1;
            hidIdVisita.Value = "0";
        }

        private bool GrabarVistas()
        {
            bool correcto = true;
            BlResumenVisita blVisita = new BlResumenVisita();
            BlEvento blEvento = new BlEvento();

            foreach (BeResumenVisita objVisita in visitas)
            {
                int idVisita = blVisita.CrearVisita(objVisita);

                if (idVisita <= 0)
                {
                    correcto = false;
                }
            }

            foreach (BeEvento evento in eventos)
            {
                blEvento.RegistrarEvento(evento);
            }

            return correcto;
        }

        private string ObtenerPeriodo(string campana)
        {
            if (string.IsNullOrEmpty(campana) || campana.Length < 6)
                return string.Empty;

            string anio = campana.Substring(0, 4);
            string periodo = string.Empty;
            int numero = Convert.ToInt32(campana.Substring(4, 2));

            if (numero >= 1 && numero <= 6)
                periodo = "I";
            else if (numero >= 7 && numero <= 12)
                periodo = "II";
            else
                periodo = "III";

            return string.Format("{0} {1}", anio, periodo);
        }



        private void ObtenerPlanAnualNuevas(string connstring, int anio, string codigoPaisAdam, string documentoIdentidad)
        {
            BlPlanAnual daProceso = new BlPlanAnual();

            if (!daProceso.ObtenerPlanAnualByUsuario(connstring, objResumen.rolUsuario, objResumen.prefijoIsoPais, anio.ToString(), documentoIdentidad, 1))
            {
                try
                {
                    string documentoIdentidadConsulta = codigoPaisAdam == Constantes.CodigoAdamPaisColombia
                                             ? Convert.ToInt32(documentoIdentidad).ToString()
                                             : documentoIdentidad.Trim();


                    BlPlanAnual planAnualBL = new BlPlanAnual();

                    DataSet dsPlanAnual = planAnualBL.ConsultaPlanDesarrollo(connstring, anio, codigoPaisAdam, documentoIdentidadConsulta, objResumen);

                    if (dsPlanAnual != null)
                    {
                        if (dsPlanAnual.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = dsPlanAnual.Tables[0].Rows[0];

                            daProceso.InsertarPlanAnualAdam(connstring, objResumen.rolUsuario,
                                                            objResumen.prefijoIsoPais,
                                                            anio.ToString(), documentoIdentidad,
                                                            dr["NombresEvaluado"].ToString(),
                                                            dr["DescripcionCompetencia"].ToString(),
                                                            dr["DescripcionComportamiento"].ToString(),
                                                            dr["ActividadesPlanDesarrollo"].ToString(),
                                                            dr["DescripcionSugerencia"].ToString(), 1,
                                                            Convert.ToInt32(dr["CodigoCompetencia"]));
                        }
                    }
                }
                catch (Exception)
                {
                    //lblMensajes.Text = "El servicio no se encuentra disponible, por favor intente luego";
                    //lblObservacion.Visible = false;
                    //txtObservacion.Visible = false;
                }
            }
        }

        private int IsLast()
        {
            int diferencia = 0;

            var objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            var blEncuestaDialogo = new BlEncuestaDialogo();
            hfCodigoUsuario.Value = objUsuario.codigoUsuario;
            hfPeriodo.Value = objUsuario.periodoEvaluacion;

            switch (objUsuario.codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    diferencia = blEncuestaDialogo.CantPorAprobarDv(objUsuario);
                    hfCodTipoEncuesta.Value = "DV DV GR";
                    break;
                case Constantes.RolGerenteRegion:
                    diferencia = blEncuestaDialogo.CantPorAprobarGr(objUsuario);
                    hfCodTipoEncuesta.Value = "GR GR GZ";
                    break;
            }

            return diferencia;
        }

        #endregion Metodos
    }
}