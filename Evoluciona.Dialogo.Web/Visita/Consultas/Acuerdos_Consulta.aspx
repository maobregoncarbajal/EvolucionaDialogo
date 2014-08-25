<%@ Page Language="C#"
    AutoEventWireup="true" CodeBehind="Acuerdos_Consulta.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.Acuerdos_Consulta"
    Title="Acuerdos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Acuerdos</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            jQuery(".checkDesabilitadoPostV .predialogo").click(function (evento) {
                evento.preventDefault();
            });
            jQuery(".checkLectura .predialogo").click(function (evento) {
                evento.preventDefault();
            });
            jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");
            jQuery(".checkLectura textarea").attr("readOnly", "readOnly");

        });

        function AbrirMensaje() {
            $("#divDialogo").dialog({
                modal: true
            });

        }
        function AbrirMensajeDespues() {
            $("#divDialogoDespues").dialog({
                modal: true
            });
        }
    </script>
</head>
<body style="background: white;">
    <form id="form1" runat="server">
        <div style="margin-top: 35px;">
            <table width="100%" border="0" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td align="left">
                        <br />
                        <div style="margin-left: 50px;"><span class="subTituloMorado">Acuerdos.</span></div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <table cellspacing="2" cellpadding="0" border="0" width="1100">
                            <tr>
                                <td style="vertical-align: top">
                                    <asp:DataList ID="dlPreguntas1" runat="server" OnSelectedIndexChanged="dlPreguntas1_SelectedIndexChanged" Width="100%">
                                        <ItemTemplate>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <div style="margin-left: 50px;">
                                                            <asp:Label ID="lblIdPregunta" runat="server" Style="display: none" Text='<%# Eval("intIDPregunta") %>'></asp:Label>
                                                            <img src="../../Images/punto.png" width="10px" alt="" />
                                                            <asp:Label ID="lblPregunta" runat="server" Text='<%# Eval("vchPregunta") %>' CssClass="texto_mensaje"></asp:Label><br />

                                                        </div>
                                                    </td>

                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="margin-left: 100px;">
                                                            <asp:TextBox ID="txtRespuesta" Width="450px" Height="80px" runat="server" TextMode="MultiLine"
                                                                CssClass="text_retro" Text='<%# Eval("vchRespuesta") %>' MaxLength="600" Rows="5"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Post - Visita" CssClass="texto_mensaje"
                                                            Width="70px"></asp:Label>
                                                        <asp:CheckBox CssClass="predialogo" ID="chbPostVisita" runat="server" Text="" TextAlign="Right"
                                                            Checked='<%# bool.Parse(Eval("bitPostVisita").ToString())%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td style="width: 350px; vertical-align: top">
                                    <img src="../../Images/acuerdos.jpg" style="width: 304px" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan='2'></td>
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
            <div style="DISPLAY: none" id="divDialogo" title="PROCESO GRABADO">
                <span>Haz completado tu proceso :</span><br />
                <p>VISITA/DURANTE/COMPETENCIA<br />
                </p>
                <span>Ya puedes realizar la post-visita</span><br />
                <a class="link" href="resumenVisita.aspx">IR A RESUMEN DE VISITA</a>
                <!--<a class="link" href="accionesacordadas.aspx?indiceM=3&indiceSM=1">VISITA/DESPUES/NEGOCIO</a>-->
            </div>
        </div>
        <div class="demo">
            <div style="DISPLAY: none" id="divDialogoDespues" title="PROCESO GRABADO">
                <span>Haz completado tu proceso :</span><br />
                <p>VISITA/DESPUES/COMPETENCIA<br />
                </p>

                <a class="link" href="resumenVisita.aspx">IR A RESUMEN DE VISITA</a>
            </div>
        </div>
    </form>
</body>

</html>




