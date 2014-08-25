<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Evaluado.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Calendario.Evaluado" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="Visualizador.ascx" TagName="visualizador" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="fullcalendar/json2.js" type="text/javascript"></script>
    <link href="fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <script src="fullcalendar/fullcalendar.js" type="text/javascript"></script>
    <script src="Jscripts/evaluadoscript.js<%=ConfigurationManager.AppSettings["versionJS"]%>" type="text/javascript"></script>
    <style type='text/css'>
        #calendar {
            width: 740px;
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript">

        var urlMetodos = "<%=Utils.RelativeWebRoot%>Calendario/Metodos.ashx";
        var urlEvaluados = "<%=Utils.RelativeWebRoot%>Calendario/Evaluados.ashx";
        var prefijoIsoPais = "<%= PrefijoIsoPais%>";
        var codigoRol = <%= CodigoRol%>;
        var codigoUsuario = "<%= CodigoUsuario%>";

        jQuery(document).ready(function() {

            $.ajaxSetup({ cache: false, async: false });
            inicializarPagina();

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table cellspacing="5" style="margin-left: auto; margin-right: auto; width: 980px">
        <tr>
            <td align="left" colspan="2">
                <uc:visualizador ID="visualizador" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr valign="top">
            <td align="left" style="width: 220px; vertical-align: top;">
                <table style="width: 100%; vertical-align: top;">
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Literal ID="litEvaluado" runat="server"></asp:Literal>
                            :<br />
                            <select id="cboEvaluado" style="width: 100%">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="datepicker">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 760px; vertical-align: top;">
                <div id="calendar">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
    </table>
    <div id="showEventsDialog" style="font-size: 70%; width: 100%; margin: 30px; display: none;"
        title="Información de Eventos">
        <table id="tablaEventos" style="width: 100%">
        </table>
    </div>
</asp:Content>
