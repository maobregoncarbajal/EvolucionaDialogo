<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="Evoluciona.Dialogo.Web.error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fin de sesion</title>
</head>
<body>
    <form id="form1" runat="server">

        <table align="center">
            <tr>
                <td>
                    <img src="Images/consul1.jpg" alt="" /></td>
                <td align="center">
                    <asp:Label runat="server" ID="lblTituMensaje" Font-Bold="True" Font-Size="12px" Font-Names="Arial" ForeColor="#6a288a"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblTextMensaje" Font-Bold="True" Font-Size="12px" Font-Names="Arial" ForeColor="#6a288a"></asp:Label>
                    <br />
                    <br />
                    <asp:HyperLink ID="hlink" runat="server" Style="color: #00acee; font-weight: bold; font-family: Arial; font-size: 11px;"></asp:HyperLink>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
