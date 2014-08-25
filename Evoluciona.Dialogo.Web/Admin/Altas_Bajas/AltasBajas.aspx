<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="AltasBajas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.AltasBajas1" %>

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
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Altas.jpg" />
    </center>
    <br />
    <br />
    <center>
        <table>
            <tr>
                <td style="text-align: right; width: 130px">País :
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
                <td style="text-align: right; width: 130px">Año Competencia :
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:DropDownList ID="ddlAnhoCompetencia" runat="server" AutoPostBack="True" Width="200px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqfvAnhoCompetencia" runat="server" ControlToValidate="ddlAnhoCompetencia"
                        ErrorMessage="*" InitialValue="" ValidationGroup="Buscar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;">Tipo Colaborador :
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
        <div id="divRegion" style="display: block" runat="server">
            <table>
                <tr>
                    <td style="text-align: right; width: 130px;">
                        <asp:Label ID="lblRegion" runat="server" Text="Región :"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 250px">&nbsp;
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" Width="200px"
                            CssClass="stiloborde" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="ddlRegion"
                            ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <td style="text-align: right; width: 130px;">Campaña Ingreso :
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:TextBox ID="txtAnioCampania" runat="server" Width="100px" class="pagedisplay"
                        MaxLength="6"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAnioCampania"
                        ErrorMessage="Error año campaña" ValidationGroup="Buscar" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;">Nombre Colaborador :
                </td>
                <td style="text-align: left; width: 250px">&nbsp;
                    <asp:TextBox ID="txtColaborador" runat="server" Width="240px" class="pagedisplay"
                        MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtColaborador"
                        ErrorMessage="Error en nombre" ValidationGroup="Buscar" ValidationExpression="(^[a-zA-ZñÑ ]*$)"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <div id="divZona" style="display: block" runat="server">
            <table>
                <tr>
                    <td style="text-align: right; width: 130px;">Zona :
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
                <td style="text-align: right; width: 130px;"></td>
                <td style="text-align: left; width: 250px">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btnSquare" OnClick="btnBuscar_Click"
                        Style="display: none" />
                    <asp:Button ID="btnBuscarAux" runat="server" Text="Buscar" class="btnSquare" OnClick="btnBuscarAux_Click"
                        ValidationGroup="Buscar" />
                </td>
            </tr>
        </table>
    </center>
    <br />
    <center>
    </center>
    <center>
        <table>
            <tr>
                <td style="text-align: left;">
                    <div id="divLeyenda" runat="server">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Exclamation-icon16.png" />&nbsp;
                        No proviene de competencias
                        <br />
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvGerenteRegion" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvGerenteRegion_PageIndexChanging"
                        OnRowDataBound="gvGerenteRegion_RowDataBound" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEliminar" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
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
                            <asp:TemplateField HeaderText="Código DataMart" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoDatamart" runat="server" Text='<%# Bind("ChrCodigoDataMart") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gerente Regional">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDesGerenteRegional" runat="server" Text='<%# Bind("DesGerenteRegional") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrigen" runat="server" Text='<%# Bind("Origen") %>' Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtDocumento" runat="server" Text='<%# Bind("DocIdentidad") %>'
                                        MaxLength="20" Width="180px" onkeypress="return IsNumber(event);"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Image ID="imgOrigen" runat="server" ImageUrl="~/Images/Exclamation-icon16.png" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="230px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCorreoElectronico" runat="server" Text='<%# Bind("CorreoElectronico") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código Región" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoRegion" runat="server" Text='<%# Bind("CodRegion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUB">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCUB" runat="server" Text='<%# Bind("CUBGR") %>' MaxLength="20" Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Directora Ventas">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDirectora" runat="server" CommandName="Directora">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvGerenteZona" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvGerenteZona_PageIndexChanging" OnRowDataBound="gvGerenteZona_RowDataBound" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEliminar" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Año Campaña" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnioCampania" runat="server" Text='<%# Bind("AnioCampana") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código Gerente" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoDataMart" runat="server" Text='<%# Bind("chrCodigoDataMart") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gerente Zona">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDesGerenteZona" Width="140" runat="server" Text='<%# Bind("DesGerenteZona") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrigen" runat="server" Text='<%# Bind("Origen") %>' Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtDocumento" runat="server" Text='<%# Bind("DocIdentidad") %>'
                                        MaxLength="20" Width="120px" onkeypress="return IsNumber(event);"></asp:TextBox>&nbsp;
                                    <asp:Image ID="imgOrigen" runat="server" ImageUrl="~/Images/Exclamation-icon16.png" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="145px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCorreoElectronico" runat="server" Width="120px" Text='<%# Bind("CorreoElectronico") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="130px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod. Región" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoRegion" runat="server" Text='<%# Bind("CodRegion") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod. Zona" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoZona" runat="server" Text='<%# Bind("CodZona") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUB">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCUB" runat="server" Text='<%# Bind("CUBGZ") %>' MaxLength="20" Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle CssClass="CssCabecTabla" />
                                <ItemStyle CssClass="CssCeldas3" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gerente Región">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlGerenteRegion" runat="server" CommandName="GerenteRegion">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150px" CssClass="CssCeldas3"></ItemStyle>
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
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" class="btnSquare" OnClick="btnGrabar_Click" />
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
