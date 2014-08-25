<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Bajas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.Bajas" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />
    <center>
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Bajas.jpg" />
    </center>
    <br />
    <br />
    <center>
        <table>
            <tr>
                <td style="text-align: right; width: 130px">
                    <asp:Label ID="Label1" runat="server" Text="País :"></asp:Label>
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="True" Width="200px" CssClass="stiloborde"
                        OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPais"
                        ErrorMessage="*" InitialValue="0" ValidationGroup="Buscar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px">Periodo :
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:DropDownList ID="ddlPeriodos" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqfvPeriodos" runat="server" ControlToValidate="ddlPeriodos"
                        ErrorMessage="*" InitialValue="" ValidationGroup="Buscar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;">
                    <asp:Label ID="Label2" runat="server" Text="Tipo Colaborador :"></asp:Label>
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:DropDownList ID="ddlTipoColaborador" runat="server" Width="200px" CssClass="stiloborde"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlTipoColaborador_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoColaborador"
                        ErrorMessage="*" InitialValue="0" ValidationGroup="Buscar"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div id="divRegion" runat="server">
            <table>
                <tr>
                    <td style="text-align: right; width: 130px;">
                        <asp:Label ID="Label4" runat="server" Text="Región :"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px">&nbsp;
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" Width="200px"
                            CssClass="stiloborde" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divZona" style="display: block" runat="server">
            <table>
                <tr>
                    <td style="text-align: right; width: 130px;">
                        <asp:Label ID="Label6" runat="server" Text="Zona :"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px">&nbsp;
                        <asp:DropDownList ID="ddlZona" runat="server" Width="200px" CssClass="stiloborde">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divColaborador" style="display: block" runat="server">
            <table>
                <tr>
                    <td style="text-align: right; width: 130px;">
                        <asp:Label ID="Label3" runat="server" Text="Nombre Colaborador :"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px">&nbsp;<%--<asp:DropDownList ID="ddlColaborador" runat="server" Width="200px" CssClass="stiloborde">
                                </asp:DropDownList>--%><asp:TextBox ID="txtColaborador" runat="server" Width="240px"
                                    class="pagedisplay" MaxLength="100"></asp:TextBox><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtColaborador"
                                        ErrorMessage="Error en nombre" ValidationGroup="Buscar" ValidationExpression="(^[a-zA-ZñÑ ]*$)"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <td style="text-align: right; width: 130px;"></td>
                <td style="text-align: left; width: 250px">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btnSquare" OnClick="btnBuscar_Click"
                        Style="display: none" />
                    <asp:Button ID="btnBuscarAux" runat="server" Text="Buscar" class="btnSquare" ValidationGroup="Buscar"
                        OnClick="btnBuscarAux_Click" />
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvGerenteRegion" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvGerenteRegion_PageIndexChanging"
                        OnRowCommand="gvGerenteRegion_RowCommand" OnRowDataBound="gvGerenteRegion_RowDataBound"
                        DataKeyNames="intIDGerenteRegion" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IntIDGerenteRegion" HeaderText="Código Gerente" Visible="False">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VchNombrecompleto" HeaderText="Gerente Regional">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="200px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ChrCodigoGerenteRegion" HeaderText="Documento Identidad">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VchCorreoElectronico" HeaderText="Correo">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="vchNombreCompletoDV" HeaderText="Directora Ventas">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="180px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <%--<asp:TemplateField HeaderText="Directora Ventas">
                                <ItemTemplate>
                                    <asp:Label ID="lblDirectoraVentas" runat="server" Text='<%# Bind("obeDirectoraVentas.vchNombreCompleto") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="180px" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Anho Campanha" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnioCampana" runat="server" Text='<%# Bind("AnioCampana") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Editar" Visible="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEditar" runat="server" CommandName="Editar" ImageUrl="~/Images/edit.png"></asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEliminar" runat="server" CommandName="Eliminar" ImageUrl="~/Images/delete_icon.png"
                                        OnClientClick="javascript:return confirm('¿Esta seguro de eliminarla?')" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvGerenteZona" runat="server" AutoGenerateColumns="False" DataKeyNames="intIDGerenteZona"
                        OnPageIndexChanging="gvGerenteZona_PageIndexChanging" OnRowCommand="gvGerenteZona_RowCommand"
                        OnRowDataBound="gvGerenteZona_RowDataBound" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Anho Campanha" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnioCampana" runat="server" Text='<%# Bind("AnioCampana") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="intIDGerenteZona" HeaderText="Código Zona" Visible="False">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="vchNombreCompleto" HeaderText="Gerente Zonal">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="200px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ChrCodigoGerenteZona" HeaderText="Documento Identidad">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="vchCorreoElectronico" HeaderText="Correo">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VchNombrecompletoGR" HeaderText="Gerente Región">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <%--<asp:TemplateField HeaderText="Gerente Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblDirectoraVentas" runat="server" Text='<%# Bind("obeGerenteRegion.VchNombrecompleto") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="180px" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Editar" Visible="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEditar" runat="server" CommandName="Editar" ImageUrl="~/Images/edit.png"></asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEliminar" runat="server" CommandName="Eliminar" ImageUrl="~/Images/delete_icon.png"
                                        OnClientClick="javascript:return confirm('¿Esta seguro de eliminarla?')" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <br />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btnSquare" />
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" class="btnSquare" />
                </td>
            </tr>
        </table>
    </center>
    <br />
    <div id="idInicio" style="display: none">
        <input id="hdenCodigoPais" type="hidden" runat="server" />
        <input id="hdenCodigoUsuario" type="hidden" runat="server" />
        <input id="hdenAnioAuxMinimo" type="hidden" runat="server" />
        <input id="hdenAnioAuxMaximo" type="hidden" runat="server" />
        <input id="hdenCampaniaAuxMinimo" type="hidden" runat="server" />
        <input id="hdenCampaniaAuxMaximo" type="hidden" runat="server" />
    </div>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            document.onkeydown = function (evt) {
                return (evt ? evt.which : event.keyCode) != 13;
            };
        });

        function ProcessOpen() {

            jQuery.blockUI({
                message: jQuery("#imgExporting"),
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: 'transparent',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    color: '#fff'
                }
            });

            $("#<%=btnBuscar.ClientID %>").trigger("click");

        }

        function ProcessClose() {

            $.unblockUI({
                onUnblock: function () { }
            });
        }

    </script>

</asp:Content>
