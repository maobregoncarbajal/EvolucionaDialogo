<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Ayuda.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Ayuda" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var postBack = <%=_esPostBack %>;

        jQuery(document).ready(function () { 

            if(postBack == 0)
            {
                var contenedorInicial = jQuery("#contenedorPaginas > .divInicio:eq(0)");
                contenedorInicial.css("display", "block");
                
                CargarContenido(contenedorInicial);
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

                divContenidos.css("display", "none");
                linkActual.addClass("claseSelectedLink");

                if(divID != -1)
                {       
                    var contenedorActual = divContenidos.eq(divID - 1);
                    CargarContenido(contenedorActual);
                }

                event.preventDefault();
            });
        });

        function CargarContenido(contenedorActual)
        {
            var url = contenedorActual.attr("url");

            contenedorActual.css("display", "block");

            if( typeof(url) != "undefined" && url.length > 0)
            {
                if(contenedorActual.find("iframe").length == 0)
                    contenedorActual.append("<iframe style='width:800px; height:400px;' src='" + url + "' />");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="float: left; padding: 0 0 0 45px">
            <img alt="nombre" src="<%=Utils.RelativeWebRoot%>Images/Ayuda.jpg" />
        </div>
        <br />
        <br />
        <br />
        <div style="padding: 0 0 0 45px">
            <table class="tablaInicio">
                <tr>
                    <td>
                        <a href="#" divid="1">Competencias</a>
                    </td>
                    <td>
                        <a href="#" divid="2">Di&aacute;logos</a>
                    </td>
                    <td>
                        <a href="#" divid="3">Visitas</a>
                    </td>
                    <td>
                        <a href="#" divid="4">Reuniones</a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left: 45px; margin-right: auto; margin-top: 25px;" id="contenedorPaginas">
            <div style="width: 100%" class="divInicio"
                url="/Files/Ayuda-Competencias.pdf">
            </div>
            <div class="divInicio" style="width: 100%"
                url="/Files/Ayuda-Dialogos.pdf">
            </div>
            <div class="divInicio">
            </div>
            <div class="divInicio">
            </div>
        </div>
    </div>
</asp:Content>
