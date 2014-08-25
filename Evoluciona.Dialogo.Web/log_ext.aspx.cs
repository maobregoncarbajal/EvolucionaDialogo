﻿
namespace Evoluciona.Dialogo.Web
{
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class log_ext : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cubEncriptado = Request.Url.Query.Replace("?CUB=","");
            Login_Evoluciona(cubEncriptado);
        }

        private void Login_Evoluciona(string cubEncriptado)
        {
            Encriptacion en = new Encriptacion();

            string cub = en.DesEncriptarCub(cubEncriptado);
            string codigoFfvv;
            string codigoRol;
            string pais;

            DataTable dt = new DataTable();
            BlLogExt bl = new BlLogExt();

            dt = bl.ObtenerParametrosParaLogueoporCub(cub);

            int cantReg = dt.Rows.Count;

            if (cantReg > 1)
            {
                //Response.Redirect("error.aspx?mensaje=CUB_D");
                RedireccionarCompetencia(cubEncriptado);
            }
            else if (cantReg == 1)
            {
                codigoFfvv = dt.Rows[0]["chrCodigoFFVV"].ToString();
                pais = dt.Rows[0]["chrPrefijoIsoPais"].ToString();
                codigoRol = dt.Rows[0]["intCodigoRol"].ToString();
                
                Response.Redirect("validacion.aspx?codigoUsuario=" + codigoFfvv + "&prefijoIsoPais=" + pais + "&codigoRol=" + codigoRol + "&IdUsuario=" + Constantes.Token);
                //Response.Redirect("validacion.aspx?codigoUsuario=N1543857&prefijoIsoPais=GT&codigoRol=6&IdUsuario=PqB6skamP/w=");
            }
            else if (cantReg == 0)
            {
                //Response.Redirect("error.aspx?mensaje=CUB_NE");
                RedireccionarCompetencia(cubEncriptado);
            }
        }

        private void RedireccionarCompetencia(string cubEncriptado)
        {
            var objMenu = new BlMenu();
            var url = objMenu.ObtenerUrlMenu("Competencias", 1);
            url = url + "?CUB=" + cubEncriptado;
            Response.Redirect(url);
        }
    }
}
