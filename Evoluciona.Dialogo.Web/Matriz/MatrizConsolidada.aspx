<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatrizConsolidada.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Matriz.MatrizConsolidada" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        .grillaMorada {
            border-collapse: collapse;
            font-family: Arial, Helvetica, sans-serif;
            color: #586777;
        }

            .grillaMorada th {
                font-weight: bold;
                background-color: #5C1D6C;
                color: #fff;
                height: 20px;
                padding: 3px 5px 3px 5px;
                border: 1.5px solid #A2ACB1;
                font-size: 13px;
                vertical-align: middle;
            }

            .grillaMorada td {
                border: 1.5px solid #A2ACB1;
                vertical-align: middle;
                text-align: center;
                padding: 5px 5px 5px 5px;
                font-size: 12px;
            }

            .grillaMorada a {
                color: #586777;
                padding: 0px;
                margin: 0px;
            }

            .grillaMorada .pgr {
                border: 0px;
            }

        .cabeceraPrincipal {
            border: 0px;
            height: 25px;
        }

        .cabeceraSecundaria {
            background-color: #E9ECED;
            height: 30px;
        }

        .resaltado01 {
            width: 60px;
            color: White;
            background-color: #0F243E;
        }

        .resaltado02 {
            background-color: #8DB4E2;
            color: #FFFFFF;
        }

        .resultados {
            background-color: #C8C8C8;
            height: 20px;
            width: 100px; /*color: #000000;*/
            color: #0066CC;
        }

        .data {
            color: #0066CC; /*#C8C8C8;*/
        }

        #tblMatrizConsolidada td {
            border: solid 1px;
            border-color: #A9B2B7; /*#E7E9EC;*/
        }
    </style>
