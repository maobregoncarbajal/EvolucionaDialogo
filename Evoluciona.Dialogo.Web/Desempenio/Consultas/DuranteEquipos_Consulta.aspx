<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuranteEquipos_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.DuranteEquipos_Consulta" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.window.css" rel="stylesheet" type="text/css" />
    <link href="../../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SimpleMenu.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        jQuery(document).ready(function () {

            jQuery("#<%=txtTextoIngresado.ClientID %>").attr("readOnly", "readOnly");

            CargarDescripcionCantidad();

            jQuery(".MenuBasico .AspNet-Menu-Link").click(function (event) {
                var textoActual = jQuery(this).attr("title");
                var valorActual = jQuery(this).attr("href");

                if (typeof (textoActual) == "undefined")
                    textoActual = "";

                jQuery("#<%=txtTextoIngresado.ClientID %>").text(textoActual);
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorActual);

                jQuery.getJSON("<%=Utils.RelativeWebRoot%>Desempenio/Handlers/CriticasHandler.ashx",
                    {
                        idProceso: "<%=idProceso %>",
                        documentoIdentidad: valorActual
                    },
                    function (data) {
                        jQuery("#<%= txtVariablesAnalizar.ClientID%>").attr("value", data.variableConsiderar);
                    });

                event.preventDefault();
            });

            jQuery(".MenuBasico_Izquierda a").click(function (event) {
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "HISTORIAL",
                    url: this.href,
                    minimizable: false,
                    maximizable: false,
                    bookmarkable: false,
                    resizable: false
                });

                event.preventDefault();
            });
        });

            function CargarDescripcionCantidad() {
                var totalSeleccionados = jQuery("#<%=mnuSelecccionados.ClientID %> li").length;

            jQuery("#lblTotalSeleccionados").text(totalSeleccionados);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
        <div style="margin: 15px 0 0 55px; text-align: left">
            <div style="margin: 55px 0 0 55px; text-align: left; width: 600px">
                <p>
                    <span class="texto_Negro">Ingresa el plan de acción para cada
                        <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado" />
                        CRITICAS priorizada
                    </span>
                </p>
                <br />
                <br />
                <p>
                    Ingresadas (<span id="lblTotalSeleccionados"></span>/<asp:Literal Text="text" runat="server" ID="lblTotalElementos" />)
                </p>
                <br />
                <br />
                <div style="width: 580px; margin-left: 15px;">
                    <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
                    <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
                </div>
                <br />
                <div style="width: 580px; margin-left: 15px;">
                    <table width="550px" cellspacing="0" cellpadding="0" style="vertical-align: top">
                        <tr>
                            <td align="left" style="color: White; background-color: #a0a0a0; font-family: Arial; font-size: 12px; font-weight: bold; text-decoration: none;">
                                <asp:Label ID="lblNombrePersonaEvaluada" runat="server" Text="[SIN SELECCIONAR]"></asp:Label>
                                <asp:TextBox runat="server" Style="display: none" ID="txtValorCritica" />
                            </td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" cellspacing="0px" cellpadding="0" style="vertical-align: top">
                                    <tr>
                                        <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left; height: 25px; vertical-align: middle; font-size: 14px; font-weight: bolder">&nbsp; Variable
                                        </td>
                                        <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left; height: 25px; vertical-align: middle; font-size: 14px; font-weight: bolder">&nbsp; Plan de acción a Considerar
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;">
                                            <asp:TextBox runat="server" ID="txtVariablesAnalizar" CssClass="inputtext_criticas" TextMode="MultiLine" Rows="5" MaxLength="600" ReadOnly="true" />
                                        </td>
                                        <td>
                                            <asp:TextBox Width="99%" ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas" TextMode="MultiLine" Rows="5" MaxLength="600"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div style="width: 580px; margin-left: 15px;">
                    <table width="550px" cellspacing="0" cellpadding="0" style="vertical-align: top">
                        <tr>
                            <td>
                                <asp:GridView ID="gvEquipos" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%">
                                    <EmptyDataTemplate>
                                        <table width="500px">
                                            <tr class="cabecera_indicadores">
                                                <th style="width: 150px;">NOMBRE COMPLETO</th>
                                                <th style="width: 100px;">VARIABLE CONSIDERAR</th>
                                                <th style="width: 200px;">PLAN DE ACCION</th>
                                            </tr>
                                            <tr class="grilla_indicadores">
                                                <td colspan="3" align="center" style="color: #0caed7;">
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="cabecera_indicadores" />
                                    <PagerSettings Mode="NextPreviousFirstLast" />
                                    <RowStyle CssClass="grilla_indicadores" Height="20px" />
                                    <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                    <Columns>
                                        <asp:BoundField HeaderText="NOMBRE COMPLETO" DataField="NombreEquipo">
                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="VARIABLE CONSIDERAR" DataField="variableConsiderar">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PlanAccion" HeaderText="PLAN DE ACCION">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
