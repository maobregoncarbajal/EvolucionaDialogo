<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="NivCompetencia.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.NivCompetencia"
    Title="Untitled Page" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuMatriz.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/JSLINQ.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <style type="text/css">
        .filaBlanco {
            height: 20px;
        }

        .textoAzul {
            color: #4660a1;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblCodUsuario" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
    </div>
    <table width="100%">
        <tr>
            <td align="center">
                <table id="tblNivelCompetencia" cellpadding="0" cellspacing="0" width="290px;">
                    <thead>
                        <tr>
                            <th style="text-align: center" class="textoAzul" colspan="2">Niveles de competencia
                            </th>
                        </tr>
                        <tr>
                            <th class="filaBlanco" style="height: 40px" colspan="2">Valores expresados en decimales (0-1)
                            </th>
                        </tr>
                        <tr>
                            <td class="texto" style="text-align: left; padding-left: 5px">*&nbsp;Calculo para el a&ntilde;o
                            </td>
                            <td style="width: 140px">
                                <asp:DropDownList ID="cboAnho" runat="server" Style="width: 105px" CssClass="stiloborde"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="texto" style="text-align: left; padding-left: 5px">*&nbsp;Pa&iacute;s
                            </td>
                            <td>
                                <asp:DropDownList ID="cboPaises" runat="server" Style="width: 105px" CssClass="stiloborde"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" class="texto" colspan="2">C&aacute;lculo por a&ntilde;o
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td class="filaBlanco" colspan="2">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="2">
                                <input type="button" id="btnGuardar" value="Guardar" class="btn" onclick="grabar();" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </td>
        </tr>
    </table>
    <div id="dialog-alert" style="display: none">
    </div>

    <script type="text/javascript" language="javascript">
        var url = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
        var urlNivelCompetencias = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
        var valorAnterior;
        var existenCambios = false;

        function moneyFormat(amount) {
            var val = parseFloat(amount);

            if (isNaN(val)) { return "0.00"; }
            if (val <= 0) { return "0.00"; }

            val += "";

            if (val > 1) {
                val = "0";
            }

            if (val.indexOf('.') == -1) {
                return val + ".00";
            }
            else {
                val = val.substring(0, val.indexOf('.') + 3);
            }

            val = (val == Math.floor(val)) ? val + '.00' : ((val * 10 == Math.floor(val * 10)) ? val + '0' : val);
            return val;
        }

        $(window).bind('beforeunload', function () {
            if (existenCambios) {
                return "Se han detectado cambios sin grabar.";
            }
        });

        $(document).ready(function () {
            crearPopUp();
            createTable();
            consultar();
            $("input.inputtext[name='txtMinimo'], input.inputtext[name='txtMaximo']").numeric({ allow: '.' });

            $("input.inputtext[name='txtMinimo']").focus(function () {
                valorAnterior = parseFloat(this.value);
            });

            $("input.inputtext[name='txtMinimo']").blur(function () {
                if (this.value == '') {
                    $("#" + this.id).val('0');
                }
                var num = moneyFormat(this.value);

                $("#" + this.id).val(num);

                if (valorAnterior != parseFloat(this.value)) {
                    existenCambios = true;
                }
            });

            $("input.inputtext[name='txtMaximo']").focus(function () {
                valorAnterior = parseFloat(this.value);
            });

            $("input.inputtext[name='txtMaximo']").blur(function () {
                if (this.value == '') {
                    $("#" + this.id).val('0');
                }
                var num = moneyFormat(this.value);

                $("#" + this.id).val(num);

                if (valorAnterior != parseFloat(this.value)) {
                    existenCambios = true;
                }
            });

            function crearPopUp() {
                var arrayDialog = [{ name: "dialog-alert", height: 100, width: 250, title: "Alerta" }];
                MATRIZ.CreateDialogs(arrayDialog);
            }

            function createTable() {
                var competencias = MATRIZ.Ajax(url, { accion: 'tipos', fileName: 'Competencia.xml', adicional: 'no' }, false);
                var tablaNc = "";

                $("#tblNivelCompetencia tbody").html("");

                if (competencias.length <= 0) {
                    return;
                }

                $.each(competencias, function (i, v) {
                    tablaNc = tablaNc + "<tr><td colspan='2'>&nbsp;</td></tr><tr>";

                    tablaNc = tablaNc + "<td class='texto' style='text-align: left; padding-left: 5px'>";
                    tablaNc = tablaNc + "*&nbsp;" + v.Descripcion.substring(0, 1).toUpperCase() + v.Descripcion.substring(1).toLowerCase();
                    tablaNc = tablaNc + "</td>";
                    tablaNc = tablaNc + "<td>&nbsp;</td>";
                    tablaNc = tablaNc + "</tr>";

                    tablaNc = tablaNc + "<tr><td colspan='2'>&nbsp;</td></tr><tr>";

                    tablaNc = tablaNc + "<tr>";
                    tablaNc = tablaNc + "<td class='texto'>";
                    tablaNc = tablaNc + "P. M&iacute;nimo";
                    tablaNc = tablaNc + "</td>";
                    tablaNc = tablaNc + "<td>";
                    tablaNc = tablaNc + "<input id='" + v.Codigo + "_MI' class='inputtext' style='width: 105px; text-align:right' name='txtMinimo' value='0.00' type='text' />";
                    tablaNc = tablaNc + "</td>";
                    tablaNc = tablaNc + "</tr>";
                    tablaNc = tablaNc + "<tr>";
                    tablaNc = tablaNc + "<td class='texto'>";
                    tablaNc = tablaNc + "P. M&aacute;ximo";
                    tablaNc = tablaNc + "</td>";
                    tablaNc = tablaNc + "<td>";
                    tablaNc = tablaNc + "<input id='" + v.Codigo + "_MA' class='inputtext' style='width: 105px; text-align:right' name='txtMaximo' value='0.00' type='text' />";
                    tablaNc = tablaNc + "</td>";
                    tablaNc = tablaNc + "</tr>";
                });

                $(tablaNc).appendTo("#tblNivelCompetencia tbody");
            }
        });

        function grabar() {
            var usuario = $("#<%=lblCodUsuario.ClientID %>").html();
            var codPais = $("#<%=cboPaises.ClientID %>").val();
            var anio = $("#<%=cboAnho.ClientID %>").val();

            anio = anio.substring(0, 4);

            if (codPais == null || anio == null) {
                return;
            }

            var maximo;
            var minimo;

            var parametros = "";
            var grabado = "";
            var grabadoFinal;

            var datosBruto = $(".inputtext");
            var matriz = new Array(datosBruto.length);

            grabadoFinal = 1;

            $.each(datosBruto, function (i, val) {
                matriz[i] = new Array(3);
                matriz[i]['competencia'] = val.id.substring(0, val.id.indexOf('_'));
                matriz[i]['tipo'] = val.id.substring(val.id.indexOf('_') + 1, val.id.length);
                matriz[i]['valor'] = val.value;
            });

            var matrizCompetencia = JSLINQ(matriz).Distinct(function (item) { return item.competencia; });
            var validacion = true;

            $.each(matrizCompetencia.items, function (i, competencia) {
                var matrizDetalle = JSLINQ(matriz).Where(function (item) { return item.competencia == competencia; });

                $.each(matrizDetalle.items, function (j, detalle) {
                    if (detalle.tipo == 'MI')
                        minimo = detalle.valor;
                    else if (detalle.tipo == 'MA')
                        maximo = detalle.valor;
                    else
                        alert('Tipo fuera del intervalo');
                });

                if (minimo > maximo) {
                    validacion = false;
                }
            });

            if (validacion == false) {
                $("#dialog-alert").html('');
                $("#dialog-alert").append('El porcentaje mínimo no puede ser mayor que el máximo.');
                $("#dialog-alert").dialog("open");
                return;
            }

            $.each(matrizCompetencia.items, function (i, competencia) {
                var matrizDetalle = JSLINQ(matriz).Where(function (item) { return item.competencia == competencia; });

                $.each(matrizDetalle.items, function (j, detalle) {
                    if (detalle.tipo == 'MI')
                        minimo = detalle.valor;
                    else if (detalle.tipo == 'MA')
                        maximo = detalle.valor;
                    else
                        alert('Tipo fuera del intervalo');
                });

                parametros = { accion: 'insertarNivelesCompetencia', codpais: codPais, anio: anio, usuario: usuario, maximo: maximo, minimo: minimo, competencia: competencia };
                grabado = MATRIZ.Ajax(urlNivelCompetencias, parametros, false);

                if (grabado != null) {
                    if (!grabado) {
                        grabadoFinal = 0;
                    }
                }
            });

            $("#dialog-alert").html('');
            if (grabado == 1) {
                existenCambios = false;
                $("#dialog-alert").append('Datos grabados correctamente.');
                $("#dialog-alert").dialog("open");
            }
            else {
                $("#dialog-alert").append('Lo sentimos, los datos no se pudieron grabar.');
                $("#dialog-alert").dialog("open");
            }
        }

        function consultar() {
            var codPais = $("#<%=cboPaises.ClientID %>").val();
            var anio = $("#<%=cboAnho.ClientID %>").val();
            var valor = "0";
            var dato;

            anio = anio.substring(0, 4);

            if (codPais == null || anio == null) {
                return;
            }

            var datosBruto = $(".inputtext");
            var parametros = { accion: 'obtenerNivelesCompetencia', codPais: codPais, anio: anio };
            var matrizConsulta = MATRIZ.Ajax(urlNivelCompetencias, parametros, false);

            if (matrizConsulta == null || matrizConsulta.length <= 0) {
                return;
            }

            $.each(datosBruto, function (i, val) {
                var competencia = val.id.substring(0, val.id.indexOf('_'));
                var tipo = val.id.substring(val.id.indexOf('_') + 1, val.id.length);
                dato = "";

                if (tipo == 'MI') {
                    dato = JSLINQ(matrizConsulta).Where(function (item) {
                        return item.CodCompetencia == competencia && item.prefijoIsoPais == codPais;
                    });

                    $.each(dato.items, function (j, d) {
                        valor = d.MinValue;
                    });
                } else if (tipo == 'MA') {
                    dato = JSLINQ(matrizConsulta).Where(function (item) {
                        return item.CodCompetencia == competencia && item.prefijoIsoPais == codPais;
                    });

                    $.each(dato.items, function (k, d) {
                        valor = d.MaxValue;
                    });
                }

                valor = moneyFormat(valor);

                $("#" + val.id).val(valor);
                valor = "0";
            });
        }
    </script>

</asp:Content>
