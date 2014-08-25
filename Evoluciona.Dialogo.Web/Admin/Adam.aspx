<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adam.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Admin.Adam" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/general.css?v4" rel="stylesheet" type="text/css" />
    <link href="../Styles/Menu.css?v4" rel="stylesheet" type="text/css" />
    <link href="../Styles/SimpleMenu.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/droplinetabs.css" rel="stylesheet" type="text/css" />
    <link href="../JQueryTheme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>

    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-ui-1.8.16.custom.min.js"
        type="text/javascript"></script>

    <style type="text/css">
        .Colaborador {
            padding: 0 5px;
        }
    </style>

    <script type="text/javascript">
        var cantidad = 0;

        jQuery(document).ready(function () {

            jQuery("#<%=txtDoc.ClientID %>").keypress(function (e) {
                var tecla = (document.all) ? e.keyCode : e.which;
                if (tecla == 8) return true; //Tecla de retroceso (para poder borrar)

                if (tecla >= 48 && tecla <= 57) {
                    return true;
                }

                if (tecla == 32) return false; //Tecla de espacio ()
                var patron = /[A-Z a-z]/; // Solo acepta letras
                var te = String.fromCharCode(tecla);
                return patron.test(te);
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="divFiltro" class="roundedDiv divFiltroPeriodo2" runat="server" visible="False"
            style="margin-top: 20px; height: 75px; margin-left: 30px; text-align: left; width: 600px;">
            <table style="margin: 5px; width: 610px; margin-top: 15px;">
                <tr style="height: 30px;">
                    <td>Documento o C&eacute;dula de Identidad:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDoc" runat="server" Width="200" MaxLength="20" />
                    </td>
                    <td></td>
                </tr>
                <tr style="height: 30px;">
                    <td>Pa&iacute;s:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboPaises" runat="server" Style="width: 200px" CssClass="combo">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnGuardar" Style="margin-left: 1px; height: 25px; width: 100px;"
                            Text="Consultar" OnClick="btnGuardar_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <br />
                        <small style="color: red; font-size: 8pt;">
                            <asp:Literal ID="litMensaje" runat="server" /></small>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:GridView ID="gvColaborador" runat="server" AutoGenerateColumns="False">
            <EmptyDataTemplate>
                <table>
                    <tr class="cabecera_procesos">
                        <th style="width: 100px;">C&oacute;digo de Planilla
                        </th>
                        <th style="width: 100px;">Documento Identidad Colaborador
                        </th>
                        <th style="width: 300px;">Nombres Evaluado
                        </th>
                        <th style="width: 160px;">Descripción Cargo Colaborador
                        </th>
                        <th style="width: 100px;">C&oacute;digo Planilla Jefe Inmediato
                        </th>
                        <th style="width: 300px;">Nombre Jefe Inmediato
                        </th>
                        <th style="width: 160px;">Descripción Cargo Jefe Inmediato
                        </th>
                    </tr>
                    <tr class="cabecera_procesos">
                        <td colspan="7" align="center" class="grilla_alterna_indicadores">No se encontraron resultados para la búsqueda actual...
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="cabecera_procesos" HorizontalAlign="Center" VerticalAlign="Middle"
                Font-Bold="False" />
            <RowStyle CssClass="grilla_alterna_indicadores" />
            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
            <Columns>
                <asp:BoundField HeaderText="Código de Planilla" DataField="CodigoPlanillaEvaluado">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Right" Width="100px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Documento Identidad Colaborador" DataField="DocumentoIdentidadColaborador">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Nombres Evaluado" DataField="NombresEvaluado">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Right" Width="300px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Descripción Cargo Colaborador" DataField="DescripcionCargoColaborador">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Right" Width="160px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Código Planilla Jefe Inmediaro" DataField="CodigoPlanillaJefeInmediato">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Nombre Jefe Inmediato" DataField="NombresJefeInmediato">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" Width="300px" CssClass="Colaborador" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Descripción Cargo Jefe Inmediato" DataField="DescripcionCargoJefe">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" Width="160px" CssClass="Colaborador" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
