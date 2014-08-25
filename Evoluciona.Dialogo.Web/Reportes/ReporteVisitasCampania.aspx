<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteVisitasCampania.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.ReporteVisitasCampaña" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporte de Visitas</title>
    <style type="text/css">
        body {
            font-weight: normal;
            font-style: normal;
            font-size: 9pt;
            font-family: Arial !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1" runat="server">
            <table>
                <tr>
                    <td>
                        <table width="100%" border='0' cellpadding='0'>
                            <tr>
                                <td>
                                    <img src="<%=Utils.RelativeWebRoot%>Images/titulo_evoluciona.jpg"
                                        alt="" align="bottom" width="200px" height="61px" /><br />
                                    <br />
                                    <div style="margin-left: 20px; font-weight: bold;">
                                        <span class="logeoBienvenida">¡Bienvenida(o)! &nbsp;&nbsp;</span>
                                        <asp:Label runat="server" ID="lblUserLogeado" Text="Usuario" CssClass="logeo"></asp:Label>
                                    </div>
                                    <div style="margin-left: 120px; font-size: 9pt;">
                                        <asp:Label Text="text" runat="server" ID="lblRolLogueado" CssClass="logeoRol" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <a href="javascript:window.print();" style="border: none; border-style: none;">
                                        <img border="none" src="../images/btnImprimir.png"></a>
                                </td>
                                <td>
                                    <asp:ImageButton runat="server" ID="btnDescargar" ImageUrl="../Images/btnDescargar.png"
                                        OnClick="btnDescargar_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton runat="server" ID="btnEmail" ImageUrl="../Images/btnEmail.png" OnClick="btnEmail_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <h3>Reporte de Visitas por Campa&ntilde;a Per&iacute;odo del
                                    <asp:Literal ID="litPeriodoTitulo" runat="server"></asp:Literal>
                                    </h3>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grResumenVisitas" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                        BorderWidth="0px" GridLines="None" CellPadding="0" CellSpacing="0">
                                        <Columns>
                                            <asp:TemplateField runat="server">
                                                <HeaderTemplate>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; background: #60497B; font-weight: bold; color: white;"
                                                        colspan="2">
                                                        <center>
                                                            <asp:Label ID="Label8" Text='Resumen de Visitas' runat="server"></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; font-weight: bold" colspan="2">
                                                        <center>
                                                            <asp:Label ID="Label21" Text='Nuevas' runat="server"></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; background: red; font-weight: bold"
                                                        colspan="2">
                                                        <center>
                                                            <asp:Label ID="Label22" Text='Criticas' runat="server"></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; background: yellow; font-weight: bold"
                                                        colspan="2">
                                                        <center>
                                                            <asp:Label ID="Label23" Text='Estables' runat="server"></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; background: green; font-weight: bold"
                                                        colspan="2">
                                                        <center>
                                                            <asp:Label ID="Label24" Text='Productivas' runat="server"></asp:Label></center>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; background: #60497B; color: white; font-weight: bold"
                                                        colspan="2">
                                                        <center>
                                                            <asp:Label ID="Label26" Text='TOTAL' runat="server"></asp:Label></center>
                                                    </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 150px; background: #D9D9D9">
                                                        <center>
                                                            <asp:Label ID="Label25" runat="server" Text='<%#Eval("texto") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px; background: #D9D9D9">
                                                        <center>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Imagen", GetUrl("../Images/{0}.jpg")) %>' /></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Nuevas") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("Porc1") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("Criticas") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label10" runat="server" Text='<%#Eval("Porc2") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Estables") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Porc3") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label11" runat="server" Text='<%#Eval("Productivas") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                        <center>
                                                            <asp:Label ID="Label12" runat="server" Text='<%#Eval("Porc4") %>'></asp:Label></center>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px; background: #D9D9D9">
                                                        <center>
                                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("Total") %>'></asp:Label></center>
                                                    </td>
                                                    <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px; background: #D9D9D9">
                                                        <center>
                                                            <asp:Label ID="Label27" runat="server" Text='<%#Eval("Porc5") %>'></asp:Label></center>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="nuevas" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h3>
                                            <asp:Label ID="lblNuevas" Visible="False" Text='Nuevas' runat="server"></asp:Label></h3>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="grNuevas" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                BorderWidth="0px" GridLines="None" CellPadding="0" CellSpacing="0"
                                OnRowDataBound="grNuevas_RowDataBound">
                                <Columns>
                                    <asp:TemplateField runat="server">
                                        <ItemStyle BorderWidth="0"></ItemStyle>
                                        <HeaderTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label8" Text='Zona' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label4" Text='GZ' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label5" Text='Variable01' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label6" Text='Variable02' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="12">
                                                <center>
                                                    <asp:Label ID="Label1" Text='Periodo' runat="server"></asp:Label></center>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td></td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C01</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C02</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C03</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C04</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C05</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C06
                                                </center>
                                            </td>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 34px">
                                                <center>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("CodZona") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 363px">
                                                <center>
                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("Descripcion") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("Variable1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("Variable2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("CampanaEstado1", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("EstadoCantidad1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("RankingCampana1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("CampanaEstado2", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("EstadoCantidad2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("RankingCampana2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("CampanaEstado3", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("EstadoCantidad3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("RankingCampana3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("CampanaEstado4", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("EstadoCantidad4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("RankingCampana4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image5" runat="server" ImageUrl='<%# Eval("CampanaEstado5", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("EstadoCantidad5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("RankingCampana5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image6" runat="server" ImageUrl='<%# Eval("CampanaEstado6", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("EstadoCantidad6") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label20" runat="server" Text='<%#Eval("RankingCampana6") %>'></asp:Label></center>
                                            </td>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:DataGrid ID="dgNuevas" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:BoundColumn runat="server" DataField="CodZona" HeaderText="CodZona" />
                                    <asp:BoundColumn runat="server" DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundColumn runat="server" DataField="Variable1" HeaderText="Variable1" />
                                    <asp:BoundColumn runat="server" DataField="Variable2" HeaderText="Variable2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado1" HeaderText="CampanaEstado1" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad1" HeaderText="EstadoCantidad1" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana1" HeaderText="RankingCampana1" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado2" HeaderText="CampanaEstado2" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad2" HeaderText="EstadoCantidad2" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana2" HeaderText="RankingCampana2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado3" HeaderText="CampanaEstado3" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad3" HeaderText="EstadoCantidad3" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana3" HeaderText="RankingCampana3" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado4" HeaderText="CampanaEstado4" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad4" HeaderText="EstadoCantidad4" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana4" HeaderText="RankingCampana4" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado5" HeaderText="CampanaEstado5" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad5" HeaderText="EstadoCantidad5" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana5" HeaderText="RankingCampana5" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado6" HeaderText="CampanaEstado6" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad6" HeaderText="EstadoCantidad6" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana6" HeaderText="RankingCampana6" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="criticas" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h3>
                                            <asp:Label ID="lblCriticas" Visible="False" Text='Críticas' runat="server"></asp:Label></h3>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="grCriticas" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                BorderWidth="0px" GridLines="None" CellPadding="0" CellSpacing="0"
                                OnRowDataBound="grNuevas_RowDataBound">
                                <Columns>
                                    <asp:TemplateField runat="server">
                                        <ItemStyle BorderWidth="0"></ItemStyle>
                                        <HeaderTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label8" Text='Zona' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label4" Text='GZ' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label5" Text='Variable01' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label6" Text='Variable02' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="12">
                                                <center>
                                                    <asp:Label ID="Label1" Text='Periodo' runat="server"></asp:Label></center>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td></td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold;"
                                                colspan="2">
                                                <center>
                                                    C01</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C02</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C03</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C04</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C05</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C06</center>
                                            </td>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 34px">
                                                <center>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("CodZona") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 363px">
                                                <center>
                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("Descripcion") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 101px">
                                                <center>
                                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("Variable1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("Variable2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("CampanaEstado1", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("EstadoCantidad1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("RankingCampana1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image7" runat="server" ImageUrl='<%# Eval("CampanaEstado2", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("EstadoCantidad2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("RankingCampana2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image8" runat="server" ImageUrl='<%# Eval("CampanaEstado3", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("EstadoCantidad3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("RankingCampana3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image9" runat="server" ImageUrl='<%# Eval("CampanaEstado4", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("EstadoCantidad4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("RankingCampana4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image10" runat="server" ImageUrl='<%# Eval("CampanaEstado5", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("EstadoCantidad5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("RankingCampana5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image11" runat="server" ImageUrl='<%# Eval("CampanaEstado6", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("EstadoCantidad6") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label20" runat="server" Text='<%#Eval("RankingCampana6") %>'></asp:Label></center>
                                            </td>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:DataGrid ID="dgCriticas" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:BoundColumn runat="server" DataField="CodZona" HeaderText="CodZona" />
                                    <asp:BoundColumn runat="server" DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundColumn runat="server" DataField="Variable1" HeaderText="Variable1" />
                                    <asp:BoundColumn runat="server" DataField="Variable2" HeaderText="Variable2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado1" HeaderText="CampanaEstado1" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad1" HeaderText="EstadoCantidad1" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana1" HeaderText="RankingCampana1" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado2" HeaderText="CampanaEstado2" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad2" HeaderText="EstadoCantidad2" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana2" HeaderText="RankingCampana2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado3" HeaderText="CampanaEstado3" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad3" HeaderText="EstadoCantidad3" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana3" HeaderText="RankingCampana3" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado4" HeaderText="CampanaEstado4" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad4" HeaderText="EstadoCantidad4" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana4" HeaderText="RankingCampana4" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado5" HeaderText="CampanaEstado5" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad5" HeaderText="EstadoCantidad5" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana5" HeaderText="RankingCampana5" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado6" HeaderText="CampanaEstado6" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad6" HeaderText="EstadoCantidad6" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana6" HeaderText="RankingCampana6" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="estables" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h3>
                                            <asp:Label ID="lblEstables" Visible="False" Text='Estables' runat="server"></asp:Label></h3>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="grEstables" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                BorderWidth="0px" GridLines="None" CellPadding="0" CellSpacing="0"
                                OnRowDataBound="grNuevas_RowDataBound">
                                <Columns>
                                    <asp:TemplateField runat="server">
                                        <ItemStyle BorderWidth="0"></ItemStyle>
                                        <HeaderTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label8" Text='Zona' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label4" Text='GZ' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label5" Text='Variable01' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label6" Text='Variable02' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="12">
                                                <center>
                                                    <asp:Label ID="Label1" Text='Periodo' runat="server"></asp:Label></center>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td></td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C01</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                C02
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C03</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C04</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C05</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C06</center>
                                            </td>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 34px">
                                                <center>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("CodZona") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 363px">
                                                <center>
                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("Descripcion") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("Variable1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("Variable2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("CampanaEstado1", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("EstadoCantidad1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("RankingCampana1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image12" runat="server" ImageUrl='<%# Eval("CampanaEstado2", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("EstadoCantidad2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("RankingCampana2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image13" runat="server" ImageUrl='<%# Eval("CampanaEstado3", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("EstadoCantidad3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("RankingCampana3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image14" runat="server" ImageUrl='<%# Eval("CampanaEstado4", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("EstadoCantidad4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("RankingCampana4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image15" runat="server" ImageUrl='<%# Eval("CampanaEstado5", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("EstadoCantidad5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("RankingCampana5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image16" runat="server" ImageUrl='<%# Eval("CampanaEstado6", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("EstadoCantidad6") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label20" runat="server" Text='<%#Eval("RankingCampana6") %>'></asp:Label></center>
                                            </td>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:DataGrid ID="dgEstables" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:BoundColumn runat="server" DataField="CodZona" HeaderText="CodZona" />
                                    <asp:BoundColumn runat="server" DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundColumn runat="server" DataField="Variable1" HeaderText="Variable1" />
                                    <asp:BoundColumn runat="server" DataField="Variable2" HeaderText="Variable2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado1" HeaderText="CampanaEstado1" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad1" HeaderText="EstadoCantidad1" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana1" HeaderText="RankingCampana1" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado2" HeaderText="CampanaEstado2" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad2" HeaderText="EstadoCantidad2" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana2" HeaderText="RankingCampana2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado3" HeaderText="CampanaEstado3" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad3" HeaderText="EstadoCantidad3" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana3" HeaderText="RankingCampana3" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado4" HeaderText="CampanaEstado4" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad4" HeaderText="EstadoCantidad4" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana4" HeaderText="RankingCampana4" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado5" HeaderText="CampanaEstado5" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad5" HeaderText="EstadoCantidad5" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana5" HeaderText="RankingCampana5" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado6" HeaderText="CampanaEstado6" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad6" HeaderText="EstadoCantidad6" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana6" HeaderText="RankingCampana6" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="productivas" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <h3>
                                            <asp:Label ID="lblProductivas" Visible="False" Text='Productivas' runat="server"></asp:Label></h3>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="grProductivas" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                BorderWidth="0px" GridLines="None" CellPadding="0" CellSpacing="0"
                                OnRowDataBound="grNuevas_RowDataBound">
                                <Columns>
                                    <asp:TemplateField runat="server">
                                        <ItemStyle BorderWidth="0"></ItemStyle>
                                        <HeaderTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label8" Text='Zona' runat="server"></asp:Label>
                                                </center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label4" Text='GZ' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label5" Text='Variable01' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                rowspan="2">
                                                <center>
                                                    <asp:Label ID="Label6" Text='Variable02' runat="server"></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="12">
                                                <center>
                                                    <asp:Label ID="Label1" Text='Periodo' runat="server"></asp:Label></center>
                                            </td>
                                            </tr>
                                        <tr>
                                            <td></td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C01</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C02</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C03</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C04</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C05</center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; background: #D9D9D9; font-weight: bold"
                                                colspan="2">
                                                <center>
                                                    C06</center>
                                            </td>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 34px">
                                                <center>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("CodZona") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 363px">
                                                <center>
                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("Descripcion") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("Variable1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 100px">
                                                <center>
                                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("Variable2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("CampanaEstado1", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("EstadoCantidad1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("RankingCampana1") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image17" runat="server" ImageUrl='<%# Eval("CampanaEstado2", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("EstadoCantidad2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <asp:Label ID="Label12" runat="server" Text='<%#Eval("RankingCampana2") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image18" runat="server" ImageUrl='<%# Eval("CampanaEstado3", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("EstadoCantidad3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("RankingCampana3") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image19" runat="server" ImageUrl='<%# Eval("CampanaEstado4", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("EstadoCantidad4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("RankingCampana4") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image20" runat="server" ImageUrl='<%# Eval("CampanaEstado5", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("EstadoCantidad5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("RankingCampana5") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 50px">
                                                <center>
                                                    <asp:Image ID="Image21" runat="server" ImageUrl='<%# Eval("CampanaEstado6", GetUrl("../Images/{0}.jpg")) %>' />
                                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("EstadoCantidad6") %>'></asp:Label></center>
                                            </td>
                                            <td style="border-color: Black; border-style: solid; border-width: 1px; width: 30px">
                                                <center>
                                                    <asp:Label ID="Label20" runat="server" Text='<%#Eval("RankingCampana6") %>'></asp:Label>
                                                </center>
                                            </td>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:DataGrid ID="dgProductivas" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:BoundColumn runat="server" DataField="CodZona" HeaderText="CodZona" />
                                    <asp:BoundColumn runat="server" DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundColumn runat="server" DataField="Variable1" HeaderText="Variable1" />
                                    <asp:BoundColumn runat="server" DataField="Variable2" HeaderText="Variable2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado1" HeaderText="CampanaEstado1" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad1" HeaderText="EstadoCantidad1" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana1" HeaderText="RankingCampana1" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado2" HeaderText="CampanaEstado2" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad2" HeaderText="EstadoCantidad2" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana2" HeaderText="RankingCampana2" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado3" HeaderText="CampanaEstado3" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad3" HeaderText="EstadoCantidad3" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana3" HeaderText="RankingCampana3" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado4" HeaderText="CampanaEstado4" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad4" HeaderText="EstadoCantidad4" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana4" HeaderText="RankingCampana4" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado5" HeaderText="CampanaEstado5" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad5" HeaderText="EstadoCantidad5" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana5" HeaderText="RankingCampana5" />
                                    <asp:BoundColumn runat="server" DataField="CampanaEstado6" HeaderText="CampanaEstado6" />
                                    <asp:BoundColumn runat="server" DataField="EstadoCantidad6" HeaderText="EstadoCantidad6" />
                                    <asp:BoundColumn runat="server" DataField="RankingCampana6" HeaderText="RankingCampana6" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:DataGrid runat="server" ID="dgResumen" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundColumn runat="server" DataField="texto" HeaderText="Resumen" />
                    <asp:BoundColumn runat="server" DataField="Imagen" HeaderText="Imagen" />
                    <asp:BoundColumn runat="server" DataField="Nuevas" HeaderText="Nuevas" />
                    <asp:BoundColumn runat="server" DataField="Porc1" HeaderText="Porc1" />
                    <asp:BoundColumn runat="server" DataField="Criticas" HeaderText="Criticas" />
                    <asp:BoundColumn runat="server" DataField="Porc2" HeaderText="Porc2" />
                    <asp:BoundColumn runat="server" DataField="Estables" HeaderText="Estables" />
                    <asp:BoundColumn runat="server" DataField="Porc3" HeaderText="Porc3" />
                    <asp:BoundColumn runat="server" DataField="Productivas" HeaderText="Productivas" />
                    <asp:BoundColumn runat="server" DataField="Porc4" HeaderText="Porc4" />
                    <asp:BoundColumn runat="server" DataField="Total" HeaderText="Total" />
                    <asp:BoundColumn runat="server" DataField="Porc5" HeaderText="Porc5" />
                </Columns>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
