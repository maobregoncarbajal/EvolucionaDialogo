Encuesta = {
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
            failure: function(msg) {

                rsp = -1;
            },
            error: function(request, status, error) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
        return rsp;
    },
    CreateDialogs: function(arrayDialog) {
        for (var i = 0; i < arrayDialog.length; i++) {
            $("#" + arrayDialog[i].name).dialog({
                autoOpen: false,
                resizable: false,
                height: arrayDialog[i].height,
                width: arrayDialog[i].width,
                title: arrayDialog[i].title,
                modal: true,
                open: function() {
                    $(this).parent().appendTo($('#aspnetForm'));
                }
            });
        }
    },
    ToolTipText: function() {
        // agregar tooltip  a los dropdownlist
//        $("select").each(function() {
//            var i = 0;
//            var s = this;

//            for (i = 0; i < s.length; i++)
//                s.options[i].title = s.options[i].text;
//            if (s.selectedIndex > -1)
//                s.onmousemove = function() {
//                     if (s.options[s.selectedIndex]!= null)      
//                     s.title = s.options[s.selectedIndex].text;
//                };
//        });
    }
    ,
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

        $('#' + name).ajaxError(function(event, request, settings) {
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
            error: function(request, status, error) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
    },
    LoadDropDownListAux: function(name, url, parameters, text,value, async) {
        var combo = document.getElementById(name);
        combo.options.length = 0;
        combo.options[0] = new Option(text,value);
        combo.selectedIndex = 0;

        $('#' + name).ajaxError(function(event, request, settings) {
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
                    combo.options[index+1] = new Option(item.Descripcion, item.Codigo);
                });

                $('#' + name).val(value);
            },
            error: function(request, status, error) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
    }
    
    
}