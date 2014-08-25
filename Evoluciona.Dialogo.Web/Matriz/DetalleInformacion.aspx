<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleInformacion.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Matriz.DetalleInformacion" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <div>
        <h2><span id="tituloDetalleInformacion" style="font-size: 14; font-weight: bold">Detalle de Información</span></h2>
        <br />
        <table class="tablesorter" id="tblDetalleInfromacion" cellspacing="0" rules="all"
            border="1">
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

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            crearPopUpDetalleInformacion();
            cargarReportDetalleInformacion();
        });

        function crearPopUpDetalleInformacion() {

            var arrayDialog = [{ name: "dialog-descarga", height: 110, width: 110, title: "Descargar" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function cargarReportDetalleInformacion() {
            var periodo = $("#ddlPeriodos").val();
            var subPeriodo = $("#ddlSubPeriodos").val();

            $("#tituloDetalleInformacion").html('');

            if ($("#ddlRegion").val() != "00") {
                codPaisEvaluado = $("#ddlRegion").val().split('-')[0];
                codRegion = $("#ddlRegion").val().split('-')[1];
                $("#tituloDetalleInformacion").html('Detalle de Información - Listado de las gerentes de zona');
            }
            else {
                codRegion = "00";
                codPaisEvaluado = codPaisEvaluador;
                $("#tituloDetalleInformacion").html('Detalle de Información - Listado de las gerentes de región');
            }

            var zona = $("#ddlZona").val();

            if (codRegion == "00" && ($("#ddlZona").val() == "00" || $("#ddlZona").val() == null)) {
                idRolEvaluado = "2";
            }
            else {
                idRolEvaluado = "3";
            }


            var parametros = { accion: 'detalleInformacion', periodo: periodo, subPeriodo: subPeriodo, region: codRegion, zona: zona, codigoUsuario: codigoUsuarioEvaluador, rolEvaluado: idRolEvaluado, pais: codPaisEvaluado, rolEvaluador: idRolEvaluador };

            var lista = MATRIZ.Ajax(url, parametros, false);

            if (lista.length > 0) {
                CrearTabla(lista, idRolEvaluado);
                $("#panelButtons").show();
                $("#pager").show();
            }
            else {
                $("#panelButtons").hide();
                $("#pager").hide();
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }
        }

        function CrearTabla(lista, idRolEvaluado) {
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
            $.each(lista, function (i, v) {
                tabla = tabla + "<tr>";


                //tabla = tabla + "<td>" + v.Cuadrante + "</td>";

                if (v.Tipo == 'N') {
                    tabla = tabla + "<td> NUEVAS </td>";
                }
                else {
                    tabla = tabla + "<td>" + v.Cuadrante + "</td>";
                }

                tabla = tabla + "<td><u style='cursor:pointer;color:#2e22eb;' class='evaluado'id='" + v.DocIdentidad + "-" + v.PrefijoIsoPais + "'>" + v.Nombre + "</u></td><td>" + v.NombreRegion + "</td><td>";

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

            $("#tblDetalleInfromacion").tablesorter({ widthFixed: true, widgets: ['zebra'] }).tablesorterPager({ container: $("#pager"), positionFixed: false });
        }

        function descargarOption() {
            $("#dialog-descarga").dialog("open");

        }

        function descargar(tipo) {

            var periodo = $("#ddlPeriodos").val();
            var subPeriodo = $("#ddlSubPeriodos").val();
            var nombreRegion = $("#ddlRegion option:selected").text();
            var zona = $("#ddlZona").val();
            var nombreZona = $("#ddlZona option:selected").text();

            window.location = url + "?accion=descargarDetalleInformacion&tipo=" + tipo + "&periodo=" + periodo + "&subPeriodo=" + subPeriodo + "&region=" + codRegion + "&zona=" + zona + "&codigoUsuario=" + codigoUsuarioEvaluador + "&rolEvaluado=" + idRolEvaluado + "&pais=" + codPaisEvaluado + "&rolEvaluador=" + idRolEvaluador + "&nombreRegion=" + nombreRegion + "&nombreZona=" + nombreZona;

            $("#dialog-descarga").dialog("close");
        }

        function imprimir() {
            window.print();
        }
    </script>

</body>
</html>
