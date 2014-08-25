
namespace Evoluciona.Dialogo.Web.Ajax
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web.UI;
    using BusinessLogic;

    public partial class AjaxCriticas : Page
    {
        protected string mensaje, error, nombreUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Visible = false;
            if (Request["accion"] == "insertar")
            {
                InsertarDatos();
            }
            else if (Request["accion"] == "adicionar")
            {
                AdicionarDatos();
            }
            else
            {
                EliminarDatos();
            }

        }

        private void InsertarDatos()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            //beResumenProceso objResumenBE = (beResumenProceso)Session[constantes.objUsuarioProcesado];

            int codigoRolUsuario = Convert.ToInt32(Request["codigoRol"]);

            int idProceso = Convert.ToInt32(Request["idProceso"]);

            BlCritica daProceso = new BlCritica();
            if (Session["dtRetroC"] != null)
            {
                DataTable dtTemporal = (DataTable)Session["dtRetroC"];

                if (dtTemporal.Rows.Count != 0)
                {
                    foreach (DataRow var in dtTemporal.Rows)
                    {
                        string Dni = var[0].ToString();
                        string VariableConsiderar = var[1].ToString();

                        bool inserto = false;
                        //blCritica daProceso = new blCritica();

                        inserto = daProceso.InsertarCriticas(Dni, idProceso, VariableConsiderar, codigoRolUsuario, connstring);
                    }
                }
                //Session["dtRetroC"] = null;
            }

        }

        private void AdicionarDatos()
        {
            string connstring = string.Empty;

            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            int codigoRolUsuario = Convert.ToInt32(Request["codigoRol"]);

            int idProceso = Convert.ToInt32(Request["idProceso"]);

            BlCritica daProceso = new BlCritica();
            string Dni = Request["docuIdentidad"].ToString();
            string VariableConsiderar = "";

            bool inserto = false;

            inserto = daProceso.InsertarCriticas(Dni, idProceso, VariableConsiderar, codigoRolUsuario, connstring);

        }

        private void EliminarDatos()
        {
            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();

            int codigoRolUsuario = Convert.ToInt32(Request["codigoRol"]);

            int idProceso = Convert.ToInt32(Request["idProceso"]);

            BlCritica daProceso = new BlCritica();
            string Dni = Request["docuIdentidad"].ToString();

            daProceso.EliminarCritica(Dni, idProceso, connstring);

        }
    }
}
