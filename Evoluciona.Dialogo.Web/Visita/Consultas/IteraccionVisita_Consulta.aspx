<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IteraccionVisita_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.IteraccionVisita_Consulta" Title="Interaccion a Realizar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta Iteraccion Visita</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">



        $(document).ready(function () {

            jQuery(".checkHabilitado .predialogo").click(function (evento) {
                evento.preventDefault();
            });

            jQuery(".checkHabilitado textarea").removeAttr("readOnly");
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");

        });
        function AbrirMensaje() {
            $("#divDialogo").dialog({
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
                                                            Se Observó
                                                            <asp:RadioButton ID="RadioButton1" Text="Si" GroupName="algo" CssClass="texto_descripciones"
                                                                runat="server" Checked='<%# bool.Parse(Eval("bitOservacion").ToString()) %>' />
                                                            <asp:RadioButton ID="RadioButton2" Text="No" GroupName="algo" CssClass="texto_descripciones"
                                                                runat="server" Checked='<%# !bool.Parse(Eval("bitOservacion").ToString()) %>' />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="margin-left: 50px;">
                                                            <asp:TextBox ID="txtRespuesta" Width="500px" Height="100px" runat="server" TextMode="MultiLine"
                                                                CssClass="text_retro" Text='<%# Eval("Respuesta") %>' MaxLength="800" Rows="5"></asp:TextBox>
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
                                    <img src="../../Images/tipo_visita.jpg" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan='2'>&nbsp;<br />
                                    <br />
                                </td>
                            </tr>
                        </table>
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
                <span>Continuar con proceso:</span><br />

                VISITA/DURANTE/COMPETENCIA
            </div>
        </div>
    </form>
</body>
</html>
