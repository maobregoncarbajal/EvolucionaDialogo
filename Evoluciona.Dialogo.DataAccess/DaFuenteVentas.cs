
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaFuenteVentas : DaConexion
    {
        public DataTable ListarPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            var dtPlanes = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_SP_PAIS_FUENTE_VENTAS_LISTAR", conex);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoPais;
                cmd.Parameters.Add("@chrCodFuenteVentas", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoFuenteVenta;
                cmd.CommandType = CommandType.StoredProcedure;
                var da = new SqlDataAdapter(cmd);
                da.Fill(dtPlanes);
            }
            return dtPlanes;
        }

        public bool RegistrarPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_SP_PAIS_FUENTE_VENTAS_REGISTRAR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoPais;
                cmd.Parameters.Add("@chrCodFuenteVentas", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoFuenteVenta;
                cmd.Parameters.Add("@vchNomFuenteVentas", SqlDbType.VarChar, 40).Value = obeFuenteVentas.FuenteVentas;
                cmd.Parameters.Add("@chrUsuarioCrea", SqlDbType.Char, 20).Value = obeFuenteVentas.CodigoUsuarioCreacion;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    conex.Close();
                }
            }
            return true;
        }

        public bool ActualizarPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_SP_PAIS_FUENTE_VENTAS_MODIFICAR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoPais;
                cmd.Parameters.Add("@chrCodFuenteVentas", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoFuenteVenta;
                cmd.Parameters.Add("@vchNomFuenteVentas", SqlDbType.VarChar, 40).Value = obeFuenteVentas.FuenteVentas;
                cmd.Parameters.Add("@chrUsuarioCrea", SqlDbType.Char, 20).Value = obeFuenteVentas.CodigoUsuarioCreacion;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    conex.Close();
                }
            }
            return true;
        }

        public bool ActualizarEstadoPaisFuenteVenta(BeFuenteVentas obeFuenteVentas)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_SP_PAIS_FUENTE_VENTAS_MODIFICAR_ESTADO", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2).Value = obeFuenteVentas.CodigoPais;
                cmd.Parameters.Add("@chrUsuarioCrea", SqlDbType.Char, 20).Value = obeFuenteVentas.CodigoUsuarioCreacion;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally
                {
                    conex.Close();
                }
            }
            return true;
        }

    }
}
