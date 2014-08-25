<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="DuranteCompetencias.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.DuranteCompetencias" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacion.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .subTituloEncuesta {
            font-family: Arial;
            font-size: 12px;
            color: #92278f;
            font-weight: bold;
        }
    </style>

    <script src="../Jscripts/ui.datepicker-es.js" type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.maxlength.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;
        var mostrarMensaje = <%= esCorrecto %>;
        var estado = <%= estadoProceso %>;
        var mostrarError = <%= huboError %>;
        var soloLectura = <%=readOnly %>;
        var avanze = <%=porcentaje %>;
        var abortar = 0;

        jQuery(document).ready(function() {

            $('.textoRespuesta').maxlength({max: 600, feedbackText: ''});
            $('#<%=txtOtros.ClientID %>').maxlength({max: 200, feedbackText: ''});

            jQuery("#noIngresar").dialog({
                autoOpen: false,
                modal: true,
                title: "NAVEGACIÓN NO PERMITIDA",
                close: function(event, ui)
                {
                    window.location = "AntesNegocio.aspx";
                },
                buttons:
                    {
                        Ok: function() {
                            jQuery(this).dialog("close");
                            window.location = "AntesNegocio.aspx";
                        }
                    }
            });

            jQuery("#<%=lnkMarcarTodos.ClientID %>").click(function(event) {
                 jQuery("#<%=TreeViewCheck.ClientID %> input").attr("checked", true);
                 event.preventDefault();
             });

            jQuery("#lblEncuesta").live("click", function() {
                var idProcesoUsuario = <%= idProceso%>;
                 jQuery("#lblEncuesta").attr("href", "<%=Utils.RelativeWebRoot %>Desempenio/EncuestaEvaluado.aspx?tipoEncuesta=0&idProceso="+idProcesoUsuario);
                 window.location = "<%=Utils.RelativeWebRoot %>Desempenio/EncuestaEvaluado.aspx?tipoEncuesta=0&idProceso="+idProcesoUsuario;
             });

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }

            if (estado != 1)
            {
                jQuery("#<%=dlPreguntas1.ClientID%> textarea, #<%=txtOtros.ClientID %>").attr("readOnly", "readOnly");
                 jQuery("#<%=lnkMarcarTodos.ClientID %>").css("display", "none");
                 jQuery("#<%=TreeViewCheck.ClientID %> input").click(function(event) {
                     event.preventDefault();
                 });

                 if (indexMenu == 2) {
                     jQuery("#<%=btnGuardar.ClientID %>").css("display", "none");
                     jQuery(".btnEliminarVisita").css("display", "none");
                     jQuery(".btnModificarVisita").css("display", "none");
                 }
             }
             else
             {
                 if (soloLectura == 1 || indexMenu == 3)
                     abortar = 1;
             }

            if (estado == 2 && indexMenu == 3)
            {
                abortar = 1;
            }

            if (estado == 3)
            {
                if (indexMenu == 3)
                {
                    if (soloLectura == 0)
                    {
                        jQuery("#<%=cboEstadoIndicador1.ClientID %>").css("display", "block");
                         jQuery("#lblAccion").text("DESPUÉS");
                         jQuery("#lblContenido").text("Ya finalizaste tu proceso de Seguimiento");
                         //jQuery("#lblUrl").attr("href", "<%=Utils.RelativeWebRoot %>Desempenio/EncuestaEvaluado.aspx");
                         //jQuery("#lblUrl").text("Te pedimos tu Opinión sobre el proceso Por favor completar la encuesta");
                     }
                     else
                     {
                         abortar = 1;
                     }
                 }
             }

            MostrarAvance(avanze);

            jQuery("#divMensaje").dialog({
                autoOpen: false,
                modal: true,
                title: "ERROR DE VALIDACIÓN",
                buttons:
                    {
                        Ok: function() {
                            jQuery(this).dialog("close");
                        }
                    }
            });

            jQuery("#msgProceso").dialog({
                autoOpen: false,
                modal: true,
                title: "PROCESO COMPLETADO",
                open: function(event, ui) {
                    jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                }
            });

            if (mostrarError == 1)
            {
                mostrarError = 0;
                jQuery("#divMensaje").dialog("open");
            }

            if (mostrarMensaje == 1)
            {
                mostrarMensaje = 0;
                jQuery("#msgProceso").dialog("open");
            }

            if (abortar == 1)
            {
                jQuery("#noIngresar").dialog("open");
            }

            $('#alertaFecha').dialog({
                autoOpen: false,
                modal: true,
                width: 200,
                height:60,
                resizable: true
            });
            $("#alertaFecha").parent().appendTo($("form:first"));

            if(jQuery('#<%=txtFechaVista.ClientID%>').css('display') != 'none')
             {
                 jQuery('#<%=txtFechaVista.ClientID%>').datepicker({
                     showOn: "button",
                     buttonImage: "<%=Utils.RelativeWebRoot %>Images/cal.png",
                     buttonImageOnly: true,
                     onSelect: function(dateText) {
                         if ($.isEmptyObject($("#<%=hidIdVisita.ClientID%>").val()) ||
                                 $("#<%=hidIdVisita.ClientID%>").val() == "0") {
                                 $('#<%=btnCargarCampanhas.ClientID%>').trigger('click');
                             }
                             document.getElementById('<%=btnAgregarActualizarVisita.ClientID %>').focus();
                         }});
                 }

            jQuery(".linkCampania").live("click", function() {

                document.getElementById('<%=btnGuardar.ClientID %>').focus();
             });

            jQuery("#ctl00_ContentPlaceHolder1_lnkEncuesta").click(function() {
                jQuery("#msgProceso").dialog("close");
                window.location = "<%=Utils.RelativeWebRoot %>Desempenio/ResumenProceso.aspx";
             });
             
             
            jQuery(".btnEnviarAprobacionStyle").click(function(event) {
                

                var diferencia = $("#<%=hfDiferencia.ClientID%>").val();
                
                 if(diferencia == "1"){

                     var hostName = window.location.hostname;
                     var pathname = "http://" + hostName + GetPortNumber() + "/Desempenio/Encuestas/";
                     var codTipoEncuesta = $("#<%=hfCodTipoEncuesta.ClientID%>").val();
                    var codigoUsuario = $("#<%=hfCodigoUsuario.ClientID%>").val();
                    var periodo = $("#<%=hfPeriodo.ClientID%>").val();
                    pathname += "Encuesta.aspx?codTipoEncuesta=" + codTipoEncuesta + "&codigoUsuario=" + codigoUsuario + "&periodo=" + periodo;
                
                    jQuery.window({
                        showModal: true,
                        modalOpacity: 0.5,
                        title: "ENCUESTA",
                        url: pathname,
                        width: 1000,
                        height: 580,
                        minimizable: false,
                        bookmarkable: false
                    });

                    event.preventDefault();
                }
             });
             
             
        });

        function validarFecha() {
            if($.isEmptyObject($("#<%=txtFechaVista.ClientID%>").val())) {
                jQuery(document).ready(function() { $('#alertaFecha').dialog('open'); });
            }
            return true;
        }
        
        function closeWindow(){

            jQuery(document).ready(function() { jQuery.window.closeAll(); });
            $("#<%=hfDiferencia.ClientID%>").val("0");
            $(".btnEnviarAprobacionStyle").trigger("click");
        }
        
       
        

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="noIngresar" style="font-size: 9pt">
        No es Posible ingresar a la P&aacute;gina debido a que aun no existen datos para
        su evaluaci&oacute;n
    </div>
    <div id="alertaFecha" style="font-size: 90%; display: none;" title="Alerta">
        <table cellspacing="5">
            <tr style="height: 25px;">
                <td style="text-align: left; padding-right: 1px;">Debe Ingresar una Fecha
                </td>
            </tr>
        </table>
    </div>
    <div id="divMensaje" style="font-size: 9pt">
        <asp:Literal runat="server" ID="lblMensajes" Text=""></asp:Literal>
    </div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        Has completado tu Proceso:<br />
        DIÁLOGO/<span id="lblAccion">DURANTE</span>/COMPETENCIAS
        <br />
        <br />
        <span id="lblContenido">Ya finalizaste tu proceso de Di&aacute;logo.
            <br />
            Recuerda hacer el monitoreo de los planes de acci&oacute;n acordados con tu equipo
            <br />
        </span>
        <br />
        <%--        <span><a href="" id="lblEncuesta">Te pedimos tu Opinión sobre el proceso Por favor completar
            la encuesta</a>
            <br />
        </span>--%>
        <div id="divEnlace" runat="server">
            <span id="lblEnunciado" runat="server"></span>
            <br />
            <a id="lnkEncuesta" style="color: #00acee;" target="_blank" href="" runat="server"></a>
            <br />
            <br />
            <span id="lblInformacionDni" runat="server" style="font-weight: bold"></span>
            <br />
            <br />
        </div>
        <div style="float: left;">
            <a style="color: #00acee;" href="<%=Utils.RelativeWebRoot %>Desempenio/ResumenProceso.aspx"
                id="lblUrl">Cerrar</a><br />
        </div>
    </div>
    <div style="margin: 35px 0 0 55px; text-align: left">
        <table border="0" width="100%">
            <tr>
                <td style="height: 21px" align="left">
                    <table>
                        <tr>
                            <td align="left" class="texto_Negro" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Ingresa la retroalimentación dada a
                                la
                                <asp:Label runat="server" ID="lblDescripcionRol" Text="Gerente de Region"></asp:Label>
                                sobre su Plan de Desarrollo de Competencias.
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="texto_descripciones" colspan="2" style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="left" class="pregunta" style="width: 125px"></td>
                            <td align="left" class="pregunta">
                                <br />
                                <asp:Label ID="lblEtiqueta" runat="server" Text="   Seleccione una Competencia :"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="12px" ForeColor="#00ACEE"></asp:Label>
                                <asp:DropDownList ID="ddlCompetencia" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="ddlCompetencia_SelectedIndexChanged" CssClass="combo">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCompetencia1" runat="server" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="right">
                                <asp:DropDownList runat="server" Width="150px" ID="cboEstadoIndicador1" Style="display: none; margin-right: 100px; margin-top: 15px">
                                    <asp:ListItem Text="En Proceso" Value="0" />
                                    <asp:ListItem Text="Completado" Value="1" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="pregunta" style="width: 125px"></td>
                            <td align="left" class="pregunta" style="width: 500px">
                                <div id="divPlan1">
                                    <asp:DataList ID="dlPreguntas1" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdPregunta" runat="server" Style="display: none" Text='<%# Eval("IdPregunta") %>'></asp:Label><br />
                                            <asp:Label ID="lblPregunta" runat="server" Text='<%# Eval("DescripcionPregunta") %>'></asp:Label><br />
                                            <asp:TextBox ID="txtRespuesta" Width="400px" Height="80px" runat="server" TextMode="MultiLine"
                                                CssClass="text_retro textoRespuesta" Text='<%# Eval("Respuesta") %>' MaxLength="600"
                                                Rows="5"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <asp:Panel ID="pnlMensaje" runat="server" Width="350px" Visible="false">
                                    <h1>Seleccione una opci&oacute;n</h1>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px"></td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td align="left">&nbsp; &nbsp;
                                            <asp:TreeView ID="TreeViewCheck" NodeStyle-VerticalPadding="2px" runat="server" ShowCheckBoxes="All"
                                                ShowExpandCollapse="False" ForeColor="#6a288a" Font-Size="13px" Font-Names="Arial" Visible="False">
                                            </asp:TreeView>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtOtros" MaxLength="200"
                                                TextMode="MultiLine" Height="51px" Width="390px" Visible="False"></asp:TextBox>
                                            <br />
                                            <br />
                                            <%--                                            <div id="divEnlace" runat="server">
                                                <span id="lblEnunciado" runat="server"></span>
                                                <br />
                                                <a id="lnkEncuesta" target="_blank" href="" runat="server"></a>
                                                <br />
                                                <br />
                                                <span id="lblInformacionDni" runat="server" style="font-weight: bold"></span>
                                                <br />
                                                <br />
                                            </div>--%>
                                            <asp:LinkButton runat="server" ID="lnkMarcarTodos" Text="(Marca Todas las alternativas)"
                                                ForeColor="#00acee" Font-Names="Arial" Font-Bold="true" Font-Size="12px" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px"></td>
                            <td>
                                <table cellspacing="5" width="100%" id="mostrarVisitas" runat="server" visible="False">
                                    <tr>
                                        <td colspan="3">
                                            <p>
                                                <br />
                                                <br />
                                            </p>
                                            <div class="subTituloMorado">
                                                Planifica tus Visitas
                                            </div>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="panVisita" runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px;">Fecha de Visita:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaVista" runat="server" Width="120" ValidationGroup="vgVisita" />
                                                            <asp:HiddenField ID="hidIdVisita" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaVista"
                                                                ErrorMessage="*" ValidationGroup="vgVisita">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Hora:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" Width="65px" ID="ddlHoras">
                                                            </asp:DropDownList>
                                                            :
                                                            <asp:DropDownList runat="server" Width="65px" ID="ddlMinutos">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Campanña:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" Width="140px" ID="ddlCampania" ValidationGroup="vgVisita">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCampania"
                                                                ErrorMessage="*" ValidationGroup="vgVisita">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td colspan="2">
                                                            <br />
                                                            <asp:LinkButton ID="btnAgregarActualizarVisita" runat="server" OnClick="btnAgregarActualizarVisita_Click"
                                                                ValidationGroup="vgVisita">Agregar/Actualizar</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:GridView ID="gvVisitas" runat="server" Width="350px" AutoGenerateColumns="False"
                                                OnRowCommand="gvVisitas_RowCommand">
                                                <EmptyDataTemplate>
                                                    <table width="350px">
                                                        <tr class="cabecera_indicadores">
                                                            <td>Fecha Visita
                                                            </td>
                                                            <td>Campa&#241;a
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="cabecera_indicadores" />
                                                <RowStyle CssClass="grilla_indicadores" Height="25px" VerticalAlign="Middle" />
                                                <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Visita" DataField="fechaPostVisita" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Campa&#241;a" DataField="campania">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnModificar" runat="server" CausesValidation="false" CommandName="Modificar"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idVisita") %>' Text="Modificar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEliminar" runat="server" CausesValidation="false" CommandName="Eliminar"
                                                                CommandArgument='<%#  DataBinder.Eval(Container.DataItem, "idVisita") %>' Text="Eliminar"
                                                                OnClientClick="javascript:return confirm('¿Esta seguro de quitar la Visita?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField />
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <div id="divCampanias" style="display: none;">
                                    <asp:Repeater ID="repCampanias" runat="server">
                                        <HeaderTemplate>
                                            <table style="width: 100px; padding: 5px;">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr align="center" valign="top">
                                                <td>
                                                    <a href="javascript:void(0);" class="linkCampania"><b>
                                                        <%# Container.DataItem %></b></a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="hidden" id="hdOtro" name="hdOtro" />
                            </td>
                            <td>
                                <div style="margin-top: 40px;">
                                    <asp:Button runat="server" CssClass="btnEnviarAprobacionStyle" ID="btnGuardar" OnClick="btnGuardar_Click"
                                        Style="margin-left: 160px;" />
                                    <asp:Button ID="btnCargarCampanhas" runat="server" Text="" BackColor="White" OnClick="btnCargarCampanhas_Click"
                                        Style="display: none;" />
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfDiferencia" runat="server" />
    <asp:HiddenField ID="hfCodTipoEncuesta" runat="server" />
    <asp:HiddenField ID="hfCodigoUsuario" runat="server" />
    <asp:HiddenField ID="hfPeriodo" runat="server" />
</asp:Content>
