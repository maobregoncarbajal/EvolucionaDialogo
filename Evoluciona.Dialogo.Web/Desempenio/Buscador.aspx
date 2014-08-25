<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Buscador.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.Buscador" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/FiltroProcesos.ascx" TagName="ControlFiltro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.colorbox-min.js"
        type="text/javascript" language="javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.dataTables.min.js"
        type="text/javascript"></script>

    <link rel="Stylesheet" href="../Styles/colorbox.css" type="text/css" />
    <script type="text/javascript">
        var urlVisitas = "<%=Utils.RelativeWebRoot%>Visita/";
        var tabIndex = <%= tabIndexActual%>;

        var registrosPagina = 10;

        jQuery(document).ready(function () {
            VerificarTextBox();

            jQuery("#<%=cboProceso.ClientID %>").change(function (event) {
                var valorActual = jQuery(this).val();

                jQuery("#<%=txtProceso.ClientID %>").attr("value", valorActual);

                AsignarValoresComboStatus(valorActual);
                jQuery("#<%=txtStatus.ClientID %>").attr("value", "-1");
            });

            jQuery("#cboEstatus").change(function () {
                jQuery("#<%=txtStatus.ClientID %>").attr("value", jQuery(this).val());
            });

            jQuery("#tabs").tabs({ selected: tabIndex });
            PaginarGrilla();
        });

        function VerificarTextBox() {
            var procesoActual = jQuery("#<%=txtProceso.ClientID %>").attr("value");

            if (procesoActual != "")
                AsignarValoresComboStatus(procesoActual);
            else {
                AsignarValoresComboStatus(-1);
                jQuery("#<%=txtStatus.ClientID %>").attr("value", "-1");
            }
        }

        function AsignarValoresComboStatus(valor) {
            if (valor == -1) {
                var opcionesHtml = "<option value='-1'>TODOS</option>";
                jQuery("#cboEstatus").html(opcionesHtml);
            }
            else if (valor == 0) {
                var opcionesHtml = "<option value='-1'>TODOS</option>" +
                                        "<option value='0'>Por Iniciar</option>" +
                                        "<option value='1'>En Proceso</option>" +
                                        "<option value='2'>En Aprobacion</option>" +
                                        "<option value='3'>Aprobado</option>";

                jQuery("#cboEstatus").html(opcionesHtml);
            }
            else if (valor == 1) {
                var opcionesHtml = "<option value='-1'>TODOS</option>" +
                                        "<option value='1'>Iniciadas</option>" +
                                        "<option value='2'>En PostVisita</option>";


                jQuery("#cboEstatus").html(opcionesHtml);
            }

            var statusValue = jQuery("#<%=txtStatus.ClientID %>").attr("value");
            if (statusValue != "") {
                jQuery("#cboEstatus").val(statusValue);
            }
        }

        function MostrarCrearVisita(codDI, codProceso) {
            VerificarVisita(codDI, codProceso);           
        }

        function MostrarPostVisita(codDI, codProceso) {
            jQuery.fn.colorbox({ href: urlVisitas + "detalleVisita.aspx?idPro=" + codProceso + "&docu=" + codDI, width: "450px", height: "400px", iframe: true, opacity: "0.70", open: true, close: "" });
        }

        function MostrarConsultaVisita(codDI, codProceso) {
            jQuery.fn.colorbox({ href: urlVisitas + "consultaVisita.aspx?idPro=" + codProceso + "&docu=" + codDI, width: "800px", height: "400px", iframe: true, opacity: "0.70", open: true, close: "" });
        }

        function IniciaVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = urlVisitas + 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario;
        }

        function IniciaPostVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = urlVisitas + 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario + '&postVisita=si';
        }

        function ConsultaVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = urlVisitas + 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario + '&consultaVisita=si';
        }

        function ConsultaPostVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = urlVisitas + 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario + '&consultaVisita=si&consultaPostVisita=si';
        }

        function VerificarVisita(codDI, codProceso) {
           
            jQuery.getJSON(urlVisitas + "AjaxVisita/VerificarVisita.aspx",
	            {
	                docu: codDI,
	                proceso: codProceso
	            },function (json) {
	                if (json.existeVisita == 'SI') {
	                    IniciaVisita(json.idVisita, codDI, json.idRol);
	                }
	                else {
	                    jQuery.fn.colorbox({ href: urlVisitas + "crearVisita.aspx?idPro=" + codProceso + "&docu=" + codDI, width: "360px", height: "300px", iframe: true, opacity: "0.70", open: true, close: "" });
	                }
	            });
        }

        function AbrirMensaje() {
            jQuery("#divDialogo").dialog({
                modal: true,
                buttons: {
                    'Aceptar': function () {
                        jQuery(this).dialog('close');
                    }
                }
            });
        } 
        
        function PaginarGrilla() {

            var totalGrillas = jQuery(".grillaPaginada").length;
            if (totalGrillas == 0) return;

            jQuery(".grillaPaginada").dataTable({
                "bJQueryUI": false,
                "bRetrieve": true,
                "sPaginationType": "full_numbers",
                "bAutoWidth": false,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": false,
                "iDisplayLength": registrosPagina,
                "bInfo": false,
                "oLanguage":
                    {
                        "sLengthMenu": "Mostrar _MENU_ registros por pagina",
                        "sZeroRecords": "Ningun Registro Encontrado - Disculpe",
                        "sInfo": "Mostrando _START_-_END_ de _TOTAL_ registros",
                        "sInfoFiltered": "(filtrado de un total de _MAX_ records)",
                        "sSearch": "Filtrar por todas las columnas",
                        "oPaginate":
                            {
                                "sFirst": "<|",
                                "sLast": "|>",
                                "sNext": ">>",
                                "sPrevious": "<<"
                            },
                        "sInfoEmpty": "No hay registros que mostrar"
                    }
            });

            jQuery(".grillaPaginada").each(function(index) {
                var padre = jQuery(this).parent();
                var hijos = jQuery(this).find("tBody tr");

                if (hijos.length < registrosPagina)
                {
                    padre.find(".dataTables_paginate").css("display", "none");
                }
            });
        }

        function CargarResumen(nombreEvaluado, codEvaluado, idProceso, rolEvaluado, codPais, codEvaluador, periodo) {
            
            var pathUrl = "<%=Utils.RelativeWebRoot%>Admin/ResumenProceso.aspx?nomEvaluado=" + nombreEvaluado + "&codEvaluado=" + codEvaluado + "&idProceso=" + idProceso + "&rolEvaluado=" + rolEvaluado + "&codPais=" + codPais + "&periodo=" + periodo + "&codEvaluador=" + codEvaluador;

            jQuery.window({
                showModal: true,
                modalOpacity: 0.5,
                title: "RESUMEN DE PROCESO",
                url: pathUrl,
                minimizable: false,
                maximizable: false,
                bookmarkable: false,
                resizable: false,
                width: 750,
                height: 500
            });

            return false;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox runat="server" ID="txtStatus" Style="display: none" />
    <asp:TextBox runat="server" ID="txtProceso" Style="display: none" />
    <div>
        <div style="float: left; padding: 0 0 0 45px">
            <img src="<%=Utils.RelativeWebRoot%>Images/Buscador.jpg" alt="Buscador"
                style="width: 350px" />
        </div>
        <div style="float: right; padding: 10px 35px 0 0">
            <asp:DropDownList runat="server" ID="cboPeriodos" CssClass="combo">
            </asp:DropDownList>
        </div>
        <br />
        <div class="roundedDiv divFiltroPeriodo" style="margin-top: 55px; height: 160px; margin-left: 45px; text-align: left;">
            <table style="margin: 5px; width: 100%; height: 100%">
                <tr>
                    <td>Rol:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboRoles" runat="server" Style="width: 175px" CssClass="combo">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Nombre:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNombre" Width="175px" class="combo" />
                    </td>
                </tr>
                <tr>
                    <td>Proceso:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboProceso" runat="server" Style="width: 175px" CssClass="combo">
                            <asp:ListItem Text="TODOS" Value="-1" />
                            <asp:ListItem Text="Dialogos" Value="0" />
                            <asp:ListItem Text="Visitas" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Status:
                    </td>
                    <td>
                        <select id="cboEstatus" style="width: 175px" class="combo">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnBuscar" CssClass="btnBuscarStyle" Style="margin-top: 8px; float: right; margin-right: 35px;"
                            OnClick="btnBuscar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 0 0 0 45px;">
            <br />
            <uc1:ControlFiltro ID="ctlContenedor" runat="server" OnMostrarDetalleProceso="ctlContenedor_MostrarDetalle" />
        </div>
    </div>
</asp:Content>
