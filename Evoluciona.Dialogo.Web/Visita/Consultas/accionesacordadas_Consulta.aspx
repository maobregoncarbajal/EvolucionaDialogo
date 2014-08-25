<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accionesacordadas_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.accionesacordadas_Consulta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Acciones Acordadas</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet"
        type="text/css" />

    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            jQuery(".checkDesabilitado").click(function (evento) {
                evento.preventDefault();
            });

            jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");
            jQuery(".checkDesabilitado").attr("readOnly", "readOnly");
        });

        function AbrirMensaje() {
            $("#divDialogo").dialog({
                modal: true,
                buttons:
                        {
                            Ok: function () {
                                jQuery(this).dialog("close");
                            }
                        }
            });
        }

    </script>

</head>
<body style="background: white;">
    <form id="form1" runat="server">
        <div style="margin-top: 35px;">
            <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td style="width: 50px;"></td>
                    <td colspan="5" align="left">
                        <br />
                        <span class="subTituloMorado">Acciones Acordadas.</span><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Repeater ID="repAcciones" runat="server" OnItemCreated="repAcciones_ItemCreated">
                            <HeaderTemplate>
                                <table>
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Acciones Acordadas
                                            </th>
                                            <th>Campañas
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: left; vertical-align: top;">
                                        <asp:HiddenField ID="hidIdAccion" runat="server" Value='<%# Eval("IdAcciones") %>' />
                                        <asp:HiddenField ID="hidIdIndicador" runat="server" Value='<%# Eval("IDIndicador1") %>' />
                                        <asp:HiddenField ID="hidTipoAccion" runat="server" Value='<%# Eval("TipoAccion") %>' />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblVariable" runat="server" CssClass="textoVarEnfoque" Text='<%# Eval("DesVariablePadre1") %>' />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:Label ID="lblVariableCausa1" runat="server" CssClass="textoVarEnfoque_hija"
                                                        Text="Variable Causa 1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 15px;">
                                                    <asp:Label ID="lblVariableCausa2" runat="server" CssClass="textoVarEnfoque_hija"
                                                        Text="Variable Causa 2" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 360px;" class="texto_morado_brand" align="center">
                                        <asp:TextBox runat="server" ID="txtaccionesAcordadas" TextMode="MultiLine" Height="110px"
                                            MaxLength="600" Width="350px" CssClass="inputtext" Text='<%# Eval("AccionesAcordadas1") %>' />
                                    </td>
                                    <td style="width: 120px" class="texto_morado_brand" align="center">
                                        <asp:TextBox runat="server" ID="txtcampanias" TextMode="MultiLine" Height="110px"
                                            CssClass="inputtext" Width="100px" MaxLength="600" Text='<%# Eval("Campanias1") %>' />
                                    </td>
                                    <td class="texto_morado_brand" align="center">
                                        <asp:CheckBox ID="chbxPostVenta" runat="server" Text="Post - Visita" Checked='<%# Eval("PostVisita1") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody> </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </div>
        <div class="demo">
            <div style="display: none" id="divDialogo" title="INFORMACION">
                <br />
                <span>No tienes Visitas registradas en este Periodo</span>
                <br />
            </div>
        </div>
    </form>
</body>
</html>
