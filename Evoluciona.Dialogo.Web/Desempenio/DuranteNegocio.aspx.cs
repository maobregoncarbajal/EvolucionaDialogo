
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helper;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;

    public partial class DuranteNegocio : Page
    {
        #region Variables

        public int indexMenuServer = 2;
        public int indexSubMenu = 1;
        public int esCorrecto = 0;
        public int estadoProceso = 0;
        public int readOnly = 0;
        public int porcentaje = 0;
        protected BeResumenProceso objResumenBE;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            estadoProceso = int.Parse(objResumenBE.estadoProceso);

            CalcularAvanze();

            #region Verificar si se esta modificando o Revisando la informacion

            if (Session["_soloLectura"] != null)
            {
                readOnly = (bool.Parse(Session["_soloLectura"].ToString())) ? 1 : 0;
            }

            #endregion

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            string nombreImagen = string.Empty;

            #region Asignando los Valores correctos al Mensaje

            if (Utils.QueryStringInt("aprobacion") == 1)
            {
                indexMenuServer = 3;
                lblAccion.Text = "DESPUÉS";
                hlkUrl.Text = "DIÁLOGO/DESPUÉS/EQUIPOS";
                hlkUrl.NavigateUrl = Utils.AbsoluteWebRoot + "Desempenio/DuranteEquipos.aspx?aprobacion=1";
                nombreImagen = "dialogo_despues_negocio.jpg";
            }
            else
            {
                lblAccion.Text = "DURANTE";
                hlkUrl.Text = "DIÁLOGO/DURANTE/EQUIPOS";
                hlkUrl.NavigateUrl = Utils.AbsoluteWebRoot + "Desempenio/DuranteEquipos.aspx";
                nombreImagen = "dialogo_durante_negocio.jpg";
            }

            #endregion

            if (IsPostBack) return;
            if (Session["NombreEvaluado"] == null) return;

            #region Cargando Header y Asignando Periodo de Evaluacion

            string periodoEvaluacion = string.Empty;
            if (Session["periodoActual"] != null)
                periodoEvaluacion = Session["periodoActual"].ToString();
            else
                periodoEvaluacion = objUsuario.periodoEvaluacion;

            hderOperaciones.CargarControles((List<string>)Session["periodosValidos"],
                Session["NombreEvaluado"].ToString(), periodoEvaluacion, nombreImagen);

            #endregion

            cboEstadoIndicador1.SelectedIndex = 0;
            cboEstadoIndicador2.SelectedIndex = 0;

            CargarDescripcionUbicacionRol();

            CargarIndicadores();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertarVariableEnfoques();
        }

        #endregion

        #region Metodos

        private void CargarDescripcionUbicacionRol()
        {
            string ubicacionRol = string.Empty;

            if (objResumenBE.codigoRolUsuario == Constantes.RolGerenteZona)
                ubicacionRol = "Secciones";
            else
                ubicacionRol = "Zonas";

            lblDescripcionLugarRol.Text = ltUbicacionRol_1.Text = ltUbicacionRol_2.Text = ubicacionRol;
        }

        private void CargarIndicadores()
        {
            string idsVariablesIndicador = string.Empty;
            string idsVariablesEnfoque = string.Empty;

            BlIndicadores objIndicadorBL = new BlIndicadores();
            BlVariableEnfoque objVarEnfoquesBL = new BlVariableEnfoque();
            DataTable dtIndicadores = objIndicadorBL.ObtenerIndicadoresProcesados(objResumenBE.idProceso);

            if (dtIndicadores.Rows.Count > 0)
            {
                #region Iterar DT Indicadores
                string campaniaProcesada = dtIndicadores.Rows[0]["chrAnioCampanha"].ToString();
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

                            cboEstadoIndicador1.SelectedValue = lstVariableEnfoque[0].postDialogo ? "1" : "0";
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

                            cboEstadoIndicador2.SelectedValue = lstVariableEnfoque[0].postDialogo ? "1" : "0";
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

                Session["valoresCargados"] = idsVariablesIndicador.Substring(1) + "|" + idsVariablesEnfoque.Substring(1);
            }
        }

        private void InsertarVariableEnfoques()
        {
            bool _esCorrectoActual = true;

            string ids = Session["valoresCargados"] == null ? string.Empty : Session["valoresCargados"].ToString();

            if (String.IsNullOrEmpty(ids)) return;

            string[] idsUtilizar = ids.Split('|');
            string[] idsIndicadores = idsUtilizar[0].Split(',');
            string[] idsVariableEnfoque = idsUtilizar[1].Split(',');

            string idIndicador1 = idsIndicadores[0];
            string idIndicador2 = idsIndicadores[1];

            string idVariableEnfoque1 = idsVariableEnfoque[0];
            string idVariableEnfoque2 = idsVariableEnfoque[1];


            if (string.IsNullOrEmpty(idIndicador1) || string.IsNullOrEmpty(idIndicador2))
            {
                return;
            }

            #region Primera Variable Enfoque

            BlVariableEnfoque objVarEnfoqueBL = new BlVariableEnfoque();
            BeVariableEnfoque objVarEnfoqueBE = new BeVariableEnfoque();

            objVarEnfoqueBE.idIndicador = Convert.ToInt32(idIndicador1);
            objVarEnfoqueBE.campania = "";
            objVarEnfoqueBE.zonas = txtZonas1.Text;
            objVarEnfoqueBE.planAccion = txtPlanAccion1.Text;
            objVarEnfoqueBE.postDialogo = cboEstadoIndicador1.SelectedValue == "1";

            if (string.IsNullOrEmpty(idVariableEnfoque1))
            {
                objVarEnfoqueBE.idVariableEnfoque = 0;
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = Convert.ToInt32(idVariableEnfoque1);
            }

            objVarEnfoqueBE.estado = Constantes.EstadoActivo;

            if (objVarEnfoqueBE.idVariableEnfoque > 0)
            {
                _esCorrectoActual &= objVarEnfoqueBL.ActualizarVariableEnfoque(objVarEnfoqueBE);
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = objVarEnfoqueBL.InsertarVariableEnfoque(objVarEnfoqueBE);

                if (objVarEnfoqueBE.idVariableEnfoque > 0)
                    _esCorrectoActual &= true;
            }

            #endregion

            #region Segunda Variable Enfoque

            objVarEnfoqueBE = new BeVariableEnfoque();
            objVarEnfoqueBE.idIndicador = Convert.ToInt32(idIndicador2);
            objVarEnfoqueBE.campania = "";
            objVarEnfoqueBE.zonas = txtZonas2.Text;
            objVarEnfoqueBE.planAccion = txtPlanAccion2.Text;
            objVarEnfoqueBE.postDialogo = cboEstadoIndicador2.SelectedValue == "1";

            if (string.IsNullOrEmpty(idVariableEnfoque2))
            {
                objVarEnfoqueBE.idVariableEnfoque = 0;
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = Convert.ToInt32(idVariableEnfoque2);
            }

            objVarEnfoqueBE.estado = Constantes.EstadoActivo;

            if (objVarEnfoqueBE.idVariableEnfoque > 0)
            {
                _esCorrectoActual &= objVarEnfoqueBL.ActualizarVariableEnfoque(objVarEnfoqueBE);
            }
            else
            {
                objVarEnfoqueBE.idVariableEnfoque = objVarEnfoqueBL.InsertarVariableEnfoque(objVarEnfoqueBE);

                if (objVarEnfoqueBE.idVariableEnfoque > 0)
                    _esCorrectoActual &= true;
            }

            #endregion

            this.esCorrecto = _esCorrectoActual ? 1 : 0;
        }

        private void CalcularAvanze()
        {
            porcentaje = ProgresoHelper.CalcularAvanze(objResumenBE.idProceso, TipoPantalla.Durante);
        }

        #endregion
    }
}