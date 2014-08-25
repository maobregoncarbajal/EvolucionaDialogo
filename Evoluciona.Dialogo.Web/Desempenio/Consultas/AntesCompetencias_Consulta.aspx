<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AntesCompetencias_Consulta.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Desempenio.Consultas.AntesCompetencias_Consulta" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/general.css" rel="stylesheet" type="text/css" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.6.2.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#<%=txtObservacion.ClientID %>").attr("readOnly", "readOnly");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 15px 0 0 55px; text-align: left">
            <asp:TextBox runat="server" ID="txtIdProceso" Style="display: none" />
            Si deseas ingresa tus observaciones de acuerdo al plan mostrado. Ayudará mucho tu
        opinión.
        <br />
            <br />
            <asp:GridView ID="gvPlanAnual" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Width="650px">
                <Columns>
                    <asp:BoundField DataField="Competencia" HeaderText="Competencia">
                        <ItemStyle CssClass="grilla_plan" Width="120px" />
                        <HeaderStyle Width="120px" CssClass="texto_descripciones" />
                    </asp:BoundField>
                    <asp:BoundField DataField="comportamiento" HeaderText="Comportamiento">
                        <ItemStyle CssClass="grilla_alternativa_plan" />
                        <HeaderStyle CssClass="texto_descripciones" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sugerencia" HeaderText="Sugerencia">
                        <ItemStyle CssClass="grilla_plan" Width="120px" />
                        <HeaderStyle Width="120px" CssClass="texto_descripciones" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="cabecera_plan" />
            </asp:GridView>
            <br />
            <br />
            <span class="subtituloPlan">Observaciones :</span>
            <br />
            <asp:TextBox ID="txtObservacion" runat="server" CssClass="inputtext" Width="650px"
                TextMode="MultiLine" Height="100px" MaxLength="600" Rows="5" />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
