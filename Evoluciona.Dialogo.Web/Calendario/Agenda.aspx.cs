
namespace Evoluciona.Dialogo.Web.Calendario
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class Agenda : Page
    {
        #region Variables

        private BeUsuario ObjUsuario;

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ObjUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (ObjUsuario == null) return;

            if (IsPostBack) return;

            txtFechaInicio.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");

            visualizador.VistaAgenda = "ui-state-active";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

            CargarEventos(fechaInicio, fechaFin);
        }

        #endregion Eventos

        #region Metodos

        private void CargarEventos(DateTime fechaInicio, DateTime fechaFin)
        {
            BlEvento blEvento = new BlEvento();
            List<BeEvento> eventos = blEvento.ObtenerEventos(ObjUsuario.codigoUsuario, fechaInicio, fechaFin);
            var fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            foreach (var evento in eventos)
            {
                if (fecha.Year == evento.FechaInicio.Year &&
                    fecha.Month == evento.FechaInicio.Month &&
                    fecha.Day == evento.FechaInicio.Day)
                {
                    evento.Fecha = string.Empty;
                    evento.Color = "#FFF";
                }
                else
                {
                    fecha = evento.FechaInicio;

                    evento.Color = "#CCC";
                    evento.Fecha = fecha.ToString("dddd, dd MMMM");
                }
            }

            repEventos.DataSource = eventos;
            repEventos.DataBind();
        }

        #endregion Metodos
    }
}