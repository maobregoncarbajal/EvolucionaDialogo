<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptParticipacionTiempo.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Reportes.RptParticipacionTiempo" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Participación del Tiempo</title>
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
        <div>
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
                        <a href="javascript:window.print();" style="border: none; border-style: none;">
                            <img border="none" src="../Images/btnImprimir.png"></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr align="center">
                                <td>
                                    <h3>
                                        <asp:Label ID="Label1" runat="server">PARTICIPACIÓN DEL TIEMPO</asp:Label></h3>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Chart ID="Chart1" runat="server" Height="350px" Width="550px" OnClick="Chart1_Click"
                                        OnPostPaint="Chart1_PostPaint">
                                        <Series>
                                            <asp:Series ChartType="Pie" CustomProperties="PieDrawingStyle=SoftEdge" Legend="Legend1"
                                                Name="Series1" LabelForeColor="white" IsValueShownAsLabel="false">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <Area3DStyle Enable3D="True" />
                                            </asp:ChartArea>
                                        </ChartAreas>
                                        <Legends>
                                            <asp:Legend Alignment="Far" Docking="Top" Name="Legend1">
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin BackColor="BurlyWood" BackGradientStyle="HorizontalCenter" BackSecondaryColor="Blue"
                                            SkinStyle="Emboss" />
                                    </asp:Chart>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
