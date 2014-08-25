
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Controls;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class visitaindicadores : Page
    {
        #region Variables

        private BeResumenVisita objVisita = new BeResumenVisita();
        private BlIndicadores indicadorBL = new BlIndicadores();
        private BlVisitaIndicadores visitaIndicadoresBL = new BlVisitaIndicadores();
        public int indexMenuServer;
        public int indexSubMenu;
        public string descRol;

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
            objVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            indexMenuServer = Convert.ToInt32(Request["indiceM"]);
            indexSubMenu = Convert.ToInt32(Request["indiceSM"]);

            if (Page.IsPostBack) return;

            BlInteraccion blProceso = new BlInteraccion();
            DataTable dtGrabadas = blProceso.ObtenerInteraccionGrabadas(CadenaConexion, objVisita.idVisita);
            if (dtGrabadas.Rows.Count > 0)
            {
                txtObjetivosVisita.Text = dtGrabadas.Rows[0]["vchObjetivoVisita"].ToString();
            }

            if (Request["paso"] == "1")
            {
                CargarVariablesNegocio();
            }
            else if (Request["paso"] == "2")
            {
                CargarVariablesCausa();
            }

            if (objVisita.codigoRolUsuario == Constantes.RolGerenteRegion)
            {
                descRol = "GR";
            }
            else if (objVisita.codigoRolUsuario == Constantes.RolGerenteZona)
            {
                descRol = "GZ";
            }
            if (Session[Constantes.VisitaModoLectura] != null)
            {
                btnGrabar.Text = "CONTINUAR";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                GuardarIndicadoresVisita();

                if (objVisita.estadoVisita == Constantes.EstadoVisitaActivo && objVisita.porcentajeAvanceAntes == 40)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objVisita.idVisita, 60, 1);
                    objVisita.porcentajeAvanceAntes = 60;
                    Session[Constantes.ObjUsuarioVisitado] = objVisita;
                }
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "_VisitaIndicadores", "<script language='javascript'> javascript:AbrirMensaje1(); </script>");
        }

        protected void cboPeriodosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCampañasFiltro();
            CargarVariablesBase();
            CargarVariablesAdicionales();
            CargarIndicadoresVisita();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVariablesBase();
            CargarVariablesAdicionales();
            CargarIndicadoresVisita();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session[Constantes.VisitaModoLectura] == null)
            {
                GuardarVariablesCausales();

                if (objVisita.estadoVisita == Constantes.EstadoVisitaActivo && objVisita.porcentajeAvanceAntes == 60)
                {
                    BlResumenVisita blResumen = new BlResumenVisita();
                    blResumen.ActualizarAvanceVisita(objVisita.idVisita, 80, 1);
                    objVisita.porcentajeAvanceAntes = 80;
                    Session[Constantes.ObjUsuarioVisitado] = objVisita;
                }
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "_VisitaIndicadores", "<script language='javascript'> javascript:AbrirMensaje2(); </script>");
        }

        #endregion Eventos

        #region Metodos

        private void CargarVariablesNegocio()
        {
            contenidoPaso1.Visible = true;
            contenidoPaso2.Visible = false;

            CargarPeriodosFiltro();
            CargarCampañasFiltro();
            CargarVariablesBase();
            CargarVariablesAdicionales();
            CargarIndicadoresVisita();
        }

        private void CargarVariablesBase()
        {
            if (cboCampanhasFiltro.SelectedIndex == 0)
            {
                DataSet dsgrdvVariablesBase =
                    indicadorBL.Cargarindicadoresporperiodo(cboPeriodosFiltro.SelectedValue.Trim(),
                                                            objVisita.codigoUsuario,
                                                            objVisita.idProceso, objVisita.codigoRolUsuario,
                                                            objVisita.prefijoIsoPais, CadenaConexion);
                grdvVariablesBase.Columns[5].Visible = false;
                grdvVariablesBase.DataSource = dsgrdvVariablesBase;
                grdvVariablesBase.DataBind();
            }
            else
            {
                DataSet dsgrdvVariablesBase =
                    indicadorBL.Cargarindicadoresporcampana(cboCampanhasFiltro.SelectedValue.Trim(),
                                                            objVisita.codigoUsuario, objVisita.idProceso,
                                                            objVisita.codigoRolUsuario, objVisita.prefijoIsoPais,
                                                            CadenaConexion);
                grdvVariablesBase.Columns[5].Visible = true;
                grdvVariablesBase.DataSource = dsgrdvVariablesBase;
                grdvVariablesBase.DataBind();
            }

            foreach (GridViewRow row in grdvVariablesBase.Rows)
            {
                CheckBox chb = (CheckBox)row.FindControl("cbEstado");
                if (chb.Checked)
                    chb.Enabled = false;
            }
        }

        private void CargarVariablesAdicionales()
        {
            if (cboCampanhasFiltro.SelectedIndex == 0)
            {
                DataSet dsgrdvVariablesAdicionales =
                    indicadorBL.CargarindicadoresporperiodoVariablesAdicionales(cboPeriodosFiltro.SelectedValue.Trim(),
                                                                                objVisita.codigoUsuario,
                                                                                objVisita.idProceso,
                                                                                objVisita.codigoRolUsuario,
                                                                                objVisita.prefijoIsoPais, CadenaConexion);
                grdvVariablesAdicionales.Columns[5].Visible = false;
                grdvVariablesAdicionales.DataSource = dsgrdvVariablesAdicionales;
                grdvVariablesAdicionales.DataBind();
            }
            else
            {
                DataSet dsgrdvVariablesAdicionales =
                    indicadorBL.CargarindicadoresporcampanaVariablesAdicionales2(cboCampanhasFiltro.SelectedValue,
                                                                                 objVisita.codigoUsuario,
                                                                                 objVisita.idProceso,
                                                                                 objVisita.codigoRolUsuario,
                                                                                 objVisita.prefijoIsoPais,
                                                                                 CadenaConexion);
                grdvVariablesAdicionales.Columns[5].Visible = true;
                grdvVariablesAdicionales.DataSource = dsgrdvVariablesAdicionales;
                grdvVariablesAdicionales.DataBind();
            }

            foreach (GridViewRow row in grdvVariablesAdicionales.Rows)
            {
                CheckBox chb = (CheckBox)row.FindControl("cbEstado");
                if (chb.Checked)
                    chb.Enabled = false;
            }
        }

        private void CargarPeriodosFiltro()
        {
            BlIndicadores indicadorBL = new BlIndicadores();
            cboPeriodosFiltro.DataSource = indicadorBL.ObtenerPeriodo(cboPeriodosFiltro.SelectedValue,
                                                                      objVisita.codigoRolUsuario,
                                                                      objVisita.prefijoIsoPais, CadenaConexion);
            cboPeriodosFiltro.DataTextField = "chrPeriodo";
            cboPeriodosFiltro.DataValueField = "chrPeriodo";
            cboPeriodosFiltro.DataBind();
            cboPeriodosFiltro.SelectedValue = Session["periodoActual"].ToString().PadRight(8, ' ');
        }

        private void CargarCampañasFiltro()
        {
            if (string.IsNullOrEmpty(cboPeriodosFiltro.SelectedValue))
            {
                cboCampanhasFiltro.DataSource = null;
                cboCampanhasFiltro.DataBind();
                cboCampanhasFiltro.Items.Insert(0, new ListItem("[TODOS]", ""));
                return;
            }

            BlIndicadores indicadorBL = new BlIndicadores();
            cboCampanhasFiltro.DataSource = indicadorBL.ObtenerCampanaDesde(cboCampanhasFiltro.SelectedValue,
                                                                            objVisita.codigoRolUsuario,
                                                                            objVisita.prefijoIsoPais,
                                                                            cboPeriodosFiltro.SelectedValue,
                                                                            CadenaConexion);

            cboCampanhasFiltro.DataTextField = "chrAnioCampana";
            cboCampanhasFiltro.DataValueField = "chrAnioCampana";
            cboCampanhasFiltro.DataBind();

            cboCampanhasFiltro.Items.Insert(0, new ListItem("[TODOS]", ""));
        }

        private void CargarIndicadoresVisita()
        {
            DataSet dsIndicadoresVisita = visitaIndicadoresBL.ObtenerIndicadoresVisita(objVisita.idProceso);

            if (dsIndicadoresVisita.Tables.Count > 0)
            {
                foreach (DataRow row in dsIndicadoresVisita.Tables[0].Rows)
                {
                    bool seleccionado = bool.Parse(row.ItemArray[3].ToString());
                    if (seleccionado)
                    {
                        foreach (GridViewRow rowGrid in grdvVariablesBase.Rows)
                        {
                            DataControlFieldCell celdaIdVariable = (DataControlFieldCell)rowGrid.Controls[0];
                            string codigo = ((Label)celdaIdVariable.Controls[1]).Text;

                            if (codigo.Trim() == row.ItemArray[2].ToString().Trim())
                            {
                                CheckBox chb = (CheckBox)rowGrid.FindControl("cbEstado");
                                chb.Checked = true;
                                break;
                            }
                        }

                        foreach (GridViewRow rowGrid in grdvVariablesAdicionales.Rows)
                        {
                            DataControlFieldCell celdaIdVariable = (DataControlFieldCell)rowGrid.Controls[0];
                            string codigo = ((Label)celdaIdVariable.Controls[1]).Text;

                            if (codigo.Trim() == row.ItemArray[2].ToString().Trim())
                            {
                                CheckBox chb = (CheckBox)rowGrid.FindControl("cbEstado");
                                chb.Checked = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void CargarVariablesCausa()
        {
            contenidoPaso1.Visible = false;
            contenidoPaso2.Visible = true;

            DataSet dsIndicadoresVisita = visitaIndicadoresBL.ObtenerIndicadoresVisita(objVisita.idProceso);
            repVariablesCausales.DataSource = dsIndicadoresVisita;
            repVariablesCausales.DataBind();
        }

        private void GuardarIndicadoresVisita()
        {
            visitaIndicadoresBL.EliminarIndicadoresVisita(objVisita.idProceso);

            foreach (GridViewRow row in grdvVariablesBase.Rows)
            {
                CheckBox chb = (CheckBox)row.FindControl("cbEstado");
                if (chb.Checked && chb.Enabled)
                {
                    DataControlFieldCell celdaIdVariable = (DataControlFieldCell)row.Controls[0];

                    BeVisitaIndicador indicador = new BeVisitaIndicador();
                    indicador.IdProceso = objVisita.idProceso;
                    indicador.Seleccioando = ((Label)celdaIdVariable.Controls[1]).Text;
                    indicador.Estado = true;
                    indicador.AnhoCampanha = objVisita.campania;
                    indicador.UsuarioCreacion = objVisita.usuarioSistema;

                    visitaIndicadoresBL.InsertarIndicadorVisita(indicador);
                }
            }

            foreach (GridViewRow row in grdvVariablesAdicionales.Rows)
            {
                CheckBox chb = (CheckBox)row.FindControl("cbEstado");
                if (chb.Checked && chb.Enabled)
                {
                    DataControlFieldCell celdaIdVariable = (DataControlFieldCell)row.Controls[0];

                    BeVisitaIndicador indicador = new BeVisitaIndicador();
                    indicador.IdProceso = objVisita.idProceso;
                    indicador.Seleccioando = ((Label)celdaIdVariable.Controls[1]).Text;
                    indicador.Estado = true;
                    indicador.AnhoCampanha = objVisita.campania;
                    indicador.UsuarioCreacion = objVisita.usuarioSistema;

                    visitaIndicadoresBL.InsertarIndicadorVisita(indicador);
                }
            }
        }

        private void GuardarVariablesCausales()
        {
            indicadorBL.EliminarVariableCausaVista(objVisita.idProceso);

            foreach (RepeaterItem item in repVariablesCausales.Items)
            {
                VariableCausaVisita uc = (VariableCausaVisita)item.FindControl("varibaleCausa");
                if (uc == null) continue;

                List<BeVariableCausa> variablesCausa = uc.ObtenerVariablesCausa();

                foreach (BeVariableCausa variableCausa in variablesCausa)
                {
                    indicadorBL.InsertarVariableCausaVista(variableCausa);
                }
            }
        }

        #endregion Metodos
    }
}