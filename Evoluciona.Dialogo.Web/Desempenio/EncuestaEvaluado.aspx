<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="EncuestaEvaluado.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.EncuestaEvaluado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        var esCorrecto = <%= Correcto %>;

        jQuery(document).ready(function () {

            if(esCorrecto == 1)
            {
                jQuery("#divMensaje").text("Se Completo su Encuesta.. Gracias..");

                jQuery("#divMensaje").dialog({
                    modal: true,
                    title: "ENCUESTA" ,
                    buttons:
                    {
                        Ok: function () {
                            jQuery(this).dialog("close");
                            window.location = "ResumenProceso.aspx";
                        }
                    }
                });
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMensaje">
    </div>
    <div style="margin-left: 35px; margin-right: auto; margin-top: 15px;">
        <table style="width: 870px;">
            <tr>
                <td style="background-color: #60497B; height: 30px; color: White; font-weight: bolder; vertical-align: middle">ENCUESTA DE SATISFACCIÓN PROCESO: DIÁLOGO EVOLUCIONA
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; background-color: #CCC0DA" cellspacing="10">
                        <tr style="height: 45px; vertical-align: middle">
                            <td style="width: 55px">Nº
                            </td>
                            <td>Pregunta
                            </td>
                            <td>Totalmente en Desacuerdo
                            </td>
                            <td>En Desacuerdo
                            </td>
                            <td>Algo de Acuerdo
                            </td>
                            <td>De Acuerdo
                            </td>
                            <td>Totalmente de Acuerdo
                            </td>
                        </tr>
                        <asp:Repeater ID="rpEncuesta" runat="server">
                            <ItemTemplate>
                                <tr style="height: 45px; vertical-align: middle">
                                    <td>
                                        <%# Eval("IdPregunta") %>
                                    </td>
                                    <td style="text-align: justify;">
                                        <%# Eval("Pregunta") %>
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="RadioButton5" GroupName="encuesta" Text="" runat="server" Checked='<%# int.Parse(Eval("Respuesta").ToString()) == 1 %>' />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="RadioButton1" GroupName="encuesta" Text="" runat="server" Checked='<%# int.Parse(Eval("Respuesta").ToString()) == 2 %>' />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="RadioButton2" GroupName="encuesta" Text="" runat="server" Checked='<%# int.Parse(Eval("Respuesta").ToString()) == 3 %>' />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="RadioButton3" GroupName="encuesta" Text="" runat="server" Checked='<%# int.Parse(Eval("Respuesta").ToString()) == 4 %>' />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="RadioButton4" GroupName="encuesta" Text="" runat="server" Checked='<%# int.Parse(Eval("Respuesta").ToString()) == 5 %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Button runat="server" CssClass="btnGuardarStyle" ID="btnGuardar" OnClick="btnGuardar_Click"
            Text="GUARDAR" />
    </div>
</asp:Content>
