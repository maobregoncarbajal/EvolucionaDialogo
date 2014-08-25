<%@ Page Language="C#" Theme="TemaDDesempenio" AutoEventWireup="true" CodeBehind="crearVisita.aspx.cs"
    Inherits="Evoluciona.Dialogo.Web.Visita.crearVisita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crear Visitas</title>
    <script src="../Jscripts/jquery-1.6.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Jscripts/jquery-ui-1.8.14.custom.min.js"></script>
    <link type="text/css" href="../Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            if ($('#lblMensajes').html() != '') {
                AbrirMensaje();
            }
        });

        function IniciarVisita(idVisita, codigoUsuario, idRolUsuario) {

            parent.IniciaVisita(idVisita, codigoUsuario, idRolUsuario);
            parent.$.fn.colorbox.close();

        }


        function AbrirMensaje() {
            $("#divDialogo").dialog({
                modal: true,
                buttons: {
                    'Aceptar': function () {
                        $(this).dialog('close');
                    }

                },
                close: function (ev, ui) {
                }
            });

        }
    </script>
</head>
<body style="background-color: White; margin-left: 60px; margin-top: 30px">
    <form id="form1" runat="server">
        <div class="demo">
            <div style="DISPLAY: none" id="divDialogo" title="Informacion">
                <p>
                    <asp:Label runat="server" ID="lblMensajes" CssClass="textoError"></asp:Label><br />
                </p>

            </div>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="text-align: center">
            <tr>
                <td>
                    <img src="../Images/pop_up_crearVisita.jpg" alt="" /></td>
            </tr>
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td>
                    <table border="1" cellpadding="4" cellspacing="4" width="100%" style="border-collapse: collapse">
                        <tr>
                            <td><span class="texto_procesos">Campaña :</span></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtCampania" CssClass="inputtext" MaxLength="6"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan='2'><span class="texto_procesos">Fecha :</span>
                                <asp:Label runat="server" ID="lblFecha" CssClass="textoGris"></asp:Label>
                                <span class="texto_procesos">Correlativo :</span>
                                <asp:Label runat="server" ID="lblCorrelativo" CssClass="textoGris"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <br />
                    <asp:Button runat="server" ID="btnCrearVisita" Text="Iniciar Visita" CssClass="button" OnClick="btnCrearVisita_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdIdRol" runat="server" />
    </form>
</body>
</html>
