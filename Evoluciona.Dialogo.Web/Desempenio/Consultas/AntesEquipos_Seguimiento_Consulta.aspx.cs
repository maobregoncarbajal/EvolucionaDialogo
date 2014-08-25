
namespace Evoluciona.Dialogo.Web.Desempenio.Consultas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class AntesEquipos_Seguimiento_Consulta : Page
    {
        #region Variables

        private BeResumenProceso objResumenBE;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];

            if (IsPostBack)
                return;

            CargarPeriodos();
            CargarCampanhas();
        }

        protected void cboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCampanhas();
        }

        #endregion

        #region Metodos

        private void CargarPeriodos()
        {
            cboPeriodos.DataSource = (List<string>)Session["periodosValidos"];
            cboPeriodos.DataBind();
        }

        private void CargarCampanhas()
        {
            BlCritica criticaBL = new BlCritica();

            string estadoPeriodo = Request.QueryString.Get("estadoPeriodo");
            string dniUsuario = Request.QueryString.Get("dniUsuario");
            string nombreUsuario = Request.QueryString.Get("nombreUsuario");

            if (string.IsNullOrEmpty(estadoPeriodo) || string.IsNullOrEmpty(dniUsuario) || string.IsNullOrEmpty(cboPeriodos.SelectedValue))
                return;

            List<BeCriticas> criticas = criticaBL.ListaSeguimientosEquipo(dniUsuario, objResumenBE.codigoUsuario, objResumenBE.codigoRolUsuario, cboPeriodos.SelectedValue, objResumenBE.prefijoIsoPais);

            foreach (var critica in criticas)
            {
                critica.campania = string.Format("C {0}", critica.campania.Substring(4, 2));

                if (critica.estadoCriticidad.Trim().ToUpper() == "CRITICA")
                    critica.EstadoSeleccionado = true;
            }

            if (criticas.Count != 0)
            {
                string periodo = cboPeriodos.SelectedValue.Substring(5).Trim();
                int campanhaInicio = 1;
                if (periodo == "II") campanhaInicio = 7;
                if (periodo == "III") campanhaInicio = 13;

                while (criticas.Count < 6)
                {
                    bool existe = false;
                    string campanha = string.Format("C {0}", (campanhaInicio++).ToString().PadLeft(2, '0'));

                    foreach (var item in criticas)
                    {
                        if (item.campania == campanha)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        BeCriticas critica = new BeCriticas();
                        critica.campania = campanha;
                        criticas.Add(critica);
                    }
                }
                criticas.Sort(delegate(BeCriticas p1, BeCriticas p2) { return p1.campania.CompareTo(p2.campania); });
                NoExistenDatos(false);
            }
            else
            {
                string periodo = cboPeriodos.SelectedValue.Substring(5).Trim();
                int campanhaInicio = 1;
                if (periodo == "II") campanhaInicio = 7;
                if (periodo == "III") campanhaInicio = 13;

                while (criticas.Count < 6)
                {
                    BeCriticas critica = new BeCriticas();
                    critica.campania = string.Format("C {0}", (campanhaInicio++).ToString().PadLeft(2, '0'));
                    criticas.Add(critica);
                }
                dlCampanhas.DataSource = criticas;
                dlCampanhas.DataBind();
                NoExistenDatos(true);
            }

            switch (objResumenBE.codigoRolUsuario)
            {
                case Constantes.RolGerenteRegion:
                    dlCampanhasGZ.DataSource = criticas;
                    dlCampanhasGZ.DataBind();
                    dlCampanhasLET.Visible = false;
                    litEvaluacion.Text = " REGIÓN:";
                    spanParticipacion.Visible = true;
                    break;
                case Constantes.RolGerenteZona:
                    dlCampanhasLET.DataSource = criticas;
                    dlCampanhasLET.DataBind();
                    dlCampanhasGZ.Visible = false;
                    litEvaluacion.Text = " ZONA:";
                    spanParticipacion.Visible = false;
                    break;
            }

            litNombre.Text = nombreUsuario;
        }

        private void NoExistenDatos(bool valor)
        {
            panExisteDatos.Visible = !valor;
            panNoExistenDatos.Visible = valor;
        }

        #endregion
    }
}
