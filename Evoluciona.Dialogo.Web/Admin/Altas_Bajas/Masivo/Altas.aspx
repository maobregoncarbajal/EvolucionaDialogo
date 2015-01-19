<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Altas.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.Masivo.Altas" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../../Jscripts/jquery-ui-1.11.0.custom/jquery-ui.css" rel="stylesheet" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>/Jscripts/jquery-ui-1.11.0.custom/jquery-ui.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.jqGrid-4.6.0/src/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../../../Jscripts/jquery.jqGrid-4.6.0/src/css/ui.jqgrid.css" rel="stylesheet" />
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
    <div id="dialogOk" title="Alerta" style="display:none">
        <p>Las altas masivas se ejecutaron correctamente</p>
    </div>
    <div id="dialogError" title="Alerta" style="display:none">
        <p>Hubo un error al realizar las altas masivas.</p>
    </div>
    <script type="text/javascript">
        

        var idsOfSelectedRows = [],
                updateIdsOfSelectedRows = function (id, isSelected) {
                    var index = $.inArray(id, idsOfSelectedRows);
                    if (!isSelected && index >= 0) {
                        idsOfSelectedRows.splice(index, 1); // remove id from the list
                    } else if (index < 0) {
                        idsOfSelectedRows.push(id);
                    }
                };

        //idsOfSelectedRows = ["8", "9", "10"];



        jQuery("#jQGridDemo").jqGrid({
            url: '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/Masivo/Altas.ashx?accion=load&pais=' + $('#<%=hfPais.ClientID%>').val(),
            datatype: "json",
            colNames: ['Rol', 'Cod. Pais Comercial', 'Nombre Completo', 'Mail', 'CUB','DirCodRegion', 'DircodZona','Doc Identidad', 'Cod. Planilla', 'Pais', 'Cod. Region', 'Cod. Zona'],
            colModel: [
                { name: 'CodCargo', index: 'CodCargo', width: 50, stype: 'text', editable: true },
                { name: 'CodigoPaisComercial', index: 'CodigoPaisComercial', width: 120, align: "right", editable: true, hidden: true },
                { name: 'Nombres', index: 'Nombres', width: 200, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'MailBelcorp', index: 'MailBelcorp', width: 150, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'Cub', index: 'Cub', width: 120, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'DirCodRegion', index: 'DirCodRegion', width: 120, align: "right", editable: true, hidden: true },
                { name: 'DircodZona', index: 'DircodZona', width: 120, align: "right", editable: true, hidden: true },
                { name: 'NroDoc', index: 'NroDoc', width: 120, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'CodPlanilla', index: 'CodPlanilla', width: 120, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'PrefijoIsoPais', index: 'PrefijoIsoPais', width: 50, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'CodRegion', index: 'CodRegion', width: 75, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'CodZona', index: 'CodZona', width: 75, align: "right", editable: true, searchoptions: { sopt: ['cn', 'bw'] } }
            ],
            rowNum: 20,
            mtype: 'GET',
            loadonce: true,
            ignoreCase: true,
            rowList: [20, 50, 100],
            pager: '#jQGridDemoPager',
            sortname: 'Nombres',
            viewrecords: true,
            sortorder: "desc",
            caption: "Altas Masivas",
            height: '100%',
            multiselect: true,
            onSelectRow: updateIdsOfSelectedRows,
            onSelectAll: function (aRowids, isSelected) {
                var i, count, id;
                for (i = 0, count = aRowids.length; i < count; i++) {
                    id = aRowids[i];
                    updateIdsOfSelectedRows(id, isSelected);
                }
            },
            loadComplete: function () {
                var $this = $(this), i, count;
                for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
                    $this.jqGrid('setSelection', idsOfSelectedRows[i], false);
                }
            }

        });


        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
            {
                edit: false,
                add: false,
                del: false,
                search: false,
                refreshtext:'Recargar datos',
                beforeRefresh: function () {
                    idsOfSelectedRows = [];
                    jQuery("#jQGridDemo").jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                }
            },
            {
                //EDIT EVENTS AND PROPERTIES GOES HERE
                },
            {
                //ADD EVENTS AND PROPERTIES GOES HERE
                },
            {//DELETE EVENTS AND PROPERTIES GOES HERE
                },
            {//SEARCH EVENTS AND PROPERTIES GOES HERE
                closeOnEscape: true
            }
        );

        $('#jQGridDemo').jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

        // AGREGANDO BOTON EJECUTAR ALTAS
        $('#jQGridDemo').jqGrid('navButtonAdd', '#jQGridDemoPager',
            {
                caption: '<span class="ui-pg-button-text">Ejecutar Alta</span>',
                buttonicon: "ui-icon-extlink",
                title: "Ejecutar alta",
                onClickButton: function () {
                    var list = [];
                       
                    //var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selarrrow');
                    var sel_id = idsOfSelectedRows;
                    
                    if (sel_id.length) {

                        for (var i = 0; i < sel_id.length; i++) {
                            //var cliente = jQuery('#jQGridDemo').jqGrid('getRowData', sel_id[i]);
                            var param = $('#jQGridDemo').jqGrid('getGridParam', 'data');
                            $.each(param, function (x, v) {
                                if (v._id_ == sel_id[i]) {
                                    list.push(v);
                                }
                            });
                            
                        }

                        var jsonList = JSON.stringify(list);
                        $.post('<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/Masivo/Altas.ashx', { accion: "altas", listaAltas: jsonList })
                            .done(function(respuesta) {
                                if (respuesta == 'true') {
                                    idsOfSelectedRows = [];
                                    jQuery("#jQGridDemo").jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                                    $("#dialogOk").dialog();
                                } else {
                                    idsOfSelectedRows = [];
                                    jQuery("#jQGridDemo").jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                                    $("#dialogError").dialog();
                                }
                            });

                    } else {
                           
                        var idSelector = "#alertmod_" + this.p.id;
                        $.jgrid.viewModal(idSelector, {
                            gbox: "#gbox_" + $.jgrid.jqID(this.p.id),
                            jqm: true
                        });
                        $(idSelector).position({
                            of: "#" + $.jgrid.jqID(this.p.id),
                            at: "center",
                            my: "center"
                        });
                        $(idSelector).find(".ui-jqdialog-titlebar-close").focus();

                    }
                       

                }
            }
        );

        // AGREGANDO BOTON EXPORTAR EXCEL
        $('#jQGridDemo').jqGrid('navButtonAdd', '#jQGridDemoPager',
            {
                caption: '<span class="ui-pg-button-text">Exportar a Excel</span>',
                buttonicon: "ui-icon-extlink",
                title: "Exportar a Excel",
                onClickButton: function () {
                    window.location = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/Masivo/Altas.ashx?accion=export&pais=' + $('#<%=hfPais.ClientID%>').val();
                }
            }
        );

    </script>
</asp:Content>
