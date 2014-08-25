
namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    using Dialogo.Helpers;
    using BusinessEntity;
    using System;
    using System.Web.UI;

    public partial class MantDv : Page
    {
        #region Variables

        private BeAdmin _objAdmin;

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            _objAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];

            switch (_objAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    hfPais.Value = "00";
                    hfValComboPais.Value = "AR:AR;BO:BO;CL:CL;CO:CO;CR:CR;DO:DO;EC:EC;GT:GT;MX:MX;PA:PA;PE:PE;PR:PR;SV:SV;VE:VE";
                    break;
                case Constantes.RolAdminPais:
                    hfPais.Value = _objAdmin.CodigoPais;
                    hfValComboPais.Value = _objAdmin.CodigoPais + ":" + _objAdmin.CodigoPais;
                    break;
                case Constantes.RolAdminEvaluciona:
                    hfPais.Value = _objAdmin.CodigoPais;
                    hfValComboPais.Value = _objAdmin.CodigoPais + ":" + _objAdmin.CodigoPais;
                    break;
            }
        }
    }
}
