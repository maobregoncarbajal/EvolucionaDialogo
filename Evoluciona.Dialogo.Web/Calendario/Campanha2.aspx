<%@ Page Title="Calendario" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Campanha2.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Calendario.Campanha2" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="Visualizador.ascx" TagName="visualizador" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="fullcalendar/json2.js" type="text/javascript"></script>
    <script src="Jscripts/campanhascript2.js<%=System.Configuration.ConfigurationManager.AppSettings["versionJS"]%>" type="text/javascript"></script>
    <style type='text/css'>
        #calendar {
            width: 740px;
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript">

        var urlMetodos = "<%=Utils.RelativeWebRoot%>Calendario/Metodos2.ashx";
        var urlCampanha = "<%=Utils.RelativeWebRoot%>Calendario/Campanhas.ashx";
        var prefijoIsoPais = "<%= PrefijoIsoPais%>";
        var codigoRol = <%= CodigoRol%>;
        var codigoUsuario = "<%= CodigoUsuario%>";

        jQuery(document).ready(function () {

            $.ajaxSetup({ cache: false, async: false });
            inicializarPagina();

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table style="margin-left: auto; margin-right: auto; width: 980px">
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
                            <table style="width: 100%; margin-top: 15px;">
                                <tr>
                                    <td>Datos en duro :
                                    </td>
                                    <td>
                                        <select id="cboAnhios" style="width: 60px">
                                        </select>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; margin-top: 15px;">
                                <tr>
                                    <td>Recorrer Lista string :
                                    </td>
                                    <td>
                                        <select id="cboAnhios2" style="width: 60px">
                                        </select>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; margin-top: 15px;">
                                <tr>
                                    <td>Serializar manualmente :
                                    </td>
                                    <td>
                                        <select id="cboAnhios3" style="width: 60px">
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
