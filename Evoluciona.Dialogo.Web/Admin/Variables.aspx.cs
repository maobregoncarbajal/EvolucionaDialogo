
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Variables : Page
    {
        #region Variables

        private BeAdmin objAdmin;
        private readonly BlVariablePais blVariablePais = new BlVariablePais();

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            if (Page.IsPostBack)
                return;

            if (Request["accion"] == "CargarVariables")
            {
                CargarVariablesDisponibles();
                return;
            }
            if (Request["accion"] == "RegistrarVariable")
            {
                RegistrarVariable();
                return;
            }

            CargarPaises();
            CargarVariablesPorPais();
        }

        protected void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVariablesPorPais();
        }

        protected void gvVariablesPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVariables.PageIndex = e.NewPageIndex;
            CargarVariablesPorPais();
        }

        protected void gvVariablesRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idVariablePais = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "cmd_eliminar":
                    EliminarVariable(idVariablePais);
                    break;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVariablesPorPais();
        }

        #endregion Eventos

        #region Metodos

        private void CargarPaises()
        {
            BlPais paisBL = new BlPais();
            List<BePais> paises = new List<BePais>();

            switch (objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    paises = paisBL.ObtenerPaises();
                    break;
                case Constantes.RolAdminPais:
                    paises.Add(paisBL.ObtenerPais(objAdmin.CodigoPais));
                    break;
            }

            cboPaises.DataTextField = "NombrePais";
            cboPaises.DataValueField = "prefijoIsoPais";
            cboPaises.DataSource = paises;
            cboPaises.DataBind();

            cboPais.DataTextField = "NombrePais";
            cboPais.DataValueField = "prefijoIsoPais";
            cboPais.DataSource = paises;
            cboPais.DataBind();
        }

        private void CargarVariablesPorPais()
        {
            string codigoPais = cboPaises.SelectedValue;
            List<BeVariablePais> variables = blVariablePais.ListarVariablesPorPais(codigoPais);

            gvVariables.DataSource = variables;
            gvVariables.DataBind();
        }

        private void CargarVariablesDisponibles()
        {
            string opciones = string.Empty;
            string codigoPais = Request["codigoPais"];
            List<BeVariablePais> variables = blVariablePais.ListarVariablesDisponiblesPorPais(codigoPais);

            if (variables != null)
            {
                foreach (BeVariablePais item in variables)
                {
                    opciones += string.Format("<option value='{0}'>{1}</option>", item.CodigoVariable, item.DescripcionVariable);
                }
            }
            Serializar(opciones);
            Response.End();
        }

        private void EliminarVariable(int idVariablePais)
        {
            blVariablePais.EliminarVariable(idVariablePais);
            CargarVariablesPorPais();
        }

        private void RegistrarVariable()
        {
            try
            {
                BeVariablePais variable = new BeVariablePais();

                variable.CodigoVariable = Request["variable"];
                variable.CodigoPais = Request["codigoPais"];
                variable.UsuarioCrea = objAdmin.IDAdmin;

                blVariablePais.AgregarVariable(variable);

                Serializar("Se asigno Satisfactoriamente la Variable.");
            }
            catch (Exception)
            {
                Serializar("Ocurrio un error al intentar guardar los Datos.");
            }
            Response.End();
        }

        public void Serializar(object obj)
        {
            string output = JsonConvert.SerializeObject(obj);
            Response.ContentType = "application/json";
            Response.Write(output);
        }

        #endregion Metodos
    }
}