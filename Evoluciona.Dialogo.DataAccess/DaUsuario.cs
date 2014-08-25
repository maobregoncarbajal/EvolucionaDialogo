
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaUsuario : DaConexion
    {
        /// <summary>
        /// Obtiene los datos del usuario
        /// </summary>
        /// <returns>el objeto beUsuario</returns>
        public BeUsuario ObtenerDatosUsuario(string prefijoIsoPais, int codigoRol, string codigoUsuario, byte estado)
        {
            BeUsuario objUsuario = null;
            string nombreSp;

            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    nombreSp = "ESE_Obtener_DatosUsuario_DV";
                    break;
                case Constantes.RolGerenteRegion:
                    nombreSp = "ESE_Obtener_DatosUsuario_GR";
                    break;
                case Constantes.RolGerenteZona:
                    nombreSp = "ESE_Obtener_DatosUsuario_GZ";
                    break;
                default:
                    return null;
            }

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand(nombreSp, conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = estado;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objUsuario = new BeUsuario
                        {
                            codigoUsuario = dr["codigoUsuario"].ToString(),
                            nombreUsuario = dr["vchNombreCompleto"].ToString(),
                            correoElectronico = dr["vchCorreoElectronico"].ToString(),
                            idUsuario = Convert.ToInt32(dr["IDUsuario"]),
                            prefijoIsoPais = prefijoIsoPais,
                            idRol = codigoRol,
                            codigoRol = codigoRol,
                            cub = dr["CUB"].ToString()
                        };

                        var dt = ObtenerDatosRol(objUsuario.idRol, Constantes.EstadoActivo);
                        if (dt.Rows.Count > 0)
                        {
                            objUsuario.idRol = Convert.ToInt32(dt.Rows[0]["intIDRol"]);
                            objUsuario.rolDescripcion = dt.Rows[0]["vchDescripcion"].ToString();
                        }
                    }
                    dr.Close();
                }
            }

            return objUsuario;
        }

        /// <summary>
        /// Obtiene los datos del Rol
        /// </summary>
        /// <param name="codigoRol"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosRol(int codigoRol, byte estado)
        {
            var dtRol = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_RolById", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dtRol);
            }
            return dtRol;
        }
    }
}
