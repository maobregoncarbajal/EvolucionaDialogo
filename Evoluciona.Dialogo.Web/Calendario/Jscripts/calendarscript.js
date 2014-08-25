var currentUpdateEvent;
var opcionSeleccionar = '<option value="0">-- Seleccionar --</option>';

function inicializarPagina() {
    $('.datepicker').datepicker({
        showOn: "button",
        buttonImage: "../Images/cal.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        firstDay: 1,
        isRTL: false,
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
        onSelect: function (dateText) {
            if (!$.isEmptyObject(this.value)) {
                cargarCampanhas(this.id);
            }
        }
    });

    var opcionesHoras = '';
    for (var i = 0; i < 24; i++) {
        if (i < 10)
            opcionesHoras += '<option value="0' + i + '">0' + i + '</option>';
        else
            opcionesHoras += '<option value="' + i + '">' + i + '</option>';
    }
    $('.cboHoras').html(opcionesHoras);

    var opcionesMinutos = '';
    for (var k = 0; k < 60; k = k + 5) {
        if (k < 10)
            opcionesMinutos += '<option value="0' + k + '">0' + k + '</option>';
        else
            opcionesMinutos += '<option value="' + k + '">' + k + '</option>';
    }
    $('.cboMinutos').html(opcionesMinutos);

    $("#cboCampanha").html(opcionSeleccionar);
    $("#cboCampanha").val(-1);

    $("#cboReunion").change(function () {
        jQuery.getJSON(urlMetodos,
        {
            accion: "CargarTiposEvento",
            reunion: $("#cboReunion").val(),
            codigoRol: codigoRol
        },
        function (data) {
            data = opcionSeleccionar + data;
            $('#cboEvento').html(data);
        });

        cargarEvaluador($("#cboReunion").val());
        $("#cboSubEvento").html(opcionSeleccionar);
    });

    $("#cboEvento").change(function () {
        jQuery.getJSON(urlMetodos,
        {
            accion: "CargarSubEvento",
            tipoEvento: $("#cboEvento").val()
        },
        function (data) {
            data = opcionSeleccionar + data;
            $('#cboSubEvento').html(data);
        });
    });

    $("#cboReunionM").change(function () {
        jQuery.getJSON(urlMetodos,
        {
            accion: "CargarTiposEvento",
            reunion: $("#cboReunionM").val(),
            codigoRol: codigoRol
        },
        function (data) {
            data = opcionSeleccionar + data;
            $('#cboEventoM').html(data);
        });
        cargarEvaluador($("#cboReunionM").val());
        $("#cboSubEventoM").html(opcionSeleccionar);
    });

    $("#cboEventoM").change(function () {
        jQuery.getJSON(urlMetodos,
        {
            accion: "CargarSubEvento",
            tipoEvento: $("#cboEventoM").val()
        },
        function (data) {
            data = opcionSeleccionar + data;
            $('#cboSubEventoM').html(data);
        });
    });

    cargarEvaluador(0);

    $('#btnNuevoEvento').click(function (evt) {
        limpirFormulario();
        $('#addDialog').dialog('open');
        return false;
    });

    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "Modificar": function () {
                var mensajeValidacion = validarFormulario("validarActualizar");

                if (mensajeValidacion == "") {
                    jQuery.getJSON(urlMetodos,
                        {
                            accion: "ActualizarEvento",
                            IDEvento: $("#txtIDEvento").val(),
                            Asunto: $("#txtAsuntoM").val(),
                            Fecha: $("#txtFechaM").val() + " " + $("#cboHoraM").val() + ":" + $("#cboMinutoM").val() + ":00",
                            Campanha: $("#cboCampanhaM").val(),
                            Reunion: $("#cboReunionM").val(),
                            Evento: $("#cboEventoM").val(),
                            SubEvento: $("#cboSubEventoM").val(),
                            Evaluado: $("#cboEvaluadoM").val()
                        },
                        function (data) {
                            updateSuccess(data);
                        });

                    $(this).dialog("close");
                    limpirFormulario();
                } else {
                    alert(mensajeValidacion);
                }
            },
            "Eliminar": function () {
                if (confirm("¿Ud. esta seguro de eliminar este evento?")) {
                    var idEvent = $("#txtIDEvento").val();

                    jQuery.getJSON(urlMetodos,
                        {
                            accion: "EliminarEvento",
                            id: idEvent
                        },
                        function (data) {
                            deleteSuccess(data);
                        });

                    $(this).dialog("close");
                    $('#calendar').fullCalendar('removeEvents', idEvent);
                    limpirFormulario();
                }
            }
        }
    });

    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "Registrar": function () {
                var mensajeValidacion = validarFormulario("validarNuevo");

                if (mensajeValidacion == "") {
                    jQuery.getJSON(urlMetodos,
                        {
                            accion: "AgregarEvento",
                            Asunto: $("#txtAsunto").val(),
                            Fecha: $("#txtFecha").val() + " " + $("#cboHora").val() + ":" + $("#cboMinuto").val() + ":00",
                            Campanha: $("#cboCampanha").val(),
                            Reunion: $("#cboReunion").val(),
                            Evento: $("#cboEvento").val(),
                            SubEvento: $("#cboSubEvento").val(),
                            Evaluado: $("#cboEvaluado").val(),
                            codigoUsuario: codigoUsuario,
                            codigoRol: codigoRol
                        },
                        function (data) {
                            addSuccess(data);
                        });

                    $(this).dialog("close");
                    limpirFormulario();
                } else {
                    alert(mensajeValidacion);
                }
            }
        }
    });

    var fechaActual = new Date();
    var ultimoDia = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0);
    var strFechaInicio = '01/' + getMonth(fechaActual) + '/' + fechaActual.getFullYear();
    var strFechaFin = ultimoDia.getDate() + '/' + getMonth(fechaActual) + '/' + fechaActual.getFullYear();

    cargarCalendario(strFechaInicio, strFechaFin, '0');
}

