<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuReportes.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.MenuReportes" %>
<table>
    <tr>
        <td>
            <div class="ui-buttonset" style="width: 100%">
                <span id="reporte1" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left "
                    style="width: 150px" runat="server"><a href="SeguimientoStatus.aspx"><span class="ui-button-text">Seguimiento de Status</span></a> </span>
                <span id="reporte2" class="ui-button ui-widget ui-state-default ui-button-text-only "
                    style="width: 150px" runat="server"><a href="RankingProductividad.aspx"><span class="ui-button-text">Ranking Productividad</span></a> </span>
                <span id="reporte3" class="ui-button ui-widget ui-state-default ui-button-text-only "
                    style="width: 200px" runat="server"><a href="ResultadoDialogo.aspx"><span class="ui-button-text">Resultados del Dialogo</span></a> </span>
                <span id="reporte4" class="ui-button ui-widget ui-state-default ui-button-text-only "
                    style="width: 200px" runat="server"><a href="ReporteTimeLine.aspx"><span class="ui-button-text">Time Line</span></a> </span>
            </div>
        </td>
    </tr>
</table>
