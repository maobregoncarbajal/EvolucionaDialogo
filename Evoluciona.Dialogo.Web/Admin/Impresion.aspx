<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Impresion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Impresion" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../Jscripts/dropdownchecklist/jquery-1.4.2.min.js" type="text/javascript"></script>

    <link href="../Jscripts/dropdownchecklist/ui.dropdownchecklist.standalone.css" rel="stylesheet"
        type="text/css" />
    <link href="../Jscripts/dropdownchecklist/ui.dropdownchecklist.themeroller.css" rel="stylesheet"
        type="text/css" />
    <link href="../Jscripts/dropdownchecklist/jquery-ui-1.8.4.custom.css" rel="stylesheet"
        type="text/css" />

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/dropdownchecklist/jquery-ui-1.8.4.custom.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.AbsoluteWebRoot%>Jscripts/dropdownchecklist/ui.dropdownchecklist.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        //<![CDATA[
        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }
        //]]>

    </script>

    <script type="text/javascript">
        jQuery(document).ready(function () {

            $("#<%=ddlFiltroImpresion.ClientID %>").dropdownchecklist({
                firstItemChecksAll: true, emptyText: "[Selecciona Impresion]", maxDropHeight: 250, width: 300, onComplete: function (selector) {
                    var values = "";
                    for (var i = 0; i < selector.options.length; i++) {

                        if (selector.options[i].selected && (selector.options[i].value != "")) {

                            if (values != "") values += ",";

                            values += selector.options[i].text;

                        }

                    }

                    $('#<%=hdEvaluadosNombres.ClientID %>').val(values);

            }

            });

            $('#btnImprimir').click(function () {
                if ($("#<%=ddlFiltroImpresion.ClientID %>").val() == null || $("#<%=ddlFiltroImpresion.ClientID %>").val() == "") {
                    $('#<%=hdEvaluados.ClientID %>').val('');
                    var texto = $("#ddlFiltroImpresion option:selected").text();
                    if (texto == "TODOS") {
                        alert('No tiene ningun dialogo a imprimir');
                    } else {
                        alert('Seleccione Algún Registro');
                    }

                }
                else {
                    $('#<%=hdEvaluados.ClientID %>').val($("#<%=ddlFiltroImpresion.ClientID %>").val());
                    __doPostBack('lnkBuscar', '');
                }
            });

        });

        function ProcessOpen() {
            jQuery.blockUI({
                message: jQuery("#imgExporting"),
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: 'transparent',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    color: '#fff'
                }
            });
            $("#<%=lnkBuscar.ClientID %>").trigger("click");
        }

        function ProcessClose() {
            $.unblockUI({
                onUnblock: function () { }
            });
        }
    </script>

    <style type="text/css">
        .grayNormalLabelI {
            font-size: 9pt;
            color: #4c4c4c;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-variant: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlFiltroImpresion" runat="server" CssClass="grayNormalLabelI "
                            Width="161" Height="21" multiple="multiple">
                        </asp:DropDownList>
                        <input id="hdEvaluados" type="hidden" runat="server" />
                        <input id="hdEvaluadosNombres" type="hidden" runat="server" />
                    </td>
                    <td>
                        <input id="btnImprimir" type="button" value="Descargar" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td colspan="2">
                        <asp:LinkButton ID="lnkBuscar" runat="server" OnClick="lnkBuscar_Click">LinkButton</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
