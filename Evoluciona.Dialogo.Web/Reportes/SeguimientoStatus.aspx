<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="SeguimientoStatus.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Reportes.SeguimientoStatus" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/MenuReportes.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div style="margin-left: 80px">
        <uc:menuReportes ID="menuReporte" runat="server" />
    </div>
    <br />
    <br />
    <asp:HiddenField ID="HdnValue" runat="server" />
    <div style="margin-left: 80px">
        <div id="panelUno">
            <table style="width: 100%">
                <tr>
                    <td colspan="11" style="text-align: right">
                        <asp:Button ID="btnExportar" runat="server" Text="Excel" OnClick="btnExportar_Click"
                            OnClientClick="ExportDivDataToExcel()" class="btnGuardarStyle" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="11">
                        <center>
                            <b>Cuadro Resumen</b></center>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr>
                    <td>
                        <span class="texto_Negro">País:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlPaises" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td></td>
                    <td>
                        <span class="texto_Negro">Nivel Jerárquico:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlNivel" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td></td>
                    <td>
                        <span class="texto_Negro">Período :</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList runat="server" ID="ddlPeriodos" CssClass="combo" Width="100px"
                            Style="margin-left: 7px;" />
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td></td>
                    <td>
                        <span class="texto_Negro">Estatus del Proceso:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlEstatus" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="4">No Abierto</asp:ListItem>
                            <asp:ListItem Value="1">En Proceso</asp:ListItem>
                            <asp:ListItem Value="2">En Aprobación</asp:ListItem>
                            <asp:ListItem Value="3">Cerrado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="11">
                        <br />
                        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" ValidationGroup="First"
                            class="btnBuscarStyle" />
                        <asp:Button ID="btnBuscarAux" runat="server" Text="Button" Style="display: none"
                            OnClick="btnBuscarAux_Click" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <br />
                        <div id="grillaUno">
                            <asp:GridView ID="gvProceso" runat="server" BackColor="White" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" Width="100%"
                                AutoGenerateColumns="False" OnRowCreated="gvProceso_RowCreated" HorizontalAlign="Center"
                                ShowFooter="True">
                                <RowStyle BackColor="#CCC0DA" ForeColor="black" BorderColor="White" BorderStyle="Solid"
                                    BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="Periodo" HeaderText="Período">
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pais" HeaderText="País">
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Nro.<br />
                                            Colaboradores a<br />
                                            Realizar Diálogos
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Colaborador") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("DialogoNoAbierto", "{0:P2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Diálogos no
                                            <br />
                                            abiertos
                                        </HeaderTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Diálogos en
                                            <br />
                                            proceso
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("DialogoProceso", "{0:P2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Diálogos en
                                            <br />
                                            acuerdo/aprobación
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("DialogoAprobacion", "{0:P2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Diálogos
                                            <br />
                                            cerrados
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("DialogoCerrado", "{0:P2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Plan de Acción
                                            <br />
                                            realizados
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("PlanAccion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AvancePlanAccion" HeaderText="Avance" DataFormatString="{0:P2}"
                                        HtmlEncode="False">
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Retroalimentación
                                            <br />
                                            en Competencias
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("RetroAlimentacion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AvanceRetroAlimentacion" HeaderText="Avance" DataFormatString="{0:P2}"
                                        HtmlEncode="False">
                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#60497B" ForeColor="white" BorderColor="white" BorderWidth="1px"
                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                <PagerStyle BackColor="#CCC0DA" ForeColor="#60497B" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="black" />
                                <HeaderStyle BackColor="#60497B" Font-Bold="True" ForeColor="white" BorderColor="white" />
                                <AlternatingRowStyle BackColor="#CCC0DA" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
            </table>
        </div>
        <div id="panelDos">
            <table style="width: 100%">
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B" colspan="11">
                        <center>
                            Detalle<br />
                            campos Filtro</center>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="texto_Negro">País: </span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlPaisesD" runat="server" Width="110px" CssClass="combo" Style="margin-left: 7px;"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPaisesD_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td style="width: 20px"></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Nivel Jerárquico:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlNivelD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlNivelD_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Período:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList runat="server" ID="ddlPeriodosD" CssClass="combo" Width="100px"
                            Style="margin-left: 7px;" AppendDataBoundItems="True" />
                        <span style="color: red">&nbsp;(*)</span>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Estatus en el Proceso:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlEstatusD" runat="server" Width="125px" CssClass="combo"
                            Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="4">No Abierto</asp:ListItem>
                            <asp:ListItem Value="1">En Proceso</asp:ListItem>
                            <asp:ListItem Value="2">En Aprobación</asp:ListItem>
                            <asp:ListItem Value="3">Cerrado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="texto_Negro">Región:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlRegionD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;"
                            OnSelectedIndexChanged="ddlRegionD_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Zona:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlZonaD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10px"></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Nombre Colaborador:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreColaboradorD" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Nombre Jefe:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreJefeD" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <br />
                        <asp:Button ID="btnBuscarD" runat="server" ValidationGroup="Second" OnClick="btnBuscarD_Click"
                            class="btnBuscarStyle" />
                        <asp:Button ID="btnBuscarDAux" runat="server" Text="Button" Style="display: none"
                            OnClick="btnBuscarDAux_Click" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <div id="grillaDos">
                            <center>
                                <asp:GridView ID="gvDetalle" runat="server" BackColor="White" BorderColor="White"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                    ShowFooter="True">
                                    <RowStyle BackColor="#CCC0DA" ForeColor="black" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="Periodo" HeaderText="Período">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Pais" HeaderText="País">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Nombre
                                                <br />
                                                Colaborador
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Colaborador") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="300px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Nivel" HeaderText="Nivel Jerárquico">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                                BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Jefe" HeaderText="Nombre Jefe">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="300px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estatus" HeaderText="Estatus del Proceso">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                                BorderWidth="1px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#60497B" ForeColor="white" BorderColor="white" BorderWidth="1px"
                                        HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <PagerStyle BackColor="#CCC0DA" ForeColor="#60497B" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="black" />
                                    <HeaderStyle BackColor="#60497B" Font-Bold="True" ForeColor="white" BorderColor="white" />
                                    <AlternatingRowStyle BackColor="#CCC0DA" />
                                </asp:GridView>
                            </center>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <hr class="hrPunteado" />
    <div style="color: red; text-align: left; padding-left: 80px">
        (*) Campo mínimo requerido para obtener resultados
    </div>
    <div id="divExport" style="display: none">
        <table style="margin-left: 20px">
            <tr>
                <td style="color: White; background-color: #60497B" colspan="13">
                    <center>
                        <p>
                            REPORTE: Seguimiento Status de Diálogos Evoluciona<br />
                            OBJETIVO:
                            <br />
                            *Monitoreo al % de FFVV que cumplió con su Diálogo Evoluciona<br />
                            *Identificar % de evaluadores que completaron los diálogos de su equipo - Por tipo
                            de evaluador<br />
                            *Monitoreo del Estatus de los Diálogos - FLUJO<br />
                        </p>
                    </center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="13">
                    <center>
                        Cuadro Resumen :</center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">País:
                </td>
                <td style="text-align: left">
                    <span id="spPais"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nivel Jerárquico:
                </td>
                <td style="text-align: left">
                    <span id="spNivel"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Período :
                </td>
                <td style="text-align: left">
                    <span id="spPeriodo"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Estatus del Proceso:
                </td>
                <td style="text-align: left">
                    <span id="spEstado"></span>
                </td>
                <td></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="12">
                    <div id="grillaUnoClone">
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="13">
                    <center>
                        Detalle<br />
                        campos Filtro</center>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">País:
                </td>
                <td style="text-align: left">
                    <span id="spPaisD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nivel Jerárquico:
                </td>
                <td style="text-align: left">
                    <span id="spNivelD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Período :
                </td>
                <td style="text-align: left">
                    <span id="spPeriodoD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Estatus en el Proceso:
                </td>
                <td style="text-align: left">
                    <span id="spEstadoD"></span>
                </td>
                <td></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td></td>
                <td style="color: White; background-color: #60497B">Región:
                </td>
                <td style="text-align: left">
                    <span id="spRegionD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Zona:
                </td>
                <td style="text-align: left">
                    <span id="spZonaD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nombre Colaborador:
                </td>
                <td style="text-align: left">
                    <span id="spNombreColaboradorD"></span>
                </td>
                <td></td>
                <td style="color: White; background-color: #60497B">Nombre Jefe:
                </td>
                <td style="text-align: left">
                    <span id="spNombreJefeD"></span>
                </td>
                <td></td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="12">
                    <center>
                        <div id="grillaDosClone">
                        </div>
                    </center>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <div id="mensajeAlertShow" style="display: none; height: 50px">
        <div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle" style="height: 20px; text-align: left; padding-left: 15px; padding-top: 5px;">
            Alerta
        </div>
        <div class="ui-widget-content ui-dialog-content" style="height: 30px; padding-top: 10px;">
            <span id="mensajeShow"></span>
        </div>
    </div>
    <input type="button" id="btnAlert" value="click" style="display: none" />
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />

    <script type="text/javascript">
        var mensajeAlert = '';
        var tipoAccion = '';
        jQuery(document).ready(function () {

            document.onkeydown = function (evt) {
                return (evt ? evt.which : event.keyCode) != 13;
            };

            $("#mensajeShow").html('');
            $("#mensajeShow").append(mensajeAlert);
            $("#btnAlert").click(function (evt) {

                PosicionarVentana(tipoAccion);
                $.blockUI({
                    message: $('#mensajeAlertShow'),
                    css: {
                        height: '50px',
                        border: 'none'
                    }
                });

                $('.blockOverlay').attr('title', '').click($.unblockUI);
                $('.blockUI').attr('title', '').click($.unblockUI);
            });
        });

        function PosicionarVentana(tipo) {
            if (tipo == "1") {
                $(window).scrollTop($("#panelUno").position().top);
            }
            else {
                $(window).scrollTop($("#panelDos").position().top);
            }
        }

        function ExportDivDataToExcel() {
            var lista = ["spPeriodo", "spPais", "spNivel", "spEstado",
            "spPeriodoD", "spNombreColaboradorD", "spNivelD", "spPaisD",
            "spZonaD", "spNombreJefeD", "spEstadoD", "spRegionD",
            "grillaUnoClone", "grillaDosClone"];
            LimpiarVariablesReporteHtml(lista);

            $("#spPeriodo").append($("#<%=ddlPeriodos.ClientID %> option:selected").text());
            $("#spPais").append($("#<%=ddlPaises.ClientID %> option:selected").text());
            $("#spNivel").append($("#<%=ddlNivel.ClientID %> option:selected").text());
            $("#spEstado").append($("#<%=ddlEstatus.ClientID %> option:selected").text());

            $("#spPeriodoD").append($("#<%=ddlPeriodosD.ClientID %> option:selected").text());
            $("#spNombreColaboradorD").append($("#<%=txtNombreColaboradorD.ClientID %>").val());
            $("#spNivelD").append($("#<%=ddlNivelD.ClientID %> option:selected").text());
            $("#spPaisD").append($("#<%=ddlPaisesD.ClientID %> option:selected").text());

            $("#spZonaD").append($("#<%=ddlZonaD.ClientID %> option:selected").text());
            $("#spNombreJefeD").append($("#<%=txtNombreJefeD.ClientID %>").val());
            $("#spEstadoD").append($("#<%=ddlEstatusD.ClientID %> option:selected").text());
            $("#spRegionD").append($("#<%=ddlRegionD.ClientID %> option:selected").text());

            $('#grillaUno').clone().appendTo('#grillaUnoClone');
            $('#grillaDos').clone().appendTo('#grillaDosClone');

            var html = $("#divExport").html();
            html = $.trim(html);
            html = html.replace(/>/g, '&gt;');
            html = html.replace(/</g, '&lt;');
            html = html.replace(/á/g, '&aacute;');
            html = html.replace(/é/g, '&eacute;');
            html = html.replace(/í/g, '&iacute;');
            html = html.replace(/ó/g, '&oacute;');
            html = html.replace(/ú/g, '&uacute;');
            html = html.replace(/ñ/g, '&ntilde;');

            html = html.replace(/Á/g, '&Aacute;');
            html = html.replace(/É/g, '&Eacute;');
            html = html.replace(/Í/g, '&Iacute;');
            html = html.replace(/Ó/g, '&Oacute;');
            html = html.replace(/Ú/g, '&Uacute;');

            html = html.replace(/Ñ/g, '&Ntilde;');

            $("input[id$='HdnValue']").val(html);
        }

        function LimpiarVariablesReporteHtml(args) {
            $.each(args, function (index, item) {
                $('#' + item).empty();
            });
        }

        function ProcessOpen(tipo) {
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

            if (tipo == "1") {
                $("#<%=btnBuscarAux.ClientID %>").trigger("click");
            } else {
                $("#<%=btnBuscarDAux.ClientID %>").trigger("click");
            }
        }

        function ProcessClose(tipo) {
            tipoAccion = tipo;
            PosicionarVentana(tipo);
            $.unblockUI({
                onUnblock: function () { }
            });
        }

        function Alerta(mensaje) {
            mensajeAlert = mensaje;
            $.unblockUI({
                onUnblock: function () { $('#btnAlert').trigger('click'); }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
