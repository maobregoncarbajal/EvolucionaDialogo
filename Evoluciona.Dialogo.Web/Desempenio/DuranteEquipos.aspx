<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="DuranteEquipos.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.DuranteEquipos" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacion.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.maxlength.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;
        var mostrarMensaje = <%= esCorrecto %>;
        var estado = <%= estadoProceso %>;
        var indexActual = -1;
        var soloLectura = <%=readOnly %>;
         var avanze = <%=porcentaje %>;
        var abortar = 0;
        var paisSinLets = '<%=noValidar %>';

         jQuery(document).ready(function () {
             $.ajaxSetup({ cache:false });

             $('#<%=txtTextoIngresado.ClientID %>').maxlength({max: 2000, feedbackText: ''});

            jQuery("#noIngresar").dialog({
                autoOpen : false,
                modal: true,
                title: "NAVEGACION NO PERMITIDA" ,
                close:function(event,ui)
                {
                    window.location = "AntesNegocio.aspx";
                },
                buttons:
                {
                    Ok: function () {
                        jQuery(this).dialog("close");
                        window.location = "AntesNegocio.aspx";
                    }
                }
            });

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }

            CargarDescripcionCantidad();

            if(estado != 1)
            {
                jQuery("#<%=lnkAgregar.ClientID %>").css("display", "none");
                jQuery("#<%=txtTextoIngresado.ClientID %>").attr("readOnly", "readOnly");

                if(indexMenu == 2)
                    jQuery("#<%=btnGuardar.ClientID %>").css("display", "none");
            }
            else
            {
                if(soloLectura == 1 || indexMenu == 3)
                    abortar = 1;
            }

            if(estado == 2 && indexMenu == 3)
            {
                abortar = 1;
            }

            if(estado == 3)
            {
                if(indexMenu == 3)
                {
                    if(soloLectura == 0)
                    {
                        jQuery("#<%=cboEstadoIndicador1.ClientID %>").css("display", "block");

                        jQuery("#<%=cboEstadoIndicador1.ClientID %>").change(function(){

                            var nombreSeleccionado = jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text();
                            var estado = jQuery(this).val();

                            if(nombreSeleccionado != "[SIN SELECCIONAR]")
                            {
                                jQuery(".MenuBasico_Izquierda a").eq(indexActual).attr("title", estado);

                                if(estado == "1")
                                    jQuery(".MenuBasico a").eq(indexActual).append("<img src='<%=Utils.RelativeWebRoot%>Images/ok.png' alt='ok' class='imagenOk'/>");
                                else
                                {
                                    var imagen = jQuery(".MenuBasico a").eq(indexActual).find(".imagenOk");

                                    imagen.remove();
                                }
                            }
                        });

                        jQuery("#<%=btnGuardar.ClientID %>").click(function(event){
                            var estadosSeleccionados = "";

                            jQuery(".MenuBasico_Izquierda a").each(function(indiceMenu){
                                estadosSeleccionados += "," + jQuery(this).attr("title");
                            });

                            jQuery("#<%= txtEstadosEvaluados.ClientID %>").attr("value", estadosSeleccionados);
                        });
                    }
                    else
                    {
                        abortar = 1;
                    }
                }
            }

            MostrarAvance(avanze);

            jQuery("#divMensaje").dialog({
                autoOpen : false,
                modal: true,
                title: "ERROR DE VALIDACION",
                buttons:
                {
                    Ok: function () {
                        jQuery(this).dialog("close");
                    }
                }
            });

            jQuery("#msgProceso").dialog({
                autoOpen : false,
                modal: true,
                title: "PROCESO COMPLETADO",
                open: function(event, ui) {
                    jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                }
            });

            if(mostrarMensaje == 1)
            {
                mostrarMensaje = 0;
                jQuery("#msgProceso").dialog("open");
            }

            jQuery("#<%=lnkAgregar.ClientID %>").click(function(event){
                 var nombreEvaluado = jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text();

                if(nombreEvaluado == "" || nombreEvaluado == "[SIN SELECCIONAR]")
                {
                    jQuery("#divMensaje").dialog("open");
                    event.preventDefault();
                }
            });

            jQuery(".MenuBasico .AspNet-Menu-Link").click(function(event){
                var textoActual = jQuery(this).attr("title");
                var valorActual = jQuery(this).attr("href");

                if(typeof(textoActual) == "undefined")
                    textoActual = "";

                indexActual = jQuery(".MenuBasico a").index(this);

                jQuery("#<%=txtTextoIngresado.ClientID %>").text(textoActual);
                jQuery("#<%=lnkAgregar.ClientID %>").text("Actualizar");
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorActual);

                if(estado == 3)
                {
                    var estadoActual = jQuery(".MenuBasico_Izquierda a").eq(indexActual).attr("title");

                    if(estadoActual != null)
                        jQuery("#<%=cboEstadoIndicador1.ClientID %>").val(estadoActual);
                }

                jQuery.getJSON("<%=Utils.RelativeWebRoot%>Desempenio/Handlers/CriticasHandler.ashx",
                    {
                        idProceso: "<%=idProceso %>",
                        documentoIdentidad: valorActual
                    },
                    function(data){
                        jQuery("#<%= txtVariablesAnalizar.ClientID%>").attr("value", data.variableConsiderar);
                    });

                event.preventDefault();
            });

            jQuery(".MenuBasico_Izquierda a").click(function(event){
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "HISTORIAL",
                    url: this.href,
                    minimizable: false,
                    maximizable: false,
                    bookmarkable: false,
                    resizable: false
                });

                event.preventDefault();
            });

            if(abortar == 1)
            {
                jQuery("#noIngresar").dialog("open");
            }

            $('#equiposPopup').dialog({
                autoOpen: false,
                modal: true,
                width: 400,
                resizable: true
            });
            $("#equiposPopup").parent().appendTo($("form:first"));

            $('#alertaNombre').dialog({
                autoOpen: false,
                modal: true,
                width: 200,
                height:60,
                resizable: true
            });
            $("#alertaNombre").parent().appendTo($("form:first"));

            $('#btnAgregar').click(function(evt) {
                LimpiarControles();
                $('#equiposPopup').dialog('open');
                return false;
            });

            $('#<%= btnCancelar.ClientID %>').click(function(evt) {
                $('#equiposPopup').dialog('close');
                LimpiarControles();
                return false;
            });

        });

        function CargarDescripcionCantidad()
        {
            var totalSeleccionados = jQuery("#<%=mnuSelecccionados.ClientID %> li").length;

            jQuery("#lblTotalSeleccionados").text(totalSeleccionados);
        }

        function LimpiarControles() {
            $("#<%= txtNombre.ClientID %>").val("");
            $("#<%= txtVariableConsiderar.ClientID %>").val("");
            $("#<%= txtPlanAccion.ClientID %>").val("");
            $("#<%= lblMensajeError.ClientID %>").empty();
            $("#<%= hidIDEquipo.ClientID %>").val("0");
        }

        function lTrim(sStr){
            if(sStr.length>0) {
                while (sStr.charAt(0) == " ")
                    sStr = sStr.substr(1, sStr.length - 1);
            }
            return sStr;
        }

        function rTrim(sStr){
            if(sStr.length>0) {
                while (sStr.charAt(sStr.length - 1) == " ")
                    sStr = sStr.substr(0, sStr.length - 1);
            }
            return sStr;
        }

        function allTrim(sStr){
            return rTrim(lTrim(sStr));
        }

        function validarNombre() {
            var texto = allTrim($("#ctl00_ContentPlaceHolder1_txtNombre").val());
            if(texto=="") {
                jQuery(document).ready(function() { $('#alertaNombre').dialog('open'); });
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox runat="server" Style="display: none" ID="txtEstadosEvaluados" />
    <div id="noIngresar" style="font-size: 9pt">
        No es Posible ingresar a la P&aacute;gina debido a que aun no existen datos para su evaluaci&oacute;n
    </div>
    <div id="divMensaje" style="font-size: 9pt">
        Se deben ingresar un valor en la caja de texto y Seleccionar alguien a Evaluar
    </div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        Has completado tu Proceso:
        <br />
        DIÁLOGO/<asp:Label Text="DURANTE" runat="server" ID="lblAccion" />/NEGOCIO
        <br />
        <br />
        Haz click aquí para continuar con el proceso:
        <asp:HyperLink runat="server" ID="hlkUrl" />
    </div>
    <div style="margin: 35px 0 0 55px; text-align: left; width: 700px">
        <p>
            <span class="texto_Negro">Ingresa el Plan de Equipo para cada
                <asp:Literal Text="Gerente de Zona " runat="server" ID="lblRolEvaluado" />
                priorizada
            </span>
        </p>
        <br />
        <br />
        <p>
            Ingresadas (<span id="lblTotalSeleccionados"></span>/<asp:Literal Text="text" runat="server" ID="lblTotalElementos" />)
        </p>
        <br />
        <br />
        <div style="width: 90%; margin-left: 15px;">
            <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
            <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
            <div runat="server" style="width: 100%" id="dvPlanEquipos">
                <table width="600px" cellspacing="0" cellpadding="0" style="vertical-align: top">
                    <tr>
                        <td align="right">
                            <asp:DropDownList runat="server" Width="180px" ID="cboEstadoIndicador1" Style="display: none">
                                <asp:ListItem Text="En Proceso" Value="0" />
                                <asp:ListItem Text="Completado" Value="1" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="color: White; background-color: #a0a0a0; font-family: Arial; font-size: 12px; font-weight: bold; text-decoration: none;">
                            <asp:Label ID="lblNombrePersonaEvaluada" runat="server" Text="[SIN SELECCIONAR]"></asp:Label>
                            <asp:TextBox runat="server" Style="display: none" ID="txtValorCritica" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <table width="100%" cellspacing="5px">
                                <tr>
                                    <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left; height: 25px; vertical-align: middle; font-size: 14px; font-weight: bolder">&nbsp; Variable
                                    </td>
                                    <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left; height: 25px; vertical-align: middle; font-size: 14px; font-weight: bolder">&nbsp; Plan de Equipo a Considerar
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 200px;">
                                        <asp:TextBox runat="server" ID="txtVariablesAnalizar" CssClass="inputtext_criticas" TextMode="MultiLine" Rows="8" MaxLength="600" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:TextBox Width="99%" ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas" TextMode="MultiLine" Rows="8" MaxLength="2000"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:LinkButton Text="Ingresar" runat="server" ID="lnkAgregar" OnClick="lnkAgregar_Click" />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <table>
                <tr>
                    <td>
                        <div style="margin: 40px 0 0 20px; text-align: left;">
                            <table>
                                <tr style="height: 20px;">
                                    <td style="text-align: left; vertical-align: top;">
                                        <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" />
                                        <br />
                                    </td>
                                    <td style="text-align: right; vertical-align: top;">
                                        <a href="javascript:void(0);" id="btnAgregar">Agregar nuevo equipo
                                            <img src="../Images/add.png" />
                                        </a>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="gvEquipos" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="600px" OnRowCommand="gvEquiposRowCommand" OnPageIndexChanging="gvEquiposPageIndexChanging">
                                            <EmptyDataTemplate>
                                                <table width="600px">
                                                    <tr class="cabecera_indicadores">
                                                        <th style="width: 150px;">NOMBRE COMPLETO</th>
                                                        <th style="width: 100px;">VARIABLE CONSIDERAR</th>
                                                        <th style="width: 200px;">PLAN DE ACCION</th>
                                                        <th style="width: 80px;"></th>
                                                    </tr>
                                                    <tr class="grilla_indicadores">
                                                        <td colspan="4" align="center" style="color: #0caed7;">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="cabecera_indicadores" />
                                            <PagerSettings Mode="NextPreviousFirstLast" />
                                            <RowStyle CssClass="grilla_indicadores" Height="20px" />
                                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                            <Columns>
                                                <asp:BoundField HeaderText="NOMBRE COMPLETO" DataField="NombreEquipo">
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="VARIABLE CONSIDERAR" DataField="variableConsiderar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PlanAccion" HeaderText="PLAN DE ACCION">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnk_editar" runat="server" BorderStyle="None" CommandArgument='<%# Eval("idCritica") %>' CommandName="cmd_editar" CausesValidation="false">
                                                            <img src="../Images/edit.png" alt="Editar" border="0" />
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnk_eliminar" runat="server" BorderStyle="None" CommandArgument='<%# Eval("idCritica") %>' CommandName="cmd_eliminar" CausesValidation="false" OnClientClick="javascript:return confirm('¿Esta seguro de quitar del Equipo?')">
                                                            <img src="../Images/delete.png" alt="Eliminar" border="0" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="alertaNombre" style="font-size: 90%; display: none;" title="Alerta">
                            <table cellspacing="5">
                                <tr style="height: 25px;">
                                    <td style="text-align: left; padding-right: 1px;">Debe Ingresar un Nombre
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="equiposPopup" style="font-size: 70%; display: none;" title="Información del Equipo">
                            <table cellspacing="5">
                                <tr style="height: 25px;">
                                    <td style="text-align: right; padding-right: 5px;">Nombre :</td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="300px" />
                                    </td>
                                    <td>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtNombre">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 5px; vertical-align: top;">Variable Considerar :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtVariableConsiderar" runat="server" MaxLength="200" TextMode="MultiLine" Rows="3" Width="300px" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 5px; vertical-align: top;">Plan de Equipo :</td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtPlanAccion" runat="server" MaxLength="2000" TextMode="MultiLine" Rows="5" Width="300px" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr style="height: 30px;">
                                    <td></td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:HiddenField ID="hidIDEquipo" runat="server" />
                                        <asp:Button ID="btnGuardarEquipo" runat="server" Text="Guardar" OnClientClick="validarNombre()" OnClick="btnGuardarEquipo_Click" Width="100px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <br />
    <br />
    <asp:Button runat="server" CssClass="btnGuardarStyle" ID="btnGuardar" OnClick="btnGuardar_Click" Text="SIGUIENTE" Style="margin-left: 480px;" />
    <br />
    <br />
    <br />
</asp:Content>
