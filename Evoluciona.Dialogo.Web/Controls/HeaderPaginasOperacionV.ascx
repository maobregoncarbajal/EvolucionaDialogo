<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderPaginasOperacionV.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.HeaderPaginasOperacionV" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<%@ Register Src="~/Controls/criticasCampaniasV.ascx" TagName="criticasCampanias"
    TagPrefix="uc1" %>
<table width="100%" style="margin: 0 0 0 25px" class="spacedTable dialogoHeader"
    border="0">
    <tr>
        <td align="left" style="width: 650px">
            <img src="<%=Utils.RelativeWebRoot%>Images/<%=nombreImagen%>"
                alt="Dialogo_Antes_Negocio" />
        </td>
        <td align="left" style="width: 300px; vertical-align: top; height: 20px">
            <div style="width: 250px; vertical-align: top; margin-top: 5px;" class='textoAvance'>
                <table>
                    <tr>
                        <td>% Avance :
                        </td>
                        <td>
                            <img src="../Images/avance.jpg" width="<%=porcentaje%>" height="20px" alt="" style="border: 0" />
                        </td>
                        <td>
                            <%=porcentaje%>
                            %
                        </td>
                    </tr>
                </table>
            </div>
        </td>
        <td align="left" class='textoVolverResumen'>
            <a href="<%=Utils.RelativeWebRoot%>Visita/resumenVisita.aspx">Volver al Resumen - VISITA</a>
        </td>
    </tr>
    <tr style="margin: 5px 0 0 0">
        <td align="left">
            <span class='textoGray11'><b>Evaluado(a):</b></span>
            <asp:Label Text="[Evaluado]" runat="server" ID="lblEvaluado" Style="color: #0caed7; font-family: Arial; font-size: 13px" />
        </td>
        <td rowspan="2" align="left" valign="top">
            <uc1:criticasCampanias ID="CriticasCampanias1" runat="server" />
        </td>
        <td align="left" class='textoVolverResumen'>Per&iacute;odo
            <asp:DropDownList runat="server" ID="cboPeriodos" AutoPostBack="true" CssClass="cboPeriodos" />
            <a href="#" class="lnkButtonVer">VER</a>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <div id="droplinetabs1" class="droplinetabsV" style="margin: 0 0 0 0;">
                <ul id="menuOperaciones">
                    <li class="btnMenu"><a href="#">ANTES |</a>
                        <ul>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/Interaccion.aspx?indiceSM=1&indiceM=1"  style="padding-left: 100px;">COMPETENCIAS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/PlanAccionVisita.aspx?indiceSM=2&indiceM=1">NEGOCIOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/visitaindicadores2.aspx?indiceSM=3&indiceM=1">EQUIPOS </a></li>
                        </ul>
                    </li>
                    <li class="btnMenu"><a href="#">DURANTE |</a>
                        <ul id="mnuDurante">
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/VisitaEvoluciona.aspx?indiceSM=1&indiceM=2"  style="padding-left: 100px;">COMPETENCIAS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/accionesacordadas.aspx?indiceSM=2&indiceM=2">NEGOCIOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/PlanAccionCritica.aspx?indiceSM=3&indiceM=2">EQUIPOS </a></li>
                        </ul>
                    </li>
                    <li class="btnMenu"><a href="#">DESPUÉS </a>
                        <ul id="mnuDespues">
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/Acuerdos.aspx?indiceSM=1&indiceM=3"  style="padding-left: 100px;">COMPETENCIAS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/accionesacordadas.aspx?indiceSM=2&indiceM=3">NEGOCIOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Visita/PlanAccionCritica.aspx?indiceSM=3&indiceM=3">EQUIPOS </a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </td>
        <td></td>
    </tr>
</table>
