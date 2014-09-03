
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class ResumenProceso : Page
    {
        #region Variables

        public int SinDatos = 0;
        public string VerResumen;
        public string CodigoPais;
        public string CodigoEvaluador;
        private BeUsuario _objUsuario;

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            _objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (IsPostBack) return;
            CargarEncuesta();

            var encuesta = LLenoEncuesta();
            hfEncuesta.Value = encuesta.ToString();

            Session["_soloLectura"] = null;

            #region Verificando si Se puede Iniciar un Dialogo

            var valorQs = Utils.QueryString("inicio");
            if (!string.IsNullOrEmpty(valorQs))
            {
                SinDatos = 1;
            }

            VerResumen = "no";
            if (Request["verResumen"] != null)
            {
                VerResumen = "si";
                puntoAproba.Visible = true;
                lblMensajeDialogo.Text = "Tu diálogo ha sido aprobado";
            }

            #endregion Verificando si Se puede Iniciar un Dialogo

            CargarPeriodos();
            CargarTipo();
            CargarFormulario();
            Session["usuarioEvaluado"] = null;
            Session["dtCargarCriticas"] = null;
            AgregarUrl();
        }

        protected void gvProcesosInactivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProcesosInactivos.PageIndex = e.NewPageIndex;
            gvProcesosInactivos.DataSource = Session["dtProcesosNoActivos"];
            gvProcesosInactivos.DataBind();

            var objResumenBe = ObtenerDatosResumenEvaluador();

            if (objResumenBe != null) return;
            if (_objUsuario.codigoRol == Constantes.RolDirectorVentas) return;
            foreach (GridViewRow row in gvProcesosInactivos.Rows)
            {
                row.Enabled = false;
                ((HyperLink) row.Cells[0].Controls[0]).ControlStyle.ForeColor =
                    System.Drawing.ColorTranslator.FromHtml("#595959");
            }
        }

        protected void gvProcesosActivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProcesosActivos.PageIndex = e.NewPageIndex;
            gvProcesosActivos.DataSource = Session["dtProcesosActivos"];
            gvProcesosActivos.DataBind();
        }

        protected void gvProcesosEnRevision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProcesosEnRevision.PageIndex = e.NewPageIndex;
            gvProcesosEnRevision.DataSource = Session["dtProcesosEnRevision"];
            gvProcesosEnRevision.DataBind();
        }

        protected void gvProcesosAprobados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProcesosAprobados.PageIndex = e.NewPageIndex;
            gvProcesosAprobados.DataSource = Session["dtProcesosAprobados"];
            gvProcesosAprobados.DataBind();
        }

        protected void imgBtnAprobarDialogo_Click(object sender, EventArgs e)
        {
            var objResumenBl = new BlResumenProceso();
            var objResumenBe = new BeResumenProceso
            {
                idProceso = Convert.ToInt32(txtIdProceso.Text),
                estadoProceso = Constantes.EstadoProcesoCulminado
            };

            if (objResumenBl.ActualizarProceso(objResumenBe))
            {
                Response.Redirect("resumenProceso.aspx?verResumen=si");
            }
        }

        protected void lnkRevisarDialogo_Click(object sender, EventArgs e)
        {

            Session["codigoRolEvaluado"] = _objUsuario.codigoRol;
            Session["_soloLectura"] = true;

            Response.Redirect("resumenProcesoIniciar.aspx?codDI=" + _objUsuario.codigoUsuario +
                "&Nombre=" + _objUsuario.nombreUsuario + "&cod=xx");
        }

        protected void cboPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboPeriodos.SelectedValue)) return;
            Session["periodoActual"] = cboPeriodos.SelectedValue;

            _objUsuario.periodoEvaluacion = cboPeriodos.SelectedValue;
            Session[Constantes.ObjUsuarioLogeado] = _objUsuario;

            CargarFormulario();
            AgregarUrl();
        }


        protected void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboPeriodos.SelectedValue)) return;
            Session["periodoActual"] = cboPeriodos.SelectedValue;

            _objUsuario.periodoEvaluacion = cboPeriodos.SelectedValue;
            Session[Constantes.ObjUsuarioLogeado] = _objUsuario;



            if (String.Equals(cboTipo.Text, "PLANDEMEJORA"))
            {
                cboTipo.Attributes.Add("style", "background-color:#007ACC; color:#FFFFFF");
                imgBtnRegistarAcuerdo.Visible = true;
                imgBtnAyuda.Visible = true;
            }
            else
            {
                cboTipo.Attributes.Add("style", "background-color:#68217A; color:#FFFFFF");
                imgBtnRegistarAcuerdo.Visible = false;
                imgBtnAyuda.Visible = false;
            }

            cboTipo.Items.FindByValue("NORMAL").Attributes.Add("style", "background-color:#68217A; color:#FFFFFF");
            cboTipo.Items.FindByValue("PLANDEMEJORA")
                .Attributes.Add("style", "background-color:#007ACC; color:#FFFFFF");

            Session["tipoDialogoDesempenio"] = cboTipo.Text;

            CargarFormulario();
            AgregarUrl();

        }


        protected void lnkIniciarDialogoEvaluado_Click(object sender, EventArgs e)
        {
            Session["codigoRolEvaluado"] = _objUsuario.codigoRol;
            IniciarProcesoEvaluado(_objUsuario.codigoUsuario, _objUsuario.nombreUsuario, String.Empty);
        }

        protected void lnkIniciarDialogoEvaluadoResumen_Click(object sender, EventArgs e)
        {
            Session["codigoRolEvaluado"] = _objUsuario.codigoRol;
            IniciarProcesoEvaluado(_objUsuario.codigoUsuario, _objUsuario.nombreUsuario, "resumenDialogo");
        }

        protected void lbViewPreDialogo_Click(object sender, EventArgs e)
        {
            Session["codigoRolEvaluado"] = _objUsuario.codigoRol;
            IniciarProcesoEvaluado(_objUsuario.codigoUsuario, _objUsuario.nombreUsuario, "resumenPreDialogo");
        }

        #endregion Eventos

        #region Metodos

        private void AgregarUrl()
        {
            var codigoRolEvaluado = 0;
            switch (_objUsuario.codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    codigoRolEvaluado = Constantes.RolGerenteRegion;
                    break;
                case Constantes.RolGerenteRegion:
                    codigoRolEvaluado = Constantes.RolGerenteZona;
                    break;
                case Constantes.RolGerenteZona:
                    codigoRolEvaluado = Constantes.RolLideres;
                    break;
            }

            var idRolEvaluado = ObtenerIdrOl(codigoRolEvaluado);
            hlResumenImpresion.NavigateUrl =
                string.Format(
                    "javascript:CargarResumenImpreso('{0}Admin/Impresion.aspx?codigoUsuario={1}&idRolEvaluado={2}&prefijoIsoPais={3}&periodoEvaluacion={4}&codigoRolEvaluado={5}');",
                    Utils.AbsoluteWebRoot, _objUsuario.codigoUsuario, idRolEvaluado, _objUsuario.prefijoIsoPais,
                    cboPeriodos.SelectedValue, codigoRolEvaluado);
        }

        private void CargarEncuesta()
        {
            switch (_objUsuario.codigoRol)
            {
                case Constantes.RolGerenteRegion:
                    CambiarUrlGr(_objUsuario.prefijoIsoPais.ToUpper());
                    break;
                case Constantes.RolGerenteZona:
                    CambiarUrlGz(_objUsuario.prefijoIsoPais.ToUpper());
                    break;
            }
        }

        private void CambiarUrlGr(string pais)
        {
            var ruta = Server.MapPath("./") + "Encuestas.xml";
            var documento = new XmlDocument();
            documento.Load(ruta);
            var nodoEnlace = documento.GetElementsByTagName("Enlaces");

            var listaEnlaces =
                ((XmlElement)nodoEnlace[0]).GetElementsByTagName("Enlace");

            foreach (XmlElement nodo in listaEnlaces)
            {
                const int i = 0;

                var rolE =
                nodo.GetElementsByTagName("Rol");

                var paisE =
                nodo.GetElementsByTagName("Pais");

                var tipoE =
                nodo.GetElementsByTagName("Tipo");

                var urlE =
                nodo.GetElementsByTagName("Url");

                var enunciadoInicialE =
                nodo.GetElementsByTagName("EnunciadoInicial");

                var enunciadoFinalE =
                nodo.GetElementsByTagName("EnunciadoFinal");

                var comentarioE =
                nodo.GetElementsByTagName("Comentario");

                if (pais != paisE[i].InnerText || rolE[i].InnerText != "GR" || tipoE[i].InnerText != "2") continue;
                lblEnunciado.InnerText = enunciadoInicialE[i].InnerText;
                lnkEncuesta.HRef = urlE[i].InnerText;
                lnkEncuesta.InnerHtml = enunciadoFinalE[i].InnerText;
                lblInformacionDni.InnerText = comentarioE[i].InnerText;
            }

        }

        private void CambiarUrlGz(string pais)
        {
            var ruta = Server.MapPath("./") + "Encuestas.xml";
            var documento = new XmlDocument();
            documento.Load(ruta);
            var nodoEnlace = documento.GetElementsByTagName("Enlaces");

            var listaEnlaces =
                ((XmlElement)nodoEnlace[0]).GetElementsByTagName("Enlace");

            foreach (XmlElement nodo in listaEnlaces)
            {
                const int i = 0;

                var rolE =
                nodo.GetElementsByTagName("Rol");

                var paisE =
                nodo.GetElementsByTagName("Pais");

                var tipoE =
                nodo.GetElementsByTagName("Tipo");

                var urlE =
                nodo.GetElementsByTagName("Url");

                var enunciadoInicialE =
                nodo.GetElementsByTagName("EnunciadoInicial");

                var enunciadoFinalE =
                nodo.GetElementsByTagName("EnunciadoFinal");

                var comentarioE =
                nodo.GetElementsByTagName("Comentario");

                if (pais != paisE[i].InnerText || rolE[i].InnerText != "GZ" || tipoE[i].InnerText != "2") continue;
                lblEnunciado.InnerText = enunciadoInicialE[i].InnerText;
                lnkEncuesta.HRef = urlE[i].InnerText;
                lnkEncuesta.InnerHtml = enunciadoFinalE[i].InnerText;
                lblInformacionDni.InnerText = comentarioE[i].InnerText;
            }

        }

        private void CargarFormulario()
        {
            var codigoRolEvaluado = 0;

            CodigoPais = _objUsuario.prefijoIsoPais;
            CodigoEvaluador = _objUsuario.codigoUsuario;

            if (Session["mnMenuImagen"] != null)
                imgHeader.ImageUrl = "~/Images/" + Session["mnMenuImagen"];

            switch (_objUsuario.codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    codigoRolEvaluado = Constantes.RolGerenteRegion;
                    break;
                case Constantes.RolGerenteRegion:
                    codigoRolEvaluado = Constantes.RolGerenteZona;
                    break;
                case Constantes.RolGerenteZona:
                    codigoRolEvaluado = Constantes.RolLideres;
                    break;
            }

            Session["codigoRolEvaluado"] = codigoRolEvaluado;

            var periodoEvaluacion = Session["periodoActual"] != null
                ? Session["periodoActual"].ToString()
                : _objUsuario.periodoEvaluacion;

            CargarProcesosNoActivos(codigoRolEvaluado, periodoEvaluacion);
            CargarProcesosActivos(codigoRolEvaluado, periodoEvaluacion);
            CargarProcesosEnRevision(codigoRolEvaluado, periodoEvaluacion);
            CargarProcesosAprobados(codigoRolEvaluado, periodoEvaluacion);

            CargarResumenEvaluador();
        }

        private void CargarResumenEvaluador()
        {
            var objResumenBe = ObtenerDatosResumenEvaluador();

            if (objResumenBe == null)
            {
                divMiDialogo.Visible = false;
                if (_objUsuario.codigoRol == Constantes.RolDirectorVentas) return;
                foreach (GridViewRow row in gvProcesosInactivos.Rows)
                {
                    row.Enabled = false;
                    ((HyperLink)row.Cells[0].Controls[0]).ControlStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#595959");
                }

                return;
            }
            var tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();
            txtIdProceso.Text = objResumenBe.idProceso.ToString(CultureInfo.InvariantCulture);
            divMiDialogo.Visible = false;
            imgBtnAprobarDialogo.Visible = false;
            puntoAproba.Visible = false;
            lblMensajeDialogo.Text = "";

            switch (objResumenBe.estadoProceso)
            {
                case Constantes.EstadoProcesoRevision:
                    divMiDialogo.Visible = true;
                    puntoAproba.Visible = true;
                    imgBtnAprobarDialogo.Visible = true;
                    divIniciaDialogo.Visible = false;
                    lblMensajeDialogo.Text = "";
                    break;
                case Constantes.EstadoProcesoActivo:
                    divMiDialogo.Visible = true;
                    if (tipoDialogoDesempenio == Constantes.PlanDeMejora)
                    {
                        divIniciaDialogo.Visible = false;
                    }
                    imgBtnAprobarDialogo.Visible = false;
                    puntoAproba.Visible = false;
                    hdModifaProcesoEvaluador.Value = "SI";
                    lblMensajeDialogo.Text = "";
                    break;
                case Constantes.EstadoProcesoEnviado:
                    divMiDialogo.Visible = true;
                    if (tipoDialogoDesempenio == Constantes.PlanDeMejora)
                    {
                        divIniciaDialogo.Visible = false;
                    }
                    imgBtnAprobarDialogo.Visible = false;
                    puntoAproba.Visible = false;
                    hdModifaProcesoEvaluador.Value = "SI";
                    lblMensajeDialogo.Text = "";
                    break;
                case Constantes.EstadoProcesoCulminado:
                    divIniciaDialogo.Visible = false;
                    divMiDialogo.Visible = true;
                    imgBtnAprobarDialogo.Visible = false;
                    puntoAproba.Visible = true;
                    lblMensajeDialogo.Text = "Tu diálogo ha sido aprobado";
                    break;
                default:
                    divMiDialogo.Visible = true;
                    if (tipoDialogoDesempenio == Constantes.PlanDeMejora)
                    {
                        divIniciaDialogo.Visible = false;
                    }
                    imgBtnAprobarDialogo.Visible = false;
                    puntoAproba.Visible = true;
                    lblMensajeDialogo.Text = "Tu diálogo ha sido aprobado";
                    break;
            }
        }

        private BeResumenProceso ObtenerDatosResumenEvaluador()
        {
            var objBlResumen = new BlResumenProceso();

            var tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();


            var objResumen = objBlResumen.ObtenerResumenProcesoByUsuario(_objUsuario.codigoUsuario, _objUsuario.idRol,
                _objUsuario.periodoEvaluacion, _objUsuario.prefijoIsoPais, tipoDialogoDesempenio);

            if (tipoDialogoDesempenio != Constantes.PlanDeMejora) return objResumen;
            if (objResumen == null && _objUsuario.codigoRol != Constantes.RolGerenteZona)
            {
                objResumen = objBlResumen.ObtenerResumenProcesoByUsuario(_objUsuario.codigoUsuario, _objUsuario.idRol, _objUsuario.periodoEvaluacion, _objUsuario.prefijoIsoPais, "NORMAL");
            }

            return objResumen;
        }

        private void CargarPeriodos()
        {
            var periodoBl = new BlPeriodos();
            var periodos = periodoBl.ObtenerPeriodos(_objUsuario.prefijoIsoPais);
            Session["periodosValidos"] = periodos;

            cboPeriodos.DataSource = periodos;
            cboPeriodos.DataBind();

            cboPeriodos.SelectedValue = Session["periodoActual"].ToString().PadRight(8, ' ');

            if (_objUsuario.codigoRol == Constantes.RolGerenteZona)
            {
                hlResumenImpresion.Visible = false;
            }
        }

        private void CargarTipo()
        {
            var list = new List<string> { Constantes.Normal, Constantes.PlanDeMejora };

            cboTipo.DataSource = list;
            cboTipo.DataBind();
            var existePdm = ValidarFechaAcuerdoPdM();

            if (String.Equals(existePdm, Constantes.StrActivo))
            {
                cboTipo.Attributes.Add("style", "background-color:#007ACC; color:#FFFFFF");
                cboTipo.SelectedValue = "PLANDEMEJORA";
                imgBtnRegistarAcuerdo.Visible = true;
                imgBtnAyuda.Visible = true;
            }
            else
            {
                cboTipo.Attributes.Add("style", "background-color:#68217A; color:#FFFFFF");
                cboTipo.SelectedValue = "NORMAL";
                imgBtnRegistarAcuerdo.Visible = false;
                imgBtnAyuda.Visible = false;
            }
            cboTipo.Items.FindByValue("NORMAL").Attributes.Add("style", "background-color:#68217A; color:#FFFFFF");
            cboTipo.Items.FindByValue("PLANDEMEJORA")
                .Attributes.Add("style", "background-color:#007ACC; color:#FFFFFF");


            Session["tipoDialogoDesempenio"] = cboTipo.Text;
        }

        private void CargarProcesosNoActivos(int codigoRolEvaluado, string periodoEvaluacion)
        {
            var objBlResumen = new BlResumenProceso();
            DataTable dt = null;

            if (codigoRolEvaluado == Constantes.RolGerenteRegion && cboTipo.Text == Constantes.Normal)
            {
                dt = objBlResumen.SeleccionarGRegionParaInicioDialogo(_objUsuario.prefijoIsoPais, periodoEvaluacion, codigoRolEvaluado, _objUsuario.codigoUsuario);
            }
            else if (codigoRolEvaluado == Constantes.RolGerenteZona && cboTipo.Text == Constantes.Normal)
            {
                dt = objBlResumen.SeleccionarGZonaParaInicioDialogo(_objUsuario.codigoUsuario, _objUsuario.prefijoIsoPais, periodoEvaluacion, codigoRolEvaluado);
            }
            else if (codigoRolEvaluado == Constantes.RolGerenteRegion && cboTipo.Text == Constantes.PlanDeMejora)
            {
                dt = objBlResumen.SeleccionarGRegionParaInicioDialogoPlanDeMejora(_objUsuario.prefijoIsoPais, periodoEvaluacion, codigoRolEvaluado, _objUsuario.codigoUsuario);
            }
            else if (codigoRolEvaluado == Constantes.RolGerenteZona && cboTipo.Text == Constantes.PlanDeMejora)
            {
                dt = objBlResumen.SeleccionarGZonaParaInicioDialogoPlanDeMejora(_objUsuario.codigoUsuario, _objUsuario.prefijoIsoPais, periodoEvaluacion, codigoRolEvaluado);
            }


            Session["dtProcesosNoActivos"] = dt;
            gvProcesosInactivos.DataSource = dt;
            gvProcesosInactivos.DataBind();
        }

        private void CargarProcesosActivos(int codigoRolEvaluado, string periodoEvaluacion)
        {
            var dt = CargarProcesosByEstado(codigoRolEvaluado, Constantes.EstadoProcesoActivo, periodoEvaluacion);
            Session["dtProcesosActivos"] = dt;
            gvProcesosActivos.DataSource = dt;
            gvProcesosActivos.DataBind();
        }

        private void CargarProcesosEnRevision(int codigoRolEvaluado, string periodoEvaluacion)
        {
            var dt = CargarProcesosByEstado(codigoRolEvaluado, Constantes.EstadoProcesoRevision, periodoEvaluacion);
            Session["dtProcesosEnRevision"] = dt;
            gvProcesosEnRevision.DataSource = dt;
            gvProcesosEnRevision.DataBind();
        }

        private void CargarProcesosAprobados(int codigoRolEvaluado, string periodoEvaluacion)
        {
            var dt = CargarProcesosByEstado(codigoRolEvaluado, Constantes.EstadoProcesoCulminado, periodoEvaluacion);
            Session["dtProcesosAprobados"] = dt;
            gvProcesosAprobados.DataSource = dt;
            gvProcesosAprobados.DataBind();
        }

        private DataTable CargarProcesosByEstado(int codigoRolEvaluado, string estadoProceso, string periodoEvaluacion)
        {
            var idRolEvaluado = ObtenerIdrOl(codigoRolEvaluado);
            var objBlResumen = new BlResumenProceso();


            DataTable dt = null;

            if (codigoRolEvaluado == Constantes.RolGerenteRegion && cboTipo.Text == Constantes.Normal)
            {
                dt = objBlResumen.SeleccionarResumenProcesoGr(_objUsuario.codigoUsuario, idRolEvaluado, _objUsuario.prefijoIsoPais, periodoEvaluacion, estadoProceso, Constantes.EstadoActivo);
            }
            else if (codigoRolEvaluado == Constantes.RolGerenteZona && cboTipo.Text == Constantes.Normal)
            {
                dt = objBlResumen.SeleccionarResumenProcesoGz(_objUsuario.codigoUsuario, idRolEvaluado, _objUsuario.prefijoIsoPais, periodoEvaluacion, estadoProceso, Constantes.EstadoActivo);
            }
            else if (codigoRolEvaluado == Constantes.RolGerenteRegion && cboTipo.Text == Constantes.PlanDeMejora)
            {
                dt = objBlResumen.SeleccionarResumenProcesoGrPlanDeMejora(_objUsuario.codigoUsuario, idRolEvaluado, _objUsuario.prefijoIsoPais, periodoEvaluacion, estadoProceso, Constantes.EstadoActivo);
            }
            else if (codigoRolEvaluado == Constantes.RolGerenteZona && cboTipo.Text == Constantes.PlanDeMejora)
            {
                dt = objBlResumen.SeleccionarResumenProcesoGzPlanDeMejora(_objUsuario.codigoUsuario, idRolEvaluado, _objUsuario.prefijoIsoPais, periodoEvaluacion, estadoProceso, Constantes.EstadoActivo);
            }

            return dt;
        }

        private static int ObtenerIdrOl(int codigoRolEvaluado)
        {
            var objRol = new BlUsuario();
            var idRol = 0;
            var dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
            }
            return idRol;
        }

        private void IniciarProcesoEvaluado(string docuIdentidad, string nombreEvaluado, string resumenEvaluado)
        {
            var codigoRolEvaluado = Convert.ToInt32(Session["codigoRolEvaluado"].ToString());
            var idRol = 0;
            const int readOnly = 0;

            var objRol = new BlUsuario();
            var dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
            }

            var objResumenBl = new BlResumenProceso();

            var tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();

            var objResumen = objResumenBl.ObtenerResumenProcesoByUsuario(docuIdentidad, idRol, _objUsuario.periodoEvaluacion, _objUsuario.prefijoIsoPais, tipoDialogoDesempenio);
            var codigoGRegion = "";
            var codigoGZona = "";
            var email = "";
            switch (codigoRolEvaluado)
            {
                case Constantes.RolGerenteRegion:
                    var objDatosGr = objResumenBl.ObtenerUsuarioGRegionEvaluado(docuIdentidad, _objUsuario.prefijoIsoPais, _objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                    if (objDatosGr != null)
                    {
                        codigoGRegion = objDatosGr.codigoGRegion;
                        email = objDatosGr.email;
                    }

                    break;
                case Constantes.RolGerenteZona:
                    var objDatosGz = objResumenBl.ObtenerUsuarioGZonaEvaluado(_objUsuario.idUsuario, _objUsuario.codigoUsuario, docuIdentidad, _objUsuario.prefijoIsoPais, _objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                    if (objDatosGz != null)
                    {
                        codigoGZona = objDatosGz.codigoGZona;
                        email = objDatosGz.email;
                    }

                    break;
            }
            if (objResumen != null)
            {
                objResumen.periodo = _objUsuario.periodoEvaluacion;
                objResumen.prefijoIsoPais = _objUsuario.prefijoIsoPais;
                objResumen.codigoRolUsuario = codigoRolEvaluado;
                objResumen.rolUsuario = idRol;


                if (resumenEvaluado != "")
                {
                    objResumen.codigoUsuarioEvaluador = _objUsuario.codigoUsuario;
                }

                objResumen.nombreEvaluado = nombreEvaluado;
                objResumen.email = email;
            }
            else
            {
                return;
            }

            switch (codigoRolEvaluado)
            {
                case Constantes.RolGerenteRegion:
                    objResumen.codigoGRegion = codigoGRegion;
                    break;
                case Constantes.RolGerenteZona:
                    objResumen.codigoGZona = codigoGZona;
                    break;
            }

            Session[Constantes.ObjUsuarioProcesado] = objResumen;


            if (nombreEvaluado.Split('-')[0].ToUpper().Trim().Length < 8)
            {
                nombreEvaluado = nombreEvaluado.Replace("-", " ") + " " + nombreEvaluado.Replace("-", " ");

            }

            Session["NombreEvaluado"] = nombreEvaluado;


            var urlInicial = "AntesNegocioEvaluado.aspx?readOnly=" + readOnly;

            CodigoPais = objResumen.prefijoIsoPais;

            switch (resumenEvaluado)
            {
                case "resumenDialogo":
                    ClientScript.RegisterStartupScript(Page.GetType(), "_resProcesoEvaluado", "<script language='javascript'> javascript:CargarResumen('" + nombreEvaluado + "','" + docuIdentidad + "'," + txtIdProceso.Text + "," + codigoRolEvaluado + "); </script>");
                    break;
                case "resumenPreDialogo":
                    ClientScript.RegisterStartupScript(Page.GetType(), "_resProcesoEvaluado", "<script language='javascript'> javascript:CargarResumenPreDialogo('" + nombreEvaluado + "','" + docuIdentidad + "'," + txtIdProceso.Text + "," + codigoRolEvaluado + "); </script>");
                    break;
                default:
                    Response.Redirect(urlInicial);
                    break;
            }
        }

        #endregion Metodos

        protected void gvProcesosInactivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var a = e.Row.DataItem;
            if (a == null) return;
            var d = (DataRowView)a;
            string dato;

            var cdgPs = _objUsuario.prefijoIsoPais;

            if (_objUsuario.codigoRol != Constantes.RolDirectorVentas)
            {

                dato = !String.Equals(cdgPs, "PE") ? d.Row[2].ToString() : d.Row[1].ToString();

            }
            else
            {
                dato = d.Row[1].ToString();

            }

            ((HyperLink)e.Row.Cells[0].Controls[0]).ControlStyle.Font.Size = 8;

            if (dato.Split('-')[0].ToUpper().Trim().Length < 8)
            {
                dato = dato.Replace("-", " ") + " " + dato.Replace("-", " ");

            }

            ((HyperLink) e.Row.Cells[0].Controls[0]).ControlStyle.ForeColor =
                System.Drawing.ColorTranslator.FromHtml(
                    String.Equals(dato.Split('-')[0].ToUpper().Trim().Substring(3, 5), Constantes.Nueva)
                        ? "#E7AB15"
                        : "#00ACEE");
        }

        protected void gvProcesosActivos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var a = e.Row.DataItem;
            if (a == null) return;
            var d = (DataRowView)a;
            var dato = d.Row[3].ToString();

            ((HyperLink)e.Row.Cells[0].Controls[0]).ControlStyle.Font.Size = 8;

            if (dato.Split('-')[0].ToUpper().Trim().Length < 8)
            {
                dato = dato.Replace("-", " ") + " " + dato.Replace("-", " ");

            }

            ((HyperLink) e.Row.Cells[0].Controls[0]).ControlStyle.ForeColor =
                System.Drawing.ColorTranslator.FromHtml(
                    String.Equals(dato.Split('-')[0].ToUpper().Trim().Substring(3, 5), Constantes.Nueva)
                        ? "#E7AB15"
                        : "#00ACEE");
        }

        protected void gvProcesosEnRevision_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var a = e.Row.DataItem;
            if (a == null) return;
            var d = (DataRowView)a;
            var dato = d.Row[3].ToString();

            ((HyperLink)e.Row.Cells[0].Controls[0]).ControlStyle.Font.Size = 8;

            if (dato.Split('-')[0].ToUpper().Trim().Length < 8)
            {
                dato = dato.Replace("-", " ") + " " + dato.Replace("-", " ");

            }

            ((HyperLink) e.Row.Cells[0].Controls[0]).ControlStyle.ForeColor =
                System.Drawing.ColorTranslator.FromHtml(
                    String.Equals(dato.Split('-')[0].ToUpper().Trim().Substring(3, 5), Constantes.Nueva)
                        ? "#E7AB15"
                        : "#00ACEE");
        }

        protected void gvProcesosAprobados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var a = e.Row.DataItem;
            if (a == null) return;
            var d = (DataRowView)a;
            var dato = d.Row[3].ToString();

            ((HyperLink)e.Row.Cells[0].Controls[0]).ControlStyle.Font.Size = 8;

            if (dato.Split('-')[0].ToUpper().Trim().Length < 8)
            {
                dato = dato.Replace("-", " ") + " " + dato.Replace("-", " ");

            }

            ((HyperLink) e.Row.Cells[0].Controls[0]).ControlStyle.ForeColor =
                System.Drawing.ColorTranslator.FromHtml(
                    String.Equals(dato.Split('-')[0].ToUpper().Trim().Substring(3, 5), Constantes.Nueva)
                        ? "#E7AB15"
                        : "#00ACEE");
        }


        private bool LLenoEncuesta()
        {
            var blEncuestaDialogo = new BlEncuestaDialogo();
            hfCodigoUsuario.Value = _objUsuario.codigoUsuario;
            hfPeriodo.Value = _objUsuario.periodoEvaluacion;

            switch (_objUsuario.codigoRol)
            {
                case Constantes.RolGerenteRegion:
                    hfCodTipoEncuesta.Value = "GR DV GR";
                    break;
                case Constantes.RolGerenteZona:
                    hfCodTipoEncuesta.Value = "GZ GR GZ";
                    break;
            }

            var encuesta = blEncuestaDialogo.LlenoEncuesta(_objUsuario, hfCodTipoEncuesta.Value);

            return encuesta;
        }

        private string ValidarFechaAcuerdoPdM()
        {
            var blCronogramaPdM = new BlCronogramaPdM();
            var pais = _objUsuario.prefijoIsoPais;
            var periodo = Session["periodoActual"].ToString().PadRight(8, ' ');
            var existePdm = blCronogramaPdM.ValidarFechaAcuerdo(pais, periodo);
            return existePdm;
        }

        protected void imgBtnRegistarAcuerdo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("RegistroAcuerdo.aspx");
        }

        protected void imgBtnAyuda_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AyudaPdM.aspx");
        }   
    }
}