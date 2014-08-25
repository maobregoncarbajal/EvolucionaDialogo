<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanAccionVisita_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.PlanAccionVisita_Consulta" Title="Plan Accion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Plan de Accion</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet"
        type="text/css" />

    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">

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
        <div style="margin-top: 30px;">
            <table cellpadding="0" cellspacing="2" width="100%" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td style="width: 15px"></td>
                    <td align="left">
                        <span class="subTituloMorado">Acuerdos del diálogo de desempeño.</span><br />
                        <div style="margin-bottom: 5px; margin-top: 5px; font-size: 13px;">
                            Objetivo de la Visita :
                        </div>
                        <asp:TextBox ID="txtObjetivosVisita" runat="server" CssClass="inputtext" Width="460px"
                            TextMode="MultiLine" Height="50px" MaxLength="600" Rows="5" Enabled="false"></asp:TextBox>
                        <br />
                        <br />
                        <table cellpadding="0">
                            <tr>
                                <td style="vertical-align: text-top; width: 370px">
                                    <asp:Label ID="Label1" runat="server" Text="Variable no Definida" CssClass="textoVarEnfoque"
                                        Width="163px"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblVariable1" Text="Variable no Definida" Width="160px"
                                        Style="display: none;"></asp:Label>
                                    <asp:GridView ID="gvVariablesCausa1" Width="350px" runat="server" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                        <RowStyle CssClass="grilla_indicadores" />
                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Variables" DataField="DescripcionVariable">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td style="width: 147px; vertical-align: text-top" class="texto_morado_brand">Zonas<br />
                                    <asp:TextBox runat="server" ID="txtZonas1" TextMode="MultiLine" Height="120px" MaxLength="600"
                                        Width="90px" CssClass="inputtext"></asp:TextBox>
                                </td>
                                <td style="width: 256px; vertical-align: text-top" class="texto_morado_brand">Plan de Acción<br />
                                    <asp:TextBox runat="server" ID="txtPlanAccion1" TextMode="MultiLine" Height="120px"
                                        CssClass="inputtext" Width="320px" MaxLength="600"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px;" colspan='2'></td>
                                <td></td>
                                <td style="height: 15px;">
                                    <span class="textoEtiquetas"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: text-top">&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="Variable no Definida" CssClass="textoVarEnfoque"
                                    Width="163px"></asp:Label>
                                    <br />
                                    <asp:Label runat="server" ID="lblVariable2" Text="Variable no Definida" Width="160px"
                                        Style="display: none;"></asp:Label>
                                    <asp:GridView ID="gvVariablesCausa2" Width="350px" runat="server" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                        <RowStyle CssClass="grilla_indicadores" />
                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Variables" DataField="DescripcionVariable">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td style="width: 147px; vertical-align: text-top" class="texto_morado_brand">Zonas<br />
                                    <asp:TextBox runat="server" ID="txtZonas2" TextMode="MultiLine" Height="120px" MaxLength="600"
                                        Width="90px" CssClass="inputtext"></asp:TextBox>
                                </td>
                                <td style="width: 256px; vertical-align: text-top" class="texto_morado_brand">Plan de Acción<br />
                                    <asp:TextBox runat="server" ID="txtPlanAccion2" TextMode="MultiLine" Height="120px"
                                        CssClass="inputtext" Width="320px" MaxLength="600"></asp:TextBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 34px"></td>
                                <td style="width: 37px">
                                    <asp:HiddenField ID="hdIdVarEnfoque1" runat="server" />
                                    <asp:HiddenField ID="hdIdIndicador1" runat="server" />
                                    <asp:Label ID="lblvariableCausa1" runat="server" Style="display: none" Text="Variable Causa no Definida"></asp:Label>
                                </td>
                                <td style="vertical-align: top; width: 144px;">
                                    <span class="textoEtiquetas"></span>&nbsp;<asp:HiddenField ID="hdIdIndicador2" runat="server" />
                                    <asp:HiddenField ID="hdIdVarEnfoque2" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <img src="../../Images/plan_accion.jpg" style="width: 328px; height: 464px" alt="" />
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
