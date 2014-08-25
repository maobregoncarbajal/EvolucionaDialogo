
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class VariableCausaVisita : UserControl
    {
        #region Variables

        BeResumenVisita objVisita = new BeResumenVisita();
        BlIndicadores indicadorBL = new BlIndicadores();

        #endregion

        #region Propiedades

        public string CodigoVariable
        {
            get { return lblvariable1Desc.Text; }
            set { lblvariable1Desc.Text = value; }
        }

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

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];

            if (Page.IsPostBack) return;

            CargarVariablesCausa();
        }

        protected void ddlVariableCausa1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(objVisita.periodo, objVisita.codigoUsuario, objVisita.idProceso, objVisita.codigoRolUsuario, objVisita.prefijoIsoPais, objVisita.campania, ddlVariableCausa1.SelectedValue, CadenaConexion);
            if (dtCampana != null)
            {
                if (dtCampana.Rows.Count > 0)
                {
                    TextBox1.Text = dtCampana.Rows[0]["decValorPlanPeriodo"].ToString();
                    TextBox2.Text = dtCampana.Rows[0]["decValorRealPeriodo"].ToString();
                    TextBox3.Text = dtCampana.Rows[0]["Diferencia"].ToString();
                    TextBox4.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Porcentaje"]);
                }
            }
        }

        protected void ddlVariableCausa2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(objVisita.periodo, objVisita.codigoUsuario, objVisita.idProceso, objVisita.codigoRolUsuario, objVisita.prefijoIsoPais, objVisita.campania, ddlVariableCausa2.SelectedValue, CadenaConexion);
            if (dtCampana != null)
            {
                if (dtCampana.Rows.Count > 0)
                {
                    TextBox5.Text = dtCampana.Rows[0]["decValorPlanPeriodo"].ToString();
                    TextBox6.Text = dtCampana.Rows[0]["decValorRealPeriodo"].ToString();
                    TextBox7.Text = dtCampana.Rows[0]["Diferencia"].ToString();
                    TextBox8.Text = string.Format("{0:F2}", dtCampana.Rows[0]["Porcentaje"]);
                }
            }
        }

        protected void ddlVariableCausa1_DataBound(object sender, EventArgs e)
        {
            ddlVariableCausa1.Items.Insert(0, new ListItem("[SELECCIONE]", ""));
        }

        protected void ddlVariableCausa2_DataBound(object sender, EventArgs e)
        {
            ddlVariableCausa2.Items.Insert(0, new ListItem("[SELECCIONE]", ""));
        }

        #endregion

        #region Metodos

        private void CargarVariablesCausa()
        {
            DataTable variablesCausa = indicadorBL.CargarCombosVariablesCausa(objVisita.periodo, objVisita.codigoUsuario,
                                                                              objVisita.idProceso,
                                                                              objVisita.codigoRolUsuario,
                                                                              objVisita.prefijoIsoPais,
                                                                              objVisita.campania, string.Empty,
                                                                              string.Empty, CadenaConexion);
            ddlVariableCausa1.DataSource = variablesCausa;
            ddlVariableCausa1.DataTextField = "vchDesVariable";
            ddlVariableCausa1.DataValueField = "chrCodVariable";
            ddlVariableCausa1.DataBind();

            ddlVariableCausa2.DataSource = variablesCausa;
            ddlVariableCausa2.DataTextField = "vchDesVariable";
            ddlVariableCausa2.DataValueField = "chrCodVariable";
            ddlVariableCausa2.DataBind();

            #region Cargar Variables Causa si existieran en la BD

            DataTable variablesCausaIndicador1 = indicadorBL.ObtenerVariablesCausaVisita(objVisita.idProceso, CodigoVariable);

            if (variablesCausaIndicador1 != null && variablesCausaIndicador1.Rows.Count > 1)
            {
                ddlVariableCausa1.SelectedValue = variablesCausaIndicador1.Rows[0].ItemArray[0].ToString().Trim();
                ddlVariableCausa2.SelectedValue = variablesCausaIndicador1.Rows[1].ItemArray[0].ToString().Trim();

                ddlVariableCausa1_SelectedIndexChanged(this, new EventArgs());
                ddlVariableCausa2_SelectedIndexChanged(this, new EventArgs());
            }

            #endregion
        }

        public List<BeVariableCausa> ObtenerVariablesCausa()
        {
            List<BeVariableCausa> variablesCausa = new List<BeVariableCausa>();

            BeVariableCausa variableCausa = new BeVariableCausa();
            variableCausa.IdProceso = objVisita.idProceso;
            variableCausa.Codigo = ddlVariableCausa1.SelectedValue;
            variableCausa.CodigoPadre = lblvariable1Desc.Text;
            variableCausa.ValorPlan = Convert.ToDouble(TextBox1.Text);
            variableCausa.ValorReal = Convert.ToDouble(TextBox2.Text);
            variableCausa.Diferencia = Convert.ToDouble(TextBox3.Text);
            variableCausa.Porcentaje = Convert.ToDouble(TextBox4.Text);

            variablesCausa.Add(variableCausa);

            variableCausa = new BeVariableCausa();
            variableCausa.IdProceso = objVisita.idProceso;
            variableCausa.Codigo = ddlVariableCausa2.SelectedValue;
            variableCausa.CodigoPadre = lblvariable1Desc.Text;
            variableCausa.ValorPlan = Convert.ToDouble(TextBox5.Text);
            variableCausa.ValorReal = Convert.ToDouble(TextBox6.Text);
            variableCausa.Diferencia = Convert.ToDouble(TextBox7.Text);
            variableCausa.Porcentaje = Convert.ToDouble(TextBox8.Text);

            variablesCausa.Add(variableCausa);

            return variablesCausa;
        }

        #endregion
    }
}