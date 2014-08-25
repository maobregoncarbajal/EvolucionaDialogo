<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="visitaindicadores.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.visitaindicadores"
    Title="Indicadores" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionV.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<%@ Register Src="~/Controls/VariableCausaVisita.ascx" TagName="VariableCausaVisita"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
       
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;
        
        jQuery(document).ready(function () {
            
            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            
            jQuery(".linkPDF").click(function(event){
                jQuery.window({
                    showModal: true,
                    modalOpacity: 0.5,
                    title: "MAPA DE VARIABLES CAUSA",
                    url: this.href,
                    width: 420,
                    height: 430,
                    minimizable: false,
                    maximizable: false,
                    bookmarkable: false                    
                });

                event.preventDefault();
            });         
            
        });
        
        function AbrirMensaje1()
        {
            $("#divDialogo1").dialog({
                modal: true
            });
        }
        
        function AbrirMensaje2()
        {
            $("#divDialogo2").dialog({
                modal: true
            });
        }
    </script>

    <div style="margin-top: 35px;">
        <div id="contenidoPaso1" runat="server" style="margin: 15px 0 0 35px; text-align: left;">
            <table width="100%" cellpadding="0" cellspacing="0" style="border: solid 1px #c8c8c7; width: 750px;">
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td style="height: 10px; text-align: left">
                        <span class="subTituloMorado">Resultados de
                            <%=descRol %>
                            en las variables del negocio.</span><br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 20px;">
                        <div style="margin-bottom: 5px; margin-top: 5px; font-size: 13px;">
                            Objetivo de la Visita :
                        </div>
                        <asp:TextBox ID="txtObjetivosVisita" runat="server" CssClass="inputtext" Width="460px"
                            TextMode="MultiLine" Height="50px" MaxLength="600" Rows="5" Enabled="false"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 20px;">
                        <table style="text-align: left; padding-top: 10px; padding-bottom: 5px; width: 550px;">
                            <tr>
                                <td>
                                    <span class="texto_Negro">Período :</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cboPeriodosFiltro" Width="150px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboPeriodosFiltro_SelectedIndexChanged" Style="margin-left: 7px;" />
                                </td>
                                <td>
                                    <span class="texto_Negro">Campañas</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="cboCampanhasFiltro" Width="150px" Style="margin-left: 7px;" />
                                </td>
                                <td align="right">
                                    <asp:Button runat="server" CssClass="btnBuscarStyle" ID="btnBuscar" OnClick="btnBuscar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td align="center">
                        <asp:GridView ID="grdvVariablesBase" Width="700px" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="cabecera_indicadores" />
                            <RowStyle CssClass="grilla_indicadores" />
                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Variable de Enfoque">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbEstado" CssClass="clsChhIndicadores" name="cbEstado" runat="server"
                                            AutoPostBack="false" Checked='<%# Eval("bitEstado")  %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td style="text-align: left; padding-left: 20px;">
                        <br />
                        <span class="link2">Consultar Variables Adicionales</span>
                        <br />
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Arial">
                    <td align="center">
                        <asp:GridView ID="grdvVariablesAdicionales" Width="700px" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="cabecera_indicadores" />
                            <RowStyle CssClass="grilla_indicadores" />
                            <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Style="display: none" runat="server" Text='<%#Eval("chrCodVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Variables" DataField="vchDesVariable">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Meta" DataField="decValorPlanPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Resultado" DataField="decValorRealPeriodo" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Diferencia" DataField="Diferencia" DataFormatString="{0:N}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Campa&#241;a" DataField="chrAnioCampana">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Variable de Enfoque">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbEstado" name="cbEstado" runat="server" AutoPostBack="false" Checked='<%# Eval("bitEstado") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <asp:Button ID="btnGrabar" runat="server" Text="SIGUIENTE" OnClick="btnGrabar_Click"
                            CssClass="button" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div id="contenidoPaso2" runat="server" style="margin: 15px 0 0 35px; text-align: left;">
            <table>
                <tr>
                    <td>
                        <span class="subTituloMorado">Selecciona las variables de causa</span><br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; padding-left: 20px;">
                        <asp:Repeater ID="repVariablesCausales" runat="server">
                            <ItemTemplate>
                                <uc1:VariableCausaVisita ID="varibaleCausa" runat="server" CodigoVariable='<%# Eval("vchSeleccionado") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        <br />
                        <a href="visitaindicadores.aspx?indiceM=<%= indexMenuServer%>&indiceSM=3&paso=1">Regresar</a> &nbsp; 
                        <asp:Button runat="server" CssClass="button" ID="btnGuardar" OnClick="btnGuardar_Click"
                            Text="SIGUIENTE" />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div id="divMensaje1">
        <div style="display: none" id="divDialogo1" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/ANTES/NEGOCIO<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="visitaindicadores.aspx?indiceM=<%= indexMenuServer%>&indiceSM=2&paso=2">VISITA/ANTES/NEGOCIO</a>
        </div>
    </div>
    <div id="divMensaje2">
        <div style="display: none" id="divDialogo2" title="PROCESO GRABADO">
            <span>Haz completado tu proceso :</span><br />
            <p>
                VISITA/ANTES/NEGOCIO<br />
            </p>
            <span>Haz click aquí para continuar con el proceso: </span>
            <br />
            <a class="link" href="visitaindicadores2.aspx?indiceM=<%= indexMenuServer%>&indiceSM=3">VISITA/ANTES/EQUIPOS</a>
        </div>
    </div>
</asp:Content>
