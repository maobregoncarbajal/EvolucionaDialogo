<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="MantGz.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.MantGz" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="../../Jscripts/JQGridReq/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Jscripts/JQGridReq/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="../../Jscripts/JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JQGridReq/grid.locale-en.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="list"><tr><td></td></tr></table>
    <div id="pager"></div>
    <asp:HiddenField ID="hfPais" runat="server" />
    <asp:HiddenField ID="hfValComboPais" runat="server" />
    <asp:HiddenField ID="hfRegion" runat="server" />
    <asp:HiddenField ID="hfRol" runat="server" />

    <script type="text/javascript">

        var url = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGz.ashx';
        

        var paises = {};
        var paisSelect = "";
        
        var regionesDePais = {};
        var regiones = [];
        var regionSelect,regionTarget;
        
        var zonasDeRegion = {};
        var zonas = [];
            
        var parametrosPaises = { accion: 'loadPaises', 'pais': '00' };
        var listObjPaises = Ajax(url, parametrosPaises, false);

        $.each(listObjPaises, function (i, v) {
            paises[v.Codigo] = v.Codigo;
            
            var parametrosRegiones = { accion: 'loadRegiones', 'pais': v.Codigo };
            var listObjRegiones = Ajax(url, parametrosRegiones, false);
            var rgions = {};
            var zonsDpais = {};

            $.each(listObjRegiones, function (o, y) {
                rgions[y.CodRegion] = y.CodRegion;

                var parametrosZonas = { accion: 'loadZonas', 'pais': v.Codigo, 'region': y.CodRegion };
                var listObjZonas = Ajax(url, parametrosZonas, false);
                var zons = {};

                $.each(listObjZonas, function (u, z) {
                    zons[z.codZona] = z.codZona;
                });

                zonasDeRegion[y.CodPais + "|" + y.CodRegion] = zons;//zonasDeRegion[y.CodRegion] = zons;
                zonas = $.extend(zonas, zons);
                
                zonsDpais = $.extend(zonsDpais, zons);

            });

            regionesDePais[v.Codigo] = rgions;
            regiones = $.extend(regiones, rgions);

            zonasDeRegion[v.Codigo + "|"] = zonsDpais;

        });
        
        var todosPaises = $.extend({ "": "Todos" }, paises);

        regionesDePais = $.extend({ "": regiones }, regionesDePais);
        var todosRegiones = $.extend({ "": "Todos" }, regiones);
        
        zonasDeRegion = $.extend({ "|": zonas }, zonasDeRegion);
        var todosZonas = $.extend({ "": "Todos" }, zonas);

        var EditCodGz, EditCubGz, inlineEditing;
        
        var lastSel = -1;
        var grid = $("#list");
        var removeTheOptionAll = function (elem) {
            // We use value:allCountries and value:allStates in the searchoptions.
            // The option {"": "All"} neams "No filter" and should be displayed only
            // in the searching toolbar and not in the searching dialog.
            // So we use dataInit:removeTheOptionAll inside of searchoptions to remove
            // the option {"": "All"} in case of the searching dialog
            if (typeof elem === "object" && typeof elem.id === "string" && elem.id.substr(0, 3) !== "gs_") {
                // we are NOT in the searching bar
                $(elem).find("option[value=\"\"]").remove();
            }
        };
        var resetStatesValues = function () {
            // set 'value' property of the editoptions to initial state
            grid.jqGrid('setColProp', 'vchCodigoRegion', { editoptions: { value: regiones } });
        };
        
        var resetZonasValues = function () {
            // set 'value' property of the editoptions to initial state
            grid.jqGrid('setColProp', 'vchCodigoZona', { editoptions: { value: zonas } });
        };

        var setStateValues = function (countryId) {
            // to have short list of options which corresponds to the country
            // from the row we have to change temporary the column property
            grid.jqGrid('setColProp', 'vchCodigoRegion', { editoptions: { value: regionesDePais[countryId] } });
        };
        
        var setZonaValues = function (regionId) {
            // to have short list of options which corresponds to the country
            // from the row we have to change temporary the column property
            grid.jqGrid('setColProp', 'vchCodigoZona', { editoptions: { value: zonasDeRegion[regionId] } });
        };

        var changeStateSelect = function (countryId, countryElem) {
            // build 'state' options based on the selected 'country' value
            var stateId, stateSelect, parentWidth, $row,
                $countryElem = $(countryElem),
                sc = regionesDePais[countryId],
                isInSearchToolbar = $countryElem.parent().parent().parent().parent().parent().parent().parent().hasClass('ui-search-toolbar'),
                //$(countryElem).parent().parent().hasClass('ui-th-column'),
                newOptions = isInSearchToolbar ? "<option value=\"\">Todos</option>" : "";

            for (stateId in sc) {
                if (sc.hasOwnProperty(stateId)) {
                    newOptions += "<option role=\"option\" value=\"" + stateId + "\">" +
                        regiones[stateId] + "</option>";
                }
            }
            setStateValues(countryId);

            // populate the subset of contries
            if (isInSearchToolbar) {
                // searching toolbar
                $row = $countryElem.closest('tr.ui-search-toolbar');
                stateSelect = $row.find('>th.ui-th-column select#gs_vchCodigoRegion');
                parentWidth = stateSelect.parent().width();
                stateSelect.html(newOptions).css({ width: parentWidth });
            } else if ($countryElem.is('.FormElement')) {
                // form editing
                $countryElem.closest('form.FormGrid').find('select#vchCodigoRegion.FormElement').html(newOptions);
                regionSelect = $countryElem.closest('form.FormGrid').find('select#vchCodigoRegion.FormElement').val();
            } else {
                // inline editing
                $row = $countryElem.closest('tr.jqgrow');
                $("select#" + $.jgrid.jqID($row.attr('id')) + "_vchCodigoRegion").html(newOptions);
                regionSelect = $("select#" + $.jgrid.jqID($row.attr('id')) + "_vchCodigoRegion").val();
            }
        };
        
        var changeZonaSelect = function (regionId, regionElem) {
            // build 'state' options based on the selected 'country' value
            var zonaId, zonaSelect, parentWidth, $row,
                $regionElem = $(regionElem),
                sc = zonasDeRegion[regionId],
                isInSearchToolbar = $regionElem.parent().parent().parent().parent().parent().parent().parent().hasClass('ui-search-toolbar'),
                //$(countryElem).parent().parent().hasClass('ui-th-column'),
                newOptions = isInSearchToolbar ? "<option value=\"\">Todos</option>" : "";

            for (zonaId in sc) {
                if (sc.hasOwnProperty(zonaId)) {
                    newOptions += "<option role=\"option\" value=\"" + zonaId + "\">" +
                        zonas[zonaId] + "</option>";
                }
            }

            setZonaValues(regionId);

            // populate the subset of contries
            if (isInSearchToolbar) {
                // searching toolbar
                $row = $regionElem.closest('tr.ui-search-toolbar');
                zonaSelect = $row.find('>th.ui-th-column select#gs_vchCodigoZona');
                parentWidth = zonaSelect.parent().width();
                zonaSelect.html(newOptions).css({ width: parentWidth });
            } else if ($regionElem.is('.FormElement')) {
                // form editing
                $regionElem.closest('form.FormGrid').find('select#vchCodigoZona.FormElement').html(newOptions);
            } else {
                // inline editing
                $row = $regionElem.closest('tr.jqgrow');
                $("select#" + $.jgrid.jqID($row.attr('id')) + "_vchCodigoZona").html(newOptions);
            }
        };

        var editGridRowOptions = {
            recreateForm: true,
            onclickPgButtons: function (whichButton, $form, rowid) {
                var $row = $('#' + $.jgrid.jqID(rowid)), countryId, regionId;
                if (whichButton === 'next') {
                    $row = $row.next();
                } else if (whichButton === 'prev') {
                    $row = $row.prev();
                }
                if ($row.length > 0) {
                    countryId = grid.jqGrid('getCell', $row.attr('id'), 'chrPrefijoIsoPais');
                    changeStateSelect(countryId, $('#chrPrefijoIsoPais')[0]);
                    paisSelect = countryId;
                    regionId = grid.jqGrid('getCell', $row.attr('id'), 'vchCodigoRegion');
                    changeZonaSelect(paisSelect + "|" + regionId, $('#vchCodigoRegion')[0]);
                }
            },
            onClose: function() {
                resetStatesValues();
                resetZonasValues();
            },
            afterSubmit: function (response, postdata) {
                
                //$("#FormError").find(".ui-state-error").removeClass("ui-state-error").addClass("ui-state-highlight");
                //$("#FormError").find(".ui-state-highlight").removeClass("ui-state-highlight").addClass("ui-state-error");
                
                resetStatesValues();
                resetZonasValues();
                inlineEditing = false;

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
            },
            closeOnEscape: true,
            reloadAfterSubmit: true,
            closeAfterEdit: true,
            drag: true
        };
        
        var addGridRowOptions = {
            recreateForm: true,
            onclickPgButtons: function (whichButton, $form, rowid) {
                var $row = $('#' + $.jgrid.jqID(rowid)), countryId, regionId;
                if (whichButton === 'next') {
                    $row = $row.next();
                } else if (whichButton === 'prev') {
                    $row = $row.prev();
                }
                if ($row.length > 0) {
                    countryId = grid.jqGrid('getCell', $row.attr('id'), 'chrPrefijoIsoPais');
                    changeStateSelect(countryId, $('#chrPrefijoIsoPais')[0]);
                    paisSelect = countryId;
                    regionId = grid.jqGrid('getCell', $row.attr('id'), 'vchCodigoRegion');
                    changeZonaSelect(paisSelect + "|" + regionId, $('#vchCodigoRegion')[0]);
                }
            },
            onClose: function () {
                resetStatesValues();
                resetZonasValues();
            },
            afterSubmit: function (response, postdata) {
                resetStatesValues();
                resetZonasValues();
                inlineEditing = false;

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
            beforeSubmit: function (postdata, formid) {
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
                    var cantCodGz = Ajax(url, paramValidaCodGz, false);

                    if (cantCodGz > 0) {
                        return [false, 'Ya existe el Doc. Identidad'];
                    }
                }

                if (_cub.val() == "") {
                    _cub.addClass("ui-state-highlight");
                    return [false, 'CUB no puede estar vacio']; //error
                } else {
                    var paramValidaCub = { accion: 'validaCub', 'pais': _pai.val(), 'region': _reg.val(), 'zona': _zon.val(), 'cub': _cub.val() };
                    var cantCub = Ajax(url, paramValidaCub, false);

                    if (cantCub > 0) {
                        return [false, 'Ya existe el CUB'];
                    }
                }

                return [true, '']; // no error
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

        var validaNombre = function (value, colname) {
            if (value.length < 1) {
                return [false, "Nombre no puede estar vacio"];
            } else {
                return [true, ""];
            }
        };

        var validaCodGz = function (value, colname) {
            
            var selr = grid.jqGrid('getGridParam', 'selrow');
            var id = $.jgrid.jqID(selr);
            var varPais, varRegion, varZona;
            
            if (inlineEditing) {
                varPais = $("select#" + id + "_chrPrefijoIsoPais").val();
                varRegion = $("select#" + id + "_vchCodigoRegion").val();
                varZona = $("select#" + id + "_vchCodigoZona").val();
            } else {
                
                varPais = $("select#chrPrefijoIsoPais").val();
                varRegion = $("select#vchCodigoRegion").val();
                varZona = $("select#vchCodigoZona").val();
            }
            
            if (value != EditCodGz) {
                
                var paramValidaCodGz = { accion: 'validaCodGz', 'pais': varPais, 'region': varRegion, 'zona': varZona, 'codGz': value };
                var cantCodGz = Ajax(url, paramValidaCodGz, false);

                if (cantCodGz > 0) {
                    return [false, 'Ya existe el Doc. Identidad'];
                }

            } else {
                return [true, ""];
            }
            
            return [true, '']; // no error
        };
        
        var validaCubGz = function (value, colname) {
            var selr = grid.jqGrid('getGridParam', 'selrow');
            var id = $.jgrid.jqID(selr);
            var varPais, varRegion, varZona;

            if (inlineEditing) {
                varPais = $("select#" + id + "_chrPrefijoIsoPais").val();
                varRegion = $("select#" + id + "_vchCodigoRegion").val();
                varZona = $("select#" + id + "_vchCodigoZona").val();
            } else {
                varPais = $("select#chrPrefijoIsoPais").val();
                varRegion = $("select#vchCodigoRegion").val();
                varZona = $("select#vchCodigoZona").val();
            }

            if (value != EditCubGz) {

                var paramValidaCub = { accion: 'validaCub', 'pais': varPais, 'region': varRegion, 'zona': varZona, 'cub': value };
                var cantCub = Ajax(url, paramValidaCub, false);

                if (cantCub > 0) {
                    return [false, 'Ya existe el CUB'];
                }

            } else {
                return [true, ""];
            }

            return [true, '']; // no error
        };


        grid.jqGrid({
            url: '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGz.ashx?accion=load&pais=' + $('#<%=hfPais.ClientID%>').val(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['intIDGerenteZona', 'ID GerenteRegion ', 'País', 'Doc. Identidad', 'Nombre Completo', 'Correo Electrónico', 'CUB', 'C. Planilla', 'C. Región', 'C. Zona', 'G. Región', 'Observación'],
            colModel: [
                { name: 'intIDGerenteZona', index: 'intIDGerenteZona', stype: 'text', editable: true, sorttype: 'int', hidden: true },
                { name: 'intIDGerenteRegion', index: 'intIDGerenteRegion', width: 100, align: "right", editable: true, hidden: true },
                {
                    name: 'chrPrefijoIsoPais',
                    index: 'chrPrefijoIsoPais',
                    width: 60,
                    align: "left",
                    editable: true,
                    formatter: 'select',
                    stype: 'select',
                    edittype: 'select',
                    searchoptions: {
                        value: todosPaises,
                        dataInit: removeTheOptionAll,
                        dataEvents: [
                            {
                                type: 'change', fn: function (e) {
                                    paisSelect = $(e.target).val();
                                    changeStateSelect($(e.target).val(), e.target);
                                    changeZonaSelect(paisSelect + "|", e.target);
                                }
                            },
                            { type: 'keyup', fn: function(e) {
                                $(e.target).trigger('change');
                            } }
                        ]
                        , defaultValue: ""
                        , sopt: ['eq', 'ne', 'le', 'lt', 'gt', 'ge']
                    },
                    editoptions: {
                        value: paises,
                        dataInit: function (elem) {
                            paisSelect = $(elem).val();
                            setStateValues($(elem).val());
                            setStateValues(paisSelect + "|" + regionSelect); ///evaluar
                        },
                        dataEvents: [
                            {
                                type: 'change', fn: function (e) {
                                    paisSelect = $(e.target).val();
                                    changeStateSelect($(e.target).val(), e.target);
                                    changeZonaSelect(paisSelect + "|" + regionSelect, regionTarget);
                                }
                            },
                            {
                                type: 'keyup', fn: function (e) {
                                $(e.target).trigger('change');
                            } }
                        ]
                    }
                },
                {
                    name: 'chrCodigoGerenteZona',
                    index: 'chrCodigoGerenteZona',
                    align: "right",
                    sortable: false,
                    width: 140,
                    editable: true,
                    editrules: {
                        custom: true,
                        custom_func: validaCodGz
                    }
                },
                {
                    name: 'vchNombreCompleto',
                    index: 'vchNombreCompleto',
                    align: "left",
                    width: 165,
                    sortable: true,
                    editable: true,
                    editrules: { custom: true, custom_func: validaNombre }
                },
                { name: 'vchCorreoElectronico', index: 'vchCorreoElectronico', width: 120, align: "left", sortable: false, editable: true },
                {
                    name: 'vchCUBGZ',
                    index: 'vchCUBGZ',
                    width: 140, align: "right",
                    sortable: false,
                    editable: true,
                    editrules: { custom: true, custom_func: validaCubGz }
                },
                { name: 'chrCodigoPlanilla', index: 'chrCodigoPlanilla', width: 80, align: "right", sortable: false, editable: true },
                {
                    name: 'vchCodigoRegion',
                    index: 'vchCodigoRegion',
                    width: 60,
                    align: "right",
                    sortable: true,
                    formatter: "select",
                    stype: "select",
                    editable: true,
                    edittype: "select",
                    searchoptions: {
                        value: todosRegiones,
                        dataInit: removeTheOptionAll,
                        dataEvents: [
                            {
                                type: 'change', fn: function (e) {
                                    regionSelect = $(e.target).val();
                                    regionTarget = e.target;
                                changeZonaSelect(paisSelect + "|" + $(e.target).val(), e.target);
                            } },
                            { type: 'keyup', fn: function(e) {
                                $(e.target).trigger('change');
                            } }
                        ],
                        defaultValue: "",
                        sopt: ['eq', 'ne', 'le', 'lt', 'gt', 'ge']
                    },
                    editoptions: {
                        value: regiones,
                        dataInit: function (elem) {
                            regionSelect = $(elem).val();
                            regionTarget = elem;
                            setZonaValues(paisSelect + "|" + $(elem).val());
                        },
                        dataEvents: [
                            {
                                type: 'change', fn: function (e) {
                                    regionSelect = $(e.target).val();
                                    regionTarget = e.target;
                                    changeZonaSelect(paisSelect + "|" + $(e.target).val(), e.target);
                                }
                            },
                            {
                                type: 'keyup', fn: function (e) {
                                    $(e.target).trigger('change');
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
                    formatter: "select",
                    stype: "select",
                    editable: true,
                    edittype: "select",
                    searchoptions: { value: todosZonas, dataInit: removeTheOptionAll, defaultValue: "" },
                    editoptions: {
                        value: zonas
                    }
                },
                { name: 'NombreGerenteRegion', index: 'NombreGerenteRegion', width: 165, align: "right", sortable: true, editable: false },
                { name: 'vchObservacion', index: 'vchObservacion', width: 90, align: "right", sortable: false, editable: true }
            ],
            onSelectRow: function (id) {
                var selId = grid.jqGrid('getGridParam', 'selrow');
                EditCodGz = grid.jqGrid('getCell', selId, 'chrCodigoGerenteZona');
                EditCubGz = grid.jqGrid('getCell', selId, 'vchCUBGZ');
                inlineEditing = false;
                
                if (id && id !== lastSel) {
                    if (lastSel !== -1) {
                        $(this).jqGrid('restoreRow', lastSel);
                        resetStatesValues();
                        resetZonasValues();
                    }
                    lastSel = id;
                }
            },
            ondblClickRow: function (id) {
                inlineEditing = true;
                if (id && id !== lastSel) {
                    $(this).jqGrid('restoreRow', lastSel);
                    lastSel = id;
                }
                resetStatesValues();
                resetZonasValues();
                $(this).jqGrid('editRow', id, {
                    keys: true,
                    aftersavefunc: function () {
                        resetStatesValues();
                        resetZonasValues();
                        inlineEditing = false;
                    },
                    afterrestorefunc: function () {
                        resetStatesValues();
                        resetZonasValues();
                        inlineEditing = false;
                    }
                });
                return;
            },
            editurl: url,
            //sortname: 'name',
            ignoreCase: true,
            height: '100%',
            width: 1100,
            forceFit: false,
            shrinkToFit:false,
            autowidth: false,
            fixed: false,
            viewrecords: true,
            rownumbers: true,
            sortorder: "desc",
            pager: '#pager',
            caption: "Mantenimiento de Gerentes de Zonas"
        });
        grid.jqGrid('navGrid', '#pager', { edit: true, add: true, del: true, search: true }, editGridRowOptions, addGridRowOptions, delGridRowOptions);
        grid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: true, defaultSearch: "cn" });


        // AGREGANDO BOTON EXPORTAR EXCEL
        $('#list').jqGrid('navButtonAdd', '#pager',
            {
                caption: '<span class="ui-pg-button-text">Export</span>',
                buttonicon: "ui-icon-extlink",
                title: "Export To Excel",
                onClickButton: function () {
                    window.location = '<%=Utils.RelativeWebRoot%>Admin/Altas_Bajas/MantUsuarioGz.ashx?accion=export&pais=' + $('#<%=hfPais.ClientID%>').val();
                }
            }
        );
        

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
