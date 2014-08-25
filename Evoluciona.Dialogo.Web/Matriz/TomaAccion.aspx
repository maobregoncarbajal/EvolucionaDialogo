<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="TomaAccion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Matriz.TomaAccion" %>

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
        <div style="width: 100%; background: url(../Styles/Matriz/lienadentro.png)  repeat-x">
            <br />
            <p style="color: #4660A1; font-size: 16px; font-weight: bold; text-align: left">
                Revisión de acuerdos
            </p>
        </div>
        <!--Inicio-->
        <div style="width: 100%; background: url(../Styles/Matriz/lienadentro.png) repeat-x; margin-bottom: 10px">
            <br />
            <div align="left" style="margin: 0px 0px 0px 0px; background: url(../Styles/Matriz/fnd3.combos.png) no-repeat; height: 85px">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50px;"></td>
                        <td style="vertical-align: middle; width: 130px;">
                            <p class="labelCboSeleccion">
                                Período de Acuerdo:
                            </p>
                        </td>
                        <td style="vertical-align: middle; width: 130px;">
                            <select id="ddlPeriodoAcuerdo">
                            </select>
                        </td>
                        <td style="vertical-align: middle; width: 130px;">
                            <p class="labelCboSeleccion">
                                Toma de Acción:
                            </p>
                        </td>
                        <td style="vertical-align: middle; width: 130px;">
                            <select id="ddlTomaAccion">
                            </select>
                        </td>
                        <td style="vertical-align: middle; width: 130px;">
                            <p class="labelCboSeleccion" id="lblRolRealizoTomaAccion">
                                Rol realizo Toma Acción:
                            </p>
                        </td>
                        <td style="vertical-align: middle; width: 130px;">
                            <select id="ddlRolRealizoTomaAccion">
                                <option value="00" selected="selected"></option>
                                <option value="DV">Directora Venta</option>
                                <option value="GR">Gerente Region</option>
                            </select>
                        </td>
                        <td style="vertical-align: middle" rowspan="2">
                            <div style="float: left; margin-left: 10px">
                                <div class="btnRedoneado">
                                    <div class="btnRedoneado-outer">
                                        <div class="btnRedoneado-inner">
                                            <div class="btnRedoneado-into">
                                                <input type="button" id="btnBuscar" value="Buscar" class="btn" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="PanelDetalle">
                <div id="divMatrizDV">
                    <table class="grillaMorada" id="tblDetalle" cellspacing="0" rules="all" border="1">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <br />
                <div style="float: right; margin-right: 10px">
                    <input type="button" class="btnSquare print" value="Imprimir" id="btnPrintReporteDetalle"
                        onclick="imprimir();" />
                    <input type="button" id="btnGuardar" value="Guardar" class="btnSquare print" />
                </div>
                <br />
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
                        <select class="pagesize" id="paginacion">
                            <option selected="selected" value="10">10</option>
                            <option value="20">20</option>
                            <option value="30">30</option>
                            <option value="40">40</option>
                        </select>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPaisEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lblCodigoEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lbIdlRolEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lblPeriodoAcuerdo" runat="server"></asp:Label>
        <asp:Label ID="lblNombreEvaluador" runat="server"></asp:Label>
    </div>
    <img src="<%=Utils.AbsoluteWebRoot%>Images/loading.gif" alt="Exporting"
        id="imgExporting" style="display: none" />
    <div id="dialog-alert">
    </div>
    <div id="dialog-confirm">
        <br />
        ¿Está seguro de realizar la operación?<br />
        <br />
        Nota:Los Planes de mejora crea un diálogo cuando son confirmados.
    </div>

    <script type="text/javascript" language="javascript">
        //region Variables
        var nombreEvaluador = '';
        var codPaisEvaluador = '';
        var codPaisEvaluado = '';
        var idRolEvaluador = '';
        var idRolEvaluado = '';
        var codigoEvaluador = '';
        var codigoEvaluado = '';
        var pageBlocked = false;
        var dlgElement;
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var lista;
        var contadorMax;
        var isValidFechaAcuerdo = false;

        //región de variables Busqueda
        var periodoAcuerdo = "";
        var codTomaAccion = "00";

        $(document).ready(function () {

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
            crearPopUp();
            inicializar();

            //EVENTOS
            $(".ver").live("click", function (e) {
                var index;
                var codigo = $(this).attr("name");
                index = codigo.split('-')[1];
                index = index - 1;
                lista[index].EstatusClickVer = 'true';
                $.fn.colorbox({
                    href: "<%=Utils.RelativeWebRoot%>Matriz/SustentoMatriz.aspx?Datos=" + lista[index].IdTomaAccion + "-" + lista[index].PrefijoIsoPaisEvaluado + "-" + lista[index].Periodo + "-" + lista[index].CodEvaluado + "-" + lista[index].TomaAccion + "-" + lista[index].IdRolEvaluado.toString()
                     + "-" + lista[index].NombreEvaluado.toString() + "-" + lista[index].NombreRolEvaluado.toString() + "-" + lista[index].CorreoEvaluado.toString() + "-" + codigoEvaluador, width: "900px", height: "460px", iframe: true, opacity: "0.8", open: true, close: ""
                });
            });

            $(".seleccion").live("change", function (e) {

                var codigo = $(this).attr("id");
                var index = codigo.split('-')[1];
                index = index - 1;

                var isValidPeriodoCampnaha = true;

                if (lista[index].TomaAccion == "01") //plan mejora
                {
                    isValidPeriodoCampnaha = validarPeriodoPorCampanha(lista[index].PrefijoIsoPaisEvaluado, lista[index].AnhoCampanhaInicio);
                }

                if (isValidPeriodoCampnaha) {
                    if ($(this).val() == '1') {
                        lista[index].EstatusChangeDdl = 'true';
                        lista[index].Estatus = 1;
                    }
                    else {
                        lista[index].EstatusChangeDdl = 'false';
                        lista[index].Estatus = 0;
                    }
                } else {

                    MATRIZ.ShowAlert('', 'El periodo de la campaña de inicio aún no ha sido abierto para el plan de mejora');
                    $(this).val('0');
                    lista[index].Estatus = 0;
                }
            });

            $("#btnGuardar").click(function (e) {
                var idSinEvaluar = "";
                if (lista.length > 0) {
                    $.each(lista, function (i, v) {

                        if (v.EstatusChangeDdl == "true") {

                            if (v.EstatusClickVer == "false") {
                                i = i + 1;
                                idSinEvaluar = idSinEvaluar + '-' + i;
                            }
                        }
                    });
                }

                if (idSinEvaluar != "") {
                    MATRIZ.ShowAlert('dialog-alert', "Debe ver el sustento antes de confirmar el item(s):" + idSinEvaluar);
                } else {
                    $("#dialog-confirm").dialog("open");
                }
            });

            $("#btnBuscar").click(function (e) {


                if (idRolEvaluador == 1) {
                    if ($("#ddlPeriodoAcuerdo").val() != "00" && $("#ddlRolRealizoTomaAccion").val() != "00") {

                        periodoAcuerdo = $("#ddlPeriodoAcuerdo").val();
                        codTomaAccion = $("#ddlTomaAccion").val();
                        listar(periodoAcuerdo, codTomaAccion);

                    } else {

                        if ($("#ddlPeriodoAcuerdo").val() == "00") {
                            MATRIZ.ShowAlert('', 'Debe seleccionar un periodo');
                        } else {
                            MATRIZ.ShowAlert('', 'Debe seleccionar Rol que realizo la Toma Acción');
                        }

                    }
                } else {

                    if ($("#ddlPeriodoAcuerdo").val() != "00") {

                        periodoAcuerdo = $("#ddlPeriodoAcuerdo").val();
                        codTomaAccion = $("#ddlTomaAccion").val();
                        listar(periodoAcuerdo, codTomaAccion);

                    } else {

                        MATRIZ.ShowAlert('', 'Debe seleccionar un periodo');

                    }

                }

            });
        });

        // FUNCIONES CRUD

        function listar(periodoAcuerdo, codTomaAccion) {
            $("#PanelDetalle").hide();
            var parametros;
            var parametrosGr;
            var listaGr;
            var listaInt;

            if ($("#ddlRolRealizoTomaAccion").val() == "GR") {

                lista = [];


                parametrosGr = { accion: 'gerenteRegion', 'codUsuario': codigoEvaluador, 'seleccionar': '0' };
                listaGr = MATRIZ.Ajax(url, parametrosGr, false);




                $.each(listaGr, function (indc, valr) {

                    parametros = {
                        accion: 'listarTomaAccion',
                        codPais: codPaisEvaluador,
                        codEvaluador: valr.Codigo.split('-')[1],
                        periodo: periodoAcuerdo,
                        idRolEvaluador: '2',
                        idRolEvaluado: '-1',
                        estadoActivo: '1',
                        codTomaAccion: codTomaAccion
                    };

                    listaInt = MATRIZ.Ajax(url, parametros, false);


                    $.each(listaInt, function (ndc, vlr) {

                        var itm = new Object();
                        itm.IdTomaAccion = vlr.IdTomaAccion;
                        itm.IdTomaAccionRef = vlr.IdTomaAccionRef;
                        itm.PrefijoIsoPaisEvaluador = vlr.PrefijoIsoPaisEvaluador;
                        itm.NombrePaisEvaluador = vlr.NombrePaisEvaluador;
                        itm.PrefijoIsoPaisEvaluado = vlr.PrefijoIsoPaisEvaluado;
                        itm.NombrePaisEvaluado = vlr.NombrePaisEvaluado;
                        itm.CorreoEvaluado = vlr.CorreoEvaluado;
                        itm.Periodo = vlr.Periodo;
                        itm.IdRolEvaluador = vlr.IdRolEvaluador;
                        itm.NombreRolEvaluador = vlr.NombreRolEvaluador;
                        itm.IdRolEvaluado = vlr.IdRolEvaluado;
                        itm.NombreRolEvaluado = vlr.NombreRolEvaluado;
                        itm.CodEvaluador = vlr.CodEvaluador;
                        itm.CorreoSupervisor = vlr.CorreoSupervisor;
                        itm.NombreEvaluador = vlr.NombreEvaluador;
                        itm.CodEvaluado = vlr.CodEvaluado;
                        itm.NombreEvaluado = vlr.NombreEvaluado;
                        itm.CodRegionActual = vlr.CodRegionActual;
                        itm.NombreRegionActual = vlr.NombreRegionActual;
                        itm.CodZonaActual = vlr.CodZonaActual;
                        itm.NombreZonaActual = vlr.NombreZonaActual;
                        itm.TomaAccion = vlr.TomaAccion;
                        itm.NombreTomaAccion = vlr.NombreTomaAccion;
                        itm.CodRegionReasignacion = vlr.CodRegionReasignacion;
                        itm.NombreRegionReasignacion = vlr.NombreRegionReasignacion;
                        itm.CodZonaReasignacion = vlr.CodZonaReasignacion;
                        itm.NombreZonaReasignacion = vlr.NombreZonaReasignacion;
                        itm.AnhoCampanhaInicio = vlr.AnhoCampanhaInicio;
                        itm.AnhoCampanhaFin = vlr.AnhoCampanhaFin;
                        itm.AnhoCampanhaInicioCritico = vlr.AnhoCampanhaInicioCritico;
                        itm.AnhoCampanhaFinCritico = vlr.AnhoCampanhaFinCritico;
                        itm.Estatus = vlr.Estatus;
                        itm.Observaciones = vlr.Observaciones;
                        itm.EstadoActivo = vlr.EstadoActivo;
                        itm.EstadoUltimo = vlr.EstadoUltimo;
                        itm.EstatusClickVer = vlr.EstatusClickVer;
                        itm.EstatusChangeDdl = vlr.EstatusChangeDdl;
                        itm.IdProceso = vlr.IdProceso;




                        lista.push(itm);

                    });

                });

            } else {

                parametros = {
                    accion: 'listarTomaAccion',
                    codPais: codPaisEvaluador,
                    codEvaluador: codigoEvaluador,
                    periodo: periodoAcuerdo,
                    idRolEvaluador: idRolEvaluador,
                    idRolEvaluado: '-1',
                    estadoActivo: '1',
                    codTomaAccion: codTomaAccion
                };
                lista = MATRIZ.Ajax(url, parametros, false);

            }








            if (lista.length > 0) {
                $("#tblDetalle thead").html("");

                var titulo = "<tr><th rowspan='2'>Nro</th>";
                titulo = titulo + "<th rowspan='2'>Nombre Gerente</th>";
                titulo = titulo + "<th rowspan='2'>Rol</th>";
                titulo = titulo + "<th colspan='2'>Actual</th>";
                titulo = titulo + "<th rowspan='2'>Toma de acción</th>";
                titulo = titulo + "<th colspan='2'>Reasignación</th>";
                titulo = titulo + "<th colspan='2'>Campaña Acción</th>";
                titulo = titulo + "<th rowspan='2'>Observación</th>";
                titulo = titulo + "<th rowspan='2'>Sustentación</th>";
                titulo = titulo + "<th rowspan='2'>Status</th></tr>";
                titulo = titulo + "<tr><th>Zona</th>";
                titulo = titulo + "<th>Región</th>";
                titulo = titulo + "<th>Zona</th>";
                titulo = titulo + "<th>Región</th>";
                titulo = titulo + "<th>Inicio</th>";
                titulo = titulo + "<th>Fin</th></tr>";

                $(titulo).appendTo("#tblDetalle thead");

                var tabla = "";
                var contador = 1;
                $.each(lista, function (i, v) {
                    tabla = tabla + "<tr><td>" + contador + "</td>";
                    tabla = tabla + "<td>" + v.NombreEvaluado + "</td>";
                    tabla = tabla + "<td>" + v.NombreRolEvaluado + "</td>";
                    tabla = tabla + "<td>" + v.NombreZonaActual + "</td>";
                    tabla = tabla + "<td>" + v.NombreRegionActual + "</td>";
                    tabla = tabla + "<td>" + v.NombreTomaAccion + "</td>";
                    tabla = tabla + "<td>" + v.NombreZonaReasignacion + "</td>";
                    tabla = tabla + "<td>" + v.NombreRegionReasignacion + "</td>";
                    tabla = tabla + "<td>" + v.AnhoCampanhaInicio + "</td>";
                    tabla = tabla + "<td>" + v.AnhoCampanhaFin + "</td>";

                    if (v.Observaciones.length > 30) {
                        tabla = tabla + "<td>" + v.Observaciones.substring(0, 30) + "...</td>";
                    } else {
                        tabla = tabla + "<td>" + v.Observaciones + "</td>";
                    }

                    tabla = tabla + "<td><a name='link-" + contador + "-" + v.Estatus.toString() + "' class='ver' id='" + contador + "' style='text-decoration: underline'>ver</a></td>";
                    if (v.Estatus.toString().toLowerCase() == "true") {
                        tabla = tabla + "<td><select id='s-" + contador + "' class='seleccion' disabled='disabled' ><option selected='selected' value='1'>Confirmado</option><option value='0'>Por Confirmar</option></select></td></tr>";
                    }
                    else {
                        tabla = tabla + "<td ><select id='s-" + contador + "' class='seleccion'><option value='1'>Confirmado</option><option selected='selected' value='0'>Por Confirmar</option></select></td></tr>";
                    }

                    contador = contador + 1;
                });

                contadorMax = contador - 1;

                $("#tblDetalle tbody").html("");
                $(tabla).appendTo("#tblDetalle tbody");

                $("#tblDetalle").tablesorter({
                    widthFixed: true, widgets: ['zebra'],
                    headers: {
                        0: { sorter: false }, 1: { sorter: false }, 2: { sorter: false }, 3: { sorter: false }, 4: { sorter: false }, 5: { sorter: false },
                        6: { sorter: false }, 7: { sorter: false }, 8: { sorter: false }, 9: { sorter: false }, 10: { sorter: false }, 11: { sorter: false }
                    }
                }).tablesorterPager({ container: $("#pager"), positionFixed: false });

                $("#PanelDetalle").show();
            } else {
                MATRIZ.ShowAlert('dialog-alert', 'No se han encontrado registros de toma de acciones');
            }
        }

        function guardar() {

            var estado;

            $.each(lista, function (i, v) {

                var lst = new Array();
                lst.push(v);

                var parametros = { accion: 'confirmarTomaAccion', entidades: JSON.stringify(lst) };
                estado = MATRIZ.Ajax(url, parametros, false);

            });


            //var parametros = { accion: 'confirmarTomaAccion', entidades: JSON.stringify(lista) };
            //var estado = MATRIZ.Ajax(url, parametros, false);

            if (estado) {
                MATRIZ.ShowAlert('', '<div class="successbox" > La operación se realizó con éxito </div>');
            } else {
                MATRIZ.ShowAlert('', '<div class="errormsgbox" > Ocurrió un error </div>');
            }

            listar(periodoAcuerdo, "00");
        }

        //FUNCIONES HELP

        function crearPopUp() {

            var arrayDialog = [{ name: "dialog-alert", height: 150, width: 250, title: "Alerta" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function inicializar() {
            codigoEvaluador = $("#<%=lblCodigoEvaluador.ClientID %>").html();
            idRolEvaluador = $("#<%=lbIdlRolEvaluador.ClientID %>").html();
            codPaisEvaluador = $("#<%=lblPaisEvaluador.ClientID %>").html();
            periodoAcuerdo = $("#<%=lblPeriodoAcuerdo.ClientID %>").html();
            nombreEvaluador = $("#<%=lblNombreEvaluador.ClientID %>").html();
            MATRIZ.LoadDropDownList("ddlTomaAccion", url, { 'accion': 'tipos', 'fileName': 'TomaAccion.xml', 'adicional': 'si' }, 0, true, false);
            MATRIZ.LoadDropDownList("ddlPeriodoAcuerdo", url, { accion: 'periodosAll', codPais: codPaisEvaluador, anho: '00', idRol: idRolEvaluador, normal: 'si', select: 'si' }, 0, true, false);
            listar(periodoAcuerdo, codTomaAccion);
            isValidFechaAcuerdo = validarFechaAcuerdo();

            if (idRolEvaluador == 2) {
                $("#lblRolRealizoTomaAccion").hide();
                $("#ddlRolRealizoTomaAccion").hide();
            }

        }

        function validarFechaAcuerdo() {
            var isValid = false;
            var parametros = { accion: 'validarFechaAcuerdo', codPais: codPaisEvaluador, periodo: periodoAcuerdo };

            var value = MATRIZ.Ajax(url, parametros, false);

            if (value == '1') {
                isValid = true;
            }

            if (!isValid) {

                $("#divMatrizDV").find('select').attr('disabled', true);
                $("#btnGuardar").attr('disabled', true);
                $(".seleccion").attr('disabled', true);

                if (value == "0") {
                    MATRIZ.ShowAlert('dialog-alert', 'El periodo de acuerdo ha terminado');
                } else {
                    MATRIZ.ShowAlert('dialog-alert', ' Esta opción no esta activa. Por favor active el cronograma correspondiente de este pais');
                }
            }
            else {
                $("#divMatrizDV").find('select').attr('disabled', false);
                $("#btnGuardar").attr('disabled', false);
                $(".seleccion").attr('disabled', false);
            }

            return isValid;
        }

        function validarPeriodoPorCampanha(codPais, campanha) {

            var isValid = false;
            var parametros = { accion: 'validarPeriodoPorCampanha', codPais: codPais, campanha: campanha };
            var value = MATRIZ.Ajax(url, parametros, false);
            if (value == "1") {
                isValid = true;
            }
            return isValid;
        }

        function imprimir() {
            window.print();
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
