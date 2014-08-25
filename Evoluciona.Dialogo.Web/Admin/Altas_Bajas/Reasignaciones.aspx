<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Reasignaciones.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.Reasignaciones" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Reasignaciones.jpg" />
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td style="text-align: right; width: 130px">
                    <asp:Label ID="Label1" runat="server" Text="País :"></asp:Label>
                </td>
                <td style="text-align: left; width: 260px">&nbsp;
                    <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="True" Width="200px" CssClass="stiloborde"
                        OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
                        <asp:ListItem Value="0">[Seleccione]</asp:ListItem>
                        <asp:ListItem Value="AR">Argentina</asp:ListItem>
                        <asp:ListItem Value="BO">Bolivia</asp:ListItem>
                        <asp:ListItem Value="CL">Chile</asp:ListItem>
                        <asp:ListItem Value="CO">Colombia</asp:ListItem>
                        <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                        <asp:ListItem Value="DO">Rep. Dominicana</asp:ListItem>
                        <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                        <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                        <asp:ListItem Value="MX">Mexico</asp:ListItem>
                        <asp:ListItem Value="PA">Panama</asp:ListItem>
                        <asp:ListItem Value="PE">Perú</asp:ListItem>
                        <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                        <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                        <asp:ListItem Value="VE">Venezuela</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;">
                    <asp:Label ID="Label4" runat="server" Text="Región :"></asp:Label>
                </td>
                <td style="text-align: left; width: 260px">&nbsp;
                    <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" Width="200px"
                        CssClass="stiloborde" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRegion"
                        ErrorMessage="*" InitialValue="0" ValidationGroup="Buscar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;">
                    <asp:Label ID="Label6" runat="server" Text="Zona :"></asp:Label>
                </td>
                <td style="text-align: left; width: 260px">&nbsp;
                    <asp:DropDownList ID="ddlZona" runat="server" Width="200px" CssClass="stiloborde">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;">
                    <asp:Label ID="Label3" runat="server" Text="Nombre Colaborador :"></asp:Label>
                </td>
                <td style="text-align: left; width: 260px">&nbsp;<asp:TextBox ID="txtColaborador" runat="server" Width="240px" class="pagedisplay"
                    MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtColaborador"
                        ErrorMessage="Error en nombre" ValidationGroup="Buscar" ValidationExpression="(^[a-zA-ZñÑ ]*$)"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;"></td>
                <td style="text-align: left; width: 260px">
                    <asp:RadioButtonList ID="rblReasignar" runat="server">
                        <asp:ListItem Value="1">Ascender a Gerente Región</asp:ListItem>
                        <asp:ListItem Value="2">Cambiar de Gerente Región</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 130px;"></td>
                <td style="text-align: left; width: 260px">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btnSquare" ValidationGroup="Buscar"
                        OnClick="btnBuscar_Click" />
                </td>
            </tr>
        </table>
    </center>
    <br />
    <div id="divAsignacion" runat="server" align="center">
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvGerenteZona" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        DataKeyNames="intIDGerenteZona,intIDGerenteRegion,chrPrefijoIsoPais,chrCodigoGerenteZona,vchNombreCompleto,vchCorreoElectronico,chrCodigoDataMart,chrCampaniaRegistro,chrIndicadorMigrado"
                        PageSize="5" OnPageIndexChanging="gvGerenteZona_PageIndexChanging" OnRowDataBound="gvGerenteZona_RowDataBound"
                        EmptyDataText="La Consulta no devuelve información.">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%--<asp:CheckBox ID="chkHeader" onclick="javascript:CopyCheckStateByColumn(this,this.offsetParent.offsetParent.id);"
                                        runat="server" EnableViewState="true"></asp:CheckBox>--%>
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
                            <asp:BoundField DataField="intIDGerenteZona" HeaderText="Código Zona" Visible="False">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="vchNombreCompleto" HeaderText="Gerente Zonal">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="200px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="vchCorreoElectronico" HeaderText="Correo">
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Gerente Región">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDGerenteRegion" runat="server" Text='<%# Bind("intIDGerenteRegion") %>'
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblCodigoGerenteRegion" runat="server" Text='<%# Bind("CodigoGerenteRegion") %>'
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="lblGerenteRegion" runat="server" Text='<%# Bind("NombreGerenteRegion") %>'></asp:Label>
                                    <asp:DropDownList ID="ddlGerenteRegion" runat="server" CommandName="GerenteRegion"
                                        Width="160px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="180px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Directora Ventas">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDirectora" runat="server" CommandName="Directora" Width="160px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="180px" CssClass="CssCeldas3"></ItemStyle>
                                <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <br />
                    <input id="hdenCodigoUsuario" type="hidden" runat="server" />
                    <input id="hdenCodigoEvaluador" type="hidden" runat="server" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btnSquare" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnGrabar" runat="server" Text="Procesar" class="btnSquare" OnClick="btnGrabar_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divProceso" runat="server" align="center">
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlProceso" runat="server">
                        <asp:GridView ID="gvProceso" runat="server" AutoGenerateColumns="False" DataKeyNames="IDProceso,IDRol,CodigoUsuario,Periodo,FechaLimiteProceso,EstadoProceso,IDRolEvaluador,CodigoEvaluador,PrefijoIsoPais,NuevasIngresadas,RegionZona"
                            PageSize="5" OnRowDataBound="gvProceso_RowDataBound" Caption="Antes de continuar se encontró:">
                            <Columns>
                                <asp:BoundField DataField="CodigoUsuario" HeaderText="Código Usuario" Visible="False">
                                    <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                    <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario - Gerente Zonal">
                                    <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                    <ItemStyle Width="200px" CssClass="CssCeldas3"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CodigoEvaluador" HeaderText="Código Evaluador" Visible="False">
                                    <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                    <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Proceso Dialogo">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rblProceso" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">&nbsp;Mantener&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Nuevo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="200px" CssClass="CssCeldas3"></ItemStyle>
                                    <HeaderStyle CssClass="CssCabecTabla" HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <asp:Label ID="Label2" runat="server" Text="Desea mantener el dialogo de su anterior <br/>jefe y asignarselo a su nuevo jefe, o eliminarlo <br/> para iniciar con su nuevo jefe."></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <br />
                    <asp:Button ID="btnProcesoCancelar" runat="server" Text="Cancelar" class="btnSquare"
                        OnClick="btnProcesoCancelar_Click" />
                    <asp:Button ID="btnProcesoContinuar" runat="server" Text="Continuar" class="btnSquare"
                        OnClick="btnProcesoContinuar_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="idInicio" style="display: none">
        <input id="hdenCodigoPais" type="hidden" runat="server" />
        <input id="Hidden1" type="hidden" runat="server" />

        <input id="hdenAnioAuxMinimo" type="hidden" runat="server" />
        <input id="hdenAnioAuxMaximo" type="hidden" runat="server" />
        <input id="hdenCampaniaAuxMinimo" type="hidden" runat="server" />
        <input id="hdenCampaniaAuxMaximo" type="hidden" runat="server" />
    </div>
</asp:Content>
