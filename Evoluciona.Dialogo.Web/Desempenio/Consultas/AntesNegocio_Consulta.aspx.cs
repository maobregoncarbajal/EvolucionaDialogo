
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class AntesNegocio_Consulta : Page
    {
        #region Variables

        public int esCorrecto = 0;

        protected BlIndicadores indicadorBL = new BlIndicadores();
        protected BeUsuario objUsuario;
        protected BeResumenProceso objResumenBE;

        public int codigoRolUsuario;
        public string prefijoIsoPais;
        public string codigoUsuario;
        public int idProceso;
        public string PeriodoEvaluacion;
        public string AnioCampana;
        public string PeriodoCerrado;

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
            CargarVariables();

            if (IsPostBack) return;
            if (Session["NombreEvaluado"] == null) return;

            CargarFormulario();
        }

        protected void ddlVariableCausa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(PeriodoCerrado, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, AnioCampana, ddlVariableCausa1.SelectedValue, CadenaConexion);
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
            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(PeriodoCerrado, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, AnioCampana, ddlVariableCausa2.SelectedValue, CadenaConexion);
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
            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(PeriodoCerrado, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, AnioCampana, ddlVariableCausa3.SelectedValue, CadenaConexion);
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
            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(PeriodoCerrado, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, AnioCampana, ddlVariableCausa4.SelectedValue, CadenaConexion);
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            CargarVariablesSeleccionadas();

            contenidoPaso1.Visible = false;
            contenedorFiltros.Visible = false;
            contenidoPaso2.Visible = true;
        }

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            contenidoPaso1.Visible = true;
            contenidoPaso2.Visible = false;
            contenedorFiltros.Visible = true;
        }

        protected void cboPeriodosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCampañasFiltro();
            RealizarBusqueda();
        }

        protected void cboCampanhasFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }

        protected void cboVariablesAdicionales1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboCombo = (DropDownList)sender;
            string valorActual = cboCombo.SelectedValue;

            DataTable dsReporte = new DataTable();
            if (cboCampanhasFiltro.SelectedIndex == 0)
            {
                dsReporte = indicadorBL.CargarindicadoresporperiodoVariablesAdicionales(cboPeriodosFiltro.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion).Tables[0];
            }
            else
            {
                dsReporte = indicadorBL.CargarindicadoresporcampanaVariablesAdicionales2(cboCampanhasFiltro.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion).Tables[0];
            }
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
            CargarCampañasFiltro();

            RealizarBusqueda();

            CargarVariablesSeleccionadas();
            CargarVariablesCausa();

            contenidoPaso1.Visible = true;
            contenidoPaso2.Visible = false;
        }

        private void CargarVariablesCausa()
        {
            DataTable variablesCausa = indicadorBL.CargarCombosVariablesCausa(PeriodoCerrado, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, AnioCampana, lblvariable1.Text, lblvariable2.Text, CadenaConexion);

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

            DataTable variablesCausaIndicador1 = indicadorBL.ObtenerVariablesCausa(idProceso, lblvariable1.Text);
            DataTable variablesCausaIndicador2 = indicadorBL.ObtenerVariablesCausa(idProceso, lblvariable2.Text);

            if (variablesCausaIndicador1.Rows.Count > 0)
            {
                ddlVariableCausa1.SelectedValue = variablesCausaIndicador1.Rows[0].ItemArray[0].ToString();
                ddlVariableCausa1_SelectedIndexChanged(this, new EventArgs());
            }

            if (variablesCausaIndicador1.Rows.Count > 1)
            {
                ddlVariableCausa2.SelectedValue = variablesCausaIndicador1.Rows[1].ItemArray[0].ToString();
                ddlVariableCausa2_SelectedIndexChanged(this, new EventArgs());
            }

            if (variablesCausaIndicador2.Rows.Count > 0)
            {
                ddlVariableCausa3.SelectedValue = variablesCausaIndicador2.Rows[0].ItemArray[0].ToString();
                ddlVariableCausa3_SelectedIndexChanged(this, new EventArgs());
            }

            if (variablesCausaIndicador2.Rows.Count > 1)
            {
                ddlVariableCausa4.SelectedValue = variablesCausaIndicador2.Rows[1].ItemArray[0].ToString();
                ddlVariableCausa4_SelectedIndexChanged(this, new EventArgs());
            }

            #endregion Cargar Variables Causa si existieran en la BD
        }

        private void CargarVariables()
        {
            BlResumenProceso objResumenBL = new BlResumenProceso();

            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            codigoRolUsuario = objResumenBE.codigoRolUsuario;
            prefijoIsoPais = objResumenBE.prefijoIsoPais;
            codigoUsuario = objResumenBE.codigoUsuario;

            PeriodoEvaluacion = Utils.QueryString("periodo");

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();
                BeResumenProceso objResumen = objResumenBL.ObtenerResumenProcesoByUsuario(codigoUsuario, objResumenBE.rolUsuario,
                    PeriodoEvaluacion, objUsuario.prefijoIsoPais, tipoDialogoDesempenio);

                idProceso = objResumen != null ? objResumen.idProceso : 0;
            }
            else
            {
                idProceso = int.Parse(txtIdProceso.Text);
            }

            ValidarPeriodoEvaluacion();
        }

        private void CargarPeriodosFiltro()
        {
            cboPeriodosFiltro.DataSource = indicadorBL.ObtenerPeriodo(cboPeriodosFiltro.SelectedValue, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
            cboPeriodosFiltro.DataTextField = "chrPeriodo";
            cboPeriodosFiltro.DataValueField = "chrPeriodo";
            cboPeriodosFiltro.DataBind();
            cboPeriodosFiltro.SelectedValue = PeriodoEvaluacion.PadRight(8, ' ');
        }

        private void CargarCampañasFiltro()
        {
            if (string.IsNullOrEmpty(cboPeriodosFiltro.SelectedValue))
            {
                cboCampanhasFiltro.DataSource = null;
                cboCampanhasFiltro.DataBind();
                cboCampanhasFiltro.Items.Insert(0, new ListItem("[Acumulado del Periodo]", ""));
                return;
            }

            cboCampanhasFiltro.DataSource = indicadorBL.ObtenerCampanaDesde(string.Empty,
                codigoRolUsuario, prefijoIsoPais, cboPeriodosFiltro.SelectedValue, CadenaConexion);

            cboCampanhasFiltro.DataTextField = "chrAnioCampana";
            cboCampanhasFiltro.DataValueField = "chrAnioCampana";
            cboCampanhasFiltro.DataBind();

            cboCampanhasFiltro.Items.Insert(0, new ListItem("[Acumulado del Periodo]", ""));
        }

        private void ValidarPeriodoEvaluacion()
        {
            DataTable dtPeriodoEvaluacion = indicadorBL.ValidarPeriodoEvaluacion(PeriodoEvaluacion, prefijoIsoPais, codigoRolUsuario, CadenaConexion);
            if (dtPeriodoEvaluacion != null)
            {
                AnioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                PeriodoCerrado = dtPeriodoEvaluacion.Rows[0]["chrPeriodo"].ToString();
            }
        }

        private void CargarIndicadores()
        {
            gvVariables.DataBind();

            if (!string.IsNullOrEmpty(cboPeriodosFiltro.SelectedValue))
            {
                DataSet dsReporte = indicadorBL.Cargarindicadoresporperiodo(cboPeriodosFiltro.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);

                gvVariables.DataSource = dsReporte;
                gvVariables.DataBind();
                OcultarCampanha(false);
            }
        }

        private void CargarVariablesAdicionales()
        {
            DataSet dsReporte = indicadorBL.CargarindicadoresporperiodoVariablesAdicionales(cboPeriodosFiltro.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);

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

        private void CargarVariablesAdicionalesPorCampana()
        {
            DataSet dsReporte = indicadorBL.CargarindicadoresporcampanaVariablesAdicionales2(cboCampanhasFiltro.SelectedValue, codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);

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

                CheckBox ch = (CheckBox)row.FindControl("cbEstado");
                if (ch.Checked)
                {
                    DataControlFieldCell celdaIdVariable = (DataControlFieldCell)row.Controls[0];
                    TableCell celdaNombreVariable = (TableCell)row.Cells[1];

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

                            if (cboVariablesAdicionales1.SelectedValue == null)
                            {

                                chkEstadoVariableAdicional1.Checked = false;

                            }
                            if (cboVariablesAdicionales2.SelectedValue == null)
                            {

                                chkEstadoVariableAdicional1.Checked = false;
                            }
                            break;
                    case 1:

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
                        
                    case 2:
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
                    default:
                        break;
                }
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
            if (cboCampanhasFiltro.SelectedIndex == 0)
            {
                DataSet dsReporte = indicadorBL.Cargarindicadoresporperiodo(cboPeriodosFiltro.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
                gvVariables.DataSource = dsReporte;
                gvVariables.DataBind();
                CargarVariablesAdicionales();
                OcultarCampanha(false);

                if (cboCampanhasFiltro.Items.Count == 0) return;
                if (cboCampanhasFiltro.Items.Count > 1)
                    litMensajeResultado.Text = string.Format("Datos de la campaña {0} a la campaña {1}", cboCampanhasFiltro.Items[1].Text, cboCampanhasFiltro.Items[cboCampanhasFiltro.Items.Count - 1].Text);
                else
                    litMensajeResultado.Text = string.Format("Datos de la campaña {0}", cboCampanhasFiltro.Items[1].Text);
            }
            else
            {
                DataSet dsReporte = indicadorBL.Cargarindicadoresporcampana(cboCampanhasFiltro.SelectedValue.Trim(), codigoUsuario, idProceso, codigoRolUsuario, prefijoIsoPais, CadenaConexion);
                gvVariables.DataSource = dsReporte;
                gvVariables.DataBind();
                CargarVariablesAdicionalesPorCampana();
                OcultarCampanha(true);

                litMensajeResultado.Text = string.Format("Datos de la campaña {0}", cboCampanhasFiltro.SelectedValue);
            }
        }

        #endregion Metodos
    }
}