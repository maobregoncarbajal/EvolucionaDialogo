<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicionCompetencia_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.MedicionCompetencia_Consulta" Title="Medicion de competencias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Medicion Competencia</title>

    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />
    <link href="../../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="../../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            jQuery(".checkdesabilitado, .checkdesabilitado textarea").click(function (evento) {
                evento.preventDefault();
            });

            jQuery(".checkdesabilitado textarea").attr("readOnly", "readOnly");


        }
         );


        function AbrirMensaje() {
            $("#divDialogo").dialog({
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
        function AbrirNavegacion() {

            $("#divNavegacion").dialog({
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
                    <td style="vertical-align: text-top; text-align: left">
                        <br />
                        <span class="subTituloMorado">Medición de competencias.</span><br />
                        <br />
                        <table cellspacing="2" cellpadding="0" border="0">
                            <tr>
                                <td style="width: 350px; vertical-align: top; text-align: center">
                                    <asp:GridView ID="grvMedicionCompetencia" Width="600px" runat="server" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="cabecera_indicadores" />
                                        <RowStyle CssClass="grilla_indicadores" />
                                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Descripcion Competencia" DataField="vchCompetencia">
                                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="% de Avance" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PorcentajeAvance" runat="server" Text='<%# GetDescripcion(Eval("PorcentajeAvance")) %>'
                                                        CssClass="clsChhIndicadores" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Enfoque">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" CssClass="clsChhIndicadores" Width="200" name="CheckBox1"
                                                        runat="server" AutoPostBack="false" Checked='<%# bool.Parse(Eval("Enfoque").ToString())%>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    &nbsp;<br />
                                    <br />
                                </td>
                                <td style="width: 380px; vertical-align: top">
                                    <img src="../../Images/image_desarrollate.jpg" alt='' /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center"></td>
                </tr>
            </table>
        </div>
        <div style="display: none" id="divDialogo" title="Información">
            <p>
                <asp:Label runat="server" ID="lblMensajes" Text="" CssClass="texto_mensaje"></asp:Label>
            </p>
        </div>
        <div style="display: none" id="divNavegacion" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/ANTES/COMPETENCIA<br />
            </p>
            <span>Continuar con proceso:</span><br />

        </div>
    </form>
</body>
</html>
