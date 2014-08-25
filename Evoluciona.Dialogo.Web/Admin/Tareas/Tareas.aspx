<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Tareas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Tareas.Tareas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td>Tarea</td>
                <td>
                    <asp:DropDownList ID="ddlTareas" runat="server">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Value="1">Alertas</asp:ListItem>
                        <asp:ListItem Value="2">Cargar Data</asp:ListItem>
                        <asp:ListItem Value="3">Cargar Directorio</asp:ListItem>
                        <asp:ListItem Value="4">Competencias</asp:ListItem>
                        <asp:ListItem Value="5">Enviar Correos</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="bTareas" runat="server" Text="Ejecutar" OnClick="bTareas_Click" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        

    </div>
</asp:Content>
