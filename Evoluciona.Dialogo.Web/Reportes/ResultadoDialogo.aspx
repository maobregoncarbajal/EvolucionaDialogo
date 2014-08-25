<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ResultadoDialogo.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Reportes.ResultadoDialogo" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/MenuReportes.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div style="margin-left: 80px">
        <uc:menuReportes ID="menuReporte" runat="server" />
    </div>
    <br />
    <br />
    <asp:HiddenField ID="HdnValue" runat="server" />
    <div style="margin-left: 80px">
        <div id="panelUno">
            <table style="text-align: left; margin-top: 5px; margin-left: 5px; width: 100%">
                <tr>
                    <td colspan="71" style="text-align: right">
                        <asp:Button ID="btnExportar" runat="server" Text="Excel" OnClientClick="ExportDivDataToExcel()"
                            class="btnGuardarStyle" OnClick="btnExportar_Click" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="71">
                        <center>
                            <b>Cuadro Resumen :</b></center>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr>
                    <td>
                        <span class="texto_Negro">País :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlPaises" runat="server" Width="175px" CssClass="combo" Style="margin-left: 7px;"
                            OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="True" />
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <span class="texto_Negro">Nivel Jerárquico :</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRoles" runat="server" Width="150px" CssClass="combo" Style="margin-left: 7px;"
                            OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td colspan="66"></td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr style="height: 20px">
                    <td></td>
                    <td colspan="5">
                        <fieldset>
                            <legend><span class="texto_Negro">Tipo Comparación:</span></legend>
                            <table style="margin-top: 10px; margin-bottom: 10px;">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rbListResumen" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbListResumen_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="Anho">Año</asp:ListItem>
                                            <asp:ListItem Value="Periodo">Período</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <span class="texto_Negro">Año : </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAnho" runat="server" Width="100" MaxLength="4" ValidationGroup="First"></asp:TextBox>
                                        <span style="color: red"></span>
                                        <br />
                                        <asp:RangeValidator ID="rvAnho" runat="server" ControlToValidate="txtAnho" ErrorMessage="Ingrese un año valido"
                                            MaximumValue="9999" MinimumValue="1900" ValidationGroup="First" Type="Integer"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td>
                                        <span class="texto_Negro">Período Actual :</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPeriodoActual" runat="server" Enabled="False" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlPeriodoActual_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <span class="texto_Negro">Período Anterior :</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodoAnterior" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td colspan="69">
                                        <div style="color: red; text-align: left">
                                            <asp:Label ID="lblMensaje" runat="server" Text="lblMensaje" Visible="false"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td colspan="65"></td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr>
                    <td colspan="71">
                        <center>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btnGuardarStyle" OnClick="btnBuscar_Click"
                                ValidationGroup="First" /></center>
                        <asp:Button ID="btnBuscarAux" runat="server" Text="Button" Style="display: none"
                            OnClick="btnBuscarAux_Click" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr>
                    <td colspan="71" style="vertical-align: top">
                        <div id="grillaUno">
                            <asp:GridView ID="gvResumen" runat="server" BackColor="White" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Width="100%"
                                AutoGenerateColumns="False" HorizontalAlign="Center" ShowFooter="True" OnRowCreated="gvResumen_RowCreated"
                                OnRowDataBound="gvResumen_RowDataBound">
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
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="71">
                        <center>
                            <b>Cuadro Resumen :</b></center>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
            </table>
            <div id="grillaDos" style="width: 300%">
                <asp:GridView ID="gvResumenPais" runat="server" BackColor="White" BorderColor="White"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                    ShowFooter="True" OnRowCreated="gvResumenPais_RowCreated" OnRowDataBound="gvResumen_RowDataBound">
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
        <div id="panelDos">
            <table style="text-align: left; margin-top: 5px; margin-left: 5px; width: 100%">
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="71">
                        <center>
                            <b>Detalle<br />
                                Campos Filtro </b>
                        </center>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr>
                    <td>
                        <span class="texto_Negro">Pais :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlPaisesD" runat="server" Width="140px" CssClass="combo" AutoPostBack="true"
                            Style="margin-left: 7px;" OnSelectedIndexChanged="ddlPaisesD_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <span class="texto_Negro">Nivel Jerárquico :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlRolesD" runat="server" Width="175px" CssClass="combo" AutoPostBack="true"
                            Style="margin-left: 7px;" OnSelectedIndexChanged="ddlPaisesD_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <span class="texto_Negro">Período :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList runat="server" ID="ddlPeriodosD" CssClass="combo" Width="100px"
                            Style="margin-left: 7px;" AppendDataBoundItems="True" />
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <span class="texto_Negro">Variable :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlVariableD" runat="server" Width="175px" CssClass="combo" />
                    </td>
                    <td colspan="60"></td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr>
                    <td>
                        <span class="texto_Negro">Región :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlRegionD" runat="server" Width="100px" CssClass="combo" Style="margin-left: 7px;"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlRegionD_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <span class="texto_Negro">Zona :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlZonaD" runat="server" Width="140px" CssClass="combo" Style="margin-left: 7px;">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <span class="texto_Negro">Tamaño de
                            <br />
                            Brecha(%) :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtBrechaTamD" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: left"></td>
                    <td colspan="60"></td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="71"></td>
                </tr>
                <tr>
                    <td colspan="71">
                        <br />
                        <center>
                            <asp:Button ID="btnBuscarD" runat="server" ValidationGroup="Second" class="btnBuscarStyle"
                                OnClick="btnBuscarD_Click" />
                            <asp:Button ID="btnBuscarDAux" runat="server" Text="Button" Style="display: none"
                                OnClick="btnBuscarDAux_Click" />
                        </center>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div id="grillaTres" style="width: 1000%; float: left; text-align: left">
                <asp:GridView ID="gvDetalle" runat="server" BackColor="White" BorderColor="White"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                    HorizontalAlign="left" ShowFooter="True" OnRowCreated="gvDetalle_RowCreated"
                    OnRowDataBound="gvResumen_RowDataBound">
                    <RowStyle BackColor="#CCC0DA" ForeColor="black" BorderColor="White" BorderStyle="Solid"
                        BorderWidth="1px" Wrap="true" />
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
    </div>
    <hr class="hrPunteado" />
    <div style="color: red; text-align: left; padding-left: 80px">
        (*) Campo mínimo requerido para obtener resultados
    </div>
    <div id="divExport" style="display: none">
        <table style="margin-left: 20px">
            <tr>
                <td style="color: White; background-color: #60497B" colspan="14">
                    <center>
                        <p>
                            REPORTE:<br />
                            Resultados del Diálogo<br />
                            Objetivo: Analizar los resultados de los paises en el negocio
                        </p>
                    </center>
                </td>
                <td style="color: White; background-color: #60497B" colspan="60"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="14">
                    <center>
                        Cuadro Resumen :</center>
                </td>
                <td style="color: White; background-color: #60497B" colspan="60"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">País
                </td>
                <td style="text-align: left">
                    <span id="spPais"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nivel Jerárquico
                </td>
                <td style="text-align: left">
                    <span id="spNivel"></span>
                </td>
                <td colspan="69"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">Año
                </td>
                <td style="text-align: left">
                    <span id="spAnho"></span>
                </td>
                <td colspan="71"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">Periodo Actual
                </td>
                <td style="text-align: left">
                    <span id="spPeriodoActual"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Periodo Anterior
                </td>
                <td style="text-align: left">
                    <span id="spPeriodoAnterior"></span>
                </td>
                <td colspan="70"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="1"></td>
                <td colspan="72">
                    <div id="grillaUnoClone">
                    </div>
                </td>
                <td></td>
            </tr>
        </table>
        <table>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="14">
                    <center>
                        Cuadro Resumen :</center>
                </td>
                <td style="color: White; background-color: #60497B" colspan="60"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
        </table>
        <table>
            <tr>
                <td></td>
                <td colspan="71">
                    <div id="grillaDosClone">
                    </div>
                </td>
                <td colspan="2"></td>
            </tr>
        </table>
        <table>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="14">
                    <center>
                        Detalle<br />
                        Campos filtro</center>
                </td>
                <td style="color: White; background-color: #60497B" colspan="60"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td colspan="1"></td>
                <td style="color: White; background-color: #60497B">País :
                </td>
                <td style="text-align: left">
                    <span id="spPaisD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nivel Jerárquico :
                </td>
                <td style="text-align: left">
                    <span id="spNivelD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Período :
                </td>
                <td style="text-align: left">
                    <span id="spPeriodoD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Variable :
                </td>
                <td style="text-align: left">
                    <span id="spVariableD"></span>
                </td>
                <td colspan="62"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td colspan="1"></td>
                <td style="color: White; background-color: #60497B">Región :
                </td>
                <td style="text-align: left">
                    <span id="spRegionD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Zona :
                </td>
                <td style="text-align: left">
                    <span id="spZonaD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Tamaño Brecha :
                </td>
                <td style="text-align: left">
                    <span id="spTamBrechaD"></span>
                </td>
                <td colspan="65"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
        </table>
        <table>
            <tr>
                <td></td>
                <td colspan="72">
                    <div id="grillaTresClone">
                    </div>
                </td>
                <td></td>
            </tr>
        </table>
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

    <script type="text/javascript">
        var mensajeAlert = '';
        var tipoAccion = '';
        jQuery(document).ready(function () {
            document.onkeydown = function (evt) {
                return (evt ? evt.which : event.keyCode) != 13;
            };
            $("#<%=txtAnho.ClientID %>").numeric();
            $("#<%=txtBrechaTamD.ClientID %>").numeric();
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
            else {
                $(window).scrollTop($("#panelDos").position().top);
            }
        }

        function ExportDivDataToExcel() {
            var lista = ["spPais", "spNivel", "spAnho", "spPeriodoActual", "spPeriodoAnterior", "spPaisD", "spNivelD",
                        "spPeriodoD", "spVariableD", "spRegionD", "spZonaD", "spTamBrechaD", "grillaUnoClone", "grillaDosClone", "grillaTresClone"];
            LimpiarVariablesReporteHtml(lista);

            $("#spPais").append($("#<%=ddlPaises.ClientID %> option:selected").text());
            $("#spNivel").append($("#<%=ddlRoles.ClientID %> option:selected").text());
            $("#spAnho").append($("#<%=txtAnho.ClientID %>").val());

            if ($("#<%=ddlPeriodoActual.ClientID %>").val() != '-Seleccione-') {
                $("#spPeriodoActual").append($("#<%=ddlPeriodoActual.ClientID %> option:selected").text());
            }

            $("#spPeriodoAnterior").append($("#<%=txtPeriodoAnterior.ClientID %>").val());

            $("#spPaisD").append($("#<%=ddlPaisesD.ClientID %> option:selected").text());
            $("#spNivelD").append($("#<%=ddlRolesD.ClientID %> option:selected").text());
            $("#spPeriodoD").append($("#<%=ddlPeriodosD.ClientID %> option:selected").text());
            $("#spVariableD").append($("#<%=ddlVariableD.ClientID %> option:selected").text());
            $("#spRegionD").append($("#<%=ddlRegionD.ClientID %> option:selected").text());
            $("#spZonaD").append($("#<%=ddlZonaD.ClientID %> option:selected").text());
            $("#spVariableD").append($("#<%=ddlVariableD.ClientID %> option:selected").text());
            $("#spTamBrechaD").append($("#<%=txtBrechaTamD.ClientID %>").val());

            $('#grillaUno').clone().appendTo('#grillaUnoClone');
            $('#grillaDos').clone().appendTo('#grillaDosClone');
            $('#grillaTres').clone().appendTo('#grillaTresClone');

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
        }

        function LimpiarVariablesReporteHtml(args) {
            $.each(args, function (index, item) {
                $('#' + item).empty();
            });
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

            if (tipo == "1") {
                $("#<%=btnBuscarAux.ClientID %>").trigger("click");
            } else {
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

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
