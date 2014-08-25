
namespace Evoluciona.Dialogo.Web
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.UI;

    public partial class Prueba : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string CifrarCadena(string CadenaOriginal)
        {
            string str = null;
            try
            {
                //string stringKey = "password";
                //string stringIV = "46428208";

                string stringIV = "password";
                string stringKey = "46428208";


                if (((stringKey != null) & (stringIV != null)))
                {

                    byte[] bytes = Encoding.UTF8.GetBytes(stringKey);
                    byte[] rgbIV = Encoding.UTF8.GetBytes(stringIV);
                    byte[] buffer = Encoding.UTF8.GetBytes(CadenaOriginal);
                    MemoryStream stream = new MemoryStream((CadenaOriginal.Length * 2));
                    ICryptoTransform transform = new DESCryptoServiceProvider().CreateEncryptor(bytes, rgbIV);
                    CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.Close();
                    return Convert.ToBase64String(stream.ToArray());
                }
                str = "";
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                str = "";
            }
            return str;
        }

        //DESENCRIPTACION
        public string DescifrarCadena(string CadenaCifrada)
        {
            string str = null;
            try
            {
                //string stringKey = "password";
                //string stringIV = "46428208";


                string stringIV = "password";
                string stringKey = "46428208";

                if (((stringKey != null) & (stringIV != null)))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(stringKey);
                    byte[] rgbIV = Encoding.UTF8.GetBytes(stringIV);
                    byte[] buffer = Convert.FromBase64String(CadenaCifrada);
                    MemoryStream stream = new MemoryStream(CadenaCifrada.Length);
                    ICryptoTransform transform = new DESCryptoServiceProvider().CreateDecryptor(bytes, rgbIV);
                    CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.Close();
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
                str = "";
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                str = "";
            }
            return str;
        }

        protected void btnURL_Click(object sender, EventArgs e)
        {
            //txtCUBEncriptado.Text = CifrarCadena(txtCUB.Text);
            txtCUBEncriptado.Text = DescifrarCadena("6nMexncFPQJBbKNAICM78Q==");

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Response.Redirect("validacion.aspx?codigoUsuario=N1543857&prefijoIsoPais=GT&codigoRol=6&IdUsuario=PqB6skamP/w=");

            // cub encriptado

            //Response.Redirect("validacion.aspx?tkn=PqB6skamP/w=&Ps=GT&cub=eLdB6NGPCZpsLW0fA/YVhA==");
            Response.Redirect("log_ext.aspx?CUB=OREnNe3YP9fsgO7CP3nj/w==");
        }
    }
}