function loadEvent(event) {
    if (event == null) return;

    var fecha = new Date(parseInt(event.FechaInicio.slice(6, -2)));
    var day = getDay(fecha);
    var month = getMonth(fecha);
    var year = fecha.getFullYear();
    var hour = getHours(fecha);
    var minute = getMinutes(fecha);

    $("#txtIDEvento").val(event.IDEvento);
    $("#txtAsuntoM").val(event.Asunto);
    $("#txtFechaM").val(day + '/' + month + '/' + year);
    $("#cboHoraM").val(hour);
    $("#cboMinutoM").val(minute);
    cargarCampanhas("txtFechaM");
    $("#cboCampanhaM").val(event.Campanha);
    $("#cboReunionM").val(event.Reunion);
    $("#cboReunionM").trigger('change');
    $("#cboEventoM").val(event.Evento);
    $("#cboEventoM").trigger('change');
    $("#cboSubEventoM").val(event.SubEvento);
    $("#cboEvaluadoM").val(event.Evaluado);
}

function updateEvent(event, element) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    $('#updatedialog').dialog('open');

    currentUpdateEvent = event;

    jQuery.getJSON(urlMetodos,
        {
            accion: "ObtenerEvento",
            id: event.id
        },
        function (data) {
            loadEvent(data);
        });
}

