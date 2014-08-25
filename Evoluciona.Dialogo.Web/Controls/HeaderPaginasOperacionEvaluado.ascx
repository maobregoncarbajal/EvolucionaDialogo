<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderPaginasOperacionEvaluado.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.HeaderPaginasOperacionEvaluado" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<%@ Register Src="~/Controls/criticasCampanias.ascx" TagName="criticasCampanias"
    TagPrefix="uc1" %>
<table width="100%" style="margin: 0 0 0 25px" class="spacedTable dialogoHeader">
    <tr>
        <td align="left" class="Top" style="width: 380px;">
            <asp:Image runat="server" ID="imgImagenDescripcion" />
        </td>
        <td align="left" style="width: 340px; vertical-align: top !important;">
            <div style="float: left; font-size: 10pt">
                <span class="texto_Negro">% Avance</span>
            </div>
            <div id="progressbar" style="width: 180px; height: 10px; float: left; margin-left: 5px;">
            </div>
            <span id="lblPorcentaje" style="font-size: 8pt; margin-left: 5px"></span>
        </td>
        <td style="vertical-align: top !important;" class='textoVolverResumen' align="left">
            <a href="<%=Utils.RelativeWebRoot%>Desempenio/ResumenProceso.aspx"
                style="color: black;">Volver al Resumen - DIÁLOGOS</a>
        </td>
    </tr>
    <tr style="margin: 5px 0 0 0">
        <td align="left">
            <span style="color: Gray; font-weight: bolder; font-size: 14px; color: #778391;"><strong>Evaluado(a)</strong></span><br />
            <strong>
                <asp:Label Text="[Evaluado]" runat="server" ID="lblEvaluado" Style="color: #00acee; font-weight: bold; font-size: 14px;" />
            </strong>
            <br />
            <asp:Label Text="" runat="server" ID="lblTextoRZ" Style="color: Gray; font-size: 14px; color: #778391;" />
            <strong>
                <asp:Label Text="" runat="server" ID="lblRegionZona" Style="color: #00acee; font-weight: bold; font-size: 14px;" />
            </strong>
            <br />
        </td>
        <td rowspan="2" align="left">
            <uc1:criticasCampanias ID="CriticasCampanias1" runat="server" />
        </td>
        <td align="left">
            <%--<span style="font-weight: bolder;">Período</span>--%>
            <asp:DropDownList runat="server" ID="cboPeriodos" CssClass="cboPeriodos" />
            <strong>
                <asp:LinkButton Text="VER" runat="server" Style="color: Black; font-weight: bolder; display: none;"
                    CssClass="lnkButtonVer" ID="lnkVerControl" /></strong>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <div id="droplinetabs1" class="droplinetabs" style="margin: 0 0 0 15px">
                <ul id="menuOperaciones">
                    <li class="btnMenu"><a href="#">ANTES</a><ul style="left: 150px;">
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/AntesNegocioEvaluado.aspx">NEGOCIOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/AntesEquiposEvaluado.aspx">EQUIPOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/AntesCompetenciasEvaluado.aspx">COMPETENCIAS</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </td>
        <td></td>
    </tr>
</table>
