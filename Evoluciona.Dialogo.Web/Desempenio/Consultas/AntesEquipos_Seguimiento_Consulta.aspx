<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntesEquipos_Seguimiento_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.AntesEquipos_Seguimiento_Consulta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 20px 0 0 20px; text-align: left; width: 96%">
            <table style="text-align: left;">
                <tr>
                    <td align="left">
                        <span style="font-weight: bold;">
                            <asp:Literal ID="litNombre" runat="server"></asp:Literal>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <br />
                        <table style="width: 210px;">
                            <tr>
                                <td>Per&iacute;odo :
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboPeriodos" runat="server" Width="120px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboPeriodo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <span id="spanParticipacion" runat="server">% de Participación en la Venta de la
                        <asp:Literal ID="litEvaluacion" runat="server" /></span>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <br />
                        <asp:Panel ID="panNoExistenDatos" runat="server">
                            <asp:DataList ID="dlCampanhas" runat="server" RepeatDirection="Horizontal" RepeatColumns="6"
                                HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table style="text-align: center; padding: 5px; width: 82px;">
                                        <tr style="background-color: #CCC;">
                                            <th colspan="2" style="padding: 6px; font-weight: bold;">
                                                <asp:Literal ID="litCampanha" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "campania")%>' />
                                            </th>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                            <br />
                            <asp:Literal ID="litNoExitenDatos" runat="server">No se encontraron datos del periodo.</asp:Literal>
                        </asp:Panel>
                        <asp:Panel ID="panExisteDatos" runat="server">
                            <asp:DataList ID="dlCampanhasGZ" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
                                <ItemTemplate>
                                    <table style="text-align: center; padding: 5px; width: 82px;">
                                        <tr style="background-color: #CCC;">
                                            <th colspan="2" style="padding: 6px; font-weight: bold;">
                                                <asp:Literal ID="litCampanha" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "campania")%>' />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom-color: black; border-bottom-style: solid; border-bottom-width: 1px;">
                                                <div style="padding: 4px 2px 4px 4px;">
                                                    <asp:Literal ID="txtProcentaje" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Porcentaje", "{0:f2} %")%>' />
                                                </div>
                                            </td>
                                            <td style="padding: 5px;">
                                                <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "EstadoSeleccionado")%>'
                                                    Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:DataList ID="dlCampanhasLET" runat="server" RepeatDirection="Horizontal" RepeatColumns="6"
                                HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table style="text-align: center; padding: 5px; width: 60px;">
                                        <tr style="background-color: #CCC;">
                                            <th style="padding: 6px; font-weight: bold; width: 60px;">
                                                <asp:Literal ID="litCampanha" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "campania")%>' />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px 15px 5px 15px;">
                                                <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "EstadoSeleccionado")%>'
                                                    Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                            <br />
                            <table style="text-align: left; width: 100%">
                                <tr>
                                    <td style="width: 5px;">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Enabled="false" />
                                    </td>
                                    <td valign="top">
                                        <div style="font-size: 9pt;">
                                            : La campaña fue Crítica.
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
