
namespace Evoluciona.Dialogo.Web
{
    using System;
    using System.Web.UI;

    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            decimal PorcentajeAvance = decimal.Round(decimal.Parse((13).ToString()) / 100, 2);
        }
    }
}
