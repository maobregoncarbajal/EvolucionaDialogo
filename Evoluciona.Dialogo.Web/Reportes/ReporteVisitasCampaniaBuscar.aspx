<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ReporteVisitasCampaniaBuscar.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.ReporteVisitasCampañaBuscar" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/FiltroProcesos.ascx" TagName="ControlFiltro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript" language="javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.dataTables.min.js"
        type="text/javascript"></script>

    <link rel="Stylesheet" href="../Styles/colorbox.css" type="text/css" />

    <script type="text/javascript">
        var urlVisitas = "<%=Utils.RelativeWebRoot%>Visita/";
        var tabIndex = <%= tabIndexActual%>;

        var registrosPagina = 10;

        jQuery(document).ready(function () {

            jQuery("#tabs").tabs({ selected: tabIndex });
            PaginarGrilla();
        });

        function abrirVentana()
        {
            var periodo=$('#<%= cboPeriodos.ClientID %>').val();
     var estado=$('#<%= cboStatus.ClientID %>').val();
     var url='ReporteVisitasCampania.aspx?periodo='+periodo+ '&estado='+estado;
     window.open(url);
 }

 function AbrirMensaje() {
     jQuery("#divDialogo").dialog({
         modal: true,
         buttons: {
             'Aceptar': function () {
                 jQuery(this).dialog('close');
             }
         }
     });
 }

 function PaginarGrilla(){

     var totalGrillas = jQuery(".grillaPaginada").length;
     if(totalGrillas == 0) return false;

     jQuery(".grillaPaginada").dataTable({
         "bJQueryUI": false,
         "bRetrieve": true,
         "sPaginationType": "full_numbers",
         "bAutoWidth": false,
         "bLengthChange": false,
         "bFilter": false,
         "bSort": false,
         "iDisplayLength": registrosPagina,
         "bInfo": false,
         "bAutoWidth": false,
         "oLanguage":
             {
                 "sLengthMenu": "Mostrar _MENU_ registros por pagina",
                 "sZeroRecords": "Ningun Registro Encontrado - Disculpe",
                 "sInfo": "Mostrando _START_-_END_ de _TOTAL_ registros",
                 "sInfoEmpty": "Mostrando 0-0 de 0 registros",
                 "sInfoFiltered": "(filtrado de un total de _MAX_ records)",
                 "sSearch" : "Filtrar por todas las columnas",
                 "oPaginate":
                 {
                     "sFirst": "<|",
                     "sLast": "|>",
                     "sNext": ">>",
                     "sPrevious": "<<"
                 },
                 "sInfoEmpty": "No hay registros que mostrar"
             }
     });

     jQuery(".grillaPaginada").each(function(index){
         var padre = jQuery(this).parent();
         var hijos = jQuery(this).find("tBody tr");

         if(hijos.length < registrosPagina)
         {
             padre.find(".dataTables_paginate").css("display", "none");
         }
     });
 }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox runat="server" ID="txtStatus" Style="display: none" />
    <asp:TextBox runat="server" ID="txtProceso" Style="display: none" />
    <div align="center" style="vertical-align: middle">
        <br />
        <br />
        <br />
        <br />
        <div>
            <table style="text-align: left; margin-top: 5px; margin-left: 5px; width: 400px;">
                <tr style="height: 30px;">
                    <td style="color: White; background-color: #60497B; vertical-align: middle;" colspan="11">
                        <center>
                            <b>REPORTE DE VISITAS POR CAMPAÑA</b></center>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;Año:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboAnio" runat="server" Style="width: 175px" CssClass="combo"
                            AutoPostBack="True" OnSelectedIndexChanged="cboAnio_SelectedIndexChanged">
                            <asp:ListItem Text="2008" Value="2008" />
                            <asp:ListItem Text="2009" Value="2009" />
                            <asp:ListItem Text="2010" Value="2010" />
                            <asp:ListItem Text="2011" Value="2011" />
                            <asp:ListItem Text="2012" Value="2012" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;Período :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPeriodos" runat="server" Style="width: 175px" CssClass="combo">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;Estado:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboStatus" runat="server" Style="width: 175px" CssClass="combo">
                            <asp:ListItem Text="TODOS" Value="-1" />
                            <asp:ListItem Text="NUEVA" Value="0" />
                            <asp:ListItem Text="CRÍTICA" Value="1" />
                            <asp:ListItem Text="ESTABLE" Value="2" />
                            <asp:ListItem Text="PRODUCTIVA" Value="3" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="left">
                        <input style="margin-top: 8px; margin-right: 35px;" id="btnVerRpt"
                            type="button" value="" class='btnBuscarStyle' onclick="abrirVentana();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="left">
                        <div style="height: 14px">
                            <table style="width: 100; height: 100%">
                                <tr>
                                    <td>
                                        <a href="../Calendario/Default.aspx">Regresar</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 0 0 0 45px;">
            <br />
            <uc1:ControlFiltro ID="ctlContenedor" runat="server" />
        </div>
    </div>
</asp:Content>
