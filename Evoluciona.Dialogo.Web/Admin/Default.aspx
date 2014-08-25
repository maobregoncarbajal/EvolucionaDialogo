<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Default" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />

    <script src="../Jscripts/jquery.window.min.js" type="text/javascript"></script>

    <script src="../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js" type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js" type="text/javascript"></script>
    
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Evoluciona.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        var periodoSearch = "<%=PeriodoBuscado %>";
    
        $(document).ready(function () {
            var url = "<%=Utils.RelativeWebRoot%>Handler/HelperHandler.ashx";
            Evoluciona.LoadDropDownList("slctPeriodos", url, { 'accion': 'cargarPeriodos', 'pais': '00' }, 0, true, false);

            $("#slctPeriodos").change(function () {
                $("#<%=HfPeriodo.ClientID %>").attr("value", $("#slctPeriodos").val());
            });

            $("#slctPeriodos").val(periodoSearch);
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HfPeriodo" runat="server" />
    <div style="float: left; padding: 10px 0 0 20px">
        <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/VisualizarDialogos.jpg" />
    </div>
    <div class="roundedDiv divFiltroPeriodo2" style="margin-top: 60px; height: 160px; margin-left: 20px; text-align: left; width: 410px;">
        <table style="margin: 5px; width: 400px; margin-top: 15px;">
            <tr>
                <td>Pa&iacute;s:
                </td>
                <td>
                    <asp:DropDownList ID="cboPaises" runat="server" Style="width: 175px"
                        CssClass="combo" AutoPostBack="True" OnSelectedIndexChanged="cboPaises_SelectedIndexChanged">
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
                <td></td>
            </tr>
            <tr>
                <td>Periodos:
                </td>
                <td>
                    <select id="slctPeriodos" class="combo" ></select>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Rol Evaluador:
                </td>
                <td>
                    <asp:DropDownList ID="cboRolEvaluador" runat="server" Style="width: 175px" CssClass="combo"
                        AutoPostBack="True" OnSelectedIndexChanged="cboRolEvaluador_SelectedIndexChanged">
                        <asp:ListItem Value="4">Director de Ventas</asp:ListItem>
                        <asp:ListItem Value="5">Gerente de Región</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Evaluador:
                </td>
                <td>
                    <asp:DropDownList ID="cboEvaluador" runat="server" CssClass="combo"
                        Width="300px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Tipo Dialogo:
                </td>
                <td>
                    <asp:DropDownList ID="cboTipoDialogo" runat="server" CssClass="combo"
                        Width="300px">
                        <asp:ListItem Value="1">Normal</asp:ListItem>
                        <asp:ListItem Value="2">Plan de Mejora</asp:ListItem>
                    </asp:DropDownList>
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
            <tr>
                <td>
                    <asp:Button ID="btnBuscarProcesosAux" runat="server" Text="" OnClick="btnBuscarProcesosAux_Click" Style="display: none" />
                </td>
                <td colspan="2" style="padding-left: 200px">
                    <asp:Button ID="btnBuscarProcesos" runat="server" OnClientClick="ProcessOpen(); return false;" Text="Consultar" class="btnGonsultarDialogoStyle"  />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div style="margin: 0 0 0 20px; text-align: left;">
        <table style="width: 100%">
            <tr>
                <td>
                    <div id="divEnviado" runat="server" class="divResumenProceso" style="width: 250px; padding-right: 5px;">
                        <asp:GridView ID="gvInactivos" runat="server" CellPadding="4" ForeColor="#778391"
                            Width="100%" GridLines="None" AutoGenerateColumns="False" CssClass="grillaPaginada"
                            OnPreRender="GvPreRender" HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvInactivos_PageIndexChanging">
                            <PagerStyle Font-Bold="False" ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="cabecera_procesos" HorizontalAlign="Center" VerticalAlign="Middle"
                                Font-Bold="True" />
                            <PagerSettings Mode="NextPreviousFirstLast" />
                            <RowStyle CssClass="grilla_procesos" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                    <HeaderTemplate>
                                        POR INICIAR
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" class="link">
                                            <%# Eval("NombrePersona")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td>
                    <div class="divResumenProceso" id="divEnProceso" runat="server" style="width: 250px; padding-right: 5px;">
                        <asp:GridView ID="gvEnProceso" runat="server" CellPadding="4" ForeColor="#778391"
                            CssClass="grillaPaginada" Width="100%" GridLines="None" AutoGenerateColumns="False"
                            OnPreRender="GvPreRender" HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvEnProceso_PageIndexChanging">
                            <PagerStyle ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="cabecera_procesos" Font-Bold="true" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <PagerSettings Mode="NextPreviousFirstLast" />
                            <RowStyle CssClass="grilla_procesos" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                    <HeaderTemplate>
                                        PROCESO
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick="javascript:CargarResumen('<%# Eval("NombrePersona")%>', '<%# Eval("CodigoUsuario")%>', <%# Eval("IdProceso") %>, <%# Eval("IdRol") %>);"
                                            class="link">
                                            <%# Eval("NombrePersona")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td>
                    <div class="divResumenProceso" id="divAprobacion" runat="server" style="width: 250px; padding-right: 5px;">
                        <asp:GridView ID="gvEnAprobacion" runat="server" CellPadding="4" ForeColor="#778391"
                            CssClass="grillaPaginada" Width="100%" GridLines="None" AutoGenerateColumns="False"
                            OnPreRender="GvPreRender" HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvEnAprobacion_PageIndexChanging">
                            <PagerStyle ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="cabecera_procesos" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <PagerSettings Mode="NextPreviousFirstLast" />
                            <RowStyle CssClass="grilla_procesos" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                    <HeaderTemplate>
                                        APROBACIÓN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick="javascript:CargarResumen('<%# Eval("NombrePersona")%>', '<%# Eval("CodigoUsuario")%>', <%# Eval("IdProceso") %>, <%# Eval("IdRol") %>);"
                                            class="link">
                                            <%# Eval("NombrePersona")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td>
                    <div class="divResumenProceso" id="divAprobado" runat="server" style="width: 250px; padding-right: 5px; border-right: white;">
                        <asp:GridView ID="gvAprobados" runat="server" CellPadding="4" ForeColor="#778391"
                            CssClass="grillaPaginada" Width="100%" GridLines="None" AutoGenerateColumns="False"
                            OnPreRender="GvPreRender" HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvAprobados_PageIndexChanging">
                            <PagerStyle ForeColor="Black" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="cabecera_procesos" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <PagerSettings Mode="NextPreviousFirstLast" />
                            <RowStyle CssClass="grilla_procesos" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                    <HeaderTemplate>
                                        APROBADO
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick="javascript:CargarResumen('<%# Eval("NombrePersona")%>', '<%# Eval("CodigoUsuario")%>', <%# Eval("IdProceso") %>, <%# Eval("IdRol") %>);"
                                            class="link">
                                            <%# Eval("NombrePersona")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting" id="imgExporting" style="display: none" />

        <script type="text/javascript">

            function CargarResumen(nombreEvaluado, codEvaluado, idProceso, rolEvaluado) {

                var codPais = $("#<%=cboPaises.ClientID %>").val();
                var periodo = $("#slctPeriodos").val();
                var codEvaluador = $("#<%=cboEvaluador.ClientID %>").val();
                var pathUrl = "<%=Utils.RelativeWebRoot%>Admin/ResumenProceso.aspx?nomEvaluado=" + nombreEvaluado + "&codEvaluado=" + codEvaluado + "&idProceso=" + idProceso + "&rolEvaluado=" + rolEvaluado + "&codPais=" + codPais + "&periodo=" + periodo + "&codEvaluador=" + codEvaluador;

                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "RESUMEN DE PROCESO",
                    url: pathUrl,
                    minimizable: false,
                    maximizable: false,
                    bookmarkable: false,
                    resizable: false,
                    width: 750,
                    height: 600
                });

                return false;
            }


            function ProcessOpen() {
                
                $("#<%=HfPeriodo.ClientID %>").attr("value", $("#slctPeriodos").val());

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
                
                $("#<%=btnBuscarProcesosAux.ClientID %>").trigger("click");
            }

            function ProcessClose() {
                $.unblockUI({
                    onUnblock: function () { }
                });
            }

        </script>
</asp:Content>
