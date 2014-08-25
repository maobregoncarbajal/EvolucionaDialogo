
namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class Mapeo : Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (Page.IsPostBack) return;
        }

        #endregion Eventos

        #region Metodos

        private void CargarVariables()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario != null)
            {
                string codigoUsuario = objUsuario.codigoUsuario;
                lblCodigoUsuario.Text = codigoUsuario;
                int rol = objUsuario.idRol;
                lblRol.Text = rol.ToString();
                string codPais = objUsuario.prefijoIsoPais;
                lblPais.Text = codPais;

                if (rol == (int)TipoRol.GerenteRegion)
                {
                    BlMatriz matrizBL = new BlMatriz();
                    lblRegion.Text = string.Format("{0}-{1}",codPais,matrizBL.ObtenerRegionUsuario(codPais, codigoUsuario).Codigo);
                }
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "crearCombos", "crearCombos();", true);
        }

        #endregion Metodos
    }
}