function updateSuccess(updateResult) {
    if (updateResult.Success = true) {
        var fechaInicio = new Date(parseInt(updateResult.Data.FechaInicio.slice(6, -2)));
        var fechaFin = new Date(parseInt(updateResult.Data.FechaInicio.slice(6, -2)));
        fechaFin.setHours(fechaFin.getHours() + 1);

        if (currentUpdateEvent != null) {
            currentUpdateEvent.title = updateResult.Data.Descripcion;
            currentUpdateEvent.start = fechaInicio;
            currentUpdateEvent.end = fechaFin;
            currentUpdateEvent.description = updateResult.Data.Descripcion;

            $('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
        }
    }
    alert(updateResult.Message);
}

function deleteSuccess(deleteResult) {
    alert(deleteResult.Message);
}

function addSuccess(addResult) {
    if (addResult.Success = true) {
        var fechaInicio = new Date(parseInt(addResult.Data.FechaInicio.slice(6, -2)));
        var fechaFin = new Date(parseInt(addResult.Data.FechaInicio.slice(6, -2)));
        fechaFin.setHours(fechaFin.getHours() + 1);

        var event = {
            title: addResult.Data.Descripcion,
            start: fechaInicio,
            end: fechaFin,
            id: addResult.Data.IDEvento,
            description: addResult.Data.Descripcion,
            allDay: false
        };

        $('#calendar').fullCalendar('renderEvent', event, true);
        $('#calendar').fullCalendar('unselect');

        alert("Se registró el Evento satisfactoriamente.");
    }
}

function selectDate(start, end, allDay) {
    limpirFormulario();
    $('#addDialog').dialog('open');

    var day = getDay(start);
    var month = getMonth(start);
    var year = start.getFullYear();
    var hour = getHours(start);
    var minute = getMinutes(start);

    $("#txtFecha").val(day + "/" + month + "/" + year);
    $("#cboHora").val(hour);
    $("#cboMinuto").val(minute);
    cargarCampanhas("txtFecha");
}

function cargarCampanhas(txtName) {
    if (txtName == "txtFecha") {
        jQuery.getJSON(urlMetodos,
        {
            accion: 'CargarCampanhas',
            fecha: $('#' + txtName).val(),
            prefijoIsoPais: prefijoIsoPais,
            codigoRol: codigoRol,
            codigoUsuario: codigoUsuario
        },
        function (data) {
            data = opcionSeleccionar + data;
            $('#cboCampanha').html(data);
        });
    } else if (txtName == "txtFechaM") {
        jQuery.getJSON(urlMetodos,
        {
            accion: 'CargarCampanhas',
            fecha: $('#' + txtName).val(),
            prefijoIsoPais: prefijoIsoPais,
            codigoRol: codigoRol,
            codigoUsuario: codigoUsuario
        },
        function (data) {
            data = opcionSeleccionar + data;
            $('#cboCampanhaM').html(data);
        });
    }
}

function seleccionarFecha(dateText, inst) {
    var fechaActual = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
    var strFechaInicio, strFechaFin;

    switch (view) {
        case 'month':
            break;
        case 'agendaWeek':
            var fechaInicio = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());
            var fechaFin = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());

            fechaInicio.setDate(fechaInicio.getDate() - fechaInicio.getDay());
            fechaFin.setDate(fechaFin.getDate() + (6 - fechaFin.getDay()));

            strFechaInicio = getDay(fechaInicio) + '/' + getMonth(fechaInicio) + '/' + fechaInicio.getFullYear();
            strFechaFin = getDay(fechaFin) + '/' + getMonth(fechaFin) + '/' + fechaFin.getFullYear();

            cargarCalendario(strFechaInicio, strFechaFin, '0');
            break;
        case 'agendaDay':
            strFechaInicio = getDay(fechaActual) + '/' + getMonth(fechaActual) + '/' + fechaActual.getFullYear();
            fechaActual.setDate(fechaActual.getDate() + 1);
            strFechaFin = getDay(fechaActual) + '/' + getMonth(fechaActual) + '/' + fechaActual.getFullYear();

            cargarCalendario(strFechaInicio, strFechaFin, '0');
            break;
    }
}

function cargarCalendario(fechaInicio, fechaFin, filtro) {
    $('#calendar').fullCalendar('destroy');

    $('#calendar').fullCalendar({
        defaultView: view,
        header: { left: '', center: '', right: 'title' },
        eventClick: updateEvent,
        selectable: true,
        select: selectDate,
        editable: true,
        events: function (start, end, callback) {
            if ($.isEmptyObject(fechaInicio) && $.isEmptyObject(fechaFin)) {
                fechaInicio = getDay(start) + '/' + getMonth(start) + '/' + start.getFullYear();
                fechaFin = getDay(end) + '/' + getMonth(end) + '/' + end.getFullYear();
            }

            jQuery.getJSON(urlCalendario,
                {
                    start: fechaInicio,
                    end: fechaFin,
                    codigoUsuario: codigoUsuario
                },
                function (data) {
                    var result = eval('(' + data + ')');
                    callback(result);
                });
        }
    });

    switch (view) {
        case 'month':
            var mesString1 = fechaInicio.substr(3, 2);
            var mes1 = parseInt(mesString1, 10);

            $('#calendar').fullCalendar('gotoDate', fechaInicio.substr(6, 4), (mes1 - 1));
            break;
        case 'agendaWeek':
        case 'agendaDay':
            var mesString2 = fechaInicio.substr(3, 2);
            var mes2 = parseInt(mesString2, 10);

            var diaString = fechaInicio.substr(0, 2);
            var dia = parseInt(diaString, 10);

            $('#calendar').fullCalendar('gotoDate', fechaInicio.substr(6, 4), (mes2 - 1), dia);
            break;
    }
    $('.fc-event-time').css('display', 'none');
}

