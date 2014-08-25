
namespace Evoluciona.Dialogo.Web
{
    using System;
    using System.Web.UI;

    public partial class Ayuda : Page
    {
        public int _esPostBack = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                _esPostBack = 1;
                return;
            }
        }
    }
}