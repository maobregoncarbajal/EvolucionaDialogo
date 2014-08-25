<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Evoluciona.Dialogo.Web._Default" %>

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
                    <img src="Images/consul1.jpg" alt="" /></td>
                <td align="center">
                    <asp:Label runat="server" ID="lblMensaje" Text="No existe el usuario, por favor vuelva a iniciar sesión" Font-Bold="True" Font-Size="Medium"></asp:Label><br />
                    <br />
                    <a href="javascript:close();" style="color: #00acee; font-weight: bold">Cerrar la aplicación</a></td>
            </tr>
        </table>
    </form>
</body>
</html>
