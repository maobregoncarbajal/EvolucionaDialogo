
using System.Globalization;
using System.Linq;

namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helper;
    using Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class AntesEquiposEvaluado : Page
    {
        #region Variables

        public int IndexMenuServer = 1;
        public int IndexSubMenu = 2;

        public int EstadoProceso = 0;
        public int EsCorrecto = 0;
        public int ReadOnly = 0;
        public int Porcentaje = 0;

        readonly BlCritica _daProceso = new BlCritica();

        protected BeUsuario ObjUsuario;
        protected BeResumenProceso ObjResumenBe;

        public int CodigoRolUsuario;
        public string PrefijoIsoPais;
        public string CodigoUsuario;
        public string PeriodoEvaluacion;
        public int IdProceso;
        public string NoValidar;

        public string AnioCampana;
        public string PeriodoCerrado;

        public string CodigoUsuarioProcesado;
        public string NmbrEvld;

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
            NmbrEvld = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);
            CargarVariables();
            CalcularAvanze();

            if (Session["_soloLectura"] != null)
                ReadOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;

            if (IsPostBack) return;

            if (Session["NombreEvaluado"] == null) return;

            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                Session["NombreEvaluado"].ToString(), PeriodoEvaluacion, "dialogo_antes_equipos.jpg");

            CargarDescripcionesRol();
            CargarResumen();
            CargarEvaluados();

            const string functionJavaScript = "javascript:CargarSeguimiento(this);";
            lstCriticas.Attributes.Add("OnDblClick", functionJavaScript);
            lstEstables.Attributes.Add("OnDblClick", functionJavaScript);
            lstProductivas.Attributes.Add("OnDblClick", functionJavaScript);

            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = NmbrEvld;
            }

            cboPeriodosFiltro.SelectedValue = Session["periodoActual"].ToString().PadRight(8, ' ');
            CargarGerentes();
        }

        protected void lnkAgregar_Click(object sender, EventArgs e)
        {
            var valorSeleccionado = txtValorCritica.Text;
            Crear_Actualizar(valorSeleccionado, cboEvaluados.SelectedItem.Text, txtTextoIngresado.Text);

            var criticaDisponible = BuscarCriticasDisponibles(valorSeleccionado);

            var criticasDisponibles = (List<BeCriticas>)Session["criticasDisponibles"];
            criticasDisponibles.Remove(criticaDisponible);
            CargarCriticasDisponibles(criticasDisponibles);

            cboEvaluados.SelectedIndex = 0;
            txtValorCritica.Text = string.Empty;
            txtTextoIngresado.Text = string.Empty;
            CargarResumen();
        }

        protected void btnEliminarSeleccionado_Click(object sender, EventArgs e)
        {
            var codigoEliminar = hidEliminadoSeleccionado.Value;

            var disponibles = (List<BeCriticas>)Session["criticasDisponibles"];
            var procesados = (List<BeCriticas>)Session["criticasProcesadas"];

            for (var i = 0; i < mnuSelecccionados.Items.Count; i++)
            {
                var item = mnuSelecccionados.Items[i];

                if (item.NavigateUrl.Trim() != codigoEliminar.Trim()) continue;

                mnuSelecccionados.Items.RemoveAt(i);
                mnuVerHistorial.Items.RemoveAt(i);
                mnuEliminar.Items.RemoveAt(i);

                var critica = BuscarCriticasProcesadas(codigoEliminar);
                if (critica != null)
                {
                    disponibles.Add(critica);
                    procesados.Remove(critica);
                }
                break;
            }

            CargarCriticasDisponibles(disponibles);
            cboEvaluados.SelectedIndex = 0;
            txtValorCritica.Text = string.Empty;
            txtTextoIngresado.Text = string.Empty;

            CargarResumen();


        }

        protected void btnGuardarDes_Click(object sender, EventArgs e)
        {
            var criticaBl = new BlCritica();
            var procesoBl = new BlProceso();

            var lstCargarCriticasProcesadas = _daProceso.ListaCargarCriticasProcesadasPreDialogo(CodigoUsuarioProcesado,
                PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion, ObjResumenBe.idProceso, AnioCampana);

            foreach (var critica in lstCargarCriticasProcesadas)
            {
                var seTieneEliminar = mnuSelecccionados.Items.Cast<MenuItem>().All(item => item.NavigateUrl.Trim() != critica.documentoIdentidad.Trim());

                if (seTieneEliminar)
                    criticaBl.EliminarCritica(critica.documentoIdentidad, ObjResumenBe.idProceso, CadenaConexion);
            }

            var esCorrecto = mnuSelecccionados.Items.Cast<MenuItem>().Aggregate(true, (current, item) => current & criticaBl.InsertarCriticas(item.NavigateUrl, IdProceso, item.ToolTip, CodigoRolUsuario, CadenaConexion));

            esCorrecto &= procesoBl.RegistrarNuevasIngresadas(IdProceso, string.IsNullOrEmpty(txtNumero.Text) ? 0 : int.Parse(txtNumero.Text));

            if (esCorrecto)
                ObjResumenBe.NuevasIngresadas = string.IsNullOrEmpty(txtNumero.Text) ? 0 : int.Parse(txtNumero.Text);

            EsCorrecto = esCorrecto ? 1 : 0;


            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = "triggerBtnguardar";
            }


        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var criticaBl = new BlCritica();
            var procesoBl = new BlProceso();

            var lstCargarCriticasProcesadas = _daProceso.ListaCargarCriticasProcesadasPreDialogo(CodigoUsuarioProcesado,
                PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion, ObjResumenBe.idProceso, AnioCampana);

            foreach (var critica in lstCargarCriticasProcesadas)
            {
                var seTieneEliminar = mnuSelecccionados.Items.Cast<MenuItem>().All(item => item.NavigateUrl.Trim() != critica.documentoIdentidad.Trim());

                if (seTieneEliminar)
                    criticaBl.EliminarCritica(critica.documentoIdentidad, ObjResumenBe.idProceso, CadenaConexion);
            }

            var esCorrecto = mnuSelecccionados.Items.Cast<MenuItem>().Aggregate(true, (current, item) => current & criticaBl.InsertarCriticasPreDialogo(item.NavigateUrl, IdProceso, item.ToolTip, CodigoRolUsuario, CadenaConexion));

            esCorrecto &= procesoBl.RegistrarNuevasIngresadas(IdProceso, string.IsNullOrEmpty(txtNumero.Text) ? 0 : int.Parse(txtNumero.Text));

            if (esCorrecto)
                ObjResumenBe.NuevasIngresadas = string.IsNullOrEmpty(txtNumero.Text) ? 0 : int.Parse(txtNumero.Text);

            EsCorrecto = esCorrecto ? 1 : 0;


            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = "triggerBtnguardar";
            }


        }




        #endregion Eventos

        #region Metodos

        private void CargarDescripcionesRol()
        {
            var ubicacionRol = ObjResumenBe.codigoRolUsuario == Constantes.RolGerenteRegion ? "Gerentes de Zona" : "Líderes";

            lblRolEvaluado.Text = lblRolEvaluado_1.Text = ubicacionRol;
        }

        private void CargarVariables()
        {
            lblTpEvld.Text = Session["NombreEvaluado"].ToString().Split('-')[0].ToUpper().Trim().Substring(3, 5);
            ObjResumenBe = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            ObjUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            CodigoRolUsuario = ObjResumenBe.codigoRolUsuario;
            PrefijoIsoPais = ObjResumenBe.prefijoIsoPais;
            CodigoUsuario = ObjResumenBe.codigoUsuario;

            PeriodoEvaluacion = Session["periodoActual"] != null ? Session["periodoActual"].ToString() : ObjUsuario.periodoEvaluacion;

            IdProceso = ObjResumenBe.idProceso;
            CodigoUsuarioProcesado = ObjResumenBe.codigoUsuario;

            EstadoProceso = int.Parse(ObjResumenBe.estadoProceso);

            switch (ObjResumenBe.codigoRolUsuario)
            {
                case Constantes.RolGerenteRegion:
                    lblGerente.Text = "Región";
                    lblBucaGerente.Text = "Región";
                    break;
                case Constantes.RolGerenteZona:
                    lblGerente.Text = "Zona";
                    lblBucaGerente.Text = "Zona";
                    break;
                default:
                    lblGerente.Text = string.Empty;
                    break;
            }

            ValidarPeriodoEvaluacion();
        }

        private void CargarResumen()
        {
            var indicadorBl = new BlIndicadores();
            var criticaBl = new BlCritica();


            switch (ObjResumenBe.codigoRolUsuario)
            {
                case Constantes.RolGerenteRegion:
                    {
                        var dtResumen = indicadorBl.ObtenerResumen(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais);
                        ddlResumen.DataSource = dtResumen;
                        ddlResumen.DataBind();

                        if (dtResumen.Rows.Count > 0)
                        {
                            lblUltimaCampanha.Text = "Segmentación en base a la &uacute;ltima campa&ntilde;a ( " + dtResumen.Rows[0].ItemArray[0].ToString().Trim() + ")";
                        }

                        lstCriticas.DataSource = criticaBl.ListaEstadosEquipo(CodigoUsuario, PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, "CRITICA");
                        lstEstables.DataSource = criticaBl.ListaEstadosEquipo(CodigoUsuario, PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, "ESTABLE");
                        lstProductivas.DataSource = criticaBl.ListaEstadosEquipo(CodigoUsuario, PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, "PRODUCTIVA");
                    }
                    break;
                case Constantes.RolGerenteZona:
                {
                    const string paisesLetsExitosas = "PA,PE,CO";
                    if (paisesLetsExitosas.IndexOf(ObjResumenBe.prefijoIsoPais, StringComparison.Ordinal) != -1)
                        {
                            var dtResumen = indicadorBl.ObtenerResumen(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais);
                            ddlResumen.DataSource = dtResumen;
                            ddlResumen.DataBind();

                            if (dtResumen.Rows.Count > 0)
                            {
                                lblUltimaCampanha.Text = "Segmentación en base a la &uacute;ltima campa&ntilde;a ( " + dtResumen.Rows[0].ItemArray[0].ToString().Trim() + ")";
                            }

                            lstCriticas.DataSource = criticaBl.ListaEstadosEquipo(CodigoUsuario, PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, "CRITICA");
                            //lstEstables.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "EXITOSA");
                            //lstProductivas.DataSource = criticaBL.ListaEstadosEquipo(codigoUsuario, PeriodoCerrado, codigoRolUsuario, prefijoIsoPais, "PRODUCTIVA");
                            lstEstables.Visible = false;
                            lstProductivas.Visible = false;
                            divSegmentacion.Visible = false;

                            lblNumero.Text = " Nº Líderes Nuevas : ";
                            lblCriticas.Text = "LÍDERES";
                            lblEstable.Text = "";
                            lblProductivas.Text = "";

                            lstCriticas.ToolTip = "";
                            lstEstables.ToolTip = "";
                            lstProductivas.ToolTip = "";

                            divMnjDblClcEstable.Visible = false;
                            divMnjDblClcProductiva.Visible = false;

                            lblUltimaCampanha.Text = "Lideres del Periodo";
                        }
                        else
                        {
                            lblNumero.Visible = false;
                            txtNumero.Visible = false;
                            lstCriticas.Visible = false;
                            lstEstables.Visible = false;
                            lstProductivas.Visible = false;
                            ImgLeyenda.Visible = true;

                            var oblLet = new BlLet();

                            List<BeLet> lstLets = oblLet.ObtenerLetsPorZona(ObjResumenBe.prefijoIsoPais, ObjResumenBe.codigoUsuario,
                                ObjResumenBe.codigoGZona, ObjResumenBe.periodo);

                            //Lista de Lets(CodigoConsultoraLet y  DesNombreLet)
                            var lstCodsLets = new List<String>();
                            var lstDatosLets = new List<BeLet>();
                            foreach (var lstLet in lstLets)
                            {
                                if (!lstCodsLets.Contains(lstLet.CodigoConsultoraLet))
                                {
                                    lstCodsLets.Add(lstLet.CodigoConsultoraLet);
                                    var dataLet = new BeLet
                                    {
                                        CodigoConsultoraLet = lstLet.CodigoConsultoraLet,
                                        DesNombreLet = lstLet.DesNombreLet,
                                        EstadoPeriodo = lstLet.EstadoPeriodo
                                    };
                                    lstDatosLets.Add(dataLet);

                                }
                            }


                            var strCampanias = new ArrayList();
                            var periodo = ObjResumenBe.periodo;
                            var anioPeriodo = periodo.Substring(0, 4);
                            periodo = periodo.Substring(5, periodo.Length - 5);
                            periodo = periodo.Trim();
                            switch (periodo)
                            {
                                case "I":
                                    for (var y = 1; y <= 6; y++)
                                    {
                                        strCampanias.Add(anioPeriodo + "0" + y);
                                    }
                                    break;
                                case "II":
                                    for (var y = 7; y <= 12; y++)
                                    {
                                        if (y < 10)
                                        {
                                            strCampanias.Add(anioPeriodo + "0" + y);
                                        }
                                        else
                                        {
                                            strCampanias.Add(anioPeriodo + y);
                                        }

                                    }
                                    break;
                                case "III":
                                    for (var y = 13; y <= 18; y++)
                                    {
                                        strCampanias.Add(anioPeriodo + y);
                                    }
                                    break;
                            }



                            //NO EXITOSA
                            var tablaLetsNoExitosas = new StringBuilder();
                            tablaLetsNoExitosas.Append(
                                "<table bgcolor='#DFE0DE' border='0' cellpadding='0' width='100%'>");


                            foreach (var codLet in lstDatosLets)
                            {
                                //                                if (RetornarNuevoNombreEstado(codLet.EstadoPeriodo) == "NO EXITOSA")
                                //                                {
                                tablaLetsNoExitosas.Append(
                                    "<tr><td width='218px' style='text-align: left; font-size: 10px; color:#404040;'>" +
                                    codLet.DesNombreLet + "</td>");

                                foreach (var cmpnh in strCampanias)
                                {
                                    var let =
                                        lstLets.Find(
                                            r =>
                                            r.AnioCampana == cmpnh.ToString() &&
                                            r.CodigoConsultoraLet == codLet.CodigoConsultoraLet);

                                    var numeroCampania = "C" + cmpnh.ToString().Substring(4, 2);
                                    tablaLetsNoExitosas.Append("<td class='texto_campania'>");

                                    if (let != null)
                                    {
                                        string urlImagen = RetornarImagen(@let.EstadoCamp);
                                        tablaLetsNoExitosas.Append("<img src='" + Utils.AbsoluteWebRoot +
                                                                   "Images/" +
                                                                   urlImagen +
                                                                   "' alt=''  width='16px' height='16px' /><br/>" +
                                                                   numeroCampania);
                                    }
                                    else
                                    {
                                        tablaLetsNoExitosas.Append("<img src='" + Utils.AbsoluteWebRoot +
                                                                   "Images/gris_fondo_gris.jpg' alt=''  width='16px' height='16px' /><br/>" +
                                                                   numeroCampania);

                                    }
                                    tablaLetsNoExitosas.Append("</td>");


                                }

                                tablaLetsNoExitosas.Append("<tr>");
                                //                                }
                            }


                            tablaLetsNoExitosas.Append("</table>");

                            var ltTablaNoExitosas = new LiteralControl {Text = tablaLetsNoExitosas.ToString()};

                            if (ltTablaNoExitosas.Text.Length > 73)
                            {
                                divNoExitosa.Controls.Add(ltTablaNoExitosas);
                                divNoExitosa.Attributes.Add("style", "display: inline");
                                lblCriticas.Text = "LETS";
                                lblEstable.Text = "";
                                lblProductivas.Text = "";
                            }
                            else
                            {
                                lblCriticas.Text = "";
                                lblEstable.Text = "";
                                lblProductivas.Text = "";
                            }
                        }
                }
                    break;
            }

            lstCriticas.DataBind();
            lstEstables.DataBind();
            lstProductivas.DataBind();
        }

        private void CargarEvaluados()
        {
            txtNumero.Text = ObjResumenBe.NuevasIngresadas > 0 ? ObjResumenBe.NuevasIngresadas.ToString(CultureInfo.InvariantCulture) : string.Empty;

            var lstCargarCriticasSinProcesar = _daProceso.ListaCargarCriticasDisponibles(CodigoUsuarioProcesado, PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais);

            lblTotalElementos.Text = lstCargarCriticasSinProcesar.Count.ToString(CultureInfo.InvariantCulture);

            var lstCargarCriticasProcesadas = _daProceso.ListaCargarCriticasProcesadasPreDialogo(CodigoUsuarioProcesado,
                PeriodoCerrado, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion, ObjResumenBe.idProceso, AnioCampana);

            var listaCriticasDisponibles = ObtenerCriticasRestantes(lstCargarCriticasSinProcesar, lstCargarCriticasProcesadas);

            Session["criticasProcesadas"] = lstCargarCriticasProcesadas;

            CargarMenusProcesados();

            var criticaSeleccione = new BeCriticas {nombresCritica = "[SELECCIONE]", documentoIdentidad = string.Empty};

            listaCriticasDisponibles.Insert(0, criticaSeleccione);

            Session["criticasDisponibles"] = listaCriticasDisponibles;

            CargarCriticasDisponibles(listaCriticasDisponibles);
        }

        private void CargarMenusProcesados()
        {
            var procesados = (List<BeCriticas>)Session["criticasProcesadas"];
            if (procesados != null && procesados.Count > 0)
            {
                foreach (var item in procesados)
                {
                    var itemMenu = new MenuItem
                    {
                        NavigateUrl = item.documentoIdentidad,
                        Text = item.nombresCritica,
                        ToolTip = item.variableConsiderar
                    };

                    var itemHitorial = new MenuItem
                    {
                        Text = "Ver Historial",
                        NavigateUrl = string.Format("{0}{1}?nombre={2}&codigoEvaluador={3}&codigoEvaluado={4}&tipo={5}",
                            Utils.AbsoluteWebRoot, "PantallasModales/HistorialPeriodoCriticidad.aspx",
                            item.nombresCritica, CodigoUsuarioProcesado, item.documentoIdentidad,
                            (int) TipoHistorial.CriticasAntes)
                    };

                    var itemEliminar = new MenuItem
                    {
                        Text = "<img src='../Images/delete_icon.png' /> Eliminar",
                        NavigateUrl = item.documentoIdentidad
                    };

                    mnuSelecccionados.Items.Add(itemMenu);
                    mnuVerHistorial.Items.Add(itemHitorial);
                    mnuEliminar.Items.Add(itemEliminar);
                }
            }
        }

        private void CargarCriticasDisponibles(List<BeCriticas> listaCriticasDisponibles)
        {
            cboEvaluados.DataSource = listaCriticasDisponibles;
            cboEvaluados.DataTextField = "nombresCritica";
            cboEvaluados.DataValueField = "documentoIdentidad";
            cboEvaluados.DataBind();
        }

        private List<BeCriticas> ObtenerCriticasRestantes(IEnumerable<BeCriticas> criticasTotales, List<BeCriticas> criticasProcesadas)
        {
            return (from item in criticasTotales let objetoEncontrado = criticasProcesadas.Find(
                objCriticaBusca => objCriticaBusca.documentoIdentidad.Trim() == item.documentoIdentidad.Trim()) where objetoEncontrado == null select item).ToList();
        }

        private BeCriticas BuscarCriticasDisponibles(string valorBuscar)
        {
            var criticas = (List<BeCriticas>)Session["criticasDisponibles"];
            if (criticas != null)
            {
                return criticas.FirstOrDefault(item => item.documentoIdentidad.Trim() == valorBuscar.Trim());
            }
            return null;
        }

        private BeCriticas BuscarCriticasProcesadas(string valorBuscar)
        {
            var criticas = (List<BeCriticas>)Session["criticasProcesadas"];
            if (criticas != null)
            {
                return criticas.FirstOrDefault(item => item.documentoIdentidad.Trim() == valorBuscar.Trim());
            }
            return null;
        }

        private void ValidarPeriodoEvaluacion()
        {
            string periodoEvaluacion = Session["periodoActual"] != null ? Session["periodoActual"].ToString() : ObjUsuario.periodoEvaluacion;

            var dtPeriodoEvaluacion = _daProceso.ValidarPeriodoEvaluacion(periodoEvaluacion, ObjResumenBe.prefijoIsoPais, CodigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void Crear_Actualizar(string valor, string nombrePersona, string textoIngresado)
        {
            foreach (MenuItem item in mnuSelecccionados.Items)
            {
                if (item.NavigateUrl == valor)
                {
                    item.ToolTip = textoIngresado;
                    return;
                }
            }

            var nuevoItem = new MenuItem {Text = nombrePersona, ToolTip = textoIngresado, NavigateUrl = valor};

            mnuSelecccionados.Items.Add(nuevoItem);

            var itemHitorial = new MenuItem
            {
                Text = "Ver Historial",
                NavigateUrl = string.Format("{0}{1}?nombre={2}&codigoEvaluador={3}&codigoEvaluado={4}&tipo={5}",
                    Utils.AbsoluteWebRoot, "PantallasModales/HistorialPeriodoCriticidad.aspx",
                    nombrePersona, CodigoUsuarioProcesado, valor, (int) TipoHistorial.CriticasAntes)
            };

            mnuVerHistorial.Items.Add(itemHitorial);

            var itemEliminar = new MenuItem
            {
                Text = "<img src='../Images/delete_icon.png' /> Eliminar",
                NavigateUrl = valor
            };

            mnuEliminar.Items.Add(itemEliminar);

            var procesados = (List<BeCriticas>)Session["criticasProcesadas"];
            var critica = BuscarCriticasDisponibles(valor);
            if (critica == null) return;
            procesados.Add(critica);
        }

        private void CalcularAvanze()
        {
            NoValidar = "no";
            Porcentaje = ProgresoHelper.CalcularAvanze(ObjResumenBe.idProceso, TipoPantalla.Antes);
            if (ObjResumenBe.codigoRolUsuario == Constantes.RolGerenteZona && Constantes.ListaPaisesSinLets.Contains(ObjResumenBe.prefijoIsoPais))
            {
                NoValidar = "si";
            }
        }

        private void CargarGerentes()
        {
            var periodo = cboPeriodosFiltro.SelectedValue;
            var indicadorBl = new BlIndicadores();
            switch (CodigoRolUsuario)
            {
                case 5:
                    ddlGerentes.DataSource = indicadorBl.CargarGerentesRegion(PrefijoIsoPais, periodo);
                    break;
                case 6:
                    ddlGerentes.DataSource = indicadorBl.CargarGerentesZona(PrefijoIsoPais, ObjResumenBe.codigoUsuarioEvaluador, periodo);
                    break;
            }

            ddlGerentes.DataTextField = "Descripcion";
            ddlGerentes.DataValueField = "Codigo";
            ddlGerentes.DataBind();


            var selectedListItem = ddlGerentes.Items.FindByValue(ObjResumenBe.codigoUsuario);

            if (selectedListItem != null)
            {
                selectedListItem.Selected = true;
            }
        }

        #endregion Metodos

        protected void cboPeriodosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGerentes();
        }

        private string RetornarImagen(string estado)
        {
            string nombreImagen;
            switch (estado.ToUpper().Trim())
            {
                case "CRITICA":
                    nombreImagen = "rojo_fondo_gris.jpg";
                    break;
                case "NO EXITOSA":
                    nombreImagen = "rojo_fondo_gris.jpg";
                    break;
                case "ESTABLE":
                    nombreImagen = "amarillo_fondo_gris.jpg";
                    break;
                case "PRODUCTIVA":
                    nombreImagen = "verde_oscuro_fondo_gris.jpg";
                    break;
                case "EXITOSA":
                    nombreImagen = "verde_oscuro_fondo_gris.jpg";
                    break;
                case "NUEVA":
                    nombreImagen = "verde_claro_fondo_gris.jpg";
                    break;
                default: nombreImagen = "gris_fondo_gris.jpg";
                    break;
            }
            return nombreImagen;
        }
    }
}