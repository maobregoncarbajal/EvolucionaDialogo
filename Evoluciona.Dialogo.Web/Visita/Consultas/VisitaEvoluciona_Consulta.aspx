<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitaEvoluciona_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.VisitaEvoluciona_Consulta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Visita Evoluciona</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        jQuery(document).ready(function () {

            jQuery(".checkDesabilitado, .checkDesabilitado textarea").click(function (evento) {
                evento.preventDefault();
            });
            jQuery(".checkDesabilitadoInicio").click(function (evento) {
                evento.preventDefault();
            });
            jQuery(".checkDesabilitado textarea").attr("readOnly", "readOnly");
            jQuery(".checkHabilitado textarea").removeAttr("readOnly");
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
                                            <asp:RadioButton ID="RadioButton1" CssClass="texto_descripciones" Text="Si" GroupName="algo" runat="server" Checked='<%# bool.Parse(Eval("bitEvoluciona").ToString()) %>' />
                                            <asp:RadioButton ID="RadioButton2" CssClass="texto_descripciones" Text="No" GroupName="algo" runat="server" Checked='<%# !bool.Parse(Eval("bitEvoluciona").ToString()) %>' />

                                        </td>
                                        <td style="width: 370px;" align="center">
                                            <asp:TextBox ID="txtRespuesta" Width="350px" Height="80px" runat="server" TextMode="MultiLine"
                                                CssClass="text_retro" Text='<%# Eval("Respuesta") %>' MaxLength="600" Rows="5"></asp:TextBox>
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
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div class="demo">
            <div style="DISPLAY: none" id="divDialogo" title="PROCESO GRABADO">
                <span>Haz completado tu proceso :</span><br />
                <p>VISITA/DURANTE/COMPETENCIA<br />
                </p>
                <span>Continuar con proceso:</span><br />

            </div>
        </div>

    </form>
</body>

</html>







