<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="Acuerdos.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.Acuerdos"
    Title="Acuerdos" %>

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
            
            $('.textoRespuesta').maxlength({max: 600, feedbackText: ''});
            
            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            jQuery(".checkDesabilitadoPostV .predialogo").click(function(evento)
            {
                evento.preventDefault();
            });
            jQuery(".checkLectura .predialogo").click(function(evento)
            {
                evento.preventDefault();
            });
            jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");
            jQuery(".checkLectura textarea").attr("readOnly", "readOnly");

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
        <table width="100%" border="0" style="border: solid 1px #c8c8c7;">
            <tr>
                <td align="left">
                    <br />
                    <div style="margin-left: 50px;">
                        <span class="subTituloMorado">Acuerdos.</span>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table cellspacing="2" cellpadding="0" border="0" width="1100">
                        <tr>
                            <td style="vertical-align: top">
                                <asp:DataList ID="dlPreguntas1" runat="server" OnSelectedIndexChanged="dlPreguntas1_SelectedIndexChanged"
                                    Width="100%">
                                    <ItemTemplate>
                                        <table border="0" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <div style="margin-left: 50px;">
                                                        <asp:Label ID="lblIdPregunta" runat="server" Style="display: none" Text='<%# Eval("intIDPregunta") %>'></asp:Label>
                                                        <img src="../Images/punto.png" width="10px" alt="" />
                                                        <asp:Label ID="lblPregunta" runat="server" Text='<%# Eval("vchPregunta") %>' CssClass="texto_mensaje"></asp:Label><br />
                                                    </div>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="margin-left: 100px;">
                                                        <asp:TextBox ID="txtRespuesta" Width="450px" Height="80px" runat="server" TextMode="MultiLine"
                                                            CssClass="text_retro textoRespuesta" Text='<%# Eval("vchRespuesta") %>' MaxLength="600" Rows="5"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="" CssClass="texto_mensaje" Width="50px"></asp:Label>
                                                    <asp:CheckBox CssClass="predialogoAcuerdo" ID="chbPostVisita" runat="server" Text="Post - Visita "
                                                        TextAlign="Left" Checked='<%# bool.Parse(Eval("bitPostVisita").ToString())%>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                            <td style="width: 350px; vertical-align: top">
                                <img src="../Images/acuerdos.jpg" style="width: 304px" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan='2'>
                                <asp:Button ID="btnGrabar" runat="server" CssClass="button" Text="SIGUIENTE" OnClick="btnGrabar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
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
            <a class="link" href="accionesacordadas.aspx?indiceM=2&indiceSM=2">VISITA/DURANTE/NEGOCIO</a>
        </div>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogoDespues" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DESPUES/COMPETENCIA<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="accionesacordadas.aspx?indiceM=3&indiceSM=2">VISITA/DESPUES/NEGOCIO</a>
        </div>
    </div>
</asp:Content>
