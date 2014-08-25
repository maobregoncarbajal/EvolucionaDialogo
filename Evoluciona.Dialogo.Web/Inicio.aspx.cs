
namespace Evoluciona.Dialogo.Web
{
    using System;
    using System.Web.UI;
    using BusinessEntity;
    using Dialogo.Helpers;

    public partial class Inicio : Page
    {
        public int _esPostBack = 0;
        BeUsuario ObjUsuario = new BeUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                _esPostBack = 1;
                return;
            }

            ObjUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (ObjUsuario != null)
            {
                if (ObjUsuario.codigoRol.Equals(Constantes.RolDirectorVentas))
                {
                    hfRol.Value = Constantes.CodDirectorVentas;
                }
                else if (ObjUsuario.codigoRol.Equals(Constantes.RolGerenteRegion))
                {
                    hfRol.Value = Constantes.CodGerenteRegion;
                }
                else if (ObjUsuario.codigoRol.Equals(Constantes.RolGerenteZona))
                {
                    hfRol.Value = Constantes.CodGerenteZona;
                }
            }
            else
            {
                return;
            }

        }
    }
}