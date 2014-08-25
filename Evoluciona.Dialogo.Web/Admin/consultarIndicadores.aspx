<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="consultarIndicadores.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Admin.consultarIndicadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar Indicadores</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>Rol:</td>
                <td>
                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="combo" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                        <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                        <asp:ListItem Value="5">Gerente de Region</asp:ListItem>
                        <asp:ListItem Value="6">Gerente de Zona</asp:ListItem>

                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>País:</td>
                <td>
                    <asp:DropDownList ID="ddlPaises" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>Campaña</td>
                <td>
                    <asp:DropDownList ID="ddlCampana" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>Gerente Region</td>
                <td>
                    <asp:DropDownList ID="ddlGR" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ddlGR_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Gerente Zona</td>
                <td>
                    <asp:DropDownList ID="ddlGZ" runat="server" Width="350px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" /></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvVariables" runat="server" Width="750px" AutoGenerateColumns="False">
                        <EmptyDataTemplate>
                            <table width="700px">
                                <tr class="cabecera_indicadores">
                                    <td>Variables
                                    </td>
                                    <td>Meta
                                    </td>
                                    <td>Resultado
                                    </td>
                                    <td>Diferencia
                                    </td>
                                    <td>Campa&#241;a
                                    </td>
                                </tr>
                                <tr class="grilla_indicadores">
                                    <td colspan="5" align="center" style="font-size: 14pt; color: #0caed7; font-style: italic">No se encontraron resultados para la búsqueda actual...
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

                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="gvVariablesA" runat="server" AutoGenerateColumns="False" Width="750px">
                        <EmptyDataTemplate>
                            <table width="700px">
                                <tr class="cabecera_indicadores">
                                    <td>Variables
                                    </td>
                                    <td>Meta
                                    </td>
                                    <td>Resultado
                                    </td>
                                    <td>Diferencia
                                    </td>
                                    <td>Campa&#241;a
                                    </td>
                                </tr>
                                <tr class="grilla_indicadores">
                                    <td align="center" colspan="5" style="font-size: 14pt; color: #0caed7; font-style: italic">No se encontraron resultados para la búsqueda actual...
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
                                    <asp:Label ID="Label2" runat="server" Style="display: none" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="vchDesVariable" HeaderText="Variables">
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="decValorPlanPeriodo" DataFormatString="{0:N}" HeaderText="Meta">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="decValorRealPeriodo" DataFormatString="{0:N}" HeaderText="Resultado">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Diferencia" DataFormatString="{0:N}" HeaderText="Diferencia">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="chrAnioCampana" HeaderText="Campa&#241;a">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
