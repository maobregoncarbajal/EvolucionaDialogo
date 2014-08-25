<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="PlanAccionVsResultadoNegocio.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Reportes.PlanAccionVsResultadoNegocio" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuReportes.ascx" TagName="menuReportes" TagPrefix="uc" %>
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
    <br />
    <br />
    <asp:HiddenField ID="HdnValue" runat="server" />
    <div id="panelUno">
        <table style="text-align: left; margin-top: 5px; margin-left: 5px;">
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
                    <asp:DropDownList ID="ddlPais" runat="server" Width="175px" CssClass="combo" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Nivel
                        <br />
                        Jerárquico :</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRoles" runat="server" Width="150px" CssClass="combo" Style="margin-left: 7px;"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Ranking<br />
                        Productividad :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlRanking" runat="server" Width="175px" CssClass="combo" />
                </td>
                <td colspan="62"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="71"></td>
            </tr>
            <tr>
                <td>
                    <span class="texto_Negro">Variable :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlVariable" runat="server" Width="175px" CssClass="combo" />
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Rango
                        <br />
                        Cumplimiento % :</span>
                </td>
                <td style="text-align: left">
                    <table style="width: 100%; margin-top: 30px">
                        <tr>
                            <td style="width: 20px">
                                <asp:TextBox ID="txtRangoInicio" Width="40px" runat="server"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="reRangoInicio" runat="server" ControlToValidate="txtRangoInicio"
                                    ValidationExpression="^[+-]?(?:\d+\.?\d*|\d*\.?\d+)[\r\n]*$" ErrorMessage="Número no permitido"
                                    ValidationGroup="First" />
                            </td>
                            <td style="text-align: left; width: 20px">
                                <asp:TextBox ID="txtRangoFin" runat="server" Width="40px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="reRangoFiin" runat="server" ControlToValidate="txtRangoFin"
                                    ValidationExpression="^[+-]?(?:\d+\.?\d*|\d*\.?\d+)[\r\n]*$" ErrorMessage="Número no permitido"
                                    ValidationGroup="First" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="6"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="71"></td>
            </tr>
            <tr style="height: 20px">
                <td></td>
                <td colspan="6">
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
                                    <asp:RangeValidator ID="rfvAnho" runat="server" ControlToValidate="txtAnho" ErrorMessage="Ingrese un año valido"
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
                <td colspan="64"></td>
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
        </table>
        <br />
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
    </div>
    <br />
    <div id="panelDos">
        <table style="text-align: left; margin-top: 5px; margin-left: 5px;">
            <tr style="height: 30px;">
                <td style="color: White; background-color: #60497B" colspan="71">
                    <center>
                        Detalle<br />
                        campos Filtro</center>
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
                    <asp:DropDownList ID="ddlPaisesD" runat="server" Width="130px" CssClass="combo" Style="margin-left: 7px;"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlRolesD_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Nivel Jerárquico :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlRolesD" runat="server" Width="150px" CssClass="combo" Style="margin-left: 7px;"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlRolesD_SelectedIndexChanged" />
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
                    <span class="texto_Negro">Sub periodo:</span>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSubPeriodo2" Width="125px" CssClass="combo"
                        Style="margin-left: 7px;">
                        <asp:ListItem Value="">Todos</asp:ListItem>
                        <asp:ListItem Value="0">Periodo completo</asp:ListItem>
                        <asp:ListItem Value="1">Sub periodo I</asp:ListItem>
                        <asp:ListItem Value="2">Sub periodo II</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="60"></td>
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
                    <asp:DropDownList ID="ddlZonaD" runat="server" Width="100px" CssClass="combo" Style="margin-left: 7px;">
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
                <td colspan="60"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="71"></td>
            </tr>
            <tr>
                <td>
                    <span class="texto_Negro">Ranking
                        <br />
                        Productividad :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlRankingD" runat="server" Width="100px" CssClass="combo" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">% Cumplimiento<br />
                        en variable (>) :</span>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCumplimientoD" runat="server"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="reCumplimineto" runat="server" ControlToValidate="txtCumplimientoD"
                        ValidationExpression="^[+-]?(?:\d+\.?\d*|\d*\.?\d+)[\r\n]*$" ErrorMessage="Número decimal no permitido"
                        ValidationGroup="Second" />
                </td>
                <td style="width: 20px"></td>
                <td>
                    <span class="texto_Negro">Enfoque :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlEnfoqueD" runat="server" Width="175px" CssClass="combo" />
                    <span style="color: red">&nbsp;(*)</span>
                </td>
                <td colspan="63"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="71"></td>
            </tr>
            <tr>
                <td colspan="71">
                    <br />
                    <center>
                        <asp:Button ID="btnBuscarD" runat="server" ValidationGroup="Second" OnClick="btnBuscarD_Click"
                            class="btnBuscarStyle" />
                        <asp:Button ID="btnBuscarDAux" runat="server" Text="Button" Style="display: none"
                            OnClick="btnBuscarDAux_Click" />
                    </center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="71"></td>
            </tr>
            <tr>
                <td colspan="71">
                    <br />
                    <center>
                        <div id="grillaDos">
                            <asp:GridView ID="gvDetalle" runat="server" BackColor="White" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Width="100%"
                                AutoGenerateColumns="False" HorizontalAlign="Center" ShowFooter="True" OnRowCreated="gvDetalle_RowCreated"
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
                    </center>
                </td>
            </tr>
        </table>
    </div>
    <hr class="hrPunteado" />
    <div style="color: red; text-align: left">
        (*) Campo mínimo requerido para obtener resultados
    </div>
    <div id="divExport" style="display: none">
        <table style="margin-left: 20px">
            <tr>
                <td style="color: White; background-color: #60497B" colspan="74">
                    <center>
                        <p>
                            REPORTE:<br />
                            Planes de acción vs. Resultados de negocio
                            <br />
                            Objetivo: Analizar la eficacia de los planes de acción llevados a cabo<br />
                        </p>
                    </center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="74">
                    <center>
                        Cuadro Resumen :</center>
                </td>
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
                <td style="color: White; background-color: #60497B">Nivel<br />
                    Jerárquico
                </td>
                <td style="text-align: left">
                    <span id="spNivel"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Ranking<br />
                    Productividad
                </td>
                <td style="text-align: left">
                    <span id="spRanking"></span>
                </td>
                <td colspan="64"></td>
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
                <td style="color: White; background-color: #60497B">Variable:
                </td>
                <td style="text-align: left">
                    <span id="spVariable"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Rango de %<br />
                    cumplimiento
                </td>
                <td style="text-align: left">
                    <span id="spRango"></span>
                </td>
                <td colspan="68"></td>
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
                <td></td>
                <td colspan="73">
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
                <td style="color: White; background-color: #60497B" colspan="74">
                    <center>
                        Detalle<br />
                        Campos filtro</center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">País
                </td>
                <td style="text-align: left">
                    <span id="spPaisD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nivel<br />
                    Jerárquico:
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
                <td colspan="65"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td></td>
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
                <td style="color: White; background-color: #60497B">Nombre<br />
                    Colaborador:
                </td>
                <td style="text-align: left">
                    <span id="spNombreColaboradorD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nombre Jefe:
                </td>
                <td></td>
                <td style="text-align: left">
                    <span id="spNombreJefeD"></span>
                </td>
                <td colspan="61"></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="74"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">Ranking<br />
                    productividad:
                </td>
                <td style="text-align: left">
                    <span id="spRankingD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">% de cumplimiento<br />
                    en variable
                </td>
                <td style="text-align: left">
                    <span id="spVariableD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Enfoque :
                </td>
                <td style="text-align: left">
                    <span id="spEnfoqueD"></span>
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
                <td colspan="73">
                    <center>
                        <div id="grillaDosClone">
                        </div>
                    </center>
                </td>
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
            $("#<%=txtRangoInicio.ClientID %>").numeric({ allow: "-." });
            $("#<%=txtRangoFin.ClientID %>").numeric({ allow: "-." });
            $("#<%=txtCumplimientoD.ClientID %>").numeric({ allow: "-." });
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
            var lista = ["spAnho", "spPais", "spNivel", "spRanking", "spVariable", "spRango",
            "spPeriodoActual", "spPeriodoAnterior", "spPaisD", "spNivelD", "spPeriodoD", "spRegionD",
            "spZonaD", "spNombreColaboradorD", "spNombreJefeD", "spRankingD", "spVariableD", "spEnfoqueD", "grillaUnoClone", "grillaDosClone"];
            LimpiarVariablesReporteHtml(lista);

            $("#spPais").append($("#<%=ddlPais.ClientID %> option:selected").text());
            $("#spNivel").append($("#<%=ddlRoles.ClientID %> option:selected").text());
            $("#spRanking").append($("#<%=ddlRanking.ClientID %> option:selected").text());
            $("#spVariable").append($("#<%=ddlVariable.ClientID %> option:selected").text());
            $("#spRango").append($("#<%=txtRangoInicio.ClientID %>").val() + " - " + $("#<%=txtRangoFin.ClientID %>").val());

            $("#spAnho").append($("#<%=txtAnho.ClientID %>").val());

            if ($("#<%=ddlPeriodoActual.ClientID %>").val() != '-Seleccione-') {
                $("#spPeriodoActual").append($("#<%=ddlPeriodoActual.ClientID %> option:selected").text());
            }

            $("#spPeriodoAnterior").append($("#<%=txtPeriodoAnterior.ClientID %>").val());

            $("#spPaisD").append($("#<%=ddlPaisesD.ClientID %> option:selected").text());
            $("#spNivelD").append($("#<%=ddlRolesD.ClientID %> option:selected").text());
            $("#spPeriodoD").append($("#<%=ddlPeriodosD.ClientID %> option:selected").text());
            $("#spRegionD").append($("#<%=ddlRegionD.ClientID %> option:selected").text());
            $("#spZonaD").append($("#<%=ddlZonaD.ClientID %> option:selected").text());

            $("#spNombreColaboradorD").append($("#<%=txtNombreColaboradorD.ClientID %>").val());
            $("#spNombreJefeD").append($("#<%=txtNombreJefeD.ClientID %>").val());
            $("#spRankingD").append($("#<%=ddlRankingD.ClientID %> option:selected").text());
            $("#spVariableD").append($("#<%=txtCumplimientoD.ClientID %>").val());
            $("#spEnfoqueD").append($("#<%=ddlEnfoqueD.ClientID %> option:selected").text());

            $('#grillaUno').clone().appendTo('#grillaUnoClone');
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
