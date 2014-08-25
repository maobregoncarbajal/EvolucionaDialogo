<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="AgrupacionZonaGPS.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.AgrupacionZonaGPS"
    Title="Untitled Page" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuMatriz.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css"
        rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/calendar.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/ui.datepicker-es.js"
        type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <div>
        <table width="100%">
            <tr>
                <td align="center" style="color: #4660a1; font-size: 16px; font-weight: bold; text-align: center;">Agrupar zona GPS
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 280px;">País :
                    <select id="ddlPais" class="stiloborde" style="width: 115px; height: 22px;">
                    </select>
                    <input type="button" class="btnSquare" value="Buscar" id="btnBuscar" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table class="tablesorter" id="tblCronograma" cellspacing="0" rules="all" border="1">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div id="divBtnGuardar" style="display: none">
                        <input type="button" class="btnSquare" value="Guardar" id="btnGuardar" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-alert" style="display: none">
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblTipo" runat="server"></asp:Label>
        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
    </div>

    <script type="text/javascript" language="javascript">

        var obj;
        var pais;

        $(document).ready(function () {
            crearPopUp();
            Buscar();
            Guardar();
        });

        function Guardar() {
            $("#btnGuardar").live('click', function () {

                var grupo1 = "";
                var grupo2 = "";
                var grupo3 = "";
                var diferencia = "";
                var resul = false;
                var xml = '<?xml version="1.0" encoding="iso-8859-1"?><TABLE>';

                if (obj.length > 0) {

                    for (var i = 0; i < obj.length; i++) {

                        if (grupo1 == "") {
                            grupo1 = $("#txt_" + i.toString()).val().toUpperCase();
                        }
                        else {
                            if (grupo2 == "") {
                                grupo2 = $("#txt_" + i.toString()).val().toUpperCase();
                                if (grupo1 == grupo2) { grupo2 = ""; }
                            }
                            else {
                                if (grupo3 == "") {
                                    grupo3 = $("#txt_" + i.toString()).val().toUpperCase();
                                    if (grupo3 == grupo1 || grupo3 == grupo2) { grupo3 = ""; }
                                }
                                else {
                                    if (diferencia == "") {

                                        diferencia = $("#txt_" + i.toString()).val().toUpperCase();

                                        if (diferencia.trim() != "") {
                                            if (diferencia != grupo1 && diferencia != grupo2 && diferencia != grupo3) {
                                                resul = true;
                                                break;
                                            } else {
                                                diferencia = "";
                                            }
                                        } else {
                                            diferencia = "";
                                        }
                                    }
                                }
                            }
                        }
                    }


                    if (resul) {
                        MATRIZ.ShowAlert('dialog-alert', 'No se pueden asignar mas de 3 grupos verifique');
                        return;
                    }


                    for (var j = 0; j < obj.length; j++) {

                        xml += '<ZONAS><ZONAGPS>' + obj[j].ZonaGps + '</ZONAGPS><GRUPO>' + $("#txt_" + j.toString()).val().toUpperCase() + '</GRUPO></ZONAS>';
                        //lista[index].TomaAccion
                    }

                    xml += '</TABLE>';


                    var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";

                    var parametros = { accion: 'grabarZonaGPS', pPais: pais, pXml: xml };

                    var resultado;

                    resultado = MATRIZ.Ajax(url, parametros, false);

                    if (resultado > 0) {
                        MATRIZ.ShowAlert('dialog-alert', 'Resgistro exitoso');
                    } else {
                        MATRIZ.ShowAlert('dialog-alert', 'Error');
                    }
                }
            });
        }

        function Buscar() {
            $("#btnBuscar").live('click', function () {


                var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
                pais = $("#ddlPais").val();

                var parametros = { accion: 'buscarZonaGPS', pPais: pais };

                obj = MATRIZ.Ajax(url, parametros, false);

                var removeMe = -1;
                $.each(obj, function (i, v) {
                    if (v.ZonaGps == '' || v.ZonaGps == null) {
                        removeMe = i;
                    }
                });

                if (removeMe != -1) {
                    obj.splice(removeMe, 1);
                }



                if (obj.length > 0) {
                    $("#tblCronograma thead").html("");
                    var titulo = "";
                    titulo = titulo + "<tr>";
                    titulo = titulo + "<th class ='CssCabecTabla' width='120px'>ZonaGPS</th>";
                    titulo = titulo + "<th class ='CssCabecTabla' width='120px'>Grupo</th>";
                    //titulo = titulo + "<th class ='CssCabecTabla' width='120px'>Edicion</th>";
                    titulo = titulo + "</tr>";

                    $(titulo).appendTo("#tblCronograma thead");

                    var tabla = "";
                    var cont = 0;
                    $.each(obj, function (i, v) {

                        tabla = tabla + "<tr>";
                        tabla = tabla + "<td class ='CssCeldas3' >" + v.ZonaGps + "</td>";
                        tabla = tabla + "<td class ='CssCeldas3' ><input type='text' size='15' maxlength='2' value='" + trim(v.Grupo.toString()) + "' id= 'txt_" + cont.toString() + "'></td>";
                        //tabla = tabla + "<td class ='CssCeldas3'><a><img class='edit' id='" + v.IdCronogramaMatriz + "' src='<%=Utils.RelativeWebRoot%>Styles/Matriz/btntabla_editar.png' alt='' align='center'  onClick='' /></a></td>";
                        tabla = tabla + "</tr>";
                        cont += 1;
                    });

                    $("#tblCronograma tbody").html("");
                    $(tabla).appendTo("#tblCronograma tbody");
                    $("#divBtnGuardar").show();
                }
                else {
                    $("#tblCronograma thead").html("");
                    $("#tblCronograma tbody").html("");
                    $("#divBtnGuardar").hide();
                    MATRIZ.ShowAlert('dialog-alert', 'No se han encontrado registros de toma de acciones');
                }

            });
        }

        function crearCombos() {

            var codPais = $("#<%=lblPais.ClientID %>").html();
            var tipo = $("#<%=lblTipo.ClientID %>").html();

            var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";

            MATRIZ.LoadDropDownList("ddlPais", url, { 'accion': 'paisConFuenteVentas', 'codPais': codPais, 'tipo': tipo, 'select': 'si' }, 0, true, false);
            //MATRIZ.LoadDropDownList("ddlPeriodo", url, { 'accion': 'periodo', 'codPais': codPais, 'tipo': tipo }, 0, true, false);
            MATRIZ.ToolTipText();
        }

        function crearPopUp() {

            var arrayDialog = [{ name: "dialog-alert", height: 150, width: 250, title: "Alerta" }];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function trim(myString) {
            return myString.replace(/^\s+/g, '').replace(/\s+$/g, '');
        }




    </script>

</asp:Content>
