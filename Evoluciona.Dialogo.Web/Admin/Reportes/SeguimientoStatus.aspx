<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="SeguimientoStatus.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Reportes.SeguimientoStatus" ValidateRequest="false" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuReportes.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>
    
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Evoluciona.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <br />
    <asp:HiddenField ID="HdnValue" runat="server" />
    <asp:HiddenField ID="HfPeriodo" runat="server" />
    <asp:HiddenField ID="HfPeriodoD" runat="server" />
    <div>
        <div id="panelUno">
            <table style="width: 100%">
                <tr>
                    <td colspan="11" style="text-align: right">&nbsp;</td>
                </tr>
                <tr style="height: 20px;">
                    <td colspan="11"></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="11">
                        <div style="text-align: center;" >
                            <b>Cuadro Resumen</b></div>
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
                        <asp:DropDownList ID="ddlPaises" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
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
                    <td style="text-align: left">
                        <span class="texto_Negro">Dialogo:</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNivel" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="2">DV a GR</asp:ListItem>
                            <asp:ListItem Value="3">GR a GZ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <span class="texto_Negro">Período :</span>
                    </td>
                    <td style="text-align: left">
                        <select id="slctPeriodos" class="combo"></select>
                    </td>
                    <td>
                        <span class="texto_Negro">Tipo:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipo" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="1">Normal</asp:ListItem>
                            <asp:ListItem Value="2">Plan de Mejora</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:CheckBox ID="cbUsuIna" runat="server" Text=" Incluir usuarios inactivos" class="texto_Negro" />
                    </td>
                    <td></td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="10">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto_Negro">Modelo:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlModDialogo" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="MNOR">MOD NORMAL</asp:ListItem>
                            <asp:ListItem Value="MPDM">MOD PLAN DE MEJORA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left"></td>
                    <td></td>
                    <td></td>
                    <td style="text-align: left"></td>
                    <td></td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left" colspan="2"></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <br />
                        <asp:Button ID="btnBuscar" runat="server" OnClientClick="ProcessOpen(1); return false;" Text="Buscar" class="btnGuardarStyle" />
                        <asp:Button ID="btnExportar" runat="server" Text="Excel" class="btnGuardarStyle" OnClientClick="ExportDivDataToExcel()" OnClick="btnExportar_Click" />
                        <asp:Button ID="btnBuscarAux" runat="server" OnClick="btnBuscarAux_Click" Text="Button" Style="display: none" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <br />
                        <div id="grillaUno" runat="server"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="panelDos">
            <table style="width: 100%">
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B" colspan="11">
                        <div style="text-align: center;" >
                            Detalle<br/>
                            campos Filtro</div>
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
                        <asp:DropDownList ID="ddlPaisesD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
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
                    <td style="width: 20px"></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Dialogo:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlNivelD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="2">DV a GR</asp:ListItem>
                            <asp:ListItem Value="3">GR a GZ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Período:</span>
                    </td>
                    <td style="text-align: left">
                        <select id="slctPeriodosD" class="combo"></select>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Estado:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlEstatusD" runat="server" Width="125px" CssClass="combo"
                            Style="margin-left: 7px;">
                            <asp:ListItem Value="">Todos</asp:ListItem>
                            <asp:ListItem Value="0">Por iniciar</asp:ListItem>
                            <asp:ListItem Value="1">En Proceso</asp:ListItem>
                            <asp:ListItem Value="2">Por Aprobar</asp:ListItem>
                            <asp:ListItem Value="3">Aprobado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="texto_Negro">Tipo:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="1">Normal</asp:ListItem>
                            <asp:ListItem Value="2">Plan de Mejora</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td colspan="2" style="text-align: left">
                        <asp:CheckBox ID="cbUsuInaD" runat="server" Text=" Incluir usuarios inactivos" class="texto_Negro" />
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Nombre Evaluado:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreColaboradorD" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td style="text-align: left">
                        <span class="texto_Negro">Nombre Evaluador:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtNombreJefeD" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="texto_Negro">Modelo:</span>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlModDialogoD" runat="server" Width="125px" CssClass="combo" Style="margin-left: 7px;">
                            <asp:ListItem Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="MNOR">MOD NORMAL</asp:ListItem>
                            <asp:ListItem Value="MPDM">MOD PLAN DE MEJORA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td colspan="2" style="text-align: left"></td>
                    <td></td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left"></td>
                    <td></td>
                    <td style="text-align: left"></td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <br />
                        <asp:Button ID="btnBuscarD" runat="server" Text="Buscar" OnClientClick="ProcessOpen(2); return false;" class="btnGuardarStyle" />
                        <asp:Button ID="btnExportarD" runat="server" Text="Excel" OnClick="btnExportarD_Click" class="btnGuardarStyle" />
                        <asp:Button ID="btnBuscarDAux" runat="server" OnClick="btnBuscarDAux_Click" Text="Button" Style="display: none" />
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="11"></td>
                </tr>
                <tr>
                    <td colspan="11">
                        <div id="grillaDos" runat="server">
                            <div style="text-align: center;" >
                                <asp:GridView ID="gvDetalle" runat="server" BackColor="White" BorderColor="White"
                                              BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                              ShowFooter="True">
                                    <RowStyle BackColor="#CCC0DA" ForeColor="black" BorderColor="White" BorderStyle="Solid"
                                              BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="Periodo" HeaderText="PERIODO">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                         VerticalAlign="Middle" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                       BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Pais" HeaderText="PAIS">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                       BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario" HeaderText="USUARIO">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                       BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Rol" HeaderText="ROL">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                                       BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreEvaluado" HeaderText="NOMBRE_EVALUADO">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="300px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                       BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Evaluador" HeaderText="EVALUADOR">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                       BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RolEvaluador" HeaderText="ROL_EVALUADOR">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                                       BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreEvaluador" HeaderText="NOMBRE_EVALUADOR">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle Width="300px" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"
                                                       BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado" HeaderText="ESTADO">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                                       BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TipoDialogo" HeaderText="TIPO_DIALOGO">
                                            <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" BorderStyle="Solid"
                                                       BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ModeloDialogo" HeaderText="MODELO_DIALOGO">
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
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <hr class="hrPunteado" />
    </div>
    <div id="divExport" style="display: none">
        <table style="margin-left: 20px">
            <tr>
                <td style="color: White; background-color: #60497B" colspan="13">
                    <div style="text-align: center;" >
                        <p>
                            REPORTE: Seguimiento Status de Diálogos Evoluciona<br/>
                            OBJETIVO:
                            <br/>
                            *Monitoreo al % de FFVV que cumplió con su Diálogo Evoluciona<br/>
                            *Identificar % de evaluadores que completaron los diálogos de su equipo - Por tipo
                            de evaluador<br/>
                            *Monitoreo del Estatus de los Diálogos - FLUJO<br/>
                        </p>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td colspan="13"></td>
            </tr>
            <tr>
                <td style="color: White; background-color: #60497B" colspan="13">
                    <div style="text-align: center;" >
                        Cuadro Resumen :</div>
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
                <td style="color: White; background-color: #60497B">Dialogo:
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
                <td style="color: White; background-color: #60497B">Tipo:
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
        var periodoSearch = "<%=PeriodoBuscado %>";
        var periodoSearchD = "<%=PeriodoBuscadoD %>";
        jQuery(document).ready(function () {
            
            var url = "<%=Utils.RelativeWebRoot%>Handler/HelperHandler.ashx";
            
            Evoluciona.LoadDropDownList("slctPeriodos", url, { 'accion': 'cargarPeriodos', 'pais': '00' }, 0, true, false);
            Evoluciona.LoadDropDownList("slctPeriodosD", url, { 'accion': 'cargarPeriodos', 'pais': '00' }, 0, true, false);
            
            $("#slctPeriodos").change(function () {
                $("#<%=HfPeriodo.ClientID %>").attr("value", $("#slctPeriodos").val());
            });
            
            $("#slctPeriodosD").change(function () {
                $("#<%=HfPeriodoD.ClientID %>").attr("value", $("#slctPeriodosD").val());
            });
            
            $("#slctPeriodos").val(periodoSearch);
            $("#slctPeriodosD").val(periodoSearchD);

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

            var lista = ["spPeriodo", "spPais", "spNivel",
            "grillaUnoClone"];
            LimpiarVariablesReporteHtml(lista);

            $("#spPeriodo").append($("#slctPeriodos option:selected").text());
            $("#spPais").append($("#<%=ddlPaises.ClientID %> option:selected").text());
            $("#spNivel").append($("#<%=ddlNivel.ClientID %> option:selected").text());
            $("#spEstado").append($("#<%=ddlTipo.ClientID %> option:selected").text());


            $("#<%=grillaUno.ClientID%>").clone().appendTo('#grillaUnoClone');


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
            
            $("#<%=HfPeriodo.ClientID %>").attr("value", $("#slctPeriodos").val());
            $("#<%=HfPeriodoD.ClientID %>").attr("value", $("#slctPeriodosD").val());


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
