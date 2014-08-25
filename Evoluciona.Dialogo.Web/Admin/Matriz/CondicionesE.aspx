<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CondicionesE.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.CondicionesE" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../Styles/pmatriz.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <style type="text/css">
        .CssTabla {
            border-collapse: collapse;
            border-color: #A2ACB1;
            border-spacing: 0px;
            width: 500px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="Div1" style="display: none">
            <asp:TextBox ID="txtCodTipoCondicion" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCodPais" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCodNumCondicion" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCodUsuario" runat="server"></asp:Label>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td colspan="2">&nbsp;<br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;<br />
                                </td>
                            </tr>
                            <tr>
                                <td class="Csstexto">
                                    <strong>Pais:
                                    <asp:Label ID="lblPais" runat="server" Text="lblPais" CssClass="CssLabel1"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="Csstexto">
                                    <strong>Condicion:
                                    <asp:Label ID="lblTipoCondicion" runat="server" Text="" CssClass="CssLabel1"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="Csstexto">
                                    <strong>
                                        <asp:Label ID="lblNumeroCondicion" runat="server" Text="lblNumeroCondicion" CssClass="CssLabel1"></asp:Label></strong>
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
                    <td align="center">
                        <table id="tblCondicionesDetalle" class="CssTabla">
                            <thead>
                                <tr>
                                    <td style="width: 200px" class="CssCabecTabla">&nbsp;&nbsp;Variable&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 180px" class="CssCabecTabla">&nbsp;&nbsp;Valor&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 120px" class="CssCabecTabla">&nbsp;&nbsp;Tipo&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;<br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <input type="button" id="btnGuardar" value="Guardar" class="btn" onclick="grabar();" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
    var urlMatriz = "<%=Utils.RelativeWebRoot%>Matriz/MatrizReporte.ashx";
    var urlAdmin = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
    var detalle;


    function moneyFormat(amount) {
        var val = parseFloat(amount);

        if (isNaN(val)) { return "0.00"; }
        if (val <= 0) { return "0.00"; }

        val += "";

        if (val.indexOf('.') == -1) {
            return val + ".00";
        }
        else {
            val = val.substring(0, val.indexOf('.') + 3);
        }

        val = (val == Math.floor(val)) ? val + '.00' : ((val * 10 ==
            Math.floor(val * 10)) ? val + '0' : val);
        return val;
    }

    $(document).ready(function () {
        createTable();

        $("input[name='txtValorVar']").numeric({ allow: '.' });

        $("input[name='txtValorVar']").blur(function () {
            if (this.value == '') {
                $("#" + this.id).val('0');
            }

            var num = moneyFormat(this.value);

            $("#" + this.id).val(num);
        });
    });

    function createTable() {
        var tabla;
        var combo;
        var prefijoIsoPais = $("#<%=txtCodPais.ClientID %>").val();
        var numeroCondicionLineamiento = $("#<%=txtCodNumCondicion.ClientID%>").val();
        var tipoCondicion = $("#<%=txtCodTipoCondicion.ClientID%>").val();

        $("#tblCondicionesDetalle tbody").html("");
        var condiciones = MATRIZ.Ajax(urlAdmin, {
            accion: 'obtenerCondicionesDetalle',
            prefijoIsoPais: prefijoIsoPais,
            numeroCondicionLineamiento: numeroCondicionLineamiento,
            tipoCondicion: tipoCondicion
        }, false);

        if (condiciones.length == 0) {
            return;
        }

        $.each(condiciones, function (i, v) {
            tabla = "";
            combo = "";
            combo = "<select id=combo" + i + " class='stiloborde' style='width: 100px;'>";
            combo = combo + "<option value='01'>N&uacute;mero</option>";
            combo = combo + "<option value='02'>Porcentaje</option>";
            combo = combo + "<option value='03'>Resultado</option>";
            combo = combo + "</select>";

            tabla = tabla + "<tr id=fila" + i + " class='fila'>";
            tabla = tabla + "<td id=" + i + "DES class='CssCeldas3'>" + v.descripcionVariables + "</td>";
            tabla = tabla + "<td id=" + i + "VV class='CssCeldas3'>";

            if (tipoCondicion == '02' && numeroCondicionLineamiento == '03') {
                var competencias = MATRIZ.Ajax(urlMatriz, { accion: 'tipos', fileName: 'Competencia.xml', adicional: 'no' }, false);

                tabla = tabla + "<select id=cbo" + i + " class='stiloborde' style='width: 150px;'>";
                $.each(competencias, function (f, g) {
                    tabla = tabla + "<option value='" + g.Codigo + "'>" + g.Descripcion + "</option>";
                });
            }
            else {
                tabla = tabla + "<input id='txt" + i + "' type='text' name='txtValorVar' value='" + v.valorVariable + "' style='text-align:center; width:80px' />";
            }

            tabla = tabla + "</td>";
            tabla = tabla + "<td id=" + i + "TV class='CssCeldas3'>" + combo + "</td>";
            tabla = tabla + "</tr>";

            $(tabla).appendTo("#tblCondicionesDetalle tbody");

            if (tipoCondicion == '02' && numeroCondicionLineamiento == '03') {
                $("#cbo" + i + " option[value=0" + v.valorVariable + "]").attr("selected", true);
            }

            $("#combo" + i + " option[value=" + v.tipoVariable + "]").attr("selected", true);
        });
    }

    function grabar() {
        var listaCondicionesDetalle = new Array();

        var prefijoIsoPais = $("#<%=txtCodPais.ClientID %>").val();
        var tipoCondicion = $("#<%=txtCodTipoCondicion.ClientID %>").val();
        var numeroCondicionLineamiento = $("#<%=txtCodNumCondicion.ClientID %>").val();
        var codUsuario = $("#<%=lblCodUsuario.ClientID %>").html();

        var descripcionVariables;
        var valorVariable;
        var tipoVariable;

        var filas = $("#tblCondicionesDetalle tbody tr.fila");

        $.each(filas, function (i) {
            descripcionVariables = "";
            valorVariable = "";
            tipoVariable = "";

            $(this).children("td").each(function (j, k) {
                switch (j) {
                    case 0:
                        descripcionVariables = $(this).text();
                        break;
                    case 1:
                        if (tipoCondicion == '02' && numeroCondicionLineamiento == '03') {
                            valorVariable = $("#tblCondicionesDetalle tbody td#" + k.id + " option:selected").val();
                            valorVariable = parseInt(valorVariable);
                        }
                        else {
                            valorVariable = $("#tblCondicionesDetalle tbody td#" + k.id + " input:text").val();
                        }
                        break;
                    case 2:
                        tipoVariable = $("#tblCondicionesDetalle tbody td#" + k.id + " option:selected").val();
                        break;
                }
            });

            listaCondicionesDetalle[i] = {
                prefijoIsoPais: prefijoIsoPais,
                tipoCondicion: tipoCondicion,
                numeroCondicionLineamiento: numeroCondicionLineamiento,
                valorVariable: valorVariable,
                tipoVariable: tipoVariable
            };
        });

        var jsonCondicionesDetalle = JSON.stringify(listaCondicionesDetalle);

        var competencias = MATRIZ.Ajax(urlAdmin, { accion: 'actualizarCondicionesDetalle', condicionesDetalle: jsonCondicionesDetalle, usuario: codUsuario }, false);
        if (competencias) {
            alert('Datos grabados correctamente');
        }
        else {
            alert('Lo sentimos, los datos no se pudieron grabar.');
        }
    }
</script>
