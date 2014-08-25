
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class crearVisita : Page
    {
        protected BeUsuario objUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            objUsuario = (BeUsuario) Session[Constantes.ObjUsuarioLogeado];
            if (!Page.IsPostBack)
            {
                lblFecha.Text = DateTime.Now.Date.ToShortDateString();
                if (Session["visita_lectura"] != null)
                {
                    btnCrearVisita.Enabled = false;
                    lblMensajes.Text = "No puede Iniciar la visita para la Gerente de zona seleccionada. Solo puede realizar consultas.";
                }
                else
                {
                    ObtenerCorrelativo();
                }
            }
        }

        private void ObtenerCorrelativo()
        {
            int idRol = ObtenerIDROl(Convert.ToInt32(Session["codigoRolEvaluado"]));
            hdIdRol.Value = idRol.ToString();
            int idProceso = Convert.ToInt32(Request["idPro"]);
            string documento = Request["docu"];
            BlResumenVisita blVisita = new BlResumenVisita();
            int cantidad = blVisita.ObtenerCorrelativoVisita(documento, idRol, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion);
            cantidad = cantidad + 1;
            lblCorrelativo.Text = cantidad.ToString();
        }

        private int ObtenerIDROl(int codigoRolEvaluado)
        {
            BlUsuario objRol = new BlUsuario();
            int idRol = 0;
            DataTable dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);
            if (dtRol.Rows.Count > 0)
            {
                idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"]);
            }
            return idRol;
        }

        protected void btnCrearVisita_Click(object sender, EventArgs e)
        {
            if (txtCampania.Text.Trim() == "")
            {
                lblMensajes.Text = "por favor ingrese la campaña para realizar la visita.";
                ClientScript.RegisterStartupScript(Page.GetType(), "_dialogo", "<script language='javascript'> javascript:AbrirMensaje(); </script>");
                return;
            }

            BeResumenVisita objVisita = new BeResumenVisita();
            BlResumenVisita blVisita = new BlResumenVisita();
            objVisita.codigoUsuario = Request["docu"];
            objVisita.idRolUsuario = Convert.ToInt32(hdIdRol.Value);
            objVisita.codigoUsuarioEvaluador = objUsuario.codigoUsuario;
            objVisita.idRolUsuarioEvaluador = objUsuario.idRol;
            objVisita.periodo = objUsuario.periodoEvaluacion;
            objVisita.prefijoIsoPais = objUsuario.prefijoIsoPais;
            objVisita.campania = txtCampania.Text;
            objVisita.idProceso = Convert.ToInt32(Request["idPro"]);
            objVisita.estadoVisita = Constantes.EstadoVisitaActivo;
            objVisita.fechaPostVisita = Convert.ToDateTime(lblFecha.Text);

            int idVisita = blVisita.CrearVisita(objVisita);
            if (idVisita > 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "_close", "<script language='javascript'> javascript:IniciarVisita(" + idVisita + ",'" + objVisita.codigoUsuario + "'," + objVisita.idRolUsuario + "); </script>");
            }
        }
    }
}
