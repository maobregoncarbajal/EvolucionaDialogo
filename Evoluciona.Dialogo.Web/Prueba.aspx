<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Prueba" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCUB" runat="server" Text="CUB"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCUB" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCUBEncriptado" runat="server" Text="CUB Encriptado"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCUBEncriptado" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblToken" runat="server" Text="Token"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtToken" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPais" runat="server" Text="Pais"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlPais" runat="server">
                        <asp:ListItem Value="AR">Argentina</asp:ListItem>
                        <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                        <asp:ListItem Value="BR">Brasil</asp:ListItem>
                        <asp:ListItem Value="CL">Chile</asp:ListItem>
                        <asp:ListItem Value="CO">Colombia</asp:ListItem>
                        <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                        <asp:ListItem Value="DO">Dominicana</asp:ListItem>
                        <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                        <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                        <asp:ListItem Value="MX">Mexico</asp:ListItem>
                        <asp:ListItem Value="PA">Panama</asp:ListItem>
                        <asp:ListItem Value="PE">Perú</asp:ListItem>
                        <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                        <asp:ListItem Value="SV">Salvador</asp:ListItem>
                        <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModulo" runat="server" Text="Modulo"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlModulo" runat="server">
                        <asp:ListItem Value="validacion">Validacion</asp:ListItem>
                        <asp:ListItem Value="validaciond.aspx">Dialogos</asp:ListItem>
                        <asp:ListItem Value="validacionv.aspx">Visitas</asp:ListItem>
                        <asp:ListItem Value="validacionm">Matriz</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnURL" runat="server" Text="URL LOGIN EVOLUCIONA"
                        OnClick="btnURL_Click" /></td>
                <td>
                    <asp:TextBox ID="txtURL" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="LOGIN"
                        OnClick="btnLogin_Click" /></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
