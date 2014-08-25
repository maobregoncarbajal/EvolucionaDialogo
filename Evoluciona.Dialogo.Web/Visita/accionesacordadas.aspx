<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="accionesacordadas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.accionesacordadas"
    Title="Acciones Acordadas" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionV.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.maxlength.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;

        $(document).ready(function() {
            
            $('.textoAccionAcordada').maxlength({max: 600, feedbackText: ''});
            $('.textoCampanias').maxlength({max: 600, feedbackText: ''});
            
            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }

            jQuery(".checkDesabilitado").click(function(evento) {
                evento.preventDefault();
            });

            jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");
            jQuery(".checkDesabilitado").attr("readOnly", "readOnly");
        });
       
        function AbrirMensaje()
        {
            $("#divDialogo").dialog({
                modal: true
            });
        }

        function AbrirMensajeDespues()
        {
            $("#divDialogoDespues").dialog({
                modal: true
            });
        }
        
    </script>

    <div style="margin-top: 35px;">
        <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border: solid 1px #c8c8c7;">
            <tr>
                <td style="width: 50px;"></td>
                <td align="left">
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
                                <td style="width: 360px;" class="texto_morado_brand textoAccionAcordada" align="center">
                                    <asp:TextBox runat="server" ID="txtaccionesAcordadas" TextMode="MultiLine" Height="110px"
                                        MaxLength="600" Width="350px" CssClass="inputtext" Text='<%# Eval("AccionesAcordadas1") %>' />
                                </td>
                                <td style="width: 120px" class="texto_morado_brand textoCampanias" align="center">
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
            <tr>
                <td></td>
                <td style="text-align: left; padding-left: 160px;">
                    <br />
                    <asp:Button ID="btngrabar" runat="server" Text="SIGUIENTE" OnClick="btngrabar_Click"
                        CssClass="button" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogo" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DURANTE/NEGOCIO<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="PlanAccionCritica.aspx?indiceM=<%= indexMenuServer%>&indiceSM=3">VISITA/DURANTE/EQUIPOS</a>
        </div>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogoDespues" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DESPUES/NEGOCIO<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="PlanAccionCritica.aspx?indiceM=<%= indexMenuServer%>&indiceSM=3">VISITA/DESPUES/EQUIPOS</a>
        </div>
    </div>
</asp:Content>
