<%@ Page Theme="TemaDDesempenio" Language="C#" AutoEventWireup="true" CodeBehind="pruebasUsuarioEC.aspx.cs" Inherits="Evoluciona.Dialogo.Web.pruebasUsuarioEC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pruebas de Usuario</title>
    <script type="text/javascript" language="javascript">
        function IrValidacion() {
            location.href = "validacion.aspx?codigoUsuario=" + document.getElementById('txtDocuIdentidad').value + "&prefijoIsoPais=" + document.getElementById('txtPrefijoIsoPais').value + "&codigoRol=" + document.getElementById('ddlRoles').value + "&app=DD";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 200px; margin-top: 100px">
            <span class="tituloPantalla">Pruebas de usuario</span><br />
            <br />
            <div>


                <asp:Label runat="server" ID="lbl1" Width="125px" CssClass="texto">Docu. Identidad:</asp:Label>
                <input type="text" id="txtDocuIdentidad" value="" class="inputtext" runat="server" /><br />
                <asp:Label runat="server" ID="Label1" Width="125px" CssClass="texto">Prefijo Iso Pa�s:</asp:Label>
                <input type="text" id="txtPrefijoIsoPais" value="EC" class="inputtext" runat="server" /><br />
                <asp:Label runat="server" ID="Label3" Width="125px" CssClass="texto">C�digo del Rol:</asp:Label>


                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="combo">
                    <asp:ListItem Value="4">Directora de Ventas</asp:ListItem>
                    <asp:ListItem Value="5">Gerente de Region</asp:ListItem>
                    <asp:ListItem Value="6">Gerente de Zona</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                <br />
                <a href="configuracion.aspx" class="link">Ir a Configuraci�n</a> &nbsp; &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkIniciarDialogo" runat="server" CssClass="link" OnClick="lnkIniciarDialogo_Click">Iniciar Di�logo de Desempe�o</asp:LinkButton>
                &nbsp; &nbsp;<asp:LinkButton ID="lnkbIniciarVisitas" runat="server" OnClick="lnkbIniciarVisitas_Click" CssClass="link">Iniciar Visita</asp:LinkButton><br />
                <br />
                <br />
                <a href="javascript:IrValidacion();" class="link"></a>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReiniciar" runat="server" OnClick="btnReiniciar_Click" Text="Reiniciar Aplicacion" Visible="false" />
                <asp:Button ID="btnParalelo" runat="server" OnClick="btnParalelo_Click" Text="Ambiente Paralelo" />
            </div>
        </div>

    </form>
</body>
</html>
