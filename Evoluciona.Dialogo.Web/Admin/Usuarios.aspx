<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Usuarios" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        jQuery(document).ready(function () {

            $('#adminsitradorPopup').dialog({
                autoOpen: false,
                modal: true,
                width: 440,
                hight: 440,
                resizable: true
            });
            $("#adminsitradorPopup").parent().appendTo($("form:first"));

            $('#btnAgregar').click(function (evt) {
                LimpiarControles();
                $('#adminsitradorPopup').dialog('open');
                return false;
            });

            $('#<%= btnCancelar.ClientID %>').click(function (evt) {
                $('#adminsitradorPopup').dialog('close');
                LimpiarControles();
                return false;
            });
        });

        function LimpiarControles() {
            $("#<%= txtCodigo.ClientID %>").val("");
            $("#<%= txtNombre.ClientID %>").val("");
            $("#<%= txtClave.ClientID %>").val("");
            document.getElementById("<%= cboPais.ClientID %>").selectedIndex = 0;
            document.getElementById("<%= cboAdmin.ClientID %>").selectedIndex = 0;
            document.getElementById("<%= cboEstado.ClientID %>").selectedIndex = 0;
            $("#<%= lblMensajeError.ClientID %>").empty();
            $("#<%= hidIDAdmin.ClientID %>").val("0");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float: left; padding: 10px 0 0 20px">
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Administradores.jpg" />
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
                            </td>
                            <td style="width: 150px; text-align: right;">
                                <a href="javascript:void(0);" id="btnAgregar">Nuevo Administrador
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
                    <asp:GridView ID="gvUsuarios" runat="server"
                        AllowPaging="false" Width="600px"
                        OnRowCommand="gvUsuariosRowCommand"
                        OnPageIndexChanging="gvUsuariosPageIndexChanging"
                        AutoGenerateColumns="False">
                        <EmptyDataTemplate>
                            <table width="600px">
                                <tr class="cabecera_indicadores">
                                    <th style="width: 100px;">CÓDIGO
                                    </th>
                                    <th style="width: 200px;">NOMBRE COMPLETO
                                    </th>
                                    <th style="width: 100px;">PAÍS
                                    </th>
                                    <th style="width: 100px;">ESTADO
                                    </th>
                                    <th style="width: 100px;">ADMIN
                                    </th>
                                    <th style="width: 100px;">OPCIONES
                                    </th>
                                </tr>
                                <tr class="grilla_indicadores">
                                    <td colspan="4" align="center" style="color: #0caed7;">
                                        <br />
                                        No se encontraron Administradores para el País <%= cboPaises.SelectedItem.Text %>
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
                            <asp:BoundField HeaderText="CÓDIGO" DataField="CodigoAdmin">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="NOMBRE COMPLETO" DataField="NombreCompleto">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="PAÍS" DataField="CodigoPais">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>

                            <asp:CheckBoxField DataField="Estado" HeaderText="ESTADO">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:CheckBoxField>

                            <asp:CheckBoxField DataField="Admin" HeaderText="ADMIN">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:CheckBoxField>

                            <asp:TemplateField HeaderText="OPCIONES">

                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_editar" runat="server" BorderStyle="None" CommandArgument='<%# Eval("IDAdmin") %>'
                                        CommandName="cmd_editar" CausesValidation="false">
                            <img src="../Images/edit.png" alt="Editar" border="0" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnk_eliminar" runat="server" BorderStyle="None" CommandArgument='<%# Eval("IDAdmin") %>'
                                        CommandName="cmd_eliminar" CausesValidation="false" OnClientClick="javascript:return confirm('¿Esta seguro de eliminar el Usuario?')">
                            <img src="../Images/delete.png" alt="Eliminar" border="0" /></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="adminsitradorPopup" style="font-size: 70%; margin: 40px; display: none;"
        title="Información del Administrador">
        <table cellspacing="5">
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">Código :
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="10" Width="150px" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="txtCodigo">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">Nombre :
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="200px" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        ControlToValidate="txtNombre">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">Clave :
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtClave" runat="server" MaxLength="20" TextMode="Password" Width="150px" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        ControlToValidate="txtClave">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">País :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="cboPais" runat="server" Width="150px" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">Gerente Evoluciona :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="cboAdmin" runat="server" Width="150px">
                        <asp:ListItem Value="True">Si</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr style="height: 25px;">
                <td style="text-align: right; padding-right: 5px;">Estado :
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="cboEstado" runat="server" Width="150px">
                        <asp:ListItem Value="True">Activo</asp:ListItem>
                        <asp:ListItem Value="False">Inactivo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px;">
                <td></td>
                <td style="text-align: left;" colspan="2">
                    <asp:HiddenField ID="hidIDAdmin" runat="server" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" Width="100px" />
                    &nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
