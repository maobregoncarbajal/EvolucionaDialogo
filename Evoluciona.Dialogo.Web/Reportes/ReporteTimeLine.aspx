<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ReporteTimeLine.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Reportes.ReporteTimeLine" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<%@ Register Src="~/Controls/MenuReportes.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/json2.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/TimeLine/timeline_ajax/simile-ajax-api.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/TimeLine/timeline_js/timeline-api.js?bundle=true" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js" type="text/javascript"></script>
    <style type="text/css">
        .t-highlight1 {
            background-color: #ccf;
        }

        .p-highlight1 {
            background-color: #fcc;
        }

        .timeline-highlight-label-start .label_t-highlight1 {
            color: #f00;
        }

        .timeline-highlight-label-end .label_t-highlight1 {
            color: #aaf;
        }

        .timeline-band-events .important {
            color: #f00;
        }

        .timeline-band-events .small-important {
            background: #c00;
        }

        /*---------------------------------*/

        .dark-theme {
            color: #eee;
        }

            .dark-theme .timeline-band-0 .timeline-ether-bg {
                background-color: #888;
            }

            .dark-theme .timeline-band-1 .timeline-ether-bg {
                background-color: #333;
            }

            .dark-theme .timeline-band-2 .timeline-ether-bg {
                background-color: #222;
            }

            .dark-theme .timeline-band-3 .timeline-ether-bg {
                background-color: #444;
            }

            .dark-theme .t-highlight1 {
                background-color: #003;
            }

            .dark-theme .p-highlight1 {
                background-color: #300;
            }

            .dark-theme .timeline-highlight-label-start .label_t-highlight1 {
                color: #f00;
            }

            .dark-theme .timeline-highlight-label-end .label_t-highlight1 {
                color: #115;
            }

            .dark-theme .timeline-band-events .important {
                color: #c00;
            }

            .dark-theme .timeline-band-events .small-important {
                background: #c00;
            }

            .dark-theme .timeline-date-label-em {
                color: #fff;
            }

            .dark-theme .timeline-ether-lines {
                border-color: #fff;
                border-style: solid;
            }

            .dark-theme .timeline-ether-highlight {
                background: #555;
            }

            .dark-theme .timeline-event-tape,
            .dark-theme .timeline-small-event-tape {
                background: #f60;
            }

            .dark-theme .timeline-ether-weekends {
                background: #111;
            }

        .style1 {
            width: 108px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <br />
    <div style="margin-left: 80px">
        <uc:menuReportes ID="menuReporte" runat="server" />
    </div>
    <br />
    <div style="margin-top: 30px; margin-left: 30px; width: 100%;">
        <div style="float: left; width: 100%;">
            <table>
                <tr>
                    <td style="width: 100px;">País :
                    </td>
                    <td>
                        <select id="ddlPais" disabled="disabled" class="combo" style="width: 100px;">
                        </select>
                    </td>
                    <td style="width: 100px;">Periodo :
                    </td>
                    <td>
                        <select id="ddlPeriodo" class="combo" style="width: 100px;">
                        </select>
                    </td>
                    <td style="width: 100px;">Rol :
                    </td>
                    <td>
                        <select id="ddlRol" class="combo" style="width: 100px;">
                        </select>
                    </td>
                    <td style="width: 100px;">Toma Acción :
                    </td>
                    <td>
                        <select id="ddlTipoAccion" class="combo" style="width: 100px;">
                        </select>
                    </td>
                    <td style="width: 100px;">
                        <input type="button" value="Buscar" id="btnBuscar" class="btnGuardarStyle" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both">
        </div>
        <br />
        <br />
        <div id="my-timeline" style="height: 500px; border: 1px solid #aaa; display: none; margin-top: 10px;">
        </div>
        <div style="clear: both">
        </div>
        <br />
        <br />
        <table width="100%">
            <tr>
                <td colspan="9">
                    <table style="border: 1 !important">
                        <tr>
                            <td>Plan de Mejora</td>
                            <td>
                                <img src="../Jscripts/TimeLine/timeline_js/images/red-circle.png" alt="" height="10px" width="10px" /></td>
                        </tr>
                        <tr>
                            <td>Reasignación</td>
                            <td>
                                <img src="../Jscripts/TimeLine/timeline_js/images/green-circle.png" alt="" height="10px" width="10px" /></td>
                        </tr>
                        <tr>
                            <td>Rotación Saludable</td>
                            <td>
                                <img src="../Jscripts/TimeLine/timeline_js/images/blue-circle.png" alt="" height="10px" width="10px" /></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <input type="button" class="btnGuardarStyle" style="float: right; display: none;" value="Imprimir" id="btnImprimir" onclick="imprimir();" />
                    <div id="tabla1" style="float: right; display: none;">
                        <asp:Button ID="btnPDF" runat="server" Text="PDF" class="btnGuardarStyle" OnClick="btnPDF_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="panelParameters" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblCodigoUsuario" runat="server"></asp:Label>
        <asp:Label ID="lblIdRol" runat="server"></asp:Label>
        <asp:Label ID="lblRutaImage" runat="server"></asp:Label>
        <asp:Label ID="lblPeriodoEvaluacion" runat="server"></asp:Label>
        <asp:Label ID="lblRutaRelativa" runat="server"></asp:Label>
    </div>

    <script type="text/javascript" language="javascript">

        //region Variables
        var timeLine;
        var resizeTimerID = null;
        var codPaisEvaluador = "";
        var idRolEvaluador = "";
        var codigoUsuarioEvaluador = "";
        var webHandler = "<%=Utils.AbsoluteWebRoot%>Reportes/TimeLineHandler.ashx";
        var webHandlerMatriz = "<%=Utils.AbsoluteWebRoot%>Matriz/MatrizReporte.ashx";
        var urlImage = "";

        //end Region Variable

        $(document).ready(function () {

            initialLoad();
            $("#btnBuscar").click(function () {

                onLoadTimeLine($("#ddlPais").val(), $("#ddlRol").val(), $("#ddlPeriodo").val(), $("#ddlTipoAccion").val());
                $("#btnImprimir").show();
                var divtabla1 = document.getElementById('tabla1');
                divtabla1.style.display = 'block';

            });

            var timeline = document.getElementById('my-timeline');
            timeline.className = (timeline.className.indexOf('dark-theme') != -1) ? timeline.className.replace('dark-theme', '') : timeline.className += ' dark-theme';

        });

        //region Initial

        function initialLoad() {
            codPaisEvaluador = $("#<%=lblPais.ClientID %>").html();
            idRolEvaluador = $("#<%=lblIdRol.ClientID %>").html();
            codigoUsuarioEvaluador = $("#<%=lblCodigoUsuario.ClientID %>").html();
            MATRIZ.LoadDropDownList("ddlPais", webHandler, { 'accion': 'loadPais' }, codPaisEvaluador, false, false);
            MATRIZ.LoadDropDownList("ddlPeriodo", webHandler, { 'accion': 'periodo', 'codPais': codPaisEvaluador, 'anho': '00', idRol: idRolEvaluador }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlRol", webHandler, { 'accion': 'loadRol', 'idRolEvaluador': idRolEvaluador }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlTipoAccion", webHandlerMatriz, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'si' }, 0, true, false);
            urlImage = $("#<%=lblRutaImage.ClientID %>").html();
        }

        //end region Initial
        //region Time Line
        function onLoadTimeLine(codPaisEvaluador, idRolEvaluado, periodo, codTomaAccion) {

            if (periodo == '00') {
                periodo = $("#<%=lblPeriodoEvaluacion.ClientID %>").html();
            }

            var parametros = {
                accion: 'loadTimeLine', codPaisEvaluador: codPaisEvaluador, codUsuarioEvaluador: codigoUsuarioEvaluador,
                idRolEvaluador: idRolEvaluador, idRolEvaluado: idRolEvaluado, periodo: periodo, codTomaAccion: codTomaAccion, urlImage: urlImage, rutaRelativa: $("#<%=lblRutaRelativa.ClientID %>").html()
            };

            var eventsData = MATRIZ.Ajax(webHandler, parametros, false);

            $("#my-timeline").show();
            SimileAjax.History.enabled = false;
            var eventSource = new Timeline.DefaultEventSource(0);
            var theme = Timeline.ClassicTheme.create();

            theme.event.bubble.width = 600;
            theme.event.bubble.height = 220;
            theme.event.track.height = 1.5;
            theme.event.track.gap = 0.1;
            theme.event.label.insideColor = "blue";
            theme.event.label.outsideColor = "black";
            theme.timeline_start = new Date(Date.UTC(2011, 0, 1));
            theme.timeline_stop = new Date(Date.UTC(2101, 0, 1));
            var expDate = new Date();
            var yyyy = expDate.getFullYear();

            if ($("#ddlPeriodo").val() != "00") {
                yyyy = $("#ddlPeriodo").val().substring(0, 4);
            }

            var bandInfos = [
                          Timeline.createBandInfo({
                              eventSource: eventSource,
                              width: "80%",
                              //date: "Jan 01 2011 00:00:00 GMT",
                              date: "Jan 01 " + yyyy + " 00:00:00 GMT",
                              intervalUnit: Timeline.DateTime.CAMPANHA,
                              intervalPixels: 150,
                              theme: theme
                          })
                          ,
               Timeline.createBandInfo({

                   eventSource: eventSource,
                   overview: true,
                   trackHeight: 0.5,
                   trackGap: 0.2,
                   date: "Jan 01 " + yyyy + " 00:00:00 GMT",
                   width: "20%",
                   timeZone: 13,
                   intervalUnit: Timeline.DateTime.YEAR,
                   intervalPixels: 150,
                   theme: theme
               })
            ];

            bandInfos[1].syncWith = 0;
            bandInfos[1].highlight = true;

            timeLine = Timeline.create(document.getElementById("my-timeline"), bandInfos);

            var data = {
                "dateTimeFormat": "iso8601",
                'events': eventsData
            };

            eventSource.clear();
            eventSource.loadJSON(data, '');
            timeLine.finishedEventLoading();
            }

            //end Region TimeLine

            function imprimir() {
                window.print();
            }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
