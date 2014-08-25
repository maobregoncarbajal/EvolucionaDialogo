<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Configuracion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Configuracion" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            width: 130px;
        }
    </style>
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float: left; padding: 10px 0 0 20px">
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Configuracion.jpg" />
    </div>
    <br />
    <br />
    <div style="margin: 50px 0 0 20px; text-align: left;">
        <table style="width: 550px;">
            <tr>
                <td colspan="4">
                    <span class="texto" style="font-weight: bold;">País : </span>
                    <asp:DropDownList runat="server" ID="ddlPais" CssClass="combo" Width="150px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp; <span class="texto" style="font-weight: bold;">Período :</span>
                    <asp:DropDownList runat="server" ID="ddlPeriodo" CssClass="combo" Width="100px">
                    </asp:DropDownList>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <hr />
        <table style="width: 550px;">
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <div class="texto" style="font-weight: bold;">
                        Inicio de Evaluación
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        D. Ventas a G. Región
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnActivarDVaGR" Text="Activar" CssClass="button"
                        OnClick="btnActivarDVaGR_Click" Width="80px" />
                    <asp:Button ID="btnTrggr01ActivarDVaGR" runat="server"
                        Text="Button" Style="display: none"
                        OnClick="btnTrggr01ActivarDVaGR_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblFechaDVaGR" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        G. Region a G. Zona
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnActivarGRaGZ" Text="Activar" CssClass="button"
                        OnClick="btnActivarGRaGZ_Click" Width="80px" />
                    <asp:Button ID="btnTrggr02ActivarGRaGZ" runat="server"
                        Text="Button" Style="display: none"
                        OnClick="btnTrggr02ActivarGRaGZ_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblFechaGRaGZ" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblMensaje" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table style="width: 550px;">
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <div class="texto" style="font-weight: bold;">
                        Correo: Diálogos Evoluciona
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        DV Evaluadora
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvCorDV" Text="Enviar" CssClass="button" Width="80px"
                        OnClick="btnEnvCorDV_Click" />
                    <asp:Button ID="btnTrggr03EnvCorDV" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr03EnvCorDV_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDeDV" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        GRs Evaluadoras
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvCorGR" Text="Enviar" CssClass="button" Width="80px"
                        OnClick="btnEnvCorGR_Click" />
                    <asp:Button ID="btnTrggr04EnvCorGR" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr04EnvCorGR_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDeGR" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblMensajeDe" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table style="width: 550px;">
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <div class="texto" style="font-weight: bold;">
                        Correo: Inicio de tu Diálogo Evoluciona
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        GRs Evaluadas
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvCorGrEvaluados" Text="Enviar" CssClass="button"
                        Width="80px" OnClick="btnEnvCorGrEvaluados_Click" />
                    <asp:Button ID="btnTrggr05EnvCorGrEvaluados" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr05EnvCorGrEvaluados_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblIdeGR" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        GZs Evaluadas
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvCorGzEvaluados" Text="Enviar" CssClass="button"
                        Width="80px" OnClick="btnEnvCorGzEvaluados_Click" />
                    <asp:Button ID="btnTrggr06EnvCorGzEvaluados" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr06EnvCorGzEvaluados_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblIdeGZ" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblMensajeIde" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table style="width: 550px;">
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <div class="texto" style="font-weight: bold;">
                        Correo: Aprobación del Diálogo Evoluciona de tu equipo
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        DV Evaluadora
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvCorAdeeDv" Text="Enviar" CssClass="button" Width="80px"
                        OnClick="btnEnvCorAdeeDv_Click" />
                    <asp:Button ID="btnTrggr07EnvCorAdeeDv" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr07EnvCorAdeeDv_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAdeeDV" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        GRs Evaluadoras
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvCorAdeeGr" Text="Enviar" CssClass="button" Width="80px"
                        OnClick="btnEnvCorAdeeGr_Click" />
                    <asp:Button ID="btnTrggr08EnvCorAdeeGr" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr08EnvCorAdeeGr_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAdeeGR" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblMensajeAdee" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table style="width: 550px;">
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <div class="texto" style="font-weight: bold;">
                        Correo: Planes acordados en diálogos
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        GRs Evaluadas
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvPlaAcorDiaGr" Text="Enviar"
                        CssClass="button" Width="80px" OnClick="btnEnvPlaAcorDiaGr_Click" />
                    <asp:Button ID="btnTrggr09EnvPlaAcorDiaGr" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr09EnvPlaAcorDiaGr_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblPlaAcorDiaGr" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="style1">
                    <div class="texto">
                        GZs Evaluadas
                    </div>
                    <br />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnEnvPlaAcorDiaGz" Text="Enviar"
                        CssClass="button" Width="80px" OnClick="btnEnvPlaAcorDiaGz_Click" />
                    <asp:Button ID="btnTrggr10EnvPlaAcorDiaGz" runat="server" Text="Button"
                        Style="display: none" OnClick="btnTrggr10EnvPlaAcorDiaGz_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblPlaAcorDiaGz" CssClass="texto"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblMensajePlaAcorDia" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting" id="imgExporting" style="display: none" />
    <script type="text/javascript">
        var mensajeAlert = '';
        var tipoAccion = '';
        jQuery(document).ready(function () {

            document.onkeydown = function (evt) {
                return (evt ? evt.which : event.keyCode) != 13;
            };

        });

        function ProcessOpen(tipo) {
            jQuery.blockUI({
                message: jQuery("#imgExporting"),
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: 'transparent',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    color: '#fff'
                }
            });

            if (tipo == "1") {
                $("#<%=btnTrggr01ActivarDVaGR.ClientID %>").trigger("click");
            } else if (tipo == "2") {
                $("#<%=btnTrggr02ActivarGRaGZ.ClientID %>").trigger("click");
            } else if (tipo == "3") {
                $("#<%=btnTrggr03EnvCorDV.ClientID %>").trigger("click");
            } else if (tipo == "4") {
                $("#<%=btnTrggr04EnvCorGR.ClientID %>").trigger("click");
            } else if (tipo == "5") {
                $("#<%=btnTrggr05EnvCorGrEvaluados.ClientID %>").trigger("click");
            } else if (tipo == "6") {
                $("#<%=btnTrggr06EnvCorGzEvaluados.ClientID %>").trigger("click");
            } else if (tipo == "7") {
                $("#<%=btnTrggr07EnvCorAdeeDv.ClientID %>").trigger("click");
            } else if (tipo == "8") {
                $("#<%=btnTrggr08EnvCorAdeeGr.ClientID %>").trigger("click");
            } else if (tipo == "9") {
                $("#<%=btnTrggr09EnvPlaAcorDiaGr.ClientID %>").trigger("click");
            } else if (tipo == "10") {
                $("#<%=btnTrggr10EnvPlaAcorDiaGz.ClientID %>").trigger("click");
            }

}

function ProcessClose() {
    $.unblockUI({
        onUnblock: function () { }
    });
}

    </script>
</asp:Content>
