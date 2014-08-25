<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntesNegocio_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.AntesNegocio_Consulta" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.window.css" rel="stylesheet" type="text/css" />
    <link href="../../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#<%=contenidoPaso2.ClientID %> select").attr("disabled", "disabled");

            var maximoMarcados = 0;

            jQuery(".clsChhIndicadores").click(function (event) {
                var cantidadChecksMarcados = jQuery("#divContenedorChecks input:checked").size();

                if (cantidadChecksMarcados > maximoMarcados)
                    event.preventDefault();
            });

            jQuery(".linkPDF").click(function (event) {
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "MAPA DE VARIABLES CAUSA",
                    url: this.href,
                    minimizable: false,
                    bookmarkable: false
                });

                event.preventDefault();
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 25px 0 0 15px" id="divPaso1Negocio">
            <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
            <div style="height: 80px; width: 250px; margin-right: auto; margin-left: 35px" id="contenedorFiltros" runat="server">
                <table style="text-align: left; margin-top: 5px; margin-left: 5px;">
                    <tr>
                        <td colspan="2">
                            <div style="text-align: center;">Selecciona el periodo y campaña para visualizar los resultados de las variables.</div>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto_Negro">Período :</span>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cboPeriodosFiltro" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboPeriodosFiltro_SelectedIndexChanged" Style="margin-left: 7px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto_Negro">Campañas :</span>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="cboCampanhasFiltro" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboCampanhasFiltro_SelectedIndexChanged" Style="margin-left: 7px;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin: 15px 0 0 15px; text-align: left; width: 100%" id="contenidoPaso1"
                runat="server">
                <table style="width: 700px; float: left">
                    <tr>
                        <td style="height: 30px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Selecciona tus Variables de Enfoque
                        </td>
                        <td align="center">Selecciona M&aacute;ximo 02 Variables
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>
                            <br />
                            <span class="texto_Negro">
                                <asp:Literal ID="litMensajeResultado" runat="server" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="margin-top: 12px;" id="divContenedorChecks">
                                <asp:GridView ID="gvVariables" Width="700px" runat="server" AutoGenerateColumns="False">
                                    <EmptyDataTemplate>
                                        <table width="750px">
                                            <tr class="cabecera_indicadores">
                                                <th style="width: 180px;">Variables
                                                </th>
                                                <th style="width: 90px;">Meta
                                                </th>
                                                <th style="width: 90px;">Resultado
                                                </th>
                                                <th style="width: 90px;">Diferencia
                                                </th>
                                                <th style="width: 90px;">(%)
                                                </th>
                                                <th style="width: 120px;">Campa&#241;a
                                                </th>
                                            </tr>
                                            <tr class="grilla_indicadores">
                                                <td colspan="6" align="center" style="font-size: 14pt; color: #0caed7; font-style: italic">No se encontraron resultados para la búsqueda actual...
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="cabecera_indicadores" />
                                    <RowStyle CssClass="grilla_indicadores" />
                                    <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="0px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Porcentaje" DataFormatString="{0:N}%" HeaderText="(%)">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Variable de Enfoque">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbEstado" CssClass="clsChhIndicadores" name="CheckBox1" runat="server"
                                                    AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <table style="width: 700px">
                                    <thead class="cabecera_indicadores">
                                        <tr>
                                            <th>Variable
                                            </th>
                                            <th style="width: 90px; text-align: right;">Meta
                                            </th>
                                            <th style="width: 90px; text-align: right;">Resultado
                                            </th>
                                            <th style="width: 90px; text-align: right;">Diferencia
                                            </th>
                                            <th style="width: 90px; text-align: right;">(%)
                                            </th>
                                            <th id="headerCampanha" runat="server" style="width: 90px; text-align: center;">Campa&ntilde;a
                                            </th>
                                            <th style="text-align: center; width: 120px;">Variable de Enfoque
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="grilla_indicadores">
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="cboVariablesAdicionales1" Width="180px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboVariablesAdicionales1_SelectedIndexChanged" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblMeta1" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblResultado1" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblDiferencia1" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblPorcentaje1" />
                                            </td>
                                            <td style="text-align: center; width: 90px;" id="filaCampanha1" runat="server">
                                                <asp:Label Text="text" runat="server" ID="lblCampanha1" />
                                            </td>
                                            <td style="text-align: center; width: 120px;">
                                                <asp:CheckBox runat="server" ID="chkEstadoVariableAdicional1" CssClass="clsChhIndicadores" />
                                            </td>
                                        </tr>
                                        <tr class="grilla_indicadores">
                                            <td align="left">
                                                <asp:DropDownList runat="server" ID="cboVariablesAdicionales2" Width="180px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboVariablesAdicionales1_SelectedIndexChanged" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblMeta2" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblResultado2" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblDiferencia2" />
                                            </td>
                                            <td class="texto_Derecha" style="width: 90px;">
                                                <asp:Label Text="text" runat="server" ID="lblPorcentaje2" />
                                            </td>
                                            <td style="text-align: center; width: 90px;" id="filaCampanha2" runat="server">
                                                <asp:Label Text="text" runat="server" ID="lblCampanha2" />
                                            </td>
                                            <td style="text-align: center; width: 120px;">
                                                <asp:CheckBox runat="server" ID="chkEstadoVariableAdicional2" CssClass="clsChhIndicadores" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="7" style="padding-top: 15px">
                                                <asp:Button runat="server" CssClass="btnAceptarStyle" ID="btnAceptar" OnClick="btnAceptar_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:TextBox runat="server" ID="txtIdVariable1" Style="display: none;" />
                <asp:TextBox runat="server" ID="txtIdVariable2" Style="display: none;" />
            </div>
            <div id="contenidoPaso2" runat="server" style="margin: 15px 0 0 35px; text-align: left;">
                <table style="width: 772px;" id="table1">
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td align="left" colspan="2"></td>
                        <td align="center" style="width: 181px"></td>
                        <td align="center" style="width: 148px"></td>
                        <td align="center" style="width: 148px"></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td style="width: 171px">
                            <asp:Label ID="lblvariable1Desc" runat="server" Text="[Variable no Seleccionada]"
                                CssClass="variables_indicadores"></asp:Label>
                            <a href="<%=Utils.RelativeWebRoot%>Files/arbolVer.jpg" style="color: Black"
                                class="linkPDF">VER+</a>
                        </td>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblvariable1" runat="server" Font-Bold="true" Style="display: none;"></asp:Label>
                        </td>
                        <td align="center" style="width: 181px"></td>
                        <td align="center" style="width: 148px"></td>
                        <td align="center" style="width: 148px"></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px; height: 18px"></td>
                        <td style="height: 18px"></td>
                        <td align="left" style="width: 131px; height: 18px"></td>
                        <td align="center" style="width: 168px; height: 18px"></td>
                        <td align="center" style="width: 181px; height: 18px"></td>
                        <td align="center" style="width: 148px; height: 18px"></td>
                        <td align="center" style="width: 148px; height: 18px"></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td style="width: 131px" align="left">
                            <asp:Label ID="Label2" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 168px">
                            <asp:Label ID="Label4" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 181px">
                            <asp:Label ID="Label5" runat="server" Text="Real" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:Label ID="Label6" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:Label ID="Label19" runat="server" Text="%" CssClass="variables"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td style="width: 131px" align="center">
                            <asp:DropDownList ID="ddlVariableCausa1" runat="server" Width="120px" CssClass="combo">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 168px" align="center">
                            <asp:TextBox ID="TextBox5" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 181px" align="center">
                            <asp:TextBox ID="TextBox6" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 148px" align="center">
                            <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:TextBox ID="TextBox17" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td style="width: 131px" align="left">
                            <asp:Label ID="Label7" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 168px">
                            <asp:Label ID="Label8" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 181px">
                            <asp:Label ID="Label9" runat="server" Text="Real" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:Label ID="Label10" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:Label ID="Label20" runat="server" Text="%" CssClass="variables"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td style="width: 131px" align="center">
                            <asp:DropDownList ID="ddlVariableCausa2" runat="server" Width="120px" CssClass="combo">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 168px" align="center">
                            <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 181px" align="center">
                            <asp:TextBox ID="TextBox9" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 148px" align="center">
                            <asp:TextBox ID="TextBox10" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:TextBox ID="TextBox18" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="height: 31px; width: 11px;"></td>
                        <td colspan="6" style="height: 31px"></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td style="width: 171px">
                            <asp:Label ID="lblvariable2Desc" runat="server" Text="[Variable no Seleccionada]"
                                CssClass="variables_indicadores"></asp:Label>
                            <a href="<%=Utils.RelativeWebRoot%>Files/arbolVer.jpg" style="color: Black"
                                class="linkPDF">VER+</a>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblvariable2" runat="server" Font-Bold="True" Style="display: none;"></asp:Label>
                        </td>
                        <td style="width: 181px"></td>
                        <td style="width: 148px"></td>
                        <td style="width: 148px"></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px; height: 21px"></td>
                        <td style="height: 21px"></td>
                        <td align="left" style="width: 131px; height: 21px"></td>
                        <td align="center" style="width: 168px; height: 21px"></td>
                        <td align="center" style="width: 181px; height: 21px"></td>
                        <td align="center" style="width: 148px; height: 21px"></td>
                        <td align="center" style="width: 148px; height: 21px"></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="height: 21px; width: 11px;"></td>
                        <td style="height: 21px"></td>
                        <td style="height: 21px; width: 131px;" align="left">
                            <asp:Label ID="Label11" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 168px;" align="center">
                            <asp:Label ID="Label12" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 181px;" align="center">
                            <asp:Label ID="Label13" runat="server" Text="Real" CssClass="variables"></asp:Label>
                        </td>
                        <td style="height: 21px; width: 148px;" align="center">
                            <asp:Label ID="Label14" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px; height: 21px">
                            <asp:Label ID="Label21" runat="server" Text="%" CssClass="variables"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td style="width: 131px" align="center">
                            <asp:DropDownList ID="ddlVariableCausa3" runat="server" Width="120px" CssClass="combo">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 168px" align="center">
                            <asp:TextBox ID="TextBox11" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 181px" align="center">
                            <asp:TextBox ID="TextBox12" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 148px" align="center">
                            <asp:TextBox ID="TextBox13" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:TextBox ID="TextBox19" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="width: 11px"></td>
                        <td></td>
                        <td style="width: 131px" align="left">
                            <asp:Label ID="Label15" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 168px">
                            <asp:Label ID="Label16" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 181px">
                            <asp:Label ID="Label17" runat="server" Text="Real" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:Label ID="Label18" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                        </td>
                        <td align="center" style="width: 148px">
                            <asp:Label ID="Label22" runat="server" Text="%" CssClass="variables"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Arial">
                        <td style="height: 26px; width: 11px;"></td>
                        <td style="height: 26px"></td>
                        <td style="width: 131px; height: 26px" align="center">
                            <asp:DropDownList ID="ddlVariableCausa4" runat="server" Width="120px" CssClass="combo">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 168px; height: 26px" align="center">
                            <asp:TextBox ID="TextBox14" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 181px; height: 26px" align="center">
                            <asp:TextBox ID="TextBox15" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td style="width: 148px; height: 26px" align="center">
                            <asp:TextBox ID="TextBox16" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                        <td align="center" style="width: 148px; height: 26px">
                            <asp:TextBox ID="TextBox20" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <div style="width: 720px; text-align: right; padding-right: 55px">
                    <asp:LinkButton Text="Regresar" runat="server" ID="lnkRegresar" OnClick="lnkRegresar_Click" /><br />
                    <br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
