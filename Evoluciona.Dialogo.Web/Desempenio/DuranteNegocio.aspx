<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="DuranteNegocio.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.DuranteNegocio" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacion.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.maxlength.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        var indexMenu = <%= indexMenuServer %>;
        var indexSubMenu = <%= indexSubMenu %>;
        var mostrarMensaje = <%= esCorrecto %>;
        var estado = <%= estadoProceso %>;
        var soloLectura = <%=readOnly %>;
        var avanze = <%=porcentaje %>;
        var abortar = 0;

        jQuery(document).ready(function () {

            $('#<%=txtPlanAccion1.ClientID %>').maxlength({max: 2000, feedbackText: ''});
            $('#<%=txtPlanAccion2.ClientID %>').maxlength({max: 2000, feedbackText: ''});

            $('#<%=txtZonas1.ClientID %>').maxlength({max: 200, feedbackText: ''});
            $('#<%=txtZonas2.ClientID %>').maxlength({max: 200, feedbackText: ''});

            jQuery("#noIngresar").dialog({
                autoOpen : false,
                modal: true,
                title: "NAVEGACIÓN NO PERMITIDA" ,
                close:function(event,ui)
                {
                    window.location = "AntesNegocio.aspx";
                },
                buttons:
                {
                    Ok: function () {
                        jQuery(this).dialog("close");
                        window.location = "AntesNegocio.aspx";
                    }
                }
            });

            if(indexMenu != null && indexMenu != 0){
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }

            if(estado != 1)
            {
                jQuery("#<%=txtPlanAccion1.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=txtPlanAccion2.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=txtZonas1.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=txtZonas2.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=txtCampania1.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=txtCampania2.ClientID %>").attr("readOnly", "readOnly");

                if(indexMenu == 2)
                    jQuery("#<%=btnGuardar.ClientID %>").css("display", "none");
            }
            else
            {
                if(soloLectura == 1 || indexMenu == 3)
                    abortar = 1;
            }

            if(estado == 2 && indexMenu == 3)
            {
                abortar = 1;
            }

            if(estado == 3)
            {
                if(indexMenu == 3)
                {
                    if(soloLectura == 0)
                    {
                        jQuery("#<%=cboEstadoIndicador1.ClientID %>").css("display", "block");
                        jQuery("#<%=cboEstadoIndicador2.ClientID %>").css("display", "block");
                    }
                    else
                    {
                        abortar = 1;
                    }
                }
            }

            MostrarAvance(avanze);

            jQuery("#msgProceso").dialog({
                autoOpen : false,
                modal: true,
                title: "PROCESO COMPLETADO",
                open: function(event, ui) {
                    jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                }
            });

            jQuery("#divMensaje").dialog({
                autoOpen : false,
                modal: true,
                title: "ERROR DE VALIDACIÓN" ,
                buttons:
                {
                    Ok: function () {
                        jQuery(this).dialog("close");
                    }
                }
            });

            if(mostrarMensaje == 1)
            {
                mostrarMensaje = 0;
                jQuery("#msgProceso").dialog("open");
            }

            if(abortar == 1)
            {
                jQuery("#noIngresar").dialog("open");
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="noIngresar" style="font-size: 9pt">
        No es Posible ingresar a la P&aacute;gina debido a que aun no existen datos para
        su evaluaci&oacute;n
    </div>
    <div id="divMensaje" style="font-size: 9pt">
        Se deben ingresar valores en las cajas de texto
    </div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        Has completado tu Proceso:<br />
        DIÁLOGO/<asp:Label Text="DURANTE" runat="server" ID="lblAccion" />/NEGOCIO
        <br />
        <br />
        Haz click aquí para continuar con el proceso:
        <asp:HyperLink runat="server" ID="hlkUrl" />
    </div>
    <div style="margin: 35px 0 0 55px; width: 850px; text-align: left;">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td colspan="1" style="width: 41px; height: 19px"></td>
                <td colspan='4' style="height: 19px;">Ingrese el detalle de las
                    <asp:Label Text="ZONAS " runat="server" CssClass="texto_Negro" ID="lblDescripcionLugarRol" />
                    a trabajar y el <span class="texto_Negro">Plan de Acci&oacute;n</span> definido
                    para las variables
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
                <td style="vertical-align: middle;">&nbsp;<asp:Label ID="lblVariable1" runat="server" CssClass="textoVarEnfoque" Text="Variable no Definida"
                    Width="165px"></asp:Label>
                    <asp:Label runat="server" ID="lblVariableGeneral1" Text="Variable no Definida" Width="113px"
                        Style="display: none;"></asp:Label>
                </td>
                <td style="width: 200px; vertical-align: middle;">
                    <table>
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
                <td style="width: 147px; text-align: center" class="texto_morado_brand">
                    <asp:Literal Text="Zonas" runat="server" ID="ltUbicacionRol_1" /><br />
                    <asp:TextBox runat="server" ID="txtZonas1" TextMode="MultiLine" Height="120px" MaxLength="600"
                        Width="90px" CssClass="inputtext"></asp:TextBox>
                </td>
                <td style="width: 356px; text-align: center" class="texto_morado_brand">&nbsp;Plan de Negocio<br />
                    <asp:TextBox runat="server" ID="txtPlanAccion1" TextMode="MultiLine" Height="120px"
                        CssClass="inputtext" Width="320px" MaxLength="2000"></asp:TextBox>
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
                <td style="width: 37px; vertical-align: middle;">&nbsp;
                    <asp:Label ID="lblVariableGeneral2" runat="server" Text="Variable no Definida" CssClass="textoVarEnfoque"
                        Width="163px"></asp:Label>
                    <asp:Label runat="server" ID="lblVariable2" Text="Variable no Definida" Width="118px"
                        Style="display: none;"></asp:Label>
                </td>
                <td style="width: 200px; vertical-align: middle;">
                    <table>
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
                <td style="width: 147px; text-align: center" class="texto_morado_brand">
                    <asp:Literal Text="Zonas" runat="server" ID="ltUbicacionRol_2" /><br />
                    <asp:TextBox runat="server" ID="txtZonas2" TextMode="MultiLine" Height="120px" MaxLength="600"
                        Width="90px" CssClass="inputtext"></asp:TextBox>
                </td>
                <td style="width: 356px; text-align: center" class="texto_morado_brand">Plan de Negocio<br />
                    <asp:TextBox runat="server" ID="txtPlanAccion2" TextMode="MultiLine" Height="120px"
                        CssClass="inputtext" Width="320px" MaxLength="2000"></asp:TextBox>
                    <input type="button" id="Button3" value="Agregar" class="button" style="display: none" />
                </td>
            </tr>
            <tr>
                <td style="width: 41px"></td>
                <td style="width: 37px"></td>
                <td style="vertical-align: top; width: 144px;">
                    <span class="textoEtiquetas"></span>&nbsp;
                </td>
                <td style="vertical-align: top; width: 147px;">
                    <span class="textoEtiquetas"></span>
                    <br />
                    <div style="text-align: right; margin-right: 10px">
                        &nbsp;
                    </div>
                </td>
                <td style="vertical-align: top; width: 356px">
                    <br />
                    <asp:Button runat="server" CssClass="btnGuardarStyle" ID="btnGuardar" OnClick="btnGuardar_Click"
                        Text="SIGUIENTE" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
