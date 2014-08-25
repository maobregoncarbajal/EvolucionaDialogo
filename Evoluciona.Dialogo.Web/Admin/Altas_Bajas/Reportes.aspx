<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Reportes.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.Reportes" %>

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
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Reporte.jpg" />
    </center>
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
                <td style="text-align: right; width: 130px">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo :"></asp:Label>
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
        <div id="divZona" runat="server">
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
        <table>
            <tr>
                <td style="text-align: right; width: 130px;">
                    <asp:Label ID="Label5" runat="server" Text="Estado :"></asp:Label>
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:DropDownList ID="ddlEstado" runat="server" Width="200px" CssClass="stiloborde">
                        <asp:ListItem Value="1">Activo</asp:ListItem>
                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
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
                    <asp:Button ID="btnBuscarAux" runat="server" Text="Buscar" class="btnSquare" ValidationGroup="Buscar"
                        OnClick="btnBuscarAux_Click" />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btnSquare" OnClick="btnBuscar_Click"
                        Style="display: none" />
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvDirectoraVentas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        EmptyDataText="La Consulta no devuelve información." DataKeyNames="ID" OnPageIndexChanging="gvDirectoraVentas_PageIndexChanging"
                        OnRowCommand="gvDirectoraVentas_RowCommand" OnRowDataBound="gvDirectoraVentas_RowDataBound">
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
                            <asp:BoundField DataField="Pais" HeaderText="País">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="120" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoColaborador" HeaderText="Tipo">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CodigoDirectora" HeaderText="Código">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Directora" HeaderText="Nombre">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Correo" HeaderText="Correo">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="180px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="80px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ver" Visible="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnVer" runat="server" CommandName="Ver" ImageUrl="~/Images/view.gif" />
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
                    <asp:GridView ID="gvGerenteRegion" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        OnPageIndexChanging="gvGerenteRegion_PageIndexChanging" EmptyDataText="La Consulta no devuelve información."
                        DataKeyNames="ID,CodigoGerente,chrCampaniaRegistro,chrCampaniaBaja,FechaBaja"
                        OnRowCommand="gvGerenteRegion_RowCommand" OnRowDataBound="gvGerenteRegion_RowDataBound">
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
                            <asp:BoundField HeaderText="País" DataField="Pais">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoColaborador" HeaderText="Tipo">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoGerente" runat="server" Text='<%# Bind("CodigoGerente") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblGerente" runat="server" Text='<%# Bind("Gerente") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="180px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Correo" HeaderText="Correo">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="80px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Region" HeaderText="Región">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Zona" HeaderText="Zona">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Jefe" HeaderText="Jefe">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="180px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CodigoJefe" HeaderText="Código Jefe">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ver">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlinkCodigo" runat="server"> <img src="<%=Utils.RelativeWebRoot%>Images/view.gif" /> </asp:HyperLink>
                                    <asp:Label ID="lblchrCampaniaRegistro" runat="server" Text='<%# Bind("chrCampaniaRegistro") %>'
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblchrCampaniaBaja" runat="server" Text='<%# Bind("chrCampaniaBaja") %>'
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblFechaBaja" runat="server" Text='<%# Bind("FechaBaja") %>' Visible="False"></asp:Label>
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
                    <asp:GridView ID="gvGerenteZona" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,CodigoGerente,chrCampaniaRegistro,chrCampaniaBaja,FechaBaja"
                        AllowPaging="True" OnPageIndexChanging="gvGerenteZona_PageIndexChanging" OnRowDataBound="gvGerenteZona_RowDataBound"
                        EmptyDataText="La Consulta no devuelve información." OnRowCommand="gvGerenteZona_RowCommand">
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
                            <asp:BoundField HeaderText="País" DataField="Pais">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoColaborador" HeaderText="Tipo">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="CodigoGerente" HeaderText="Código">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="100px" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoGerente" runat="server" Text='<%# Bind("CodigoGerente") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="100px" />
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Gerente" HeaderText="Nombre">
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="180px" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblGerente" runat="server" Text='<%# Bind("Gerente") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="180px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Correo" HeaderText="Correo">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="80px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Region" HeaderText="Región">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Zona" HeaderText="Zona">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Jefe" HeaderText="Jefe">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="180px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CodigoJefe" HeaderText="Código Jefe">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Ver">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlinkCodigo" runat="server"> <img src="<%=Utils.RelativeWebRoot%>Images/view.gif" /> </asp:HyperLink>
                                    <asp:Label ID="lblchrCampaniaRegistro" runat="server" Text='<%# Bind("chrCampaniaRegistro") %>'
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblchrCampaniaBaja" runat="server" Text='<%# Bind("chrCampaniaBaja") %>'
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblFechaBaja" runat="server" Text='<%# Bind("FechaBaja") %>' Visible="False"></asp:Label>
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
                    <asp:Button ID="btnGrabar" runat="server" Text="Ver PDF" class="btnSquare" OnClick="btnGrabar_Click" />
                </td>
            </tr>
        </table>
    </center>

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
