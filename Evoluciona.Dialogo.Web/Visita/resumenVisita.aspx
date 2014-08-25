<%@ Page Language="C#" AutoEventWireup="true" Title="Iniciar Visitas" MasterPageFile="~/Visitas.Master"
    Theme="TemaDDesempenio" CodeBehind="resumenVisita.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.resumenVisita" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="../Jscripts/jquery.colorbox-min.js" type="text/javascript" language="javascript"></script>

    <link rel="Stylesheet" href="../Styles/colorbox.css" type="text/css" />

    <script language="javascript" type="text/javascript">
        function MostrarCrearVisita(codDI, codProceso) {
            if ($('#hdEsSoloLectura').val() == 'SI') {
                AbrirMensaje();
            }
            else {
                VerificarVisita(codDI, codProceso);
            }

        }
        function MostrarPostVisita(codDI, codProceso) {
            $.fn.colorbox({ href: "detalleVisita.aspx?idPro=" + codProceso + "&docu=" + codDI, width: "450px", height: "400px", iframe: true, opacity: "0.70", open: true, close: "" });
        }
        function MostrarConsultaVisita(codDI, codProceso) {
            $.fn.colorbox({ href: "consultaVisita.aspx?idPro=" + codProceso + "&docu=" + codDI, width: "800px", height: "400px", iframe: true, opacity: "0.70", open: true, close: "" });
        }
        function MostrarOpciones(idCelda) {
            var arrID = idCelda.split('_');
            document.getElementById(idCelda).className = 'opcionVisitaSelecionada';

            nombreOpcion = arrID[1];
            var divSelecionado = 'div' + nombreOpcion;
            document.getElementById(divSelecionado).style.display = '';
            switch (nombreOpcion) {
                case 'CrearVisita':
                    document.getElementById('td_PostVisita').className = 'opcionVisita';
                    document.getElementById('td_ConsultaVisita').className = 'opcionVisita';
                    document.getElementById('divPostVisita').style.display = 'none';
                    document.getElementById('divConsultaVisita').style.display = 'none';
                    break;
                case 'PostVisita':
                    document.getElementById('td_CrearVisita').className = 'opcionVisita';
                    document.getElementById('td_ConsultaVisita').className = 'opcionVisita';
                    document.getElementById('divCrearVisita').style.display = 'none';
                    document.getElementById('divConsultaVisita').style.display = 'none';
                    break;
                case 'ConsultaVisita':
                    document.getElementById('td_CrearVisita').className = 'opcionVisita';
                    document.getElementById('td_PostVisita').className = 'opcionVisita';
                    document.getElementById('divCrearVisita').style.display = 'none';
                    document.getElementById('divPostVisita').style.display = 'none';
                    break;
                default:
                    //code to be executed if n is different from case 1 and 2
            }
        }
        function IniciaVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario;
        }
        function IniciaPostVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario + '&postVisita=si';
        }
        function ConsultaVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario + '&consultaVisita=si';
        }
        function ConsultaPostVisita(idVisita, codigoUsuario, idRolUsuario) {
            location.href = 'resumenVisitaIniciar.aspx?codVisita=' + idVisita + '&codDocu=' + codigoUsuario + '&idRol=' + idRolUsuario + '&consultaVisita=si&consultaPostVisita=si';
        }
        function VerificarVisita(codDI, codProceso) {

            $.getJSON('AjaxVisita/VerificarVisita.aspx',
	                {
	                    docu: codDI,
	                    proceso: codProceso
	                },
	                 function (json) {

	                     if (json.existeVisita == 'SI') {
	                         IniciaVisita(json.idVisita, codDI, json.idRol);
	                     }
	                     else {
	                         $.fn.colorbox({ href: "crearVisita.aspx?idPro=" + codProceso + "&docu=" + codDI, width: "360px", height: "300px", iframe: true, opacity: "0.70", open: true, close: "" });
	                     }
	                 }
	               );
        }

        function AbrirMensaje() {
            $("#divDialogo").dialog({
                modal: true,
                buttons: {
                    'Aceptar': function () {
                        $(this).dialog('close');
                    }
                },
                close: function (ev, ui) {
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="95%" border="0" cellpadding="0" style="height: 100%; text-align: left; margin-left: 30px">
        <tr>
            <td valign="top">
                <div id="descripcion">
                    Te invitamos a realizar las visitas
                </div>
                <div id="subtitulos">
                    Filtro de Evaluadas
                </div>
                <div style="float: left; width: 430px; border-right: #c8c8c7 1px solid; border-top: #c8c8c7 1px solid; border-left: #c8c8c7 1px solid; border-bottom: #c8c8c7 1px solid;"
                    id="DivBuscar"
                    runat="server">
                    <br />
                    <asp:Label runat="server" ID="lblNombreEvaluada" Text="Nombre:" Width="100px" CssClass="texto_procesos"></asp:Label>
                    <asp:TextBox runat="server" ID="txtNombreEvaluada" Width="200px" CssClass="inputtext"
                        MaxLength="100"></asp:TextBox>
                    <br />
                    <asp:Label runat="server" ID="lblDocuIdent" Text="Rol Evaluada:" CssClass="texto_procesos"
                        Width="100px"></asp:Label>
                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="combo" Width="202px">
                    </asp:DropDownList>
                    <br />
                    <div style="text-align: right; width: 420px;">
                        <asp:Button runat="server" ID="btnBuscarProcesos" CssClass="button" Text="Buscar"
                            OnClick="btnBuscarProcesos_Click" TabIndex="4" CausesValidation="False" />
                    </div>
                    <br />
                </div>
                <br />
                <br />
            </td>
            <td rowspan="2">
                <img src="../Images/consul1.jpg" alt="" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <table width="60%" border="0" cellspacing="0" cellpadding="3">
                    <tr>
                        <td style="height: 22px;" id="td_CrearVisita" class="opcionVisitaSelecionada" onclick="javascript:MostrarOpciones('td_CrearVisita');">Crear Visitas
                        </td>
                        <td style="width: 4px;"></td>
                        <td id="td_PostVisita" class="opcionVisita" onclick="javascript:MostrarOpciones('td_PostVisita');">Realizar Post-Visita
                        </td>
                        <td style="width: 4px;"></td>
                        <td id="td_ConsultaVisita" class="opcionVisita" onclick="javascript:MostrarOpciones('td_ConsultaVisita');">Consulta Visita
                        </td>
                    </tr>
                </table>
                <table width="90%" cellpadding="0" cellspacing="0" border="1" rules="none" style="border-color: #c8c8c7; height: 300px">
                    <tr>
                        <td valign="top">
                            <br />
                            <div id="divCrearVisita" style="width: 580px">
                                <asp:GridView ID="gviewCrearVisita" runat="server" Width="580px" CellPadding="2"
                                    CellSpacing="4" ForeColor="#778391" GridLines="None" AllowPaging="True" PageSize="9"
                                    AutoGenerateColumns="False"
                                    OnPageIndexChanging="gviewCrearVisita_PageIndexChanging">
                                    <RowStyle CssClass="grilla_procesos" />
                                    <FooterStyle CssClass="footer_procesos" />
                                    <PagerStyle CssClass="footer_procesos" />
                                    <HeaderStyle CssClass="cabecera_visitaResumen" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                            <HeaderTemplate>
                                                <img src="../Images/punto.png" alt="" width="6px" height="6px" />
                                                Evaluadas
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <a class="link" href="javascript:MostrarCrearVisita('<%#Eval("codigoUsuario") %>','<%#Eval("idProceso") %>');">
                                                    <%#Eval("nombreEvaluado") %></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="250px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cantVisitasIniciadas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Iniciadas">
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidadVisitasCerradas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Cerradas">
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="divPostVisita" style="display: none; width: 560px">
                                <asp:GridView ID="gviewPostVisita" runat="server" CellPadding="2" CellSpacing="4"
                                    ForeColor="#778391" GridLines="None" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gviewPostVisita_PageIndexChanging">
                                    <RowStyle CssClass="grilla_procesos" />
                                    <FooterStyle CssClass="footer_procesos" />
                                    <PagerStyle CssClass="footer_procesos" />
                                    <HeaderStyle CssClass="cabecera_visitaResumen" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                            <HeaderTemplate>
                                                <img src="../Images/punto.png" alt="" width="6px" height="6px" />
                                                Evaluadas
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <a class="link" href='javascript:MostrarPostVisita("<%#Eval("codigoUsuario") %>",<%#Eval("idProceso") %>);'>
                                                    <%#Eval("nombreEvaluado")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cantVisitasIniciadas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Nº de Visitas">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="divConsultaVisita" style="display: none; width: 580px">
                                <asp:GridView ID="gviewConsultaVista" runat="server" Width="580px" CellPadding="2" CellSpacing="4"
                                    ForeColor="#778391" GridLines="None" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gviewConsultaVista_PageIndexChanging">
                                    <RowStyle CssClass="grilla_procesos" />
                                    <FooterStyle CssClass="footer_procesos" />
                                    <PagerStyle CssClass="footer_procesos" />
                                    <HeaderStyle CssClass="cabecera_visitaResumen" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                            <HeaderTemplate>
                                                <img src="../Images/punto.png" alt="" width="6px" height="6px" />
                                                Evaluadas
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <a class="link" href='javascript:MostrarConsultaVisita("<%#Eval("codigoUsuario") %>",<%#Eval("idProceso") %>);'>
                                                    <%#Eval("nombreEvaluado") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cantVisitasIniciadas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Iniciadas">
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidadVisitasCerradas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Cerradas">
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="demo">
        <div style="display: none" id="divDialogo" title="Información">
            <p>
                <asp:Label runat="server" ID="lblMensajes" Text="" CssClass="texto_mensaje"></asp:Label>
            </p>
        </div>
    </div>
    <div>
        <asp:HiddenField runat="server" ID="hdEsSoloLectura" Value="" />
    </div>
</asp:Content>
