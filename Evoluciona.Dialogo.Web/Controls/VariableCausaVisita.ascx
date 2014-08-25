<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VariableCausaVisita.ascx.cs"
    Inherits="Evoluciona.Dialogo.Web.Controls.VariableCausaVisita" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<table>
    <tr style="font-size: 12pt; font-family: Arial">
        <td style="width: 11px"></td>
        <td colspan="5">
            <br />
            <asp:Label ID="lblvariable1Desc" runat="server" Text="[Variable no Seleccionada]"
                CssClass="variables_indicadores"></asp:Label>
            &nbsp;<a href="<%=Utils.RelativeWebRoot%>Files/arbolVer.jpg" style="color: Black"
                class="linkPDF">ver+</a>
        </td>
        <td align="left">
            <asp:Label ID="lblvariable1" runat="server" Font-Bold="true" Style="display: none;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="7" style="height: 12px"></td>
    </tr>
    <tr style="font-size: 12pt; font-family: Arial">
        <td style="width: 11px"></td>
        <td></td>
        <td align="left">
            <asp:Label ID="Label1" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label4" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label5" runat="server" Text="Real" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label6" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label3" runat="server" Text="%" CssClass="variables"></asp:Label>
        </td>
    </tr>
    <tr style="font-size: 12pt; font-family: Arial">
        <td style="width: 11px"></td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlVariableCausa1"
                ErrorMessage="*">*</asp:RequiredFieldValidator>
        </td>
        <td align="center" style="width: 150px">
            <asp:DropDownList ID="ddlVariableCausa1" runat="server" Width="140px" CssClass="combo"
                OnDataBound="ddlVariableCausa1_DataBound" AutoPostBack="true" OnSelectedIndexChanged="ddlVariableCausa1_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox4" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
    </tr>
    <tr style="font-size: 12pt; font-family: Arial">
        <td style="width: 11px"></td>
        <td></td>
        <td align="left">
            <asp:Label ID="Label7" runat="server" Text="Variable Causa" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label8" runat="server" Text="Objetivo" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label9" runat="server" Text="Real" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label10" runat="server" Text="Diferencia" CssClass="variables"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="Label19" runat="server" Text="%" CssClass="variables"></asp:Label>
        </td>
    </tr>
    <tr style="font-size: 12pt; font-family: Arial">
        <td style="width: 11px"></td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVariableCausa2"
                ErrorMessage="*">*</asp:RequiredFieldValidator>
        </td>
        <td align="center" style="width: 150px">
            <asp:DropDownList ID="ddlVariableCausa2" runat="server" Width="140px" CssClass="combo"
                OnDataBound="ddlVariableCausa2_DataBound" AutoPostBack="true" OnSelectedIndexChanged="ddlVariableCausa2_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox5" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox6" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
        <td align="center" style="width: 120px">
            <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" CssClass="inputtext" Width="100px"></asp:TextBox>
        </td>
    </tr>
</table>
