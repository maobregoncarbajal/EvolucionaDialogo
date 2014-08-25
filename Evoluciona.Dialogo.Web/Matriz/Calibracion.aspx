<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Calibracion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Matriz.Calibracion" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/MenuMatriz.ascx" TagName="MenuMatriz" TagPrefix="ucMenuMatriz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/colorboxAlt.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/Matriz.css" rel="stylesheet"
        type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/Validation.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/typography.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/TableSorter.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JSLINQ.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/json2.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.validate.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="contentma1">
        <div class="conte2ral">
            <ucMenuMatriz:MenuMatriz ID="menuMatrizLink" runat="server" />
        </div>
        <!--Inicio-->
        <div style="width: 100%; background: url(../Styles/Matriz/lienadentro.png)  repeat-x">
            <br />
            <div id="divMatrizDV">
                <div>
                    <table id="tblCalibracion" class="grillaAzulChica">
                        <tr>
                            <th style="width: 130px">Período Acuerdo
                            </th>
                            <th>Toma de acción
                            </th>
                            <th>Región
                            </th>
                            <th>Zona
                            </th>
                            <th>Gerente de Zona
                            </th>
                            <th>Región de Asignación
                            </th>
                            <th>Zona de Reasignación
                            </th>
                            <th>Campaña Inicio
                            </th>
                            <th>Campaña Fin
                            </th>
                            <th>Observaciones
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPeriodoAcuerdo" runat="server"></asp:Label>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlTomaAccion" name="ddlTomaAccion">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlRegion" name="ddlRegion">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlZona" name="ddlZona">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlGerenteZona" name="ddlGerenteZona">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlRegionReasignacion" name="ddlRegionReasignacion"
                                    disabled="disabled">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlZonaReasignacion" name="ddlZonaReasignacion">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlCampanhaInicio" name="ddlCampanhaInicio">
                                </select>
                            </td>
                            <td>
                                <select style="width: 120px" id="ddlCampanhaFin" name="ddlCampanhaFin">
                                </select>
                            </td>
                            <td>
                                <textarea rows="3" style="width: 160px" id="txtObservacion" name="txtObservacion"></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <input type="button" id="btnCancelar" value="Cancelar" class="btnSquareAzul" style="display: none" />
                                <input type="button" id="btnRegistrar" value="Registrar" class="btnSquareAzul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                &nbsp;<br />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <br />
            <div style="margin: 0px 0px 20px 0px; float: left; background: url(../Styles/Matriz/fnd_filtro.png) no-repeat; height: 40px; width: 920px"
                id="panelBuscador">
                <p class="labelCboSeleccion" style="margin-top: 6px; margin-left: -3px">
                    Periodo de Acuerdo:
                </p>
                <div style="float: left; margin-top: 1px; padding: 5px;">
                    <select id="ddlPeriodoAcuerdo" class="stiloborde" style="width: 70px; height: 22px">
                    </select>
                </div>
                <div style="float: left; margin-top: 4px; margin-left: 5px">
                    <div class="btnRedoneado">
                        <div class="btnRedoneado-outer">
                            <div class="btnRedoneado-inner">
                                <div class="btnRedoneado-into">
                                    <input type="button" id="btnBuscar" class="btn" value="Buscar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both;">
            </div>
            <div id="PanelDetalle">
                <table class="tablesorter" id="tblDetalle" cellspacing="0" rules="all" border="1">
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
        </div>
        <!--Fin-->
    </div>
    <div id="dialog-alert">
    </div>
    <div id="dialog-delete">
        <br />
        ¿Está seguro de realizar la operación?
    </div>
    <div id="dialog-confirm">
        <br />
        ¿Está seguro de realizar la operación?
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPaisEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lblCodigoEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lbIdlRolEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lblNombreEvaluador" runat="server"></asp:Label>
    </div>
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />

    <script type="text/javascript" language="javascript">
        //Variables
        //region Variables
        var periodoAcuerdo = '';
        var codPaisEvaluador = '';
        var idRolEvaluador = '';
        var codigoEvaluador = '';
        var nombreEvaluador = '';
        var codPaisEvaluado = '';
        var codRegionEvaluado = '';
        var codUsuarioEvaluado = '';
        var nombreEvaluado = '';
        var codZonaEvaluado = '';
        var pageBlocked = false;
        var isValidFechaAcuerdo = false;
        var campanhaActual = "";
        var dlgElement;
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var lista;
        var accion = "N";
        var idTomaAccionRef = "0";
        var idTomaAccionDelete = "0";

        $(document).ready(function () {

            $.validator.addMethod('selectNone',
                    function (value, element) {

                        if (element.value == '' || element.value == '00') {
                            return false;
                        }
                        else return true;
                    }, "*");

            $.validator.addMethod("compare", function (value, element) {
                return parseInt($("#ddlCampanhaFin").val()) > parseInt($("#ddlCampanhaInicio").val());
            }, "*");

            $("#aspnetForm").validate({
                rules: {
                    ddlTomaAccion: {
                        selectNone: true
                    },
                    ddlRegion: {
                        selectNone: true
                    },
                    ddlZona: {
                        selectNone: true
                    },
                    ddlGerenteZona: {
                        selectNone: true
                    },
                    ddlRegionReasignacion: {
                        selectNone: true
                    },
                    ddlZonaReasignacion: {
                        selectNone: true
                    },
                    ddlCampanhaInicio: {
                        required: true,
                        compare: true
                    },
                    ddlCampanhaFin: {
                        required: true,
                        compare: true
                    },
                    txtObservacion: {
                        required: true
                    }
                },
                messages: {
                    ddlTomaAccion: {
                        selectNone: "*"
                    },
                    ddlGerenteRegion: {
                        selectNone: "*"
                    },
                    ddlRegion: {
                        selectNone: "*"
                    },
                    ddlZona: {
                        selectNone: "*"
                    },
                    ddlRegionReasignacion: {
                        selectNone: "*"
                    },
                    ddlZonaReasignacion: {
                        selectNone: "*"
                    },
                    ddlGerenteZona: {
                        selectNone: "*"
                    },
                    ddlCampanhaInicio: {
                        compare: '*'
                    },
                    ddlCampanhaFin: {
                        compare: "debe ser mayor a la camp. de Incio"
                    },
                    txtObservacion: {
                        required: "*"
                    }
                }
            });

            $("#dialog-confirm").dialog({
                autoOpen: false,
                resizable: false,
                height: 200,
                width: 200,
                title: "Alerta",
                modal: true,
                buttons: {
                    "Aceptar": function () {
                        guardar();
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    //$(this).parent().appendTo($('#aspnetForm'));
                }
            });

            $("#dialog-delete").dialog({
                autoOpen: false,
                resizable: false,
                height: 200,
                width: 200,
                title: "Alerta",
                modal: true,
                buttons: {
                    "Aceptar": function () {
                        DeleteTomaAccion();
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    //$(this).parent().appendTo($('#aspnetForm'));
                }
            });

            $.ajaxSetup({ cache: false, async: false });

            dlgElement = jQuery("#imgExporting");
            $.blockUI.defaults.baseZ = 100000;
            $.blockUI.defaults.overlayCSS.opacity = 0.4;
            $.blockUI.defaults.css.backgroundColor = "transparent";
            $.blockUI.defaults.css.border = '0px none';
            $.blockUI.defaults.fadeIn = 100,

            $.blockUI.defaults.fadeOut = 100,

                $(document).ajaxStart(function () {

                    if (!pageBlocked) {
                        jQuery.blockUI({
                            message: dlgElement,
                            onBlock: function () {
                                pageBlocked = true;
                            }
                        });
                    }
                });

            $(document).ajaxStop(function () {
                jQuery.unblockUI();
                pageBlocked = false;
            });

            crearPopUp();
            inicializar();
            //EVENTOS

            $("#ddlRegion").change(function () {

                limpiarControles();

                var codigo = $(this).val();
                var array = codigo.split('-');

                if (array.length == "2") {// si existe pais y codigo de la region
                    codPaisEvaluado = array[0];
                    codRegionEvaluado = array[1];

                    MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluado, 'codRegion': codRegionEvaluado, 'select': 'select' }, 0, true, false);

                    MATRIZ.ToolTipText();
                } else {
                    codPaisEvaluado = '';
                    codRegionEvaluado = '';
                }
            });

            $("#btnRegistrar").click(function (evt) {

                var isValid = $("#aspnetForm").valid();
                var isValidReasig = true;
                var existeOperacion = false;

                if ($("#ddlTomaAccion").val() == "02") {
                    if ($("#ddlZonaReasignacion").val() == '') {
                        isValidReasig = false;
                    }
                }

                if (!isValidReasig) {
                    MATRIZ.ShowAlert('dialog-alert', 'Está haciendo una reasinación por lo tanto debe seleccionar una zona');
                }

                if (accion == "N") {
                    var parametros = { accion: 'validarTomaAccion', codPais: codPaisEvaluado, codUsuario: codUsuarioEvaluado, idRol: "3", periodo: periodoAcuerdo, estadoActivo: "-1" };

                    var estado = MATRIZ.Ajax(url, parametros, false);

                    if (estado == "1") {
                        MATRIZ.ShowAlert('', 'Ya se ha realizado una toma de acción a esta colaboradora en este periodo');
                        existeOperacion = true;
                    }
                }

                if (isValid && isValidReasig && !existeOperacion) {
                    $("#dialog-confirm").dialog("open");
                } else {
                    evt.preventDefault();
                }
            });

            $("#btnBuscar").click(function (evt) {

                if ($("#ddlPeriodoAcuerdo").val() != "00") {
                    listar($("#ddlPeriodoAcuerdo").val());
                } else {
                    MATRIZ.ShowAlert('', 'Debe seleccionar un periodo');
                }
            });

            $("#ddlRegionReasignacion").change(function () {

                var arraydata = $(this).val().split('-');

                if (arraydata.length == 2) {
                    MATRIZ.LoadDropDownList("ddlZonaReasignacion", url, { 'accion': 'obtenerZonasDisponibles', 'codPais': arraydata[0], 'codRegion': arraydata[1], 'periodo': periodoAcuerdo, estadoActivo: '-1' }, 0, true, false);
                }
            });

            $("#ddlTomaAccion").change(function () {

                if (accion != "E") {
                    $('#ddlRegion').empty();
                    limpiarControles();
                } else {
                    $("#ddlRegionReasignacion").empty();
                    $("#ddlZonaReasignacion").empty();
                }

                hideReasignacion(true);

                if ($(this).val() == "02")//Reasignación
                {
                    hideReasignacion(false);
                }

                if ($(this).val() == "01")//Plan Mejora
                {
                    hideCampanhas(false);
                    $("#ddlCampanhaInicio").show();
                    $("#ddlCampanhaFin").show();

                } else {
                    hideCampanhas(true);
                    $("#ddlCampanhaInicio").hide();
                    $("#ddlCampanhaFin").hide();
                }

                if ($(this).val() != "00" && accion == "N") {

                    MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regiones', 'codPais': codPaisEvaluador, 'select': 'select' }, 0, true, false);

                }

                MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { accion: 'regiones', codPais: codPaisEvaluador, select: 'select' }, 0, true, false);
            });

            $("#ddlZona").change(function () {
                $('#ddlGerenteZona').empty();
                MATRIZ.LoadDropDownList("ddlGerenteZona", url, { 'accion': 'loadAllGerentesZonaByZona', 'codPais': codPaisEvaluado, 'codRegion': codRegionEvaluado, 'codZona': $(this).val(), periodo: periodoAcuerdo }, 0, true, false);
                MATRIZ.ToolTipText();
            });

            $("#ddlGerenteZona").change(function () {
                codUsuarioEvaluado = $(this).val();
                nombreEvaluado = $("#ddlGerenteZona option:selected").text();
                campanhaActual = obtenerCampanahActual();

                if (campanhaActual != "" && campanhaActual != null) {
                    crearCampanhas(campanhaActual);
                }

                MATRIZ.ToolTipText();
            });

            $("#btnCancelar").click(function (evt) {
                codUsuarioEvaluado = "";
                codPaisEvaluado = "";
                idTomaAccionRef = "0";
                accion = "N";
                $("#btnCancelar").hide();
                $("#PanelDetalle").show();
                MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'select' }, 0, true, false);

                DisableControl(false);
                limpiarControles();
                $("#ddlRegion").empty();
                $("#ddlRegionReasignacion").empty();
                $("#aspnetForm").data('validator').resetForm();
            });
        });

        function inicializar() {

            codPaisEvaluador = $("#<%=lblPaisEvaluador.ClientID %>").html();
            idRolEvaluador = $("#<%=lbIdlRolEvaluador.ClientID %>").html();
            codigoEvaluador = $("#<%=lblCodigoEvaluador.ClientID %>").html();
            periodoAcuerdo = $("#<%=lblPeriodoAcuerdo.ClientID %>").html();
            nombreEvaluador = $("#<%=lblNombreEvaluador.ClientID %>").html();
            MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'select' }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlPeriodoAcuerdo", url, { accion: 'periodosAll', codPais: codPaisEvaluador, anho: '00', idRol: idRolEvaluador, normal: 'si', select: 'si' }, 0, true, false);
            MATRIZ.ToolTipText();

            hideReasignacion(true);
            hideCampanhas(true);

            isValidFechaAcuerdo = validarFechaAcuerdo();

            listar(periodoAcuerdo);
        }

        function crearPopUp() {

            var arrayDialog = [{ name: "dialog-alert", height: 150, width: 250, title: "Alerta" },
                               { name: "dialog-delete", height: 150, width: 250, title: "Alerta" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function guardar() {

            var entidad = new Object();

            if (accion == "E") {
                entidad.IdTomaAccionRef = idTomaAccionRef;
            }

            entidad.PrefijoIsoPaisEvaluador = codPaisEvaluador;
            entidad.PrefijoIsoPaisEvaluado = codPaisEvaluado;
            entidad.NombreEvaluador = nombreEvaluador;
            entidad.NombreEvaluado = nombreEvaluado;
            entidad.Periodo = periodoAcuerdo;
            entidad.IdRolEvaluador = idRolEvaluador;
            entidad.IdRolEvaluado = 3;
            entidad.CodEvaluador = codigoEvaluador;
            entidad.CodEvaluado = codUsuarioEvaluado;
            entidad.CodRegionActual = $("#ddlRegion").val().split('-')[1];
            entidad.CodZonaActual = $("#ddlZona").val();
            entidad.TomaAccion = $("#ddlTomaAccion").val();
            entidad.EstadoActivo = 0;

            if (entidad.TomaAccion == "02")//Reasignación
            {
                entidad.CodRegionReasignacion = $("#ddlRegionReasignacion").val().split('-')[1];
                entidad.CodZonaReasignacion = $("#ddlZonaReasignacion").val();
            }
            else {
                entidad.CodRegionReasignacion = "";
                entidad.CodZonaReasignacion = "";
            }

            if (entidad.TomaAccion == "01")//Plan de mejora
            {
                entidad.AnhoCampanhaInicio = $("#ddlCampanhaInicio").val();
                entidad.AnhoCampanhaFin = $("#ddlCampanhaFin").val();
            } else {
                entidad.AnhoCampanhaInicio = "";
                entidad.AnhoCampanhaFin = "";
            }

            entidad.Observaciones = $("#txtObservacion").val();

            var parametros = { accion: 'insertTomaAccion', entidad: JSON.stringify(entidad) };

            var id = MATRIZ.Ajax(url, parametros, false);

            if (id != "0" && id != "") {
                if (id == "2")
                    MATRIZ.ShowAlert('', '<div class="warningbox" > La operación se realizó pero no se llegó a enviar el correo</div>');

                if (id == "3")
                    MATRIZ.ShowAlert('', '<div class="successbox" > La operación se realizó con éxito </div>');

            } else {
                MATRIZ.ShowAlert('', '<div class="errormsgbox" > Ocurrió un error </div>');
            }

            //reiniciar
            $("#btnCancelar").trigger("click");
            listar(periodoAcuerdo);
        }

        function validarFechaAcuerdo() {

            var isValid = false;
            var parametros = { accion: 'validarFechaAcuerdo', codPais: codPaisEvaluador, periodo: periodoAcuerdo };

            var value = MATRIZ.Ajax(url, parametros, false);

            if (value == '1') {
                isValid = true;
            }

            if (!isValid) {

                $("#divMatrizDV").find('input, textarea, select').attr('disabled', true);
                $("#btnRegistrar").attr('disabled', true);

                if (value == "0") {
                    MATRIZ.ShowAlert('dialog-alert', 'El periodo de acuerdo ha terminado');
                } else {
                    MATRIZ.ShowAlert('dialog-alert', ' Esta opción no esta activa. Por favor active el cronograma correspondiente de este pais');
                }
            }
            else {
                $("#divMatrizDV").find('input, textarea, select').attr('disabled', false);
                $("#btnRegistrar").attr('disabled', false);
            }

            $("#ddlRegionReasignacion").attr('disabled', false);

            return isValid;
        }

        function obtenerCampanahActual() {

            var parametros = { accion: 'obtenerCampanhaActual', periodo: periodoAcuerdo, codPais: codPaisEvaluado, codUsuario: codUsuarioEvaluado, idRol: '3' };

            var value = MATRIZ.Ajax(url, parametros, false);

            return value;
        }

        function crearCampanhas(campanha) {
            $('#ddlCampanhaInicio').empty();
            $('#ddlCampanhaFin').empty();
            MATRIZ.LoadDropDownList("ddlCampanhaInicio", url, { 'accion': 'obtenerCampanhas', 'campanha': campanha }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlCampanhaFin", url, { 'accion': 'obtenerCampanhas', 'campanha': campanha }, 0, true, false);
        }

        function limpiarControles() {
            $('#ddlZona').empty();
            $('#ddlGerenteZona').empty();
            $("#ddlZonaReasignacion").empty();
            $("#ddlCampanhaInicio").empty();
            $("#ddlCampanhaFin").empty();
            $("#txtObservacion").val('');
        }

        function listar(paramPeriodoAcuerdo) {

            $("#PanelDetalle").hide();
            var parametros = { accion: 'listarTomaAccion', codPais: codPaisEvaluador, codEvaluador: codigoEvaluador, periodo: paramPeriodoAcuerdo, idRolEvaluador: idRolEvaluador, idRolEvaluado: '3', estadoActivo: '-1', codTomaAccion: '00' };

            lista = MATRIZ.Ajax(url, parametros, false);

            if (lista.length > 0) {
                $("#tblDetalle thead").html("");

                var titulo = "<tr><th> Período Acuerdo</th>";
                titulo = titulo + "<th> Toma de acción</th>";
                titulo = titulo + "<th> Región</th>";
                titulo = titulo + "<th> Zona</th>";
                titulo = titulo + "<th> Gerente de Zona</th>";
                titulo = titulo + "<th> Región de Asignación</th>";
                titulo = titulo + "<th> Zona de Reasignación</th>";
                titulo = titulo + "<th> Campaña Inicio</th>";
                titulo = titulo + "<th> Campaña Fin</th>";
                titulo = titulo + "<th>Observaciones</th><th></th><th></th></tr>";

                $(titulo).appendTo("#tblDetalle thead");

                var tabla = "";
                $.each(lista, function (i, v) {
                    tabla = tabla + "<tr><td>" + v.Periodo + "</td>";
                    tabla = tabla + "<td>" + v.NombreTomaAccion + "</td>";
                    tabla = tabla + "<td>" + v.NombreRegionActual + "</td>";
                    tabla = tabla + "<td>" + v.NombreZonaActual + "</td>";
                    tabla = tabla + "<td>" + v.NombreEvaluado + "</td>";
                    tabla = tabla + "<td>" + v.NombreRegionReasignacion + "</td>";
                    tabla = tabla + "<td>" + v.NombreZonaReasignacion + "</td>";
                    tabla = tabla + "<td>" + v.AnhoCampanhaInicio + "</td>";
                    tabla = tabla + "<td>" + v.AnhoCampanhaFin + "</td>";

                    if (v.Observaciones.length > 30) {
                        tabla = tabla + "<td>" + v.Observaciones.substring(0, 30) + "...</td>";
                    } else {
                        tabla = tabla + "<td>" + v.Observaciones + "</td>";
                    }

                    tabla = tabla + "<td><img src='<%=Utils.AbsoluteWebRoot%>Styles/Matriz/btntabla_editar.png' title='Editar' class='EditRow' style='cursor:pointer'id='btnEdit-" + v.IdTomaAccion + "'/></td><td><img src='<%=Utils.AbsoluteWebRoot%>Styles/Matriz/Delete-icon.png' title='Eliminar' class='DelRow' style='cursor:pointer'id='btnDel-" + v.IdTomaAccion + "'/></td></tr>";
                });

                $("#tblDetalle tbody").html("");
                $(tabla).appendTo("#tblDetalle tbody");
                $("#tblDetalle").tablesorter({
                    widthFixed: true, widgets: ['zebra'],
                    headers: {
                        0: { sorter: false }, 1: { sorter: false }, 2: { sorter: false }, 3: { sorter: false }, 4: { sorter: false }, 5: { sorter: false },
                        6: { sorter: false }, 7: { sorter: false }, 8: { sorter: false }, 9: { sorter: false }, 10: { sorter: false }, 11: { sorter: false }
                    }
                }).tablesorterPager({ container: $("#pager"), positionFixed: false });

                $(".EditRow").live('click', function () {

                    if (isValidFechaAcuerdo) {
                        var codigo = $(this).attr("id");
                        var idTomaAccion = codigo.split("-")[1];
                        var entidad = JSLINQ(lista).Where(function (item) { return item.IdTomaAccion == idTomaAccion; }).items[0];

                        if ($.trim(entidad.Periodo) == $.trim(periodoAcuerdo)) {
                            if (entidad.Estatus.toString().toLowerCase() == "true") {

                                MATRIZ.ShowAlert('', 'La Toma de acción ya fue Confirmada');
                            } else {
                                accion = "E";
                                $("#btnCancelar").show();
                                $("#PanelDetalle").hide();
                                loadTomaAccion(entidad);
                            }
                        } else {
                            MATRIZ.ShowAlert('', 'La consultora se encuentra en un periodo diferente al de acuerdo');
                        }
                    }
                });

                $(".DelRow").live('click', function () {
                    idTomaAccionDelete = "0";
                    if (isValidFechaAcuerdo) {
                        var codigo = $(this).attr("id");
                        var idTomaAccion = codigo.split("-")[1];
                        idTomaAccionDelete = idTomaAccion;
                        var entidad = JSLINQ(lista).Where(function (item) { return item.IdTomaAccion == idTomaAccion; }).items[0];

                        if ($.trim(entidad.Periodo) == $.trim(periodoAcuerdo)) {
                            if (entidad.Estatus.toString().toLowerCase() == "true") {

                                MATRIZ.ShowAlert('', 'La Toma de acción ya fue Confirmada');
                            } else {
                                $("#dialog-delete").dialog("open");
                            }
                        } else {
                            MATRIZ.ShowAlert('', 'La consultora se encuentra en un periodo diferente al de acuerdo');
                        }
                    }
                });

                $("#PanelDetalle").show();
            } else {
                MATRIZ.ShowAlert('dialog-alert', 'No se han realizado toma de acciones en este periodo de acuerdo');
            }
        }

        function loadTomaAccion(entidad) {
            idTomaAccionRef = entidad.IdTomaAccion;

            $("#ddlTomaAccion").empty();
            $("#ddlRegionReasignacion").empty();
            $("#ddlZonaReasignacion").empty();

            MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'select' }, entidad.TomaAccion, false, false);
            codRegionEvaluado = entidad.CodRegionActual;
            codPaisEvaluado = entidad.PrefijoIsoPaisEvaluado;
            MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regiones', 'codPais': codPaisEvaluador, 'select': 'select' }, entidad.PrefijoIsoPaisEvaluado + "-" + entidad.CodRegionActual, false, false);
            codZonaEvaluado = entidad.CodZonaActual;
            MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluado, 'codRegion': codRegionEvaluado, 'select': 'select' }, codZonaEvaluado, false, false);
            codUsuarioEvaluado = entidad.CodEvaluado;
            MATRIZ.LoadDropDownList("ddlGerenteZona", url, { 'accion': 'loadAllGerentesZonaByZona', 'codPais': codPaisEvaluado, 'codRegion': codRegionEvaluado, 'codZona': codZonaEvaluado, periodo: periodoAcuerdo }, codUsuarioEvaluado, false, false);
            campanhaActual = obtenerCampanahActual();

            MATRIZ.LoadDropDownList("ddlCampanhaInicio", url, { 'accion': 'obtenerCampanhas', 'campanha': campanhaActual }, entidad.AnhoCampanhaInicio, false, false);
            MATRIZ.LoadDropDownList("ddlCampanhaFin", url, { 'accion': 'obtenerCampanhas', 'campanha': campanhaActual }, entidad.AnhoCampanhaFin, false, false);

            if (entidad.TomaAccion == "01")//Plan de mejora
            {
                hideCampanhas(false);
                $("#ddlCampanhaInicio").show();
                $("#ddlCampanhaFin").show();

            } else {
                hideCampanhas(true);
            }

            if (entidad.TomaAccion == "02")//Reasignación
            {
                hideReasignacion(false);
                MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { accion: 'regiones', codPais: codPaisEvaluador, select: 'select' }, entidad.PrefijoIsoPaisEvaluado + "-" + entidad.CodRegionReasignacion, false, false);
                MATRIZ.LoadDropDownListAux("ddlZonaReasignacion", url, { 'accion': 'obtenerZonasDisponibles', 'codPais': codPaisEvaluado, 'codRegion': entidad.CodRegionReasignacion, 'periodo': entidad.Periodo }, entidad.NombreZonaReasignacion, entidad.CodZonaReasignacion, false);
            }
            else {
                hideReasignacion(true);
                MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { accion: 'regiones', codPais: codPaisEvaluador, select: 'select' }, 0, true, false);
            }

            $("#txtObservacion").val(entidad.Observaciones);
            DisableControl(true);
        }

        function DeleteTomaAccion() {
            var parametros = { accion: 'deleteTomaAccion', idTomaAccion: idTomaAccionDelete };
            var estado = MATRIZ.Ajax(url, parametros, false);

            if (estado == "1") {
                MATRIZ.ShowAlert('', '<div class="successbox" > La operación se realizó con éxito </div>');
            } else {
                MATRIZ.ShowAlert('', '<div class="errormsgbox" > Ocurrió un error </div>');
            }

            listar(periodoAcuerdo);
            $("#dialog-delete").dialog("close");
        }

        function DisableControl(value) {

            $("#ddlRegion").attr('disabled', value);
            $("#ddlZona").attr('disabled', value);
            $("#ddlGerenteZona").attr('disabled', value);
        }

        function hideReasignacion(value) {
            if (value) {
                $('#tblCalibracion td:nth-child(6),#tblCalibracion th:nth-child(6)').hide();
                $('#tblCalibracion td:nth-child(7),#tblCalibracion th:nth-child(7)').hide();
            } else {
                $('#tblCalibracion td:nth-child(6),#tblCalibracion th:nth-child(6)').show();
                $('#tblCalibracion td:nth-child(7),#tblCalibracion th:nth-child(7)').show();
            }
        }

        function hideCampanhas(value) {
            if (value) {
                $('#tblCalibracion td:nth-child(8),#tblCalibracion th:nth-child(8)').hide();
                $('#tblCalibracion td:nth-child(9),#tblCalibracion th:nth-child(9)').hide();
            } else {
                $('#tblCalibracion td:nth-child(8),#tblCalibracion th:nth-child(8)').show();
                $('#tblCalibracion td:nth-child(9),#tblCalibracion th:nth-child(9)').show();
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
