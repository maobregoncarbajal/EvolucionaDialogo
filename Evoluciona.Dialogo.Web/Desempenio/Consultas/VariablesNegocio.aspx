<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VariablesNegocio.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.VariablesNegocio" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.window.css" rel="stylesheet" type="text/css" />
    <link href="../../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#<%=gvVariables.ClientID %> input:checkbox").attr("disabled", "disabled");
            jQuery("#<%=gvVariablesAdicionales.ClientID %> input:checkbox").attr("disabled", "disabled");

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <br />
                    <span class="texto_Negro"></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblCabecera" runat="server" CssClass="tituloSeguimiento" Width="100%" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="texto_Negro"></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvVariables" Width="700px" runat="server" AutoGenerateColumns="False">
                        <EmptyDataTemplate>
                            <table width="750px">
                                <tr class="cabecera_indicadores">
                                    <th style="width: 180px;">Variables</th>
                                    <th style="width: 90px;">Meta</th>
                                    <th style="width: 90px;">Resultado</th>
                                    <th style="width: 90px;">Diferencia</th>
                                    <th style="width: 90px;">(%)</th>
                                    <th style="width: 120px;">Campa&#241;a</th>
                                </tr>
                                <tr class="grilla_indicadores">
                                    <td colspan="6" align="center" style="font-size: 14pt; color: #0caed7; font-style: italic">No se encontraron resultados para la búsqueda actual...
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="cabecera_indicadores" />
                        <RowStyle CssClass="grilla_indicadores" />
                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Porcentaje" DataFormatString="{0:N}%" HeaderText="(%)">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Variable de Enfoque">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbEstado" CssClass="clsChhIndicadores" name="CheckBox1" runat="server" AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="gvVariablesAdicionales" Width="700px" runat="server" AutoGenerateColumns="False">
                        <EmptyDataTemplate>
                            <table width="750px">
                                <tr class="cabecera_indicadores">
                                    <th style="width: 180px;">Variables</th>
                                    <th style="width: 90px;">Meta</th>
                                    <th style="width: 90px;">Resultado</th>
                                    <th style="width: 90px;">Diferencia</th>
                                    <th style="width: 90px;">(%)</th>
                                    <th style="width: 120px;">Campa&#241;a</th>
                                </tr>
                                <tr class="grilla_indicadores">
                                    <td colspan="6" align="center" style="font-size: 14pt; color: #0caed7; font-style: italic">No se encontraron resultados para la búsqueda actual...
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="cabecera_indicadores" />
                        <RowStyle CssClass="grilla_indicadores" />
                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Porcentaje" DataFormatString="{0:N}%" HeaderText="(%)">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Variable de Enfoque">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbEstado" CssClass="clsChhIndicadores" name="CheckBox1" runat="server" AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
