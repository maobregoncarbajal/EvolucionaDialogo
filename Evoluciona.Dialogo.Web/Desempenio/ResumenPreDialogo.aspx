<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResumenPreDialogo.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.ResumenPreDialogo" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />
    <link href="../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js" type="text/javascript"></script>
        <script type="text/javascript">

            var imprimir = '<%= Imprimir %>';
            var soloNegocio = '<%= SoloNegocio %>';

            jQuery(document).ready(function () {

                jQuery("#tabs").tabs();

                if (imprimir == 'SI') {
                    jQuery("#<%= btnImprimir.ClientID %>").trigger("click");
            }

            if (soloNegocio == 'SI') {
                jQuery("#liTab2").css("display", "none");
                jQuery("#liTab3").css("display", "none");
                jQuery("#tabs-2").css("display", "none");
                jQuery("#tabs-3").css("display", "none");
            }

        });

        function ExportDivDataToExcel() {
            var html = "<table style='width: 550px;'><tr><td>" + $("#divTitulo").html() + "</td></tr>";

            html += "<tr><td><br /><b>" + $("#divAntesNegocio").html() + "</b><br /></td></tr>";
            html += "<tr><td><br /><b>" + $("#divVariableNegocio").html() + "</b></td></tr>";
            html += "<tr><td><br />" + $("#divTablaVariablesNegocio").html() + "</td></tr>";
            html += "<tr><td><br /><b>" + $("#divVariablesAdicionales").html() + "</b></td></tr>";
            html += "<tr><td><br />" + $("#divTablaVariablesAdicionales").html() + "</td></tr>";
            html += "<tr><td><br /><b>" + $("#divVariablesCausales").html() + "</b></td></tr>";
            html += "<tr><td><br />" + $("#divTablaVariablesCausales").html() + "</td></tr>";
            html += "<tr><td><br /><b>" + $("#divAntesEquipo").html() + "</b><br /></td></tr>";
            html += "<tr><td><br />" + $("#divResumenCriticas").html() + "<br /></td></tr>";
            html += "<tr><td><br />" + $("#divPersonasIngresadas").html() + "</td></tr>";
            html += "<tr><td><br /><b>" + $("#divAntesCompetencias").html() + "</b><br /></td></tr>";
            html += "<tr><td><br />" + $("#divPlanAnual").html() + "</td></tr>";
            html += "<tr><td><br /><b>" + $("#divObservaciones").html() + "</b></td></tr>";
            html += "<tr><td><br />" + $("#divTextoObservaciones").html() + "<br /><br /></td></tr>";

            if (soloNegocio != 'SI') {

                html += "<tr><td><br /><b>DURANTE Y DESPUES</b><br /></td></tr>";
                html += "<tr><td><br /><b>DURANTE Y DESPUES NEGOCIO</b><br /></td></tr>";
                html += "<tr><td><br />" + $("#divTextoDDNegocio").html() + "</td></tr>";
                html += "<tr><td><br />" + $("#divTablaDDNegocio").html() + "</td></tr>";
                html += "<tr><td><br /><b>DURANTE Y DESPUES EQUIPOS</b><br /></td></tr>";
                html += "<tr><td><br />" + $(".divTablaDDEquipo").last().html() + "</td></tr>";
                html += "<tr><td><br /><b>DURANTE Y DESPUES COMPETENCIAS</b><br /></td></tr>";
                html += "<tr><td><br />" + $("#divTextoDDCompetencias1").html() + "</td></tr>";
                html += "<tr><td><br />" + $("#divTextoDDCompetencias2").html() + "</td></tr>";
                html += "<tr><td><br />" + $("#divTablaPreguntas").html() + "</td></tr>";
            }

            html += "</table>";

            $("#divOculto").html(html);
            $("#divOculto .imgOcultar").remove();

            html = $("#divOculto").html();
            html = $.trim(html);
            html = html.replace(/>/g, '&gt;');
            html = html.replace(/</g, '&lt;');
            $("#<%= hidTitulo.ClientID %>").val(html);
        }

        function modificarTexto() {
            jQuery('.columnaEnfoque').each(function () {
                if (this.innerText == "True")
                    this.innerText = 'SI';
                else
                    this.innerText = 'NO';
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divTitulo">
                <div style="text-align: left; font-family: Arial; float: left; width: 100%; font-size: 16px; font-weight: bold; color: #6a288a;">
                    <asp:Literal Text="[Evaluado]" runat="server" ID="lblEvaluado" />
                </div>
            </div>
            <div style="text-align: right; padding-right: 10px; padding-bottom: 5px; font-size: 10pt;">
                <asp:LinkButton ID="btnImprimir" runat="server" Text="Exportar MS Excel" OnClick="btnImprimir_Click"
                    OnClientClick="ExportDivDataToExcel()" />
            </div>
            <div id="tabs" style="font-size: 0.7em;">
                <ul>
                    <li id="liTab1"><a href="#tabs-1">ANTES</a></li>
                </ul>
                <div id="tabs-1">
                    <div style="text-align: left; font-size: 10pt;">
                        <table>
                            <tr>
                                <td>
                                    <div id="divAntesNegocio" style="font-size: 11pt; font-weight: bold;">
                                        ANTES NEGOGIO
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="divVariableNegocio" class="variables" style="font-weight: bold;">
                                                    Resultados en las variables del Negocio.
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divTablaVariablesNegocio">
                                                    <asp:GridView ID="grdvVariablesBase" Width="550px" runat="server" AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                                        <RowStyle CssClass="grilla_indicadores" />
                                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Variable de Enfoque">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" CssClass="columnaEnfoque" runat="server" Text='<%# Eval("bitEstado") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divVariablesAdicionales" class="variables" style="font-weight: bold;">
                                                    Variables Adicionales
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divTablaVariablesAdicionales">
                                                    <asp:GridView ID="grdvVariablesAdicionales" Width="550px" runat="server" AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                                        <RowStyle CssClass="grilla_indicadores" />
                                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Variable de Enfoque">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label7" CssClass="columnaEnfoque" runat="server" Text='<%# Eval("bitEstado") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divVariablesCausales" class="variables" style="font-weight: bold;">
                                                    Variables Causales
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divTablaVariablesCausales">
                                                    <table style="width: 550px; text-align: left; border-collapse: collapse;" border="1"
                                                        rules="all" cellspacing="0">
                                                        <tr style="font-size: 9pt; font-family: Arial">
                                                            <td align="left" colspan="4">
                                                                <asp:Label ID="lblvariable1Desc" runat="server" Text="[Variable no Seleccionada]"
                                                                    CssClass="variables_indicadores"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-weight: bold; font-family: Arial" class="cabecera_indicadores">
                                                            <td style="height: 21px; width: 100px" align="left">
                                                                <asp:Label ID="Label1" runat="server" Text="Variable Causa"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 110px">
                                                                <asp:Label ID="Label4" runat="server" Text="Objetivo"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 120px">
                                                                <asp:Label ID="Label5" runat="server" Text="Real"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 120px">
                                                                <asp:Label ID="Label6" runat="server" Text="Diferencia"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-family: Arial" class="grilla_indicadores">
                                                            <td style="height: 21px; width: 100px;" align="center">
                                                                <asp:Literal ID="ddlVariableCausa1" runat="server">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:Literal ID="txtVariable1PlanPeriodo" runat="server" />
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Literal ID="txtVariable1Real" runat="server" />
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Literal ID="txtVariable1Diferencia" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-family: Arial" class="grilla_alterna_indicadores">
                                                            <td style="height: 21px; width: 100px" align="center">
                                                                <asp:Literal ID="ddlVariableCausa2" runat="server">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:Literal ID="txtVariable2PlanPeriodo" runat="server" />
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Literal ID="txtVariable2Real" runat="server" />
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Literal ID="txtVariable2Diferencia" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-family: Arial">
                                                            <td align="left" style="height: 21px" colspan="4">
                                                                <asp:Label ID="lblvariable2Desc" runat="server" Text="[Variable no Seleccionada]"
                                                                    CssClass="variables_indicadores"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-weight: bold; font-family: Arial" class="cabecera_indicadores">
                                                            <td style="height: 21px; width: 100px;" align="left">
                                                                <asp:Label ID="Label11" runat="server" Text="Variable Causa"></asp:Label>
                                                            </td>
                                                            <td style="height: 21px; width: 110px;" align="center">
                                                                <asp:Label ID="Label12" runat="server" Text="Objetivo"></asp:Label>
                                                            </td>
                                                            <td style="height: 21px; width: 120px;" align="center">
                                                                <asp:Label ID="Label13" runat="server" Text="Real"></asp:Label>
                                                            </td>
                                                            <td style="height: 21px; width: 120px;" align="center">
                                                                <asp:Label ID="Label14" runat="server" Text="Diferencia"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-family: Arial" class="grilla_indicadores">
                                                            <td style="height: 21px; width: 100px" align="center">
                                                                <asp:Literal ID="ddlVariableCausa3" runat="server">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:Literal ID="txtVariable3PlanPeriodo" runat="server" />
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Literal ID="txtVariable3Real" runat="server" />
                                                            </td>
                                                            <td style="width: 120px" align="center">
                                                                <asp:Literal ID="txtVariable3Diferencia" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr style="font-size: 9pt; font-family: Arial" class="grilla_alterna_indicadores">
                                                            <td style="height: 21px; width: 100px; height: 26px" align="center">
                                                                <asp:Literal ID="ddlVariableCausa4" runat="server">
                                                                </asp:Literal>
                                                            </td>
                                                            <td style="width: 110px; height: 26px" align="center">
                                                                <asp:Literal ID="txtVariable4PlanPeriodo" runat="server" />
                                                            </td>
                                                            <td style="width: 120px; height: 26px" align="center">
                                                                <asp:Literal ID="txtVariable4Real" runat="server" />
                                                            </td>
                                                            <td style="width: 120px; height: 26px" align="center">
                                                                <asp:Literal ID="txtVariable4Diferencia" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divAntesEquipo" style="font-size: 11pt; font-weight: bold;">
                                        ANTES EQUIPOS
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divResumenCriticas">
                                        <asp:DataList ID="dlstResumenCriticas" runat="server" RepeatDirection="Horizontal"
                                            HorizontalAlign="Left">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <img class="imgOcultar" src="../Images/vinetablue.jpg" alt="" style="width: 10px; height: 10px;" />
                                                            <span class="texto_morado_brand" style="padding-right: 2px; padding-left: 2px;">
                                                                <%# Eval("vchEstadoPeriodo")%></span> <span class="texto_morado_brand">
                                                                    <%# Eval("%", "{0}%")%></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divPersonasIngresadas">
                                        <asp:GridView ID="gvPersonasIngresadas" runat="server" AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="cabecera_indicadores" />
                                            <RowStyle CssClass="grilla_indicadores" />
                                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                            <Columns>
                                                <asp:BoundField DataField="nombresCritica" HeaderText="Críticas">
                                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="variableConsiderar" HeaderText="Variables a considerar">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divAntesCompetencias" style="font-size: 11pt; font-weight: bold;">
                                        ANTES COMPETENCIAS
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="divPlanAnual">
                                                    <asp:GridView ID="gvPlanAnual" runat="server" AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                                        <RowStyle CssClass="grilla_indicadores" />
                                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Competencia" HeaderText="Competencia">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="comportamiento" HeaderText="Comportamiento">
                                                                <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Sugerencia" HeaderText="Sugerencia">
                                                                <ItemStyle HorizontalAlign="Center" Width="230px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divObservaciones" class="subtituloPlan">
                                                    Observaciones :
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divTextoObservaciones">
                                                    <asp:Label runat="server" ID="lblObservacion" Font-Names="Arial" Font-Size="12px"
                                                        Style="text-align: justify;" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidTitulo" runat="server" />
        <div id="divOculto" style="display: none;">
        </div>
    </form>
</body>
</html>
