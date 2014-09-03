<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ResumenProceso.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.ResumenProceso" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/jquery.window.css" rel="stylesheet" type="text/css" />

    <script src="../Jscripts/jquery.window.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        function CargarResumen(nombreEvaluado, codEvaluado, idProceso, rolEvaluado) {

            var codPais = "<%= CodigoPais %>";
            var periodo = $("#<%=cboPeriodos.ClientID %>").val();
            var codEvaluador = "<%= CodigoEvaluador %>";
            var pathUrl = "<%=Utils.RelativeWebRoot%>Admin/ResumenProceso.aspx?nomEvaluado=" + nombreEvaluado + "&codEvaluado=" + codEvaluado + "&idProceso=" + idProceso + "&rolEvaluado=" + rolEvaluado + "&codPais=" + codPais + "&periodo=" + periodo + "&codEvaluador=" + codEvaluador;

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "RESUMEN DE PROCESO",
                url: pathUrl,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 700,
                height: 500
            });

            return false;
        }
        
        function CargarResumenPreDialogo(nombreEvaluado, codEvaluado, idProceso, rolEvaluado) {

            var codPais = "<%= CodigoPais %>";
            var periodo = $("#<%=cboPeriodos.ClientID %>").val();
            var codEvaluador = "<%= CodigoEvaluador %>";
            var pathUrl = "<%=Utils.RelativeWebRoot%>Desempenio/ResumenPreDialogo.aspx?nomEvaluado=" + nombreEvaluado + "&codEvaluado=" + codEvaluado + "&idProceso=" + idProceso + "&rolEvaluado=" + rolEvaluado + "&codPais=" + codPais + "&periodo=" + periodo + "&codEvaluador=" + codEvaluador;

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "RESUMEN DE PROCESO",
                url: pathUrl,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 700,
                height: 500
            });

            return false;
        }

        function CargarResumenImpreso(pathUrl) {

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "SELECCIONE RESÚMENES A IMPRIMIR",
                url: pathUrl,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 500,
                height: 400
            });
        }

        function CargarVista(pathUrl) {

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "BÚSQUEDA DE DATOS",
                url: pathUrl,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 800,
                height: 300
            });
        }
    </script>

    <style type="text/css">
        .botonAprobar {
            top: 4px;
            position: relative;
            left: -167px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript" language="javascript">
        var noHayDatos = '<%= SinDatos %>';
        var rutaRes='<%=VerResumen %>';

        jQuery(document).ready(function () {

            jQuery("#ctl00_ContentPlaceHolder1_lnkEncuesta").click(function() {
                jQuery("#msgProceso").dialog("close");
                window.location = "<%=Utils.RelativeWebRoot %>Desempenio/ResumenProceso.aspx";
             });

            jQuery("#mensaje").dialog({
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

            jQuery("#msgProceso").dialog({
                autoOpen : false,
                modal: true,
                title: "DIÁLOGO APROBADO",
                open: function() {
                    jQuery(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
                }
            });


            if(noHayDatos == '1')
            {
                jQuery("#mensaje").dialog("open");
            }

            jQuery("#btnVerResumen").click(function(event){
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "RESUMEN",
                    url: this.href,
                    width: 900,
                    height:500,
                    minimizable: false,
                    bookmarkable: false
                });

                event.preventDefault();
            });

            if(rutaRes=="si")
            {
                jQuery("#msgProceso").dialog("open");
            }
            
            jQuery(".botonAprobar").click(function(event) {

                var encuesta = $("#<%=hfEncuesta.ClientID%>").val();
                

                if(encuesta != "1"){
                
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
        
        
        function closeWindow(){
            jQuery(document).ready(function() { jQuery.window.closeAll(); });
            $("#<%=hfEncuesta.ClientID%>").val("1");
            $(".botonAprobar").trigger("click");
        }
        

        function AbrirResumen()
        {
            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "RESUMEN",
                url: "resumenProcesoIniciar.aspx?verResumen=si",
                width: 950,
                height:500,
                minimizable: false,
                bookmarkable: false
            });
            jQuery("#msgProceso").dialog("close");
        }
        function AbrirResumenEvaluado()
        {
            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "RESUMEN",
                url: "../PantallasModales/ResumenAntesEvaluado.aspx",
                width: 950,
                height: 500,
                minimizable: false,
                bookmarkable: false
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="msgProceso" class="texto_negrita" style="display: none">
        <br />
        Has Aprobado tu Proceso:<br />
        DIÁLOGO EVOLUCIONA
        <br />
        <br />
        <div id="divEnlace" runat="server">
            <span id="lblEnunciado" runat="server"></span>
            <br />
            <a id="lnkEncuesta" style="color: #00acee;" target="_blank" href="" runat="server"></a>
            <br />
            <br />
            <span id="lblInformacionDni" runat="server" style="font-weight: bold;"></span>
            <br />
            <br />
        </div>
        Para ver el resumen de tu Diálogo
        <br />
        <div style="float: left">
            <a href="#" style="color: #00acee;" onclick="AbrirResumen();"><b>Haz click aquí</b></a><br />
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdModifaProcesoEvaluador" />
    <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
    <div id="mensaje" style="font-size: 9pt;">
        No se Puede iniciar un proceso porque no existen datos...
    </div>
    <div>
        <div style="float: left; padding: 0 0 0 45px">
            <asp:Image ID="imgHeader" runat="server" Style="width: 350px; height: 32px;" ImageUrl="~/Images/DIALOGO.jpg" />
        </div>
        <div style="float: right; padding: 10px 35px 0 0">
            <strong style="margin: 3px; font-weight: bolder">Período</strong>
            <asp:DropDownList runat="server" ID="cboPeriodos" AutoPostBack="true" OnSelectedIndexChanged="cboPeriodos_SelectedIndexChanged">
            </asp:DropDownList>&nbsp&nbsp&nbsp
            <strong style="margin: 3px; font-weight: bolder">Tipo</strong>
            <asp:DropDownList runat="server" ID="cboTipo" AutoPostBack="true" OnSelectedIndexChanged="cboTipo_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:ImageButton runat="server" ID="imgBtnAyuda" ImageUrl="../Images/ayuda.png"
                                Height="20px" align="right" OnClick="imgBtnAyuda_Click"/>
            <br />
                <asp:ImageButton runat="server" ID="imgBtnRegistarAcuerdo" ImageUrl="../Images/btn-registrar.png"
                                Height="30px" align="right" OnClick="imgBtnRegistarAcuerdo_Click" />
        </div>
        <br />
        <br />
        <div id="divMiDialogo" runat="server">
            <div style="margin: 25px 0 0 45px; text-align: left">
                <table rules="none" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 9px;"></td>
                        <td class="subTituloMorado" colspan="3" style="border-bottom: dotted 1px #778391">Mi Diálogo - Evaluación
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="descripcion">
                            <asp:Image ID="puntoAproba" runat="server" ImageUrl="~/Images/punto.png"
                                Width="3px" Height="3px" />&nbsp;<b><asp:Label runat="server" ID="lblMensajeDialogo"
                                    Text="Aprueba tu Diálogo de desempeño" Font-Names="Arial" ForeColor="#6A288A"
                                    Font-Size="12px" Font-Bold="true"></asp:Label>
                                </b>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="imgBtnAprobarDialogo" ImageUrl="../Images/ico_dialogo2.jpg"
                                OnClick="imgBtnAprobarDialogo_Click" Height="42px" CssClass="botonAprobar" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="descripcion">
                            <br />
                            <div id="divIniciaDialogo" runat="server">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/punto.png"
                                    Width="3px" Height="3px" />&nbsp;<b><asp:LinkButton runat="server" ID="lnkIniciarDialogoEvaluado"
                                        CssClass="link_dialogo" Text="Prepárate para tu diálogo aquí" OnClick="lnkIniciarDialogoEvaluado_Click"></asp:LinkButton><label class="link_dialogo"> | </label><asp:LinkButton runat="server" ID="lbViewPreDialogo" CssClass="link_dialogo" Text="Resumen de tu preparación" OnClick="lbViewPreDialogo_Click"></asp:LinkButton></b>
                                <br />
                                <br />
                            </div>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/punto.png" Width="3px" Height="3px" />&nbsp;
                            <b><asp:LinkButton runat="server" ID="lnkIniciarDialogoEvaluadoResumen" CssClass="link_dialogo" Text="Resumen de tu diálogo" OnClick="lnkIniciarDialogoEvaluadoResumen_Click"></asp:LinkButton></b>
                            <br />
                            <br />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="contentPage" style="margin-top: 20px;">
            Recuerda que puede seguir a tus equipos utilizando el <a href="Buscador.aspx" style="color: Black">
                <strong style="color: Black">BUSCADOR</strong></a>
            <br />
            <br />
            <div style="text-align: left; padding-left: 5px">
                Resumen
                <br />
                <br />
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <asp:HyperLink ID="hlResumenImpresion" runat="server" ForeColor="Black">IMPRIMIR RESUMEN DI&Aacute;LOGO</asp:HyperLink>
                        </td>
                        <td style="width: 20px;"></td>
                        <td style="width: 200px;">
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div runat="server" class="divResumenProceso" id="divEnviado" style="width: 220px; padding-right: 5px;">
                <asp:Label runat="server" ID="lblEnviado" Text="Por Iniciar" CssClass="subTituloPlomo_procesos"></asp:Label>
                <br />
                <asp:GridView ID="gvProcesosInactivos" runat="server" CellPadding="4" ForeColor="#778391"
                    Width="100%" GridLines="None" AllowPaging="True" PageSize="10" AutoGenerateColumns="False"
                    CssClass="grillaResumenProceso" ShowHeader="False" OnPageIndexChanging="gvProcesosInactivos_PageIndexChanging"
                    OnRowDataBound="gvProcesosInactivos_RowDataBound">
                    <RowStyle CssClass="grilla_procesos" />
                    <FooterStyle CssClass="footer_procesos" />
                    <PagerStyle CssClass="footer_procesos" />
                    <HeaderStyle CssClass="cabecera_procesos" />
                    <AlternatingRowStyle CssClass="grilla_alternativa_procesos" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="codigoUsuario,Nombres" DataTextField="Nombres"
                            InsertVisible="False" ShowHeader="False" DataNavigateUrlFormatString="resumenProcesoIniciar.aspx?codDI={0}&Nombre={1}&cod="></asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divResumenProceso" id="DivProceso" runat="server" style="width: 220px; padding-right: 5px;">
                <asp:Label runat="server" ID="Label1" Text="En Proceso" CssClass="subTituloPlomo_procesos"></asp:Label>
                <br />
                <asp:GridView ID="gvProcesosActivos" runat="server" Width="100%" CellPadding="4"
                    CssClass="grillaResumenProceso" ForeColor="#778391" GridLines="None" AllowPaging="True"
                    PageSize="10" AutoGenerateColumns="False" ShowHeader="False" OnPageIndexChanging="gvProcesosActivos_PageIndexChanging"
                    OnRowDataBound="gvProcesosActivos_RowDataBound">
                    <RowStyle CssClass="grilla_procesos" />
                    <FooterStyle CssClass="footer_procesos" />
                    <PagerStyle CssClass="footer_procesos" />
                    <HeaderStyle CssClass="cabecera_procesos" />
                    <AlternatingRowStyle CssClass="grilla_alternativa_procesos" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="codigoUsuario,Nombres" DataTextField="Nombres"
                            InsertVisible="False" ShowHeader="False" DataNavigateUrlFormatString="resumenProcesoIniciar.aspx?codDI={0}&Nombre={1}&cod="></asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divResumenProceso" id="divAprobacion" runat="server" style="width: 220px; padding-right: 5px;">
                <asp:Label runat="server" ID="Label2" Text="En Aprobación" CssClass="subTituloPlomo_procesos"></asp:Label>
                <br />
                <asp:GridView ID="gvProcesosEnRevision" runat="server" Width="100%" CellPadding="4"
                    CssClass="grillaResumenProceso" ForeColor="#778391" GridLines="None" AllowPaging="True"
                    PageSize="10" AutoGenerateColumns="False" ShowHeader="False" OnPageIndexChanging="gvProcesosEnRevision_PageIndexChanging"
                    OnRowDataBound="gvProcesosEnRevision_RowDataBound">
                    <RowStyle CssClass="grilla_procesos" />
                    <FooterStyle CssClass="footer_procesos" />
                    <PagerStyle CssClass="footer_procesos" />
                    <HeaderStyle CssClass="cabecera_procesos" />
                    <AlternatingRowStyle CssClass="grilla_alternativa_procesos" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="codigoUsuario,Nombres" DataTextField="Nombres"
                            InsertVisible="False" ShowHeader="False" DataNavigateUrlFormatString="resumenProcesoIniciar.aspx?codDI={0}&Nombre={1}&cod="></asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divResumenProceso" id="divAprobado" runat="server" style="border-right: white; width: 220px; padding-right: 5px;">
                <asp:Label runat="server" ID="Label3" Text="Aprobado" CssClass="subTituloPlomo_procesos"></asp:Label>
                <br />
                <asp:GridView ID="gvProcesosAprobados" runat="server" Width="100%" CellPadding="4"
                    CssClass="grillaResumenProceso" ForeColor="#778391" GridLines="None" AllowPaging="True"
                    PageSize="10" AutoGenerateColumns="False" ShowHeader="False" OnPageIndexChanging="gvProcesosAprobados_PageIndexChanging"
                    OnRowDataBound="gvProcesosAprobados_RowDataBound">
                    <RowStyle CssClass="grilla_procesos" />
                    <FooterStyle CssClass="footer_procesos" />
                    <PagerStyle CssClass="footer_procesos" />
                    <HeaderStyle CssClass="cabecera_procesos" />
                    <AlternatingRowStyle CssClass="grilla_alternativa_procesos" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="codigoUsuario,Nombres" DataTextField="Nombres"
                            InsertVisible="False" ShowHeader="False" DataNavigateUrlFormatString="resumenProcesoIniciar.aspx?codDI={0}&Nombre={1}&cod="></asp:HyperLinkField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfEncuesta" runat="server" />
    <asp:HiddenField ID="hfCodTipoEncuesta" runat="server" />
    <asp:HiddenField ID="hfCodigoUsuario" runat="server" />
    <asp:HiddenField ID="hfPeriodo" runat="server" />
</asp:Content>

