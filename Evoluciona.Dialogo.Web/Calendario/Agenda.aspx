<%@ Page Title="Calendario" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Agenda.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Calendario.Agenda" %>

<%@ Register Src="Visualizador.ascx" TagName="visualizador" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Jscripts/ui.datepicker-es.js" type="text/javascript"></script>

    <script type="text/javascript">

        jQuery(document).ready(function () {

            $('.datepicker').datepicker({
                showOn: "button",
                changeMonth: true,
                changeYear: true,
                buttonImage: "../Images/cal.png",
                buttonImageOnly: true,
                prevText: '<-',
                nextText: '->',
                currentText: 'Actual',
                closeText: 'Aceptar',
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
                    'Junio', 'Julio', 'Agosto', 'Septiembre',
                    'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
                    'Junio', 'Julio', 'Agosto', 'Septiembre',
                    'Octubre', 'Noviembre', 'Diciembre'],
            });

        });

        function validarFechas() {
            var esValido = true;
            var pattern = /^((([0][1-9]|[12][\d])|[3][01])[-\/]([0][13578]|[1][02])[-\/][1-9]\d\d\d)|((([0][1-9]|[12][\d])|[3][0])[-\/]([0][13456789]|[1][012])[-\/][1-9]\d\d\d)|(([0][1-9]|[12][\d])[-\/][0][2][-\/][1-9]\d([02468][048]|[13579][26]))|(([0][1-9]|[12][0-8])[-\/][0][2][-\/][1-9]\d\d\d)$/;
            var fechaInicio = document.getElementById('<%= txtFechaInicio.ClientID %>').value;
            var fechaFin = document.getElementById('<%= txtFechaFin.ClientID %>').value;

            if (fechaInicio == '' || fechaFin == '') {
                esValido = false;
            }

            if (!pattern.test(fechaInicio) || !pattern.test(fechaFin)) {
                esValido = false;
            }

            fechaInicio = fechaInicio.split('/');
            fechaFin = fechaFin.split('/');

            fechaInicio = new Date(fechaInicio[2], fechaInicio[1], fechaInicio[0]).valueOf();
            fechaFin = new Date(fechaFin[2], fechaFin[1], fechaFin[0]).valueOf();

            if (fechaInicio >= fechaFin) {
                alert('La primera fecha debe ser menor a la segunda.');
                esValido = false;
            }

            return esValido;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table style="margin-left: auto; margin-right: auto; width: 950px">
        <tr align="left">
            <td colspan="5">
                <uc:visualizador ID="visualizador" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <br />
            </td>
        </tr>
        <tr align="left">
            <td style="width: 100px;">Fecha desde el :
            </td>
            <td style="width: 130px;">
                <asp:TextBox ID="txtFechaInicio" runat="server" Width="100px" CssClass="datepicker"></asp:TextBox>
            </td>
            <td style="width: 60px;">hasta el :
            </td>
            <td style="width: 130px;">
                <asp:TextBox ID="txtFechaFin" runat="server" Width="100px" CssClass="datepicker"></asp:TextBox>
            </td>
            <td>
                <div style="font-size: 8pt">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only ui-button-text"
                        Text="Mostrar eventos" OnClick="btnBuscar_Click" OnClientClick="return validarFechas();"
                        Width="135px" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Repeater ID="repEventos" runat="server">
                    <HeaderTemplate>
                        <table style="margin-left: auto; margin-right: auto; width: 790px">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td colspan="5">
                                <hr style='background-color: <%# DataBinder.Eval(Container.DataItem, "Color") %>; color: <%# DataBinder.Eval(Container.DataItem, "Color") %>; margin-top: 5px; margin-bottom: 5px;' />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 140px; color: #3366CC">
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "Fecha") %></b>
                            </td>
                            <td style="width: 10px"></td>
                            <td style="width: 80px; color: #666">
                                <%# DataBinder.Eval(Container.DataItem, "Hora") %>
                            </td>
                            <td style="width: 15px"></td>
                            <td align="left" style="color: #3366CC">
                                <a>
                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="5">
                                <br />
                                <hr style='background-color: #CCC; color: #CCC' />
                            </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
