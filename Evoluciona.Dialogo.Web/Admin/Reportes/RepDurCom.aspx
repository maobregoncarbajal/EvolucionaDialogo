<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RepDurCom.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Reportes.RepDurCom" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuRepDialogos.ascx" TagPrefix="uc1" TagName="MenuRepDialogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="../../Jscripts/JQGridReq/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Jscripts/JQGridReq/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../../Jscripts/JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/grid.locale-en.js" type="text/javascript"></script>
    
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jQuery UI 1.10.4/jquery-ui-1.10.4/ui/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc1:MenuRepDialogos runat="server" ID="MenuRepDialogos" />
    <br />
    <br />
    <div style="text-align: center;padding-top: 10px ;height: 20px;color: White; background-color: #60497B;">
        <b>Reporte Durante Competencia</b>
    </div>
    <br />
    <div>
        <b><label>País : </label></b>
        <select id="pais">
            <option value="">Seleccione</option>
            <option value="AR">Argentina</option>
            <option value="BO">Bolivia</option>
            <option value="CL">Chile</option>
            <option value="CO">Colombia</option>
            <option value="CR">Costa Rica</option>
            <option value="DO">Rep. Dominicana</option>
            <option value="EC">Ecuador</option>
            <option value="GT">Guatemala</option>
            <option value="MX">México</option>
            <option value="PA">Panamá</option>
            <option value="PE">Perú</option>
            <option value="PR">Puerto Rico</option>
            <option value="SV">El Salvador</option>
            <option value="VE">Venezuela</option>
        </select>
        <b><label>Año : </label></b>
        <select id="anho">
            <option value="">Seleccione</option>
            <option value="2012">2012</option>
            <option value="2013">2013</option>
            <option value="2014">2014</option>
        </select>
        <b><label>Período : </label></b>
        <select id="periodo">
            <option value="">Seleccione</option>
            <option value="I">I</option>
            <option value="II">II</option>
            <option value="III">III</option>
            <option value="00">Todos</option>
        </select>
        <b><label>Rol Evaluado: </label></b>
        <select id="idRol">
            <option value="">Seleccione</option>
            <option value="2">Gerente Región</option>
            <option value="3">Gerente Zona</option>
        </select>
        <b><label>Tipo Diálogo: </label></b>
        <select id="planMejora">
            <option value="">Seleccione</option>
            <option value="false">Normal</option>
            <option value="true">Plan de Mejora</option>
        </select>
        <button id="btnBuscarReporte">Buscar</button>
    </div>
    <br />
    <br />
    <div>
        <table id="list"><tr><td></td></tr></table>
        <div id="pager"></div>
    </div>
    <div id="dialog-message" title="Alerta" style="display:none; text-align: center;">
        Debe seleccionar <label id="label-message"></label>
    </div>
    
<script id="ReporteDialogo" type="text/javascript">

    var grillaReporte = 'list';

    jQuery().ready(function () {

        $("#dialog-message").dialog({
            autoOpen: false,
            height: 100,
            width: 'auto'
        });

        jQuery("#btnBuscarReporte")
            .click(function (e) {

                if ($("#pais").val() == '') {
                    $("#label-message").html('País');
                    $("#dialog-message").dialog("open");
                } else if ($("#anho").val() == '') {
                    $("#label-message").html('Año');
                    $("#dialog-message").dialog("open");
                } else if ($("#periodo").val() == '') {
                    $("#label-message").html('Período');
                    $("#dialog-message").dialog("open");
                } else if ($("#idRol").val() == '') {
                    $("#label-message").html('Rol Evaluado');
                    $("#dialog-message").dialog("open");
                } else if ($("#planMejora").val() == '') {
                    $("#label-message").html('Tipo Diálogo');
                    $("#dialog-message").dialog("open");
                } else {
                    GetReporte();
                }
                e.preventDefault();
            });

    });

    function GetReporte() {
        var pais = $("#pais").val();
        var anho = $("#anho").val();
        var periodo = $("#periodo").val();
        var idRol = $("#idRol").val();
        var planMejora = $("#planMejora").val();
        var tipoReporte = "DurCom";
        var url = '<%=Utils.RelativeWebRoot%>Admin/Reportes/RepDialogos.ashx';
        var urlExport = url + '?accion=export&pais=' + pais + '&anho=' + anho + '&periodo=' + periodo + '&idRol=' + idRol + '&planMejora=' + planMejora + '&tipoReporte=' + tipoReporte;

        var parametros = { accion: 'load', 'pais': pais, 'anho': anho, 'periodo': periodo, 'idRol': idRol, 'planMejora': planMejora, 'tipoReporte': tipoReporte };
        var listObj = Ajax(url, parametros, false);

        ListarReporte(listObj, urlExport);
    }

    function ListarReporte(listObj, urlExport) {
        $('#list').jqGrid('GridUnload');
        jQuery("#list").jqGrid({
            data: listObj,
            datatype: "local",
            colNames: [
                'Cod.Evaluador', 'Nombre Evaluador', 'Cod.Evaluado', 'Nombre Evaluado',
                'Competencia', 'Retroalimentación', 'Respuesta',
                'Período', 'Tipo Diálogo', 'País'
            ],
            colModel: [
                { name: 'CodEvaluador', index: 'CodEvaluador', width: 100 },
                { name: 'NombreEvaluador', index: 'NombreEvaluador', width: 160 },
                { name: 'CodEvaluado', index: 'CodEvaluado', width: 100 },
                { name: 'NombreEvaluado', index: 'NombreEvaluado', width: 160 },

                { name: 'Competencia', index: 'Competencia', width: 100 },
                { name: 'DesPreguRetro', index: 'DesPreguRetro', width: 100 },
                { name: 'Respuesta', index: 'Respuesta', width: 100 },

                { name: 'Periodo', index: 'Periodo', width: 100 },
                { name: 'TipoDialogo', index: 'TipoDialogo', width: 100 },
                { name: 'Pais', index: 'Pais', width: 100 }
            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            ignoreCase: true,
            rowList: [10, 20, 30],
            pager: '#pager',
            viewrecords: true,
            sortorder: "desc",
            caption: "",
            height: '100%',
            width: 1100,
            forceFit: false,
            shrinkToFit: false,
            autowidth: false,
            fixed: false,
        });


        jQuery('#list').jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: true });
        // AGREGANDO BOTON EXPORTAR EXCEL
        jQuery('#list').jqGrid('navButtonAdd', '#pager',
            {
                caption: '<span class="ui-pg-button-text">Export</span>',
                buttonicon: "ui-icon-extlink",
                title: "Export To Excel",
                onClickButton: function () {
                    window.location = urlExport;
                }
            }
        );

    }


    function Ajax(url, parameters, async) {
        var rsp = '';
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            cache: false,
            responseType: "json",
            data: parameters,
            success: function (response) {
                rsp = response;
            },
            failure: function () {
                rsp = -1;
            },
            error: function (request) {
                alert(jQuery.parseJSON(request.responseText).Message);
            }
        });
        return rsp;
    }

</script>
</asp:Content>