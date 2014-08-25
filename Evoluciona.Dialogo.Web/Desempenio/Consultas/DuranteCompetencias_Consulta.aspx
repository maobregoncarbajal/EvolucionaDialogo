<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuranteCompetencias_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.DuranteCompetencias_Consulta" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            jQuery("#<%=dlPreguntas1.ClientID%> textarea, #<%=txtOtros.ClientID %>").attr("readOnly", "readOnly");
            jQuery("#<%=lnkMarcarTodos.ClientID %>").css("display", "none");
            jQuery("#<%=TreeViewCheck.ClientID %> input").click(function (event) {
                event.preventDefault();
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 15px 0 0 55px; text-align: left">
        <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
        <table border="0" width="100%" style="border: solid 1px #c8c8c7;">
            <tr>
                <td style="height: 21px" align="left">
                    <table>
                        <tr>
                            <td align="left" class="texto_descripciones" colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Ingresa la retroalimentación dada a
                                la
                                <asp:Label runat="server" ID="lblDescripcionRol" Text="Gerente de Region"></asp:Label>
                                sobre su Plan de Desarrollo de Competencias.
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="texto_descripciones" colspan="2" style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="pregunta" style="width: 125px">
                            </td>
                            <td align="left" class="pregunta">
                                <br />
                                <asp:Label ID="lblEtiqueta" runat="server" Text="   Seleccione una Competencia :"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="12px" ForeColor="#00ACEE"></asp:Label>
                                <asp:DropDownList ID="ddlCompetencia" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="ddlCompetencia_SelectedIndexChanged" CssClass="combo">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCompetencia1" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">                               
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="pregunta" style="width: 125px">
                            </td>
                            <td align="left" class="pregunta" style="width: 500px">
                                <div id="divPlan1">
                                    <asp:DataList ID="dlPreguntas1" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdPregunta" runat="server" Style="display: none" Text='<%# Eval("IdPregunta") %>'></asp:Label><br />
                                            <asp:Label ID="lblPregunta" runat="server" Text='<%# Eval("DescripcionPregunta") %>'></asp:Label><br />
                                            <asp:TextBox ID="txtRespuesta" Width="400px" Height="80px" runat="server" TextMode="MultiLine"
                                                CssClass="text_retro" Text='<%# Eval("Respuesta") %>' MaxLength="600" Rows="5"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <asp:Panel ID="pnlMensaje" runat="server" Width="350px" Visible="false">
                                    <h1>
                                        Seleccione una opci&oacute;n</h1>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px">
                            </td>
                            <td>
                                <table width="100%" style="border: solid 1px #c8c8c7;">
                                    <tr>
                                        <td align="left">
                                            &nbsp; &nbsp;
                                            <asp:TreeView ID="TreeViewCheck" NodeStyle-VerticalPadding="2px" runat="server" ShowCheckBoxes="All"
                                                ShowExpandCollapse="False" ForeColor="#6a288a" Font-Size="13px" Font-Names="Arial">
                                            </asp:TreeView>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtOtros" MaxLength="200"
                                                TextMode="MultiLine" Height="51px" Width="319px"></asp:TextBox>
                                            <br />
                                            <br />
                                            <asp:LinkButton runat="server" ID="lnkMarcarTodos" Text="(Marca Todas las alternativas)"
                                                ForeColor="#00acee" Font-Names="Arial" Font-Bold="true" Font-Size="12px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="hidden" id="hdOtro" name="hdOtro" />
                            </td>
                            <td>                               
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
