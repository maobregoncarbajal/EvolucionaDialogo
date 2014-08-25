
namespace Evoluciona.Dialogo.Web.Visita
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class resumenVisita : Page
    {
        protected BeUsuario objUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (!Page.IsPostBack)
            {
                Session[Constantes.ObjUsuarioVisitado] = null;
                CargarRoles();
                int codigoRolEvaluado = 0;
                switch (objUsuario.codigoRol)
                {
                    case Constantes.RolDirectorVentas:
                        codigoRolEvaluado = Constantes.RolGerenteRegion;
                        break;
                    case Constantes.RolGerenteRegion:
                        codigoRolEvaluado = Constantes.RolGerenteZona;
                        break;
                    case Constantes.RolGerenteZona:
                        codigoRolEvaluado = Constantes.RolLideres;
                        break;
                }


                int idRolEvaluado = ObtenerIDROl(Convert.ToInt32(ddlRoles.SelectedValue));
                CargarVisitasCreadas(idRolEvaluado);
                CargarPostVisitas(idRolEvaluado);
                CargarConsultaVisitas(idRolEvaluado);

                Session["codigoRolEvaluado"] = codigoRolEvaluado;

            }
        }

        protected void btnBuscarProcesos_Click(object sender, EventArgs e)
        {
            int idRolEvaluado = ObtenerIDROl(Convert.ToInt32(ddlRoles.SelectedValue));
            CargarVisitasCreadas(idRolEvaluado);
            CargarPostVisitas(idRolEvaluado);
            CargarConsultaVisitas(idRolEvaluado);
        }

        private void CargarRoles()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigoRol");
            dt.Columns.Add("Rol");
            DataRow dr = null;
            if (objUsuario.codigoRol == Constantes.RolDirectorVentas)
            {
                dr = dt.NewRow();
                dr["codigoRol"] = Constantes.RolGerenteRegion;
                dr["Rol"] = "Gerente de Región";
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();
            dr["codigoRol"] = Constantes.RolGerenteZona;
            dr["Rol"] = "Gerente de Zona";
            dt.Rows.Add(dr);

            ddlRoles.DataSource = dt;
            ddlRoles.DataValueField = "codigoRol";
            ddlRoles.DataTextField = "Rol";
            ddlRoles.DataBind();
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

        private void CargarVisitasCreadas(int idRolEvaluado)
        {
            lblMensajes.Text = "";
            hdEsSoloLectura.Value = "";
            Session["lstVisitasCreadas"] = null;
            Session["visita_lectura"] = null;
            BlResumenVisita objBLVisita = new BlResumenVisita();
            List<BeResumenVisita> listResumen = null;
            if (objUsuario.codigoRol == Constantes.RolDirectorVentas)
            {
                if (Convert.ToInt32(ddlRoles.SelectedValue) == Constantes.RolGerenteRegion)
                {
                    listResumen = objBLVisita.BuscarResumenProcesoVisitaGr(txtNombreEvaluada.Text, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                }
                else
                {
                    Session["visita_lectura"] = "SI";
                    lblMensajes.Text = "No puede Iniciar la visita para la Gerente de zona seleccionada. Solo puede realizar consultas.";
                    hdEsSoloLectura.Value = "SI";
                    listResumen = objBLVisita.BuscarResumenProcesoVisitaGz(txtNombreEvaluada.Text, "", idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                }
            }
            else if (objUsuario.codigoRol == Constantes.RolGerenteRegion)
            {
                listResumen = objBLVisita.BuscarResumenProcesoVisitaGz(txtNombreEvaluada.Text, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
            }
            if (listResumen != null && listResumen.Count > 0)
            {
                gviewCrearVisita.DataSource = listResumen;
                Session["lstVisitasCreadas"] = listResumen;
            }
            else
            {
                gviewCrearVisita.DataSource = null;
                
            }
            gviewCrearVisita.PageIndex = 0;
            gviewCrearVisita.DataBind();
        }

        private void CargarPostVisitas(int idRolEvaluado)
        {
            Session["lstPostVisitas"] = null;
            
            BlResumenVisita objBLVisita = new BlResumenVisita();
            List<BeResumenVisita> listResumen = null;
            if (objUsuario.codigoRol == Constantes.RolDirectorVentas)
            {
                if (Convert.ToInt32(ddlRoles.SelectedValue) == Constantes.RolGerenteRegion)
                {
                    listResumen = objBLVisita.BuscarPostVisitaGr(txtNombreEvaluada.Text, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                }
                else
                {
                    listResumen = objBLVisita.BuscarPostVisitaGz(txtNombreEvaluada.Text, "", idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                }
            }
            else if (objUsuario.codigoRol == Constantes.RolGerenteRegion)
            {
                listResumen = objBLVisita.BuscarPostVisitaGz(txtNombreEvaluada.Text, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
            }
            if (listResumen != null && listResumen.Count > 0)
            {
                gviewPostVisita.DataSource = listResumen;
                Session["lstPostVisitas"] = listResumen;
            }
            else
            {
                gviewPostVisita.DataSource = null;

            }
            gviewPostVisita.PageIndex = 1;
            gviewPostVisita.DataBind();
        }

        private void CargarConsultaVisitas(int idRolEvaluado)
        {
            Session["lstConsultaVisitas"] = null;

            BlResumenVisita objBLVisita = new BlResumenVisita();
            List<BeResumenVisita> listResumen = null;
            if (objUsuario.codigoRol == Constantes.RolDirectorVentas)
            {
                if (Convert.ToInt32(ddlRoles.SelectedValue) == Constantes.RolGerenteRegion)
                {
                    listResumen = objBLVisita.BuscarVisitaGr(txtNombreEvaluada.Text, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                }
                else
                {
                    listResumen = objBLVisita.BuscarVisitaGz(txtNombreEvaluada.Text, "", idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                }
            }
            else if (objUsuario.codigoRol == Constantes.RolGerenteRegion)
            {
                listResumen = objBLVisita.BuscarVisitaGz(txtNombreEvaluada.Text, objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
            }
            if (listResumen != null && listResumen.Count > 0)
            {
                gviewConsultaVista.DataSource = listResumen;
                Session["lstConsultaVisitas"] = listResumen;
            }
            else
            {
                gviewConsultaVista.DataSource = null;

            }
            gviewConsultaVista.PageIndex = 1;
            gviewConsultaVista.DataBind();
        }

        protected void gviewCrearVisita_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gviewCrearVisita.PageIndex = e.NewPageIndex;
            gviewCrearVisita.DataSource = (List<BeResumenVisita>)Session["lstVisitasCreadas"];
            gviewCrearVisita.DataBind();
        }

        protected void gviewPostVisita_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gviewPostVisita.PageIndex = e.NewPageIndex;
            gviewPostVisita.DataSource = (List<BeResumenVisita>)Session["lstPostVisitas"];
            gviewPostVisita.DataBind();
        }

        protected void gviewConsultaVista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gviewConsultaVista.PageIndex = e.NewPageIndex;
            gviewConsultaVista.DataSource = (List<BeResumenVisita>)Session["lstConsultaVisitas"];
            gviewConsultaVista.DataBind();
        }

    }
}
