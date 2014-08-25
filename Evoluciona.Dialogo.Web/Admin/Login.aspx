<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%">
            <table cellspacing="5" style="margin-left: auto; margin-right: auto;">
                <tr>
                    <th colspan="3" style="text-align: center;">
                        <span style="font-weight: bold; font-size: 12pt;">ADMINISTRAR</span>
                    </th>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>C&oacute;digo :
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsuario" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario"
                            ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Contrase&ntilde;a :
                    </td>
                    <td>
                        <asp:TextBox ID="txtContrasenia" runat="server" Width="200px" MaxLength="20" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContrasenia"
                            ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Tipo Administrador :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTipoAdmin" runat="server" AutoPostBack="true" Width="150px"
                            OnSelectedIndexChanged="cboTipoAdmin_SelectedIndexChanged">
                            <asp:ListItem Value="C">Coorporativo</asp:ListItem>
                            <asp:ListItem Value="P">Pais</asp:ListItem>
                            <asp:ListItem Value="E">Evoluciona</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Pais :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPais" runat="server" Width="150px" Enabled="false">
                            <asp:ListItem Value="00">Seleccione</asp:ListItem>
                            <asp:ListItem Value="AR">Argentina</asp:ListItem>
                            <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                            <asp:ListItem Value="CL">Chile</asp:ListItem>
                            <asp:ListItem Value="CO">Colombia</asp:ListItem>
                            <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                            <asp:ListItem Value="DO">Rep. Dominicana</asp:ListItem>
                            <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                            <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                            <asp:ListItem Value="MX">Mexico</asp:ListItem>
                            <asp:ListItem Value="PA">Panama</asp:ListItem>
                            <asp:ListItem Value="PE">Perú</asp:ListItem>
                            <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                            <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                            <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="False" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
                        &nbsp;
                    <asp:Button ID="brnCancelar" runat="server" Text="Cancelar" OnClick="brnCancelar_Click"
                        CausesValidation="False" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
