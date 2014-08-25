
namespace Evoluciona.Dialogo.Web.Visita
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Dialogo.Helpers;
    using BusinessLogic;
    using BusinessEntity;

    public partial class visitaindicadores_Consulta : Page
    {
        #region Variables

        private BeResumenVisita objVisita = new BeResumenVisita();
        private BlIndicadores indicadorBL = new BlIndicadores();
        private BlVisitaIndicadores visitaIndicadoresBL = new BlVisitaIndicadores();
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

            string periodo = Request["periodo"];

            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerCodigoVisita(objVisita.codigoUsuario, objVisita.codigoUsuarioEvaluador, objVisita.idRolUsuario, periodo);
            if (dtVisita.Rows.Count > 0)
            {
                objVisita.idVisita = Convert.ToInt32(dtVisita.Rows[0]["codigoVisita"]);
                objVisita.estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                objVisita.idProceso = Convert.ToInt32(dtVisita.Rows[0]["intIDProceso"]);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_accionesConsulta", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }

            if (Page.IsPostBack) return;

            BlInteraccion blProceso = new BlInteraccion();
            DataTable dtGrabadas = blProceso.ObtenerInteraccionGrabadas(CadenaConexion, objVisita.idVisita);
            if (dtGrabadas.Rows.Count > 0)
            {
                txtObjetivosVisita.Text = dtGrabadas.Rows[0]["vchObjetivoVisita"].ToString();
            }

            CargarVariablesNegocio();

            if (objVisita.codigoRolUsuario == Constantes.RolGerenteRegion)
            {
                descRol = "GR";
            }
            else if (objVisita.codigoRolUsuario == Constantes.RolGerenteZona)
            {
                descRol = "GZ";
            }
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

        #endregion Eventos

        #region Metodos

        private void CargarVariablesNegocio()
        {
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

        #endregion Metodos
    }
}