<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AntesEquiposEvaluado.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.AntesEquiposEvaluado" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionEvaluado.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.maxlength.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var indexMenu = <%= IndexMenuServer%>;
        var indexSubMenu = <%= IndexSubMenu %>;
        var mostrarMensaje = <%= EsCorrecto %>;
        var estado = <%= EstadoProceso %>;
        var soloLectura = <%=ReadOnly %>;
        var avanze = <%=Porcentaje %>;
        var totalSeleccionados = 0;

        jQuery(document).ready(function () {

            $('#<%=txtTextoIngresado.ClientID %>').maxlength({max: 200, feedbackText: ''});

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }

            CargarDescripcionCantidad();

            jQuery("#<%=cboEvaluados.ClientID %>").change(function(){
                var opcionSeleccionada = jQuery('option:selected', '#<%=cboEvaluados.ClientID %>');
                var selectedIndex = opcionSeleccionada.index();
                var nombreEvaluada = opcionSeleccionada.text();
                var valorSeleccionado = opcionSeleccionada.val();

                if(selectedIndex != 0)
                {
                    jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(nombreEvaluada);
                    jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorSeleccionado);
                    jQuery("#<%=txtTextoIngresado.ClientID %>").attr("value", "");
                    jQuery("#<%=lnkAgregar.ClientID %>").text("INGRESAR");
                    jQuery("#<%=txtTextoIngresado.ClientID %>").focus();
                }
                else
                {
                    jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text("[SIN SELECCIONAR]");
                }
            });

            MostrarAvance(avanze);

            jQuery("#divMensaje").dialog({
                autoOpen : false,
                modal: true,
                title: "ERROR DE VALIDACIÓN",
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

            jQuery("#<%=btnGuardar.ClientID %>").click(function(event){
                if(totalSeleccionados == 0)
                {
                    var tpEvld = jQuery("#<%=lblTpEvld.ClientID %>").html();
                 
                    if(tpEvld != "NUEVA"){
                        jQuery("#divMensaje").text("Debe haber por lo menos una Evaluada");
                        jQuery("#divMensaje").dialog("open");
                        event.preventDefault();
                    }
                }
            });
            
            
            jQuery(".btnLnkStyle").click(function(event) {

                var hostName = window.location.hostname;
                var pathname = "http://" + hostName + GetPortNumber() + "/Desempenio/Consultas/";
                var periodoUsuarioEvaluado = jQuery("#<%=cboPeriodosFiltro.ClientID%>").val();
                var codigoUsuarioEvaluado = jQuery("#<%=ddlGerentes.ClientID%>").val();
                var nombreUsuarioEvaluado = jQuery("#<%=ddlGerentes.ClientID %> option:selected").text();
                var descripcionRol = jQuery("#<%=lblGerente.ClientID%>").text().toUpperCase();

                pathname += "VariablesAnalizar.aspx?periodo=" + periodoUsuarioEvaluado + "&codigo=" + codigoUsuarioEvaluado + "&nombre=" + nombreUsuarioEvaluado + "&rol=" + descripcionRol;

                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "HISTORICO ANTES/EQUIPOS GERENTE DE " + descripcionRol + ".",
                    url: pathname,
                    width: 1000,
                    height: 650,
                    minimizable: false,
                    bookmarkable: false
                });

                event.preventDefault();
            });
            

            if(estado != 1 || soloLectura == 1)
            {
                jQuery("#<%=txtNumero.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=lnkAgregar.ClientID %>").css("display", "none");
                jQuery("#<%=cboEvaluados.ClientID %>").attr("disabled", "disabled");
                jQuery("#<%=txtTextoIngresado.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=mnuEliminar.ClientID %>").css("display", "none");
                jQuery("#<%=btnGuardar.ClientID %>").css("display", "none");
            }

            if(mostrarMensaje == 1)
            {
                mostrarMensaje = 0;
                jQuery("#msgProceso").dialog("open");
            }

            jQuery("#<%=lnkAgregar.ClientID %>").click(function(event){
                var nombreEvaluado = jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text();

                if(nombreEvaluado == "" || nombreEvaluado == "[SIN SELECCIONAR]")
                {
                    jQuery("#divMensaje").text("Seleccione primero una persona a Evaluar");
                    jQuery("#divMensaje").dialog("open");
                    event.preventDefault();
                }
            });

            jQuery("#<%=txtNumero.ClientID %>").keydown(function(e) {
                //            if (event.keyCode >31 && (event.keyCode < 48 || event.keyCode > 57) ) {
                //                        event.preventDefault();
                //                    }
                return VerificaTeclas(e);
            });

            jQuery(".MenuBasico .AspNet-Menu-Link").click(function(event){
                var textoActual = jQuery(this).attr("title");

                if(typeof(textoActual) == "undefined")
                    textoActual = "";

                var valorActual = jQuery(this).attr("href");

                jQuery("#<%=lnkAgregar.ClientID %>").text("Actualizar");
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorActual);
                jQuery("#<%=txtTextoIngresado.ClientID %>").attr("value", textoActual);
                jQuery("#<%=txtTextoIngresado.ClientID %>").focus();
                jQuery("#<%=txtTextoIngresado.ClientID %>").select();

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

            jQuery(".MenuBasico_Izquierda2 a").click(function(event) {

                if (confirm("Esta seguro de quitarlo del listado?")) {

                    jQuery("#<%=hidEliminadoSeleccionado.ClientID %>").val(jQuery(this).attr("href"));
                    jQuery("#<%=btnEliminarSeleccionado.ClientID %>").trigger("click");
                }

                event.preventDefault();
            });
            
            var nuevas = '';
            nuevas = jQuery("#ctl00_ContentPlaceHolder1_lblNuevas").text();
            
            if(nuevas == "NUEVA") {
                jQuery("#<%=btnGuardar.ClientID %>").trigger('click');   
            }else if(nuevas == "triggerBtnguardar") {
                //jQuery("#urlDestino").trigger('click');
                window.location.href = "<%=Utils.RelativeWebRoot %>Desempenio/AntesCompetenciasEvaluado.aspx";
            }            
             
            
        });

    function CargarSeguimiento(lst) {

        if (lst.length > 0) {

            var dniUsuarioEquipo = lst.value;
            var estadoEquipo = lst.title;
            var nombreUsuario = lst.options[lst.selectedIndex].text;
            var pathname = "<%=Utils.RelativeWebRoot%>Desempenio/Consultas/AntesEquipos_Seguimiento_Consulta.aspx?dniUsuario=" + dniUsuarioEquipo + "&estadoPeriodo=" + estadoEquipo + "&nombreUsuario=" + nombreUsuario;

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "SEGUIMIENTO POR CAMPAÑA",
                url: pathname,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 550,
                height: 250
            });
        }
        return false;
    }

    function CargarDescripcionCantidad()
    {
        totalSeleccionados = jQuery("#<%=mnuSelecccionados.ClientID %> li").length;

            jQuery("#lblTotalSeleccionados").text(totalSeleccionados);
        }

        function VerificaTeclas(e){
            var ctrl = e.ctrlKey;
            if( (ctrl && e.which == 67) ||
                (ctrl && e.which == 86) ||
                (ctrl && e.which == 118) ||
                (ctrl && e.which == 99) )
                return true;
            return ( e.which!=127 && e.which!=8 && e.which!=0 &&
			         e.which!=37 && e.which!=39 && e.which!=46 &&
                    (e.which<48 || e.which>57) &&
                    (e.which<96 || e.which>105) ) ? false : true;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="divMensaje" style="font-size: 9pt">Seleccione primero una persona a Evaluar</div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        Has completado tu Proceso:
		<br />
        DIÁLOGO/ANTES/EQUIPOS
        <br />
        <br />
        Haz click aquí para continuar con el proceso: <a href="<%=Utils.RelativeWebRoot %>Desempenio/AntesCompetenciasEvaluado.aspx" id="urlDestino">DIÁLOGO/ANTES/COMPETENCIAS</a>
    </div>
    <div id="contenedorFiltros" runat="server" class="divFiltroPeriodo4 roundedDiv">
        <table style="text-align: left; margin-top: 5px; margin-left: 15px;">
            <tr>
                <td colspan="2">
                    <br />
                    Historico Antes/Equipos Gerente de
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
                    <asp:DropDownList runat="server" ID="cboPeriodosFiltro" Width="80px"
                        Style="margin-left: 7px;" CssClass="combo"
                        OnSelectedIndexChanged="cboPeriodosFiltro_SelectedIndexChanged" AutoPostBack="True">
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
                    <asp:DropDownList runat="server" ID="ddlGerentes" Width="250px" Style="margin-left: 7px;" CssClass="combo" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-left: 235px">
                    <br />
                    <asp:LinkButton ID="lbConsultar" runat="server" Text="CONSULTAR" class="btnLnkStyle"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 1150px; margin: 20px 0 0 25px; text-align: left">
        <fieldset style="padding: 10px; margin-left: 10px; margin-right: auto; float: left; width: 330px; text-align: center">
            <legend><span class="texto_Negro">
                <asp:Literal runat="server" ID="lblUltimaCampanha" /></span></legend>
            <br />
            <div style="width: 330px; text-align: center; border-style: solid; border-width: 1px; padding: 5px 0px 5px 0px;" class="roundedDiv" runat="server" id="divSegmentacion">
                <br />
                <asp:Image ID="ImgLeyenda" runat="server" Visible="False" ImageUrl="../Images/leyendaSemaforo.jpg" />
                <asp:DataList ID="ddlResumen" runat="server" Style="margin-left: auto; margin-right: auto">
                    <ItemTemplate>
                        <div class="texto_descripciones" style="text-align: left;">
                            <img style="width: 10px;" src="<%=Utils.RelativeWebRoot %>Images/vinetablue.jpg" alt="puntito.jpg" />
                            <span style="margin: 5px;">
                                <%# Eval("vchEstadoPeriodo")%>
                            </span>
                            <%# Eval("%")%> %
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <asp:Label Text=" Nº Gerente de Zonas Nuevas : " runat="server" ID="lblNumero" CssClass="texto_descripciones" />
                <asp:TextBox runat="server" ID="txtNumero" Width="30px" Style="text-align: right;" />
            </div>
            <br />
            <asp:Label Text="GZ CRÍTICAS" runat="server" CssClass="texto_descripciones" ID="lblCriticas" />
            <br />
            <asp:ListBox runat="server" ID="lstCriticas" CssClass="combo" DataTextField="nombresCritica" DataValueField="documentoIdentidad" Style="width: 290px; height: 110px; margin-top: 5px;" ToolTip="CRITICA"></asp:ListBox>
            <div runat="server" id="divNoExitosa" style="width: 200px; float: left; height: 70px; display: none"></div>
            <br />
            <div style="font-size: 8pt;"></div>
            <br />
            <asp:Label Text="GZ ESTABLES" runat="server" CssClass="texto_descripciones" ID="lblEstable" />
            <br />
            <asp:ListBox runat="server" ID="lstEstables" CssClass="combo" DataTextField="nombresCritica" DataValueField="documentoIdentidad" Style="width: 290px; height: 110px; margin-top: 5px;" ToolTip="ESTABLE"></asp:ListBox>
            <div runat="server" id="divExitosa" style="width: 200px; float: left; height: 70px; display: none"></div>
            <br />
            <div style="font-size: 8pt;" runat="server" id="divMnjDblClcEstable"></div>
            <br />
            <asp:Label Text="GZ PRODUCTIVAS" runat="server" CssClass="texto_descripciones" ID="lblProductivas" />
            <br />
            <asp:ListBox runat="server" ID="lstProductivas" CssClass="combo" DataTextField="nombresCritica" DataValueField="documentoIdentidad" Style="width: 290px; height: 110px; margin-top: 5px;" ToolTip="PRODUCTIVA"></asp:ListBox>
            <div runat="server" id="divSinEstado" style="width: 200px; float: left; height: 70px; display: none"></div>
            <br />
            <div style="font-size: 8pt;" runat="server" id="divMnjDblClcProductiva"></div>
        </fieldset>
        <fieldset style="position: relative; left: 20px; width: 600px; padding-left: 23px;">
            <legend>Selecciona las
                <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado" />
                CRÍTICAS que priorizarás en el diálogo</legend>
            <br />
            <span class="texto_Negro">Selecciona :</span>
            <asp:DropDownList runat="server" ID="cboEvaluados" Width="350px" CssClass="combo" />
            <br />
            <br />
            Ingrese las variables a analizar de la(s)
            <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado_1" />
            CRÍTICAS:
                <br />
            <br />
            <table width="500px" cellspacing="0" cellpadding="0" style="vertical-align: top">
                <tr>
                    <td align="left" style="color: White; background-color: #a0a0a0; font-family: Arial; font-size: 12px; font-weight: bold; text-decoration: none;">
                        <asp:Label ID="lblNombrePersonaEvaluada" runat="server" Text="[SIN SELECCIONAR]"></asp:Label>
                        <asp:TextBox runat="server" Style="display: none" ID="txtValorCritica" />
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        <table width="100%">
                            <tr>
                                <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left;">&nbsp; Variables a Considerar
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas textoAntesEquipo" TextMode="MultiLine" Rows="5" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <br />
                        <asp:LinkButton Text="INGRESAR" runat="server" ID="lnkAgregar" OnClick="lnkAgregar_Click" CssClass="btnLnkStyleBotonPequenho" />

                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <fieldset style="position: relative; left: 20px; width: 600px; padding-left: 23px;">
            <legend>Ingresadas (&nbsp;<span id="lblTotalSeleccionados"></span>&nbsp;/&nbsp;<asp:Literal Text="text" runat="server" ID="lblTotalElementos" />&nbsp;)
            </legend>
            <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
            <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
            <asp:Menu ID="mnuEliminar" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda2" />
            <asp:Button ID="btnEliminarSeleccionado" runat="server" OnClick="btnEliminarSeleccionado_Click" Style="display: none" />
            <asp:HiddenField ID="hidEliminadoSeleccionado" runat="server" />
        </fieldset>
        <div style="margin-top: 15px;">
            <asp:LinkButton ID="btnGuardar" CssClass="btnLnkStyleBoton" Text="SIGUIENTE" Style="margin-left: 570px;" runat="server" OnClick="btnGuardar_Click"></asp:LinkButton>
        </div>
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblTpEvld" runat="server"></asp:Label>
    </div>
    <asp:Label ID="lblNuevas" runat="server"></asp:Label>
</asp:Content>
