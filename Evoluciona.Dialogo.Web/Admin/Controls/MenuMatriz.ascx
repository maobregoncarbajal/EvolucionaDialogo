<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuMatriz.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Admin.Controls.MenuMatriz" %>
<table width="100%">
    <tr>
        <td>
            <div class="ui-buttonset">
                <span id="spNivCompetencia" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-left " style="width: 150px" runat="server">
                    <a href="NivCompetencia.aspx"><span class="ui-button-text">Níveles Compt.</span></a>
                </span>
                <span id="spCondiciones" class="ui-button ui-widget ui-state-default ui-button-text-only" style="width: 150px" runat="server">
                    <a href="Condiciones.aspx"><span class="ui-button-text">Condiciones</span></a>
                </span>
                <span id="spCalibracion" class="ui-button ui-widget ui-state-default ui-button-text-only" style="width: 150px" runat="server">
                    <a href="Calibracion.aspx"><span class="ui-button-text">Calibración</span></a>
                </span>
                <span id="spAgrupacionZonaGPS" class="ui-button ui-widget ui-state-default ui-button-text-only" style="width: 150px" runat="server">
                    <a href="AgrupacionZonaGPS.aspx"><span class="ui-button-text">Agrupacion zona GPS</span></a>
                </span>
                <span id="spFuenteVentas" class="ui-button ui-widget ui-state-default ui-button-text-only ui-corner-right " style="width: 150px" runat="server">
                    <a href="FuenteVentas.aspx"><span class="ui-button-text">Fuente Ventas</span></a>
                </span>
            </div>
        </td>
    </tr>
</table>