function validarFormulario(accion) {
    var mensajeErrores = "";
    var mensajeFecha = "Debe ingresar la fecha.\n";
    var mensajeCampanha = "Debe seleccionar la Campaña.\n";
    var mensajeAsunto = "Debe ingresar el Asunto.\n";
    var mensajeReunion = "Debe seleccionar un tipo de Reunión.\n";
    var mensajeEvaluado = "Debe seleccionar el Evaluado.\n";

    if (accion == "validarNuevo") {
        if ($("#txtFecha").val() == "") {
            mensajeErrores += mensajeFecha;
        }
        if ($("#cboCampanha").val() == 0) {
            mensajeErrores += mensajeCampanha;
        }
        if ($("#cboReunion").val() == 0) {
            mensajeErrores += mensajeReunion;
        }
        if ($("#cboReunion").val() == 1 && $("#cboEvaluado").val() == 0) {
            mensajeErrores += mensajeEvaluado;
        }
        if ($("#txtAsunto").val() == "") {
            mensajeErrores += mensajeAsunto;
        }
    } else if (accion == "validarActualizar") {
        if ($("#txtFechaM").val() == "") {
            mensajeErrores += mensajeFecha;
        }
        if ($("#cboCampanhaM").val() == 0) {
            mensajeErrores += mensajeCampanha;
        }
        if ($("#cboReunionM").val() == 0) {
            mensajeErrores += mensajeReunion;
        }
        if ($("#cboReunionM").val() == 1 && $("#cboEvaluadoM").val() == 0) {
            mensajeErrores += mensajeEvaluado;
        }
        if ($("#txtAsuntoM").val() == "") {
            mensajeErrores += mensajeAsunto;
        }
    }

    return mensajeErrores;
}

function limpirFormulario() {
    $("#txtIDEvento").val(0);
    $(".datepicker").val("");
    $(".asuntoEvento").val("");
    $('.cboHoras').val("00");
    $('.cboMinutos').val("00");
    $('.comboCampanha').val("");
    $('.comboReunion').val(0);
    $('.comboEvento').val(0);
    $('.comboSubEvento').val(0);
    $('.comboEvaluado').val(0);
}

function cargarEvaluador(tipoReunion) {
    switch (rolEvaluador) {
        case rolDirectorVentas:
            $('.evaluado').text('Gerente de Región');

            jQuery.getJSON(urlMetodos,
                {
                    accion: 'CargarGerentesRegion',
                    prefijoIsoPais: prefijoIsoPais
                },
                function (data) {
                    data = opcionSeleccionar + data;
                    $('#cboEvaluado').html(data);
                });
            break;
        case rolGerenteRegion:
            $('.evaluado').text('Gerente de Zona');

            jQuery.getJSON(urlMetodos,
                {
                    accion: 'CargarGerentesZona',
                    prefijoIsoPais: prefijoIsoPais,
                    codigoUsuario: codigoUsuario
                },
                function (data) {
                    data = opcionSeleccionar + data;
                    $('#cboEvaluado').html(data);
                });
            break;
        case rolGerenteZona:
            $('.evaluado').text('Lider');

            jQuery.getJSON(urlMetodos,
                {
                    accion: 'CargarLideres',
                    codigoUsuario: codigoUsuario,
                    prefijoIsoPais: prefijoIsoPais
                },
                function (data) {
                    data = opcionSeleccionar + data;
                    $('#cboEvaluado').html(data);
                });
            break;
    }

    $('#cboEvaluadoM').html($('#cboEvaluado').html());

    //Verificamos si es una reunion Individual
    if (tipoReunion == 1) {
        $('.comboEvaluado').removeAttr('disabled');
    } else {
        $('.comboEvaluado').attr('disabled', 'disabled');
        $('.comboEvaluado').val(0);
    }
}