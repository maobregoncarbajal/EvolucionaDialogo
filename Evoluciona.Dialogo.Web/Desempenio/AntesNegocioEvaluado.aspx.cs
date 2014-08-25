
using System.Linq;

namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class AntesNegocioEvaluado : Page
    {
        #region Variables

        public int IndexMenuServer = 1;
        public int IndexSubMenu = 1;
        public int EstadoProceso = 0;
        public int ReadOnly = 0;
        public int EsCorrecto = 0;
        public int Porcentaje = 0;

        readonly BlIndicadores _indicadorBl = new BlIndicadores();

        protected BeUsuario ObjUsuario;
        protected BeResumenProceso ObjResumenBe;

        public int CodigoRolUsuario;
        public string PrefijoIsoPais;
        public string CodigoUsuario;
        public int IdProceso;

        public string AnioCampana;
        public string PeriodoCerrado;
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
            hftipoDialogoDesempenio.Value = Session["tipoDialogoDesempenio"].ToString();

            CargarVariables();
            CalcularAvanze();

            if (IsPostBack) return;

            if (Session["NombreEvaluado"] == null) return;

            string periodoEvaluacion = Session["periodoActual"] != null ? Session["periodoActual"].ToString() : ObjUsuario.periodoEvaluacion;

            CargarFormulario();
            CargarGerentes();

            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                Session["NombreEvaluado"].ToString(), periodoEvaluacion, "dialogo_antes_negocio.jpg");

            if (String.Equals(NmbrEvld, Constantes.Nueva))
            {
                lblNuevas.Text = NmbrEvld;

                if (EstadoProceso == 2)
                {
                    hlSiguiente.Visible = true;
                }


            }
        }

        protected void ddlVariableCausa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarDatosVariableCausaNuevas(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa1.SelectedValue, CadenaConexion) : _indicadorBl.CargarDatosVariableCausaEvaluado(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa1.SelectedValue, CadenaConexion);

            if (dtCampana != null)
            {
                if (dtCampana.Rows.Count > 0)
                {
                    TextBox5.Text = dtCampana.Rows[0]["decValorPlanPeriodo"].ToString();
                    TextBox6.Text = dtCampana.Rows[0]["decValorRealPeriodo"].ToString();
                    TextBox7.Text = dtCampana.Rows[0]["Diferencia"].ToString();
                    TextBox17.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Porcentaje"]);
                }
            }
        }

        protected void ddlVariableCausa2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarDatosVariableCausaNuevas(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa2.SelectedValue, CadenaConexion) : _indicadorBl.CargarDatosVariableCausaEvaluado(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa2.SelectedValue, CadenaConexion);

            if (dtCampana != null)
            {
                if (dtCampana.Rows.Count > 0)
                {
                    TextBox8.Text = dtCampana.Rows[0]["decValorPlanPeriodo"].ToString();
                    TextBox9.Text = dtCampana.Rows[0]["decValorRealPeriodo"].ToString();
                    TextBox10.Text = dtCampana.Rows[0]["Diferencia"].ToString();
                    TextBox18.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Porcentaje"]);
                }
            }
        }

        protected void ddlVariableCausa3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarDatosVariableCausaNuevas(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa3.SelectedValue, CadenaConexion) : _indicadorBl.CargarDatosVariableCausa(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa3.SelectedValue, CadenaConexion);

            if (dtCampana != null)
            {
                if (dtCampana.Rows.Count > 0)
                {
                    TextBox11.Text = dtCampana.Rows[0]["decValorPlanPeriodo"].ToString();
                    TextBox12.Text = dtCampana.Rows[0]["decValorRealPeriodo"].ToString();
                    TextBox13.Text = dtCampana.Rows[0]["Diferencia"].ToString();
                    TextBox19.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Porcentaje"]);
                }
            }
        }

        protected void ddlVariableCausa4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarDatosVariableCausaNuevas(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa4.SelectedValue, CadenaConexion) : _indicadorBl.CargarDatosVariableCausa(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, ddlVariableCausa4.SelectedValue, CadenaConexion);


            if (dtCampana != null)
            {
                if (dtCampana.Rows.Count > 0)
                {
                    TextBox14.Text = dtCampana.Rows[0]["decValorPlanPeriodo"].ToString();
                    TextBox15.Text = dtCampana.Rows[0]["decValorRealPeriodo"].ToString();
                    TextBox16.Text = dtCampana.Rows[0]["Diferencia"].ToString();
                    TextBox20.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Porcentaje"]);
                }
            }
        }


        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            contenidoPaso1.Visible = true;
            contenidoPaso2.Visible = false;
            contenedorFiltros.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool esCorrecto = _indicadorBl.InsertarIndicadoresEvaluado(txtIdVariable1.Text, txtIdVariable2.Text,
                ObjResumenBe.idProceso, AnioCampana, 0, CadenaConexion);

            if (esCorrecto)
            {
                esCorrecto = _indicadorBl.InsertarVariablesCausaEvaluado(IdProceso,
                   txtIdVariable1.Text, txtIdVariable2.Text,
                   ddlVariableCausa1.SelectedValue, ddlVariableCausa2.SelectedValue,
                   ddlVariableCausa3.SelectedValue, ddlVariableCausa4.SelectedValue,
                   TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text,
                   TextBox9.Text, TextBox10.Text, TextBox11.Text, TextBox12.Text,
                   TextBox13.Text, TextBox14.Text, TextBox15.Text, TextBox16.Text, CadenaConexion);

                EsCorrecto = esCorrecto ? 1 : 0;
            }
        }

        protected void cboVariablesAdicionales1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cboCombo = (DropDownList)sender;
            string valorActual = cboCombo.SelectedValue;

            DataTable dsReporte = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarindicadoresporperiodoVariablesAdicionalesNuevas(cboPeriodosFiltro.SelectedValue.Trim(), CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion).Tables[0] : _indicadorBl.CargarindicadoresporperiodoVariablesAdicionalesPreDialogo(cboPeriodosFiltro.SelectedValue.Trim(), CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion).Tables[0];


            DataRow[] filas = dsReporte.Select(string.Format("chrCodVariable = '{0}'", valorActual));

            if (filas.Length > 0)
            {
                DataRow filaActual = filas[0];

                if (cboCombo.ID == "cboVariablesAdicionales1")
                {
                    lblMeta1.Text = filaActual["decValorPlanPeriodo"].ToString();
                    lblResultado1.Text = filaActual["decValorRealPeriodo"].ToString();
                    lblDiferencia1.Text = filaActual["Diferencia"].ToString();
                    lblPorcentaje1.Text = string.Format("{0:F2}%", filaActual["Porcentaje"]);
                    lblCampanha1.Text = filaActual["chrAnioCampana"].ToString();
                }
                else
                {
                    lblMeta2.Text = filaActual["decValorPlanPeriodo"].ToString();
                    lblResultado2.Text = filaActual["decValorRealPeriodo"].ToString();
                    lblDiferencia2.Text = filaActual["Diferencia"].ToString();
                    lblPorcentaje2.Text = string.Format("{0:F2}%", filaActual["Porcentaje"]);
                    lblCampanha2.Text = filaActual["chrAnioCampana"].ToString();
                }
            }
            else
            {
                if (cboCombo.ID == "cboVariablesAdicionales1")
                {
                    lblMeta1.Text = "";
                    lblResultado1.Text = "";
                    lblDiferencia1.Text = "";
                    lblPorcentaje1.Text = "";
                    lblCampanha1.Text = "";
                }
                else
                {
                    lblMeta2.Text = "";
                    lblResultado2.Text = "";
                    lblDiferencia2.Text = "";
                    lblPorcentaje2.Text = "";
                    lblCampanha2.Text = "";
                }
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarFormulario()
        {
            CargarPeriodosFiltro();

            RealizarBusqueda();

            CargarVariablesSeleccionadas();
            CargarVariablesCausa();

            contenidoPaso1.Visible = true;
            contenidoPaso2.Visible = false;
        }

        private void CargarVariablesCausa()
        {
            DataTable variablesCausa = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarCombosVariablesCausaNuevas(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, lblvariable1.Text, lblvariable2.Text, CadenaConexion) : _indicadorBl.CargarCombosVariablesCausaEvaluado(PeriodoCerrado, CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, AnioCampana, lblvariable1.Text, lblvariable2.Text, CadenaConexion);

            ddlVariableCausa1.DataSource = variablesCausa;
            ddlVariableCausa1.DataTextField = "vchDesVariable";
            ddlVariableCausa1.DataValueField = "chrCodVariable";
            ddlVariableCausa1.DataBind();
            ddlVariableCausa1.Items.Insert(0, new ListItem("[SELECCIONE]", ""));

            ddlVariableCausa2.DataSource = variablesCausa;
            ddlVariableCausa2.DataTextField = "vchDesVariable";
            ddlVariableCausa2.DataValueField = "chrCodVariable";
            ddlVariableCausa2.DataBind();
            ddlVariableCausa2.Items.Insert(0, new ListItem("[SELECCIONE]", ""));

            ddlVariableCausa3.DataSource = variablesCausa;
            ddlVariableCausa3.DataTextField = "vchDesVariable";
            ddlVariableCausa3.DataValueField = "chrCodVariable";
            ddlVariableCausa3.DataBind();
            ddlVariableCausa3.Items.Insert(0, new ListItem("[SELECCIONE]", ""));

            ddlVariableCausa4.DataSource = variablesCausa;
            ddlVariableCausa4.DataTextField = "vchDesVariable";
            ddlVariableCausa4.DataValueField = "chrCodVariable";
            ddlVariableCausa4.DataBind();
            ddlVariableCausa4.Items.Insert(0, new ListItem("[SELECCIONE]", ""));

            #region Cargar Variables Causa si existieran en la BD

            DataTable variablesCausaIndicador1 = _indicadorBl.ObtenerVariablesCausaPreDialogo(IdProceso, txtIdVariable1.Text);
            DataTable variablesCausaIndicador2 = _indicadorBl.ObtenerVariablesCausaPreDialogo(IdProceso, txtIdVariable2.Text);

            if (variablesCausaIndicador1.Rows.Count > 0)
            {
                ddlVariableCausa1.SelectedValue = variablesCausaIndicador1.Rows[0].ItemArray[0].ToString().Trim();
                ddlVariableCausa1_SelectedIndexChanged(this, new EventArgs());
            }

            if (variablesCausaIndicador1.Rows.Count > 1)
            {
                ddlVariableCausa2.SelectedValue = variablesCausaIndicador1.Rows[1].ItemArray[0].ToString().Trim();
                ddlVariableCausa2_SelectedIndexChanged(this, new EventArgs());
            }

            if (variablesCausaIndicador2.Rows.Count > 1)
            {
                ddlVariableCausa3.SelectedValue = variablesCausaIndicador2.Rows[0].ItemArray[0].ToString().Trim();
                ddlVariableCausa3_SelectedIndexChanged(this, new EventArgs());
            }

            if (variablesCausaIndicador2.Rows.Count > 1)
            {
                ddlVariableCausa4.SelectedValue = variablesCausaIndicador2.Rows[1].ItemArray[0].ToString().Trim();
                ddlVariableCausa4_SelectedIndexChanged(this, new EventArgs());
            }

            #endregion Cargar Variables Causa si existieran en la BD
        }

        private void CargarVariables()
        {
            ObjResumenBe = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            ObjUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            CodigoRolUsuario = ObjResumenBe.codigoRolUsuario;
            PrefijoIsoPais = ObjResumenBe.prefijoIsoPais;
            CodigoUsuario = ObjResumenBe.codigoUsuario;

            IdProceso = ObjResumenBe.idProceso;

            EstadoProceso = int.Parse(ObjResumenBe.estadoProceso);

            if (Session["_soloLectura"] != null)
                ReadOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;


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

        private void CargarPeriodosFiltro()
        {
            DataTable periodosDt = _indicadorBl.ObtenerPeriodo(cboPeriodosFiltro.SelectedValue, ObjResumenBe.codigoRolUsuario, ObjResumenBe.prefijoIsoPais, CadenaConexion);

            var periodosList = (from DataRow periodo in periodosDt.Rows select periodo["chrPeriodo"].ToString()).ToList();
            Session["periodosValidos"] = periodosList;

            cboPeriodosFiltro.DataSource = periodosDt;
            cboPeriodosFiltro.DataTextField = "chrPeriodo";
            cboPeriodosFiltro.DataValueField = "chrPeriodo";
            cboPeriodosFiltro.DataBind();
            cboPeriodosFiltro.SelectedValue = Session["periodoActual"].ToString().PadRight(8, ' ');
        }



        private void ValidarPeriodoEvaluacion()
        {
            string periodoEvaluacion = Session["periodoActual"] != null ? Session["periodoActual"].ToString() : ObjUsuario.periodoEvaluacion;

            DataTable dtPeriodoEvaluacion = _indicadorBl.ValidarPeriodoEvaluacion(periodoEvaluacion, ObjUsuario.prefijoIsoPais, CodigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void CargarVariablesAdicionales()
        {
            DataSet dsReporte = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarindicadoresporperiodoVariablesAdicionalesNuevas(cboPeriodosFiltro.SelectedValue.Trim(), CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion) : _indicadorBl.CargarindicadoresporperiodoVariablesAdicionalesPreDialogo(cboPeriodosFiltro.SelectedValue.Trim(), CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion);

            cboVariablesAdicionales1.DataSource = dsReporte;
            cboVariablesAdicionales1.DataTextField = "vchDesVariable";
            cboVariablesAdicionales1.DataValueField = "chrCodVariable";
            cboVariablesAdicionales1.DataBind();

            cboVariablesAdicionales2.DataSource = dsReporte;
            cboVariablesAdicionales2.DataTextField = "vchDesVariable";
            cboVariablesAdicionales2.DataValueField = "chrCodVariable";
            cboVariablesAdicionales2.DataBind();

            cboVariablesAdicionales1_SelectedIndexChanged(cboVariablesAdicionales1, new EventArgs());
            cboVariablesAdicionales1_SelectedIndexChanged(cboVariablesAdicionales2, new EventArgs());

            int totalSeleccionados = 0;
            foreach (DataRow row in dsReporte.Tables[0].Rows)
            {
                bool seleccionado = bool.Parse(row.ItemArray[6].ToString());
                if (seleccionado)
                {
                    if (totalSeleccionados == 0)
                    {
                        cboVariablesAdicionales1.SelectedValue = row.ItemArray[0].ToString();

                        cboVariablesAdicionales1_SelectedIndexChanged(cboVariablesAdicionales1, new EventArgs());

                        chkEstadoVariableAdicional1.Checked = true;
                        totalSeleccionados++;
                    }
                    else if (totalSeleccionados == 1)
                    {
                        cboVariablesAdicionales2.SelectedValue = row.ItemArray[0].ToString();

                        cboVariablesAdicionales1_SelectedIndexChanged(cboVariablesAdicionales2, new EventArgs());

                        chkEstadoVariableAdicional2.Checked = true;
                        totalSeleccionados++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void CargarVariablesSeleccionadas()
        {
            int cantidadMarcados = 0;

            foreach (GridViewRow row in gvVariables.Rows)
            {
                if (cantidadMarcados == 2) return;

                var ch = (CheckBox)row.FindControl("cbEstado");
                if (ch.Checked)
                {
                    var celdaIdVariable = (DataControlFieldCell)row.Controls[0];
                    TableCell celdaNombreVariable = row.Cells[1];

                    string id = ((Label)celdaIdVariable.Controls[1]).Text;
                    string variableSeleccionada = celdaNombreVariable.Text;

                    if (cantidadMarcados == 0)
                    {
                        txtIdVariable1.Text = id;
                        lblvariable1Desc.Text = variableSeleccionada;
                    }
                    else
                    {
                        txtIdVariable2.Text = id;
                        lblvariable2Desc.Text = variableSeleccionada;
                    }

                    cantidadMarcados++;
                }
            }

            if (cantidadMarcados < 2)
            {
                switch (cantidadMarcados)
                {
                    case 0:
                        {
                            if (chkEstadoVariableAdicional1.Checked)
                            {
                                txtIdVariable1.Text = cboVariablesAdicionales1.SelectedValue;
                                lblvariable1Desc.Text = cboVariablesAdicionales1.SelectedItem.Text;
                            }

                            if (chkEstadoVariableAdicional2.Checked)
                            {
                                txtIdVariable2.Text = cboVariablesAdicionales2.SelectedValue;
                                lblvariable2Desc.Text = cboVariablesAdicionales2.SelectedItem.Text;
                            }
                            break;
                        }
                    case 1:
                        {
                            if (chkEstadoVariableAdicional1.Checked)
                            {
                                txtIdVariable2.Text = cboVariablesAdicionales1.SelectedValue;
                                lblvariable2Desc.Text = cboVariablesAdicionales1.SelectedItem.Text;
                            }

                            if (chkEstadoVariableAdicional2.Checked)
                            {
                                txtIdVariable2.Text = cboVariablesAdicionales2.SelectedValue;
                                lblvariable2Desc.Text = cboVariablesAdicionales2.SelectedItem.Text;
                            }
                            break;
                        }
                }
            }
        }

        private void CalcularAvanze()
        {
            Porcentaje = ProgresoHelper.CalcularAvanze(ObjResumenBe.idProceso, TipoPantalla.Antes);
            ObjResumenBe = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
        }

        private void CargarGerentes()
        {
            string periodo = cboPeriodosFiltro.SelectedValue;

            switch (CodigoRolUsuario)
            {
                case 5:
                    ddlGerentes.DataSource = _indicadorBl.CargarGerentesRegion(PrefijoIsoPais, periodo);
                    break;
                case 6:
                    ddlGerentes.DataSource = _indicadorBl.CargarGerentesZona(PrefijoIsoPais, ObjResumenBe.codigoUsuarioEvaluador, periodo);
                    break;
            }

            ddlGerentes.DataTextField = "Descripcion";
            ddlGerentes.DataValueField = "Codigo";
            ddlGerentes.DataBind();

            ListItem selectedListItem = ddlGerentes.Items.FindByValue(ObjResumenBe.codigoUsuario);

            if (selectedListItem != null)
            {
                selectedListItem.Selected = true;
            }
        }

        private void OcultarCampanha(bool valor)
        {
            gvVariables.Columns[6].Visible = valor;
            headerCampanha.Visible = valor;
            filaCampanha1.Visible = valor;
            filaCampanha2.Visible = valor;
            gvVariables.DataBind();
        }

        private void RealizarBusqueda()
        {
            DataSet dsReporte = String.Equals(NmbrEvld, Constantes.Nueva) ? _indicadorBl.CargarindicadoresporperiodoNuevas(cboPeriodosFiltro.SelectedValue.Trim(), CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion) : _indicadorBl.CargarindicadoresporperiodoPreDialogo(cboPeriodosFiltro.SelectedValue.Trim(), CodigoUsuario, IdProceso, CodigoRolUsuario, PrefijoIsoPais, CadenaConexion);


            gvVariables.DataSource = dsReporte;
            gvVariables.DataBind();
            CargarVariablesAdicionales();
            OcultarCampanha(false);


        }

        #endregion Metodos

        protected void cboPeriodosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGerentes();
        }

        protected void btnAceptarAux_Click(object sender, EventArgs e)
        {
            string modeloDialogo = String.Empty;

            if (rbNormal.Checked)
            {
                modeloDialogo = "MNOR";
            }

            if (rbPlanMejora.Checked)
            {
                modeloDialogo = "MPDM";
            }

            if (!String.IsNullOrEmpty(modeloDialogo))
            {
                _indicadorBl.ActualizaModeloDialogo(IdProceso, modeloDialogo);
            }

            CargarVariablesSeleccionadas();
            contenidoPaso1.Visible = false;
            contenedorFiltros.Visible = false;
            contenidoPaso2.Visible = true;
        }

    }
}