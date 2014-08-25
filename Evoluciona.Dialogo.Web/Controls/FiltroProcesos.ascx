<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroProcesos.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.FiltroProcesos" %>
<div id="divProcesoDialogo" runat="server" style="height: 350px;">
    <div id="divEnviado" runat="server" class="divResumenProceso">
        <asp:GridView ID="gvInactivos" runat="server" CellPadding="4" ForeColor="#778391"
            Width="100%" GridLines="None" AutoGenerateColumns="False" CssClass="grillaPaginada"
            OnPreRender="gvPreRender">
            <HeaderStyle CssClass="cabecera_procesos" />
            <RowStyle CssClass="grilla_procesos" />
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                    <HeaderTemplate>
                        POR INICIAR
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMostrarDetalle" runat="server" CssClass="link" CommandArgument='<%# Eval("NombrePersona") + "," + Eval("CodigoUsuario")+ "," + Eval("IdProceso") + "," + Eval("IdRol") %>'
                            OnClick="btnMostrarDetalle_Click"><%# Eval("NombrePersona")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="250px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="divResumenProceso" id="DivProceso" runat="server">
        <asp:GridView ID="gvEnProceso" runat="server" CellPadding="4" ForeColor="#778391"
            CssClass="grillaPaginada" Width="100%" GridLines="None" AutoGenerateColumns="False"
            OnPreRender="gvPreRender">
            <HeaderStyle CssClass="cabecera_procesos" />
            <RowStyle CssClass="grilla_procesos" />
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                    <HeaderTemplate>
                        PROCESO
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMostrarDetalle" runat="server" CssClass="link" CommandArgument='<%# Eval("NombrePersona") + "," + Eval("CodigoUsuario")+ "," + Eval("IdProceso") + "," + Eval("IdRol") %>'
                            OnClick="btnMostrarDetalle_Click"><%# Eval("NombrePersona")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="250px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="divResumenProceso" id="divAprobacion" runat="server">
        <asp:GridView ID="gvEnAprobacion" runat="server" CellPadding="4" ForeColor="#778391"
            CssClass="grillaPaginada" Width="100%" GridLines="None" AutoGenerateColumns="False"
            OnPreRender="gvPreRender">
            <HeaderStyle CssClass="cabecera_procesos" />
            <RowStyle CssClass="grilla_procesos" />
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                    <HeaderTemplate>
                        APROBACIÓN
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMostrarDetalle" runat="server" CssClass="link" CommandArgument='<%# Eval("NombrePersona") + "," + Eval("CodigoUsuario")+ "," + Eval("IdProceso") + "," + Eval("IdRol") %>'
                            OnClick="btnMostrarDetalle_Click"><%# Eval("NombrePersona")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="250px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="divResumenProceso" style="border-right: white;" id="divAprobado" runat="server">
        <asp:GridView ID="gvAprobados" runat="server" CellPadding="4" ForeColor="#778391"
            CssClass="grillaPaginada" Width="100%" GridLines="None" AutoGenerateColumns="False"
            OnPreRender="gvPreRender">
            <HeaderStyle CssClass="cabecera_procesos" />
            <RowStyle CssClass="grilla_procesos" />
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                    <HeaderTemplate>
                        APROBADO
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMostrarDetalle" runat="server" CssClass="link" CommandArgument='<%# Eval("NombrePersona") + "," + Eval("CodigoUsuario")+ "," + Eval("IdProceso") + "," + Eval("IdRol") %>'
                            OnClick="btnMostrarDetalle_Click"><%# Eval("NombrePersona")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="250px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <br />
</div>
<div id="divProcesoVisitas" runat="server" style="width: 600px;">
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Crear Visitas</a></li>
            <li><a href="#tabs-2">Realizar Post-Visitas</a></li>
            <li><a href="#tabs-3">Consulta Visitas</a></li>
        </ul>
        <div id="tabs-1">
            <div id="divCrearVisita" style="width: 560px; text-align: left;">
                <asp:GridView ID="gviewCrearVisita" runat="server" Width="560px" CellPadding="2"
                    CellSpacing="4" ForeColor="#778391" GridLines="None" AutoGenerateColumns="False">
                    <RowStyle CssClass="grilla_procesos" />
                    <FooterStyle CssClass="footer_procesos" />
                    <PagerStyle CssClass="footer_procesos" />
                    <HeaderStyle CssClass="cabecera_visitaResumen" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                            <HeaderTemplate>
                                <img src="../Images/punto.png" alt="" width="6px" height="6px" />
                                Evaluadas
                                <br />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a class="link" href="javascript:MostrarCrearVisita('<%# Eval("CodigoUsuario") %>','<%#Eval("IdProceso") %>');">
                                    <%# Eval("NombrePersona") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CantidadVisitasIniciadas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Iniciadas">
                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CantidadVisitasCerradas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Cerradas">
                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="tabs-2">
            <div id="divPostVisita" style="width: 560px; text-align: left;">
                <asp:GridView ID="gviewPostVisita" runat="server" CellPadding="2" CellSpacing="4"
                    ForeColor="#778391" GridLines="None" AutoGenerateColumns="False">
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
                                <a class="link" href="javascript:MostrarPostVisita('<%#Eval("CodigoUsuario") %>','<%#Eval("IdProceso") %>');">
                                    <%#Eval("NombrePersona")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CantidadVisitasIniciadas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Nº de Visitas">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="tabs-3">
            <div id="divConsultaVisita" style="width: 560px; text-align: left;">
                <asp:GridView ID="gviewConsultaVista" runat="server" CellPadding="2" CellSpacing="4"
                    ForeColor="#778391" GridLines="None" AutoGenerateColumns="False">
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
                                <a class="link" href="javascript:MostrarConsultaVisita('<%#Eval("CodigoUsuario") %>','<%#Eval("IdProceso") %>');">
                                    <%#Eval("NombrePersona") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CantidadVisitasIniciadas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Iniciadas">
                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CantidadVisitasCerradas" HtmlEncode="false" HeaderText="<img src='../Images/punto.png' alt='' width='6px' height='6px'> Visitas Cerradas">
                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
<div id="divProcesoAmbos" runat="server" style="text-align: left; height: 350px;">
    <asp:GridView ID="gvProcesoAmbos" runat="server" Width="760px" CellPadding="2" CssClass="grillaPaginada"
        CellSpacing="4" ForeColor="#778391" GridLines="None" OnPreRender="gvPreRender"
        AutoGenerateColumns="False">
        <RowStyle CssClass="grilla_procesos" />
        <FooterStyle CssClass="footer_procesos" />
        <PagerStyle CssClass="footer_procesos" />
        <HeaderStyle CssClass="cabecera_procesos" />
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                <HeaderTemplate>
                    Evaluadas
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("NombrePersona") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Tipo" HtmlEncode="false" HeaderText="Proceso">
                <ItemStyle HorizontalAlign="Center" Width="170px" />
            </asp:BoundField>
            <asp:BoundField DataField="EstadoDescripcion" HtmlEncode="false" HeaderText="Estado">
                <ItemStyle HorizontalAlign="Center" Width="140px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</div>
