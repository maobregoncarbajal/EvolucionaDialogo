
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class resumenVisitaIniciar : Page
    {
        protected BeUsuario objUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (!Page.IsPostBack)
            {
                Session[Constantes.VisitaModoLectura] = null;
                int idVisita = 0;
                string estadoVisita = "";
                if (Request["codVisita"] != null)
                {
                    //visita recien creada
                    idVisita = Convert.ToInt32(Request["codVisita"]);
                }
                else
                {
                    //obtiene el ID de la visita
                    BlResumenVisita blVisita = new BlResumenVisita();
                    DataTable dtVisita = blVisita.ObtenerCodigoVisita(Request["codDocu"], objUsuario.codigoUsuario, Convert.ToInt32(Request["idRol"]), objUsuario.periodoEvaluacion);
                    if (dtVisita.Rows.Count > 0)
                    {
                        idVisita = Convert.ToInt32(dtVisita.Rows[0]["codigoVisita"]);
                        estadoVisita = dtVisita.Rows[0]["chrEstadoVisita"].ToString();
                    }
                }
                if (Request["postVisita"] != null)
                {
                    //se inicia la postVisita
                    IniciarPostVisita(idVisita);
                }
                if (Request["consultaVisita"] != null)
                {
                    Session[Constantes.VisitaModoLectura] = "SI";
                }
                //Obtiene los datos de la visita
                CargarVisita(idVisita);
            }
        }

        private void CargarVisita(int idVisita)
        {
            Session[Constantes.ObjUsuarioVisitado] = null;
            int codigoRolEvaluado = Convert.ToInt32(Session["codigoRolEvaluado"].ToString());
            if (Session["visita_lectura"] != null)
            {
                //modo solo lectura
                codigoRolEvaluado = Constantes.RolGerenteZona;
                Session["codigoRolEvaluado"] = codigoRolEvaluado;
                Session[Constantes.VisitaModoLectura] = "SI";
            }
            BeResumenVisita objVisita = new BeResumenVisita();
            BlResumenVisita blVisita = new BlResumenVisita();
            switch (codigoRolEvaluado)
            {
                case Constantes.RolGerenteRegion:
                    objVisita = blVisita.ObtenerVisitaGr(Request["codDocu"], idVisita, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion);
                    break;
                case Constantes.RolGerenteZona:
                    objVisita = blVisita.ObtenerVisitaGz(Request["codDocu"], idVisita, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion);
                    break;
            }
            objVisita.idVisita = idVisita;
            objVisita.codigoRolUsuario = codigoRolEvaluado;
            objVisita.periodo = objUsuario.periodoEvaluacion;

            string urlInicio = "Interaccion.aspx?indiceSM=1&indiceM=1";
            if (Request["postVisita"] != null || Request["consultaPostVisita"] != null)
            {
                urlInicio = "Acuerdos.aspx?indiceSM=1&indiceM=3";
            }
            Session[Constantes.ObjUsuarioVisitado] = objVisita;

            ObtenerPeriodosByUsuario(objVisita.codigoUsuario, objVisita.prefijoIsoPais);

            Response.Redirect(urlInicio);
        }

        private void IniciarPostVisita(int idVisita)
        {
            BlResumenVisita blVisita = new BlResumenVisita();
            blVisita.IniciarPostVisita(idVisita);
        }

        private void ObtenerPeriodosByUsuario(string codigoUsuario, string prefijoIsoPais)
        {
            BlResumenVisita blVisita = new BlResumenVisita();
            DataTable dtVisita = blVisita.ObtenerPeriodoVisitasByUsuario(codigoUsuario, prefijoIsoPais);
            Session[Constantes.VisitaPeriodos] = dtVisita;
        }
    }
}