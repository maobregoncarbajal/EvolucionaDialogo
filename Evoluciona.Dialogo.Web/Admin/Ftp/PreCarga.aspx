<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="PreCarga.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Ftp.PreCarga" %>

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

    <script type="text/javascript">
        jQuery("#jQGridDemo").jqGrid({
            url: '<%=Utils.RelativeWebRoot%>Admin/Ftp/JQGridHandler.ashx?accion=load',
            datatype: "json",
            colNames: ['Id', 'CUB', 'D.Identidad', 'C.Planilla', 'C.Rol', 'D.Rol', 'Nombres', 'A.Paterno', 'A.Materno', 'Pais', 'Region', 'Zona', 'Email', 'Sexo', 'Estado', 'C.CUB Jefe', 'C.Planilla Jefe'],
            colModel: [
            { name: '_id', index: '_id', width: 30, stype: 'text', editable: false, sorttype: 'int' },
            { name: 'Cub', index: 'Cub', width: 80, editable: true },
            { name: 'DocIdentidad', index: 'DocIdentidad', width: 80, editable: true },
            { name: 'Planilla', index: 'Planilla', width: 80, editable: true },
            { name: 'CodRol', index: 'CodRol', width: 35, sortable: false, editable: true },
            { name: 'DesRol', index: 'DesRol', width: 35, align: "left", editable: true },
            { name: 'Nombres', index: 'Nombres', width: 80, align: "left", editable: true },
            { name: 'ApePaterno', index: 'ApePaterno', width: 80, sortable: false, editable: true },
            { name: 'ApeMaterno', index: 'ApeMaterno', width: 80, sortable: false, editable: true },
            { name: 'CodPais', index: 'CodPais', width: 35, sortable: false, editable: true },
            { name: 'CodRegion', index: 'CodRegion', width: 35, sortable: false, editable: true },
            { name: 'CodZona', index: 'CodZona', width: 35, sortable: false, editable: true },
            { name: 'Email', index: 'Email', width: 80, sortable: false, editable: true },
            { name: 'Sexo', index: 'Sexo', width: 35, sortable: false, editable: true },
            { name: 'Estado', index: 'Estado', width: 35, sortable: false, editable: true },
            { name: 'CubJefe', index: 'CubJefe', width: 80, sortable: false, editable: true },
            { name: 'PlanillaJefe', index: 'PlanillaJefe', width: 80, sortable: false, editable: true }
            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            rowList: [10, 20, 30],
            pager: '#jQGridDemoPager',
            sortname: '_id',
            viewrecords: true,
            sortorder: "desc",
            caption: "FFVV_base_evoluciona.csv",
            editurl: '<%=Utils.RelativeWebRoot%>Admin/Ftp/JQGridHandler.ashx',
            height: '100%',
            multiselect: true,
            onSelectRow: function (id) {
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
                        var value = $('#jQGridDemo').jqGrid('getCell', sel_id, '_id');
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
                    closeOnEscape: true,
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
                            var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selarrrow');
                            if (sel_id.length) {
                                for (var i = 0; i < sel_id.length; i++) {
                                    value[i] = $('#jQGridDemo').jqGrid('getCell', sel_id[i], '_id');
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
                       window.location = '<%=Utils.RelativeWebRoot%>Admin/Ftp/JQGridHandler.ashx?accion=export';
                   }
               }
        );

                // AGREGANDO BOTON SINCRONIZAR
                $('#jQGridDemo').jqGrid('navButtonAdd', '#jQGridDemoPager',
                       {
                           caption: '<span class="ui-pg-button-text">Upload</span>',
                           buttonicon: "ui-icon-arrowthick-1-n",
                           title: "Upload",
                           onClickButton: function () {
                               if (confirm("Esta seguro de realizar esta operación?")) {
                                   window.location = '<%=Utils.RelativeWebRoot%>Admin/Ftp/JQGridHandler.ashx?accion=sinc';
                               }

                           }
                       }
        );

    </script>

</asp:Content>
