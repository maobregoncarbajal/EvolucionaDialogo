
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class detalleVisita : Page
    {
        protected BeUsuario objUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
             objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
             if (!Page.IsPostBack)
             {
                 CargarVisitas();
             }
        }
        private void CargarVisitas()
        {
            string codigoUsuario = Request["docu"].ToString();
            BlResumenVisita blVisita = new BlResumenVisita();
            int codigoRol = Convert.ToInt32(Session["codigoRolEvaluado"].ToString());
            if (Session["visita_lectura"] != null)
            { 
                // solo lectura
                codigoRol = Constantes.RolGerenteZona;
            }
            int idRol = ObtenerIDROl(codigoRol);
            hdIdRol.Value = idRol.ToString();
            DataTable dtVisitas = blVisita.SeleccionarVisitaUsuario(codigoUsuario, idRol, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion);
            dtVisitas.Columns.Add("correlativo");

            if (dtVisitas.Rows.Count > 0)
            {
                int visitas = 0;
                for (int i = 0; i < dtVisitas.Rows.Count; i++)
                {
                    visitas =i + 1;
                    dtVisitas.Rows[i]["correlativo"] = "Visita " + visitas.ToString();
                }
            }
            

            gviewPostVisita.DataSource = dtVisitas;
            gviewPostVisita.DataBind();

        }
        private int ObtenerIDROl(int codigoRolEvaluado)
        {
            BlUsuario objRol = new BlUsuario();
            int idRol = 0;
            DataTable dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
            }
            return idRol;
        }
    }
}
