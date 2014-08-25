<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuranteNegocio_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.DuranteNegocio_Consulta" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            jQuery("#<%=txtPlanAccion1.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=txtPlanAccion2.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=txtZonas1.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=txtZonas2.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=txtCampania1.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=txtCampania2.ClientID %>").attr("readOnly", "readOnly");

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 15px 0 0 55px; width: 750px">
            <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
            <table border="0" cellpadding="0" cellspacing="4" width="100%" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td colspan="1" style="width: 41px; height: 19px"></td>
                    <td colspan='4' style="height: 19px" class="texto_descripciones">Ingrese el detalle de las zonas a trabajar y el plan de acción definido para las
                    variables
                    </td>
                </tr>
                <tr>
                    <td style="width: 41px; height: 19px"></td>
                    <td colspan="3"></td>
                    <td style="height: 19px; text-align: right">
                        <asp:DropDownList runat="server" Width="180px" ID="cboEstadoIndicador1" Style="display: none">
                            <asp:ListItem Text="En Proceso" Value="0" />
                            <asp:ListItem Text="Completado" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 41px" align="right">&nbsp;
                    </td>
                    <td>&nbsp;<asp:Label ID="lblVariable1" runat="server" CssClass="textoVarEnfoque" Text="Variable no Definida"
                        Width="165px"></asp:Label>
                        <asp:Label runat="server" ID="lblVariableGeneral1" Text="Variable no Definida" Width="113px"
                            Style="display: none;"></asp:Label>
                    </td>
                    <td style="width: 200px;">
                        <table style="border: solid 1px #c8c8c7;">
                            <tr>
                                <td class="texto_morado_brand">Variables Causales
                                <div>
                                    <br />
                                    <asp:Label runat="server" ID="lblvariableCausa1" Text="Variable Causa no Definida"
                                        Style="display: none;"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Variable Causa no Definida"
                                        CssClass="textoVarEnfoque_hija" Width="149px"></asp:Label>
                                    <asp:Label runat="server" ID="lblvariableCausa2" Text="Variable Causa no Definida"
                                        Style="display: none;"></asp:Label><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Variable Causa no Definida"
                                        CssClass="textoVarEnfoque_hija" Width="149px"></asp:Label>
                                </div>
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <span class="textoEtiquetas">Campaña</span><br />
                            <asp:TextBox ID="txtCampania1" runat="server" MaxLength="6" Width="90px" CssClass="inputtext"></asp:TextBox>
                            <br />
                        </div>
                    </td>
                    <td style="width: 147px" class="texto_morado_brand">Zonas<br />
                        <asp:TextBox runat="server" ID="txtZonas1" TextMode="MultiLine" Height="120px" MaxLength="600"
                            Width="90px" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 256px" class="texto_morado_brand">&nbsp;Plan de Acción<br />
                        <asp:TextBox runat="server" ID="txtPlanAccion1" TextMode="MultiLine" Height="120px"
                            CssClass="inputtext" Width="220px" MaxLength="600"></asp:TextBox>
                        <input type="button" id="Button2" value="Agregar" class="button" style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 41px"></td>
                    <td></td>
                    <td style="vertical-align: top; width: 144px;">
                        <span class="textoEtiquetas"></span>
                    </td>
                    <td style="vertical-align: top; width: 147px;">
                        <span class="textoEtiquetas"></span>&nbsp;<br />
                        <div style="text-align: right; margin-right: 10px">
                            &nbsp;
                        </div>
                    </td>
                    <td style="vertical-align: middle; width: 256px; text-align: right">
                        <asp:DropDownList runat="server" Width="180px" ID="cboEstadoIndicador2" Style="display: none">
                            <asp:ListItem Text="En Proceso" Value="0" />
                            <asp:ListItem Text="Completado" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 41px" align="right">&nbsp;
                    </td>
                    <td style="width: 37px;">&nbsp;
                    <asp:Label ID="lblVariableGeneral2" runat="server" Text="Variable no Definida" CssClass="textoVarEnfoque"
                        Width="163px"></asp:Label>
                        <asp:Label runat="server" ID="lblVariable2" Text="Variable no Definida" Width="118px"
                            Style="display: none;"></asp:Label>
                    </td>
                    <td style="width: 200px">
                        <table style="border: solid 1px #c8c8c7;">
                            <tr>
                                <td class="texto_morado_brand">Variables Causales
                                <div>
                                    <br />
                                    <asp:Label runat="server" ID="lblvariableCausa3" Text="Variable Causa no Definida"
                                        Style="display: none;"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label5" runat="server" CssClass="textoVarEnfoque_hija" Text="Variable Causa no Definida"
                                        Width="149px"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblvariableCausa4" Text="Variable Causa no Definida"
                                        Style="display: none;"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label6" runat="server" CssClass="textoVarEnfoque_hija" Text="Variable Causa no Definida"
                                        Width="149px"></asp:Label>
                                </div>
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <span class="textoEtiquetas">Campaña</span><br />
                            <asp:TextBox ID="txtCampania2" runat="server" MaxLength="6" Width="90px" CssClass="inputtext"></asp:TextBox>
                            <br />
                        </div>
                    </td>
                    <td style="width: 147px" class="texto_morado_brand">Zonas<br />
                        <asp:TextBox runat="server" ID="txtZonas2" TextMode="MultiLine" Height="120px" MaxLength="600"
                            Width="90px" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 256px" class="texto_morado_brand">Plan de Acción<br />
                        <asp:TextBox runat="server" ID="txtPlanAccion2" TextMode="MultiLine" Height="120px"
                            CssClass="inputtext" Width="220px" MaxLength="600"></asp:TextBox>
                        <input type="button" id="Button3" value="Agregar" class="button" style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 41px"></td>
                    <td style="width: 37px"></td>
                    <td style="vertical-align: top; width: 144px;">&nbsp;
                    </td>
                    <td style="vertical-align: top; width: 147px;">&nbsp;
                    </td>
                    <td style="vertical-align: top; width: 256px">&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
