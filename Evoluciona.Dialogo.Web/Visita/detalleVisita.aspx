<%@ Page Theme="TemaDDesempenio" Language="C#" AutoEventWireup="true" CodeBehind="detalleVisita.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.detalleVisita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Post-Diálogo</title>
    <script src="../Jscripts/jquery-1.6.2.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function MostrarVisita(codigoUsuario, idVisita, esPostVisita) {
            if (esPostVisita == "true") {
                var idRolUsuario = $('#hdIdRol').val();
                parent.IniciaPostVisita(idVisita, codigoUsuario, idRolUsuario);
                parent.$.fn.colorbox.close();
            }
        }
    </script>

</head>
<body style="background-color: White">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img src="../Images/pop_up_Post-Visita.jpg" alt="" /></td>
            </tr>
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td>
                    <div style="width: 400px; border: solid 1px #c8c8c7;">
                        <asp:GridView ID="gviewPostVisita" runat="server" Width="360px" CellPadding="2" CellSpacing="4" ForeColor="#778391" GridLines="None" AutoGenerateColumns="False">
                            <RowStyle CssClass="grilla_procesos" />
                            <HeaderStyle CssClass="cabecera_visitaResumen" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="160px">
                                    <ItemTemplate>
                                        <a class='link<%#Eval("esPostVisita")%>' href='javascript:MostrarVisita("<%#Eval("chrCodigoUsuario") %>",<%#Eval("intIDVisita") %>,"<%# Eval("esPostVisita").ToString()%>");'><%#Eval("correlativo")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="chrAnioCampana" HeaderText="Campaña">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="datFechaCrea" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
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
