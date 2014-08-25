Evoluciona = {
    Ajax: function(url, parameters, async) {
        var rsp = '';
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            cache: false,
            responseType: "json",
            data: parameters,
            success: function(response) {

                rsp = response;
            },
            failure: function() {

                rsp = -1;
            },
            error: function(request) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
        return rsp;
    },
    CreateDialogs: function(arrayDialog, aspnetForm) {
        for (var i = 0; i < arrayDialog.length; i++) {
            $("#" + arrayDialog[i].name).dialog({
                autoOpen: false,
                resizable: false,
                height: arrayDialog[i].height,
                width: arrayDialog[i].width,
                title: arrayDialog[i].title,
                modal: true,
                open: function() {
                    //$(this).parent().appendTo($('#aspnetForm'));
                    $(this).parent().appendTo($(aspnetForm));
                }
            });
        }
    },
    ToolTipText: function() {
        //agregar tooltip  a los dropdownlist
        $("select").each(function() {
            var i;
            var s = this;

            for (i = 0; i < s.length; i++)
                s.options[i].title = s.options[i].text;
            if (s.selectedIndex > -1)
                s.onmousemove = function() {
                    if (s.options[s.selectedIndex] != null)
                        s.title = s.options[s.selectedIndex].text;
                };
        });
    },
    ShowAlert: function(dialog, mensaje) {
        if (dialog == '') {
            dialog = 'dialog-alert';
        }

        $('#' + dialog).html("");
        $('#' + dialog).append("<br/>" + mensaje);
        $('#' + dialog).dialog("open");
    },
    LoadDropDownList: function(name, url, parameters, selected, isValIndex, async) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option("");
        combo.selectedIndex = 0;

        $('#' + name).ajaxError(function() {
            combo.options[0] = new Option("Error al cargar.");
        });
        $.ajax({
            url: url,
            async: async,
            cache: false,
            responseType: "json",
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(items) {
                $.each(items, function(index, item) {
                    combo.options[index] = new Option(item.Descripcion, item.Codigo);
                });
                if (selected == undefined) selected = 0;

                if (isValIndex) {
                    $('#' + name).attr('selectedIndex', selected);
                } else {
                    $('#' + name).val(selected);
                }
            },
            error: function(request) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
    },
    LoadDropDownListAux: function(name, url, parameters, text, value, async) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option(text, value);
        combo.selectedIndex = 0;

        $('#' + name).ajaxError(function() {
            combo.options[0] = new Option("Error al cargar.");
        });
        $.ajax({
            url: url,
            async: async,
            cache: false,
            responseType: "json",
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(items) {
                $.each(items, function(index, item) {
                    combo.options[index + 1] = new Option(item.Descripcion, item.Codigo);
                });

                $('#' + name).val(value);
            },
            error: function(request) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
    },
    SetFormatCalendar: function(idioma) {
        if (idioma == "es") {
            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '&#x3c;Ant',
                nextText: 'Sig&#x3e;',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['es']);
        }
    },
    PrefijoIsoPais: function(nombre) {
        var codPais;

        switch (nombre) {
        case "ARGENTINA":
            codPais = "AR";
            break;
        case "BOLIVIA":
            codPais = "BO";
            break;
        case "CHILE":
            codPais = "CL";
            break;
        case "COLOMBIA":
            codPais = "CO";
            break;
        case "COSTARICA":
            codPais = "CR";
            break;
        case "ECUADOR":
            codPais = "EC";
            break;
        case "EL SALVADOR":
            codPais = "";
            break;
        case "GUATEMALA":
            codPais = "GT";
            break;
        case "MEXICO":
            codPais = "MX";
            break;
        case "PANAMA":
            codPais = "PA";
            break;
        case "PERU":
            codPais = "PE";
            break;
        case "PUERTORICO":
            codPais = "PR";
            break;
        case "REP.DOMINICANA":
            codPais = "DO";
            break;
        case "VENEZUELA":
            codPais = "VE";
            break;
        default:
            codPais = "";
        }

        return codPais;
    }
}
    