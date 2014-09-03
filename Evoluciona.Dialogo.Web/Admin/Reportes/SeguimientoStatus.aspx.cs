
namespace Evoluciona.Dialogo.Web.Admin.Reportes
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.Matriz.Helpers;
    using Web.Matriz.Helpers.Excel;

    //Admin
    public partial class SeguimientoStatus : Page
    {
        #region Variables

        protected BeAdmin ObjAdmin;
        public string PeriodoBuscado;
        public string PeriodoBuscadoD;

        #endregion Variables

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ObjAdmin = (BeAdmin)Session[Constantes.ObjUsuarioLogeado];


            if (Page.IsPostBack)
            {
                PeriodoBuscado = HfPeriodo.Value;
                PeriodoBuscadoD = HfPeriodoD.Value;
                return;
            }

            SeleccionarPais();
            menuReporte.Reporte1 = "ui-state-active";
            Session["tDinamica"] = "";
            Session["tDinamicaDetalle"] = "";
        }

        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(1);", true);
        //}

        protected void btnBuscarAux_Click(object sender, EventArgs e)
        {
            grillaUno.InnerHtml = String.Empty;

            var blReportedata = new BlReporte();
            //string periodo = ddlPeriodos.SelectedValue;
            var periodo = HfPeriodo.Value;
            PeriodoBuscado = periodo;
            var pais = ddlPaises.SelectedValue.Trim();
            var nivel = ddlNivel.SelectedValue.Trim();
            const string estado = "";
            var nombreColaborador = String.Empty;
            var nombreJefe = String.Empty;
            var tipo = ddlTipo.SelectedValue.Trim();
            var usuIna = cbUsuIna.Checked;
            var modeloDialogo = ddlModDialogo.SelectedValue.Trim();

            var entidades = blReportedata.ListarStatusDialogosDetalle(periodo, nombreColaborador, nivel, pais, nombreJefe, estado, tipo, usuIna, modeloDialogo);

            if (entidades.Count != 0)
            {
                //INICIO CABECERA

                var lsEncabezados = new List<string>
                {
                    "Paises",
                    "Por Iniciar",
                    "En Proceso",
                    "Por Aprobar",
                    "Aprobado",
                    "% Por Iniciar",
                    "% En Proceso",
                    "% Por Aprobar",
                    "% Aprobar"
                };

                var tDinamica = new HtmlTable();
                var htrFilaCabecera = new HtmlTableRow();
                htrFilaCabecera.Attributes.Add("style", "color:White;background-color:#60497B;border-color:White;font-weight:bold;");

                foreach (var encabezado in lsEncabezados)
                {
                    var htcCeldaCabecera = new HtmlTableCell();
                    htcCeldaCabecera.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;");
                    htcCeldaCabecera.InnerHtml = encabezado;
                    htrFilaCabecera.Cells.Add(htcCeldaCabecera);
                }

                tDinamica.Rows.Add(htrFilaCabecera);

                //FIN CABECERA

                //INICIO DATA
                var lhtrFilaData = new List<HtmlTableRow>();

                var lsPaises = new List<String>
                {
                    "AR",
                    "BO",
                    "CL",
                    "CO",
                    "CR",
                    "DO",
                    "EC",
                    "GT",
                    "MX",
                    "PA",
                    "PE",
                    "PR",
                    "SV",
                    "VE"
                };

                foreach (var country in lsPaises)
                {
                    var htrFila = new HtmlTableRow();
                    htrFila.Attributes.Add("style", "color:Black;background-color:#CCC0DA;border-color:White;border-width:1px;border-style:Solid;");
                    var htcceldaPais = new HtmlTableCell();
                    htcceldaPais.Attributes.Add("style", "color:White;background-color:#60497B;border-color:White;font-weight:bold;");
                    htcceldaPais.InnerHtml = country;
                    htrFila.Cells.Add(htcceldaPais);

                    var htcceldaPorIniciar = new HtmlTableCell();
                    htcceldaPorIniciar.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaPorIniciar.InnerHtml = ObtenerCantidadByPaisAndEstado(entidades, country, "Por iniciar");
                    htrFila.Cells.Add(htcceldaPorIniciar);

                    var htcceldaEnProceso = new HtmlTableCell();
                    htcceldaEnProceso.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaEnProceso.InnerHtml = ObtenerCantidadByPaisAndEstado(entidades, country, "En proceso");
                    htrFila.Cells.Add(htcceldaEnProceso);

                    var htcceldaPorAprobar = new HtmlTableCell();
                    htcceldaPorAprobar.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaPorAprobar.InnerHtml = ObtenerCantidadByPaisAndEstado(entidades, country, "Por Aprobar");
                    htrFila.Cells.Add(htcceldaPorAprobar);

                    var htcceldaAprobado = new HtmlTableCell();
                    htcceldaAprobado.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaAprobado.InnerHtml = ObtenerCantidadByPaisAndEstado(entidades, country, "Aprobado");
                    htrFila.Cells.Add(htcceldaAprobado);

                    var htcceldaPorcentajePorIniciar = new HtmlTableCell();
                    htcceldaPorcentajePorIniciar.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaPorcentajePorIniciar.InnerHtml = ObtenerPorcentajeByPaisAndEstado(entidades, country, "Por iniciar");
                    htrFila.Cells.Add(htcceldaPorcentajePorIniciar);

                    var htcceldaPorcentajeEnProceso = new HtmlTableCell();
                    htcceldaPorcentajeEnProceso.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaPorcentajeEnProceso.InnerHtml = ObtenerPorcentajeByPaisAndEstado(entidades, country, "En proceso");
                    htrFila.Cells.Add(htcceldaPorcentajeEnProceso);

                    var htcceldaPorcentajePorAprobar = new HtmlTableCell();
                    htcceldaPorcentajePorAprobar.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaPorcentajePorAprobar.InnerHtml = ObtenerPorcentajeByPaisAndEstado(entidades, country, "Por Aprobar");
                    htrFila.Cells.Add(htcceldaPorcentajePorAprobar);

                    var htcceldaPorcentajeAprobado = new HtmlTableCell();
                    htcceldaPorcentajeAprobado.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;width:80px;");
                    htcceldaPorcentajeAprobado.InnerHtml = ObtenerPorcentajeByPaisAndEstado(entidades, country, "Aprobado");
                    htrFila.Cells.Add(htcceldaPorcentajeAprobado);

                    lhtrFilaData.Add(htrFila);
                }


                foreach (var fd in lhtrFilaData)
                {
                    tDinamica.Rows.Add(fd);
                }

                //FIN DATA

                //INICIO PIE

                var porIniciar = ObtenerCantidadByEstado(entidades, "Por iniciar");
                var enProceso = ObtenerCantidadByEstado(entidades, "En proceso");
                var porAprobar = ObtenerCantidadByEstado(entidades, "Por Aprobar");
                var aprobado = ObtenerCantidadByEstado(entidades, "Aprobado");
                var constante = 100 / (Convert.ToDecimal(porIniciar) + Convert.ToDecimal(enProceso) + Convert.ToDecimal(porAprobar) + Convert.ToDecimal(aprobado));

                var lsPies = new List<string>
                {
                    "Total",
                    porIniciar,
                    enProceso,
                    porAprobar,
                    aprobado,
                    (Convert.ToDecimal(porIniciar)*constante).ToString(("###")) + " %",
                    (Convert.ToDecimal(enProceso)*constante).ToString(("###")) + " %",
                    (Convert.ToDecimal(porAprobar)*constante).ToString(("###")) + " %",
                    (Convert.ToDecimal(aprobado)*constante).ToString(("###")) + " %"
                };


                var htrFilaPie = new HtmlTableRow();
                htrFilaPie.Attributes.Add("style", "color:White;background-color:#60497B;border-color:White;font-weight:bold;");

                foreach (var pie in lsPies)
                {
                    var htcCeldaPie = new HtmlTableCell();
                    htcCeldaPie.Attributes.Add("style", "border-color:White;border-width:1px;border-style:Solid;");
                    htcCeldaPie.InnerHtml = pie;
                    htrFilaPie.Cells.Add(htcCeldaPie);
                }

                tDinamica.Rows.Add(htrFilaPie);

                grillaUno.InnerHtml = GetHtmlOfHtmlTable(tDinamica);
                Session["tDinamica"] = GetHtmlOfHtmlTable(tDinamica);


                //FIN PIE

            }
            else
            {
                const string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose(1);", true);
        }

        private string ObtenerCantidadByPaisAndEstado(IEnumerable<BeSeguimientoStatusDetalle> entidades, string pais, string estado)
        {
            var cantidadByPaisAndEstado = entidades.Count(entidad => entidad.Pais == pais && entidad.Estado == estado);

            return cantidadByPaisAndEstado.ToString(CultureInfo.InvariantCulture);
        }

        private string ObtenerPorcentajeByPaisAndEstado(IEnumerable<BeSeguimientoStatusDetalle> entidades, string pais, string estado)
        {
            decimal cantidadByPaisAndEstado = 0;
            decimal cantidadByPais = 0;

            foreach (var entidad in entidades)
            {
                if (entidad.Pais == pais && entidad.Estado == estado)
                {
                    cantidadByPaisAndEstado = cantidadByPaisAndEstado + 1;
                }

                if (entidad.Pais == pais)
                {
                    cantidadByPais = cantidadByPais + 1;
                }
            }

            if (cantidadByPais == 0)
            {
                cantidadByPais = 1;
            }

            var porcentaje = ((cantidadByPaisAndEstado / cantidadByPais) * 100);

            if (porcentaje == 0)
            {
                return "0 %";
            }
            return porcentaje.ToString(("###")) + " %";
        }

        private string ObtenerCantidadByEstado(IEnumerable<BeSeguimientoStatusDetalle> entidades, string estado)
        {
            var cantidadByEstado = entidades.Count(entidad => entidad.Estado == estado);

            return cantidadByEstado.ToString(CultureInfo.InvariantCulture);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            var html = HdnValue.Value;
            ExportToExcel(ref html, "SeguimientoStatus");
        }

        protected void btnExportarD_Click(object sender, EventArgs e)
        {

            var strFolder = Server.MapPath(@"~/Reportes/Imagen/");
            var id = Guid.NewGuid().ToString();

            var list = (List<BeSeguimientoStatusDetalle>)Session["tDinamicaDetalle"];

            var data = Utils.ConvertToDataTable(list);
            var fileName = string.Format("{0}.{1}", "SeguimientoStatus" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s"), "xls");
            var strFilePath = strFolder + id + fileName;

            var headerTitles = new List<string>();
            data.TableName = "Detalle de Estatus"; //titulo

            headerTitles.Add("PAIS");
            headerTitles.Add("PERIODO");
            headerTitles.Add("USUARIO");
            headerTitles.Add("ROL");
            headerTitles.Add("NOMBRE_EVALUADO");
            headerTitles.Add("EVALUADOR");
            headerTitles.Add("ROL_EVALUADOR");
            headerTitles.Add("NOMBRE_EVALUADOR");
            headerTitles.Add("ESTADO");
            headerTitles.Add("TIPO_DIALOGO");
            headerTitles.Add("MODELO_DIALOGO");


            HeaderTitle(headerTitles, ref data);


            var edw = new ExcelDatasetWriter();
            var ds = new DataSet();
            ds.Tables.Add(data);
            var book = edw.CreateWorkbook(ds);
            book.Save(strFilePath);
            var archivo = MatrizHelper.ReadFile(strFilePath);
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.ContentType = "application/xls";
            Response.BinaryWrite(archivo);
            Response.Flush();
            MatrizHelper.DeleteFile(strFilePath);
            Response.End();

        }

        private void HeaderTitle(IEnumerable<string> titulos, ref DataTable dt)
        {
            var i = 0;
            foreach (var titulo in titulos)
            {
                dt.Columns[i].ColumnName = titulo;
                i++;
            }
        }

        protected void btnBuscarDAux_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Session["tDinamica"].ToString()))
            {
                grillaUno.InnerHtml = Session["tDinamica"].ToString();
            }

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();

            var blReportedata = new BlReporte();
            //string periodo = ddlPeriodosD.SelectedValue;
            var periodoD = HfPeriodoD.Value;
            PeriodoBuscadoD = periodoD;
            var nombreColaborador = txtNombreColaboradorD.Text.Trim();
            var nivel = ddlNivelD.SelectedValue;
            var pais = ddlPaisesD.SelectedValue.Trim();
            var nombreJefe = txtNombreJefeD.Text.Trim();
            var estado = ddlEstatusD.SelectedValue.Trim();
            var tipo = ddlTipoD.SelectedValue.Trim();
            var usuIna = cbUsuInaD.Checked;
            var modeloDialogo = ddlModDialogoD.SelectedValue.Trim();

            var entidades = blReportedata.ListarStatusDialogosDetalle(periodoD, nombreColaborador, nivel, pais, nombreJefe, estado, tipo, usuIna, modeloDialogo);

            if (entidades.Count != 0)
            {
                gvDetalle.DataSource = entidades;
                gvDetalle.Columns[0].FooterText = "TOTAL :";
                gvDetalle.Columns[2].FooterText = entidades.Count.ToString(CultureInfo.InvariantCulture);
                gvDetalle.DataBind();

                Session["tDinamicaDetalle"] = entidades;
            }
            else
            {
                const string mensaje = "NO EXISTEN REGISTROS PARA LOS FILTROS SOLICITADOS";
                Page.ClientScript.RegisterStartupScript(GetType(), "Alerta", "Alerta('" + mensaje + "');", true);
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "ProcessClose", "ProcessClose(2);", true);
        }

        //protected void btnBuscarD_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "ProcessOpen", "ProcessOpen(2);", true);
        //}





        private String GetHtmlOfHtmlTable(HtmlTable htTable)
        {
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            var hw = new HtmlTextWriter(tw);
            htTable.RenderControl(hw);
            return sb.ToString();
        }


        public void ExportToExcel(ref string html, string fileName)
        {
            html = html.Replace("&gt;", ">");
            html = html.Replace("&lt;", "<");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
        }


        private void SeleccionarPais()
        {

            switch (ObjAdmin.TipoAdmin)
            {
                case Constantes.RolAdminCoorporativo:
                    ddlPaises.Enabled = true;
                    ddlPaisesD.Enabled = true;
                    break;
                case Constantes.RolAdminPais:
                    ddlPaises.Enabled = false;
                    ddlPaisesD.Enabled = false;
                    break;
                case Constantes.RolAdminEvaluciona:
                    ddlPaises.Enabled = false;
                    ddlPaisesD.Enabled = false;

                    if (ObjAdmin.CodigoPais == "CR")
                    {
                        ddlPaises.Items.Clear();
                        ddlPaises.Items.Insert(0, new ListItem("COSTA RICA", "CR"));
                        ddlPaises.Items.Insert(1, new ListItem("GUATEMALA", "GT"));
                        ddlPaises.Items.Insert(2, new ListItem("PANAMA", "PA"));
                        ddlPaises.Items.Insert(3, new ListItem("EL SALVADOR", "SV"));
                        ddlPaises.Enabled = true;

                        ddlPaisesD.Items.Clear();
                        ddlPaisesD.Items.Insert(0, new ListItem("COSTA RICA", "CR"));
                        ddlPaisesD.Items.Insert(1, new ListItem("GUATEMALA", "GT"));
                        ddlPaisesD.Items.Insert(2, new ListItem("PANAMA", "PA"));
                        ddlPaisesD.Items.Insert(3, new ListItem("EL SALVADOR", "SV"));
                        ddlPaisesD.Enabled = true;

                    }

                    if (ObjAdmin.CodigoPais == "DO")
                    {
                        ddlPaises.Items.Clear();
                        ddlPaises.Items.Insert(0, new ListItem("REP.DOMINICANA", "DO"));
                        ddlPaises.Items.Insert(1, new ListItem("PUERTORICO", "PR"));
                        ddlPaises.Enabled = true;

                        ddlPaisesD.Items.Clear();
                        ddlPaisesD.Items.Insert(0, new ListItem("REP.DOMINICANA", "DO"));
                        ddlPaisesD.Items.Insert(1, new ListItem("PUERTORICO", "PR"));
                        ddlPaisesD.Enabled = true;
                    }

                    if (ObjAdmin.CodigoPais != "CR" && ObjAdmin.CodigoPais != "DO")
                    {
                        ddlPaises.Items.Clear();
                        ddlPaises.Items.Insert(0, new ListItem(ObjAdmin.NombrePais, ObjAdmin.CodigoPais));
                        ddlPaises.Enabled = true;

                        ddlPaisesD.Items.Clear();
                        ddlPaisesD.Items.Insert(0, new ListItem(ObjAdmin.NombrePais, ObjAdmin.CodigoPais));
                        ddlPaisesD.Enabled = true;
                    }

                    break;
            }
        }

        #endregion Metodos
    }
}