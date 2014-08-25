<%@ Page Title="Calendario" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Calendario.Default" %>

<%@ Register Src="Filtros.ascx" TagName="filtros" TagPrefix="uc" %>
<%@ Register Src="Visualizador.ascx" TagName="visualizador" TagPrefix="uc" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <script src="fullcalendar/json2.js" type="text/javascript"></script>
    <script src="fullcalendar/fullcalendar.js" type="text/javascript"></script>
    <script src="Jscripts/calendarscript.js<%=ConfigurationManager.AppSettings["versionJS"]%>" type="text/javascript"></script>
    <script src="Jscripts/utiles.js" type="text/javascript"></script>

    <style type='text/css'>
        #calendar {
            width: 740px;
            margin: 0 auto;
        }

        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 5px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 5px;
        }
    </style>
    <script type="text/javascript">

        var view = '<%= View %>';
        var rolEvaluador = <%= RolEvaluador %> ;
        var rolDirectorVentas = <%= constantes.rolDirectorVentas %> ;
        var rolGerenteRegion = <%= constantes.rolGerenteRegion %> ;
        var rolGerenteZona = <%= constantes.rolGerenteZona %> ;
        var urlMetodos = "<%=Utils.RelativeWebRoot%>Calendario/Metodos.ashx";
        var urlCalendario = "<%=Utils.RelativeWebRoot%>Calendario/Calendario.ashx";
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
    <table style="margin-left: auto; margin-right: auto;">
        <tr>
            <td align="left">
                <uc:visualizador ID="visualizador" runat="server" />
            </td>
            <td align="right">
                <button id="btnNuevoEvento" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only">
                    <span class="ui-button-text" style="color: black; font-weight: bold;">Agregar Evento</span></button>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: top;">
                <br />
                <table>
                    <tr>
                        <td style="vertical-align: top; padding-right: 10px;">
                            <br />
                            <div style="font-weight: bold;">
                                Calendario
                            </div>
                            <uc:filtros ID="filtros" runat="server" MostrarFiltros="false" />
                        </td>
                        <td style="vertical-align: top;">
                            <div id="calendar">
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    <div id="addDialog" style="font-size: 70%; margin: 40px; display: none;" title="Agregar Evento">
        <table cellpadding="0">
            <tr>
                <td class="alignRight">Fecha:
                </td>
                <td class="alignLeft">
                    <input id="txtFecha" type="text" style="width: 150px" class="datepicker" />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Hora:
                </td>
                <td class="alignLeft" valign="top">
                    <select id="cboHora" style="width: 50px" class="cboHoras">
                    </select>
                    :
                    <select id="cboMinuto" style="width: 50px" class="cboMinutos">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Campa&ntilde;a:
                </td>
                <td class="alignLeft">
                    <select id="cboCampanha" style="width: 200px" class="comboCampanha">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Reunión:
                </td>
                <td class="alignLeft">
                    <select id="cboReunion" style="width: 200px" class="comboReunion">
                        <option value="0">-- Seleccionar --</option>
                        <option value="1">Individual</option>
                        <option value="2">Grupal</option>
                        <option value="3">Otros</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Evento:
                </td>
                <td class="alignLeft">
                    <select id="cboEvento" style="width: 200px" class="comboEvento">
                        <option value="0">-- Seleccionar --</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Sub-Evento:
                </td>
                <td class="alignLeft">
                    <select id="cboSubEvento" style="width: 200px" class="comboSubEvento">
                        <option value="0">-- Seleccionar --</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">
                    <span class="evaluado"></span>:
                </td>
                <td class="alignLeft">
                    <select id="cboEvaluado" style="width: 250px" class="comboEvaluado">
                        <option value="0">-- Seleccionar --</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Asunto:
                </td>
                <td class="alignLeft">
                    <input id="txtAsunto" type="text" style="width: 250px" class="asuntoEvento" />
                </td>
            </tr>
        </table>
    </div>
    <div id="updatedialog" style="font-size: 70%; margin: 40px; display: none;" title="Actualizar o Eliminar Evento">
        <table cellpadding="0">
            <tr>
                <td class="alignRight">Fecha:
                </td>
                <td class="alignLeft">
                    <input id="txtFechaM" type="text" style="width: 150px" class="datepicker" />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Hora:
                </td>
                <td class="alignLeft" valign="top">
                    <select id="cboHoraM" style="width: 50px" class="cboHoras">
                    </select>
                    :
                    <select id="cboMinutoM" style="width: 50px" class="cboMinutos">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Campa&ntilde;a:
                </td>
                <td class="alignLeft">
                    <select id="cboCampanhaM" style="width: 200px" class="comboCampanha">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Reunión:
                </td>
                <td class="alignLeft">
                    <select id="cboReunionM" style="width: 200px" class="comboReunion">
                        <option value="0">-- Seleccionar --</option>
                        <option value="1">Individual</option>
                        <option value="2">Grupal</option>
                        <option value="3">Otros</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Evento:
                </td>
                <td class="alignLeft">
                    <select id="cboEventoM" style="width: 200px" class="comboEvento">
                        <option value="0">-- Seleccionar --</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Sub-Evento:
                </td>
                <td class="alignLeft">
                    <select id="cboSubEventoM" style="width: 200px" class="comboSubEvento">
                        <option value="0">-- Seleccionar --</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">
                    <span class="evaluado"></span>:
                </td>
                <td class="alignLeft">
                    <select id="cboEvaluadoM" style="width: 250px" class="comboEvaluado">
                        <option value="0">-- Seleccionar --</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Asunto:
                </td>
                <td class="alignLeft">
                    <input id="txtAsuntoM" type="text" style="width: 250px" class="asuntoEvento" />
                </td>
            </tr>
        </table>
        <input type="hidden" id="txtIDEvento" />
    </div>
</asp:Content>
