<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Filtros.ascx.cs" Inherits="Evoluciona.Dialogo.Web.Calendario.Filtros" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<style type="text/css">
    .marcarEvento, .marcarEvento a:link, .marcarEvento a:visited {
        font-weight: bold;
        color: #000;
        text-decoration: none;
        text-align: center;
    }
</style>
<table style="width: 100%; vertical-align: top;">
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
    <tr>
        <td>
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="panFiltros" runat="server">
                <div style="font-weight: bold;">
                    Filtros de Visualización :
                </div>
                <br />
                <ul id="ulFiltros" class="staticMenu">
                </ul>
            </asp:Panel>
        </td>
    </tr>
</table>
<script type="text/javascript">

    var dates = [];
    var contador = 0;
    var fechaUltima;
    var cambiarMeses = '<%= cambiarMeses %>';
    var urlMetodos = "<%=Utils.RelativeWebRoot%>Calendario/Metodos.ashx";
    var prefijoIsoPais = "<%= PrefijoIsoPais%>";
    var codigoRol = <%= CodigoRol%> ;
    var codigoUsuario = "<%= CodigoUsuario%>";

    jQuery(document).ready(function () {

        $.ajaxSetup({ cache: false, async: false });

        $('#datepicker').datepicker({
            inline: true,
            firstDay: 1,
            isRTL: false,
            changeYear: true,
            changeMonth: true,
            dateFormat: 'dd/mm/yy',
            prevText: '<-',
            nextText: '->',
            currentText: 'Actual',
            closeText: 'Aceptar',
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
                    'Junio', 'Julio', 'Agosto', 'Septiembre',
                    'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
                    'Junio', 'Julio', 'Agosto', 'Septiembre',
                    'Octubre', 'Noviembre', 'Diciembre'],
            beforeShowDay: marcarDiasEnCalendario,
            onSelect: seleccionarFecha,
            onChangeMonthYear: function (year, month, inst) {
                var date = new Date(year, month, 0);
                cargarCalendarioPorFecha(date);
            }
        });

        jQuery.getJSON(urlMetodos,
                    {
                        accion: "CargarFiltros",
                        codigoRol: codigoRol,
                        codigoUsuario: codigoUsuario
                    },
                    function (data) {
                        cargarFiltros(data);
                    });
    });

    function cargarFiltros(filtros) {
        $('#ulFiltros').empty();
        $('#ulFiltros').append("<li><a href='javascript:cambiarFiltro(\"0\");'>Mostrar todo</a><br/></li>");

        $.each(filtros, function () {
            $('#ulFiltros').append("<li><a href='javascript:cambiarFiltro(\"" + this.Codigo + "\");'>" + this.Descripcion + "</a><br/></li>");
        });
    }

    function cambiarFiltro(valorFiltro) {

        var strFechaInicio = $('#hidFechaInicio').text();
        var strFechaFin = $('#hidFechaFin').text();

        if (strFechaInicio == "" && strFechaFin == "") {

            var date;
            if (fechaUltima == null) {
                date = $("#datepicker").datepicker('getDate');
            } else {
                date = new Date(fechaUltima.getFullYear(), fechaUltima.getMonth() + 1, 0);
            }

            var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);

            strFechaInicio = '01/' + getMonth(date) + '/' + date.getFullYear();
            strFechaFin = ultimoDia.getDate() + '/' + getMonth(date) + '/' + date.getFullYear();
        }

        cargarCalendario(strFechaInicio, strFechaFin, valorFiltro);
    }

    function marcarDiasEnCalendario(date) {
        var firstDay = $('#datepicker').datepicker('option', 'firstDay');

        if (date.getDate() == firstDay) {

            if (contador == 1) {
                contador = 0;
            } else {

                var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                var strFechaInicio = '01/' + getMonth(date) + '/' + date.getFullYear();
                var strFechaFin = ultimoDia.getDate() + '/' + getMonth(date) + '/' + date.getFullYear();

                dates = [];

                jQuery.getJSON(urlMetodos,
                    {
                        accion: "CargarFechasReunionesFiltro",
                        fechaInicio: strFechaInicio,
                        fechaFin: strFechaFin,
                        mostrarFechaVisitas: '<%= MostrarFechaVisitas %>',
                        codigoRol: codigoRol,
                        codigoUsuario: codigoUsuario
                    },
                    function (data) {
                        cargarFechaReuniones(data);
                    });

                    contador++;

                    fechaUltima = new Date(date.getFullYear(), date.getMonth() + 1, 0);

                //Limpiar datos campanha
                    $('#cboAnhios').val("");
                    $('#cboNumeros').val("");
                    $('#hidFechaInicio').empty();
                    $('#hidFechaFin').empty();
                }
            }

            for (var i = 0; i < dates.length; i++) {
                if ((dates[i].getDate() == date.getDate()) &&
                (dates[i].getMonth() == date.getMonth()) &&
                (dates[i].getFullYear() == date.getFullYear())) {
                    return [true, 'marcarEvento'];
                }
            }
            return [true, ''];
        }

        function cargarFechaReuniones(fechas) {
            $.each(fechas, function () {
                var fecha = new Date(parseInt(this.FechaInicio.slice(6, -2)));
                dates.push(fecha);
            });
        }

        function cargarCalendarioPorFecha(date) {

            var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            var strFechaInicio = '01/' + getMonth(date) + '/' + date.getFullYear();
            var strFechaFin = ultimoDia.getDate() + '/' + getMonth(date) + '/' + date.getFullYear();

            cargarCalendario(strFechaInicio, strFechaFin, '0');
        }

</script>
