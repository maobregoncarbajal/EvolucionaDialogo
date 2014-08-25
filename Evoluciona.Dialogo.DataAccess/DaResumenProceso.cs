
namespace Evoluciona.Dialogo.DataAccess
{
    using BusinessEntity;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DaResumenProceso : DaConexion
    {
        /// <summary>
        /// Obtiene el resumen de proceso del usuario
        /// </summary>
        /// <returns>la lista de los procesos en el perido determinado</returns>
        public BeResumenProceso ObtenerResumenProcesoByUsuario(string codigoUsuarioEvaluado, int idRol, string periodo, string prefijoIsoPais, string tipoDialogoDesempenio)
        {
            BeResumenProceso objResumen = null;
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_ResumenProcesoByUsuario", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);
                cmd.Parameters.Add("@vchrTipoDialogoDesempenio", SqlDbType.VarChar, 20);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuarioEvaluado;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;
                cmd.Parameters["@vchrTipoDialogoDesempenio"].Value = tipoDialogoDesempenio;


                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    objResumen = new BeResumenProceso
                    {
                        codigoUsuario = codigoUsuarioEvaluado,
                        estadoProceso = dr["chrEstadoProceso"].ToString(),
                        prefijoIsoPais = prefijoIsoPais,
                        idProceso = Convert.ToInt32(dr["intIDProceso"].ToString()),
                        rolUsuarioEvaluador = Convert.ToInt32(dr["intIDRolEvaluador"].ToString()),
                        codigoUsuarioEvaluador = dr["chrCodigoUsuarioEvaluador"].ToString(),
                        fechaCreacion = Convert.ToDateTime(dr["datFechaCrea"].ToString()),
                        cub = dr["cub"].ToString()
                    };

                    if (!dr.IsDBNull(dr.GetOrdinal("intNuevasIngresadas")))
                        objResumen.NuevasIngresadas = dr.GetInt32(dr.GetOrdinal("intNuevasIngresadas"));
                }
                dr.Close();
            }

