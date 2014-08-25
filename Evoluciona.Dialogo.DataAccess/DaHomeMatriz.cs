
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System.Data;
    using System.Data.SqlClient;

    public class DaHomeMatriz : DaConexion
    {
        public int ObtenerGRplanMejoraDv(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_GR_CON_PLAN_DE_MEJORA_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("CantGRplanMejoraDV"));
                }
            }

            return 0;
        }

        public int ObtenerGZplanMejoraDv(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_GZ_CON_PLAN_DE_MEJORA_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("CantGZplanMejoraDV"));
                }
            }

            return 0;
        }



        public int ObtenerGRplanReasignacionDv(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_GR_CON_PLAN_REASIGNACION_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("CantGRplanReasignacionDV"));
                }
            }

            return 0;
        }



        public int ObtenerGZplanReasignacionDv(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_GZ_CON_PLAN_REASIGNACION_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("CantGZplanReasignacionDV"));
                }
            }

            return 0;
        }


        public decimal ObtenerPorcentGRrecuperacionDv(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_PORCENT_GR_CON_RECUPERACION_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetDecimal(reader.GetOrdinal("PorcentGRrecuperacionDV"));
                }
            }

            return 0;
        }



        public decimal ObtenerPorcentGZrecuperacionDv(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_PORCENT_GZ_CON_RECUPERACION_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetDecimal(reader.GetOrdinal("PorcentGZrecuperacionDV"));
                }
            }

            return 0;
        }

        public decimal ObtenerPorcntIncrGR_EstaProdDV(BeUsuario objUsuario, string periodoAnterior)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_PORCENT_INCREMENTO_GR_ESTABLES_PRODUCTIVAS_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodoEvaluacion", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPeriodoAnterior", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = periodoAnterior;
                cmd.Parameters[3].Value = objUsuario.codigoUsuario;
                cmd.Parameters[4].Value = 2;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetDecimal(reader.GetOrdinal("PorcentGRincrementoDV"));
                }
            }

            return 0;
        }



        public decimal ObtenerPorcntIncrGZ_EstaProdDV(BeUsuario objUsuario, string periodoAnterior)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_PORCENT_INCREMENTO_GZ_ESTABLES_PRODUCTIVAS_DE_DV", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodoEvaluacion", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPeriodoAnterior", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodDirectorVenta", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = periodoAnterior;
                cmd.Parameters[3].Value = objUsuario.codigoUsuario;
                cmd.Parameters[4].Value = 3;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetDecimal(reader.GetOrdinal("PorcentGZincrementoDV"));
                }
            }

            return 0;
        }



        public int ObtenerGZplanMejoraGr(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_GZ_CON_PLAN_DE_MEJORA_DE_GR", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("CantGZplanMejoraGR"));
                }
            }

            return 0;
        }

        public int ObtenerGZplanReasignacionGr(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_GZ_CON_PLAN_REASIGNACION_DE_GR", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoGerenteRegion", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("CantGZplanReasignacionGR"));
                }
            }

            return 0;
        }

        public decimal ObtenerPorcentGZrecuperacionGr(BeUsuario objUsuario)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_PORCENT_GZ_CON_RECUPERACION_DE_GR", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodGerenteRegion", SqlDbType.Char, 20);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = objUsuario.codigoUsuario;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetDecimal(reader.GetOrdinal("PorcentGZrecuperacionGR"));
                }
            }

            return 0;
        }

        public decimal ObtenerPorcntIncrGZ_EstaProdGR(BeUsuario objUsuario, string periodoAnterior)
        {
            using (var cnn = ObtieneConexion())
            {
                cnn.Open();

                var cmd = new SqlCommand("ESE_MATRIZ_OBTENER_PORCENT_INCREMENTO_GZ_ESTABLES_PRODUCTIVAS_DE_GR", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodoEvaluacion", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPeriodoAnterior", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIdRol", SqlDbType.Int);


                cmd.Parameters[0].Value = objUsuario.prefijoIsoPais;
                cmd.Parameters[1].Value = objUsuario.periodoEvaluacion;
                cmd.Parameters[2].Value = periodoAnterior;
                cmd.Parameters[3].Value = objUsuario.codigoUsuario;
                cmd.Parameters[4].Value = 3;

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetDecimal(reader.GetOrdinal("PorcentGZincrementoGR"));
                }
            }

            return 0;
        }
    }
}
