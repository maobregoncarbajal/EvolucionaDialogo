<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Calibracion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.Calibracion"
    Title="Calibracion" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuMatriz.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/json2.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

    <style type="text/css">
        .filtroError {
            color: #FF0000;
            font-family: Arial;
            font-size: 10px;
        }

        .filtroVacio {
            color: #0000FF;
            font-family: Arial;
            font-size: 10px;
        }

        .filtroActual {
            color: #4660a1;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <div id="idParametros" style="display: none">
        <label for="lblCodPais" id="lblCodPais">
        </label>
        <label for="lblCodAnio" id="lblCodAnio">
        </label>
        <label for="lblCodPeriodo" id="lblCodPeriodo">
        </label>
        <asp:Label ID="lblTipoAdmin" runat="server"></asp:Label>
        <asp:Label ID="lblCodPaisUsuario" runat="server"></asp:Label>
        <asp:Label ID="lblCodigoUsuario" runat="server"></asp:Label>
    </div>
    <table>
        <tr>
            <td align="center">
                <table style="text-align: center">
                    <tr>
                        <td>&nbsp;<br />
                        </td>
                    </tr>
                    <tr>
                        <td class="Csstexto">Pais:
                            <select id="cboPais" style="width: 160px; height: 22px;" class="stiloborde">
                            </select>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A&ntilde;o:
                            <select id="cboAnho" style="width: 100px; height: 22px;" class="stiloborde">
                            </select>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Periodo:
                            <select id="cboPeriodos" style="width: 100px; height: 22px;" class="stiloborde">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px">
                            <label id="lblFiltroError" class="filtroError">
                            </label>
                            <label id="lblFiltroVacio" class="filtroVacio">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <input type="button" id="btnBuscar" value="Buscar" class="btn" onclick="createTable();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;<br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblDesPais" class="filtroActual">
                            </label>
                            &nbsp;&nbsp;&nbsp;
                            <label id="lblDesAnio" class="filtroActual">
                            </label>
                            &nbsp;&nbsp;&nbsp;
                            <label id="lblDesPeriodo" class="filtroActual">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;<br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: center; width: 900px">
                <table id="tblCalibracion" class="CssTabla2">
                    <thead>
                        <tr>
                            <th style='width: 40px' class="CssCabecTabla">Pa&iacute;s
                            </th>
                            <th style='width: 260px' class="CssCabecTabla">Directora de Venta
                            </th>
                            <th style='width: 260px' class="CssCabecTabla">Gerente de Zona
                            </th>
                            <th style='width: 158px' class="CssCabecTabla">Toma de Acci&oacute;n
                            </th>
                            <th style='width: 117px' class="CssCabecTabla">Observaci&oacute;n
                            </th>
                            <th style='width: 64px' class="CssCabecTabla">Activar
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>

    <div style="height: 20px">
        &nbsp;
    </div>

    <div id="pager" class="pager" style="height: 30px">
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
    </div>

    <div style="height: 20px">
        &nbsp;
    </div>

    <div>
        <input type="button" id="btnGuardar" disabled="disabled" value="Guardar" class="btn" />
    </div>

    <div id="dialog-alert" style="display: none">
    </div>
    <div id="dialog-confirm">
        <br />
        ¿Está seguro de realizar la operación?<br />
        <br />
        Nota:Los Planes de mejora crea un diálogo cuando son confirmados.
    </div>

    <script type="text/javascript" language="javascript">
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var urlAdmin = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";

        $(document).ready(function () {
            //INICIO
            crearPopUp();
            cargarPais();

            $("#dialog-confirm").dialog({
                autoOpen: false,
                resizable: false,
                height: 200,
                width: 200,
                title: "Alerta",
                modal: true,
                buttons: {
                    "Aceptar": function () {
                        grabar();
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    $(this).parent().appendTo($('#form1'));
                }
            });

            //EVENTOS
            $("#cboPais").change(function () {
                MATRIZ.LoadDropDownList("cboAnho", url, { 'accion': 'anho', 'codPais': $(this).val(), 'select': 'Seleccionar' }, 0, true, false);
                MATRIZ.ToolTipText();

                $("#cboPeriodos").empty().append('<option></option>');
            });

            $("#cboAnho").change(function () {
                if ($("#cboAnho").val() == '00') {
                    $("#cboPeriodos").empty().append('<option></option>');
                    return;
                }

                MATRIZ.LoadDropDownList("cboPeriodos", url, { 'accion': 'periodo', 'codPais': $("#cboPais").val(), 'anho': $(this).val(), idRol: '-1' }, 0, true, false);
                MATRIZ.ToolTipText();
            });

            $("#btnGuardar").click(function (e) {
                $("#dialog-confirm").dialog("open");
            });
        });

        function crearPopUp() {
            var arrayDialog = [{ name: "dialog-alert", height: 100, width: 250, title: "Alerta" }];
            MATRIZ.CreateDialogs(arrayDialog);
        }

        function cargarPais() {
            var tipoAdmin = $("#<%=lblTipoAdmin.ClientID%>").html();
            var codPaisUsuario = $("#<%=lblCodPaisUsuario.ClientID%>").html();
            MATRIZ.LoadDropDownList("cboPais", urlAdmin, { 'accion': 'pais', 'codPais': codPaisUsuario, 'tipo': tipoAdmin, 'select': 'Seleccionar' }, 0, true, false);
        }

        function createTable() {
            var tabla = "";
            var codPais = $("#cboPais").val();
            var periodo = $("#cboPeriodos").val();
            var estado;
            var desactivar = 1;

            $("#lblCodPais").html('');
            $("#lblDesPais").html('');
            $("#lblCodAnio").html('');
            $("#lblDesAnio").html('');
            $("#lblCodPeriodo").html('');
            $("#lblDesPeriodo").html('');
            $("#lblFiltroError").html('');
            $("#lblFiltroVacio").html('');

            if ((codPais == null || codPais == '') || (periodo == null || periodo == '')) {
                $("#lblFiltroError").html('Aún no se ha seleccionado todos los filtros.');
                $("#btnGuardar").attr("disabled", "disabled");
                return;
            }

            $("#tblCalibracion tbody").html("");
            var datosCalibracion = MATRIZ.Ajax(url, { accion: 'listarCalibraciones', codPais: codPais, periodo: periodo }, false);

            if (datosCalibracion == null || datosCalibracion.length == 0) {
                //MATRIZ.ShowAlert('dialog-alert', 'No se han encontrado Registros');
                $("#lblFiltroVacio").html('(0) n&uacute;mero de registros encontrados.');
                $("#btnGuardar").attr("disabled", "disabled");
                return;
            }
            else {
                $("#btnGuardar").removeAttr("disabled");
            }

            $("#lblCodPais").html($("#cboPais option:selected").val());
            $("#lblDesPais").html("Pa&iacute;s: " + $("#cboPais option:selected").text());
            $("#lblCodAnio").html($("#cboAnho option:selected").val());
            $("#lblDesAnio").html("A&ntilde;o: " + $("#cboAnho option:selected").text());
            $("#lblCodPeriodo").html($("#cboPeriodos option:selected").val());
            $("#lblDesPeriodo").html("Periodo: " + $("#cboPeriodos option:selected").text());

            var fechaLimiteOk = MATRIZ.Ajax(url, { accion: 'validarFechaAcuerdo', codPais: codPais, periodo: periodo }, false);

            if (fechaLimiteOk == 0) {
                desactivar = 0;
                MATRIZ.ShowAlert('dialog-alert', 'El periodo de acuerdo ha terminado o no existe');
                $("#btnGuardar").attr('disabled', true);
            } else {
                $("#btnGuardar").attr('disabled', false);
            }

            $.each(datosCalibracion, function (i, v) {
                tabla = tabla + "<tr class='fila'>";
                tabla = tabla + "<td id=" + i + " style='display: none'>" + v.IdTomaAccion + "</td>";
                tabla = tabla + "<td class='CssCeldas3'>" + v.PrefijoIsoPaisEvaluado + "</td>";
                tabla = tabla + "<td class='CssCeldas3'>" + v.NombreEvaluador + "</td>";
                tabla = tabla + "<td class='CssCeldas3'>" + v.NombreEvaluado + "</td>";
                tabla = tabla + "<td class='CssCeldas3'>" + v.NombreTomaAccion + "</td>";
                tabla = tabla + "<td class='CssCeldas3'>" + v.Observaciones + "</td>";
                tabla = tabla + "<td id='check" + i + "' class='CssCeldas3'>";

                if (v.EstadoActivo == 0) {
                    estado = "";
                }
                else {
                    estado = "checked = 'checked'";
                }

                if (desactivar == 0) {
                    tabla = tabla + "<input id=checked" + i + " type='checkbox' value='1' disabled='disabled' " + estado + "/>";
                }
                else {
                    tabla = tabla + "<input id=checked" + i + " type='checkbox' value='1' " + estado + "/>";
                }

                tabla = tabla + "</td>";
                tabla = tabla + "</tr>";
            });

            $(tabla).appendTo("#tblCalibracion tbody");

            $("tblCalibracion").trigger('destroy.pager');
            $("#tblCalibracion").tablesorter({ widthFixed: false }).tablesorterPager({ container: $("#pager"), positionFixed: false });
        }

        function grabar() {
            var listaCalibraciones = new Array();
            var filas = $("#tblCalibracion tbody tr.fila");
            var usuario = $("#<%=lblCodigoUsuario.ClientID %>").text();

            var IdTomaAccion;
            var estado;

            $.each(filas, function (i) {
                IdTomaAccion = "";
                estado = "";

                $(this).children("td").each(function (j, k) {
                    switch (j) {
                        case 0:
                            IdTomaAccion = $(this).text();
                            break;
                        case 6:
                            estado = $("#tblCalibracion tbody td#" + k.id + " input[type=checkbox]:checked").val();
                            break;
                    }
                });
                listaCalibraciones[i] = {
                    IdTomaAccion: IdTomaAccion,
                    EstadoActivo: estado
                };
            });

            var jsonCondiciones = JSON.stringify(listaCalibraciones);

            var competencias = MATRIZ.Ajax(url, { accion: 'updateCalibracion', calibraciones: jsonCondiciones, usuario: usuario }, false);
            $("#dialog-alert").html('');
            if (competencias) {
                $("#dialog-alert").append('Datos grabados correctamente.');
                $("#dialog-alert").dialog("open");
            }
            else {
                $("#dialog-alert").append('Lo sentimos, los datos no se pudieron grabar.');
                $("#dialog-alert").dialog("open");
            }
        }

    </script>

</asp:Content>
