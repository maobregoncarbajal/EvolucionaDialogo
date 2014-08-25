<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="FuenteVentas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.FuenteVentas" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuMatriz.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/calendar.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/ui.datepicker-es.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <table width="100%">
        <tr>
            <td style="color: #4660a1; font-size: 16px; font-weight: bold; text-align: center;">Fuente de Ventas
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="left" style="padding-left: 280px;">
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" class="btnSquare" OnClick="btnNuevo_Click" />
            </td>
        </tr>
    </table>
    <center>
        <asp:Panel ID="pnlDatosFuenteVentas" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="País"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPais" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Fuente Ventas"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFuenteVenta" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" class="btnSquare" OnClick="btnGrabar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btnSquare" OnClick="btnCancelar_Click" /><asp:HiddenField
                            ID="hfModo" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </center>
    <br />
    <center>
        <asp:Panel ID="pnlListaFuenteVenta" runat="server">
            <asp:GridView ID="gvFuenteVentas" runat="server" AutoGenerateColumns="False" OnRowCommand="gvFuenteVentas_RowCommand"
                OnRowDataBound="gvFuenteVentas_RowDataBound" DataKeyNames="chrPrefijoIsoPais,chrCodFuenteVentas,vchNomFuenteVentas,chrUsuarioCrea"
                AllowPaging="True" OnPageIndexChanging="gvFuenteVentas_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="chrPrefijoIsoPais" HeaderText="País">
                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                        <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="vchNomFuenteVentas" HeaderText="Fuente de Ventas">
                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                        <ItemStyle Width="220px" CssClass="CssCeldas3"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEditar" runat="server" CommandName="Editar" ImageUrl="~/Images/edit.png"></asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEliminar" runat="server" CommandName="Eliminar" ImageUrl="~/Images/delete_icon.png" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </center>
    <br />
    <div id="Div1" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblTipo" runat="server"></asp:Label>
        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
    </div>
</asp:Content>
