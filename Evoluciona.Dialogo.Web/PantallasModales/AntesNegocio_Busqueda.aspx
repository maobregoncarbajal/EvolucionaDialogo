<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntesNegocio_Busqueda.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.PantallasModales.AntesNegocio_Busqueda" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/general.css?v3" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js"
        type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <table width="85%" align="center" style="border: solid 1px #c8c8c7; margin-left: auto; margin-right: auto;">
                                        <tr>
                                            <td colspan="3" class="subtitulo">
                                                <div style="text-align: center;">Selecciona el periodo y campaña para visualizar los resultados de las variables.</div>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3" style="height: 23px">&nbsp;&nbsp;
                                            <asp:Label ID="lblTextoIndicadores" runat="server" Text="Seleccione un Tipo de Filtro "
                                                Width="650px" CssClass="texto_descripciones"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <div id="FiltroIndicadores" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 159px; height: 11px; padding-left: 10px;" align="left">
                                                                <asp:RadioButton ID="radioPeriodo" Text="Por Período" Checked="true" GroupName="indicador"
                                                                    runat="server" AutoPostBack="true" OnCheckedChanged="radioPeriodo_CheckedChanged"
                                                                    CssClass="radio_indicadores" />
                                                            </td>
                                                            <td align="left" style="width: 517px; height: 11px;">
                                                                <asp:DropDownList ID="ddlperiodo" runat="server" Width="120px" OnSelectedIndexChanged="ddlperiodo_SelectedIndexChanged"
                                                                    AutoPostBack="True" CssClass="combo">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 159px; height: 10px"></td>
                                                            <td align="left" style="width: 517px; height: 10px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100px; height: 24px; padding-left: 10px;">
                                                                <asp:RadioButton ID="radioCampana" Text="Por Campaña" GroupName="indicador" runat="server"
                                                                    AutoPostBack="true" OnCheckedChanged="radioCampana_CheckedChanged" CssClass="radio_indicadores" />
                                                            </td>
                                                            <td align="left" style="width: 517px; height: 24px;">
                                                                <asp:Label ID="lblCampanadesde" runat="server" Text="Desde : " Width="70px" CssClass="radio_indicadores"></asp:Label>
                                                                <asp:DropDownList ID="ddlCampanadesde" runat="server" Width="120px" CssClass="combo"
                                                                    OnSelectedIndexChanged="ddlCampanadesde_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblCampanahasta" runat="server" Text="Hasta : " CssClass="radio_indicadores"
                                                                    Width="70px"></asp:Label>
                                                                <asp:DropDownList ID="ddlCampanahasta" runat="server" Width="120px" CssClass="combo"
                                                                    OnSelectedIndexChanged="ddlCampanahasta_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="font-size: 12pt; font-family: Arial">
                                            <td colspan="3"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt; font-family: Arial">
                                <td align="center" style="height: 15px">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div class="texto_Negro">
                            <asp:Literal ID="litMensajeResultado" runat="server" /></div>
                        <br />
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial;">
                    <td align="center">
                        <asp:GridView ID="GridView0" Width="700px" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="cabecera_indicadores" />
                            <RowStyle CssClass="grilla_indicadores" />
                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Variables" DataField="vchDesVariable" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Porcentaje" DataFormatString="{0:F2} %" HeaderText="(%)">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Campaña" DataField="chrAnioCampana" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="margin-right: 150px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp;
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td align="center">
                        <asp:GridView ID="GridView1" Width="700px" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="cabecera_indicadores" />
                            <RowStyle CssClass="grilla_indicadores" />
                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Variables" DataField="vchDesVariable" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Porcentaje" DataFormatString="{0:F2} %" HeaderText="(%)">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Campaña" DataField="chrAnioCampana" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td align="center"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
