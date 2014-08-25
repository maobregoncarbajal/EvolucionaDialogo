<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="RegistroAcuerdo.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Matriz.RegistroAcuerdo" %>

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
        <div style="width: 100%; background: url(../Styles/Matriz/lienadentro.png)  repeat-x"
            id="divMatrizDV">
            <br />
            <div>
                <table id="tblRegistro" class="grillaAzulChica">
                    <tr>
                        <th rowspan="2">Período Acuerdo
                        </th>
                        <th rowspan="2">Toma de acción
                        </th>
                        <th rowspan="2">
                            <span id="spTituloColumnGerente"></span>
                        </th>
                        <th colspan="2">Actual
                        </th>
                        <th colspan="2">Reasignación
                        </th>
                        <th colspan="2">Campaña Acción
                        </th>
                        <th rowspan="2">Observaciones
                        </th>
                    </tr>
                    <tr>
                        <th>Región
                        </th>
                        <th>Zona
                        </th>
                        <th>Región
                        </th>
                        <th>Zona
                        </th>
                        <th>Inicio
                        </th>
                        <th>Fin
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPeriodoAcuerdo" runat="server"></asp:Label>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlTomaAccion" name="ddlTomaAccion">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlGerenteRegionZona" name="ddlGerenteRegionZona">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlRegion" name="ddlRegion">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlZona" name="ddlZona">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlRegionReasignacion" name="ddlRegionReasignacion">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlZonaReasignacion" name="ddlZonaReasignacion">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlCampanhaInicio" name="ddlCampanhaInicio">
                            </select>
                        </td>
                        <td>
                            <select style="width: 80px" id="ddlCampanhaFin" name="ddlCampanhaFin">
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
        <div style="clear: both;">
        </div>
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
        <!--Fin-->
    </div>
    <div id="dialog-alert">
    </div>
    <div id="dialog-confirm">
        <br />
        ¿Está seguro de realizar la operación?
    </div>
    <div id="dialog-delete">
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
        var idRolEvaluado = '';
        var codEvaluador = '';
        var nombreEvaluador = '';
        var codPaisEvaluado = '';
        var codRegionEvaluado = '';
        var codEvaluado = '';
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
        var anhoCampanhaInicioCritico = "";
        var anhoCampanhaFinCritico = "";
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

            $("#form1").validate({
                rules: {
                    ddlTomaAccion: {
                        selectNone: true
                    },
                    ddlRegion: {
                        selectNone: true
                    },
                    ddlGerenteRegionZona: {
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
                    ddlRegionReasignacion: {
                        selectNone: "*"
                    },
                    ddlZonaReasignacion: {
                        selectNone: "*"
                    },
                    ddlGerenteRegionZona: {
                        selectNone: "*"
                    },
                    ddlCampanhaInicio: {
                        compare: '*'
                    },
                    ddlCampanhaFin: {
                        compare: "debe ser mayor a la camp. de Incio"
                    },
                    txtObservacion: {
                        required: '*'
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
                    // $(this).parent().appendTo($('#aspnetForm'));
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
            listar(periodoAcuerdo);

            //EVENTOS

            $("#ddlTomaAccion").change(function () {

                $("#ddlZonaReasignacion").show();
                $("#ddlRegionReasignacion").show();
                $("#ddlCampanhaFin").hide();
                $("#ddlCampanhaInicio").hide();

                if (accion != "E") {
                    limpiarControles();
                    $("#ddlGerenteRegionZona").attr("disabled", false);
                    MATRIZ.LoadDropDownList("ddlGerenteRegionZona", url, { 'accion': 'lineamientoAcuerdo', 'codPaisEvaluador': codPaisEvaluador, 'codEvaluador': codEvaluador, 'periodo': periodoAcuerdo, 'idRolEvaluado': idRolEvaluado, tipoCondicion: $(this).val() }, 0, true, false);
                }

                if ($(this).val() == "01")//Plan de Mejora
                {
                    $("#ddlCampanhaFin").show();
                    $("#ddlCampanhaInicio").show();
                }

                if ($(this).val() == "02")//Reasignación
                {
                    $("#ddlRegionReasignacion").attr("disabled", false);

                    if (idRolEvaluado == "2")//GR
                    {
                        $("#ddlZonaReasignacion").hide();
                    }

                    if (idRolEvaluado == "3")//GZ
                    {
                        $("#ddlZonaReasignacion").attr("disabled", false);
                    }
                } else {
                    $("#ddlRegionReasignacion").hide();
                    $("#ddlZonaReasignacion").hide();
                }
            });

            $("#ddlGerenteRegionZona").change(function () {
                $("#ddlRegion").empty();
                $("#ddlZona").empty();
                $("#ddlCampanhaInicio").empty();
                $("#ddlCampanhaFin").empty();
                $("#txtObservacion").val('');

                if ($(this).val() != "00") {

                    var arraydata = $(this).val().split('-');

                    if (arraydata.length == 2) {
                        codPaisEvaluado = arraydata[0];
                        codEvaluado = arraydata[1];
                    }

                    if (arraydata.length == 4) {
                        codPaisEvaluado = arraydata[0];
                        codEvaluado = arraydata[1];
                        anhoCampanhaInicioCritico = arraydata[2];
                        anhoCampanhaFinCritico = arraydata[3];
                    }

                    if (arraydata.length == 5) {
                        codPaisEvaluado = arraydata[0];
                        codEvaluado = arraydata[1] + "-" + arraydata[2];
                        anhoCampanhaInicioCritico = arraydata[3];
                        anhoCampanhaFinCritico = arraydata[4];
                    }


                    nombreEvaluado = $("#ddlGerenteRegionZona option:selected").text();
                    var parametros = { accion: 'cuadranteUsuario', codPais: codPaisEvaluado, codigoUsuario: codEvaluado, idRol: idRolEvaluado, anho: periodoAcuerdo.substr(0, 4), periodo: periodoAcuerdo };

                    var datos = MATRIZ.Ajax(url, parametros, false);

                    $("#ddlRegion").append(new Option(datos.NombreRegion, datos.CodPais + '-' + datos.CodRegion));

                    if (idRolEvaluado == "2")//GR
                    {
                        if ($("#ddlTomaAccion").val() == "02")//reasinacion
                        {
                            MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { 'accion': 'obtenerRegionesDisponibles', 'codPais': codPaisEvaluador, 'periodo': periodoAcuerdo, 'estadoActivo': "1" }, 0, true, false);
                        }
                    }

                    if (idRolEvaluado == "3")//GZ
                    {
                        MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { accion: 'regiones', codPais: codPaisEvaluado, select: 'select' }, 0, true, false);
                        $("#ddlZona").append(new Option(datos.NombreZona, datos.CodZona));
                    }

                    if (anhoCampanhaFinCritico != "") {

                        campanhaActual = anhoCampanhaFinCritico;

                        if (campanhaActual != "" && campanhaActual != null) {
                            crearCampanhas(campanhaActual);
                        }
                    }

                    MATRIZ.ToolTipText();
                }
            });

            $("#btnBuscar").click(function (evt) {

                if ($("#ddlPeriodoAcuerdo").val() != "00") {
                    listar($("#ddlPeriodoAcuerdo").val());
                } else {
                    MATRIZ.ShowAlert('', 'Debe seleccionar un periodo');
                }
            });

            $("#btnRegistrar").click(function (evt) {

                var isValid = $("#form1").valid();
                var existeOperacion = false;

                if (isValid) {

                    if (accion == "N") {
                        var parametros = { accion: 'validarTomaAccion', codPais: codPaisEvaluado, codUsuario: codEvaluado, idRol: idRolEvaluado, periodo: periodoAcuerdo, estadoActivo: "-1" };

                        var estado = MATRIZ.Ajax(url, parametros, false);

                        if (estado == "1") {
                            MATRIZ.ShowAlert('', 'Ya se ha realizado una toma de acción a esta colaboradora en este periodo');
                            existeOperacion = true;
                        }
                    }
                }
                if (isValid && !existeOperacion) {
                    $("#dialog-confirm").dialog("open");
                } else {
                    evt.preventDefault();
                }
            });

            $("#btnCancelar").click(function (evt) {
                codEvaluado = "";
                codPaisEvaluado = "";
                idTomaAccionRef = "0";
                accion = "N";
                $("#btnCancelar").hide();
                $("#PanelDetalle").show();
                $("#panelBuscador").show();

                MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'select' }, 0, true, false);

                DisableControl(true);
                limpiarControles();
                $("#ddlRegion").empty();
                $("#form1").data('validator').resetForm();

            });

            $("#ddlRegionReasignacion").change(function () {

                var arraydata = $(this).val().split('-');
                var codRegionReasignacion = "";

                if (arraydata.length == 2) {
                    codRegionReasignacion = arraydata[1];
                }
                if (idRolEvaluado == "3" && $("#ddlTomaAccion").val() == "02")//GZ -- Reasignacion
                {
                    MATRIZ.LoadDropDownList("ddlZonaReasignacion", url, { 'accion': 'obtenerZonasDisponibles', 'codPais': codPaisEvaluado, 'codRegion': codRegionReasignacion, 'periodo': periodoAcuerdo, estadoActivo: '1' }, 0, true, false);
                }
            });
        });

        //CRUD

        function listar(varPeriodo) {
            $("#PanelDetalle").hide();
            var parametros = { accion: 'listarTomaAccion', codPais: codPaisEvaluador, codEvaluador: codEvaluador, periodo: varPeriodo, idRolEvaluador: idRolEvaluador, idRolEvaluado: idRolEvaluado, estadoActivo: '1', codTomaAccion: '00' };

            lista = MATRIZ.Ajax(url, parametros, false);

            if (lista.length > 0) {
                $("#tblDetalle thead").html("");

                var titulo = "<tr><th> Período Acuerdo</th>";
                titulo = titulo + "<th> Toma de acción</th>";
                titulo = titulo + "<th> Región</th>";

                if (idRolEvaluado == "3")//GZ>
                    titulo = titulo + "<th> Zona</th>";

                if (idRolEvaluado == "2")//GR
                    titulo = titulo + "<th> Gerente de Región</th>";

                else if (idRolEvaluado == "3")//GZ
                {
                    titulo = titulo + "<th> Gerente de Zona</th>";
                }

                titulo = titulo + "<th> Región de Asignación</th>";

                if (idRolEvaluado == "3")//GZ
                {
                    titulo = titulo + "<th> Zona de Reasignación</th>";
                }

                titulo = titulo + "<th> Campaña Inicio</th>";
                titulo = titulo + "<th> Campaña Fin</th>";
                titulo = titulo + "<th>Observaciones</th><th></th><th></th></tr>";

                $(titulo).appendTo("#tblDetalle thead");

                var tabla = "";
                $.each(lista, function (i, v) {
                    tabla = tabla + "<tr><td>" + v.Periodo + "</td>";
                    tabla = tabla + "<td>" + v.NombreTomaAccion + "</td>";
                    tabla = tabla + "<td>" + v.NombreRegionActual + "</td>";

                    if (idRolEvaluado == "3")//GZ
                    {
                        tabla = tabla + "<td>" + v.NombreZonaActual + "</td>";
                    }

                    tabla = tabla + "<td>" + v.NombreEvaluado + "</td>";
                    tabla = tabla + "<td>" + v.NombreRegionReasignacion + "</td>";

                    if (idRolEvaluado == "3")//GZ
                    {
                        tabla = tabla + "<td>" + v.NombreZonaReasignacion + "</td>";
                    }
                    tabla = tabla + "<td>" + v.AnhoCampanhaInicio + "</td>";
                    tabla = tabla + "<td>" + v.AnhoCampanhaFin + "</td>";
                    tabla = tabla + "<td>" + v.Observaciones + "</td>";
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
                                $("#panelBuscador").hide();
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
                MATRIZ.ShowAlert('dialog-alert', 'No se han realizado toma de acciones en este periodo');
            }
        }

        function guardar() {

            var entidad = new Object();

            if (accion == "E") //Editar
            {
                entidad.IdTomaAccionRef = idTomaAccionRef;
            }

            entidad.PrefijoIsoPaisEvaluador = codPaisEvaluador;
            entidad.PrefijoIsoPaisEvaluado = codPaisEvaluado;
            entidad.NombreEvaluador = nombreEvaluador;
            entidad.NombreEvaluado = nombreEvaluado;
            entidad.Periodo = periodoAcuerdo;
            entidad.IdRolEvaluador = idRolEvaluador;
            entidad.IdRolEvaluado = idRolEvaluado;
            entidad.CodEvaluador = codEvaluador;
            entidad.CodEvaluado = codEvaluado;
            entidad.CodRegionActual = $("#ddlRegion").val().split('-')[1];

            if (idRolEvaluado == "3")//GZ
                entidad.CodZonaActual = $("#ddlZona").val();

            entidad.TomaAccion = $("#ddlTomaAccion").val();
            entidad.EstadoActivo = 1;

            if (entidad.TomaAccion == "02")//Reasignación
            {
                entidad.CodRegionReasignacion = $("#ddlRegionReasignacion").val().split('-')[1];

                if (idRolEvaluado == "3")//GZ
                    entidad.CodZonaReasignacion = $("#ddlZonaReasignacion").val();
            }

            if (entidad.TomaAccion == "01")//Plan de mejora
            {
                entidad.AnhoCampanhaInicio = $("#ddlCampanhaInicio").val();
                entidad.AnhoCampanhaFin = $("#ddlCampanhaFin").val();
            }

            entidad.AnhoCampanhaInicioCritico = anhoCampanhaInicioCritico;
            entidad.AnhoCampanhaFinCritico = anhoCampanhaFinCritico;

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
        function loadTomaAccion(entidad) {

            idTomaAccionRef = entidad.IdTomaAccion;
            $("#ddlTomaAccion").empty();
            $("#ddlRegionReasignacion").empty();
            $("#ddlZonaReasignacion").empty();
            $("#ddlGerenteRegionZona").empty();

            MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'select' }, entidad.TomaAccion, false, false);
            codRegionEvaluado = entidad.CodRegionActual;
            codPaisEvaluado = entidad.PrefijoIsoPaisEvaluado;
            MATRIZ.LoadDropDownList("ddlRegion", url, { 'accion': 'regiones', 'codPais': codPaisEvaluador, 'select': 'select' }, entidad.PrefijoIsoPaisEvaluado + "-" + entidad.CodRegionActual, false, false);
            codEvaluado = entidad.CodEvaluado;
            campanhaActual = obtenerCampanahActual();
            periodoAcuerdo = entidad.Periodo;
            $("#ddlGerenteRegionZona").append(new Option(entidad.NombreEvaluado, entidad.CodEvaluado));

            if (idRolEvaluado == "3")//GZ
            {
                MATRIZ.LoadDropDownList("ddlZona", url, { 'accion': 'zonas', 'codPais': codPaisEvaluado, 'codRegion': codRegionEvaluado, 'select': 'select' }, entidad.CodZonaActual, false, false);
            }

            if (entidad.TomaAccion == "01")//Plan de Mejora
            {
                $("#ddlCampanhaFin").show();
                $("#ddlCampanhaInicio").show();
            }

            anhoCampanhaInicioCritico = entidad.AnhoCampanhaInicioCritico;
            anhoCampanhaFinCritico = entidad.AnhoCampanhaFinCritico;

            MATRIZ.LoadDropDownList("ddlCampanhaInicio", url, { 'accion': 'obtenerCampanhas', 'campanha': anhoCampanhaFinCritico }, entidad.AnhoCampanhaInicio, false, false);
            MATRIZ.LoadDropDownList("ddlCampanhaFin", url, { 'accion': 'obtenerCampanhas', 'campanha': anhoCampanhaFinCritico }, entidad.AnhoCampanhaFin, false, false);

            if (entidad.TomaAccion == "02")//Resignacion
            {
                $("#ddlRegionReasignacion").show();
                $("#ddlRegionReasignacion").attr('disabled', false);

                if (idRolEvaluado == "2")//GR
                {
                    $("#ddlZonaReasignacion").hide();
                    MATRIZ.LoadDropDownListAux("ddlRegionReasignacion", url, { 'accion': 'obtenerRegionesDisponibles', 'codPais': codPaisEvaluador, 'periodo': periodoAcuerdo, 'estadoActivo': "1" }, entidad.NombreRegionReasignacion, entidad.PrefijoIsoPaisEvaluado + '-' + entidad.CodRegionReasignacion, false);
                }

                if (idRolEvaluado == "3")//GZ
                {
                    $("#ddlZonaReasignacion").show();
                    $("#ddlZonaReasignacion").attr('disabled', false);
                    MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { accion: 'regiones', codPais: codPaisEvaluado, select: 'select' }, entidad.PrefijoIsoPaisEvaluado + '-' + entidad.CodRegionReasignacion, false, false);
                    MATRIZ.LoadDropDownListAux("ddlZonaReasignacion", url, { 'accion': 'obtenerZonasDisponibles', 'codPais': codPaisEvaluado, 'codRegion': entidad.CodRegionReasignacion, 'periodo': entidad.Periodo }, entidad.NombreZonaReasignacion, entidad.CodZonaReasignacion, false);
                }
            }
            else {

                if (idRolEvaluado == "2")//GR
                {
                    MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { 'accion': 'obtenerRegionesDisponibles', 'codPais': codPaisEvaluador, 'periodo': periodoAcuerdo, 'estadoActivo': "1" }, 0, true, false);
                }

                if (idRolEvaluado == "3")//GZ
                {
                    MATRIZ.LoadDropDownList("ddlRegionReasignacion", url, { accion: 'regiones', codPais: codPaisEvaluado, select: 'select' }, 0, true, false);
                    MATRIZ.LoadDropDownList("ddlZonaReasignacion", url, { 'accion': 'obtenerZonasDisponibles', 'codPais': codPaisEvaluado, 'codRegion': entidad.CodRegionReasignacion, 'periodo': entidad.Periodo }, 0, true, false);
                }

                $("#ddlZonaReasignacion").hide();
                $("#ddlRegionReasignacion").hide();
            }

            $("#txtObservacion").val(entidad.Observaciones);
            DisableControl(true);

            $("#ddlRegionReasignacion").attr('disabled', false);

            if (entidad.TomaAccion == "02" && idRolEvaluado == "3")//Resignacion
            {
                $("#ddlZonaReasignacion").attr('disabled', false);
            }
        }

        //Funciones

        function crearPopUp() {
            var arrayDialog = [{ name: "dialog-alert", height: 150, width: 250, title: "Alerta" },
                               { name: "dialog-delete", height: 150, width: 250, title: "Alerta" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function inicializar() {
            //variables evaluador
            codPaisEvaluador = $("#<%=lblPaisEvaluador.ClientID %>").html();
            idRolEvaluador = $("#<%=lbIdlRolEvaluador.ClientID %>").html();
            codEvaluador = $("#<%=lblCodigoEvaluador.ClientID %>").html();
            periodoAcuerdo = $("#<%=lblPeriodoAcuerdo.ClientID %>").html();
            nombreEvaluador = $("#<%=lblNombreEvaluador.ClientID %>").html();
            MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'select' }, 0, true, false);
            MATRIZ.ToolTipText();

            isValidFechaAcuerdo = validarFechaAcuerdo();
            //tabla Registro
            $("#PanelDetalle").hide();
            $("#spTituloColumnGerente").html("");
            $("#ddlRegion").attr('disabled', true);
            $("#ddlZona").attr('disabled', true);
            $("#ddlRegionReasignacion").attr('disabled', true);
            $("#ddlZonaReasignacion").attr('disabled', true);
            $("#ddlCampanhaFin").hide();
            $("#ddlCampanhaInicio").hide();

            if (idRolEvaluador == "1")//DV
            {
                $("#spTituloColumnGerente").append("Gerente Región");
                idRolEvaluado = "2"; //GR
            }

            if (idRolEvaluador == "2")//GR
            {
                $("#spTituloColumnGerente").append("Gerente Zona");

                idRolEvaluado = "3"; //GZ
            }

            MATRIZ.LoadDropDownList("ddlPeriodoAcuerdo", url, { accion: 'periodosAll', codPais: codPaisEvaluador, anho: '00', idRol: idRolEvaluador, normal: 'si', select: 'si' }, 0, true, false);
            MATRIZ.ToolTipText();
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

            var parametros = { accion: 'obtenerCampanhaActual', periodo: periodoAcuerdo, codPais: codPaisEvaluado, codUsuario: codEvaluado, idRol: idRolEvaluado };

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
            $('#ddlRegion').empty();
            $('#ddlZona').empty();
            $('#ddlGerenteRegionZona').empty();
            $("#ddlRegionReasignacion").empty();
            $("#ddlZonaReasignacion").empty();
            $("#ddlCampanhaInicio").empty();
            $("#ddlCampanhaFin").empty();
            $("#txtObservacion").val('');
            $("#ddlCampanhaFin").hide();
            $("#ddlCampanhaInicio").hide();
            anhoCampanhaInicioCritico = "";
            anhoCampanhaFinCritico = "";
        }

        function DisableControl(value) {
            $("#ddlRegion").attr('disabled', value);
            $("#ddlZona").attr('disabled', value);
            $("#ddlGerenteRegionZona").attr('disabled', value);
            $("#ddlRegionReasignacion").attr('disabled', value);
            $("#ddlZonaReasignacion").attr('disabled', value);
        }

        function crearCampanhas(campanha) {
            $('#ddlCampanhaInicio').empty();
            $('#ddlCampanhaFin').empty();
            MATRIZ.LoadDropDownList("ddlCampanhaInicio", url, { 'accion': 'obtenerCampanhas', 'campanha': campanha }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlCampanhaFin", url, { 'accion': 'obtenerCampanhas', 'campanha': campanha }, 0, true, false);
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
