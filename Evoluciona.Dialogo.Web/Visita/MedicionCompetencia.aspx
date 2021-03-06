<%@ Page Theme="TemaDDesempenio" Language="C#" MasterPageFile="~/Visitas.Master"
    AutoEventWireup="true" CodeBehind="MedicionCompetencia.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Visita.MedicionCompetencia"
    Title="Medicion de competencias" %>

<%@ Register Src="~/Controls/HeaderPaginasOperacionV.ascx" TagName="HeaderPaginasOperacion"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:HeaderPaginasOperacion ID="hderOperaciones" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../Jscripts/jquery-ui-1.8.16.custom.min.js"></script>

    <script type="text/javascript" language="javascript">
        var indexMenu = <%= indexMenuServer%>;
        var indexSubMenu = <%= indexSubMenu %>;
        
        $(document).ready(function(){

            if (indexMenu != null && indexMenu != 0) {
                SeleccionarLinks(indexMenu, indexSubMenu, true);
            }
            jQuery(".checkdesabilitado, .checkdesabilitado textarea").click(function(evento)
            {
                evento.preventDefault();
            });   
         
            jQuery(".checkdesabilitado textarea").attr("readOnly", "readOnly");
           
       
        }
         );
                    

        function AbrirMensaje()
        {
            $("#divDialogo").dialog({
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
        function AbrirNavegacion()
        {
    
            $("#divNavegacion").dialog({
                modal: true
            });
        }
    </script>

    <div style="margin-top: 35px;">
        <table width="100%" border="0" style="border: solid 1px #c8c8c7;">
            <tr>
                <td style="vertical-align: text-top; text-align: left">
                    <br />
                    <span class="subTituloMorado">Medici�n de competencias.</span><br />
                    <br />
                    <table cellspacing="2" cellpadding="0" border="0">
                        <tr>
                            <td style="width: 350px; vertical-align: top; text-align: center">
                                <asp:GridView ID="grvMedicionCompetencia" Width="600px" runat="server" AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="cabecera_indicadores" />
                                    <RowStyle CssClass="grilla_indicadores" />
                                    <AlternatingRowStyle CssClass="grilla_alterna_indicadores" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Descripcion Competencia" DataField="vchCompetencia">
                                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="% de Avance" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Label ID="PorcentajeAvance" runat="server" Text='<%# GetDescripcion(Eval("PorcentajeAvance")) %>'
                                                    CssClass="clsChhIndicadores" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Enfoque">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" CssClass="clsChhIndicadores" Width="200" name="CheckBox1"
                                                    runat="server" AutoPostBack="false" Checked='<%# bool.Parse(Eval("Enfoque").ToString())%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                                <asp:Button ID="btnGrabar" runat="server" Text="SIGUIENTE" OnClick="btnGrabar_Click"
                                    CssClass="button" />
                            </td>
                            <td style="width: 380px; vertical-align: top">
                                <img src="../Images/image_desarrollate.jpg" alt='' />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center"></td>
            </tr>
        </table>
    </div>
    <div style="display: none" id="divDialogo" title="Informaci�n">
        <p>
            <asp:Label runat="server" ID="lblMensajes" Text="" CssClass="texto_mensaje"></asp:Label>
        </p>
    </div>
    <div style="display: none" id="divNavegacion" title="PROCESO GRABADO">
        <span>Haz completado tu proceso :</span><br />
        <p>
            VISITA/ANTES/COMPETENCIA<br />
        </p>
        <span>Haz click aqu� para continuar con el proceso: </span>
        <br />
        <a class="link" href="PlanAccionVisita.aspx?indiceM=1&indiceSM=2">VISITA/ANTES/NEGOCIO</a>
    </div>
</asp:Content>
