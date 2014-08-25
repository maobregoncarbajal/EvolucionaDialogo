namespace Evoluciona.Dialogo.Web.Admin.Encuestas
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    public partial class Crear : Page
    {
        #region Variables

        private readonly BlEncuestaDialogo _blEncuestaDialogo = new BlEncuestaDialogo();

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPreguntas();
            LoadTipoEncuesta();
            LoadTipoRespuesta();
            LoadPeriodo();
        }

        private void LoadPreguntas()
        {
            try
            {
                var oListPreguntas = _blEncuestaDialogo.ListarPreguntas();
                hfDesPreguntas.Value = FormatComboJqGrid(oListPreguntas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadTipoEncuesta()
        {
            try
            {
                var oListTipoEncuesta = _blEncuestaDialogo.ListarTipoEncuesta();
                hfDesTipoEncuesta.Value = FormatComboJqGrid(oListTipoEncuesta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadTipoRespuesta()
        {
            try
            {
                var oListTipoRespuesta = _blEncuestaDialogo.ListarTipoRespuesta();
                hfDesTipoRespuesta.Value = FormatComboJqGrid(oListTipoRespuesta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadPeriodo()
        {
            try
            {
                var blPeriodos = new BlPeriodos();

                var oListPeriodo = blPeriodos.ObtenerListaDePeriodos(Constantes.CeroCero);

                hfPeriodo.Value = FormatComboJqGrid(oListPeriodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string FormatComboJqGrid(IReadOnlyCollection<BeComun> oList)
        {
            var cadena = string.Empty;
            const string puntoComa = ";";
            var cont = 0;

            var countList = oList.Count;

            foreach (var item in oList)
            {
                cont++;
                var cod = item.Codigo;
                var des = cod.Split('|');

                cadena = cadena + cod.Replace(":", "") + ":" + des[0].Replace(":", "");

                if (cont != countList)
                {
                    cadena = cadena + puntoComa;
                }
            }

            return cadena;
        }
    }
}
