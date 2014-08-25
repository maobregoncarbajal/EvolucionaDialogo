
namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaMenu : DaConexion
    {
        /// <summary>
        /// Obtiene las opciones para el menu del usuario
        /// </summary>
        /// <returns>la lista con el menu</returns>
        public DataTable ObtenerMenuPrincipal(int idRol, byte estado)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_MenuPrincipal", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Obtiene las opciones para el menu del usuario
        /// </summary>
        /// <returns>la lista con el menu</returns>
        public DataTable ObtenerMenu(int? idMenuPadre)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_Menu", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIdMenuPadre", SqlDbType.Int);
                cmd.Parameters["@intIdMenuPadre"].Value = idMenuPadre;

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Obtiene los sub menu del usuario
        /// </summary>
        /// <param name="idMenu"></param>
        /// <param name="idRol"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataTable ObtenerDetalleMenu(int idMenu, int idRol, byte estado)
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Obtener_MenuDetalle", conex) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDMenu", SqlDbType.Int);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDMenu"].Value = idMenu;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }

        public DataTable ValidarAprobacion(int intIdProceso, string chrEstadoProceso)
        {

            var ds = new DataSet();
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Actualizar_ResumenProcesoPRUEBA", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);

                cmd.Parameters[0].Value = intIdProceso;
                cmd.Parameters[1].Value = chrEstadoProceso;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conex != null) conex.Close();
                }
            }
            return ds.Tables[0];
        }

        public DataTable ValidarAprobacionSinLets(int intIdProceso, string chrEstadoProceso)
        {

            var ds = new DataSet();
            using (var conex = ObtieneConexion())
            {
                var cmd = new SqlCommand("ESE_Actualizar_ResumenProcesoSinLets", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);

                cmd.Parameters[0].Value = intIdProceso;
                cmd.Parameters[1].Value = chrEstadoProceso;

                try
                {
                    var dap = new SqlDataAdapter(cmd);
                    dap.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (conex != null) conex.Close();
                }
            }
            return ds.Tables[0];
        }


        /// <summary>
        /// Obtiene la Url del Menu
        /// </summary>
        /// <param name="descripcionMenu">código del pais</param>
        /// /// <param name="estado">código del pais</param>
        /// <returns>retorna la Url del Menu</returns>
        public string ObtenerUrlMenu(string descripcionMenu, byte estado)
        {
            var url = string.Empty;

            using (var cn = ObtieneConexion())
            {
                cn.Open();
                var cmd = new SqlCommand("ESE_OBTENER_URL_TRX_MENU", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@vchDescripcionMenu", SqlDbType.VarChar, 25);
                cmd.Parameters["@vchDescripcionMenu"].Value = descripcionMenu;

                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters["@bitEstado"].Value = estado;

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            url = dr.IsDBNull(dr.GetOrdinal("vchUrl")) ? default(string) : dr.GetString(dr.GetOrdinal("vchUrl"));
                        }

                        dr.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();

                    cn.Dispose();
                }
            }

            return url;
        }

    }
}
