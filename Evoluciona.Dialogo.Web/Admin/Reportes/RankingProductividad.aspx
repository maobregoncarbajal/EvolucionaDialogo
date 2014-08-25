<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="RankingProductividad.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Reportes.RankingProductividad" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/Admin/Controls/MenuReportes.ascx" TagName="menuReportes" TagPrefix="uc" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <asp:HiddenField ID="HdnValue" runat="server" />
    <br />
    <div id="panelUno">
        <table style="text-align: left; margin-top: 5px; margin-left: 5px; width: 100%">
            <tr>
                <td colspan="11" style="text-align: right">
                    <asp:Button ID="btnExportar" runat="server" Text="Excel" class="btnGuardarStyle"
                        OnClick="btnExportar_Click" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr style="height: 30px;">
                <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="11">
                    <center>
                        <b>Cuadro Resumen</b></center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr>
                <td>
                    <span class="texto_Negro">Pa&iacute;s :</span>
                </td>
                <td>
                    <asp:DropDownList ID="cboPaises" runat="server" Width="120px" CssClass="combo" OnSelectedIndexChanged="cboPaises_SelectedIndexChanged"
                        AutoPostBack="True" Style="margin-left: 7px;" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px;"></td>
                <td>
                    <span class="texto_Negro">Rol :</span>
                </td>
                <td>
                    <asp:DropDownList ID="cboRoles" runat="server" Width="150px" CssClass="combo" Style="margin-left: 7px;"
                        AutoPostBack="True" OnSelectedIndexChanged="cboRoles_SelectedIndexChanged" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px;"></td>
                <td>
                    <span class="texto_Negro">Per&iacute;odo :</span>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cboPeriodos" CssClass="combo" Width="100px"
                        AutoPostBack="true" OnSelectedIndexChanged="cboPeriodos_SelectedIndexChanged"
                        Style="margin-left: 7px;" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px;"></td>
                <td>
                    <span class="texto_Negro">Campañas :</span>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cboCampanhasFiltro" CssClass="combo" Width="100px"
                        Style="margin-left: 7px;" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr>
                <td colspan="11">
                    <center>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"
                            class="btnGuardarStyle" />
                        <asp:Button ID="btnBuscarAux" runat="server" Text="Button" Style="display: none"
                            OnClick="btnBuscarAux_Click" />
                    </center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr>
                <td colspan="5" style="vertical-align: top">
                    <div id="grillaUno">
                        <asp:GridView ID="gvResumen" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Width="100%"
                            AutoGenerateColumns="False" HorizontalAlign="Center" ShowFooter="True" OnRowCreated="gvResumen_RowCreated"
                            Visible="False">
                            <RowStyle BackColor="#CCC0DA" ForeColor="black" BorderColor="White" BorderStyle="Solid"
                                BorderWidth="1px" />
                            <Columns>
                                <asp:BoundField DataField="Periodo" HeaderText="Período">
                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Pais" HeaderText="País" ConvertEmptyStringToNull="true"
                                    HtmlEncode="false" HtmlEncodeFormatString="false">
                                    <ControlStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Width="120px" />
                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                        VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Colaboradores" HeaderText="Nº Colaboradores">
                                    <FooterStyle Width="80px" />
                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PorcentajeEstado1" HeaderText="PorcentajeEstado1" DataFormatString="{0:P2}"
                                    HtmlEncode="False">
                                    <FooterStyle Width="80px" />
                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                        VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PorcentajeEstado2" HeaderText="PorcentajeEstado2" DataFormatString="{0:P2}"
                                    HtmlEncode="False">
                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Width="80px" />
                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                        VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PorcentajeEstado3" HeaderText="PorcentajeEstado3" DataFormatString="{0:P2}"
                                    HtmlEncode="False">
                                    <FooterStyle Width="80px" />
                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                        VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#60497B" ForeColor="white" BorderColor="white" BorderWidth="1px"
                                HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerStyle BackColor="#CCC0DA" ForeColor="#60497B" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="black" />
                            <HeaderStyle BackColor="#60497B" Font-Bold="True" ForeColor="white" BorderColor="white" />
                            <AlternatingRowStyle BackColor="#CCC0DA" />
                        </asp:GridView>
                    </div>
                </td>
                <td colspan="6" valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Chart ID="ChartPeriodo" runat="server" Height="250px" Width="300px" BorderDashStyle="Solid"
                                    BorderWidth="2px" BorderColor="#1A3B69" Visible="False" EnableViewState="true">
                                    <Legends>
                                        <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default"
                                            Alignment="Center" Docking="Left" IsDockedInsideChartArea="False">
                                        </asp:Legend>
                                    </Legends>
                                    <Titles>
                                        <asp:Title Alignment="TopCenter" Font="Microsoft Sans Serif, 8pt, style=Bold" Name="Title1"
                                            Text="PERIODO">
                                            <Position Height="6.1912384" Width="60" X="40" />
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series Name="Estado1" ChartType="StackedColumn100" BorderColor="180, 26, 59, 105"
                                            Color="OliveDrab" ChartArea="ChartArea1" LegendText="Estado1">
                                        </asp:Series>
                                        <asp:Series Name="Estado2" ChartType="StackedColumn100" BorderColor="180, 26, 59, 105"
                                            Color="Yellow" ChartArea="ChartArea1" LegendText="Estado2">
                                        </asp:Series>
                                        <asp:Series Name="Estado3" ChartType="StackedColumn100" BorderColor="180, 26, 59, 105"
                                            Color="DarkRed" ChartArea="ChartArea1" LegendText="Estado3">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                            BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                                            BackGradientStyle="TopBottom">
                                            <AxisY LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            </AxisY>
                                            <AxisX LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            </AxisX>
                                            <Position Y="3" Height="92" Width="60" X="35"></Position>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                            <td>
                                <asp:Chart ID="ChartCampanha" runat="server" Height="250px" Width="300px" BorderDashStyle="Solid"
                                    BorderWidth="2px" BorderColor="#1A3B69" Visible="False" EnableViewState="true">
                                    <Legends>
                                        <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default"
                                            Alignment="Center" Docking="Left" IsDockedInsideChartArea="False" Enabled="False">
                                        </asp:Legend>
                                    </Legends>
                                    <Titles>
                                        <asp:Title Alignment="TopCenter" Font="Microsoft Sans Serif, 8pt, style=Bold" Name="Title1"
                                            Text="CAMPAÑA">
                                            <Position Height="6.1912384" Width="60" X="20" />
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series Name="Estado1" ChartType="StackedColumn100" BorderColor="180, 26, 59, 105"
                                            Color="OliveDrab" ChartArea="ChartArea1" LegendText="Estado1">
                                        </asp:Series>
                                        <asp:Series Name="Estado2" ChartType="StackedColumn100" BorderColor="180, 26, 59, 105"
                                            Color="Yellow" ChartArea="ChartArea1" LegendText="Estado2">
                                        </asp:Series>
                                        <asp:Series Name="Estado3" ChartType="StackedColumn100" BorderColor="180, 26, 59, 105"
                                            Color="DarkRed" ChartArea="ChartArea1" LegendText="Estado3">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                            BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                                            BackGradientStyle="TopBottom">
                                            <AxisY LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            </AxisY>
                                            <AxisX LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            </AxisX>
                                            <Position Y="3" Height="92" Width="92" X="2"></Position>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11">
                    <rsweb:ReportViewer ID="rpvResumen" runat="server" AsyncRendering="false" ExportContentDisposition="AlwaysAttachment"
                        Font-Names="Verdana" Font-Size="8pt" Height="400px" ShowExportControls="False"
                        ShowPrintButton="False" ShowRefreshButton="False" ShowToolBar="False" Width="400px"
                        Visible="false">
                        <LocalReport ReportPath="Informe\rptRankingProductividad.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </div>
    <div id="panelDos">
        <table style="text-align: left; margin-top: 5px; margin-left: 5px; width: 100%">
            <tr>
                <td style="color: White; background-color: #60497B" colspan="13">
                    <center>
                        Detalle<br />
                        campos Filtro</center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11" align="right">
                    <asp:Button ID="btnExportarExcelDetalle" runat="server" Text="Excel" OnClientClick="ExportDivDataToExcel()"
                        class="btnGuardarStyle" OnClick="btnExportarExcelDetalle_Click" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr>
                <td>
                    <span class="texto_Negro">País :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cboPaisesD" AutoPostBack="True" runat="server" Width="130px"
                        CssClass="combo" Style="margin-left: 7px;" OnSelectedIndexChanged="cboPaisesD_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Nivel Jerárquico :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cboRolesD" runat="server" Width="150px" CssClass="combo" Style="margin-left: 7px;"
                        AutoPostBack="True" OnSelectedIndexChanged="cboRolesD_SelectedIndexChanged" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Período :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList runat="server" ID="cboPeriodosD" CssClass="combo" Width="100px"
                        Style="margin-left: 7px;" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Cuadrante :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cboCuadranteD" runat="server" Width="175px" CssClass="combo" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr>
                <td>
                    <span class="texto_Negro">Región :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cboRegionD" runat="server" Width="175px" CssClass="combo" Style="margin-left: 7px;"
                        AutoPostBack="true" OnSelectedIndexChanged="cboRegionD_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Zona :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cboZonaD" runat="server" Width="140px" CssClass="combo" Style="margin-left: 7px;">
                    </asp:DropDownList>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Nombre
                        <br />
                        Colaborador :</span>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNombreColaboradorD" runat="server"></asp:TextBox>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Nombre Jefe :</span>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNombreJefeD" runat="server"></asp:TextBox>
                </td>
                <td style="width: 20px"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
            <tr>
                <td colspan="11">
                    <br />
                    <center>
                        <asp:Button ID="btnBuscarD" runat="server" ValidationGroup="Second" OnClick="btnBuscarD_Click"
                            class="btnBuscarStyle" /><asp:Button ID="btnBuscarDAux" runat="server" Text="Button"
                                OnClick="btnBuscarDAux_Click" Style="display: none" />
                    </center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="11"></td>
            </tr>
        </table>
        <div id="grillaDos">
            <asp:GridView ID="gvDetalle" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Width="150%"
                AutoGenerateColumns="False" HorizontalAlign="Center" ShowFooter="True" OnRowCreated="gvDetalle_RowCreated"
                OnRowDataBound="gvDetalle_RowDataBound">
                <RowStyle BackColor="#CCC0DA" ForeColor="black" BorderColor="White" BorderStyle="Solid"
                    BorderWidth="1px" />
                <FooterStyle BackColor="#60497B" ForeColor="white" BorderColor="white" BorderWidth="1px"
                    HorizontalAlign="Center" VerticalAlign="Middle" />
                <PagerStyle BackColor="#CCC0DA" ForeColor="#60497B" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="black" VerticalAlign="Middle"
                    HorizontalAlign="Center" />
                <HeaderStyle BackColor="#60497B" Font-Bold="True" ForeColor="white" BorderColor="white"
                    VerticalAlign="Middle" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="#CCC0DA" />
            </asp:GridView>
        </div>
    </div>
    <%--<div id="panelTres">
      <table>
        <tr>
         <td></td>
         <td></td>
        </tr>
        <tr>
         <td></td>
         <td></td>
        </tr>
      </table>
    </div>--%>
    <hr class="hrPunteado" />
    <div style="color: red; text-align: left">
        (*) Campo mínimo requerido para obtener resultados
    </div>
    <div id="mensajeAlertShow" style="display: none; height: 50px">
        <div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle" style="height: 20px; text-align: left; padding-left: 15px; padding-top: 5px;">
            Alerta
        </div>
        <div class="ui-widget-content ui-dialog-content" style="height: 30px; padding-top: 10px;">
            <span id="mensajeShow"></span>
        </div>
    </div>
    <input type="button" id="btnAlert" value="click" style="display: none" />
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />
    <div id="divExport" style="display: none">
        <table>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="26">
                    <center>
                        <p>
                            REPORTE:
                            <br />
                            Relación de colaboradores por cada status del Ranking de Productividad<br />
                            Objetivo: Analizar Status del Ranking
                        </p>
                    </center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="26"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="26">
                    <center>
                        Detalle<br />
                        campos Filtro</center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="26"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">País :</span>
                </td>
                <td style="text-align: left">
                    <span id="spPaisD"></span>
                </td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Nivel Jerárquico :</span>
                </td>
                <td style="text-align: left">
                    <span id="spNivelD"></span>
                </td>
                <td style="width: 20px"></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Período :</span>
                </td>
                <td style="text-align: left">
                    <span id="spPeriodoD"></span>
                </td>
                <td style="width: 20px"></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Cuadrante :</span>
                </td>
                <td style="text-align: left">
                    <span id="spCuadranteD"></span>
                </td>
                <td colspan="14"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="26"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Región :</span>
                </td>
                <td style="text-align: left">
                    <span id="spRegionD"></span>
                </td>
                <td style="width: 20px"></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Zona :</span>
                </td>
                <td style="text-align: left">
                    <span id="spZonaD"></span>
                </td>
                <td style="width: 20px"></td>
                <td style="width: 20px"></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Nombre
                        <br />
                        Colaborador :</span>
                </td>
                <td style="text-align: left">
                    <span id="spNombreColaboradorD"></span>
                </td>
                <td style="width: 20px"></td>
                <td style="color: White; background-color: #60497B">
                    <span class="texto_Negro">Nombre Jefe :</span>
                </td>
                <td style="text-align: left">
                    <span id="spNombreJefeD"></span>
                </td>
                <td style="width: 20px"></td>
                <td colspan="14"></td>
            </tr>
            <tr style="height: 20px">
                <td></td>
                <td colspan="25"></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="25">
                    <div id="grillaDosClone">
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var mensajeAlert = '';
        var tipoAccion = '';
        jQuery(document).ready(function () {

            document.onkeydown = function (evt) {
                return (evt ? evt.which : event.keyCode) != 13;
            };

            $("#mensajeShow").html('');
            $("#mensajeShow").append(mensajeAlert);
            $("#btnAlert").click(function (evt) {
                PosicionarVentana(tipoAccion);
                $.blockUI({
                    message: $('#mensajeAlertShow'),
                    css: {
                        height: '50px',
                        border: 'none'
                    }
                });

                $('.blockOverlay').attr('title', '').click($.unblockUI);
                $('.blockUI').attr('title', '').click($.unblockUI);
            });
        });

        function PosicionarVentana(tipo) {
            if (tipo == "1") {
                $(window).scrollTop($("#panelUno").position().top);
            }
            if (tipo == "2") {
                $(window).scrollTop($("#panelDos").position().top);
            }
        }

        function ProcessOpen(tipo) {

            jQuery.blockUI({
                message: jQuery("#imgExporting"),
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: 'transparent',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    color: '#fff'
                }
            });

            tipoAccion = tipo;

            if (tipo == "1") {

                $("#<%=btnBuscarAux.ClientID %>").trigger("click");
            }
            else {
                $("#<%=btnBuscarDAux.ClientID %>").trigger("click");
            }
        }

        function ProcessClose(tipo) {
            tipoAccion = tipo;
            PosicionarVentana(tipo);
            $.unblockUI({
                onUnblock: function () { }
            });
        }

        function Alerta(mensaje) {
            mensajeAlert = mensaje;
            $.unblockUI({
                onUnblock: function () { $('#btnAlert').trigger('click'); }
            });
        }

        function ExportDivDataToExcel() {
            var lista = ["spAnhoD", "spNombreColaboradorD", "spNivelD", "spZonaD",
            "spPaisD", "spNombreJefeD", "spCuadranteD", "spRegionD", , "grillaDosClone"];
            LimpiarVariablesReporteHtml(lista);

            $("#spPeriodoD").append($("#<%=cboPeriodosD.ClientID %> option:selected").text());
            $("#spNombreColaboradorD").append($("#<%=txtNombreColaboradorD.ClientID %>").val());
            $("#spNivelD").append($("#<%=cboRolesD.ClientID %> option:selected").text());
            $("#spZonaD").append($("#<%=cboZonaD.ClientID %> option:selected").text());
            $("#spPaisD").append($("#<%=cboPaisesD.ClientID %> option:selected").text());
            $("#spNombreJefeD").append($("#<%=txtNombreJefeD.ClientID %>").val());
            $("#spCuadranteD").append($("#<%=cboCuadranteD.ClientID %> option:selected").text());
            $("#spRegionD").append($("#<%=cboRegionD.ClientID %> option:selected").text());

            $('#grillaDos').clone().appendTo('#grillaDosClone');
            var html = $("#divExport").html();
            html = $.trim(html);
            html = html.replace(/>/g, '&gt;');
            html = html.replace(/</g, '&lt;');
            html = html.replace(/á/g, '&aacute;');
            html = html.replace(/é/g, '&eacute;');
            html = html.replace(/í/g, '&iacute;');
            html = html.replace(/ó/g, '&oacute;');
            html = html.replace(/ú/g, '&uacute;');
            html = html.replace(/ñ/g, '&ntilde;');

            html = html.replace(/Á/g, '&Aacute;');
            html = html.replace(/É/g, '&Eacute;');
            html = html.replace(/Í/g, '&Iacute;');
            html = html.replace(/Ó/g, '&Oacute;');
            html = html.replace(/Ú/g, '&Uacute;');

            html = html.replace(/Ñ/g, '&Ntilde;');

            $("input[id$='HdnValue']").val(html);
            $('#grillaDosClone').html("");
        }

        function LimpiarVariablesReporteHtml(args) {
            $.each(args, function (index, item) {
                $('#' + item).empty();
            });
        }

    </script>

</asp:Content>
