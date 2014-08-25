<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="IteraccionVisita.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.IteraccionVisita"
    Title="Interaccion a Realizar" %>

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
            
            $('.textoRespuesta').maxlength({max: 800, feedbackText: ''});
            
            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            jQuery(".checkHabilitado .predialogo").click(function(evento)
            {
                evento.preventDefault();
            }); 
     
            //jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkHabilitado").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");

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
                <td style="vertical-align: top">
                    <br />
                    <table cellspacing="2" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 350px; vertical-align: top">
                                <asp:DataList ID="dlPreguntas1" runat="server" CellPadding="0">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left">
                                                    <div style="margin-left: 50px;">
                                                        <asp:Label ID="lblCodigoPlanAnual" runat="server" Style="display: none" Text='<%# Eval("intcodigoPlanAnual") %>'></asp:Label>
                                                        &nbsp;<asp:Label ID="lblDescripcionPlanAnual" runat="server" Width="300px" BackColor="#a75ba5"
                                                            Height="20px" CssClass="tituloPantalla" Text='<%# Eval("vchCompetencia") %>'></asp:Label>
                                                        &nbsp;<span class='texto_descripciones'>Se Observó</span>
                                                        <asp:RadioButton ID="RadioButton1" Text="Si" GroupName="algo" CssClass="texto_descripciones"
                                                            runat="server" Checked='<%# bool.Parse(Eval("bitOservacion").ToString()) %>' />
                                                        <asp:RadioButton ID="RadioButton2" Text="No" GroupName="algo" CssClass="texto_descripciones"
                                                            runat="server" Checked='<%# !bool.Parse(Eval("bitOservacion").ToString()) %>' />
                                                        <br />
                                                        <br />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div style="margin-left: 50px;" class="texto_descripciones">
                                                        <asp:Label ID="lblPregunta" runat="server" Width="180px" Text="¿Cómo lo observó?"
                                                            CssClass="texto_mensaje"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="margin-left: 50px;">
                                                        <asp:TextBox ID="txtRespuesta" Width="500px" Height="100px" runat="server" TextMode="MultiLine"
                                                            CssClass="text_retro textoRespuesta" Text='<%# Eval("Respuesta") %>' MaxLength="800" Rows="5"></asp:TextBox>
                                                        <br />

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">


                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                            <td style="width: 390px; vertical-align: top; text-align: right">
                                <img src="../Images/tipo_visita.jpg" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan='2'>
                                <asp:Button ID="Button1" runat="server" CssClass="button" Text="SIGUIENTE" OnClick="btnGrabar_Click" />
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
    </div>
    <div class="demo">
        <div style="DISPLAY: none" id="divDialogo" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DURANTE/COMPETENCIA<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="Acuerdos.aspx?indiceM=<%= indexMenuServer%>&indiceSM=<%= indexSubMenu %>">VISITA/DURANTE/COMPETENCIA</a>
        </div>
    </div>
</asp:Content>
