<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encuesta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Encuestas.Encuesta" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Encuesta/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/Encuesta/Encuesta.js" type="text/javascript"></script>
    <link href="../../Jscripts/Encuesta/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Jscripts/Encuesta/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CssTdEncuesta {
            /*vertical-align: middle !important;*/
            background-image: url('../../Images/cabeEncuesta.jpg');
            background-repeat: no-repeat !important;
            height: 240px;
            background-position: center !important;
        }

        .cssBtnEncuesta {
            padding: 3px 4px !important;
            background: #cccccc !important;
            color: #000000 !important;
            -webkit-border-radius: 4px !important;
            -moz-border-radius: 4px !important;
            border-radius: 4px !important;
            text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.4) !important;
            -webkit-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.4), 0 1px 1px rgba(0, 0, 0, 0.2) !important;
            -moz-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.4), 0 1px 1px rgba(0, 0, 0, 0.2) !important;
            box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.4), 0 1px 1px rgba(0, 0, 0, 0.2) !important;
            text-decoration: none !important;
            font: normal 12px Arial !important;
        }

        .center {
            margin-left: 65px;
            margin-top: 110px;
            font-size: 19px;
        }
    </style>
    <script type="text/javascript">

        jQuery(document).ready(function () {
            crearPopUp();
            cargarEncuesta();
        });

        function cargarEncuesta() {
            var periodo = $('#<%=hfPeriodo.ClientID %>').val(),
            codTipoEncuesta = $('#<%=hfCodTipoEncuesta.ClientID %>').val(),
            url = "<%=Utils.RelativeWebRoot%>Desempenio/Encuestas/Encuesta.ashx";

            var paramCarga = { accion: 'loadEncuestaCompletar', periodo: periodo, codTipoEncuesta: codTipoEncuesta };
            var listaPreguntas = Encuesta.Ajax(url, paramCarga, false);

            var paramOpcionesRspts = { accion: 'loadOpcionesRspts' };
            var listaOpcionesRspts = Encuesta.Ajax(url, paramOpcionesRspts, false);

            if (listaPreguntas.length > 0) {

                //Crear Tabla

                //Limpiar Tabla
                $("#tblEncuesta thead").html("");
                $("#tblEncuesta tbody").html("");


                //Cabecera
                var titulo = "";
                titulo = titulo + "<tr>";
                titulo = titulo + "<th>Encuesta</th>";
                titulo = titulo + "</tr>";
                $(titulo).appendTo("#tblEncuesta thead");


                //Cuerpo
                var tabla = "";
                var numPregunta = 0;
                tabla = tabla + "<tr>";
                tabla = tabla + "<td class='CssTdEncuesta'>";

                switch (codTipoEncuesta) {
                    case 'DV DV GR':
                        tabla = tabla + "<div class='center'><p>Respecto a los diálogos evoluciona que acabas de terminar con tus<br />Gerentes Regionales. Por favor marca con una X tu percepción de cada acción.</p></div>";
                        break;
                    case 'GR GR GZ':
                        tabla = tabla + "<div class='center'><p>Respecto a los diálogos evoluciona que acabas de terminar con tus<br />Gerentes de Zona. Por favor marca con una X tu percepción de cada acción.</p></div>";
                        break;
                    case 'GR DV GR':
                        tabla = tabla + "<div class='center'><p>Evalúa el Diálogo Evoluciona que acabas de terminar con tu<br />Director de Ventas. Marcando con una X tu percepción de cada acción.</p></div>";
                        break;
                    case 'GZ GR GZ':
                        tabla = tabla + "<div class='center'><p>Evalúa el Diálogo Evoluciona que acabas de terminar con tu<br />Gerente Regional. Marcando con una X tu percepcion de cada acción.</p></div>";
                        break;
                }

                tabla = tabla + "</td>";
                tabla = tabla + "</tr>";


                $.each(listaPreguntas, function (i, v) {
                    numPregunta = numPregunta + 1;
                    tabla = tabla + "<tr>";
                    tabla = tabla + "<td>" + numPregunta + ". " + v.DesPreguntas + "<br /><br />";

                    //Respuesta
                    var rbRespuestas = "";

                    if (v.CodTipoRespuesta == "PUN") {
                        $.each(listaOpcionesRspts, function (o, w) {
                            rbRespuestas = rbRespuestas + "<input type='radio' style='width: 30px;height: 30px;vertical-align: middle;margin: 0px;' name='group" + v.IdEncuestaDialogo + "' value='" + w.CodPuntaje + "'><span>" + w.DesPuntaje + "</span>&nbsp&nbsp&nbsp";
                        });
                    } else if (v.CodTipoRespuesta == "COM") {
                        rbRespuestas = rbRespuestas + "<textarea name='Text" + v.IdEncuestaDialogo + "' id='Text" + v.IdEncuestaDialogo + "' cols='40' rows='4' ></textarea>";
                    }

                    tabla = tabla + rbRespuestas;

                    tabla = tabla + "<br /><br /></td>";
                    tabla = tabla + "</tr>";
                });
                $(tabla).appendTo("#tblEncuesta tbody");
            }
            else {
                //Encuesta.ShowAlert('dialog-alert', "No existen encuesta");
                alert('No existen encuesta');
            }
        }

        function crearPopUp() {
            var arrayDialog = [
                { name: "dialog-alert", height: 100, width: 200, title: "Alerta" }
            ];

            Encuesta.CreateDialogs(arrayDialog);
        }


        function Grabar() {

            var periodo = $('#<%=hfPeriodo.ClientID %>').val(),
            codTipoEncuesta = $('#<%=hfCodTipoEncuesta.ClientID %>').val(),
            url = "<%=Utils.RelativeWebRoot%>Desempenio/Encuestas/Encuesta.ashx",
            codigoUsuario = $('#<%=hfCodigoUsuario.ClientID %>').val();

            var paramCarga = { accion: 'loadEncuestaCompletar', periodo: periodo, codTipoEncuesta: codTipoEncuesta };
            var listaPreguntas = Encuesta.Ajax(url, paramCarga, false);

            if (groupChecked(listaPreguntas)) {
                $.each(listaPreguntas, function (i, v) {
                    if (v.CodTipoRespuesta == "PUN") {
                        var codPuntaje = $("input[name='group" + v.IdEncuestaDialogo + "']:checked").val();
                        var paramgrabarRspts = { accion: 'insertRspts', idEncuestaDialogo: v.IdEncuestaDialogo, codPuntaje: codPuntaje, comentario: '', codTipoEncuesta: codTipoEncuesta, codigoUsuario: codigoUsuario, periodo: periodo };
                        var listaOpcionesRspts = Encuesta.Ajax(url, paramgrabarRspts, false);

                    } else if (v.CodTipoRespuesta == "COM") {
                        var comentario = $("#Text" + v.IdEncuestaDialogo + "").val();
                        var paramgrabarRspts = { accion: 'insertRspts', idEncuestaDialogo: v.IdEncuestaDialogo, codPuntaje: '', comentario: comentario, codTipoEncuesta: codTipoEncuesta, codigoUsuario: codigoUsuario, periodo: periodo };
                        var listaOpcionesRspts = Encuesta.Ajax(url, paramgrabarRspts, false);
                    }


                });

                window.parent.closeWindow();

            } else {
                alert('Debe responder todas las preguntas');
                //Encuesta.ShowAlert('dialog-alert', "Debe responder todas las preguntas");
            }

        }

        function groupChecked(listaPreguntas) {
            var cantPreguntas = listaPreguntas.length;
            var cantRespuestas = 0;

            $.each(listaPreguntas, function (i, v) {

                if (v.CodTipoRespuesta == "PUN") {
                    if ($("input[name='group" + v.IdEncuestaDialogo + "']:checked").length > 0) {
                        cantRespuestas = cantRespuestas + 1;
                    }
                } else if (v.CodTipoRespuesta == "COM") {
                    if ($("#Text" + v.IdEncuestaDialogo + "").val() != "") {
                        cantRespuestas = cantRespuestas + 1;
                    }
                }
            });

            if (cantPreguntas == cantRespuestas) {
                return true;
            } else {
                return false;
            }
        }


    </script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfCodTipoEncuesta" runat="server" />
        <asp:HiddenField ID="hfCodigoUsuario" runat="server" />
        <asp:HiddenField ID="hfPeriodo" runat="server" />
        <div style="margin: 20px; padding: 0;">
            <table id="tblEncuesta" style="border: 0;" frame="void" rules="rows">
                <thead>
                </thead>
                <tbody>
                </tbody>
            </table>

            <div style="text-align: right">
                <input type="button" value="Continuar" id="btnGrabar" class="cssBtnEncuesta" onclick="Grabar();" />
            </div>

        </div>
        <div id="dialog-alert" style="display: none"></div>
    </form>
</body>
</html>
