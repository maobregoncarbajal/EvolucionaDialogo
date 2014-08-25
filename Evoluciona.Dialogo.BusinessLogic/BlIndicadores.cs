
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;
    using System.Data;

    public class BlIndicadores
    {

        private static readonly DaIndicadores DaIndicadores = new DaIndicadores();

        public DataTable ObtenerPeriodo(string ddlperiodo, int codigoRolUsuario, string prefijoIsoPais,
                                        string connstring)
        {
            return DaIndicadores.ObtenerPeriodo(ddlperiodo, codigoRolUsuario, prefijoIsoPais, connstring);
        }

        public DataTable ObtenerCampanaDesde(string ddlCampana, int codigoRolUsuario, string prefijoIsoPais,
                                             string periodoActual, string connstring)
        {
            return DaIndicadores.ObtenerCampanaDesde(ddlCampana, codigoRolUsuario, prefijoIsoPais, periodoActual, connstring);
        }

        public DataTable ObtenerCampanaHasta(string ddlCampana, int codigoRolUsuario, string prefijoIsoPais,
                                             string connstring)
        {
            return DaIndicadores.ObtenerCampanaHasta(ddlCampana, codigoRolUsuario, prefijoIsoPais, connstring);
        }

        public DataSet Cargarindicadoresporperiodo(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                   int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.Cargarindicadoresporperiodo(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                         prefijoIsoPais, connstring);
        }

        public DataSet CargarindicadoresporperiodoPreDialogo(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                           int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoPreDialogo(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                         prefijoIsoPais, connstring);
        }

        public DataSet ObtenerVariablesEnfoqueSeleccionadasPorPeriodo(string periodo, string codigo, string pais, int rol, byte variablesAdicionales)
        {
            return DaIndicadores.ObtenerVariablesEnfoqueSeleccionadasPorPeriodo(periodo, codigo, pais, rol, variablesAdicionales);
        }

        public DataSet ObtenerVariablesEnfoqueSeleccionadasPorPeriodoPreDialogo(string periodo, string codigo, string pais, int rol, byte variablesAdicionales)
        {
            return DaIndicadores.ObtenerVariablesEnfoqueSeleccionadasPorPeriodoPreDialogo(periodo, codigo, pais, rol, variablesAdicionales);
        }


        public DataSet CargarindicadoresporperiodoNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                   int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoNuevas(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                         prefijoIsoPais, connstring);
        }

        public DataSet CargarindicadoresporperiodoEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                           int codigoRolUsuario, string prefijoIsoPais,
                                                           string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoEvaluado(ddlperiodo, codigoUsuario, intIdProceso,
                                                                 codigoRolUsuario, prefijoIsoPais, connstring);
        }

        public DataSet Cargarindicadoresporcampana(string ddlCampana, string codigoUsuario, int intIdProceso,
                                                   int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.Cargarindicadoresporcampana(ddlCampana, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                         prefijoIsoPais, connstring);
        }


        public DataSet CargarindicadoresporcampanaNuevas(string ddlCampana, string codigoUsuario, int intIdProceso,
                                                   int codigoRolUsuario, string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporcampanaNuevas(ddlCampana, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                         prefijoIsoPais, connstring);
        }


        public DataSet CargarindicadoresporcampanaEvaluado(string ddlCampana, string codigoUsuario, int intIdProceso,
                                                           int codigoRolUsuario, string prefijoIsoPais,
                                                           string connstring)
        {
            return DaIndicadores.CargarindicadoresporcampanaEvaluado(ddlCampana, codigoUsuario, intIdProceso,
                                                                 codigoRolUsuario, prefijoIsoPais, connstring);
        }

        public DataSet CargarIndicadoresPorRangoCampana(string campanaDesde, string campanaHasta, string codigoUsuario,
                                                        int idProceso, int codigoRolUsuario, string prefijoIsoPais)
        {
            return DaIndicadores.CargarIndicadoresPorRangoCampana(campanaDesde, campanaHasta, codigoUsuario, idProceso,
                                                              codigoRolUsuario, prefijoIsoPais);
        }

        public DataSet CargarindicadoresporperiodoVariablesAdicionales(string ddlperiodo, string codigoUsuario,
                                                                       int intIdProceso, int codigoRolUsuario,
                                                                       string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoVariablesAdicionales(ddlperiodo, codigoUsuario, intIdProceso,
                                                                             codigoRolUsuario, prefijoIsoPais,
                                                                             connstring);
        }

        public DataSet CargarindicadoresporperiodoVariablesAdicionalesPreDialogo(string ddlperiodo, string codigoUsuario,
                                                                       int intIdProceso, int codigoRolUsuario,
                                                                       string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoVariablesAdicionalesPreDialogo(ddlperiodo, codigoUsuario, intIdProceso,
                                                                             codigoRolUsuario, prefijoIsoPais,
                                                                             connstring);
        }


        public DataSet CargarindicadoresporperiodoVariablesAdicionalesNuevas(string ddlperiodo, string codigoUsuario,
                                                                       int intIdProceso, int codigoRolUsuario,
                                                                       string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoVariablesAdicionalesNuevas(ddlperiodo, codigoUsuario, intIdProceso,
                                                                             codigoRolUsuario, prefijoIsoPais,
                                                                             connstring);
        }

        public DataSet CargarindicadoresporperiodoVariablesAdicionalesEvaluado(string ddlperiodo, string codigoUsuario,
                                                                               int intIdProceso, int codigoRolUsuario,
                                                                               string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporperiodoVariablesAdicionalesEvaluado(ddlperiodo, codigoUsuario,
                                                                                     intIdProceso, codigoRolUsuario,
                                                                                     prefijoIsoPais, connstring);
        }

        public DataSet CargarindicadoresporcampanaVariablesAdicionales2(string anioCampana, string codigoUsuario,
                                                                        int intIdProceso, int codigoRolUsuario,
                                                                        string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporcampanaVariablesAdicionales2(anioCampana, codigoUsuario, intIdProceso,
                                                                              codigoRolUsuario, prefijoIsoPais,
                                                                              connstring);
        }


        public DataSet CargarindicadoresporcampanaVariablesAdicionales2Nuevas(string anioCampana, string codigoUsuario,
                                                                        int intIdProceso, int codigoRolUsuario,
                                                                        string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporcampanaVariablesAdicionales2Nuevas(anioCampana, codigoUsuario, intIdProceso,
                                                                              codigoRolUsuario, prefijoIsoPais,
                                                                              connstring);
        }

        public DataSet CargarindicadoresporcampanaVariablesAdicionales2Evaluado(string anioCampana, string codigoUsuario,
                                                                                int intIdProceso, int codigoRolUsuario,
                                                                                string prefijoIsoPais, string connstring)
        {
            return DaIndicadores.CargarindicadoresporcampanaVariablesAdicionales2Evaluado(anioCampana, codigoUsuario,
                                                                                      intIdProceso, codigoRolUsuario,
                                                                                      prefijoIsoPais, connstring);
        }

        public DataSet CargarindicadoresporcampanaVariablesAdicionales(string ddlCampanadesde, string ddlCampanahasta,
                                                                       string codigoUsuario, int intIdProceso,
                                                                       int codigoRolUsuario, string prefijoIsoPais,
                                                                       string connstring)
        {
            return DaIndicadores.CargarindicadoresporcampanaVariablesAdicionales(ddlCampanadesde, ddlCampanahasta,
                                                                             codigoUsuario, intIdProceso,
                                                                             codigoRolUsuario, prefijoIsoPais,
                                                                             connstring);
        }

        public DataTable SeleccionarCampana(string periodoEvaluacion, int codigoRolUsuario, string prefijoIsoPais,
                                            string connstring)
        {
            return DaIndicadores.SeleccionarCampana(periodoEvaluacion, codigoRolUsuario, prefijoIsoPais, connstring);
        }

        public bool InsertarIndicadores(string codVariable1, string codVariable2, int intIdProceso, string anioCampana,
                                        int numeroIngresado, string connstring)
        {
            return DaIndicadores.InsertarIndicadores(codVariable1, codVariable2, intIdProceso, anioCampana, numeroIngresado,
                                                 connstring);
        }

        public bool InsertarIndicadoresEvaluado(string codVariable1, string codVariable2, int intIdProceso,
                                                string anioCampana, int numeroIngresado, string connstring)
        {
            return DaIndicadores.InsertarIndicadoresEvaluado(codVariable1, codVariable2, intIdProceso, anioCampana,
                                                         numeroIngresado, connstring);
        }

        public DataTable ObtenerIndicadoresProcesados(int idProceso)
        {
            return DaIndicadores.ObtenerIndicadoresProcesados(idProceso);
        }

        public DataTable CargarCombosVariablesCausa(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                    int codigoRolUsuario, string prefijoIsoPais,
                                                    string anoCampanaCerrado, string variable1, string variable2,
                                                    string connstring)
        {
            if (string.IsNullOrEmpty(ddlperiodo) || string.IsNullOrEmpty(codigoUsuario) || string.IsNullOrEmpty(prefijoIsoPais) || string.IsNullOrEmpty(connstring))
                return null;

            return DaIndicadores.CargarCombosVariablesCausa(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                        prefijoIsoPais, anoCampanaCerrado, variable1, variable2,
                                                        connstring);
        }


        public DataTable CargarCombosVariablesCausaNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                    int codigoRolUsuario, string prefijoIsoPais,
                                                    string anoCampanaCerrado, string variable1, string variable2,
                                                    string connstring)
        {
            if (string.IsNullOrEmpty(ddlperiodo) || string.IsNullOrEmpty(codigoUsuario) || string.IsNullOrEmpty(prefijoIsoPais) || string.IsNullOrEmpty(connstring))
                return null;

            return DaIndicadores.CargarCombosVariablesCausaNuevas(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                        prefijoIsoPais, anoCampanaCerrado, variable1, variable2,
                                                        connstring);
        }

        public DataTable CargarCombosVariablesCausaEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                            int codigoRolUsuario, string prefijoIsoPais,
                                                            string anoCampanaCerrado, string variable1, string variable2,
                                                            string connstring)
        {
            return DaIndicadores.CargarCombosVariablesCausaEvaluado(ddlperiodo, codigoUsuario, intIdProceso,
                                                                codigoRolUsuario, prefijoIsoPais, anoCampanaCerrado,
                                                                variable1, variable2, connstring);
        }

        public DataTable CargarDatosVariableCausa(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                  int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado,
                                                  string variableCausa, string connstring)
        {
            return DaIndicadores.CargarDatosVariableCausa(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                      prefijoIsoPais, anoCampanaCerrado, variableCausa, connstring);
        }

        public DataTable CargarDatosVariableCausaNuevas(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                  int codigoRolUsuario, string prefijoIsoPais, string anoCampanaCerrado,
                                                  string variableCausa, string connstring)
        {
            return DaIndicadores.CargarDatosVariableCausaNuevas(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                      prefijoIsoPais, anoCampanaCerrado, variableCausa, connstring);
        }

        public DataTable CargarDatosVariableCausaEvaluado(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                          int codigoRolUsuario, string prefijoIsoPais,
                                                          string anoCampanaCerrado, string variableCausa,
                                                          string connstring)
        {
            return DaIndicadores.CargarDatosVariableCausaEvaluado(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                              prefijoIsoPais, anoCampanaCerrado, variableCausa,
                                                              connstring);
        }

        public DataTable CargarDatosVariableCausaEvaluadoPreDialogo(string ddlperiodo, string codigoUsuario, int intIdProceso,
                                                          int codigoRolUsuario, string prefijoIsoPais,
                                                          string anoCampanaCerrado, string variableCausa,
                                                          string connstring)
        {
            return DaIndicadores.CargarDatosVariableCausaEvaluadoPreDialogo(ddlperiodo, codigoUsuario, intIdProceso, codigoRolUsuario,
                                                              prefijoIsoPais, anoCampanaCerrado, variableCausa,
                                                              connstring);
        }

        public bool InsertarVariablesCausa(int idProceso, string variable1, string variable2, string variableCausa1,
                                           string variableCausa2, string variableCausa3, string variableCausa4,
                                           string objetivo1, string real1, string diferencia1, string objetivo2,
                                           string real2, string diferencia2, string objetivo3, string real3,
                                           string diferencia3, string objetivo4, string real4, string diferencia4,
                                           string connstring)
        {
            return DaIndicadores.InsertarVariablesCausa(idProceso, variable1, variable2, variableCausa1, variableCausa2,
                                                    variableCausa3, variableCausa4, objetivo1, real1, diferencia1,
                                                    objetivo2, real2, diferencia2, objetivo3, real3, diferencia3,
                                                    objetivo4, real4, diferencia4, connstring);
        }

        public bool InsertarVariablesCausaEvaluado(int idProceso, string variable1, string variable2,
                                                   string variableCausa1, string variableCausa2, string variableCausa3,
                                                   string variableCausa4, string objetivo1, string real1,
                                                   string diferencia1, string objetivo2, string real2,
                                                   string diferencia2, string objetivo3, string real3,
                                                   string diferencia3, string objetivo4, string real4,
                                                   string diferencia4, string connstring)
        {
            return DaIndicadores.InsertarVariablesCausaEvaluado(idProceso, variable1, variable2, variableCausa1,
                                                            variableCausa2, variableCausa3, variableCausa4, objetivo1,
                                                            real1, diferencia1, objetivo2, real2, diferencia2, objetivo3,
                                                            real3, diferencia3, objetivo4, real4, diferencia4,
                                                            connstring);
        }

        public DataTable ObtenerVariablesCausa(int idProceso, string codVariablePadre)
        {
            return DaIndicadores.ObtenerVariablesCausa(idProceso, codVariablePadre);
        }


        public DataTable ObtenerVariablesCausaPreDialogo(int idProceso, string codVariablePadre)
        {
            return DaIndicadores.ObtenerVariablesCausaPreDialogo(idProceso, codVariablePadre);
        }

        public DataTable ObtenerVariablesCausaEvaluado(int idProceso, string codVariablePadre)
        {
            return DaIndicadores.ObtenerVariablesCausaEvaluado(idProceso, codVariablePadre);
        }

        public DataTable ValidarPeriodoEvaluacion(string periodoEvaluacion, string prefijoIsoPais, int codigoRolUsuario,
                                                  string connstring)
        {
            return DaIndicadores.ValidarPeriodoEvaluacion(periodoEvaluacion, prefijoIsoPais, codigoRolUsuario, connstring);
        }

        public DataTable CargaDatosVariableEnfoque(string variable, int intIdProceso, string connstring)
        {
            return DaIndicadores.CargaDatosVariableEnfoque(variable, intIdProceso, connstring);
        }

        public DataTable ObtenerDescripcionVariableEnfoque(string variable, string connstring)
        {
            return DaIndicadores.ObtenerDescripcionVariableEnfoque(variable, connstring);
        }

        public DataTable ObtenerVariablesCausaPorProceso(int idProceso)
        {
            return DaIndicadores.ObtenerVariablesCausaPorProceso(idProceso);
        }

        public DataTable ObtenerVariablesCausaPorProcesoEvaluado(int idProceso)
        {
            return DaIndicadores.ObtenerVariablesCausaPorProcesoEvaluado(idProceso);
        }

        public DataTable ObtenerResumen(string periodo, string codigoUsuario, int idProceso, int codigoRolUsuario,
                                        string prefijoPais)
        {
            if (string.IsNullOrEmpty(periodo) || string.IsNullOrEmpty(codigoUsuario) || string.IsNullOrEmpty(prefijoPais))
                return null;

            return DaIndicadores.ObtenerResumen(periodo, codigoUsuario, idProceso, codigoRolUsuario, prefijoPais);
        }

        public DataTable ObtenerVariablesCausaVisita(int idProceso, string codVariablePadre, string documentoIdentidad)
        {
            return DaIndicadores.ObtenerVariablesCausaVisita(idProceso, codVariablePadre, documentoIdentidad);
        }

        public DataTable ObtenerVariablesCausaVisitaGz(int idProceso, string codVariablePadre, string documentoIdentidad)
        {
            return DaIndicadores.ObtenerVariablesCausaVisitaGz(idProceso, codVariablePadre, documentoIdentidad);
        }

        public DataTable ObtenerVariablesCausaVisita(int idProceso, string codVariablePadre)
        {
            return DaIndicadores.ObtenerVariablesCausaVisita(idProceso, codVariablePadre);
        }

        public void InsertarVariableCausaVista(BeVariableCausa variableCausa)
        {
            DaIndicadores.InsertarVariableCausaVista(variableCausa);
        }

        public void EliminarVariableCausaVista(int idProceso)
        {
            DaIndicadores.EliminarVariableCausaVista(idProceso);
        }

        public List<BeComun> CargarGerentesRegion(string prefijoIsoPais, string periodo)
        {
            return DaIndicadores.CargarGerentesRegion(prefijoIsoPais, periodo);
        }

        public List<BeComun> CargarGerentesZona(string prefijoIsoPais, string codigoUsuario, string periodo)
        {
            return DaIndicadores.CargarGerentesZona(prefijoIsoPais, codigoUsuario, periodo);
        }

        public bool ActualizaModeloDialogo(int idProceso, string modeloDialogo)
        {
            return DaIndicadores.ActualizaModeloDialogo(idProceso, modeloDialogo);
        }

    }
}