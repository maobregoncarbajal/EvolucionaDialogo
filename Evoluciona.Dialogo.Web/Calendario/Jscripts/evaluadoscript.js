var dates = [];

function inicializarPagina() {
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
        onChangeMonthYear: function (year, month, inst) {
            var date = new Date(year, month, 0);
            var ultimoDia = new Date(year, month, 0);
            var strFechaInicio = '01/' + getMonth(date) + '/' + date.getFullYear();
            var strFechaFin = ultimoDia.getDate() + '/' + getMonth(date) + '/' + date.getFullYear();
            cargarCalendario(strFechaInicio, strFechaFin);
        }
    });

    jQuery.getJSON(urlMetodos,
        {
            accion: "CargarEvaluados",
            prefijoIsoPais: prefijoIsoPais,
            codigoUsuario: codigoUsuario,
            codigoRol: codigoRol
        },
        function (data) {
            $('#cboEvaluado').html(data);
            document.getElementById('cboEvaluado').selectedIndex = -1;
        });

    $("#cboEvaluado").change(function () {
        var date = $('#datepicker').datepicker('getDate');
        var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        var strFechaInicio = '01/' + getMonth(date) + '/' + date.getFullYear();
        var strFechaFin = ultimoDia.getDate() + '/' + getMonth(date) + '/' + date.getFullYear();

        cargarCalendario(strFechaInicio, strFechaFin);
        $("#datepicker").datepicker("refresh");
    });

    $('#showEventsDialog').dialog({
        autoOpen: false,
        width: 350,
        buttons: {
            "Cerrar": function () {
                $(this).dialog("close");
            }
        }
    });
}

var contador = 0;
function marcarDiasEnCalendario(date) {
    if ($('#cboEvaluado').val() == "")
        return [true, ''];

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
                    accion: "CargarFechasReuniones",
                    idEvaluado: $('#cboEvaluado').val(),
                    fechaInicio: strFechaInicio,
                    fechaFin: strFechaFin,
                    codigoRol: codigoRol,
                    prefijoIsoPais: prefijoIsoPais
                },
                function (data) {
                    cargarFechaReuniones(data);
                });

            //cargarCalendario(strFechaInicio, strFechaFin);
            contador++;
        }
    }

    for (var i = 0; i < dates.length; i++) {
        if ((dates[i].getDate() == date.getDate()) &&
            (dates[i].getMonth() == date.getMonth()) &&
            (dates[i].getFullYear() == date.getFullYear())) {
            return [true, 'ui-state-active-c'];
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

function mostrarEventos(event, element) {
    var fecha = getDay(event.start) + '/' + getMonth(event.start) + '/' + event.start.getFullYear();

    if (event.id.substring(0, 1) == '_') {
        //Es una Visita

        jQuery.getJSON(urlMetodos,
            {
                accion: "MostrarDetalleVisitasEval",
                id: event.id.substring(1),
                fecha: fecha,
                idEvaluado: $('#cboEvaluado').val(),
                codigoRol: codigoRol,
                prefijoIsoPais: prefijoIsoPais
            },
            function (data) {
                cargarDetalleVisitas(data);
            });
    } else {
        //Es un Evento

        jQuery.getJSON(urlMetodos,
            {
                accion: "MostrarDetalleEventosEval",
                id: event.id,
                idEvaluado: $('#cboEvaluado').val()
            },
            function (data) {
                cargarDetalleEventos(data);
            });
    }

    $('#showEventsDialog').dialog('open');
}

function cargarDetalleVisitas(eventos) {
    $('#tablaEventos').empty();

    $.each(eventos, function () {
        var fecha = new Date(parseInt(this.FechaInicio.slice(6, -2)));
        var strFecha = getDay(fecha) + '/' + getMonth(fecha) + '/' + fecha.getFullYear();

        $('#tablaEventos').append("<tr><td><table style='width: 100%'>" +
                                  "<tr style='height:18px;'><td align='right'>Asunto :</td><td style='padding-left:5px; font-weight: bold'>" + this.Asunto + "</td></tr>" +
                                  "<tr style='height:18px;'><td align='right'>Fecha :</td><td style='padding-left:5px;'>" + strFecha + "</td></tr>" +
                                  "<tr style='height:18px;'><td align='right'>Descripción :</td><td style='padding-left:5px;'>" + this.Descripcion + "</td></tr>" +
                                  "</table></td></tr>");

        $('#tablaEventos').append("<tr><td><br /><hr style='background-color: #CCC' /><br /></td></tr>");
    });
}

function cargarDetalleEventos(eventos) {
    $('#tablaEventos').empty();

    $.each(eventos, function () {
        var fecha = new Date(parseInt(this.FechaInicio.slice(6, -2)));
        var strFecha = getDay(fecha) + '/' + getMonth(fecha) + '/' + fecha.getFullYear() + ' ' + getHours(fecha) + ':' + getMinutes(fecha);

        $('#tablaEventos').append("<tr><td><table style='width: 100%'>" +
                                  "<tr style='height:18px;'><td align='right'>Asunto :</td><td style='padding-left:5px; font-weight: bold'>" + this.Asunto + "</td></tr>" +
                                  "<tr style='height:18px;'><td align='right'>Fecha :</td><td style='padding-left:5px;'>" + strFecha + "</td></tr>" +
                                  "<tr style='height:18px;'><td align='right'>Descripción :</td><td style='padding-left:5px;'>" + this.Descripcion + "</td></tr>" +
                                  "</table></td></tr>");

        $('#tablaEventos').append("<tr><td><br /><hr style='background-color: #CCC' /><br /></td></tr>");
    });
}

function cargarCalendario(fechaInicio, fechaFin) {
    $('#calendar').fullCalendar('destroy');

    $('#calendar').fullCalendar({
        defaultView: 'month',
        header: { left: '', center: '', right: 'title' },
        eventClick: mostrarEventos,
        selectable: true,
        events: function (start, end, callback) {
            jQuery.getJSON(urlEvaluados,
            {
                codEvaluado: $('#cboEvaluado').val(),
                start: fechaInicio,
                end: fechaFin,
                codigoRol: codigoRol,
                prefijoIsoPais: prefijoIsoPais
            },
            function (data) {
                var result = eval('(' + data + ')');
                callback(result);
            });
        }
    });

    var mesString = fechaInicio.substr(3, 2);
    var mes = parseInt(mesString, 10);

    $('#calendar').fullCalendar('gotoDate', fechaInicio.substr(6, 4), (mes - 1));

    $('.fc-event-time').css('display', 'none');
    $('.fc-event-inner').css('text-align', 'center');
    $('.fc-event-inner').css('padding', '4px 0px 4px 0px');
}

function getDay(fecha) {
    var day = fecha.getDate();
    day = day < 10 ? ('0' + day) : day;
    return day;
}

function getMonth(fecha) {
    var month = fecha.getMonth() + 1;
    month = month < 10 ? ('0' + month) : month;
    return month;
}

function getHours(fecha) {
    var hour = fecha.getHours();
    hour = hour < 10 ? ('0' + hour) : hour;
    return hour;
}

function getMinutes(fecha) {
    var minute = fecha.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;
    return minute;
}