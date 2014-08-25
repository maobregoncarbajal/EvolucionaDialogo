<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderPaginasOperacion.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.HeaderPaginasOperacion" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<%@ Register Src="~/Controls/criticasCampanias.ascx" TagName="criticasCampanias" TagPrefix="uc1" %>
<script src="../Jscripts/jquery.window.min.js" type="text/javascript"></script>
<script src="../Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<script type="text/javascript">

    function CargarResumen(pathUrl) {
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
    }
</script>
<table width="100%" style="margin-left: auto; margin-right: auto;" class="spacedTable dialogoHeader">
    <tr>
        <td align="left" class="Top" style="width: 400px;">
            <asp:Image runat="server" ID="imgImagenDescripcion" />
        </td>
        <td align="left" style="width: 340px; vertical-align: top !important;">
            <div style="float: left; font-size: 10pt">
                <span class="texto_Negro">% Avance : </span>
            </div>
            <div id="progressbar" style="width: 180px; height: 10px; float: left; margin-left: 5px;"></div>
            <span id="lblPorcentaje" style="font-size: 8pt; margin-left: 5px"></span>
        </td>
        <td style="vertical-align: top !important;" class='textoVolverResumen' align="left">
            <a href="<%=Utils.RelativeWebRoot%>Desempenio/ResumenProceso.aspx" style="color: black;">Volver al Resumen - DIÁLOGOS</a>
        </td>
    </tr>
    <tr>
        <td align="left">
            <span style="color: Gray; font-weight: bolder; font-size: 14px; color: #778391;">
                <strong>Evaluado(a)</strong>
            </span>
            <br />
            <strong>
                <asp:Label Text="[Evaluado]" runat="server" ID="lblEvaluado" Style="color: #00acee; font-weight: bold; font-size: 14px;" />
            </strong>
            <br />
            <asp:Label Text="" runat="server" ID="lblTextoRZ" Style="color: Gray; font-size: 14px; color: #778391;" />
            <strong>
                <asp:Label Text="" runat="server" ID="lblRegionZona" Style="color: #00acee; font-weight: bold; font-size: 14px;" />
            </strong>
            <br />
            <div style="padding-left: 70px; padding-top: 5px;">
                <asp:HyperLink ID="hlResumen" runat="server" Font-Size="11px" ForeColor="Black">Ver Resumen Di&aacute;logo</asp:HyperLink>
            </div>
        </td>
        <td rowspan="2" align="left">
            <uc1:criticasCampanias ID="CriticasCampanias1" runat="server" />
        </td>
        <td align="left">
            <span class="texto_Negro">Período :</span>
            <asp:DropDownList runat="server" ID="cboPeriodos" CssClass="cboPeriodos" />
            <strong>
                <asp:LinkButton Text="VER" runat="server" CssClass="lnkButtonVer" ID="lnkVerControl" />
            </strong>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <div id="droplinetabs1" class="droplinetabs">
                <ul id="menuOperaciones">
                    <li class="btnMenu"><a href="#">ANTES |</a>
                        <ul id="mnuAntes" style="margin-left: 85px;">
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/AntesNegocio.aspx">NEGOCIOS |</a> </li>
                            <li><a id="hlAntEquipos" href="../Desempenio/AntesEquipos.aspx" runat="server">EQUIPOS |</a></li>
                            <li><a id="hlAntCompetencias" href="../Desempenio/AntesCompetencias.aspx" runat="server">COMPETENCIAS</a></li>
                        </ul>
                    </li>
                    <li class="btnMenu"><a href="#">DURANTE | </a>
                        <ul id="mnuDurante" style="margin-left: 85px;">
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/DuranteNegocio.aspx">NEGOCIOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/DuranteEquipos.aspx">EQUIPOS |</a></li>
                            <li><a href="<%=Utils.RelativeWebRoot%>Desempenio/DuranteCompetencias.aspx">COMPETENCIAS</a></li>
                        </ul>
                    </li>
                    <li class="btnMenu"><a href="#">DESPUÉS </a>
                        <ul id="mnuDespues" style="margin-left: 85px;">
                            <li><a id="hlDesNegocio" href="../Desempenio/DuranteNegocio.aspx?aprobacion=1" runat="server">NEGOCIOS |</a></li>
                            <li><a id="hlDesEquipos" href="../Desempenio/DuranteEquipos.aspx?aprobacion=1" runat="server">EQUIPOS |</a></li>
                            <li><a id="hlDesCompetencias" href="../Desempenio/DuranteCompetencias.aspx?aprobacion=1" runat="server">COMPETENCIAS</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </td>
        <td></td>
    </tr>
</table>
