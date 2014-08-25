
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
    using WsPlanDesarrollo;

    public partial class Adam : Page
    {
        #region Atributos

        private string dni;
        private int anio;
        private string pais;
        private string cub;

        #endregion Atributos

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            CargarPaises();
            string avanzada = Request["avanzada"];
            if (avanzada == "1")
                divFiltro.Visible = true;
            else
                BusquedaSimple();
        }

        private void BusquedaSimple()
        {
            BlPlanAnual daProceso = new BlPlanAnual();
            WsInterfaceFFVVSoapClient ws = new WsInterfaceFFVVSoapClient();
            BlGerenteRegion blgRegion = new BlGerenteRegion();
            DataSet ds = new DataSet();

            dni = Request["dni"];
            anio = Convert.ToInt32(Request["anio"]);
            pais = Request["pais"];
            cub = Request["cub"];
            string documentoIdentidadConsulta = string.Empty;

            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, pais);
            if (codigoPaisAdam == Constantes.CodigoAdamPaisColombia)
            {
                if (EsNumero(dni.Trim()))
                    documentoIdentidadConsulta = Convert.ToInt32(dni).ToString();
                else
                    documentoIdentidadConsulta = "0";
            }
            else
                documentoIdentidadConsulta = dni.Trim();

            //ds = ws.ConsultaPlanDesarrollo(anio, codigoPaisAdam, documentoIdentidadConsulta);
            ds = ws.ConsultaPlanDesarrollo(anio, cub);

            gvColaborador.DataSource = ds.Tables[0];
            gvColaborador.DataBind();
        }

        private void BusquedaAvanzada()
        {
            BlPlanAnual daProceso = new BlPlanAnual();
            WsInterfaceFFVVSoapClient ws = new WsInterfaceFFVVSoapClient();
            BlGerenteRegion blgRegion = new BlGerenteRegion();
            DataSet ds = new DataSet();
            string documentoIdentidadConsulta = string.Empty;
            anio = blgRegion.ObtenerAnio();
            string codigoPaisAdam = daProceso.ObtenerPaisAdam(CadenaConexion, pais);
            if (codigoPaisAdam == Constantes.CodigoAdamPaisColombia)
            {
                if (EsNumero(dni.Trim()))
                    documentoIdentidadConsulta = Convert.ToInt32(dni).ToString();
                else
                    documentoIdentidadConsulta = "0";
            }
            else
                documentoIdentidadConsulta = dni.Trim();

            ds = ws.ConsultaPlanDesarrollo(anio, cub);

            gvColaborador.DataSource = ds.Tables[0];
            gvColaborador.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            pais = cboPaises.SelectedValue;

            if (txtDoc.Text == string.Empty)
                dni = "0";
            else
                dni = txtDoc.Text;

            BusquedaAvanzada();
        }

        private bool EsNumero(string cadena)
        {
            if (cadena.Length == 0)
                return false;

            foreach (char c in cadena)
            {
                if (c < 48 || c > 57)
                    return false;
            }
            return true;
        }

        private void CargarPaises()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = new List<BePais>();
            paises = paisBL.ObtenerPaises();

            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";
            cboPaises.DataSource = paises;
            cboPaises.DataBind();
        }
    }
}