<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="DirectoraVentas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.DirectoraVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <center>
            <br />
            <table width="100%">
                <tr>
                    <td style="color: #4660a1; font-size: 16px; font-weight: bold; text-align: center;">
                        <strong>
                            <asp:Label ID="lblSubTitulo" runat="server" Text=""></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="pnlDatos" runat="server">
                <table>
                    <tr>
                        <td style="text-align: right; width: 130px">
                            <asp:Label ID="Label1" runat="server" Text="Documento :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">&nbsp;
                            <asp:TextBox ID="txtDocumento" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 130px;">
                            <asp:Label ID="Label2" runat="server" Text="Nombre :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">&nbsp;
                            <asp:TextBox ID="txtNombre" runat="server" Width="240px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 130px;">
                            <asp:Label ID="Label3" runat="server" Text="Correo :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">&nbsp;
                            <asp:TextBox ID="txtCorreo" runat="server" Width="240px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <table>
                <tr>
                    <td style="text-align: right; width: 130px;">
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" class="btnSquare" />
                    </td>
                    <td style="text-align: left; width: 250px">
                        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click"
                            class="btnSquare" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                            class="btnSquare" />
                        <input id="hdenCodigoPais" type="hidden" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="pnlDetalle" runat="server">
                <asp:GridView ID="gvDirectoraVentas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvDirectoraVentas_PageIndexChanging" OnRowCommand="gvDirectoraVentas_RowCommand"
                    OnRowDataBound="gvDirectoraVentas_RowDataBound" DataKeyNames="intIDDirectoraVenta,vchDocumentoIndentidad,vchNombreCompleto,vchCorreoElectronico,chrCodigoDirectoraVentas"
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
                        <asp:BoundField DataField="intIDDirectoraVenta" HeaderText="Código" Visible="False">
                            <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            <ItemStyle Width="50px" CssClass="CssCeldas3"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="chrCodigoDirectoraVentas" HeaderText="Documento">
                            <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="vchNombreCompleto" HeaderText="Nombre">
                            <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            <ItemStyle Width="200px" CssClass="CssCeldas3"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="vchCorreoElectronico" HeaderText="Correo">
                            <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                            <ItemStyle Width="200px" CssClass="CssCeldas3"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEditar" runat="server" CommandName="Editar" ImageUrl="~/Images/edit.png"></asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                            <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEliminar" runat="server" CommandName="Eliminar" ImageUrl="~/Images/delete_icon.png" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="70px" CssClass="CssCeldas3"></ItemStyle>
                            <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnRetornar" runat="server" Text="Retornar" class="btnSquare" OnClick="btnRetornar_Click" />
                        &nbsp;
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btnSquare" OnClick="btnEliminar_Click" />
                    </td>
                </tr>
            </table>
        </center>
        <div id="divAuxiliar" style="display: none">
            <asp:HiddenField ID="hfModo" runat="server" />
            <asp:HiddenField ID="hfCodigoDirectora" runat="server" />
            <asp:HiddenField ID="hfTipoMante" runat="server" />
        </div>
    </div>



</asp:Content>
