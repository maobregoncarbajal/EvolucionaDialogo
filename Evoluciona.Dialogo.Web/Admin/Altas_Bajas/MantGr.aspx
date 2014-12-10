<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="MantGr.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.MantGr" %>

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
    <br />
    <table id="list">
        <tr>
            <td></td>
        </tr>
    </table>
    <div id="pager"></div>
    <asp:HiddenField ID="hfPais" runat="server" />
    <asp:HiddenField ID="hfUsuario" runat="server" />

    <script id="MantGr" type="text/javascript">

        var url = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGr.ashx';
        var grid = $("#list");
        var inEdit;
        var pais = $("#<%=hfPais.ClientID %>").val();
        var paisSearch = "";
        var regionSearch = "";//fff
        var listPaises = funPaises();
        var lastSel = -1;

        var paisEdit = "";
        var regionEdit = "";

        var codGrEdit = "";
        var cubGrEdit = "";


        var validaNombre = function (value, colname) {
            if (value.length < 1) {
                return [false, "Nombre no puede estar vacio"];
            } else {
                return [true, ""];
            }
        };

        var validaCodGr = function (value, colname) {

            var varPais = $("select#ChrPrefijoIsoPais").val();
            var varRegion = $("select#VchCodigoRegion").val();


            if (value != codGrEdit && value != "") {

                var paramValidaCodGr = { accion: 'validaCodGr', 'pais': varPais, 'region': varRegion, 'codGr': value };
                var cantCodGr = Evoluciona.Ajax(url, paramValidaCodGr, false);

                if (cantCodGr > 0) {
                    return [false, 'Ya existe el Doc. Identidad'];
                }
            } else {
                if (value == "") {
                    return [false, 'Doc. Identidad no puede estar vacio'];
                } else {
                    return [true, ""];
                }
            }

            return [true, '']; // no error
        };

        var validaCubGr = function (value, colname) {

            var varPais = $("select#ChrPrefijoIsoPais").val();
            var varRegion = $("select#VchCodigoRegion").val();


            if (value != cubGrEdit) {

                var paramValidaCub = { accion: 'validaCub', 'pais': varPais, 'region': varRegion, 'cub': value };
                var cantCub = Evoluciona.Ajax(url, paramValidaCub, false);

                if (cantCub > 0) {
                    return [false, 'Ya existe el CUB'];
                }

            } else {
                return [true, ""];
            }

            return [true, '']; // no error
        };


        jQuery(document).ready(function () {


            var editGridRowOptions = {
                recreateForm: true
                , beforeInitData: function () { inEdit = true; }
                , viewPagerButtons: false
                , closeOnEscape: true
                , reloadAfterSubmit: true
                , closeAfterEdit: true
                , drag: true
                , onClose: function () {
                    //paisEdit = "";
                    //regionEdit = "";
                    //codGzEdit = "";
                    //cubGzEdit = "";
                }
                , afterSubmit: function (response, postdata) {
                    if (response.responseText == "") {
                        grid.trigger("reloadGrid", [{ current: true }]);
                        return [false, response.responseText];
                    }
                    else {
                        grid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                        return [true, response.responseText];
                    }
                }
            };


            var addGridRowOptions = {
                recreateForm: true,
                beforeInitData: function () { inEdit = false; },
                afterSubmit: function (response) {
                    if (response.responseText == "") {
                        grid.jqGrid('setGridParam',
                          { datatype: 'json' }).trigger('reloadGrid');//Reloads the grid after edit
                        return [true, ''];
                    }
                    else {
                        grid.jqGrid('setGridParam',
                          { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                        return [false, response.responseText];
                        //Captures and displays the response text on th Edit window
                    }
                },
                closeOnEscape: true,
                reloadAfterSubmit: true,
                closeAfterAdd: true,
                drag: true,
                beforeSubmit: function () {
                    //validations
                    var _pai = $('#ChrPrefijoIsoPais');
                    var _reg = $('#VchCodigoRegion');
                    var _nom = $('#VchNombrecompleto');
                    var _cgr = $('#ChrCodigoGerenteRegion');
                    var _cub = $('#VchCUBGR');

                    if (_nom.val() == "") {
                        _nom.addClass("ui-state-highlight");
                        return [false, 'Nombre no puede estar vacio']; //error
                    }

                    if (_cgr.val() == "") {
                        _cgr.addClass("ui-state-highlight");
                        return [false, 'Doc. Identidad no puede estar vacio']; //error
                    } else {
                        var paramValidaCodGr = { accion: 'validaCodGr', 'pais': _pai.val(), 'region': _reg.val(), 'codGr': _cgr.val() };
                        var cantCodGr = Evoluciona.Ajax(url, paramValidaCodGr, false);

                        if (cantCodGr > 0) {
                            return [false, 'Ya existe el Doc. Identidad'];
                        }
                    }

                    if (_cub.val() == "") {
                        _cub.addClass("ui-state-highlight");
                        return [false, 'CUB no puede estar vacio']; //error
                    } else {
                        var paramValidaCub = { accion: 'validaCub', 'pais': _pai.val(), 'region': _reg.val(), 'cub': _cub.val() };
                        var cantCub = Evoluciona.Ajax(url, paramValidaCub, false);

                        if (cantCub > 0) {
                            return [false, 'Ya existe el CUB'];
                        }
                    }

                    return [true, '']; // no error
                }
                , onClose: function () {
                    //paisEdit = "";
                    //regionEdit = "";
                    //codGzEdit = "";
                    //cubGzEdit = "";
                }
            };


            var delGridRowOptions = {
                closeAfterDelete: true,
                reloadAfterSubmit: true,
                closeOnEscape: true,
                drag: true,
                afterSubmit: function (response, postdata) {
                    if (response.responseText == "") {
                        grid.trigger("reloadGrid", [{ current: true }]);
                        return [false, response.responseText];
                    }
                    else {
                        grid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                        return [true, response.responseText];
                    }
                },
                delData: {
                    IntID: function () {
                        var selId = grid.jqGrid('getGridParam', 'selrow');
                        var value = grid.jqGrid('getCell', selId, 'IntIDGerenteRegion');
                        return value;
                    }
                }
            };




            grid.jqGrid({
                colNames: [
                    'IntIDGerenteRegion'
                    , 'País'
                    , 'Doc. Identidad'
                    , 'Nombre Completo'
                    , 'Correo Electrónico'
                    , 'CUB'
                    , 'C. Planilla'
                    , 'C. Region'
                    , 'Directora Venta'
                    , 'Observación'
                ],
                colModel: [
                    { name: 'IntIDGerenteRegion', index: 'IntIDGerenteRegion', stype: 'text', editable: true, sorttype: 'int', hidden: true },
                {
                    name: 'ChrPrefijoIsoPais',
                    index: 'ChrPrefijoIsoPais',
                    width: 60,
                    align: "center",
                    sortable: true,
                    editable: true,
                    edittype: 'select',
                    stype: 'select',
                    editoptions: {
                        value: listPaises,
                        dataInit: function (elem) {
                            paisEdit = $(elem).val();
                            funRegiones(paisEdit);
                        },
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function (e) {
                                    paisEdit = $(e.target).val();
                                    funCambiarRegionEdit(paisEdit);
                                }
                            }
                        ],
                    },
                    searchoptions: {
                        value: $.extend({ "": "Todos" }, listPaises),
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function (e) {
                                    paisSearch = $(e.target).val();
                                    funCambiarRegionSearch(paisSearch);
                                }
                            }
                        ],
                    }
                },
                    {
                        name: 'ChrCodigoGerenteRegion', index: 'ChrCodigoGerenteRegion', align: "right", sortable: false, width: 140,
                        editable: true, editrules: { custom: true, custom_func: validaCodGr }
                    },
                    {
                        name: 'VchNombrecompleto', index: 'VchNombrecompleto', align: "left", width: 165, sortable: true,
                        editable: true, editrules: { custom: true, custom_func: validaNombre }
                    },
                    { name: 'VchCorreoElectronico', index: 'VchCorreoElectronico', width: 120, align: "left", sortable: false, editable: true },
                    {
                        name: 'VchCUBGR', index: 'VchCUBGR', width: 140, align: "right", sortable: false,
                        editable: true, editrules: { custom: true, custom_func: validaCubGr }
                    },
                    { name: 'ChrCodigoPlanilla', index: 'ChrCodigoPlanilla', width: 80, align: "right", sortable: false, editable: true },
                    {
                        name: 'VchCodigoRegion',
                        index: 'VchCodigoRegion',
                        width: 60,
                        align: "right",
                        sortable: true,
                        editable: true,
                        edittype: 'select',
                        stype: 'select',
                        searchoptions: {
                            value: { "": "Todos" }
                        }
                    },
                    { name: 'obeDirectoraVentas.vchNombreCompleto', index: 'obeDirectoraVentas.vchNombreCompleto', width: 165, align: "left", sortable: true, editable: false },
                    { name: 'VchObservacion', index: 'VchObservacion', width: 90, align: "right", sortable: false, editable: true }
                ],
                onSelectRow: function (id) {
                    paisEdit = grid.jqGrid('getCell', id, 'ChrPrefijoIsoPais');
                    regionEdit = grid.jqGrid('getCell', id, 'VchCodigoRegion');
                    codGrEdit = grid.jqGrid('getCell', id, 'ChrCodigoGerenteRegion');
                    cubGrEdit = grid.jqGrid('getCell', id, 'VchCUBGR');

                },
                url: url + '?accion=load&pais=' + pais,
                datatype: 'json',
                mtype: 'GET',
                loadonce: true,
                editurl: url,
                rowNum: 15,
                ignoreCase: true,
                rowList: [15, 30, 45],
                pager: '#pager',
                viewrecords: true,
                sortorder: "desc",
                caption: "Mantenimiento de Gerentes de Región",
                height: '100%',
                width: 1100,
                forceFit: false,
                shrinkToFit: false,
                autowidth: false,
                fixed: false,
                rownumbers: true
            });

            grid.jqGrid('navGrid', '#pager', { edit: true, add: true, del: true, search: false }, editGridRowOptions, addGridRowOptions, delGridRowOptions);
            grid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });

            // AGREGANDO BOTON EXPORTAR EXCEL
            grid.jqGrid('navButtonAdd', '#pager',
                {
                    caption: '<span class="ui-pg-button-text"></span>',
                    buttonicon: "ui-icon-extlink",
                    title: "Exportar a Excel",
                    onClickButton: function () {
                        window.location = url + '?accion=export&pais=' + pais;
                    }
                }
            );

        });



        function funPaises() {
            var parametrosPais = { accion: 'loadPaises', 'pais': pais };
            var listObjPais = Evoluciona.Ajax(url, parametrosPais, false);
            var paises = {};

            $.each(listObjPais, function (i, v) {
                paises[v.Codigo] = v.Codigo;
            });

            return paises;
        }

        function funRegiones(varPaisEdit) {
            grid.jqGrid('setColProp', 'VchCodigoRegion', {
                editoptions: {
                    dataUrl: url + '?accion=loadRegiones&pais=' + varPaisEdit,
                    buildSelect: function (data) {
                        var s = '<select id="VchCodigoRegion">';
                        var listData = JSON.parse(data);
                        $.each(listData, function (index, value) {
                            s += "<option value='" + value.CodRegion + "'>" + value.CodRegion + "</option>";
                        });

                        return s + "</select>";
                    }
                }
            });
        }

        function funCambiarRegionSearch(varPaisSearch) {
            var parametrosRegion = { accion: 'loadRegiones', 'pais': varPaisSearch };
            var listObjRegion = Evoluciona.Ajax(url, parametrosRegion, false);
            var newOptions = "<option value=\"\">Todos</option>";


            $.each(listObjRegion, function (i, v) {
                newOptions += "<option role=\"option\" value=\"" + v.CodRegion + "\">" + v.CodRegion + "</option>";
            });

            $('#gs_VchCodigoRegion').find('option').remove().end();
            $('#gs_VchCodigoRegion').html(newOptions);

        }

        function funCambiarRegionEdit(varPaisEdit) {
            var parametrosRegion = { accion: 'loadRegiones', 'pais': varPaisEdit };
            var listObjRegion = Evoluciona.Ajax(url, parametrosRegion, false);
            var newOptions = "";


            $.each(listObjRegion, function (i, v) {
                newOptions += "<option role=\"option\" value=\"" + v.CodRegion + "\">" + v.CodRegion + "</option>";
            });

            $('select#VchCodigoRegion.FormElement').find('option').remove().end();
            $('select#VchCodigoRegion.FormElement').html(newOptions);


            regionEdit = $('select#VchCodigoRegion.FormElement').val();

        }


        function funLimpiarRegionSearch() {

            var newOptions = "<option value=\"\">Todos</option>";
            $('#gs_VchCodigoRegion').find('option').remove().end();
            $('#gs_VchCodigoRegion').html(newOptions);

        }

    </script>
</asp:Content>
