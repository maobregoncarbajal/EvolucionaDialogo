<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatrizZona.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Matriz.MatrizZona" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Página sin título</title>
</head>
<body style="text-align: center !important;">
    <form>
        <div>
            <h2>
                <br />
                <span style="font-size: 14; font-weight: bold">Matriz Zona</span>
            </h2>
            <br />
            <table width="100%">
                <tr>
                    <td align="center">
                        <table id="tblMatrizZona" cellspacing="0" rules="all" border="0">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="width: 100%;" id="Div1">
                <div style="float: right">
                    <input type="button" class="btnSquare" value="Imprimir" id="btnImprimirMz" onclick="imprimirMz();" />
                    &nbsp;&nbsp;&nbsp;
                <input type="button" class="btnSquare" value="Exp. PDF" id="btnExpPdfMz" onclick="ExpPdfMz();" />
                </div>
            </div>
        </div>
        <div id='dialog-detInfoMz' style="display: none">
            <br />
            <br />
            <br />
            <br />
            <table class="tablesorter" id="tblDetalleInformacionMz" cellspacing="0" rules="all"
                border="1">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div style="width: 100%;" id="panelButtonsDI">
                <div style="float: right">
                    <input type="button" class="btnSquare" value="Descargar" id="btnDescargarReporteDIMz"
                        onclick="descargarOptionDIMz();" />
                </div>
            </div>
        </div>
        <div id="idParametrosMz" style="display: none">
            <asp:Label ID="lblZonaMZ" runat="server"></asp:Label>
        </div>

        <script type="text/javascript" language="javascript">
            var htmlImprimir;
            $(document).ready(function () {

                crearPopUpMz();
                if ($("#ddlRegionMZ").val() == "00") {
                    MATRIZ.ShowAlert('dialog-alert', "Debe seleccionar una Region");
                } else if ($("#ddlAnhoMZ").val() == "00") {
                    MATRIZ.ShowAlert('dialog-alert', "Debe seleccionar un Año");
                } else {
                    var tipoMz = $("#ddlTipoMZ").val();
                    var paisMz = $("#ddlPaisMZ").val();
                    var regionMz = $("#ddlRegionMZ").val().split('-')[1];
                    var anhoMz = $("#ddlAnhoMZ").val();
                    var periodosMz = $("#ddlPeriodosMZ").val();

                    var parametros = { accion: 'obtenerListaMZ', tipoMz: tipoMz, paisMz: paisMz, regionMz: regionMz, anhoMz: anhoMz, periodosMz: periodosMz };
                    $.ajax({
                        url: url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        responseType: "json",
                        data: parametros,
                        success: function (response) {
                            if (response.length > 0) {
                                if (response.toString().substring(0, 5) != "Error") {

                                    CrearMatrizZona(response);
                                    $('.DatosCuadranteMz').click(function () {
                                        $("#lblZonaMZ").val("");
                                        var zonaMz = $(this).attr("id");
                                        $("#lblZonaMZ").val(zonaMz);
                                        cargarReportDetalleInformacionMz(response, zonaMz);
                                        jQuery.unblockUI();
                                    });
                                } else {
                                    jQuery.unblockUI();
                                    MATRIZ.ShowAlert('dialog-alert', response);
                                }
                            } else {
                                jQuery.unblockUI();
                                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
                            }
                        },
                        failure: function (msg) {
                            jQuery.unblockUI();
                            MATRIZ.ShowAlert('dialog-alert', "Error en la consulta");
                        },
                        error: function (request, status, error) {
                            alert(jQuery.parseJSON(request.responseText).Message);
                        }
                    });

                    /*var listaMz = obtenerListaMZ(tipoMz, paisMz, regionMz, anhoMz, periodosMz);
    
                    if (listaMz.length > 0) {
                    CrearMatrizZona(listaMz);
                    $('.DatosCuadranteMz').click(function() {
                    $("#lblZonaMZ").val("");
                    var zonaMz = $(this).attr("id");
                    $("#lblZonaMZ").val(zonaMz);
                    cargarReportDetalleInformacionMz(listaMz, zonaMz);
                    });
                    } else {
                    MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
                    }*/


                }
            });

            function obtenerListaMZ(vtipoMz, vpaisMz, vregionMz, vanhoMz, vperiodosMz) {

                var parametros = { accion: 'obtenerListaMZ', tipoMz: vtipoMz, paisMz: vpaisMz, regionMz: vregionMz, anhoMz: vanhoMz, periodosMz: vperiodosMz };
                var lista = MATRIZ.Ajax(url, parametros, false);
                return lista;
            }

            function CrearMatrizZona(lista) {

                var cuadrante1;
                var cuadrante2;
                var cuadrante3;
                var cuadrante4;
                var cuadrante5;
                var cuadrante6;
                var cuadrante7;
                var cuadrante8;
                var cuadrante9;

                $("#tblMatrizZona thead").html("");
                $("#tblMatrizZona tbody").html("");


                var inicial = 30;
                var factor = 6;

                var tituloMz = "";
                $(tituloMz).appendTo("#tblMatrizZona thead");

                var tbMatrizMz = "";
                tbMatrizMz = tbMatrizMz + "<tr><td style='border:0px !important;' class='subtiulo' colspan='5'>&nbsp;<br />&nbsp;<br /></td></tr>";

                tbMatrizMz = tbMatrizMz + "<tr><td class='CssTdAlto'></td>";
                //Inicio Cuadrante 7
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante7 = obtenerZonasxCuadrante(lista, 7).items;
                $.each(cuadrante7, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                tbMatrizMz = tbMatrizMz + "</td>";
                //Fin Cuadrante 7
                //Inicio Cuadrante 8
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante8 = obtenerZonasxCuadrante(lista, 8).items;
                $.each(cuadrante8, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                tbMatrizMz = tbMatrizMz + "</td>";
                //Fin Cuadrante 8
                //Inicio Cuadrante 9
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante9 = obtenerZonasxCuadrante(lista, 9).items;
                $.each(cuadrante9, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                tbMatrizMz = tbMatrizMz + "</td>";
                //Fin Cuadrante 9
                tbMatrizMz = tbMatrizMz + "</tr>";


                tbMatrizMz = tbMatrizMz + "<tr><td class='CssTdMedio'></td>";
                //Inicio Cuadrante 4
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante4 = obtenerZonasxCuadrante(lista, 4).items;
                $.each(cuadrante4, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                tbMatrizMz = tbMatrizMz + "</td>";
                //Fin Cuadrante 4
                //Inicio Cuadrante 5
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante5 = obtenerZonasxCuadrante(lista, 5).items;
                $.each(cuadrante5, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                tbMatrizMz = tbMatrizMz + "</td>";
                //Fin Cuadrante 5
                //Inicio Cuadrante 6
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante6 = obtenerZonasxCuadrante(lista, 6).items;
                $.each(cuadrante6, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                tbMatrizMz = tbMatrizMz + "</td>";
                //Fin Cuadrante 6
                tbMatrizMz = tbMatrizMz + "</tr>";


                tbMatrizMz = tbMatrizMz + "<tr><td class='CssTdBajo'></td>";
                //Inicio Cuadrante 1
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante1 = obtenerZonasxCuadrante(lista, 1).items;
                $.each(cuadrante1, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                //Fin Cuadrante 1
                //Inicio Cuadrante 2
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante2 = obtenerZonasxCuadrante(lista, 2).items;
                $.each(cuadrante2, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                //Fin Cuadrante 2
                //Inicio Cuadrante 3
                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz'>";
                cuadrante3 = obtenerZonasxCuadrante(lista, 3).items;
                $.each(cuadrante3, function (i, v) {
                    var constante = (Math.round(v.Participacion) * factor + inicial);
                    tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "' class='divMz' style='width:" + constante + "px !important;height:" + constante + "px !important;line-height:" + constante + "px !important;'>";
                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
                    tbMatrizMz = tbMatrizMz + "</div>";
                });
                //Fin Cuadrante 3
                tbMatrizMz = tbMatrizMz + "</tr>";

                tbMatrizMz = tbMatrizMz + "<tr><td style='border:0px !important;'></td>";
                tbMatrizMz = tbMatrizMz + "<td class='CssTdTamVenta'>Pequeño</td>";
                tbMatrizMz = tbMatrizMz + "<td class='CssTdTamVenta'>Mediano</td>";
                tbMatrizMz = tbMatrizMz + "<td class='CssTdTamVenta'>Grande</td>";
                tbMatrizMz = tbMatrizMz + "</tr>";

                $(tituloMz).appendTo("#tblMatrizZona thead");
                $(tbMatrizMz).appendTo("#tblMatrizZona tbody");

                htmlImprimir = $("#tblMatrizZona").html();
            }



            //        function CrearMatrizZona(lista) {

            //            $("#tblMatrizZona thead").html("");
            //            $("#tblMatrizZona tbody").html("");


            //            var inicial = 30;
            //            var factor = 6;

            //            var tituloMz = "";
            //            $(tituloMz).appendTo("#tblMatrizZona thead");

            //            var tbMatrizMz = "";
            //            tbMatrizMz = tbMatrizMz + "<tr><td style='border:0px !important;' class='subtiulo' colspan='5'>&nbsp;<br />&nbsp;<br /></td></tr>";


            //            var cuadrante;
            //            var c = 7;
            //            var residuo;
            //            var divisor = 3;

            //            while (c>0) {

            //                residuo = c % divisor;
            //                
            //                if (residuo == 1) {
            //                    tbMatrizMz = tbMatrizMz + "<tr><td class='CssTdRangoGap"+c+"'></td>";
            //                }
            //                //Inicio Cuadrante 1
            //                tbMatrizMz = tbMatrizMz + "<td class='CuadMediMz' style='background-image:url(../Styles/images/burbuja/imgCuadrante" + c + ".png); background-repeat:no-repeat;' >";
            //                cuadrante = obtenerZonasxCuadrante(lista, c).items;
            //                $.each(cuadrante, function(i, v) {
            //                var constante = (Math.round(v.Participacion) * factor + inicial);
            //                tbMatrizMz = tbMatrizMz + "<div title='Zona: " + v.CodZona + "'  style='width:" + constante + "px !important;height:" + constante + "px !important; float: left; '>";
            //                tbMatrizMz = tbMatrizMz + "<div class='fondoestiradoMz'>";
            //                    tbMatrizMz = tbMatrizMz + "<img src='../Styles/images/burbuja.jpg' />";
            //                    tbMatrizMz = tbMatrizMz + "</div>";
            //                    tbMatrizMz = tbMatrizMz + "<div class='contenedorgeneralMz'>";
            //                    tbMatrizMz = tbMatrizMz + "<a class='DatosCuadranteMz' id=" + v.CodZona + " style='font-size:10px;'>" + parseInt(v.Participacion) + "%</a>";
            //                    tbMatrizMz = tbMatrizMz + "</div>";
            //                    tbMatrizMz = tbMatrizMz + "</div>";
            //                });
            //                tbMatrizMz = tbMatrizMz + "</td>";
            //                //Fin Cuadrante 1
            //                
            //                if (residuo == 0) {
            //                    tbMatrizMz = tbMatrizMz + "</tr>";
            //                }

            //                if (residuo > 0)
            //                {
            //                    c = c + 1;
            //                }else
            //                {
            //                    c = c - 5;
            //                }
            //            }

            //            tbMatrizMz = tbMatrizMz + "<tr><td style='border:0px !important;'></td>";
            //            tbMatrizMz = tbMatrizMz + "<td class='CssTdTamVenta'>Pequeño</td>";
            //            tbMatrizMz = tbMatrizMz + "<td class='CssTdTamVenta'>Mediano</td>";
            //            tbMatrizMz = tbMatrizMz + "<td class='CssTdTamVenta'>Grande</td>";
            //            tbMatrizMz = tbMatrizMz + "</tr>";

            //            $(tituloMz).appendTo("#tblMatrizZona thead");
            //            $(tbMatrizMz).appendTo("#tblMatrizZona tbody");

            //            htmlImprimir = $("#tblMatrizZona").html();
            //        }

            function obtenerZonasxCuadrante(listaCompleta, cuadrante) {
                var sample = JSLINQ(listaCompleta).Where(function (item) { return item.Cuadrante == cuadrante; });
                return sample;
            }


            function cargarReportDetalleInformacionMz(listaDetalleInformacion, zona) {

                if (listaDetalleInformacion.length > 0) {

                    var listaFiltradaxZona = (filtrarListaxZona(listaDetalleInformacion, zona)).items;

                    if (listaFiltradaxZona.length > 0) {

                        $("#tblDetalleInformacionMz thead").html("");

                        var tituloMz = "<tr>";

                        var tipo = $("#ddlTipoMZ").val();

                        if (tipo = "01") {

                            tituloMz = tituloMz + "<th>Nombre Gerente Zona</th><th>chrCodZona</th><th>activasFinales</th><th>poblacion</th><th>tamPoblacion</th><th>penetracion</th><th>Benchmark</th><th>gapPenentracion</th><th>rangosGAP</th><th>Factor</th><th>Benchmark2</th><th>PentxFactor</th><th>gapFactor</th><th>RangosGAP2</th><th>ventaMN</th><th>TamanhodeVenta</th><th>Cuadrante</th><th>Participacion</th></tr>";
                        } else if (tipo = "02") {
                            tituloMz = tituloMz + "<th>Nombre Gerente Zona</th><th>Zona</th><th>ActivasFinales</th><th>Poblacion</th><th>TamPoblacion</th><th>GPS</th><th>Penetracion</th><th>Benchmark</th><th>GAPPenetracion</th><th>RangosGAp</th><th>Grupo</th><th>Factor</th><th>Benchmark2</th><th>PenetracionFactor</th><th>GAPFactor</th><th>RangosGAP2</th><th>VentasNetas</th><th>TamanioVenta</th><th>Cuadrante</th><th>Participacion</th></tr>";
                        }

                        $(tituloMz).appendTo("#tblDetalleInformacionMz thead");

                        var tablaMz = "";

                        $.each(listaFiltradaxZona, function (i, v) {
                            if (tipo = "01") {
                                if (v.RangosGAp == null) {
                                    v.RangosGAp = '';
                                }
                                tablaMz = tablaMz + "<tr><td>" + v.DesGerenteZona + "</td><td>" + v.CodZona + "</td><td>" + v.ActivasFinales + "</td><td>" + v.Poblacion + "</td><td>" + v.TamPoblacion + "</td><td>" + v.Penetracion + "</td><td>" + v.Benchmark + "</td><td>" + v.GapPenentracion + "</td><td>" + v.RangosGAp + "</td><td>" + v.Factor + "</td><td>" + v.Benchmark2 + "</td><td>" + v.PentFactor + "</td><td>" + v.GapFactor + "</td><td>" + v.RangosGAp2 + "</td><td>" + v.VentaMn + "</td><td>" + v.TamanhoVenta + "</td><td>" + v.Cuadrante + "</td><td>" + parseInt(v.Participacion) + "%</td></tr>";
                            } else if (tipo = "02") {
                                if (v.RangosGAp == null) {
                                    v.RangosGAp = '';
                                }
                                tablaMz = tablaMz + "<tr><td>" + v.DesGerenteZona + "</td><td>" + v.CodZona + "</td><td>" + v.ActivasFinales + "</td><td>" + v.Poblacion + "</td><td>" + v.TamPoblacion + "</td><td>" + v.GPs + "</td><td>" + v.Penetracion + "</td><td>" + v.Benchmark + "</td><td>" + v.GapPenentracion + "</td><td>" + v.RangosGAp + "</td><td>" + v.Grupo + "</td><td>" + v.Factor + "</td><td>" + v.Benchmark2 + "</td><td>" + v.PentFactor + "</td><td>" + v.GapFactor + "</td><td>" + v.RangosGAp2 + "</td><td>" + v.VentaMn + "</td><td>" + v.TamanhoVenta + "</td><td>" + v.Cuadrante + "</td><td>" + parseInt(v.Participacion) + "%</td></tr>";
                            }

                        });


                        $("#tblDetalleInformacionMz tbody").html("");
                        $(tablaMz).appendTo("#tblDetalleInformacionMz tbody");

                        $("#dialog-detInfoMz").dialog("open");

                    } else {
                        MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
                    }
                } else {
                    MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
                }
            }

            function filtrarListaxZona(listaFiltrar, zona) {
                var sample = JSLINQ(listaFiltrar).
                        Where(function (item) { return jQuery.trim(item.CodZona) == zona; });
                return sample;
            }


            function crearPopUpMz() {

                var arrayDialogMz = [
                        { name: "dialog-detInfoMz", height: 500, width: 900, title: "Detalle Información" }
                ];

                MATRIZ.CreateDialogs(arrayDialogMz);
            }


            function descargarOptionDIMz() {
                $("#dialog-detInfoMz").dialog("close");
                $.blockUI({
                    message: '<h1>Descargando...</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#000',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    }
                });
                var tipo = $("#ddlTipoMZ").val();
                var pais = $("#ddlPaisMZ").val();
                var region = $("#ddlRegionMZ").val().split('-')[1];
                var anho = $("#ddlAnhoMZ").val();
                var periodos = $("#ddlPeriodosMZ").val();
                var zona = $("#lblZonaMZ").val();

                window.location = url + "?accion=descargarDetalleInformacionMz&tipoMz=" + tipo + "&paisMz=" + pais + "&regionMz=" + region + "&anhoMz=" + anho + "&periodosMz=" + periodos + "&zonaMz=" + zona;


                setTimeout(function () {
                    $.unblockUI({
                        onUnblock: function () {
                        }
                    });
                }, 10000);


                $("#dialog-descargaDIMz").dialog("close");
            }

            function imprimirMz() {
                //            top.consoleRef = window.open('', 'myconsole',
                //                'width=350,height=250'
                //                    + ',menubar=0'
                //                        + ',toolbar=1'
                //                            + ',status=0'
                //                                + ',scrollbars=1'
                //                                    + ',resizable=1');
                //            top.consoleRef.document.writeln(
                //                '<html><head><title>Console</title><link href="<%=Evoluciona.Dialogo.Web.Helpers.Utils.AbsoluteWebRoot%>Styles/Matriz.css" rel="stylesheet" type="text/css" /><link href="<%=WebDDesempenio.Helpers.Utils.AbsoluteWebRoot%>Styles/Matriz.css" rel="stylesheet" type="text/css" media="print"/></head>'
                //                    + '<body bgcolor=white onLoad="self.focus()"><table>'
                //                        + htmlImprimir
                //                            + '</table></body></html>'
                //            );
                //            top.consoleRef.document.close();
                window.print();
            }

            function ExpPdfMz() {
                $("#dialog-detInfoMz").dialog("close");

                $.blockUI({
                    message: '<h1>Descargando...</h1>',
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#000',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    }
                });


                var tipo = $("#ddlTipoMZ").val();
                var pais = $("#ddlPaisMZ").val();
                var region = $("#ddlRegionMZ").val().split('-')[1];
                var anho = $("#ddlAnhoMZ").val();
                var periodos = $("#ddlPeriodosMZ").val();


                var nombreTipoMatriz = $("#ddlTipoMZ option:selected").text();
                var nombrePais = $("#ddlPaisMZ option:selected").text();
                var nombrePeriodos = $("#ddlPeriodosMZ option:selected").text();
                var nombreRegion = $("#ddlRegionMZ option:selected").text();

                window.location = url + "?accion=descargarMatrizZona&tipoMz=" + tipo + "&paisMz=" + pais + "&regionMz=" + region + "&anhoMz=" + anho + "&periodosMz=" + periodos + "&nomTipoMz=" + nombreTipoMatriz + "&nomPais=" + nombrePais + "&nomPeriodo=" + nombrePeriodos + "&nomRegion=" + nombreRegion;

                setTimeout(function () {
                    $.unblockUI({
                        onUnblock: function () {
                        }
                    });
                }, 10000);

                $("#dialog-descargaDIMz").dialog("close");
            }

        </script>

    </form>
</body>
</html>
