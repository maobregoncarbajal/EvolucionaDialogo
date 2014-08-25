<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResumenAntes.aspx.cs" Inherits="Evoluciona.Dialogo.Web.PantallasModales.ResumenAntes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var imprimir = <%= Imprimir %>;

        jQuery(document).ready(function () { 
            if(imprimir == 1)
            {
                window.print();
            }
        });
    </script>

</head>
<body style="font-size: 9pt !important;">
    <form id="form1" runat="server">
    <div id="contentidoImprimir">
        <h3 style="text-align: center; font-family: Arial">
            Evaluado(a) :
            <asp:Literal Text="[Evaluado]" runat="server" ID="lblEvaluado" /></h3>
        <br />
        <div>
            <span style="text-decoration: underline; font-family: Arial"><strong>ANTES NEGOCIO :</strong></span>
            <br />
            <br />
            <table>
                <tr>
                    <td style="vertical-align: text-top">
                        <table width="520px">
                            <tr>
                                <td>
                                    <span class="subTituloMorado">Resultados de
                                        <%=descRol %>
                                        en las variables del negocio.</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdvVariablesBase" Width="500px" runat="server" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                        <RowStyle CssClass="grilla_indicadores" />
                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Variable de Enfoque">
                                                <ItemTemplate>
                                                    <asp:CheckBox Enabled="false" ID="CheckBox1" CssClass="clsChhIndicadores" name="CheckBox1"
                                                        runat="server" AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="variables">
                                    Variables Adicionales
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 166px">
                                    <asp:GridView ID="grdvVariablesAdicionales" Width="500px" runat="server" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                        <RowStyle CssClass="grilla_indicadores" />
                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Variable de Enfoque">
                                                <ItemTemplate>
                                                    <asp:CheckBox Enabled="false" ID="CheckBox2" name="CheckBox2" runat="server" AutoPostBack="false"
                                                        Checked='<%# Eval("bitEstado") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="502">
                                        <tr>
                                            <td>
                                                <span class="subTituloMorado">Variables Causales.</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 502px; text-align: left; border-style: solid" id="table1">
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td align="left" style="width: 100px">
                                                            <asp:Label ID="lblvariable1Desc" runat="server" Text="[Variable no Seleccionada]"
                                                                CssClass="variables_indicadores"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 110px">
                                                        </td>
                                                        <td align="center" style="width: 120px">
                                                        </td>
                                                        <td align="center" style="width: 120px">
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td style="width: 100px" align="left">
                                                            <asp:Label ID="Label1" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 110px">
                                                            <asp:Label ID="Label4" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 120px">
                                                            <asp:Label ID="Label5" runat="server" Text="Real" CssClass="variables"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 120px">
                                                            <asp:Label ID="Label6" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td style="width: 100px" align="center">
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
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td style="width: 100px" align="center">
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
                                                        <td colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td align="left" style="width: 100px; height: 21px">
                                                            <asp:Label ID="lblvariable2Desc" runat="server" Text="[Variable no Seleccionada]"
                                                                CssClass="variables_indicadores"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 110px; height: 21px">
                                                        </td>
                                                        <td align="center" style="width: 120px; height: 21px">
                                                        </td>
                                                        <td align="center" style="width: 120px; height: 21px">
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td style="height: 21px; width: 100px;" align="left">
                                                            <asp:Label ID="Label11" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                                                        </td>
                                                        <td style="height: 21px; width: 110px;" align="center">
                                                            <asp:Label ID="Label12" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                                                        </td>
                                                        <td style="height: 21px; width: 120px;" align="center">
                                                            <asp:Label ID="Label13" runat="server" Text="Real" CssClass="variables"></asp:Label>
                                                        </td>
                                                        <td style="height: 21px; width: 120px;" align="center">
                                                            <asp:Label ID="Label14" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td style="width: 100px" align="center">
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
                                                    <tr style="font-size: 9pt; font-family: Arial">
                                                        <td style="width: 100px; height: 26px" align="center">
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
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="dlstResumenCriticas" runat="server" RepeatDirection="Horizontal"
                                        Width="200px">
                                        <ItemStyle CssClass="grilla_indicadores" />
                                        <HeaderTemplate>
                                            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                                                <tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td>
                                                <img src="../Images/vinetablue.jpg" alt="" />
                                            </td>
                                            <td>
                                                <label runat="server" class="texto_morado_brand" id="lblTexto">
                                                    <%# Eval("vchEstadoPeriodo")%></label>
                                            </td>
                                            <td>
                                                <label runat="server" class="texto_morado_brand" id="Label1">
                                                    <%# Eval("%", "{0}%")%></label>
                                            </td>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tr></table>
                                        </FooterTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 10px;">
            <span style="text-decoration: underline; font-family: Arial"><strong>ANTES EQUIPOS :</strong></span>
            <br />
            <br />
            <div style="margin-left: 5px;">
                <asp:GridView ID="gvPersonasIngresadas" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" Width="500px">
                    <HeaderStyle Width="250px" CssClass="cabecera_indicadores" />
                    <RowStyle CssClass="grilla_indicadores" />
                    <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                    <Columns>
                        <asp:BoundField DataField="nombresCritica" HeaderText="Criticas"></asp:BoundField>
                        <asp:BoundField DataField="variableConsiderar" HeaderText="Variables a considerar">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div style="margin-top: 10px;">
            <span style="text-decoration: underline; font-family: Arial"><strong>ANTES COMPETENCIAS
                :</strong></span>
            <br />
            <br />
            <div style="margin-left: 5px;">
                <asp:GridView ID="gvPlanAnual" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Width="500px">
                    <HeaderStyle Width="250px" CssClass="cabecera_indicadores" />
                    <RowStyle CssClass="grilla_indicadores" />
                    <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                    <Columns>
                        <asp:BoundField DataField="Competencia" HeaderText="Competencia">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="comportamiento" HeaderText="Comportamiento"></asp:BoundField>
                        <asp:BoundField DataField="Sugerencia" HeaderText="Sugerencia">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <br />
                <span class="subtituloPlan">Observaciones :</span>
                <br />
                <br />
                <asp:Label runat="server" ID="lblObservacion" Font-Names="Arial" Font-Size="12px" />
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
