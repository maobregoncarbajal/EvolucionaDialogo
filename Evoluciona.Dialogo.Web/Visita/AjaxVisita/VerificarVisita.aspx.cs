
namespace Evoluciona.Dialogo.Web.Visita.AjaxVisita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class VerificarVisita : Page
    {
        protected string idVisita, idRol, existeVisita;
        protected BeUsuario objUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            //string accion = Request["accion"];
            form1.Visible = false;
            int idRolEvaluado = ObtenerIDROl(Convert.ToInt32(Session["codigoRolEvaluado"]));

            //int idProceso = Convert.ToInt32(Request["idPro"]);
            string documento = Request["docu"];
            ValidarVisita(documento, idRolEvaluado);
        }

        private void ValidarVisita(string documento, int idRolEvaluado)
        {
            existeVisita = "";
            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerCodigoVisita(documento, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.periodoEvaluacion);
            if (dtVisita.Rows.Count > 0)
            {
                idVisita = dtVisita.Rows[0]["codigoVisita"].ToString();
                idRol = idRolEvaluado.ToString();
                string estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                if (estadoVisita == Constantes.EstadoVisitaActivo || estadoVisita == Constantes.EstadoVisitaPostDialogo)
                {
                    existeVisita = "SI";
                }
            }
        }

        private int ObtenerIDROl(int codigoRolEvaluado)
        {
            BlUsuario objRol = new BlUsuario();
            int idRolEvaluado = 0;
            DataTable dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRolEvaluado = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
            }
            return idRolEvaluado;
        }
    }
}