<%@ Page Language="C#"
    AutoEventWireup="true" CodeBehind="PlanAccionCritica_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.PlanAccionCritica_Consulta"
    Title="Plan de Accion De las Criticas" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta Plan de Accion Critica</title>
    <link href="../../App_Themes/TemaDDesempenio/general.css" type="text/css" rel="stylesheet" />

    <link href="../../Styles/jquery.window.css" rel="stylesheet" type="text/css" />
    <link href="../../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SimpleMenu.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.window.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        
        var estado = <%= estadoProceso %>;
        var indexActual = -1;
        var soloLectura = <%=readOnly %>;
         
         jQuery(document).ready(function () {
           
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
                }
                else
                {
                    jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text("[SIN SELECCIONAR]");
                }
            });

            if(estado != 1)
            {
                           
                jQuery("#<%=cboEvaluados.ClientID %>").attr("disabled", "disabled");
                jQuery("#<%=txtTextoIngresado.ClientID %>").attr("ReadOnly", true);

               
            }
            else
            {
                if(soloLectura == 1 )
                    abortar = 1;
            }          

           
               
              
            jQuery("#<%=cbkPostVisita.ClientID %>").css("display", "block");
            jQuery("#<%=cbkPostVisita.ClientID %>").parent().css("display", "block");
                       
            jQuery("#<%=cbkPostVisita.ClientID %>").click(function(evento)
            {
                evento.preventDefault();
            }); 
                               
            abortar = 1;
            

            // MostrarAvance(avanze);

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

           

            jQuery(".MenuBasico .AspNet-Menu-Link").click(function(event){
                var textoActual = jQuery(this).attr("title");
                var valorActual = jQuery(this).attr("href");                

                if(typeof(textoActual) == "undefined")
                    textoActual = "";

                indexActual = jQuery(".MenuBasico a").index(this);                

                jQuery("#<%=txtTextoIngresado.ClientID %>").text(textoActual);
               
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorActual);
                
                
                var estadoActual = jQuery(".MenuBasico_Izquierda a").eq(indexActual).attr("title");

                if(typeof(estadoActual) != "undefined")
                {
                    var activado = false;

                    if(estadoActual == "1") 
                        activado = true;

                    jQuery("#<%=cbkPostVisita.ClientID %>").attr("checked", activado);
                    }
               

                event.preventDefault();
            });

            jQuery(".MenuBasico_Izquierda a").click(function (event) {
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
                    modal: true,
                    buttons: 
                    {
                        Ok: function () {
                            jQuery(this).dialog("close");                        
                        }
                    }   
                });

            }
        
        
            function CargarDescripcionCantidad()
            {            
                var totalSeleccionados = jQuery("#<%=mnuSelecccionados.ClientID %> li").length;

             jQuery("#lblTotalSeleccionados").text(totalSeleccionados);
         }
    </script>
</head>
<body style="background: white;">
    <form id="form1" runat="server">
        <div style="margin: 15px 0 0 55px; text-align: left">
            <asp:TextBox runat="server" Style="display: none" ID="txtEstadosEvaluados" />
            <table cellpadding="0" cellspacing="2" width="100%" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td style="text-align: left">
                        <p>
                            <span class="texto_Negro">Selecciona las
                            <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado" />
                                CRITICAS priorizadas en el dialogo</span>
                        </p>
                        <br />
                        Selecciona :
                    <asp:DropDownList runat="server" ID="cboEvaluados" Width="350px" />
                        <br />
                        <br />
                        Ingrese las variables a analizar de la(s)
                    <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado_1" />
                        CRITICAS:
                    <br />
                        <br />
                        <table width="550px" cellspacing="0" cellpadding="0" style="vertical-align: top; margin-left: 3px">
                            <tr>
                                <td align="right">
                                    <asp:CheckBox Text="Post Visita" runat="server" ID="cbkPostVisita" Style="display: none;" />
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
                                    <table width="100%">
                                        <tr>
                                            <td style="color: #ffffff; background-color: #a45ca4; font-family: Arial; font-size: 12px; text-align: left;">&nbsp; Variables a Considerar
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox Width="99%" ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas"
                                                    TextMode="MultiLine" Rows="5" MaxLength="600"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>
                        <br />
                        <p>
                            Ingresadas (<span id="lblTotalSeleccionados"></span>/<asp:Literal Text="text" runat="server"
                                ID="lblTotalElementos" />)
                        </p>
                        <br />
                        <div style="margin-left: 15px">
                            <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
                            <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
                            &nbsp;
                        </div>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div class="demo">
            <div style="display: none" id="divDialogo" title="INFORMACION">
                <br />
                <span>No tienes Visitas registradas en este Periodo</span>
                <br />

            </div>
        </div>


    </form>
</body>

</html>





