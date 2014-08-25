<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="AntesCompetencias.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.AntesCompetencias" %>
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
                window.location.href = "<%=Utils.RelativeWebRoot %>Desempenio/DuranteNegocio.aspx";
            }



        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMensaje" style="font-size: 9pt">
        Ingrese una Observaci&oacute;n antes de Grabar...
    </div>
    <div id="msgProceso" style="font-size: 10pt; font-family: Arial">
        Has completado tu Proceso:<br />
        DIÁLOGO/ANTES/COMPETENCIAS
        <br />
        <br />
        Ya finalizaste tu proceso de Preparación Esta información puede ser modificada durante
        el proceso de Diálogo.
        <br />
        Recuerda asistir a tu Diálogo
        <br />
        <br />
        <div style="float: left;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="height: 20px">
                        <a href="<%=Utils.RelativeWebRoot %>Desempenio/DuranteNegocio.aspx">Cerrar</a>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px">
                        <asp:HyperLink ID="hlkResumen" runat="server" CssClass="VerResumen">Ver Resumen</asp:HyperLink>
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
