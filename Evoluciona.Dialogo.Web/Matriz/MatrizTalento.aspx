<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatrizTalento.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Matriz.MatrizTalento" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <div class="divExport">
        <h2><span style="font-size: 14; font-weight: bold">Matriz Talento</span></h2>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 150px;"></td>
                <td align="center">
                    <table id="tblMatrizTalento" cellspacing="0" rules="all" border="0">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </td>
                <td style="width: 50px;"></td>
                <td align="center">
                    <table id="tblMatrizTalentoSinMed" cellspacing="0" rules="all" border="0">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </td>
                <td style="width: 150px;"></td>
            </tr>
        </table>
    </div>
    <div style="width: 100%;" id="panelButtons">
        <div style="float: right">
            <input type="button" class="btnSquare print" value="Imprimir" id="btnPrintReporteDetalle" onclick="imprimir();" />
        </div>
        <div style="float: right">
            <input type="button" class="btnSquare" value="Descargar" id="btnDescargarReporteDetalle" onclick="descargarOption();" />
        </div>
    </div>
    <div id="dialog-descarga" style="display: none">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="button" id="btnPdf" class="downloadPDF" onclick="descargar('pdf');"></button>
                </td>
                <td>
                    <button type="button" id="btnExcel" class="downloadExcel" onclick="descargar('xls');"></button>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-descargaDI" style="display: none">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="button" id="btnPdfDI" class="downloadPDF" onclick="descargarDI('pdf');"></button>
                </td>
                <td>
                    <button type="button" id="btnExcelDI" class="downloadExcel" onclick="descargarDI('xls');"></button>
                </td>
            </tr>
        </table>
    </div>
    <div id='dialog-detInfo' style="display: none">
        <br />
        <br />
        <strong style="font-weight: bold;">Cuadrante :&nbsp;&nbsp;<span id="lblCuadrante"></span></strong>
        <br />
        <br />
        <table class="tablesorter" id="tblDetalleInfromacion" cellspacing="0" rules="all" border="1">
            <thead>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div id="pager" class="pager">
            <form>
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/first.png"
                    class="first" />
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/prev.png"
                    class="prev" />
                <input type="text" class="pagedisplay" />
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/next.png"
                    class="next" />
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/last.png"
                    class="last" />
                <select class="pagesize">
                    <option selected="selected" value="10">10</option>
                    <option value="20">20</option>
                    <option value="30">30</option>
                    <option value="40">40</option>
                </select>
            </form>
        </div>
        <div style="width: 100%;" id="panelButtonsDI">
            <div style="float: right">
                <input type="button" class="btnSquare" value="Descargar" id="btnDescargarReporteDI" onclick="descargarOptionDI();" />
            </div>
        </div>
    </div>
    <div>
        <table bgcolor="" width='30px'>
            <tr>
                <td align="right"></td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            // CRITICA-SIN MEDICION
            var CriticaAntSinMedicion = "";
            var CriticaNueSinMedicion = "";
            // ESTABLE-SIN MEDICION
            var EstableAntSinMedicion = "";
            var EstableNueSinMedicion = "";
            // PRODUCTIVA-SIN MEDICION
            var ProductivaAntSinMedicion = "";
            var ProductivaNueSinMedicion = "";

            crearPopUp();

            var lista = cargarListDetalleInformacion();
            if (lista == "0") {
                MATRIZ.ShowAlert('dialog-alert', "Debe seleccionar una Region");
            } else {
                cargarReportMatrizTalento(lista);
                $('.DatosCuadrante').click(function () {
                    var cuadrante = $(this).attr("id");

                    if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                        if ($("#ddlTipoReporte").val() == "02") {
                            idRolEvaluado = "2";
                        }

                        if ($("#ddlTipoReporte").val() == "03") {
                            idRolEvaluado = "3";
                        }
                    }
                    else {
                        idRolEvaluado = "3";
                    }
                    cargarReportDetalleInformacion(lista, cuadrante, idRolEvaluado);
                });
                $('.DatosCuadranteSM').click(function () {
                    var cuadrante = $(this).attr("id");

                    if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                        if ($("#ddlTipoReporte").val() == "02") {
                            idRolEvaluado = "2";
                        }

                        if ($("#ddlTipoReporte").val() == "03") {
                            idRolEvaluado = "3";
                        }
                    }
                    else {
                        idRolEvaluado = "3";
                    }
                    cargarReportDetalleInformacionSM(lista, cuadrante, idRolEvaluado);
                });
            }
        });

        function crearPopUp() {

            var arrayDialog = [
                                { name: "dialog-detInfo", height: 500, width: 900, title: "Detalle Información" },
                                { name: "dialog-descargaDI", height: 110, width: 110, title: "Descargar" },
                                { name: "dialog-descarga", height: 110, width: 110, title: "Descargar" }
            ];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function cargarListDetalleInformacion() {

            if ($("#ddlTipoReporte").val() == "03" && $("#ddlRegion").val() == "00" && ($("#ddlZona").val() == null || $("#ddlZona").val() == "00")) {
                return "0";
            } else {
                var periodo = $("#ddlPeriodos").val();
                //var subPeriodo = $("#ddlSubPeriodos").val();
                var subPeriodo = "00";

                if ($("#ddlRegion").val() != "00") {
                    codPaisEvaluado = $("#ddlRegion").val().split('-')[0];
                    codRegion = $("#ddlRegion").val().split('-')[1];
                }
                else {
                    codRegion = "00";
                    codPaisEvaluado = codPaisEvaluador;
                }

                var zona = $("#ddlZona").val();


                if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                    if ($("#ddlTipoReporte").val() == "02") {
                        idRolEvaluado = "2";
                    }

                    if ($("#ddlTipoReporte").val() == "03") {
                        idRolEvaluado = "3";
                    }
                }
                else {
                    idRolEvaluado = "3";
                }





                var parametros = { accion: 'detalleInformacion', periodo: periodo, subPeriodo: subPeriodo, region: codRegion, zona: zona, codigoUsuario: codigoUsuarioEvaluador, rolEvaluado: idRolEvaluado, pais: codPaisEvaluado, rolEvaluador: idRolEvaluador };
                var listaDI = MATRIZ.Ajax(url, parametros, false);
                return listaDI;

            }



        }

        function cargarReportMatrizTalento(lista) {

            if (lista.length > 0) {
                CrearTablaMatrizTalento(lista);
                CrearTablaMatrizTalentoSinMed(lista);
            }
            else {
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }
        }

        function CrearTablaMatrizTalento(lista) {

            var subtitulos = MATRIZ.Ajax(url, { accion: 'tipos', fileName: 'Competencia.xml', adicional: 'no' }, false);

            $("#tblMatrizTalento thead").html("");

            var titulo = "";
            $(titulo).appendTo("#tblMatrizTalento thead");

            var tbMatriz = "";
            tbMatriz = tbMatriz + "<tr><td style='border:0px !important;' class='subtiulo' colspan='5'>&nbsp;<br />&nbsp;<br /></td></tr>";
            tbMatriz = tbMatriz + "<tr><td class='CssTdProductiva'></td>";

            $.each(subtitulos, function (i, val) {
                tbMatriz = tbMatriz + "<td class='CuadMedi'><a class='DatosCuadrante' id ='PRODUCTIVA-" + val.Descripcion + "'>" + ContarProductiva(lista, val.Descripcion) + "</td>";
            });

            tbMatriz = tbMatriz + "</tr>";
            tbMatriz = tbMatriz + "<tr><td class='CssTdEstable'></td>";

            $.each(subtitulos, function (i, val) {
                tbMatriz = tbMatriz + "<td class='CuadMedi'><a class='DatosCuadrante' id ='ESTABLE-" + val.Descripcion + "'>" + ContarEstable(lista, val.Descripcion) + "</td>";
            });

            tbMatriz = tbMatriz + "</tr>";
            tbMatriz = tbMatriz + "<tr><td class='CssTdCritica'></td>";

            $.each(subtitulos, function (i, val) {
                tbMatriz = tbMatriz + "<td class='CuadMedi'><a class='DatosCuadrante' id ='CRITICA-" + val.Descripcion + "'>" + ContarCritica(lista, val.Descripcion) + "</td>";
            });

            tbMatriz = tbMatriz + "</tr>";
            tbMatriz = tbMatriz + "<tr><td style='border:0px !important;'></td>";

            $.each(subtitulos, function (i, val) {
                varDescripcion = val.Descripcion.toLowerCase();
                varDescripcion = varDescripcion.substring(0, 1).toUpperCase() + varDescripcion.substring(1);
                tbMatriz = tbMatriz + "<td class='CssTdCompetencia'>" + varDescripcion + "</td>";
            });

            tbMatriz = tbMatriz + "</tr>";

            $("#tblMatrizTalento thead").html("");
            $(titulo).appendTo("#tblMatrizTalento thead");

            $("#tblMatrizTalento tbody").html("");
            $(tbMatriz).appendTo("#tblMatrizTalento tbody");
        }

        function CrearTablaMatrizTalentoSinMed(lista) {

            //CRITICAS-SIN MEDICION
            CriticaAntSinMedicion = ContarCriticaAntSinMedicion(lista);
            CriticaNueSinMedicion = ContarCriticaNueSinMedicion(lista);

            //ESTABLE-SIN MEDICION
            EstableAntSinMedicion = ContarEstableAntSinMedicion(lista);
            EstableNueSinMedicion = ContarEstableNueSinMedicion(lista);

            //PRODUCTIVA-SIN MEDICION
            ProductivaAntSinMedicion = ContarProductivaAntSinMedicion(lista);
            ProductivaNueSinMedicion = ContarProductivaNueSinMedicion(lista);

            //TOTALES SIN MEDICION
            TotalAntSinMedicion = ProductivaAntSinMedicion + EstableAntSinMedicion + CriticaAntSinMedicion;
            TotalNueSinMedicion = ProductivaNueSinMedicion + EstableNueSinMedicion + CriticaNueSinMedicion;

            $("#tblMatrizTalentoSinMed thead").html("");

            var titulo = "";

            $(titulo).appendTo("#tblMatrizTalentoSinMed thead");

            var tbMatriz = "";
            tbMatriz = tbMatriz + "<tr><td style='border:0px !important;' class='subtiuloMT'>Antiguas<br />Sin Medicion</td><td style='border:0px !important;' class='subtiuloMT'>Nuevas<br />Sin Medicion</td></tr>"
            tbMatriz = tbMatriz + "<tr><td class='CuadMedi'><a class='DatosCuadranteSM' id='PRODUCTIVA-ANTIGUASM'>" + ProductivaAntSinMedicion + "</a></td><td class='CuadMedi'><a class='DatosCuadranteSM' id='PRODUCTIVA-NUEVASM'>" + ProductivaNueSinMedicion + "</a></td></tr>";
            tbMatriz = tbMatriz + "<tr><td class='CuadMedi'><a class='DatosCuadranteSM' id='ESTABLE-ANTIGUASM'>" + EstableAntSinMedicion + "</a></td><td class='CuadMedi'><a class='DatosCuadranteSM' id='ESTABLE-NUEVASM'>" + EstableNueSinMedicion + "</a></td></tr>";
            tbMatriz = tbMatriz + "<tr><td class='CuadMedi'><a class='DatosCuadranteSM' id='CRITICA-ANTIGUASM'>" + CriticaAntSinMedicion + "</a></td><td class='CuadMedi'><a class='DatosCuadranteSM' id='CRITICA-NUEVASM'>" + CriticaNueSinMedicion + "</a></td></tr>";
            tbMatriz = tbMatriz + "<tr><td class='CuadSinMediTotal'>" + TotalAntSinMedicion + "</td><td class='CuadSinMediTotal'>" + TotalNueSinMedicion + "</td></tr>";

            $("#tblMatrizTalentoSinMed thead").html("");
            $(titulo).appendTo("#tblMatrizTalentoSinMed thead");

            $("#tblMatrizTalentoSinMed tbody").html("");
            $(tbMatriz).appendTo("#tblMatrizTalentoSinMed tbody");
        }

        function cargarReportDetalleInformacion(lista, cuadrante, idRolEvaluado) {

            if (lista.length > 0) {
                CrearTabla(lista, cuadrante, idRolEvaluado);
            }
            else {
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }
        }

        function cargarReportDetalleInformacionSM(lista, cuadrante, idRolEvaluado) {

            if (lista.length > 0) {
                CrearTablaSM(lista, cuadrante, idRolEvaluado);
            }
            else {
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }
        }

        function CrearTabla(lista, cuadrante, idRolEvaluado) {

            var listaFiltrada = (FiltrarListaxCuadrante(lista, cuadrante)).items;

            if (listaFiltrada.length > 0) {


                $("#lblCuadrante").html("");
                $("#lblCuadrante").append(cuadrante);
                $("#tblDetalleInfromacion thead").html("");

                var titulo = "<tr><th>Cuadrante</th>";

                if (idRolEvaluado == "3")//GZ
                {
                    titulo = titulo + " <th>Gerente de Zona</th><th>Región</th><th>Zona</th>";
                }
                else {
                    titulo = titulo + " <th>Gerente de Región</th><th>Región</th>";
                }

                titulo = titulo + "<th>Puntos Rankig</th><th>Ranking</th><th>Competencia</th><th>Venta Periodo</th><th>%Logro</th><th>%Participación Venta</th><th>Tamaño Venta</th></tr>";

                $(titulo).appendTo("#tblDetalleInfromacion thead");

                var tabla = "";
                $.each(listaFiltrada, function (i, v) {
                    tabla = tabla + "<tr><td>" + v.Cuadrante + "</td><td>" + v.Nombre + "</td><td>" + v.NombreRegion + "</td><td>";

                    if (idRolEvaluado == "3")//GZ
                    {
                        tabla = tabla + v.NombreZona + "</td><td>";
                    }

                    tabla = tabla + parseFloat(v.PuntoRanking).toFixed(2);

                    switch (v.Ranking.toString().toUpperCase()) {
                        case 'CRITICA':
                            tabla = tabla + "</td><td style='font-weight:bold;width:50px;' class='rojo'>";
                            break;
                        case 'ESTABLE':
                            tabla = tabla + "</td><td style='font-weight:bold;width:50px;' class='amarillo'>";
                            break;
                        case 'PRODUCTIVA':
                            tabla = tabla + "</td><td style='font-weight:bold;width:50px;' class='verde'>";
                            break;
                    }
                    tabla = tabla + v.Ranking + "</td><td>" + v.Competencia + "</td><td>" + v.VentaPeriodo + "</td><td>" + v.LogroPeriodo + " %" + "</td><td>" + parseFloat(v.ParticipacionVenta).toFixed(2) + " %" + "</td><td>" + v.TamVenta + "</td></tr>";
                });

                $("#tblDetalleInfromacion tbody").html("");
                $(tabla).appendTo("#tblDetalleInfromacion tbody");

                $("#tblDetalleInfromacion").tablesorter({ widthFixed: true }).tablesorterPager({ container: $("#pager"), positionFixed: false });

                $("#dialog-detInfo").dialog("open");
                $("#dialog-detInfo").dialog("widget").next(".ui-widget-overlay").css("background", "#f00ba2");




            }
            else {
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }


        }

        function CrearTablaSM(lista, cuadrante, idRolEvaluado) {

            var listaFiltrada = null;
            if (cuadrante.split('-')[1] == 'ANTIGUASM') {
                listaFiltrada = (FiltrarListaxCuadranteASM(lista, cuadrante)).items;
            }

            if (cuadrante.split('-')[1] == 'NUEVASM') {
                listaFiltrada = (FiltrarListaxCuadranteNSM(lista, cuadrante)).items;
            }

            if (listaFiltrada.length > 0) {
                var str = cuadrante;
                var substr = str.split('-');
                var estado = "";

                if (substr[1] == 'ANTIGUASM') {
                    estado = "Antiguas Sin Medición";
                }

                if (substr[1] == 'NUEVASM') {
                    estado = "Nuevas Sin Medición";
                }

                $("#lblCuadrante").html("");
                $("#lblCuadrante").append(substr[0] + " - " + estado);

                $("#tblDetalleInfromacion thead").html("");

                var titulo = "<tr><th>Cuadrante</th>";

                if (idRolEvaluado == "3")//GZ
                {
                    titulo = titulo + " <th>Gerente de Zona</th><th>Región</th><th>Zona</th>";
                }
                else {
                    titulo = titulo + " <th>Gerente de Región</th><th>Región</th>";
                }

                titulo = titulo + "<th>Puntos Rankig</th>  <th>Ranking</th> <th>Competencia</th>  <th>Venta Periodo</th> <th>%Logro</th> <th>%Participación Venta</th> <th>Tamaño Venta</th></tr>";

                $(titulo).appendTo("#tblDetalleInfromacion thead");

                var tabla = "";
                $.each(listaFiltrada, function (i, v) {
                    tabla = tabla + "<tr><td>" + $("#lblCuadrante").text() + "</td><td>" + v.Nombre + "</td><td>" + v.NombreRegion + "</td><td>";

                    if (idRolEvaluado == "3")//GZ
                    {
                        tabla = tabla + v.NombreZona + "</td><td>";
                    }

                    tabla = tabla + parseFloat(v.PuntoRanking).toFixed(2);

                    switch (v.Ranking.toString().toUpperCase()) {
                        case 'CRITICA':
                            tabla = tabla + "</td><td style='font-weight:bold;width:50px;' class='rojo'>";
                            break;
                        case 'ESTABLE':
                            tabla = tabla + "</td><td style='font-weight:bold;width:50px;' class='amarillo'>";
                            break;
                        case 'PRODUCTIVA':
                            tabla = tabla + "</td><td style='font-weight:bold;width:50px;' class='verde'>";
                            break;
                    }
                    tabla = tabla + v.Ranking + "</td><td>" + v.Competencia + "</td><td>" + v.VentaPeriodo + "</td><td>" + v.LogroPeriodo + " %" + "</td><td>" + parseFloat(v.ParticipacionVenta).toFixed(2) + " %" + "</td><td>" + v.TamVenta + "</td></tr>";
                });

                $("#tblDetalleInfromacion tbody").html("");
                $(tabla).appendTo("#tblDetalleInfromacion tbody");

                $("#dialog-detInfo").dialog("open");
                $("#dialog-detInfo").dialog("widget").next(".ui-widget-overlay").css("background", "#f00ba2");
            }
            else {
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }



        }

        // Filtro Critica
        function ContarCritica(listaDI, competencia) {
            var sample = JSLINQ(listaDI).Count(function (item) { return item.Ranking == 'CRITICA' && item.Cuadrante == 'CRITICA-' + competencia + '' && item.Competencia != ''; });
            return sample;
        }


        function ContarCriticaAntSinMedicion(listaDI) {
            var sample = JSLINQ(listaDI).Count(function (item) { return (item.Ranking == 'CRITICA' && item.Tipo != 'N' && item.Cuadrante == '') || (item.Ranking == 'CRITICA' && item.Tipo != 'N' && (item.Competencia == '')); });
            return sample;
        }

        function ContarCriticaNueSinMedicion(listaDI) {
            var sample = JSLINQ(listaDI).Count(function (item) { return (item.Ranking == 'CRITICA' && item.Tipo == 'N' && item.Cuadrante == '') || (item.Ranking == 'CRITICA' && item.Tipo == 'N' && (item.Competencia == '')); });
            return sample;
        }

        // Filtro Estable
        function ContarEstable(listaDI, competencia) {
            var sample = JSLINQ(listaDI).Count(function (item) { return item.Ranking == 'ESTABLE' && item.Cuadrante == 'ESTABLE-' + competencia + '' && item.Competencia != ''; });
            return sample;
        }

        function ContarEstableAntSinMedicion(listaDI) {
            var sample = JSLINQ(listaDI).Count(function (item) { return (item.Ranking == 'ESTABLE' && item.Tipo != 'N' && item.Cuadrante == '') || (item.Ranking == 'ESTABLE' && item.Tipo != 'N' && (item.Competencia == '')); });
            return sample;
        }

        function ContarEstableNueSinMedicion(listaDI) {
            var sample = JSLINQ(listaDI).Count(function (item) { return (item.Ranking == 'ESTABLE' && item.Tipo == 'N' && item.Cuadrante == '') || (item.Ranking == 'ESTABLE' && item.Tipo == 'N' && (item.Competencia == '')); });
            return sample;
        }

        // Filtro Productiva

        function ContarProductiva(listaDI, competencia) {
            var sample = JSLINQ(listaDI).Count(function (item) { return item.Ranking == 'PRODUCTIVA' && item.Cuadrante == 'PRODUCTIVA-' + competencia + '' && item.Competencia != ''; });
            return sample;
        }

        function ContarProductivaAntSinMedicion(listaDI) {
            var sample = JSLINQ(listaDI).Count(function (item) { return (item.Ranking == 'PRODUCTIVA' && item.Tipo != 'N' && item.Cuadrante == '') || (item.Ranking == 'PRODUCTIVA' && item.Tipo != 'N' && (item.Competencia == '')); });
            return sample;
        }

        function ContarProductivaNueSinMedicion(listaDI) {
            var sample = JSLINQ(listaDI).Count(function (item) { return (item.Ranking == 'PRODUCTIVA' && item.Tipo == 'N' && item.Cuadrante == '') || (item.Ranking == 'PRODUCTIVA' && item.Tipo == 'N' && (item.Competencia == '')); });
            return sample;
        }

        /**/

        function FiltrarListaxCuadrante(listaDI, cuadrant) {
            var sample = JSLINQ(listaDI).
            Where(function (item) { return item.Cuadrante == cuadrant && item.Competencia != ''; });
            return sample;
        }

        function FiltrarListaxCuadranteASM(listaDI, cuadrant) {
            var sample = JSLINQ(listaDI).
            Where(function (item) { return (item.Ranking == cuadrant.split('-')[0] && item.Tipo != 'N' && item.Cuadrante == '') || (item.Ranking == cuadrant.split('-')[0] && item.Tipo != 'N' && item.Competencia == ''); });
            return sample;
        }

        function FiltrarListaxCuadranteNSM(listaDI, cuadrant) {
            var sample = JSLINQ(listaDI).
            Where(function (item) { return (item.Ranking == cuadrant.split('-')[0] && item.Tipo == 'N' && item.Cuadrante == '') || (item.Ranking == cuadrant.split('-')[0] && item.Tipo == 'N' && item.Competencia == ''); });
            return sample;
        }

        function descargarOption() {
            $("#dialog-descarga").dialog("open");
        }

        function descargarOptionDI() {
            $("#dialog-detInfo").dialog("close");
            $("#dialog-descargaDI").dialog("open");
        }

        function descargar(tipo) {

            var periodo = $("#ddlPeriodos").val();
            //var subPeriodo = $("#ddlSubPeriodos").val();
            var subPeriodo = "00";

            if ($("#ddlRegion").val() != "00") {
                codPaisEvaluado = $("#ddlRegion").val().split('-')[0];
                codRegion = $("#ddlRegion").val().split('-')[1];
            }
            else {
                codRegion = "00";
                codPaisEvaluado = codPaisEvaluador;
            }

            var zona = $("#ddlZona").val();
            var nombreRegion = $("#ddlRegion option:selected").text();
            var nombreZona = $("#ddlZona option:selected").text();


            if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                if ($("#ddlTipoReporte").val() == "02") {
                    idRolEvaluado = "2";
                }

                if ($("#ddlTipoReporte").val() == "03") {
                    idRolEvaluado = "3";
                }
            }
            else {
                idRolEvaluado = "3";
            }

            window.location = url + "?accion=descargarMatrizTalento&tipo=" + tipo + "&periodo=" + periodo + "&subPeriodo=" + subPeriodo + "&region=" + codRegion + "&zona=" + zona + "&codigoUsuario=" + codigoUsuarioEvaluador + "&rolEvaluado=" + idRolEvaluado + "&pais=" + codPaisEvaluado + "&rolEvaluador=" + idRolEvaluador + "&nombreRegion=" + nombreRegion + "&nombreZona=" + nombreZona;

            $("#dialog-descarga").dialog("close");
        }


        function descargarDI(tipo) {

            var periodo = $("#ddlPeriodos").val();

            //var subPeriodo = $("#ddlSubPeriodos").val();
            var subPeriodo = "00";


            if ($("#ddlRegion").val() != "00") {
                codPaisEvaluado = $("#ddlRegion").val().split('-')[0];
                codRegion = $("#ddlRegion").val().split('-')[1];
            }
            else {
                codRegion = "00";
                codPaisEvaluado = codPaisEvaluador;
            }

            var nombreRegion = $("#ddlRegion option:selected").text();

            var zona = $("#ddlZona").val();

            var nombreZona = $("#ddlZona option:selected").text();


            if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                if ($("#ddlTipoReporte").val() == "02") {
                    idRolEvaluado = "2";
                }

                if ($("#ddlTipoReporte").val() == "03") {
                    idRolEvaluado = "3";
                }
            }
            else {
                idRolEvaluado = "3";
            }

            var cuadrante = $("#lblCuadrante").text();

            window.location = url + "?accion=descargarDetalleInformacionMatrizTalento&tipo=" + tipo + "&periodo=" + periodo + "&subPeriodo=" + subPeriodo + "&region=" + codRegion + "&zona=" + zona + "&codigoUsuario=" + codigoUsuarioEvaluador + "&rolEvaluado=" + idRolEvaluado + "&pais=" + codPaisEvaluado + "&rolEvaluador=" + idRolEvaluador + "&nombreRegion=" + nombreRegion + "&nombreZona=" + nombreZona + "&cuadrante=" + cuadrante;

            $("#dialog-descargaDI").dialog("close");
        }

        function imprimir() {
            window.print();
        }
    </script>
</body>
</html>
