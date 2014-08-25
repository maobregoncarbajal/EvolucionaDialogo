<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="RptParticipacionTiempoBuscar.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Reportes.RptParticipacionTiempoBuscar" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript" language="javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.dataTables.min.js"
        type="text/javascript"></script>

    <link rel="Stylesheet" href="../Styles/colorbox.css" type="text/css" />

    <script type="text/javascript" language="javascript">

        jQuery(document).ready(function () {

        });

        function abrirVentana() {
            var periodo = $('#<%= cboPeriodos.ClientID %>').val();
            var url = 'RptParticipacionTiempo.aspx?periodo=' + periodo;
            window.open(url);
        }

        function abrirMensaje() {
            jQuery("#divMensaje").dialog({
                modal: true,
                buttons: {
                    'Aceptar': function () {
                        jQuery(this).dialog('close');
                    }
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox runat="server" ID="txtStatus" Style="display: none" />
    <asp:TextBox runat="server" ID="txtProceso" Style="display: none" />
    <div id="divMensaje" style="display: none; font-size: 12px; text-align: center" title="MENSAJE">
        <br />
        <table>
            <tr>
                <td>
                    <img alt="" src="../Images/02.JPG" />
                </td>
                <td>
                    <span>No se encontraron registros para realizar el grafico del reporte. </span>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div align="center" style="vertical-align: middle">
        <br />
        <br />
        <br />
        <div style="height: 160px;">
            <table style="text-align: left; margin-top: 5px; margin-left: 5px; width: 400px;">
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="11">
                        <center>
                            <b>REPORTE DE PARTICIPACIÓN DEL TIEMPO</b></center>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;Año:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboAnio" runat="server" Style="width: 175px" CssClass="combo"
                            AutoPostBack="True" OnSelectedIndexChanged="cboAnio_SelectedIndexChanged">
                            <asp:ListItem Text="2008" Value="2008" />
                            <asp:ListItem Text="2009" Value="2009" />
                            <asp:ListItem Text="2010" Value="2010" />
                            <asp:ListItem Text="2011" Value="2011" />
                            <asp:ListItem Text="2012" Value="2012" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;Período :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPeriodos" runat="server" Style="width: 175px" CssClass="combo">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnVerReporte" runat="server" Text="" Style="margin-top: 8px; margin-right: 35px;"
                            CssClass="btnBuscarStyle" OnClick="btnVerReporte_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="left">
                        <div style="height: 14px">
                            <table style="width: 100; height: 100%">
                                <tr>
                                    <td>
                                        <a href="../Calendario/Default.aspx">Regresar</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
