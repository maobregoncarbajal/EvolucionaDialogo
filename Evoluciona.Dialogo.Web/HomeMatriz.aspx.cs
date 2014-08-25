
using System.Globalization;

namespace Evoluciona.Dialogo.Web
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class HomeMatriz : Page
    {
        #region Variables

        BeUsuario _objUsuario = new BeUsuario();
        readonly BlHomeMatriz _objBlHomeMatriz = new BlHomeMatriz();
        private string _periodoAnterior;
        private const string Incremento = "de Incremento";
        private const string Decremento = "de Decremento";

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarObjUsuario();
            }

            if (_objUsuario == null) return;
            if (_objUsuario.codigoRol.Equals(Constantes.RolDirectorVentas))
            {
                CargarInformacionDv();
            }
            else if (_objUsuario.codigoRol.Equals(Constantes.RolGerenteRegion))
            {
                CargarInformacionGr();
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarObjUsuario()
        {
            _objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            _periodoAnterior = PeriodoAnterior(_objUsuario.periodoEvaluacion);
        }

        private static string PeriodoAnterior(string periodoActual)
        {
            var periodoAnterior = "";
            string anio;
            string periodo;

            switch (periodoActual.Trim().Length)
            {
                case 8:
                    anio = periodoActual.Substring(0, 4);
                    periodo = periodoActual.Substring(5, 2);
                    periodoAnterior = anio + " " + periodo;
                    break;
                case 7:
                    anio = periodoActual.Substring(0, 4);
                    periodo = periodoActual.Substring(5, 1);
                    periodoAnterior = anio + " " + periodo;
                    break;
                case 6:
                    anio = (int.Parse(periodoActual.Substring(0, 4)) - 1).ToString(CultureInfo.InvariantCulture);
                    periodo = "III";
                    periodoAnterior = anio + " " + periodo;
                    break;
            }

            return periodoAnterior;
        }

        private void CargarInformacionDv()
        {
            MostrarFechaRegistrarAcuerdos();
            MostrarGryGZplanMejoraDv();
            MostrarGryGZplanReasignacionDv();
            MostrarPorcentGryGZrecuperacionDv();
            mostrarPorcntIncrGRyGZ_EstaProdDV();
        }


        private void MostrarFechaRegistrarAcuerdos()
        {
            var objBlCronoMatriz = new BlCronogramaPdM();
            DateTime? fechaRegistrarAcuerdos;

            var objBeCronoMatriz = objBlCronoMatriz.BuscarCronogramaPdM(_objUsuario);

                if (!string.IsNullOrEmpty(objBeCronoMatriz.FechaProrroga.ToString()))
                {
                    fechaRegistrarAcuerdos = objBeCronoMatriz.FechaProrroga;
                    lblFechaRegistrarAcuerdos.Text = (DateTime.Parse(fechaRegistrarAcuerdos.ToString())).ToString("dd/MM/yyyy");
                }
                else
                {
                    if (!string.IsNullOrEmpty(objBeCronoMatriz.FechaLimite.ToString()))
                    {
                        fechaRegistrarAcuerdos = objBeCronoMatriz.FechaLimite;
                        lblFechaRegistrarAcuerdos.Text = (DateTime.Parse(fechaRegistrarAcuerdos.ToString())).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lblTh.Text = "La fecha para registrar acuerdos no esta definida.";
                        lblFechaRegistrarAcuerdos.Text = "";
                        lblRa.Text = "";
                    }
                }               
        }


        private void MostrarGryGZplanMejoraDv()
        {
            var cantGRplanMejoraDv = _objBlHomeMatriz.ObtenerGRplanMejoraDv(_objUsuario);

            var cantGZplanMejoraDv = _objBlHomeMatriz.ObtenerGZplanMejoraDv(_objUsuario);

            lblCantEvaluadosPlanMejora.Text = cantGRplanMejoraDv + " Gerentes Regionales / " + cantGZplanMejoraDv + " Gerentes de Zona";
        }


        private void MostrarGryGZplanReasignacionDv()
        {
            var cantGRplanReasignacionDv = _objBlHomeMatriz.ObtenerGRplanReasignacionDv(_objUsuario);

            var cantGZplanReasignacionDv = _objBlHomeMatriz.ObtenerGZplanReasignacionDv(_objUsuario);

            lblCantEvaluadosPlanReasignacion.Text = cantGRplanReasignacionDv + " Gerentes Regionales / " + cantGZplanReasignacionDv + " Gerentes de Zona";
        }


        private void MostrarPorcentGryGZrecuperacionDv()
        {
            var porcentGRrecuperacionDv = (Math.Truncate(_objBlHomeMatriz.ObtenerPorcentGRrecuperacionDv(_objUsuario))).ToString(CultureInfo.InvariantCulture);

            var porcentGZrecuperacionDv = (Math.Truncate(_objBlHomeMatriz.ObtenerPorcentGZrecuperacionDv(_objUsuario))).ToString(CultureInfo.InvariantCulture);

            lblporcentRecuperadosConPlanMejora.Text = porcentGRrecuperacionDv + "% Gerentes Regionales / " + porcentGZrecuperacionDv + "% Gerentes de Zona";
        }

        private void mostrarPorcntIncrGRyGZ_EstaProdDV()
        {
            var medidaGr = "";
            var medidaGz = "";

            var porcntIncrGrEstaProdDv = Int32.Parse((Math.Truncate(_objBlHomeMatriz.ObtenerPorcntIncrGR_EstaProdDV(_objUsuario, _periodoAnterior))).ToString(CultureInfo.InvariantCulture));

            if (porcntIncrGrEstaProdDv > 0)
            {
                medidaGr = Incremento;
            }
            else if (porcntIncrGrEstaProdDv < 0)
            {
                medidaGr = Decremento;
                porcntIncrGrEstaProdDv = Math.Abs(porcntIncrGrEstaProdDv);
            }


            var porcntIncrGzEstaProdDv = Int32.Parse((Math.Truncate(_objBlHomeMatriz.ObtenerPorcntIncrGZ_EstaProdDV(_objUsuario, _periodoAnterior))).ToString(CultureInfo.InvariantCulture));

            if (porcntIncrGzEstaProdDv > 0)
            {
                medidaGz = Incremento;
            }
            else if (porcntIncrGzEstaProdDv < 0)
            {
                medidaGz = Decremento;
                porcntIncrGzEstaProdDv = Math.Abs(porcntIncrGzEstaProdDv);
            }


            lblporcentIncrementoEstablesyProductivas.Text = porcntIncrGrEstaProdDv + "% " + medidaGr + " Gerentes Regionales / " + porcntIncrGzEstaProdDv + "% " + medidaGz + " Gerentes de Zona";

        }

        private void CargarInformacionGr()
        {
            MostrarFechaRegistrarAcuerdos();
            MostrarGZplanMejoraGr();
            MostrarGZplanReasignacionGr();
            MostrarPorcentGZrecuperacionGr();
            mostrarPorcntIncrGZ_EstaProdGR();
        }


        private void MostrarGZplanMejoraGr()
        {
            var cantGZplanMejoraGr = _objBlHomeMatriz.ObtenerGZplanMejoraGr(_objUsuario);

            lblCantEvaluadosPlanMejora.Text = cantGZplanMejoraGr + " Gerentes Zona";
        }

        private void MostrarGZplanReasignacionGr()
        {
            var cantGZplanReasignacionGr = _objBlHomeMatriz.ObtenerGZplanReasignacionGr(_objUsuario);

            lblCantEvaluadosPlanReasignacion.Text = cantGZplanReasignacionGr + " Gerentes Zona";
        }

        private void MostrarPorcentGZrecuperacionGr()
        {
            var porcentGZrecuperacionGr = (Math.Truncate(_objBlHomeMatriz.ObtenerPorcentGZrecuperacionGr(_objUsuario))).ToString(CultureInfo.InvariantCulture);

            lblporcentRecuperadosConPlanMejora.Text = porcentGZrecuperacionGr + "% Gerentes Zona";
        }

        private void mostrarPorcntIncrGZ_EstaProdGR()
        {
            var medidaGz = String.Empty;

            var porcntIncrGzEstaProdGr = Int32.Parse((Math.Truncate(_objBlHomeMatriz.ObtenerPorcntIncrGZ_EstaProdGR(_objUsuario, _periodoAnterior))).ToString(CultureInfo.InvariantCulture));

            if (porcntIncrGzEstaProdGr > 0)
            {
                medidaGz = Incremento;
            }
            else if (porcntIncrGzEstaProdGr < 0)
            {
                medidaGz = Decremento;
                porcntIncrGzEstaProdGr = Math.Abs(porcntIncrGzEstaProdGr);
            }

            lblporcentIncrementoEstablesyProductivas.Text = porcntIncrGzEstaProdGr + "% " + medidaGz + " Gerentes Zona";
        }

        #endregion Metodos
    }
}
