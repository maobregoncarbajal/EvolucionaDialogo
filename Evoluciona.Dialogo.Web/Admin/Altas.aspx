<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Altas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function VerificarAlta() {

            var paises = $("#<%=cboPaises.ClientID %>").val();
            var evaluador = $("#<%=cboEvaluador.ClientID %>").val();

            if (paises != 0 && evaluador != 0) {
                return confirm('¿Esta seguro de Cambiar el Documento?');
            } else {
                alert('Datos No Válidos');
                return true;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="roundedDiv divFiltroPeriodo2" style="margin-top: 20px; height: 140px; margin-left: 30px; text-align: left; width: 410px;">
        <table style="margin: 5px; width: 400px; margin-top: 15px;">
            <tr style="height: 30px;">
                <td>Pa&iacute;s:
                </td>
                <td>
                    <asp:DropDownList ID="cboPaises" runat="server" Style="width: 175px" CssClass="combo"
                        AutoPostBack="True" OnSelectedIndexChanged="cboPaises_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="cboPaises">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>Rol Evaluador:
                </td>
                <td>
                    <asp:DropDownList ID="cboRolEvaluador" runat="server" Style="width: 175px" CssClass="combo"
                        AutoPostBack="True" OnSelectedIndexChanged="cboRolEvaluador_SelectedIndexChanged">
                        <asp:ListItem Value="5">Gerente de Región</asp:ListItem>
                        <asp:ListItem Value="6">Gerente de Zona</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        ControlToValidate="cboRolEvaluador">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>Evaluador:
                </td>
                <td>
                    <asp:DropDownList ID="cboEvaluador" runat="server" CssClass="combo" Width="300px"
                        AutoPostBack="True" OnSelectedIndexChanged="cboEvaluador_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        ControlToValidate="cboEvaluador">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>Documento:
                </td>
                <td>
                    <asp:TextBox ID="txtDocumento" runat="server" Width="120" />
                    <asp:Button runat="server" Text="Guardar" ID="btnGuardar" Style="margin-left: 20px;"
                        OnClientClick="VerificarAlta();" OnClick="btnGuardar_Click" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <br />
                    <small style="color: red; font-size: 8pt;">
                        <asp:Literal ID="litMensaje" runat="server" /></small>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 15px 0 15px 30px; text-align: left; width: 100%" id="contenidoAltas">
        <asp:GridView ID="gvAltas" CssClass="grillaPaginada" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" OnPageIndexChanging="gvAltas_PageIndexChanging">
            <EmptyDataTemplate>
                <table>
                    <tr class="cabecera_procesos">
                        <th style="width: 120px;">Codigo
                        </th>
                        <th style="width: 60px;">Pais
                        </th>
                        <th style="width: 300px;">Nombre
                        </th>
                        <th style="width: 250px;">Correo
                        </th>
                        <th style="width: 120px;">Evaluador
                        </th>
                        <th style="width: 120px;">CodDataMart
                        </th>
                    </tr>
                    <tr class="grilla_indicadores">
                        <td colspan="6" align="center" class="grilla_alterna_indicadores">No se encontraron resultados para la búsqueda actual...
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="cabecera_procesos" HorizontalAlign="Center" VerticalAlign="Middle"
                Font-Bold="False" />
            <RowStyle CssClass="grilla_alterna_indicadores" />
            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("intIDGerenteRegion") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
                    <ItemStyle Width="0px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Codigo" DataField="chrCodigoGerenteRegion">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Pais" DataField="chrPrefijoIsoPais">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Nombre" DataField="vchNombrecompleto">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Correo" DataField="vchCorreoElectronico">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="chrCodDirectorVenta" HeaderText="Evaluador">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CodDataMart" DataField="chrCodigoDataMart">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <input id="cbEstado" runat="server" checked='<%# Eval("bitEstado")  %>' disabled="disabled"
                            type="checkbox" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
