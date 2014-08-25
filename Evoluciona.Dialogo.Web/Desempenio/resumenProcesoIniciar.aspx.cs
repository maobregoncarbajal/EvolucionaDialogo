
namespace Evoluciona.Dialogo.Web.Desempenio
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Data;
    using System.Web.UI;

    public partial class resumenProcesoIniciar : Page
    {
        #region Variables

        protected BeUsuario objUsuario;
        public string tipoDialogoDesempenio;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IniciarProceso();
            }
        }

        #endregion

        #region Metodos

        private void IniciarProceso()
        {
            objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            string docuIdentidad = string.Empty;
            string nombreEvaluado = string.Empty;

            tipoDialogoDesempenio = Session["tipoDialogoDesempenio"].ToString();


            if (Request["verResumen"] != null)
            {
                Session["codigoRolEvaluado"] = objUsuario.codigoRol;
                docuIdentidad = objUsuario.codigoUsuario;
                nombreEvaluado = objUsuario.nombreUsuario;
            }
            else
            {
                docuIdentidad = Request["codDI"];
                nombreEvaluado = Request["Nombre"];
            }

            int idRol = 0;
            int readOnly = 0;
            int codigoRolEvaluado = 0;

            if (Request["readOnly"] != null)
                readOnly = int.Parse(Request["readOnly"]);

            if (!string.IsNullOrEmpty(Request["rolID"]))
            {
                idRol = Convert.ToInt32(Request["rolID"]);

                switch (idRol)
                {
                    case 2: //ID rol Gerente de Región
                        codigoRolEvaluado = Constantes.RolGerenteRegion;
                        break;
                    case 3: //ID rol Gerente de Zona
                        codigoRolEvaluado = Constantes.RolGerenteZona;
                        break;
                }
            }
            else
            {
                codigoRolEvaluado = Convert.ToInt32(Session["codigoRolEvaluado"]);

                BlUsuario objRol = new BlUsuario();
                DataTable dtRol = objRol.ObtenerDatosRol(codigoRolEvaluado, Constantes.EstadoActivo);

                if (dtRol.Rows.Count > 0)
                {
                    idRol = Convert.ToInt32(dtRol.Rows[0]["intIDRol"].ToString());
                }
            }

            BlResumenProceso objResumenBL = new BlResumenProceso();
            string codigoGRegion = string.Empty;
            string codigoGZona = string.Empty;
            bool sePuedeEvaluar = false;
            string email = string.Empty;
            string codigoPais = string.Empty;


            BeResumenProceso objDatosGR = new BeResumenProceso();
            BeResumenProceso objDatosGZ = new BeResumenProceso();

            switch (codigoRolEvaluado)
            {
                case Constantes.RolGerenteRegion:
                    objDatosGR = objResumenBL.ObtenerUsuarioGRegionEvaluado(docuIdentidad, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                    if (objDatosGR != null)
                    {
                        codigoGRegion = objDatosGR.codigoGRegion;
                        email = objDatosGR.email;
                        codigoPais = objDatosGR.prefijoIsoPais;
                        sePuedeEvaluar = true;
                    }
                    else
                    {
                        if (String.Equals(nombreEvaluado.Split('-')[0].ToUpper().Trim().Substring(3, 5), Constantes.Nueva))
                        {
                            objDatosGR = objResumenBL.ObtenerUsuarioNuevaGRegionEvaluado(docuIdentidad, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                            if (objDatosGR != null)
                            {
                                codigoGRegion = objDatosGR.codigoGRegion;
                                email = objDatosGR.email;
                                codigoPais = objDatosGR.prefijoIsoPais;
                                sePuedeEvaluar = true;
                            }
                        }
                    }
                    break;
                case Constantes.RolGerenteZona:
                    objDatosGZ = objResumenBL.ObtenerUsuarioGZonaEvaluado(objUsuario.idUsuario, objUsuario.codigoUsuario, docuIdentidad, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                    if (objDatosGZ != null)
                    {
                        codigoGZona = objDatosGZ.codigoGZona;
                        email = objDatosGZ.email;
                        sePuedeEvaluar = true;
                    }
                    else
                    {
                        if (String.Equals(nombreEvaluado.Split('-')[0].ToUpper().Trim().Substring(3, 5), Constantes.Nueva))
                        {
                            objDatosGZ = objResumenBL.ObtenerUsuarioNuevaGZonaEvaluado(objUsuario.idUsuario, objUsuario.codigoUsuario, docuIdentidad, objUsuario.prefijoIsoPais, objUsuario.periodoEvaluacion, Constantes.EstadoActivo);
                            if (objDatosGZ != null)
                            {
                                codigoGZona = objDatosGZ.codigoGZona;
                                email = objDatosGZ.email;
                                sePuedeEvaluar = true;
                            }
                        }
                    }
                    break;
            }

            codigoPais = string.IsNullOrEmpty(codigoPais) ? objUsuario.prefijoIsoPais : codigoPais;


            BeResumenProceso objResumen = new BeResumenProceso();

            //if(tipoDialogoDesempenio==constantes.planDeMejora)
            //{
            //    objResumen = objResumenBL.ObtenerResumenProcesoByUsuarioPlanDeMejora(docuIdentidad, idRol, objUsuario.periodoEvaluacion, codigoPais);

            //}else
            //{

            objResumen = objResumenBL.ObtenerResumenProcesoByUsuario(docuIdentidad, idRol, objUsuario.periodoEvaluacion, codigoPais, tipoDialogoDesempenio);
            //}

            if (!((objUsuario.codigoRol == Constantes.RolDirectorVentas && codigoRolEvaluado == Constantes.RolGerenteRegion) ||
                (objUsuario.codigoRol == Constantes.RolGerenteRegion && codigoRolEvaluado == Constantes.RolGerenteZona)))
            {
                readOnly = 1;
                if (objResumen == null)
                {
                    if (Request.UrlReferrer != null)
                        Response.Redirect(Request.UrlReferrer.ToString());
                    else
                        Response.Redirect("Buscador.aspx");
                }
            }

            if ((objResumen != null && tipoDialogoDesempenio == Constantes.Normal) ||
                (objResumen != null && tipoDialogoDesempenio == Constantes.PlanDeMejora && objResumen.estadoProceso != Constantes.EstadoProcesoEnviado)
                )
            {
                objResumen.periodo = objUsuario.periodoEvaluacion;
                objResumen.prefijoIsoPais = codigoPais;
                objResumen.codigoRolUsuario = codigoRolEvaluado;
                objResumen.rolUsuario = idRol;
                objResumen.codigoUsuarioEvaluador = objUsuario.codigoUsuario;
                objResumen.nombreEvaluado = nombreEvaluado;
                objResumen.email = email;


            }
            else
            {
                objResumen = new BeResumenProceso();

                objResumen.periodo = objUsuario.periodoEvaluacion;
                objResumen.codigoUsuario = docuIdentidad;
                objResumen.rolUsuario = idRol;
                objResumen.codigoUsuarioEvaluador = objUsuario.codigoUsuario;
                objResumen.fechaLimiteProceso = DateTime.Now;
                objResumen.prefijoIsoPais = codigoPais;
                objResumen.rolUsuarioEvaluador = objUsuario.idRol;
                objResumen.nombreEvaluado = nombreEvaluado;
                objResumen.estadoProceso = Constantes.EstadoProcesoActivo;


                if (!sePuedeEvaluar)
                {
                    Response.Redirect("resumenProceso.aspx?inicio=sinUsuario");
                }
                else
                {
                    bool insertarProceso = false;

                    if (Session["tipoDialogoDesempenio"].ToString() == Constantes.PlanDeMejora)
                    {
                        insertarProceso = objResumenBL.InsertarProcesoPlanDeMejora(objResumen);
                    }
                    else
                    {
                        insertarProceso = objResumenBL.InsertarProceso(objResumen);
                    }




                    if (insertarProceso)
                    {
                        //if (tipoDialogoDesempenio == constantes.planDeMejora)
                        //{
                        //    objResumen = objResumenBL.ObtenerResumenProcesoByUsuarioPlanDeMejora(docuIdentidad, idRol, objUsuario.periodoEvaluacion, objResumen.prefijoIsoPais); 
                        //}
                        //else
                        //{
                        objResumen = objResumenBL.ObtenerResumenProcesoByUsuario(docuIdentidad, idRol, objUsuario.periodoEvaluacion, objResumen.prefijoIsoPais, tipoDialogoDesempenio);
                        //}

                        objResumen.periodo = objUsuario.periodoEvaluacion;
                        objResumen.codigoRolUsuario = codigoRolEvaluado;
                        objResumen.rolUsuario = idRol;
                        objResumen.prefijoIsoPais = codigoPais;
                        objResumen.fechaLimiteProceso = DateTime.Now;
                        objResumen.nombreEvaluado = nombreEvaluado;
                        objResumen.email = email;

                        //EnviarCorreos(objResumen.email, objResumen.codigoRolUsuario, objResumen.periodo, objResumen.codigoUsuario, objResumen.prefijoIsoPais);
                    }
                    else
                    {
                        Response.Redirect("resumenProceso.aspx?inicio=no");
                    }
                }
            }

            switch (codigoRolEvaluado)
            {
                case Constantes.RolGerenteRegion:
                    objResumen.codigoGRegion = codigoGRegion;
                    break;
                case Constantes.RolGerenteZona:
                    objResumen.codigoGZona = codigoGZona;
                    break;
            }

            if (Session["soloLecturaGZ"] != null)
            {
                objResumen.estadoProceso = Constantes.EstadoProcesoEnviado;
            }
            Session[Constantes.ObjUsuarioProcesado] = objResumen;

            Session["NombreEvaluado"] = nombreEvaluado;

            string urlInicial = "AntesNegocio.aspx?readOnly=" + readOnly;
            //mostrar la pantalla de resumen
            if (Request["verResumen"] != null)
            {
                urlInicial = "../PantallasModales/ResumenAntes.aspx";
            }
            else
            {
                if (!string.IsNullOrEmpty(Request["cod"]))
                {
                    Session["link"] = "aprobar";
                }
                else
                {
                    Session["link"] = "";
                }
            }

            Response.Redirect(urlInicial);
        }

        #endregion
    }
}