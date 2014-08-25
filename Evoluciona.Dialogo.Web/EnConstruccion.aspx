<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="EnConstruccion.aspx.cs" Inherits="Evoluciona.Dialogo.Web.EnConstruccion" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="border: solid 1px #c8c8c7; margin-left: auto; margin-right: auto" align="center">
            <tr>
                <td style="width: 100px" rowspan="4" valign="bottom" align="center">
                    <img src="<%=Utils.RelativeWebRoot%>Images/consul1.jpg" alt=""
                        width="250px" />
                </td>
            </tr>
            <tr>
                <td style="height: 74px;" align="center">
                    <span style="font-family: Arial; font-size: 12px; font-weight: bold; color: #6a288a;">
                        <br />
                        <br />
                        Estamos Trabajando para ti &nbsp;&nbsp;</span><br />
                    <span style="font-family: Arial; font-size: 15px; font-weight: bold; color: #6a288a;">En Construcción</span><br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <a class="texto" href="Index.aspx" style="text-decoration: none;">Retornar>>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
