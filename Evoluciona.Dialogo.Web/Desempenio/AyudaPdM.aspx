<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AyudaPdM.aspx.cs" Inherits="Evoluciona.Dialogo.Web.Desempenio.AyudaPdM" %>
<%@ Import Namespace="Evoluciona.Dialogo.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <link href="../Jscripts/bjqs/css/bjqs.css" rel="stylesheet" />
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/jquery-1.11.0.min.js"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/bjqs/bjqs-1.3.min.js"></script>
    <script src="<%=Utils.RelativeWebRoot%>Jscripts/bjqs/libs/jquery.secret-source.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div style="text-align: center;padding-top: 10px ;height: 20px;color: White; background-color: #60497B;">
        <b>Ayuda Registrar acuerdos</b>
    </div>
        <div style="text-align: right;padding-top: 10px">
        <a href="resumenProceso.aspx">
            <img src="../Images/regresar-dialogo-btn.png" alt="Regresar" Height="30px" align="right" />
        </a>
    </div>
    <div id="container">
        <!--  Outer wrapper for presentation only, this can be anything you like -->
      <div id="banner-slide">

        <!-- start Basic Jquery Slider -->
        <ul class="bjqs">
          <li><img src="../Images/ayudaPdMUno.jpg" title="Paso 1" /></li>
          <li><img src="../Images/ayudaPdMDos.jpg" title="Paso 2" /></li>
            <li><img src="../Images/ayudaPdMTres.jpg" title="Paso 2" /></li>
            <li><img src="../Images/ayudaPdMCuatroyCinco.jpg" title="Paso 3" /></li>
        </ul>
        <!-- end Basic jQuery Slider -->

      </div>
      <!-- End outer wrapper -->
        
      <!-- attach the plug-in to the slider parent element and adjust the settings as required -->
      <script>
          jQuery(document).ready(function ($) {

              $('#banner-slide').bjqs({
                  animtype: 'slide',
                  height: 320,
                  width: 620,
                  responsive: true,
                  randomstart: false
              });

          });
      </script>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
