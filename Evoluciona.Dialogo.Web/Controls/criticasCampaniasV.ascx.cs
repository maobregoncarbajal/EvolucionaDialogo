
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Text;
    using System.Web.UI;

    public partial class criticasCampaniasV : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MuestraCampaniasCriticas();
            //if (!Page.IsPostBack)
            //{
            //    MuestraCampaniasCriticas();
            //}
        }
        private void MuestraCampaniasCriticas()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            BeResumenVisita objResumenBE = (BeResumenVisita)Session[Constantes.ObjUsuarioVisitado];

            string connstring = string.Empty;
            if (Session["connApp"].ToString() == "")
                Session["connApp"] = ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            connstring = Session["connApp"].ToString();//ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ToString();
            BlCritica objCriticaBL = new BlCritica();
            DataTable dtCriticas = new DataTable();
            //string periodoCerrado = RetornarPeriodoCerrador(objResumenBE.periodo);
            string periodoCerrado = objResumenBE.periodo;

            if (Session["dtCargarCriticas"] == null)
            {
                if (objResumenBE.codigoRolUsuario == Constantes.RolGerenteRegion)
                {
                    dtCriticas = objCriticaBL.CargarCampaniasCriticas_GR(objResumenBE.codigoUsuario, periodoCerrado, objResumenBE.prefijoIsoPais, connstring);
                }
                else if (objResumenBE.codigoRolUsuario == Constantes.RolGerenteZona)
                {
                    dtCriticas = objCriticaBL.CargarCampaniasCriticas_GZ(objResumenBE.codigoUsuario, periodoCerrado, objResumenBE.prefijoIsoPais, objResumenBE.codigoUsuarioEvaluador, connstring);
                }
                Session["dtCargarCriticas"] = dtCriticas;
            }
            dtCriticas = (DataTable)Session["dtCargarCriticas"];

            if (dtCriticas.Rows.Count > 0)
            {
                List<BeCriticas> listCriticas = new List<BeCriticas>();
                for (int x = 0; x < dtCriticas.Rows.Count; x++)
                {
                    BeCriticas objCriticasBE = new BeCriticas();
                    objCriticasBE.campania = dtCriticas.Rows[x]["chrAnioCampana"].ToString();
                    objCriticasBE.estadoCriticidad = dtCriticas.Rows[x]["vchEstadoCamp"].ToString();
                    listCriticas.Add(objCriticasBE);
                }
                LlenarCampanias(periodoCerrado, listCriticas, objResumenBE.nombreEvaluado);
            }

            //objResumenBE.rolUsuario = idRol;


        }
        private void LlenarCampanias(string periodo, List<BeCriticas> listCriticas, string nombreEvaluado)
        {
            string anioPeriodo = periodo.Substring(0, 4);
            ArrayList strCampanias = new ArrayList();
            if (listCriticas.Count > 0)
            {
                periodo = periodo.Substring(5, periodo.Length - 5);
                periodo = periodo.Trim();
                switch (periodo)
                {
                    case "I":
                        for (int y = 1; y <= 6; y++)
                        {
                            strCampanias.Add(anioPeriodo + "0" + y.ToString());
                        }
                        break;
                    case "II":
                        for (int y = 7; y <= 12; y++)
                        {
                            if (y < 10)
                            {
                                strCampanias.Add(anioPeriodo + "0" + y.ToString());
                            }
                            else
                            {
                                strCampanias.Add(anioPeriodo + y.ToString());
                            }

                        }
                        break;
                    case "III":
                        for (int y = 13; y <= 18; y++)
                        {
                            strCampanias.Add(anioPeriodo + y.ToString());
                        }
                        break;
                }
                StringBuilder tablaCampanias = new StringBuilder();
                tablaCampanias.Append("<table id='tablaCriticas' border='0' cellpadding='0' width='170px'>");
                tablaCampanias.Append("<tr><td align='center' colspan='" + strCampanias.Count + "' class='tituloSeguimiento'>Seguimiento del Período</td></tr><tr>");
                for (int z = 0; z < strCampanias.Count; z++)
                {
                    string campania = strCampanias[z].ToString();
                    string numeroCampania = "C" + campania.Substring(4, 2);
                    string urlImagen = "";
                    tablaCampanias.Append("<td class='texto_campania'>");
                    BeCriticas objCriticasBE = new BeCriticas();
                    objCriticasBE = listCriticas.Find(delegate(BeCriticas objCriticasAux) { return objCriticasAux.campania == campania; });

                    if (objCriticasBE != null && objCriticasBE.campania != null)
                    {
                        urlImagen = RetornarImagen(objCriticasBE.estadoCriticidad);
                        tablaCampanias.Append("<img src='" + Utils.AbsoluteWebRoot + "Images/" + urlImagen + "' alt='' width='16px' height='16px' /><br/>" + numeroCampania);
                    }
                    else
                    {
                        tablaCampanias.Append("<img src='" + Utils.AbsoluteWebRoot + "Images/gris.jpg' alt=''  width='16px' height='16px'/><br/>" + numeroCampania);
                    }

                    tablaCampanias.Append("</td>");
                }
                tablaCampanias.Append("</tr></table>");

                LiteralControl ltTabla = new LiteralControl();
                ltTabla.Text = tablaCampanias.ToString();
                divMuestraCampaniasCriticas.Controls.Add(ltTabla);
            }
        }

        private string RetornarImagen(string estado)
        {
            string nombreImagen = "";
            switch (estado.ToUpper().Trim())
            {
                case "CRITICA":
                    nombreImagen = "rojo.jpg";
                    break;
                case "ESTABLE":
                    nombreImagen = "amarillo.jpg";
                    break;
                case "PRODUCTIVA":
                    nombreImagen = "verde_oscuro.jpg";
                    break;
                case "NUEVA":
                    nombreImagen = "verde_claro.jpg";
                    break;
                default: nombreImagen = "gris.jpg";
                    break;

            }
            return nombreImagen;
        }

        private string RetornarPeriodoCerrador(string periodoCerrado)
        {
            string anioPeriodo = periodoCerrado.Substring(0, 4);
            periodoCerrado = periodoCerrado.Substring(5, periodoCerrado.Length - 5);
            periodoCerrado = periodoCerrado.Trim();
            int anioCerrado = Convert.ToInt32(anioPeriodo);
            switch (periodoCerrado)
            {
                case "I":
                    anioCerrado = anioCerrado - 1;
                    periodoCerrado = " III";
                    break;
                case "II":
                    periodoCerrado = " I";
                    break;
                case "III":
                    periodoCerrado = " II";
                    break;
            }
            periodoCerrado = anioCerrado.ToString() + periodoCerrado;
            return periodoCerrado;
        }
    }
}
