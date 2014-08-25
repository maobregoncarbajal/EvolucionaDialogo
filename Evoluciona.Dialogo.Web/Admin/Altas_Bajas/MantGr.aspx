<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="MantGr.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.MantGr" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery-1.9.0.min.js"
        type="text/javascript"></script>

    <link href="../../Jscripts/JQGridReq/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Jscripts/JQGridReq/jquery-ui-1.10.3.custom.css" rel="stylesheet"
        type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery.jqGrid.js"
        type="text/javascript"></script>

    <link href="../../Jscripts/JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/grid.locale-en.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="jQGridDemo">
    </table>
    <div id="jQGridDemoPager">
    </div>
    <asp:HiddenField ID="hfPais" runat="server" />
    <asp:HiddenField ID="hfValComboPais" runat="server" />
    <asp:HiddenField ID="hfValComboRegion" runat="server" />
    <asp:HiddenField ID="hfRol" runat="server" />

    <script type="text/javascript">

        var countries = $('#<%=hfValComboPais.ClientID%>').val();

        jQuery("#jQGridDemo").jqGrid({
            url: '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGr.ashx?accion=load&pais=' + $('#<%=hfPais.ClientID%>').val(),
            datatype: "json",
            colNames: ['IntIDGerenteRegion', 'C. G. Region', 'Pais', 'Nombre Completo', 'Correo Electronico', 'C. DirectorVenta', 'CUB', 'C. Planilla', 'C. Region', 'Observacion'],
            colModel: [
                { name: 'IntIDGerenteRegion', index: 'IntIDGerenteRegion', stype: 'text', editable: false, sorttype: 'int', hidden: true },
                { name: 'ChrCodigoGerenteRegion', index: 'ChrCodigoGerenteRegion', width: 100, align: "right", editable: true },
                { name: 'ChrPrefijoIsoPais', index: 'ChrPrefijoIsoPais', width: 100, align: "right", editable: true, edittype: "select" },
                { name: 'VchNombrecompleto', index: 'VchNombrecompleto', width: 220, editable: true },
                { name: 'VchCorreoElectronico', index: 'VchCorreoElectronico', width: 160, align: "left", sortable: false, editable: true },
                { name: 'ChrCodDirectorVenta', index: 'ChrCodDirectorVenta', width: 100, align: "right", editable: true },
                { name: 'VchCUBGR', index: 'VchCUBGR', width: 120, align: "right", editable: true },
                { name: 'ChrCodigoPlanilla', index: 'ChrCodigoPlanilla', width: 100, align: "right", sortable: false, editable: true },
                { name: 'VchCodigoRegion', index: 'VchCodigoRegion', width: 100, align: "right", editable: true },
                { name: 'VchObservacion', index: 'vchObservacion', width: 120, align: "right", sortable: false, editable: true }
            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            ignoreCase: true,
            rowList: [10, 20, 30],
            pager: '#jQGridDemoPager',
            sortname: 'IntIDGerenteRegion',
            viewrecords: true,
            sortorder: "desc",
            caption: "Gerente Region",
            editurl: '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGr.ashx',
            height: '100%',
            multiselect: true,
            onSelectRow: function (id) {
                var selRows = $(this).jqGrid('getGridParam', 'selarrrow');
                if (selRows.length === 1) {
                    //alert("no rows are selected now");
                    // you can disable the button
                    $("#edit_" + this.id).removeClass('ui-state-disabled');
                    //cargarRegiones('3');
                } else {
                    //alert("a row is selected now");
                    // you can disable the button
                    $("#edit_" + this.id).addClass('ui-state-disabled');
                    //cargarRegiones('4');
                }
            },
            onSelectAll: function (id) {
                var selRows = $(this).jqGrid('getGridParam', 'selarrrow');
                if (selRows.length === 1) {
                    //alert("no rows are selected now");
                    // you can disable the button
                    $("#edit_" + this.id).removeClass('ui-state-disabled');
                } else {
                    //alert("a row is selected now");
                    // you can disable the button
                    $("#edit_" + this.id).addClass('ui-state-disabled');
                }
            },
            loadComplete: function () {
                $("#edit_" + this.id).addClass('ui-state-disabled');
                $('#jQGridDemo').setColProp('ChrPrefijoIsoPais', { editoptions: { value: $('#<%=hfValComboPais.ClientID%>').val() } });
            }
        });

        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
            {
                edit: true,
                add: true,
                del: true,
                search: true,
                searchtext: "Search",
                addtext: "Add",
                edittext: "Edit",
                deltext: "Delete"
            },
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
                afterSubmit: function (response, postdata) {
                    if (response.responseText == "") {

                        $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                        return [true, ''];
                    }
                    else {
                        $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                        return [false, response.responseText]//Captures and displays the response text on th Edit window
                    }
                },
                editData: {
                    IntID: function () {
                        var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                        var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'IntIDGerenteRegion');
                        return value;
                    }
                },
                beforeSubmit: function (postdata, formid) {
                    //more validations
                    if ($('#Nombres').val() == "") {
                        $('#Nombres').addClass("ui-state-highlight");
                        return [false, 'Nombre no puede estar vacio']; //error
                    }
                    return [true, '']; // no error
                },
                viewPagerButtons: false
            },
            {//ADD EVENTS AND PROPERTIES GOES HERE
                closeAfterAdd: true, //Closes the add window after add
                afterSubmit: function (response, postdata) {
                    if (response.responseText == "") {

                        $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                        return [true, ''];
                    }
                    else {
                        $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                        return [false, response.responseText];
                    }
                }
            },
            {//DELETE EVENTS AND PROPERTIES GOES HERE
                //DELETE
                closeOnEscape: true,
                closeAfterDelete: true,
                reloadAfterSubmit: true,
                drag: true,
                afterSubmit: function (response, postdata) {
                    if (response.responseText == "") {

                        $("#jQGridDemo").trigger("reloadGrid", [{ current: true }]);
                        return [false, response.responseText];
                    }
                    else {
                        $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                        return [true, response.responseText];
                    }
                },
                delData: {
                    IntID: function () {
                        var value = new Array();
                        var cadena = '';
                        var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selarrrow');
                        if (sel_id.length) {
                            for (var i = 0; i < sel_id.length; i++) {
                                value[i] = $('#jQGridDemo').jqGrid('getCell', sel_id[i], 'IntIDGerenteRegion');
                                var cliente = jQuery('#jQGridDemo').jqGrid('getRowData', sel_id[i]);
                                cadena = cadena + cliente.VchObservacion;
                            }
                            if (cadena.trim() == '') {
                                alert('Debe completar por lo menos un campo Observacion con motivo de la eliminacion');
                                value = 0;
                                return value;
                            }
                        }
                        return value;
                    }
                }

            },
            {//SEARCH EVENTS AND PROPERTIES GOES HERE
                closeOnEscape: true
            }
        );


        // AGREGANDO BOTON EXPORTAR EXCEL
        $('#jQGridDemo').jqGrid('navButtonAdd', '#jQGridDemoPager',
            {
                caption: '<span class="ui-pg-button-text">Export</span>',
                buttonicon: "ui-icon-extlink",
                title: "Export To Excel",
                onClickButton: function () {
                    window.location = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGr.ashx?accion=export&pais=' + $('#<%=hfPais.ClientID%>').val();
            }
        }
        );



        function obtenerRegiones(codPais) {
            var url = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGr.ashx';
                var codPaisRegionporPeriodo = Ajax(url, { accion: 'loadRegion', 'pais': codPais }, false);
                //            $('#<%=hfValComboRegion.ClientID%>').val('');
                $('#<%=hfValComboRegion.ClientID%>').val(codPaisRegionporPeriodo);
                return codPaisRegionporPeriodo;
            }

            function cargarRegiones(mensaje) {
                alert(mensaje);
                $('#jQGridDemo').jqGrid('setColProp', 'VchCodigoRegion', { editoptions: { value: mensaje + ':' + mensaje } });
                //$('#jQGridDemo').jqGrid('setColProp', 'VchCodigoRegion', { editoptions: { value: $('#<%=hfValComboRegion.ClientID%>').val()} });
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
                failure: function (msg) {

                    rsp = -1;
                },
                error: function (request, status, error) {
                    alert(jQuery.parseJSON(request.responseText).Message);
                }
            });
            return rsp;
        }

    </script>

</asp:Content>
