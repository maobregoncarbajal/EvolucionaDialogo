<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Visualizador.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Calendario.Visualizador" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<table>
    <tr>
        <td>
            <div class="ui-buttonset">
                <span id="vistaDia" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left "
                    style="width: 80px" runat="server"><a href="Default.aspx?view=agendaDay"><span class="ui-button-text">D&iacute;a</span></a> </span><span id="vistaSemana" class="ui-button ui-widget ui-state-default ui-button-text-only "
                        style="width: 80px" runat="server"><a href="Default.aspx?view=agendaWeek"><span class="ui-button-text">Semana</span></a> </span><span id="vistaMes" class="ui-button ui-widget ui-state-default ui-button-text-only "
                            style="width: 80px" runat="server"><a href="Default.aspx?view=month"><span class="ui-button-text">Mes</span></a> </span><span id="vistaCampanha" class="ui-button ui-widget ui-state-default ui-button-text-only "
                                style="width: 80px" runat="server"><a href="Campanha.aspx"><span class="ui-button-text">Campa&ntilde;a</span></a> </span><span id="vistaAgenda" class="ui-button ui-widget ui-state-default ui-button-text-only "
                                    style="width: 80px" runat="server"><a href="Agenda.aspx"><span class="ui-button-text">Agenda</span></a> </span><span id="vistaEvaluado" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-right "
                                        style="width: 140px" runat="server"><a href="Evaluado.aspx"><span class="ui-button-text">
                                            <asp:Literal ID="litEvaluado" runat="server"></asp:Literal></span></a>



                                    </span>
            </div>
        </td>
        <td style="width: 80px; text-align: right; padding-right: 5px;">
            <div style="font-weight: bold;">Reportes : </div>
        </td>
        <td style="width: 230px;">
            <div class="staticMenu" style="width: 100%;">
                <ul>
                    <li><a href="<%=Utils.RelativeWebRoot%>Reportes/ReporteVisitasCampaniaBuscar.aspx">Visitas</a> |</li>
                    <li><a href="<%=Utils.RelativeWebRoot%>Reportes/RptParticipacionTiempoBuscar.aspx">Participación</a> |</li>
                    <li><a href="<%=Utils.RelativeWebRoot%>Reportes/rptUsoTiempo.aspx" target="_blank">Uso de Tiempo</a></li>
                </ul>
            </div>
        </td>
    </tr>
</table>
