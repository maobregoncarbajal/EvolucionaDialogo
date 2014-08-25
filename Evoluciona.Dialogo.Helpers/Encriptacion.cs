
namespace Evoluciona.Dialogo.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Encriptacion
    {
        private static RSACryptoServiceProvider CargarLlave(string archivo, RSACryptoServiceProvider rsa)
        {
            var fs = new FileStream(archivo, FileMode.Open, FileAccess.Read, FileShare.Read);
            var sr = new StreamReader(fs);
            var llave = sr.ReadToEnd();
            sr.Close();
            rsa.FromXmlString(llave);
            return rsa;
        }

        public string Encriptar(string textoParaEncriptar, string rutaLlavePublica)
        {
            var textoOriginal = Encoding.UTF8.GetBytes(textoParaEncriptar);

            var rsa2 = new RSACryptoServiceProvider();
            rsa2 = CargarLlave(rutaLlavePublica, rsa2);
            var output = rsa2.Encrypt(textoOriginal, false);

            return Convert.ToBase64String(output);
        }

        public string Desencriptar(string textoEncriptado, string rutaLlavePrivada)
        {
            var textoOriginal = Convert.FromBase64String(textoEncriptado);

            var rsa = new RSACryptoServiceProvider();
            rsa = CargarLlave(rutaLlavePrivada, rsa);
            var output = rsa.Decrypt(textoOriginal, false);
            return Encoding.UTF8.GetString(output);
        }

        public string EncriptarCub(string cub)
        {
            string str;
            try
            {
                const string stringKey = Constantes.StringKey;
                const string stringIv = Constantes.StringIv;

                {

                    var bytes = Encoding.UTF8.GetBytes(stringKey);
                    var rgbIv = Encoding.UTF8.GetBytes(stringIv);
                    var buffer = Encoding.UTF8.GetBytes(cub);
                    var stream = new MemoryStream((cub.Length * 2));
                    var transform = new DESCryptoServiceProvider().CreateEncryptor(bytes, rgbIv);
                    var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.Close();
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
            catch (Exception)
            {
                str = "";
            }
            return str;
        }

        public string DesEncriptarCub(string cubEncriptado)
        {
            string str;
            try
            {
                const string stringKey = Constantes.StringKey;
                const string stringIv = Constantes.StringIv;

                {
                    var bytes = Encoding.UTF8.GetBytes(stringKey);
                    var rgbIv = Encoding.UTF8.GetBytes(stringIv);
                    var buffer = Convert.FromBase64String(cubEncriptado);
                    var stream = new MemoryStream(cubEncriptado.Length);
                    var transform = new DESCryptoServiceProvider().CreateDecryptor(bytes, rgbIv);
                    var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.Close();
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception)
            {
                str = "";
            }
            return str;
        }
    }
}
