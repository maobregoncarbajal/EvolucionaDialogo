<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="HomeMatriz.aspx.cs" Inherits="Evoluciona.Dialogo.Web.HomeMatriz" %>

<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>

<%@ Register Src="Controls/MenuMatriz.ascx" TagName="MenuMatriz" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Utils.RelativeWebRoot%>Styles/Matriz.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="contentma1">
        <div class="conte2ral">
            <uc1:MenuMatriz ID="MenuMatrizLink" runat="server" />
        </div>
        <div style="background-image: url(Styles/Matriz/fndhomematriz.png); width: 100%; height: 376px; background-repeat: no-repeat; background-position: right;">
            <div style="float: left; margin-left: 40px; margin-top: 37px; width: 588px; text-align: left;">
                <ul class="listaCuadrados">
                    <li class="tituloHomePlomo" style="text-align: justify;">
                        <asp:Label ID="lblTh" class="tituloHomePlomo2" runat="server" Text="Tienes hasta "></asp:Label>
                        <strong class="tituloHomeAzul">
                            <asp:Label class="tituloHomeAzul" ID="lblFechaRegistrarAcuerdos" runat="server" Text=""></asp:Label>
                        </strong>
                        <asp:Label ID="lblRa" class="tituloHomePlomo2" runat="server" Text=" para Registrar Acuerdos."></asp:Label>
                    </li>
                    <li class="tituloHomePlomo" style="text-align: justify;">Tiene <strong class="tituloHomeAzul">
                        <asp:Label class="tituloHomeAzul" ID="lblCantEvaluadosPlanMejora" runat="server" Text=""></asp:Label>
                    </strong>con plan de mejora.
                    </li>
                    <li class="tituloHomePlomo" style="text-align: justify;">Tiene <strong class="tituloHomeAzul">
                        <asp:Label class="tituloHomeAzul" ID="lblCantEvaluadosPlanReasignacion" runat="server" Text=""></asp:Label>
                    </strong>con plan de reasignación.
                    </li>
                    <li class="tituloHomePlomo" style="text-align: justify;">Tiene <strong class="tituloHomeAzul">
                        <asp:Label class="tituloHomeAzul" ID="lblporcentRecuperadosConPlanMejora" runat="server" Text=""></asp:Label>
                    </strong>de Recuperación de Colaboradores que estuvieron en Plan de Mejora y obtuvieron un desempeño favorable en la siguiente medición de desempeño.
                    </li>
                    <li class="tituloHomePlomo" style="text-align: justify;">Tiene <strong class="tituloHomeAzul">
                        <asp:Label class="tituloHomeAzul" ID="lblporcentIncrementoEstablesyProductivas" runat="server" Text=""></asp:Label>
                    </strong>estables y/o productivas.
                    </li>
                </ul>
            </div>
            <div style="float: left; margin-left: 20px; margin-top: 40px; width: 500px; text-align: left;">
                <p class="leyendaTituloHome">
                    Matriz Evoluciona
                </p>
                <br />
                <p class="descripcionHome" style="text-align: justify;">
                    En la Matriz Evoluciona se muestra información relevante para la Directora de Ventas, sirve como puerta de entrada  a todos los procesos disponibles. 
                Dentro de la misma, podrás consultar información que te permita evaluar cómo va tu equipo en las variables de venta, comparándolo con sus resultados en años anteriores. Como consecuencia de esta evaluación, podrás tomar decisiones que te ayudará a mejorar su productividad. También podrás consultar y confirmar los acuerdos que hayas registrado anteriormente.
                </p>
            </div>
        </div>
        <div style="margin-top: 5px; width: 100%; height: 19px; background-color: #6C1E73; color: #fff; font-size: 10px">
            <span style="float: right; margin: 3px 3px 0px 0px">2012 Belcorp. Derechos reservados</span>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
