
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Data;

    public partial class HeaderPaginasOperacionV : System.Web.UI.UserControl
    {
        protected int porcentaje;
        protected string nombreImagen;
        protected BeResumenVisita objResumenVisita;
        protected void Page_Load(object sender, EventArgs e)
        {
            objResumenVisita = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];
            lblEvaluado.Text = objResumenVisita.nombreEvaluado;

            if (Request["indiceM"] != null)
            {
                switch (Request["indiceM"].ToString())
                {
                    case "1": porcentaje = objResumenVisita.porcentajeAvanceAntes;
                        MostrarImagenTitulos("1");
                        break;
                    case "2": porcentaje = objResumenVisita.porcentajeAvanceDurante;
                        MostrarImagenTitulos("2");
                        break;
                    case "3": porcentaje = objResumenVisita.porcentajeAvanceDespues;
                        MostrarImagenTitulos("3");
                        break;
                }
            }
            else
            {
                porcentaje = 0;
            }
            //cboPeriodos.Items.Insert(0, new ListItem(objResumenVisita.periodo, objResumenVisita.periodo));
            CargarPeriodos();
        }
        private void MostrarImagenTitulos(string indiceMenu)
        {
            string subMenu = Request["indiceSM"].ToString();
            switch (indiceMenu)
            {
                case "1":
                    if (subMenu == "2")
                    {
                        nombreImagen = "visita_antes_negocio.jpg";
                    }
                    else if (subMenu == "3")
                    {
                        nombreImagen = "visita_antes_equipos.jpg";
                    }
                    else if (subMenu == "1")
                    {
                        nombreImagen = "visita_antes_competencias.jpg";
                    }
                    break;
                case "2":
                    if (subMenu == "2")
                    {
                        nombreImagen = "visita_durante_negocio.jpg";
                    }
                    else if (subMenu == "3")
                    {
                        nombreImagen = "visita_durante_equipos.jpg";
                    }
                    else if (subMenu == "1")
                    {
                        nombreImagen = "visita_durante_competencias.jpg";
                    }
                    break;
                case "3":
                    if (subMenu == "2")
                    {
                        nombreImagen = "visita_despues_negocio.jpg";
                    }
                    else if (subMenu == "3")
                    {
                        nombreImagen = "visita_despues_equipos.jpg";
                    }
                    else if (subMenu == "1")
                    {
                        nombreImagen = "visita_despues_competencias.jpg";
                    }
                    break;
            }

        }
        public void CargarPeriodos()
        {
            cboPeriodos.DataSource = (DataTable)Session[Constantes.VisitaPeriodos];
            cboPeriodos.DataValueField = "PeriodoVisita";
            cboPeriodos.DataTextField = "PeriodoVisita";
            cboPeriodos.DataBind();
            cboPeriodos.SelectedValue = objResumenVisita.periodo.Trim();

            //lblEvaluado.Text = nombreEvaluado.ToUpper();
        }
    }
}