<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Resultado.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Matriz.Resultado" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/MenuMatriz.ascx" TagName="MenuMatriz" TagPrefix="ucMenuMatriz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/colorboxAlt.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/Matriz.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/Validation.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/typography.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JSLINQ.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/json2.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.validate.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="contentma1">
        <div class="conte2ral">
            <ucMenuMatriz:MenuMatriz ID="menuMatrizLink" runat="server" />
        </div>
        <div style="width: 100%; background: url(../Styles/Matriz/lienadentro.png) repeat-x; margin-bottom: 10px">
            <br />
            <div align="left" style="margin: 0px 0px 0px 10px; background: url(../Styles/Matriz/fnd2.combos.png) no-repeat; height: 85px">
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle; width: 90px;">
                            <p class="labelCboSeleccion">
                                G. Región:
                            </p>
                        </td>
                        <td style="vertical-align: middle; width: 160px;">
                            <select id="ddlGerenteRegion" name="ddlGerenteRegion" class="stiloborde" style="width: 150px; height: 22px;">
                            </select>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 50px;" align="center">
                            <p class="labelCboSeleccion">
                                Año :
                            </p>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 94px;">
                            <select id="ddlAnho" name="ddlAnho" class="stiloborde" style="width: 95px; height: 22px;">
                            </select>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 70px;" align="center">
                            <p class="labelCboSeleccion">
                                Periodo :
                            </p>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 94px;">
                            <select id="ddlPeriodo" name="ddlPeriodo" class="stiloborde" style="width: 95px; height: 22px;">
                            </select>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 70px;" align="center">
                            <p class="labelCboSeleccion">
                                Variable :
                            </p>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 94px;">
                            <select id="ddlVariable" name="ddlVariable" class="stiloborde" style="width: 95px; height: 22px;">
                            </select>
                        </td>
                        <td rowspan="2" style="vertical-align: middle; width: 31px;"></td>
                        <td style="vertical-align: middle" rowspan="2">
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
                    <tr>
                        <td style="vertical-align: middle">
                            <p class="labelCboSeleccion">
                                G. Zona:
                            </p>
                        </td>
                        <td style="vertical-align: middle; width: 150px; height: 28px;">
                            <select id="ddlGerenteZona" name="ddlGerenteZona" class="stiloborde" style="width: 150px; height: 22px;">
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="clear: both; margin: 10px 0px 0px 0px; display: none" id="panelResultado">
            <div style="font-family: Arial; text-align: left; color: #4B5A66; font-size: 12px; margin: 10px 0px 0px 0px">
                <div style="float: left; width: 400px">

                    <label style="color: #4660A1; font-size: 14px; font-weight: bold; cursor: pointer; text-decoration: underline"
                        class="lanzaFichaPersonal" id="spNombreEvaluado">
                    </label>
                    <label style="color: #4660A1; font-size: 14px; font-weight: bold">
                        (<span id="spRolEvaluado"></span>)</label>
                    <p style="margin-top: 5px;">
                        Región: <span style="font-weight: bold" id="spRegionZonaEvaluado"></span>
                        <br />
                        Cuadrante:
                        <span style="font-weight: bold" id="spCuadrante"></span>
                    </p>
                    <input type="hidden" id="hfCodRegion" />
                    <input type="hidden" id="hfCodZona" />
                    <br />
                </div>
                <div style="clear: both">
                </div>
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%">
                        <div style="font-family: Arial; text-align: left; color: #4B5A66; font-size: 12px; margin: 10px 0px 0px 0px">
                            <p>
                                Ranking
                            </p>
                            <br />
                        </div>
                        <table id="tblResultado" class="grillaMorada" style="float: left">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <img id="ChartArea" style="display: none" />
                    </td>
                </tr>
            </table>
            <div style="clear: both">
            </div>
            <div style="clear: both; padding-top: 0px; margin-left: 0px">
                <br />
                <input type="button" id="btnMostrarComparacion" value="MOSTRAR COMPARACION" class="btnFlechaAbajo"
                    style="float: left" />
                <div style="clear: both">
                </div>
            </div>
            <div id="divTablasComparacion">
                <br />
                <p class="tituloComparacion" style="float: left; clear: left">
                    Análisis comparativo de Ranking por campaña y período.
                </p>
                <br />
                <label class="tituloComparacion" style="margin: 10px 0px 5px 0px; float: left">
                    <span id="spTituloAnhoAnt"></span>
                </label>
                <div style="clear: both">
                </div>
                <br />
                <span id="sptblResultadoAnhoAnt" style="font-weight: bold"></span>
                <table id="tblResultadoAnhoAnt" class="grillaMorada" style="margin: 10px 0px 10px 0px">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div style="clear: both">
                </div>
                <br />
                <br />
                <label class="tituloComparacion" style="margin: 10px 0px 5px 0px; float: left">
                    <span id="spTituloAnhoAct"></span>
                </label>
                <div style="clear: both">
                </div>
                <br />
                <span id="sptblResultadoAnhoAct" style="font-weight: bold"></span>
                <table id="tblResultadoAnhoAct" class="grillaMorada" style="margin: 10px 0px 10px 0px">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div style="width: 100%;" id="panelButtons">
                <div style="float: right">
                    <input type="button" class="btnSquare print" value="Imprimir" id="btnPrintReporteDetalle"
                        onclick="imprimir();" />
                </div>
                <div style="float: right">
                    <input type="button" class="btnSquare" value="Descargar" id="btnDescargarReporteDetalle"
                        onclick="descargarOption();" />
                </div>
            </div>
        </div>
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblNombre" runat="server"></asp:Label>
        <asp:Label ID="lblCodigoUsuario" runat="server"></asp:Label>
        <asp:Label ID="lblRol" runat="server"></asp:Label>
        <asp:Label ID="lblRegion" runat="server"></asp:Label>
    </div>
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />
    <div id="dialog-descarga" style="display: none">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="button" id="btnPdf" class="downloadPDF" onclick="descargar('pdf');">
                    </button>
                </td>
                <td>
                    <button type="button" id="btnExcel" class="downloadExcel" onclick="descargar('xls');">
                    </button>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-alert" style="display: none">
    </div>

    <script type="text/javascript" language="javascript">
        //region Variables
        var codPais = '';
        var idRol = '';
        var codigoUsuario = '';
        var codRegion = '';
        var codZona = '';
        var pageBlocked = false;
        var dlgElement;
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var codVariable = '';
        var isPorcentaje = '';
        var rolEvaluado = '';
        var nombreVariable = '';
        var gerenteRegion = '';
        var gerenteZona = '';
        //endregion
        $(document).ready(function () {

            $.validator.addMethod('selectNone',
                function (value, element) {

                    if (element.value == '' || element.value == '00') {
                        return false;
                    }
                    else return true;
                }, "Please select an option");

            $("#form1").validate({
                rules: {
                    ddlGerenteRegion: {
                        selectNone: true
                    },
                    ddlAnho: {
                        selectNone: true
                    },
                    ddlPeriodo: {
                        required: true
                    },
                    ddlVariable: {
                        selectNone: true
                    }
                },
                messages: {
                    ddlGerenteRegion: {
                        selectNone: "*"
                    },
                    ddlAnho: {
                        selectNone: "*"
                    },
                    ddlPeriodo: {
                        required: "*"
                    },
                    ddlVariable: {
                        selectNone: "*"
                    }
                }
            });

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
            $("#ddlGerenteRegion").change(function () {
                var codigo = $(this).val();
                if (codigo != "00") {
                    var codigoUsuarioEvaluado = codigo.split('-')[1];
                    var paisUsuario = codigo.split('-')[0];
                    var nombre = $("#ddlGerenteRegion option:selected").text();

                    MATRIZ.LoadDropDownList("ddlGerenteZona", url, { 'accion': 'gerenteZona', 'codPais': paisUsuario, 'codUsuario': codigoUsuarioEvaluado, 'nombre': nombre }, 0, true, false);
                    MATRIZ.ToolTipText();
                }
            });

            $("#ddlAnho").change(function () {
                MATRIZ.LoadDropDownList("ddlPeriodo", url, { 'accion': 'periodosAll', 'codPais': codPais, 'anho': $(this).val(), idRol: idRol }, 0, true, false);
                MATRIZ.ToolTipText();
            });

            $("#btnMostrarComparacion").click(function () {

                $("#btnMostrarComparacion").removeClass();
                var estado = $("#divTablasComparacion").css("display");

                if (estado == "none") {
                    $("#divTablasComparacion").css("display", "");
                    $("#btnMostrarComparacion").addClass('btnFlechaArriba');
                } else {
                    $("#divTablasComparacion").css("display", "none");
                    $("#btnMostrarComparacion").addClass('btnFlechaAbajo');
                }
            });

            $("#spNombreEvaluado").click(function () {

                var codigoRol = "";
                var codigo = "";

                if ($("#ddlGerenteZona").val() == "00") {
                    codigo = $("#ddlGerenteRegion").val();
                    codigoRol = "5";
                } else {
                    codigo = $("#ddlGerenteZona").val();
                    codigoRol = "6";
                }

                var codigoUsuarioEvaulado = codigo.split('-')[1];
                var codPaisEvaluado = codigo.split('-')[0];

                $.fn.colorbox({ href: "<%=Utils.RelativeWebRoot%>Matriz/FichaPersonal.aspx?pais=" + codPaisEvaluado + "&codigoUsuario=" + codigoUsuarioEvaulado + "&codigoRol=" + codigoRol, width: "800px", height: "460px", iframe: true, opacity: "0.8", open: true, close: "" });
            });

            $("#btnBuscar").click(function (evt) {


                try {

                    $("#panelResultado").hide();
                    $("#divTablasComparacion").css("display", "none");
                    $("#btnMostrarComparacion").addClass('btnFlechaAbajo');

                    var isValid = $("#form1").valid();

                    if (isValid) {

                        var codigo;
                        $("#spCuadrante").html('');
                        $("#spNombreEvaluado").html('');
                        $("#spRolEvaluado").html('');
                        $("#spRegionZonaEvaluado").html('');

                        if ($("#ddlGerenteZona").val() == "00") {
                            codigo = $("#ddlGerenteRegion").val();
                            rolEvaluado = "5";
                            $("#spRolEvaluado").append("GR");
                            $("#spNombreEvaluado").append($("#ddlGerenteRegion option:selected").text());
                        } else {
                            codigo = $("#ddlGerenteZona").val();
                            $("#spRolEvaluado").append("GZ");
                            rolEvaluado = "6";
                            $("#spNombreEvaluado").append($("#ddlGerenteZona option:selected").text());
                        }

                        var codigoUsuarioEvaluado = codigo.split('-')[1];
                        var codPaisEvaluado = codigo.split('-')[0];

                        var anhoAct = $("#ddlAnho").val();
                        var periodo = $("#ddlPeriodo").val();

                        codVariable = $("#ddlVariable").val().split('-')[0];

                        isPorcentaje = $("#ddlVariable").val().split('-')[1];
                        nombreVariable = $("#ddlVariable option:selected").text();
                        gerenteRegion = $("#ddlGerenteRegion option:selected").text();
                        gerenteZona = $("#ddlGerenteZona option:selected").text();

                        var idRolEvaluado = "";
                        if (rolEvaluado == "5") {
                            idRolEvaluado = "2";
                        }

                        if (rolEvaluado == "6") {
                            idRolEvaluado = "3";
                        }

                        parametros = { accion: 'cuadranteUsuario', codPais: codPaisEvaluado, codigoUsuario: codigoUsuarioEvaluado, idRol: idRolEvaluado, anho: anhoAct, periodo: periodo };
                        var datosCuadrante = MATRIZ.Ajax(url, parametros, false);

                        $("#spCuadrante").html('');

                        if (rolEvaluado == "6") {
                            $("#spRegionZonaEvaluado").append(datosCuadrante.NombreRegion + "   Zona: " + datosCuadrante.NombreZona);
                        }
                        else {
                            $("#spRegionZonaEvaluado").append(datosCuadrante.NombreRegion);
                        }

                        $("#hfCodRegion").val(datosCuadrante.CodRegion);
                        codRegion = datosCuadrante.CodRegion;

                        $("#hfCodZona").val(datosCuadrante.CodZona);
                        codZona = datosCuadrante.CodZona;

                        $("#spCuadrante").append(datosCuadrante.Cuadrante);

                        var parametros = { accion: 'verResultado', codigoUsuario: codigoUsuarioEvaluado, anho: anhoAct, periodo: periodo, codPais: codPaisEvaluado, codVariable: codVariable, rolEvaluado: rolEvaluado, nombreVariable: nombreVariable, gerenteRegion: gerenteRegion, gerenteZona: gerenteZona, codRegion: codRegion, codZona: codZona };

                        var lista = MATRIZ.Ajax(url, parametros, false);

                        if (lista.length > 0) {
                            $("#panelResultado").show();


                            var periodos = JSLINQ(lista).Distinct(function (item) { return item.Periodo; }).items;

                            if (periodo.length > 0) {

                                var anhoAnt = parseInt(anhoAct) - parseInt("1");
                                periodos = crearPeriodos(periodos, anhoAct, anhoAnt);

                                crearTablaResumen(periodos, lista, anhoAnt, anhoAct);
                                crearTablaAnho(periodos, lista, anhoAnt, 'tblResultadoAnhoAnt', rolEvaluado);
                                crearTablaAnho(periodos, lista, anhoAct, 'tblResultadoAnhoAct', rolEvaluado);

                                $("#spTituloAnhoAnt").html('');
                                $("#spTituloAnhoAct").html('');
                                $("#spTituloAnhoAnt").append('Campañas del ' + anhoAnt);
                                $("#spTituloAnhoAct").append('Campañas del ' + anhoAct);
                            }

                            parametros = { accion: 'verChart', codigoUsuario: codigoUsuarioEvaluado, anho: anhoAct, periodo: periodo, codPais: codPaisEvaluado, codVariable: codVariable, isPorcentaje: isPorcentaje, rolEvaluado: rolEvaluado, codRegion: codRegion, codZona: codZona };

                            $("#ChartArea").hide();
                            var urlChart = MATRIZ.Ajax(url, parametros, false);
                            $("#ChartArea").attr("src", urlChart);
                            $("#ChartArea").show();
                        } else {
                            MATRIZ.ShowAlert('', 'No se encuentran datos');
                        }
                    }
                    else {
                        evt.preventDefault();
                    }

                } catch (err) {
                    alert("Error de click: " + err);
                }
            });
        });

        function crearPopUp() {

            var arrayDialog = [{ name: "dialog-alert", height: 100, width: 100, title: "Alerta" },
                                { name: "dialog-descarga", height: 110, width: 110, title: "Descargar" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function crearCombos() {
            codPais = $("#<%=lblPais.ClientID %>").html();
            idRol = $("#<%=lblRol.ClientID %>").html();
            codigoUsuario = $("#<%=lblCodigoUsuario.ClientID %>").html();
            var nombre = $("#<%=lblNombre.ClientID %>").html();;

            if (idRol == "2")//GR
            {
                $("#form1").validate({
                    rules: {
                        ddlGerenteZona: {
                            selectNone: true
                        }
                    },
                    messages: {
                        ddlGerenteZona: {
                            selectNone: "*"
                        }
                    }
                });

                var combo = document.getElementById("ddlGerenteRegion");
                combo.options.length = 0;
                combo.options[0] = new Option("00");
                combo.selectedIndex = 0;
                combo.options[0] = new Option(nombre, codPais + "-" + $.trim(codigoUsuario));

                combo.selectedIndex = 0;
                MATRIZ.LoadDropDownList("ddlGerenteZona", url, { 'accion': 'gerenteZona', 'codPais': codPais, 'codUsuario': codigoUsuario, 'nombre': nombre }, 0, true, false);
            }

            else {
                MATRIZ.LoadDropDownList("ddlGerenteRegion", url, { 'accion': 'gerenteRegion', 'codUsuario': codigoUsuario }, 0, true, false);
            }

            MATRIZ.LoadDropDownList("ddlAnho", url, { 'accion': 'anho', 'codPais': codPais }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlPeriodo", url, { 'accion': 'periodosAll', 'codPais': codPais, 'anho': $("#ddlAnho").val(), idRol: idRol }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlVariable", url, { 'accion': 'variablePais', 'codPais': codPais }, 0, true, false);
        }

        function crearPeriodos(periodos, anhoActual, anhoAnt) {
            var periodosAuxFinal = new Array();
            var periodosAux = [{ Anho: anhoAnt, Descripcion: anhoAnt + " I", Existe: true, Activo: true },
                                { Anho: anhoActual, Descripcion: anhoActual + " I", Existe: true, Activo: true },
                                { Anho: anhoAnt, Descripcion: anhoAnt + " II", Existe: true, Activo: true },
                                { Anho: anhoActual, Descripcion: anhoActual + " II", Existe: true, Activo: true },
                                { Anho: anhoAnt, Descripcion: anhoAnt + " III", Existe: true, Activo: true },
                                { Anho: anhoActual, Descripcion: anhoActual + " III", Existe: true, Activo: true }];

            $.each(periodosAux, function (i, v) {
                var pass = false;
                $.each(periodos, function (j, w) {

                    if (v.Descripcion == w) {
                        pass = true;
                    }
                });

                v.Existe = pass;
            });

            if (periodosAux[0].Existe == true || periodosAux[1].Existe == true) {
                periodosAuxFinal.push({ Anho: periodosAux[0].Anho, Descripcion: periodosAux[0].Descripcion });
                periodosAuxFinal.push({ Anho: periodosAux[1].Anho, Descripcion: periodosAux[1].Descripcion });
            }

            if (periodosAux[2].Existe == true || periodosAux[3].Existe == true) {
                periodosAuxFinal.push({ Anho: periodosAux[2].Anho, Descripcion: periodosAux[2].Descripcion });
                periodosAuxFinal.push({ Anho: periodosAux[3].Anho, Descripcion: periodosAux[3].Descripcion });
            }
            if (periodosAux[4].Existe == true || periodosAux[5].Existe == true) {
                periodosAuxFinal.push({ Anho: periodosAux[4].Anho, Descripcion: periodosAux[4].Descripcion });
                periodosAuxFinal.push({ Anho: periodosAux[5].Anho, Descripcion: periodosAux[5].Descripcion });
            }

            return periodosAuxFinal;
        }

        function crearTablaResumen(periodos, lista, anhoAnt, anhoActual) {

            var cabecera = "<tr><td style='background-color:#5C1D6C;width:30px'></td>";
            var ranking = "<tr><th>Ranking</th>";

            var participacion;
            var logro;
            var text = "";

            if (isPorcentaje == "1") {
                text = "%";
            }

            if (codVariable == "VtaNet") {
                participacion = "<tr><th>%Part. Venta</th>";
                logro = "<tr><th>%Logro Venta</th>";

            } else {
                participacion = "<tr><th>" + text + "Valor Planeado</th>";
                logro = "<tr><th>" + text + "Valor Real</th>";
            }

            $.each(periodos, function (i, v) {

                cabecera = cabecera + "<th>" + v.Descripcion + "</th>";
                var entidad = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Periodo == v.Descripcion; });

                if (entidad.items.length > 0) {

                    switch (entidad.items[0].EstadoPeriodo.toString().toUpperCase()) {

                        case 'CRITICA':
                            ranking = ranking + "<td style='font-weight:bold;width:50px;' class='rojo'>CRITICA </td>";
                            break;
                        case 'ESTABLE':
                            ranking = ranking + "<td style='font-weight:bold;width:50px;' class='amarillo'>ESTABLE</td>";
                            break;
                        case 'PRODUCTIVA':
                            ranking = ranking + "<td style='font-weight:bold;width:50px;' class='verde'>PRODUCTIVA</td>";
                            break;
                    }

                    logro = logro + "<td>" + parseFloat(entidad.items[0].LogroPeriodo).toFixed(2) + "</td>";
                    participacion = participacion + "<td>" + parseFloat(entidad.items[0].ParticipacionPeriodo).toFixed(2) + "</td>";

                } else {
                    ranking = ranking + "<td>-</td>";
                    participacion = participacion + "<td>-</td>";
                    logro = logro + "<td>-</td>";
                }
            });

            if ($("#ddlPeriodo").val() == "00") {

                cabecera = cabecera + "<th>" + anhoAnt + "</th>" + "<th>" + anhoActual + "</th>";

                var varAnt = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Anho == anhoAnt; }).Select(function (item) { return item.ValorRanking; }).items;
                var avgValAnt = CalcularEstadoRanking(calculateAvarege(varAnt));

                var varAct = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Anho == anhoActual; }).Select(function (item) { return item.ValorRanking; }).items;
                var avgValAct = CalcularEstadoRanking(calculateAvarege(varAct));

                ranking = ranking + avgValAnt + avgValAct;

                varAnt = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Anho == anhoAnt; }).Select(function (item) { return item.ParticipacionPeriodo; }).items;
                avgValAnt = calculateAvarege(varAnt);

                varAct = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Anho == anhoActual; }).Select(function (item) { return item.ParticipacionPeriodo; }).items;
                avgValAct = calculateAvarege(varAct);

                participacion = participacion + "<td>" + avgValAnt + "</td>" + "<td>" + avgValAct + "</td>";

                varAnt = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Anho == anhoAnt; }).Select(function (item) { return item.LogroPeriodo; }).items;
                avgValAnt = calculateAvarege(varAnt);

                varAct = JSLINQ(lista).Where(function (item) { return item.Tipo == 'UC' && item.Anho == anhoActual; }).Select(function (item) { return item.LogroPeriodo; }).items;
                avgValAct = calculateAvarege(varAct);

                logro = logro + "<td>" + avgValAnt + "</td>" + "<td>" + avgValAct + "</td>";
            }

            cabecera = cabecera + "</tr>";
            ranking = ranking + "</tr>";
            participacion = participacion + "</tr>";
            logro = logro + "</tr>";

            $("#tblResultado thead").html("");
            $(cabecera).appendTo("#tblResultado thead");

            $("#tblResultado tbody").html("");
            $(ranking).appendTo("#tblResultado tbody");
            $(participacion).appendTo("#tblResultado tbody");
            $(logro).appendTo("#tblResultado tbody");
        }

        function crearTablaAnho(periodos, lista, anho, nombreTabla, codRol) {
            $("#sp" + nombreTabla).html('');

            var cabeceraPeriodo = "<tr><th rowspan='2' style='width:30px'>" + anho + "</th>";
            var cabeceraCampanha = "<tr>";
            var ranking = "<tr><th>Ranking</th>";

            var region = "";

            if (codRol == "5")
                region = "<tr><th>Región</th>";

            if (codRol == "6")
                region = "<tr><th>Zona</th>";

            var participacion;
            var logro;
            var text = '';

            if (isPorcentaje == "1") {
                text = "%";
            }

            if (codVariable == "VtaNet") {
                participacion = "<tr><th>%Part. Venta</th>";
                logro = "<tr><th>%Logro Venta</th>";

            } else {
                participacion = "<tr><th>" + text + "Valor Planeado</th>";
                logro = "<tr><th>" + text + "Valor Real</th>";
            }

            $.each(periodos, function (i, v) {

                var datos = JSLINQ(lista).Where(function (item) { return item.Periodo == v.Descripcion && item.Anho == anho; }).OrderBy(function (item) { return item.NombreCampanha; });

                if (datos.items.length > 0) {
                    cabeceraPeriodo = cabeceraPeriodo + "<th colspan='" + datos.items.length + "'>" + v.Descripcion + "</th>";

                    $.each(datos.items, function (j, w) {

                        cabeceraCampanha = cabeceraCampanha + "<th>" + w.NombreCampanha + "</th>";

                        switch (w.EstadoCampana.toUpperCase()) {
                            case 'CRITICA':
                                ranking = ranking + "<td style='font-weight:bold;width:50px;' class='rojo'>CRITICA </td>";
                                break;
                            case 'ESTABLE':
                                ranking = ranking + "<td style='font-weight:bold;width:50px;' class='amarillo'>ESTABLE</td>";
                                break;
                            case 'PRODUCTIVA':
                                ranking = ranking + "<td style='font-weight:bold;width:50px;' class='verde'>PRODUCTIVA</td>";
                                break;
                        }

                        participacion = participacion + "<td>" + parseFloat(w.ParticipacionCampana).toFixed(2) + "</td>";
                        logro = logro + "<td>" + parseFloat(w.LogroCampana).toFixed(2) + "</td>";
                        region = region + "<td>" + w.CodRegionZona + "</td>";
                    });
                }
            });

            cabeceraPeriodo = cabeceraPeriodo + "</tr>";
            cabeceraCampanha = cabeceraCampanha + "</tr>";
            ranking = ranking + "</tr>";
            participacion = participacion + "</tr>";
            logro = logro + "</tr>";
            region = region + "</tr>";

            $("#" + nombreTabla + " thead").html("");
            $("#" + nombreTabla + " tbody").html("");
            $(cabeceraPeriodo).appendTo("#" + nombreTabla + " thead");
            $(cabeceraCampanha).appendTo("#" + nombreTabla + " thead");
            $(ranking).appendTo("#" + nombreTabla + " tbody");
            $(participacion).appendTo("#" + nombreTabla + " tbody");
            $(logro).appendTo("#" + nombreTabla + " tbody");
            $(region).appendTo("#" + nombreTabla + " tbody");

            var numberColumns = countColumn(nombreTabla);

            if (numberColumns < 2) {
                $("#" + nombreTabla + " thead").html("");
                $("#" + nombreTabla + " tbody").html("");
                $("#sp" + nombreTabla).append('No existe datos en este año');
            }
        }

        function calculateAvarege(array) {

            var total = 0;
            if (array.length > 0) {
                $.each(array, function (i, v) {

                    total = total + parseFloat(v);
                });

                var resultado = parseFloat(total / parseFloat(array.length)).toFixed(2);
                return resultado;
            } else {
                return 0;
            }
        }

        function countColumn(table) {
            return $("#" + table).find('tr')[0].cells.length;
        }

        function descargarOption() {
            $("#dialog-descarga").dialog("open");

        }

        function CalcularEstadoRanking(valor) {

            var estado = "";
            valor = parseFloat(valor).toFixed(2);

            if (valor >= parseFloat("0").toFixed(2) && valor < parseFloat("3.5").toFixed(2)) {
                estado = "<td style='font-weight:bold;width:50px;' class='rojo'>CRITICA </td>";
            }

            if (valor >= parseFloat("3.5").toFixed(2) && valor < parseFloat("6.5").toFixed(2)) {
                estado = "<td style='font-weight:bold;width:50px;' class='amarillo'>ESTABLE</td>";
            }

            if (valor >= parseFloat("6.5").toFixed(2) && valor <= parseFloat("9").toFixed(2)) {
                estado = "<td style='font-weight:bold;width:50px;' class='verde'>PRODUCTIVA</td>";
            }

            return estado;
        }

        function descargar(tipo) {

            var codigo;
            if ($("#ddlGerenteZona").val() == "00") {
                codigo = $("#ddlGerenteRegion").val();

            } else {
                codigo = $("#ddlGerenteZona").val();
            }

            var codigoUsuarioEvaluado = codigo.split('-')[1];
            var codPaisEvaluado = codigo.split('-')[0];

            var anhoAct = $("#ddlAnho").val();
            var periodo = $("#ddlPeriodo").val();

            window.location = url + "?accion=descargarResultado&tipo=" + tipo + "&codigoUsuario=" + codigoUsuarioEvaluado + "&anho=" + anhoAct + "&periodo=" + periodo + "&codPais=" + codPaisEvaluado + "&codVariable=" + codVariable + "&isPorcentaje=" + isPorcentaje + "&rolEvaluado=" + rolEvaluado + "&codRegion=" + codRegion + "&codZona=" + codZona + "&nombreVariable=" + nombreVariable + "&gerenteRegion=" + gerenteRegion + "&gerenteZona=" + gerenteZona;
            $("#dialog-descarga").dialog("close");
        }

        function imprimir() {
            window.print();
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
