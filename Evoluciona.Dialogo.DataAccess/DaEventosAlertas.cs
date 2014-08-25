
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEventosAlertas : DaConexion
    {
        public List<BeUsuario> SeleccionarGr()
        {
            var lstUsuariosGr = new List<BeUsuario>();
            using (var cn = ObtieneConexion())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ObtenerDatosGR", cn) {CommandType = CommandType.StoredProcedure};


                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objUser = new BeUsuario
                    {
                        nombreUsuario = dr["vchDesGerenteRegional"].ToString(),
                        correoElectronico = dr["vchCorreoElectronico"].ToString(),
                        codigoUsuario = dr["chrDocIdentidad"].ToString(),
                        prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString()
                    };
                    lstUsuariosGr.Add(objUser);
                }
                dr.Close();
            }

            return lstUsuariosGr;
        }

        public List<BeEventosAlertas> SeleccionarEventosAlertas()
        {
            var lstEventosAlertas = new List<BeEventosAlertas>();
            using (var cn = ObtieneConexion())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_ObtenerEventosActual", cn) {CommandType = CommandType.StoredProcedure};


                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var objEventosAlertas = new BeEventosAlertas
                    {
                        RolUsuario = Convert.ToInt32(dr["intIDRolUsuario"]),
                        CodUsuario = dr["chrCodUsuario"].ToString(),
                        FechaInicio = dr["datFechaInicio"].ToString(),
                        FechaFin = dr["datFechaFin"].ToString(),
                        Asunto = dr["vchAsunto"].ToString()
                    };
                    lstEventosAlertas.Add(objEventosAlertas);
                }
                dr.Close();
            }

            return lstEventosAlertas;
        }

    }
}
