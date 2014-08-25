using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WSEvoluciona
{
    public class DaRestService
    {

        public Resultado GetListUser(string prefijoIsoPais, string fechaModi)
        {
            Resultado resultado = new Resultado();
            List<Usuario> listUsuarios = new List<Usuario>();


            using (SqlConnection cnn = DaConexion.GetConexion())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("ESE_CONSULTA_LISTA_USUARIOS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char,2);
                cmd.Parameters.Add("@datFechaModi", SqlDbType.DateTime);

                cmd.Parameters[0].Value = prefijoIsoPais;
                cmd.Parameters[1].Value = Convert.ToDateTime(fechaModi ?? "1900-01-01");

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Rol = reader.GetString(reader.GetOrdinal("chrRol"));
                        usuario.PrefijoIsoPais = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais"));
                        usuario.NombreCompleto = reader.GetString(reader.GetOrdinal("vchNombreCompleto"));
                        usuario.CUB = reader.GetString(reader.GetOrdinal("vchCUB"));
                        usuario.CodigoRegion = reader.GetString(reader.GetOrdinal("vchCodigoRegion"));
                        usuario.CodigoZona = reader.GetString(reader.GetOrdinal("vchCodigoZona"));
                        usuario.FechaModi = reader.GetString(reader.GetOrdinal("datFechaModi"));
                        usuario.DocumentoIdentidad = reader.GetString(reader.GetOrdinal("chrDocumentoIdentidad"));
                        usuario.CodigoTransac = reader.GetString(reader.GetOrdinal("vchCodigoTransac"));
                        listUsuarios.Add(usuario);
                    }

                    resultado.Codigo = "1";

                    if (listUsuarios.Count == 0)
                    {
                        resultado.Mensaje = ConfigurationSettings.AppSettings["msjNoRegistros"].ToString();
                    }
                    else {
                        resultado.Mensaje = "";
                    }
                    
                    resultado.ListaUsuarios = listUsuarios;
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    resultado.Codigo = "0";
                    resultado.Mensaje = ex.Message;
                    return resultado;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }
            }

            return resultado;
        }


        public Resultado GetDataUser(string documentoIdentidad, string rol, string prefijoIsoPais, string cub)
        {
            Resultado resultado = new Resultado();
            List<Usuario> listUsuarios = new List<Usuario>();

            using (SqlConnection cnn = DaConexion.GetConexion())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("ESE_CONSULTA_DATOS_USUARIO", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@chrDocumentoIdentidad", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@chrRol", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@vchCUB", SqlDbType.VarChar, 20);

                cmd.Parameters[0].Value = documentoIdentidad;
                cmd.Parameters[1].Value = rol;
                cmd.Parameters[2].Value = prefijoIsoPais;
                cmd.Parameters[3].Value = cub;

                try
                {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Rol = reader.GetString(reader.GetOrdinal("chrRol"));
                    usuario.PrefijoIsoPais = reader.GetString(reader.GetOrdinal("chrPrefijoIsoPais"));
                    usuario.NombreCompleto = reader.GetString(reader.GetOrdinal("vchNombreCompleto"));
                    usuario.CUB = reader.GetString(reader.GetOrdinal("vchCUB"));
                    usuario.CodigoRegion = reader.GetString(reader.GetOrdinal("vchCodigoRegion"));
                    usuario.CodigoZona = reader.GetString(reader.GetOrdinal("vchCodigoZona"));
                    usuario.FechaModi = reader.GetString(reader.GetOrdinal("datFechaModi"));
                    usuario.DocumentoIdentidad = reader.GetString(reader.GetOrdinal("chrDocumentoIdentidad"));
                    usuario.CodigoTransac = String.Empty;
                    listUsuarios.Add(usuario);
                }

                    resultado.Codigo = "1";

                    if (listUsuarios.Count == 0)
                    {
                        resultado.Mensaje = ConfigurationSettings.AppSettings["msjNoUsuario"].ToString(); 
                    }
                    else
                    {
                        resultado.Mensaje = "";
                    }

                    resultado.ListaUsuarios = listUsuarios;

                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    resultado.Codigo = "0";
                    resultado.Mensaje = ex.Message;
                    return resultado;
                }
                finally
                {
                    if (cnn != null) cnn.Close();
                }

            }

            return resultado;
        }

    }
}