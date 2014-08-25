
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaAcuerdo
    {
        // Ingresar Plan accion  ingresarPlanAccion
        public bool IngresarAcuerdo(string connstring, BeAcuerdo beAcuerdo)
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_VS_Insertar_Acuerdo", conn) {CommandType = CommandType.StoredProcedure};


                cmd.Parameters.Add("@intIDVisita", SqlDbType.Int);
                cmd.Parameters.Add("@intIDPregunta", SqlDbType.Int);
                cmd.Parameters.Add("@vchRespuesta", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@bitPostVisita", SqlDbType.Bit);
                cmd.Parameters.Add("@intIdUsuario", SqlDbType.Int);


                cmd.Parameters[0].Value = beAcuerdo.IDVisita;
                cmd.Parameters[1].Value = beAcuerdo.IDPregunta;
                cmd.Parameters[2].Value = beAcuerdo.Respuesta;
                cmd.Parameters[3].Value = beAcuerdo.PostVisita;
                cmd.Parameters[4].Value = beAcuerdo.idUsuario;

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
                    conn.Close();

                }
            }
            return true;


        }




        // Obtener Acuerdo Grabadas
        public DataTable ObtenerAcuerdoGrabadas(string connstring, int idVisita)
        {
            var ds = new DataSet();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                var cmd = new SqlCommand("ESE_VS_Obtener_Acuerdo_Procesado", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIdVisita", SqlDbType.Int);


                cmd.Parameters[0].Value = idVisita;

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
                    conn.Close();
                }
            }
            return ds.Tables[0];
        }



    }
}
