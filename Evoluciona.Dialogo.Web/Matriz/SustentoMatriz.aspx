<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SustentoMatriz.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Matriz.SustentoMatriz" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sustento Matriz</title>
    <link href="../Styles/Matriz.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ColorBoxAlt.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Matriz.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/colorboxAlt.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/BlockUI.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Validation.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/typography.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableSorter.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JSLINQ.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/json2.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.validate.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>/Jscripts/jquery.window.min.js"
        type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        var idTomaAccion;
        var prefijoIsoPaisEvaluado;
        var periodo;
        var codEvaluado;
        var tomaAccion;
        var idRolEvaluado;
        var indexView = 0;
        var codEvaluador;
        var datosFicha;
        var lista;
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var codVariable;

        $(document).ready(function () {

            inicalizar();
            switch (indexView) {
                case 0:
                    MATRIZ.LoadDropDownList("ddlVariableEnfoque", url, { 'accion': 'variablesEnfoque', codPais: prefijoIsoPaisEvaluado, codEvaluado: codEvaluado, idRolEvaluado: idRolEvaluado, periodo: periodo }, 0, true, false);
                    MATRIZ.ToolTipText();
                    break;
                case 1:
                    CargarResumen();
                    break;
                case 2:
                    break;
                case 3:
                    FuncTomaAccion();
                default:
                    break;
            }


            $('#btnBuscar').click(function () {

                $("#panelResultado").hide();

                codVariable = $("#ddlVariableEnfoque").val();
                var parametros = { accion: 'verResultadoSustento', codPaisEvaluado: prefijoIsoPaisEvaluado, codEvaluado: codEvaluado, codVariable: codVariable, periodo: periodo, idRolEvaluado: idRolEvaluado, idTomaAccion: idTomaAccion, codTomaAccion: tomaAccion };

                var lista = MATRIZ.Ajax(url, parametros, false);

                if (lista.length > 0) {
                    $("#panelResultado").show();
                    crearTabla('tblResultadoPre', "A", lista, idRolEvaluado);
                    crearTabla('tblResultadoPost', "P", lista, idRolEvaluado);

                    parametros = { accion: 'verChartSustento', codPaisEvaluado: prefijoIsoPaisEvaluado, codEvaluado: codEvaluado, codVariable: codVariable, periodo: periodo, idRolEvaluado: idRolEvaluado, idTomaAccion: idTomaAccion, codTomaAccion: tomaAccion };
                    $("#ChartArea").hide();
                    var urlChart = MATRIZ.Ajax(url, parametros, false);
                    $("#ChartArea").attr("src", urlChart);
                    $("#ChartArea").show();
                }
            });
        });

        function inicalizar() {
            idTomaAccion = $("#<%=lblIdTomaAccion.ClientID %>").html();
            prefijoIsoPaisEvaluado = $("#<%=lblPrefijoIsoPaisEvaluado.ClientID %>").html();
            periodo = $("#<%=lblPeriodo.ClientID %>").html();
            codEvaluado = $("#<%=lblCodEvaluado.ClientID %>").html();
            tomaAccion = $("#<%=lblTomaAccion.ClientID %>").html();
            idRolEvaluado = $("#<%=lblIdRolEvaluado.ClientID %>").html();
            codEvaluador = $("#<%=lblCodEvaluador.ClientID %>").html();
            datosFicha = $("#<%=lblDatosFichaPersonal.ClientID %>").html();



        }

        var FuncTomaAccion = function () {

            var titulo;
            $("#DivAcc").hide();

            var parametros = { accion: 'verCondiciones', idTomaAcc: idTomaAccion, prefijoIsoPaisEval: prefijoIsoPaisEvaluado, perio: periodo, codEval: codEvaluado, tomaAcc: tomaAccion, idRolEval: idRolEvaluado };

            lista = MATRIZ.Ajax(url, parametros, false);

            switch (tomaAccion) {
                case '01':
                    $("#tbl01 thead").html("");
                    titulo = "<tr><th>Plan de Mejora</th>";
                    titulo = titulo + "<th>FFVV cumple con condiciones</th></tr>";
                    $(titulo).appendTo("#tbl01 thead");
                    break;
                case '02':

                    $("#tbl01 thead").html("");
                    titulo = "<tr><th>Condiciones de Reasignación</th>";
                    titulo = titulo + "<th>Cumple con Condiciones</th></tr>";
                    $(titulo).appendTo("#tbl01 thead");
                    break;

                case '03':

                    $("#tbl01 thead").html("");
                    titulo = "<tr><th>Condiciones para Desvinculación</th>";
                    titulo = titulo + "<th>FFVV cumple con condiciones</th></tr>";
                    $(titulo).appendTo("#tbl01 thead");
                    break;
            }

            var tabla = "";
            $.each(lista, function (index, entidad) {
                tabla = tabla + "<tr><td>" + entidad.DescripcionLineamiento + "</td>";
                tabla = tabla + "<td>" + entidad.CumpleLineamiento + "</td></tr>";

            });

            $("#tbl01 tbody").html("");
            $(tabla).appendTo("#tbl01 tbody");

            $("#DivAcc").show();
        };

        function crearTabla(nombreTabla, tipo, lista, codRol) {

            var cabeceraCampanha = "<tr><th style='width:10px'></th>";
            var ranking = "<tr><th>Ranking</th>";

            var region = "";

            if (codRol == "2")
                region = "<tr><th>Región</th>";

            if (codRol == "3")
                region = "<tr><th>Zona</th>";

            var participacion;
            var logro;
            var text = '';

            if (codVariable == "VtaNet") {
                participacion = "<tr><th>%Part. Venta</th>";
                logro = "<tr><th>%Logro Venta</th>";

            } else {
                participacion = "<tr><th>" + text + "Valor Planeado</th>";
                logro = "<tr><th>" + text + "Valor Real</th>";
            }


            var datos = JSLINQ(lista).Where(function (item) { return item.Tipo == tipo; }).OrderBy(function (item) { return item.NombreCampanha; });

            if (datos.items.length > 0) {

                $.each(datos.items, function (j, w) {

                    cabeceraCampanha = cabeceraCampanha + "<th>" + w.NombreCampanha + "</th>";

                    switch (w.EstadoCampana.toUpperCase()) {
                        case 'CRITICA':
                            ranking = ranking + "<td style='font-weight:bold;width:50px;' class='rojo'>CRITICA </td>";
                            break;
                        case 'ESTABLE':
                            ranking = ranking + "<td style='font-weight:bold;width:50px;' class='amarillo'>ESTABLE</td>";
                            break;
                        case 'PRODUCTIVA':
                            ranking = ranking + "<td style='font-weight:bold;width:50px;' class='verde'>PRODUCTIVA</td>";
                            break;
                    }

                    participacion = participacion + "<td>" + parseFloat(w.ParticipacionCampana).toFixed(2) + "</td>";
                    logro = logro + "<td>" + parseFloat(w.LogroCampana).toFixed(2) + "</td>";
                    region = region + "<td>" + w.CodRegionZona + "</td>";
                });
            }

            cabeceraCampanha = cabeceraCampanha + "</tr>";
            ranking = ranking + "</tr>";
            participacion = participacion + "</tr>";
            logro = logro + "</tr>";
            region = region + "</tr>";

            $("#" + nombreTabla + " thead").html("");
            $("#" + nombreTabla + " tbody").html("");
            $(cabeceraCampanha).appendTo("#" + nombreTabla + " thead");
            $(ranking).appendTo("#" + nombreTabla + " tbody");
            $(participacion).appendTo("#" + nombreTabla + " tbody");
            $(logro).appendTo("#" + nombreTabla + " tbody");
            $(region).appendTo("#" + nombreTabla + " tbody");

            var numberColumns = countColumn(nombreTabla);

            if (numberColumns < 2) {
                $("#" + nombreTabla + " thead").html("");
                $("#" + nombreTabla + " tbody").html("");
            }
        }

        function countColumn(table) {
            return $("#" + table).find('tr')[0].cells.length;
        }

        function CargarResumen() {
            var pathUrl;
            pathUrl = "<%=Utils.AbsoluteWebRoot%>Admin/ResumenProceso.aspx?" + "nomEvaluado=" + datosFicha.toString().split('|')[0] + "&codEvaluado=" +
                datosFicha.toString().split('|')[1] + "&idProceso=" + datosFicha.toString().split('|')[2] + "&rolEvaluado=" + datosFicha.toString().split('|')[3] +
                "&codPais=" + datosFicha.toString().split('|')[4] + "&periodo=" + datosFicha.toString().split('|')[5] + "&codEvaluador=" + datosFicha.toString().split('|')[6];

            $.fn.colorbox({ href: pathUrl, width: "700px", height: "600px", iframe: true, opacity: "0.8", open: true, close: "" });
        };

        function inicializarView(index) {
            indexView = index;
        };

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Label class="labelTituloPrincipal" Style="margin: 0px" ID="lblNom" runat="server">
                AYDA QUINTEROS SALAS </asp:Label>
                <asp:Label class="labelTituloPlomo" runat="server" ID="lblRol">(Gerente Regional)</asp:Label>
                <div>
                    <label class="labelInfo">
                        Correo Electrónico</label>
                    <asp:Label class="labelTitulo" ID="lblCorreo" runat="server">
                    aakeber@co.belcorp.biz</asp:Label>
                </div>
            </div>
            <div style="margin-top: 10px; margin-bottom: 10px">
                <ul class="tabnav" style="clear: both">
                    <li>
                        <asp:LinkButton runat="server" ID="btnVerResultados" Text="VER RESULTADOS" CssClass="current"
                            OnClick="btnVerResultados_Click" /></li>
                    <li>
                        <asp:LinkButton runat="server" ID="btnDialogoDesempeno" Text="DIALOGO DESEMPEÑO"
                            OnClick="btnDialogoDesempeno_Click" /></li>
                    <li>
                        <asp:LinkButton runat="server" ID="btnVisitas" Text="SALIDAS DE DESARROLLO" OnClick="btnVisitas_Click" /></li>
                    <li>
                        <asp:LinkButton runat="server" ID="btnCondiciones" Text="VER CONDICIONES" OnClick="btnCondiciones_Click" /></li>
                </ul>
            </div>
            <asp:MultiView ID="MainView" runat="server">
                <asp:View ID="View1" runat="server">
                    <label class="labelTitulo">
                        Variables Enfoque:</label>
                    <select id="ddlVariableEnfoque">
                    </select>
                    <input type="button" id="btnBuscar" class="btn" value="Buscar" style="margin-left: 5px" />
                    <br />
                    <div style="clear: both; margin: 10px 0px 0px 0px; display: none" id="panelResultado">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table id="tblResultadoPre" class="grillaMorada" style="float: left">
                                        <thead>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </td>
                                <td>
                                    <table id="tblResultadoPost" class="grillaMorada" style="float: left">
                                        <thead>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%" colspan="2">
                                    <div style="margin-top: 20px">
                                        <img id="ChartArea" style="display: none" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                </asp:View>
                <asp:View ID="View3" runat="server">
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <div style="display: none" id="DivAcc" align="center">
                        <br />
                        <br />
                        <p class="labelTitulo">
                            Ver Condiciones
                        </p>
                        <br />
                        <table class="grillaMorada" id="tbl01" cellspacing="0" rules="all" border="1">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblIdTomaAccion" runat="server"></asp:Label>
        <asp:Label ID="lblPrefijoIsoPaisEvaluado" runat="server"></asp:Label>
        <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
        <asp:Label ID="lblCodEvaluado" runat="server"></asp:Label>
        <asp:Label ID="lblTomaAccion" runat="server"></asp:Label>
        <asp:Label ID="lblIdRolEvaluado" runat="server"></asp:Label>
        <asp:Label ID="lblCodEvaluador" runat="server"></asp:Label>
        <asp:Label ID="lblDatosFichaPersonal" runat="server"></asp:Label>
    </div>
</body>
</html>
