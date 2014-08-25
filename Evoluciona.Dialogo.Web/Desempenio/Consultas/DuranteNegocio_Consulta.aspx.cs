
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;

    public partial class DuranteNegocio_Consulta : Page
    {
        #region Variables

        protected BeResumenProceso objResumenBE;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            BlResumenProceso objResumenBL = new BlResumenProceso();
            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (IsPostBack) return;

            string periodoEvaluacion = Utils.QueryString("periodo");

            if (string.IsNullOrEmpty(txtIdProceso.Text))
            {
                string tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();
                BeResumenProceso objResumenNuevo = objResumenBL.ObtenerResumenProcesoByUsuario(
                    objResumenBE.codigoUsuario,
                    objResumenBE.rolUsuario,
                    periodoEvaluacion,
                    objUsuario.prefijoIsoPais,
                    tipoDialogoDesempenio);

                if (objResumenNuevo != null)
                    objResumenBE.idProceso = objResumenNuevo.idProceso;
            }
            else
            {
                objResumenBE.idProceso = int.Parse(txtIdProceso.Text);
            }

            cboEstadoIndicador1.SelectedIndex = 0;
            cboEstadoIndicador2.SelectedIndex = 0;

            CargarIndicadores();
        }

        #endregion

        #region Eventos

        private void CargarIndicadores()
        {
            string idsVariablesIndicador = string.Empty;
            string idsVariablesEnfoque = string.Empty;

            BlIndicadores objIndicadorBL = new BlIndicadores();
            BlVariableEnfoque objVarEnfoquesBL = new BlVariableEnfoque();
            DataTable dtIndicadores = objIndicadorBL.ObtenerIndicadoresProcesados(objResumenBE.idProceso);

            if (dtIndicadores.Rows.Count > 0)
            {
                string campaniaProcesada = dtIndicadores.Rows[0]["chrAnioCampanha"].ToString();

                #region Iterar DT Indicadores

                for (int x = 0; x < dtIndicadores.Rows.Count; x++)
                {
                    string idVariable = dtIndicadores.Rows[x]["intIDIndicador"].ToString();
                    string idVarEnfoque = string.Empty;

                    if (x == 0)
                    {
                        lblVariable1.Text = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();

                        List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(idVariable));

                        if (lstVariableEnfoque.Count > 0)
                        {
                            txtCampania1.Text = lstVariableEnfoque[0].campania;
                            txtZonas1.Text = lstVariableEnfoque[0].zonas;
                            idVarEnfoque = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                            txtPlanAccion1.Text = lstVariableEnfoque[0].planAccion;
                        }
                    }
                    if (x == 1)
                    {
                        lblVariable2.Text = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();
                        idVariable = dtIndicadores.Rows[x]["intIDIndicador"].ToString();

                        List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(idVariable));
                        if (lstVariableEnfoque.Count > 0)
                        {
                            txtCampania2.Text = lstVariableEnfoque[0].campania;
                            txtZonas2.Text = lstVariableEnfoque[0].zonas;
                            idVarEnfoque = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                            txtPlanAccion2.Text = lstVariableEnfoque[0].planAccion;
                        }
                    }

                    idsVariablesIndicador += "," + idVariable;
                    idsVariablesEnfoque += "," + idVarEnfoque;
                }

                #endregion

                #region Cargar Variables de Enfoque

                DataTable dtDescripcionVariableEnfoque1 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblVariable1.Text.Trim(), campaniaProcesada, objResumenBE.periodo);
                if (dtDescripcionVariableEnfoque1 != null)
                {
                    if (dtDescripcionVariableEnfoque1.Rows.Count > 0)
                    {
                        lblVariableGeneral1.Text = dtDescripcionVariableEnfoque1.Rows[0]["vchDesVariable"].ToString();

                    }
                }

                DataTable dtDescripcionVariableEnfoque2 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblVariable2.Text.Trim(), campaniaProcesada, objResumenBE.periodo);
                if (dtDescripcionVariableEnfoque2 != null)
                {
                    if (dtDescripcionVariableEnfoque2.Rows.Count > 0)
                    {
                        lblVariableGeneral2.Text = dtDescripcionVariableEnfoque2.Rows[0]["vchDesVariable"].ToString();

                    }
                }

                DataTable dtVariablesCausa1 = objIndicadorBL.ObtenerVariablesCausa(objResumenBE.idProceso, lblVariable1.Text.Trim());

                if (dtVariablesCausa1.Rows.Count > 0)
                {
                    lblvariableCausa1.Text = dtVariablesCausa1.Rows[0]["chrCodVariableHija"].ToString();

                    if (dtVariablesCausa1.Rows.Count > 1)
                    {
                        lblvariableCausa2.Text = dtVariablesCausa1.Rows[1]["chrCodVariableHija"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque3 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblvariableCausa1.Text.Trim(), campaniaProcesada, objResumenBE.periodo);
                if (dtDescripcionVariableEnfoque3 != null)
                {
                    if (dtDescripcionVariableEnfoque3.Rows.Count > 0)
                    {
                        Label3.Text = dtDescripcionVariableEnfoque3.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque4 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblvariableCausa2.Text.Trim(), campaniaProcesada, objResumenBE.periodo);
                if (dtDescripcionVariableEnfoque4 != null)
                {
                    if (dtDescripcionVariableEnfoque4.Rows.Count > 0)
                    {
                        Label4.Text = dtDescripcionVariableEnfoque4.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtVariablesCausa2 = objIndicadorBL.ObtenerVariablesCausa(objResumenBE.idProceso, lblVariable2.Text.Trim());

                if (dtVariablesCausa2.Rows.Count > 0)
                {
                    lblvariableCausa3.Text = dtVariablesCausa2.Rows[0]["chrCodVariableHija"].ToString();

                    if (dtVariablesCausa2.Rows.Count > 1)
                    {
                        lblvariableCausa4.Text = dtVariablesCausa2.Rows[1]["chrCodVariableHija"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque5 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblvariableCausa3.Text.Trim(), campaniaProcesada, objResumenBE.periodo);
                if (dtDescripcionVariableEnfoque5 != null)
                {
                    if (dtDescripcionVariableEnfoque5.Rows.Count > 0)
                    {
                        Label5.Text = dtDescripcionVariableEnfoque5.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque6 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(lblvariableCausa4.Text.Trim(), campaniaProcesada, objResumenBE.periodo);
                if (dtDescripcionVariableEnfoque6 != null)
                {
                    if (dtDescripcionVariableEnfoque6.Rows.Count > 0)
                    {
                        Label6.Text = dtDescripcionVariableEnfoque6.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                #endregion
            }
        }

        #endregion
    }
}