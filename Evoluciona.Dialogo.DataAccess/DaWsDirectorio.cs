

namespace Evoluciona.Dialogo.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DaWsDirectorio : DaConexion

    {
        public bool DeleteClientesDirectorio(string codPais)
        {
            var resultado = true;
            using (var conn = ObtieneConexionJob())
            {
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_DELETE_IN_CLIENTES", conn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add("@codPais", SqlDbType.VarChar, 3);
                cmd.Parameters["@codPais"].Value = codPais;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    resultado = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }


        public string InsertClientesDirectorio(DataTable dtClienteDirWebService)
        {
            var resultado = "1";

            using (var cn = ObtieneConexionJob())
            {
                cn.Open();

                //var tran = cn.BeginTransaction();

                try
                {

                    dtClienteDirWebService.TableName = "dbo.ESE_IN_CLIENTES_DIRECTORIO";

                    using (var s = new SqlBulkCopy(cn))
                    {
                        s.DestinationTableName = dtClienteDirWebService.TableName;

                        foreach (var column in dtClienteDirWebService.Columns)
                            s.ColumnMappings.Add(column.ToString(), column.ToString());

                        s.WriteToServer(dtClienteDirWebService);
                    }
                }

                catch (Exception ex)
                {
                    resultado = ex.Message;
                    //tran.Rollback();
                }
                finally
                {
                    //tran.Commit();
                    if (cn.State == ConnectionState.Open)
                        cn.Close();

                    cn.Dispose();
                }
            }

            return resultado;
        }


        public void InsertarLogCargaDirectorio(string codigoPaisComercial, string descripcion)
        {
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_INSERT_LOG_CARGA", conn) { CommandTimeout = 300, CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@chrCodigoPaisComercial", SqlDbType.VarChar, 3);
                cmd.Parameters.Add("@vchDescripcion", SqlDbType.VarChar, 1000);
                cmd.Parameters["@chrCodigoPaisComercial"].Value = codigoPaisComercial;
                cmd.Parameters["@vchDescripcion"].Value = descripcion;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                conn.Close();
            }
        }


        public string DirectorioCargaInClientes()
        {
            var resultado = "1";

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_SP_DIRECTORIO_CARGA_IN_CLIENTES", conn) { CommandTimeout = 300, CommandType = CommandType.StoredProcedure };

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    resultado = ex.Message;
                }
                finally
                {
                    //tran.Commit();
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn.Dispose();
                }
            }
            return resultado;
        }






    }
}
