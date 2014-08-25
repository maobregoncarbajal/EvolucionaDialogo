
namespace Evoluciona.Dialogo.Helpers
{
    public static class Constantes
    {
        public const string ObjUsuarioLogeado = "usuarioLogeado";
        public const string ObjUsuarioProcesado = "usuarioProcesado";
        public const byte EstadoActivo = 1;
        public const byte EstadoInactivo = 0;
        public const string EstadoProcesoEnviado = "0";
        public const string EstadoProcesoActivo = "1";
        public const string EstadoProcesoRevision = "2";
        public const string EstadoProcesoCulminado = "3";
        public const int RolDirectorVentas = 4;
        public const int RolGerenteRegion = 5;
        public const int RolGerenteZona = 6;
        public const int RolGerenteEvoluciona = 7;
        public const int RolAdministradores = 8;
        public const int RolLideres = 7;
        public const string IndicadorEvaluadoDvGr = "1";
        public const string IndicadorEvaluadoGrGz = "2";
        public const string IndicadorEvaluadoGzLt = "3";
        public const byte EmailEnviado = 1;
        public const byte EmailNoEnviado = 0;
        public const string MsjUsuarioNoProcesado = "Debe seleccionar un usuario para iniciar el pre-dialogo";

        //visitas
        public const string ObjUsuarioVisitado = "usuarioVisitado";
        public const string EstadoVisitaActivo = "1";
        public const string EstadoVisitaPostDialogo = "2";
        public const string EstadoVisitaCerrado = "3";
        public const string VisitaModoLectura = "visitaModoLectura";
        public const string VisitaPeriodos = "visitaPeriodos";

        public const string ListaPaisesSinLets = "CO,PE";

        //Administración
        public const string RolAdminCoorporativo = "C";
        public const string RolAdminPais = "P";
        public const string RolAdminEvaluciona = "E";

        //Eventos
        public const int ReunionIndividual = 1;
        public const int ReunionGrupal = 2;
        public const int ReunionOtros = 3;

        public const string CodigoAdamPaisColombia = "02";

        //Toma de Accion
        public const string MatrizConstPlanMejora = "01";
        public const string MatrizConstReasignacion = "02";
        public const string MatrizConstRotacionSaludable = "03";

        //Matriz Zona 

        public const string AccionUnoFuenteVentas = "1"; // Acción de Grabar
        public const string AccionDosFuenteVentas = "2";
        public const string AccionTresFuenteVentas = "3";

        public const string Pequenho = "Pequenho";
        public const string Mediano = "Mediano";
        public const string Grande = "Grande";

        public const string Bajo = "Bajo";
        public const string Medio = "Medio";
        public const string Alto = "Alto";

        public const double Benchmark = 0.75;


        public const string VentaMn = "VtaNet";
        public const string ActivasFinales = "ActFin";

        public const string Competencias = "Competencias";


        //Dialogo Nuevas

        public const string Nueva = "NUEVA";

        //Dialogo Plan de Mejora

        public const string Normal = "NORMAL";
        public const string PlanDeMejora = "PLANDEMEJORA";


        //Logeo  Externo


        //public const string StringKey = "password";
        //public const string StringIv = "46428208";

        public const string StringKey = "46428208";
        public const string StringIv = "password";
        public const string Token = "t0k3n";

        //Reporte Status

        public const int IdRolDirectorVentas = 1;
        public const int IdRolGerenteRegion = 2;
        public const int IdRolGerenteZona = 3;

        //Reporte Ranking

        public const string CodDirectorVentas = "DV";
        public const string CodGerenteRegion = "GR";
        public const string CodGerenteZona = "GZ";


        public const bool Activo = true;
        public const bool Inactivo = false;
        public const string CeroCero= "00";

        public const string StrActivo = "1";
        public const string StrInactivo = "0";
        
    }
}