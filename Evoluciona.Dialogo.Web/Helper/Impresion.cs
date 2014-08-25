
namespace Evoluciona.Dialogo.Web.Helper
{
    using BusinessEntity;
    using BusinessLogic;
    using CarlosAg.ExcelXmlWriter;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.UI.WebControls;

    public class Impresion
    {
        #region Variables

        public int idProceso;
        private string codUsuarioEvaluado;
        private int rolUsuarioEvaluado;
        private string codPais;
        private string periodo;
        private string nombreEvaluado;
        private string codUsuarioEvaluador;
        public string imprimir;
        public string soloNegocio;
        private readonly BlIndicadores indicadorBL = new BlIndicadores();
        private string estadoProceso;

        #endregion Variables

        #region Propiedades

        private string CadenaConexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["cnxDialogoDesempenio"].ConnectionString;
            }
        }

        public string EstadoProceso
        {
            get { return estadoProceso; }
            set { estadoProceso = value; }
        }

        #endregion Propiedades

        public void GenerarExcel(string filename, string evaluados, string evaluadosNombre, int idRolEvaluado, string prefijoIsoPais)
        {
            Workbook libro = new Workbook();
            BlProceso blproceso = new BlProceso();
            BeProceso proceso = new BeProceso();
            libro.ExcelWorkbook.ActiveSheetIndex = 0;
            libro.ExcelWorkbook.ProtectWindows = false;
            libro.ExcelWorkbook.ProtectStructure = false;
            codPais = prefijoIsoPais;

            // -----------------------------------------------
            //  Generate Styles
            // -----------------------------------------------
            GenerateStyles(libro.Styles);

            if (evaluados != "")
            {
                string[] arrEvaluados = evaluados.Split(',');
                string[] arrEvaluadosNombre = evaluadosNombre.Split(',');

                for (int i = 0; i < arrEvaluadosNombre.Length; i++)
                {
                    if (arrEvaluados.Length == arrEvaluadosNombre.Length)
                    {
                        proceso = blproceso.ObtenerProceso(Convert.ToInt32(arrEvaluados[i]));
                        idProceso = proceso.IdProceso;
                        codUsuarioEvaluado = proceso.CodigoUsuario;
                        codUsuarioEvaluador = proceso.CodigoUsuarioEvaluador;
                        nombreEvaluado = arrEvaluadosNombre[i];
                        periodo = proceso.Periodo;
                        rolUsuarioEvaluado = idRolEvaluado;
                        estadoProceso = proceso.EstadoDescripcion;

                        BlResumenProceso objResumenBL = new BlResumenProceso();
                        BeResumenProceso objDatosGR = objResumenBL.ObtenerUsuarioGRegionEvaluado(codUsuarioEvaluado, string.Empty, periodo, Constantes.EstadoActivo);
                        if (objDatosGR != null && !string.IsNullOrEmpty(objDatosGR.prefijoIsoPais))
                            codPais = objDatosGR.prefijoIsoPais;

                        GenerarResumen(libro.Worksheets, proceso, arrEvaluadosNombre[i]);
                    }
                    else
                    {
                        proceso = blproceso.ObtenerProceso(Convert.ToInt32(arrEvaluados[i + 1]));
                        idProceso = proceso.IdProceso;
                        codUsuarioEvaluado = proceso.CodigoUsuario;
                        codUsuarioEvaluador = proceso.CodigoUsuarioEvaluador;
                        nombreEvaluado = arrEvaluadosNombre[i];
                        periodo = proceso.Periodo;
                        rolUsuarioEvaluado = idRolEvaluado;

                        BlResumenProceso objResumenBL = new BlResumenProceso();
                        BeResumenProceso objDatosGR = objResumenBL.ObtenerUsuarioGRegionEvaluado(codUsuarioEvaluado, string.Empty, periodo, Constantes.EstadoActivo);
                        if (objDatosGR != null && !string.IsNullOrEmpty(objDatosGR.prefijoIsoPais))
                            codPais = objDatosGR.prefijoIsoPais;

                        GenerarResumen(libro.Worksheets, proceso, arrEvaluadosNombre[i]);
                    }
                }
            }

            libro.Save(filename);
            //Process.Start(filename);
        }

        private void GenerateStyles(WorksheetStyleCollection styles)
        {
            // -----------------------------------------------
            //  Default
            // -----------------------------------------------
            WorksheetStyle Default = styles.Add("Default");
            Default.Name = "Normal";
            Default.Font.FontName = "Calibri";
            Default.Font.Size = 11;
            Default.Font.Color = "#000000";
            Default.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            // -----------------------------------------------
            //  m76566016
            // -----------------------------------------------
            WorksheetStyle m76566016 = styles.Add("m76566016");
            m76566016.Font.FontName = "Arial";
            m76566016.Font.Size = 9;
            m76566016.Font.Color = "#000000";
            m76566016.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            m76566016.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            m76566016.Alignment.WrapText = true;
            m76566016.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            m76566016.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            m76566016.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            m76566016.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  m76566036
            // -----------------------------------------------
            WorksheetStyle m76566036 = styles.Add("m76566036");
            m76566036.Font.FontName = "Arial";
            m76566036.Font.Size = 9;
            m76566036.Font.Color = "#000000";
            m76566036.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            m76566036.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            m76566036.Alignment.WrapText = true;
            m76566036.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            m76566036.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            m76566036.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            m76566036.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  m76566056
            // -----------------------------------------------
            WorksheetStyle m76566056 = styles.Add("m76566056");
            m76566056.Font.FontName = "Calibri";
            m76566056.Font.Size = 9;
            m76566056.Font.Color = "#000000";
            m76566056.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            m76566056.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            m76566056.Alignment.WrapText = true;
            m76566056.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            m76566056.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            m76566056.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            m76566056.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  m76566076
            // -----------------------------------------------
            WorksheetStyle m76566076 = styles.Add("m76566076");
            m76566076.Font.FontName = "Calibri";
            m76566076.Font.Size = 9;
            m76566076.Font.Color = "#000000";
            m76566076.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            m76566076.Alignment.WrapText = true;
            m76566076.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            m76566076.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            m76566076.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            m76566076.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  m76566096
            // -----------------------------------------------
            WorksheetStyle m76566096 = styles.Add("m76566096");
            m76566096.Font.FontName = "Calibri";
            m76566096.Font.Size = 9;
            m76566096.Font.Color = "#000000";
            m76566096.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            m76566096.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            m76566096.Alignment.WrapText = true;
            m76566096.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            m76566096.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            m76566096.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            m76566096.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  m76566116
            // -----------------------------------------------
            WorksheetStyle m76566116 = styles.Add("m76566116");
            m76566116.Font.FontName = "Calibri";
            m76566116.Font.Size = 9;
            m76566116.Font.Color = "#000000";
            m76566116.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            m76566116.Alignment.WrapText = true;
            m76566116.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            m76566116.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            m76566116.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            m76566116.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s62
            // -----------------------------------------------
            WorksheetStyle s62 = styles.Add("s62");
            s62.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s62.Alignment.WrapText = true;
            // -----------------------------------------------
            //  s63
            // -----------------------------------------------
            WorksheetStyle s63 = styles.Add("s63");
            s63.Font.Bold = true;
            s63.Font.FontName = "Calibri";
            s63.Font.Size = 11;
            s63.Font.Color = "#000000";
            s63.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            s63.Alignment.Vertical = StyleVerticalAlignment.Center;
            s63.Alignment.WrapText = true;
            s63.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s63.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s63.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s63.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s64
            // -----------------------------------------------
            WorksheetStyle s64 = styles.Add("s64");
            s64.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            s64.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s64.Alignment.WrapText = true;
            s64.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s64.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s64.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s64.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s65
            // -----------------------------------------------
            WorksheetStyle s65 = styles.Add("s65");
            s65.Alignment.Horizontal = StyleHorizontalAlignment.Right;
            s65.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s65.Alignment.WrapText = true;
            s65.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s65.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s65.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s65.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s66
            // -----------------------------------------------
            WorksheetStyle s66 = styles.Add("s66");
            s66.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            s66.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s66.Alignment.WrapText = true;
            s66.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s66.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s66.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s66.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s67
            // -----------------------------------------------
            WorksheetStyle s67 = styles.Add("s67");
            s67.Alignment.Horizontal = StyleHorizontalAlignment.Right;
            s67.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s67.Alignment.WrapText = true;
            s67.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s67.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s67.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s67.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            s67.NumberFormat = "Standard";
            // -----------------------------------------------
            //  s68
            // -----------------------------------------------
            WorksheetStyle s68 = styles.Add("s68");
            s68.Font.Bold = true;
            s68.Font.FontName = "Arial";
            s68.Font.Size = 9;
            s68.Font.Color = "#000000";
            s68.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            s68.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s68.Alignment.WrapText = true;
            s68.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s68.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s68.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s68.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s69
            // -----------------------------------------------
            WorksheetStyle s69 = styles.Add("s69");
            s69.Font.Bold = true;
            s69.Font.FontName = "Arial";
            s69.Font.Size = 9;
            s69.Font.Color = "#000000";
            s69.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            s69.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s69.Alignment.WrapText = true;
            s69.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s69.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s69.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s69.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s70
            // -----------------------------------------------
            WorksheetStyle s70 = styles.Add("s70");
            s70.Font.FontName = "Arial";
            s70.Font.Size = 9;
            s70.Font.Color = "#000000";
            s70.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            s70.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s70.Alignment.WrapText = true;
            s70.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s70.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s70.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s70.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s74
            // -----------------------------------------------
            WorksheetStyle s74 = styles.Add("s74");
            s74.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s74.Alignment.WrapText = true;
            s74.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s74.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            s74.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s75
            // -----------------------------------------------
            WorksheetStyle s75 = styles.Add("s75");
            s75.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s75.Alignment.WrapText = true;
            s75.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s75.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#000000");
            s75.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s89
            // -----------------------------------------------
            WorksheetStyle s89 = styles.Add("s89");
            s89.Font.Bold = true;
            s89.Font.FontName = "Arial";
            s89.Font.Size = 9;
            s89.Font.Color = "#00ACEE";
            s89.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s89.Alignment.WrapText = true;
            // -----------------------------------------------
            //  s90
            // -----------------------------------------------
            WorksheetStyle s90 = styles.Add("s90");
            s90.Font.Bold = true;
            s90.Font.FontName = "Calibri";
            s90.Font.Size = 11;
            s90.Font.Color = "#000000";
            s90.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s90.Alignment.WrapText = true;
            // -----------------------------------------------
            //  s91
            // -----------------------------------------------
            WorksheetStyle s91 = styles.Add("s91");
            s91.Font.FontName = "Arial";
            s91.Font.Size = 9;
            s91.Font.Color = "#000000";
            s91.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s91.Alignment.WrapText = true;
            // -----------------------------------------------
            //  s92
            // -----------------------------------------------
            WorksheetStyle s92 = styles.Add("s92");
            s92.Font.Bold = true;
            s92.Font.FontName = "Arial";
            s92.Font.Size = 12;
            s92.Font.Color = "#6A288A";
            s92.Alignment.Horizontal = StyleHorizontalAlignment.Left;
            s92.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s92.Alignment.WrapText = true;
            // -----------------------------------------------
            //  s93
            // -----------------------------------------------
            WorksheetStyle s93 = styles.Add("s93");
            s93.Font.Bold = true;
            s93.Font.FontName = "Calibri";
            s93.Font.Size = 11;
            s93.Font.Color = "#000000";
            s93.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s93.Alignment.WrapText = true;
            s93.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s94
            // -----------------------------------------------
            WorksheetStyle s94 = styles.Add("s94");
            s94.Font.Bold = true;
            s94.Font.FontName = "Calibri";
            s94.Font.Size = 11;
            s94.Font.Color = "#000000";
            s94.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s94.Alignment.WrapText = true;
            s94.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#000000");
            s94.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");
            // -----------------------------------------------
            //  s95
            // -----------------------------------------------
            WorksheetStyle s95 = styles.Add("s95");
            s95.Font.Bold = true;
            s95.Font.FontName = "Calibri";
            s95.Font.Size = 11;
            s95.Font.Color = "#000000";
            s95.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s95.Alignment.WrapText = true;
            s95.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#000000");

            // -----------------------------------------------
            WorksheetStyle s96 = styles.Add("s96");
            s96.Font.FontName = "Arial";
            s96.Font.Size = 9;
            s96.Font.Color = "#000000";
            s96.Alignment.Vertical = StyleVerticalAlignment.Bottom;
            s96.Alignment.WrapText = true;
        }

        private void GenerarResumen(WorksheetCollection hojas, BeProceso proceso, string variable)
        {
            Worksheet hoja = hojas.Add(variable);
            // -----------------------------------------------
            WorksheetRow row = hoja.Table.Rows.Add();
            WorksheetCell cell;
            // -----------------------------------------------
            hoja.Table.DefaultRowHeight = 15F;
            hoja.Table.DefaultColumnWidth = 100F;
            hoja.Table.ExpandedColumnCount = 10000;
            hoja.Table.ExpandedRowCount = 300000;
            hoja.Table.FullColumns = 1;
            hoja.Table.FullRows = 1;
            //hoja.Table.Columns.Add(221);
            //WorksheetColumn column1 = hoja.Table.Columns.Add();
            //column1.Width = 240;
            //column1.Span = 1;
            //WorksheetColumn column2 = hoja.Table.Columns.Add();
            //column2.Index = 4;
            //column2.Width = 84;
            //hoja.Table.Columns.Add(48);
            //hoja.Table.Columns.Add(100);

            hoja.Table.Columns.Add(154);
            hoja.Table.Columns.Add(240);
            hoja.Table.Columns.Add(240);
            hoja.Table.Columns.Add(240);
            hoja.Table.Columns.Add(240);
            hoja.Table.Columns.Add(100);

            // -----------------------------------------------
            row.Height = 15;
            row.AutoFitHeight = false;
            cell = row.Cells.Add();
            cell.StyleID = "s92";
            cell.Data.Type = DataType.String;
            cell.Data.Text = variable;
            cell.MergeAcross = 5;
            // -----------------------------------------------
            row = hoja.Table.Rows.Add();
            cell = row.Cells.Add();
            cell.StyleID = "s62";
            cell.MergeAcross = 5;
            // -----------------------------------------------
            row = hoja.Table.Rows.Add();
            row.AutoFitHeight = false;
            cell = row.Cells.Add();
            cell.StyleID = "s90";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "ANTES NEGOGIO ";
            cell.MergeAcross = 5;
            // -----------------------------------------------
            row = hoja.Table.Rows.Add();
            row.AutoFitHeight = false;
            cell = row.Cells.Add();
            cell.StyleID = "s93";
            cell.Data.Type = DataType.String;
            cell.Data.Text = "Resultados en las variables del Negocio. ";
            cell.MergeAcross = 5;
            // -----------------------------------------------
            try
            {
                DataTable dtPeriodoEvaluacion = indicadorBL.ValidarPeriodoEvaluacion(periodo, codPais, rolUsuarioEvaluado, CadenaConexion);
                string anioCampana = string.Empty;

                if (dtPeriodoEvaluacion != null)
                {
                    anioCampana = dtPeriodoEvaluacion.Rows[0]["chrAnioCampana"].ToString();
                }

                DataSet dsgrdvVariablesBase = indicadorBL.Cargarindicadoresporperiodo(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, CadenaConexion);
                if (dsgrdvVariablesBase.Tables.Count > 0)
                {
                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    row.Cells.Add("Variables", DataType.String, "s63");
                    row.Cells.Add("Meta", DataType.String, "s63");
                    row.Cells.Add("Resultado", DataType.String, "s63");
                    row.Cells.Add("Diferencia", DataType.String, "s63");
                    row.Cells.Add("Campaña", DataType.String, "s63");
                    row.Cells.Add("Variable de Enfoque", DataType.String, "s63");
                    // -----------------------------------------------
                    foreach (DataRow fila in dsgrdvVariablesBase.Tables[0].Rows)
                    {
                        string vchDesVariable = fila["vchDesVariable"].ToString();
                        string decValorPlanPeriodo = fila["decValorPlanPeriodo"].ToString();
                        string decValorRealPeriodo = fila["decValorRealPeriodo"].ToString();
                        string diferencia = fila["Diferencia"].ToString();
                        string chrAnioCampana = fila["chrAnioCampana"].ToString();
                        string bitEstado = fila["bitEstado"].ToString();
                        row = hoja.Table.Rows.Add();
                        row.Cells.Add(vchDesVariable, DataType.String, "s64");
                        row.Cells.Add(decValorPlanPeriodo, DataType.String, "s65");
                        row.Cells.Add(decValorRealPeriodo, DataType.String, "s65");
                        row.Cells.Add(diferencia, DataType.String, "s65");
                        row.Cells.Add(chrAnioCampana, DataType.String, "s66");
                        row.Cells.Add(formatear(bitEstado), DataType.String, "s66");
                    }
                }

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s94";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "Variables Adicionales ";
                cell.MergeAcross = 5;

                DataSet dsgrdvVariablesAdicionales = indicadorBL.CargarindicadoresporperiodoVariablesAdicionales(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, CadenaConexion);
                if (dsgrdvVariablesAdicionales.Tables.Count > 0)
                {
                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    row.Cells.Add("Variables", DataType.String, "s63");
                    row.Cells.Add("Meta", DataType.String, "s63");
                    row.Cells.Add("Resultado", DataType.String, "s63");
                    row.Cells.Add("Diferencia", DataType.String, "s63");
                    row.Cells.Add("Campaña", DataType.String, "s63");
                    row.Cells.Add("Variable de Enfoque", DataType.String, "s63");
                    // -----------------------------------------------
                    foreach (DataRow fila in dsgrdvVariablesAdicionales.Tables[0].Rows)
                    {
                        string vchDesVariable = fila["vchDesVariable"].ToString();
                        string decValorPlanPeriodo = fila["decValorPlanPeriodo"].ToString();
                        string decValorRealPeriodo = fila["decValorRealPeriodo"].ToString();
                        string diferencia = fila["Diferencia"].ToString();
                        string chrAnioCampana = fila["chrAnioCampana"].ToString();
                        string bitEstado = fila["bitEstado"].ToString();
                        row = hoja.Table.Rows.Add();
                        row.Cells.Add(vchDesVariable, DataType.String, "s64");
                        row.Cells.Add(decValorPlanPeriodo, DataType.String, "s65");
                        row.Cells.Add(decValorRealPeriodo, DataType.String, "s65");
                        row.Cells.Add(diferencia, DataType.String, "s65");
                        row.Cells.Add(chrAnioCampana, DataType.String, "s66");
                        row.Cells.Add(formatear(bitEstado), DataType.String, "s66");
                    }
                }

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s95";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "Variables Causales ";
                cell.MergeAcross = 5;

                int totalSeleccionados = 0;
                string idVariable1 = string.Empty;
                string idVariable2 = string.Empty;
                string idVariable1A = string.Empty;
                string idVariable2A = string.Empty;

                if (dsgrdvVariablesBase.Tables.Count > 0)
                {
                    foreach (DataRow fila in dsgrdvVariablesBase.Tables[0].Rows)
                    {
                        bool seleccionado = bool.Parse(fila.ItemArray[6].ToString());
                        if (seleccionado)
                        {
                            if (totalSeleccionados == 0)
                            {
                                idVariable1 = fila.ItemArray[0].ToString();
                                idVariable1A = fila.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else if (totalSeleccionados == 1)
                            {
                                idVariable2 = fila.ItemArray[0].ToString();
                                idVariable2A = fila.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (dsgrdvVariablesAdicionales.Tables.Count > 0)
                {
                    foreach (DataRow fila in dsgrdvVariablesAdicionales.Tables[0].Rows)
                    {
                        bool seleccionado = bool.Parse(fila.ItemArray[6].ToString());
                        if (seleccionado)
                        {
                            if (totalSeleccionados == 0)
                            {
                                idVariable1 = fila.ItemArray[0].ToString();
                                idVariable1A = fila.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else if (totalSeleccionados == 1)
                            {
                                idVariable2 = fila.ItemArray[0].ToString();
                                idVariable2A = fila.ItemArray[1].ToString();
                                totalSeleccionados++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                DataTable variablesCausaIndicador1 = indicadorBL.ObtenerVariablesCausa(idProceso, idVariable1);
                DataTable variablesCausaIndicador2 = indicadorBL.ObtenerVariablesCausa(idProceso, idVariable2);
                string ddlVariableCausa1 = string.Empty, ddlVariableCausa2 = string.Empty;
                string ddlVariableCausa3 = string.Empty, ddlVariableCausa4 = string.Empty;
                string txtVariable1PlanPeriodo = string.Empty, txtVariable1Real = string.Empty;
                string txtVariable1Diferencia = string.Empty, txtVariable2PlanPeriodo = string.Empty;
                string txtVariable2Real = string.Empty, txtVariable2Diferencia = string.Empty;
                string txtVariable3PlanPeriodo = string.Empty, txtVariable3Real = string.Empty;
                string txtVariable3Diferencia = string.Empty, txtVariable4PlanPeriodo = string.Empty;
                string txtVariable4Real = string.Empty, txtVariable4Diferencia = string.Empty;

                if (variablesCausaIndicador1.Rows.Count > 1)
                {
                    ddlVariableCausa1 = variablesCausaIndicador1.Rows[0].ItemArray[0].ToString().Trim();
                    ddlVariableCausa2 = variablesCausaIndicador1.Rows[1].ItemArray[0].ToString().Trim();
                }

                if (variablesCausaIndicador2.Rows.Count > 1)
                {
                    ddlVariableCausa3 = variablesCausaIndicador2.Rows[0].ItemArray[0].ToString().Trim();
                    ddlVariableCausa4 = variablesCausaIndicador2.Rows[1].ItemArray[0].ToString().Trim();
                }

                DataTable dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa1, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa1 = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable1PlanPeriodo = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable1Real = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable1Diferencia = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa2, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa2 = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable2PlanPeriodo = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable2Real = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable2Diferencia = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa3, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa3 = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable3PlanPeriodo = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable3Real = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable3Diferencia = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                dtCampana = indicadorBL.CargarDatosVariableCausaEvaluado(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais, anioCampana, ddlVariableCausa4, CadenaConexion);
                if (dtCampana != null)
                {
                    if (dtCampana.Rows.Count > 0)
                    {
                        ddlVariableCausa4 = dtCampana.Rows[0]["vchDesVariable"].ToString();
                        txtVariable4PlanPeriodo = string.Format("{0:F2}", dtCampana.Rows[0]["decValorPlanPeriodo"]);
                        txtVariable4Real = string.Format("{0:F2}", dtCampana.Rows[0]["decValorRealPeriodo"]);
                        txtVariable4Diferencia = string.Format("{0:F2}", dtCampana.Rows[0]["Diferencia"]);
                    }
                }

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "m76566016";
                cell.Data.Type = DataType.String;
                cell.Data.Text = idVariable1A;
                cell.MergeAcross = 3;
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 15;
                row.AutoFitHeight = false;
                row.Cells.Add("Variable Causa", DataType.String, "s68");
                row.Cells.Add("Objetivo", DataType.String, "s69");
                row.Cells.Add("Real", DataType.String, "s69");
                row.Cells.Add("Diferencia", DataType.String, "s69");
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 15;
                row.AutoFitHeight = false;
                row.Cells.Add(ddlVariableCausa1, DataType.String, "s70");
                row.Cells.Add(txtVariable1PlanPeriodo, DataType.String, "s70");
                row.Cells.Add(txtVariable1Real, DataType.String, "s70");
                row.Cells.Add(txtVariable1Diferencia, DataType.String, "s70");
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 19;
                row.AutoFitHeight = false;
                row.Cells.Add(ddlVariableCausa2, DataType.String, "s70");
                row.Cells.Add(txtVariable2PlanPeriodo, DataType.String, "s70");
                row.Cells.Add(txtVariable2Real, DataType.String, "s70");
                row.Cells.Add(txtVariable2Diferencia, DataType.String, "s70");

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 15;
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "m76566036";
                cell.Data.Type = DataType.String;
                cell.Data.Text = idVariable2A;
                cell.MergeAcross = 3;
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 15;
                row.AutoFitHeight = false;
                row.Cells.Add("Variable Causa", DataType.String, "s68");
                row.Cells.Add("Objetivo", DataType.String, "s69");
                row.Cells.Add("Real", DataType.String, "s69");
                row.Cells.Add("Diferencia", DataType.String, "s69");
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 15;
                row.AutoFitHeight = false;
                row.Cells.Add(ddlVariableCausa3, DataType.String, "s70");
                row.Cells.Add(txtVariable3PlanPeriodo, DataType.String, "s70");
                row.Cells.Add(txtVariable3Real, DataType.String, "s70");
                row.Cells.Add(txtVariable3Diferencia, DataType.String, "s70");
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Height = 19;
                row.AutoFitHeight = false;
                row.Cells.Add(ddlVariableCausa4, DataType.String, "s70");
                row.Cells.Add(txtVariable4PlanPeriodo, DataType.String, "s70");
                row.Cells.Add(txtVariable4Real, DataType.String, "s70");
                row.Cells.Add(txtVariable4Diferencia, DataType.String, "s70");

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.MergeAcross = 5;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "ANTES EQUIPO ";
                cell.MergeAcross = 5;

                DataTable dtResumen = indicadorBL.ObtenerResumen(periodo, codUsuarioEvaluado, idProceso, rolUsuarioEvaluado, codPais);
                if (dtResumen != null)
                {
                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    foreach (DataRow fila in dtResumen.Rows)
                    {
                        string vchEstadoPeriodo1 = fila["vchEstadoPeriodo"].ToString();
                        string TotalEstados = fila["%"].ToString();
                        row.Cells.Add(vchEstadoPeriodo1 + " " + TotalEstados + " % ", DataType.String, "s62");
                    }
                }
                //Antes Equipos

                BlCritica criticaBL = new BlCritica();

                DataTable dtPeriodoEvaluacionE = criticaBL.ValidarPeriodoEvaluacion(periodo, codPais, rolUsuarioEvaluado, CadenaConexion);
                string campanha = string.Empty;
                if (dtPeriodoEvaluacionE != null)
                {
                    if (dtPeriodoEvaluacionE.Rows.Count > 0)
                        campanha = dtPeriodoEvaluacionE.Rows[0]["chrAnioCampana"].ToString();
                }

                List<BeCriticas> lstCargarCriticasProcesadas = criticaBL.ListaCargarCriticasProcesadas(codUsuarioEvaluado, periodo, rolUsuarioEvaluado, codPais, CadenaConexion, idProceso, campanha);

                if (lstCargarCriticasProcesadas.Count > 0)
                {
                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    cell = row.Cells.Add();
                    cell.StyleID = "s62";
                    cell.MergeAcross = 5;

                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    row.Cells.Add("Críticas", DataType.String, "s63");
                    row.Cells.Add("Variables a considerar", DataType.String, "s63");
                    // -----------------------------------------------
                    foreach (BeCriticas critica in lstCargarCriticasProcesadas)
                    {
                        row = hoja.Table.Rows.Add();
                        row.AutoFitHeight = true;
                        row.Cells.Add(critica.nombresCritica, DataType.String, "s64");
                        row.Cells.Add(critica.variableConsiderar, DataType.String, "s66");
                    }
                }

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.MergeAcross = 5;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "ANTES COMPETENCIAS";
                cell.MergeAcross = 5;

                BlPlanAnual daProceso = new BlPlanAnual();
                BeResumenProceso resumenProceso = new BeResumenProceso();
                resumenProceso.idProceso = idProceso;
                DataTable dtGrabadas = daProceso.ObtenerPlanAnualGrabadas(CadenaConexion, resumenProceso);
                string lblObservacion = string.Empty;

                if (dtGrabadas != null)
                {
                    if (dtGrabadas.Rows.Count > 0)
                    {
                        // -----------------------------------------------
                        row = hoja.Table.Rows.Add();
                        row.Cells.Add("Competencia", DataType.String, "s63");
                        row.Cells.Add("Comportamiento", DataType.String, "s63");
                        row.Cells.Add("Sugerencia", DataType.String, "s63");
                        foreach (DataRow fila in dtGrabadas.Rows)
                        {
                            row = hoja.Table.Rows.Add();
                            row.AutoFitHeight = true;
                            string competencia = fila["Competencia"].ToString();
                            string comportamiento = fila["comportamiento"].ToString();
                            string sugerencia = fila["Sugerencia"].ToString();
                            row.Cells.Add(competencia, DataType.String, "s66");
                            row.Cells.Add(comportamiento, DataType.String, "s66");
                            row.Cells.Add(sugerencia, DataType.String, "s66");

                            int[] lista = new int[] { competencia.Length, comportamiento.Length, sugerencia.Length };
                            int mayor = Mayor(lista);
                            int tamanio = Convert.ToInt32(decimal.Floor((mayor / 45))) + 1;
                            row.Height = tamanio * row.Height;
                        }
                        lblObservacion = dtGrabadas.Rows[0]["observacion"].ToString().ToUpper();
                    }
                }

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "Observaciones :";
                cell.MergeAcross = 5;

                // -----------------------------------------------
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s96";
                cell.Data.Type = DataType.String;
                cell.Data.Text = lblObservacion;
                cell.MergeAcross = 5;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.MergeAcross = 5;
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "DURANTE Y DESPUES";
                cell.MergeAcross = 5;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.MergeAcross = 5;
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "DURANTE Y DESPUES NEGOCIO";
                cell.MergeAcross = 5;

                //DURANTE Y DESPUES

                string idsVariablesIndicador = string.Empty;
                string idsVariablesEnfoque = string.Empty;
                string litEstadoNegocio = string.Empty;
                string litEstadoEquipo = string.Empty;
                string litEstadoCompetencias = string.Empty;

                if (proceso.EstadoDescripcion == "DESPUES")
                {
                    litEstadoNegocio = "DESPUÉS";
                    litEstadoEquipo = "DESPUÉS";
                    litEstadoCompetencias = "DESPUÉS";
                }

                //blIndicadores objIndicadorBL = new blIndicadores();
                BlVariableEnfoque objVarEnfoquesBL = new BlVariableEnfoque();
                string campaniaProcesada = string.Empty;
                string variable1 = string.Empty;
                string variable2 = string.Empty;
                string variableCausa1 = string.Empty;
                string variableCausa2 = string.Empty;
                string variableCausa3 = string.Empty;
                string variableCausa4 = string.Empty;
                string txtZonas1 = string.Empty;
                string txtPlanAccion1 = string.Empty;
                string lblEstadoIndicador1 = string.Empty;
                string txtZonas2 = string.Empty;
                string txtPlanAccion2 = string.Empty;
                string lblEstadoIndicador2 = string.Empty;
                DataTable dtIndicadores = indicadorBL.ObtenerIndicadoresProcesados(idProceso);

                if (dtIndicadores != null)
                {
                    if (dtIndicadores.Rows.Count > 0)
                        campaniaProcesada = dtIndicadores.Rows[0]["chrAnioCampanha"].ToString();
                }

                #region Iterar DT Indicadores

                for (int x = 0; x < dtIndicadores.Rows.Count; x++)
                {
                    string idVariable = dtIndicadores.Rows[x]["intIDIndicador"].ToString();
                    string idVarEnfoque = string.Empty;

                    if (x == 0)
                    {
                        variable1 = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();

                        List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(idVariable));

                        if (lstVariableEnfoque.Count > 0)
                        {
                            txtZonas1 = lstVariableEnfoque[0].zonas;
                            idVarEnfoque = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                            txtPlanAccion1 = lstVariableEnfoque[0].planAccion;

                            lblEstadoIndicador1 = lstVariableEnfoque[0].postDialogo ? "(Completado)" : "(En Proceso)";
                        }
                    }
                    if (x == 1)
                    {
                        variable2 = dtIndicadores.Rows[x]["vchSeleccionado"].ToString();
                        idVariable = dtIndicadores.Rows[x]["intIDIndicador"].ToString();

                        List<BeVariableEnfoque> lstVariableEnfoque = objVarEnfoquesBL.ObtenerVariablesEnfoqueProcesadas(Convert.ToInt32(idVariable));
                        if (lstVariableEnfoque.Count > 0)
                        {
                            txtZonas2 = lstVariableEnfoque[0].zonas;
                            idVarEnfoque = lstVariableEnfoque[0].idVariableEnfoque.ToString();
                            txtPlanAccion2 = lstVariableEnfoque[0].planAccion;

                            lblEstadoIndicador2 = lstVariableEnfoque[0].postDialogo ? "(Completado)" : "(En Proceso)";
                        }
                    }

                    idsVariablesIndicador += "," + idVariable;
                    idsVariablesEnfoque += "," + idVarEnfoque;
                }

                #endregion Iterar DT Indicadores

                #region Cargar Variables de Enfoque

                string lblVariableGeneral1 = "Variable no Definida";
                string lblVariableGeneral2 = "Variable no Definida";
                string Label3 = "Variable Causa no Definida";
                string Label7 = "Variable Causa no Definida";
                string Label8 = "Variable Causa no Definida";
                string Label9 = "Variable Causa no Definida";

                DataTable dtDescripcionVariableEnfoque1 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variable1.Trim(), campaniaProcesada, periodo);
                if (dtDescripcionVariableEnfoque1 != null)
                {
                    if (dtDescripcionVariableEnfoque1.Rows.Count > 0)
                    {
                        lblVariableGeneral1 = dtDescripcionVariableEnfoque1.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque2 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variable2.Trim(), campaniaProcesada, periodo);
                if (dtDescripcionVariableEnfoque2 != null)
                {
                    if (dtDescripcionVariableEnfoque2.Rows.Count > 0)
                    {
                        lblVariableGeneral2 = dtDescripcionVariableEnfoque2.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtVariablesCausa1 = indicadorBL.ObtenerVariablesCausa(idProceso, variable1.Trim());
                if (dtVariablesCausa1.Rows.Count > 0)
                {
                    variableCausa1 = dtVariablesCausa1.Rows[0]["chrCodVariableHija"].ToString();

                    if (dtVariablesCausa1.Rows.Count > 1)
                    {
                        variableCausa2 = dtVariablesCausa1.Rows[1]["chrCodVariableHija"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque3 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa1.Trim(), campaniaProcesada, periodo);
                if (dtDescripcionVariableEnfoque3 != null)
                {
                    if (dtDescripcionVariableEnfoque3.Rows.Count > 0)
                    {
                        Label3 = dtDescripcionVariableEnfoque3.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque4 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa2.Trim(), campaniaProcesada, periodo);
                if (dtDescripcionVariableEnfoque4 != null)
                {
                    if (dtDescripcionVariableEnfoque4.Rows.Count > 0)
                    {
                        Label7 = dtDescripcionVariableEnfoque4.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtVariablesCausa2 = indicadorBL.ObtenerVariablesCausa(idProceso, variable2.Trim());
                if (dtVariablesCausa2.Rows.Count > 0)
                {
                    variableCausa3 = dtVariablesCausa2.Rows[0]["chrCodVariableHija"].ToString();

                    if (dtVariablesCausa2.Rows.Count > 1)
                    {
                        variableCausa4 = dtVariablesCausa2.Rows[1]["chrCodVariableHija"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque5 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa3.Trim(), campaniaProcesada, periodo);
                if (dtDescripcionVariableEnfoque5 != null)
                {
                    if (dtDescripcionVariableEnfoque5.Rows.Count > 0)
                    {
                        Label8 = dtDescripcionVariableEnfoque5.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                DataTable dtDescripcionVariableEnfoque6 = objVarEnfoquesBL.ObtenerDescripcionVariableEnfoque(variableCausa4.Trim(), campaniaProcesada, periodo);
                if (dtDescripcionVariableEnfoque6 != null)
                {
                    if (dtDescripcionVariableEnfoque6.Rows.Count > 0)
                    {
                        Label9 = dtDescripcionVariableEnfoque6.Rows[0]["vchDesVariable"].ToString();
                    }
                }

                #endregion Cargar Variables de Enfoque

                string ubicacionRol = string.Empty;
                string lblDescripcionLugarRol1 = string.Empty;
                string lblDescripcionLugarRol = string.Empty;

                if (rolUsuarioEvaluado == Constantes.RolGerenteZona)
                    ubicacionRol = "Secciones";
                else
                    ubicacionRol = "Zonas";

                lblDescripcionLugarRol = lblDescripcionLugarRol1 = ubicacionRol;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "Detalle de las " + lblDescripcionLugarRol + " a trabajar y el plan de acción definido para las variables.";
                cell.MergeAcross = 5;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.Cells.Add("Variable", DataType.String, "s63");
                row.Cells.Add("Variables Causales", DataType.String, "s63");
                row.Cells.Add(lblDescripcionLugarRol1, DataType.String, "s63");
                row.Cells.Add("Plan de Acción", DataType.String, "s63");

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = true;

                int[] listaZona = new int[] { txtZonas1.Length, txtPlanAccion1.Length };
                int mayorZona = Mayor(listaZona);
                int tamanioZona = Convert.ToInt32(decimal.Floor((mayorZona / 45))) + 1;
                row.Height = tamanioZona * row.Height;

                row.Cells.Add(lblVariableGeneral1, DataType.String, "s74");
                row.Cells.Add(Label3, DataType.String, "s74");
                cell = row.Cells.Add(txtZonas1, DataType.String, "s74");
                cell.StyleID = "m76566056";
                cell.MergeDown = 1;
                cell = row.Cells.Add(txtPlanAccion1, DataType.String, "s74");
                cell.StyleID = "m76566076";
                cell.MergeDown = 1;

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = true;
                row.Cells.Add(lblEstadoIndicador1, DataType.String, "s75");
                row.Cells.Add(Label7, DataType.String, "s75");

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = true;

                listaZona = new int[] { txtZonas2.Length, txtPlanAccion2.Length };
                mayorZona = Mayor(listaZona);
                tamanioZona = Convert.ToInt32(decimal.Floor((mayorZona / 45))) + 1;
                row.Height = tamanioZona * row.Height;

                row.Cells.Add(lblVariableGeneral2, DataType.String, "s74");
                row.Cells.Add(Label8, DataType.String, "s74");
                cell = row.Cells.Add(txtZonas2, DataType.String, "s74");
                cell.StyleID = "m76566096";
                cell.MergeDown = 1;
                cell = row.Cells.Add(txtPlanAccion2, DataType.String, "s74");
                cell.StyleID = "m76566116";
                cell.MergeDown = 1;
                // -----------------------------------------------
                row = row.Table.Rows.Add();
                row.AutoFitHeight = true;
                row.Cells.Add(lblEstadoIndicador2, DataType.String, "s75");
                row.Cells.Add(Label9, DataType.String, "s75");

                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.MergeAcross = 5;
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = false;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "DURANTE Y DESPUES EQUIPOS";
                cell.MergeAcross = 5;

                //CARGAR EQUIPOS
                BeResumenProceso objResumenBE = new BeResumenProceso();
                objResumenBE.idProceso = idProceso;
                objResumenBE.codigoUsuario = codUsuarioEvaluado;
                objResumenBE.prefijoIsoPais = codPais;

                BlPlanAccion planAccionBL = new BlPlanAccion();
                BeUsuario usuario = new BeUsuario();
                usuario.prefijoIsoPais = codPais;

                List<BePlanAccion> procesados = planAccionBL.ObtenerCriticas(usuario, objResumenBE, periodo, rolUsuarioEvaluado);
                BlCritica criticasE = new BlCritica();

                foreach (BePlanAccion planAccion in procesados)
                {
                    BeCriticas criticaActual = criticasE.ObtenerCritica(idProceso, planAccion.DocuIdentidad);
                    planAccion.Variable = criticaActual.variableConsiderar;
                    planAccion.Estado = planAccion.PreDialogo ? "Completado" : "En Proceso";
                }

                if (procesados.Count > 0)
                {
                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    row.Cells.Add("Colaborador", DataType.String, "s63");
                    row.Cells.Add("Variable", DataType.String, "s63");
                    row.Cells.Add("Plan de Acción", DataType.String, "s63");
                    //if (EstadoProceso == "DESPUES")
                    row.Cells.Add("Estado", DataType.String, "s63");

                    foreach (BePlanAccion fila in procesados)
                    {
                        string nombreCritica = (fila.NombreCritica == null) ? string.Empty : fila.NombreCritica;
                        string variableE = (fila.Variable == null) ? string.Empty : fila.Variable;
                        string planAccion = (fila.PlanAccion == null) ? string.Empty : fila.PlanAccion;
                        string estado = (fila.Estado == null) ? string.Empty : fila.Estado;
                        // -----------------------------------------------
                        row = hoja.Table.Rows.Add();
                        row.AutoFitHeight = true;

                        int[] lista = new int[] { nombreCritica.Length, variableE.Length, planAccion.Length, estado.Length };
                        int mayor = Mayor(lista);
                        int tamanio = Convert.ToInt32(decimal.Floor((mayor / 45))) + 1;
                        row.Height = tamanio * row.Height;

                        row.Cells.Add(nombreCritica, DataType.String, "s64");
                        row.Cells.Add(variableE, DataType.String, "s66");
                        row.Cells.Add(planAccion, DataType.String, "s66");
                        //if (EstadoProceso == "DESPUES")
                        row.Cells.Add(estado, DataType.String, "s66");
                    }
                }

                //COMPETENCIAS
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                cell = row.Cells.Add();
                cell.StyleID = "s62";
                cell.MergeAcross = 5;
                // -----------------------------------------------
                row = hoja.Table.Rows.Add();
                row.AutoFitHeight = true;
                cell = row.Cells.Add();
                cell.StyleID = "s90";
                cell.Data.Type = DataType.String;
                cell.Data.Text = "DURANTE Y DESPUES COMPETENCIAS";
                cell.MergeAcross = 5;

                BeResumenProceso beReseumen = new BeResumenProceso();
                BlRetroalimentacion daProcesoE = new BlRetroalimentacion();
                bool lblEtiqueta = true;
                if (string.IsNullOrEmpty(periodo)) return;

                string anio = periodo.Substring(0, 4);
                beReseumen.codigoUsuario = codUsuarioEvaluado;
                beReseumen.prefijoIsoPais = codPais;

                DataTable dtCompetencia = daProcesoE.CargarCompetencia(CadenaConexion, beReseumen, anio);
                if (dtCompetencia != null)
                {
                    if (dtCompetencia.Rows.Count == 0)
                    {
                        lblEtiqueta = false;
                    }
                }

                DropDownList ddlCompetencia = new DropDownList();
                ddlCompetencia.DataSource = dtCompetencia;
                ddlCompetencia.DataTextField = "Competencia";
                ddlCompetencia.DataValueField = "CodigoPlanAnual";
                ddlCompetencia.DataBind();

                if (ddlCompetencia.Items.Count != 0)
                {
                    string codigoPlanAnual = ddlCompetencia.SelectedValue;

                    BlRetroalimentacion daProcesoEe = new BlRetroalimentacion();
                    BeRetroalimentacion beRetroalimentacion = new BeRetroalimentacion();
                    beRetroalimentacion.idProceso = idProceso;
                    beRetroalimentacion.CodigoPlanAnual = Convert.ToInt32(codigoPlanAnual);

                    DataTable resultadoPreguntas = daProcesoEe.ListarRetroalimentacion(CadenaConexion, beRetroalimentacion);

                    // -----------------------------------------------
                    row = hoja.Table.Rows.Add();
                    row.AutoFitHeight = false;
                    cell = row.Cells.Add();
                    cell.StyleID = "s62";
                    cell.Data.Type = DataType.String;
                    cell.Data.Text = "Retroalimentación dada a la Gerente de Región  sobre su plan de desarrollo.";
                    cell.MergeAcross = 5;

                    string lblEstadoIndicador3 = string.Empty;
                    string estadoProcesoRetroalimentacion = resultadoPreguntas.Rows[0].ItemArray[4].ToString();
                    string lblCompentencia = string.Empty;

                    if (!string.IsNullOrEmpty(estadoProcesoRetroalimentacion))
                        lblEstadoIndicador3 = bool.Parse(estadoProcesoRetroalimentacion.Trim()) ? " (Completado)" : " (En Proceso)";
                    else
                        lblEstadoIndicador3 = " (En Proceso)";

                    lblCompentencia = ddlCompetencia.SelectedItem.Text;
                    string textoCompleto = "";
                    // -----------------------------------------------
                    if (lblEtiqueta)
                        textoCompleto = "Competencia :";

                    row = hoja.Table.Rows.Add();
                    row.AutoFitHeight = false;
                    cell = row.Cells.Add();
                    cell.StyleID = "s89";
                    cell.Data.Type = DataType.String;
                    cell.Data.Text = textoCompleto + " " + lblCompentencia + " " + lblEstadoIndicador3;
                    cell.MergeAcross = 5;

                    if (resultadoPreguntas != null)
                    {
                        if (resultadoPreguntas.Rows.Count > 0)
                        {
                            // -----------------------------------------------
                            row = hoja.Table.Rows.Add();
                            row.Cells.Add("Pregunta", DataType.String, "s63");
                            row.Cells.Add("Respuesta", DataType.String, "s63");

                            foreach (DataRow fila in resultadoPreguntas.Rows)
                            {
                                string pregunta = fila["DescripcionPregunta"].ToString();
                                string respuesta = fila["Respuesta"].ToString();
                                // -----------------------------------------------
                                row = hoja.Table.Rows.Add();

                                int[] lista = new int[] { pregunta.Length, respuesta.Length };
                                int mayor = Mayor(lista);
                                int tamanio = Convert.ToInt32(decimal.Floor((mayor / 45))) + 1;
                                row.Height = tamanio * row.Height;

                                row.Cells.Add(pregunta, DataType.String, "s64");
                                row.Cells.Add(respuesta, DataType.String, "s64");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            hoja.Options.Selected = true;
            hoja.Options.ProtectObjects = false;
            hoja.Options.ProtectScenarios = false;
        }

        private string formatear(string variable)
        {
            string resultado = string.Empty;

            switch (variable)
            {
                case "True":
                    resultado = "SI";
                    break;
                case "False":
                    resultado = "NO";
                    break;
                default:
                    resultado = string.Empty;
                    break;
            }
            return resultado;
        }

        private int Mayor(int[] lista)
        {
            int mayor = int.MinValue;
            foreach (int item in lista)
            {
                if (item > mayor)
                    mayor = item;
            }
            return mayor;
        }
    }
}