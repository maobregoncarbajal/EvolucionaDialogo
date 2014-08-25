<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistorialPeriodoCriticidadEval.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.PantallasModales.HistorialPeriodoCriticidadEval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/general.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 350px">
            <table width="100%">
                <tr>
                    <td align="center" class="texto_negrita">Historia:
                    <asp:Literal Text="text" runat="server" ID="lblNombreEvaluadora" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="dlHistorico" runat="server" Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="Label1" Text='<%#Eval("chrPeriodo") %>' runat="server" class="texto_Negro" />
                                <br />
                                <asp:TextBox Width="99%" ID="txtTextoIngresado" runat="server" CssClass="inputtext_criticas"
                                    Height="45px" Text='<%#Eval("vchVariableConsiderar")%>' TextMode="MultiLine"
                                    ReadOnly="true" Rows="5" MaxLength="600"></asp:TextBox>
                                <br />
                                <br />
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
