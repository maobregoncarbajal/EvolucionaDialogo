﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resumen_Durante_Despues.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Admin.Controls.Resumen_Durante_Despues1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        var indexActual = -1;

        jQuery(document).ready(function () {

            var estado = '<%= EstadoProceso %>';
        if (estado == "DESPUES") {
            jQuery("#<%=lblEstadoIndicador1.ClientID %>").css("display", "block");
            jQuery("#<%=lblEstadoIndicador2.ClientID %>").css("display", "block");
            jQuery("#<%=lblEstadoIndicador3.ClientID %>").css("display", "inline");
        }
    });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Estos son los planes acordados en el Diálogo de periodo 
                    <asp:Literal ID="litPeriodo" runat="server"></asp:Literal>:
                    
                    <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="font-size: 12pt; font-weight: bold; color: #00B0F0">
                            <asp:Literal ID="litEstadoNegocio" runat="server" Text="DURANTE" />&nbsp;NEGOGIO</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 600px; border-collapse: collapse;" border="0" cellspacing="0">
                            <tr>
                                <td style="height: 20px">
                                    <div id="divTextoDDNegocio" class="texto_morado_brand">
                                        Detalle de las
                                    <asp:Label Text="ZONAS " runat="server" ID="lblDescripcionLugarRol" />
                                        a trabajar y el plan de acción definido para las variables.
                                    </div>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divTablaDDNegocio">
                                        <table style="width: 100%; border-collapse: collapse;" border="1" rules="all" cellspacing="0">
                                            <tr class="cabecera_indicadores">
                                                <th>Variable
                                                </th>
                                                <th style="width: 130px;">Variables Causales
                                                </th>
                                                <th style="width: 100px">
                                                    <asp:Label Text="Zonas " runat="server" CssClass="texto_Negro" ID="lblDescripcionLugarRol1" />
                                                </th>
                                                <th style="width: 250px;">Plan de Acción
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblVariableGeneral1" runat="server" Text="Variable no Definida" CssClass="textoVarEnfoque"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblEstadoIndicador1" runat="server" Text="" CssClass="textoVarEnfoque_hija" Style="display: none" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Variable Causa no Definida" CssClass="textoVarEnfoque_hija"></asp:Label><br />
                                                    <asp:Label ID="Label7" runat="server" Text="Variable Causa no Definida" CssClass="textoVarEnfoque_hija"></asp:Label>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label runat="server" ID="txtZonas1" Font-Size="9pt" CssClass="textoVarEnfoque_hija"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPlanAccion1" Font-Size="9pt" CssClass="textoVarEnfoque_hija"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblVariableGeneral2" runat="server" Text="Variable no Definida" CssClass="textoVarEnfoque"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblEstadoIndicador2" runat="server" Text="" CssClass="textoVarEnfoque_hija" Style="display: none" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" CssClass="textoVarEnfoque_hija" Text="Variable Causa no Definida"></asp:Label><br />
                                                    <asp:Label ID="Label9" runat="server" CssClass="textoVarEnfoque_hija" Text="Variable Causa no Definida"></asp:Label>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label runat="server" ID="txtZonas2" Font-Size="9pt" CssClass="textoVarEnfoque_hija"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPlanAccion2" Font-Size="9pt" CssClass="textoVarEnfoque_hija"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="font-size: 12pt; font-weight: bold; color: #00B0F0">
                            <asp:Literal ID="litEstadoEquipo" runat="server" Text="DURANTE" />&nbsp;EQUIPOS
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="divTablaDDEquipo">
                            <asp:GridView ID="gvEquipo" runat="server" AutoGenerateColumns="False">
                                <HeaderStyle CssClass="cabecera_indicadores" />
                                <RowStyle CssClass="grilla_indicadores" />
                                <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                <Columns>
                                    <asp:BoundField HeaderText="Colaborador" DataField="NombreCritica">
                                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Variable" DataField="Variable">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Plan de Acción" DataField="PlanAccion">
                                        <ItemStyle HorizontalAlign="Center" Width="220px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Estado" DataField="Estado">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="font-size: 12pt; font-weight: bold; color: #00B0F0">
                            <asp:Literal ID="litEstadoCompetencias" runat="server" Text="DURANTE" />&nbsp;COMPETENCIAS
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="text-align: left">
                            <table border="0" width="100%">
                                <tr>
                                    <td style="height: 21px" align="left">
                                        <table>
                                            <tr>
                                                <td align="left" class="texto_descripciones">
                                                    <div id="divTextoDDCompetencias1">
                                                        Retroalimentación dada a la
                                                    <asp:Label runat="server" ID="lblDescripcionRol" Text="Gerente de Región"></asp:Label>
                                                        &nbsp;sobre su plan de desarrollo.
                                                    </div>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="pregunta">
                                                    <div id="divTextoDDCompetencias2">
                                                        <asp:Label ID="lblEtiqueta" runat="server" Text="   Competencia :"
                                                            Font-Bold="True" Font-Size="12px" ForeColor="#00ACEE"></asp:Label>
                                                        &nbsp; 
                                                    <asp:Label ID="lblCompentencia" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblEstadoIndicador3" runat="server" Text="" CssClass="textoVarEnfoque_hija" Style="display: none" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="pregunta">
                                                    <div id="divTablaPreguntas">
                                                        <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False">
                                                            <HeaderStyle CssClass="cabecera_indicadores" />
                                                            <RowStyle CssClass="grilla_indicadores" />
                                                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Pregunta" DataField="DescripcionPregunta">
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Respuesta" DataField="Respuesta">
                                                                    <ItemStyle HorizontalAlign="Left" Width="450px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
