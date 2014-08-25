function inicializarPagina() {
    jQuery.getJSON(urlMetodos,
        {
            accion: "CargarAnhos"
        },
        function (data) {
            $('#cboAnhios').html(data);
        });

    jQuery.getJSON(urlMetodos,
        {
            accion: "CargarAnhos2",
            prefijoIsoPais: prefijoIsoPais
        },
        function (json) {
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#cboAnhios2").append("<option value='" + json[i] + "' >" + json[i] + " </option>");
                }
            }
        });

    jQuery.getJSON(urlMetodos,
        {
            accion: "CargarAnhos3",
            prefijoIsoPais: prefijoIsoPais
        },
        function (result) {
            var json = eval("(" + result + ")");
            $.each(json, function (index, item) {
                $("#cboAnhios3").append("<option value='" + item + "' >" + item + " </option>");
            });
        });
}