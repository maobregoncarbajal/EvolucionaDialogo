<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="VisitaEvoluciona.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.VisitaEvoluciona"
    Title="Visita Evoluciona" %>

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

        jQuery(document).ready(function() {

            $('.textoRespuesta').maxlength({ max: 600, feedbackText: '' });

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            jQuery(".checkDesabilitado").click(function(evento)
            {
                evento.preventDefault();
            });
            jQuery(".checkDesabilitadoInicio").click(function(evento)
            {
                evento.preventDefault();
            });
            jQuery(".checkHabilitado").removeAttr("readOnly");
            //jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");
            jQuery(".checkDesabilitado").attr("readOnly", "readOnly");
        });
      
        function AbrirMensaje()
        {
            $("#divDialogo").dialog({
                modal: true
            });
        }
    </script>

    <div style="margin-top: 35px;">
        <table width="100%" border="0" style="border: solid 1px #c8c8c7;">
            <tr>
                <td style="width: 10px"></td>
                <td align="left">
                    <br />
                    <span class="subTituloMorado">Visita Evoluciona.</span><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 10px"></td>
                <td style="width: 100%">
                    <asp:DataList ID="dlPreguntas1" runat="server" RepeatColumns="2">
                        <AlternatingItemStyle CssClass="grilla_alterna_indicadores" />
                        <ItemTemplate>
                            <table cellpadding='0' cellspacing='0' border='0'>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblIdPregunta" runat="server" Style="display: none" Text='<%# Eval("intIDPregunta") %>'></asp:Label><br />
                                        <asp:Label ID="lblPregunta" runat="server" Width="200px" Text='<%# Eval("vchPregunta") %>'
                                            CssClass="texto_descripciones"></asp:Label><br />
                                        <br />
                                        <asp:RadioButton ID="RadioButton1" CssClass="texto_descripciones" Text="Si" GroupName="algo"
                                            runat="server" Checked='<%# bool.Parse(Eval("bitEvoluciona").ToString()) %>' />
                                        <asp:RadioButton ID="RadioButton2" CssClass="texto_descripciones" Text="No" GroupName="algo"
                                            runat="server" Checked='<%# !bool.Parse(Eval("bitEvoluciona").ToString()) %>' />
                                    </td>
                                    <td style="width: 370px;" align="center">
                                        <asp:TextBox ID="txtRespuesta" Width="350px" Height="80px" runat="server" TextMode="MultiLine"
                                            CssClass="text_retro textoRespuesta" Text='<%# Eval("Respuesta") %>' MaxLength="600" Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='2' style="height: 15px"></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td style="width: 10px"></td>
                <td style="height: 21px">
                    <asp:Button ID="btnGrabar" runat="server" Text="SIGUIENTE" OnClick="btnGrabar_Click"
                        CssClass="button" /><br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogo" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DURANTE/COMPETENCIA<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="IteraccionVisita.aspx?indiceM=<%=indexMenuServer%>&indiceSM=1">VISITA/DURANTE/COMPETENCIA</a>
        </div>
    </div>
</asp:Content>
