
namespace Evoluciona.Dialogo.Web.Matriz
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.IO;
    using System.Web.UI;

    public partial class Resultado : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (Page.IsPostBack) return;

            // Eliminar Archivos Anteriores de Reportes
            foreach (string file in Directory.GetFiles(Server.MapPath(@"~/Charts/")))
            {
                FileInfo theFile = new FileInfo(file);

                if (DateTime.Parse(theFile.CreationTime.ToShortDateString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    File.Delete(file);
                }
            }
        }

        private void CargarVariables()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario != null)
            {
                string codigoUsuario = objUsuario.codigoUsuario;
                lblCodigoUsuario.Text = codigoUsuario;
                lblNombre.Text = objUsuario.nombreUsuario;
                int rol = objUsuario.idRol;
                lblRol.Text = rol.ToString();
                string codPais = objUsuario.prefijoIsoPais;
                lblPais.Text = codPais;

                if (rol == (int)TipoRol.GerenteRegion)
                {
                    BlMatriz matrizBL = new BlMatriz();
                    lblRegion.Text = matrizBL.ObtenerRegionUsuario(codPais, codigoUsuario).Codigo;
                }
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "crearCombos", "crearCombos();", true);
        }
    }
}
