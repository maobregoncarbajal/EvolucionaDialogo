<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configuracion.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.configuracion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <title>Configuración Dialogo de Desempeño</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 200px; margin-top: 100px">
            <span class="tituloPantalla">Configuración para el Dialogo de Desempeño</span><br />
            <br />
            <div style="float: left; padding: 5px">
                <label>
                    <asp:Label runat="server" ID="lblPais" Text="Pais :" CssClass="texto"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlPais" CssClass="combo">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label1" Text="Inicio de Evaluación" CssClass="texto"
                        Width="140px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label2" Text="D. Ventas a G. Region" CssClass="texto"
                        Width="150px" Height="25px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label3" Text="G. Region a G. Zona" CssClass="texto"
                        Width="150px"></asp:Label>
                </label>
            </div>
            <div style="float: left; padding: 5px">
                <asp:Label runat="server" ID="Label4" Text="Periodo :" CssClass="texto"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlPeriodo" CssClass="combo">
                </asp:DropDownList>
                <br />
                <br />
                <div style="float: left">
                    <label>
                        <br />
                        <br />
                        <asp:Button runat="server" ID="btnActivarDVaGR" Text="Activar" CssClass="button"
                            OnClick="btnActivarDVaGR_Click" />
                        <br />
                        <br />
                        <asp:Button runat="server" ID="btnActivarGRaGZ" Text="Activar" CssClass="button"
                            OnClick="btnActivarGRaGZ_Click" />
                    </label>
                </div>
                <div style="float: left">
                    &nbsp;
                <asp:Label runat="server" ID="Label5" Text="Fecha" CssClass="texto"></asp:Label><br />
                    <br />
                    <asp:Label runat="server" ID="lblFechaDVaGR" CssClass="texto" Height="25px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblFechaGRaGZ" Text="" CssClass="texto"></asp:Label>
                </div>
            </div>
            <div style="clear: left; margin-left: 60px;">
                <br />
                <br />
                <a href="pruebasUsuario.aspx" class="link">Retornar a Inicio de Diálogo</a>
                <br />
                <br />
                <asp:Label runat="server" ID="lblMensaje" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