            return objResumen;
        }


        public BeResumenProceso ObtenerResumenProcesoByUsuarioPlanDeMejora(string codigoUsuarioEvaluado, int idRol, string periodo, string prefijoIsoPais)
        {
            BeResumenProceso objResumen = null;
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_ResumenProcesoByUsuarioPlanDeMejora", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuario"].Value = codigoUsuarioEvaluado;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    objResumen = new BeResumenProceso
                    {
                        codigoUsuario = codigoUsuarioEvaluado,
                        estadoProceso = dr["chrEstadoProceso"].ToString(),
                        prefijoIsoPais = prefijoIsoPais,
                        idProceso = Convert.ToInt32(dr["intIDProceso"].ToString()),
                        rolUsuarioEvaluador = Convert.ToInt32(dr["intIDRolEvaluador"].ToString()),
                        codigoUsuarioEvaluador = dr["chrCodigoUsuarioEvaluador"].ToString(),
                        fechaCreacion = Convert.ToDateTime(dr["datFechaCrea"].ToString()),
                        cub = dr["cub"].ToString()
                    };

                    if (!dr.IsDBNull(dr.GetOrdinal("intNuevasIngresadas")))
                        objResumen.NuevasIngresadas = dr.GetInt32(dr.GetOrdinal("intNuevasIngresadas"));
                }
                dr.Close();
            }

            return objResumen;
        }


        /// <summary>
        /// Busca a las GR para Iniciar el dialogo
        /// </summary>
        /// <param name="codigoGRegion"></param>
        /// <param name="docuIdentidad"></param>
        /// <param name="nombres"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="codigoRol"></param>
        /// <returns></returns>
        public DataTable BuscarGRegionParaInicioDialogo(string codigoGRegion, string docuIdentidad, string nombres, string prefijoIsoPais, string periodo, int codigoRol)
        {
            var dtDatos = new DataTable();

            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Buscar_GR_ParaInicioDialogo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombres", SqlDbType.Char, 100);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoGRegion;
                cmd.Parameters["@chrDocIdentidad"].Value = docuIdentidad;
                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EmailEnviado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }

        /// <summary>
        /// Busca a las Gz para Inicial el Dialogo
        /// </summary>
        /// <param name="docuIdentidadGRegion"></param>
        /// <param name="docuIdentidad"></param>
        /// <param name="codGerenteZona"></param>
        /// <param name="nombres"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="codigoRol"></param>
        /// <returns></returns>
        public DataTable BuscarGZonaParaInicioDialogo(string docuIdentidadGRegion, string docuIdentidad, string codGerenteZona, string nombres, string prefijoIsoPais, string periodo, int codigoRol)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Buscar_GZ_ParaInicioDialogo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrDocIdentidadGR", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@vchNombres", SqlDbType.Char, 100);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrDocIdentidadGR"].Value = docuIdentidadGRegion;
                cmd.Parameters["@chrDocIdentidad"].Value = docuIdentidad;
                cmd.Parameters["@chrCodGerenteZona"].Value = codGerenteZona;
                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EmailEnviado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }

        /// <summary>
        /// Busca a las GR que han iniciado proceso
        /// </summary>
        /// <param name="docuIdentidad"></param>
        /// <param name="nombres"></param>
        /// <param name="codGerenteRegion"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataTable BuscarResumenProcesoGr(string docuIdentidad, string nombres, string codGerenteRegion, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Buscar_ResumenProceso_GR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrDocIdentidad"].Value = docuIdentidad;
                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodGerenteRegional"].Value = codGerenteRegion;
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }

        /// <summary>
        /// Busca las GZ que han iniciado Proceso
        /// </summary>
        /// <param name="docuIdentidad"></param>
        /// <param name="nombres"></param>
        /// <param name="codGerenteZona"></param>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public DataTable BuscarResumenProcesoGz(string docuIdentidad, string nombres, string codGerenteZona, string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Buscar_ResumenProceso_GZ", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrDocIdentidad", SqlDbType.Char, 20);
                cmd.Parameters.Add("@vchNombres", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrDocIdentidad"].Value = docuIdentidad;
                cmd.Parameters["@vchNombres"].Value = nombres;
                cmd.Parameters["@chrCodGerenteZona"].Value = codGerenteZona;
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }

        /// <summary>
        /// Selecciona los GR que estan listos para iniciar el dialogo
        /// </summary>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="codigoRol"></param>
        /// <param name="codigoDv"></param>
        /// <returns></returns>
        public DataTable SeleccionarGRegionParaInicioDialogo(string prefijoIsoPais, string periodo, int codigoRol, string codigoDv)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_Usuarios_GR_ParaInicioDialogo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);
                cmd.Parameters.Add("@chrCodigoDV", SqlDbType.Char, 20);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EmailEnviado;
                cmd.Parameters["@chrCodigoDV"].Value = codigoDv;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }


        public DataTable SeleccionarGRegionParaInicioDialogoPlanDeMejora(string prefijoIsoPais, string periodo, int codigoRol, string codigoDv)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_Usuarios_GR_ParaInicioDialogoPlanDeMejora", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);
                cmd.Parameters.Add("@chrCodigoDV", SqlDbType.Char, 20);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EmailEnviado;
                cmd.Parameters["@chrCodigoDV"].Value = codigoDv;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }


        /// <summary>
        /// Selecciona los GZ que estan listos para iniciar el dialogo
        /// </summary>
        /// <param name="codigoGRegion"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="codigoRol"></param>
        /// <returns></returns>
        public DataTable SeleccionarGZonaParaInicioDialogo(string codigoGRegion, string prefijoIsoPais, string periodo, int codigoRol)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_Usuarios_GZ_ParaInicioDialogo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoGRegion;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EmailEnviado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }



        public DataTable SeleccionarGZonaParaInicioDialogoPlanDeMejora(string codigoGRegion, string prefijoIsoPais, string periodo, int codigoRol)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_Usuarios_GZ_ParaInicioDialogoPlanDeMejora", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@intCodigoRol", SqlDbType.Int);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoGRegion;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@intCodigoRol"].Value = codigoRol;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EmailEnviado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }



        public DataTable SeleccionarLideresParaInicioDialogo(string codigoGZona, string prefijoIsoPais, string periodo)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_Usuarios_LET_ParaInicioDialogo", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrDocIdentidadGZ", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEnviado", SqlDbType.Bit);

                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrDocIdentidadGZ"].Value = codigoGZona;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEnviado"].Value = Constantes.EstadoActivo;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }

        /// <summary>
        /// Selecciona los procesos realizados por el usuario GR
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns>Lista con todos los procesos realizados</returns>
        public DataTable SeleccionarResumenProcesoGr(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_GR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }


        public DataTable SeleccionarResumenProcesoGrPlanDeMejora(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_GRPlanDeMejora", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }


        public List<BeComun> SeleccionarResumenProcesoGrList(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var comunes = new List<BeComun>();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_GR", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var comun = new BeComun
                    {
                        Codigo = reader["intIDProceso"].ToString(),
                        Descripcion = reader["Nombres"].ToString()
                    };

                    comunes.Add(comun);
                }

                reader.Close();
            }

            return comunes;
        }

        /// <summary>
        /// Selecciona los procesos realizados por el usuario GZ
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns>Lista con todos los procesos realizados</returns>
        public DataTable SeleccionarResumenProcesoGz(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_GZ", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }


        public DataTable SeleccionarResumenProcesoGzPlanDeMejora(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_GZPlanDeMejora", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }


        public List<BeComun> SeleccionarResumenProcesoGzList(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var comunes = new List<BeComun>();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_GZ", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var comun = new BeComun
                    {
                        Codigo = reader["intIDProceso"].ToString(),
                        Descripcion = reader["Nombres"].ToString()
                    };

                    comunes.Add(comun);
                }

                reader.Close();
            }

            return comunes;
        }

        /// <summary>
        /// Selecciona los procesos realizados por el usuario Lider
        /// </summary>
        /// <param name="codigoUsuarioEvaluador"></param>
        /// <param name="idRol"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estadoProceso"></param>
        /// <param name="estado"></param>
        /// <returns>Lista con todos los procesos realizados</returns>
        public DataTable SeleccionarResumenProcesoLider(string codigoUsuarioEvaluador, int idRol, string prefijoIsoPais, string periodo, string estadoProceso, byte estado)
        {
            var dtDatos = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Seleccionar_ResumenProceso_Lider", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = codigoUsuarioEvaluador;
                cmd.Parameters["@intIDRol"].Value = idRol;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@chrEstadoProceso"].Value = estadoProceso;
                cmd.Parameters["@bitEstado"].Value = estado;

                var dap = new SqlDataAdapter(cmd);

                dap.Fill(dtDatos);
            }

            return dtDatos;
        }

        /// <summary>
        /// Obtiene los datos del usuario GR a ser evaluado
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GR</returns>
        public BeResumenProceso ObtenerUsuarioGRegionEvaluado(string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            var objUsuario = new BeResumenProceso();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_Datos_GR_Evaluada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = estado;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objUsuario.codigoUsuario = codigoUsuario;
                        objUsuario.nombreEvaluado = dr["NombreCompleto"].ToString();
                        objUsuario.email = dr["correoDataMart"].ToString();
                        objUsuario.codigoGRegion = dr["chrCodGerenteRegional"].ToString();
                        objUsuario.prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString();

                        var obUsuarioDa = new DaUsuario();
                        var dt = obUsuarioDa.ObtenerDatosRol(Constantes.RolGerenteRegion, Constantes.EstadoActivo);
                        if (dt.Rows.Count > 0)
                        {
                            objUsuario.rolUsuario = Convert.ToInt32(dt.Rows[0]["intIDRol"]);
                            objUsuario.rolDescripcion = dt.Rows[0]["vchDescripcion"].ToString();
                        }
                    }
                    else
                    {
                        objUsuario = null;
                    }
                    dr.Close();
                }
            }
            return objUsuario;
        }



        /// <summary>
        /// Obtiene los datos del usuario GR a ser evaluado
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GR</returns>
        public BeResumenProceso ObtenerUsuarioNuevaGRegionEvaluado(string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            var objUsuario = new BeResumenProceso();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_Datos_Nueva_GR_Evaluada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = estado;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objUsuario.codigoUsuario = codigoUsuario;
                        objUsuario.nombreEvaluado = dr["NombreCompleto"].ToString();
                        objUsuario.email = dr["correoDataMart"].ToString();
                        objUsuario.codigoGRegion = dr["chrCodGerenteRegional"].ToString();
                        objUsuario.prefijoIsoPais = dr["chrPrefijoIsoPais"].ToString();

                        var obUsuarioDa = new DaUsuario();
                        var dt = obUsuarioDa.ObtenerDatosRol(Constantes.RolGerenteRegion, Constantes.EstadoActivo);
                        if (dt.Rows.Count > 0)
                        {
                            objUsuario.rolUsuario = Convert.ToInt32(dt.Rows[0]["intIDRol"]);
                            objUsuario.rolDescripcion = dt.Rows[0]["vchDescripcion"].ToString();
                        }
                    }
                    else
                    {
                        objUsuario = null;
                    }
                    dr.Close();
                }
            }
            return objUsuario;
        }






        /// <summary>
        /// Obtiene los datos del usuario GZ a ser evaluado
        /// </summary>
        /// <param name="idUsuarioGRegion"></param>
        /// <param name="codigoUsuarioGRegion"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GZ</returns>
        public BeResumenProceso ObtenerUsuarioGZonaEvaluado(int idUsuarioGRegion, string codigoUsuarioGRegion, string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            var objUsuario = new BeResumenProceso();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_Datos_GZ_Evaluada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDGerenteRegion"].Value = idUsuarioGRegion;
                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoUsuarioGRegion;
                cmd.Parameters["@chrCodGerenteZona"].Value = codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = estado;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objUsuario.codigoUsuario = codigoUsuario;
                        objUsuario.nombreEvaluado = dr["NombreCompleto"].ToString();
                        objUsuario.email = dr["correoDataMart"].ToString();
                        objUsuario.codigoGZona = dr["chrCodGerenteZona"].ToString();
                        var obUsuarioDa = new DaUsuario();
                        var dt = obUsuarioDa.ObtenerDatosRol(Constantes.RolGerenteZona, Constantes.EstadoActivo);
                        if (dt.Rows.Count > 0)
                        {
                            objUsuario.rolUsuario = Convert.ToInt32(dt.Rows[0]["intIDRol"]);
                            objUsuario.rolDescripcion = dt.Rows[0]["vchDescripcion"].ToString();
                        }
                    }
                    else
                    {
                        objUsuario = null;
                    }
                    dr.Close();
                }
            }
            return objUsuario;
        }


        /// <summary>
        /// Obtiene los datos del usuario GZ a ser evaluado
        /// </summary>
        /// <param name="idUsuarioGRegion"></param>
        /// <param name="codigoUsuarioGRegion"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>el objeto Resumen con los datos de la GZ</returns>
        public BeResumenProceso ObtenerUsuarioNuevaGZonaEvaluado(int idUsuarioGRegion, string codigoUsuarioGRegion, string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            var objUsuario = new BeResumenProceso();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_Datos_Nueva_GZ_Evaluada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDGerenteRegion", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodGerenteRegional", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDGerenteRegion"].Value = idUsuarioGRegion;
                cmd.Parameters["@chrCodGerenteRegional"].Value = codigoUsuarioGRegion;
                cmd.Parameters["@chrCodGerenteZona"].Value = codigoUsuario;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = prefijoIsoPais;
                cmd.Parameters["@chrPeriodo"].Value = periodo;
                cmd.Parameters["@bitEstado"].Value = estado;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objUsuario.codigoUsuario = codigoUsuario;
                        objUsuario.nombreEvaluado = dr["NombreCompleto"].ToString();
                        objUsuario.email = dr["correoDataMart"].ToString();
                        objUsuario.codigoGZona = dr["chrCodGerenteZona"].ToString();
                        var obUsuarioDa = new DaUsuario();
                        var dt = obUsuarioDa.ObtenerDatosRol(Constantes.RolGerenteZona, Constantes.EstadoActivo);
                        if (dt.Rows.Count > 0)
                        {
                            objUsuario.rolUsuario = Convert.ToInt32(dt.Rows[0]["intIDRol"]);
                            objUsuario.rolDescripcion = dt.Rows[0]["vchDescripcion"].ToString();
                        }
                    }
                    else
                    {
                        objUsuario = null;
                    }
                    dr.Close();
                }
            }
            return objUsuario;
        }


        /// <summary>
        /// Obtiene los datos de la lider evaluada
        /// </summary>
        /// <param name="codigoUsuarioGerenteZona"></param>
        /// <param name="codigoUsuario"></param>
        /// <param name="prefijoIsoPais"></param>
        /// <param name="periodo"></param>
        /// <param name="estado"></param>
        /// <returns>Los datos de la lider</returns>
        public BeResumenProceso ObtenerUsuarioLiderEvaluado(string codigoUsuarioGerenteZona, string codigoUsuario, string prefijoIsoPais, string periodo, byte estado)
        {
            var objUsuario = new BeResumenProceso();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_Datos_Lider_Evaluada", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@chrCodGerenteZona", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodigoConsultoraLET", SqlDbType.Char, 10);
                cmd.Parameters.Add("@chrCodPais", SqlDbType.Char, 2);

                cmd.Parameters["@chrCodGerenteZona"].Value = codigoUsuarioGerenteZona;
                cmd.Parameters["@chrCodigoConsultoraLET"].Value = codigoUsuario;
                cmd.Parameters["@chrCodPais"].Value = prefijoIsoPais;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objUsuario.codigoUsuario = codigoUsuario;
                        objUsuario.nombreEvaluado = dr["NombreCompleto"].ToString();

                        var obUsuarioDa = new DaUsuario();
                        var dt = obUsuarioDa.ObtenerDatosRol(Constantes.RolGerenteZona, Constantes.EstadoActivo);
                        if (dt.Rows.Count > 0)
                        {
                            objUsuario.rolUsuario = Convert.ToInt32(dt.Rows[0]["intIDRol"]);
                            objUsuario.rolDescripcion = dt.Rows[0]["vchDescripcion"].ToString();
                        }
                    }
                    else
                    {
                        objUsuario = null;
                    }
                    dr.Close();
                }
            }
            return objUsuario;
        }

        /// <summary>
        /// Inserta el proceso para inicial el dialogo de desempeño
        /// </summary>
        /// <param name="objResumenBe"></param>
        /// <returns></returns>
        public bool InsertarProceso(BeResumenProceso objResumenBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Insertar_ResumenProceso", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@datFechaLimiteProceso", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@intIDRolEvaluador", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDRol"].Value = objResumenBe.rolUsuario;
                cmd.Parameters["@chrCodigoUsuario"].Value = objResumenBe.codigoUsuario;
                cmd.Parameters["@chrPeriodo"].Value = objResumenBe.periodo;
                cmd.Parameters["@datFechaLimiteProceso"].Value = objResumenBe.fechaLimiteProceso;
                cmd.Parameters["@chrEstadoProceso"].Value = objResumenBe.estadoProceso;
                cmd.Parameters["@intIDRolEvaluador"].Value = objResumenBe.rolUsuarioEvaluador;
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = objResumenBe.codigoUsuarioEvaluador;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumenBe.prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                cmd.ExecuteNonQuery();
            }
            return true;
        }


        public bool InsertarProcesoPlanDeMejora(BeResumenProceso objResumenBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Insertar_ResumenProcesoPlanDeMejora", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDRol", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuario", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPeriodo", SqlDbType.Char, 8);
                cmd.Parameters.Add("@datFechaLimiteProceso", SqlDbType.DateTime);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);
                cmd.Parameters.Add("@intIDRolEvaluador", SqlDbType.Int);
                cmd.Parameters.Add("@chrCodigoUsuarioEvaluador", SqlDbType.Char, 20);
                cmd.Parameters.Add("@chrPrefijoIsoPais", SqlDbType.Char, 2);
                cmd.Parameters.Add("@bitEstado", SqlDbType.Bit);

                cmd.Parameters["@intIDRol"].Value = objResumenBe.rolUsuario;
                cmd.Parameters["@chrCodigoUsuario"].Value = objResumenBe.codigoUsuario;
                cmd.Parameters["@chrPeriodo"].Value = objResumenBe.periodo;
                cmd.Parameters["@datFechaLimiteProceso"].Value = objResumenBe.fechaLimiteProceso;
                cmd.Parameters["@chrEstadoProceso"].Value = objResumenBe.estadoProceso;
                cmd.Parameters["@intIDRolEvaluador"].Value = objResumenBe.rolUsuarioEvaluador;
                cmd.Parameters["@chrCodigoUsuarioEvaluador"].Value = objResumenBe.codigoUsuarioEvaluador;
                cmd.Parameters["@chrPrefijoIsoPais"].Value = objResumenBe.prefijoIsoPais;
                cmd.Parameters["@bitEstado"].Value = Constantes.EstadoActivo;

                cmd.ExecuteNonQuery();
            }
            return true;
        }


        /// <summary>
        /// Actualiza el estado del proceso
        /// </summary>
        /// <param name="objResumenBe"></param>
        /// <returns></returns>
        public bool ActualizarProceso(BeResumenProceso objResumenBe)
        {
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Actualizar_ResumenProceso", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);
                cmd.Parameters.Add("@chrEstadoProceso", SqlDbType.Char, 1);

                cmd.Parameters["@intIDProceso"].Value = objResumenBe.idProceso;
                cmd.Parameters["@chrEstadoProceso"].Value = objResumenBe.estadoProceso;

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public DataTable ObtenerResumenProcesoById(int idProceso)
        {
            //ESE_Obtener_ResumenProcesoByID
            var dt = new DataTable();
            using (var conex = ObtieneConexion())
            {
                conex.Open();
                var cmd = new SqlCommand("ESE_Obtener_ResumenProcesoByID", conex)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@intIDProceso", SqlDbType.Int);

                cmd.Parameters["@intIDProceso"].Value = idProceso;

                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conex.Close();
            }
            return dt;
        }
    }
}