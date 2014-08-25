<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Consulta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

        <style type="text/css">
    .header   {
         background-color:#3E3E3E;        
         font-family:Calibri;
         color:White;
         text-align:center;  
         line-height:25px;     
    }
    .rowstyle    {             
         font-family:Calibri;
         line-height:25px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="100px" 
                Width="860px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
    </div>
    </form>
</body>
</html>
