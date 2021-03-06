﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="MantGz.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.MantGz" %>

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

    <script id="MantGz" type="text/javascript">

        var url = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGz.ashx';
        var grid = $("#list");
        var inEdit;
        var pais = $("#<%=hfPais.ClientID %>").val();
        var paisSearch = "";
        var regionSearch = "";
        var listPaises = funPaises();
        var lastSel = -1;

        var paisEdit = "";
        var regionEdit = "";

        var codGzEdit = "";
        var cubGzEdit = "";


        var validaNombre = function (value, colname) {
            if (value.length < 1) {
                return [false, "Nombre no puede estar vacio"];
            } else {
                return [true, ""];
            }
        };

        var validaCodGz = function (value, colname) {

            var varPais = $("select#chrPrefijoIsoPais").val();
            var varRegion = $("select#vchCodigoRegion").val();
            var varZona = $("select#vchCodigoZona").val();

            if (value != codGzEdit && value != "") {

                var paramValidaCodGz = { accion: 'validaCodGz', 'pais': varPais, 'region': varRegion, 'zona': varZona, 'codGz': value };
                var cantCodGz = Evoluciona.Ajax(url, paramValidaCodGz, false);

                if (cantCodGz > 0) {
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

        var validaCubGz = function (value, colname) {

            var varPais = $("select#chrPrefijoIsoPais").val();
            var varRegion = $("select#vchCodigoRegion").val();
            var varZona = $("select#vchCodigoZona").val();


            if (value != cubGzEdit) {

                var paramValidaCub = { accion: 'validaCub', 'pais': varPais, 'region': varRegion, 'zona': varZona, 'cub': value };
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

                    //$("#FormError").find(".ui-state-error").removeClass("ui-state-error").addClass("ui-state-highlight");
                    //$("#FormError").find(".ui-state-highlight").removeClass("ui-state-highlight").addClass("ui-state-error");

                    if (response.responseText == "") {
                        grid.jqGrid('setGridParam',
                          { datatype: 'json' }).trigger('reloadGrid');//Reloads the grid after edit
                        return [true, ''];
                    }
                    else {
                        grid.jqGrid('setGridParam',
                          { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                        //return [false, myInfo];
                        return [false, response.responseText];
                        //Captures and displays the response text on th Edit window
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
                    var _pai = $('#chrPrefijoIsoPais');
                    var _reg = $('#vchCodigoRegion');
                    var _zon = $('#vchCodigoZona');
                    var _nom = $('#vchNombreCompleto');
                    var _cgz = $('#chrCodigoGerenteZona');
                    var _cub = $('#vchCUBGZ');

                    if (_nom.val() == "") {
                        _nom.addClass("ui-state-highlight");
                        return [false, 'Nombre no puede estar vacio']; //error
                    }

                    if (_cgz.val() == "") {
                        _cgz.addClass("ui-state-highlight");
                        return [false, 'Doc. Identidad no puede estar vacio']; //error
                    } else {
                        var paramValidaCodGz = { accion: 'validaCodGz', 'pais': _pai.val(), 'region': _reg.val(), 'zona': _zon.val(), 'codGz': _cgz.val() };
                        var cantCodGz = Evoluciona.Ajax(url, paramValidaCodGz, false);

                        if (cantCodGz > 0) {
                            return [false, 'Ya existe el Doc. Identidad'];
                        }
                    }

                    if (_cub.val() == "") {
                        _cub.addClass("ui-state-highlight");
                        return [false, 'CUB no puede estar vacio']; //error
                    } else {
                        var paramValidaCub = { accion: 'validaCub', 'pais': _pai.val(), 'region': _reg.val(), 'zona': _zon.val(), 'cub': _cub.val() };
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
                        var value = grid.jqGrid('getCell', selId, 'intIDGerenteZona');
                        return value;
                    }
                }
            };




            grid.jqGrid({
                colNames: ['intIDGerenteZona', 'ID GerenteRegion ', 'País', 'Doc. Identidad', 'Nombre Completo', 'Correo Electrónico', 'CUB', 'C. Planilla', 'C. Región', 'C. Zona', 'Gerente Región', 'Observación'],
                colModel: [
                    { name: 'intIDGerenteZona', index: 'intIDGerenteZona', stype: 'text', editable: true, sorttype: 'int', hidden: true },
                    { name: 'intIDGerenteRegion', index: 'intIDGerenteRegion', width: 100, align: "right", editable: true, hidden: true },
                {
                    name: 'chrPrefijoIsoPais',
                    index: 'chrPrefijoIsoPais',
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
                                    funCambiarZonaEdit(paisEdit, regionEdit);
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
                                    funLimpiarZonaSearch();

                                }
                            }
                        ],
                    }
                },
                    {
                        name: 'chrCodigoGerenteZona', index: 'chrCodigoGerenteZona', align: "right", sortable: false, width: 140
                        , editable: true, editrules: { custom: true, custom_func: validaCodGz }
                        , searchoptions: { sopt: ['cn', 'bw'] }
                    },
                    {
                        name: 'vchNombreCompleto', index: 'vchNombreCompleto', align: "left", width: 165, sortable: true
                        , editable: true, editrules: { custom: true, custom_func: validaNombre }
                        , searchoptions: { sopt: ['cn', 'bw'] }
                    },
                    { name: 'vchCorreoElectronico', index: 'vchCorreoElectronico', width: 120, align: "left", sortable: false, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                    {
                        name: 'vchCUBGZ', index: 'vchCUBGZ', width: 140, align: "right", sortable: false
                        , editable: true, editrules: { custom: true, custom_func: validaCubGz }
                        , searchoptions: { sopt: ['cn', 'bw'] }
                    },
                    { name: 'chrCodigoPlanilla', index: 'chrCodigoPlanilla', width: 80, align: "right", sortable: false, editable: true, searchoptions: { sopt: ['cn', 'bw'] } },
                {
                    name: 'vchCodigoRegion',
                    index: 'vchCodigoRegion',
                    width: 60,
                    align: "right",
                    sortable: true,
                    editable: true,
                    edittype: 'select',
                    stype: 'select',
                    editoptions: {
                        dataInit: function (elem) {
                            funZonas(paisEdit, regionEdit);
                        },
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function (e) {
                                    regionEdit = $(e.target).val();
                                    funCambiarZonaEdit(paisEdit, regionEdit);
                                }
                            }
                        ],
                    },
                    searchoptions: {
                        value: { "": "Todos" },
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function (e) {
                                    regionSearch = $(e.target).val();
                                    funCambiarZonaSearch(paisSearch, regionSearch);
                                }
                            }
                        ]
                    }
                },
                {
                    name: 'vchCodigoZona',
                    index: 'vchCodigoZona',
                    width: 85,
                    align: "right",
                    sortable: true,
                    editable: true,
                    edittype: 'select',
                    stype: 'select',
                    searchoptions: {
                        value: { "": "Todos" }
                    }
                },
                { name: 'NombreGerenteRegion', index: 'NombreGerenteRegion', width: 165, align: "left", sortable: true, editable: false, searchoptions: { sopt: ['cn', 'bw'] } },
                { name: 'vchObservacion', index: 'vchObservacion', width: 90, align: "right", sortable: false, editable: true, searchoptions: { sopt: ['cn', 'bw'] } }
                ],
                onSelectRow: function (id) {
                    paisEdit = grid.jqGrid('getCell', id, 'chrPrefijoIsoPais');
                    regionEdit = grid.jqGrid('getCell', id, 'vchCodigoRegion');
                    codGzEdit = grid.jqGrid('getCell', id, 'chrCodigoGerenteZona');
                    cubGzEdit = grid.jqGrid('getCell', id, 'vchCUBGZ');

                },
                url: url + '?accion=load&pais=' + pais,
                datatype: 'json',
                mtype: 'GET',
                editurl: url,
                rowNum: 15,
                ignoreCase: true,
                rowList: [15, 30, 45],
                pager: '#pager',
                viewrecords: true,
                sortorder: "desc",
                caption: "Mantenimiento de Gerentes de Zonas",
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
            grid.jqGrid('setColProp', 'vchCodigoRegion', {
                editoptions: {
                    dataUrl: url + '?accion=loadRegiones&pais=' + varPaisEdit,
                    buildSelect: function (data) {
                        var s = '<select id="vchCodigoRegion">';
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

            $('#gs_vchCodigoRegion').find('option').remove().end();
            $('#gs_vchCodigoRegion').html(newOptions);

        }

        function funCambiarRegionEdit(varPaisEdit) {
            var parametrosRegion = { accion: 'loadRegiones', 'pais': varPaisEdit };
            var listObjRegion = Evoluciona.Ajax(url, parametrosRegion, false);
            var newOptions = "";


            $.each(listObjRegion, function (i, v) {
                newOptions += "<option role=\"option\" value=\"" + v.CodRegion + "\">" + v.CodRegion + "</option>";
            });

            $('select#vchCodigoRegion.FormElement').find('option').remove().end();
            $('select#vchCodigoRegion.FormElement').html(newOptions);


            regionEdit = $('select#vchCodigoRegion.FormElement').val();

        }

        function funZonas(varPaisEdit, varRegionEdit) {

            if (!inEdit) {

                var parametrosRegion = { accion: 'loadRegiones', 'pais': varPaisEdit };
                var listObjRegion = Evoluciona.Ajax(url, parametrosRegion, false);

                $.each(listObjRegion, function (i, v) {
                    varRegionEdit = v.CodRegion;
                    return false;
                });

            }

            grid.jqGrid('setColProp', 'vchCodigoZona', {
                editoptions: {
                    dataUrl: url + '?accion=loadZonas&pais=' + varPaisEdit + '&region=' + varRegionEdit,
                    buildSelect: function (data) {
                        var s = '<select id="vchCodigoZona">';
                        var listData = JSON.parse(data);
                        $.each(listData, function (index, value) {
                            s += "<option value='" + value.codZona + "'>" + value.codZona + "</option>";
                        });

                        return s + "</select>";
                    }
                }
            });
        }

        function funCambiarZonaSearch(varPaisSearch, varRegionSearch) {
            var parametrosZona = { accion: 'loadZonas', 'pais': varPaisSearch, 'region': varRegionSearch };
            var listObjZona = Evoluciona.Ajax(url, parametrosZona, false);
            var newOptions = "<option value=\"\">Todos</option>";


            $.each(listObjZona, function (i, v) {
                newOptions += "<option role=\"option\" value=\"" + v.codZona + "\">" + v.codZona + "</option>";
            });

            $('#gs_vchCodigoZona').find('option').remove().end();
            $('#gs_vchCodigoZona').html(newOptions);

        }

        function funCambiarZonaEdit(varPaisEdit, varRegionEdit) {
            var parametrosZona = { accion: 'loadZonas', 'pais': varPaisEdit, 'region': varRegionEdit };
            var listObjZona = Evoluciona.Ajax(url, parametrosZona, false);
            var newOptions = "";


            $.each(listObjZona, function (i, v) {
                newOptions += "<option role=\"option\" value=\"" + v.codZona + "\">" + v.codZona + "</option>";
            });

            $('select#vchCodigoZona.FormElement').find('option').remove().end();
            $('select#vchCodigoZona.FormElement').html(newOptions);

        }

        function funLimpiarZonaSearch() {

            var newOptions = "<option value=\"\">Todos</option>";
            $('#gs_vchCodigoZona').find('option').remove().end();
            $('#gs_vchCodigoZona').html(newOptions);

        }

    </script>
</asp:Content>
