using System;
using System.Web.UI;

namespace Evoluciona.Dialogo.Web.Admin.Controls
{
    public partial class MenuRepDialogos : UserControl
    {
        #region Variables
        #endregion

        #region Propiedades

        public string RepAntNeg
        {
            get { return spRepAntNeg.Attributes["class"]; }
            set { spRepAntNeg.Attributes["class"] += value; }
        }

        public string RepAntEqu
        {
            get { return spRepAntEqu.Attributes["class"]; }
            set { spRepAntEqu.Attributes["class"] += value; }
        }

        public string RepAntCom
        {
            get { return spRepAntCom.Attributes["class"]; }
            set { spRepAntCom.Attributes["class"] += value; }
        }

        public string RepDurNeg
        {
            get { return spRepDurNeg.Attributes["class"]; }
            set { spRepDurNeg.Attributes["class"] += value; }
        }

        public string RepDurEqu
        {
            get { return spRepDurEqu.Attributes["class"]; }
            set { spRepDurEqu.Attributes["class"] += value; }
        }

        public string RepDurCom
        {
            get { return spRepDurCom.Attributes["class"]; }
            set { spRepDurCom.Attributes["class"] += value; }
        }


        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
        }

        #endregion

        #region Metodos
        #endregion
    }
}