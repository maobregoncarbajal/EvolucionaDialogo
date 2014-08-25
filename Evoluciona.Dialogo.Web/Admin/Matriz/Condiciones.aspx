<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Condiciones.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.Condiciones"
    Title="Untitled Page" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuMatriz.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <link href="../../Styles/colorboxAlt.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script type="text/javascript" src="../../Jscripts/jquery.colorbox-min.js"></script>

    <style type="text/css">
        .filtroVacio {
            color: #0000FF;
            font-family: Arial;
            font-size: 10px;
        }

        .CssTabla {
            border-collapse: collapse;
            border-color: #A2ACB1;
            border-spacing: 0px;
            width: 700px;
        }
    </style>

    <script type="text/javascript">
        function showCondicionesE(fila) {
            var codNumCondicion;
            var desNumCondicion;

            codNumCondicion = $("#" + fila + "COD").text();
            desNumCondicion = $("#" + fila + "DES").text();

            $.fn.colorbox({ href: "CondicionesE.aspx?codPais=" + $("#txtCodPais").val() + "&desPais=" + $("#txtDesPais").val() + " &codTipoCondicion=" + $("#txtCodTipoCondicion").val() + "&desTipoCondicion=" + $("#lblDesTipoCondicion").html() + "&codNumCondicion=" + codNumCondicion + "&desNumCondicion=" + desNumCondicion, width: "800px", height: "460px", iframe: true, opacity: "0.8", open: true, close: "" });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <div id="idParametros" style="display: none">
        <input type="text" id="txtCodTipoCondicion" />
        <input type="text" id="txtCodPais" />&nbsp;&nbsp;&nbsp;
        <input type="text" id="txtDesPais" />&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCodUsuario" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
    </div>
    <table width="100%">
        <tr>
            <td align="center">
                <table style="text-align: center">
                    <tr>
                        <td>&nbsp;<br />
                        </td>
                    </tr>
                    <tr>
                        <td class="Csstexto">Pais:
                            <asp:DropDownList ID="cboPaises" runat="server" Style="width: 150px" CssClass="stiloborde">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Condicion:
                            <select id="cboTipoCondicion" class="stiloborde" style="width: 150px;">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px">
                            <label id="lblFiltroVacio" class="filtroVacio"></label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <input type="button" id="btnBuscar" value="Buscar" class="btn" onclick="createTable();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;<br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>&nbsp;<br />
            </td>
        </tr>
        <tr>
            <td>
                <label id="lblDesTipoCondicion" style="color: #4660a1; font-size: 16px; font-weight: bold;">
                </label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;<br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table id="tblCondiciones" class="CssTabla">
                    <thead>
                        <tr>
                            <th style="width: 400px" class="CssCabecTabla">Lineamiento
                            </th>
                            <th style="width: 140px" class="CssCabecTabla">Estado
                            </th>
                            <th style="width: 160px" class="CssCabecTabla">Editar
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>

    <div style="height: 20px">
        &nbsp;
    </div>

    <div id="pager" class="pager" style="height: 30px">
        <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/first.png"
            class="first" />
        <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/prev.png"
            class="prev" />
        <input type="text" class="pagedisplay" />
        <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/next.png"
            class="next" />
        <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/last.png"
            class="last" />
        <select class="pagesize">
            <option selected="selected" value="10">10</option>
            <option value="20">20</option>
            <option value="30">30</option>
            <option value="40">40</option>
        </select>
    </div>

    <div style="height: 20px">
        &nbsp;
    </div>

    <input type="button" id="btnGuardar" disabled="disabled" value="Guardar" class="btn" onclick="grabar();" />

    <div id="dialog-alert" style="display: none">
    </div>

    <script type="text/javascript" language="javascript">
        var urlMatriz = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var urlAdmin = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
        var condiciones;

        $(document).ready(function () {
            crearPopUp();
            loadTipos();
        });

        function loadTipos() {
            var competencias = MATRIZ.Ajax(urlMatriz, { accion: 'tipos', fileName: 'TomaAccion.xml', adicional: 'no' }, false);

            $.each(competencias, function (i, v) {
                $('#cboTipoCondicion').append($('<option></option>').attr('value', v.Codigo).text(v.Descripcion));
            });
        }

        function crearPopUp() {
            var arrayDialog = [{ name: "dialog-alert", height: 100, width: 250, title: "Alerta" }];
            MATRIZ.CreateDialogs(arrayDialog);
        }

        function createTable() {
            var codPais = $("#<%=cboPaises.ClientID %>").val();
            var tipoCondicion = $("#cboTipoCondicion").val();

            $("#lblFiltroVacio").html('');
            $("#txtCodPais").val(codPais);
            $("#txtDesPais").val($("#<%=cboPaises.ClientID %> option:selected").text());
            $("#txtCodTipoCondicion").val(tipoCondicion);
            $("#lblDesTipoCondicion").html("Condiciones para " + $("#cboTipoCondicion option:selected").text());

            var j = $("#cboTipoCondicion");
            var x = $("#cboTipoCondicion option:selected").text();

            var tabla = "";
            var combo = "";
            $("#tblCondiciones tbody").html("");

            condiciones = MATRIZ.Ajax(urlAdmin, { accion: 'obtenerCondiciones', codPais: codPais, tipoCondicion: tipoCondicion }, false);

            if (condiciones.length == 0) {
                $("#lblFiltroVacio").html('(0) n&uacute;mero de registros encontrados.');
                $("#btnGuardar").attr("disabled", "disabled");
            }
            else {
                $("#btnGuardar").removeAttr("disabled");
            }

            $.each(condiciones, function (i, v) {
                combo = "";
                combo = "<select id=" + i + " class='stiloborde' style='width: 70px;'>";
                combo = combo + "<option value='0'>Inactivo</option>";
                combo = combo + "<option value='1'>Activo</option>";
                combo = combo + "</select>";

                tabla = tabla + "<tr id=fila" + i + " class='fila'><td id=" + i + "COD style='display: none'>" + v.numeroCondicionLineamiento + "</td>";
                tabla = tabla + "<td id=" + i + "DES class='CssCeldas3'>" + v.descripcionCondicion + "</td>";
                tabla = tabla + "<td id=" + i + "COM class='CssCeldas3'>" + combo + "</td>";
                tabla = tabla + "<td id=" + i + "LNK class='CssCeldas3'><a href=\"javascript:showCondicionesE('" + i + "')\">Editar</a></td></tr>";

                $(tabla).appendTo("#tblCondiciones tbody");
                $("#" + i + " option[value=" + v.estadoActivo + "]").attr("selected", true);
                tabla = "";
            });

            $("tblCondiciones").trigger('destroy.pager');
            $("#tblCondiciones").tablesorter({ widthFixed: true }).tablesorterPager({ container: $("#pager"), positionFixed: false });
        }

        function grabar() {
            var listaCondiciones = new Array();

            var codPais = $("#txtCodPais").val();
            var tipoCondicion = $("#txtCodTipoCondicion").val();
            var codUsuario = $("#<%=lblCodUsuario.ClientID %>").html();
            var filas = $("#tblCondiciones tbody tr.fila");

            var numeroCondicionLineamiento;
            var descripcionCondicion;
            var estado;

            $.each(filas, function (i) {
                numeroCondicionLineamiento = "";
                descripcionCondicion = "";
                estado = "";

                $(this).children("td").each(function (j, k) {
                    switch (j) {
                        case 0:
                            numeroCondicionLineamiento = $(this).text();
                            break;
                        case 1:
                            descripcionCondicion = $(this).text();
                            break;
                        case 2:
                            estado = $("#tblCondiciones tbody td#" + k.id + " option:selected").val();
                            break;
                    }
                });

                listaCondiciones[i] = {
                    PrefijoIsoPais: codPais,
                    tipoCondicion: tipoCondicion,
                    descripcionCondicion: descripcionCondicion,
                    numeroCondicionLineamiento: numeroCondicionLineamiento,
                    estadoActivo: estado
                };
            });

            var jsonCondiciones = JSON.stringify(listaCondiciones);

            var competencias = MATRIZ.Ajax(urlAdmin, { accion: 'actualizarCondiciones', condiciones: jsonCondiciones, usuario: codUsuario }, false);
            $("#dialog-alert").html('');
            if (competencias) {
                $("#dialog-alert").append('Datos grabados correctamente.');
                $("#dialog-alert").dialog("open");
            }
            else {
                $("#dialog-alert").append('Lo sentimos, los datos no se pudieron grabar.');
                $("#dialog-alert").dialog("open");
            }
        }
    </script>

</asp:Content>
