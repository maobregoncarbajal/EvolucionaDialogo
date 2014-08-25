<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioNoRegistrado.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.UsuarioNoRegistrado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Usuario No Registrado</title>
    <style type="text/css">
        .style1 {
            width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center">
                <tr>
                    <td>
                        <img src="Images/consul1.jpg" alt="" /></td>
                    <td align="justify" class="style1">
                        <asp:Label runat="server" ID="lblTituMensaje" Font-Bold="True" Font-Size="12px" Font-Names="Arial" ForeColor="#6a288a"></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="lblTextMensaje" Font-Bold="True" Font-Size="12px" Font-Names="Arial" ForeColor="#6a288a"></asp:Label>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hlLogin" runat="server" Style="color: #00acee; font-weight: bold; font-family: Arial; font-size: 11px;"></asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hlComptenecia" runat="server" Style="color: #00acee; font-weight: bold; font-family: Arial; font-size: 11px;"></asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
