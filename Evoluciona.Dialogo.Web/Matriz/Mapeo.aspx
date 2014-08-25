<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Mapeo.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Matriz.Mapeo" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/MenuMatriz.ascx" TagName="MenuMatriz" TagPrefix="ucMenuMatriz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/colorboxAlt.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/TableSorter.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/Matriz.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/Matriz.css" rel="stylesheet"
        type="text/css" media="print" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JSLINQ.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/json2.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript"></script>

    <style type="text/css">
        .style1 {
            width: 56px;
        }

        .style2 {
            width: 135px;
        }

        .style3 {
            width: 96px;
        }

        .style4 {
            width: 136px;
        }

        .style5 {
            width: 148px;
        }

        .style6 {
            width: 243px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="contentma1" id="contentMain">
        <div class="conte2ral">
            <ucMenuMatriz:MenuMatriz ID="menuMatrizLink" runat="server" />
            <div style="width: 100%; background: url(../Styles/Matriz/lienadentro.png) repeat-x; margin-bottom: 10px">
                <br />
                <div align="left" style="margin: 0px 0px 0px 0px; background: url(../Styles/Matriz/fnd3.combos.png) no-repeat; height: 85px">
                    <table style="width: 100%">
                        <tr>
                            <td class="style1">
                                <p class="labelCboSeleccion" id="lblAnho">
                                    Año:
                                </p>
                                <p class="labelCboSeleccion" id="lblTipoMZ">
                                    Tipo:
                                </p>
                            </td>
                            <td class="style2">
                                <select id="ddlAnho" class="stiloborde">
                                </select>
                                <select id="ddlTipoMZ" class="stiloborde">
                                </select>
                            </td>
                            <td class="style3">
                                <p class="labelCboSeleccion" id="lblPrd">
                                    Período:
                                </p>
                                <p class="labelCboSeleccion" id="lblPaisMZ">
                                    Pais:
                                </p>
                            </td>
                            <td class="style4">
                                <select id="ddlPeriodos" class="stiloborde">
                                </select>
                                <select id="ddlPaisMZ" class="stiloborde">
                                </select>
                            </td>
                            <td class="style5">
                                <p class="labelCboSeleccion" id="lblSubPeriodo">
                                    Sub Período:
                                </p>
                                <p class="labelCboSeleccion" id="lblAnhoMZ">
                                    Año:
                                </p>
                            </td>
                            <td class="style4">
                                <select id="ddlSubPeriodos" class="stiloborde">
                                </select>
                                <select id="ddlAnhoMZ" class="stiloborde">
                                </select>
                            </td>
                            <td class="style6">
                                <p class="labelCboSeleccion" id="lblTipoColaborador">
                                    Tipo de Colaborador:
                                </p>
                                <p class="labelCboSeleccion" id="lblPeriodosMZ">
                                    Periodo:
                                </p>
                            </td>
                            <td class="style6">
                                <select id="ddlTipoColaborador" class="stiloborde">
                                </select>
                                <select id="ddlPeriodosMZ" class="stiloborde">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="labelCboSeleccion">
                                    Ver:
                                </p>
                            </td>
                            <td>
                                <select id="ddlTipoReporte" class="stiloborde">
                                </select>
                            </td>
                            <td>
                                <p id="lblRgn" class="labelCboSeleccion">
                                    Región:
                                </p>
                                <p id="lblRgnMZ" class="labelCboSeleccion">
                                    Región:
                                </p>
                            </td>
                            <td>
                                <select id="ddlRegion" class="stiloborde">
                                </select>
                                <select id="ddlRegionMZ" class="stiloborde">
                                </select>
                            </td>
                            <td>
                                <p id="lblZona" class="labelCboSeleccion">
                                    Zonas:
                                </p>
                            </td>
                            <td>
                                <select id="ddlZona" class="stiloborde">
                                </select>
                            </td>
                            <td></td>
                            <td>
                                <div style="float: left; margin-top: 5px; margin-left: 10px">
                                    <div class="btnRedoneado">
                                        <div class="btnRedoneado-outer">
                                            <div class="btnRedoneado-inner">
                                                <div class="btnRedoneado-into">
                                                    <input type="button" id="btnBuscar" value="Buscar" class="btn" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="dynamic">
            </div>
        </div>
    </div>
    <div id="dialog-alert">
    </div>
    <div id="ficha-persona">
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblCodigoUsuario" runat="server"></asp:Label>
        <asp:Label ID="lblRol" runat="server"></asp:Label>
        <asp:Label ID="lblRegion" runat="server"></asp:Label>
        <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
    </div>
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />

    <script type="text/javascript" language="javascript">
        //region Variables
        var codPaisEvaluador = '';
        var codPaisEvaluado = '';
        var idRolEvaluador = '';
        var idRolEvaluado = '';
        var codigoUsuarioEvaluador = '';
        var codigoUsuarioEvaluado = '';
        var codRegion = '';
        var pageBlocked = false;
        var dlgElement;
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        //endregion
        $(document).ready(function () {
            $.ajaxSetup({ cache: false, async: false });

            dlgElement = jQuery("#imgExporting");
            $.blockUI.defaults.baseZ = 100000;
            $.blockUI.defaults.overlayCSS.opacity = 0.4;
            $.blockUI.defaults.css.backgroundColor = "transparent";
            $.blockUI.defaults.css.border = '0px none';
            $.blockUI.defaults.fadeIn = 100,
            $.blockUI.defaults.fadeOut = 100,

            $(document).ajaxStart(function () {

                if (!pageBlocked) {
                    jQuery.blockUI({
                        message: dlgElement,
                        onBlock: function () {
                            pageBlocked = true;
                        }
                    });
                }
            });

            $(document).ajaxStop(function () {
                jQuery.unblockUI();
                pageBlocked = false;
            });
            MATRIZ.ToolTipText();
            crearPopUp();
            //Eventos
            $("#ddlRegion").change(function () {
                $("#ddlZona").empty();
                if ($(this).val() != "00") {
                    codPaisEvaluado = $(this).val().split('-')[0];
                    codRegion = $(this).val().split('-')[1];
                    MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluado, 'codRegion': codRegion }, 0, true, false);
                    MATRIZ.ToolTipText();
                }
                else {
                    codRegion = "00";
                    codPaisEvaluado = codPaisEvaluador;
                    if ($("#ddlTipoReporte").val() == "01") {
                        $('#ddlZona').append('<option value="" selected="selected"></option>');
                        $('#ddlZona').append('<option value="00">Todos</option>');
                    }
                }
            });

            $("#ddlAnho").change(function () {
                MATRIZ.LoadDropDownList("ddlPeriodos", url, { 'accion': 'periodo', 'codPais': codPaisEvaluador, 'anho': $(this).val(), idRol: idRolEvaluador }, 0, true, false);
                MATRIZ.ToolTipText();


                if (idRolEvaluador == "2")// Gerente Región
                {
                    MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regionGRporPeriodo', 'codPais': codPaisEvaluador, 'codUsuario': $("#<%=lblCodigoUsuario.ClientID %>").html(), 'periodo': $("#ddlPeriodos").val() }, 0, true, false);
            } else {
                MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regiones', 'codPais': codPaisEvaluador }, 0, true, false);
            }

                if (idRolEvaluador == "2")// Gerente Región
                {
                    $("#ddlTipoReporte option[value='02']").remove();

                    var parametrosRegionporPeriodo = { accion: 'paisRegionporPeriodo', 'codPais': $("#ctl00_ContentPlaceHolder2_lblPais").html(), 'codUsuario': $("#ctl00_ContentPlaceHolder2_lblCodigoUsuario").html(), 'periodo': $("#ddlPeriodos").val() };
                    var codPaisRegionporPeriodo = MATRIZ.Ajax(url, parametrosRegionporPeriodo, false);

                    //var codPaisRegion = $("#ctl00_ContentPlaceHolder2_lblRegion").html();

                    var codPaisRegion = codPaisRegionporPeriodo;

                    codPaisEvaluador = codPaisRegion.split('-')[0];
                    codRegion = codPaisRegion.split('-')[1];
                    $('#ddlRegion option').each(function (i, option) {

                        if ($(option).val() != codPaisRegion) {
                            $(option).remove();
                        }
                    });

                    MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluador, 'codRegion': codRegion }, 0, true, false);
                    MATRIZ.ToolTipText();
                }


            });

            $("#ddlAnhoMZ").change(function () {
                MATRIZ.LoadDropDownList("ddlPeriodosMZ", url, { 'accion': 'periodosAll', 'codPais': codPaisEvaluador, 'anho': $("#ddlAnhoMZ").val(), idRol: idRolEvaluador }, 0, true, false);
                MATRIZ.ToolTipText();
            });

            //QWERTY MATRIZ.LoadDropDownList("ddlRegionMZ", url, { 'accion': 'regiones', 'codPais': $("#ddlPaisMZ").val(), 'select': 'si' }, 0, true, false);
            $("#ddlPaisMZ").change(function () {
                MATRIZ.LoadDropDownList("ddlRegionMZ", url, { 'accion': 'regionesMz', 'codPais': $("#ddlPaisMZ").val(), 'select': 'si' }, 0, true, false);
                MATRIZ.ToolTipText();
            });



            $("#ddlPeriodos").change(function () {
                if (idRolEvaluador == "2")// Gerente Región
                {
                    codPaisEvaluador = $("#<%=lblPais.ClientID %>").html();


                    if (idRolEvaluador == "2")// Gerente Región
                    {
                        MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regionGRporPeriodo', 'codPais': codPaisEvaluador, 'codUsuario': $("#<%=lblCodigoUsuario.ClientID %>").html(), 'periodo': $("#ddlPeriodos").val() }, 0, true, false);
                    } else {
                        MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regiones', 'codPais': codPaisEvaluador }, 0, true, false);
                    }


                    $("#ddlTipoReporte option[value='02']").remove();

                    var parametrosRegionporPeriodo = { accion: 'paisRegionporPeriodo', 'codPais': $("#<%=lblPais.ClientID %>").html(), 'codUsuario': $("#<%=lblCodigoUsuario.ClientID %>").html(), 'periodo': $(this).val() };
                    var codPaisRegionporPeriodo = MATRIZ.Ajax(url, parametrosRegionporPeriodo, false);

                    //var codPaisRegion = $("#<%=lblRegion.ClientID %>").html();

                    var codPaisRegion = codPaisRegionporPeriodo;

                    codPaisEvaluador = codPaisRegion.split('-')[0];
                    codRegion = codPaisRegion.split('-')[1];
                    $('#ddlRegion option').each(function (i, option) {

                        if ($(option).val() != codPaisRegion) {
                            $(option).remove();
                        }
                    });

                    MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluador, 'codRegion': codRegion }, 0, true, false);
                    MATRIZ.ToolTipText();
                }
            });


            $("#btnBuscar").click(function () {
                loadDynamic();
            });

            $("#ddlTipoReporte").change(function () {
                $("#lblRgn").show();
                $("#ddlRegion").show();
                $("#lblZona").show();
                $("#ddlZona").show();
                $("#ddlRegion").attr('disabled', false);
                $("#lblSubPeriodo").show();
                $("#ddlSubPeriodos").show();
                $("#lblTipoColaborador").show();
                $("#ddlTipoColaborador").show();
                $("#lblPrd").show();
                $("#ddlPeriodos").show();
                $("#lblAnho").show();
                $("#ddlAnho").show();

                $("#lblTipoMZ").hide();
                $("#ddlTipoMZ").hide();
                $("#lblPaisMZ").hide();
                $("#ddlPaisMZ").hide();
                $("#lblAnhoMZ").hide();
                $("#ddlAnhoMZ").hide();
                $("#lblPeriodosMZ").hide();
                $("#ddlPeriodosMZ").hide();
                $("#lblRgnMZ").hide();
                $("#ddlRegionMZ").hide();

                switch ($(this).val()) {
                    case "01": //Matriz Consolidada
                        $("#ddlZona").empty();
                        $('#ddlZona').append('<option value="" selected="selected"></option>');
                        $('#ddlZona').append('<option value="00">Todos</option>');
                        break;
                    case "02": //Matriz Talento GR
                        $("#ddlRegion").val("00");
                        $("#ddlZona").empty();
                        $("#ddlRegion").attr('disabled', true);
                        $("#lblZona").hide();
                        $("#ddlSubPeriodos").hide();
                        $("#ddlTipoColaborador").hide();
                        $("#lblSubPeriodo").hide();
                        $("#lblTipoColaborador").hide();
                        //$("#ddlZona").empty();
                        $("#ddlZona").hide();

                        $("#lblTipoMZ").hide();
                        $("#ddlTipoMZ").hide();
                        $("#lblPaisMZ").hide();
                        $("#ddlPaisMZ").hide();
                        $("#lblAnhoMZ").hide();
                        $("#ddlAnhoMZ").hide();
                        $("#lblPeriodosMZ").hide();
                        $("#ddlPeriodosMZ").hide();
                        $("#lblRgnMZ").hide();
                        $("#ddlRegionMZ").hide();

                        break;
                    case "03": //Matriz Talento GZ
                        $("#ddlSubPeriodos").hide();
                        $("#ddlTipoColaborador").hide();
                        $("#lblSubPeriodo").hide();
                        $("#lblTipoColaborador").hide();
                        //$("#ddlZona").empty();

                        $("#lblTipoMZ").hide();
                        $("#ddlTipoMZ").hide();
                        $("#lblPaisMZ").hide();
                        $("#ddlPaisMZ").hide();
                        $("#lblAnhoMZ").hide();
                        $("#ddlAnhoMZ").hide();
                        $("#lblPeriodosMZ").hide();
                        $("#ddlPeriodosMZ").hide();
                        $("#lblRgnMZ").hide();
                        $("#ddlRegionMZ").hide();
                        break;
                    case "04": //Detalle Información
                        $("#ddlTipoColaborador").hide();
                        $("#lblTipoColaborador").hide();
                        if (idRolEvaluador != "2")// Gerente Región
                        {
                            $("#ddlZona").empty();
                        }
                        $("#lblTipoMZ").hide();
                        $("#ddlTipoMZ").hide();
                        $("#lblPaisMZ").hide();
                        $("#ddlPaisMZ").hide();
                        $("#lblAnhoMZ").hide();
                        $("#ddlAnhoMZ").hide();
                        $("#lblPeriodosMZ").hide();
                        $("#ddlPeriodosMZ").hide();
                        $("#lblRgnMZ").hide();
                        $("#ddlRegionMZ").hide();
                        break;
                    case "05": //Matriz Zona
                        $("#lblAnho").hide();
                        $("#ddlAnho").hide();
                        $("#lblPrd").hide();
                        $("#ddlPeriodos").hide();
                        $("#lblSubPeriodo").hide();
                        $("#ddlSubPeriodos").hide();
                        $("#lblTipoColaborador").hide();
                        $("#ddlTipoColaborador").hide();
                        $("#lblRgn").hide();
                        $("#ddlRegion").hide();
                        $("#lblZona").hide();
                        //$("#ddlZona").empty();
                        $("#ddlZona").hide();

                        //Combos y Label de MZ
                        $("#lblTipoMZ").show();
                        $("#ddlTipoMZ").show();
                        $("#lblPaisMZ").show();
                        $("#ddlPaisMZ").show();
                        $("#lblAnhoMZ").show();
                        $("#ddlAnhoMZ").show();
                        $("#lblPeriodosMZ").show();
                        $("#ddlPeriodosMZ").show();
                        $("#lblRgnMZ").show();
                        $("#ddlRegionMZ").show();

                        var parametrosObtenTipoMz = { accion: 'obtenerTipoMz', 'codPais': $("#<%=lblPais.ClientID %>").html() };
                        var tipoMz = MATRIZ.Ajax(url, parametrosObtenTipoMz, false);

                        if (tipoMz != "") {
                            $("#ddlTipoMZ").val(tipoMz);
                            $("#ddlTipoMZ").attr("disabled", true);

                            $("#ddlPaisMZ").val($("#<%=lblPais.ClientID %>").html());
                            $("#ddlPaisMZ").attr("disabled", false);


                            if (idRolEvaluador == "2")// Gerente Región
                            {
                                $("#ddlRegionMZ").val($("#<%=lblRegion.ClientID %>").html());
                                $("#ddlRegionMZ").attr("disabled", true);
                            } else {
                                $("#ddlRegionMZ").val("00");
                            }


                            $("#ddlAnhoMZ").val("00");
                            $("#ddlPeriodosMZ").val("00");
                        } else {
                            MATRIZ.ShowAlert('dialog-alert', "No se ha asignado un tipo de fuente de ventas al pais");

                            $("#ddlTipoMZ").val("02");
                            $("#ddlTipoMZ").attr("disabled", true);

                            $("#ddlPaisMZ").val($("#<%=lblPais.ClientID %>").html());
                            $("#ddlPaisMZ").attr("disabled", true);

                            if (idRolEvaluador == "2")// Gerente Región
                            {
                                $("#ddlRegionMZ").val($("#<%=lblRegion.ClientID %>").html());
                                $("#ddlRegionMZ").attr("disabled", true);
                            } else {
                                $("#ddlRegionMZ").val("00");
                            }
                            $("#ddlAnhoMZ").val("00");
                            $("#ddlPeriodosMZ").val("00");


                        }

                        break;
                }
            });

            //Detalle de Información mostrar Ficha
            $(".evaluado").live('click', function () {
                var id = $(this).attr("id");
                var codigoUsuario = id.split('-')[0];
                var pais = id.split('-')[1];

                var codigoRol = "";
                if ($("#ddlZona").val() == "0" || $("#ddlZona").val() == null) {
                    codigoRol = "5";
                }
                else {
                    codigoRol = "6";
                }

                $.fn.colorbox({ href: "<%=Utils.RelativeWebRoot%>Matriz/FichaPersonal.aspx?pais=" + pais + "&codigoUsuario=" + codigoUsuario + "&codigoRol=" + codigoRol, width: "800px", height: "460px", iframe: true, opacity: "0.8", open: true, close: "" });
            });


            $("#lblTipoMZ").hide();
            $("#ddlTipoMZ").hide();
            $("#lblPaisMZ").hide();
            $("#ddlPaisMZ").hide();
            $("#lblAnhoMZ").hide();
            $("#ddlAnhoMZ").hide();
            $("#lblPeriodosMZ").hide();
            $("#ddlPeriodosMZ").hide();
            $("#lblRgnMZ").hide();
            $("#ddlRegionMZ").hide();

        });

        function crearCombos() {

            codPaisEvaluador = $("#<%=lblPais.ClientID %>").html();
            idRolEvaluador = $("#<%=lblRol.ClientID %>").html();
            codigoUsuarioEvaluador = $("#<%=lblCodigoUsuario.ClientID %>").html();

            MATRIZ.LoadDropDownList("ddlAnho", url, { 'accion': 'anho', 'codPais': codPaisEvaluador }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlPeriodos", url, { 'accion': 'periodo', 'codPais': codPaisEvaluador, 'anho': $("#ddlAnho").val(), idRol: idRolEvaluador }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlTipoReporte", url, { 'accion': 'tipos', 'fileName': 'MapeoReportes.xml', 'adicional': 'no' }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlTipoColaborador", url, { 'accion': 'tipos', 'fileName': 'TipoColaborador.xml', 'adicional': 'si' }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlSubPeriodos", url, { 'accion': 'tipos', 'fileName': 'SubPeriodos.xml', 'adicional': 'si' }, 0, true, false);

            if (idRolEvaluador == "2")// Gerente Región
            {
                MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regionGRporPeriodo', 'codPais': codPaisEvaluador, 'codUsuario': $("#<%=lblCodigoUsuario.ClientID %>").html(), 'periodo': $("#ddlPeriodos").val() }, 0, true, false);
        } else {
            MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regiones', 'codPais': codPaisEvaluador }, 0, true, false);
        }

        $("#ddlZona").empty();
        $('#ddlZona').append('<option value="" selected="selected"></option>');
        $('#ddlZona').append('<option value="00">Todos</option>');

            /*Matriz Zona*/
        MATRIZ.LoadDropDownList("ddlTipoMZ", url, { 'accion': 'listarTiposMZ', 'fileName': 'TipoMatrizZona.xml', 'adicional': 'no' }, 0, true, false);
        MATRIZ.LoadDropDownList("ddlPaisMZ", url, { 'accion': 'paisMz', 'codPaisUsuario': codPaisEvaluador, 'tipoAdmin': 'C' }, 0, true, false);
        MATRIZ.LoadDropDownList("ddlRegionMZ", url, { 'accion': 'regionesMz', 'codPais': $("#ddlPaisMZ").val(), 'select': 'si' }, 0, true, false);
        MATRIZ.LoadDropDownList("ddlAnhoMZ", url, { 'accion': 'anho', 'codPais': codPaisEvaluador, 'select': 'si' }, 0, true, false);
        MATRIZ.LoadDropDownList("ddlPeriodosMZ", url, { 'accion': 'periodosAll', 'codPais': codPaisEvaluador, 'anho': $("#ddlAnhoMZ").val(), idRol: idRolEvaluador }, 0, true, false);

        if (idRolEvaluador == "2")// Gerente Región
        {
            $("#ddlTipoReporte option[value='02']").remove();

            var parametrosRegionporPeriodo = { accion: 'paisRegionporPeriodo', 'codPais': $("#<%=lblPais.ClientID %>").html(), 'codUsuario': $("#<%=lblCodigoUsuario.ClientID %>").html(), 'periodo': $("#ddlPeriodos").val() };
                var codPaisRegionporPeriodo = MATRIZ.Ajax(url, parametrosRegionporPeriodo, false);

                //var codPaisRegion = $("#<%=lblRegion.ClientID %>").html();

                var codPaisRegion = codPaisRegionporPeriodo;

                codPaisEvaluador = codPaisRegion.split('-')[0];
                codRegion = codPaisRegion.split('-')[1];
                $('#ddlRegion option').each(function (i, option) {

                    if ($(option).val() != codPaisRegion) {
                        $(option).remove();
                    }
                });

                MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluador, 'codRegion': codRegion }, 0, true, false);
                MATRIZ.ToolTipText();
            }
        }

        function crearPopUp() {

            var arrayDialog = [{ name: "dialog-alert", height: 100, width: 100, title: "Alerta" },
                               { name: "dialog-print", height: 100, width: 100, title: "Imprimir" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function loadDynamic() {

            var tipo = $("#ddlTipoReporte").val();
            var vista = "";

            var ddlRgn = $("#ddlRegion").val();

            if (ddlRgn == null) {
                if (tipo != "05") {
                    MATRIZ.ShowAlert('dialog-alert', "El usuario no tiene asignado una región para este perido");
                    return;
                }
            }

            switch (tipo) {
                case "01":
                    vista = "MatrizConsolidada.aspx";
                    break;
                case "02":
                    vista = "MatrizTalento.aspx";
                    break;
                case "03":
                    vista = "MatrizTalento.aspx";
                    break;
                case "04":
                    vista = "DetalleInformacion.aspx";
                    break;
                case "05": /*Matriz Zona*/
                    vista = "MatrizZona.aspx";
                    break;
            }
            jQuery.blockUI({
                message: dlgElement,
                onBlock: function () {
                    pageBlocked = true;
                }
            });

            $("#dynamic").load(vista, // URL to call  
            // POST vars (optional)
            // Callback function on completion (optional)
                    function (content) {
                        // make content visible with effect  
                        $(this).hide().fadeIn("slow");
                        return false;
                    });

        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
