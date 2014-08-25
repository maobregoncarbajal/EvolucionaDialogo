<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="NuevoMantGz.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Altas_Bajas.NuevoMantGz" %>

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
        var pais = $("#<%=hfPais.ClientID %>").val();
        var paisSelect = "";
        var regionSelect = "";
        var listPaises = funPaises();
        var paisEdit = "";
        var lastSel = -1;


        jQuery(document).ready(function () {
            

            
        var resetRegionesValues = function () {
            grid.setColProp('vchCodigoRegion', { editoptions: { value: {} } });
        };

        var editGridRowOptions = {
            recreateForm: true,
        };
       
        grid.jqGrid({
            colNames: ['intIDGerenteZona', 'ID GerenteRegion ', 'País', 'Doc. Identidad', 'Nombre Completo', 'Correo Electrónico', 'CUB', 'C. Planilla', 'C. Región', 'C. Zona', 'G. Región', 'Observación'],
            colModel: [
                { name: 'intIDGerenteZona', index: 'intIDGerenteZona', stype: 'text', editable: true, sorttype: 'int', hidden: true },
                { name: 'intIDGerenteRegion', index: 'intIDGerenteRegion', width: 100, align: "right", editable: true, hidden: true },
                {
                    name: 'chrPrefijoIsoPais',
                    index: 'chrPrefijoIsoPais',
                    width: 60,
                    align: "left",
                    sortable: true,
                    editable: true,
                    edittype: 'select',
                    stype: 'select',
                    editoptions: {
                        value: listPaises,
                        dataInit: function (elem) {
                            var v = $(elem).val();
                            grid.setColProp('vchCodigoRegion', { editoptions: { value: funRegiones(v) } });
                        }
                    },
                    searchoptions: {
                        value: $.extend({ "": "Todos" }, listPaises),
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function(e) {
                                    paisSelect = $(e.target).val();
                                    funCambiarRegionSelect(paisSelect);
                                    funLimpiarZonaSelect();

                                }
                            }
                        ],
                    }
                },
                { name: 'chrCodigoGerenteZona', index: 'chrCodigoGerenteZona', align: "right", sortable: false, width: 140, editable: true },
                { name: 'vchNombreCompleto', index: 'vchNombreCompleto', align: "left", width: 165, sortable: true, editable: true },
                { name: 'vchCorreoElectronico', index: 'vchCorreoElectronico', width: 120, align: "left", sortable: false, editable: true },
                { name: 'vchCUBGZ', index: 'vchCUBGZ', width: 140, align: "right", sortable: false, editable: true },
                { name: 'chrCodigoPlanilla', index: 'chrCodigoPlanilla', width: 80, align: "right", sortable: false, editable: true },
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
                        value: [],
                        dataInit: function(elem) {
                            funRegiones("");
                        }
                    },
                    searchoptions: {
                        value: { "": "Todos" },
                        dataEvents: [
                            {
                                type: 'change',
                                fn: function(e) {
                                    regionSelect = $(e.target).val();
                                    funCambiarZonaSelect(paisSelect, regionSelect);
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
                    editoptions: {
                        value: listPaises
                    },
                    searchoptions: {
                        value: { "": "Todos" }
                    }
                },
                { name: 'NombreGerenteRegion', index: 'NombreGerenteRegion', width: 165, align: "right", sortable: true, editable: false },
                { name: 'vchObservacion', index: 'vchObservacion', width: 90, align: "right", sortable: false, editable: true }
            ],
            onSelectRow: function (id) {
                if (id && id !== lastSel) {
                    if (lastSel != -1) {
                        resetRegionesValues();
                        grid.restoreRow(lastSel);
                    }
                    lastSel = id;
                }
            },
            url: url + '?accion=load&pais=' + pais,
            datatype: 'json',
            mtype: 'GET',
            editurl: url,
            rowNum: 10,
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
            fixed: false
        });

        grid.jqGrid('navGrid', '#pager', { edit: true, add: false, del: false, search: false }, editGridRowOptions);
        grid.jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });
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
        var parametrosRegion = { accion: 'loadRegiones', 'pais': varPaisEdit };
        var listObjRegion = Evoluciona.Ajax(url, parametrosRegion, false);
        var regiones = {};

        $.each(listObjRegion, function (i, v) {
            regiones[v.CodRegion] = v.CodRegion;
        });


        return regiones;
        //grid.jqGrid('setColProp', 'vchCodigoRegion', { editoptions: { value: regiones } });
    }

    function funZonas() {
        var parametrosZona = { accion: 'loadZonas', 'pais': pais, 'region': '00' };
        var listObjZona = Evoluciona.Ajax(url, parametrosZona, false);
        var zonas = {};

        $.each(listObjZona, function (i, v) {
            zonas[v.codZona] = v.codZona;
        });

        return zonas;
    }

    function funCambiarRegionSelect(varPaisSelect) {
        var parametrosRegion = { accion: 'loadRegiones', 'pais': varPaisSelect };
        var listObjRegion = Evoluciona.Ajax(url, parametrosRegion, false);
        var newOptions = "<option value=\"\">Todos</option>";


        $.each(listObjRegion, function (i, v) {
            newOptions += "<option role=\"option\" value=\"" + v.CodRegion + "\">" + v.CodRegion + "</option>";
        });

        $('#gs_vchCodigoRegion').find('option').remove().end();
        $('#gs_vchCodigoRegion').html(newOptions);

    }

    function funCambiarZonaSelect(varpaisSelect, varRegionSelect) {
        var parametrosZona = { accion: 'loadZonas', 'pais': varpaisSelect, 'region': varRegionSelect };
        var listObjZona = Evoluciona.Ajax(url, parametrosZona, false);
        var newOptions = "<option value=\"\">Todos</option>";


        $.each(listObjZona, function (i, v) {
            newOptions += "<option role=\"option\" value=\"" + v.codZona + "\">" + v.codZona + "</option>";
        });

        $('#gs_vchCodigoZona').find('option').remove().end();
        $('#gs_vchCodigoZona').html(newOptions);

    }

    function funLimpiarZonaSelect() {

        var newOptions = "<option value=\"\">Todos</option>";
        $('#gs_vchCodigoZona').find('option').remove().end();
        $('#gs_vchCodigoZona').html(newOptions);

    }

    </script>
</asp:Content>
