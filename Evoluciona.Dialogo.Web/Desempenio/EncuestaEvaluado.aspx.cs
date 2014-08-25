
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class EncuestaEvaluado : Page
    {
        #region Variables

        BeResumenProceso objResumen;
        TipoEncuesta tipo;
        BeProceso objProceso;

        public int Correcto = 0;

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            objResumen = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            int tipoEncuesta = Utils.QueryStringInt("tipoEncuesta");
            tipo = (TipoEncuesta)Enum.Parse(typeof(TipoEncuesta), tipoEncuesta.ToString());
            int idProceso = (Utils.QueryStringInt("idProceso") == 0) ? objResumen.idProceso : Utils.QueryStringInt("idProceso");
            BlProceso procesobl = new BlProceso();
            objProceso = procesobl.ObtenerProceso(idProceso);

            if (IsPostBack) return;

            rpEncuesta.DataSource = (new BlEncuesta()).ObtenerPreguntasEncuesta(objProceso.IdProceso, tipo);
            rpEncuesta.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int indexActual = 1;

            BlEncuesta encuestaBL = new BlEncuesta();
            List<BeEncuesta> respuestasEncuesta = new List<BeEncuesta>();

            foreach (RepeaterItem item in rpEncuesta.Items)
            {
                BeEncuesta nuevaEncuesta = new BeEncuesta();

                nuevaEncuesta.IdPregunta = indexActual++;
                nuevaEncuesta.IdProceso = objProceso.IdProceso;
                nuevaEncuesta.CodigoUsuario = objProceso.CodigoUsuario;
                nuevaEncuesta.Respuesta = ObtenerIndex(item);

                respuestasEncuesta.Add(nuevaEncuesta);
            }

            bool _esCorrecto = encuestaBL.RegistrarEncuesta(respuestasEncuesta, tipo);
            this.Correcto = _esCorrecto ? 1 : 0;
        }

        #endregion Eventos

        #region Metodos

        private int ObtenerIndex(RepeaterItem item)
        {
            int indexActual = 1;
            foreach (Control control in item.Controls)
            {
                if (!(control is CheckBox)) continue;

                CheckBox chk = (CheckBox)control;
                if (chk.Checked) return indexActual;

                indexActual++;
            }

            return indexActual;
        }

        #endregion Metodos
    }
}