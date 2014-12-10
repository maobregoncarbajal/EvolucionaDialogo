<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AntesCompetenciasEvaluado.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.AntesCompetenciasEvaluado" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionEvaluado.ascx" TagName="HeaderPaginasOperacion"
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
        var soloLectura = <%=readOnly %>;
        var avanze = <%=porcentaje %>;
        var noExisteData = <%=noExisteData %>;

        jQuery(document).ready(function () {

            $('#<%=txtObservacion.ClientID %>').maxlength({max: 600, feedbackText: ''});

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
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

            MostrarAvance(avanze);

            if(mostrarMensaje == 1)
            {
                mostrarMensaje = 0;
                jQuery("#msgProceso").dialog("open");
            }

            if(estado != 1 || soloLectura)
            {
                jQuery("#<%=txtObservacion.ClientID %>").attr("readOnly", "readOnly");
                jQuery("#<%=btnGuardar.ClientID %>").css("display", "none");
            }

            if(noExisteData == 1)
            {
                jQuery("#divMensaje").text("Usted no tiene Registrado un Plan Anual");
                jQuery("#divMensaje").dialog("open");
            }

            jQuery(".VerResumen").click(function(event){

                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "RESUMEN DE PROCESO",
                    url: this.href,
                    minimizable: false,
                    maximizable: false,
                    bookmarkable: false,
                    resizable: false,
                    width: 700,
                    height: 500
                });

                event.preventDefault();
            });

            var nuevas = '';
            nuevas = jQuery("#ctl00_ContentPlaceHolder1_lblNuevas").text();

            if(nuevas == "NUEVA") {
                jQuery("#<%=btnGuardar.ClientID %>").trigger('click');
            }
            else if(nuevas == "triggerBtnguardar") {
                //jQuery("#urlDestino").trigger('click');
                window.location.href = "<%=Utils.RelativeWebRoot%>Desempenio/DuranteNegocio.aspx";
            }
        });
        
        function ShowPopup(message) {
            $(function () {
                $("#divSincro").html(message);
                $("#divSincro").dialog({
                    title: "Atencion",
                    buttons: {
                        Ok: function () {
                            $(this).dialog('close');
                            $("#msgProceso").dialog("open");
                        }
                    },
                    open: function(event, ui) {
                        jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                    },
                    modal: true
                });
            });
        };

    </script>
    <style type="text/css">
        a.hyperlink2 {
            text-decoration: none !important;
            color: #41b7d8 !important;
            font-weight: bold !important;
        }
        a.hyperlink2:visited {
            color: #FF0000 !important;
            text-decoration: underline !important;

        }
        a.hyperlink2:hover {
            color: #41b7d8 !important;
            text-decoration: underline !important;
            font-weight: bold !important;

        }
        a.hyperlink2:active {
            color: #FF0000 !important;
            text-decoration: underline !important;

        }

        .cssBtn {
            display: inline !important;
            width: 40px !important;
            height: 25px !important;
            background: #D1D1D1 !important;
            padding: 6px 16px !important;
            text-align: center !important;
            border-radius: 5px !important;
            color: #000000 !important;
            text-decoration: none !important;
            border: solid 1px #A8A8A8;
            /*font-weight: bold !important;*/
        }

        .VerResumen {
            display: inline !important;
            width: 90px !important;
            height: 25px !important;
            background: #D1D1D1 !important;
            padding: 6px 16px !important;
            text-align: center !important;
            border-radius: 5px !important;
            color: #000000 !important;
            text-decoration: none !important;
            border: solid 1px #A8A8A8;
            /*font-weight: bold !important;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMensaje" style="font-size: 9pt">
        Ingrese una Observaci&oacute;n antes de Grabar...
    </div>
    <div id="divSincro" style="display: none"></div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        <p style="text-align:justify">Has completado tu proceso de preparación de:</p>
        <p style="text-align:justify"><strong>DIÁLOGO/ANTES/COMPETENCIAS</strong></p>
        <br />
        <p style="text-align:justify">Ya finalizaste tu proceso de preparación.</p>
        <p style="text-align:justify">Esta información puede ser modificada durante el proceso de Diálogo.</p>
        <br />
        <p style="text-align:justify">Recuerda asistir a tu Diálogo.</p>
        <br />
        <p style="text-align:justify">Si deseas que tu preparación forme parte de tu diálogo 
            <asp:LinkButton ID="lSincronizaDialogo" runat="server" CssClass="hyperlink2" OnClick="lSincronizaDialogo_Click"> >>> haz clic aquí. <<<</asp:LinkButton>
        </p>
        <hr style="margin-bottom: 14px; margin-top: 14px ;color:#A8A8A8;background-color: #A8A8A8" />
        <div style="float: left;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="height: 20px">
                        <asp:HyperLink ID="hlkResumen" runat="server" CssClass="VerResumen">Ver Resumen</asp:HyperLink>
                        <span style="margin-left: 42px; margin-right: 42px"></span>
                        <a class="cssBtn" href="<%=Utils.RelativeWebRoot %>Desempenio/ResumenProceso.aspx">Cerrar</a>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    <div style="margin: 35px 0 0 55px; text-align: left">
        <span class="texto_Negro">Si deseas ingresa</span> tus observaciones de acuerdo
        al plan mostrado. Ayudará mucho tu opinión.
        <br />
        <br />
        <asp:GridView ID="gvPlanAnual" runat="server" AutoGenerateColumns="False" CellPadding="4"
            Width="650px">
            <Columns>
                <asp:BoundField DataField="Competencia" HeaderText="Competencia">
                    <ItemStyle CssClass="grilla_plan" Width="120px" />
                    <HeaderStyle Width="120px" CssClass="texto_descripciones" />
                </asp:BoundField>
                <asp:BoundField DataField="comportamiento" HeaderText="Comportamiento">
                    <ItemStyle CssClass="grilla_alternativa_plan" />
                    <HeaderStyle CssClass="texto_descripciones" />
                </asp:BoundField>
                <asp:BoundField DataField="Sugerencia" HeaderText="Sugerencia">
                    <ItemStyle CssClass="grilla_plan" Width="120px" />
                    <HeaderStyle Width="120px" CssClass="texto_descripciones" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="cabecera_plan" />
        </asp:GridView>
        <br />
        <br />
        <asp:Label Text="Observaciones :" runat="server" ID="lblObservacion" CssClass="subtituloPlan" />
        <br />
        <asp:TextBox ID="txtObservacion" runat="server" CssClass="inputtext" Width="650px"
            TextMode="MultiLine" Height="100px" MaxLength="600" Rows="5" />
        <br />
        <br />
        <asp:Button runat="server" CssClass="btnGuardarStyle" Text="SIGUIENTE" Style="margin-left: 575px;"
            ID="btnGuardar" OnClick="btnGuardar_Click" />
        <br />
        <br />
    </div>
    <asp:Label ID="lblNuevas" runat="server"></asp:Label>
</asp:Content>
