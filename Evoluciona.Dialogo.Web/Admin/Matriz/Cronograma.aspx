<%@ Page Language="C#" MasterPageFile="~/Admin/SiteAdmin.Master" AutoEventWireup="true"
    CodeBehind="Cronograma.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Matriz.Cronograma" Title="Página sin título" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Admin/Controls/MenuMatriz.ascx" TagName="menuReportes" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/BlockUI.css" rel="stylesheet" type="text/css" />
    <link href="<%=Utils.AbsoluteWebRoot%>Styles/calendar.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.alphanumeric.pack.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Matriz.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/ui.datepicker-es.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <uc:menuReportes ID="menuReporte" runat="server" />
    <br />
    <div>
        <table width="100%">
            <tr>
                <td align="center" style="color: #4660a1; font-size: 16px; font-weight: bold; text-align: center;">Cronograma Matriz</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 280px;">
                    <input type="button" class="btnSquare" value="Nuevo" id="btnNuevo" onclick="Nuevo();" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <table class="tablesorter" id="tblCronograma" cellspacing="0" rules="all" border="1">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <div id="pager" class="pager">
            <form>
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/first.png" class="first" alt="" />
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/prev.png" class="prev" />
                <input type="text" class="pagedisplay" />
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/next.png" class="next" />
                <img src="<%=Utils.AbsoluteWebRoot%>Styles/images/last.png" class="last" />
                <select class="pagesize">
                    <option selected="selected" value="10">10</option>
                    <option value="20">20</option>
                    <option value="30">30</option>
                    <option value="40">40</option>
                </select>
            </form>
        </div>
    </div>
    <div id="dialog-alert" style="display: none"></div>
    <div id="div-edit" style="display: none">
        <table>
            <tr>
                <td>
                    <input type="hidden" id="hdId" name="hdId" value="" /></td>
                <td>
                    <input type="hidden" id="hdFechaServer" name="hdFechaServer" value="" /></td>
            </tr>
            <tr>
                <td>País :</td>
                <td>
                    <select id="ddlPais" class="stiloborde" style="width: 115px; height: 22px;"></select></td>
            </tr>
            <%--<tr>
            <td>Periodo : </td><td><select id="ddlPeriodo" class="stiloborde" style="width: 115px; height: 22px;"></select></td>
        </tr>--%>
            <tr>
                <td>Fecha Limite : </td>
                <td>
                    <input class="bodyinput" onkeypress="javascript:validateInputDate(this);" id="txtFechaLimite" style="width: 110px" type="text" maxlength="10" size="11" name="txtFechaLimite" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Fecha Prorroga : </td>
                <td>
                    <input class="bodyinput" onkeypress="javascript:validateInputDate(this);" id="txtFechaProrroga" style="width: 110px" type="text" maxlength="10" size="11" name="txtFechaProrroga" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" class="btnSquare" value="Grabar" id="btnGrabar" onclick="Grabar();" />
                </td>
                <td>
                    <input type="button" class="btnSquare" value="Cancelar" id="btnCancelar" onclick="Cancelar();" />
                </td>
            </tr>
        </table>
    </div>
    <div id="div-del" style="display: none">
        <table>
            <tr>
                <td colspan="2">¿Esta seguro que desea eliminar?
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" class="btnSquare" value="Aceptar" id="btnEliminar" onclick="Aceptar();" />
                </td>
                <td>
                    <input type="button" class="btnSquare" value="Cancelar" id="btnCancelarEliminar" onclick="Cancelar();" />
                </td>
            </tr>
        </table>
    </div>
    <div id="idParametros" style="display: none">
        <asp:Label ID="lblPais" runat="server"></asp:Label>
        <asp:Label ID="lblTipo" runat="server"></asp:Label>
        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
    </div>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            crearPopUp();
            loadReportCronogramaMatriz();
            Editar();
            Eliminar();
            Calendario();
        });

        function crearPopUp() {
            var arrayDialog = [
            { name: "dialog-alert", height: 100, width: 200, title: "Alerta" },
            { name: "div-edit", height: 160, width: 250, title: "Cronograma Matriz" },
            { name: "div-del", height: 75, width: 225, title: "Eliminar Cronograma Matriz" }
            ];

            MATRIZ.CreateDialogs(arrayDialog);
        }

        function loadReportCronogramaMatriz() {

            var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";

            var pais = $("#<%=lblPais.ClientID %>").html();

            var parametros = { accion: 'cronograma', pais: pais };

            var lista = MATRIZ.Ajax(url, parametros, false);

            if (lista.length > 0) {
                CrearTabla(lista);
                $("#panelButtons").show();
                $("#pager").show();
            }
            else {
                $("#panelButtons").hide();
                $("#pager").hide();
                MATRIZ.ShowAlert('dialog-alert', "No existen Datos");
            }
        }

        function CrearTabla(lista) {
            $("#tblCronograma thead").html("");
            var titulo = "";
            titulo = titulo + "<tr>";
            titulo = titulo + "<th class ='CssCabecTabla' width='120px'>Pais</th>";
            //titulo = titulo + "<th class ='CssCabecTabla' width='120px'>Periodo</th>";
            titulo = titulo + "<th class ='CssCabecTabla' width='120px'>Fecha Limite</th>";
            titulo = titulo + "<th class ='CssCabecTabla' width='120px'>Fecha Prorroga</th>";
            titulo = titulo + "<th class ='CssCabecTabla' width='60px'>Editar</th>";
            titulo = titulo + "<th class ='CssCabecTabla' width='60px'>Eliminar</th>";
            titulo = titulo + "</tr>";

            $(titulo).appendTo("#tblCronograma thead");

            var tabla = "";
            $.each(lista, function (i, v) {
                tabla = tabla + "<tr>";
                tabla = tabla + "<td class ='CssCeldas3' >" + v.PrefijoIsoPais + "</td>";
                //tabla = tabla + "<td class ='CssCeldas3'>" + v.Periodo + "</td>";
                //tabla = tabla + "<td class ='CssCeldas3'>" + FormatearFecha(v.FechaProrroga) + "_" + v.FechaProrroga + "</td>";
                tabla = tabla + "<td class ='CssCeldas3'>" + FormatearFecha(v.FechaLimite) + "</td>";
                tabla = tabla + "<td class ='CssCeldas3'>" + FormatearFecha(v.FechaProrroga) + "</td>";
                tabla = tabla + "<td class ='CssCeldas3'><a><img class='edit' id='" + v.IdCronogramaMatriz + "' src='<%=Utils.RelativeWebRoot%>Styles/Matriz/btntabla_editar.png' alt='' align='center'  onClick='' /></a></td>";
                tabla = tabla + "<td class ='CssCeldas3'><a><img class='del' id='" + v.IdCronogramaMatriz + "' src='<%=Utils.RelativeWebRoot%>Styles/Matriz/Delete-icon.png' alt='' align='center' onClick='' /></a></td>";
                tabla = tabla + "</tr>";
            });

            $("#tblCronograma tbody").html("");
            $(tabla).appendTo("#tblCronograma tbody");

            $("#tblCronograma").tablesorter({ widthFixed: true, widgets: ['zebra'] }).tablesorterPager({ container: $("#pager"), positionFixed: false });
        }

        //function FormatearFecha(cellvalue) {

        //    try {

        //        var fecha;

        //        if (cellvalue != null) {
                //    var milliseconds = cellvalue.substring(6, 19);
                //    var expDate = new Date(parseInt(milliseconds));
                //    var dd = expDate.getDate();
                //    var mm = expDate.getMonth() + 1; //January is 0!
                //    var yyyy = expDate.getFullYear();
                //    if (dd < 10) {
                //        dd = '0' + dd;
                //    }
                //    if (mm < 10) {
                //        mm = '0' + mm;
                //    }

                //    fecha = dd + '/' + mm + '/' + yyyy;

        //        } else {
        //            fecha = '';
        //        }

        //        return fecha;

        //    }
        //    catch (err) {
        //        return null;
        //    }
        //}


                    function FormatearFecha(cellvalue) {
                        if (cellvalue == null)
                            return "";
                        try {

                            var anio = cellvalue.substring(0, 4);
                            var mes = cellvalue.substring(5, 7);
                            var dia = cellvalue.substring(8, 10);

                            return dia + "/" + mes + "/" + anio;
                        } catch (err) {
                            return null;
                        }
                    }

        function ConvertStringtoDate(variable) {

            var from = variable.split("/");
            var f = new Date(from[2], from[1] - 1, from[0]);

            return f;

        }

        function Editar() {
            $(".edit").live('click', function () {
                var Id = $(this).attr("id");

                var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
                var parametros = { accion: 'selectCronograma', id: Id };
                var obj = MATRIZ.Ajax(url, parametros, false);

                var fechaLimite = FormatearFecha(obj.FechaLimite).toString();
                var fechaProrroga = null;

                if (obj.FechaProrroga != null) {
                    fechaProrroga = FormatearFecha(obj.FechaProrroga).toString();
                }

                var fechaServer = FormatearFecha(obj.FechaServer).toString();

                $('input[name=hdId]').val(Id);
                $('#<%=txtFechaLimite.ClientID %>').val(fechaLimite);
                $('#<%=txtFechaProrroga.ClientID %>').val(fechaProrroga);
                $('input[name=hdFechaServer]').val(fechaServer);

                //$("#ddlPeriodo option[value='" + obj.Periodo + "']").attr("selected", true);
                $("#ddlPais option[value='" + obj.PrefijoIsoPais + "']").attr("selected", true);
                $("#ddlPais").attr("disabled", true);

                $('#<%=txtFechaLimite.ClientID %>').datepicker("enable");

                if (obj.FechaProrroga != null) {
                    if (ConvertStringtoDate(fechaProrroga) >= ConvertStringtoDate(fechaServer)) {
                        if (ConvertStringtoDate(fechaLimite) < ConvertStringtoDate(fechaServer)) {
                            $('#<%=txtFechaLimite.ClientID %>').datepicker("disable");
                        }

                        $("#div-edit").dialog("open");
                    } else {
                        MATRIZ.ShowAlert('dialog-alert', "No es posible Editar este registro porque Fecha de Prorroga ya vencio");
                    }
                } else {
                    if (ConvertStringtoDate(fechaLimite) >= ConvertStringtoDate(fechaServer)) {
                        $("#div-edit").dialog("open");
                    } else {
                        MATRIZ.ShowAlert('dialog-alert', "No es posible Editar este registro porque Fecha Limite ya vencio");
                    }
                }
            });
        }

        function crearCombos() {

            var codPais = $("#<%=lblPais.ClientID %>").html();
            var tipo = $("#<%=lblTipo.ClientID %>").html();

            var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";

            MATRIZ.LoadDropDownList("ddlPais", url, { 'accion': 'pais', 'codPais': codPais, 'tipo': tipo }, 0, true, false);
            //MATRIZ.LoadDropDownList("ddlPeriodo", url, { 'accion': 'periodo', 'codPais': codPais, 'tipo': tipo }, 0, true, false);
            MATRIZ.ToolTipText();
        }

        function Cancelar() {
            $('input[name=hdId]').val("");
            $('#<%=txtFechaLimite.ClientID %>').val("");
            $('#<%=txtFechaProrroga.ClientID %>').val("");

            $("#div-edit").dialog("close");
            $("#div-del").dialog("close");
        }

        function Grabar() {
            var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
            var Id = $("input:hidden#hdId").val();
            var PrefijoIsoPais = $("#ddlPais").val();
            //var Periodo = $("#ddlPeriodo").val();
            var FechaLimite = $("#<%=txtFechaLimite.ClientID %>").val();
            var FechaProrroga = $("#<%=txtFechaProrroga.ClientID %>").val();
            var Usuario = $("#<%=lblUsuario.ClientID %>").html();

            var parametrosofs = { accion: 'obtenerFechaServer' };
            var obj = MATRIZ.Ajax(url, parametrosofs, false);

            var FechaServer = FormatearFecha(obj.FechaServer);

            if (FechaLimite != "") {

                if (FechaProrroga != "") {
                    if (ConvertStringtoDate(FechaProrroga) >= ConvertStringtoDate(FechaServer)) {

                        if (ConvertStringtoDate(FechaProrroga) >= ConvertStringtoDate(FechaLimite)) {
                            if (Id != "") {
                                updateCronograma(Id, PrefijoIsoPais, FechaLimite, FechaProrroga, Usuario);
                            }
                            else {
                                insertCronograma(PrefijoIsoPais, FechaLimite, FechaProrroga, Usuario);
                            }
                        } else {
                            if (Id != "") {
                                insertCronograma(PrefijoIsoPais, FechaLimite, FechaLimite, Usuario);
                            } else {
                                $("#div-edit").dialog("close");
                                MATRIZ.ShowAlert('dialog-alert', "Fecha de Prorroga debe ser mayor a la fecha limite");
                            }
                        }
                    } else {
                        $("#div-edit").dialog("close");
                        MATRIZ.ShowAlert('dialog-alert', "Fecha de Prorroga debe ser mayor a la fecha actual");
                    }
                } else {
                    if (ConvertStringtoDate(FechaLimite) >= ConvertStringtoDate(FechaServer)) {
                        if (Id != "") {
                            updateCronograma(Id, PrefijoIsoPais, FechaLimite, FechaProrroga, Usuario);
                        }
                        else {
                            insertCronograma(PrefijoIsoPais, FechaLimite, FechaProrroga, Usuario);
                        }
                    } else {
                        $("#div-edit").dialog("close");
                        MATRIZ.ShowAlert('dialog-alert', "La Fecha Limite debe ser mayor a la fecha actual");
                    }
                }

            } else {
                MATRIZ.ShowAlert('dialog-alert', "La Fecha Limite no puede estar vacia");
            }

            $("#div-edit").dialog("close");

            $('input[name=hdId]').val("");
            $('#<%=txtFechaLimite.ClientID %>').val("");
            $('#<%=txtFechaProrroga.ClientID %>').val("");

        }

        function Eliminar() {
            $(".del").live('click', function () {
                var Id = $(this).attr("id");

                $('input[name=hdId]').val(Id);
                $("#div-del").dialog("open");
            });
        }

        function Aceptar() {
            var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
            var Id = $("input:hidden#hdId").val();

            var parametros = { accion: 'deleteCronograma', id: Id };
            var estado = MATRIZ.Ajax(url, parametros, false);

            $("#div-del").dialog("close");

            $('input[name=hdId]').val("");
            $('#<%=txtFechaLimite.ClientID %>').val("");
            $('#<%=txtFechaProrroga.ClientID %>').val("");

            loadReportCronogramaMatriz();

            if (estado) {
                MATRIZ.ShowAlert('dialog-alert', "Se Elimino correctamente");
            }
            else {
                MATRIZ.ShowAlert('dialog-alert', "No se pudo eliminar, intente nuevamente");
            }
        }

        function Nuevo() {

            $('input[name=hdId]').val("");
            $('#<%=txtFechaLimite.ClientID %>').val("");
            $('#<%=txtFechaProrroga.ClientID %>').val("");
            $("#ddlPais").attr("disabled", false);
            $('#<%=txtFechaLimite.ClientID %>').datepicker("enable");
            $("#div-edit").dialog("open");
        }


        function updateCronograma(Id, PrefijoIsoPais, FechaLimite, FechaProrroga, Usuario) {

            $('<div></div>').appendTo('body')
                    .html('<div><h6>¿Desea continuar la operación?</h6></div>')
                    .dialog({
                        modal: true, title: 'Confirmar Operación', zIndex: 10000, autoOpen: true,
                        width: 'auto', resizable: false,
                        buttons: {
                            Confirmar: function () {

                                var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";;
                                var estado = false;
                                var parametros = { accion: 'updateCronograma', id: Id, PrefijoIsoPais: PrefijoIsoPais, FechaLimite: FechaLimite, FechaProrroga: FechaProrroga, Usuario: Usuario };
                                estado = MATRIZ.Ajax(url, parametros, false);
                                $(this).dialog("close");

                                if (estado) {
                                    MATRIZ.ShowAlert('dialog-alert', "La operación se realizo con exito");
                                    loadReportCronogramaMatriz();
                                }
                                else {
                                    MATRIZ.ShowAlert('dialog-alert', "*** No se pudo realizar la operación ***");
                                }

                            },
                            Cancelar: function () {
                                $(this).dialog("close");
                                return false;
                            }
                        },
                        close: function (event, ui) {
                            $(this).remove();
                        }
                    });
                }


                function insertCronograma(PrefijoIsoPais, FechaLimite, FechaProrroga, Usuario) {
                    $('<div></div>').appendTo('body')
                    .html('<div><h6>¿Desea continuar con la operación?</h6></div>')
                    .dialog({
                        modal: true, title: 'Confirmar Operación', zIndex: 10000, autoOpen: true,
                        width: 'auto', resizable: false,
                        buttons: {
                            Confirmar: function () {
                                var url = "<%=Utils.RelativeWebRoot%>Admin/Matriz/MatrizAdmin.ashx";
                                var estado = false;
                                var parametros = { accion: 'insertCronograma', PrefijoIsoPais: PrefijoIsoPais, FechaLimite: FechaLimite, FechaProrroga: FechaProrroga, Usuario: Usuario };
                                estado = MATRIZ.Ajax(url, parametros, false);
                                $(this).dialog("close");

                                if (estado) {
                                    MATRIZ.ShowAlert('dialog-alert', "La operación se realizo con exito");
                                    loadReportCronogramaMatriz();
                                }
                                else {
                                    MATRIZ.ShowAlert('dialog-alert', "*** No se pudo realizar la operación ***");
                                }

                            },
                            Cancelar: function () {
                                $(this).dialog("close");
                                return false;
                            }
                        },
                        close: function (event, ui) {
                            $(this).remove();
                        }
                    });
                }

                function Calendario() {
                    $('#<%=txtFechaLimite.ClientID %>').datepicker({
                        buttonImage: '<%=Utils.RelativeWebRoot%>Images/calendar.png',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        showOn: 'both',
                        dateFormat: 'dd/mm/yy'
                    });
                    $('#<%=txtFechaProrroga.ClientID %>').datepicker({
                        buttonImage: '<%=Utils.RelativeWebRoot%>Images/calendar.png',
                        buttonImageOnly: true,
                        changeMonth: true,
                        changeYear: true,
                        showOn: 'both',
                        dateFormat: 'dd/mm/yy'
                    });
                }
    </script>
</asp:Content>