</head>
<body>
    <div align="center">
        <h2>
            <span id="tituloMatrizConsolidada" style="font-size: 14; font-weight: bold">Matriz consolidada</span></h2>
        <br />
        <table id="tblMatrizConsolidada" cellspacing="0" rules="all" border="1px" style="text-align: center; font-family: Arial; font-size: 9px">
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
                onclick="descargarOption01();" />
        </div>
    </div>
    <div id='dialog-resumen' style="display: none">
        <br />
        <table id="tblResumen" class="grillaMorada" cellspacing="0" rules="all" border="1">
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
        <div style="width: 100%;" id="Div1">
            <div style="float: right">
                <input type="button" class="btnSquare" value="Descargar" id="Button4" onclick="descargarOption02();" />
            </div>
        </div>
        <div>
            <input id='Flag' style="display: none" name='txtFlag' value='' type='text' />
        </div>
    </div>
    <div id='dialog-ficha' style="display: none" />
    <div id="dialog-descarga-01" style="display: none">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="button" id="btnPdf" class="downloadPDF" onclick="descargar('pdf', '01');">
                    </button>
                </td>
                <td>
                    <button type="button" id="btnExcel" class="downloadExcel" onclick="descargar('xls', '01');">
                    </button>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-descarga-02" style="display: none">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="button" id="Button1" class="downloadPDF" onclick="descargar('pdf', '02');">
                    </button>
                </td>
                <td>
                    <button type="button" id="Button2" class="downloadExcel" onclick="descargar('xls', '02');">
                    </button>
                </td>
            </tr>
        </table>
    </div>
    <div id="mensajeSinDatos" style="display: none; height: 30px">
        No existen datos.
    </div>

    <script type="text/javascript" language="javascript">
        var matrizFiltradaFinal = "";
        var matrizFiltradaFinalDetalle = "";

        function optMensaje() {
            $("#mensajeSinDatos").dialog("open");
        }

        $(document).ready(function () {
            cargarReporteMatrizConsolidada();
            CrearPopUp();

            $(".data u, .resultados u").click(function () {
                var matrizCompleta = "";
                var matrizDatos = this.id.split('.');
                var esTipo = "";
                var id = this.id;
                var tipoColaborador = $("#ddlTipoColaborador").val();

                if (matrizDatos.length == 0) {
                    return;
                }

                if (matrizDatos.length == 1) {
                    if (tipoColaborador == null) {
                        return;
                    } else if (tipoColaborador == '00') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.TamVenta.toUpperCase() == matrizDatos[0].toUpperCase();
                        });
                    } else if (tipoColaborador == '01') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.TamVenta.toUpperCase() == matrizDatos[0].toUpperCase()
                            && item.Tipo == 'N';
                        });
                    } else if (tipoColaborador == '02') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.TamVenta.toUpperCase() == matrizDatos[0].toUpperCase()
                            && item.Tipo != 'N';
                        });
                    } else {
                        return;
                    }
                } else if (matrizDatos.length == 2) {
                    esTipo = matrizDatos[1].toUpperCase();
                    if (esTipo == 'N') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                            && item.Tipo.toUpperCase() == matrizDatos[1].toUpperCase();
                        });
                    } else if (esTipo == 'ASM') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return (item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                                    && item.Tipo.toUpperCase() != 'N' && item.Cuadrante == '') ||
                                    (item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                                    && item.Tipo.toUpperCase() != 'N' && item.Competencia == '');
                        });
                    } else {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                            && item.NivelCompetencia.toUpperCase() == matrizDatos[1].toUpperCase()
                                        && item.Tipo != 'N' && item.Competencia != '';
                            ;
                        });
                    }
                } else if (matrizDatos.length == 3) {
                    esTipo = matrizDatos[1].toUpperCase();
                    if (esTipo == 'N') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                                && item.Tipo.toUpperCase() == matrizDatos[1].toUpperCase()
                                && item.TamVenta.toUpperCase() == matrizDatos[2].toUpperCase();
                        });
                    } else if (esTipo == 'ASM') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return (item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                                    && item.TamVenta.toUpperCase() == matrizDatos[2].toUpperCase()
                                    && item.Tipo.toUpperCase() != 'N' && item.Cuadrante == '') ||
                                    (item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                                    && item.TamVenta.toUpperCase() == matrizDatos[2].toUpperCase()
                                    && item.Tipo.toUpperCase() != 'N' && item.Competencia == '');
                        });
                    } else {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return item.Ranking.toUpperCase() == matrizDatos[0].toUpperCase()
                                && item.NivelCompetencia.toUpperCase() == matrizDatos[1].toUpperCase()
                                && item.TamVenta.toUpperCase() == matrizDatos[2].toUpperCase()
                                            && item.Tipo != 'N' && item.Competencia != '';
                            ;
                        });
                    }
                } else if (matrizDatos.length == 4) {
                    if (tipoColaborador == null) {
                        return;
                    } else if (tipoColaborador == '00') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return 1 == 1;
                        });
                    } else if (tipoColaborador == '01') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return 1 == 1
                                    && item.Tipo == 'N';
                        });
                    } else if (tipoColaborador == '02') {
                        matrizCompleta = JSLINQ(matrizFiltradaFinal).Where(function (item) {
                            return 1 == 1
                                    && item.Tipo != 'N';
                        });
                    } else {
                        return;
                    }
                }


                matrizFiltradaFinalDetalle = matrizCompleta;

                if (matrizCompleta.items.length > 0) {
                    GetResumen(matrizCompleta);
                    $("#Flag").val(id);
                    $("#dialog-resumen").dialog("open");
                }
                else {
                    optMensaje();
                    //alert("Lo sentimos, esta coordenada se encuentra sin datos.");
                }
            });

            $(".data u, .resultados u").mouseenter(function (evento) {
                $(this).css('cursor', 'hand');
            });

            $(".data u, .resultados u").mouseleave(function (evento) {
                $(this).css('cursor', 'pointer');
            });
        });

        function GetResumen(arreglo) {

            var cdRgn = $("#ddlRegion").val();
            var cdZn = $("#ddlZona").val();

            var titulo = "";
            titulo = titulo + '<tr><th></th><th>Cuadrante</th>';
            if (cdRgn == "00" && cdZn == "00") {
                titulo = titulo + '<th>Nombre GR/GZ</th>';
            } else if (cdRgn != "00") {
                titulo = titulo + '<th>Nombre GZ</th>';
            } else {
                titulo = titulo + '<th>Nombre GR</th>';
            }

            titulo = titulo + '<th>Código Región</th> <th>Puntos Ranking</th> <th>Ranking</th> <th>Competencias</th> <th>Venta Periodo</th> <th>%Logro Venta</th> <th>Participación Venta</th><th>Tamaño Venta</th></tr>';


            $("#tblResumen thead").html("");
            $("#tblResumen tbody").html("");

            $(titulo).appendTo("#tblResumen thead");

            var tabla = "";
            $.each(arreglo.items, function (i, v) {
                tabla = tabla + "<tr>";
                var index = parseInt(i) + parseInt(1);
                tabla = tabla + "<td>" + index + "</td>";

                if (v.Tipo == 'N') {
                    tabla = tabla + "<td> NUEVAS </td>";
                }
                else {
                    tabla = tabla + "<td>" + v.Cuadrante + "</td>";
                }

                tabla = tabla + "<td><div class='evaluador' id='" + v.DocIdentidad + "-" + v.PrefijoIsoPais + "' style='cursor:pointer;'>" + v.Nombre + "</div></td>";
                tabla = tabla + "<td>" + v.NombreRegion + "</td>";
                tabla = tabla + "<td>" + v.PuntoRanking + "</td>";

                switch (v.Ranking.toUpperCase()) {
                    case 'CRITICA':
                        tabla = tabla + "<td style='font-weight:bold;width:50px;' class='rojo'>" + v.Ranking + "</td>";
                        break;
                    case 'ESTABLE':
                        tabla = tabla + "<td style='font-weight:bold;width:50px;' class='amarillo'>" + v.Ranking + "</td>";
                        break;
                    case 'PRODUCTIVA':
                        tabla = tabla + "<td style='font-weight:bold;width:50px;' class='verde'>" + v.Ranking + "</td>";
                        break;
                }

                tabla = tabla + "<td>" + v.Competencia + "</td>";
                tabla = tabla + "<td>" + v.VentaPeriodo + "</td>";
                tabla = tabla + "<td>" + v.LogroPeriodo + "</td>";
                tabla = tabla + "<td>" + v.ParticipacionVenta + "%" + "</td>";
                tabla = tabla + "<td>" + v.TamVenta + "</td>";
                tabla = tabla + "</tr>";
            });
            $(tabla).appendTo("#tblResumen tbody");

            $("#dialog-resumen").dialog("open");
            $("#dialog-resumen").dialog("widget").next(".ui-widget-overlay").css("background", "#f00ba2");
            $("#tblResumen").tablesorter({ widthFixed: true }).tablesorterPager({ container: $("#pager"), positionFixed: false });

            $('.evaluador').click(function () {
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
        }

        function CrearPopUp() {
            var arrayDialog;

            arrayDialog = [{ name: "dialog-resumen", height: 500, width: 900, title: "Detalle Información" },
            { name: "dialog-ficha", height: 500, width: 200, title: "Ficha" }];
            MATRIZ.CreateDialogs(arrayDialog);

            arrayDialog = [{ name: "dialog-descarga-01", height: 110, width: 110, title: "Descargar" }];
            MATRIZ.CreateDialogs(arrayDialog);

            arrayDialog = [{ name: "dialog-descarga-02", height: 110, width: 110, title: "Descargar" }];
            MATRIZ.CreateDialogs(arrayDialog);

            arrayDialog = [{ name: "mensajeSinDatos", height: 110, width: 110, title: "Alerta" }];
            MATRIZ.CreateDialogs(arrayDialog);
        }

        function cargarReporteMatrizConsolidada() {
            var periodo = $("#ddlPeriodos").val();
            var subPeriodo = $("#ddlSubPeriodos").val();
            var zona = $("#ddlZona").val();

            $("#tituloMatrizConsolidada").html('');

            if ($("#ddlRegion").val() != "00") {
                codPaisEvaluado = $("#ddlRegion").val().split('-')[0];
                codRegion = $("#ddlRegion").val().split('-')[1];
                $("#tituloMatrizConsolidada").html('Matriz consolidada - Listado de las gerentes de zona');
            }
            else {
                codRegion = "00";
                codPaisEvaluado = codPaisEvaluador;
                $("#tituloMatrizConsolidada").html('Matriz consolidada - Listado de las gerentes de región');
            }

            var rolEvaluado = "";
            if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                idRolEvaluado = "2";
            }
            else {
                idRolEvaluado = "3";
            }

            var parametros;
            var lista;

            if (codRegion == "00" && zona == "00") {
                parametros = {
                    accion: 'detalleInformacion'
                                , periodo: periodo
                                , subPeriodo: subPeriodo
                                , region: codRegion
                                , zona: zona
                                , codigoUsuario: codigoUsuarioEvaluador
                                , rolEvaluado: idRolEvaluado
                                , pais: codPaisEvaluado
                                , rolEvaluador: idRolEvaluador
                };

                lista = MATRIZ.Ajax(url, parametros, false);

                $.each(lista, function (indc, valr) {

                    valr.Nombre = 'GR: ' + valr.Nombre;
                });

                $.each(lista, function (indi, valo) {
                    var para = {
                        accion: 'detalleInformacion'
                                , periodo: periodo
                                , subPeriodo: subPeriodo
                                , region: valo.NombreRegion.split('-')[0]
                                , zona: zona
                                , codigoUsuario: codigoUsuarioEvaluador
                                , rolEvaluado: 3
                                , pais: codPaisEvaluado
                                , rolEvaluador: 1
                    };

                    var lista2 = MATRIZ.Ajax(url, para, false);


                    $.each(lista2, function (ndc, vlr) {

                        var itm = new Object();
                        itm.PrefijoIsoPais = vlr.PrefijoIsoPais;
                        itm.Cuadrante = vlr.Cuadrante;
                        itm.Nombre = 'GZ: ' + vlr.Nombre;
                        itm.DocIdentidad = vlr.DocIdentidad;
                        itm.NombreRegion = vlr.NombreRegion;
                        itm.NombreZona = vlr.NombreZona;
                        itm.PuntoRanking = vlr.PuntoRanking;
                        itm.Ranking = vlr.Ranking;
                        itm.Competencia = vlr.Competencia;
                        itm.VentaPeriodo = vlr.VentaPeriodo;
                        itm.VentaPlanPeriodo = vlr.VentaPlanPeriodo;
                        itm.NivelCompetencia = vlr.NivelCompetencia;
                        itm.LogroPeriodo = vlr.LogroPeriodo;
                        itm.ParticipacionVenta = vlr.ParticipacionVenta;
                        itm.PorcentajeAvance = vlr.PorcentajeAvance;
                        itm.TamVenta = vlr.TamVenta;
                        itm.Tipo = vlr.Tipo;


                        lista.push(itm);

                    });

                });
            } else if (codRegion == "00" && zona == "") {
                parametros = { accion: 'detalleInformacion', periodo: periodo, subPeriodo: subPeriodo, region: codRegion, zona: zona, codigoUsuario: codigoUsuarioEvaluador, rolEvaluado: 2, pais: codPaisEvaluado, rolEvaluador: idRolEvaluador };
                lista = MATRIZ.Ajax(url, parametros, false);
            } else {
                parametros = { accion: 'detalleInformacion', periodo: periodo, subPeriodo: subPeriodo, region: codRegion, zona: zona, codigoUsuario: codigoUsuarioEvaluador, rolEvaluado: idRolEvaluado, pais: codPaisEvaluado, rolEvaluador: idRolEvaluador };
                lista = MATRIZ.Ajax(url, parametros, false);
            }

            lista = (JSLINQ(lista).OrderBy(function (item) { return item.NombreRegion; })).items;


            matrizFiltradaFinal = lista;

            if (lista.length > 0) {
                CrearTabla(lista, rolEvaluado);
            }
        }

        function CrearTabla(lista, rolEvaluado) {
            var tipoColaborador = $("#ddlTipoColaborador").val();
            var colSpan = "";
            var titulo = "";
            var matriz = new Array("CRITICA", "ESTABLE", "PRODUCTIVA");

            if (tipoColaborador == null) {
                return;
            }
            else if (tipoColaborador == '00') {
                colSpan = "5";
            }
            else if (tipoColaborador == '01') {
                colSpan = "1";
            }
            else if (tipoColaborador == '02') {
                colSpan = "4";
            }
            else { return; }

            var subtitulos = MATRIZ.Ajax(url, { accion: 'tipos', fileName: 'Competencia.xml', adicional: 'no' }, false);

            $("#tblMatrizConsolidada tbody").html("");

            titulo = "<tr>";
            titulo = titulo + "<td rowspan='2' class='resaltado01'>";
            titulo = titulo + "Tamaño venta";
            titulo = titulo + "</td>";

            titulo = titulo + "<td colspan='" + colSpan + "' class='cabeceraPrincipal' style='background-color:#FF4646'>";
            titulo = titulo + "<b>Cr&iacute;tica</b></td>";
            titulo = titulo + "<td colspan='" + colSpan + "' class='cabeceraPrincipal' style='background-color:#FFFF66'>";
            titulo = titulo + "<b>Estable</b></td>";
            titulo = titulo + "<td colspan='" + colSpan + "' class='cabeceraPrincipal' style='background-color:#51D739'>";
            titulo = titulo + "<b>Productiva</b></td>";
            titulo = titulo + "<td rowspan='2' class='resaltado01'>";
            titulo = titulo + "Total";
            titulo = titulo + "</td>";
            titulo = titulo + "</tr>";

            titulo = titulo + "<tr class='cabeceraSecundaria'>";

            var varDescripcion = "";

            if (subtitulos.length > 0) {
                for (var i = 0; i < matriz.length; i++) {
                    if (tipoColaborador == '00' || tipoColaborador == '01') {
                        titulo = titulo + "<td>Nuevas</td>";
                    }

                    if (tipoColaborador == '00' || tipoColaborador == '02') {
                        $.each(subtitulos, function (f, val) {
                            varDescripcion = val.Descripcion.toLowerCase();
                            varDescripcion = varDescripcion.substring(0, 1).toUpperCase() + varDescripcion.substring(1);
                            titulo = titulo + "<td>" + varDescripcion + "</td>";
                        });
                        titulo = titulo + "<td>Sin Medición</td>";
                    }
                }
            }

            titulo = titulo + "</tr>";

            var subtitulosH = MATRIZ.Ajax(url, { accion: 'tipos', fileName: 'TamVenta.xml', adicional: 'no' }, false);

            if (subtitulosH.length > 0) {
                $.each(subtitulosH, function (vi, val) {
                    var varSubtiDatos = val.Descripcion.toLowerCase();
                    varSubtiDatos = varSubtiDatos.substring(0, 1).toUpperCase() + varSubtiDatos.substring(1);
                    titulo = titulo + "<tr class='data'>";
                    titulo = titulo + "<td class='resaltado02'><b>" + varSubtiDatos + "</b></td>";

                    for (var x = 0; x < matriz.length; x++) {
                        if (tipoColaborador == '00' || tipoColaborador == '01') {
                            var nuevas = JSLINQ(lista).Where(function (item) {
                                return item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                && item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase()
                                && item.Tipo == 'N';
                            });

                            titulo = titulo + "<td><u id='" + matriz[x] + ".N." + val.Descripcion + "'>" + JSLINQ(nuevas.items).Count() + "</u></td>";
                        }
                        if (tipoColaborador == '00' || tipoColaborador == '02') {
                            $.each(subtitulos, function (vsi, varSubtitulo) {
                                var filtrado = JSLINQ(lista).Where(function (item) {
                                    return item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                    && item.NivelCompetencia.toUpperCase() == varSubtitulo.Descripcion.toUpperCase()
                                    && item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase()
                                    && item.Tipo != 'N' && item.Competencia != '';
                                });
                                titulo = titulo + "<td><u id='" + matriz[x] + "." + varSubtitulo.Descripcion + "." + val.Descripcion + "'>" + JSLINQ(filtrado.items).Count() + "</u></td>";
                            });
                            var sinmedicion = JSLINQ(lista).Where(function (item) {
                                return (item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                && item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase()
                                && item.Tipo != 'N' && item.Cuadrante == '') || (item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                && item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase()
                                && item.Tipo != 'N' && item.Competencia == '');
                            });
                            titulo = titulo + "<td><u id='" + matriz[x] + ".ASM." + val.Descripcion + "'>" + JSLINQ(sinmedicion.items).Count() + "</u></td>";
                        }
                    }
                    var totalV = "0";

                    if (tipoColaborador == '00') {
                        var filtrado = JSLINQ(lista).Where(function (item) {
                            return item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase();
                        });
                        totalV = JSLINQ(filtrado.items).Count();
                    }
                    else if (tipoColaborador == '01') {
                        var filtrado = JSLINQ(lista).Where(function (item) {
                            return item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase()
                            && item.Tipo == 'N';
                        });
                        totalV = JSLINQ(filtrado.items).Count();
                    }
                    else if (tipoColaborador == '02') {
                        var filtrado = JSLINQ(lista).Where(function (item) {
                            return item.TamVenta.toUpperCase() == val.Descripcion.toUpperCase()
                            && item.Tipo != 'N';
                        });
                        totalV = JSLINQ(filtrado.items).Count();
                    }

                    titulo = titulo + "<td class='resultados'><u id=" + val.Descripcion + ">" + totalV + "</u></td>";
                    titulo = titulo + "</tr>";
                });

                titulo = titulo + "<tr class='resultados'>";
                titulo = titulo + "<td class='resaltado01'>Total</td>";

                for (var x = 0; x < matriz.length; x++) {
                    if (tipoColaborador == '00' || tipoColaborador == '01') {
                        var nuevas = JSLINQ(lista).Where(function (item) {
                            return item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                && item.Tipo == 'N';
                        });

                        titulo = titulo + "<td><u id='" + matriz[x] + ".N'>" + JSLINQ(nuevas.items).Count() + "</u></td>";
                    }
                    if (tipoColaborador == '00' || tipoColaborador == '02') {
                        $.each(subtitulos, function (i, varSubtitulo) {
                            var filtrado = JSLINQ(lista).Where(function (item) {
                                return item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                    && item.NivelCompetencia.toUpperCase() == varSubtitulo.Descripcion.toUpperCase()
                                    && item.Tipo != 'N' && item.Competencia != '';
                            });

                            titulo = titulo + "<td><u id='" + matriz[x] + "." + varSubtitulo.Descripcion + "'>" + JSLINQ(filtrado.items).Count() + "</u></td>";
                        });
                        var sinmedicion = JSLINQ(lista).Where(function (item) {
                            return (item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                && item.Tipo != 'N' && item.Cuadrante == '') || (item.Ranking.toUpperCase() == matriz[x].toUpperCase()
                                && item.Tipo != 'N' && item.Competencia == '');
                        });
                        titulo = titulo + "<td><u id='" + matriz[x] + ".ASM'>" + JSLINQ(sinmedicion.items).Count() + "</u></td>";
                    }
                }

                var totalTotal = "0";
                var filtradoTotal;

                if (tipoColaborador == '00') {
                    filtradoTotal = JSLINQ(lista).Where(function (item) {
                        return 1 == 1;
                    });
                    totalTotal = JSLINQ(filtradoTotal.items).Count();
                }
                else if (tipoColaborador == '01') {
                    filtradoTotal = JSLINQ(lista).Where(function (item) {
                        return 1 == 1
                            && item.Tipo == 'N';
                    });
                    totalTotal = JSLINQ(filtradoTotal.items).Count();
                }
                else if (tipoColaborador == '02') {
                    filtradoTotal = JSLINQ(lista).Where(function (item) {
                        return 1 == 1
                            && item.Tipo != 'N';
                    });
                    totalTotal = JSLINQ(filtradoTotal.items).Count();
                }
                titulo = titulo + "<td><u id='Total.Total.Total.Total'>" + totalTotal + "</u></td>";
                titulo = titulo + "</tr>";
            }

            $(titulo).appendTo("#tblMatrizConsolidada tbody");
        }

        function descargarOption01() {
            $("#dialog-descarga-01").dialog("open");
        }

        function descargarOption02() {
            $("#dialog-descarga-02").dialog("open");
        }

        function descargar(tipo, formato) {
            var periodo = $("#ddlPeriodos").val();
            var subPeriodo = $("#ddlSubPeriodos").val();
            var zona = $("#ddlZona").val();
            var nombreZona = $("#ddlZona option:selected").text();
            var nombreRegion = $("#ddlRegion option:selected").text();
            var tipoColaborador = $("#ddlTipoColaborador").val();
            var codRegion;

            if ($("#ddlRegion").val() != "00") {
                codPaisEvaluado = $("#ddlRegion").val().split('-')[0];
                codRegion = $("#ddlRegion").val().split('-')[1];
            }
            else {
                codRegion = "00";
                codPaisEvaluado = codPaisEvaluador;
            }

            var idRolEvaluado = "";
            if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                idRolEvaluado = "2";
            }
            else {
                idRolEvaluado = "3";
            }

            if (tipoColaborador == null) {
                alert('Aún no ha seleccionado un tipo de colaborador');
                return;
            }

            var html = $("#tblMatrizConsolidada").html();

            if (formato == '01') {
                var datos = $('td');
                var varResultados = "";

                $.each(datos, function (clave, valor) {
                    $(this).children("u").each(function (j, k, l) {
                        if (varResultados != "") {
                            varResultados += "|" + $(this).text();
                        }
                        else {
                            varResultados += $(this).text();
                        }
                    });
                });

                window.location = url + "?accion=descargarMatrizConsolidada&tipo=" + tipo + "&periodo=" + periodo + "&subPeriodo=" + subPeriodo + "&region=" + codRegion + "&zona=" + zona + "&nombreRegion=" + nombreRegion + "&nombreZona=" + nombreZona + "&rolEvaluado=" + idRolEvaluado + "&formato=" + formato + "&tipoColaborador=" + tipoColaborador + "&resultados=" + varResultados;
            }
            else if (formato == '02') {
                var matrizDatos = $("#Flag").val().split('.');
                var ranking = "";
                var nivelCompetencia = "";
                var tamVenta = "";
                var tipoData = "";
                var esTipo = "";

                if (matrizDatos.length == 0) {
                    return;
                }

                if (matrizDatos.length == 1) {
                    if (tipoColaborador == '00') {
                        tamVenta = matrizDatos[0].toUpperCase();
                    }
                    else if (tipoColaborador == '01') {
                        tamVenta = matrizDatos[0].toUpperCase();
                        tipoData = 'N';
                    }
                    else if (tipoColaborador == '02') {
                        tamVenta = matrizDatos[0].toUpperCase();
                    }
                    else { return; }
                }
                else if (matrizDatos.length == 2) {
                    esTipo = matrizDatos[1].toUpperCase();
                    if (esTipo == 'N') {
                        ranking = matrizDatos[0].toUpperCase();
                        tipoData = matrizDatos[1].toUpperCase();
                    } else if (esTipo == 'ASM') {
                        ranking = matrizDatos[0].toUpperCase();
                        tipoData = matrizDatos[1].toUpperCase();
                    }
                    else {
                        ranking = matrizDatos[0].toUpperCase();
                        nivelCompetencia = matrizDatos[1].toUpperCase();
                        tipoData = "-1";
                    }
                }
                else if (matrizDatos.length == 3) {
                    esTipo = matrizDatos[1].toUpperCase();
                    if (esTipo == 'N') {
                        ranking = matrizDatos[0].toUpperCase();
                        tipoData = matrizDatos[1].toUpperCase();
                        tamVenta = matrizDatos[2].toUpperCase();
                    } else if (esTipo == 'ASM') {
                        ranking = matrizDatos[0].toUpperCase();
                        tipoData = matrizDatos[1].toUpperCase();
                        tamVenta = matrizDatos[2].toUpperCase();
                    }
                    else {
                        ranking = matrizDatos[0].toUpperCase();
                        nivelCompetencia = matrizDatos[1].toUpperCase();
                        tamVenta = matrizDatos[2].toUpperCase();
                        tipoData = "-1";
                    }
                } else if (matrizDatos.length == 4) {
                    if (tipoColaborador == '00') {
                    }
                    else if (tipoColaborador == '01') {
                        tipoData = 'N';
                    }
                    else if (tipoColaborador == '02') {
                    }
                    else { return; }
                }


                window.location = url + "?accion=descargarMatrizConsolidada&tipo=" + tipo + "&periodo=" + periodo + "&subPeriodo=" + subPeriodo + "&region=" + codRegion + "&zona=" + zona + "&codigoUsuario=" + codigoUsuarioEvaluador + "&rolEvaluado=" + idRolEvaluado + "&pais=" + codPaisEvaluado + "&rolEvaluador=" + idRolEvaluador + "&nombreRegion=" + nombreRegion + "&nombreZona=" + nombreZona + "&ranking=" + ranking + "&nivelCompetencia=" + nivelCompetencia + "&tamVenta=" + tamVenta + "&tipoColaborador=" + tipoColaborador + "&tipoData=" + tipoData + "&formato=" + formato;
            }

            $("#dialog-descarga-01").dialog("close");
            $("#dialog-descarga-02").dialog("close");
        }

        function imprimir() {
            window.print();
        }
    </script>

</body>
</html>
