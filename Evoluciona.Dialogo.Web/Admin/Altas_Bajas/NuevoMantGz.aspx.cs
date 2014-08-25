﻿using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.Helpers;
using System;

namespace Evoluciona.Dialogo.Web.Admin.Altas_Bajas
{
    public partial class NuevoMantGz : System.Web.UI.Page
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
                    hfUsuario.Value = _objAdmin.CodigoAdmin;
                    break;
                case Constantes.RolAdminPais:
                    hfPais.Value = _objAdmin.CodigoPais;
                    hfUsuario.Value = _objAdmin.CodigoAdmin;
                    break;
                case Constantes.RolAdminEvaluciona:
                    hfPais.Value = _objAdmin.CodigoPais;
                    hfUsuario.Value = _objAdmin.CodigoAdmin;
                    break;
            }

        }
    }
}