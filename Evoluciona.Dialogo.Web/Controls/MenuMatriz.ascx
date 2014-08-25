<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuMatriz.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.MenuMatriz" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<script type="text/javascript">
    $(document).ready(function () {
        var ruta = window.location.pathname;

        if (ruta.indexOf('Mapeo') != -1
            || ruta.indexOf('Registro') != -1
            || ruta.indexOf('Resultado') != -1) {
            $('#lnkMapeo').addClass('current');
        }
        else if (ruta.indexOf('TomaAccion') != -1) {
            $('#lnkAccion').addClass('current');
        }

        if (ruta.indexOf('Mapeo') != -1) {
            $('#lnkMapeoResumen').addClass('current');
        } else if (ruta.indexOf('Registro') != -1) {
            $('#lnkRegistrar').addClass('current');
        } else if (ruta.indexOf('Resultado') != -1) {
            $('#lnkResultados').addClass('current');
        } else if (ruta.indexOf('Calibracion') != -1) {
            $('#lnkCalibracion').addClass('current');
        }
    });

</script>

<div style="margin-top: 0px;">
    <ul class="nav" style="clear: both">
        <li><a id="lnkMapeo" href="<%=Utils.RelativeWebRoot%>Matriz/Mapeo.aspx">Mapeo y Planificación</a></li>
        <li class="derecha">
            <asp:Label class="mensaje" ID="lblFechaRegistrarAcuerdos" runat="server" Text=""></asp:Label>
        </li>
    </ul>
</div>
<div runat="server" id="divSubMenus" style="margin-top: 1px">
    <ul class="subnav" style="clear: both">
        <li>
            <a id="lnkMapeoResumen" href="<%=Utils.RelativeWebRoot%>Matriz/Mapeo.aspx">Mapeo</a>
        </li>
        <li>
            <a id="lnkResultados" href="<%=Utils.RelativeWebRoot%>Matriz/Resultado.aspx">Resultados</a>
        </li>
        <li id="liCalibracion" runat="server">
            <a id="lnkCalibracion" href="<%=Utils.RelativeWebRoot%>Matriz/Calibracion.aspx">Calibración</a>
        </li>
    </ul>
</div>
