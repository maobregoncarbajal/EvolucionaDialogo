
namespace Evoluciona.Dialogo.DataAccess
{
    using System.Data;
    using System.Data.SqlClient;

    public class DaMenuAdmin : DaConexion
    {
        public DataTable SeleccionarMenuAdmin()
        {
            var dt = new DataTable();
            using (var conex = ObtieneConexionJob())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_OBTENER_MENU_ADMIN", conex) {CommandType = CommandType.StoredProcedure};

                var dap = new SqlDataAdapter(cmd);
                dap.Fill(dt);
            }

            return dt;
        }
    }
}
