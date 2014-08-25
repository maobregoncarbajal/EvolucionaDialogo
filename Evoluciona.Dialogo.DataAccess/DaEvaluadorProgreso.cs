
namespace Evoluciona.Dialogo.DataAccess
{
    using Helpers;
    using System.Data;
    using System.Data.SqlClient;

    public class DaEvaluadorProgreso : DaConexion
    {
        public int CalcularAvanze(int idProceso, TipoPantalla tipo)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_CalcularAvanze", cnn)
                {
                    CommandTimeout = 90,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@tipo", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = (int)tipo;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("Progreso"));
                }
            }

            return 0;
        }

        public int CalcularAvanze_Eval(int idProceso, TipoPantalla tipo)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_CalcularAvanze_Eval", cnn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@tipo", SqlDbType.Int);

                cmd.Parameters[0].Value = idProceso;
                cmd.Parameters[1].Value = (int)tipo;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("Progreso"));
                }
            }

            return 0;
        }
    }
}