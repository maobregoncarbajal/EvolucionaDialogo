<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="Interaccion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.Interaccion"
    Title="Interaccion" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionV.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery.maxlength.min.js"
        type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;
        var maximoMarcados = 1;

        $(document).ready(function () {
            
            $('#<%=txtOtros.ClientID %>').maxlength({max: 600, feedbackText: ''});
            $('#<%=txtObjetivosVisita.ClientID %>').maxlength({max: 600, feedbackText: ''});
            
            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            
            jQuery(".checkdesabilitado").click(function (evento) {
                evento.preventDefault();
            });
           
            jQuery(".chkAlternativa input:checkbox").click(function(event){
                var cantidadChecksMarcados = jQuery("#divContenedorChecks input:checked").size();

                if(cantidadChecksMarcados > maximoMarcados)
                    event.preventDefault();             
            });
        });

        function AbrirMensaje()
        {
            $("#divDialogo").dialog({
                modal: true
            });
        }
        
        function AbrirMensajeError()
        {
            $("#divMensajeError").dialog({
                modal: true,
                buttons: {
                    'Aceptar': function() { 
                        $(this).dialog('close');
                    }
                },
                close: function(ev, ui) {
                }
            });
        }
        
    </script>

    <div style="margin-top: 35px;">
        <table width="100%" border="0" style="border: solid 1px #c8c8c7;">
            <tr>
                <td style="width: 30px"></td>
                <td style="height: 600px; text-align: left">
                    <span class="subTituloMorado">Interacción a realizar.</span><br />
                    <table cellspacing="2" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 350px; vertical-align: top">
                                <div id="divContenedorChecks" style="padding-left: 10px; border: solid 1px #c8c8c7; margin-top: 20px;">
                                    <asp:TreeView ID="TreeViewCheck" NodeStyle-VerticalPadding="2px" runat="server" ShowCheckBoxes="All"
                                        ShowExpandCollapse="False" ForeColor="#6a288a" Font-Size="13px" Font-Names="Arial"
                                        Width="335px">
                                        <NodeStyle VerticalPadding="2px" CssClass="chkAlternativa" />
                                    </asp:TreeView>
                                    <asp:HiddenField ID="hidPlanAnual" runat="server" />
                                    <br />
                                </div>
                                <div style="margin-left: 10px;">
                                    <asp:Panel ID="pnlTodo" runat="server" Visible="true">
                                        <table border="0">
                                            <tr>
                                                <td class="subtituloPlan">Alternativas Adicionales
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtOtros" runat="server" CssClass="inputtext" Width="335px" TextMode="MultiLine"
                                                        Height="80px" MaxLength="600" Rows="5" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </td>
                            <td style="width: 20px;"></td>
                            <td style="width: 450px; vertical-align: top">
                                <table border='0' cellpadding='0' style="width: 100%">
                                    <tr>
                                        <td>
                                            <div class="subtituloPlan" style="margin-bottom: 5px;">Objetivo de la Visita</div>
                                            <asp:TextBox ID="txtObjetivosVisita" runat="server" CssClass="inputtext" Width="450px"
                                                TextMode="MultiLine" Height="136px" MaxLength="600" Rows="5"></asp:TextBox>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="subtituloPlan" style="margin-bottom: 5px;">Medición de competencias</div>
                                            <asp:GridView ID="grvMedicionCompetencia" Width="450px" runat="server" AutoGenerateColumns="False">
                                                <HeaderStyle CssClass="cabecera_indicadores" />
                                                <RowStyle CssClass="grilla_indicadores" />
                                                <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Competencia" DataField="vchCompetencia">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="% de Avance" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PorcentajeAvance" runat="server" Text='<%# GetDescripcion(Eval("PorcentajeAvance")) %>'
                                                                CssClass="clsChhIndicadores" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enfoque">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" CssClass="clsChhIndicadores" name="CheckBox1"
                                                                runat="server" AutoPostBack="false" Checked='<%# bool.Parse(Eval("Enfoque").ToString())%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="bottom" align="right">
                                            <br />
                                            <asp:Button ID="btnGrabar" runat="server" Text="SIGUIENTE" OnClick="btnGrabar_Click"
                                                CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 327px">
                                <img src="../Images/image_jefe.jpg" style="width: 312px; height: 528px" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="hidden" id="hdOtro" name="hdOtro" style="width: 32px" />
                                <asp:HiddenField ID="hdEsEditable" runat="server" Value="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="divMensajeError" title="Información">
        <p>
            <asp:Label runat="server" ID="lblMensajes" Text="" CssClass="texto_mensaje"></asp:Label>
        </p>
    </div>
    <div class="demo">
        <div style="display: none" id="divDialogo" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/ANTES/COMPETENCIA<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="PlanAccionVisita.aspx?indiceM=1&indiceSM=2">VISITA/ANTES/NEGOCIO</a>
        </div>
    </div>
</asp:Content>
