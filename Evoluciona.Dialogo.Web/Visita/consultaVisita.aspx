<%@ Page Language="C#" Theme="TemaDDesempenio" AutoEventWireup="true" CodeBehind="consultaVisita.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.consultaVisita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta de las visitas</title>

    <script src="../Jscripts/jquery-1.6.2.js" type="text/javascript" language="javascript"></script>

    <script type="text/javascript" language="javascript">
        function MostrarVisita(codigoUsuario, idVisita) {
            var idRolUsuario = $('#hdIdRol').val();
            parent.ConsultaVisita(idVisita, codigoUsuario, idRolUsuario);
            parent.$.fn.colorbox.close();
        }
        function MostrarPostVisita(codigoUsuario, idVisita) {
            var idRolUsuario = $('#hdIdRol').val();
            parent.ConsultaPostVisita(idVisita, codigoUsuario, idRolUsuario);
            parent.$.fn.colorbox.close();
        }

    </script>

</head>
<body style="background-color: White">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img src="../Images/pop_up_VisitaRealizada.jpg" alt="" />
                </td>
                <td style="width: 20px">&nbsp;
                </td>
                <td>
                    <img src="../Images/pop_up_PostVisitaRealizada.jpg" alt="" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px" colspan='3'></td>
            </tr>
            <tr>
                <td>
                    <div style="width: 320px; border: solid 1px #c8c8c7;">
                        <asp:GridView ID="gviewVisita" runat="server" Width="310px" CellPadding="2" CellSpacing="4"
                            ForeColor="#778391" GridLines="None" AutoGenerateColumns="False">
                            <RowStyle CssClass="grilla_procesos" />
                            <HeaderStyle CssClass="cabecera_visitaResumen" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <a class='link' href='javascript:MostrarVisita("<%#Eval("chrCodigoUsuario") %>",<%#Eval("intIDVisita") %>);'>
                                            <%#Eval("correlativo")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="chrAnioCampana" HeaderText="Campaña">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="datFechaCrea" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td></td>
                <td>
                    <div style="width: 320px; border: solid 1px #c8c8c7;">
                        <asp:GridView ID="gviewPostVisita" runat="server" Width="310px" CellPadding="2" CellSpacing="4"
                            ForeColor="#778391" GridLines="None" AutoGenerateColumns="False">
                            <RowStyle CssClass="grilla_procesos" />
                            <HeaderStyle CssClass="cabecera_visitaResumen" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <a class='link' href='javascript:MostrarPostVisita("<%#Eval("chrCodigoUsuario") %>",<%#Eval("intIDVisita") %>);'>
                                            <%#Eval("correlativo")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="chrAnioCampana" HeaderText="Campaña">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="datFechaPostVisita" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hdIdRol" Value="" />
    </form>
</body>
</html>
