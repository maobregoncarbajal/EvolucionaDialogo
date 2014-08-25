<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visitaindicadores_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.visitaindicadores_Consulta" Title="Indicadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Visita Indicadores</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet"
        type="text/css" />

    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        jQuery(document).ready(function () {

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
            <table width="100%" cellpadding="0" cellspacing="0" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td style="height: 10px; text-align: left">&nbsp;&nbsp;&nbsp; <span class="subTituloMorado">Resultados de
                        <%=descRol %>
                        en las variables del negocio.</span><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 50px;">
                        <div style="margin-bottom: 5px; margin-top: 5px; font-size: 13px;">
                            Objetivo de la Visita :
                        </div>
                        <asp:TextBox ID="txtObjetivosVisita" runat="server" CssClass="inputtext" Width="460px"
                            TextMode="MultiLine" Height="50px" MaxLength="600" Rows="5" Enabled="false"></asp:TextBox>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 50px;">
                        <table style="text-align: left; padding-top: 10px; padding-bottom: 5px; width: 550px;">
                            <tr>
                                <td>
                                    <span class="texto_Negro">Período :</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cboPeriodosFiltro" Width="150px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboPeriodosFiltro_SelectedIndexChanged" Style="margin-left: 7px;" />
                                </td>
                                <td>
                                    <span class="texto_Negro">Campañas</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cboCampanhasFiltro" Width="150px" Style="margin-left: 7px;" />
                                </td>
                                <td align="right">
                                    <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" Text="BUSCAR" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td align="left" style="padding-left: 50px;">
                        <asp:GridView ID="grdvVariablesBase" Width="700px" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="cabecera_indicadores" />
                            <RowStyle CssClass="grilla_indicadores" />
                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Variable de Enfoque">
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" ID="cbEstado" CssClass="clsChhIndicadores"
                                            runat="server" AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td align="left" style="padding-left: 50px;">
                        <br />
                        <asp:LinkButton CssClass="link2" runat="server" ID="btnVariablesAdicionales" Text="Consultar Variables Adicionales"></asp:LinkButton>
                        <br />
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td align="left" style="padding-left: 50px;">
                        <asp:GridView ID="grdvVariablesAdicionales" Width="700px" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="cabecera_indicadores" />
                            <RowStyle CssClass="grilla_indicadores" />
                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Variable de Enfoque">
                                    <ItemTemplate>
                                        <asp:CheckBox Enabled="false" ID="cbEstado" runat="server" AutoPostBack="false"
                                            Checked='<%# Eval("bitEstado") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center"></td>
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
