
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaDataMart : DaConexion
    {
        protected string CodigoPaisDataMart;

        public void ObtenerDataMart(List<BePais> listaPaises)
        {
            try
            {
                LimpiarTablasCargaInicial();

                foreach (var objPais in listaPaises)
                {
                    InsertarLogCarga("obtenerDataMart", "Inicia proceso para el país: " + objPais.codigoPais);
                    CodigoPaisDataMart = objPais.codigoPais;
                    ObtenerDataFfvvGerenteRegion(listaPaises);
                    ObtenerDataFfvvDetalleGr(listaPaises);
                    ObtenerDataFfvvGerenteZona(listaPaises);
                    ObtenerDataFfvvDetalleGz(listaPaises);
                    ObtenerDataFfvvLet(listaPaises);
                    InsertarLogCarga("obtenerDataMart", "Fin proceso para el país: " + objPais.codigoPais);
                }
            }
            catch (Exception ex)
            {
                InsertarLogCarga("obtenerDataMart", ex.Message);
            }
        }

        public void LimpiarTablasCargaInicial()
        {
            using (var conn = ObtieneConexionJob())
            {
                conn.Open();
                var cmd1 = new SqlCommand("ESE_LimpiarTablasCargaInicial", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.ExecuteNonQuery();
            }
        }

        public List<BePais> ObtenerListaPaises()
        {
            var listaPaises = new List<BePais>();
            using (var conn = ObtieneConexionJob())
            {
                conn.Open();
                var cmd1 = new SqlCommand("ESE_Obtener_Paises", conn) {CommandType = CommandType.StoredProcedure};

                var dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    var objPais = new BePais
                    {
                        codigoPais = dr["chrCodPais"].ToString(),
                        prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString(),
                        codigoComercial = dr["chrCodigoPaisComercial"].ToString(),
                        ActualizacionDM =
                            !dr.IsDBNull(dr.GetOrdinal("bitActualizacionDM")) &&
                            dr.GetBoolean(dr.GetOrdinal("bitActualizacionDM"))
                    };
                    listaPaises.Add(objPais);
                }
                dr.Close();
            }
            return listaPaises;
        }

        private static string ObtenerPrefijoPais(string codigoPais, List<BePais> listaPaises)
        {
            var objPais = listaPaises.Find(
                objFiltro =>
                    objFiltro.codigoPais == codigoPais.Trim().ToUpper() ||
                    objFiltro.codigoComercial == codigoPais.Trim().ToUpper());
            if (objPais.codigoComercial != null)
            {
                codigoPais = objPais.prefijoIsoPais;
            }
            return codigoPais;
        }

        public void ObtenerDataFfvvGerenteRegion(List<BePais> listaPaises)
        {
            var dt = new DataTable();
            using (var conn = ObtieneConexionDataMart())
            {
                conn.Open();
                var cmd1 = new SqlCommand("SELECT [AnioCampana], [CodPais], [CodRegion], [CodGerenteRegional], [DesGerenteRegional], [DocIdentidad], [CorreoElectronico], [EstadoCamp], [PtoRankingProdCamp], [Periodo], [EstadoPeriodo], [PtoRankingProdPeriodo], [FechaUltAct], [FlagProceso], [FlagControl], [FlagControl_CSFyGH], [CUBGR] FROM FFVVGerenteRegion WHERE CodPais=@codPais", conn)
                {
                    CommandType = CommandType.Text
                };
                cmd1.Parameters.Add("@codPais", SqlDbType.Char, 2);
                cmd1.Parameters["@codPais"].Value = CodigoPaisDataMart;
                var dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                conn.Close();
            }

            if (dt.Rows.Count <= 0) return;
            InsertarLogCarga("obtenerDataFFVVGerenteRegion", "cantidad de registros:" + dt.Rows.Count);
            InsertarFfvvGerenteRegion(dt, listaPaises);
            ActualizarINGerenteRegion(listaPaises);
            InsertarLogCarga("obtenerDataFFVVGerenteRegion", "Fin de obtener datos FFVVGerenteRegion");
        }

        /// <summary>
        /// Inserta los datos en la TABLA ESE_IN_GERENTE_REGION
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="listaPaises"></param>
        private void InsertarFfvvGerenteRegion(DataTable dt, List<BePais> listaPaises)
        {
            var codigoPaisDd = ObtenerPrefijoPais(CodigoPaisDataMart, listaPaises);

            foreach (DataRow var in dt.Rows)
            {
                using (var cn = ObtieneConexion())
                {
                    cn.Open();
                    var cmd = new SqlCommand("ESE_InsertarIN_GRregion", cn) {CommandType = CommandType.StoredProcedure};

                    cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                    cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                    cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@vchDesGerenteRegional", SqlDbType.VarChar, 55);
                    cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                    cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@vchEstadoCamp", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@intPtoRankingProdCamp", SqlDbType.Int);
                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@decPtoRankingProdPeriodo", SqlDbType.Decimal);
                    cmd.Parameters.Add("@datFechaUltAct", SqlDbType.DateTime);
                    cmd.Parameters.Add("@intFlagProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl_CSFyGH", SqlDbType.Int);
                    cmd.Parameters.Add("@vchCUBGR", SqlDbType.VarChar, 20);

                    cmd.Parameters[0].Value = var["AnioCampana"].ToString();
                    cmd.Parameters[1].Value = codigoPaisDd;
                    cmd.Parameters[2].Value = var["CodRegion"].ToString();
                    cmd.Parameters[3].Value = var["CodGerenteRegional"].ToString();
                    cmd.Parameters[4].Value = var["DesGerenteRegional"].ToString();
                    cmd.Parameters[5].Value = var["DocIdentidad"].ToString();
                    cmd.Parameters[6].Value = var["CorreoElectronico"] == DBNull.Value ? "" : var["CorreoElectronico"].ToString();
                    cmd.Parameters[7].Value = var["EstadoCamp"] == DBNull.Value ? "" : var["EstadoCamp"].ToString();
                    cmd.Parameters[8].Value = Convert.ToInt32(var["PtoRankingProdCamp"]);
                    cmd.Parameters[9].Value = var["Periodo"] == DBNull.Value ? "" : var["Periodo"].ToString();
                    cmd.Parameters[10].Value = var["EstadoPeriodo"] == DBNull.Value ? "" : var["EstadoPeriodo"].ToString();
                    cmd.Parameters[11].Value = var["PtoRankingProdPeriodo"] == DBNull.Value ? 0 : Convert.ToDecimal(var["PtoRankingProdPeriodo"]);
                    cmd.Parameters[12].Value = Convert.ToDateTime(var["FechaUltAct"]);
                    cmd.Parameters[13].Value = var["FlagProceso"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagProceso"]);
                    cmd.Parameters[14].Value = var["FlagControl"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl"]);
                    cmd.Parameters[15].Value = var["FlagControl_CSFyGH"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl_CSFyGH"]);
                    cmd.Parameters[16].Value = var["CUBGR"] == DBNull.Value ? "" : var["CUBGR"].ToString();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex.InnerException);
                    }

                    cn.Close();
                }
            }
        }

        private void ActualizarINGerenteRegion(IEnumerable<BePais> listaPaises)
        {
            foreach (var pais in listaPaises)
            {
                if (!pais.ActualizacionDM) return;

                using (var cn = ObtieneConexion())
                {
                    var cmd = new SqlCommand("ESE_Actualizar_IN_GERENTE_REGION_DM", cn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                    cmd.Parameters["@chrCodigoPais"].Value = pais.prefijoIsoPais;

                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
            }
        }

        public void ObtenerDataFfvvDetalleGr(List<BePais> listaPaises)
        {
            //   bool resultado = false;

            var dt = new DataTable();
            using (var conn = ObtieneConexionDataMart())
            {
                conn.Open();
                var cmd1 = new SqlCommand("select [AnioCampana], [CodPais], [CodRegion], [CodGerenteRegional], [CodVariable], [DesVariable], [ValorPlan], [ValorReal], [Periodo], [ValorPlanPeriodo], [ValorRealPeriodo], [FechaUltAct], [FlagProceso], [FlagControl], [FlagControl_CSFyGH] from FFVVDetalleGR WHERE CodPais=@codPais", conn)
                {
                    CommandType = CommandType.Text
                };
                cmd1.Parameters.Add("@codPais", SqlDbType.Char, 2);
                cmd1.Parameters["@codPais"].Value = CodigoPaisDataMart;

                var dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                conn.Close();
            }
            if (dt.Rows.Count <= 0) return;
            InsertarLogCarga("obtenerDataFFVVDetalleGR", "cantidad de registros:" + dt.Rows.Count);
            insertarESE_IN_DETALLE_GERENTE_REGION(dt, listaPaises);
            InsertarLogCarga("obtenerDataFFVVDetalleGR", "Fin de obtener datos FFVVDetalleGR");
            //   return dt;
        }

        /// <summary>
        /// inserta los datos en la tabla ESE_IN_DETALLE_GERENTE_REGION
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="listaPaises"></param>
        private void insertarESE_IN_DETALLE_GERENTE_REGION(DataTable dt, List<BePais> listaPaises)
        {
            var codigoPaisDd = ObtenerPrefijoPais(CodigoPaisDataMart, listaPaises);
            foreach (DataRow var in dt.Rows)
            {
                using (var cn = ObtieneConexion())
                {
                    cn.Open();
                    var cmd = new SqlCommand("ESE_InsertarIN_GRegion_Detalle", cn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                    cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                    cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@chrCodVariable", SqlDbType.Char, 20);
                    cmd.Parameters.Add("@vchDesVariable", SqlDbType.VarChar, 75);
                    cmd.Parameters.Add("@decValorPlan", SqlDbType.Real);
                    cmd.Parameters.Add("@decValorReal", SqlDbType.Real);
                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@decValorPlanPeriodo", SqlDbType.Real);
                    cmd.Parameters.Add("@decValorRealPeriodo", SqlDbType.Real);
                    cmd.Parameters.Add("@datFechaUltAct", SqlDbType.DateTime);
                    cmd.Parameters.Add("@intFlagProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl_CSFyGH", SqlDbType.Int);

                    cmd.Parameters[0].Value = var["AnioCampana"].ToString();
                    cmd.Parameters[1].Value = codigoPaisDd;
                    cmd.Parameters[2].Value = var["CodRegion"].ToString();
                    cmd.Parameters[3].Value = var["CodGerenteRegional"].ToString();
                    cmd.Parameters[4].Value = var["CodVariable"].ToString();
                    cmd.Parameters[5].Value = var["DesVariable"].ToString();
                    cmd.Parameters[6].Value = var["ValorPlan"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorPlan"]);
                    cmd.Parameters[7].Value = var["ValorReal"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorReal"]);
                    cmd.Parameters[8].Value = var["Periodo"].ToString();
                    cmd.Parameters[9].Value = var["ValorPlanPeriodo"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorPlanPeriodo"]);
                    cmd.Parameters[10].Value = var["ValorRealPeriodo"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorRealPeriodo"]);
                    cmd.Parameters[11].Value = Convert.ToDateTime(var["FechaUltAct"]);
                    cmd.Parameters[12].Value = var["FlagProceso"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagProceso"]);
                    cmd.Parameters[13].Value = var["FlagControl"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl"]);
                    cmd.Parameters[14].Value = var["FlagControl_CSFyGH"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl_CSFyGH"]);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        // cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                    cn.Close();
                }
            }
        }

        public void ObtenerDataFfvvGerenteZona(List<BePais> listaPaises)
        {
            var dt = new DataTable();

            using (var conn = ObtieneConexionDataMart())
            {
                conn.Open();
                var cmd1 = new SqlCommand("SELECT [AnioCampana], [CodPais], [CodRegion], [CodGerenteRegional], [codZona], [CodGerenteZona], [DesGerenteZona], [DocIdentidad], [CorreoElectronico], [EstadoCamp], [PtoRankingProdCamp], [Periodo], [EstadoPeriodo], [PtoRankingProdPeriodo], [FechaUltAct], [FlagProceso], [FlagControl], [FlagControl_CSFyGH], [CUBGR], [CUBGZ] FROM FFVVGerenteZona  WHERE CodPais=@codPais", conn)
                {
                    CommandType = CommandType.Text
                };
                cmd1.Parameters.Add("@codPais", SqlDbType.Char, 2);
                cmd1.Parameters["@codPais"].Value = CodigoPaisDataMart;

                var dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                conn.Close();
            }

            if (dt.Rows.Count <= 0) return;
            InsertarLogCarga("obtenerDataFFVVGerenteZona", "cantidad de registros:" + dt.Rows.Count);
            insertarESE_IN_GERENTE_ZONA(dt, listaPaises);
            ActualizarInGerenteZona(listaPaises);
            InsertarLogCarga("obtenerDataFFVVGerenteZona", "Fin de obtener datos FFVVGerenteZona");
        }

        /// <summary>
        /// Inserta los datos en la tabla ESE_IN_GERENTE_ZONA
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="listaPaises"></param>
        private void insertarESE_IN_GERENTE_ZONA(DataTable dt, List<BePais> listaPaises)
        {
            var codigoPaisDd = ObtenerPrefijoPais(CodigoPaisDataMart, listaPaises);
            foreach (DataRow var in dt.Rows)
            {
                using (var cn = ObtieneConexion())
                {
                    cn.Open();
                    var cmd = new SqlCommand("ESE_InsertarIN_GZona", cn) {CommandType = CommandType.StoredProcedure};

                    cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                    cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                    cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 6);
                    cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@vchDesGerenteZona", SqlDbType.VarChar, 55);
                    cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                    cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 55);
                    cmd.Parameters.Add("@vchEstadoCamp", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@intPtoRankingProdCamp", SqlDbType.Int);
                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@decPtoRankingProdPeriodo", SqlDbType.Decimal);
                    cmd.Parameters.Add("@datFechaUltAct", SqlDbType.DateTime);
                    cmd.Parameters.Add("@intFlagProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl_CSFyGH", SqlDbType.Int);
                    cmd.Parameters.Add("@vchCUBGR", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@vchCUBGZ", SqlDbType.VarChar, 20);

                    cmd.Parameters[0].Value = var["AnioCampana"].ToString();
                    cmd.Parameters[1].Value = codigoPaisDd;
                    cmd.Parameters[2].Value = var["CodRegion"].ToString();
                    cmd.Parameters[3].Value = var["CodGerenteRegional"].ToString();
                    cmd.Parameters[4].Value = var["codZona"].ToString();
                    cmd.Parameters[5].Value = var["CodGerenteZona"].ToString();
                    cmd.Parameters[6].Value = var["DesGerenteZona"].ToString();
                    cmd.Parameters[7].Value = var["DocIdentidad"].ToString();
                    cmd.Parameters[8].Value = var["correoElectronico"].ToString();
                    cmd.Parameters[9].Value = var["EstadoCamp"].ToString();
                    cmd.Parameters[10].Value = var["PtoRankingProdCamp"] == DBNull.Value ? 0 : Convert.ToInt32(var["PtoRankingProdCamp"]);
                    cmd.Parameters[11].Value = var["Periodo"].ToString();
                    cmd.Parameters[12].Value = var["EstadoPeriodo"].ToString();
                    cmd.Parameters[13].Value = var["PtoRankingProdPeriodo"] == DBNull.Value ? 0 : Convert.ToDecimal(var["PtoRankingProdPeriodo"]);
                    cmd.Parameters[14].Value = Convert.ToDateTime(var["FechaUltAct"]);
                    cmd.Parameters[15].Value = var["FlagProceso"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagProceso"]);
                    cmd.Parameters[16].Value = var["FlagControl"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl"]);
                    cmd.Parameters[17].Value = var["FlagControl_CSFyGH"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl_CSFyGH"]);
                    cmd.Parameters[18].Value = var["CUBGR"] == DBNull.Value ? "" : var["CUBGR"].ToString();
                    cmd.Parameters[19].Value = var["CUBGZ"] == DBNull.Value ? "" : var["CUBGZ"].ToString();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        //cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                    cn.Close();
                }
            }
        }

        private static void ActualizarInGerenteZona(IEnumerable<BePais> listaPaises)
        {
            foreach (var pais in listaPaises)
            {
                if (!pais.ActualizacionDM) return;

                using (var cn = ObtieneConexion())
                {
                    var cmd = new SqlCommand("ESE_Actualizar_IN_GERENTE_ZONA_DM", cn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrCodigoPais", SqlDbType.Char, 2);
                    cmd.Parameters["@chrCodigoPais"].Value = pais.prefijoIsoPais;

                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
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
            }
        }

        public void ObtenerDataFfvvDetalleGz(List<BePais> listaPaises)
        {
            // bool resultado = false;

            var dt = new DataTable();
            using (var conn = ObtieneConexionDataMart())
            {
                conn.Open();
                var cmd1 = new SqlCommand("SELECT [AnioCampana], [CodPais], [CodRegion], [codZona], [CodGerenteRegional], [CodGerenteZona], [CodVariable], [DesVariable], [ValorPlan], [ValorReal], [Periodo], [ValorPlanPeriodo], [ValorRealPeriodo], [FechaUltAct], [FlagProceso], [FlagControl], [FlagControl_CSFyGH] FROM FFVVDetalleGZ  WHERE CodPais=@codPais", conn)
                {
                    CommandType = CommandType.Text
                };
                cmd1.Parameters.Add("@codPais", SqlDbType.Char, 2);
                cmd1.Parameters["@codPais"].Value = CodigoPaisDataMart;

                var dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                conn.Close();
            }
            if (dt.Rows.Count <= 0) return;
            InsertarLogCarga("obtenerDataFFVVDetalleGZ", "cantidad de registros:" + dt.Rows.Count);
            insertarESE_IN_DETALLE_GERENTE_ZONA(dt, listaPaises);
            InsertarLogCarga("obtenerDataFFVVDetalleGZ", "Fin de obtener datos FFVVDetalleGZ");
            // return dt;
            //
        }

        private void insertarESE_IN_DETALLE_GERENTE_ZONA(DataTable dt, List<BePais> listaPaises)
        {
            var codigoPaisDd = ObtenerPrefijoPais(CodigoPaisDataMart, listaPaises);
            foreach (DataRow var in dt.Rows)
            {
                using (var cn = ObtieneConexion())
                {
                    cn.Open();
                    var cmd = new SqlCommand("ESE_InsertarIN_GZona_Detalle", cn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                    cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                    cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                    cmd.Parameters.Add("@chrCodZona", SqlDbType.Char, 6);

                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@vchCodVariable", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@vchDesVariable", SqlDbType.VarChar, 75);
                    cmd.Parameters.Add("@decValorPlan", SqlDbType.Real);
                    cmd.Parameters.Add("@decValorReal", SqlDbType.Real);
                    cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                    cmd.Parameters.Add("@decValorPlanPeriodo", SqlDbType.Real);
                    cmd.Parameters.Add("@decValorRealPeriodo", SqlDbType.Real);
                    cmd.Parameters.Add("@datFechaUltAct", SqlDbType.DateTime);
                    cmd.Parameters.Add("@intFlagProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl_CSFyGH", SqlDbType.Int);

                    cmd.Parameters[0].Value = var["AnioCampana"].ToString();
                    cmd.Parameters[1].Value = codigoPaisDd;
                    cmd.Parameters[2].Value = var["CodRegion"].ToString();
                    cmd.Parameters[3].Value = var["codZona"].ToString();
                    cmd.Parameters[4].Value = var["CodGerenteRegional"].ToString();
                    cmd.Parameters[5].Value = var["CodGerenteZona"].ToString();
                    cmd.Parameters[6].Value = var["CodVariable"].ToString();
                    cmd.Parameters[7].Value = var["DesVariable"].ToString();
                    cmd.Parameters[8].Value = var["ValorPlan"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorPlan"]);
                    cmd.Parameters[9].Value = var["ValorReal"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorReal"]);
                    cmd.Parameters[10].Value = var["Periodo"].ToString();
                    cmd.Parameters[11].Value = var["ValorPlanPeriodo"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorPlanPeriodo"]);
                    cmd.Parameters[12].Value = var["ValorRealPeriodo"] == DBNull.Value ? 0 : Convert.ToDouble(var["ValorRealPeriodo"]);
                    cmd.Parameters[13].Value = Convert.ToDateTime(var["FechaUltAct"]);
                    cmd.Parameters[14].Value = var["FlagProceso"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagProceso"]);
                    cmd.Parameters[15].Value = var["FlagControl"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl"]);
                    cmd.Parameters[16].Value = var["FlagControl_CSFyGH"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl_CSFyGH"]);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        // cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex.InnerException);
                    }

                    cn.Close();
                }
            }
        }

        public void ObtenerDataFfvvLet(List<BePais> listaPaises)
        {
            var dt = new DataTable();
            using (var conn = ObtieneConexionDataMart())
            {
                conn.Open();
                var cmd1 = new SqlCommand("SELECT [AnioCampana], [CodPais], [CodRegion], [CodGerenteRegional], [CodGerenteZona], [CodigoConsultoraLET], [DesNombreLET], [CorreoElectronico], [EstadoCamp], [EstadoPeriodo], [FechaUltAct], [FlagProceso], [FlagControl], [FlagControl_CSFyGH] FROM FFVVLet  WHERE CodPais=@codPais", conn)
                {
                    CommandType = CommandType.Text
                };
                cmd1.Parameters.Add("@codPais", SqlDbType.Char, 2);
                cmd1.Parameters["@codPais"].Value = CodigoPaisDataMart;

                var dap = new SqlDataAdapter(cmd1);
                dap.Fill(dt);
                conn.Close();
            }

            if (dt.Rows.Count <= 0) return;
            InsertarLogCarga("obtenerDataFFVVLet", "cantidad de registros:" + dt.Rows.Count);
            insertarESE_INT_LET(dt, listaPaises);
            InsertarLogCarga("obtenerDataFFVVLet", "Fin de obtener datos FFVVLet");
        }

        /// <summary>
        /// Inserta los datos en la tabla ESE_INT_LET
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="listaPaises"></param>
        private void insertarESE_INT_LET(DataTable dt, List<BePais> listaPaises)
        {
            var codigoPaisDd = ObtenerPrefijoPais(CodigoPaisDataMart, listaPaises);
            foreach (DataRow var in dt.Rows)
            {
                using (var cn = ObtieneConexion())
                {
                    cn.Open();
                    var cmd = new SqlCommand("ESE_InsertarIN_LET", cn) {CommandType = CommandType.StoredProcedure};

                    cmd.Parameters.Add("@chrAnioCampana", SqlDbType.Char, 6);
                    cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);
                    cmd.Parameters.Add("@chrCodRegion", SqlDbType.Char, 3);
                    cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                    cmd.Parameters.Add("@vchCodigoConsultoraLET", SqlDbType.VarChar, 10);
                    cmd.Parameters.Add("@vchDesNombreLET", SqlDbType.VarChar, 55);
                    cmd.Parameters.Add("@vchCorreoElectronico", SqlDbType.VarChar, 55);
                    cmd.Parameters.Add("@vchEstadoCamp", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@vchEstadoPeriodo", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@FechaUltAct", SqlDbType.DateTime);
                    cmd.Parameters.Add("@intProceso", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl", SqlDbType.Int);
                    cmd.Parameters.Add("@intFlagControl_CSFyGH", SqlDbType.Int);

                    cmd.Parameters[0].Value = var["AnioCampana"].ToString();
                    cmd.Parameters[1].Value = codigoPaisDd;
                    cmd.Parameters[2].Value = var["CodRegion"].ToString();
                    cmd.Parameters[3].Value = var["CodGerenteRegional"].ToString();
                    cmd.Parameters[4].Value = var["CodGerenteZona"].ToString();
                    cmd.Parameters[5].Value = var["CodigoConsultoraLET"].ToString();
                    cmd.Parameters[6].Value = var["DesNombreLET"].ToString();
                    cmd.Parameters[7].Value = var["CorreoElectronico"].ToString();
                    cmd.Parameters[8].Value = var["EstadoCamp"].ToString();
                    cmd.Parameters[9].Value = var["EstadoPeriodo"].ToString();
                    cmd.Parameters[10].Value = Convert.ToDateTime(var["FechaUltAct"]);
                    cmd.Parameters[11].Value = var["FlagProceso"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagProceso"]);
                    cmd.Parameters[12].Value = var["FlagControl"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl"]);
                    cmd.Parameters[13].Value = var["FlagControl_CSFyGH"] == DBNull.Value ? 0 : Convert.ToInt32(var["FlagControl_CSFyGH"]);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        // cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                    cn.Close();
                }
            }
        }

        public string InsertarEnTablaTRX_GR()
        {
            var grabo = "";
            using (var conn = ObtieneConexion())
            {
                conn.Open();

                var transaccion = conn.BeginTransaction();
                var cmd = new SqlCommand("ESE_InsertarIN_TRXGR", conn, transaccion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };
                try
                {
                    cmd.ExecuteNonQuery();
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    grabo = "ESE_InsertarIN_TRXGR," + ex.Message;
                }

                conn.Close();
            }
            return grabo;
        }

        public string InsertarEnTablaTRX_GZ()
        {
            var grabo = "";

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var transaccion = conn.BeginTransaction();
                var cmd = new SqlCommand("ESE_InsertarIN_TRXGZ", conn, transaccion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };
                try
                {
                    cmd.ExecuteNonQuery();
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    grabo = "ESE_InsertarIN_TRXGZ," + ex.Message;
                }
                conn.Close();
            }
            return grabo;
        }

        public string InsertarEnTablaTRXDetalle_GR()
        {
            var grabo = "";

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var transaccion = conn.BeginTransaction();
                var cmd = new SqlCommand("ESE_InsertarIN_TRXDETALLEGR", conn, transaccion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };
                try
                {
                    cmd.ExecuteNonQuery();
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    transaccion.Rollback();
                    grabo = "ESE_InsertarIN_TRXDETALLEGR," + ex.Message;
                }
                conn.Close();
            }
            return grabo;
        }

        public string InsertarEnTablaTRXDetalle_GZ()
        {
            var grabo = "";

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var transaccion = conn.BeginTransaction();
                var cmd = new SqlCommand("ESE_InsertarIN_TRXDETALLEGZ", conn, transaccion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };
                try
                {
                    cmd.ExecuteNonQuery();

                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    transaccion.Rollback();
                    grabo = "ESE_InsertarIN_TRXDETALLEGZ," + ex.Message;
                }
                conn.Close();
            }
            return grabo;
        }

        public string InsertarEnTablaTrxlet()
        {
            var grabo = "";

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var transaccion = conn.BeginTransaction();
                var cmd = new SqlCommand("ESE_InsertarIN_TRXLET", conn, transaccion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };
                try
                {
                    cmd.ExecuteNonQuery();

                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    transaccion.Rollback();
                    grabo = "ESE_InsertarIN_TRXLET," + ex.Message;
                }
                conn.Close();
            }
            return grabo;
        }


        public string ActualizarEstandarizacionCodigo()
        {
            var grabo = "";

            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var transaccion = conn.BeginTransaction();
                var cmd = new SqlCommand("ESE_ActualizarEstandarizacionCodigo", conn, transaccion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };
                try
                {
                    cmd.ExecuteNonQuery();

                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex.InnerException);
                    transaccion.Rollback();
                    grabo = "ESE_ActualizarEstandarizacionCodigo," + ex.Message;
                }
                conn.Close();
            }
            return grabo;
        }

        /// <summary>
        /// Registra los logs
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="descripcion"></param>
        public void InsertarLogCarga(string seccion, string descripcion)
        {
            //ESE_Insertar_LogCarga
            using (var conn = ObtieneConexion())
            {
                conn.Open();
                var cmd = new SqlCommand("ESE_Insertar_LogCarga", conn)
                {
                    CommandTimeout = 300,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@vchSeccion", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@vchDescripcion", SqlDbType.VarChar, 500);
                cmd.Parameters["@vchSeccion"].Value = seccion;
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
    }
}