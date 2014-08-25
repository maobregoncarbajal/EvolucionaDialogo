
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaRol : DaConexion
    {
        public List<BeRol> ObtenerRolesSubordinados(int idRolActual)
        {
            var roles = new List<BeRol>();

            try
            {
                using (var cnn = ObtieneConexion())
                {
                    cnn.Open();

                    var cmd = new SqlCommand("ESE_ObtenerRolesSubordinados", cnn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                    cmd.Parameters[0].Value = idRolActual;

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var nuevoRol = new BeRol
                        {
                            IdRol = reader.GetInt32(reader.GetOrdinal("intIDRol")),
                            CodigoRol = reader.GetInt32(reader.GetOrdinal("intCodigoRol")),
                            Descripcion = reader.GetString(reader.GetOrdinal("vchDescripcion"))
                        };

                        roles.Add(nuevoRol);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }           

            return roles;
        }
    }
}
