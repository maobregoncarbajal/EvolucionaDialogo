<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Interaccion_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.Interaccion_Consulta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Interaccion</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet"
        type="text/css" />

    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            jQuery(".checkdesabilitado").click(function (evento) {
                evento.preventDefault();
            });
        });

        function AbrirMensaje() {
            $("#divDialogo").dialog({
                modal: true,
                buttons:
                    {
                        Ok: function () {
                            jQuery(this).dialog("close");
                        }
                    }
            });
        }
        function AbrirMensajeError() {
            $("#divMensajeError").dialog({
                modal: true,
                buttons: {
                    'Aceptar': function () {
                        $(this).dialog('close');
                    }
                },
                close: function (ev, ui) {
                }
            });
        }
    </script>

</head>
<body style="background: white;">
    <form id="form1" runat="server">
        <div style="margin-top: 35px;">
            <table width="100%" border="0" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td style="width: 30px"></td>
                    <td style="height: 500px; text-align: left">
                        <span class="subTituloMorado">Interacción a realizar.</span><br />
                        <br />
                        <table cellspacing="2" cellpadding="0" border="0">
                            <tr>
                                <td style="width: 320px; vertical-align: top">
                                    <div id="divContenedorChecks" style="padding-left: 10px; border: solid 1px #c8c8c7; margin-top: 20px;">
                                        <asp:TreeView ID="TreeViewCheck" NodeStyle-VerticalPadding="2px" runat="server" ShowCheckBoxes="All"
                                            ShowExpandCollapse="False" ForeColor="#6a288a" Font-Size="13px" Font-Names="Arial"
                                            Width="300px">
                                            <NodeStyle VerticalPadding="2px" CssClass="chkAlternativa" />
                                        </asp:TreeView>
                                        <asp:HiddenField ID="hidPlanAnual" runat="server" />
                                        <br />
                                    </div>
                                    <div style="margin-left: 10px;">
                                        <asp:Panel ID="pnlTodo" runat="server" Visible="true">
                                            <table border="0">
                                                <tr>
                                                    <td class="subtituloPlan">Alternativas Adicionales
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtOtros" runat="server" CssClass="inputtext" Width="300px" TextMode="MultiLine"
                                                            Height="80px" MaxLength="600" Rows="5" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                                <td style="width: 20px;"></td>
                                <td style="width: 400px; vertical-align: top">
                                    <table border='0' cellpadding='0' style="width: 100%">
                                        <tr>
                                            <td>
                                                <div class="subtituloPlan" style="margin-bottom: 5px;">
                                                    Objetivo de la Visita
                                                </div>
                                                <asp:TextBox ID="txtObjetivosVisita" runat="server" CssClass="inputtext" Width="400px"
                                                    TextMode="MultiLine" Height="136px" MaxLength="600" Rows="5"></asp:TextBox>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="subtituloPlan" style="margin-bottom: 5px;">
                                                    Medición de competencias
                                                </div>
                                                <asp:GridView ID="grvMedicionCompetencia" Width="400px" runat="server" AutoGenerateColumns="False">
                                                    <HeaderStyle CssClass="cabecera_indicadores" />
                                                    <RowStyle CssClass="grilla_indicadores" />
                                                    <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Competencia" DataField="vchCompetencia">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="% de Avance" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PorcentajeAvance" runat="server" Text='<%# GetDescripcion(Eval("PorcentajeAvance")) %>'
                                                                    CssClass="clsChhIndicadores" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Enfoque">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" CssClass="clsChhIndicadores" name="CheckBox1" runat="server"
                                                                    AutoPostBack="false" Checked='<%# bool.Parse(Eval("Enfoque").ToString())%>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 327px">
                                    <img src="../../Images/image_jefe.jpg" style="width: 312px; height: 528px" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="hidden" id="hdOtro" name="hdOtro" style="width: 32px" />
                                    <asp:HiddenField ID="hdEsEditable" runat="server" Value="" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="display: none" id="divMensajeError" title="Información">
            <p>
                <asp:Label runat="server" ID="lblMensajes" Text="" CssClass="texto_mensaje"></asp:Label>
            </p>
        </div>
        <div class="demo">
            <div style="display: none" id="divDialogo" title="INFORMACION">
                <br />
                <span>No tienes Visitas registradas en este Periodo</span>
                <br />
            </div>
        </div>
    </form>
</body>
</html>
