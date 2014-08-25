
namespace Evoluciona.Dialogo.Web
{
    using Dialogo.Helpers;
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;

    public partial class pruebasUsuarioEC : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connstring = string.Empty;
            connstring = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            Session["connApp"] = connstring;
        }

        protected void lnkIniciarDialogo_Click(object sender, EventArgs e)
        {
            //validacion.aspx?codigoUsuario="+document.getElementById('txtDocuIdentidad').value+"&prefijoIsoPais="+document.getElementById('txtPrefijoIsoPais').value+"&codigoRol="+document.getElementById('listRoles').value

            string archivo = Server.MapPath("pruebasUsuarioEC.aspx").Replace("pruebasUsuarioEC.aspx",
                                                                             "KeyPublicaDDesempenio.xml");
            Encriptacion objEncriptar = new Encriptacion();
            string variableParaEncriptar = txtDocuIdentidad.Value.Trim() + "-" + txtPrefijoIsoPais.Value.Trim() + "-" +
                                           "periodo-" + ddlRoles.SelectedValue.Trim();
            string parametroEncriptado = objEncriptar.Encriptar(variableParaEncriptar, archivo);

            Response.Redirect("validacion.aspx?sson=" + HttpUtility.UrlEncode(parametroEncriptado));
        }

        protected void lnkbIniciarVisitas_Click(object sender, EventArgs e)
        {
            string archivo = Server.MapPath("pruebasUsuarioEC.aspx").Replace("pruebasUsuarioEC.aspx",
                                                                             "KeyPublicaDDesempenio.xml");
            Encriptacion objEncriptar = new Encriptacion();
            string variableParaEncriptar = txtDocuIdentidad.Value.Trim() + "-" + txtPrefijoIsoPais.Value.Trim() + "-" +
                                           "periodo-" + ddlRoles.SelectedValue.Trim();
            string parametroEncriptado = objEncriptar.Encriptar(variableParaEncriptar, archivo);

            Response.Redirect("validacion.aspx?app=V&sson=" + HttpUtility.UrlEncode(parametroEncriptado));
        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            HttpRuntime.UnloadAppDomain();
        }

        protected void btnParalelo_Click(object sender, EventArgs e)
        {

        }
    }
}
