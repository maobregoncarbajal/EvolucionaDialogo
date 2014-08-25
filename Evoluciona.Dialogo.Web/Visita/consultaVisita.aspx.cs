
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class consultaVisita : Page
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

            DataTable dtPostVisitas = new DataTable();
            dtPostVisitas.Columns.Add("chrCodigoUsuario");
            dtPostVisitas.Columns.Add("intIDVisita");
            dtPostVisitas.Columns.Add("correlativo");
            dtPostVisitas.Columns.Add("chrAnioCampana");
            dtPostVisitas.Columns.Add("datFechaPostVisita",Type.GetType("System.DateTime"));
            if (dtVisitas.Rows.Count > 0)
            {
                int visitas = 0;
                int postVisitas = 0;
                for (int i = 0; i < dtVisitas.Rows.Count; i++)
                {
                    visitas = i + 1;
                    dtVisitas.Rows[i]["correlativo"] = "Visita " + visitas.ToString();
                    if (dtVisitas.Rows[i]["datFechaPostVisita"] != System.DBNull.Value)
                    {
                         postVisitas = i + 1;
                        DataRow drPostVisita = dtPostVisitas.NewRow();
                        drPostVisita["chrCodigoUsuario"] = dtVisitas.Rows[i]["chrCodigoUsuario"].ToString();
                        drPostVisita["intIDVisita"] = dtVisitas.Rows[i]["intIDVisita"].ToString();
                        drPostVisita["chrAnioCampana"] = dtVisitas.Rows[i]["chrAnioCampana"].ToString();
                        drPostVisita["datFechaPostVisita"] = dtVisitas.Rows[i]["datFechaPostVisita"];
                        drPostVisita["correlativo"] = "Post-Visita " + postVisitas.ToString();
                        dtPostVisitas.Rows.Add(drPostVisita);
                    }
                }
            }


            gviewVisita.DataSource = dtVisitas;
            gviewVisita.DataBind();

            gviewPostVisita.DataSource = dtPostVisitas;
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
