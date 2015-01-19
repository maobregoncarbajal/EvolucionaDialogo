<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="MantDv.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.MantDv" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../Jscripts/jquery-ui-1.11.0.custom/jquery-ui.css" rel="stylesheet" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>/Jscripts/jquery-ui-1.11.0.custom/jquery-ui.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.jqGrid-4.6.0/src/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../../Jscripts/jquery.jqGrid-4.6.0/src/css/ui.jqgrid.css" rel="stylesheet" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.jqGrid-4.6.0/src/i18n/grid.locale-es.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Evoluciona.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="jQGridDemo">
    </table>
    <div id="jQGridDemoPager">
    </div>
    <asp:HiddenField ID="hfPais" runat="server" />
    <asp:HiddenField ID="hfValComboPais" runat="server" />

    <script type="text/javascript">
        jQuery("#jQGridDemo").jqGrid({
            url: '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioDv.ashx?accion=load&pais=' + $('#<%=hfPais.ClientID%>').val(),
            datatype: "json",
            colNames: ['intIDDirectoraVenta', 'Pais', 'C. Directora Ventas', 'Nombre Completo', 'Correo Electronico', 'CUB', 'C. Planilla', 'Observacion'],
            colModel: [
            { name: 'intIDDirectoraVenta', index: 'intIDDirectoraVenta', width: 100, stype: 'text', editable: false, sorttype: 'int', hidden: true },
            { name: 'chrPrefijoIsoPais', index: 'chrPrefijoIsoPais', width: 40, editable: true, edittype: "select", searchoptions: { sopt: ['cn', 'bw'] } },
            { name: 'chrCodigoDirectoraVentas', index: 'chrCodigoDirectoraVentas', width: 120, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
            { name: 'vchNombreCompleto', index: 'vchNombreCompleto', width: 220, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
            { name: 'vchCorreoElectronico', index: 'vchCorreoElectronico', width: 220, align: "left", sortable: false, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
            { name: 'vchCUBDV', index: 'vchCUBDV', width: 120, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
            { name: 'chrCodigoPlanilla', index: 'chrCodigoPlanilla', width: 120, align: "right", sortable: false, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
            { name: 'vchObservacion', index: 'vchObservacion', width: 120, align: "right", sortable: false, editable: true, searchoptions: { sopt: ['cn', 'bw'] } }
            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            ignoreCase: true,
            rowList: [10, 20, 30],
            pager: '#jQGridDemoPager',
            sortname: 'intIDDirectoraVenta',
            viewrecords: true,
            sortorder: "desc",
            caption: "Directora Ventas",
            editurl: '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioDv.ashx',
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
                $('#jQGridDemo').setColProp('chrPrefijoIsoPais', { editoptions: { value: $('#<%=hfValComboPais.ClientID%>').val() } });
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
                        var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'intIDDirectoraVenta');
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
                            var cadena = '';
                            var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selarrrow');
                            if (sel_id.length) {
                                for (var i = 0; i < sel_id.length; i++) {
                                    value[i] = $('#jQGridDemo').jqGrid('getCell', sel_id[i], 'intIDDirectoraVenta');
                                    var cliente = jQuery('#jQGridDemo').jqGrid('getRowData', sel_id[i]);
                                    cadena = cadena + cliente.vchObservacion;
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
        
    $('#jQGridDemo').jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

        // AGREGANDO BOTON EXPORTAR EXCEL
        $('#jQGridDemo').jqGrid('navButtonAdd', '#jQGridDemoPager',
               {
                   caption: '<span class="ui-pg-button-text">Export</span>',
                   buttonicon: "ui-icon-extlink",
                   title: "Export To Excel",
                   onClickButton: function () {
                       window.location = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioDv.ashx?accion=export&pais=' + $('#<%=hfPais.ClientID%>').val();
                   }
               }
        );



    </script>

</asp:Content>
