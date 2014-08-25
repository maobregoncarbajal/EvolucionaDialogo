<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="alta_masi.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.alta_masi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
  
    <link href="../../Jscripts/jquery-ui-1.10.4.custom/css/ui-lightness/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Jscripts/JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    
   
    <script src="../../Jscripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="<%=Evoluciona.Dialogo.Web.Helpers.Utils.AbsoluteWebRoot%>Jscripts/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.js" type="text/javascript"></script>
    <script src="<%=Evoluciona.Dialogo.Web.Helpers.Utils.AbsoluteWebRoot%>Jscripts/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    
    <script src="<%=Evoluciona.Dialogo.Web.Helpers.Utils.AbsoluteWebRoot%>/Jscripts/Albama/albama.js" type="text/javascript"></script>
    <script src="<%=Evoluciona.Dialogo.Web.Helpers.Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery.jqGrid.js" type="text/javascript"></script>
    <script src="<%=Evoluciona.Dialogo.Web.Helpers.Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/grid.locale-en.js" type="text/javascript"></script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table>
        <tr>
            <td align="left"><p id="lblPais">País:</p></td>
            <td align="left"><select id="ddlPais"></select></td>
        </tr>
        <tr>
            <td align="left"><p id="lblRegion">Región:</p></td>
            <td align="left"><select id="ddlRegion"></select></td>
        </tr>
        <tr>
            <td align="left"><p id="lblZona">Zona:</p></td>
            <td align="left"><select id="ddlZona"></select></td>
        </tr>
        <tr>
            <td align="left"><p id="lblCargo">Cargo:</p></td>
            <td align="left">
                <table id="tblCargo" style="border: 0;" FRAME="void" RULES="rows">
                    <thead></thead>
                    <tbody></tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left"><p id="lblPeriodo">Periodo:</p></td>
            <td align="left"><input type="text" id="txtPeriodo" value=""/></td>
        </tr>
        <tr>
            <td align="left"><p id="lblEstCargo">Estado Cargo:</p></td>
            <td align="left">
                <table id="tblEstadoCargo" style="border: 0;" FRAME="void" RULES="rows">
                    <thead></thead>
                    <tbody></tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" >
                <input type="button" value="Consultar" id="btnConsultar"/>
            </td>
        </tr>
    </table>
    
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    
    <table id="jQGridDirectorio">
    </table>
    <div id="jQGridDirectorioPager">
    </div>
    <div id="dialog-alert" ></div>
    <asp:HiddenField ID="hfPais" runat="server" />
    <asp:HiddenField ID="hfValComboPais" runat="server" />
    <script type="text/javascript" language="javascript">

        var url = '../Altas_Bajas/albama.ashx';
        

        $(document).ready(function() {
            crearPopUp();
            comboPais();
            comboRegion();
            comboZona();
            cbCargo();
            cbEstadoCargo();

            $("#ddlPais").change(function() {
                comboRegion();
                comboZona();
            });

            $("#ddlRegion").change(function() {
                comboZona();
            });


            $(".cssCargo").children("input:checkbox").click(function(e) {
                if ($(this).val() != '') {
                    if ($(this).is(':checked')) {
                        $("#cbCargo").attr('checked', false);
                    }
                } else {
                    if ($(this).is(':checked')) {
                        $('input[name="cbCargo[]"]:checked').each(function(index, item) {
                            if ($(item).val() != '') {
                                $(item).attr('checked', false);
                            }
                        });
                    }
                }
            });



            $(".cssEstadoCargo").children("input:checkbox").click(function(e) {
                if ($(this).val() != '') {
                    if ($(this).is(':checked')) {
                        $("#cbEstadoCargo").attr('checked', false);
                    }
                } else {
                    if ($(this).is(':checked')) {
                        $('input[name="cbEstadoCargo[]"]:checked').each(function(index, item) {
                            if ($(item).val() != '') {
                                $(item).attr('checked', false);
                            }
                        });
                    }
                }
            });

            $("#btnConsultar").click(function() {
                consultar();
            });

        });

        function comboPais() {
            Albama.LoadDropDownList("ddlPais", url, { 'accion': 'loadPais', 'codPais': '00' }, 0, true, false);
        }

        function comboRegion() {
            var codPais = $("#ddlPais").val();

            if (codPais != '') {
                Albama.LoadDropDownList("ddlRegion", url, { 'accion': 'loadRegion', 'codPais': codPais }, 0, true, false);
            }else {
            $("#ddlRegion").empty();
            $('#ddlRegion').append('<option value="">[SELECCIONE]</option>');
            }
        }

        function comboZona() {
            var codPaisRegion = $("#ddlRegion").val();

            if (codPaisRegion != '') {

                var paRe = codPaisRegion.split("|");

                if (paRe[1] != '') {
                    Albama.LoadDropDownList("ddlZona", url, { 'accion': 'loadZona', 'codPaisRegion': codPaisRegion }, 0, true, false);
                }else {
                $("#ddlZona").empty();
                $('#ddlZona').append('<option value="' + codPaisRegion + '|">[TODOS]</option>');
                }
            }else {
            $("#ddlZona").empty();
            $('#ddlZona').append('<option value="">[SELECCIONE]</option>');
            }
        }

        function cbCargo() {
            $("#tblCargo thead").html("");
            $("#tblCargo tbody").html("");

            var tabla = "";
            tabla = tabla + "<tr><td><fieldset class='cssCargo'>";

            var paramCargo = { accion: 'loadCargo', 'codCargo': '00' };
            var listaCargo = Albama.Ajax(url, paramCargo, false);

            var cbCargo = "";

            $.each(listaCargo, function(o, w) {
            cbCargo = cbCargo + "<input type='checkbox' name='cbCargo[]' value='" + w.Codigo + "' id='cbCargo" + w.Codigo + "'>" + w.Descripcion + "<br>";
            });

            tabla = tabla + cbCargo;
            tabla = tabla + "</fieldset></td></tr>";
            $(tabla).appendTo("#tblCargo tbody");
        }

        function cbEstadoCargo() {
            $("#tblEstadoCargo thead").html("");
            $("#tblEstadoCargo tbody").html("");

            var tabla = "";
            tabla = tabla + "<tr><td><fieldset class='cssEstadoCargo'>";

            var paramCargo = { accion: 'loadEstadoCargo', 'codEstadoCargo': '00' };
            var listaCargo = Albama.Ajax(url, paramCargo, false);

            var cbCargo = "";

            $.each(listaCargo, function(o, w) {
            cbCargo = cbCargo + "<input type='checkbox' name='cbEstadoCargo[]' value='" + w.Codigo + "' id='cbEstadoCargo" + w.Codigo + "'>" + w.Descripcion + "<br>";
            });

            tabla = tabla + cbCargo;
            tabla = tabla + "</fieldset></td></tr>";
            $(tabla).appendTo("#tblEstadoCargo tbody");
        }



        function crearPopUp() {
            var arrayDialog = [
            { name: "dialog-alert", height: 100, width: 200, title: "Alerta" }
            ];

            Albama.CreateDialogs(arrayDialog);
        }


        function validarParametros() {

            var vari = false;
            var cbpais = $("#ddlPais").val();

            if (cbpais == '') {
                Albama.ShowAlert('dialog-alert', "Debe Seleccionar un País");
                return vari;
            }

            if ($(".cssCargo").children("input:checkbox:checked").length == 0) {
                Albama.ShowAlert('dialog-alert', "Debe Seleccionar un Cargo");
                return vari;
            }

            if ($(".cssEstadoCargo").children("input:checkbox:checked").length == 0) {
                Albama.ShowAlert('dialog-alert', "Debe Seleccionar un Estado");
                return vari;
            }

            vari = true;
            return vari;

        }

        function loadDirectorio() {

            var paisRegionZona = $("#ddlZona").val();

            var cargo = '';
            $('input[name="cbCargo[]"]:checked').each(function() {
                cargo += $(this).val() + ",";
            });

            cargo = cargo.substring(0, cargo.length - 1);

            var periodo = $("#txtPeriodo").val();

            var estadoCargo = '';
            $('input[name="cbEstadoCargo[]"]:checked').each(function() {
                estadoCargo += $(this).val() + ",";
            });

            estadoCargo = estadoCargo.substring(0, estadoCargo.length - 1);

            var parametros = { accion: 'loadDirectorio', paisRegionZona: paisRegionZona, cargo:cargo,periodo:periodo, estadoCargo: estadoCargo };
            var resultado = Albama.Ajax(url, parametros, false);

//            if (resultado.codigo == '0') {
//                return resultado.clienteDIRWebService;
//            }else {
//                Albama.ShowAlert('dialog-alert', resultado.mensaje);
//                resultado.clienteDIRWebService = [];
//                return resultado.clienteDIRWebService;
//            }

            return resultado;
        }

        function consultar() {

            if (validarParametros()) {

                var directorio = loadDirectorio();
                var mensaje = directorio.mensaje.split("|");
                var codigo = directorio.codigo.split("|");

                var tabla = "<table>";
                $.each(mensaje, function(i, v) {
                    tabla = tabla + "<tr>";
                    tabla = tabla + "<td>" + v + "</td>";
                    tabla = tabla + "</tr>";
                });
                tabla = tabla + "</table>";

                
                Albama.ShowAlert('dialog-alert', tabla);


                if (directorio.clienteDIRWebService.length > 0) {

                jQuery("#jQGridDirectorio").jqGrid({
                data: directorio.clienteDIRWebService,
                    datatype: "local",
                    colNames: ['idDirectorio', 'apeMat', 'apePat', 'cargo', 'codCargo', 'codCliente', 'codGrupoFuncional', 'codigoPeriodo', 'codNovedad', 'codPais', 'codPerfil', 'codPlanilla', 'codRegion', 'codRol', 'codZona', 'CUB', 'CUBJefe', 'desGrupoFuncional', 'desRegion', 'desRelContractual', 'desZona', 'estado', 'estadoCargo', 'fechaFin', 'fechaInicio', 'fecIngreso', 'genero', 'jefeDirecto', 'mailBelcorp', 'nombres', 'nroDoc', 'perfil', 'puestoOrg', 'rol', 'telefCasa', 'telefMovil', 'usuarioRed'],
                    colModel: [
                        { name: 'idDirectorio', index: 'idDirectorio', width: 100, stype: 'text', editable: false, sorttype: 'int', hidden: true },
                        { name: 'apeMat', index: 'apeMat', width: 40, editable: true, edittype: "select" },
                        { name: 'apePat', index: 'apePat', width: 120, align: "right", editable: true },
                        { name: 'cargo', index: 'cargo', width: 220, editable: true },
                        { name: 'codCargo', index: 'codCargo', width: 220, align: "left", sortable: false, editable: true },
                        { name: 'codCliente', index: 'codCliente', width: 120, align: "right", editable: true },
                        { name: 'codGrupoFuncional', index: 'codGrupoFuncional', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codigoPeriodo', index: 'codigoPeriodo', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codNovedad', index: 'codNovedad', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codPais', index: 'codPais', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codPerfil', index: 'codPerfil', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codPlanilla', index: 'codPlanilla', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codRegion', index: 'codRegion', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codRol', index: 'codRol', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'codZona', index: 'codZona', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'CUB', index: 'CUB', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'CUBJefe', index: 'CUBJefe', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'desGrupoFuncional', index: 'desGrupoFuncional', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'desRegion', index: 'desRegion', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'desRelContractual', index: 'desRelContractual', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'desZona', index: 'desZona', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'estado', index: 'estado', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'estadoCargo', index: 'estadoCargo', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'fechaFin', index: 'fechaFin', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'fechaInicio', index: 'fechaInicio', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'fecIngreso', index: 'fecIngreso', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'genero', index: 'genero', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'jefeDirecto', index: 'jefeDirecto', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'mailBelcorp', index: 'mailBelcorp', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'nombres', index: 'nombres', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'nroDoc', index: 'nroDoc', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'perfil', index: 'perfil', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'puestoOrg', index: 'puestoOrg', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'rol', index: 'rol', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'telefCasa', index: 'telefCasa', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'telefMovil', index: 'telefMovil', width: 120, align: "right", sortable: false, editable: true },
                        { name: 'usuarioRed', index: 'usuarioRed', width: 120, align: "right", sortable: false, editable: true }
                    ],
                    rowNum: 10,
                    mtype: 'GET',
                    loadonce: true,
                    rowList: [10, 20, 30],
                    pager: '#jQGridDirectorioPager',
                    sortname: 'intIDDirectoraVenta',
                    viewrecords: true,
                    sortorder: "desc",
                    caption: "Directorio FFVV",
                    editurl: url,
                    height: '100%',
                    multiselect: true,
                    onSelectRow: function(id) {
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
                    onSelectAll: function(id) {
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
                    loadComplete: function() {
                        $("#edit_" + this.id).addClass('ui-state-disabled');
                        //$('#jQGridDirectorio').setColProp('chrPrefijoIsoPais', { editoptions: { value: $('#<%=hfValComboPais.ClientID%>').val()} });
                    }
                });
                

//                $('#jQGridDirectorio').jqGrid('navGrid', '#jQGridDirectorioPager',
//                    {
//                        edit: true,
//                        add: true,
//                        del: true,
//                        search: true,
//                        searchtext: "Search",
//                        addtext: "Add",
//                        edittext: "Edit",
//                        deltext: "Delete"
//                    },
//                    {
////EDIT EVENTS AND PROPERTIES GOES HERE
//                    //EDIT
//                    //                       height: 300,
//                    //                       width: 400,
//                    //                       top: 50,
//                    //                       left: 100,
//                    //                       dataheight: 280,
//                        closeOnEscape: true, //Closes the popup on pressing escape key
//                        reloadAfterSubmit: true,
//                        drag: true,
//                        afterSubmit: function(response, postdata) {
//                            if (response.responseText == "") {

//                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
//                                return [true, ''];
//                            } else {
//                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
//                                return [false, response.responseText] //Captures and displays the response text on th Edit window
//                            }
//                        },
//                        editData: {
//                            IntID: function() {
//                                var sel_id = $('#jQGridDirectorio').jqGrid('getGridParam', 'selrow');
//                                var value = $('#jQGridDirectorio').jqGrid('getCell', sel_id, 'idDirectorio');
//                                return value;
//                            }
//                        },
//                        beforeSubmit: function(postdata, formid) {
//                            //more validations
//                            //                    if ($('#Nombres').val() == "") {
//                            //                        $('#Nombres').addClass("ui-state-highlight");
//                            //                        return [false, 'Nombre no puede estar vacio']; //error
//                            //                    }
//                            return [true, '']; // no error
//                        },
//                        viewPagerButtons: false
//                    },
//                    {
////ADD EVENTS AND PROPERTIES GOES HERE
//                        closeAfterAdd: true, //Closes the add window after add
//                        afterSubmit: function(response, postdata) {
//                            if (response.responseText == "") {

//                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid') //Reloads the grid after Add
//                                return [true, ''];
//                            } else {
//                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid') //Reloads the grid after Add
//                                return [false, response.responseText];
//                            }
//                        }
//                    },
//                    {
////DELETE EVENTS AND PROPERTIES GOES HERE
//                    //DELETE
//                        closeOnEscape: true,
//                        closeAfterDelete: true,
//                        reloadAfterSubmit: true,
//                        closeOnEscape: true,
//                        drag: true,
//                        afterSubmit: function(response, postdata) {
//                            if (response.responseText == "") {

//                                $("#jQGridDirectorio").trigger("reloadGrid", [{ current: true }]);
//                                return [false, response.responseText];
//                            } else {
//                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
//                                return [true, response.responseText];
//                            }
//                        },
//                        delData: {
//                            IntID: function() {
//                                //                            var value = new Array();
//                                //                            var cadena = '';
//                                var sel_id = $('#jQGridDirectorio').jqGrid('getGridParam', 'selarrrow');
//                                if (sel_id.length) {
//                                    for (var i = 0; i < sel_id.length; i++) {
//                                        //                                    value[i] = $('#jQGridDirectorio').jqGrid('getCell', sel_id[i], 'idDirectorio');
//                                        var cliente = jQuery('#jQGridDirectorio').jqGrid('getRowData', sel_id[i]);
//                                        //                                    cadena = cadena + cliente.vchObservacion;
//                                    }
//                                    //                                if (cadena.trim() == '') {
//                                    //                                    alert('Debe completar por lo menos un campo Observacion con motivo de la eliminacion');
//                                    //                                    value = 0;
//                                    //                                    return value;

//                                    //                                }
//                                }
//                                //                            return value;
//                            }
//                        }
//                    },
//                    {
////SEARCH EVENTS AND PROPERTIES GOES HERE
//                        closeOnEscape: true
//                    }
//                );

//                // AGREGANDO BOTON EXPORTAR EXCEL
//                $('#jQGridDirectorio').jqGrid('navButtonAdd', '#jQGridDirectorioPager',
//                    {
//                        caption: '<span class="ui-pg-button-text">Export</span>',
//                        buttonicon: "ui-icon-extlink",
//                        title: "Export To Excel",
//                        onClickButton: function() {
//                            window.location = '/Admin/Altas_Bajas/MantUsuarioDv.ashx?accion=export&pais=' + $('#<%=hfPais.ClientID%>').val();
//                        }
//                    }
//                );
                    
                    
                }

            }
        }

    </script>
</asp:Content>
