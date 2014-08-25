<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Variables.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Variables" EnableEventValidation="false" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        jQuery(document).ready(function () {

            $('#variablePopup').dialog({
                autoOpen: false,
                modal: true,
                width: 380,
                resizable: true
            });
            $("#variablePopup").parent().appendTo($("form:first"));

            $('#btnAgregar').click(function (evt) {
                LimpiarControles();
                $('#<%= cboPais.ClientID %>').val($('#<%= cboPaises.ClientID %>').val());
                $('#<%= cboPais.ClientID %>').trigger('change');
                $('#variablePopup').dialog('open');
                return false;
            });

            $('#<%= cboPais.ClientID %>').change(function () {

                var parameters = { accion: 'CargarVariables', codigoPais: $('#<%= cboPais.ClientID %>').val() };

                $.ajax({
                    type: "POST",
                    url: window.location.pathname,
                    data: parameters,
                    async: false,
                    success: function (response) {
                        $('#<%= cboVariable.ClientID %>').html(response);
                    },
                    failure: function (msg) { }
                });
            });

            $('#<%= btnGuardar.ClientID %>').click(function (evt) {
                var parameters = {
                    accion: 'RegistrarVariable',
                    variable: $('#<%= cboVariable.ClientID %>').val(),
                    codigoPais: $('#<%= cboPais.ClientID %>').val()
                };

                $.ajax({
                    type: "POST",
                    url: window.location.pathname,
                    data: parameters,
                    async: false,
                    success: function (response) {
                        $('#variablePopup').dialog('close');
                        alert(response);
                        document.getElementById("<%=btnActualizar.ClientID%>").click();
                    },
                    failure: function (msg) { }
                });

                return false;
            });

            $('#<%= btnCancelar.ClientID %>').click(function (evt) {
                $('#variablePopup').dialog('close');
                LimpiarControles();
                return false;
            });
        });

        function LimpiarControles() {
            document.getElementById("<%= cboPais.ClientID %>").selectedIndex = 0;
            document.getElementById("<%= cboVariable.ClientID %>").selectedIndex = 0;
            $("#<%= lblMensajeError.ClientID %>").empty();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float: left; padding: 10px 0 0 20px">
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/ParametrizacionRanking.jpg" />
    </div>
    <br />
    <br />
    <div style="margin: 40px 0 0 20px; text-align: left;">
        <table>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 50px;">País :
                            </td>
                            <td>
                                <asp:DropDownList ID="cboPaises" runat="server" AutoPostBack="True" Width="150px"
                                    OnSelectedIndexChanged="cboPaises_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CausesValidation="False"
                                    OnClick="btnActualizar_Click" Style="display: none;" />
                            </td>
                            <td style="width: 150px; text-align: right;">
                                <a href="javascript:void(0);" id="btnAgregar">Agregar Variable
                                    <img src="../Images/add.png" />
                                </a>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr style="height: 20px;">
                <td style="text-align: center; vertical-align: top;">
                    <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvVariables" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        Width="600px" OnRowCommand="gvVariablesRowCommand" OnPageIndexChanging="gvVariablesPageIndexChanging">
                        <EmptyDataTemplate>
                            <table width="600px">
                                <tr class="cabecera_indicadores">
                                    <th style="width: 100px;">CÓDIGO
                                    </th>
                                    <th>VARIABLE
                                    </th>
                                    <th style="width: 150px;">OPCIONES
                                    </th>
                                </tr>
                                <tr class="grilla_indicadores">
                                    <td colspan="4" align="center" style="color: #0caed7;">
                                        <br />
                                        No se encontraron Variables para el País
                                        <%= cboPaises.SelectedItem.Text %>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="cabecera_indicadores" />
                        <PagerSettings Mode="NextPreviousFirstLast" />
                        <RowStyle CssClass="grilla_indicadores" Height="20px" />
                        <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                        <Columns>
                            <asp:BoundField HeaderText="CÓDIGO" DataField="CodigoVariable">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="VARIABLE" DataField="DescripcionVariable">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="OPCIONES">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_eliminar" runat="server" BorderStyle="None" CommandArgument='<%# Eval("IDVariablePais") %>'
                                        CommandName="cmd_eliminar" CausesValidation="false" OnClientClick="javascript:return confirm('¿Esta seguro de quitar la Variable?')">
                            <img src="../Images/delete.png" alt="Eliminar" border="0" /></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="variablePopup" style="font-size: 70%; margin: 40px; display: block;" title="Variable del Ranking">
        <table cellspacing="5">
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">País :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="cboPais" runat="server" Width="200px" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">Variable :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="cboVariable" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align: left;" colspan="2">
                    <br />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="100px" />
                    &nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                        Width="100px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
