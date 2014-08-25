<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CronogramaPdM.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.CronogramaPdM" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Jscripts/jquery-ui-1.11.0.custom/jquery-ui.css" rel="stylesheet"  />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>/Jscripts/jquery-ui-1.11.0.custom/jquery-ui.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.jqGrid-4.6.0/src/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../Jscripts/jquery.jqGrid-4.6.0/src/css/ui.jqgrid.css" rel="stylesheet" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.jqGrid-4.6.0/src/i18n/grid.locale-es.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Evoluciona.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center;padding-top: 10px ;height: 20px;color: White; background-color: #60497B;">
        <b>Cronograma PDM</b>
    </div>
    <div>
        <table id="list"><tr><td></td></tr></table>
        <div id="pager"></div>
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
    </div>
<script id="ReporteDialogo" type="text/javascript">

    $(function () {
        Evoluciona.SetFormatCalendar('es');
    });

    var url = '<%=Utils.RelativeWebRoot%>Admin/Handler/CronogramaPdMHandler.ashx';
    var pais = $("#<%=lblPais.ClientID %>").html();
    var paisesEdit = {};
    var paisesSearch = {};
    var parametrosPais = { accion: 'loadPaises' };
    var listObjPais = Evoluciona.Ajax(url, parametrosPais, false);
    
    $.each(listObjPais, function (i, v) {
        paisesEdit[v.prefijoIsoPais] = v.NombrePais;
        paisesSearch[v.NombrePais] = v.NombrePais;
    });
    
    paisesSearch = $.extend({ "": "Todos" }, paisesSearch);
    
    var periodos = {};
    var parametrosPeriodo = { accion: 'loadPeriodos' };
    var listObjPeriodo = Evoluciona.Ajax(url, parametrosPeriodo, false);

    $.each(listObjPeriodo, function (i, v) {
        periodos[v] = v;
    });

    var paisSelectRow, periodoSelectRow;


    jQuery("#list").jqGrid({
        colNames: ['IdCronogramaPdM', 'País', 'Periodo', 'F.Limite', 'F.Prorroga'],
        colModel: [
            { name: 'IdCronogramaPdM', index: 'IdCronogramaPdM', width: 100, hidden: true, editable: false },
            {
                name: 'OBePais.NombrePais', index: 'OBePais.NombrePais', width: 120, editable: true
                , edittype: 'select', stype: 'select'
                , editoptions: {
                    value: paisesEdit
                },
                searchoptions: {
                    value: paisesSearch
                }
            },
            {
                name: 'Periodo', index: 'Periodo', width: 120, editable: true
                , edittype: 'select', stype: 'select'
                , editoptions: {
                    value: periodos
                },
                searchoptions: {
                    value: $.extend({ "": "Todos" }, periodos)
                }
            },
            {
                name: 'FechaLimite', index: 'FechaLimite', width: 120, editable: true
                , sorttype: 'date'
                , formatter: 'date'
                , formatoptions: {
                    srcformat: 'Y-m-dTh:i:s'
                    , newformat: 'd/m/Y'
                }
                , editoptions: {
                    dataInit: function (elem) {
                        $(elem).datepicker({
                            changeYear: true,
                            changeMonth: true,
                            dateFormat: 'dd/mm/yy',
                            onClose: function () { this.focus(); }
                        });
                    }
                }
                , searchoptions: {
                    sopt: ['eq']
                    , dataInit: function (elem) {
                        $(elem).datepicker({
                            changeYear: true,
                            changeMonth: true,
                            dateFormat: 'dd/mm/yy',
                            onClose: function () { this.focus(); }
                        });
                    }
                }
            },
            {
                name: 'FechaProrroga', index: 'FechaProrroga', width: 120, editable: true
                , sorttype: 'date'
                ,formatter: 'date'
                ,formatoptions: {
                    srcformat: 'Y-m-dTh:i:s'
                    , newformat: 'd/m/Y'
                }
                , editoptions: {
                    dataInit: function (elem) {
                        $(elem).datepicker({
                            changeYear: true,
                            changeMonth: true,
                            dateFormat: 'dd/mm/yy',
                            onClose: function () { this.focus(); }
                        });
                    }
                }
                , searchoptions: {
                    sopt: ['eq']
                    , dataInit: function (elem) {
                        $(elem).datepicker({
                            changeYear: true,
                            changeMonth: true,
                            dateFormat: 'dd/mm/yy',
                            onClose: function () { this.focus(); }
                        });
                    }
                }
            }
        ],
        url: url + "?accion=loadCronogramaPdM",
        datatype: 'json',
        mtype: 'GET',
        editurl: url,
        rowNum: 10,
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
        onSelectRow: function (id) {
            var selId = jQuery('#list').jqGrid('getGridParam', 'selrow');
            paisSelectRow = Evoluciona.PrefijoIsoPais(jQuery('#list').jqGrid('getCell', selId, 'OBePais.NombrePais'));
            periodoSelectRow = jQuery('#list').jqGrid('getCell', selId, 'Periodo');
        }
    });

    jQuery('#list').jqGrid('navGrid', '#pager', { edit: true, add: true, del: true, search: false },
        {//EDIT EVENTS AND PROPERTIES GOES HERE
        //EDIT
            //                       height: 300,
            //                       width: 400,
            //                       top: 50,
            //                       left: 100,
            //                       dataheight: 280,
            closeOnEscape: true, //Closes the popup on pressing escape key
            reloadAfterSubmit: true,
            drag: true,
            afterSubmit: function (response) {
                if (response.responseText == "") {
                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                    return [true, ''];
                }
                else {
                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                    return [false, response.responseText]; //Captures and displays the response text on th Edit window
                }
            },
            editData: {
                IdCronogramaPdM: function () {
                    var selId = $('#list').jqGrid('getGridParam', 'selrow');
                    var value = $('#list').jqGrid('getCell', selId, 'IdCronogramaPdM');
                    return value;
                },
                UsuarioModi: function () {
                    var value = $("#<%=lblUsuario.ClientID %>").html();
                    return value;
                }
            },
            beforeSubmit: function () {
                //more validations
                
                if ($('#FechaLimite').val() == "") {
                    $('#FechaLimite').addClass("ui-state-highlight");
                    return [false, 'Fecha Limite no puede estar vacio']; //error
                } else {
                    var pais = document.getElementById("OBePais.NombrePais").value;
                    var periodo = $('#Periodo').val();
                    
                    if (pais != paisSelectRow || periodo != periodoSelectRow) {
                        var paramValidaCronogramaPdM = { accion: 'validaCronogramaPdM', 'pais': pais, 'periodo': periodo };
                        var cant = Evoluciona.Ajax(url, paramValidaCronogramaPdM, false);

                        if (cant > 0) {
                            return [false, 'Ya existe un Cronograma para este País y Periodo, Edite el existente.'];
                        }
                    }
                }

                
                if ($('#FechaProrroga').val() != "") {

                    var fechaLimite = $.datepicker.parseDate("dd/mm/yy", $('#FechaLimite').val());
                    var fechaProrroga = $.datepicker.parseDate("dd/mm/yy", $('#FechaProrroga').val());
                    
                    if (fechaLimite > fechaProrroga)
                    {
                        $('#FechaProrroga').addClass("ui-state-highlight");
                        return [false, 'Fecha Limite no puede ser mayor a la Fecha Prorroga']; //error                        
                    }
                }

                return [true, '']; // no error
            },
            viewPagerButtons: false
        },
        {
            closeAfterAdd: true, //Closes the add window after add
            afterSubmit: function (response) {
                if (response.responseText == "") {
                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');//Reloads the grid after Add
                    return [true, ''];
                }
                else {
                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');//Reloads the grid after Add
                    return [false, response.responseText];
                }
            },
            editData: {
                UsuarioCrea: function () {
                    var value = $("#<%=lblUsuario.ClientID %>").html();
                    return value;
                }
            },
            beforeSubmit: function () {
                //more validations
                
                if ($('#FechaLimite').val() == "") {
                    $('#FechaLimite').addClass("ui-state-highlight");
                    return [false, 'Fecha Limite no puede estar vacio']; //error
                } else {
                    var pais = document.getElementById("OBePais.NombrePais").value;
                    var periodo = $('#Periodo').val();
                    var paramValidaCronogramaPdM = { accion: 'validaCronogramaPdM', 'pais': pais, 'periodo': periodo };
                    var cant = Evoluciona.Ajax(url, paramValidaCronogramaPdM, false);

                    if (cant > 0) {
                        return [false, 'Ya existe Cronograma'];
                    }
                }

                
                if ($('#FechaProrroga').val() != "") {

                    var fechaLimite = $.datepicker.parseDate("dd/mm/yy", $('#FechaLimite').val());
                    var fechaProrroga = $.datepicker.parseDate("dd/mm/yy", $('#FechaProrroga').val());
                    
                    if (fechaLimite > fechaProrroga)
                    {
                        $('#FechaProrroga').addClass("ui-state-highlight");
                        return [false, 'Fecha Limite no puede ser mayor a la Fecha Prorroga']; //error                        
                    }
                }

                return [true, '']; // no error
            }
        },
        {//DELETE EVENTS AND PROPERTIES GOES HERE
        //DELETE
            closeOnEscape: true,
            closeAfterDelete: true,
            reloadAfterSubmit: true,
            drag: true,
            afterSubmit: function (response) {
                if (response.responseText == "") {

                    $("#list").trigger("reloadGrid", [{ current: true }]);
                    return [false, response.responseText];
                }
                else {
                    $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                    return [true, response.responseText];
                }
            },
            delData: {
                IdCronogramaPdM: function () {
                    var selId = $('#list').jqGrid('getGridParam', 'selrow');
                    var value = $('#list').jqGrid('getCell', selId, 'IdCronogramaPdM');
                    return value;
                }
                , UsuarioModi: function () {
                    var value = $("#<%=lblUsuario.ClientID %>").html();
                    return value;
                }
            }

        },
        {});
    jQuery("#list").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });
</script>
</asp:Content>
