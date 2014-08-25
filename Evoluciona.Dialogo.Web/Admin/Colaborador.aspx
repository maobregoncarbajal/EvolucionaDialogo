<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Colaborador.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Colaborador" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Colaborador {
            padding: 0 5px;
        }
    </style>
    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/AdapterUtils.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/MenuAdapter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js"
        type="text/javascript"></script>

    <script src="../Jscripts/jquery.window.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var cantidad = 0;
        var espacios = 0;

        jQuery(document).ready(function () {
            jQuery("#<%=txtNombres.ClientID %>").keydown(function (e) {
                var tecla = (document.all) ? e.keyCode : e.which;
                if (tecla == 8) {
                    if (cantidad > 0) {
                        cantidad--;
                    }
                    espacios = 0;

                    return true;
                }

                if (tecla == 32) {
                    if (cantidad > 0 && espacios == 0) {
                        cantidad++;
                        espacios++;
                        return true;
                    } else
                        return false;
                }

                var patron = /[A-Z]/; // Solo acepta letras
                var te = String.fromCharCode(tecla);
                var verdad = patron.test(te);
                if (verdad) {
                    cantidad++;
                    espacios = 0;
                }
                return verdad;
            });

            jQuery("#<%=txtDoc.ClientID %>").keypress(function (e) {
                var tecla = (document.all) ? e.keyCode : e.which;
                if (tecla == 8) return true; //Tecla de retroceso (para poder borrar)

                if (tecla >= 48 && tecla <= 57) {
                    return true;
                }

                if (tecla == 32) return false; //Tecla de espacio ()
                var patron = /[A-Z a-z]/; // Solo acepta letras
                var te = String.fromCharCode(tecla);
                return patron.test(te);
            });
        });
        function CargarVista(pathUrl) {

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "BÚSQUEDA DE DATOS",
                url: pathUrl,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 800,
                height: 300
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="roundedDiv divFiltroPeriodo2" style="margin-top: 20px; height: 190px; margin-left: 30px; text-align: left; width: 410px;">
        <table style="margin: 5px; width: 400px; margin-top: 15px;">
            <tr style="height: 30px;">
                <td>Nombres y Apellidos:
                </td>
                <td>
                    <asp:TextBox ID="txtNombres" runat="server" Width="200" MaxLength="100" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px;">
                <td>Pa&iacute;s:
                </td>
                <td>
                    <asp:DropDownList ID="cboPaises" runat="server" Style="width: 200px"
                        CssClass="combo" OnSelectedIndexChanged="cboPaises_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="cboPaises">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>Cargo:
                </td>
                <td>
                    <asp:DropDownList ID="cboCargo" runat="server" Style="width: 200px" CssClass="combo">
                        <asp:ListItem Value="4">Director de Ventas</asp:ListItem>
                        <asp:ListItem Value="5">Gerente de Región</asp:ListItem>
                        <asp:ListItem Value="6">Gerente de Zona</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        ControlToValidate="cboCargo">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>Documento o C&eacute;dula de Identidad:
                </td>
                <td>
                    <asp:TextBox ID="txtDoc" runat="server" Width="200" MaxLength="20" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="hlColaborador" runat="server" ForeColor="Black">B&Uacute;SQUEDA DIRECTA</asp:HyperLink>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGuardar" Style="margin-left: 1px; height: 25px; width: 100px;"
                        Text="Consultar" OnClick="btnGuardar_Click" />
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
    <div style="margin: 15px 0px 15px 30px; text-align: left;" id="contenidoAltas">
        <asp:GridView ID="gvColaborador" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvColaborador_PageIndexChanging">
            <EmptyDataTemplate>
                <table>
                    <tr class="cabecera_procesos">
                        <th style="width: 100px;">Documento
                        </th>
                        <th style="width: 300px;">Nombre del Colaborador
                        </th>
                        <th style="width: 80px;">Pais
                        </th>
                        <th style="width: 160px;">Cargo
                        </th>
                        <th style="width: 300px;">Nombre del Jefe Inmediato
                        </th>
                        <th style="width: 100px;">Doc. del Jefe Inmediato
                        </th>
                        <%--                        <th style="width: 60px;">
                            Estado
                        </th>--%>
                    </tr>
                    <tr class="cabecera_procesos">
                        <td colspan="7" align="center" class="grilla_alterna_indicadores">No se encontraron resultados para la búsqueda actual...
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
                        <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("CodPais") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
                    <ItemStyle Width="0px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Documento" DataField="Documento">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Right" Width="100px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Nombre del Colaborador" DataField="Nombre">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Left" Width="300px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="País" DataField="Pais">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Right" Width="80px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Cargo" DataField="Cargo">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Right" Width="160px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Nombre del Jefe Inmediato" DataField="Jefe">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Left" Width="300px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Doc. del Jefe Inmediato" DataField="DocJefe">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" CssClass="Colaborador" />
                </asp:BoundField>
                <%--                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <input id="cbEstado" runat="server" checked='<%# Eval("BitEstado")  %>' disabled="disabled"
                            type="checkbox" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="javascript:CargarVista('Adam.aspx?dni=<%# Eval("Documento") %>&pais=<%# Eval("CodPais") %>&anio=<%# Eval("Anio") %>&cub=<%# Eval("CUB") %>');">Ver</a>
                    </ItemTemplate>
                    <ItemStyle CssClass="link gridViewLink" Width="40px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
