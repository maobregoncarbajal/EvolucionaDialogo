<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="PlanAccionCritica.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.PlanAccionCritica"
    Title="Plan de Accion De las Criticas" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionV.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;
        var estado = <%= estadoProceso %>;
        var indexActual = -1;
        var soloLectura = <%=readOnly %>;

         jQuery(document).ready(function () {
             if (indexMenu != null && indexMenu != 0) {
                 SeleccionarLinks(indexMenu, indexSubMenu, true);
             }
             CargarDescripcionCantidad();           

             if(estado != 1)
             {
                 jQuery("#<%=lnkAgregar.ClientID %>").css("display", "none");
                jQuery("#<%=txtTextoIngresado.ClientID %>").attr("ReadOnly", true);
            }
            else
            {
                if(soloLectura == 1 || indexMenu == 3)
                    abortar = 1;
            }

            if(indexMenu == 2)
            {
                if(soloLectura == 1)
                {
                    jQuery("#<%=cbkPostVisita.ClientID %>").css("display", "block");

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
                else if(indexMenu == 3)
                {
                    jQuery("#<%=lnkAgregar.ClientID %>").css("display", "none");
                    if(soloLectura == 0)
                    {
                        jQuery("#<%=cbkPostVisita.ClientID %>").css("display", "block");
                        jQuery("#<%=cbkPostVisita.ClientID %>").parent().css("display", "block");

                        jQuery("#<%=cbkPostVisita.ClientID %>").change(function(){

                            var nombreSeleccionado = jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text();
                            var estado = jQuery(this).attr("checked");
                            if(nombreSeleccionado != "[SIN SELECCIONAR]")
                            {
                                var activado = 0;

                                if(estado == "checked")
                                    activado = 1;

                                jQuery(".MenuBasico_Izquierda a").eq(indexActual).attr("title", activado);
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
                        jQuery("#<%=cbkPostVisita.ClientID %>").css("display", "block");
                        jQuery("#<%=cbkPostVisita.ClientID %>").parent().css("display", "block");

                        jQuery("#<%=cbkPostVisita.ClientID %>").click(function(evento)
                        {
                            evento.preventDefault();
                        });

                        abortar = 1;
                    }
                }

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
                var docuActual = jQuery(this).attr("href");
                var arrdocuActual=docuActual.split(',');
                var valorActual=arrdocuActual[0];

                if(typeof(textoActual) == "undefined")
                    textoActual = "";

                indexActual = jQuery(".MenuBasico a").index(this);

                jQuery("#<%=txtTextoIngresado.ClientID %>").text(textoActual);
                jQuery("#<%=lnkAgregar.ClientID %>").text("Actualizar");
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", docuActual);

                if(indexMenu == 3)
                {
                    var estadoActual = jQuery(".MenuBasico_Izquierda a").eq(indexActual).attr("title");

                    if(typeof(estadoActual) != "undefined")
                    {
                        var activado = false;

                        if(estadoActual == "1")
                            activado = true;

                        jQuery("#<%=cbkPostVisita.ClientID %>").attr("checked", activado);
                    }
                }
                jQuery.getJSON("<%=Evoluciona.Dialogo.Web.Helpers.Utils.RelativeWebRoot%>Desempenio/Handlers/CriticasHandler.ashx", 
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
        });

            function AbrirMensaje()
            {
                $("#divDialogo").dialog({
                    modal: true
                });

            }
         
            function AbrirMensajeDespues()
            {
                $("#divDialogoDespues").dialog({
                    modal: true
                });

            }

            function CargarDescripcionCantidad()
            {
                var totalSeleccionados = jQuery("#<%=mnuSelecccionados.ClientID %> li").length;

            jQuery("#lblTotalSeleccionados").text(totalSeleccionados);
        }
    </script>

    <div style="margin-top: 30px;">
        <asp:TextBox runat="server" Style="display: none" ID="txtEstadosEvaluados" />
        <table cellpadding="0" cellspacing="2" width="100%" style="border: solid 1px #c8c8c7; padding-left: 20px;">
            <tr>
                <td align="left">
                    <br />
                    <span class="subTituloMorado">Planes de Acción.</span><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <p>
                        <span class="texto_Negro">Selecciona las
                            <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado" />
                            CRITICAS priorizadas en el dialogo</span>
                    </p>
                    <p>
                        Ingresadas (<span id="lblTotalSeleccionados"></span>/<asp:Literal Text="text" runat="server"
                            ID="lblTotalElementos" />)
                    </p>
                    <br />
                    <div style="width: 100%; margin-left: 15px">
                        <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
                        <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
                        <div style="clear: both;">
                        </div>
                    </div>
                    <br />
                    <div style="width: 100%">
                        <table width="550px" cellspacing="0" cellpadding="0" style="vertical-align: top; margin-left: 3px">
                            <tr>
                                <td style="width: 100%;" align="right">
                                    <p>
                                        <asp:CheckBox CssClass="texto_Negro" Text="Post Visita" runat="server" ID="cbkPostVisita"
                                            Style="display: none;" Width="150px" TextAlign="Left" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="color: White; background-color: #a0a0a0; font-family: Arial; font-size: 12px; font-weight: bold; text-decoration: none;">
                                    <asp:Label ID="lblNombrePersonaEvaluada" runat="server" Text="[SIN SELECCIONAR]"></asp:Label>
                                    <asp:TextBox runat="server" Style="display: none" ID="txtValorCritica" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" cellspacing="5px">
                                        <tr>
                                            <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left; height: 25px; vertical-align: middle; font-size: 14px; font-weight: bolder">&nbsp; Variable
                                            </td>
                                            <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left; height: 25px; vertical-align: middle; font-size: 14px; font-weight: bolder">&nbsp; Plan de acción a Considerar
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px;">
                                                <asp:TextBox runat="server" ID="txtVariablesAnalizar" CssClass="inputtext_criticas"
                                                    TextMode="MultiLine" Rows="5" MaxLength="600" ReadOnly="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="99%" ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas"
                                                    TextMode="MultiLine" Rows="5" MaxLength="600"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:LinkButton Text="Ingresar" runat="server" ID="lnkAgregar" OnClick="lnkAgregar_Click"
                                        CssClass="link" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
                    <asp:Button runat="server" CssClass="button" ID="btnGuardar" OnClick="btnGuardar_Click"
                        Style="margin-left: 400px;" Text="SIGUIENTE" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogo" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DURANTE/EQUIPOS<br />
            </p>
            <span>Ya puedes realizar la post-visita</span><br />
            <a class="link" href="resumenVisita.aspx">IR A RESUMEN DE VISITA</a>
        </div>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogoDespues" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/DESPUES/EQUIPOS<br />
            </p>
            <span>Ya finalizaste tu proceso de Visita</span><br />
            <a class="link" href="resumenVisita.aspx">IR A RESUMEN DE VISITA</a>
        </div>
    </div>
</asp:Content>
