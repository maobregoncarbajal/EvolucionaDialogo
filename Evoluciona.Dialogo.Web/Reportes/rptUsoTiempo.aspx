<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptUsoTiempo.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Reportes.rptUsoTiempo" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Uso del Tiempo</title>
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
            <asp:DataGrid ID="dgReuniones" runat="server" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundColumn runat="server" DataField="TipoReunion" HeaderText="TipoReunion" />
                    <asp:BoundColumn runat="server" DataField="Actividad" HeaderText="Actividad" />
                    <asp:BoundColumn runat="server" DataField="Cantidad1" HeaderText="Cantidad1" />
                    <asp:BoundColumn runat="server" DataField="Cantidad2" HeaderText="Cantidad2" />
                    <asp:BoundColumn runat="server" DataField="Cantidad3" HeaderText="Cantidad3" />
                    <asp:BoundColumn runat="server" DataField="Cantidad4" HeaderText="Cantidad4" />
                    <asp:BoundColumn runat="server" DataField="Cantidad5" HeaderText="Cantidad5" />
                    <asp:BoundColumn runat="server" DataField="Cantidad6" HeaderText="Cantidad6" />
                    <asp:BoundColumn runat="server" DataField="Total1" HeaderText="Total1" />
                    <asp:BoundColumn runat="server" DataField="Porcentaje1" HeaderText="Porcentaje1" />
                    <asp:BoundColumn runat="server" DataField="Cantidad7" HeaderText="Cantidad7" />
                    <asp:BoundColumn runat="server" DataField="Cantidad8" HeaderText="Cantidad8" />
                    <asp:BoundColumn runat="server" DataField="Cantidad9" HeaderText="Cantidad9" />
                    <asp:BoundColumn runat="server" DataField="Cantidad10" HeaderText="Cantidad10" />
                    <asp:BoundColumn runat="server" DataField="Cantidad11" HeaderText="Cantidad11" />
                    <asp:BoundColumn runat="server" DataField="Cantidad12" HeaderText="Cantidad12" />
                    <asp:BoundColumn runat="server" DataField="Total2" HeaderText="Total2" />
                    <asp:BoundColumn runat="server" DataField="Porcentaje2" HeaderText="Porcentaje2" />
                    <asp:BoundColumn runat="server" DataField="Cantidad13" HeaderText="Cantidad13" />
                    <asp:BoundColumn runat="server" DataField="Cantidad14" HeaderText="Cantidad14" />
                    <asp:BoundColumn runat="server" DataField="Cantidad15" HeaderText="Cantidad15" />
                    <asp:BoundColumn runat="server" DataField="Cantidad16" HeaderText="Cantidad16" />
                    <asp:BoundColumn runat="server" DataField="Cantidad17" HeaderText="Cantidad17" />
                    <asp:BoundColumn runat="server" DataField="Cantidad18" HeaderText="Cantidad18" />
                    <asp:BoundColumn runat="server" DataField="Total3" HeaderText="Total3" />
                    <asp:BoundColumn runat="server" DataField="Porcentaje3" HeaderText="Porcentaje3" />
                </Columns>
            </asp:DataGrid>
            <asp:DataGrid ID="dgTotales" runat="server" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundColumn runat="server" DataField="CantCampania1" HeaderText="CantCampania1" />
                    <asp:BoundColumn runat="server" DataField="CantCampania2" HeaderText="CantCampania2" />
                    <asp:BoundColumn runat="server" DataField="CantCampania3" HeaderText="CantCampania3" />
                    <asp:BoundColumn runat="server" DataField="CantCampania4" HeaderText="CantCampania4" />
                    <asp:BoundColumn runat="server" DataField="CantCampania5" HeaderText="CantCampania5" />
                    <asp:BoundColumn runat="server" DataField="CantCampania6" HeaderText="CantCampania6" />
                    <asp:BoundColumn runat="server" DataField="TotalGeneral1" HeaderText="TotalGeneral1" />
                    <asp:BoundColumn runat="server" DataField="PorcGeneral1" HeaderText="PorcGeneral1" />
                    <asp:BoundColumn runat="server" DataField="CantCampania7" HeaderText="CantCampania7" />
                    <asp:BoundColumn runat="server" DataField="CantCampania8" HeaderText="CantCampania8" />
                    <asp:BoundColumn runat="server" DataField="CantCampania9" HeaderText="CantCampania9" />
                    <asp:BoundColumn runat="server" DataField="CantCampania10" HeaderText="CantCampania10" />
                    <asp:BoundColumn runat="server" DataField="CantCampania11" HeaderText="CantCampania11" />
                    <asp:BoundColumn runat="server" DataField="CantCampania12" HeaderText="CantCampania12" />
                    <asp:BoundColumn runat="server" DataField="TotalGeneral2" HeaderText="TotalGeneral2" />
                    <asp:BoundColumn runat="server" DataField="PorcGeneral2" HeaderText="PorcGeneral2" />
                    <asp:BoundColumn runat="server" DataField="CantCampania13" HeaderText="CantCampania13" />
                    <asp:BoundColumn runat="server" DataField="CantCampania14" HeaderText="CantCampania14" />
                    <asp:BoundColumn runat="server" DataField="CantCampania15" HeaderText="CantCampania15" />
                    <asp:BoundColumn runat="server" DataField="CantCampania16" HeaderText="CantCampania16" />
                    <asp:BoundColumn runat="server" DataField="CantCampania17" HeaderText="CantCampania17" />
                    <asp:BoundColumn runat="server" DataField="CantCampania18" HeaderText="CantCampania18" />
                    <asp:BoundColumn runat="server" DataField="TotalGeneral3" HeaderText="TotalGeneral3" />
                    <asp:BoundColumn runat="server" DataField="PorcGeneral3" HeaderText="PorcGeneral3" />
                </Columns>
            </asp:DataGrid>
            <table style="min-width: 900px;">
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
                        <table width="100%" style="margin-left: auto; margin-right: auto;">
                            <tr>
                                <td align="center">
                                    <h3>REPORTE DEL USO DE TIEMPO</h3>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                    <br />
                                    <asp:Literal runat="server" ID="ltReuniones"></asp:Literal>
                                    <asp:Literal runat="server" ID="ltTotales"></asp:Literal>
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
