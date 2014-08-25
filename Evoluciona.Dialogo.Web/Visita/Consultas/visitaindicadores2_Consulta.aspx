<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visitaindicadores2_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.Consultas.visitaindicadores2_Consulta" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Indicadores</title>
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

            jQuery(".MenuBasico .AspNet-Menu-Link").click(function(event){
                var textoActual = jQuery(this).attr("title");
                var valorActual = jQuery(this).attr("href");                

                if(typeof(textoActual) == "undefined")
                    textoActual = "";

                indexActual = jQuery(".MenuBasico a").index(this);                

                jQuery("#<%=txtTextoIngresado.ClientID %>").text(textoActual);
                
                jQuery("#<%=lblNombrePersonaEvaluada.ClientID %>").text(jQuery(this).text());
                jQuery("#<%=txtValorCritica.ClientID %>").attr("value", valorActual);

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
        <div style="margin-top: 35px;">
            <table width="100%" cellpadding="0" cellspacing="0" style="border: solid 1px #c8c8c7;">
                <tr>
                    <td colspan="2" style="height: 10px;"></td>
                </tr>
                <tr>
                    <td style="width: 20px">&nbsp;
                    </td>
                    <td style="height: 10px; text-align: left">
                        <span class="subTituloMorado">Status del equipo de la
                        <%=descRol%>.</span><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left; padding-left: 50px;">
                        <div style="margin-bottom: 5px; margin-top: 5px; font-size: 13px;">
                            Objetivo de la Visita :
                        </div>
                        <asp:TextBox ID="txtObjetivosVisita" runat="server" CssClass="inputtext" Width="460px"
                            TextMode="MultiLine" Height="50px" MaxLength="600" Rows="5" Enabled="false"></asp:TextBox>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td align="left" style="width: 800px" colspan='2'>
                        <div style="width: 600px; border: solid 1px #c8c8c7; margin-left: 50px;">
                            <br />
                            <asp:Panel ID="Panel1" runat="server" Height="100%">
                                <asp:Repeater ID="grdvEstadosporPeriodos" runat="server">
                                    <HeaderTemplate>
                                        <table border="0" cellpadding="2" cellspacing="2" style="margin-left: 20px">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <img src="../../Images/vinetablue.jpg" alt="" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="texto_morado_brand" ID="lblTexto" Text='<%# Eval("vchEstadoPeriodo")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="texto_morado_brand" ID="Label2" Text='<%# Eval("%", "{0}%")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <br />
                                <table border="0" cellpadding="2" cellspacing="2" style="margin-left: 20px">
                                    <tr>
                                        <td>
                                            <img src="../../Images/vinetablue.jpg" alt="" />
                                        </td>
                                        <td>
                                            <asp:Label Text="" runat="server" ID="lblRolCritica" CssClass="texto_morado_brand" />
                                        </td>
                                        <td>
                                            <asp:Label Text="" runat="server" ID="lblCantidadNuevas" CssClass="texto_morado_brand" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="2" cellspacing="2" style="margin-left: 20px">
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:Label ID="lblRol" CssClass="texto_morado_brand" runat="server"></asp:Label><br />
                                            <asp:TextBox runat="server" Style="display: none" ID="txtEstadosEvaluados" />
                                            <table cellpadding="0" cellspacing="2" width="100%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <p>
                                                            <span class="texto_Negro">Selecciona las
                                                            <asp:Literal Text="Gerentes de Zona " runat="server" ID="lblRolEvaluado" />
                                                                CRITICAS priorizadas en el dialogo</span>
                                                        </p>
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
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:LinkButton Text="Ingresar" runat="server" ID="lnkAgregar" OnClick="lnkAgregar_Click" />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <p class="texto_Negro">
                                                            Ingresadas (<span id="lblTotalSeleccionados"></span>/<asp:Literal Text="text" runat="server"
                                                                ID="lblTotalElementos" />)
                                                        </p>
                                                        <br />
                                                        <div style="margin-left: 15px">
                                                            <asp:Menu ID="mnuSelecccionados" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico" />
                                                            <asp:Menu ID="mnuVerHistorial" runat="server" Orientation="Vertical" CssSelectorClass="MenuBasico_Izquierda" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
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
