
namespace Evoluciona.Dialogo.Web.Visita.AjaxVisita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;

    public partial class Consultas : Page
    {
        #region Variables



        #endregion

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

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            RealizarAccion();
        }

        #endregion  

        #region Metodos

        private void RealizarAccion()
        {
            string accion = Request["accion"];

            switch (accion)
            {
                case "CargarDatosVariable":
                    CargarDatosVariable();
                    break;
            }
        }

        private void CargarDatosVariable()
        {
            string codigoVariable = Request["codigoVariable"];
            BeResumenVisita objVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            BlIndicadores indicadorBL = new BlIndicadores();

            DataTable dtCampana = indicadorBL.CargarDatosVariableCausa(objVisita.periodo, objVisita.codigoUsuario, objVisita.idProceso, objVisita.codigoRolUsuario, objVisita.prefijoIsoPais, objVisita.campania, codigoVariable, CadenaConexion);
            if (dtCampana == null) return;
            if (dtCampana.Rows.Count <= 0) return;

            BeVariableCausa variableCausa = new BeVariableCausa();
            variableCausa.ValorPlan = Convert.ToDouble(dtCampana.Rows[0]["decValorPlanPeriodo"]);
            variableCausa.ValorReal = Convert.ToDouble(dtCampana.Rows[0]["decValorRealPeriodo"]);
            variableCausa.Diferencia = Convert.ToDouble(dtCampana.Rows[0]["Diferencia"]);
            variableCausa.Porcentaje = Convert.ToDouble(dtCampana.Rows[0]["Porcentaje"]);

            Serializar(variableCausa);
        }

        private void Serializar(object obj)
        {
            string output = JsonConvert.SerializeObject(obj);
            Response.ContentType = "application/json";
            Response.Write(output);
            Response.End();
        }

        #endregion
    }
}
