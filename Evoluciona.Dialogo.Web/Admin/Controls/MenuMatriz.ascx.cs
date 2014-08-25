
namespace Evoluciona.Dialogo.Web.Admin.Controls
{
    using System;
    using System.Web.UI;

    public partial class MenuMatriz : UserControl
    {
        #region Propiedades

        public string NivCompetencia
        {
            get { return spNivCompetencia.Attributes["class"]; }
            set { spNivCompetencia.Attributes["class"] += value; }
        }

        public string Condiciones
        {
            get { return spCondiciones.Attributes["class"]; }
            set { spCondiciones.Attributes["class"] += value; }
        }

        public string Calibracion
        {
            get { return spCalibracion.Attributes["class"]; }
            set { spCalibracion.Attributes["class"] += value; }
        }

        public string Agrupacion
        {
            get { return spAgrupacionZonaGPS.Attributes["class"]; }
            set { spAgrupacionZonaGPS.Attributes["class"] += value; }
        }

        public string FuenteVentas
        {
            get { return spFuenteVentas.Attributes["class"]; }
            set { spFuenteVentas.Attributes["class"] += value; }
        }


        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        #endregion
    }
}