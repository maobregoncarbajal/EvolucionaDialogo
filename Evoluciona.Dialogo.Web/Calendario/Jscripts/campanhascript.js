function inicializarPagina() {
    jQuery.getJSON(urlMetodos,
        {
            accion: "CargarAnhos",
            prefijoIsoPais: prefijoIsoPais
        },
        function (data) {
            $('#cboAnhios').html(data);
            document.getElementById('cboAnhios').selectedIndex = -1;
        });

    $("#cboAnhios").change(function () {
        jQuery.getJSON(urlMetodos,
        {
            accion: "CargarNumerosCampanha",
            anho: $("#cboAnhios").val(),
            prefijoIsoPais: prefijoIsoPais
        },
        function (data) {
            $('#cboNumeros').html(data);
            document.getElementById('cboNumeros').selectedIndex = -1;
        });

        $('#hidFechaInicio').empty();
        $('#hidFechaFin').empty();
    });

    $("#cboNumeros").change(function () {
        jQuery.getJSON(urlMetodos,
        {
            accion: "CargarRangoFechas",
            anho: $("#cboAnhios").val(),
            numero: $("#cboNumeros").val(),
            prefijoIsoPais: prefijoIsoPais,
            codigoRol: codigoRol,
            codigoUsuario: codigoUsuario
        },
        function (data) {
            cargarRangoFechas(data);
        });
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

    var today = new Date();
    cargarCalendarioPorFecha(today);
}

function cargarRangoFechas(obj) {
    var fechaInicio = new Date(parseInt(obj.FechaInicio.slice(6, -2)));
    var fechaFin = new Date(parseInt(obj.FechaFin.slice(6, -2)));

    var strFechaInicio = getDay(fechaInicio) + '/' + getMonth(fechaInicio) + '/' + fechaInicio.getFullYear();
    var strFechaFin = getDay(fechaFin) + '/' + getMonth(fechaFin) + '/' + fechaFin.getFullYear();

    $('#hidFechaInicio').text(strFechaInicio);
    $('#hidFechaFin').text(strFechaFin);

    cargarCalendario(strFechaInicio, strFechaFin, '0');
}

function mostrarEventos(event, element) {
    var fecha = getDay(event.start) + '/' + getMonth(event.start) + '/' + event.start.getFullYear();

    if (event.id.substring(0, 1) == '_') {
        //Es una Visita

        jQuery.getJSON(urlMetodos,
        {
            accion: "MostrarDetalleVisitas",
            id: event.id.substring(1),
            fecha: fecha,
            codigoUsuario: codigoUsuario,
            codigoRol: codigoRol
        },
        function (data) {
            cargarDetalleVisitas(data);
        });
    } else {
        //Es un Evento

        jQuery.getJSON(urlMetodos,
        {
            accion: "MostrarDetalleEventos",
            id: event.id
        },
        function (data) {
            cargarDetalleVisitas(data);
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

function cargarCalendario(fechaInicio, fechaFin, filtro) {
    $('#calendar').fullCalendar('destroy');

    $('#calendar').fullCalendar({
        defaultView: 'month',
        header: { left: '', center: '', right: 'title' },
        eventClick: mostrarEventos,
        selectable: true,
        events: function (start, end, callback) {
            jQuery.getJSON(urlCampanha,
                {
                    codFiltro: filtro,
                    start: fechaInicio,
                    end: fechaFin,
                    codigoRol: codigoRol,
                    codigoUsuario: codigoUsuario
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

function seleccionarFecha() {
}