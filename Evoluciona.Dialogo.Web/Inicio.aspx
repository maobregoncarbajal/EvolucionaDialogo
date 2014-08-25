<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Inicio" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var postBack = "<%=_esPostBack %>";

        jQuery(document).ready(function () {

            if(postBack == 0)
            {
                jQuery("#contenedorPaginas > .divInicio").eq(1).css("display", "block");
                jQuery(".tablaInicio a").eq(0).addClass("claseSelectedLink");
                postBack = 1;
            }

            jQuery(".AspNet-Menu-Selected").removeClass("AspNet-Menu-Selected");

            jQuery(".tablaInicio a").click(function(event){
                var links = jQuery(".tablaInicio a");

                links.css("text-decoration", "underline");
                links.removeClass("claseSelectedLink");

                var linkActual = jQuery(this);
                var divContenidos = jQuery("#contenedorPaginas > .divInicio");
                var divID = linkActual.attr("divID");

                linkActual.addClass("claseSelectedLink");

                divContenidos.css("display", "none");

                if(divID != -1)
                    divContenidos.eq(divID - 1).css("display", "block");

                event.preventDefault();
            });
            var rol = $('#<%=hfRol.ClientID%>').val();


            if (rol == 'GZ') {
                $('#linkReport').css('display', 'none');
            }
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <br />
        <div style="float: left; padding: 0 0 0 45px">
            <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Inicio.jpg" />
        </div>
        <br />
        <br />
        <br />
        <div style="float: left; padding: 0 0 0 45px">
            <table class="tablaInicio">
                <tr>
                    <td>
                        <a href="#" divid="2">¿Qué es Evoluciona?</a> |
                    </td>
                    <td>
                        <a href="#" divid="1">Visión de Ventas</a> |
                    </td>
                    <td>
                        <a href="#" divid="3">Nuestras Competencias</a> |
                    </td>
                    <td>
                        <a href="#" divid="4">Metodolog&iacute;as de Trabajo</a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; padding: 0 0 0 5px;">
            <table>
                <tr>
                    <td id="tdReportes" runat="server">|&nbsp; <a href="<%=Utils.RelativeWebRoot%>Reportes/RankingProductividad.aspx"
                        id="linkReport" style="color: black">Reportes</a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left: 45px; margin-right: auto; margin-top: 25px;" id="contenedorPaginas">
            <div class="divInicio">
                <div style="margin-left: 100px; float: left">
                    <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/chica1.jpg"
                        width="300px" height="400px" />
                </div>
                <div class="inicioVision">
                    Recordemos nuestra Visión de Ventas:
                    <br />
                    <br />
                    <p style="width: 350px; margin-left: auto; margin-right: auto">
                        Ganar la preferencia de las Consultoras a través de la mejor propuesta de valor
                        y la mejor fuerza de ventas
                    </p>
                </div>
            </div>
            <div class="divInicio">
                <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/queEs.jpg"
                    width="700px" height="500px" />
            </div>
            <div class="divInicio">
                <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/competencias.jpg"
                    width="700px" height="500px" />
            </div>
            <div class="divInicio">
                <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/MetodologiaTrabajo.jpg"
                    width="768px" height="614px" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfRol" runat="server" />
</asp:Content>
