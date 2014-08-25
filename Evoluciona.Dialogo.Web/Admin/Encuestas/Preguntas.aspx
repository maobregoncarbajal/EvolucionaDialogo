<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="Preguntas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Encuestas.Preguntas" %>

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
            url: '<%=Utils.RelativeWebRoot%>Admin/Encuestas/Preguntas.ashx?accion=load',
            datatype: "json",
            colNames: ['IdPregunta', 'Preguntas'],
            colModel: [
                { name: 'IdPregunta', index: 'IdPregunta', stype: 'text', editable: false, sorttype: 'int', hidden: true },
                { name: 'DesPreguntas', index: 'DesPreguntas', width: 1050, align: "left", sortable: false, editable: true }
            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            ignoreCase: true,
            rowList: [10, 20, 30],
            pager: '#jQGridDemoPager',
            sortname: 'IdPregunta',
            viewrecords: true,
            sortorder: "desc",
            caption: "Preguntas",
            editurl: '<%=Utils.RelativeWebRoot%>Admin/Encuestas/Preguntas.ashx',
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
                $('#jQGridDemo').setColProp('DesPreguntas', { editoptions: { dataInit: function (elem) { $(elem).width(900); } } });
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
                width: 1086,
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
                        var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'IdPregunta');
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
                width: 1086,
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
                        var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selarrrow');
                        if (sel_id.length) {
                            for (var i = 0; i < sel_id.length; i++) {
                                value[i] = $('#jQGridDemo').jqGrid('getCell', sel_id[i], 'IdPregunta');
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



    </script>

</asp:Content>
