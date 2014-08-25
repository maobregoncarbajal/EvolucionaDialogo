<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeBehind="AntesNegocioEvaluado.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.AntesNegocioEvaluado" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionEvaluado.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var indexMenu = <%= IndexMenuServer%>;
        var indexSubMenu = <%= IndexSubMenu %>;
        var estado = <%= EstadoProceso %>;
        var mostrarMensaje = <%= EsCorrecto %>;
        var maximoMarcados = 2;
        var soloLectura = <%= ReadOnly %>;
        var avanze = <%=Porcentaje %>;

        jQuery(function() {
            var dlg = jQuery("#dialog-modelo").dialog({
                autoOpen : false,
                resizable: false,
                height: 150,
                width: 200,
                title: "TIPO DE DIÁLOGO",
                modal: true,
                open: function(event, ui) {
                    jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                }
            });
            dlg.parent().appendTo(jQuery("form:first"));

        });



        jQuery(document).ready(function () {

            if(indexMenu != null && indexMenu != 0){
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }

            if(estado != 1 || soloLectura == 1)
            {
                jQuery("#<%=contenidoPaso2.ClientID %> select").attr("disabled", "disabled");
                jQuery("#<%=btnGuardar.ClientID %>").css("display", "none");

                jQuery("#<%=cboVariablesAdicionales1.ClientID %>").attr("disabled", "disabled");
                jQuery("#<%=cboVariablesAdicionales2.ClientID %>").attr("disabled", "disabled");

                maximoMarcados = 0;
            }

            jQuery(".clsChhIndicadores").click(function(event){
                var cantidadChecksMarcados = jQuery("#divContenedorChecks input:checked").size();

                if(cantidadChecksMarcados > maximoMarcados)
                    event.preventDefault();
            });

            MostrarAvance(avanze);

            jQuery(".linkPDF").click(function(event){
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "MAPA DE VARIABLES CAUSA",
                    url: this.href,
                    width: 860,
                    height: 433,
                    minimizable: false,
                    bookmarkable: false
                });

                event.preventDefault();
            });

            jQuery(".clsBuscarIndicadores").click(function(event){
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "BÚSQUEDA INDICADORES",
                    url: this.href,
                    width: 800,
                    height: 500,
                    minimizable: false,
                    bookmarkable: false
                });

                event.preventDefault();
            });

            jQuery("#msgProceso").dialog({
                autoOpen : false,
                modal: true,
                title: "PROCESO COMPLETADO",
                open: function(event, ui) {
                    jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                }
            });

            jQuery("#msnInformacion").dialog({
                autoOpen : false,
                modal: true,
                title: "AVISO" ,
                buttons:
                {
                    Ok: function () {
                        jQuery(this).dialog("close");
                    }
                }
            });
            
            
            jQuery("#<%=btnGuardar.ClientID%>").click(function(event) {
                //                var combos = jQuery("#<%=contenidoPaso2.ClientID %> select");
                //                combos.each(function(index){
                //                    var valorSeleccionado = jQuery(this).val();
                //                    if(valorSeleccionado == "")
                //                    {
                //                        Mensaje("ERROR", "Debe Seleccionar todas las Variables");
                //                        event.preventDefault();
                //                        return false;
                //                    }
                //                });
                var vc1 = jQuery("#<%=ddlVariableCausa1.ClientID%>").val();
                var vc2 = jQuery("#<%=ddlVariableCausa2.ClientID%>").val();
                var vc3 = jQuery("#<%=ddlVariableCausa3.ClientID%>").val();
                var vc4 = jQuery("#<%=ddlVariableCausa4.ClientID%>").val();

                if ((vc1 == "" && vc2 == "")||(vc3 == "" && vc4 == "")) {
                    Mensaje("ERROR", "Debe Seleccionar al menos una variable de causa por cada variable de enfoque");
                    event.preventDefault();
                    return false;
                }
                
                
            });

            if(mostrarMensaje == 1)
            {
                mostrarMensaje = 0;
                jQuery("#msgProceso").dialog("open");
            }

            jQuery("#<%=btnAceptar.ClientID %>").click(function(event) {
                if (!ValidarChecks()) {
                    event.preventDefault();
                } else {
                    ModeloDialogo();
                    event.preventDefault();
                }
                
            });
            
            
            jQuery(".btnLnkStyle").click(function(event) {

                var hostName = window.location.hostname;
                var pathname = "http://" + hostName + GetPortNumber() + "/Desempenio/Consultas/";
                var periodoUsuarioEvaluado = jQuery("#<%=cboPeriodosFiltro.ClientID%>").val();
                var codigoUsuarioEvaluado = jQuery("#<%=ddlGerentes.ClientID%>").val();
                var nombreUsuarioEvaluado = jQuery("#<%=ddlGerentes.ClientID %> option:selected").text();
                var descripcionRol = jQuery("#<%=lblGerente.ClientID%>").text().toUpperCase();

                pathname += "variablesNegocio.aspx?periodo=" + periodoUsuarioEvaluado + "&codigo=" + codigoUsuarioEvaluado + "&nombre=" + nombreUsuarioEvaluado + "&rol=" + descripcionRol;

                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "HISTORICO ANTES/NEGOCIO GERENTE DE " + descripcionRol + ".",
                    url: pathname,
                    width: 1000,
                    height: 580,
                    minimizable: false,
                    bookmarkable: false
                });

                event.preventDefault();
            });
            
            

            var cantidadFilas = 0;
            var visible = jQuery("#<%=contenidoPaso1.ClientID %>").length;

            cantidadFilas = jQuery("#<%=gvVariables.ClientID %> tr").length;

            if(cantidadFilas <= 3 && visible == 1)
            {
                Mensaje("ERROR", "No se encontraron resultados para la búsqueda actual...");
            }

        });

        function ValidarChecks()
        {
            var cantidadChecksMarcados = jQuery("#divContenedorChecks input:checked").size();
            if(cantidadChecksMarcados != 2)
            {
                Mensaje("ERROR", "Se deben seleccionar dos Variables");
                return false;
            }

            return true;
        }

        function Mensaje(titulo, mensaje)
        {
            jQuery("#msnInformacion").text(mensaje);
            jQuery("#msnInformacion").dialog("title", titulo);
            jQuery("#msnInformacion").dialog("open");
        }

        function ModeloDialogo() {
            
            jQuery("#dialog-modelo").dialog("open");
            jQuery("#dialog-modelo").dialog("widget").next(".ui-widget-overlay").css("background", "#C0C0C0");
            
        }
        
        function CerrarModeloDialogo() {
            jQuery("#dialog-modelo").dialog("close");
        }
       
   
    </script>
    <style type="text/css">
        .cssBtnModDia {
            padding: 3px 4px !important;
            background: #cccccc !important;
            color: #000000 !important;
            -webkit-border-radius: 4px !important;
            -moz-border-radius: 4px !important;
            border-radius: 4px !important;
            text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.4) !important;
            -webkit-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.4), 0 1px 1px rgba(0, 0, 0, 0.2) !important;
            -moz-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.4), 0 1px 1px rgba(0, 0, 0, 0.2) !important;
            box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.4), 0 1px 1px rgba(0, 0, 0, 0.2) !important;
            text-decoration: none !important;
            font: normal 12px Arial !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="msnInformacion" style="font-size: 9pt"></div>
    <div id="dialog-modelo" align="left">
        <br />
        <div align="left" style="width: auto; min-height: 0px; height: 56px;">
            <asp:RadioButton ID="rbNormal" GroupName="rbModeloDialogo" Text="Normal" runat="server" Checked="True" /><br />
            <asp:RadioButton ID="rbPlanMejora" GroupName="rbModeloDialogo" Text="Plan de Mejora" runat="server" /><br />
        </div>
        <hr />
        <div>
            <div>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAceptarAux" runat="server" Text="Continuar" CssClass="cssBtnModDia"
                    OnClick="btnAceptarAux_Click" />&nbsp;&nbsp;&nbsp;
		        <input type="button" value="Cerrar" class="cssBtnModDia" onclick="CerrarModeloDialogo()" />
            </div>
        </div>
    </div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        Has completado tu Proceso:<br />
        DIÁLOGO/ANTES/NEGOCIO
        <br />
        <br />
        Haz click aquí para continuar con el proceso:
        <%
            if (lblNuevas.Text == "NUEVA")
            {%>
        <a href="<%=Utils.RelativeWebRoot %>Desempenio/DuranteNegocioEvaluado.aspx"
            id="urlDestino">DIÁLOGO/DURANTE/NEGOCIO</a>
        <%}
            else
            {%>
        <a href="<%=Utils.RelativeWebRoot %>Desempenio/AntesEquiposEvaluado.aspx"
            id="A1">DIÁLOGO/ANTES/EQUIPOS</a>
        <%}%>
    </div>
    <div style="margin: 35px 0 0 15px" id="divPaso1Negocio">
        <div id="contenedorFiltros" runat="server" class="divFiltroPeriodo3 roundedDiv">
            <table style="text-align: left; margin-top: 5px; margin-left: 15px;">
                <tr>
                    <td colspan="2">
                        <br />
                        Historico Antes/Negocio Gerente de
                        <asp:Label ID="lblGerente" runat="server" Text=""></asp:Label>:.
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <span class="texto_Negro">Período :</span>
                    </td>
                    <td>
                        <br />
                        <asp:DropDownList runat="server" ID="cboPeriodosFiltro" Width="80px" Style="margin-left: 7px;"
                            CssClass="combo" OnSelectedIndexChanged="cboPeriodosFiltro_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="2014 II">2014 II</asp:ListItem>
                            <asp:ListItem Value="2014 I">2014 I</asp:ListItem>
                            <asp:ListItem Value="2013 III">2013 III</asp:ListItem>
                            <asp:ListItem Value="2013 II ">2013 II </asp:ListItem>
                            <asp:ListItem Value="2013 I  ">2013 I  </asp:ListItem>
                            <asp:ListItem Value="2012 III">2012 III</asp:ListItem>
                            <asp:ListItem Value="2012 II ">2012 II </asp:ListItem>
                            <asp:ListItem Value="2012 I  ">2012 I  </asp:ListItem>
                            <asp:ListItem Value="2011 III">2011 III</asp:ListItem>
                            <asp:ListItem Value="2011 II ">2011 II </asp:ListItem>
                            <asp:ListItem Value="2011 I  ">2011 I  </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto_Negro">
                            <asp:Label class="texto_Negro" ID="lblBucaGerente" runat="server" Text=""></asp:Label>
                            :</span>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGerentes" Width="250px" Style="margin-left: 7px;"
                            CssClass="combo" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-left: 240px">
                        <br />
                        <asp:LinkButton ID="btnBusVariGerente" runat="server" Text="CONSULTAR" class="btnLnkStyle"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 15px 0 0 15px; text-align: left; width: 100%" id="contenidoPaso1"
            runat="server">
            <table style="width: 700px; float: left">
                <tr>
                    <td>
                        <br />
                        <a href="<%=Utils.RelativeWebRoot%>PantallasModales/AntesNegocio_Busqueda.aspx"
                            id="btnBuscarIndicadores" class="clsBuscarIndicadores">Consultar Resultados Acumulados</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <%--<div style="margin-top: 12px;" id="divContenedorChecks">--%>
                        <fieldset style="margin-top: 10px; padding-top: 15px" id="divContenedorChecks">
                            <legend>Selecciona tus Variables de Enfoque (Máximo 02 Variables)</legend>
                            <asp:GridView ID="gvVariables" Width="750px" runat="server" AutoGenerateColumns="False">
                                <EmptyDataTemplate>
                                    <table width="750px">
                                        <tr class="cabecera_indicadores">
                                            <th style="width: 180px;">Variables
                                            </th>
                                            <th style="width: 90px;">Meta
                                            </th>
                                            <th style="width: 90px;">Resultado
                                            </th>
                                            <th style="width: 90px;">Diferencia
                                            </th>
                                            <th style="width: 90px;">(%)
                                            </th>
                                            <th style="width: 120px;">Campa&#241;a
                                            </th>
                                        </tr>
                                        <tr class="grilla_indicadores">
                                            <td colspan="6" align="center" style="font-size: 14pt; color: #0caed7; font-style: italic">No se encontraron resultados para la búsqueda actual...
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="cabecera_indicadores" />
                                <RowStyle CssClass="grilla_indicadores" />
                                <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Porcentaje" DataFormatString="{0:N}%" HeaderText="(%)">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Variable de Enfoque">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbEstado" CssClass="clsChhIndicadores" name="CheckBox1" runat="server"
                                                AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <table style="width: 750px">
                                <thead class="cabecera_indicadores">
                                    <tr>
                                        <th>Variable
                                        </th>
                                        <th style="width: 90px; text-align: right;">Meta
                                        </th>
                                        <th style="width: 90px; text-align: right;">Resultado
                                        </th>
                                        <th style="width: 90px; text-align: right;">Diferencia
                                        </th>
                                        <th style="width: 90px; text-align: right;">(%)
                                        </th>
                                        <th id="headerCampanha" runat="server" style="width: 90px; text-align: center;">Campa&ntilde;a
                                        </th>
                                        <th style="text-align: center; width: 120px;">Variable de Enfoque
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="grilla_indicadores">
                                        <td align="left">
                                            <asp:DropDownList runat="server" ID="cboVariablesAdicionales1" Width="180px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboVariablesAdicionales1_SelectedIndexChanged" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblMeta1" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblResultado1" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblDiferencia1" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblPorcentaje1" />
                                        </td>
                                        <td style="text-align: center; width: 90px;" id="filaCampanha1" runat="server">
                                            <asp:Label Text="text" runat="server" ID="lblCampanha1" />
                                        </td>
                                        <td style="text-align: center; width: 120px;">
                                            <asp:CheckBox runat="server" ID="chkEstadoVariableAdicional1" CssClass="clsChhIndicadores" />
                                        </td>
                                    </tr>
                                    <tr class="grilla_indicadores">
                                        <td align="left">
                                            <asp:DropDownList runat="server" ID="cboVariablesAdicionales2" Width="180px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboVariablesAdicionales1_SelectedIndexChanged" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblMeta2" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblResultado2" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblDiferencia2" />
                                        </td>
                                        <td class="texto_Derecha" style="width: 90px;">
                                            <asp:Label Text="text" runat="server" ID="lblPorcentaje2" />
                                        </td>
                                        <td style="text-align: center; width: 90px;" id="filaCampanha2" runat="server">
                                            <asp:Label Text="text" runat="server" ID="lblCampanha2" />
                                        </td>
                                        <td style="text-align: center; width: 120px;">
                                            <asp:CheckBox runat="server" ID="chkEstadoVariableAdicional2" CssClass="clsChhIndicadores" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <%--</div>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 15px">
                        <asp:Button ID="btnAceptar" runat="server" CssClass="btnAceptarStyle" />
                    </td>
                </tr>
            </table>
            <asp:TextBox runat="server" ID="txtIdVariable1" Style="display: none;" />
            <asp:TextBox runat="server" ID="txtIdVariable2" Style="display: none;" />
        </div>
        <div id="contenidoPaso2" runat="server" style="margin: 15px 0 0 35px; text-align: left;">
            <table style="width: 772px;" id="table1">
                <tr>
                    <td colspan='7'>
                        <span class="subTituloMorado">Selecciona las variables de causa</span><br />
                        <br />
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="width: 11px"></td>
                    <td colspan="6"></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 171px" colspan="3">
                        <asp:Label ID="lblvariable1Desc" runat="server" Text="[Variable no Seleccionada]"
                            CssClass="variables_indicadores"></asp:Label>
                        <a href="<%=Utils.RelativeWebRoot%>Files/arbolVer.jpg" style="color: Black"
                            class="linkPDF">Ver árbol de variables</a>
                        <asp:Label ID="lblvariable1" runat="server" Font-Bold="true" Style="display: none;"></asp:Label>
                    </td>
                    <td align="center" style="width: 181px"></td>
                    <td align="center" style="width: 148px"></td>
                    <td align="center" style="width: 148px"></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px; height: 18px"></td>
                    <td colspan="6"></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px" align="left">
                        <asp:Label ID="Label1" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 168px">
                        <asp:Label ID="Label4" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 181px">
                        <asp:Label ID="Label5" runat="server" Text="Real" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:Label ID="Label6" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:Label ID="Label3" runat="server" Text="%" CssClass="variables"></asp:Label>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px" align="center">
                        <asp:DropDownList ID="ddlVariableCausa1" runat="server" Width="120px" AutoPostBack="True"
                            CssClass="combo" OnSelectedIndexChanged="ddlVariableCausa1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 168px" align="center">
                        <asp:TextBox ID="TextBox5" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 181px" align="center">
                        <asp:TextBox ID="TextBox6" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 148px" align="center">
                        <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:TextBox ID="TextBox17" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px" align="left">
                        <asp:Label ID="Label7" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 168px">
                        <asp:Label ID="Label8" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 181px">
                        <asp:Label ID="Label9" runat="server" Text="Real" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:Label ID="Label10" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:Label ID="Label19" runat="server" Text="%" CssClass="variables"></asp:Label>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px" align="center">
                        <asp:DropDownList ID="ddlVariableCausa2" runat="server" Width="120px" AutoPostBack="True"
                            CssClass="combo" OnSelectedIndexChanged="ddlVariableCausa2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 168px" align="center">
                        <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 181px" align="center">
                        <asp:TextBox ID="TextBox9" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 148px" align="center">
                        <asp:TextBox ID="TextBox10" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:TextBox ID="TextBox18" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="height: 31px; width: 11px;"></td>
                    <td colspan="6" style="height: 31px"></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 171px" colspan="3">
                        <asp:Label ID="lblvariable2Desc" runat="server" Text="[Variable no Seleccionada]"
                            CssClass="variables_indicadores"></asp:Label>
                        <a href="<%=Utils.RelativeWebRoot%>Files/arbolVer.jpg" style="color: Black"
                            class="linkPDF">Ver árbol de variables</a>
                        <asp:Label ID="lblvariable2" runat="server" Font-Bold="True" Style="display: none;"></asp:Label>
                    </td>
                    <td style="width: 181px"></td>
                    <td style="width: 148px"></td>
                    <td style="width: 148px"></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px; height: 21px"></td>
                    <td colspan="6"></td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="height: 21px; width: 11px;"></td>
                    <td style="width: 50px;"></td>
                    <td style="height: 21px; width: 131px;" align="left">
                        <asp:Label ID="Label11" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                    </td>
                    <td style="height: 21px; width: 168px;" align="center">
                        <asp:Label ID="Label12" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                    </td>
                    <td style="height: 21px; width: 181px;" align="center">
                        <asp:Label ID="Label13" runat="server" Text="Real" CssClass="variables"></asp:Label>
                    </td>
                    <td style="height: 21px; width: 148px;" align="center">
                        <asp:Label ID="Label14" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px; height: 21px">
                        <asp:Label ID="Label20" runat="server" Text="%" CssClass="variables"></asp:Label>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px" align="center">
                        <asp:DropDownList ID="ddlVariableCausa3" runat="server" Width="120px" AutoPostBack="True"
                            CssClass="combo" OnSelectedIndexChanged="ddlVariableCausa3_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 168px" align="center">
                        <asp:TextBox ID="TextBox11" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 181px" align="center">
                        <asp:TextBox ID="TextBox12" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 148px" align="center">
                        <asp:TextBox ID="TextBox13" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:TextBox ID="TextBox19" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="width: 11px"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px" align="left">
                        <asp:Label ID="Label15" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 168px">
                        <asp:Label ID="Label16" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 181px">
                        <asp:Label ID="Label17" runat="server" Text="Real" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:Label ID="Label18" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
                    </td>
                    <td align="center" style="width: 148px">
                        <asp:Label ID="Label21" runat="server" Text="%" CssClass="variables"></asp:Label>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="height: 26px; width: 11px;"></td>
                    <td style="width: 50px;"></td>
                    <td style="width: 131px; height: 26px" align="center">
                        <asp:DropDownList ID="ddlVariableCausa4" runat="server" Width="120px" AutoPostBack="True"
                            CssClass="combo" OnSelectedIndexChanged="ddlVariableCausa4_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 168px; height: 26px" align="center">
                        <asp:TextBox ID="TextBox14" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 181px; height: 26px" align="center">
                        <asp:TextBox ID="TextBox15" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td style="width: 148px; height: 26px" align="center">
                        <asp:TextBox ID="TextBox16" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 148px; height: 26px">
                        <asp:TextBox ID="TextBox20" runat="server" ReadOnly="True" CssClass="inputtext"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <div style="width: 720px; text-align: right; padding-right: 55px">
                <asp:LinkButton Text="Regresar " runat="server" ID="lnkRegresar" OnClick="lnkRegresar_Click" />
                <asp:HyperLink Text="| Siguiente" ID="hlSiguiente" NavigateUrl="DuranteNegocioEvaluado.aspx"
                    Visible="False" runat="server"></asp:HyperLink>
                <br />
                <br />
                <asp:Button runat="server" CssClass="btnGuardarStyle" ID="btnGuardar" OnClick="btnGuardar_Click"
                    Text="SIGUIENTE" />
                <br />
                <br />
            </div>
        </div>
    </div>
    <asp:Label ID="lblNuevas" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="hfModeloDialogo" runat="server" />
    <asp:HiddenField ID="hftipoDialogoDesempenio" runat="server" />
</asp:Content>
