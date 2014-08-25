<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntesEquipos_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.AntesEquipos_Consulta" %>
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
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">

        jQuery(document).ready(function () {

            jQuery("#<%=txtTextoIngresado.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=txtNumero.ClientID %>").attr("readOnly", "readOnly");
            CargarDescripcionCantidad();

            jQuery(".MenuBasico .AspNet-Menu-Link").click(function (event) {
                var textoActual = jQuery(this).attr("title");
                var valorActual = jQuery(this).attr("href");

                if (typeof (textoActual) == "undefined")
                    textoActual = "";

                jQuery("#<%=txtTextoIngresado.ClientID %>").text(textoActual);
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorActual);

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
        <div style="margin: 15px 0 0 55px; text-align: left">
            <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
            <p>
                <span class="texto_Negro">
                    <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado" />
                    CRÍTICAS que priorizarás en el di&aacute;logo</span>
            </p>
            <p>
                &nbsp;
            </p>
            <div style="width: 980px; margin-top: 15px;">
                <div style="float: left; width: 300px;">
                    <table>
                        <tr>
                            <td>
                                <div style="width: 280px; border-style: solid; text-align: center; border-width: 1px; padding: 5px 0px 5px 5px;"
                                    class="roundedDiv">
                                    <small>Segmentación en base a la &uacute;ltima campa&ntilde;a
                                        <br />
                                        (
									<span class="texto_Negro">
                                        <asp:Literal runat="server" ID="lblUltimaCampanha" /></span>)</small>
                                    <br />
                                    <br />
                                    <asp:DataList ID="ddlResumen" runat="server" Style="margin-left: auto; margin-right: auto">
                                        <ItemTemplate>
                                            <div class="texto_descripciones" style="text-align: left;">
                                                <img style="width: 10px;" src="<%=Utils.RelativeWebRoot %>Images/vinetablue.jpg"
                                                    alt="puntito.jpg" />
                                                <span style="margin: 5px;">
                                                    <%# Eval("vchEstadoPeriodo")%>
                                                </span>
                                                <%# Eval("%")%>
                                            %
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <br />
                                    <asp:Label Text=" Nº Gerente de Zonas Nuevas" runat="server" ID="lblNumero" CssClass="texto_descripciones" />
                                    :
                                <asp:TextBox runat="server" ID="txtNumero" Width="60px" Style="text-align: right;" />
                                </div>
                                <br />
                                <asp:Label Text="GZ CRITICAS" runat="server" CssClass="texto_descripciones" ID="lblCriticas" />
                                <br />
                                <asp:ListBox runat="server" ID="lstCriticas" DataTextField="nombresCritica" DataValueField="documentoIdentidad"
                                    Style="width: 290px; height: 110px; margin-top: 5px;"></asp:ListBox>
                                <br />
                                <br />
                                <asp:Label Text="GZ ESTABLES" runat="server" CssClass="texto_descripciones" ID="lblEstable" />
                                <br />
                                <asp:ListBox runat="server" ID="lstEstables" DataTextField="nombresCritica" DataValueField="documentoIdentidad"
                                    Style="width: 290px; height: 110px; margin-top: 5px;"></asp:ListBox>
                                <br />
                                <br />
                                <asp:Label Text="GZ PRODUCTIVAS" runat="server" CssClass="texto_descripciones" ID="lblProductivas" />
                                <br />
                                <asp:ListBox runat="server" ID="lstProductivas" DataTextField="nombresCritica" DataValueField="documentoIdentidad"
                                    Style="width: 290px; height: 110px; margin-top: 5px;"></asp:ListBox>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="position: relative; left: 70px; width: 560px; float: left">
                    <br />

                    <br />
                    Ingrese las variables a analizar de la(s)
                <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado_1" />
                    CRÍTICAS:
                <br />
                    <br />
                    <table width="550px" cellspacing="0" cellpadding="0" style="vertical-align: top">
                        <tr>
                            <td align="left" style="color: White; background-color: #a0a0a0; font-family: Arial; font-size: 12px; font-weight: bold; text-decoration: none;">
                                <asp:Label ID="lblNombrePersonaEvaluada" runat="server" Text="[SIN SELECCIONAR]"></asp:Label>
                                <asp:TextBox runat="server" Style="display: none" ID="txtValorCritica" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left;">&nbsp; Variables a Considerar
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas textoAntesEquipo"
                                                TextMode="MultiLine" Rows="5" MaxLength="600"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"></td>
                        </tr>
                    </table>
                    <p class="texto_espaciado">
                        Ingresadas (&nbsp;<span id="lblTotalSeleccionados"></span>&nbsp;/&nbsp;<asp:Literal
                            Text="text" runat="server" ID="lblTotalElementos" />&nbsp;)
                    </p>
                    <br />
                    <br />
                    <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
                    <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
