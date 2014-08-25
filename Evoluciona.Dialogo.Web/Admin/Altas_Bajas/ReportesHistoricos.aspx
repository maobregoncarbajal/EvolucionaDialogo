<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportesHistoricos.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.ReportesHistoricos" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Historico Reporte ::.</title>
    <link href="~/Styles/BlockUI.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/calendar.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/droplinetabs.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/FichaPersonal.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Matriz.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Menu.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/pmatriz.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/SimpleMenu.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/typography.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Validation.css" rel="stylesheet" type="text/css" />
    <link href="~/App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function cerrarse() {
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <div style="float: center; padding: 10px 0 0 20px">
                    <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Reporte.jpg" />
                </div>
                <br />
                <table>
                    <tr>
                        <td style="text-align: right; width: 200px">
                            <asp:Label ID="Label1" runat="server" Text="Documento Identidad :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblDocumento" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right; width: 200px">
                            <asp:Label ID="Label5" runat="server" Text="Campaña Ingreso :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblCampaniaIngreso" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 200px;">
                            <asp:Label ID="Label2" runat="server" Text="Nombre Colaborador :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblNombre" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right; width: 200px">
                            <asp:Label ID="Label7" runat="server" Text="Campaña Baja :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblCampaniaBaja" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 200px;">&nbsp;
                        </td>
                        <td style="text-align: left; width: 250px">&nbsp;
                        </td>
                        <td style="text-align: right; width: 200px">
                            <asp:Label ID="Label11" runat="server" Text="Fecha Baja :"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 250px">
                            <asp:Label ID="lblFechaBaja" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text="Lista Competencias :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:GridView ID="gvCompetencias" runat="server" AutoGenerateColumns="False" EmptyDataText="La Consulta no devuelve información.">
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
                                    <asp:BoundField HeaderText="Anio" DataField="Anio">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Competencia" DataField="Competencia">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PorcentajeAvance" HeaderText="PorcentajeAvance">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Sugerencia" HeaderText="Sugerencia">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rol" HeaderText="Rol">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="200px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Button ID="btnCompetenciaPDF" runat="server" Text="Ver PDF" class="btnSquare"
                                OnClick="btnCompetenciaPDF_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text="Lista Historico :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:GridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                EmptyDataText="La Consulta no devuelve información." OnPageIndexChanging="gvHistorico_PageIndexChanging">
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
                                    <asp:BoundField HeaderText="CodRegion" DataField="CodRegion">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="codZona" DataField="codZona">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AnioCampana" HeaderText="Año Campaña">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodGerenteRegional" HeaderText="Codigo Gerente Regional">
                                        <HeaderStyle CssClass="CssCabecTabla" />
                                        <ItemStyle CssClass="CssCeldas3" Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PtoRankingProdCamp" HeaderText="PtoRankingProdCamp">
                                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                        <ItemStyle Width="160px" CssClass="CssCeldas3"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PtoRankingProdPeriodo" HeaderText="PtoRankingProdPeriodo">
                                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                        <ItemStyle Width="80px" CssClass="CssCeldas3"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EstadoPeriodo" HeaderText="Estado Periodo">
                                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                        <ItemStyle Width="100px" CssClass="CssCeldas3"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaActualizacion" HeaderText="FechaActualizacion">
                                        <HeaderStyle CssClass="CssCabecTabla"></HeaderStyle>
                                        <ItemStyle Width="180px" CssClass="CssCeldas3"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <br />
                            <input id="hdenTipo" type="hidden" runat="server" />
                            <input type="button" value="Cerrar" onclick="cerrarse()" class="btnSquare">
                            <asp:Button ID="btnHistoricoPDF" runat="server" Text="Ver PDF" class="btnSquare"
                                OnClick="btnHistoricoPDF_Click" />
                        </td>
                    </tr>
                </table>
            </center>

        </div>
    </form>
</body>
</html>
